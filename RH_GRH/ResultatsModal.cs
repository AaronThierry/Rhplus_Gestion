using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace RH_GRH
{
    /// <summary>
    /// Fenêtre modale pour afficher les résultats de calcul de salaire
    /// </summary>
    public partial class ResultatsModal : Form
    {
        private PayrollSnapshot _snapshot;

        public ResultatsModal(PayrollSnapshot snapshot)
        {
            InitializeComponent();
            _snapshot = snapshot;

            // Configuration de la fenêtre modale
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.ShowInTaskbar = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Activer le défilement horizontal pour les ListViews
            listViewGains.Scrollable = true;
            listViewRetenues.Scrollable = true;

            // Afficher les résultats
            AfficherResultats();
        }

        /// <summary>
        /// Affiche les résultats du calcul
        /// </summary>
        private void AfficherResultats()
        {
            if (_snapshot == null) return;

            var snap = _snapshot;

            // Net à payer (grande police, très visible, vert)
            labelNetAPayer.Text = $"{snap.SalaireNetaPayerFinal:N0} FCFA";

            // Montant en lettres
            labelNetEnLettres.Text = $"({ConvertirMontantEnLettres(snap.SalaireNetaPayerFinal)} francs CFA)";

            // GAINS - Liste
            listViewGains.Items.Clear();
            AjouterLigneGain("Salaire de base", snap.SalaireBase);
            if (snap.PrimeHeuressupp > 0)
                AjouterLigneGain($"Heures supplémentaires ({snap.TauxHeureSupp:N0}h)", snap.PrimeHeuressupp);
            if (snap.PrimeAnciennete > 0)
                AjouterLigneGain($"Prime d'ancienneté ({snap.AncienneteStr})", snap.PrimeAnciennete);
            if (snap.Sursalaire > 0)
                AjouterLigneGain("Sursalaire", snap.Sursalaire);
            if (snap.IndemNum > 0)
                AjouterLigneGain("Indemnités numéraires", snap.IndemNum);
            if (snap.IndemNat > 0)
                AjouterLigneGain("Avantages en nature", snap.IndemNat);

            // Ligne de total brut
            var itemTotalBrut = new ListViewItem("═══ SALAIRE BRUT TOTAL");
            itemTotalBrut.Font = new Font("Montserrat", 9.5F, FontStyle.Bold);
            itemTotalBrut.ForeColor = Color.FromArgb(22, 160, 133);
            itemTotalBrut.SubItems.Add($"{snap.SalaireBrut:N0} FCFA");
            listViewGains.Items.Add(itemTotalBrut);

            // RETENUES - Liste
            listViewRetenues.Items.Clear();
            if (snap.CNSS_Employe > 0)
                AjouterLigneRetenue("CNSS Employé (3.6%)", snap.CNSS_Employe);
            if (snap.IUTS_Final > 0)
                AjouterLigneRetenue($"IUTS (Impôt) - {snap.NombreCharges} charge(s)", snap.IUTS_Final);
            if (snap.IndemNat > 0)
                AjouterLigneRetenue("Avantages en nature (déduits)", snap.IndemNat);
            if (snap.EffortPaix > 0)
                AjouterLigneRetenue("Effort de paix (1%)", snap.EffortPaix);
            if (snap.ValeurDette > 0)
                AjouterLigneRetenue("Remboursement dette", snap.ValeurDette);
            if (snap.TotalAbonnements > 0)
                AjouterLigneRetenue($"Abonnement ({snap.NombreAbonnements})", snap.TotalAbonnements);

            // Ligne de total retenues
            decimal totalRetenues = snap.CNSS_Employe + snap.IUTS_Final + snap.IndemNat + snap.EffortPaix + snap.ValeurDette + snap.TotalAbonnements;
            var itemTotalRetenues = new ListViewItem("═══ TOTAL RETENUES");
            itemTotalRetenues.Font = new Font("Montserrat", 9.5F, FontStyle.Bold);
            itemTotalRetenues.ForeColor = Color.FromArgb(192, 57, 43);
            itemTotalRetenues.SubItems.Add($"{totalRetenues:N0} FCFA");
            listViewRetenues.Items.Add(itemTotalRetenues);

            // Information de calcul
            labelInfoCalcul.Text = $"📅 Période : {snap.PeriodeSalaire}";
        }

        /// <summary>
        /// Ajoute une ligne de gain (vert)
        /// </summary>
        private void AjouterLigneGain(string description, decimal montant)
        {
            if (montant <= 0) return;

            var item = new ListViewItem(description);
            item.ForeColor = Color.FromArgb(46, 204, 113);
            item.Font = new Font("Montserrat", 9F);
            item.SubItems.Add($"+ {montant:N0} FCFA");
            listViewGains.Items.Add(item);
        }

        /// <summary>
        /// Ajoute une ligne de retenue (rouge)
        /// </summary>
        private void AjouterLigneRetenue(string description, decimal montant)
        {
            if (montant <= 0) return;

            var item = new ListViewItem(description);
            item.ForeColor = Color.FromArgb(231, 76, 60);
            item.Font = new Font("Montserrat", 9F);
            item.SubItems.Add($"− {montant:N0} FCFA");
            listViewRetenues.Items.Add(item);
        }

        /// <summary>
        /// Convertit un montant en lettres (français)
        /// </summary>
        private string ConvertirMontantEnLettres(decimal montant)
        {
            if (montant == 0) return "zéro";

            long montantLong = (long)Math.Round(montant);
            if (montantLong < 0) return "montant négatif";

            string resultat = "";

            // Milliards
            long milliards = montantLong / 1000000000;
            if (milliards > 0)
            {
                resultat += ConvertirGroupe((int)milliards) + " milliard";
                if (milliards > 1) resultat += "s";
                montantLong %= 1000000000;
                if (montantLong > 0) resultat += " ";
            }

            // Millions
            long millions = montantLong / 1000000;
            if (millions > 0)
            {
                resultat += ConvertirGroupe((int)millions) + " million";
                if (millions > 1) resultat += "s";
                montantLong %= 1000000;
                if (montantLong > 0) resultat += " ";
            }

            // Milliers
            long milliers = montantLong / 1000;
            if (milliers > 0)
            {
                if (milliers == 1)
                    resultat += "mille";
                else
                    resultat += ConvertirGroupe((int)milliers) + " mille";
                montantLong %= 1000;
                if (montantLong > 0) resultat += " ";
            }

            // Unités
            if (montantLong > 0)
            {
                resultat += ConvertirGroupe((int)montantLong);
            }

            return resultat.Trim();
        }

        /// <summary>
        /// Convertit un groupe de 3 chiffres en lettres
        /// </summary>
        private string ConvertirGroupe(int nombre)
        {
            if (nombre == 0) return "";

            string[] unites = { "", "un", "deux", "trois", "quatre", "cinq", "six", "sept", "huit", "neuf" };
            string[] dizaines = { "", "dix", "vingt", "trente", "quarante", "cinquante", "soixante", "soixante", "quatre-vingt", "quatre-vingt" };
            string[] special = { "dix", "onze", "douze", "treize", "quatorze", "quinze", "seize", "dix-sept", "dix-huit", "dix-neuf" };

            string resultat = "";

            // Centaines
            int centaines = nombre / 100;
            if (centaines > 0)
            {
                if (centaines == 1)
                    resultat += "cent";
                else
                    resultat += unites[centaines] + " cent";

                if (nombre % 100 > 0)
                    resultat += " ";
                else if (centaines > 1)
                    resultat += "s";
            }

            nombre %= 100;

            // Cas spéciaux 10-19
            if (nombre >= 10 && nombre <= 19)
            {
                resultat += special[nombre - 10];
                return resultat;
            }

            // Dizaines
            int diz = nombre / 10;
            if (diz > 0)
            {
                if (diz == 7 || diz == 9)
                {
                    resultat += dizaines[diz];
                    nombre %= 10;
                    if (nombre > 0)
                    {
                        if (diz == 7 && nombre == 1)
                            resultat += " et onze";
                        else if (diz == 7)
                            resultat += "-" + special[nombre];
                        else
                            resultat += "-" + special[nombre];
                        return resultat;
                    }
                }
                else
                {
                    resultat += dizaines[diz];
                    nombre %= 10;
                    if (nombre > 0)
                    {
                        if (nombre == 1 && (diz == 2 || diz == 3 || diz == 4 || diz == 5 || diz == 6))
                            resultat += " et un";
                        else
                            resultat += "-" + unites[nombre];
                        return resultat;
                    }
                    else if (diz == 8)
                        resultat += "s";
                }
            }
            else if (nombre > 0)
            {
                resultat += unites[nombre];
            }

            return resultat;
        }

        /// <summary>
        /// Ferme la fenêtre modale
        /// </summary>
        private void buttonFermer_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Imprime le bulletin
        /// </summary>
        private void buttonImprimer_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Permet de déplacer la fenêtre en cliquant sur le panel supérieur
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        // Imports pour déplacer la fenêtre
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelBackground_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelBackground_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void listViewGains_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
