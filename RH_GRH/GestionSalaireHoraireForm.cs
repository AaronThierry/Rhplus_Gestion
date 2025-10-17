using FastReport.Utils;
using MySql.Data.MySqlClient;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RH_GRH
{

    public partial class GestionSalaireHoraireForm : Form
    {
        // en haut du Form
        private PayrollSnapshot _lastSnapshot;

        public GestionSalaireHoraireForm()
        {
            InitializeComponent();
            InitPeriode();
            
        }





        /// <summary>
        /// *******************************************
        /// </summary>

        // À appeler après InitializeComponent()
        private void InitPeriode()
        {
            guna2DateTimePickerDebut.Format = DateTimePickerFormat.Custom;
            guna2DateTimePickerDebut.CustomFormat = "dd/MM/yyyy";
            guna2DateTimePickerFin.Format = DateTimePickerFormat.Custom;
            guna2DateTimePickerFin.CustomFormat = "dd/MM/yyyy";

            // La fin ne peut jamais être avant le début
            guna2DateTimePickerFin.MinDate = guna2DateTimePickerDebut.Value.Date;
        }










        /// <summary>
        /// Calcule le salaire de base par proratisation :
        /// salaireBase = (salaireCategoriel / unitesContractuellesTotales) * (unitesContractuellesTotales - unitesAbsences)
        /// </summary>
        /// <param name="salaireCategoriel">Salaire contractuel (mensuel) en devise.</param>
        /// <param name="unitesContractuellesTotales">
        /// Nombre total d’unités contractuelles sur la période (ex. heures/mois ou jours/mois).
        /// </param>
        /// <param name="unitesAbsences">Unités d’absence sur la même période et la même unité.</param>
        /// <param name="baseUnitaire">Retourne la valeur par unité (salaireCategoriel / unités totales).</param>
        /// <param name="unitesPayees">Retourne les unités dues (totales - absences, après clamp).</param>
        /// <param name="clampAbsences">
        /// Si true (défaut), borne les absences entre 0 et unités totales.
        /// Si false, lève une exception si absences hors bornes.
        /// </param>
        /// <returns>Salaire de base calculé.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Lancée si unités totales <= 0, ou si absences hors bornes avec clampAbsences=false.
        /// </exception>
        public static decimal CalculerSalaireBase(
        decimal salaireCategoriel,
        decimal unitesContractuellesTotales,
        decimal unitesAbsences,
        out decimal baseUnitaire,
        out decimal unitesPayees,
        bool clampAbsences = true)
        {
            if (unitesContractuellesTotales <= 0)
                throw new ArgumentOutOfRangeException(nameof(unitesContractuellesTotales),
                    "Le nombre d’unités contractuelles doit être > 0.");

            if (clampAbsences)
            {
                if (unitesAbsences < 0) unitesAbsences = 0;
                if (unitesAbsences > unitesContractuellesTotales)
                    unitesAbsences = unitesContractuellesTotales;
            }
            else
            {
                if (unitesAbsences < 0 || unitesAbsences > unitesContractuellesTotales)
                    throw new ArgumentOutOfRangeException(nameof(unitesAbsences),
                        "Les absences doivent être comprises entre 0 et les unités totales.");
            }

            baseUnitaire = salaireCategoriel / unitesContractuellesTotales;
            unitesPayees = unitesContractuellesTotales - unitesAbsences;
            decimal salaireBase = baseUnitaire * unitesPayees;

            return Math.Round(salaireBase, 2, MidpointRounding.AwayFromZero);
        }












        /// <summary>
        /// nbreHC = unités contractuelles (heures) de la période.
        /// hsNormJour = heures supp normales de jour (avec palier 8h: 1.15 puis 1.35)
        /// hsNormNuit = heures supp normales de nuit (1.50)
        /// hsFerieJour = heures supp férié/dimanche de jour (1.60)
        /// hsFerieNuit = heures supp férié/dimanche de nuit (2.20)
        /// </summary>
        /// 
        public static decimal CalculerHeuresSupp(
    int nbreHC,                 // heures contractuelles sur la période
    decimal salaireCategoriel,  // salaire de la période
    int hsNormJour,             // HS normales jour
    int hsNormNuit,             // HS normales nuit
    int hsFerieJour,            // HS férié/dimanche jour
    int hsFerieNuit)            // HS férié/dimanche nuit
        {
            if (nbreHC <= 0) return 0m;
            if (salaireCategoriel < 0m) return 0m;

            // clamp négatifs
            hsNormJour = Math.Max(hsNormJour, 0);
            hsNormNuit = Math.Max(hsNormNuit, 0);
            hsFerieJour = Math.Max(hsFerieJour, 0);
            hsFerieNuit = Math.Max(hsFerieNuit, 0);

            decimal scParHeure = salaireCategoriel / nbreHC;

            // paliers jour: 1..8 à 1.15 ; >8 à 1.35
            int palier1 = Math.Min(hsNormJour, 8);
            int palier2 = Math.Max(hsNormJour - 8, 0);

            decimal total = 0m;
            total += palier1 * (scParHeure * 1.15m);
            total += palier2 * (scParHeure * 1.35m);
            total += hsNormNuit * (scParHeure * 1.50m);
            total += hsFerieJour * (scParHeure * 1.60m);
            total += hsFerieNuit * (scParHeure * 2.20m);

            return Math.Round(total, 2, MidpointRounding.AwayFromZero);
        }





        //Somme indemnite 

        public static Dictionary<string, double> GetSommeIndemnitesParIds(int idEmploye)
        {
            const string sql =
                "SELECT " +
                "  SUM(CASE WHEN LOWER(type) IN ('logement numeraire','fonction','transport numeraire') " +
                "           THEN valeur ELSE 0 END) AS somme_numeraire, " +
                "  SUM(CASE WHEN LOWER(type) IN ('logement nature','transport nature','domesticité nationaux','domesticité étrangers','autres avantages') " +
                "           THEN valeur ELSE 0 END) AS somme_nature " +
                "FROM indemnite i " +
                "WHERE id_personnel = @emp;";


            var result = new Dictionary<string, double>
            {
                ["somme_numeraire"] = 0.0,
                ["somme_nature"] = 0.0
            };

            var connect = new Dbconnect();               // ta classe de connexion
            using (var con = connect.getconnection)      // ou connect.GetConnection()
            {
                con.Open();
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@emp", idEmploye);

                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            int iNum = rd.GetOrdinal("somme_numeraire");
                            int iNat = rd.GetOrdinal("somme_nature");

                            result["somme_numeraire"] = rd.IsDBNull(iNum) ? 0.0 : Convert.ToDouble(rd.GetDecimal(iNum));
                            result["somme_nature"] = rd.IsDBNull(iNat) ? 0.0 : Convert.ToDouble(rd.GetDecimal(iNat));
                        }
                    }
                }
            }
            return result;
        }








        //Prime Ancienete 
        /// <summary>
        /// //
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>






        /// <summary>
        /// Calcule la prime d'ancienneté pour un employé (par ID).
        /// Retourne la prime totale (decimal). Produit aussi une chaîne "ancienneteStr".
        /// </summary>
        public static decimal CalculerAncienneteEtPrime(
            int idEmploye,
            out string ancienneteStr)
        {
            ancienneteStr = string.Empty;

            // 1) Charger les infos employé (Date d'entrée, SGMM renseigné éventuel)
            // TODO: remplace par ton service/dal réel (Doit retourner DateEntree et SgmmRenseigne nullable)
            var emp = EmployeService.GetEmployeDetails(idEmploye);
            if (emp == null)
                throw new InvalidOperationException("Employé introuvable.");

            DateTime dateEntree = emp.DateEntree;           // ex: DateTime
            decimal? sgmmRenseigne = emp.SalaireMoyen;              // ex: decimal? ou null si non renseigné

            // 2) Ancienneté en mois (calcul précis, sans surestimer si le jour du mois n’est pas atteint)
            var today = DateTime.Today;
            int ancienneteMois = ((today.Year - dateEntree.Year) * 12) + (today.Month - dateEntree.Month);
            if (today.Day < dateEntree.Day) ancienneteMois -= 1;
            if (ancienneteMois < 0) ancienneteMois = 0;

            ancienneteStr = FormaterAnciennete(ancienneteMois);
            // Console.WriteLine("Ancienneté : " + ancienneteStr);

            // < 36 mois → prime = 0
            if (ancienneteMois < 36)
                return 0m;

            // 3) Prime de base (36e mois) : fenêtre 30–36
            decimal sgmm36 = CalculerSGMMFenetre(idEmploye, dateEntree, 36, sgmmRenseigne);
            decimal primeBase = Decimal.Round(sgmm36 * 0.05m, 2, MidpointRounding.AwayFromZero);

            decimal prime48 = 0m;
            decimal primeAnnuelle = 0m;

            if (ancienneteMois >= 48)
            {
                // 4) Prime au 48e mois : fenêtre 42–48
                decimal sgmm48 = CalculerSGMMFenetre(idEmploye, dateEntree, 48, sgmmRenseigne);
                prime48 = Decimal.Round(sgmm48 * 0.01m, 2, MidpointRounding.AwayFromZero);

                // 5) Années supplémentaires après 48 mois : +1%/an (basé sur SGMM48)
                int anneesSup = (ancienneteMois - 48) / 12;
                if (anneesSup > 0)
                    primeAnnuelle = Decimal.Round(sgmm48 * 0.01m * anneesSup, 2, MidpointRounding.AwayFromZero);
            }

            // 6) Prime totale
            decimal primeTotale = Decimal.Round(primeBase + prime48 + primeAnnuelle, 2, MidpointRounding.AwayFromZero);
            return primeTotale;
        }

        /// <summary>
        /// Formate "X an(s) Y mois", "X an(s)" ou "Y mois".
        /// </summary>
        private static string FormaterAnciennete(int mois)
        {
            int ans = mois / 12;
            int m = mois % 12;
            if (ans > 0 && m > 0) return ans + " an(s) " + m + " mois";
            if (ans > 0) return ans + " an(s)";
            return m + " mois";
        }

        /// <summary>
        /// Retourne le SGMM pour la fenêtre [cibleMois-6, cibleMois] à partir de dateEntree.
        /// Si sgmmRenseigne est fourni, on l'utilise en priorité (même logique que ton Java).
        /// Sinon, on va chercher la moyenne des salaires (ou SGMM) de la période.
        /// </summary>
        private static decimal CalculerSGMMFenetre(
            int idEmploye,
            DateTime dateEntree,
            int cibleMois,
            decimal? sgmmRenseigne)
        {
            if (sgmmRenseigne.HasValue && sgmmRenseigne.Value > 0m)
                return sgmmRenseigne.Value;

            // Fenêtre 6 mois glissants alignée sur la date d'entrée
            DateTime debut = dateEntree.AddMonths(cibleMois - 6); // inclus
            DateTime fin = dateEntree.AddMonths(cibleMois);     // inclus

            // TODO : remplace par ta vraie récupération de SGMM/Salaires sur la période.
            // Ex : moyenne des salaires bruts sur [debut..fin].
            // Ex d’appel : return PaieRepository.GetSgmmMoyen(idEmploye, debut, fin);

            return FetchSgmmMoyenFallback(idEmploye, debut, fin); // placeholder sûr (retourne 0 si pas de données)
        }

        // ====== PLACEHOLDER DAL ======
        // Implémente ceci selon ton schéma (table paie, colonnes, etc.)
        private static decimal FetchSgmmMoyenFallback(int idEmploye, DateTime debut, DateTime fin)
        {
            // Exemple vide : à remplacer par ta vraie requête SQL
            // SELECT AVG(sgmm) FROM paie WHERE id_employe=@emp AND date_paie BETWEEN @d1 AND @d2
            return 0m;
        }








        /// <summary>
        /// Salaire Brut
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>



            public static decimal CalculerSalaireBrut(decimal salaireBase,decimal primeHeuresSupp, decimal indemniteNumeraire,decimal indemniteNature,decimal primeAnciennete)
            {
                var brut = salaireBase + primeHeuresSupp + indemniteNumeraire + indemniteNature + primeAnciennete;
                return Math.Round(brut, 2, MidpointRounding.AwayFromZero);
            }










        /// <summary>
        /// Récupère les montants pour plusieurs noms en une seule requête.
        /// Retourne un dictionnaire { nom_indemnite -> montant } ; noms non trouvés sont absents (ou 0 avec l’option fillMissingAsZero).
        /// </summary>
        public static Dictionary<string, decimal> GetIndemnitesParNoms(int idEmploye, IEnumerable<string> noms, bool fillMissingAsZero = true)
        {
            var result = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
            var list = (noms ?? Enumerable.Empty<string>()).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            if (list.Count == 0) return result;

            // Construire des placeholders @n0, @n1, ...
            var placeholders = new List<string>();
            for (int i = 0; i < list.Count; i++) placeholders.Add("@n" + i);

            string sql = "SELECT type, valeur FROM indemnite WHERE id_personnel = @emp AND type IN (" + string.Join(",", placeholders) + ")";

            var connect = new Dbconnect();
            using (var con = connect.getconnection)
            {
                con.Open();
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@emp", idEmploye);
                    for (int i = 0; i < list.Count; i++)
                        cmd.Parameters.AddWithValue("@n" + i, list[i]);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            string nom = rd.IsDBNull(rd.GetOrdinal("type")) ? "" : rd.GetString("type");
                            decimal montant = rd.IsDBNull(rd.GetOrdinal("valeur")) ? 0m : rd.GetDecimal("valeur");
                            // si plusieurs lignes même nom, on additionne
                            if (result.ContainsKey(nom)) result[nom] += montant;
                            else result[nom] = montant;
                        }
                    }
                }
            }

            // Remplir les noms non trouvés à 0 si souhaité
            if (fillMissingAsZero)
            {
                foreach (var n in list)
                    if (!result.ContainsKey(n)) result[n] = 0m;
            }

            return result;
        }








        public static class DeductibilitesIndemnites
        {
            // ───────────────────────────────── Exonérations unitaires ─────────────────────────────────

            /// <summary>
            /// Exonération logement :
            /// - Numéraire seul : min(montant, min(20% * BS, 75 000))
            /// - Nature seul    : min(montant / 240, 75 000)
            /// - Sinon          : 0 (fidèle à ta logique "un seul type" actif)
            /// </summary>
            public static decimal CalculerExonerationLogement(
                decimal montantLogementNumeraire,
                decimal montantLogementNature,
                decimal salaireBrutSocial)
            {
                if (salaireBrutSocial <= 0m) return 0m;

                if (montantLogementNumeraire > 0m && montantLogementNature == 0m)
                {
                    decimal plafondPourcentage = 0.20m * salaireBrutSocial;
                    decimal plafondFixe = 75000m;
                    return Min(montantLogementNumeraire, Min(plafondPourcentage, plafondFixe));
                }
                else if (montantLogementNumeraire == 0m && montantLogementNature > 0m)
                {
                    decimal plafondFixe = 75000m;
                    return Min(montantLogementNature / 240m, plafondFixe);
                }

                return 0m;
            }

            /// <summary>
            /// Exonération transport :
            /// - Numéraire seul : min( min(5% * BS, 30 000), montant )
            /// - Nature seul    : min( min(5% * BS, 30 000), montant/240 )
            /// - Sinon          : 0
            /// </summary>
            public static decimal CalculerExonerationTransport(
                decimal montantTransportNumeraire,
                decimal montantTransportNature,
                decimal salaireBrutSocial)
            {
                if (salaireBrutSocial <= 0m) return 0m;

                decimal plafondPourcentage = 0.05m * salaireBrutSocial;
                decimal plafondFixe = 30000m;
                decimal montantBrut = 0m;

                if (montantTransportNumeraire > 0m && montantTransportNature == 0m)
                    montantBrut = montantTransportNumeraire;
                else if (montantTransportNumeraire == 0m && montantTransportNature > 0m)
                    montantBrut = montantTransportNature / 240m;
                else
                    return 0m;

                return Min(Min(plafondPourcentage, plafondFixe), montantBrut);
            }

            /// <summary>
            /// Exonération indemnité de fonction (numéraire) :
            /// min(montant, min(5% * BS, 50 000))
            /// </summary>
            public static decimal CalculerExonerationFonction(
                decimal montantFonctionNumeraire,
                decimal salaireBrutSocial)
            {
                if (montantFonctionNumeraire <= 0m || salaireBrutSocial <= 0m) return 0m;

                decimal plafondPourcentage = 0.05m * salaireBrutSocial;
                decimal plafondFixe = 50000m;
                return Min(montantFonctionNumeraire, Min(plafondPourcentage, plafondFixe));
            }

            /// <summary>
            /// Déductibilité totale = exonLogement + exonTransport + exonFonction
            /// </summary>
            public static decimal CalculerDeductibiliteIndemnites(
                decimal exonerationLogement,
                decimal exonerationTransport,
                decimal exonerationFonction)
            {
                return exonerationLogement + exonerationTransport + exonerationFonction;
            }

            // ───────────────────────────────── Orchestrateur pratique ─────────────────────────────────
            /// <summary>
            /// Calcule la déductibilité totale à partir des montants des indemnités et du salaire brut social,
            /// puis journalise en une ligne.
            /// </summary>
            public static decimal ComputeDeductibiliteTotale(
                decimal salaireBrutSocial,
                decimal logementNum, decimal logementNat,
                decimal transportNum, decimal transportNat,
                decimal fonctionNum)
            {
                var exLog = CalculerExonerationLogement(logementNum, logementNat, salaireBrutSocial);
                var exTrp = CalculerExonerationTransport(transportNum, transportNat, salaireBrutSocial);
                var exFct = CalculerExonerationFonction(fonctionNum, salaireBrutSocial);

                var total = CalculerDeductibiliteIndemnites(exLog, exTrp, exFct);

                Debug.WriteLine(
                    $"[DEDUCT] BSoc={salaireBrutSocial:N2} | Log(N)={logementNum:N2} Log( nat)={logementNat:N2} | " +
                    $"Trp(N)={transportNum:N2} Trp(nat)={transportNat:N2} | Fct(N)={fonctionNum:N2} | " +
                    $"ExLog={exLog:N2} ExTrp={exTrp:N2} ExFct={exFct:N2} | Total={total:N2}"
                );

                return total;
            }

            // Helper min decimal (équivalent à Math.min double côté Java)
            private static decimal Min(decimal a, decimal b) => a < b ? a : b;
        }












public static class IUTS
    {
        /// <summary>
        /// Calcule l'IUTS (brut par tranches) et l'IUTS final après réduction liée au nombre de charges.
        /// baseIUTS doit idéalement être déjà planché à la centaine (ex: floor( /100 )*100).
        /// </summary>
        /// <param name="baseIUTS">Base imposable IUTS</param>
        /// <param name="nombreCharges">Nombre de charges (1→8%, 2→10%, 3→12%, 4 ou +→14%)</param>
        /// <param name="iutsBrut">Sortie: IUTS avant réduction</param>
        /// <returns>IUTS final après réduction</returns>
        public static decimal Calculer(decimal baseIUTS, int nombreCharges, out decimal iutsBrut)
        {
            if (baseIUTS <= 0m)
            {
                iutsBrut = 0m;
                return 0m;
            }

            // Tranches (bornes incluses à gauche, exclusives à droite)
            decimal[] bornesInf = { 0m, 30100m, 50100m, 80100m, 120100m, 170100m, 250100m };
            decimal[] bornesSup = { 30000m, 50000m, 80000m, 120000m, 170000m, 250000m, decimal.MaxValue };
            decimal[] tauxPct = { 0.00m, 12.10m, 13.90m, 15.70m, 18.40m, 21.70m, 25.00m };

            decimal iuts = 0m;

            for (int i = 0; i < bornesInf.Length; i++)
            {
                if (baseIUTS < bornesInf[i]) break;

                decimal trancheDebut = bornesInf[i];
                decimal trancheFin = bornesSup[i];

                decimal plafond = baseIUTS < trancheFin ? baseIUTS : trancheFin;
                decimal montantTranche = plafond - trancheDebut;

                if (montantTranche > 0m)
                {
                    iuts += montantTranche * (tauxPct[i] / 100m);
                }
            }

            // Réduction selon charges
            decimal reduction = 0m;
            switch (nombreCharges)
            {
                case 1: reduction = 0.08m; break;
                case 2: reduction = 0.10m; break;
                case 3: reduction = 0.12m; break;
                default:
                    if (nombreCharges >= 4) reduction = 0.14m; // 4 ou plus
                    break;
            }

            iutsBrut = Math.Round(iuts, 2, MidpointRounding.AwayFromZero);
            decimal iutsFinal = Math.Round(iutsBrut * (1m - reduction), 2, MidpointRounding.AwayFromZero);

            // Debug compact (une ligne)
            Debug.WriteLine(
                $"[IUTS] Base={baseIUTS:N2} | Brut={iutsBrut:N2} | Charges={nombreCharges} (−{reduction:P0}) | Final={iutsFinal:N2}"
            );

            return iutsFinal;
        }
}



        //******************************************************************************************
        //******************************************************************************************
        //******************************************************************************************
        //**************//********************************//********************************************///
        /// <summary>
        /// /*******************************************
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>




        public sealed class PayrollSnapshot
        {
            // Identifiants
            public int IdEntreprise { get; set; }
            public int IdEmploye { get; set; }
            public string AncienneteStr { get; set; } = "";



            //Salaire Base
            public decimal BaseUnitaire { get; set; }
            public decimal SalaireBase { get; set; }
            public decimal TauxSalaireDeBase { get; set; }
            //****************************************


            //HEURES SUPPLEMENTAIRES
            public decimal PrimeHeuressupp { get; set; }
            public decimal TauxHeureSupp { get; set; }
            //******************************************


            //PRIME ANCIENNETE
            public decimal PrimeAnciennete { get; set; }
            //******************************************





            //Infos Employe 
            public string NomPrenom { get; set; } = "";
            public string Civilite { get; set; } = "";
            public string Poste { get; set; } = "";
            public string Matricule { get; set; } = "";
            public string NumeroEmploye { get; set; } = "";
            public string AdresseEmploye { get; set; } = "";
            public string PeriodeSalaire { get; set; } = "";
            public string Contrat { get; set; } = "";
            public string Sexe { get; set; } = "";
            public string DureeContrat { get; set; } = "";
            public int HeureContrat { get; set; }



            public string Sigle { get; set; } = "";
            public string NomEntreprise { get; set; } = "";
            public string TelephoneEntreprise { get; set; } = "";
            public string EmailEntreprise { get; set; } = "";
            public string AdressePhysiqueEntreprise { get; set; } = "";
            public string AdressePostaleEntreprise { get; set; } = "";


            public DateTime DateNaissance { get; set; } // Utilisation de DateTime pour les dates
            public DateTime DateEntree { get; set; } // Utilisation de DateTime pour les dates
            public DateTime? DateSortie { get; set; } // Nullable DateTime pour la sortie


            // Composantes de gains

            public decimal HeuresSupp { get; set; }
            public decimal IndemNum { get; set; }
            public decimal IndemNat { get; set; }

            // Totaux bruts / sociaux
            public decimal SalaireBrut { get; set; }
            public decimal SalaireBrutSocial { get; set; }

            // CNSS & TPA (employé / employeur)
            public decimal CNSS_Employe { get; set; }
            public decimal PensionEmployeur { get; set; }
            public decimal RisqueProEmployeur { get; set; }
            public decimal PFEmployeur { get; set; }
            public decimal CNSS_Employeur_Total { get; set; }
            public decimal TPA { get; set; }

            // IUTS
            public decimal DeductibiliteIndemnites { get; set; }
            public decimal BaseIUTS { get; set; }
            public decimal BaseIUTS_Arrondie { get; set; }
            public int NombreCharges { get; set; }
            public decimal IUTS_Brut { get; set; }
            public decimal IUTS_Final { get; set; }

            // Net
            public decimal SalaireNet { get; set; }

            // Méta (facultatif)
            public string Categorie { get; set; } = "";
            public string Direction { get; set; } = "";
            public string Service { get; set; } = "";
            public string NumeroCnssEmploye { get; set; } = "";
            public decimal TauxTPA { get; set; }
            public string StatutCadre { get; set; } = ""; // "oui"/"non"


        }







        public void RemplirIndemnites(Dictionary<string, object> para, int idEmploye)
        {
            var listeIndemnites = IndemniteClass.GetIndemnitesByEmploye(idEmploye);

            int maxIndemnites = 5; // Tu limites à 5 indemnités max, si tu veux plus, ajuste cette valeur
            for (int i = 0; i < Math.Min(listeIndemnites.Count, maxIndemnites); i++)
            {
                var ind = listeIndemnites[i];
                para.Add($"type{i + 1}", ind.NomIndemnite);
                para.Add($"valeur{i + 1}", ind.MontantIndemnite);
                para.Add($"taux_indemnite{i + 1}", ind.TauxIndem);
            }

        }




        //*******************************************************************************************
        //*******************************************************************************************
        //*******************************************************************************************








        private void GestionSalaireHoraireForm_Load(object sender, EventArgs e)
        {
            EntrepriseClass.ChargerEntreprises(ComboBoxEntreprise);
        }


        /*///////////////**////////////////**************/****/*****************////////////

        /// <summary>
        /// Récupère l'ID sélectionné d'un ComboBox même si SelectedValue est un DataRowView.
        /// </summary>
        private static int? GetSelectedIntOrNull(ComboBox combo, string valueColumnName)
        {
            if (combo.SelectedValue == null) return null;

            // Cas normal : déjà un int
            if (combo.SelectedValue is int i) return i;

            // Certaines configs renvoient un DataRowView
            if (combo.SelectedValue is DataRowView drv)
            {
                var val = drv[valueColumnName];
                return val == DBNull.Value ? (int?)null : Convert.ToInt32(val);
            }

            // Fallback: essayer de parser
            if (int.TryParse(combo.SelectedValue.ToString(), out var parsed))
                return parsed;

            return null;
        }

        private void ComboBoxEntreprise_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int? idEnt = GetSelectedIntOrNull(ComboBoxEntreprise, "id_entreprise");
            if (idEnt == null || idEnt.Value <= 0)
            {
                // rien de sélectionné / placeholder
                ComboBoxEmploye.DataSource = null;
                return;
            }
            ComboBoxEmploye.Enabled = true;
            // Charge les employés filtrés par entreprise
            EmployeClass.ChargerEmployesParEntrepriseHoraire(ComboBoxEmploye, idEnt.Value, null, true);

        }



        //**************//********************************//********************************************///


        private void ComboBoxEmploye_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Vérifier si un employé a été sélectionné (valeur valide, pas l'option par défaut)
            if (ComboBoxEmploye.SelectedIndex == -1 || ComboBoxEmploye.SelectedValue == null || Convert.ToInt32(ComboBoxEmploye.SelectedValue) == 0)
            {
                // Si aucune sélection n'est faite ou si la sélection est invalide (par exemple, l'option par défaut)
                return;
            }

            try
            {
                // Récupérer l'ID de l'employé sélectionné
                var selectedValue = ComboBoxEmploye.SelectedValue.ToString();
                int idEmploye;

                Console.WriteLine($"Valeur sélectionnée : {selectedValue}"); // Log de la valeur sélectionnée

                // Vérifier si la valeur peut être convertie en entier
                if (!int.TryParse(selectedValue, out idEmploye) || idEmploye <= 0)
                {
                    Console.WriteLine("L'ID de l'employé est invalide ou nul.");
                    MessageBox.Show("Sélectionnez un employé valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Récupérer les informations de l'employé sélectionné
                Console.WriteLine($"Récupération des informations de l'employé avec ID : {idEmploye}");
                var employe = EmployeService.GetEmployeDetails(idEmploye);


                if (employe != null)
                {
                    // Mettre à jour l'interface utilisateur avec les informations de l'employé
                    Console.WriteLine("Informations récupérées avec succès.");
                    textBoxPoste.Text = employe.Poste ?? string.Empty;
                    textBoxMatricule.Text = employe.Matricule ?? string.Empty;

                    // Exemple de récupération sécurisée pour d'autres champs
                    textBoxContrat.Text = employe.Contrat?.ToString() ?? string.Empty;
                    textBoxCategorie.Text = employe.Categorie?.ToString() ?? string.Empty;
                    textBoxNP.Text = employe.Nom ?? string.Empty;
                    textBoxtypeContrat.Text = employe.TypeContrat ?? string.Empty;
                    textBoxHcontrat.Text = employe.HeureContrat.ToString();
                    // Montant nullable
                    textBoxSalaire.Text = employe.Montant.HasValue? employe.Montant.Value.ToString("N2", CultureInfo.CurrentCulture): string.Empty;

                    var sums = GetSommeIndemnitesParIds(idEmploye);

                    // Exemples d’usage
                    System.Diagnostics.Debug.WriteLine($"Num={sums["somme_numeraire"]:N2} | Nat={sums["somme_nature"]:N2} FCFA");





                }
                else
                {
                    Console.WriteLine("Aucun employé trouvé avec l'ID spécifié.");
                    MessageBox.Show("L'employé sélectionné n'a pas été trouvé.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // En cas d'erreur, afficher un message d'erreur et loguer l'exception
                Console.WriteLine($"Erreur lors de la récupération des informations de l'employé : {ex.Message}");
                MessageBox.Show($"Erreur lors de la récupération des informations de l'employé : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {



        }

        private void guna2DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            var d0 = guna2DateTimePickerDebut.Value.Date;
            var d1 = guna2DateTimePickerFin.Value.Date;

            if (d1 < d0)
                guna2DateTimePickerFin.Value = d0; // clamp
            textBoxAbsences.Enabled = (d1 > d0);
            textboxJourNo.Enabled = (d1 > d0);
            textBoxNuitNo.Enabled = (d1 > d0);
            textBoxJourHSF.Enabled = (d1 > d0);  
            textBoxNuitHSF.Enabled = (d1 > d0);
        }

        private void guna2DateTimePickerDebut_ValueChanged(object sender, EventArgs e)
        {
            guna2DateTimePickerFin.MinDate = guna2DateTimePickerDebut.Value.Date;
        }







        /// Bouton "Calculer"
        /// <summary>
        /// Bouton "Calculer"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void buttonEffacer_Click(object sender, EventArgs e)
        {// 0) Sélection employé valide
            if (ComboBoxEmploye.SelectedValue == null
                || !int.TryParse(ComboBoxEmploye.SelectedValue.ToString(), out int idEmploye)
                || idEmploye <= 0)
            {
                MessageBox.Show("Sélectionnez un employé valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 1) Charger l'employé
            var employe = EmployeService.GetEmployeDetails(idEmploye);
            if (employe == null)
            {
                MessageBox.Show("L'employé sélectionné n'a pas été trouvé.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Par la version correcte ci-dessous :
            decimal salaireCategoriel = (decimal)(employe.Montant ?? 0.0); // decimal? -> decimal
            decimal unitesTotales = employe.HeureContrat;             // heures/jours contractuels
            decimal unitesAbsences = ParseDecimal(textBoxAbsences.Text);              // heures/jours d'absence
            int nbreHC = employe.HeureContrat;
            int hsNJ = ParseInt(textboxJourNo.Text);
            int hsNN = ParseInt(textBoxNuitNo.Text);
            int hsFJ = ParseInt(textBoxJourHSF.Text);
            int hsFN = ParseInt(textBoxNuitHSF.Text);




            if (salaireCategoriel <= 0m)
            {
                MessageBox.Show("Le salaire catégoriel de l'employé est invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (unitesTotales <= 0m)
            {
                MessageBox.Show("Le nombre d’unités contractuelles doit être > 0.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3) Calcul (back)
            try
            {

                // Calcule le salaire de base***********************************************

                decimal baseUnitaire, unitesPayees;
                decimal salaireBase = CalculerSalaireBase(
                    salaireCategoriel,
                    unitesTotales,
                    unitesAbsences,
                    out baseUnitaire,
                    out unitesPayees
                );






                // Calcule les heures supplémentaires****************************************

                decimal primeHS = CalculerHeuresSupp(nbreHC,
                    salaireCategoriel, 
                    hsNJ, 
                    hsNN, 
                    hsFJ, 
                    hsFN);
                decimal tauxHS = hsNJ + hsNN + hsFJ + hsFN;




                //Prime Ancienete

                string anc;
                decimal prime = CalculerAncienneteEtPrime(idEmploye, out anc);







                //Calculer Salaire BRUT 
                // 4) Calcul du BRUT
                var sums = GetSommeIndemnitesParIds(idEmploye);
                decimal salaireBrut = CalculerSalaireBrut(
                    salaireBase,
                    primeHS,
                    (decimal)sums["somme_numeraire"],
                    (decimal)sums["somme_nature"],
                    prime   // ta prime d'ancienneté (decimal)
                );



                //***********************************************************************
                //***********************************************************************
                //Calculer les cotisations CNSS et la TPA
                //************************************************************************
                //************************************************************************


                // 1) Récup infos employé
                var emp = EmployeService.GetEmployeDetails(idEmploye); // Doit fournir emp.Contrat (string) et emp.TPA (decimal?) si dispo
                if (emp == null)
                {
                    MessageBox.Show("Aucune donnée trouvée pour cet employé.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string contrat = emp.Contrat ?? string.Empty;
                decimal tauxTpa = emp.Tpa.HasValue ? emp.Tpa.Value : 0m; // en %

                // 2) Lire le salaire brut (déjà calculé et affiché)
                
                if (salaireBrut <= 0m)
                {
                    MessageBox.Show("Salaire brut invalide ou non calculé.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3) Taux (si besoin de visualiser)
                var taux = CNSSCalculator.GetTauxParContrat(contrat);

                // 4) Calculs
                decimal cnssEmploye = CNSSCalculator.CalculerCNSSEmploye(salaireBrut, contrat);
                decimal pensionEmployeur = CNSSCalculator.CalculerPensionEmployeur(salaireBrut);
                decimal risqueProEmployeur = CNSSCalculator.CalculerRisqueProEmployeur(salaireBrut);
                decimal pfEmployeur = CNSSCalculator.CalculerPFEmployeur(salaireBrut);
                decimal cnssEmployeur = pensionEmployeur + risqueProEmployeur + pfEmployeur;
                decimal tpa = CNSSCalculator.CalculerTpa(salaireBrut, tauxTpa);









                // 1) Tu as déjà le Salaire Brut Social (BSoc) = Brut - CNSS(employé)
                decimal SalairebrutSocial = salaireBrut - cnssEmploye; // ou calcule-le avant









                // 3) Déductibilité totale(à passer ensuite à l’IUTS)
                var noms = new[]
                {
                    "transport nature",
                    "logement nature",
                    "transport numeraire",
                    "logement numeraire",
                    "fonction"
                };

                var indemMap = GetIndemnitesParNoms(idEmploye, noms); // Dictionary<string, decimal>

                decimal montantTransportNature = indemMap["transport nature"];
                decimal montantLogementNature = indemMap["logement nature"];
                decimal montantTransportNumeraire = indemMap["transport numeraire"];
                decimal montantLogementNumeraire = indemMap["logement numeraire"];
                decimal montantFonction = indemMap["fonction"];

                decimal deductibiliteIndem = DeductibilitesIndemnites.ComputeDeductibiliteTotale(
                SalairebrutSocial, montantLogementNumeraire, montantLogementNature, montantTransportNumeraire, montantTransportNature, montantFonction);



                // CALCUL BASE IUTS ,SALAIRE BRUT SOCIAL

                var r = IUTSCalculator.CalculerIUTS(
                    salaireBrut, cnssEmploye, emp.Cadre, deductibiliteIndem, salaireCategoriel, prime, floorCentaines: true);

                decimal baseIutsArr = r.BaseIUTS; // ta base IUTS arrondie à la centaine (decimal)
                int nombreCharges = ChargeClass.CountTotalCharges(idEmploye);
                decimal iutsBrut;
                decimal iutsFinal = IUTS.Calculer(baseIutsArr, nombreCharges, out iutsBrut); // ta méthode de barème


                // Récupère les deux dates
                DateTime d0 = guna2DateTimePickerDebut.Value.Date;
                DateTime d1 = guna2DateTimePickerFin.Value.Date;

                // Concatène en "dd/MM/yyyy - dd/MM/yyyy"
                string periode = $"{d0:dd/MM/yyyy} - {d1:dd/MM/yyyy}";


                //************************************************************************
                //************************************************************************
                //Remplir le snapshot

               



                // Suppose : idEntreprise, idEmploye, anc, contrat, emp.Cadre, tauxTpa, etc. sont disponibles
                var snapshot = new PayrollSnapshot
                {
                    NomPrenom = employe.Nom ?? "",
                    Civilite = employe.Civilite ?? "",
                    Matricule = employe.Matricule,
                    Poste = employe.Poste,
                    NumeroEmploye = employe.TelephoneEmploye,
                    AdresseEmploye = employe.Adresse,
                    PeriodeSalaire = periode,
                    Categorie = employe.Categorie,
                    Service = employe.Service,
                    Direction = employe.Direction,
                    NumeroCnssEmploye = employe.NumeroCnssEmploye,
                    Sexe = employe.Sexe,
                    DureeContrat = employe.DureeContrat,
                    HeureContrat  = employe.HeureContrat,

                    // Infos entreprise
                    Sigle = employe.Sigle,
                    NomEntreprise = employe.NomEntreprise,
                    TelephoneEntreprise = employe.TelephoneEntreprise,
                    EmailEntreprise = employe.EmailEntreprise,
                    AdressePhysiqueEntreprise = employe.AdressePhysiqueEntreprise,
                    AdressePostaleEntreprise = employe.AdressePostaleEntreprise,



                    ///*******************

                    DateNaissance = employe.DateNaissance,
                    DateEntree = employe.DateEntree,
                    DateSortie = employe?.DateSortie,     // type DateTime?



                    IdEntreprise = int.Parse(ComboBoxEntreprise.SelectedValue.ToString()),
                    IdEmploye = idEmploye,
                    AncienneteStr = anc,


                    // Gains

                    HeuresSupp = primeHS,
                    IndemNum = (decimal)sums["somme_numeraire"],
                    IndemNat = (decimal)sums["somme_nature"],

                    // Bruts
                    SalaireBrut = salaireBrut,
                    SalaireBrutSocial = SalairebrutSocial,

                    // CNSS/TPA
                    CNSS_Employe = cnssEmploye,
                    PensionEmployeur = pensionEmployeur,
                    RisqueProEmployeur = risqueProEmployeur,
                    PFEmployeur = pfEmployeur,
                    CNSS_Employeur_Total = cnssEmployeur,
                    TPA = tpa,
                    TauxTPA = tauxTpa,

                    // IUTS
                    DeductibiliteIndemnites = deductibiliteIndem,
                    BaseIUTS = r.BaseIUTS,
                    BaseIUTS_Arrondie = r.BaseIUTSArrondieCent,
                    NombreCharges = nombreCharges,
                    IUTS_Brut = iutsBrut,
                    IUTS_Final = iutsFinal,

                    // Net (exemple)
                    SalaireNet = salaireBrut - cnssEmploye - iutsFinal - tpa,

                    // Méta
                    Contrat = contrat,
                    StatutCadre = emp.Cadre,



                    //*******************
                    //SALAIRE BASE
                    BaseUnitaire = baseUnitaire,
                    SalaireBase = salaireBase,
                    TauxSalaireDeBase = unitesPayees,
                    //*******************


                    //********************
                    //HEURES SUPPLEMENTAIRES
                     PrimeHeuressupp = primeHS,
                     TauxHeureSupp = tauxHS,
                    //********************


                    //*******************
                    //PRIME ANCIENNETE
                    PrimeAnciennete = prime


                };

                // 👉 Stocke-le pour le bouton "Enregistrer" (champ du Form)
                _lastSnapshot = snapshot;

                // (Optionnel) trace 1 ligne
                System.Diagnostics.Debug.WriteLine(
                    $"[SNAP] EmpId={snapshot.IdEmploye} | Brut={snapshot.SalaireBrut:N2} | BSoc={snapshot.SalaireBrutSocial:N2} | " +
                    $"CNSS(E)={snapshot.CNSS_Employe:N2} | IUTS={snapshot.IUTS_Final:N2} | TPA={snapshot.TPA:N2} | Net={snapshot.SalaireNet:N2}"
                );





                //************************************************************************
                //************************************************************************




                // 4) Sortie terminal (fenêtre Sortie de VS)
                Debug.WriteLine($"[PAIE] EmpId={idEmploye} | BaseUnitaire={baseUnitaire:N6} | UnitesPayees={unitesPayees:N2} | SalaireBase={salaireBase:N2} FCFA");
                Debug.WriteLine($"[HS] EmpId={idEmploye} | NJ={hsNJ} NN={hsNN} FJ={hsFJ} FN={hsFN} | Total={primeHS:N2} FCFA");
                System.Diagnostics.Debug.WriteLine($"[ANCIENNETE] {anc} | Prime={prime:N2} FCFA");
                // 🔹 Trace complète en une seule ligne
                Debug.WriteLine(
                    $"[BRUT] EmpId={idEmploye} | Base={salaireBase:N2} | HS={primeHS:N2} | " +
                    $"Num={sums["somme_numeraire"]:N2} | Nat={sums["somme_nature"]:N2} | " +
                    $"Anc={prime:N2} | => BRUT={salaireBrut:N2} FCFA"
                );
                // 6) Debug COMPACT sur une seule ligne
                Debug.WriteLine(
                    $"[CNSS] EmpId={idEmploye} | Contrat='{contrat}' | Brut={salaireBrut:N2} | " +
                    $"Employe(CNSS)={cnssEmploye:N2} | Employeur(Pens)={pensionEmployeur:N2} | Employeur(RP)={risqueProEmployeur:N2} | Employeur(PF)={pfEmployeur:N2} | " +
                    $"Employeur cnss (Total)={cnssEmployeur:N2} | TPA@{tauxTpa:N2}%={tpa:N2}"
                );
                Debug.WriteLine($"[IUTS] Base(⌊100⌋)={baseIutsArr:N2} | Charges={nombreCharges} | Brut={iutsBrut:N2} | Final={iutsFinal:N2}");
                // … ta suite (affichage, net à payer, etc.)
                // (si tu as une console attachée)
                // Console.WriteLine($"[PAIE] EmpId={idEmploye} | BaseUnitaire={baseUnitaire:N6} | UnitesPayees={unitesPayees:N2} | SalaireBase={salaireBase:N2} FCFA");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Paramètre hors bornes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du calcul du salaire de base : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>Parse un décimal tolérant (espaces, FCFA, virgule ou point).</summary>
        private static decimal ParseDecimal(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0m;

            string clean = input.ToUpperInvariant()
                                .Replace("FCFA", "")
                                .Replace(" ", "")
                                .Replace("\u00A0", "")
                                .Trim()
                                .Replace(',', '.');

            return decimal.TryParse(clean, NumberStyles.Any, CultureInfo.InvariantCulture, out var v) ? v : 0m;
        }

        private static int ParseInt(string s) => int.TryParse((s ?? "").Trim(), out var v) ? v : 0;

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonparcourir_Click(object sender, EventArgs e)
        {
            if (_lastSnapshot == null)
            {
                MessageBox.Show("Effectuez d'abord le calcul pour constituer les valeurs à enregistrer.",
                    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Exemple pour récupérer et afficher la première indemnité
            var listeIndemnites = IndemniteClass.GetIndemnitesByEmploye(_lastSnapshot.IdEmploye);
            try
            {
                /// Initialiser les variables avec des valeurs par défaut (vides ou à zéro)
                string Numero_indemnite_1 = string.Empty;
                string Nom_Indemnite_1 = string.Empty;   // Nom de la première indemnité (vide par défaut)
                string Montant_Indemnite_1 = string.Empty;        // Montant de la première indemnité (0.0 par défaut)
                string Taux_Indemnite_1 = string.Empty;           // Taux de la première indemnité (0.0 par défaut)


                string Numero_indemnite_2 = string.Empty;
                string Nom_Indemnite_2 = string.Empty;   // Nom de la deuxième indemnité (vide par défaut)
                string Montant_Indemnite_2 = string.Empty;        // Montant de la deuxième indemnité (0.0 par défaut)
                string Taux_Indemnite_2 = string.Empty;           // Taux de la deuxième indemnité (0.0 par défaut)


                string Numero_indemnite_3 = string.Empty;
                string Nom_Indemnite_3 = string.Empty;   // Nom de la deuxième indemnité (vide par défaut)
                string Montant_Indemnite_3 = string.Empty;        // Montant de la deuxième indemnité (0.0 par défaut)
                string Taux_Indemnite_3 = string.Empty;           // Taux de la deuxième indemnité (0.0 par défaut)


                string Numero_indemnite_4 = string.Empty;
                string Nom_Indemnite_4 = string.Empty;   // Nom de la deuxième indemnité (vide par défaut)
                string Montant_Indemnite_4 = string.Empty;        // Montant de la deuxième indemnité (0.0 par défaut)
                string Taux_Indemnite_4 = string.Empty;           // Taux de la deuxième indemnité (0.0 par défaut)


                string Numero_indemnite_5 = string.Empty;
                string Nom_Indemnite_5 = string.Empty;   // Nom de la deuxième indemnité (vide par défaut)
                string Montant_Indemnite_5 = string.Empty;        // Montant de la deuxième indemnité (0.0 par défaut)
                string Taux_Indemnite_5 = string.Empty;           // Taux de la deuxième indemnité (0.0 par défaut)

                // Récupérer la liste des indemnités pour un employé donné

                // Vérifier si la liste contient des indemnités
                if (listeIndemnites.Count > 0)
                {
                    // Récupérer la première indemnité (si elle existe)
                    var indemnite1 = listeIndemnites[0];
                    Numero_indemnite_1 = "04";
                    Nom_Indemnite_1 = indemnite1.NomIndemnite;  // Récupérer le nom de la première indemnité
                    Montant_Indemnite_1 = indemnite1.MontantIndemnite;  // Récupérer le montant de la première indemnité
                    Taux_Indemnite_1 = indemnite1.TauxIndem;  // Récupérer le taux de la première indemnité

                    // Vérifier si une deuxième indemnité existe
                    if (listeIndemnites.Count > 1)
                    {
                        var indemnite2 = listeIndemnites[1];
                        Numero_indemnite_2 = "05";
                        Nom_Indemnite_2 = indemnite2.NomIndemnite;  // Récupérer le nom de la deuxième indemnité
                        Montant_Indemnite_2 = indemnite2.MontantIndemnite;  // Récupérer le montant de la deuxième indemnité
                        Taux_Indemnite_2 = indemnite2.TauxIndem;  // Récupérer le taux de la deuxième indemnité
                    }

                    // Vérifier si une deuxième indemnité existe
                    if (listeIndemnites.Count > 2)
                    {
                        var indemnite3 = listeIndemnites[2];
                        Numero_indemnite_3 = "06";
                        Nom_Indemnite_3 = indemnite3.NomIndemnite;  // Récupérer le nom de la deuxième indemnité
                        Montant_Indemnite_3 = indemnite3.MontantIndemnite;  // Récupérer le montant de la deuxième indemnité
                        Taux_Indemnite_3 = indemnite3.TauxIndem;  // Récupérer le taux de la deuxième indemnité
                    }

                    // Vérifier si une deuxième indemnité existe
                    if (listeIndemnites.Count > 3)
                    {
                        var indemnite4 = listeIndemnites[3];
                        Numero_indemnite_4 = "07";
                        Nom_Indemnite_4 = indemnite4.NomIndemnite;  // Récupérer le nom de la deuxième indemnité
                        Montant_Indemnite_4 = indemnite4.MontantIndemnite;  // Récupérer le montant de la deuxième indemnité
                        Taux_Indemnite_4 = indemnite4.TauxIndem;  // Récupérer le taux de la deuxième indemnité
                    }

                    // Vérifier si une deuxième indemnité existe
                    if (listeIndemnites.Count > 4)
                    {
                        var indemnite5 = listeIndemnites[4];
                        Numero_indemnite_5 = "05";
                        Nom_Indemnite_5 = indemnite5.NomIndemnite;  // Récupérer le nom de la deuxième indemnité
                        Montant_Indemnite_5 = indemnite5.MontantIndemnite;  // Récupérer le montant de la deuxième indemnité
                        Taux_Indemnite_5 = indemnite5.TauxIndem;  // Récupérer le taux de la deuxième indemnité
                    }
                }

                // Affichage des valeurs récupérées
                Console.WriteLine($"Indemnité 1: {Nom_Indemnite_1}, Montant: {Montant_Indemnite_1:N2}, Taux: {Taux_Indemnite_1:N2}");
                Console.WriteLine($"Indemnité 2: {Nom_Indemnite_2}, Montant: {Montant_Indemnite_2:N2}, Taux: {Taux_Indemnite_2:N2}");


                byte[] logo = EntrepriseClass.GetLogoEntreprise(_lastSnapshot.IdEntreprise);
                var model = new BulletinModel
                {
                    NomEmploye = _lastSnapshot.NomPrenom,
                    Civilite = _lastSnapshot.Civilite,
                    Matricule = _lastSnapshot.Matricule,
                    Poste = _lastSnapshot.Poste,
                    NumeroEmploye = _lastSnapshot.NumeroEmploye,
                    Mois = "Août 2025",
                    HeuresSup = 15000,
                    CNSS = (decimal)_lastSnapshot.CNSS_Employe,
                    SalaireNet = (decimal)_lastSnapshot.SalaireNet,
                    AdresseEmploye = _lastSnapshot.AdresseEmploye,
                    Periode = _lastSnapshot.PeriodeSalaire,
                    LogoEntreprise = logo,
                    DateNaissance = _lastSnapshot.DateNaissance,
                    DateDebut = _lastSnapshot.DateEntree,
                    DateFin = _lastSnapshot.DateSortie,
                    Contrat = _lastSnapshot.Contrat,
                    Categorie = _lastSnapshot.Categorie,
                    Service = _lastSnapshot.Service,
                    Direction = _lastSnapshot.Direction,
                    NumeroCNSSEmploye = _lastSnapshot.NumeroCnssEmploye,
                    Sexe = _lastSnapshot.Sexe,
                    Charges = _lastSnapshot.NombreCharges,
                    DureeContrat = _lastSnapshot.DureeContrat,
                    Anciennete = _lastSnapshot.AncienneteStr,
                    NbJourHeure = _lastSnapshot.HeureContrat,
                    Sigle = _lastSnapshot.Sigle,
                    NomEntreprise = _lastSnapshot.NomEntreprise,
                    AdressePhysiqueEntreprise = _lastSnapshot.AdressePhysiqueEntreprise,
                    AdressePostaleEntreprise = _lastSnapshot.AdressePostaleEntreprise,
                    TelephoneEntreprise = _lastSnapshot.TelephoneEntreprise,
                    EmailEntreprise = _lastSnapshot.EmailEntreprise,
                    Numero_indemnite_1 = Numero_indemnite_1,
                    Nom_Indemnite_1 = Nom_Indemnite_1,
                    Montant_Indemnite_1 = Montant_Indemnite_1,
                    Taux_Indemnite_1 = Taux_Indemnite_1,
                    Numero_indemnite_2 = Numero_indemnite_2,
                    Nom_Indemnite_2 = Nom_Indemnite_2,
                    Montant_Indemnite_2 = Montant_Indemnite_2,
                    Taux_Indemnite_2 = Taux_Indemnite_2,
                    Numero_indemnite_3 = Numero_indemnite_3,
                    Nom_Indemnite_3 = Nom_Indemnite_3,
                    Montant_Indemnite_3 = Montant_Indemnite_3,
                    Taux_Indemnite_3 = Taux_Indemnite_3,
                    Numero_indemnite_4 = Numero_indemnite_4,
                    Nom_Indemnite_4 = Nom_Indemnite_4,
                    Montant_Indemnite_4 = Montant_Indemnite_4,
                    Taux_Indemnite_4 = Taux_Indemnite_4,
                    Numero_indemnite_5 = Numero_indemnite_5,
                    Nom_Indemnite_5 = Nom_Indemnite_5,
                    Montant_Indemnite_5 = Montant_Indemnite_5,
                    Taux_Indemnite_5 = Taux_Indemnite_5,
                    //SALAIRE DE BASE
                    baseUnitaire = (double)_lastSnapshot.BaseUnitaire,
                    SalaireDeBase = (double)_lastSnapshot.SalaireBase,
                    TauxSalaireDeBase = (double)_lastSnapshot.TauxSalaireDeBase,
                    //HEURES SUPPLEMENTAIRES
                    PrimeHeureSupp = (double)_lastSnapshot.HeuresSupp,
                    TauxHeureSupp = (double)_lastSnapshot.TauxHeureSupp,
                    //PRIME ANCIENNETE
                    PrimeAnciennete = (decimal)_lastSnapshot.PrimeAnciennete,
                    //SALAIRE BRUT
                    SalaireBrut = (double)_lastSnapshot.SalaireBrut,
                    //BASE IUTS
                    BaseIUTS = (double)_lastSnapshot.BaseIUTS,
                    //IUTS
                    Iuts = (double)_lastSnapshot.IUTS_Final,
                    //TPA ET TAUX TPA
                    Tpa = (double)_lastSnapshot.TPA,
                    TauxTpa = (double)_lastSnapshot.TauxTPA,
                    //CNSS EMPLOYE ET EMPLOYEUR
                    CnssEmploye = (double)_lastSnapshot.CNSS_Employe,
                    CnssEmployeur = (double)_lastSnapshot.PensionEmployeur,
                    //RISQUE PROFESSIONNEL EMPLOYEUR
                    RisqueProfessionnel = (double)_lastSnapshot.RisqueProEmployeur,
                    //PRESTATION FAMILIALE EMPLOYEUR
                    PrestationFamiliale = (double)_lastSnapshot.PFEmployeur,
                    //AVANTAGES EN NATURE
                    AvantageNature = (double)_lastSnapshot.IndemNat,


                };





                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Title = "Enregistrer le bulletin de paie";
                    saveDialog.Filter = "Fichier PDF (*.pdf)|*.pdf";
                    // Nettoyer la période avant d'insérer dans le nom du fichier
                    string periodeSafe = model.Periode
                        .Replace("/", "-")   // remplace les slashs
                        .Replace(" ", "_")   // remplace les espaces
                        .Replace(":", "-");  // remplace les deux-points s'il y en a

                    // Exemple : "27/08/2025 - 13/10/2025" devient "27-08-2025_-_13-10-2025"

                    // Générer le nom du fichier
                    saveDialog.FileName = $"Bulletin_{model.Matricule}_{periodeSafe}.pdf";


                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        var document = new BulletinDocument(model);
                        document.GeneratePdf(saveDialog.FileName);

                        // Ouvrir le PDF directement
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = saveDialog.FileName,
                            UseShellExecute = true
                        });

                        MessageBox.Show("Bulletin généré avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue :\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- UTIL ---
    }




}
