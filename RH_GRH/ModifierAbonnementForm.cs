using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class ModifierAbonnementForm : Form
    {
        private Dbconnect connect = new Dbconnect();
        private int idAbonnement;
        private Abonnement abonnementActuel;

        public ModifierAbonnementForm(int idAbonnement)
        {
            InitializeComponent();
            this.idAbonnement = idAbonnement;
            ChargerEmployes();
            ChargerAbonnement();
        }

        private void ChargerEmployes()
        {
            try
            {
                var dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Display", typeof(string));

                using (var con = connect.getconnection)
                {
                    con.Open();
                    const string query = @"
                        SELECT p.id_personnel AS Id,
                               CONCAT(p.nomPrenom, ' (', p.matricule, ') - ', e.nomEntreprise) AS Display
                        FROM personnel p
                        INNER JOIN entreprise e ON p.id_entreprise = e.id_entreprise
                        ORDER BY p.nomPrenom";

                    using (var cmd = new MySqlCommand(query, con))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DataRow row = dt.NewRow();
                                row["Id"] = reader["Id"];
                                row["Display"] = reader["Display"];
                                dt.Rows.Add(row);
                            }
                        }
                    }
                }

                comboBoxEmploye.DataSource = dt;
                comboBoxEmploye.DisplayMember = "Display";
                comboBoxEmploye.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement des employés :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
        }

        private void ChargerAbonnement()
        {
            try
            {
                abonnementActuel = AbonnementRepository.GetById(idAbonnement);

                if (abonnementActuel == null)
                {
                    CustomMessageBox.Show("Abonnement introuvable.", "Erreur",
                        CustomMessageBox.MessageType.Error);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

                // Remplir les champs
                comboBoxEmploye.SelectedValue = abonnementActuel.IdPersonnel;
                textBoxNom.Text = abonnementActuel.Nom;
                textBoxDescription.Text = abonnementActuel.Description;
                dateTimePickerDateDebut.Value = abonnementActuel.DateDebut;
                dateTimePickerDateFin.Value = abonnementActuel.DateFin;
                textBoxMontant.Text = abonnementActuel.Montant.ToString("0");
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement de l'abonnement :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
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
                abonnementActuel.IdPersonnel = Convert.ToInt32(comboBoxEmploye.SelectedValue);
                abonnementActuel.Nom = textBoxNom.Text.Trim();
                abonnementActuel.Description = textBoxDescription.Text.Trim();
                abonnementActuel.DateDebut = dateTimePickerDateDebut.Value.Date;
                abonnementActuel.DateFin = dateTimePickerDateFin.Value.Date;
                abonnementActuel.Montant = montant;

                AbonnementRepository.Modifier(abonnementActuel);

                CustomMessageBox.Show("Abonnement modifié avec succès!", "Succès",
                    CustomMessageBox.MessageType.Success);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de la modification :\n{ex.Message}", "Erreur",
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
