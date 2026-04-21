namespace RH_GRH
{
    partial class LoginFormModern
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginFormModern));
            this.panelBackground = new RH_GRH.CustomGradientPanel();
            this.panelGlass = new System.Windows.Forms.Panel();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.panelUsername = new System.Windows.Forms.Panel();
            this.pictureBoxUserIcon = new System.Windows.Forms.PictureBox();
            this.textBoxNomUtilisateur = new System.Windows.Forms.TextBox();
            this.panelPassword = new System.Windows.Forms.Panel();
            this.pictureBoxLockIcon = new System.Windows.Forms.PictureBox();
            this.textBoxMotDePasse = new System.Windows.Forms.TextBox();
            this.checkBoxAfficherMdp = new System.Windows.Forms.CheckBox();
            this.buttonConnexion = new System.Windows.Forms.Button();
            this.linkLabelMotDePasseOublie = new System.Windows.Forms.LinkLabel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelAppName = new System.Windows.Forms.Label();
            this.buttonMinimize = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelVersion = new System.Windows.Forms.Label();
            this.panelBackground.SuspendLayout();
            this.panelGlass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.panelUsername.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUserIcon)).BeginInit();
            this.panelPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLockIcon)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBackground
            // 
            this.panelBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelBackground.Controls.Add(this.panelGlass);
            this.panelBackground.Controls.Add(this.panelHeader);
            this.panelBackground.Controls.Add(this.labelVersion);
            this.panelBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBackground.Location = new System.Drawing.Point(0, 0);
            this.panelBackground.Margin = new System.Windows.Forms.Padding(4);
            this.panelBackground.Name = "panelBackground";
            this.panelBackground.Size = new System.Drawing.Size(587, 714);
            this.panelBackground.TabIndex = 0;
            // 
            // panelGlass
            // 
            this.panelGlass.BackColor = System.Drawing.Color.White;
            this.panelGlass.Controls.Add(this.pictureBoxLogo);
            this.panelGlass.Controls.Add(this.labelWelcome);
            this.panelGlass.Controls.Add(this.labelSubtitle);
            this.panelGlass.Controls.Add(this.panelUsername);
            this.panelGlass.Controls.Add(this.panelPassword);
            this.panelGlass.Controls.Add(this.checkBoxAfficherMdp);
            this.panelGlass.Controls.Add(this.buttonConnexion);
            this.panelGlass.Controls.Add(this.linkLabelMotDePasseOublie);
            this.panelGlass.Location = new System.Drawing.Point(53, 86);
            this.panelGlass.Margin = new System.Windows.Forms.Padding(4);
            this.panelGlass.Name = "panelGlass";
            this.panelGlass.Size = new System.Drawing.Size(480, 578);
            this.panelGlass.TabIndex = 1;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLogo.Image = global::RH_GRH.Properties.Resources.logo_RH___1_;
            this.pictureBoxLogo.Location = new System.Drawing.Point(153, 25);
            this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(173, 160);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            this.pictureBoxLogo.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxLogo_Paint);
            // 
            // labelWelcome
            // 
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Bold);
            this.labelWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.labelWelcome.Location = new System.Drawing.Point(133, 197);
            this.labelWelcome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(190, 47);
            this.labelWelcome.TabIndex = 1;
            this.labelWelcome.Text = "Bienvenue";
            this.labelWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSubtitle
            // 
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.Font = new System.Drawing.Font("Montserrat", 9F);
            this.labelSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelSubtitle.Location = new System.Drawing.Point(80, 255);
            this.labelSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(245, 24);
            this.labelSubtitle.TabIndex = 2;
            this.labelSubtitle.Text = "Connectez-vous pour continuer";
            this.labelSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelUsername
            // 
            this.panelUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.panelUsername.Controls.Add(this.pictureBoxUserIcon);
            this.panelUsername.Controls.Add(this.textBoxNomUtilisateur);
            this.panelUsername.Location = new System.Drawing.Point(40, 295);
            this.panelUsername.Margin = new System.Windows.Forms.Padding(4);
            this.panelUsername.Name = "panelUsername";
            this.panelUsername.Size = new System.Drawing.Size(400, 55);
            this.panelUsername.TabIndex = 3;
            // 
            // pictureBoxUserIcon
            // 
            this.pictureBoxUserIcon.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxUserIcon.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxUserIcon.Image")));
            this.pictureBoxUserIcon.Location = new System.Drawing.Point(16, 14);
            this.pictureBoxUserIcon.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxUserIcon.Name = "pictureBoxUserIcon";
            this.pictureBoxUserIcon.Size = new System.Drawing.Size(29, 27);
            this.pictureBoxUserIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxUserIcon.TabIndex = 0;
            this.pictureBoxUserIcon.TabStop = false;
            // 
            // textBoxNomUtilisateur
            // 
            this.textBoxNomUtilisateur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.textBoxNomUtilisateur.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxNomUtilisateur.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxNomUtilisateur.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.textBoxNomUtilisateur.Location = new System.Drawing.Point(56, 16);
            this.textBoxNomUtilisateur.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxNomUtilisateur.Name = "textBoxNomUtilisateur";
            this.textBoxNomUtilisateur.Size = new System.Drawing.Size(327, 21);
            this.textBoxNomUtilisateur.TabIndex = 1;
            this.textBoxNomUtilisateur.Text = "Nom d\'utilisateur";
            // 
            // panelPassword
            // 
            this.panelPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.panelPassword.Controls.Add(this.pictureBoxLockIcon);
            this.panelPassword.Controls.Add(this.textBoxMotDePasse);
            this.panelPassword.Location = new System.Drawing.Point(40, 363);
            this.panelPassword.Margin = new System.Windows.Forms.Padding(4);
            this.panelPassword.Name = "panelPassword";
            this.panelPassword.Size = new System.Drawing.Size(400, 55);
            this.panelPassword.TabIndex = 4;
            // 
            // pictureBoxLockIcon
            // 
            this.pictureBoxLockIcon.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLockIcon.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLockIcon.Image")));
            this.pictureBoxLockIcon.Location = new System.Drawing.Point(16, 14);
            this.pictureBoxLockIcon.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxLockIcon.Name = "pictureBoxLockIcon";
            this.pictureBoxLockIcon.Size = new System.Drawing.Size(29, 27);
            this.pictureBoxLockIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLockIcon.TabIndex = 0;
            this.pictureBoxLockIcon.TabStop = false;
            // 
            // textBoxMotDePasse
            // 
            this.textBoxMotDePasse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.textBoxMotDePasse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMotDePasse.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxMotDePasse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.textBoxMotDePasse.Location = new System.Drawing.Point(56, 16);
            this.textBoxMotDePasse.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxMotDePasse.Name = "textBoxMotDePasse";
            this.textBoxMotDePasse.Size = new System.Drawing.Size(327, 21);
            this.textBoxMotDePasse.TabIndex = 1;
            this.textBoxMotDePasse.Text = "Mot de passe";
            this.textBoxMotDePasse.UseSystemPasswordChar = true;
            // 
            // checkBoxAfficherMdp
            // 
            this.checkBoxAfficherMdp.AutoSize = true;
            this.checkBoxAfficherMdp.Font = new System.Drawing.Font("Montserrat", 8F);
            this.checkBoxAfficherMdp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.checkBoxAfficherMdp.Location = new System.Drawing.Point(40, 431);
            this.checkBoxAfficherMdp.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxAfficherMdp.Name = "checkBoxAfficherMdp";
            this.checkBoxAfficherMdp.Size = new System.Drawing.Size(201, 26);
            this.checkBoxAfficherMdp.TabIndex = 5;
            this.checkBoxAfficherMdp.Text = "Afficher le mot de passe";
            this.checkBoxAfficherMdp.UseVisualStyleBackColor = true;
            this.checkBoxAfficherMdp.CheckedChanged += new System.EventHandler(this.checkBoxAfficherMdp_CheckedChanged);
            // 
            // buttonConnexion
            // 
            this.buttonConnexion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonConnexion.FlatAppearance.BorderSize = 0;
            this.buttonConnexion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConnexion.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonConnexion.ForeColor = System.Drawing.Color.White;
            this.buttonConnexion.Location = new System.Drawing.Point(40, 468);
            this.buttonConnexion.Margin = new System.Windows.Forms.Padding(4);
            this.buttonConnexion.Name = "buttonConnexion";
            this.buttonConnexion.Size = new System.Drawing.Size(400, 55);
            this.buttonConnexion.TabIndex = 6;
            this.buttonConnexion.Text = "SE CONNECTER";
            this.buttonConnexion.UseVisualStyleBackColor = false;
            this.buttonConnexion.Click += new System.EventHandler(this.buttonConnexion_Click);
            // 
            // linkLabelMotDePasseOublie
            // 
            this.linkLabelMotDePasseOublie.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.linkLabelMotDePasseOublie.AutoSize = true;
            this.linkLabelMotDePasseOublie.Font = new System.Drawing.Font("Montserrat", 8F);
            this.linkLabelMotDePasseOublie.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(139)))), ((int)(((byte)(196)))));
            this.linkLabelMotDePasseOublie.Location = new System.Drawing.Point(140, 535);
            this.linkLabelMotDePasseOublie.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabelMotDePasseOublie.Name = "linkLabelMotDePasseOublie";
            this.linkLabelMotDePasseOublie.Size = new System.Drawing.Size(161, 22);
            this.linkLabelMotDePasseOublie.TabIndex = 7;
            this.linkLabelMotDePasseOublie.TabStop = true;
            this.linkLabelMotDePasseOublie.Text = "Mot de passe oublié ?";
            this.linkLabelMotDePasseOublie.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelMotDePasseOublie_LinkClicked);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.labelAppName);
            this.panelHeader.Controls.Add(this.buttonMinimize);
            this.panelHeader.Controls.Add(this.buttonClose);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(587, 55);
            this.panelHeader.TabIndex = 0;
            // 
            // labelAppName
            // 
            this.labelAppName.AutoSize = true;
            this.labelAppName.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAppName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(139)))), ((int)(((byte)(196)))));
            this.labelAppName.Location = new System.Drawing.Point(21, 17);
            this.labelAppName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAppName.Name = "labelAppName";
            this.labelAppName.Size = new System.Drawing.Size(133, 27);
            this.labelAppName.TabIndex = 0;
            this.labelAppName.Text = "RH+ GESTION";
            // 
            // buttonMinimize
            // 
            this.buttonMinimize.BackColor = System.Drawing.Color.Transparent;
            this.buttonMinimize.FlatAppearance.BorderSize = 0;
            this.buttonMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.buttonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMinimize.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.buttonMinimize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.buttonMinimize.Location = new System.Drawing.Point(480, 6);
            this.buttonMinimize.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMinimize.Name = "buttonMinimize";
            this.buttonMinimize.Size = new System.Drawing.Size(47, 43);
            this.buttonMinimize.TabIndex = 1;
            this.buttonMinimize.Text = "—";
            this.buttonMinimize.UseVisualStyleBackColor = false;
            this.buttonMinimize.Click += new System.EventHandler(this.buttonMinimize_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.Transparent;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.buttonClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.buttonClose.Location = new System.Drawing.Point(533, 6);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(47, 43);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "✕";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Montserrat", 7F);
            this.labelVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.labelVersion.Location = new System.Drawing.Point(233, 683);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(73, 18);
            this.labelVersion.TabIndex = 2;
            this.labelVersion.Text = "Version 1.1.5";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelVersion.Click += new System.EventHandler(this.labelVersion_Click);
            // 
            // LoginFormModern
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 714);
            this.Controls.Add(this.panelBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LoginFormModern";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RH+ Gestion - Connexion";
            this.panelBackground.ResumeLayout(false);
            this.panelBackground.PerformLayout();
            this.panelGlass.ResumeLayout(false);
            this.panelGlass.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.panelUsername.ResumeLayout(false);
            this.panelUsername.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUserIcon)).EndInit();
            this.panelPassword.ResumeLayout(false);
            this.panelPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLockIcon)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomGradientPanel panelBackground;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelAppName;
        private System.Windows.Forms.Button buttonMinimize;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panelGlass;
        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.Label labelSubtitle;
        private System.Windows.Forms.Panel panelUsername;
        private System.Windows.Forms.PictureBox pictureBoxUserIcon;
        private System.Windows.Forms.TextBox textBoxNomUtilisateur;
        private System.Windows.Forms.Panel panelPassword;
        private System.Windows.Forms.PictureBox pictureBoxLockIcon;
        private System.Windows.Forms.TextBox textBoxMotDePasse;
        private System.Windows.Forms.CheckBox checkBoxAfficherMdp;
        private System.Windows.Forms.Button buttonConnexion;
        private System.Windows.Forms.LinkLabel linkLabelMotDePasseOublie;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
    }

    // Panel personnalisé avec dégradé animé et particules
    public class CustomGradientPanel : System.Windows.Forms.Panel
    {
        public CustomGradientPanel()
        {
            this.DoubleBuffered = true;
            this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint |
                         System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                         System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            // Dégradé de fond lumineux et subtil
            using (System.Drawing.Drawing2D.LinearGradientBrush brush =
                new System.Drawing.Drawing2D.LinearGradientBrush(
                    this.ClientRectangle,
                    System.Drawing.Color.FromArgb(248, 250, 252),
                    System.Drawing.Color.FromArgb(242, 246, 250),
                    45f))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            // Effet de lumière ambiante douce (bleu RH+)
            using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddEllipse(this.Width / 2 - 300, -200, 600, 600);
                using (System.Drawing.Drawing2D.PathGradientBrush pgb =
                    new System.Drawing.Drawing2D.PathGradientBrush(path))
                {
                    pgb.CenterColor = System.Drawing.Color.FromArgb(8, 56, 139, 196);
                    pgb.SurroundColors = new System.Drawing.Color[] {
                        System.Drawing.Color.FromArgb(0, 56, 139, 196)
                    };
                    e.Graphics.FillPath(pgb, path);
                }
            }
        }
    }
}
