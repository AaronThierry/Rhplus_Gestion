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
// using static RH_GRH.GestionSalaireHoraireForm; // ⚠️ SUPPRIMÉ - plus nécessaire

namespace RH_GRH
{

    public partial class GestionSalaireHoraireForm : Form
    {
        // en haut du Form
        private PayrollSnapshot _lastSnapshot;

        public GestionSalaireHoraireForm()
        {
            InitializeComponent();
            StyliserHeader();
            InitPeriode();
            ConfigurerValidation();
        }

        private void StyliserHeader()
        {
            panel2.Height = 70;
            panel2.Paint += (s, e) =>
            {
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    panel2.ClientRectangle,
                    Color.FromArgb(41, 128, 185),    // Bleu vif
                    Color.FromArgb(52, 152, 219),    // Bleu ciel
                    System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, panel2.ClientRectangle);
                }
            };

            label1.Text = "SALAIRE HORAIRE";
            label1.Font = new Font("Montserrat", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.TextAlign = ContentAlignment.MiddleLeft;
            label1.Padding = new Padding(70, 0, 0, 0);

            label1.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int iconX = 15;
                int iconY = (label1.Height - 32) / 2;

                // Icône : Horloge
                using (var pen = new Pen(Color.White, 2.2f))
                using (var brush = new SolidBrush(Color.White))
                {
                    // Cercle extérieur (cadran)
                    e.Graphics.DrawEllipse(pen, iconX + 4, iconY + 4, 28, 28);

                    // Aiguille des heures (courte, vers 3h)
                    e.Graphics.DrawLine(pen, iconX + 18, iconY + 18, iconX + 26, iconY + 18);

                    // Aiguille des minutes (longue, vers 12h)
                    e.Graphics.DrawLine(pen, iconX + 18, iconY + 18, iconX + 18, iconY + 8);

                    // Point central
                    e.Graphics.FillEllipse(brush, iconX + 16, iconY + 16, 4, 4);

                    // Marques 12, 3, 6, 9
                    e.Graphics.FillRectangle(brush, iconX + 17, iconY + 5, 2, 4);    // 12
                    e.Graphics.FillRectangle(brush, iconX + 29, iconY + 17, 4, 2);   // 3
                    e.Graphics.FillRectangle(brush, iconX + 17, iconY + 27, 2, 4);   // 6
                    e.Graphics.FillRectangle(brush, iconX + 5, iconY + 17, 4, 2);    // 9
                }
            };

            panel2.Invalidate();
            label1.Invalidate();
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

            // Définir la date par défaut à la date du jour
            guna2DateTimePickerDebut.Value = DateTime.Now;
            guna2DateTimePickerFin.Value = DateTime.Now;

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
    public static decimal Calculer(decimal baseIUTS, int nombreCharges, out decimal iutsBrut)
    {
        if (baseIUTS <= 0m)
        {
            iutsBrut = 0m;
            return 0m;
        }

        // ✅ Force un nombre rond à la centaine inférieure : 12345 -> 12300
        baseIUTS = Math.Floor(baseIUTS / 100m) * 100m;

        decimal[] bornesInf = { 0m, 30100m, 50100m, 80100m, 120100m, 170100m, 250100m };
        decimal[] bornesSup = { 30000m, 50000m, 80000m, 120000m, 170000m, 250000m, decimal.MaxValue };
        decimal[] tauxPct   = { 0.00m, 12.10m, 13.90m, 15.70m, 18.40m, 21.70m, 25.00m };

        decimal iuts = 0m;
        for (int i = 0; i < bornesInf.Length; i++)
        {
            if (baseIUTS < bornesInf[i]) break;

            decimal trancheDebut = bornesInf[i];
            decimal trancheFin   = bornesSup[i];
            decimal plafond      = baseIUTS < trancheFin ? baseIUTS : trancheFin;
            decimal montant      = plafond - trancheDebut;

            if (montant > 0m)
                iuts += montant * (tauxPct[i] / 100m);
        }

        decimal reduction = 0m;
        switch (nombreCharges)
        {
            case 1: reduction = 0.08m; break;
            case 2: reduction = 0.10m; break;
            case 3: reduction = 0.12m; break;
            default:
                if (nombreCharges >= 4) reduction = 0.14m;
                break;
        }

        iutsBrut = Math.Round(iuts, 2, MidpointRounding.AwayFromZero);
        decimal iutsFinal = Math.Round(iutsBrut * (1m - reduction), 2, MidpointRounding.AwayFromZero);

        Debug.WriteLine($"[IUTS] Base(⌊100⌋)={baseIUTS:N0} | Brut={iutsBrut:N2} | Charges={nombreCharges} (−{reduction:P0}) | Final={iutsFinal:N2}");
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








        private DataTable tousLesEmployesHoraires;

        private void GestionSalaireHoraireForm_Load(object sender, EventArgs e)
        {
            // Masquer la sélection d'entreprise - plus nécessaire
            // ComboBoxEntreprise.Visible = false; // Contrôle supprimé du formulaire
            // label18.Visible = false; // Label "Entreprise"

            // Charger tous les employés horaires
            ChargerTousLesEmployesHoraires();
        }

        private void ChargerTousLesEmployesHoraires()
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
                        WHERE p.typeContrat = 'Horaire'
                        ORDER BY p.nomPrenom";

                    using (var cmd = new MySqlCommand(query, con))
                    {
                        var adapter = new MySqlDataAdapter(cmd);
                        tousLesEmployesHoraires = new DataTable();
                        adapter.Fill(tousLesEmployesHoraires);

                        // Afficher tous au départ
                        AfficherEmployesFiltres(tousLesEmployesHoraires);

                        // Activer tous les champs une fois les données chargées
                        ActiverTousLesChamps();
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement :\n{ex.Message}", "Erreur", CustomMessageBox.MessageType.Error);
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
            textboxJourNo.Enabled = true;
            textBoxNuitNo.Enabled = true;
            textBoxAbsences.Enabled = true;
            textBoxJourHSF.Enabled = true;
            textBoxNuitHSF.Enabled = true;
            textBoxDette.Enabled = true;

            // Activer les boutons
            buttonAjouter.Enabled = true;
            buttonCalculer.Enabled = true;
        }

        private void TextBoxRechercheEmploye_TextChanged(object sender, EventArgs e)
        {
            if (tousLesEmployesHoraires == null) return;

            string recherche = textBoxRechercheEmploye.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(recherche))
            {
                // Afficher tous si recherche vide
                AfficherEmployesFiltres(tousLesEmployesHoraires);
                return;
            }

            // Filtrer les résultats sur TOUS les champs possibles
            var filtres = tousLesEmployesHoraires.AsEnumerable()
                .Where(row =>
                    // Informations principales
                    row.Field<string>("Nom")?.ToLower().Contains(recherche) == true ||
                    row.Field<string>("Matricule")?.ToLower().Contains(recherche) == true ||
                    row.Field<string>("Entreprise")?.ToLower().Contains(recherche) == true ||

                    // Informations professionnelles
                    row.Field<string>("Poste")?.ToLower().Contains(recherche) == true ||
                    row.Field<string>("Service")?.ToLower().Contains(recherche) == true ||
                    row.Field<string>("Direction")?.ToLower().Contains(recherche) == true ||
                    row.Field<string>("Categorie")?.ToLower().Contains(recherche) == true ||

                    // Informations contractuelles
                    row.Field<string>("Contrat")?.ToLower().Contains(recherche) == true ||
                    row.Field<string>("Cadre")?.ToLower().Contains(recherche) == true ||

                    // Informations personnelles
                    row.Field<string>("Telephone")?.ToLower().Contains(recherche) == true ||
                    row.Field<string>("Adresse")?.ToLower().Contains(recherche) == true ||
                    row.Field<string>("Identification")?.ToLower().Contains(recherche) == true ||
                    row.Field<string>("NumeroCNSS")?.ToLower().Contains(recherche) == true ||
                    row.Field<string>("Civilite")?.ToLower().Contains(recherche) == true ||
                    row.Field<string>("Sexe")?.ToLower().Contains(recherche) == true
                );

            DataTable dtFiltre;
            if (filtres.Any())
            {
                dtFiltre = filtres.CopyToDataTable();
            }
            else
            {
                // Table vide avec même structure
                dtFiltre = tousLesEmployesHoraires.Clone();
            }

            AfficherEmployesFiltres(dtFiltre);
        }

        private void AfficherEmployesFiltres(DataTable dt)
        {
            // Ajouter ligne par défaut
            var dtAvecDefault = dt.Copy();
            var defaultRow = dtAvecDefault.NewRow();
            defaultRow["id_personnel"] = 0;
            defaultRow["Display"] = $"-- {dtAvecDefault.Rows.Count} employé(s) trouvé(s) --";
            dtAvecDefault.Rows.InsertAt(defaultRow, 0);

            ComboBoxEmploye.DataSource = dtAvecDefault;
            ComboBoxEmploye.DisplayMember = "Display";
            ComboBoxEmploye.ValueMember = "id_personnel";

            // Si un seul employé trouvé, le sélectionner automatiquement
            if (dt.Rows.Count == 1)
            {
                ComboBoxEmploye.SelectedIndex = 1; // Index 1 car 0 est la ligne par défaut
            }
            else
            {
                ComboBoxEmploye.SelectedIndex = 0; // Sinon afficher la ligne par défaut
            }
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

        // Méthode plus nécessaire - sélection directe d'employé sans filtrage par entreprise
        private void ComboBoxEntreprise_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Désactivé - la sélection d'entreprise n'est plus utilisée
            return;
        }



        //**************//********************************//********************************************///


        private void ComboBoxEmploye_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Utiliser la méthode helper pour gérer DataRowView correctement
            int? idEmploye = GetSelectedIntOrNull(ComboBoxEmploye, "id_personnel");

            if (idEmploye == null || idEmploye.Value <= 0)
            {
                // Aucune sélection valide
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
                    textBoxHcontrat.Text = employe.HeureContrat > 0 ? employe.HeureContrat.ToString() : string.Empty;
                    // Montant nullable
                    textBoxSalaire.Text = employe.Montant.HasValue ? employe.Montant.Value.ToString("N2", CultureInfo.CurrentCulture) : string.Empty;

                    var sums = GetSommeIndemnitesParIds(idEmploye.Value);

                    // Exemples d’usage
                    System.Diagnostics.Debug.WriteLine($"Num={sums["somme_numeraire"]:N2} | Nat={sums["somme_nature"]:N2} FCFA");





                }
                else
                {
                    Console.WriteLine("Aucun employé trouvé avec l'ID spécifié.");
                    CustomMessageBox.Show("L'employé sélectionné n'a pas été trouvé.", "Erreur", CustomMessageBox.MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                // En cas d'erreur, afficher un message d'erreur et loguer l'exception
                Console.WriteLine($"Erreur lors de la récupération des informations de l'employé : {ex.Message}");
                CustomMessageBox.Show($"Erreur lors de la récupération des informations de l'employé :\n{ex.Message}", "Erreur", CustomMessageBox.MessageType.Error);
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







        /// <summary>
        /// Bouton "Calculer" - Effectue le calcul du salaire horaire
        /// </summary>
        private void buttonCalculer_Click(object sender, EventArgs e)
        {
            // 0) Valider tous les champs numériques
            if (!ValiderTousLesChamps())
            {
                CustomMessageBox.Show("Veuillez corriger les erreurs de saisie avant de calculer.",
                    "Validation", CustomMessageBox.MessageType.Warning);
                return;
            }

            // 1) Sélection employé valide
            if (ComboBoxEmploye.SelectedValue == null
                || !int.TryParse(ComboBoxEmploye.SelectedValue.ToString(), out int idEmploye)
                || idEmploye <= 0)
            {
                CustomMessageBox.Show("Veuillez sélectionner un employé valide.", "Validation", CustomMessageBox.MessageType.Warning);
                return;
            }

            // 1) Charger l'employé
            var employe = EmployeService.GetEmployeDetails(idEmploye);
            if (employe == null)
            {
                CustomMessageBox.Show("L'employé sélectionné n'a pas été trouvé.", "Erreur", CustomMessageBox.MessageType.Error);
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
                CustomMessageBox.Show("Le salaire catégoriel de l'employé est invalide.", "Erreur", CustomMessageBox.MessageType.Error);
                return;
            }
            if (unitesTotales <= 0m)
            {
                CustomMessageBox.Show("Le nombre d'unités contractuelles doit être supérieur à 0.", "Erreur", CustomMessageBox.MessageType.Error);
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
                    CustomMessageBox.Show("Aucune donnée trouvée pour cet employé.", "Information", CustomMessageBox.MessageType.Warning);
                    return;
                }
                string dureeContrat = emp.DureeContrat ?? string.Empty;
                decimal tauxTpa = emp.Tpa.HasValue ? emp.Tpa.Value : 0m; // en %

                // 2) Lire le salaire brut (déjà calculé et affiché)

                if (salaireBrut <= 0m)
                {
                    CustomMessageBox.Show("Salaire brut invalide ou non calculé.", "Information", CustomMessageBox.MessageType.Warning);
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
                //var res = NetCalculator.Calculer(salaireBrut, cnssEmploye, iutsFinal, IndemNat, tauxEffort: 0.01m, arrondirNetAPayerCeil: true,ValeurDette);

                // Par cette ligne correcte :
                var res = NetCalculator.Calculer(salaireBrut, cnssEmploye, iutsFinal, IndemNat, ValeurDette, 0.01m, true);
                // (Optionnel)affichage UI
                var fr = System.Globalization.CultureInfo.GetCultureInfo("fr-FR");




                // Suppose : idEntreprise, idEmploye, anc, contrat, emp.Cadre, tauxTpa, etc. sont disponibles
                var snapshot = new PayrollSnapshot
                {
                    NomPrenom = employe.Nom ?? "",
                    Civilite = employe.Civilite ?? "",
                    Matricule = employe.Matricule ?? "",
                    Poste = employe.Poste ?? "",
                    NumeroEmploye = employe.TelephoneEmploye ?? "",
                    AdresseEmploye = employe.Adresse ?? "",
                    PeriodeSalaire = periode ?? "",
                    Categorie = employe.Categorie ?? "",
                    Service = employe.Service ?? "",
                    Direction = employe.Direction ?? "",
                    NumeroCnssEmploye = employe.NumeroCnssEmploye ?? "",
                    Sexe = employe.Sexe ?? "",
                    DureeContrat = employe.DureeContrat ?? "",
                    HeureContrat = employe.HeureContrat,

                    // Infos entreprise
                    Sigle = employe.Sigle ?? "",
                    NomEntreprise = employe.NomEntreprise ?? "",
                    TelephoneEntreprise = employe.TelephoneEntreprise ?? "",
                    EmailEntreprise = employe.EmailEntreprise ?? "",
                    AdressePhysiqueEntreprise = employe.AdressePhysiqueEntreprise ?? "",
                    AdressePostaleEntreprise = employe.AdressePostaleEntreprise ?? "",
                    ResponsableEntreprise = employe.ResponsableEntreprise ?? "",



                    ///*******************

                    DateNaissance = employe.DateNaissance,
                    DateEntree = employe.DateEntree,
                    DateSortie = employe?.DateSortie,     // type DateTime?



                    IdEntreprise = employe.Entreprise,  // Utiliser directement l'ID entreprise de l'employé
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
                     PrimeHeuressupp = primeHS,
                     TauxHeureSupp = tauxHS,
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

                // Afficher les résultats dans le panneau moderne
                AfficherResultats();

                // (Optionnel) trace 1 lign




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
                    $"[CNSS] EmpId={idEmploye} | Contrat='{dureeContrat}' | Brut={salaireBrut:N2} | " +
                    $"Employe(CNSS)={cnssEmploye:N2} | Employeur(Pens)={pensionEmployeur:N2} | Employeur(RP)={risqueProEmployeur:N2} | Employeur(PF)={pfEmployeur:N2} | " +
                    $"Employeur cnss (Total)={cnssEmployeur:N2} | TPA@{tauxTpa:N2}%={tpa:N2}"
                );
                Debug.WriteLine($"[IUTS] Base(⌊100⌋)={baseIutsArr:N2} | Charges={nombreCharges} | Brut={iutsBrut:N2} | Final={iutsFinal:N2}");
                // Trace compacte
                System.Diagnostics.Debug.WriteLine(
                    $" " +
                    $"=> Net={res.SalaireNet:N2} | Effort(1%)={res.Effort:N2} | NetAPayer(ceil)={res.NetAPayer:N2}");
                // … ta suite (affichage, net à payer, etc.)
                // (si tu as une console attachée)
                // Console.WriteLine($"[PAIE] EmpId={idEmploye} | BaseUnitaire={baseUnitaire:N6} | UnitesPayees={unitesPayees:N2} | SalaireBase={salaireBase:N2} FCFA");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                CustomMessageBox.Show(ex.Message, "Paramètre hors bornes", CustomMessageBox.MessageType.Warning);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du calcul du salaire de base :\n{ex.Message}", "Erreur", CustomMessageBox.MessageType.Error);
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
                CustomMessageBox.Show("Effectuez d'abord le calcul pour constituer les valeurs à enregistrer.",
                    "Information", CustomMessageBox.MessageType.Info);
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
                    ResponsableEntreprise = _lastSnapshot.ResponsableEntreprise,
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
                    SalaireNetaPayer = _lastSnapshot.SalaireNetaPayer,
                    ValeurDette = _lastSnapshot.ValeurDette,
                    SalaireNetaPayerFinal = _lastSnapshot.SalaireNetaPayerFinal

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

                        CustomMessageBox.Show("✓ Bulletin généré avec succès !", "Succès", CustomMessageBox.MessageType.Success);

                        // Réinitialiser le formulaire après impression réussie
                        ResetFormulaire();
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Une erreur est survenue :\n{ex.Message}", "Erreur", CustomMessageBox.MessageType.Error);
            }
        }

        /// <summary>
        /// Réinitialise le formulaire après impression du bulletin
        /// </summary>
        private void ResetFormulaire()
        {
            // Réinitialiser le TextBox de recherche
            textBoxRechercheEmploye.Clear();

            // Réinitialiser le ComboBox employé
            if (tousLesEmployesHoraires != null)
            {
                AfficherEmployesFiltres(tousLesEmployesHoraires);
            }

            // Réinitialiser les champs d'informations employé (lecture seule)
            textBoxMatricule.Clear();
            textBoxPoste.Clear();
            textBoxCategorie.Clear();
            textBoxSalaire.Clear();
            textBoxHcontrat.Clear();
            textBoxtypeContrat.Clear();
            textBoxContrat.Clear();

            // Réinitialiser les champs de saisie (heures et absences) avec valeurs par défaut
            textboxJourNo.Text = "0";
            textBoxNuitNo.Text = "0";
            textBoxJourHSF.Text = "0";
            textBoxNuitHSF.Text = "0";
            textBoxAbsences.Text = "0";
            textBoxDette.Text = "0";
            textBoxNP.Clear();

            // Réinitialiser le snapshot
            _lastSnapshot = null;
        }

        /// <summary>
        /// Gère le clic sur le bouton "Nouveau Calcul" pour réinitialiser le formulaire
        /// </summary>
        private void buttonAjouter_Click(object sender, EventArgs e)
        {
            // Réinitialiser complètement le formulaire
            ResetFormulaire();

            // Message de confirmation
            CustomMessageBox.Show("✓ Formulaire réinitialisé avec succès.\n\nVous pouvez saisir un nouveau calcul de salaire.",
                "Nouveau Calcul",
                CustomMessageBox.MessageType.Success);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)

        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Bouton Impression LOT - Saisie en masse et génération d'un PDF consolidé
        /// </summary>
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

                        // Ouvrir le formulaire de saisie en lot pour les Horaires
                        using (var formSaisie = new SaisiePayeLotForm(idEntreprise, periodeDebut, periodeFin, "Horaire"))
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

        // --- RÉSULTATS ET AFFICHAGE ---

        /// <summary>
        /// Affiche les résultats du calcul dans une fenêtre modale
        /// </summary>
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

        /// <summary>
        /// Convertit un montant en lettres (français)
        /// </summary>
        private string ConvertirMontantEnLettres(decimal montant)
        {
            if (montant == 0) return "zéro";

            long partieEntiere = (long)Math.Floor(montant);

            if (partieEntiere < 0) return "montant négatif";
            if (partieEntiere == 0) return "zéro";

            string[] unites = { "", "un", "deux", "trois", "quatre", "cinq", "six", "sept", "huit", "neuf" };
            string[] dizaines = { "", "dix", "vingt", "trente", "quarante", "cinquante", "soixante", "soixante-dix", "quatre-vingt", "quatre-vingt-dix" };
            string[] teens = { "dix", "onze", "douze", "treize", "quatorze", "quinze", "seize", "dix-sept", "dix-huit", "dix-neuf" };

            string resultat = "";

            // Milliards
            if (partieEntiere >= 1000000000)
            {
                long milliards = partieEntiere / 1000000000;
                resultat += (milliards == 1 ? "un milliard " : ConvertirNombreBasique(milliards) + " milliards ");
                partieEntiere %= 1000000000;
            }

            // Millions
            if (partieEntiere >= 1000000)
            {
                long millions = partieEntiere / 1000000;
                resultat += (millions == 1 ? "un million " : ConvertirNombreBasique(millions) + " millions ");
                partieEntiere %= 1000000;
            }

            // Milliers
            if (partieEntiere >= 1000)
            {
                long milliers = partieEntiere / 1000;
                resultat += (milliers == 1 ? "mille " : ConvertirNombreBasique(milliers) + " mille ");
                partieEntiere %= 1000;
            }

            // Centaines, dizaines, unités
            if (partieEntiere > 0)
            {
                resultat += ConvertirNombreBasique(partieEntiere);
            }

            return resultat.Trim();
        }

        /// <summary>
        /// Convertit un nombre de 0 à 999 en lettres
        /// </summary>
        private string ConvertirNombreBasique(long nombre)
        {
            if (nombre == 0) return "";

            string[] unites = { "", "un", "deux", "trois", "quatre", "cinq", "six", "sept", "huit", "neuf" };
            string[] dizaines = { "", "dix", "vingt", "trente", "quarante", "cinquante", "soixante", "soixante-dix", "quatre-vingt", "quatre-vingt-dix" };
            string[] teens = { "dix", "onze", "douze", "treize", "quatorze", "quinze", "seize", "dix-sept", "dix-huit", "dix-neuf" };

            string resultat = "";

            // Centaines
            if (nombre >= 100)
            {
                long centaines = nombre / 100;
                if (centaines == 1)
                    resultat += "cent ";
                else
                    resultat += unites[centaines] + " cent ";
                nombre %= 100;
                if (nombre == 0) resultat = resultat.TrimEnd() + " "; // "deux cents" si nombre = 200
            }

            // Dizaines et unités
            if (nombre >= 20)
            {
                long diz = nombre / 10;
                long unit = nombre % 10;

                if (diz == 7 || diz == 9) // 70-79, 90-99
                {
                    resultat += dizaines[diz - 1] + "-";
                    if (unit == 1) resultat += "et-onze";
                    else resultat += teens[unit];
                }
                else if (diz == 8) // 80-89
                {
                    if (unit == 0)
                        resultat += "quatre-vingts";
                    else
                        resultat += "quatre-vingt-" + unites[unit];
                }
                else
                {
                    resultat += dizaines[diz];
                    if (unit > 0)
                    {
                        if (unit == 1 && (diz == 2 || diz == 3 || diz == 4 || diz == 5 || diz == 6))
                            resultat += "-et-un";
                        else
                            resultat += "-" + unites[unit];
                    }
                }
            }
            else if (nombre >= 10)
            {
                resultat += teens[nombre - 10];
            }
            else if (nombre > 0)
            {
                resultat += unites[nombre];
            }

            return resultat.Trim() + " ";
        }

        // --- VALIDATION EN TEMPS RÉEL ---

        /// <summary>
        /// Valide un champ numérique en temps réel
        /// </summary>
        private bool ValiderChampNumerique(Guna.UI2.WinForms.Guna2TextBox textBox, string nomChamp, bool autoriserVide = false)
        {
            // Effacer l'erreur précédente
            errorProvider.SetError(textBox, string.Empty);

            // Si vide et autorisé, mettre à 0 et valider
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (autoriserVide)
                {
                    textBox.Text = "0";
                    textBox.BorderColor = Color.FromArgb(213, 218, 223); // Gris neutre
                    return true;
                }
                else
                {
                    textBox.BorderColor = Color.FromArgb(231, 76, 60); // Rouge
                    errorProvider.SetError(textBox, $"{nomChamp} est requis");
                    return false;
                }
            }

            // Vérifier si c'est un nombre décimal valide
            if (!decimal.TryParse(textBox.Text, out decimal valeur))
            {
                textBox.BorderColor = Color.FromArgb(231, 76, 60); // Rouge
                errorProvider.SetError(textBox, $"{nomChamp} doit être un nombre valide");
                return false;
            }

            // Vérifier si négatif
            if (valeur < 0)
            {
                textBox.BorderColor = Color.FromArgb(231, 76, 60); // Rouge
                errorProvider.SetError(textBox, $"{nomChamp} ne peut pas être négatif");
                return false;
            }

            // Validation OK - bordure verte
            textBox.BorderColor = Color.FromArgb(46, 204, 113); // Vert
            return true;
        }

        /// <summary>
        /// Valide les absences (ne peut pas dépasser les heures du contrat)
        /// </summary>
        private bool ValiderAbsences()
        {
            errorProvider.SetError(textBoxAbsences, string.Empty);

            if (string.IsNullOrWhiteSpace(textBoxAbsences.Text))
            {
                textBoxAbsences.Text = "0";
                textBoxAbsences.BorderColor = Color.FromArgb(213, 218, 223);
                return true;
            }

            if (!decimal.TryParse(textBoxAbsences.Text, out decimal absences))
            {
                textBoxAbsences.BorderColor = Color.FromArgb(231, 76, 60);
                errorProvider.SetError(textBoxAbsences, "Absences doit être un nombre valide");
                return false;
            }

            if (absences < 0)
            {
                textBoxAbsences.BorderColor = Color.FromArgb(231, 76, 60);
                errorProvider.SetError(textBoxAbsences, "Absences ne peut pas être négatif");
                return false;
            }

            // Vérifier si absences > heures contrat
            if (decimal.TryParse(textBoxHcontrat.Text, out decimal heuresContrat))
            {
                if (absences > heuresContrat)
                {
                    textBoxAbsences.BorderColor = Color.FromArgb(243, 156, 18); // Orange (warning)
                    errorProvider.SetError(textBoxAbsences, $"⚠️ Absences ({absences}h) > Heures contrat ({heuresContrat}h)");
                    return true; // Warning, pas erreur bloquante
                }
            }

            textBoxAbsences.BorderColor = Color.FromArgb(46, 204, 113);
            return true;
        }

        /// <summary>
        /// Configure les événements de validation pour tous les champs
        /// </summary>
        private void ConfigurerValidation()
        {
            // Validation heures supplémentaires normales
            textboxJourNo.TextChanged += (s, e) => ValiderChampNumerique(textboxJourNo, "Heures jour normales", true);
            textBoxNuitNo.TextChanged += (s, e) => ValiderChampNumerique(textBoxNuitNo, "Heures nuit normales", true);

            // Validation heures supplémentaires fériés
            textBoxJourHSF.TextChanged += (s, e) => ValiderChampNumerique(textBoxJourHSF, "Heures jour fériés", true);
            textBoxNuitHSF.TextChanged += (s, e) => ValiderChampNumerique(textBoxNuitHSF, "Heures nuit fériés", true);

            // Validation absences (avec vérification spéciale)
            textBoxAbsences.TextChanged += (s, e) => ValiderAbsences();

            // Validation dette
            textBoxDette.TextChanged += (s, e) => ValiderChampNumerique(textBoxDette, "Remboursement dette", true);

            // Validation à la perte de focus (pour nettoyer les valeurs)
            textboxJourNo.Leave += (s, e) => NettoyerChampNumerique(textboxJourNo);
            textBoxNuitNo.Leave += (s, e) => NettoyerChampNumerique(textBoxNuitNo);
            textBoxJourHSF.Leave += (s, e) => NettoyerChampNumerique(textBoxJourHSF);
            textBoxNuitHSF.Leave += (s, e) => NettoyerChampNumerique(textBoxNuitHSF);
            textBoxAbsences.Leave += (s, e) => NettoyerChampNumerique(textBoxAbsences);
            textBoxDette.Leave += (s, e) => NettoyerChampNumerique(textBoxDette);
        }

        /// <summary>
        /// Nettoie un champ numérique (enlève espaces, remplace vide par 0)
        /// </summary>
        private void NettoyerChampNumerique(Guna.UI2.WinForms.Guna2TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "0";
            }
            else
            {
                // Nettoyer les espaces
                textBox.Text = textBox.Text.Trim();

                // Essayer de parser et reformater
                if (decimal.TryParse(textBox.Text, out decimal valeur))
                {
                    textBox.Text = valeur.ToString("0.##");
                }
            }
        }

        /// <summary>
        /// Valide tous les champs avant calcul
        /// </summary>
        private bool ValiderTousLesChamps()
        {
            bool valide = true;

            valide &= ValiderChampNumerique(textboxJourNo, "Heures jour normales", true);
            valide &= ValiderChampNumerique(textBoxNuitNo, "Heures nuit normales", true);
            valide &= ValiderChampNumerique(textBoxJourHSF, "Heures jour fériés", true);
            valide &= ValiderChampNumerique(textBoxNuitHSF, "Heures nuit fériés", true);
            valide &= ValiderAbsences();
            valide &= ValiderChampNumerique(textBoxDette, "Remboursement dette", true);

            return valide;
        }

        // --- UTIL ---
    }




}
