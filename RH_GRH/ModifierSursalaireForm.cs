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
    public partial class ModifierSursalaireForm : Form
    {
        private Dbconnect connect = new Dbconnect();
        private int idSursalaire;
        private Sursalaire sursalaireActuel;

        public ModifierSursalaireForm(int idSursalaire)
        {
            InitializeComponent();
            this.idSursalaire = idSursalaire;
            ChargerEmployes();
            ChargerSursalaire();
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

        private void ChargerSursalaire()
        {
            try
            {
                sursalaireActuel = SursalaireRepository.GetById(idSursalaire);

                if (sursalaireActuel == null)
                {
                    CustomMessageBox.Show("Sursalaire introuvable.", "Erreur",
                        CustomMessageBox.MessageType.Error);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

                // Remplir les champs
                comboBoxEmploye.SelectedValue = sursalaireActuel.IdPersonnel;
                textBoxNom.Text = sursalaireActuel.Nom;
                textBoxDescription.Text = sursalaireActuel.Description;
                textBoxMontant.Text = sursalaireActuel.Montant.ToString("0");
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement du sursalaire :\n{ex.Message}",
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
                CustomMessageBox.Show("Veuillez saisir le nom du sursalaire.", "Validation",
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

            try
            {
                sursalaireActuel.IdPersonnel = Convert.ToInt32(comboBoxEmploye.SelectedValue);
                sursalaireActuel.Nom = textBoxNom.Text.Trim();
                sursalaireActuel.Description = textBoxDescription.Text.Trim();
                sursalaireActuel.Montant = montant;

                SursalaireRepository.Modifier(sursalaireActuel);

                CustomMessageBox.Show("Sursalaire modifié avec succès!", "Succès",
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
