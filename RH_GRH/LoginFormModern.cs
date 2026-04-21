using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using RH_GRH.Auth;
using RH_GRH.Auth.Models;

namespace RH_GRH
{
    public partial class LoginFormModern : Form
    {
        private AuthenticationService authService;
        private Font montserratLight;
        private Font montserratRegular;
        private Font montserratSemiBold;
        private Font montserratBold;

        public LoginFormModern()
        {
            InitializeComponent();
            authService = new AuthenticationService();
            LoadMontserratFonts();
            ConfigureForm();
            InitializeAnimations();
        }

        private void LoadMontserratFonts()
        {
            // Fallback to Segoe UI if Montserrat is not installed
            try
            {
                montserratLight = new Font("Montserrat Light", 10F, FontStyle.Regular);
                montserratRegular = new Font("Montserrat", 10F, FontStyle.Regular);
                montserratSemiBold = new Font("Montserrat SemiBold", 11F, FontStyle.Bold);
                montserratBold = new Font("Montserrat", 14F, FontStyle.Bold);
            }
            catch
            {
                // Fallback to Segoe UI
                montserratLight = new Font("Segoe UI Light", 10F, FontStyle.Regular);
                montserratRegular = new Font("Segoe UI", 10F, FontStyle.Regular);
                montserratSemiBold = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
                montserratBold = new Font("Segoe UI", 14F, FontStyle.Bold);
            }
        }

        private void ConfigureForm()
        {
            // Configuration de base
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(248, 250, 252);

            // Apply Montserrat fonts to all controls
            ApplyMontserratFonts();

            // Configurer les placeholders
            SetupPlaceholder(textBoxNomUtilisateur, "Nom d'utilisateur");
            SetupPlaceholder(textBoxMotDePasse, "Mot de passe");

            // Effet hover sur le bouton de connexion (orange plus foncé)
            buttonConnexion.MouseEnter += (s, e) =>
            {
                buttonConnexion.BackColor = Color.FromArgb(230, 110, 0);
            };
            buttonConnexion.MouseLeave += (s, e) =>
            {
                buttonConnexion.BackColor = Color.FromArgb(255, 128, 0);
            };

            // Arrondir les coins du formulaire
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, 20, 20, 180, 90);
            path.AddArc(this.Width - 20, 0, 20, 20, 270, 90);
            path.AddArc(this.Width - 20, this.Height - 20, 20, 20, 0, 90);
            path.AddArc(0, this.Height - 20, 20, 20, 90, 90);
            path.CloseAllFigures();
            this.Region = new Region(path);

            // Ombre portée
            this.Paint += (s, e) =>
            {
                using (Pen shadowPen = new Pen(Color.FromArgb(30, 0, 0, 0), 10))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawPath(shadowPen, path);
                }
            };

            // Configuration des champs
            textBoxMotDePasse.UseSystemPasswordChar = true;

            // Événements Enter key
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

            // Focus initial après l'affichage
            this.Shown += (s, e) =>
            {
                textBoxNomUtilisateur.Focus();
                FadeIn();
            };

            // Permettre le déplacement de la fenêtre
            panelHeader.MouseDown += Panel_MouseDown;
            panelHeader.MouseMove += Panel_MouseMove;
            panelHeader.MouseUp += Panel_MouseUp;

            // Ajouter une ombre sous le header
            panelHeader.Paint += (s, e) =>
            {
                using (Pen shadowPen = new Pen(Color.FromArgb(20, 0, 0, 0), 1))
                {
                    e.Graphics.DrawLine(shadowPen, 0, panelHeader.Height - 1, panelHeader.Width, panelHeader.Height - 1);
                }
            };

            // Appliquer des coins arrondis au panelGlass
            GraphicsPath glassPath = new GraphicsPath();
            int radius = 12;
            Rectangle glassRect = new Rectangle(0, 0, panelGlass.Width, panelGlass.Height);
            glassPath.AddArc(glassRect.X, glassRect.Y, radius, radius, 180, 90);
            glassPath.AddArc(glassRect.X + glassRect.Width - radius, glassRect.Y, radius, radius, 270, 90);
            glassPath.AddArc(glassRect.X + glassRect.Width - radius, glassRect.Y + glassRect.Height - radius, radius, radius, 0, 90);
            glassPath.AddArc(glassRect.X, glassRect.Y + glassRect.Height - radius, radius, radius, 90, 90);
            glassPath.CloseAllFigures();
            panelGlass.Region = new Region(glassPath);

            // Créer une ombre douce multi-couches (Material Design elevation)
            panelBackground.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Créer plusieurs couches d'ombres pour un effet de profondeur
                Rectangle shadowRect1 = new Rectangle(panelGlass.Left - 1, panelGlass.Top + 2, panelGlass.Width + 2, panelGlass.Height);
                Rectangle shadowRect2 = new Rectangle(panelGlass.Left - 2, panelGlass.Top + 4, panelGlass.Width + 4, panelGlass.Height);
                Rectangle shadowRect3 = new Rectangle(panelGlass.Left - 3, panelGlass.Top + 8, panelGlass.Width + 6, panelGlass.Height);

                using (GraphicsPath shadowPath1 = CreateRoundedRectPath(shadowRect1, 12))
                using (GraphicsPath shadowPath2 = CreateRoundedRectPath(shadowRect2, 12))
                using (GraphicsPath shadowPath3 = CreateRoundedRectPath(shadowRect3, 12))
                {
                    using (PathGradientBrush brush1 = new PathGradientBrush(shadowPath1))
                    {
                        brush1.CenterColor = Color.FromArgb(8, 0, 0, 0);
                        brush1.SurroundColors = new[] { Color.Transparent };
                        e.Graphics.FillPath(brush1, shadowPath1);
                    }

                    using (PathGradientBrush brush2 = new PathGradientBrush(shadowPath2))
                    {
                        brush2.CenterColor = Color.FromArgb(5, 0, 0, 0);
                        brush2.SurroundColors = new[] { Color.Transparent };
                        e.Graphics.FillPath(brush2, shadowPath2);
                    }

                    using (PathGradientBrush brush3 = new PathGradientBrush(shadowPath3))
                    {
                        brush3.CenterColor = Color.FromArgb(3, 0, 0, 0);
                        brush3.SurroundColors = new[] { Color.Transparent };
                        e.Graphics.FillPath(brush3, shadowPath3);
                    }
                }
            };
        }

        private GraphicsPath CreateRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            return path;
        }

        private void ApplyMontserratFonts()
        {
            // Apply to text boxes
            if (textBoxNomUtilisateur != null)
                textBoxNomUtilisateur.Font = montserratRegular;
            if (textBoxMotDePasse != null)
                textBoxMotDePasse.Font = montserratRegular;

            // Apply to buttons
            if (buttonConnexion != null)
                buttonConnexion.Font = montserratSemiBold;

            // Apply to labels
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Label)
                {
                    if (ctrl.Name.Contains("Title") || ctrl.Name.Contains("titre"))
                        ctrl.Font = montserratBold;
                    else
                        ctrl.Font = montserratRegular;
                }
                else if (ctrl is CheckBox)
                {
                    ctrl.Font = montserratLight;
                }
                else if (ctrl is LinkLabel)
                {
                    ctrl.Font = montserratLight;
                }
            }
        }

        private void InitializeAnimations()
        {
            // Effet de fondu d'entrée seulement (plus moderne et professionnel)
            this.Opacity = 0;
        }

        private void FadeIn()
        {
            Timer fadeTimer = new Timer();
            fadeTimer.Interval = 20;
            double targetOpacity = 1.0;
            double step = 0.05;

            fadeTimer.Tick += (s, e) =>
            {
                if (this.Opacity < targetOpacity)
                {
                    this.Opacity += step;
                }
                else
                {
                    this.Opacity = targetOpacity;
                    fadeTimer.Stop();
                }
            };
            fadeTimer.Start();
        }


        // Variables pour le déplacement de la fenêtre
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void buttonConnexion_Click(object sender, EventArgs e)
        {
            string username = textBoxNomUtilisateur.Text.Trim();
            string password = textBoxMotDePasse.Text;

            // Vérifier les placeholders
            if (username == "Nom d'utilisateur" || string.IsNullOrWhiteSpace(username))
            {
                ShowModernMessage("Veuillez saisir votre nom d'utilisateur", MessageType.Warning);
                textBoxNomUtilisateur.Focus();
                return;
            }

            if (password == "Mot de passe" || string.IsNullOrWhiteSpace(password))
            {
                ShowModernMessage("Veuillez saisir votre mot de passe", MessageType.Warning);
                textBoxMotDePasse.Focus();
                return;
            }

            // Animation du bouton
            AnimateButton(buttonConnexion, () =>
            {
                try
                {
                    var (success, user, errorMessage) = authService.Authenticate(username, password);

                    if (success)
                    {
                        // Vérifier si c'est la première connexion
                        if (EstPremiereConnexion(username))
                        {
                            ShowModernMessage("Première connexion détectée. Changement de mot de passe requis.", MessageType.Info);

                            // Ouvrir le formulaire de changement de mot de passe obligatoire
                            Timer delayTimer = new Timer();
                            delayTimer.Interval = 800;
                            delayTimer.Tick += (s2, e2) =>
                            {
                                delayTimer.Stop();

                                using (ChangerMotDePasseObligatoireForm formChangeMdp = new ChangerMotDePasseObligatoireForm(username))
                                {
                                    DialogResult result = formChangeMdp.ShowDialog();

                                    if (result == DialogResult.OK)
                                    {
                                        // Mot de passe changé avec succès, se déconnecter et redemander de se connecter
                                        Auth.SessionManager.Instance.TerminateSession();
                                        ShowModernMessage("Mot de passe changé ! Veuillez vous reconnecter avec votre nouveau mot de passe.", MessageType.Success);
                                        textBoxMotDePasse.Clear();
                                        textBoxMotDePasse.Focus();
                                    }
                                    else
                                    {
                                        // Utilisateur a annulé, déconnecter
                                        Auth.SessionManager.Instance.TerminateSession();
                                        ShowModernMessage("Changement de mot de passe annulé. Veuillez réessayer.", MessageType.Warning);
                                        textBoxMotDePasse.Clear();
                                        textBoxMotDePasse.Focus();
                                    }
                                }
                            };
                            delayTimer.Start();
                        }
                        else
                        {
                            // Connexion normale
                            ShowModernMessage($"Bienvenue {user.NomComplet} !", MessageType.Success);

                            // Attendre 500ms puis faire un fade out rapide
                            Timer delayTimer = new Timer();
                            delayTimer.Interval = 500;
                            delayTimer.Tick += (s2, e2) =>
                            {
                                delayTimer.Stop();

                                // Fade out rapide avant de fermer
                                Timer fadeOut = new Timer();
                                fadeOut.Interval = 15;
                                fadeOut.Tick += (s3, e3) =>
                                {
                                    if (this.Opacity > 0)
                                    {
                                        this.Opacity -= 0.15;
                                    }
                                    else
                                    {
                                        fadeOut.Stop();
                                        this.DialogResult = DialogResult.OK;
                                        this.Close();
                                    }
                                };
                                fadeOut.Start();
                            };
                            delayTimer.Start();
                        }
                    }
                    else
                    {
                        ShowModernMessage(errorMessage, MessageType.Error);
                        textBoxMotDePasse.Clear();
                        textBoxMotDePasse.Focus();
                        ShakeForm();
                    }
                }
                catch (Exception ex)
                {
                    ShowModernMessage($"Erreur: {ex.Message}", MessageType.Error);
                    textBoxMotDePasse.Clear();
                    textBoxMotDePasse.Focus();
                }
            });
        }

        private void AnimateButton(Control button, Action callback)
        {
            int originalY = button.Top;
            Timer timer = new Timer();
            timer.Interval = 10;
            int step = 0;

            timer.Tick += (s, e) =>
            {
                step++;
                if (step < 5)
                {
                    button.Top = originalY + 2;
                }
                else if (step < 10)
                {
                    button.Top = originalY;
                }
                else
                {
                    timer.Stop();
                    callback?.Invoke();
                }
            };
            timer.Start();
        }

        private void ShakeForm()
        {
            Point original = this.Location;
            Timer timer = new Timer();
            timer.Interval = 30;
            int step = 0;

            timer.Tick += (s, e) =>
            {
                step++;
                if (step % 2 == 0)
                    this.Location = new Point(original.X + 10, original.Y);
                else
                    this.Location = new Point(original.X - 10, original.Y);

                if (step > 6)
                {
                    this.Location = original;
                    timer.Stop();
                }
            };
            timer.Start();
        }

        private void ShowModernMessage(string message, MessageType type)
        {
            Color bgColor = Color.FromArgb(240, 240, 240);
            Color textColor = Color.FromArgb(50, 50, 50);

            switch (type)
            {
                case MessageType.Success:
                    // Vert avec une touche de bleu du logo
                    bgColor = Color.FromArgb(56, 139, 196);
                    textColor = Color.White;
                    break;
                case MessageType.Error:
                    bgColor = Color.FromArgb(239, 68, 68);
                    textColor = Color.White;
                    break;
                case MessageType.Warning:
                    // Orange du logo
                    bgColor = Color.FromArgb(255, 128, 0);
                    textColor = Color.White;
                    break;
            }

            Panel messagePanel = new Panel
            {
                Size = new Size(this.Width - 60, 60),
                Location = new Point(30, this.Height - 90),
                BackColor = bgColor
            };

            GraphicsPath msgPath = new GraphicsPath();
            msgPath.AddArc(0, 0, 10, 10, 180, 90);
            msgPath.AddArc(messagePanel.Width - 10, 0, 10, 10, 270, 90);
            msgPath.AddArc(messagePanel.Width - 10, messagePanel.Height - 10, 10, 10, 0, 90);
            msgPath.AddArc(0, messagePanel.Height - 10, 10, 10, 90, 90);
            msgPath.CloseAllFigures();
            messagePanel.Region = new Region(msgPath);

            Label messageLabel = new Label
            {
                Text = message,
                ForeColor = textColor,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(messagePanel.Width - 20, messagePanel.Height),
                Location = new Point(10, 0),
                TextAlign = ContentAlignment.MiddleCenter
            };

            messagePanel.Controls.Add(messageLabel);
            this.Controls.Add(messagePanel);
            messagePanel.BringToFront();

            // Auto-dismiss après 2 secondes pour les erreurs, 500ms pour le succès
            Timer dismissTimer = new Timer();
            dismissTimer.Interval = type == MessageType.Success ? 500 : 2000;
            dismissTimer.Tick += (s, e) =>
            {
                this.Controls.Remove(messagePanel);
                messagePanel.Dispose();
                dismissTimer.Stop();
            };
            dismissTimer.Start();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void checkBoxAfficherMdp_CheckedChanged(object sender, EventArgs e)
        {
            textBoxMotDePasse.UseSystemPasswordChar = !checkBoxAfficherMdp.Checked;
        }

        private void linkLabelMotDePasseOublie_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Récupérer les informations de l'administrateur système
            string adminInfo = GetAdminContactInfo();

            string message = string.IsNullOrEmpty(adminInfo)
                ? "Contactez l'administrateur système pour réinitialiser votre mot de passe."
                : $"Pour réinitialiser votre mot de passe, contactez :\n\n{adminInfo}";

            MessageBox.Show(message,
                "Mot de passe oublié",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// Récupère les informations de contact de l'administrateur système
        /// </summary>
        private string GetAdminContactInfo()
        {
            Dbconnect db = new Dbconnect();
            try
            {
                db.openConnect();

                string query = @"
                    SELECT u.nom_complet, u.email, u.telephone
                    FROM utilisateurs u
                    INNER JOIN utilisateur_roles ur ON u.id = ur.utilisateur_id
                    INNER JOIN roles r ON ur.role_id = r.id
                    WHERE r.nom_role = 'Administrateur'
                      AND u.actif = 1
                    ORDER BY u.id ASC
                    LIMIT 1";

                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, db.getconnection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nom = reader.GetString("nom_complet");
                            string email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString("email");
                            string telephone = reader.IsDBNull(reader.GetOrdinal("telephone")) ? "" : reader.GetString("telephone");

                            string info = nom;
                            if (!string.IsNullOrEmpty(email)) info += $"\nEmail: {email}";
                            if (!string.IsNullOrEmpty(telephone)) info += $"\nTél: {telephone}";

                            return info;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur récupération admin: {ex.Message}");
            }
            finally
            {
                db.closeConnect();
            }

            return "";
        }

        private enum MessageType
        {
            Success,
            Error,
            Warning,
            Info
        }

        private void SetupPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.FromArgb(148, 163, 184);

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.FromArgb(30, 41, 59);
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.FromArgb(148, 163, 184);
                }
            };
        }

        private void pictureBoxLogo_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb.Image == null) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            // Ne pas dessiner de fond - le parent (panelGlass) est blanc
            // Le PictureBox est transparent, donc le fond blanc du panelGlass sera visible

            // Créer un chemin circulaire pour le logo
            using (GraphicsPath circlePath = new GraphicsPath())
            {
                circlePath.AddEllipse(0, 0, pb.Width, pb.Height);

                // Remplir le cercle avec la couleur du fond du panel (blanc)
                using (SolidBrush bgBrush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillPath(bgBrush, circlePath);
                }
            }

            // Dessiner l'image avec un clipping circulaire
            using (GraphicsPath imageClip = new GraphicsPath())
            {
                imageClip.AddEllipse(8, 8, pb.Width - 16, pb.Height - 16);
                Region oldClip = e.Graphics.Clip;
                e.Graphics.SetClip(imageClip);
                e.Graphics.DrawImage(pb.Image, 8, 8, pb.Width - 16, pb.Height - 16);
                e.Graphics.Clip = oldClip;
            }

            // Contour subtil bleu RH+
            using (Pen borderPen = new Pen(Color.FromArgb(100, 56, 139, 196), 3))
            {
                e.Graphics.DrawEllipse(borderPen, 1, 1, pb.Width - 3, pb.Height - 3);
            }
        }

        /// <summary>
        /// Vérifie si c'est la première connexion de l'utilisateur
        /// </summary>
        private bool EstPremiereConnexion(string nomUtilisateur)
        {
            Dbconnect db = new Dbconnect();
            try
            {
                db.openConnect();

                string query = @"
                    SELECT premier_connexion
                    FROM utilisateurs
                    WHERE nom_utilisateur = @nomUtilisateur";

                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, db.getconnection))
                {
                    cmd.Parameters.AddWithValue("@nomUtilisateur", nomUtilisateur);

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToBoolean(result);
                    }
                }
            }
            catch (Exception ex)
            {
                // En cas d'erreur, on considère que ce n'est pas la première connexion
                // pour ne pas bloquer l'utilisateur
                System.Diagnostics.Debug.WriteLine($"Erreur vérification première connexion: {ex.Message}");
            }
            finally
            {
                db.closeConnect();
            }

            return false;
        }

        private void labelVersion_Click(object sender, EventArgs e)
        {

        }
    }
}
