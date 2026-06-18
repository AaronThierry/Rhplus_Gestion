using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class SelectionEmployesElementsForm : Form
    {
        public class EmployeElement
        {
            public int IdPersonnel { get; set; }
            public string NomPrenom { get; set; }
            public string Matricule { get; set; }
            public string Poste { get; set; }
            public bool CNSS { get; set; }
            public bool IUTS { get; set; }
            public bool TPA { get; set; }
            public bool EffortPaix { get; set; }
        }

        public List<EmployeElement> EmployesSelectionnes { get; private set; }

        private DataTable _employes;
        private string _nomEntreprise;

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        public SelectionEmployesElementsForm(DataTable employes, string nomEntreprise)
        {
            _employes = employes;
            _nomEntreprise = nomEntreprise;
            InitializeComponent();

            // Arrondir les coins du formulaire
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            ConfigurerInterface();
            ChargerEmployes();
        }

        private void ConfigurerInterface()
        {
            // Configuration du formulaire - Design moderne et professionnel
            this.Text = "";
            this.Size = new Size(1400, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(250, 251, 252);
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Centrer le formulaire après chargement
            this.Load += (s, e) => CenterToScreen();

            // Configuration du DataGridView - Style épuré et moderne
            dataGridViewEmployes.AutoGenerateColumns = false;
            dataGridViewEmployes.AllowUserToAddRows = false;
            dataGridViewEmployes.AllowUserToDeleteRows = false;
            dataGridViewEmployes.AllowUserToResizeRows = false;
            dataGridViewEmployes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEmployes.MultiSelect = false;
            dataGridViewEmployes.RowHeadersVisible = false;
            dataGridViewEmployes.BackgroundColor = Color.White;
            dataGridViewEmployes.BorderStyle = BorderStyle.None;
            dataGridViewEmployes.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridViewEmployes.EnableHeadersVisualStyles = false;
            dataGridViewEmployes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewEmployes.ColumnHeadersHeight = 48;
            dataGridViewEmployes.RowTemplate.Height = 52;

            // En-tête avec dégradé violet sophistiqué
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(88, 43, 132);
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 8.5F, FontStyle.Bold);
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.Padding = new Padding(16, 0, 16, 0);
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Style des cellules - Design épuré
            dataGridViewEmployes.DefaultCellStyle.BackColor = Color.White;
            dataGridViewEmployes.DefaultCellStyle.ForeColor = Color.FromArgb(30, 35, 45);
            dataGridViewEmployes.DefaultCellStyle.Font = new Font("Montserrat", 9F, FontStyle.Regular);
            dataGridViewEmployes.DefaultCellStyle.Padding = new Padding(16, 8, 16, 8);
            dataGridViewEmployes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(237, 231, 246);
            dataGridViewEmployes.DefaultCellStyle.SelectionForeColor = Color.FromArgb(88, 43, 132);
            dataGridViewEmployes.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Lignes alternées avec contraste subtil
            dataGridViewEmployes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dataGridViewEmployes.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(237, 231, 246);
            dataGridViewEmployes.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(88, 43, 132);

            // Bordure subtile
            dataGridViewEmployes.GridColor = Color.FromArgb(240, 242, 245);

            // Coins arrondis pour le badge du compteur
            labelNombreEmployes.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                var rect = labelNombreEmployes.ClientRectangle;
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    int radius = 8;
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                    path.CloseFigure();

                    labelNombreEmployes.Region = new Region(path);
                }
            };

            // Colonnes avec largeurs optimisées
            var colNom = new DataGridViewTextBoxColumn
            {
                Name = "NomPrenom",
                HeaderText = "EMPLOYÉ",
                DataPropertyName = "NomPrenom",
                Width = 320,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Font = new Font("Montserrat", 9.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(20, 25, 35)
                }
            };

            var colMatricule = new DataGridViewTextBoxColumn
            {
                Name = "Matricule",
                HeaderText = "MATRICULE",
                DataPropertyName = "Matricule",
                Width = 140,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Consolas", 9F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(100, 105, 115),
                    BackColor = Color.FromArgb(248, 249, 250)
                }
            };

            var colPoste = new DataGridViewTextBoxColumn
            {
                Name = "Poste",
                HeaderText = "FONCTION",
                DataPropertyName = "Poste",
                Width = 260,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    ForeColor = Color.FromArgb(65, 70, 80)
                }
            };

            var colCNSS = new DataGridViewCheckBoxColumn
            {
                Name = "CNSS",
                HeaderText = "CNSS",
                DataPropertyName = "CNSS",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    NullValue = false
                }
            };

            var colIUTS = new DataGridViewCheckBoxColumn
            {
                Name = "IUTS",
                HeaderText = "IUTS",
                DataPropertyName = "IUTS",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    NullValue = false
                }
            };

            var colTPA = new DataGridViewCheckBoxColumn
            {
                Name = "TPA",
                HeaderText = "TPA",
                DataPropertyName = "TPA",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    NullValue = false
                }
            };

            var colEffortPaix = new DataGridViewCheckBoxColumn
            {
                Name = "EffortPaix",
                HeaderText = "EFFORT PAIX",
                DataPropertyName = "EffortPaix",
                Width = 145,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    NullValue = false
                }
            };

            dataGridViewEmployes.Columns.Add(colNom);
            dataGridViewEmployes.Columns.Add(colMatricule);
            dataGridViewEmployes.Columns.Add(colPoste);
            dataGridViewEmployes.Columns.Add(colCNSS);
            dataGridViewEmployes.Columns.Add(colIUTS);
            dataGridViewEmployes.Columns.Add(colTPA);
            dataGridViewEmployes.Columns.Add(colEffortPaix);

            // Permettre l'édition fluide des checkboxes
            dataGridViewEmployes.EditMode = DataGridViewEditMode.EditOnEnter;

            // Ombre portée sur le grid
            dataGridViewEmployes.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(10, 0, 0, 0), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, dataGridViewEmployes.Width - 1, dataGridViewEmployes.Height - 1);
                }
            };
        }

        private void ChargerEmployes()
        {
            try
            {
                var dt = new DataTable();
                dt.Columns.Add("IdPersonnel", typeof(int));
                dt.Columns.Add("NomPrenom", typeof(string));
                dt.Columns.Add("Matricule", typeof(string));
                dt.Columns.Add("Poste", typeof(string));
                dt.Columns.Add("CNSS", typeof(bool));
                dt.Columns.Add("IUTS", typeof(bool));
                dt.Columns.Add("TPA", typeof(bool));
                dt.Columns.Add("EffortPaix", typeof(bool));

                foreach (DataRow row in _employes.Rows)
                {
                    var newRow = dt.NewRow();
                    newRow["IdPersonnel"] = row["IdPersonnel"];
                    newRow["NomPrenom"] = row["NomPrenom"];
                    newRow["Matricule"] = row["Matricule"];
                    newRow["Poste"] = row["Poste"] != DBNull.Value ? row["Poste"] : "";
                    // Cocher tous les éléments par défaut
                    newRow["CNSS"] = true;
                    newRow["IUTS"] = true;
                    newRow["TPA"] = true;
                    newRow["EffortPaix"] = true;
                    dt.Rows.Add(newRow);
                }

                dataGridViewEmployes.DataSource = dt;
                labelNombreEmployes.Text = $"{dt.Rows.Count} employé(s) non conforme(s)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des employés :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonToutCocher_Click(object sender, EventArgs e)
        {
            CocherTout(true);
        }

        private void buttonToutDecocher_Click(object sender, EventArgs e)
        {
            CocherTout(false);
        }

        private void CocherTout(bool etat)
        {
            var dt = dataGridViewEmployes.DataSource as DataTable;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    row["CNSS"] = etat;
                    row["IUTS"] = etat;
                    row["TPA"] = etat;
                    row["EffortPaix"] = etat;
                }
                dataGridViewEmployes.Refresh();
            }
        }

        private void buttonValider_Click(object sender, EventArgs e)
        {
            try
            {
                EmployesSelectionnes = new List<EmployeElement>();

                var dt = dataGridViewEmployes.DataSource as DataTable;
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var employe = new EmployeElement
                        {
                            IdPersonnel = Convert.ToInt32(row["IdPersonnel"]),
                            NomPrenom = row["NomPrenom"].ToString(),
                            Matricule = row["Matricule"].ToString(),
                            Poste = row["Poste"].ToString(),
                            CNSS = Convert.ToBoolean(row["CNSS"]),
                            IUTS = Convert.ToBoolean(row["IUTS"]),
                            TPA = Convert.ToBoolean(row["TPA"]),
                            EffortPaix = Convert.ToBoolean(row["EffortPaix"])
                        };
                        EmployesSelectionnes.Add(employe);
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la validation :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
