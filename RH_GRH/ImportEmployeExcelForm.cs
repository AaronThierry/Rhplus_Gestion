using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RH_GRH
{
    /// <summary>
    /// Formulaire professionnel pour l'import Excel avec génération de template et validation
    /// </summary>
    public partial class ImportEmployeExcelForm : Form
    {
        private Dbconnect connect = new Dbconnect();
        private Button btnGenererTemplate;
        private Button btnImporter;
        private Button btnFermer;
        private Panel pnlGeneration;
        private Panel pnlImport;
        private Label lblTitle;
        private Label lblEntrepriseGen;
        private Label lblEntrepriseImp;
        private ComboBox cboEntrepriseGen;
        private ComboBox cboEntrepriseImp;
        private TextBox txtFichierTemplate;
        private TextBox txtFichierImport;
        private Button btnParcourirTemplate;
        private Button btnParcourirImport;
        private ProgressBar progressBar;
        private Label lblProgress;

        public ImportEmployeExcelForm()
        {
            InitializeComponents();
            LoadEntreprises();
        }

        private void InitializeComponents()
        {
            this.Text = "Import Excel - Gestion Employés";
            this.Size = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(245, 245, 245);

            // Title
            lblTitle = new Label
            {
                Text = "IMPORT EXCEL - EMPLOYÉS EN LOT",
                Font = new Font("Montserrat", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 120, 140),
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.White
            };
            this.Controls.Add(lblTitle);

            // Panel Generation Template
            pnlGeneration = new Panel
            {
                Location = new Point(30, 80),
                Size = new Size(820, 220),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblGenTitle = new Label
            {
                Text = "ÉTAPE 1 : GÉNÉRER LE TEMPLATE EXCEL",
                Font = new Font("Montserrat", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 160, 180),
                Location = new Point(20, 15),
                Size = new Size(780, 30)
            };
            pnlGeneration.Controls.Add(lblGenTitle);

            lblEntrepriseGen = new Label
            {
                Text = "Sélectionner l'entreprise :",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(20, 55),
                Size = new Size(200, 25)
            };
            pnlGeneration.Controls.Add(lblEntrepriseGen);

            cboEntrepriseGen = new ComboBox
            {
                Location = new Point(220, 55),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            pnlGeneration.Controls.Add(cboEntrepriseGen);

            var lblTemplateInstructions = new Label
            {
                Text = "Le template Excel contiendra des listes déroulantes pour les catégories, services et directions\n" +
                       "spécifiques à l'entreprise sélectionnée, ainsi que des validations de données automatiques.",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(20, 95),
                Size = new Size(780, 40),
                AutoSize = false
            };
            pnlGeneration.Controls.Add(lblTemplateInstructions);

            txtFichierTemplate = new TextBox
            {
                Location = new Point(20, 145),
                Size = new Size(540, 30),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.White
            };
            pnlGeneration.Controls.Add(txtFichierTemplate);

            btnParcourirTemplate = new Button
            {
                Text = "📁 Parcourir",
                Location = new Point(570, 143),
                Size = new Size(110, 35),
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.FromArgb(220, 220, 220),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnParcourirTemplate.FlatAppearance.BorderSize = 0;
            btnParcourirTemplate.Click += BtnParcourirTemplate_Click;
            pnlGeneration.Controls.Add(btnParcourirTemplate);

            btnGenererTemplate = new Button
            {
                Text = "📝 GÉNÉRER TEMPLATE",
                Location = new Point(690, 143),
                Size = new Size(110, 35),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(35, 160, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnGenererTemplate.FlatAppearance.BorderSize = 0;
            btnGenererTemplate.Click += BtnGenererTemplate_Click;
            pnlGeneration.Controls.Add(btnGenererTemplate);

            this.Controls.Add(pnlGeneration);

            // Panel Import
            pnlImport = new Panel
            {
                Location = new Point(30, 320),
                Size = new Size(820, 200),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblImpTitle = new Label
            {
                Text = "ÉTAPE 2 : IMPORTER LE FICHIER EXCEL REMPLI",
                Font = new Font("Montserrat", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 160, 180),
                Location = new Point(20, 15),
                Size = new Size(780, 30)
            };
            pnlImport.Controls.Add(lblImpTitle);

            lblEntrepriseImp = new Label
            {
                Text = "Entreprise concernée :",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(20, 55),
                Size = new Size(200, 25)
            };
            pnlImport.Controls.Add(lblEntrepriseImp);

            cboEntrepriseImp = new ComboBox
            {
                Location = new Point(220, 55),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            pnlImport.Controls.Add(cboEntrepriseImp);

            var lblImportInstructions = new Label
            {
                Text = "Sélectionnez le fichier Excel que vous avez rempli avec les données des employés.",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(20, 95),
                Size = new Size(780, 25)
            };
            pnlImport.Controls.Add(lblImportInstructions);

            txtFichierImport = new TextBox
            {
                Location = new Point(20, 130),
                Size = new Size(540, 30),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.White
            };
            pnlImport.Controls.Add(txtFichierImport);

            btnParcourirImport = new Button
            {
                Text = "📁 Parcourir",
                Location = new Point(570, 128),
                Size = new Size(110, 35),
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.FromArgb(220, 220, 220),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnParcourirImport.FlatAppearance.BorderSize = 0;
            btnParcourirImport.Click += BtnParcourirImport_Click;
            pnlImport.Controls.Add(btnParcourirImport);

            btnImporter = new Button
            {
                Text = "📥 IMPORTER",
                Location = new Point(690, 128),
                Size = new Size(110, 35),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(46, 139, 87),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnImporter.FlatAppearance.BorderSize = 0;
            btnImporter.Click += BtnImporter_Click;
            pnlImport.Controls.Add(btnImporter);

            this.Controls.Add(pnlImport);

            // Progress bar
            progressBar = new ProgressBar
            {
                Location = new Point(30, 535),
                Size = new Size(820, 25),
                Visible = false
            };
            this.Controls.Add(progressBar);

            lblProgress = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 9F),
                Location = new Point(30, 565),
                Size = new Size(820, 20),
                TextAlign = ContentAlignment.MiddleLeft,
                Visible = false
            };
            this.Controls.Add(lblProgress);

            // Bouton Fermer
            btnFermer = new Button
            {
                Text = "Fermer",
                Location = new Point(750, 570),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.FromArgb(200, 200, 200),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnFermer.FlatAppearance.BorderSize = 0;
            btnFermer.Click += (s, e) => this.Close();
            this.Controls.Add(btnFermer);
        }

        private void LoadEntreprises()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connect.getconnection.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT id_entreprise, nomEntreprise FROM entreprise ORDER BY nomEntreprise";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            var entreprises = new List<EntrepriseItem>();
                            while (reader.Read())
                            {
                                entreprises.Add(new EntrepriseItem
                                {
                                    Id = reader.GetInt32("id_entreprise"),
                                    Nom = reader.GetString("nomEntreprise")
                                });
                            }

                            cboEntrepriseGen.DataSource = new List<EntrepriseItem>(entreprises);
                            cboEntrepriseGen.DisplayMember = "Nom";
                            cboEntrepriseGen.ValueMember = "Id";

                            cboEntrepriseImp.DataSource = new List<EntrepriseItem>(entreprises);
                            cboEntrepriseImp.DisplayMember = "Nom";
                            cboEntrepriseImp.ValueMember = "Id";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des entreprises: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnParcourirTemplate_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Fichier Excel (*.xlsx)|*.xlsx";
                sfd.FileName = $"Template_Employes_{DateTime.Now:yyyyMMdd}.xlsx";
                sfd.Title = "Enregistrer le template Excel";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    txtFichierTemplate.Text = sfd.FileName;
                }
            }
        }

        private void BtnParcourirImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Fichier Excel (*.xlsx)|*.xlsx";
                ofd.Title = "Sélectionner le fichier Excel à importer";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFichierImport.Text = ofd.FileName;
                    btnImporter.Enabled = true;
                }
            }
        }

        private void BtnGenererTemplate_Click(object sender, EventArgs e)
        {
            if (cboEntrepriseGen.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une entreprise.", "Attention",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFichierTemplate.Text))
            {
                MessageBox.Show("Veuillez sélectionner l'emplacement du fichier.", "Attention",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                progressBar.Visible = true;
                lblProgress.Visible = true;
                lblProgress.Text = "Test de la bibliothèque EPPlus...";
                btnGenererTemplate.Enabled = false;

                // TEST EPPlus d'abord
                string testPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "test_epplus.xlsx");
                string testResult = EPPlusTest.TestEPPlus(testPath);

                if (!testResult.StartsWith("Succès"))
                {
                    throw new Exception($"Le test EPPlus a échoué:\n{testResult}");
                }

                lblProgress.Text = "Génération du template en cours...";

                var entreprise = (EntrepriseItem)cboEntrepriseGen.SelectedItem;
                var generator = new ExcelTemplateGenerator(connect.getconnection.ConnectionString);
                string filePath = generator.GenerateTemplate(entreprise.Id, entreprise.Nom, txtFichierTemplate.Text);

                progressBar.Visible = false;
                lblProgress.Visible = false;
                btnGenererTemplate.Enabled = true;

                var result = MessageBox.Show(
                    $"Template Excel cree avec succes!\n\n" +
                    $"Emplacement : {filePath}\n\n" +
                    $"Le fichier contient :\n" +
                    $"- Listes deroulantes pour Categorie, Service, Direction\n" +
                    $"- Listes deroulantes pour Civilite, Sexe, Contrat, etc.\n" +
                    $"- Validation des donnees automatique\n" +
                    $"- Champs obligatoires identifies\n" +
                    $"- Instructions detaillees dans une feuille separee\n\n" +
                    $"Voulez-vous ouvrir le fichier maintenant?",
                    "Succes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(filePath);
                }
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                lblProgress.Visible = false;
                btnGenererTemplate.Enabled = true;

                MessageBox.Show($"Erreur lors de la génération du template:\n\n{ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnImporter_Click(object sender, EventArgs e)
        {
            if (cboEntrepriseImp.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner l'entreprise concernée.", "Attention",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFichierImport.Text) || !File.Exists(txtFichierImport.Text))
            {
                MessageBox.Show("Veuillez sélectionner un fichier Excel valide.", "Attention",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                progressBar.Visible = true;
                lblProgress.Visible = true;
                lblProgress.Text = "Lecture et validation du fichier Excel...";
                progressBar.Style = ProgressBarStyle.Marquee;
                btnImporter.Enabled = false;

                var entreprise = (EntrepriseItem)cboEntrepriseImp.SelectedItem;
                var importer = new ExcelEmployeImporter(connect.getconnection.ConnectionString, entreprise.Id, entreprise.Nom);
                var result = importer.ImportEmployees(txtFichierImport.Text);

                progressBar.Visible = false;
                progressBar.Style = ProgressBarStyle.Continuous;

                if (!result.Success)
                {
                    lblProgress.Text = result.ErrorMessage;
                    lblProgress.ForeColor = Color.Red;

                    // Afficher le formulaire de prévisualisation avec les erreurs
                    using (var previewForm = new ImportPreviewForm(result))
                    {
                        previewForm.ShowDialog(this);
                    }
                }
                else
                {
                    lblProgress.Text = result.ErrorMessage;
                    lblProgress.ForeColor = Color.Green;

                    MessageBox.Show(
                        $"Import réussi!\n\n" +
                        $"{result.ImportedCount} employé(s) importé(s) avec succès dans l'entreprise {entreprise.Nom}.",
                        "Succès",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                lblProgress.Visible = false;
                btnImporter.Enabled = true;

                MessageBox.Show($"Erreur lors de l'import:\n\n{ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class EntrepriseItem
        {
            public int Id { get; set; }
            public string Nom { get; set; }
        }
    }
}
