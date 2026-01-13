using MySql.Data.MySqlClient;
using Org.BouncyCastle.Math;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// using static RH_GRH.GestionSalaireHoraireForm; // ⚠️ SUPPRIMÉ - plus nécessaire

namespace RH_GRH
{
    public partial class GestionSalaireJournalierForm : Form
    {

        private PayrollSnapshot _lastSnapshot;
        private DataTable tousLesEmployesJournaliers;

        public GestionSalaireJournalierForm()
        {
            InitializeComponent();
            StyliserHeader();
            InitPeriode();
        }

        private void StyliserHeader()
        {
            panel2.Height = 85;
            panel2.Paint += (s, e) =>
            {
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    panel2.ClientRectangle,
                    Color.FromArgb(230, 126, 34),    // Orange
                    Color.FromArgb(243, 156, 18),    // Orange clair
                    System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, panel2.ClientRectangle);
                }
            };

            label1.Text = "☀️ SALAIRE JOURNALIER";
            label1.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.ForeColor = System.Drawing.Color.White;
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label1.Padding = new System.Windows.Forms.Padding(70, 0, 0, 0);

            label1.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int iconX = 15;
                int iconY = (label1.Height - 32) / 2;

                // Icône : Soleil (représentant le jour)
                using (var pen = new Pen(Color.White, 2.2f))
                using (var brush = new SolidBrush(Color.White))
                {
                    // Cercle central (soleil)
                    e.Graphics.FillEllipse(brush, iconX + 10, iconY + 10, 16, 16);

                    // 8 rayons autour
                    float centerX = iconX + 18;
                    float centerY = iconY + 18;
                    float innerRadius = 10;
                    float outerRadius = 15;

                    for (int i = 0; i < 8; i++)
                    {
                        float angle = (float)(i * Math.PI / 4);
                        float x1 = centerX + innerRadius * (float)Math.Cos(angle);
                        float y1 = centerY + innerRadius * (float)Math.Sin(angle);
                        float x2 = centerX + outerRadius * (float)Math.Cos(angle);
                        float y2 = centerY + outerRadius * (float)Math.Sin(angle);

                        e.Graphics.DrawLine(pen, x1, y1, x2, y2);
                    }
                }
            };

            panel2.Invalidate();
            label1.Invalidate();
        }



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
        /// nbreJC = unités contractuelles (heures) de la période.
        /// hsDimFerie 
        /// </summary>
        /// 


        public static decimal CalculerJourSupp(
            int nbreJC,                  // unités contractuelles en JOURS sur la période
            decimal salaireCategoriel,   // salaire de la période
            int nbJoursFerieDimanche,    // nombre de jours fériés/dimanches travaillés
            decimal multiplicateur = 1.60m) // coef par jour (par défaut 1,60)
        {
            if (nbreJC <= 0)
                throw new ArgumentOutOfRangeException(nameof(nbreJC), "nbreJC doit être > 0.");
            if (salaireCategoriel < 0m)
                throw new ArgumentOutOfRangeException(nameof(salaireCategoriel), "Salaire négatif.");

            // Clamp négatif
            if (nbJoursFerieDimanche < 0) nbJoursFerieDimanche = 0;

            // Salaire catégoriel par JOUR
            decimal scParJour = salaireCategoriel / nbreJC;

            // Prime jours supplémentaires
            decimal total = nbJoursFerieDimanche * (scParJour * multiplicateur);

            return Math.Round(total, 2, MidpointRounding.AwayFromZero);
        }














        //Recuperer Indemnite


        /// <summary>
        /// Sommes des indemnités (numéraire / nature) pour un employé d'une entreprise (IDs).
        /// Clés retournées: "somme_numeraire", "somme_nature".
        /// </summary>
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



        public static decimal CalculerSalaireBrut(decimal salaireBase, decimal primeHeuresSupp, decimal indemniteNumeraire, decimal indemniteNature, decimal primeAnciennete)
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






        // ⚠️ SUPPRIMÉ: Utilisation de la classe PayrollSnapshot globale (PayrollSnapshot.cs)
        // La définition locale a été retirée pour éviter les conflits de type



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






        private void GestionSalaireJournalierForm_Load(object sender, EventArgs e)
        {
            // Charger tous les employés journaliers
            ChargerTousLesEmployesJournaliers();
        }

        private void ChargerTousLesEmployesJournaliers()
        {
            try
            {
                var dbConnect = new Dbconnect();
                using (var con = dbConnect.getconnection)
                {
                    con.Open();

                    string query = @"
                        SELECT
                            p.id_personnel,
                            p.nomPrenom AS Nom,
                            p.matricule AS Matricule,
                            e.nomEntreprise AS Entreprise,
                            p.poste AS Poste,
                            p.telephone AS Telephone,
                            p.adresse AS Adresse,
                            p.contrat AS Contrat,
                            p.identification AS Identification,
                            p.cadre AS Cadre,
                            p.civilite AS Civilite,
                            p.sexe AS Sexe,
                            s.nomService AS Service,
                            d.nomDirection AS Direction,
                            c.nomCategorie AS Categorie,
                            p.numerocnss AS NumeroCNSS,
                            CONCAT(p.nomPrenom, ' (', p.matricule, ') - ', e.nomEntreprise) AS Display
                        FROM personnel p
                        INNER JOIN entreprise e ON p.id_entreprise = e.id_entreprise
                        LEFT JOIN service s ON s.id_service = p.id_service
                        LEFT JOIN direction d ON d.id_direction = p.id_direction
                        LEFT JOIN categorie c ON c.id_categorie = p.id_categorie
                        WHERE p.typeContrat = 'Journalier'
                        ORDER BY p.nomPrenom";

                    using (var cmd = new MySqlCommand(query, con))
                    {
                        var adapter = new MySqlDataAdapter(cmd);
                        tousLesEmployesJournaliers = new DataTable();
                        adapter.Fill(tousLesEmployesJournaliers);

                        // Afficher tous au départ
                        AfficherEmployesFiltres(tousLesEmployesJournaliers);

                        // Activer tous les champs une fois les données chargées
                        ActiverTousLesChamps();
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement des employés journaliers :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
        }

        private void ActiverTousLesChamps()
        {
            // Activer les champs de recherche et sélection
            textBoxRechercheEmploye.Enabled = true;
            ComboBoxEmploye.Enabled = true;

            // Activer tous les champs de saisie
            textBoxMatricule.Enabled = true;
            textBoxPoste.Enabled = true;
            textBoxtypeContrat.Enabled = true;
            textBoxContrat.Enabled = true;
            textBoxCategorie.Enabled = true;
            textBoxSalaire.Enabled = true;
            textBoxHcontrat.Enabled = true;
            textBoxNP.Enabled = true;
            textBoxAbsences.Enabled = true;
            textBoxJoursFD.Enabled = true;
            textBoxDette.Enabled = true;

            // Activer les boutons
            buttonAjouter.Enabled = true;
            buttonValider.Enabled = true;
        }

        private void AfficherEmployesFiltres(DataTable dtFiltres)
        {
            if (dtFiltres == null) return;

            // Créer une copie pour ajouter la ligne par défaut
            DataTable dtAvecDefault = dtFiltres.Copy();

            // Ajouter la ligne par défaut au début
            DataRow defaultRow = dtAvecDefault.NewRow();
            defaultRow["id_personnel"] = 0;
            defaultRow["Display"] = "--- Sélectionner un employé journalier ---";
            dtAvecDefault.Rows.InsertAt(defaultRow, 0);

            // Lier au ComboBox
            ComboBoxEmploye.DataSource = dtAvecDefault;
            ComboBoxEmploye.DisplayMember = "Display";
            ComboBoxEmploye.ValueMember = "id_personnel";

            // Sélectionner par défaut
            if (dtFiltres.Rows.Count == 1)
            {
                ComboBoxEmploye.SelectedIndex = 1; // Index 1 car 0 est la ligne par défaut
            }
            else
            {
                ComboBoxEmploye.SelectedIndex = 0; // Sinon afficher la ligne par défaut
            }
        }

        private void TextBoxRechercheEmploye_TextChanged(object sender, EventArgs e)
        {
            if (tousLesEmployesJournaliers == null) return;

            string recherche = textBoxRechercheEmploye.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(recherche))
            {
                // Afficher tous les employés
                AfficherEmployesFiltres(tousLesEmployesJournaliers);
                return;
            }

            // Filtrer les employés par nom, matricule ou entreprise
            var rows = tousLesEmployesJournaliers.Select(string.Format(
                "Nom LIKE '%{0}%' OR Matricule LIKE '%{0}%' OR Entreprise LIKE '%{0}%'",
                recherche.Replace("'", "''")));

            if (rows.Length > 0)
            {
                DataTable dtFiltre = tousLesEmployesJournaliers.Clone();
                foreach (var row in rows)
                {
                    dtFiltre.ImportRow(row);
                }
                AfficherEmployesFiltres(dtFiltre);
            }
            else
            {
                // Aucun résultat - afficher un combobox vide avec message
                DataTable dtVide = tousLesEmployesJournaliers.Clone();
                DataRow noResultRow = dtVide.NewRow();
                noResultRow["id_personnel"] = 0;
                noResultRow["Display"] = "❌ Aucun employé journalier trouvé";
                dtVide.Rows.Add(noResultRow);

                ComboBoxEmploye.DataSource = dtVide;
                ComboBoxEmploye.DisplayMember = "Display";
                ComboBoxEmploye.ValueMember = "id_personnel";
                ComboBoxEmploye.SelectedIndex = 0;
            }
        }



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

        // ComboBoxEntreprise_SelectedIndexChanged - REMOVED (ComboBoxEntreprise no longer exists)
        /*
        private void ComboBoxEntreprise_SelectedIndexChanged(object sender, EventArgs e)
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
            EmployeClass.ChargerEmployesParEntrepriseJournalier(ComboBoxEmploye, idEnt.Value, null, true);
        }
        */

        private void ComboBoxEmploye_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Utiliser la méthode helper pour gérer les DataRowView
            int? idEmploye = GetSelectedIntOrNull(ComboBoxEmploye, "id_personnel"
                );

            // Vérifier si un employé valide a été sélectionné
            if (!idEmploye.HasValue || idEmploye.Value <= 0)
            {
                // Si aucune sélection n'est faite ou si la sélection est invalide (par exemple, l'option par défaut)
                return;
            }

            try
            {
                Console.WriteLine($"Valeur sélectionnée : {idEmploye.Value}"); // Log de la valeur sélectionnée

                // Récupérer les informations de l'employé sélectionné
                Console.WriteLine($"Récupération des informations de l'employé avec ID : {idEmploye.Value}");
                var employe = EmployeService.GetEmployeDetails(idEmploye.Value);

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
                    textBoxHcontrat.Text = employe.JourContrat.ToString();
                    // Montant nullable
                    textBoxSalaire.Text = employe.Montant.HasValue ? employe.Montant.Value.ToString("N2", CultureInfo.CurrentCulture) : string.Empty;

                    //somme indemnite
                    // Appel
                    var sums = GetSommeIndemnitesParIds(idEmploye.Value);

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

        private void guna2DateTimePickerFin_ValueChanged(object sender, EventArgs e)
        {
            var d0 = guna2DateTimePickerDebut.Value.Date;
            var d1 = guna2DateTimePickerFin.Value.Date;

            if (d1 < d0)
                guna2DateTimePickerFin.Value = d0; // clamp
            textBoxAbsences.Enabled = (d1 > d0);
            textBoxJoursFD.Enabled = (d1 > d0);  // Holidays/Sundays

        }

        private void guna2DateTimePickerDebut_ValueChanged(object sender, EventArgs e)
        {
            guna2DateTimePickerFin.MinDate = guna2DateTimePickerDebut.Value.Date;
        }

        private void buttonValider_Click(object sender, EventArgs e)
        {
            // 0) Sélection employé valide
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
          // heures/jours contractuels
            int unitesTotalesJour = employe.JourContrat;             // heures/jours contractuels
            decimal unitesAbsences = ParseDecimal(textBoxAbsences.Text);              // heures/jours d'absence
            int nbreHC = employe.HeureContrat;
            int jsFDJ = ParseInt(textBoxJoursFD.Text);  // Jours fériés/dimanches travaillés




            if (salaireCategoriel <= 0m)
            {
                MessageBox.Show("Le salaire catégoriel de l'employé est invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (unitesTotalesJour <= 0m)
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
                    unitesTotalesJour,
                    unitesAbsences,
                    out baseUnitaire,
                    out unitesPayees
                );






                // Calcule les heures supplémentaires****************************************

                decimal primeJourSupp = CalculerJourSupp(
                    unitesTotalesJour,
                    salaireCategoriel,
                    jsFDJ 
                );
                decimal tauxJS = jsFDJ;



                //Prime Ancienete

                string anc;
                decimal prime = CalculerAncienneteEtPrime(idEmploye, out anc);






                //Calculer Salaire BRUT 
                // 4) Calcul du BRUT
                var sums = GetSommeIndemnitesParIds(idEmploye);
                decimal salaireBrut = CalculerSalaireBrut(
                    salaireBase,
                    primeJourSupp,
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
                string dureeContrat = emp.DureeContrat ?? string.Empty;
                decimal tauxTpa = emp.Tpa.HasValue ? emp.Tpa.Value : 0m; // en %

                // 2) Lire le salaire brut (déjà calculé et affiché)

                if (salaireBrut <= 0m)
                {
                    MessageBox.Show("Salaire brut invalide ou non calculé.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3) Taux (si besoin de visualiser)
                var taux = CNSSCalculator.GetTauxParContrat(dureeContrat);

                // 4) Calculs
                decimal cnssEmploye = CNSSCalculator.CalculerCNSSEmploye(salaireBrut, dureeContrat);
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

                decimal baseIutsArr = r.BaseIUTSArrondieCent; // ta base IUTS arrondie à la centaine (decimal)
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



                decimal IndemNat = (decimal)sums["somme_nature"];
                //Salaire Net
                decimal ValeurDette = ParseDecimal(textBoxDette.Text);
                var res = NetCalculator.Calculer(salaireBrut, cnssEmploye, iutsFinal, IndemNat, ValeurDette, 0.01m, true);
                // (Optionnel)affichage UI
                var fr = System.Globalization.CultureInfo.GetCultureInfo("fr-FR");




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
                    HeureContrat = employe.JourContrat,

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



                    IdEntreprise = employe.Entreprise, // Get enterprise ID from employee object
                    IdEmploye = idEmploye,
                    AncienneteStr = anc,


                    // Gains

                    HeuresSupp = primeJourSupp,
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
                    BaseIUTS = r.BaseIUTSArrondieCent,
                    BaseIUTS_Arrondie = r.BaseIUTSArrondieCent,
                    NombreCharges = nombreCharges,
                    IUTS_Brut = iutsBrut,
                    IUTS_Final = iutsFinal,

                    // Net (exemple)


                    // Méta
                    Contrat = dureeContrat,
                    StatutCadre = emp.Cadre,



                    //*******************
                    //SALAIRE BASE
                    BaseUnitaire = baseUnitaire,
                    SalaireBase = salaireBase,
                    TauxSalaireDeBase = unitesPayees,
                    //*******************


                    //********************
                    //HEURES SUPPLEMENTAIRES
                    PrimeHeuressupp = primeJourSupp,
                    TauxHeureSupp = tauxJS,
                    //********************


                    //*******************
                    //PRIME ANCIENNETE
                    PrimeAnciennete = prime,

                    //SALAIRE NET A PAYER
                    SalaireNet = res.SalaireNet,
                    EffortPaix = res.Effort,
                    SalaireNetaPayer = res.NetAPayer,
                    ValeurDette = ValeurDette,
                    SalaireNetaPayerFinal = res.NetAPayerFinal

                };




                // 👉 Stocke-le pour le bouton "Enregistrer" (champ du Form)
                _lastSnapshot = snapshot;

                // Afficher les résultats dans une modal
                AfficherResultats();



                // 4) Sortie terminal (fenêtre Sortie de VS)
                Debug.WriteLine($"[PAIE] EmpId={idEmploye} | BaseUnitaire={baseUnitaire:N6} | UnitesPayees={unitesPayees:N2} | SalaireBase={salaireBase:N2} FCFA");
                System.Diagnostics.Debug.WriteLine($"[JOUR SUPP] Total={primeJourSupp:N2} FCFA");
                System.Diagnostics.Debug.WriteLine($"[ANCIENNETE] {anc} | Prime={prime:N2} FCFA");
                Debug.WriteLine(
                $"[BRUT] EmpId={idEmploye} | Base={salaireBase:N2} | HS={primeJourSupp:N2} | " +
                $"Num={sums["somme_numeraire"]:N2} | Nat={sums["somme_nature"]:N2} | " +
                $"Anc={prime:N2} | => BRUT={salaireBrut:N2} FCFA"
                );
                Debug.WriteLine(
                $"[CNSS] EmpId={idEmploye} | Contrat='{dureeContrat}' | Brut={salaireBrut:N2} | " +
                $"Employe(CNSS)={cnssEmploye:N2} | Employeur(Pens)={pensionEmployeur:N2} | Employeur(RP)={risqueProEmployeur:N2} | Employeur(PF)={pfEmployeur:N2} | " +
                $"Employeur cnss (Total)={cnssEmployeur:N2} | TPA@{tauxTpa:N2}%={tpa:N2}"
                );
                Debug.WriteLine($"[IUTS] Base(⌊100⌋)={baseIutsArr:N2} | Charges={nombreCharges} | Brut={iutsBrut:N2} | Final={iutsFinal:N2}");
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
                    //SALAIRE NET A PAYER
                    SalaireNet = _lastSnapshot.SalaireNet,
                    EffortDePaix = _lastSnapshot.EffortPaix,
                    SalaireNetaPayer = _lastSnapshot.SalaireNetaPayer

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonImprimerLot_Click(object sender, EventArgs e)
        {
            try
            {
                // Ouvrir le formulaire de sélection d'entreprise et de période
                using (var formSelection = new SelectionEntrepriseForm())
                {
                    if (formSelection.ShowDialog() == DialogResult.OK)
                    {
                        int idEntreprise = formSelection.EntrepriseSelectionnee;
                        string nomEntreprise = formSelection.NomEntrepriseSelectionnee;
                        DateTime periodeDebut = formSelection.DateDebut;
                        DateTime periodeFin = formSelection.DateFin;

                        // Ouvrir le formulaire de saisie en lot pour les Journaliers
                        using (var formSaisie = new SaisiePayeLotForm(idEntreprise, periodeDebut, periodeFin, "Journalier"))
                        {
                            formSaisie.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de l'impression en lot :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
        }

        private void AfficherResultats()
        {
            if (_lastSnapshot == null)
            {
                return;
            }

            // Ouvrir la fenêtre modale avec les résultats
            using (var modal = new ResultatsModal(_lastSnapshot))
            {
                var result = modal.ShowDialog(this);

                // Si l'utilisateur a cliqué sur Imprimer
                if (result == DialogResult.OK)
                {
                    // Appeler la méthode d'impression
                    ImprimerBulletin();
                }
            }
        }

        /// <summary>
        /// Méthode d'impression du bulletin
        /// </summary>
        private void ImprimerBulletin()
        {
            // Cette méthode est appelée depuis le modal ou depuis buttonparcourir_Click
            // Elle doit contenir la logique d'impression du bulletin
            buttonparcourir_Click(null, null);
        }

        private void labelTitreImpressionLot_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Gère le clic sur le bouton "Nouveau Calcul" pour réinitialiser le formulaire
        /// </summary>
        private void buttonAjouter_Click(object sender, EventArgs e)
        {
            // Réinitialiser les champs de saisie
            textBoxJoursFD.Clear();
            textBoxAbsences.Clear();
            textBoxDette.Clear();

            // Réinitialiser les champs d'informations employé
            textBoxMatricule.Clear();
            textBoxNP.Clear();
            textBoxPoste.Clear();
            textBoxCategorie.Clear();
            textBoxHcontrat.Clear();
            textBoxSalaire.Clear();
            textBoxtypeContrat.Clear();
            textBoxContrat.Clear();

            // Réinitialiser la sélection d'employé
            ComboBoxEmploye.SelectedIndex = -1;
            textBoxRechercheEmploye.Clear();

            // Activer les champs de saisie
            textBoxJoursFD.Enabled = true;
            textBoxAbsences.Enabled = true;
            textBoxDette.Enabled = true;

            // Activer les boutons
            buttonAjouter.Enabled = true;
            buttonValider.Enabled = true;

            // Message de confirmation
            CustomMessageBox.Show("✓ Formulaire réinitialisé avec succès.\n\nVous pouvez saisir un nouveau calcul de salaire.",
                "Nouveau Calcul",
                CustomMessageBox.MessageType.Success,
                CustomMessageBox.MessageButtons.OK);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }



}
