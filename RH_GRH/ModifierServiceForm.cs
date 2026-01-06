using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class ModifierServiceForm : Form
    {
        private Dbconnect connect = new Dbconnect();
        private int idService;
        private string nomServiceActuel;
        private int idEntrepriseActuelle;

        public ModifierServiceForm(int idService, string nomService, int idEntreprise)
        {
            InitializeComponent();
            this.idService = idService;
            this.nomServiceActuel = nomService;
            this.idEntrepriseActuelle = idEntreprise;
            ChargerEntreprises();
            ChargerDonnees();
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

        private void ChargerDonnees()
        {
            // Remplir le formulaire avec les données actuelles
            textBoxNomService.Text = nomServiceActuel;
            comboBoxEntreprise.SelectedValue = idEntrepriseActuelle;
        }

        private void buttonModifier_Click(object sender, EventArgs e)
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

            // 2) Confirmation si changement
            if (nomService == nomServiceActuel && idEntreprise.Value == idEntrepriseActuelle)
            {
                CustomMessageBox.Show("Aucune modification détectée.", "Information",
                                CustomMessageBox.MessageType.Info);
                return;
            }

            // 3) Modification
            try
            {
                Cursor = Cursors.WaitCursor;
                buttonModifier.Enabled = false;

                ServiceClass.ModifierService(idService, nomService, idEntreprise.Value);

                // 4) Fermer la modale avec succès
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
                buttonModifier.Enabled = true;
            }
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ModifierServiceForm_Load(object sender, EventArgs e)
        {
            textBoxNomService.Focus();
            textBoxNomService.SelectAll();
        }
    }
}
