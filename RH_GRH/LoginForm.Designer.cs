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
            this.panelLogin = new Guna.UI2.WinForms.Guna2Panel();
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
            this.panelLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.labelAppDesc = new System.Windows.Forms.Label();
            this.labelAppName = new System.Windows.Forms.Label();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.guna2VSeparator1 = new Guna.UI2.WinForms.Guna2VSeparator();
            this.guna2Separator2 = new Guna.UI2.WinForms.Guna2Separator();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.panelMain.SuspendLayout();
            this.panelLogin.SuspendLayout();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.panelLogin);
            this.panelMain.Controls.Add(this.panelLeft);
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1200, 738);
            this.panelMain.TabIndex = 0;
            // 
            // panelLogin
            // 
            this.panelLogin.BackColor = System.Drawing.Color.Transparent;
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
            this.panelLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLogin.FillColor = System.Drawing.Color.White;
            this.panelLogin.Location = new System.Drawing.Point(644, 0);
            this.panelLogin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Padding = new System.Windows.Forms.Padding(80, 98, 80, 98);
            this.panelLogin.Size = new System.Drawing.Size(556, 738);
            this.panelLogin.TabIndex = 1;
            // 
            // linkLabelMotDePasseOublie
            // 
            this.linkLabelMotDePasseOublie.AutoSize = true;
            this.linkLabelMotDePasseOublie.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelMotDePasseOublie.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(43)))), ((int)(((byte)(132)))));
            this.linkLabelMotDePasseOublie.Location = new System.Drawing.Point(287, 375);
            this.linkLabelMotDePasseOublie.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabelMotDePasseOublie.Name = "linkLabelMotDePasseOublie";
            this.linkLabelMotDePasseOublie.Size = new System.Drawing.Size(156, 22);
            this.linkLabelMotDePasseOublie.TabIndex = 5;
            this.linkLabelMotDePasseOublie.TabStop = true;
            this.linkLabelMotDePasseOublie.Text = "Mot de passe oublié?";
            this.linkLabelMotDePasseOublie.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelMotDePasseOublie_LinkClicked);
            // 
            // checkBoxAfficherMdp
            // 
            this.checkBoxAfficherMdp.AutoSize = true;
            this.checkBoxAfficherMdp.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(43)))), ((int)(((byte)(132)))));
            this.checkBoxAfficherMdp.CheckedState.BorderRadius = 3;
            this.checkBoxAfficherMdp.CheckedState.BorderThickness = 0;
            this.checkBoxAfficherMdp.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(43)))), ((int)(((byte)(132)))));
            this.checkBoxAfficherMdp.Font = new System.Drawing.Font("Montserrat", 8F);
            this.checkBoxAfficherMdp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.checkBoxAfficherMdp.Location = new System.Drawing.Point(80, 373);
            this.checkBoxAfficherMdp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxAfficherMdp.Name = "checkBoxAfficherMdp";
            this.checkBoxAfficherMdp.Size = new System.Drawing.Size(201, 26);
            this.checkBoxAfficherMdp.TabIndex = 4;
            this.checkBoxAfficherMdp.Text = "Afficher le mot de passe";
            this.checkBoxAfficherMdp.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.checkBoxAfficherMdp.UncheckedState.BorderRadius = 3;
            this.checkBoxAfficherMdp.UncheckedState.BorderThickness = 2;
            this.checkBoxAfficherMdp.UncheckedState.FillColor = System.Drawing.Color.White;
            this.checkBoxAfficherMdp.UseVisualStyleBackColor = true;
            this.checkBoxAfficherMdp.CheckedChanged += new System.EventHandler(this.checkBoxAfficherMdp_CheckedChanged);
            // 
            // buttonAnnuler
            // 
            this.buttonAnnuler.Animated = true;
            this.buttonAnnuler.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(225)))));
            this.buttonAnnuler.BorderRadius = 12;
            this.buttonAnnuler.BorderThickness = 2;
            this.buttonAnnuler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAnnuler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAnnuler.FillColor = System.Drawing.Color.White;
            this.buttonAnnuler.Font = new System.Drawing.Font("Montserrat", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.buttonAnnuler.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(43)))), ((int)(((byte)(132)))));
            this.buttonAnnuler.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.buttonAnnuler.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(43)))), ((int)(((byte)(132)))));
            this.buttonAnnuler.Location = new System.Drawing.Point(80, 498);
            this.buttonAnnuler.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(387, 59);
            this.buttonAnnuler.TabIndex = 7;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            // 
            // buttonConnexion
            // 
            this.buttonConnexion.Animated = true;
            this.buttonConnexion.BackColor = System.Drawing.Color.Transparent;
            this.buttonConnexion.BorderRadius = 12;
            this.buttonConnexion.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonConnexion.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonConnexion.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonConnexion.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonConnexion.FillColor = System.Drawing.Color.MidnightBlue;
            this.buttonConnexion.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonConnexion.ForeColor = System.Drawing.Color.White;
            this.buttonConnexion.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.buttonConnexion.Location = new System.Drawing.Point(80, 418);
            this.buttonConnexion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonConnexion.Name = "buttonConnexion";
            this.buttonConnexion.ShadowDecoration.BorderRadius = 12;
            this.buttonConnexion.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(43)))), ((int)(((byte)(132)))));
            this.buttonConnexion.ShadowDecoration.Depth = 20;
            this.buttonConnexion.ShadowDecoration.Enabled = true;
            this.buttonConnexion.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 5, 0, 10);
            this.buttonConnexion.Size = new System.Drawing.Size(387, 64);
            this.buttonConnexion.TabIndex = 6;
            this.buttonConnexion.Text = "Se connecter";
            this.buttonConnexion.Click += new System.EventHandler(this.buttonConnexion_Click);
            // 
            // textBoxMotDePasse
            // 
            this.textBoxMotDePasse.Animated = true;
            this.textBoxMotDePasse.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(225)))));
            this.textBoxMotDePasse.BorderRadius = 12;
            this.textBoxMotDePasse.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxMotDePasse.DefaultText = "";
            this.textBoxMotDePasse.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxMotDePasse.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxMotDePasse.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxMotDePasse.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxMotDePasse.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.textBoxMotDePasse.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(43)))), ((int)(((byte)(132)))));
            this.textBoxMotDePasse.FocusedState.FillColor = System.Drawing.Color.White;
            this.textBoxMotDePasse.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxMotDePasse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
            this.textBoxMotDePasse.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(43)))), ((int)(((byte)(132)))));
            this.textBoxMotDePasse.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.textBoxMotDePasse.Location = new System.Drawing.Point(80, 295);
            this.textBoxMotDePasse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxMotDePasse.Name = "textBoxMotDePasse";
            this.textBoxMotDePasse.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.textBoxMotDePasse.PlaceholderText = "Mot de passe";
            this.textBoxMotDePasse.SelectedText = "";
            this.textBoxMotDePasse.Size = new System.Drawing.Size(387, 59);
            this.textBoxMotDePasse.TabIndex = 3;
            this.textBoxMotDePasse.TextOffset = new System.Drawing.Point(5, 0);
            // 
            // textBoxNomUtilisateur
            // 
            this.textBoxNomUtilisateur.Animated = true;
            this.textBoxNomUtilisateur.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(225)))));
            this.textBoxNomUtilisateur.BorderRadius = 12;
            this.textBoxNomUtilisateur.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxNomUtilisateur.DefaultText = "";
            this.textBoxNomUtilisateur.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxNomUtilisateur.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxNomUtilisateur.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxNomUtilisateur.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxNomUtilisateur.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.textBoxNomUtilisateur.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(43)))), ((int)(((byte)(132)))));
            this.textBoxNomUtilisateur.FocusedState.FillColor = System.Drawing.Color.White;
            this.textBoxNomUtilisateur.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxNomUtilisateur.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
            this.textBoxNomUtilisateur.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(43)))), ((int)(((byte)(132)))));
            this.textBoxNomUtilisateur.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.textBoxNomUtilisateur.Location = new System.Drawing.Point(80, 197);
            this.textBoxNomUtilisateur.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxNomUtilisateur.Name = "textBoxNomUtilisateur";
            this.textBoxNomUtilisateur.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.textBoxNomUtilisateur.PlaceholderText = "Nom d\'utilisateur";
            this.textBoxNomUtilisateur.SelectedText = "";
            this.textBoxNomUtilisateur.Size = new System.Drawing.Size(387, 59);
            this.textBoxNomUtilisateur.TabIndex = 1;
            this.textBoxNomUtilisateur.TextOffset = new System.Drawing.Point(5, 0);
            // 
            // labelMotDePasse
            // 
            this.labelMotDePasse.AutoSize = true;
            this.labelMotDePasse.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMotDePasse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.labelMotDePasse.Location = new System.Drawing.Point(80, 265);
            this.labelMotDePasse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMotDePasse.Name = "labelMotDePasse";
            this.labelMotDePasse.Size = new System.Drawing.Size(112, 24);
            this.labelMotDePasse.TabIndex = 2;
            this.labelMotDePasse.Text = "Mot de passe";
            // 
            // labelNomUtilisateur
            // 
            this.labelNomUtilisateur.AutoSize = true;
            this.labelNomUtilisateur.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNomUtilisateur.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.labelNomUtilisateur.Location = new System.Drawing.Point(80, 166);
            this.labelNomUtilisateur.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNomUtilisateur.Name = "labelNomUtilisateur";
            this.labelNomUtilisateur.Size = new System.Drawing.Size(146, 24);
            this.labelNomUtilisateur.TabIndex = 0;
            this.labelNomUtilisateur.Text = "Nom d\'utilisateur";
            // 
            // labelSubTitle
            // 
            this.labelSubTitle.Font = new System.Drawing.Font("Montserrat", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(125)))));
            this.labelSubTitle.Location = new System.Drawing.Point(80, 129);
            this.labelSubTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSubTitle.Name = "labelSubTitle";
            this.labelSubTitle.Size = new System.Drawing.Size(387, 27);
            this.labelSubTitle.TabIndex = 1;
            this.labelSubTitle.Text = "Accédez à votre espace";
            this.labelSubTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("Montserrat", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
            this.labelTitle.Location = new System.Drawing.Point(84, 58);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(387, 49);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Connexion";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.Transparent;
            this.panelLeft.Controls.Add(this.guna2Separator2);
            this.panelLeft.Controls.Add(this.pictureBoxLogo);
            this.panelLeft.Controls.Add(this.guna2VSeparator1);
            this.panelLeft.Controls.Add(this.labelAppDesc);
            this.panelLeft.Controls.Add(this.labelAppName);
            this.panelLeft.Controls.Add(this.labelWelcome);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.FillColor = System.Drawing.Color.MidnightBlue;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(53, 49, 53, 49);
            this.panelLeft.Size = new System.Drawing.Size(644, 738);
            this.panelLeft.TabIndex = 0;
            this.panelLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.panelLeft_Paint);
            // 
            // labelAppDesc
            // 
            this.labelAppDesc.Font = new System.Drawing.Font("Montserrat", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAppDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(235)))));
            this.labelAppDesc.Location = new System.Drawing.Point(99, 464);
            this.labelAppDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAppDesc.Name = "labelAppDesc";
            this.labelAppDesc.Size = new System.Drawing.Size(427, 98);
            this.labelAppDesc.TabIndex = 2;
            this.labelAppDesc.Text = "Progiciel complet de gestion des ressources humaines pour optimisation des proces" +
    "sus RH.";
            this.labelAppDesc.Click += new System.EventHandler(this.labelAppDesc_Click);
            // 
            // labelAppName
            // 
            this.labelAppName.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAppName.ForeColor = System.Drawing.Color.White;
            this.labelAppName.Location = new System.Drawing.Point(97, 415);
            this.labelAppName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAppName.Name = "labelAppName";
            this.labelAppName.Size = new System.Drawing.Size(427, 49);
            this.labelAppName.TabIndex = 1;
            this.labelAppName.Text = "Gestion Moderne RH";
            this.labelAppName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelWelcome
            // 
            this.labelWelcome.Font = new System.Drawing.Font("Montserrat ExtraBold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWelcome.ForeColor = System.Drawing.Color.White;
            this.labelWelcome.Location = new System.Drawing.Point(88, 332);
            this.labelWelcome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(427, 74);
            this.labelWelcome.TabIndex = 0;
            this.labelWelcome.Text = "Bienvenue !";
            this.labelWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // guna2VSeparator1
            // 
            this.guna2VSeparator1.FillColor = System.Drawing.Color.White;
            this.guna2VSeparator1.FillThickness = 2;
            this.guna2VSeparator1.Location = new System.Drawing.Point(77, 358);
            this.guna2VSeparator1.Name = "guna2VSeparator1";
            this.guna2VSeparator1.Size = new System.Drawing.Size(22, 40);
            this.guna2VSeparator1.TabIndex = 3;
            // 
            // guna2Separator2
            // 
            this.guna2Separator2.FillColor = System.Drawing.Color.White;
            this.guna2Separator2.FillThickness = 2;
            this.guna2Separator2.Location = new System.Drawing.Point(104, 453);
            this.guna2Separator2.Name = "guna2Separator2";
            this.guna2Separator2.Size = new System.Drawing.Size(303, 10);
            this.guna2Separator2.TabIndex = 6;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLogo.Image = global::RH_GRH.Properties.Resources.PROPOSITION_LOGO_RH___2_;
            this.pictureBoxLogo.Location = new System.Drawing.Point(173, 27);
            this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(255, 290);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 4;
            this.pictureBoxLogo.TabStop = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 628);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private Guna.UI2.WinForms.Guna2Panel panelLeft;
        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.Label labelAppName;
        private System.Windows.Forms.Label labelAppDesc;
        private Guna.UI2.WinForms.Guna2Panel panelLogin;
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
        private Guna.UI2.WinForms.Guna2VSeparator guna2VSeparator1;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator2;
    }
}
