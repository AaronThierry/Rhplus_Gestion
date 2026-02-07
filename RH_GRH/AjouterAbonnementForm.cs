using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class AjouterAbonnementForm : Form
    {
        private Dbconnect connect = new Dbconnect();
        private DataTable tousLesEmployes;
        private DataTable employesFiltres;

        public AjouterAbonnementForm()
        {
            InitializeComponent();
            ChargerTousLesEmployes();
        }

        private void ChargerTousLesEmployes()
        {
            try
            {
                using (var con = connect.getconnection)
                {
                    con.Open();
                    const string query = @"
                        SELECT p.id_personnel AS Id,
                               p.nomPrenom AS Nom,
                               p.matricule AS Matricule,
                               e.nomEntreprise AS Entreprise,
                               CONCAT(p.nomPrenom, ' (', p.matricule, ') - ', e.nomEntreprise) AS Display
                        FROM personnel p
                        INNER JOIN entreprise e ON p.id_entreprise = e.id_entreprise
                        ORDER BY p.nomPrenom";

                    using (var cmd = new MySqlCommand(query, con))
                    {
                        var adapter = new MySqlDataAdapter(cmd);
                        tousLesEmployes = new DataTable();
                        adapter.Fill(tousLesEmployes);

                        // Créer une copie pour les filtres
                        employesFiltres = tousLesEmployes.Copy();

                        // Ajouter la ligne par défaut
                        DataRow row = employesFiltres.NewRow();
                        row["Id"] = 0;
                        row["Display"] = "-- Sélectionner un employé --";
                        employesFiltres.Rows.InsertAt(row, 0);

                        comboBoxEmploye.DataSource = employesFiltres;
                        comboBoxEmploye.DisplayMember = "Display";
                        comboBoxEmploye.ValueMember = "Id";
                        comboBoxEmploye.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement des employés :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
        }

        private void textBoxRecherche_TextChanged(object sender, EventArgs e)
        {
            FiltrerEmployes(textBoxRecherche.Text.Trim());
        }

        private void FiltrerEmployes(string recherche)
        {
            try
            {
                if (tousLesEmployes == null || tousLesEmployes.Rows.Count == 0)
                    return;

                // Si la recherche est vide, afficher tous les employés
                if (string.IsNullOrWhiteSpace(recherche))
                {
                    employesFiltres = tousLesEmployes.Copy();
                }
                else
                {
                    // Filtrer les employés selon le texte de recherche
                    var rechercheLower = recherche.ToLower();
                    var rows = tousLesEmployes.AsEnumerable()
                        .Where(row =>
                            row.Field<string>("Nom").ToLower().Contains(rechercheLower) ||
                            row.Field<string>("Matricule").ToLower().Contains(rechercheLower) ||
                            row.Field<string>("Entreprise").ToLower().Contains(rechercheLower)
                        );

                    employesFiltres = rows.Any() ? rows.CopyToDataTable() : tousLesEmployes.Clone();
                }

                // Ajouter la ligne par défaut
                DataRow defaultRow = employesFiltres.NewRow();
                defaultRow["Id"] = 0;
                defaultRow["Display"] = employesFiltres.Rows.Count > 0
                    ? $"-- {employesFiltres.Rows.Count} employé(s) trouvé(s) --"
                    : "-- Aucun employé trouvé --";
                employesFiltres.Rows.InsertAt(defaultRow, 0);

                // Mettre à jour le ComboBox
                comboBoxEmploye.DataSource = employesFiltres;
                comboBoxEmploye.DisplayMember = "Display";
                comboBoxEmploye.ValueMember = "Id";
                comboBoxEmploye.SelectedIndex = 0;

                // Si un seul résultat, le sélectionner automatiquement
                if (employesFiltres.Rows.Count == 2 && !string.IsNullOrWhiteSpace(recherche))
                {
                    comboBoxEmploye.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                // Gestion silencieuse des erreurs de filtrage
                System.Diagnostics.Debug.WriteLine($"Erreur filtrage: {ex.Message}");
            }
        }

        private void buttonEnregistrer_Click(object sender, EventArgs e)
        {
            // Validation
            if (comboBoxEmploye.SelectedValue == null || Convert.ToInt32(comboBoxEmploye.SelectedValue) == 0)
            {
                CustomMessageBox.Show("Veuillez sélectionner un employé.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                comboBoxEmploye.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxNom.Text))
            {
                CustomMessageBox.Show("Veuillez saisir le nom de l'abonnement.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                textBoxNom.Focus();
                return;
            }

            if (!decimal.TryParse(textBoxMontant.Text, out decimal montant) || montant < 0)
            {
                CustomMessageBox.Show("Veuillez saisir un montant valide.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                textBoxMontant.Focus();
                return;
            }

            // Validation des dates
            if (dateTimePickerDateDebut.Value.Date > dateTimePickerDateFin.Value.Date)
            {
                CustomMessageBox.Show("La date de début ne peut pas être après la date de fin.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                dateTimePickerDateDebut.Focus();
                return;
            }

            try
            {
                var abonnement = new Abonnement
                {
                    IdPersonnel = Convert.ToInt32(comboBoxEmploye.SelectedValue),
                    Nom = textBoxNom.Text.Trim(),
                    Description = textBoxDescription.Text.Trim(),
                    DateDebut = dateTimePickerDateDebut.Value.Date,
                    DateFin = dateTimePickerDateFin.Value.Date,
                    Montant = montant
                };

                AbonnementRepository.Ajouter(abonnement);

                CustomMessageBox.Show("Abonnement ajouté avec succès!", "Succès",
                    CustomMessageBox.MessageType.Success);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de l'ajout :\n{ex.Message}", "Erreur",
                    CustomMessageBox.MessageType.Error);
            }
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Autoriser uniquement les chiffres dans le montant
        private void textBoxMontant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
