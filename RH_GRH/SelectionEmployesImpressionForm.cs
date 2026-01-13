using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class SelectionEmployesImpressionForm : Form
    {
        public List<int> EmployesSelectionnes { get; private set; }
        public DateTime PeriodeDebut { get; private set; }
        public DateTime PeriodeFin { get; private set; }
        public string DossierDestination { get; private set; }

        private DataTable dtEmployes;

        public SelectionEmployesImpressionForm(int idEntreprise)
        {
            InitializeComponent();
            ChargerEmployes(idEntreprise);
            ConfigurerInterface();
        }

        private void ConfigurerInterface()
        {
            // Configurer les dates par défaut (mois en cours)
            var today = DateTime.Today;
            guna2DateTimePickerDebut.Value = new DateTime(today.Year, today.Month, 1);
            guna2DateTimePickerFin.Value = guna2DateTimePickerDebut.Value.AddMonths(1).AddDays(-1);

            // Configurer le DataGridView
            dataGridViewEmployes.AutoGenerateColumns = false;
            dataGridViewEmployes.AllowUserToAddRows = false;
            dataGridViewEmployes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEmployes.MultiSelect = true;

            // Colonne de sélection (checkbox)
            var colCheck = new DataGridViewCheckBoxColumn
            {
                Name = "Selectionne",
                HeaderText = "✓",
                Width = 40,
                DataPropertyName = "Selectionne"
            };
            dataGridViewEmployes.Columns.Add(colCheck);

            // Colonne Matricule
            var colMatricule = new DataGridViewTextBoxColumn
            {
                Name = "Matricule",
                HeaderText = "Matricule",
                DataPropertyName = "Matricule",
                Width = 100,
                ReadOnly = true
            };
            dataGridViewEmployes.Columns.Add(colMatricule);

            // Colonne Nom
            var colNom = new DataGridViewTextBoxColumn
            {
                Name = "NomPrenom",
                HeaderText = "Nom et Prénom",
                DataPropertyName = "NomPrenom",
                Width = 250,
                ReadOnly = true
            };
            dataGridViewEmployes.Columns.Add(colNom);

            // Colonne Type Contrat
            var colTypeContrat = new DataGridViewTextBoxColumn
            {
                Name = "TypeContrat",
                HeaderText = "Type Contrat",
                DataPropertyName = "TypeContrat",
                Width = 100,
                ReadOnly = true
            };
            dataGridViewEmployes.Columns.Add(colTypeContrat);

            // Style du header
            dataGridViewEmployes.EnableHeadersVisualStyles = false;
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(94, 148, 255);
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 10F, FontStyle.Bold);
        }

        private void ChargerEmployes(int idEntreprise)
        {
            try
            {
                var today = DateTime.Today;
                var debut = new DateTime(today.Year, today.Month, 1);
                var fin = debut.AddMonths(1).AddDays(-1);

                var employes = BatchBulletinService.GetEmployesEntreprise(
                    idEntreprise,
                    debut,
                    fin,
                    comboBoxTypeContrat.SelectedItem?.ToString());

                dtEmployes = new DataTable();
                dtEmployes.Columns.Add("Selectionne", typeof(bool));
                dtEmployes.Columns.Add("IdPersonnel", typeof(int));
                dtEmployes.Columns.Add("Matricule", typeof(string));
                dtEmployes.Columns.Add("NomPrenom", typeof(string));
                dtEmployes.Columns.Add("TypeContrat", typeof(string));

                foreach (var emp in employes)
                {
                    dtEmployes.Rows.Add(true, emp.IdPersonnel, emp.Matricule, emp.NomPrenom, emp.TypeContrat);
                }

                dataGridViewEmployes.DataSource = dtEmployes;
                MettreAJourCompteur();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des employés : {ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MettreAJourCompteur()
        {
            if (dtEmployes == null) return;

            int total = dtEmployes.Rows.Count;
            int selectionnes = dtEmployes.AsEnumerable()
                .Count(row => row.Field<bool>("Selectionne"));

            labelCompteur.Text = $"{selectionnes} / {total} employé(s) sélectionné(s)";
        }

        private void checkBoxTout_CheckedChanged(object sender, EventArgs e)
        {
            if (dtEmployes == null) return;

            foreach (DataRow row in dtEmployes.Rows)
            {
                row["Selectionne"] = checkBoxTout.Checked;
            }

            dataGridViewEmployes.Refresh();
            MettreAJourCompteur();
        }

        private void dataGridViewEmployes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) // Colonne checkbox
            {
                MettreAJourCompteur();
            }
        }

        private void buttonParcourir_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Sélectionnez le dossier de destination des bulletins";
                folderDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxDossier.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void buttonGenerer_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(textBoxDossier.Text))
            {
                MessageBox.Show("Veuillez sélectionner un dossier de destination.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectionnes = dtEmployes.AsEnumerable()
                .Where(row => row.Field<bool>("Selectionne"))
                .Select(row => row.Field<int>("IdPersonnel"))
                .ToList();

            if (selectionnes.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner au moins un employé.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Stocker les valeurs
            EmployesSelectionnes = selectionnes;
            PeriodeDebut = guna2DateTimePickerDebut.Value;
            PeriodeFin = guna2DateTimePickerFin.Value;
            DossierDestination = textBoxDossier.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comboBoxTypeContrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Recharger les employés avec le nouveau filtre
            if (dtEmployes != null)
            {
                var idEntreprise = dtEmployes.Rows.Count > 0
                    ? Convert.ToInt32(dtEmployes.Rows[0]["IdPersonnel"])
                    : 0;

                // À améliorer : stocker l'ID entreprise dans une variable de classe
            }
        }
    }
}
