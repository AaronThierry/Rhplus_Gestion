namespace RH_GRH
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.linkLabelMotDePasseOublie = new System.Windows.Forms.LinkLabel();
            this.checkBoxAfficherMdp = new Guna.UI2.WinForms.Guna2CheckBox();
            this.buttonAnnuler = new Guna.UI2.WinForms.Guna2Button();
            this.buttonConnexion = new Guna.UI2.WinForms.Guna2Button();
            this.textBoxMotDePasse = new Guna.UI2.WinForms.Guna2TextBox();
            this.textBoxNomUtilisateur = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelMotDePasse = new System.Windows.Forms.Label();
            this.labelNomUtilisateur = new System.Windows.Forms.Label();
            this.labelSubTitle = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.panelMain.SuspendLayout();
            this.panelLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            //
            // panelMain
            //
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMain.Controls.Add(this.panelLogin);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(500, 650);
            this.panelMain.TabIndex = 0;
            //
            // panelLogin
            //
            this.panelLogin.BackColor = System.Drawing.Color.White;
            this.panelLogin.Controls.Add(this.linkLabelMotDePasseOublie);
            this.panelLogin.Controls.Add(this.checkBoxAfficherMdp);
            this.panelLogin.Controls.Add(this.buttonAnnuler);
            this.panelLogin.Controls.Add(this.buttonConnexion);
            this.panelLogin.Controls.Add(this.textBoxMotDePasse);
            this.panelLogin.Controls.Add(this.textBoxNomUtilisateur);
            this.panelLogin.Controls.Add(this.labelMotDePasse);
            this.panelLogin.Controls.Add(this.labelNomUtilisateur);
            this.panelLogin.Controls.Add(this.labelSubTitle);
            this.panelLogin.Controls.Add(this.labelTitle);
            this.panelLogin.Controls.Add(this.pictureBoxLogo);
            this.panelLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLogin.Location = new System.Drawing.Point(20, 20);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Padding = new System.Windows.Forms.Padding(40);
            this.panelLogin.Size = new System.Drawing.Size(460, 610);
            this.panelLogin.TabIndex = 0;
            //
            // linkLabelMotDePasseOublie
            //
            this.linkLabelMotDePasseOublie.AutoSize = true;
            this.linkLabelMotDePasseOublie.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelMotDePasseOublie.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.linkLabelMotDePasseOublie.Location = new System.Drawing.Point(291, 407);
            this.linkLabelMotDePasseOublie.Name = "linkLabelMotDePasseOublie";
            this.linkLabelMotDePasseOublie.Size = new System.Drawing.Size(121, 15);
            this.linkLabelMotDePasseOublie.TabIndex = 5;
            this.linkLabelMotDePasseOublie.TabStop = true;
            this.linkLabelMotDePasseOublie.Text = "Mot de passe oublié?";
            this.linkLabelMotDePasseOublie.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelMotDePasseOublie_LinkClicked);
            //
            // checkBoxAfficherMdp
            //
            this.checkBoxAfficherMdp.AutoSize = true;
            this.checkBoxAfficherMdp.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.checkBoxAfficherMdp.CheckedState.BorderRadius = 2;
            this.checkBoxAfficherMdp.CheckedState.BorderThickness = 0;
            this.checkBoxAfficherMdp.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.checkBoxAfficherMdp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBoxAfficherMdp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.checkBoxAfficherMdp.Location = new System.Drawing.Point(48, 405);
            this.checkBoxAfficherMdp.Name = "checkBoxAfficherMdp";
            this.checkBoxAfficherMdp.Size = new System.Drawing.Size(149, 19);
            this.checkBoxAfficherMdp.TabIndex = 4;
            this.checkBoxAfficherMdp.Text = "Afficher le mot de passe";
            this.checkBoxAfficherMdp.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.checkBoxAfficherMdp.UncheckedState.BorderRadius = 2;
            this.checkBoxAfficherMdp.UncheckedState.BorderThickness = 2;
            this.checkBoxAfficherMdp.UncheckedState.FillColor = System.Drawing.Color.White;
            this.checkBoxAfficherMdp.UseVisualStyleBackColor = true;
            this.checkBoxAfficherMdp.CheckedChanged += new System.EventHandler(this.checkBoxAfficherMdp_CheckedChanged);
            //
            // buttonAnnuler
            //
            this.buttonAnnuler.BorderRadius = 8;
            this.buttonAnnuler.BorderThickness = 1;
            this.buttonAnnuler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAnnuler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAnnuler.FillColor = System.Drawing.Color.White;
            this.buttonAnnuler.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.buttonAnnuler.Location = new System.Drawing.Point(243, 460);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(169, 50);
            this.buttonAnnuler.TabIndex = 7;
            this.buttonAnnuler.Text = "ANNULER";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            //
            // buttonConnexion
            //
            this.buttonConnexion.BorderRadius = 8;
            this.buttonConnexion.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonConnexion.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonConnexion.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonConnexion.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonConnexion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.buttonConnexion.ForeColor = System.Drawing.Color.White;
            this.buttonConnexion.Location = new System.Drawing.Point(48, 460);
            this.buttonConnexion.Name = "buttonConnexion";
            this.buttonConnexion.Size = new System.Drawing.Size(169, 50);
            this.buttonConnexion.TabIndex = 6;
            this.buttonConnexion.Text = "SE CONNECTER";
            this.buttonConnexion.Click += new System.EventHandler(this.buttonConnexion_Click);
            //
            // textBoxMotDePasse
            //
            this.textBoxMotDePasse.BorderRadius = 8;
            this.textBoxMotDePasse.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxMotDePasse.DefaultText = "";
            this.textBoxMotDePasse.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxMotDePasse.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxMotDePasse.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxMotDePasse.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxMotDePasse.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxMotDePasse.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.textBoxMotDePasse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxMotDePasse.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxMotDePasse.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.textBoxMotDePasse.Location = new System.Drawing.Point(48, 350);
            this.textBoxMotDePasse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxMotDePasse.Name = "textBoxMotDePasse";
            this.textBoxMotDePasse.PasswordChar = '\0';
            this.textBoxMotDePasse.PlaceholderText = "Saisissez votre mot de passe";
            this.textBoxMotDePasse.SelectedText = "";
            this.textBoxMotDePasse.Size = new System.Drawing.Size(364, 45);
            this.textBoxMotDePasse.TabIndex = 3;
            this.textBoxMotDePasse.TextOffset = new System.Drawing.Point(10, 0);
            //
            // textBoxNomUtilisateur
            //
            this.textBoxNomUtilisateur.BorderRadius = 8;
            this.textBoxNomUtilisateur.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxNomUtilisateur.DefaultText = "";
            this.textBoxNomUtilisateur.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxNomUtilisateur.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxNomUtilisateur.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxNomUtilisateur.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxNomUtilisateur.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxNomUtilisateur.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.textBoxNomUtilisateur.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxNomUtilisateur.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxNomUtilisateur.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.textBoxNomUtilisateur.Location = new System.Drawing.Point(48, 258);
            this.textBoxNomUtilisateur.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxNomUtilisateur.Name = "textBoxNomUtilisateur";
            this.textBoxNomUtilisateur.PasswordChar = '\0';
            this.textBoxNomUtilisateur.PlaceholderText = "Saisissez votre nom d\'utilisateur";
            this.textBoxNomUtilisateur.SelectedText = "";
            this.textBoxNomUtilisateur.Size = new System.Drawing.Size(364, 45);
            this.textBoxNomUtilisateur.TabIndex = 1;
            this.textBoxNomUtilisateur.TextOffset = new System.Drawing.Point(10, 0);
            //
            // labelMotDePasse
            //
            this.labelMotDePasse.AutoSize = true;
            this.labelMotDePasse.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMotDePasse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelMotDePasse.Location = new System.Drawing.Point(48, 322);
            this.labelMotDePasse.Name = "labelMotDePasse";
            this.labelMotDePasse.Size = new System.Drawing.Size(101, 19);
            this.labelMotDePasse.TabIndex = 2;
            this.labelMotDePasse.Text = "Mot de passe";
            //
            // labelNomUtilisateur
            //
            this.labelNomUtilisateur.AutoSize = true;
            this.labelNomUtilisateur.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNomUtilisateur.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelNomUtilisateur.Location = new System.Drawing.Point(48, 230);
            this.labelNomUtilisateur.Name = "labelNomUtilisateur";
            this.labelNomUtilisateur.Size = new System.Drawing.Size(126, 19);
            this.labelNomUtilisateur.TabIndex = 0;
            this.labelNomUtilisateur.Text = "Nom d\'utilisateur";
            //
            // labelSubTitle
            //
            this.labelSubTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.labelSubTitle.Location = new System.Drawing.Point(48, 180);
            this.labelSubTitle.Name = "labelSubTitle";
            this.labelSubTitle.Size = new System.Drawing.Size(364, 23);
            this.labelSubTitle.TabIndex = 1;
            this.labelSubTitle.Text = "Connectez-vous pour accéder au système";
            this.labelSubTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // labelTitle
            //
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelTitle.Location = new System.Drawing.Point(48, 140);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(364, 32);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "CONNEXION";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // pictureBoxLogo
            //
            this.pictureBoxLogo.Location = new System.Drawing.Point(155, 40);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(150, 80);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            //
            // LoginForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 650);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connexion - Gestion Moderne RH";
            this.panelMain.ResumeLayout(false);
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelSubTitle;
        private System.Windows.Forms.Label labelNomUtilisateur;
        private System.Windows.Forms.Label labelMotDePasse;
        private Guna.UI2.WinForms.Guna2TextBox textBoxNomUtilisateur;
        private Guna.UI2.WinForms.Guna2TextBox textBoxMotDePasse;
        private Guna.UI2.WinForms.Guna2Button buttonConnexion;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
        private Guna.UI2.WinForms.Guna2CheckBox checkBoxAfficherMdp;
        private System.Windows.Forms.LinkLabel linkLabelMotDePasseOublie;
    }
}
