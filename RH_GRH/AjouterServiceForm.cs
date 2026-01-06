using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class AjouterServiceForm : Form
    {
        private Dbconnect connect = new Dbconnect();

        public AjouterServiceForm()
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
            string nomService = textBoxNomService.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nomService))
            {
                CustomMessageBox.Show("Veuillez saisir le nom du service.", "Information",
                                CustomMessageBox.MessageType.Info);
                textBoxNomService.Focus();
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

            // 2) Enregistrement
            try
            {
                Cursor = Cursors.WaitCursor;
                buttonAjouter.Enabled = false;

                ServiceClass.EnregistrerService(nomService, idEntreprise.Value);

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

        private void AjouterServiceForm_Load(object sender, EventArgs e)
        {
            // Configuration au chargement si nécessaire
            textBoxNomService.Focus();
        }
    }
}
