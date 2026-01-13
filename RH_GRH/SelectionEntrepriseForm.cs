using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RH_GRH
{
    public partial class SelectionEntrepriseForm : Form
    {
        public int EntrepriseSelectionnee { get; private set; }
        public string NomEntrepriseSelectionnee { get; private set; }
        public DateTime DateDebut { get; private set; }
        public DateTime DateFin { get; private set; }

        public SelectionEntrepriseForm()
        {
            InitializeComponent();
            ConfigurerInterface();
            ChargerEntreprises();
        }

        private void ConfigurerInterface()
        {
            // Configuration du formulaire
            this.Text = "Sélection d'entreprise";
            this.Size = new Size(600, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Configuration du DataGridView
            dataGridViewEntreprises.AutoGenerateColumns = false;
            dataGridViewEntreprises.AllowUserToAddRows = false;
            dataGridViewEntreprises.AllowUserToDeleteRows = false;
            dataGridViewEntreprises.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEntreprises.MultiSelect = false;
            dataGridViewEntreprises.RowHeadersVisible = false;
            dataGridViewEntreprises.BackgroundColor = Color.White;
            dataGridViewEntreprises.DefaultCellStyle.SelectionBackColor = Color.FromArgb(94, 148, 255);
            dataGridViewEntreprises.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridViewEntreprises.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dataGridViewEntreprises.BorderStyle = BorderStyle.None;
            dataGridViewEntreprises.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewEntreprises.EnableHeadersVisualStyles = false;
            dataGridViewEntreprises.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewEntreprises.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(94, 148, 255);
            dataGridViewEntreprises.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewEntreprises.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewEntreprises.ColumnHeadersHeight = 40;
            dataGridViewEntreprises.RowTemplate.Height = 35;

            // Colonnes
            dataGridViewEntreprises.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "id_entreprise",
                HeaderText = "ID",
                DataPropertyName = "id_entreprise",
                Width = 60,
                ReadOnly = true
            });

            dataGridViewEntreprises.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "sigle",
                HeaderText = "Sigle",
                DataPropertyName = "sigle",
                Width = 100,
                ReadOnly = true
            });

            dataGridViewEntreprises.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nomEntreprise",
                HeaderText = "Nom de l'entreprise",
                DataPropertyName = "nomEntreprise",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            // Double-clic pour sélectionner
            dataGridViewEntreprises.CellDoubleClick += DataGridViewEntreprises_CellDoubleClick;
        }

        private void ChargerEntreprises()
        {
            try
            {
                var dt = new DataTable();
                dt.Columns.Add("id_entreprise", typeof(int));
                dt.Columns.Add("sigle", typeof(string));
                dt.Columns.Add("nomEntreprise", typeof(string));

                var connect = new Dbconnect();
                using (var con = connect.getconnection)
                {
                    con.Open();

                    string sql = @"
                        SELECT
                            id_entreprise,
                            sigle,
                            nomEntreprise
                        FROM entreprise
                        ORDER BY nomEntreprise";

                    using (var cmd = new MySqlCommand(sql, con))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = dt.NewRow();
                            row["id_entreprise"] = reader.GetInt32("id_entreprise");
                            row["sigle"] = reader.IsDBNull(reader.GetOrdinal("sigle")) ? "" : reader.GetString("sigle");
                            row["nomEntreprise"] = reader.GetString("nomEntreprise");
                            dt.Rows.Add(row);
                        }
                    }
                }

                dataGridViewEntreprises.DataSource = dt;
                labelNombreEntreprises.Text = $"{dt.Rows.Count} entreprise(s) disponible(s)";

                // Sélectionner la première ligne par défaut
                if (dataGridViewEntreprises.Rows.Count > 0)
                {
                    dataGridViewEntreprises.Rows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des entreprises :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridViewEntreprises_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ValiderSelection();
            }
        }

        private void ValiderSelection()
        {
            if (dataGridViewEntreprises.SelectedRows.Count > 0)
            {
                // Valider les dates
                if (dateTimePickerDebut.Value.Date > dateTimePickerFin.Value.Date)
                {
                    MessageBox.Show("La date de début ne peut pas être supérieure à la date de fin.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var row = dataGridViewEntreprises.SelectedRows[0];
                EntrepriseSelectionnee = Convert.ToInt32(row.Cells["id_entreprise"].Value);
                NomEntrepriseSelectionnee = row.Cells["nomEntreprise"].Value.ToString();
                DateDebut = dateTimePickerDebut.Value.Date;
                DateFin = dateTimePickerFin.Value.Date;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une entreprise.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonValider_Click(object sender, EventArgs e)
        {
            ValiderSelection();
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
