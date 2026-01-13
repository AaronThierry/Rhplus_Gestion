using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class ImportEmployeForm : Form
    {
        private List<ExcelImportServiceV2.EmployeImportRow> employesAImporter;
        private string cheminFichier;

        public ImportEmployeForm()
        {
            InitializeComponent();
            ConfigurerDataGridView();
        }

        private void ConfigurerDataGridView()
        {
            dataGridViewPreview.AutoGenerateColumns = false;
            dataGridViewPreview.AllowUserToAddRows = false;
            dataGridViewPreview.AllowUserToDeleteRows = false;
            dataGridViewPreview.ReadOnly = true;
            dataGridViewPreview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPreview.RowHeadersVisible = false;

            // Configurer les colonnes
            dataGridViewPreview.Columns.Clear();

            dataGridViewPreview.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Ligne",
                HeaderText = "Ligne",
                DataPropertyName = "LigneExcel",
                Width = 60
            });

            dataGridViewPreview.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NomPrenom",
                HeaderText = "Nom et Prénom",
                DataPropertyName = "NomPrenom",
                Width = 200
            });

            dataGridViewPreview.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Entreprise",
                HeaderText = "Entreprise",
                DataPropertyName = "NomEntreprise",
                Width = 150
            });

            dataGridViewPreview.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Categorie",
                HeaderText = "Catégorie",
                DataPropertyName = "NomCategorie",
                Width = 120
            });

            dataGridViewPreview.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Poste",
                HeaderText = "Poste",
                DataPropertyName = "Poste",
                Width = 150
            });

            dataGridViewPreview.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Telephone",
                HeaderText = "Téléphone",
                DataPropertyName = "Telephone",
                Width = 120
            });

            dataGridViewPreview.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Resultat",
                HeaderText = "Résultat",
                DataPropertyName = "Erreur",
                Width = 250,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Styliser le DataGridView
            dataGridViewPreview.BackgroundColor = Color.White;
            dataGridViewPreview.BorderStyle = BorderStyle.None;
            dataGridViewPreview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewPreview.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewPreview.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridViewPreview.ColumnHeadersDefaultCellStyle.BackColor = Color.MidnightBlue;
            dataGridViewPreview.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewPreview.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 10F, FontStyle.Bold);
            dataGridViewPreview.EnableHeadersVisualStyles = false;
        }

        private void buttonParcourir_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Tous les fichiers supportés (*.csv;*.xls;*.xlsx)|*.csv;*.xls;*.xlsx|Fichiers CSV (*.csv)|*.csv|Fichiers Excel (*.xls;*.xlsx)|*.xls;*.xlsx";
                ofd.Title = "Sélectionner un fichier à importer";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    cheminFichier = ofd.FileName;
                    textBoxFichier.Text = cheminFichier;
                    ChargerPreview();
                }
            }
        }

        private async void ChargerPreview()
        {
            try
            {
                labelProgres.Text = "Chargement du fichier...";
                labelProgres.Visible = true;
                progressBar.Style = ProgressBarStyle.Marquee;
                progressBar.Visible = true;
                buttonImporter.Enabled = false;
                dataGridViewPreview.DataSource = null;

                await Task.Run(() =>
                {
                    employesAImporter = ExcelImportServiceV2.LireFichierExcel(cheminFichier);
                });

                dataGridViewPreview.DataSource = employesAImporter;
                labelProgres.Text = $"{employesAImporter.Count} employé(s) trouvé(s) dans le fichier";
                progressBar.Visible = false;
                buttonImporter.Enabled = true;

                // Afficher le panel d'informations
                labelTotalEmployes.Text = employesAImporter.Count.ToString();
                labelSucces.Text = "0";
                labelErreurs.Text = "0";
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                labelProgres.Visible = false;
                buttonImporter.Enabled = false;

                // Réinitialiser les compteurs en cas d'erreur
                labelTotalEmployes.Text = "0";
                labelSucces.Text = "0";
                labelErreurs.Text = "0";

                CustomMessageBox.Show(
                    $"Erreur lors de la lecture du fichier\n\n" +
                    $"Le fichier sélectionné n'a pas pu être lu. Vérifiez que :\n" +
                    $"• Le fichier est au format CSV, XLS ou XLSX\n" +
                    $"• Le fichier n'est pas corrompu\n" +
                    $"• Le fichier n'est pas ouvert dans une autre application\n\n" +
                    $"Détails de l'erreur :\n{ex.Message}",
                    "Erreur de lecture",
                    CustomMessageBox.MessageType.Error);
            }
        }

        private async void buttonImporter_Click(object sender, EventArgs e)
        {
            if (employesAImporter == null || employesAImporter.Count == 0)
            {
                CustomMessageBox.Show("Aucun employé à importer.", "Information",
                    CustomMessageBox.MessageType.Info);
                return;
            }

            var result = CustomMessageBox.Show(
                $"Voulez-vous vraiment importer {employesAImporter.Count} employé(s) ?\n\n" +
                "Cette opération va créer de nouveaux employés dans la base de données.",
                "Confirmer l'importation",
                CustomMessageBox.MessageType.Question,
                CustomMessageBox.MessageButtons.YesNo);

            if (result != DialogResult.Yes)
                return;

            try
            {
                buttonImporter.Enabled = false;
                buttonParcourir.Enabled = false;
                buttonAnnuler.Enabled = false;
                progressBar.Style = ProgressBarStyle.Continuous;
                progressBar.Value = 0;
                progressBar.Maximum = employesAImporter.Count;
                progressBar.Visible = true;
                labelProgres.Visible = true;

                var progress = new Progress<string>(message =>
                {
                    labelProgres.Text = message;
                    if (progressBar.Value < progressBar.Maximum)
                        progressBar.Value++;
                });

                var resultats = await ExcelImportServiceV2.ImporterEmployesAsync(employesAImporter, progress);

                // Mettre à jour l'affichage avec les résultats
                employesAImporter = resultats;
                dataGridViewPreview.DataSource = null;
                dataGridViewPreview.DataSource = resultats;

                // Colorier les lignes selon le résultat
                foreach (DataGridViewRow row in dataGridViewPreview.Rows)
                {
                    var employe = row.DataBoundItem as ExcelImportServiceV2.EmployeImportRow;
                    if (employe != null)
                    {
                        if (employe.Succes)
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(200, 255, 200);
                            row.Cells["Resultat"].Value = $"✓ Importé - Matricule: {employe.Matricule}";
                        }
                        else
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
                        }
                    }
                }

                int succes = resultats.Count(r => r.Succes);
                int erreurs = resultats.Count(r => !r.Succes);

                labelSucces.Text = succes.ToString();
                labelSucces.ForeColor = Color.FromArgb(46, 125, 50);
                labelErreurs.Text = erreurs.ToString();
                labelErreurs.ForeColor = Color.FromArgb(211, 47, 47);

                progressBar.Visible = false;
                labelProgres.Text = $"Importation terminée: {succes} réussi(s), {erreurs} erreur(s)";

                // Afficher un message approprié selon le résultat
                if (succes > 0 && erreurs == 0)
                {
                    // Tous les employés ont été importés avec succès
                    CustomMessageBox.Show(
                        $"Importation terminée avec succès !\n\n" +
                        $"✓ {succes} employé(s) importé(s) avec succès",
                        "Succès",
                        CustomMessageBox.MessageType.Success);
                }
                else if (succes > 0 && erreurs > 0)
                {
                    // Importation partielle - certains ont réussi, d'autres ont échoué
                    CustomMessageBox.Show(
                        $"Importation partielle terminée\n\n" +
                        $"✓ {succes} employé(s) importé(s) avec succès\n" +
                        $"✗ {erreurs} erreur(s) détectée(s)\n\n" +
                        $"Consultez le tableau ci-dessous pour voir les erreurs en détail.",
                        "Attention",
                        CustomMessageBox.MessageType.Warning);
                }
                else
                {
                    // Aucun employé n'a été importé
                    CustomMessageBox.Show(
                        $"Aucun employé n'a pu être importé\n\n" +
                        $"✗ {erreurs} erreur(s) détectée(s)\n\n" +
                        $"Veuillez corriger les erreurs affichées en rouge dans le tableau ci-dessous et réessayer.",
                        "Erreur",
                        CustomMessageBox.MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                labelProgres.Visible = false;

                CustomMessageBox.Show(
                    $"Erreur critique lors de l'importation\n\n" +
                    $"Une erreur inattendue s'est produite pendant l'importation.\n" +
                    $"Aucun employé n'a été importé.\n\n" +
                    $"Détails de l'erreur :\n{ex.Message}\n\n" +
                    $"Veuillez réessayer ou contacter le support technique si le problème persiste.",
                    "Erreur d'importation",
                    CustomMessageBox.MessageType.Error);
            }
            finally
            {
                buttonImporter.Enabled = false; // Désactiver après importation pour éviter les doublons
                buttonParcourir.Enabled = true;
                buttonAnnuler.Enabled = true;
                progressBar.Visible = false;
            }
        }

        private void buttonTelechargerModele_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Fichier CSV (*.csv)|*.csv";
                sfd.FileName = "Modele_Import_Employes.csv";
                sfd.Title = "Enregistrer le modèle";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Supprimer le fichier s'il existe déjà
                        if (File.Exists(sfd.FileName))
                            File.Delete(sfd.FileName);

                        ExcelImportServiceV2.CreerModeleCSV(sfd.FileName);

                        CustomMessageBox.Show(
                            $"Modèle CSV créé avec succès !\n\nEmplacement : {sfd.FileName}\n\n" +
                            "Remplissez ce fichier avec les données des employés en respectant les colonnes suivantes :\n\n" +
                            "COLONNES OBLIGATOIRES :\n" +
                            "- NomPrenom : Nom complet de l'employé\n" +
                            "- Entreprise : Nom exact de l'entreprise (doit exister dans la base)\n" +
                            "- Categorie : Nom exact de la catégorie (doit exister dans la base)\n\n" +
                            "COLONNES OPTIONNELLES :\n" +
                            "- Civilite, Sexe, DateNaissance, Adresse, Telephone\n" +
                            "- Direction, Service, Poste, NumeroCNSS\n" +
                            "- Contrat, TypeContrat, ModePayement, Cadre\n" +
                            "- DateEntree, DateSortie, HeureContrat, JourContrat\n" +
                            "- NumeroBancaire, Banque, SalaireMoyen, DureeContrat\n\n" +
                            "NOTES IMPORTANTES :\n" +
                            "✓ Une ligne d'exemple est fournie\n" +
                            "✓ Ne supprimez pas la ligne d'en-têtes\n" +
                            "✓ Utilisez le format DD/MM/YYYY pour les dates\n" +
                            "✓ Les noms doivent correspondre exactement à ceux de la base",
                            "Succès",
                            CustomMessageBox.MessageType.Success);

                        // Ouvrir le fichier
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                    catch (Exception ex)
                    {
                        CustomMessageBox.Show($"Erreur lors de la création du modèle :\n{ex.Message}", "Erreur",
                            CustomMessageBox.MessageType.Error);
                    }
                }
            }
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
