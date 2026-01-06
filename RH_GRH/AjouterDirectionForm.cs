using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class AjouterDirectionForm : Form
    {
        private Dbconnect connect = new Dbconnect();

        public AjouterDirectionForm()
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
            string nomDirection = textBoxNomDirection.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nomDirection))
            {
                CustomMessageBox.Show("Veuillez saisir le nom de la direction.", "Information",
                                CustomMessageBox.MessageType.Info);
                textBoxNomDirection.Focus();
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

                DirectionClass.EnregistrerDirection(nomDirection, idEntreprise.Value);

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

        private void AjouterDirectionForm_Load(object sender, EventArgs e)
        {
            // Configuration au chargement si nécessaire
            textBoxNomDirection.Focus();
        }
    }
}
