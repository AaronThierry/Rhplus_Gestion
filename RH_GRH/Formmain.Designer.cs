using System.Windows.Forms;

namespace RH_GRH
{
    partial class Formmain
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        /// 

        // 🔽 Ici tu peux ajouter ce code :

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Formmain));
            this.panel_slide = new System.Windows.Forms.Panel();
            this.button_exit = new System.Windows.Forms.Button();
            this.panel_administration_submenu = new System.Windows.Forms.Panel();
            this.button_profil = new System.Windows.Forms.Button();
            this.button_utilisateur = new System.Windows.Forms.Button();
            this.button_categorie = new System.Windows.Forms.Button();
            this.button_service = new System.Windows.Forms.Button();
            this.button_direction = new System.Windows.Forms.Button();
            this.button_entreprise = new System.Windows.Forms.Button();
            this.button_administration = new System.Windows.Forms.Button();
            this.panel_salaire_submenu = new System.Windows.Forms.Panel();
            this.button_journalier = new System.Windows.Forms.Button();
            this.button_horaire = new System.Windows.Forms.Button();
            this.button_salaire = new System.Windows.Forms.Button();
            this.panel_personnel_submenu = new System.Windows.Forms.Panel();
            this.button_indemnite = new System.Windows.Forms.Button();
            this.button_charge = new System.Windows.Forms.Button();
            this.button_employe = new System.Windows.Forms.Button();
            this.button_personnel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_main = new System.Windows.Forms.Panel();
            this.panel_cover = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label_entreprise = new System.Windows.Forms.Label();
            this.label_nbreentreprise = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel_slide.SuspendLayout();
            this.panel_administration_submenu.SuspendLayout();
            this.panel_salaire_submenu.SuspendLayout();
            this.panel_personnel_submenu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel_main.SuspendLayout();
            this.panel_cover.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_slide
            // 
            resources.ApplyResources(this.panel_slide, "panel_slide");
            this.panel_slide.BackColor = System.Drawing.Color.MidnightBlue;
            this.panel_slide.Controls.Add(this.button_exit);
            this.panel_slide.Controls.Add(this.panel_administration_submenu);
            this.panel_slide.Controls.Add(this.button_administration);
            this.panel_slide.Controls.Add(this.panel_salaire_submenu);
            this.panel_slide.Controls.Add(this.button_salaire);
            this.panel_slide.Controls.Add(this.panel_personnel_submenu);
            this.panel_slide.Controls.Add(this.button_personnel);
            this.panel_slide.Controls.Add(this.panel1);
            this.panel_slide.Name = "panel_slide";
            // 
            // button_exit
            // 
            resources.ApplyResources(this.button_exit, "button_exit");
            this.button_exit.FlatAppearance.BorderSize = 0;
            this.button_exit.ForeColor = System.Drawing.Color.White;
            this.button_exit.Name = "button_exit";
            this.button_exit.UseVisualStyleBackColor = true;
            this.button_exit.Click += new System.EventHandler(this.button_exit_Click);
            // 
            // panel_administration_submenu
            // 
            this.panel_administration_submenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(37)))), ((int)(((byte)(80)))));
            this.panel_administration_submenu.Controls.Add(this.button_profil);
            this.panel_administration_submenu.Controls.Add(this.button_utilisateur);
            this.panel_administration_submenu.Controls.Add(this.button_categorie);
            this.panel_administration_submenu.Controls.Add(this.button_service);
            this.panel_administration_submenu.Controls.Add(this.button_direction);
            this.panel_administration_submenu.Controls.Add(this.button_entreprise);
            resources.ApplyResources(this.panel_administration_submenu, "panel_administration_submenu");
            this.panel_administration_submenu.Name = "panel_administration_submenu";
            // 
            // button_profil
            // 
            resources.ApplyResources(this.button_profil, "button_profil");
            this.button_profil.FlatAppearance.BorderSize = 0;
            this.button_profil.ForeColor = System.Drawing.Color.White;
            this.button_profil.Name = "button_profil";
            this.button_profil.UseVisualStyleBackColor = true;
            this.button_profil.Click += new System.EventHandler(this.button_profil_Click);
            // 
            // button_utilisateur
            // 
            resources.ApplyResources(this.button_utilisateur, "button_utilisateur");
            this.button_utilisateur.FlatAppearance.BorderSize = 0;
            this.button_utilisateur.ForeColor = System.Drawing.Color.White;
            this.button_utilisateur.Name = "button_utilisateur";
            this.button_utilisateur.UseVisualStyleBackColor = true;
            this.button_utilisateur.Click += new System.EventHandler(this.button_utilisateur_Click);
            // 
            // button_categorie
            // 
            resources.ApplyResources(this.button_categorie, "button_categorie");
            this.button_categorie.FlatAppearance.BorderSize = 0;
            this.button_categorie.ForeColor = System.Drawing.Color.White;
            this.button_categorie.Name = "button_categorie";
            this.button_categorie.UseVisualStyleBackColor = true;
            this.button_categorie.Click += new System.EventHandler(this.button_categorie_Click);
            // 
            // button_service
            // 
            resources.ApplyResources(this.button_service, "button_service");
            this.button_service.FlatAppearance.BorderSize = 0;
            this.button_service.ForeColor = System.Drawing.Color.White;
            this.button_service.Name = "button_service";
            this.button_service.UseVisualStyleBackColor = true;
            this.button_service.Click += new System.EventHandler(this.button_service_Click);
            // 
            // button_direction
            // 
            resources.ApplyResources(this.button_direction, "button_direction");
            this.button_direction.FlatAppearance.BorderSize = 0;
            this.button_direction.ForeColor = System.Drawing.Color.White;
            this.button_direction.Name = "button_direction";
            this.button_direction.UseVisualStyleBackColor = true;
            this.button_direction.Click += new System.EventHandler(this.button_direction_Click);
            // 
            // button_entreprise
            // 
            resources.ApplyResources(this.button_entreprise, "button_entreprise");
            this.button_entreprise.FlatAppearance.BorderSize = 0;
            this.button_entreprise.ForeColor = System.Drawing.Color.White;
            this.button_entreprise.Name = "button_entreprise";
            this.button_entreprise.UseVisualStyleBackColor = true;
            this.button_entreprise.Click += new System.EventHandler(this.button_entreprise_Click);
            // 
            // button_administration
            // 
            resources.ApplyResources(this.button_administration, "button_administration");
            this.button_administration.FlatAppearance.BorderSize = 0;
            this.button_administration.ForeColor = System.Drawing.Color.White;
            this.button_administration.Name = "button_administration";
            this.button_administration.UseVisualStyleBackColor = true;
            this.button_administration.Click += new System.EventHandler(this.button_administration_Click);
            // 
            // panel_salaire_submenu
            // 
            this.panel_salaire_submenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(37)))), ((int)(((byte)(80)))));
            this.panel_salaire_submenu.Controls.Add(this.button_journalier);
            this.panel_salaire_submenu.Controls.Add(this.button_horaire);
            resources.ApplyResources(this.panel_salaire_submenu, "panel_salaire_submenu");
            this.panel_salaire_submenu.Name = "panel_salaire_submenu";
            // 
            // button_journalier
            // 
            resources.ApplyResources(this.button_journalier, "button_journalier");
            this.button_journalier.FlatAppearance.BorderSize = 0;
            this.button_journalier.ForeColor = System.Drawing.Color.White;
            this.button_journalier.Name = "button_journalier";
            this.button_journalier.UseVisualStyleBackColor = true;
            this.button_journalier.Click += new System.EventHandler(this.button_journalier_Click);
            // 
            // button_horaire
            // 
            resources.ApplyResources(this.button_horaire, "button_horaire");
            this.button_horaire.FlatAppearance.BorderSize = 0;
            this.button_horaire.ForeColor = System.Drawing.Color.White;
            this.button_horaire.Name = "button_horaire";
            this.button_horaire.UseVisualStyleBackColor = true;
            this.button_horaire.Click += new System.EventHandler(this.button_horaire_Click);
            // 
            // button_salaire
            // 
            resources.ApplyResources(this.button_salaire, "button_salaire");
            this.button_salaire.FlatAppearance.BorderSize = 0;
            this.button_salaire.ForeColor = System.Drawing.Color.White;
            this.button_salaire.Name = "button_salaire";
            this.button_salaire.UseVisualStyleBackColor = true;
            this.button_salaire.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel_personnel_submenu
            // 
            this.panel_personnel_submenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(37)))), ((int)(((byte)(80)))));
            this.panel_personnel_submenu.Controls.Add(this.button_indemnite);
            this.panel_personnel_submenu.Controls.Add(this.button_charge);
            this.panel_personnel_submenu.Controls.Add(this.button_employe);
            resources.ApplyResources(this.panel_personnel_submenu, "panel_personnel_submenu");
            this.panel_personnel_submenu.Name = "panel_personnel_submenu";
            // 
            // button_indemnite
            // 
            resources.ApplyResources(this.button_indemnite, "button_indemnite");
            this.button_indemnite.FlatAppearance.BorderSize = 0;
            this.button_indemnite.ForeColor = System.Drawing.Color.White;
            this.button_indemnite.Name = "button_indemnite";
            this.button_indemnite.UseVisualStyleBackColor = true;
            this.button_indemnite.Click += new System.EventHandler(this.button_indemnite_Click);
            // 
            // button_charge
            // 
            resources.ApplyResources(this.button_charge, "button_charge");
            this.button_charge.FlatAppearance.BorderSize = 0;
            this.button_charge.ForeColor = System.Drawing.Color.White;
            this.button_charge.Name = "button_charge";
            this.button_charge.UseVisualStyleBackColor = true;
            this.button_charge.Click += new System.EventHandler(this.button_charge_Click);
            // 
            // button_employe
            // 
            resources.ApplyResources(this.button_employe, "button_employe");
            this.button_employe.FlatAppearance.BorderSize = 0;
            this.button_employe.ForeColor = System.Drawing.Color.White;
            this.button_employe.Name = "button_employe";
            this.button_employe.UseVisualStyleBackColor = true;
            this.button_employe.Click += new System.EventHandler(this.button_employe_Click);
            // 
            // button_personnel
            // 
            this.button_personnel.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.button_personnel, "button_personnel");
            this.button_personnel.FlatAppearance.BorderSize = 0;
            this.button_personnel.ForeColor = System.Drawing.Color.White;
            this.button_personnel.Name = "button_personnel";
            this.button_personnel.UseVisualStyleBackColor = true;
            this.button_personnel.Click += new System.EventHandler(this.button_personnel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.button2, "button2");
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::RH_GRH.Properties.Resources.logo_RH___1_;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.panel_main);
            this.panel2.Name = "panel2";
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.panel_cover);
            resources.ApplyResources(this.panel_main, "panel_main");
            this.panel_main.Name = "panel_main";
            // 
            // panel_cover
            // 
            this.panel_cover.Controls.Add(this.panel3);
            this.panel_cover.Controls.Add(this.panel4);
            this.panel_cover.Controls.Add(this.pictureBox2);
            this.panel_cover.Controls.Add(this.panel5);
            resources.ApplyResources(this.panel_cover, "panel_cover");
            this.panel_cover.Name = "panel_cover";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.MidnightBlue;
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Controls.Add(this.panel6);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // panel7
            // 
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Controls.Add(this.label1);
            this.panel7.Controls.Add(this.label2);
            this.panel7.Name = "panel7";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Name = "label2";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label_entreprise);
            this.panel6.Controls.Add(this.label_nbreentreprise);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // label_entreprise
            // 
            resources.ApplyResources(this.label_entreprise, "label_entreprise");
            this.label_entreprise.ForeColor = System.Drawing.Color.White;
            this.label_entreprise.Name = "label_entreprise";
            // 
            // label_nbreentreprise
            // 
            resources.ApplyResources(this.label_nbreentreprise, "label_nbreentreprise");
            this.label_nbreentreprise.ForeColor = System.Drawing.Color.Red;
            this.label_nbreentreprise.Name = "label_nbreentreprise";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.MidnightBlue;
            this.panel4.Controls.Add(this.pictureBox5);
            this.panel4.Controls.Add(this.pictureBox6);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label8);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // pictureBox5
            // 
            resources.ApplyResources(this.pictureBox5, "pictureBox5");
            this.pictureBox5.Image = global::RH_GRH.Properties.Resources.team_8163740;
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::RH_GRH.Properties.Resources._84dead02_a175_49bb_aa12_145eb624d1ad_remove_bg_io;
            resources.ApplyResources(this.pictureBox6, "pictureBox6");
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.TabStop = false;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Name = "label8";
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Image = global::RH_GRH.Properties.Resources.gr;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.label6);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label6.Name = "label6";
            // 
            // Formmain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel_slide);
            this.Name = "Formmain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Formmain_Load);
            this.panel_slide.ResumeLayout(false);
            this.panel_administration_submenu.ResumeLayout(false);
            this.panel_salaire_submenu.ResumeLayout(false);
            this.panel_personnel_submenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel_main.ResumeLayout(false);
            this.panel_cover.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_slide;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_personnel_submenu;
        private System.Windows.Forms.Panel panel_salaire_submenu;
        private System.Windows.Forms.Panel panel_administration_submenu;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Button button_exit;
        private Button button_profil;
        private Button button_utilisateur;
        private Button button_categorie;
        private Button button_service;
        private Button button_direction;
        private Button button_entreprise;
        private Button button_administration;
        private Button button_journalier;
        private Button button_horaire;
        private Button button_salaire;
        private Button button_indemnite;
        private Button button_charge;
        private Button button_employe;
        private Button button_personnel;
        private Button button2;
        private Panel panel2;
        private Panel panel_main;
        private Panel panel_cover;
        private Panel panel3;
        private Panel panel6;
        private Label label_entreprise;
        private Label label_nbreentreprise;
        private Panel panel4;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;
        private Label label7;
        private Label label8;
        private PictureBox pictureBox2;
        private Panel panel5;
        private Label label6;
        private Panel panel7;
        private Label label1;
        private Label label2;
    }
}

