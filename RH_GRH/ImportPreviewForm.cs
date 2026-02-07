using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace RH_GRH
{
    /// <summary>
    /// Formulaire pour prévisualiser les données avant l'import
    /// </summary>
    public partial class ImportPreviewForm : Form
    {
        private ImportResult importResult;
        private DataGridView dgvPreview;
        private Label lblSummary;
        private Button btnImport;
        private Button btnCancel;
        private Panel pnlButtons;
        private TabControl tabControl;
        private TabPage tabValid;
        private TabPage tabInvalid;
        private DataGridView dgvInvalid;

        public bool UserConfirmedImport { get; private set; }

        public ImportPreviewForm(ImportResult result)
        {
            this.importResult = result;
            InitializeComponents();
            LoadData();
        }

        private void InitializeComponents()
        {
            this.Text = "Prévisualisation de l'import Excel";
            this.Size = new Size(1600, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new Size(1200, 600);

            // Summary label
            lblSummary = new Label
            {
                Dock = DockStyle.Top,
                Height = 60,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Padding = new Padding(10),
                BackColor = Color.FromArgb(240, 240, 240),
                TextAlign = ContentAlignment.MiddleLeft
            };
            this.Controls.Add(lblSummary);

            // TabControl
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F)
            };

            // Tab Valid
            tabValid = new TabPage("Données valides");
            dgvPreview = CreateDataGridView();
            tabValid.Controls.Add(dgvPreview);
            tabControl.TabPages.Add(tabValid);

            // Tab Invalid
            tabInvalid = new TabPage("Données invalides");
            dgvInvalid = CreateDataGridView();
            tabInvalid.Controls.Add(dgvInvalid);
            tabControl.TabPages.Add(tabInvalid);

            this.Controls.Add(tabControl);

            // Buttons panel
            pnlButtons = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            btnCancel = new Button
            {
                Text = "Annuler",
                Size = new Size(120, 35),
                Location = new Point(this.ClientSize.Width - 270, 12),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.FromArgb(220, 220, 220),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) =>
            {
                UserConfirmedImport = false;
                this.Close();
            };

            btnImport = new Button
            {
                Text = "Importer",
                Size = new Size(120, 35),
                Location = new Point(this.ClientSize.Width - 140, 12),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = importResult.InvalidRows.Count == 0
            };
            btnImport.FlatAppearance.BorderSize = 0;
            btnImport.Click += (s, e) =>
            {
                UserConfirmedImport = true;
                this.Close();
            };

            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnImport);
            this.Controls.Add(pnlButtons);
        }

        private DataGridView CreateDataGridView()
        {
            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeColumns = true,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = true,
                RowHeadersWidth = 50,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                ColumnHeadersHeight = 35,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                Font = new Font("Consolas", 9F),
                EnableHeadersVisualStyles = false,
                ScrollBars = ScrollBars.Both
            };

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 112);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 5, 8, 5);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.RowTemplate.Height = 32;
            dgv.DefaultCellStyle.Padding = new Padding(5, 3, 5, 3);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);

            return dgv;
        }

        private void LoadData()
        {
            // Summary
            string summary = $"Total de lignes: {importResult.TotalRows}\n";
            summary += $"Données valides: {importResult.ValidRows.Count}\n";
            summary += $"Données invalides: {importResult.InvalidRows.Count}";

            if (importResult.InvalidRows.Count > 0)
            {
                summary += $"\n\n⚠ Corrigez les erreurs avant d'importer.";
                lblSummary.ForeColor = Color.Red;
            }
            else
            {
                summary += $"\n\n✓ Toutes les données sont valides. Vous pouvez procéder à l'import.";
                lblSummary.ForeColor = Color.Green;
            }

            lblSummary.Text = summary;

            // Load valid data
            LoadGridView(dgvPreview, importResult.ValidRows, false);

            // Load invalid data
            LoadGridView(dgvInvalid, importResult.InvalidRows, true);

            // Select appropriate tab
            if (importResult.InvalidRows.Count > 0)
            {
                tabControl.SelectedTab = tabInvalid;
            }
        }

        private void LoadGridView(DataGridView dgv, System.Collections.Generic.List<EmployeeImportRow> rows, bool showErrors)
        {
            dgv.Columns.Clear();

            // Add columns with MinimumWidth for better display
            dgv.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Ligne",
                HeaderText = "#",
                MinimumWidth = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 60
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Matricule",
                HeaderText = "Matricule",
                MinimumWidth = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Identification",
                HeaderText = "N° Identification",
                MinimumWidth = 120,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Civilite",
                HeaderText = "Civ.",
                MinimumWidth = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Nom",
                HeaderText = "Nom",
                MinimumWidth = 120,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Prenom",
                HeaderText = "Prénom",
                MinimumWidth = 120,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "DateNaissance",
                HeaderText = "Date Naiss.",
                MinimumWidth = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Contrat",
                HeaderText = "Contrat",
                MinimumWidth = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Cadre",
                HeaderText = "Statut",
                MinimumWidth = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "DateEmbauche",
                HeaderText = "Embauche",
                MinimumWidth = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Salaire",
                HeaderText = "Salaire",
                MinimumWidth = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Categorie",
                HeaderText = "Catégorie",
                MinimumWidth = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Service",
                HeaderText = "Service",
                MinimumWidth = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Direction",
                HeaderText = "Direction",
                MinimumWidth = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            if (showErrors)
            {
                var errCol = new DataGridViewTextBoxColumn {
                    Name = "Erreurs",
                    HeaderText = "Erreurs / Messages",
                    MinimumWidth = 200,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                };
                errCol.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                errCol.DefaultCellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                errCol.DefaultCellStyle.Font = new Font("Segoe UI", 8.5F);
                dgv.Columns.Add(errCol);
            }

            // Add rows
            foreach (var row in rows)
            {
                int index = dgv.Rows.Add(
                    row.RowNumber,
                    row.Matricule,
                    row.Identification,
                    row.Civilite,
                    row.Nom,
                    row.Prenom,
                    row.DateNaissance?.ToString("dd/MM/yyyy") ?? "",
                    row.Contrat,
                    row.Cadre,
                    row.DateEntree?.ToString("dd/MM/yyyy") ?? "",
                    row.SalaireMoyen?.ToString("N0") ?? "0",
                    row.NomCategorie,
                    row.NomService,
                    row.NomDirection
                );

                if (showErrors)
                {
                    dgv.Rows[index].Cells["Erreurs"].Value = row.ErrorSummary;
                    dgv.Rows[index].DefaultCellStyle.BackColor = Color.FromArgb(255, 240, 240);
                }
            }
        }
    }
}
