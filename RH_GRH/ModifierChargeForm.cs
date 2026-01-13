using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class ModifierChargeForm : Form
    {
        private Dbconnect connect = new Dbconnect();
        private int idCharge;

        public ModifierChargeForm(int idCharge)
        {
            InitializeComponent();
            this.idCharge = idCharge;

            InitialiserDonnees();
            ConfigurerRaccourcisClavier();
            ChargerDonneesDepuisId(idCharge);
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

        private void ChargerDonneesDepuisId(int idCharge)
        {
            try
            {
                // Récupérer la charge depuis la base de données
                var charge = ChargeRepository.GetById(idCharge);
                if (charge == null)
                {
                    CustomMessageBox.Show("Charge introuvable.", "Erreur",
                        CustomMessageBox.MessageType.Error);
                    this.Close();
                    return;
                }

                // Récupérer l'entreprise de l'employé
                int idEntreprise = 0;
                var localConnect = new Dbconnect();
                using (var con = localConnect.getconnection)
                {
                    con.Open();
                    const string query = "SELECT id_entreprise FROM personnel WHERE id_personnel = @idp LIMIT 1";
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@idp", charge.IdPersonnel);
                        var result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            idEntreprise = Convert.ToInt32(result);
                        }
                    }
                }

                // Désactiver temporairement l'événement pour éviter les conflits de connexion
                comboBoxEntreprise.SelectedIndexChanged -= ComboBoxEntreprise_SelectedIndexChanged;

                // Charger l'entreprise d'abord
                if (idEntreprise > 0)
                {
                    comboBoxEntreprise.SelectedValue = idEntreprise;

                    // Réactiver l'événement
                    comboBoxEntreprise.SelectedIndexChanged += ComboBoxEntreprise_SelectedIndexChanged;

                    // Charger les employés manuellement
                    ChargerEmployes(idEntreprise);

                    // Attendre que les employés soient chargés
                    Application.DoEvents();

                    // Charger l'employé
                    if (comboBoxEmploye.DataSource != null)
                    {
                        comboBoxEmploye.SelectedValue = charge.IdPersonnel;
                    }
                }
                else
                {
                    // Réactiver l'événement même si idEntreprise <= 0
                    comboBoxEntreprise.SelectedIndexChanged += ComboBoxEntreprise_SelectedIndexChanged;
                }

                // Charger le type
                string typeTexte = charge.Type == ChargeType.Epouse ? "Épouse" : "Enfant";
                comboBoxType.SelectedItem = typeTexte;

                // Charger les autres champs
                textBoxNomPrenom.Text = charge.NomPrenom ?? "";
                datePickerNaissance.Value = charge.DateNaissance;
                textBoxIdentification.Text = charge.Identification ?? "";

                // Charger la scolarisation si c'est un enfant
                if (charge.Type == ChargeType.Enfant && !string.IsNullOrWhiteSpace(charge.Scolarisation))
                {
                    comboBoxScolarisation.SelectedItem = charge.Scolarisation;
                }
            }
            catch (Exception ex)
            {
                // Réactiver l'événement en cas d'erreur
                comboBoxEntreprise.SelectedIndexChanged += ComboBoxEntreprise_SelectedIndexChanged;

                CustomMessageBox.Show($"Erreur lors du chargement de la charge :\n{ex.Message}", "Erreur",
                    CustomMessageBox.MessageType.Error);
            }
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

        private void buttonValider_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (comboBoxEntreprise.SelectedValue == null ||
                    !int.TryParse(comboBoxEntreprise.SelectedValue.ToString(), out int idEnt) || idEnt <= 0)
                {
                    CustomMessageBox.Show("Veuillez sélectionner une entreprise.", "Validation",
                        CustomMessageBox.MessageType.Warning);
                    comboBoxEntreprise.Focus();
                    return;
                }

                if (comboBoxEmploye.SelectedValue == null ||
                    !int.TryParse(comboBoxEmploye.SelectedValue.ToString(), out int idEmp) || idEmp <= 0)
                {
                    CustomMessageBox.Show("Veuillez sélectionner un employé.", "Validation",
                        CustomMessageBox.MessageType.Warning);
                    comboBoxEmploye.Focus();
                    return;
                }

                if (comboBoxType.SelectedIndex <= 0)
                {
                    CustomMessageBox.Show("Veuillez sélectionner le type de charge.", "Validation",
                        CustomMessageBox.MessageType.Warning);
                    comboBoxType.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxNomPrenom.Text))
                {
                    CustomMessageBox.Show("Veuillez saisir le nom et prénom.", "Validation",
                        CustomMessageBox.MessageType.Warning);
                    textBoxNomPrenom.Focus();
                    return;
                }

                // Si c'est un enfant, vérifier la scolarisation
                if (comboBoxType.SelectedItem.ToString() == "Enfant" && comboBoxScolarisation.SelectedIndex <= 0)
                {
                    CustomMessageBox.Show("Veuillez sélectionner la scolarisation.", "Validation",
                        CustomMessageBox.MessageType.Warning);
                    comboBoxScolarisation.Focus();
                    return;
                }

                // Créer l'objet Charge
                var charge = new Charge
                {
                    IdCharge = idCharge,
                    IdPersonnel = idEmp,
                    Type = comboBoxType.SelectedItem.ToString() == "Épouse" ? ChargeType.Epouse : ChargeType.Enfant,
                    NomPrenom = textBoxNomPrenom.Text.Trim(),
                    DateNaissance = datePickerNaissance.Value,
                    Identification = textBoxIdentification.Text.Trim(),
                    Scolarisation = comboBoxType.SelectedItem.ToString() == "Enfant" && comboBoxScolarisation.SelectedIndex > 0
                        ? comboBoxScolarisation.SelectedItem.ToString()
                        : null
                };

                // Modifier la charge
                ChargeRepository.ModifierCharge(charge);

                CustomMessageBox.Show("Charge modifiée avec succès.", "Succès",
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
                CustomMessageBox.Show($"Erreur lors de la modification de la charge :\n{ex.Message}", "Erreur",
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
