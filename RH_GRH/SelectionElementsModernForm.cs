using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace RH_GRH
{
    public partial class SelectionElementsModernForm : Form
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

        public SelectionElementsModernForm(DataTable employes, string nomEntreprise)
        {
            _employes = employes;
            _nomEntreprise = nomEntreprise;
            InitializeComponent();

            // Arrondir les coins du formulaire
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            ConfigurerDataGrid();
            ChargerEmployes();
        }

        private void ConfigurerDataGrid()
        {
            // Configuration moderne du DataGridView
            dataGridViewEmployes.AutoGenerateColumns = false;
            dataGridViewEmployes.AllowUserToAddRows = false;
            dataGridViewEmployes.AllowUserToDeleteRows = false;
            dataGridViewEmployes.AllowUserToResizeRows = false;
            dataGridViewEmployes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEmployes.MultiSelect = false;
            dataGridViewEmployes.RowHeadersVisible = false;
            dataGridViewEmployes.BackgroundColor = Color.White;
            dataGridViewEmployes.BorderStyle = BorderStyle.None;
            dataGridViewEmployes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewEmployes.EnableHeadersVisualStyles = false;
            dataGridViewEmployes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewEmployes.ColumnHeadersHeight = 50;
            dataGridViewEmployes.RowTemplate.Height = 55;

            // En-tête violet moderne
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(88, 43, 132);
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 9F, FontStyle.Bold);
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 10, 0);

            // Style des cellules
            dataGridViewEmployes.DefaultCellStyle.BackColor = Color.White;
            dataGridViewEmployes.DefaultCellStyle.ForeColor = Color.FromArgb(30, 35, 45);
            dataGridViewEmployes.DefaultCellStyle.Font = new Font("Montserrat", 8.5F);
            dataGridViewEmployes.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dataGridViewEmployes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(237, 231, 246);
            dataGridViewEmployes.DefaultCellStyle.SelectionForeColor = Color.FromArgb(88, 43, 132);

            // Lignes alternées
            dataGridViewEmployes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dataGridViewEmployes.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(237, 231, 246);
            dataGridViewEmployes.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(88, 43, 132);

            // Bordures
            dataGridViewEmployes.GridColor = Color.FromArgb(230, 230, 235);

            // Colonnes
            var colNom = new DataGridViewTextBoxColumn
            {
                Name = "NomPrenom",
                HeaderText = "EMPLOYÉ",
                DataPropertyName = "NomPrenom",
                Width = 416,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Font = new Font("Montserrat", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(20, 25, 35)
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
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    NullValue = false
                }
            };

            dataGridViewEmployes.Columns.Add(colNom);
            dataGridViewEmployes.Columns.Add(colCNSS);
            dataGridViewEmployes.Columns.Add(colIUTS);
            dataGridViewEmployes.Columns.Add(colTPA);
            dataGridViewEmployes.Columns.Add(colEffortPaix);

            dataGridViewEmployes.EditMode = DataGridViewEditMode.EditOnEnter;
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
                    newRow["CNSS"] = true;
                    newRow["IUTS"] = true;
                    newRow["TPA"] = true;
                    newRow["EffortPaix"] = true;
                    dt.Rows.Add(newRow);
                }

                dataGridViewEmployes.DataSource = dt;
                int count = dt.Rows.Count;
                labelCounter.Text = count > 1 ? $"{count} EMPLOYÉS" : count == 1 ? "1 EMPLOYÉ" : "AUCUN EMPLOYÉ";
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
