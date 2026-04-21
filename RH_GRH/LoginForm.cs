using System;
using System.Drawing;
using System.Windows.Forms;
using RH_GRH.Auth;
using RH_GRH.Auth.Models;

namespace RH_GRH
{
    public partial class LoginForm : Form
    {
        private AuthenticationService authService;

        public LoginForm()
        {
            InitializeComponent();
            authService = new AuthenticationService();
            ConfigureForm();
        }

        private void ConfigureForm()
        {
            // Centrer le formulaire
            this.StartPosition = FormStartPosition.CenterScreen;

            // Configuration du mot de passe
            textBoxMotDePasse.UseSystemPasswordChar = true;

            // Enter key pour connexion
            textBoxNomUtilisateur.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true;
                    textBoxMotDePasse.Focus();
                }
            };

            textBoxMotDePasse.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true;
                    buttonConnexion_Click(s, e);
                }
            };

            // Focus initial
            this.Shown += (s, e) => textBoxNomUtilisateur.Focus();
        }

        private void buttonConnexion_Click(object sender, EventArgs e)
        {
            string username = textBoxNomUtilisateur.Text.Trim();
            string password = textBoxMotDePasse.Text;

            // Validation des champs
            if (string.IsNullOrWhiteSpace(username))
            {
                CustomMessageBox.Show("Veuillez saisir votre nom d'utilisateur",
                    "Champ requis", CustomMessageBox.MessageType.Warning);
                textBoxNomUtilisateur.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                CustomMessageBox.Show("Veuillez saisir votre mot de passe",
                    "Champ requis", CustomMessageBox.MessageType.Warning);
                textBoxMotDePasse.Focus();
                return;
            }

            // Désactiver le bouton pendant l'authentification
            buttonConnexion.Enabled = false;
            buttonConnexion.Text = "Connexion en cours...";
            this.Cursor = Cursors.WaitCursor;

            try
            {
                var (success, user, errorMessage) = authService.Authenticate(username, password);

                if (success)
                {
                    // Afficher un message de bienvenue
                    CustomMessageBox.Show($"Bienvenue {user.NomComplet} !",
                        "Connexion réussie", CustomMessageBox.MessageType.Success);

                    // Marquer comme succès et fermer
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Afficher l'erreur
                    CustomMessageBox.Show(errorMessage,
                        "Échec de connexion", CustomMessageBox.MessageType.Error);

                    // Effacer le mot de passe
                    textBoxMotDePasse.Clear();
                    textBoxMotDePasse.Focus();
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Une erreur s'est produite lors de la connexion:\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);

                textBoxMotDePasse.Clear();
                textBoxMotDePasse.Focus();
            }
            finally
            {
                // Réactiver le bouton
                buttonConnexion.Enabled = true;
                buttonConnexion.Text = "SE CONNECTER";
                this.Cursor = Cursors.Default;
            }
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void checkBoxAfficherMdp_CheckedChanged(object sender, EventArgs e)
        {
            textBoxMotDePasse.UseSystemPasswordChar = !checkBoxAfficherMdp.Checked;
        }

        private void LinkLabelMotDePasseOublie_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CustomMessageBox.Show("Pour réinitialiser votre mot de passe, veuillez contacter l'administrateur système.",
                "Mot de passe oublié", CustomMessageBox.MessageType.Info);
        }
    }
}
