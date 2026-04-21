using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using RH_GRH.Auth;
using RH_GRH.Auth.Models;
using MySql.Data.MySqlClient;

namespace RH_GRH
{
    /// <summary>
    /// Formulaire de changement obligatoire de mot de passe lors de la première connexion
    /// </summary>
    public partial class ChangerMotDePasseObligatoireForm : Form
    {
        private string nomUtilisateur;
        private TextBox txtNouveauMotDePasse;
        private TextBox txtConfirmationMotDePasse;
        private Button btnValider;
        private Button btnAnnuler;
        private Label lblTitre;
        private Label lblMessage;
        private Label lblNouveauMotDePasse;
        private Label lblConfirmation;
        private Label lblRegles;
        private CheckBox chkAfficherMotDePasse;
        private Panel panelHeader;
        private Panel panelContent;

        public ChangerMotDePasseObligatoireForm(string nomUtilisateur)
        {
            this.nomUtilisateur = nomUtilisateur;
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Configuration du formulaire
            this.ClientSize = new Size(550, 600);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular);

            // Panel header
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.FromArgb(41, 128, 185)
            };
            panelHeader.Paint += PanelHeader_Paint;
            this.Controls.Add(panelHeader);

            // Titre
            lblTitre = new Label
            {
                Text = "Changement de mot de passe obligatoire",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(500, 40),
                Location = new Point(25, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelHeader.Controls.Add(lblTitre);

            // Message d'explication
            lblMessage = new Label
            {
                Text = "Ceci est votre première connexion. Pour des raisons de sécurité,\nvous devez changer votre mot de passe par défaut.",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(500, 50),
                Location = new Point(25, 70),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelHeader.Controls.Add(lblMessage);

            // Panel contenu
            panelContent = new Panel
            {
                Location = new Point(25, 140),
                Size = new Size(500, 420),
                BackColor = Color.White
            };
            panelContent.Paint += PanelContent_Paint;
            this.Controls.Add(panelContent);

            // Label nouveau mot de passe
            lblNouveauMotDePasse = new Label
            {
                Text = "Nouveau mot de passe",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(450, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };
            panelContent.Controls.Add(lblNouveauMotDePasse);

            // TextBox nouveau mot de passe
            txtNouveauMotDePasse = new TextBox
            {
                Location = new Point(20, 50),
                Size = new Size(450, 30),
                Font = new Font("Segoe UI", 11F),
                UseSystemPasswordChar = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            panelContent.Controls.Add(txtNouveauMotDePasse);

            // Label confirmation
            lblConfirmation = new Label
            {
                Text = "Confirmer le mot de passe",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(20, 95),
                Size = new Size(450, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };
            panelContent.Controls.Add(lblConfirmation);

            // TextBox confirmation
            txtConfirmationMotDePasse = new TextBox
            {
                Location = new Point(20, 125),
                Size = new Size(450, 30),
                Font = new Font("Segoe UI", 11F),
                UseSystemPasswordChar = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            panelContent.Controls.Add(txtConfirmationMotDePasse);

            // CheckBox afficher mot de passe
            chkAfficherMotDePasse = new CheckBox
            {
                Text = "Afficher les mots de passe",
                Location = new Point(20, 165),
                Size = new Size(250, 25),
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(52, 73, 94)
            };
            chkAfficherMotDePasse.CheckedChanged += ChkAfficherMotDePasse_CheckedChanged;
            panelContent.Controls.Add(chkAfficherMotDePasse);

            // Label règles
            lblRegles = new Label
            {
                Text = "Règles de sécurité du mot de passe :\n\n" +
                       "• Au moins 8 caractères\n" +
                       "• Au moins une lettre majuscule (A-Z)\n" +
                       "• Au moins une lettre minuscule (a-z)\n" +
                       "• Au moins un chiffre (0-9)\n" +
                       "• Au moins un caractère spécial (!@#$%...)",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Location = new Point(20, 200),
                Size = new Size(450, 140),
                ForeColor = Color.FromArgb(127, 140, 141),
                BackColor = Color.FromArgb(236, 240, 241)
            };
            lblRegles.Paint += LblRegles_Paint;
            panelContent.Controls.Add(lblRegles);

            // Bouton Valider
            btnValider = new Button
            {
                Text = "Valider et se connecter",
                Location = new Point(20, 360),
                Size = new Size(220, 45),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(39, 174, 96),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnValider.FlatAppearance.BorderSize = 0;
            btnValider.Click += BtnValider_Click;
            btnValider.MouseEnter += (s, e) => btnValider.BackColor = Color.FromArgb(46, 204, 113);
            btnValider.MouseLeave += (s, e) => btnValider.BackColor = Color.FromArgb(39, 174, 96);
            panelContent.Controls.Add(btnValider);

            // Bouton Annuler
            btnAnnuler = new Button
            {
                Text = "Annuler",
                Location = new Point(250, 360),
                Size = new Size(220, 45),
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                ForeColor = Color.FromArgb(52, 73, 94),
                BackColor = Color.FromArgb(189, 195, 199),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAnnuler.FlatAppearance.BorderSize = 0;
            btnAnnuler.Click += BtnAnnuler_Click;
            btnAnnuler.MouseEnter += (s, e) => btnAnnuler.BackColor = Color.FromArgb(149, 165, 166);
            btnAnnuler.MouseLeave += (s, e) => btnAnnuler.BackColor = Color.FromArgb(189, 195, 199);
            panelContent.Controls.Add(btnAnnuler);

            this.ResumeLayout(false);
        }

        private void InitializeCustomComponents()
        {
            // Permettre le déplacement du formulaire
            panelHeader.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            };
        }

        private void PanelHeader_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Gradient de fond
            using (LinearGradientBrush brush = new LinearGradientBrush(
                panelHeader.ClientRectangle,
                Color.FromArgb(41, 128, 185),
                Color.FromArgb(52, 152, 219),
                LinearGradientMode.Horizontal))
            {
                g.FillRectangle(brush, panelHeader.ClientRectangle);
            }
        }

        private void PanelContent_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Bordure arrondie
            using (GraphicsPath path = GetRoundedRectangle(panelContent.ClientRectangle, 8))
            {
                panelContent.Region = new Region(path);
                using (Pen pen = new Pen(Color.FromArgb(220, 220, 220), 1))
                {
                    g.DrawPath(pen, path);
                }
            }
        }

        private void LblRegles_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = GetRoundedRectangle(lblRegles.ClientRectangle, 6))
            {
                lblRegles.Region = new Region(path);
            }
        }

        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void ChkAfficherMotDePasse_CheckedChanged(object sender, EventArgs e)
        {
            txtNouveauMotDePasse.UseSystemPasswordChar = !chkAfficherMotDePasse.Checked;
            txtConfirmationMotDePasse.UseSystemPasswordChar = !chkAfficherMotDePasse.Checked;
        }

        private void BtnValider_Click(object sender, EventArgs e)
        {
            string nouveauMdp = txtNouveauMotDePasse.Text.Trim();
            string confirmation = txtConfirmationMotDePasse.Text.Trim();

            // Vérifier que les champs ne sont pas vides
            if (string.IsNullOrWhiteSpace(nouveauMdp))
            {
                CustomMessageBox.Show("Veuillez saisir un nouveau mot de passe",
                    "Champ requis", CustomMessageBox.MessageType.Warning);
                txtNouveauMotDePasse.Focus();
                return;
            }

            // Vérifier la confirmation
            if (nouveauMdp != confirmation)
            {
                CustomMessageBox.Show("Les mots de passe ne correspondent pas",
                    "Erreur de confirmation", CustomMessageBox.MessageType.Error);
                txtConfirmationMotDePasse.Clear();
                txtConfirmationMotDePasse.Focus();
                return;
            }

            // Vérifier que le nouveau mot de passe n'est pas le mot de passe par défaut
            if (PasswordGenerator.IsDefaultPassword(nouveauMdp))
            {
                CustomMessageBox.Show("Vous ne pouvez pas utiliser le mot de passe par défaut.\nVeuillez choisir un mot de passe personnel.",
                    "Mot de passe invalide", CustomMessageBox.MessageType.Warning);
                txtNouveauMotDePasse.Clear();
                txtConfirmationMotDePasse.Clear();
                txtNouveauMotDePasse.Focus();
                return;
            }

            // Valider la force du mot de passe
            if (!PasswordGenerator.ValidatePasswordStrength(nouveauMdp, out string errorMessage))
            {
                CustomMessageBox.Show(errorMessage, "Mot de passe trop faible", CustomMessageBox.MessageType.Warning);
                txtNouveauMotDePasse.Focus();
                return;
            }

            // Changer le mot de passe dans la base de données
            if (ChangerMotDePasse(nomUtilisateur, nouveauMdp))
            {
                CustomMessageBox.Show($"Mot de passe changé avec succès !\n\nVous pouvez maintenant vous connecter avec votre nouveau mot de passe.",
                    "Succès", CustomMessageBox.MessageType.Success);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                CustomMessageBox.Show("Erreur lors du changement de mot de passe.\nVeuillez réessayer ou contacter l'administrateur.",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
        }

        private void BtnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult result = CustomMessageBox.Show(
                "Êtes-vous sûr de vouloir annuler ?\n\nVous devrez changer votre mot de passe lors de votre prochaine connexion.",
                "Confirmation", CustomMessageBox.MessageType.Question);

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private bool ChangerMotDePasse(string nomUtilisateur, string nouveauMotDePasse)
        {
            Dbconnect db = new Dbconnect();
            try
            {
                db.openConnect();

                // Hasher le nouveau mot de passe
                string passwordHash = PasswordHasher.HashPassword(nouveauMotDePasse);

                string query = @"
                    UPDATE utilisateurs
                    SET mot_de_passe_hash = @motDePasseHash,
                        premier_connexion = FALSE,
                        mot_de_passe_par_defaut = NULL,
                        date_modification = NOW()
                    WHERE nom_utilisateur = @nomUtilisateur";

                using (var cmd = new MySqlCommand(query, db.getconnection))
                {
                    cmd.Parameters.AddWithValue("@motDePasseHash", passwordHash);
                    cmd.Parameters.AddWithValue("@nomUtilisateur", nomUtilisateur);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Logger l'événement
                        AuditLogger.Log(null, nomUtilisateur, "PASSWORD_CHANGE_FIRST_LOGIN", "Sécurité",
                            "Changement de mot de passe obligatoire effectué", Auth.Models.LogResultat.SUCCESS);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                AuditLogger.Log(null, nomUtilisateur, "PASSWORD_CHANGE_ERROR", "Sécurité",
                    $"Erreur lors du changement de mot de passe: {ex.Message}", Auth.Models.LogResultat.FAILURE);
            }
            finally
            {
                db.closeConnect();
            }

            return false;
        }

        // API Windows pour déplacer le formulaire
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
    }
}
