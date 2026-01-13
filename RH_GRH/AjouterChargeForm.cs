using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class AjouterChargeForm : Form
    {
        private Dbconnect connect = new Dbconnect();

        public AjouterChargeForm()
        {
            InitializeComponent();
            InitialiserDonnees();
            ConfigurerRaccourcisClavier();

            // Focus automatique sur le premier champ
            this.Shown += (s, e) => comboBoxEntreprise.Focus();
        }

        private void ConfigurerRaccourcisClavier()
        {
            // Enter = Valider, Escape = Annuler
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    buttonValider_Click(null, null);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    buttonAnnuler_Click(null, null);
                    e.Handled = true;
                }
            };
        }

        private void InitialiserDonnees()
        {
            // Charger les entreprises
            EntrepriseClass.ChargerEntreprises(comboBoxEntreprise, null, true);

            // Initialiser le type de charge
            comboBoxType.Items.Clear();
            comboBoxType.Items.Add("-- Sélectionner --");
            comboBoxType.Items.Add("Épouse");
            comboBoxType.Items.Add("Enfant");
            comboBoxType.SelectedIndex = 0;

            // Initialiser la scolarisation
            comboBoxScolarisation.Items.Clear();
            comboBoxScolarisation.Items.Add("-- Sélectionner --");
            comboBoxScolarisation.Items.Add("Oui");
            comboBoxScolarisation.Items.Add("Non");
            comboBoxScolarisation.SelectedIndex = 0;
            comboBoxScolarisation.Enabled = false;

            // Événements
            comboBoxEntreprise.SelectedIndexChanged += ComboBoxEntreprise_SelectedIndexChanged;
            comboBoxType.SelectedIndexChanged += ComboBoxType_SelectedIndexChanged;
        }

        private void ComboBoxEntreprise_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEntreprise.SelectedValue != null &&
                int.TryParse(comboBoxEntreprise.SelectedValue.ToString(), out int idEnt) && idEnt > 0)
            {
                // Charger les employés de cette entreprise
                ChargerEmployes(idEnt);
            }
            else
            {
                comboBoxEmploye.DataSource = null;
            }
        }

        private void ChargerEmployes(int idEntreprise)
        {
            try
            {
                var localConnect = new Dbconnect();
                using (var con = localConnect.getconnection)
                {
                    con.Open();
                    const string query = @"
                        SELECT p.id_personnel AS Id,
                               CONCAT(p.nomPrenom, ' (', p.matricule, ')') AS Nom
                        FROM personnel p
                        WHERE p.id_entreprise = @idEnt
                        ORDER BY p.nomPrenom";

                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@idEnt", idEntreprise);
                        var adapter = new MySqlDataAdapter(cmd);
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        // Ajouter une ligne vide au début
                        DataRow row = dt.NewRow();
                        row["Id"] = 0;
                        row["Nom"] = "-- Sélectionner un employé --";
                        dt.Rows.InsertAt(row, 0);

                        comboBoxEmploye.DataSource = dt;
                        comboBoxEmploye.DisplayMember = "Nom";
                        comboBoxEmploye.ValueMember = "Id";
                        comboBoxEmploye.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement des employés :\n{ex.Message}", "Erreur",
                    CustomMessageBox.MessageType.Error);
            }
        }

        private void ComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Activer la scolarisation uniquement si "Enfant" est sélectionné
            bool estEnfant = comboBoxType.SelectedItem?.ToString() == "Enfant";
            comboBoxScolarisation.Enabled = estEnfant;

            if (!estEnfant)
            {
                comboBoxScolarisation.SelectedIndex = 0;
            }
        }

        private bool ValiderFormulaire(out Charge charge)
        {
            charge = null;

            // Validation
            if (comboBoxEntreprise.SelectedValue == null ||
                !int.TryParse(comboBoxEntreprise.SelectedValue.ToString(), out int idEnt) || idEnt <= 0)
            {
                CustomMessageBox.Show("Veuillez sélectionner une entreprise.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                comboBoxEntreprise.Focus();
                return false;
            }

            if (comboBoxEmploye.SelectedValue == null ||
                !int.TryParse(comboBoxEmploye.SelectedValue.ToString(), out int idEmp) || idEmp <= 0)
            {
                CustomMessageBox.Show("Veuillez sélectionner un employé.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                comboBoxEmploye.Focus();
                return false;
            }

            if (comboBoxType.SelectedIndex <= 0)
            {
                CustomMessageBox.Show("Veuillez sélectionner le type de charge.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                comboBoxType.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxNomPrenom.Text))
            {
                CustomMessageBox.Show("Veuillez saisir le nom et prénom.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                textBoxNomPrenom.Focus();
                return false;
            }

            // Si c'est un enfant, vérifier la scolarisation
            if (comboBoxType.SelectedItem.ToString() == "Enfant" && comboBoxScolarisation.SelectedIndex <= 0)
            {
                CustomMessageBox.Show("Veuillez sélectionner la scolarisation.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                comboBoxScolarisation.Focus();
                return false;
            }

            // Créer l'objet Charge
            charge = new Charge
            {
                IdPersonnel = idEmp,
                Type = comboBoxType.SelectedItem.ToString() == "Épouse" ? ChargeType.Epouse : ChargeType.Enfant,
                NomPrenom = textBoxNomPrenom.Text.Trim(),
                DateNaissance = datePickerNaissance.Value,
                Identification = textBoxIdentification.Text.Trim(),
                Scolarisation = comboBoxType.SelectedItem.ToString() == "Enfant" && comboBoxScolarisation.SelectedIndex > 0
                    ? comboBoxScolarisation.SelectedItem.ToString()
                    : null
            };

            return true;
        }

        private void ReinitialiserFormulaire()
        {
            // Réinitialiser les champs de la section 2 uniquement
            comboBoxType.SelectedIndex = 0;
            comboBoxScolarisation.SelectedIndex = 0;
            comboBoxScolarisation.Enabled = false;
            textBoxNomPrenom.Clear();
            textBoxIdentification.Clear();
            datePickerNaissance.Value = DateTime.Now;

            // Focus sur le champ Type pour saisie rapide
            comboBoxType.Focus();
        }

        private void buttonValider_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValiderFormulaire(out Charge charge))
                    return;

                // Ajouter la charge
                ChargeRepository.Ajouter(charge);

                CustomMessageBox.Show("Charge ajoutée avec succès.", "Succès",
                    CustomMessageBox.MessageType.Success);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                CustomMessageBox.Show(ex.Message, "Validation",
                    CustomMessageBox.MessageType.Warning);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de l'ajout de la charge :\n{ex.Message}", "Erreur",
                    CustomMessageBox.MessageType.Error);
            }
        }

        private void buttonValiderContinuer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValiderFormulaire(out Charge charge))
                    return;

                // Ajouter la charge
                ChargeRepository.Ajouter(charge);

                CustomMessageBox.Show($"✓ Charge de {charge.NomPrenom} ajoutée!\n\nVous pouvez ajouter une autre charge.", "Succès",
                    CustomMessageBox.MessageType.Success);

                // Réinitialiser le formulaire pour saisir une nouvelle charge
                ReinitialiserFormulaire();
            }
            catch (InvalidOperationException ex)
            {
                CustomMessageBox.Show(ex.Message, "Validation",
                    CustomMessageBox.MessageType.Warning);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de l'ajout de la charge :\n{ex.Message}", "Erreur",
                    CustomMessageBox.MessageType.Error);
            }
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
