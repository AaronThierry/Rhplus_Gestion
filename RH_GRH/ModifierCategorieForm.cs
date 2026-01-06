using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class ModifierCategorieForm : Form
    {
        private Dbconnect connect = new Dbconnect();
        private int idCategorie;
        private string nomCategorieActuel;
        private decimal montantActuel;
        private int idEntrepriseActuelle;

        public ModifierCategorieForm(int idCategorie, string nomCategorie, decimal montant, int idEntreprise)
        {
            InitializeComponent();
            this.idCategorie = idCategorie;
            this.nomCategorieActuel = nomCategorie;
            this.montantActuel = montant;
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
            textBoxNomCategorie.Text = nomCategorieActuel;
            textBoxMontant.Text = montantActuel.ToString("F2");
            comboBoxEntreprise.SelectedValue = idEntrepriseActuelle;
        }

        private void buttonModifier_Click(object sender, EventArgs e)
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

            // 2) Confirmation si changement
            if (nomCategorie == nomCategorieActuel &&
                montant == montantActuel &&
                idEntreprise.Value == idEntrepriseActuelle)
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

                Categorie.ModifierCategorie(idCategorie, nomCategorie, montant, idEntreprise.Value);

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

        private void ModifierCategorieForm_Load(object sender, EventArgs e)
        {
            textBoxNomCategorie.Focus();
            textBoxNomCategorie.SelectAll();
        }
    }
}
