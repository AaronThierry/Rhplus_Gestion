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

            // Form redimensionnable
            this.FormBorderStyle = FormBorderStyle.Sizable;

            // Boutons Agrandir / Réduire
            this.MaximizeBox = true;
            this.MinimizeBox = true;

            // Centrer à l'ouverture
            this.StartPosition = FormStartPosition.CenterScreen;
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
            //..Le code
            hideSubmenu();
        }

        private void button_profil_Click(object sender, EventArgs e)
        {
            SetActiveButton(button_profil);
            //..Le code
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
                activeForm.Close();
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
