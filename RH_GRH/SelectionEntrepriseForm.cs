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
            this.Size = new Size(800, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Configuration du DataGridView - Style moderne et épuré
            dataGridViewEntreprises.AutoGenerateColumns = false;
            dataGridViewEntreprises.AllowUserToAddRows = false;
            dataGridViewEntreprises.AllowUserToDeleteRows = false;
            dataGridViewEntreprises.AllowUserToResizeRows = false;
            dataGridViewEntreprises.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEntreprises.MultiSelect = false;
            dataGridViewEntreprises.RowHeadersVisible = false;
            dataGridViewEntreprises.BackgroundColor = Color.White;
            dataGridViewEntreprises.BorderStyle = BorderStyle.None;
            dataGridViewEntreprises.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewEntreprises.EnableHeadersVisualStyles = false;
            dataGridViewEntreprises.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewEntreprises.ColumnHeadersHeight = 42;
            dataGridViewEntreprises.RowTemplate.Height = 40;

            // Style de l'en-tête - Élégant avec MidnightBlue
            dataGridViewEntreprises.ColumnHeadersDefaultCellStyle.BackColor = Color.MidnightBlue;
            dataGridViewEntreprises.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewEntreprises.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 9.5F, FontStyle.Bold);
            dataGridViewEntreprises.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewEntreprises.ColumnHeadersDefaultCellStyle.Padding = new Padding(12, 0, 12, 0);
            dataGridViewEntreprises.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.MidnightBlue;

            // Style des cellules par défaut - Soft et lisible
            dataGridViewEntreprises.DefaultCellStyle.BackColor = Color.White;
            dataGridViewEntreprises.DefaultCellStyle.ForeColor = Color.FromArgb(50, 50, 50);
            dataGridViewEntreprises.DefaultCellStyle.Font = new Font("Montserrat", 9F, FontStyle.Regular);
            dataGridViewEntreprises.DefaultCellStyle.Padding = new Padding(12, 8, 12, 8);
            dataGridViewEntreprises.DefaultCellStyle.SelectionBackColor = Color.FromArgb(176, 196, 222); // LightSteelBlue
            dataGridViewEntreprises.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 25, 112); // MidnightBlue foncé

            // Style des lignes alternées - Très subtil
            dataGridViewEntreprises.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 252);
            dataGridViewEntreprises.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(50, 50, 50);
            dataGridViewEntreprises.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(176, 196, 222);
            dataGridViewEntreprises.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 25, 112);

            // Couleur de la grille - Très légère
            dataGridViewEntreprises.GridColor = Color.FromArgb(235, 237, 242);

            // Colonnes avec style épuré
            var colId = new DataGridViewTextBoxColumn
            {
                Name = "id_entreprise",
                HeaderText = "ID",
                DataPropertyName = "id_entreprise",
                Width = 55,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Montserrat", 7.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(100, 100, 120)
                }
            };

            var colSigle = new DataGridViewTextBoxColumn
            {
                Name = "sigle",
                HeaderText = "Sigle",
                DataPropertyName = "sigle",
                Width = 180,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Font = new Font(new FontFamily("Montserrat"), 9F, FontStyle.Bold),
                    ForeColor = Color.MidnightBlue
                }
            };

            var colNom = new DataGridViewTextBoxColumn
            {
                Name = "nomEntreprise",
                HeaderText = "Nom de l'Entreprise",
                DataPropertyName = "nomEntreprise",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Font = new Font("Montserrat", 9F, FontStyle.Regular)
                }
            };

            dataGridViewEntreprises.Columns.Add(colId);
            dataGridViewEntreprises.Columns.Add(colSigle);
            dataGridViewEntreprises.Columns.Add(colNom);

            // Double-clic pour sélectionner
            dataGridViewEntreprises.CellDoubleClick += DataGridViewEntreprises_CellDoubleClick;

            // Effet hover personnalisé
            dataGridViewEntreprises.CellMouseEnter += DataGridViewEntreprises_CellMouseEnter;
            dataGridViewEntreprises.CellMouseLeave += DataGridViewEntreprises_CellMouseLeave;

            // Définir la date par défaut à la date du jour
            dateTimePickerDebut.Value = DateTime.Now;
            dateTimePickerFin.Value = DateTime.Now;
        }

        private void DataGridViewEntreprises_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridViewEntreprises.Cursor = Cursors.Hand;
            }
        }

        private void DataGridViewEntreprises_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewEntreprises.Cursor = Cursors.Default;
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
