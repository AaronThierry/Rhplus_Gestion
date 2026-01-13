using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class ProgressionImpressionForm : Form
    {
        private CancellationTokenSource cancellationTokenSource;
        private Stopwatch stopwatch;
        private BatchBulletinService.BatchPrintResult result;

        public ProgressionImpressionForm()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();
        }

        public async Task<BatchBulletinService.BatchPrintResult> GenererBulletinsAsync(
            System.Collections.Generic.List<int> idsEmployes,
            DateTime periodeDebut,
            DateTime periodeFin,
            string dossierDestination)
        {
            cancellationTokenSource = new CancellationTokenSource();
            stopwatch.Start();

            var progress = new Progress<BatchBulletinService.PrintProgress>(OnProgressChanged);

            try
            {
                result = await BatchBulletinService.GenererBulletinsAsync(
                    idsEmployes,
                    periodeDebut,
                    periodeFin,
                    dossierDestination,
                    progress,
                    cancellationTokenSource.Token);

                stopwatch.Stop();

                if (result.SuccessCount > 0)
                {
                    AfficherResultats();
                }

                return result;
            }
            catch (OperationCanceledException)
            {
                stopwatch.Stop();
                labelStatut.Text = "⚠️ Opération annulée par l'utilisateur";
                labelStatut.ForeColor = Color.Orange;
                return null;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                labelStatut.Text = $"❌ Erreur : {ex.Message}";
                labelStatut.ForeColor = Color.Red;
                MessageBox.Show($"Une erreur est survenue :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void OnProgressChanged(BatchBulletinService.PrintProgress progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<BatchBulletinService.PrintProgress>(OnProgressChanged), progress);
                return;
            }

            // Mettre à jour la barre de progression
            progressBar.Maximum = progress.Total;
            progressBar.Value = progress.Current;

            // Pourcentage
            int pourcentage = progress.Total > 0 ? (progress.Current * 100 / progress.Total) : 0;
            labelPourcentage.Text = $"{pourcentage}%";

            // Employé en cours
            labelEmployeEnCours.Text = $"En cours : {progress.CurrentEmployeeName}";

            // Compteurs
            labelCompteurs.Text = $"✅ Réussis : {progress.Success}  |  ❌ Erreurs : {progress.Errors}  |  ⏳ Restants : {progress.Total - progress.Current}";

            // Temps écoulé
            labelTempsEcoule.Text = $"Temps écoulé : {stopwatch.Elapsed:mm\\:ss}";

            // Temps estimé
            if (progress.Current > 0 && progress.Current < progress.Total)
            {
                var tempsParEmploye = stopwatch.Elapsed.TotalSeconds / progress.Current;
                var tempsRestant = TimeSpan.FromSeconds(tempsParEmploye * (progress.Total - progress.Current));
                labelTempsEstime.Text = $"Temps estimé : {tempsRestant:mm\\:ss}";
            }

            // Statut
            if (progress.Current >= progress.Total)
            {
                labelStatut.Text = "✅ Génération terminée !";
                labelStatut.ForeColor = Color.Green;
                buttonAnnuler.Enabled = false;
            }
            else
            {
                labelStatut.Text = $"⚡ Génération en cours... ({progress.Current}/{progress.Total})";
                labelStatut.ForeColor = Color.FromArgb(94, 148, 255);
            }
        }

        private void AfficherResultats()
        {
            // Créer le panneau de résultats
            var panelResultats = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 150,
                BackColor = Color.FromArgb(240, 240, 240),
                Padding = new Padding(15)
            };

            var labelTitre = new Label
            {
                Text = "📊 RÉSULTATS",
                Font = new Font("Montserrat", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(94, 148, 255),
                Dock = DockStyle.Top,
                Height = 30
            };
            panelResultats.Controls.Add(labelTitre);

            var labelDetails = new Label
            {
                Text = $"• Bulletins générés : {result.SuccessCount}\n" +
                       $"• Erreurs : {result.ErrorCount}\n" +
                       $"• Durée totale : {result.Duration:mm\\:ss}",
                Font = new Font("Montserrat", 9F),
                Dock = DockStyle.Top,
                Height = 70,
                Top = 30
            };
            panelResultats.Controls.Add(labelDetails);

            var buttonOuvrirDossier = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "📁 Ouvrir le dossier",
                Size = new Size(180, 40),
                Location = new Point(15, 100),
                BorderRadius = 8,
                FillColor = Color.FromArgb(94, 148, 255),
                Font = new Font("Montserrat", 9F, FontStyle.Bold)
            };
            buttonOuvrirDossier.Click += (s, e) =>
            {
                if (result.GeneratedFiles.Count > 0)
                {
                    var dossier = Path.GetDirectoryName(result.GeneratedFiles[0]);
                    Process.Start("explorer.exe", dossier);
                }
            };
            panelResultats.Controls.Add(buttonOuvrirDossier);

            var buttonFermer = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "✅ Fermer",
                Size = new Size(180, 40),
                Location = new Point(205, 100),
                BorderRadius = 8,
                FillColor = Color.Green,
                Font = new Font("Montserrat", 9F, FontStyle.Bold)
            };
            buttonFermer.Click += (s, e) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };
            panelResultats.Controls.Add(buttonFermer);

            Controls.Add(panelResultats);
            panelResultats.BringToFront();
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                "Êtes-vous sûr de vouloir annuler la génération en cours ?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                cancellationTokenSource?.Cancel();
                buttonAnnuler.Enabled = false;
                labelStatut.Text = "⚠️ Annulation en cours...";
                labelStatut.ForeColor = Color.Orange;
            }
        }
    }
}
