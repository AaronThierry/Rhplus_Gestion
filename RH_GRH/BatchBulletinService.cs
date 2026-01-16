using MySql.Data.MySqlClient;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RH_GRH
{
    /// <summary>
    /// Service de génération de bulletins de paie en lot pour toute une entreprise
    /// </summary>
    public class BatchBulletinService
    {
        /// <summary>
        /// Classe pour reporter la progression
        /// </summary>
        public class PrintProgress
        {
            public int Total { get; set; }
            public int Current { get; set; }
            public string CurrentEmployeeName { get; set; }
            public int Success { get; set; }
            public int Errors { get; set; }
            public string LastError { get; set; }
        }

        /// <summary>
        /// Résultat de l'impression en lot
        /// </summary>
        public class BatchPrintResult
        {
            public int TotalProcessed { get; set; }
            public int SuccessCount { get; set; }
            public int ErrorCount { get; set; }
            public List<string> GeneratedFiles { get; set; } = new List<string>();
            public List<string> Errors { get; set; } = new List<string>();
            public TimeSpan Duration { get; set; }
        }

        /// <summary>
        /// Données minimales d'un employé pour impression
        /// </summary>
        public class EmployePrintData
        {
            public int IdPersonnel { get; set; }
            public string Matricule { get; set; }
            public string NomPrenom { get; set; }
            public string TypeContrat { get; set; }
            public int IdEntreprise { get; set; }
        }

        /// <summary>
        /// Récupère la liste des employés d'une entreprise pour une période donnée
        /// </summary>
        public static List<EmployePrintData> GetEmployesEntreprise(
            int idEntreprise,
            DateTime periodeDebut,
            DateTime periodeFin,
            string typeContrat = null)
        {
            var employes = new List<EmployePrintData>();

            var connect = new Dbconnect();
            using (var con = connect.getconnection)
            {
                con.Open();

                string sql = @"
                    SELECT
                        p.id_personnel,
                        p.matricule,
                        p.nomPrenom,
                        p.typeContrat,
                        p.id_entreprise
                    FROM personnel p
                    WHERE p.id_entreprise = @idEntreprise
                    AND p.date_entree <= @periodeFin
                    AND (p.date_sortie IS NULL OR p.date_sortie >= @periodeDebut)";

                if (!string.IsNullOrEmpty(typeContrat) && typeContrat != "Tous")
                {
                    sql += " AND p.typeContrat = @typeContrat";
                }

                sql += " ORDER BY p.nomPrenom";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);
                    cmd.Parameters.AddWithValue("@periodeDebut", periodeDebut);
                    cmd.Parameters.AddWithValue("@periodeFin", periodeFin);

                    if (!string.IsNullOrEmpty(typeContrat) && typeContrat != "Tous")
                    {
                        cmd.Parameters.AddWithValue("@typeContrat", typeContrat);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employes.Add(new EmployePrintData
                            {
                                IdPersonnel = reader.GetInt32("id_personnel"),
                                Matricule = reader.IsDBNull(reader.GetOrdinal("matricule")) ? "" : reader.GetString("matricule"),
                                NomPrenom = reader.IsDBNull(reader.GetOrdinal("nomPrenom")) ? "" : reader.GetString("nomPrenom"),
                                TypeContrat = reader.IsDBNull(reader.GetOrdinal("typeContrat")) ? "" : reader.GetString("typeContrat"),
                                IdEntreprise = reader.GetInt32("id_entreprise")
                            });
                        }
                    }
                }
            }

            return employes;
        }

        /// <summary>
        /// Génère les bulletins en lot pour une liste d'employés
        /// </summary>
        public static async Task<BatchPrintResult> GenererBulletinsAsync(
            List<int> idsEmployes,
            DateTime periodeDebut,
            DateTime periodeFin,
            string dossierDestination,
            IProgress<PrintProgress> progress = null,
            CancellationToken cancellationToken = default)
        {
            var startTime = DateTime.Now;
            var result = new BatchPrintResult
            {
                TotalProcessed = idsEmployes.Count
            };

            var printProgress = new PrintProgress
            {
                Total = idsEmployes.Count,
                Current = 0,
                Success = 0,
                Errors = 0
            };

            // Créer le dossier s'il n'existe pas
            if (!Directory.Exists(dossierDestination))
            {
                Directory.CreateDirectory(dossierDestination);
            }

            // Créer un sous-dossier avec la période
            string periodeSafe = $"{periodeDebut:yyyy-MM-dd}_au_{periodeFin:yyyy-MM-dd}";
            string sousDossier = Path.Combine(dossierDestination, $"Bulletins_{periodeSafe}");

            if (!Directory.Exists(sousDossier))
            {
                Directory.CreateDirectory(sousDossier);
            }

            // Traiter chaque employé
            foreach (var idEmploye in idsEmployes)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                try
                {
                    printProgress.Current++;

                    // Charger les données de l'employé
                    var employe = EmployeService.GetEmployeDetails(idEmploye);
                    if (employe == null)
                    {
                        throw new Exception($"Employé ID {idEmploye} non trouvé");
                    }

                    printProgress.CurrentEmployeeName = employe.Nom;
                    progress?.Report(printProgress);

                    // Vérifier s'il existe des données de paie pour cette période
                    var snapshot = await Task.Run(() =>
                        RecupererOuCalculerSnapshot(idEmploye, periodeDebut, periodeFin),
                        cancellationToken);

                    if (snapshot == null)
                    {
                        throw new Exception($"Aucune donnée de paie trouvée pour {employe.Nom}");
                    }

                    // Générer le PDF
                    var model = ConvertirSnapshotEnBulletinModel(snapshot);

                    string nomFichier = $"{employe.Matricule}_{employe.Nom.Replace(" ", "_")}_{periodeSafe}.pdf";
                    string cheminComplet = Path.Combine(sousDossier, nomFichier);

                    var document = new BulletinDocument(model);
                    await Task.Run(() => document.GeneratePdf(cheminComplet), cancellationToken);

                    result.GeneratedFiles.Add(cheminComplet);
                    result.SuccessCount++;
                    printProgress.Success++;
                }
                catch (Exception ex)
                {
                    result.ErrorCount++;
                    printProgress.Errors++;

                    string errorMsg = $"Erreur pour employé ID {idEmploye}: {ex.Message}";
                    result.Errors.Add(errorMsg);
                    printProgress.LastError = errorMsg;
                }

                progress?.Report(printProgress);
            }

            result.Duration = DateTime.Now - startTime;
            return result;
        }

        /// <summary>
        /// Récupère ou calcule le snapshot de paie pour un employé
        /// IMPORTANT : Pour la version simplifiée, cette méthode génère un snapshot minimal
        /// Vous devrez stocker les snapshots dans la BDD pour une utilisation complète
        /// </summary>
        private static PayrollSnapshot RecupererOuCalculerSnapshot(
            int idEmploye,
            DateTime periodeDebut,
            DateTime periodeFin)
        {
            // TODO: Implémenter la récupération depuis une table de sauvegarde
            // Pour l'instant, on crée un snapshot minimal avec les données de l'employé

            var employe = EmployeService.GetEmployeDetails(idEmploye);
            if (employe == null) return null;

            string periode = $"{periodeDebut:dd/MM/yyyy} - {periodeFin:dd/MM/yyyy}";

            // Créer un snapshot minimal (sans calculs de paie)
            // L'utilisateur devra d'abord calculer les paies individuellement
            // puis les sauvegarder dans une table avant l'impression en lot
            return new PayrollSnapshot
            {
                IdEmploye = idEmploye,
                NomPrenom = employe.Nom ?? "",
                Civilite = employe.Civilite ?? "",
                Matricule = employe.Matricule ?? "",
                Poste = employe.Poste ?? "",
                NumeroEmploye = employe.TelephoneEmploye ?? "",
                AdresseEmploye = employe.Adresse ?? "",
                PeriodeSalaire = periode,
                Categorie = employe.Categorie ?? "",
                Service = employe.Service ?? "",
                Direction = employe.Direction ?? "",
                NumeroCnssEmploye = employe.NumeroCnssEmploye ?? "",
                Sexe = employe.Sexe ?? "",
                DureeContrat = employe.DureeContrat ?? "",
                HeureContrat = employe.HeureContrat,
                DateNaissance = employe.DateNaissance,
                DateEntree = employe.DateEntree,
                DateSortie = employe.DateSortie,
                Sigle = employe.Sigle ?? "",
                NomEntreprise = employe.NomEntreprise ?? "",
                TelephoneEntreprise = employe.TelephoneEntreprise ?? "",
                EmailEntreprise = employe.EmailEntreprise ?? "",
                AdressePhysiqueEntreprise = employe.AdressePhysiqueEntreprise ?? "",
                AdressePostaleEntreprise = employe.AdressePostaleEntreprise ?? "",
                ResponsableEntreprise = employe.ResponsableEntreprise ?? "",

                // Valeurs par défaut pour les calculs (à remplacer par les vraies valeurs)
                SalaireBrut = 0,
                SalaireNet = 0,
                SalaireNetaPayer = 0,

                // NOTE: Cette version génère des bulletins VIDES
                // Pour une utilisation complète, vous devez :
                // 1. Créer une table `paie_calculee` dans la BDD
                // 2. Sauvegarder les PayrollSnapshot après chaque calcul
                // 3. Modifier cette méthode pour les récupérer
            };
        }

        /// <summary>
        /// Convertit un PayrollSnapshot en BulletinModel
        /// </summary>
        private static BulletinModel ConvertirSnapshotEnBulletinModel(PayrollSnapshot snapshot)
        {
            return new BulletinModel
            {
                // Infos employé
                NomEmploye = snapshot.NomPrenom,
                Civilite = snapshot.Civilite,
                Matricule = snapshot.Matricule,
                Poste = snapshot.Poste,
                NumeroEmploye = snapshot.NumeroEmploye,
                AdresseEmploye = snapshot.AdresseEmploye,
                DateNaissance = snapshot.DateNaissance,
                DateDebut = snapshot.DateEntree,
                DateFin = snapshot.DateSortie,
                Sexe = snapshot.Sexe,
                NumeroCNSSEmploye = snapshot.NumeroCnssEmploye,
                DureeContrat = snapshot.DureeContrat,
                Contrat = snapshot.Contrat,

                // Infos entreprise
                Sigle = snapshot.Sigle,
                NomEntreprise = snapshot.NomEntreprise,
                TelephoneEntreprise = snapshot.TelephoneEntreprise,
                EmailEntreprise = snapshot.EmailEntreprise,
                AdressePhysiqueEntreprise = snapshot.AdressePhysiqueEntreprise,
                AdressePostaleEntreprise = snapshot.AdressePostaleEntreprise,
                ResponsableEntreprise = snapshot.ResponsableEntreprise,

                // Période
                Periode = snapshot.PeriodeSalaire,

                // Catégorie, Service, Direction
                Categorie = snapshot.Categorie,
                Service = snapshot.Service,
                Direction = snapshot.Direction,

                // Éléments de calcul - conversion decimal vers double
                baseUnitaire = (double)snapshot.BaseUnitaire,
                SalaireDeBase = (double)snapshot.SalaireBase,
                TauxSalaireDeBase = (double)snapshot.TauxSalaireDeBase,
                NbJourHeure = snapshot.HeureContrat,
                PrimeHeureSupp = (double)snapshot.PrimeHeuressupp,
                TauxHeureSupp = (double)snapshot.TauxHeureSupp,
                PrimeAnciennete = snapshot.PrimeAnciennete,
                Anciennete = snapshot.AncienneteStr,
                HeuresSup = snapshot.HeuresSupp,

                // Bruts
                SalaireBrut = (double)snapshot.SalaireBrut,

                // CNSS/TPA
                CnssEmploye = (double)snapshot.CNSS_Employe,
                CnssEmployeur = (double)snapshot.PensionEmployeur,
                RisqueProfessionnel = (double)snapshot.RisqueProEmployeur,
                PrestationFamiliale = (double)snapshot.PFEmployeur,
                Tpa = (double)snapshot.TPA,
                TauxTpa = (double)snapshot.TauxTPA,

                // IUTS
                BaseIUTS = (double)snapshot.BaseIUTS,
                Charges = snapshot.NombreCharges,
                Iuts = (double)snapshot.IUTS_Final,

                // Net
                SalaireNet = snapshot.SalaireNet,
                EffortDePaix = snapshot.EffortPaix,
                SalaireNetaPayer = snapshot.SalaireNetaPayer,
                SalaireNetaPayerFinal = snapshot.SalaireNetaPayer, // Même valeur si pas de dette
                ValeurDette = 0, // Par défaut

                // Logo (optionnel)
                LogoEntreprise = null // À charger depuis la BDD si disponible
            };
        }

        /// <summary>
        /// Génère un fichier ZIP contenant tous les bulletins
        /// </summary>
        public static string CreerArchiveZip(string dossierSource, string nomArchive = null)
        {
            if (string.IsNullOrEmpty(nomArchive))
            {
                nomArchive = $"Bulletins_{DateTime.Now:yyyy-MM-dd_HHmmss}.zip";
            }

            string cheminZip = Path.Combine(Path.GetDirectoryName(dossierSource), nomArchive);

            // Supprimer le ZIP s'il existe déjà
            if (File.Exists(cheminZip))
            {
                File.Delete(cheminZip);
            }

            // Créer l'archive
            System.IO.Compression.ZipFile.CreateFromDirectory(dossierSource, cheminZip);

            return cheminZip;
        }
    }
}
