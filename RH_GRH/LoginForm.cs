using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using RH_GRH.Auth;
using RH_GRH.Auth.Models;

namespace RH_GRH
{
    public partial class LoginForm : Form
    {
        private AuthenticationService authService;
        private Timer fadeOutTimer;
        private double currentOpacity = 1.0;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        public LoginForm()
        {
            InitializeComponent();
            authService = new AuthenticationService();
            ConfigureForm();
            InitializeFadeTimer();
        }

        private void InitializeFadeTimer()
        {
            fadeOutTimer = new Timer();
            fadeOutTimer.Interval = 20; // 20ms entre chaque frame pour un effet fluide
            fadeOutTimer.Tick += FadeOutTimer_Tick;
        }

        private void FadeOutTimer_Tick(object sender, EventArgs e)
        {
            currentOpacity -= 0.05; // Réduire l'opacité progressivement

            if (currentOpacity <= 0)
            {
                fadeOutTimer.Stop();
                this.Opacity = 0;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.Opacity = currentOpacity;
            }
        }

        private void ConfigureForm()
        {
            // Centrer le formulaire
            this.StartPosition = FormStartPosition.CenterScreen;

            // Coins arrondis
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 900, 600, 20, 20));

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
                    // Désactiver les contrôles pour empêcher les interactions pendant la transition
                    buttonConnexion.Enabled = false;
                    buttonAnnuler.Enabled = false;
                    textBoxNomUtilisateur.Enabled = false;
                    textBoxMotDePasse.Enabled = false;

                    // Changer le texte du bouton pour indiquer le succès
                    buttonConnexion.Text = "✓ CONNEXION RÉUSSIE";
                    this.Cursor = Cursors.Default;

                    // Attendre un court instant pour que l'utilisateur voie le message de succès
                    Task.Delay(800).ContinueWith(_ =>
                    {
                        // Lancer l'animation de fondu sur le thread UI
                        this.Invoke(new Action(() =>
                        {
                            fadeOutTimer.Start();
                        }));
                    });

                    return; // Important : ne pas exécuter le finally qui réactiverait le bouton
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

        private void labelAppDesc_Click(object sender, EventArgs e)
        {

        }

        private void panelLeft_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
