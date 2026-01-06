using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class AjouterCategorieForm : Form
    {
        private Dbconnect connect = new Dbconnect();

        public AjouterCategorieForm()
        {
            InitializeComponent();
            ChargerEntreprises();
        }

        private void ChargerEntreprises()
        {
            try
            {
                EntrepriseClass.ChargerEntreprises(comboBoxEntreprise, ajouterPlaceholder: true);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Erreur lors du chargement des entreprises : " + ex.Message,
                                "Erreur", CustomMessageBox.MessageType.Error);
            }
        }

        private void buttonAjouter_Click(object sender, EventArgs e)
        {
            // 1) Validations
            string nomCategorie = textBoxNomCategorie.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nomCategorie))
            {
                CustomMessageBox.Show("Veuillez saisir le nom de la catégorie.", "Information",
                                CustomMessageBox.MessageType.Info);
                textBoxNomCategorie.Focus();
                return;
            }

            if (!decimal.TryParse(textBoxMontant.Text?.Trim(), out decimal montant) || montant < 0)
            {
                CustomMessageBox.Show("Veuillez saisir un montant valide (nombre positif).", "Information",
                                CustomMessageBox.MessageType.Info);
                textBoxMontant.Focus();
                return;
            }

            int? idEntreprise = EntrepriseClass.GetIdEntrepriseSelectionnee(comboBoxEntreprise);
            if (!idEntreprise.HasValue)
            {
                CustomMessageBox.Show("Veuillez sélectionner une entreprise.", "Information",
                                CustomMessageBox.MessageType.Info);
                comboBoxEntreprise.DroppedDown = true;
                return;
            }

            // 2) Exécution
            try
            {
                Cursor = Cursors.WaitCursor;
                buttonAjouter.Enabled = false;

                Categorie.EnregistrerCategorie(nomCategorie, montant, idEntreprise.Value);

                // 3) Fermer la modale avec succès
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Erreur inattendue : " + ex.Message, "Erreur",
                                CustomMessageBox.MessageType.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                buttonAjouter.Enabled = true;
            }
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AjouterCategorieForm_Load(object sender, EventArgs e)
        {
            textBoxNomCategorie.Focus();
        }
    }
}
