using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class ModifierDirectionForm : Form
    {
        private Dbconnect connect = new Dbconnect();
        private int idDirection;
        private string nomDirectionActuel;
        private int idEntrepriseActuelle;

        public ModifierDirectionForm(int idDirection, string nomDirection, int idEntreprise)
        {
            InitializeComponent();
            this.idDirection = idDirection;
            this.nomDirectionActuel = nomDirection;
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
            textBoxNomDirection.Text = nomDirectionActuel;
            comboBoxEntreprise.SelectedValue = idEntrepriseActuelle;
        }

        private void buttonModifier_Click(object sender, EventArgs e)
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

            // 2) Confirmation si changement
            if (nomDirection == nomDirectionActuel && idEntreprise.Value == idEntrepriseActuelle)
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

                DirectionClass.ModifierDirection(idDirection, nomDirection, idEntreprise.Value);

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

        private void ModifierDirectionForm_Load(object sender, EventArgs e)
        {
            textBoxNomDirection.Focus();
            textBoxNomDirection.SelectAll();
        }
    }
}
