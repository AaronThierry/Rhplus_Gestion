using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class ModifierIndemniteForm : Form
    {
        private int idIndemnite;
        private Dbconnect connect = new Dbconnect();

        public ModifierIndemniteForm(int idIndemnite)
        {
            InitializeComponent();
            this.idIndemnite = idIndemnite;
            InitialiserDonnees();
            ConfigurerRaccourcisClavier();
            ChargerDonneesDepuisId(idIndemnite);
        }

        private void ConfigurerRaccourcisClavier()
        {
            // Escape = Annuler
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    buttonAnnuler_Click(null, null);
                    e.Handled = true;
                }
            };
        }

        private void InitialiserDonnees()
        {
            // Charger les entreprises (mais elles seront désactivées)
            EntrepriseClass.ChargerEntreprises(comboBoxEntreprise, null, true);
        }

        private void ChargerDonneesDepuisId(int idIndemnite)
        {
            try
            {
                const string sql = @"
SELECT
    i.id_indemnite,
    i.id_personnel,
    i.type,
    i.valeur,
    p.nomPrenom      AS NomPersonnel,
    p.matricule,
    p.id_entreprise  AS IdEntreprise,
    e.nomEntreprise  AS NomEntreprise
FROM indemnite i
LEFT JOIN personnel p  ON p.id_personnel  = i.id_personnel
LEFT JOIN entreprise e ON e.id_entreprise = p.id_entreprise
WHERE i.id_indemnite = @id
LIMIT 1;";

                var localConnect = new Dbconnect();
                using (var con = localConnect.getconnection)
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = idIndemnite;

                        using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.Read())
                            {
                                CustomMessageBox.Show("Indemnité introuvable.", "Erreur",
                                    CustomMessageBox.MessageType.Error);
                                this.Close();
                                return;
                            }

                            // Entreprise
                            if (!reader.IsDBNull(reader.GetOrdinal("IdEntreprise")))
                            {
                                int idEnt = Convert.ToInt32(reader["IdEntreprise"]);
                                comboBoxEntreprise.SelectedValue = idEnt;
                            }

                            // Employé
                            string nomEmploye = reader["NomPersonnel"]?.ToString() ?? "";
                            string matricule = reader["matricule"]?.ToString() ?? "";
                            textBoxEmploye.Text = $"{nomEmploye} ({matricule})";

                            // Type
                            string typeLib = reader["type"]?.ToString() ?? "";
                            textBoxType.Text = typeLib;

                            // Valeur
                            if (!reader.IsDBNull(reader.GetOrdinal("valeur")))
                            {
                                decimal valeur = Convert.ToDecimal(reader["valeur"]);
                                textBoxMontant.Text = valeur.ToString("F2", CultureInfo.InvariantCulture);
                            }
                        }
                    }
                }

                // Désactiver les champs non modifiables
                comboBoxEntreprise.Enabled = false;
                textBoxEmploye.Enabled = false;
                textBoxType.Enabled = false;

                // Focus sur le montant (seul champ modifiable)
                textBoxMontant.Focus();
                textBoxMontant.SelectAll();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement de l'indemnité :\n{ex.Message}", "Erreur",
                    CustomMessageBox.MessageType.Error);
                this.Close();
            }
        }

        private bool ValiderFormulaire(out decimal montant)
        {
            montant = 0;

            // Validation du montant
            if (string.IsNullOrWhiteSpace(textBoxMontant.Text))
            {
                CustomMessageBox.Show("Veuillez saisir un montant.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                textBoxMontant.Focus();
                return false;
            }

            string saisieValeur = textBoxMontant.Text.Replace(",", ".");
            if (!decimal.TryParse(saisieValeur, NumberStyles.Any, CultureInfo.InvariantCulture, out montant) || montant <= 0)
            {
                CustomMessageBox.Show("Veuillez saisir un montant valide supérieur à 0.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                textBoxMontant.Focus();
                return false;
            }

            return true;
        }

        private void buttonValider_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValiderFormulaire(out decimal montant))
                    return;

                // Confirmation
                var confirm = CustomMessageBox.Show(
                    $"Voulez-vous vraiment modifier le montant de cette indemnité ?\n\n" +
                    $"Type : {textBoxType.Text}\n" +
                    $"Nouveau montant : {montant:F2}",
                    "Confirmation",
                    CustomMessageBox.MessageType.Question,
                    CustomMessageBox.MessageButtons.YesNo);

                if (confirm != DialogResult.Yes)
                    return;

                // Mise à jour
                var db = new Dbconnect();
                using (var con = db.getconnection)
                {
                    con.Open();

                    const string sql = @"UPDATE indemnite SET valeur = @valeur WHERE id_indemnite = @id;";
                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", idIndemnite);
                        cmd.Parameters.AddWithValue("@valeur", montant);
                        cmd.ExecuteNonQuery();
                    }
                }

                CustomMessageBox.Show("Montant modifié avec succès.", "Succès",
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
    }
}
