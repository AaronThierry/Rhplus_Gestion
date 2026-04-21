using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class Formmain : Form
    {
        EntrepriseClass Entreprise = new EntrepriseClass();

        // Variables pour les animations
        private Timer animationTimer;
        private Panel animatingPanel;
        private int targetHeight;
        private bool isExpanding;
        private const int ANIMATION_STEP = 20;
        private const int ANIMATION_INTERVAL = 10;

        // Bouton actuellement sélectionné
        private Button currentActiveButton = null;

        public Formmain()
        {
            InitializeComponent();
            customDesign();
            count();
            InitializeAnimations();
            ApplyModernStyling();
            SetVersionLabel();
            ConfigureUserPermissions();
            UpdateUserLabel();
            SetupUserLabelDesign();

            // Espacements uniformes dans la sidebar
            button2.Margin = new Padding(0, 5, 0, 0);              // Tableau de bord
            button_personnel.Margin = new Padding(0, 10, 0, 0);     // Personnel
            button_salaire.Margin = new Padding(0, 10, 0, 0);       // Salaire
            button_administration.Margin = new Padding(0, 10, 0, 0); // Administration
            button_exit.Margin = new Padding(0, 10, 0, 0);          // Sortir

            // Form redimensionnable
            this.FormBorderStyle = FormBorderStyle.Sizable;

            // Boutons Agrandir / Réduire
            this.MaximizeBox = true;
            this.MinimizeBox = true;

            // Centrer à l'ouverture
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Configure la visibilité des éléments UI en fonction des permissions de l'utilisateur connecté
        /// </summary>
        private void ConfigureUserPermissions()
        {
            var sessionManager = Auth.SessionManager.Instance;

            // Si pas d'utilisateur connecté, tout masquer sauf sortir
            if (!sessionManager.IsAuthenticated)
            {
                HideAllMenuItems();
                return;
            }

            var currentUser = sessionManager.CurrentUser;
            bool isAdmin = currentUser.IsAdmin();

            // Section Administration - Réservée aux administrateurs
            if (!isAdmin)
            {
                button_administration.Visible = false;
                panel_administration_submenu.Visible = false;
            }
            else
            {
                // Sous-menu Administration
                ConfigureAdminSubMenu(currentUser);
            }

            // Section Personnel - Vérifier si l'utilisateur a au moins une permission Personnel
            bool hasPersonnelAccess = isAdmin ||
                HasPermission(currentUser, "EMPLOYE_VIEW") ||
                HasPermission(currentUser, "CHARGE_VIEW") ||
                HasPermission(currentUser, "INDEMNITE_VIEW") ||
                HasPermission(currentUser, "SURSALAIRE_VIEW") ||
                HasPermission(currentUser, "ABONNEMENT_VIEW");

            if (!hasPersonnelAccess)
            {
                button_personnel.Visible = false;
                panel_personnel_submenu.Visible = false;
            }
            else
            {
                ConfigurePersonnelSubMenu(currentUser);
            }

            // Section Salaire - Vérifier si l'utilisateur a au moins une permission Salaire
            bool hasSalaireAccess = isAdmin ||
                HasPermission(currentUser, "SALAIRE_HORAIRE_VIEW") ||
                HasPermission(currentUser, "SALAIRE_JOURNALIER_VIEW") ||
                HasPermission(currentUser, "BULLETIN_VIEW");

            if (!hasSalaireAccess)
            {
                button_salaire.Visible = false;
                panel_salaire_submenu.Visible = false;
            }
            else
            {
                ConfigureSalaireSubMenu(currentUser);
            }
        }

        /// <summary>
        /// Met à jour le label avec le nom de l'utilisateur connecté
        /// </summary>
        private void UpdateUserLabel()
        {
            var sessionManager = Auth.SessionManager.Instance;

            if (sessionManager.IsAuthenticated && sessionManager.CurrentUser != null)
            {
                var user = sessionManager.CurrentUser;
                // Afficher le nom complet s'il existe, sinon le nom d'utilisateur
                string displayName = "";
                if (!string.IsNullOrWhiteSpace(user.NomComplet))
                {
                    displayName = user.NomComplet;
                }
                else
                {
                    displayName = user.NomUtilisateur;
                }

                // Format élégant avec icône
                labelUtilisateur.Text = $"👤  {displayName}";
            }
            else
            {
                labelUtilisateur.Text = "👤  Invité";
            }
        }

        /// <summary>
        /// Configure le design moderne du label utilisateur avec effet visuel
        /// </summary>
        private void SetupUserLabelDesign()
        {
            // Activer le double buffering pour éviter le scintillement
            labelUtilisateur.Paint += LabelUtilisateur_Paint;

            // Positionner le label correctement en bas avec un offset
            PositionUserLabel();

            // Repositionner quand la fenêtre est redimensionnée
            this.Resize += (s, e) => PositionUserLabel();
        }

        /// <summary>
        /// Positionne le label utilisateur dans la zone désirée (avant le bas)
        /// </summary>
        private void PositionUserLabel()
        {
            // Positionner le label à 60px du bas du panel_slide
            int yPosition = panel_slide.Height - labelUtilisateur.Height - 60;
            labelUtilisateur.Location = new Point(0, yPosition);
        }

        /// <summary>
        /// Dessine un effet visuel moderne pour le label utilisateur
        /// </summary>
        private void LabelUtilisateur_Paint(object sender, PaintEventArgs e)
        {
            Label label = sender as Label;
            if (label == null) return;

            // Anti-aliasing pour un rendu lisse
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Bordure supérieure orange (accent RH+) - centré car le texte est centré
            int borderWidth = 60;
            int borderX = (label.Width - borderWidth) / 2;
            using (SolidBrush accentBrush = new SolidBrush(Color.FromArgb(255, 128, 0)))
            {
                e.Graphics.FillRectangle(accentBrush, borderX, 0, borderWidth, 3);
            }

            // Effet de brillance subtile en haut (glass morphism léger)
            using (System.Drawing.Drawing2D.LinearGradientBrush shine =
                new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Point(0, 3),
                    new Point(0, label.Height / 3),
                    Color.FromArgb(25, 255, 255, 255),
                    Color.Transparent))
            {
                e.Graphics.FillRectangle(shine, 0, 3, label.Width, label.Height / 3);
            }

            // Bordure supérieure subtile pour séparer du bouton du dessus
            using (Pen topBorder = new Pen(Color.FromArgb(40, 255, 255, 255), 1))
            {
                e.Graphics.DrawLine(topBorder, 12, 0, label.Width - 12, 0);
            }
        }

        /// <summary>
        /// Configure le sous-menu Administration
        /// </summary>
        private void ConfigureAdminSubMenu(Auth.Models.User user)
        {
            bool isAdmin = user.IsAdmin();
            bool isSuperAdmin = user.IsSuperAdmin();

            // Gestion des entreprises - Admin ou permission VIEW/CREATE/EDIT
            button_entreprise.Visible = isAdmin ||
                HasPermission(user, "ENTREPRISE_VIEW") ||
                HasPermission(user, "ENTREPRISE_CREATE") ||
                HasPermission(user, "ENTREPRISE_EDIT");

            // Gestion des directions - Admin ou permission VIEW/CREATE/EDIT
            button_direction.Visible = isAdmin ||
                HasPermission(user, "DIRECTION_VIEW") ||
                HasPermission(user, "DIRECTION_CREATE") ||
                HasPermission(user, "DIRECTION_EDIT");

            // Gestion des services - Admin ou permission VIEW/CREATE/EDIT
            button_service.Visible = isAdmin ||
                HasPermission(user, "SERVICE_VIEW") ||
                HasPermission(user, "SERVICE_CREATE") ||
                HasPermission(user, "SERVICE_EDIT");

            // Gestion des catégories - Admin ou permission VIEW/CREATE/EDIT
            button_categorie.Visible = isAdmin ||
                HasPermission(user, "CATEGORIE_VIEW") ||
                HasPermission(user, "CATEGORIE_CREATE") ||
                HasPermission(user, "CATEGORIE_EDIT");

            // Gestion des utilisateurs - UNIQUEMENT Super Administrateur ou permission système
            button_utilisateur.Visible = isSuperAdmin || HasPermission(user, "UTILISATEUR_VIEW");

            // Gestion abonnement - Admin ou permission
            button_abonnement.Visible = isAdmin || HasPermission(user, "ABONNEMENT_VIEW");

            // Logs - UNIQUEMENT Super Administrateur ou permission
            button_profil.Visible = isSuperAdmin || HasPermission(user, "LOG_VIEW");

            // Rôles & Permissions - UNIQUEMENT Super Administrateur ou permission
            button_roles.Visible = isSuperAdmin || HasPermission(user, "ROLE_VIEW");
        }

        /// <summary>
        /// Configure le sous-menu Personnel
        /// </summary>
        private void ConfigurePersonnelSubMenu(Auth.Models.User user)
        {
            bool isAdmin = user.IsAdmin();

            // Gestion des employés - Admin ou permission VIEW/CREATE/EDIT
            button_employe.Visible = isAdmin ||
                HasPermission(user, "EMPLOYE_VIEW") ||
                HasPermission(user, "EMPLOYE_CREATE") ||
                HasPermission(user, "EMPLOYE_EDIT");

            // Charges familiales - Admin ou permission VIEW/CREATE/EDIT
            button_charge.Visible = isAdmin ||
                HasPermission(user, "CHARGE_VIEW") ||
                HasPermission(user, "CHARGE_CREATE") ||
                HasPermission(user, "CHARGE_EDIT");

            // Indemnités - Admin ou permission VIEW/CREATE/EDIT
            button_indemnite.Visible = isAdmin ||
                HasPermission(user, "INDEMNITE_VIEW") ||
                HasPermission(user, "INDEMNITE_CREATE") ||
                HasPermission(user, "INDEMNITE_EDIT");
        }

        /// <summary>
        /// Configure le sous-menu Salaire
        /// </summary>
        private void ConfigureSalaireSubMenu(Auth.Models.User user)
        {
            bool isAdmin = user.IsAdmin();

            // Sursalaires - Admin ou permission VIEW/CREATE/EDIT
            button_sursalaire.Visible = isAdmin ||
                HasPermission(user, "SURSALAIRE_VIEW") ||
                HasPermission(user, "SURSALAIRE_CREATE") ||
                HasPermission(user, "SURSALAIRE_EDIT");

            // Horaires - Admin ou permission VIEW/CREATE/EDIT
            button_horaire.Visible = isAdmin ||
                HasPermission(user, "SALAIRE_HORAIRE_VIEW") ||
                HasPermission(user, "SALAIRE_HORAIRE_CREATE") ||
                HasPermission(user, "SALAIRE_HORAIRE_EDIT");

            // Journalier - Admin ou permission VIEW/CREATE/EDIT
            button_journalier.Visible = isAdmin ||
                HasPermission(user, "SALAIRE_JOURNALIER_VIEW") ||
                HasPermission(user, "SALAIRE_JOURNALIER_CREATE") ||
                HasPermission(user, "SALAIRE_JOURNALIER_EDIT");
        }

        /// <summary>
        /// Vérifie si l'utilisateur a une permission spécifique
        /// </summary>
        private bool HasPermission(Auth.Models.User user, string permissionName)
        {
            if (user == null || user.Roles == null) return false;

            foreach (var role in user.Roles)
            {
                if (role.HasPermission(permissionName))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Masque tous les éléments de menu (mode sécurisé par défaut)
        /// </summary>
        private void HideAllMenuItems()
        {
            button_personnel.Visible = false;
            button_salaire.Visible = false;
            button_administration.Visible = false;
            panel_personnel_submenu.Visible = false;
            panel_salaire_submenu.Visible = false;
            panel_administration_submenu.Visible = false;
        }

        private void InitializeAnimations()
        {
            animationTimer = new Timer();
            animationTimer.Interval = ANIMATION_INTERVAL;
            animationTimer.Tick += AnimationTimer_Tick;
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (animatingPanel == null) return;

            if (isExpanding)
            {
                if (animatingPanel.Height < targetHeight)
                {
                    animatingPanel.Height = Math.Min(animatingPanel.Height + ANIMATION_STEP, targetHeight);
                }
                else
                {
                    animationTimer.Stop();
                    animatingPanel = null;
                }
            }
            else
            {
                if (animatingPanel.Height > 0)
                {
                    animatingPanel.Height = Math.Max(animatingPanel.Height - ANIMATION_STEP, 0);
                }
                else
                {
                    animatingPanel.Visible = false;
                    animationTimer.Stop();
                    animatingPanel = null;
                }
            }
        }

        private void ApplyModernStyling()
        {
            // Style du sidebar principal
            panel_slide.BackColor = Color.MidnightBlue;

            // Appliquer un style moderne aux boutons principaux
            ApplyButtonHoverEffect(button_personnel);
            ApplyButtonHoverEffect(button_salaire);
            ApplyButtonHoverEffect(button_administration);
            ApplyButtonHoverEffect(button_exit);

            // Appliquer aux sous-menus avec ordre inversé pour Dock.Top
            Button[] personnelButtons = panel_personnel_submenu.Controls.OfType<Button>().Reverse().ToArray();
            foreach (Button btn in personnelButtons)
            {
                ApplySubmenuButtonHoverEffect(btn);
            }

            Button[] salaireButtons = panel_salaire_submenu.Controls.OfType<Button>().Reverse().ToArray();
            foreach (Button btn in salaireButtons)
            {
                ApplySubmenuButtonHoverEffect(btn);
            }

            Button[] adminButtons = panel_administration_submenu.Controls.OfType<Button>().Reverse().ToArray();
            foreach (Button btn in adminButtons)
            {
                ApplySubmenuButtonHoverEffect(btn);
            }
        }

        private void ApplyButtonHoverEffect(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 60, 140);
            btn.Cursor = Cursors.Hand;
            btn.Padding = new Padding(15, 0, 10, 0);
            btn.TextAlign = ContentAlignment.MiddleLeft;

            // Ajouter un indicateur visuel sur le côté
            btn.Paint += (s, e) => {
                if (btn == currentActiveButton)
                {
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 120, 215)))
                    {
                        e.Graphics.FillRectangle(brush, 0, 0, 4, btn.Height);
                    }
                }
            };

            btn.MouseEnter += (s, e) => {
                if (btn != currentActiveButton)
                {
                    btn.BackColor = Color.FromArgb(25, 60, 140);
                }
            };

            btn.MouseLeave += (s, e) => {
                if (btn != currentActiveButton)
                {
                    btn.BackColor = Color.Transparent;
                }
            };
        }

        private void SetActiveButton(Button btn)
        {
            // Réinitialiser l'ancien bouton actif
            if (currentActiveButton != null)
            {
                currentActiveButton.BackColor = Color.Transparent;
                currentActiveButton.Font = new Font(currentActiveButton.Font.FontFamily, currentActiveButton.Font.Size, FontStyle.Regular);
                currentActiveButton.Invalidate();
            }

            // Définir le nouveau bouton actif
            currentActiveButton = btn;
            if (btn != null)
            {
                btn.BackColor = Color.FromArgb(15, 37, 80);
                btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size, FontStyle.Bold);
                btn.Invalidate();
            }
        }

        private void ApplySubmenuButtonHoverEffect(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 120, 215);
            btn.Cursor = Cursors.Hand;
            btn.Padding = new Padding(35, 0, 10, 0);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Height = 40;
            btn.Dock = DockStyle.Top;

            btn.MouseEnter += (s, e) => {
                btn.BackColor = Color.FromArgb(0, 120, 215);
            };

            btn.MouseLeave += (s, e) => {
                btn.BackColor = Color.Transparent;
            };
        }




        public void count()
        {
            //Display count entreprie employe
            // label_entreprise.Text = "Total des entreprises : " + Entreprise.totalEntreprie();
            label_nbreentreprise.Text = Entreprise.totalEntreprie();
            label2.Text = EmployeClass.TotalEmployes();
        }

        private void SetVersionLabel()
        {
            // Lire la version depuis AssemblyInfo
         





            
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            labelVersion.Text = $"Version {version.Major}.{version.Minor}.{version.Build}";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_salaire_submenu);
        }

        private void Formmain_Load(object sender, EventArgs e)
        {
        }
        private void customDesign()
        {
            // Calculer automatiquement la hauteur en fonction du nombre de boutons (60px par bouton)
            int hauteurBouton = 40;

            panel_personnel_submenu.Height = 0;
            panel_personnel_submenu.Visible = false;
            panel_personnel_submenu.Tag = panel_personnel_submenu.Controls.Count * hauteurBouton;

            panel_salaire_submenu.Height = 0;
            panel_salaire_submenu.Visible = false;
            panel_salaire_submenu.Tag = panel_salaire_submenu.Controls.Count * hauteurBouton;

            panel_administration_submenu.Height = 0;
            panel_administration_submenu.Visible = false;
            panel_administration_submenu.Tag = panel_administration_submenu.Controls.Count * hauteurBouton;
        }

        private void hideSubmenu()
        {
            if(panel_personnel_submenu.Visible == true)
                AnimateSubmenu(panel_personnel_submenu, false);
            if (panel_salaire_submenu.Visible == true)
                AnimateSubmenu(panel_salaire_submenu, false);
            if(panel_administration_submenu.Visible == true)
                AnimateSubmenu(panel_administration_submenu, false);
        }

        private void AnimateSubmenu(Panel submenu, bool expand)
        {
            if (animationTimer.Enabled)
            {
                animationTimer.Stop();
            }

            animatingPanel = submenu;
            isExpanding = expand;

            if (expand)
            {
                submenu.Visible = true;
                submenu.Height = 0;
                targetHeight = (int)submenu.Tag;
            }
            else
            {
                targetHeight = 0;
            }

            animationTimer.Start();
        }

        private void showSubmenu(Panel submenu)
        {
            if (submenu.Height == 0 || !submenu.Visible)
            {
                // Fermer les autres sous-menus d'abord
                if (panel_personnel_submenu != submenu && panel_personnel_submenu.Visible)
                    AnimateSubmenu(panel_personnel_submenu, false);
                if (panel_salaire_submenu != submenu && panel_salaire_submenu.Visible)
                    AnimateSubmenu(panel_salaire_submenu, false);
                if (panel_administration_submenu != submenu && panel_administration_submenu.Visible)
                    AnimateSubmenu(panel_administration_submenu, false);

                // Attendre un court instant avant d'ouvrir le nouveau
                Task.Delay(100).ContinueWith(t => {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() => AnimateSubmenu(submenu, true)));
                    }
                });
            }
            else
            {
                AnimateSubmenu(submenu, false);
            }
        }
        private void button_personnel_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_personnel_submenu);
        }

        private void button_administration_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_administration_submenu);
        }

        private void button_employe_Click(object sender, EventArgs e)
        {
            SetActiveButton(button_employe);
            OpenChildForm(new GestionEmployeForm());
            hideSubmenu();
        }

        private void button_charge_Click(object sender, EventArgs e)
        {
            SetActiveButton(button_charge);
            OpenChildForm(new GestionChargeForm());
            hideSubmenu();
        }

        private void button_sursalaire_Click(object sender, EventArgs e)
        {
            SetActiveButton(button_sursalaire);
            OpenChildForm(new GestionSursalaireForm());
            hideSubmenu();
        }

        private void button_abonnement_Click(object sender, EventArgs e)
        {
            SetActiveButton(button_abonnement);
            OpenChildForm(new GestionAbonnementForm());
            hideSubmenu();
        }

        private void button_indemnite_Click(object sender, EventArgs e)
        {
            SetActiveButton(button_indemnite);
            OpenChildForm(new GestionIndemniteForm());
            hideSubmenu();
        }

        private void button_horaire_Click(object sender, EventArgs e)
        {
            SetActiveButton(button_horaire);
            OpenChildForm(new GestionSalaireHoraireForm());
            hideSubmenu();
        }

        private void button_journalier_Click(object sender, EventArgs e)
        {
            SetActiveButton(button_journalier);
            OpenChildForm(new GestionSalaireJournalierForm());
            hideSubmenu();
        }

        private void button_entreprise_Click(object sender, EventArgs e)
        {
            SetActiveButton(button_entreprise);
            OpenChildForm(new GestionEntrepriseForm());
            hideSubmenu();
        }

        private void button_direction_Click(object sender, EventArgs e)
        {
            SetActiveButton(button_direction);
            OpenChildForm(new GestionDirectionForm());
            hideSubmenu();
        }

        private void button_service_Click(object sender, EventArgs e)
        {
            SetActiveButton(button_service);
            OpenChildForm(new GestionServiceForm());
            hideSubmenu();
        }

        private void button_categorie_Click(object sender, EventArgs e)
        {
            SetActiveButton(button_categorie);
            OpenChildForm(new GestionCategorieForm());
            hideSubmenu();
        }

        private void button_utilisateur_Click(object sender, EventArgs e)
        {
            SetActiveButton(button_utilisateur);
            OpenChildForm(new GestionUtilisateursForm());
            hideSubmenu();
        }

        private void button_profil_Click(object sender, EventArgs e)
        {
            SetActiveButton(button_profil);
            OpenChildForm(new VisualisationLogsForm());
            hideSubmenu();
        }

        // Gestion des rôles et permissions
        private void button_roles_Click(object sender, EventArgs e)
        {
            SetActiveButton(button_roles);
            OpenChildForm(new GestionRolesPermissionsForm());
            hideSubmenu();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        //show Entreprise
        private Form activeForm = null;
        private void OpenChildForm(Form childForm)
        {
            if(activeForm != null)
            {
                activeForm.Close();
                panel2.Controls.Remove(activeForm);
                activeForm.Dispose();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            // Demander confirmation avant de se déconnecter
            DialogResult result = CustomMessageBox.Show(
                "Êtes-vous sûr de vouloir vous déconnecter ?",
                "Confirmation",
                CustomMessageBox.MessageType.Question,
                CustomMessageBox.MessageButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // Déconnexion de l'utilisateur
                Auth.SessionManager.Instance.TerminateSession();

                // Fermer le formulaire principal
                // Le Program.cs détectera la déconnexion et rouvrira automatiquement le login
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                activeForm.Close();
                panel2.Controls.Add(panel_main);
            }
            // Réinitialiser le bouton actif
            SetActiveButton(null);
            count();
        }
    }
}
