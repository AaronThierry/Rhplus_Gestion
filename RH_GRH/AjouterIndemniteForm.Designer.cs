namespace RH_GRH
{
    partial class AjouterIndemniteForm
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.buttonAnnuler = new Guna.UI2.WinForms.Guna2Button();
            this.buttonValider = new Guna.UI2.WinForms.Guna2Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.buttonAjouterLigne = new Guna.UI2.WinForms.Guna2Button();
            this.panelLignes = new System.Windows.Forms.Panel();
            this.labelIndemnites = new System.Windows.Forms.Label();
            this.panelEmploye = new System.Windows.Forms.Panel();
            this.comboBoxEmploye = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labelEmploye = new System.Windows.Forms.Label();
            this.panelEntreprise = new System.Windows.Forms.Panel();
            this.comboBoxEntreprise = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labelEntreprise = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.labelTitre = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelEmploye.SuspendLayout();
            this.panelEntreprise.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            //
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.panelFooter);
            this.panelMain.Controls.Add(this.panelContent);
            this.panelMain.Controls.Add(this.panelHeader);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(900, 650);
            this.panelMain.TabIndex = 0;
            // 
            // panelFooter
            //
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelFooter.Controls.Add(this.buttonAnnuler);
            this.panelFooter.Controls.Add(this.buttonValider);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 585);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(25, 12, 25, 12);
            this.panelFooter.Size = new System.Drawing.Size(900, 65);
            this.panelFooter.TabIndex = 2;
            // 
            // buttonAnnuler
            //
            this.buttonAnnuler.Animated = true;
            this.buttonAnnuler.BackColor = System.Drawing.Color.Transparent;
            this.buttonAnnuler.BorderRadius = 8;
            this.buttonAnnuler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAnnuler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAnnuler.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonAnnuler.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.buttonAnnuler.Font = new System.Drawing.Font("Montserrat", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.White;
            this.buttonAnnuler.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(98)))), ((int)(((byte)(104)))));
            this.buttonAnnuler.Location = new System.Drawing.Point(620, 12);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(120, 41);
            this.buttonAnnuler.TabIndex = 1;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            //
            // buttonValider
            //
            this.buttonValider.Animated = true;
            this.buttonValider.BackColor = System.Drawing.Color.Transparent;
            this.buttonValider.BorderRadius = 8;
            this.buttonValider.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonValider.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonValider.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonValider.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonValider.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonValider.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(112)))));
            this.buttonValider.Font = new System.Drawing.Font("Montserrat", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonValider.ForeColor = System.Drawing.Color.White;
            this.buttonValider.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.buttonValider.Location = new System.Drawing.Point(740, 12);
            this.buttonValider.Name = "buttonValider";
            this.buttonValider.Size = new System.Drawing.Size(135, 41);
            this.buttonValider.TabIndex = 0;
            this.buttonValider.Text = "Valider";
            this.buttonValider.Click += new System.EventHandler(this.buttonValider_Click);
            // 
            // panelContent
            //
            this.panelContent.AutoScroll = true;
            this.panelContent.BackColor = System.Drawing.Color.White;
            this.panelContent.Controls.Add(this.buttonAjouterLigne);
            this.panelContent.Controls.Add(this.panelLignes);
            this.panelContent.Controls.Add(this.labelIndemnites);
            this.panelContent.Controls.Add(this.panelEmploye);
            this.panelContent.Controls.Add(this.panelEntreprise);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 90);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(35, 18, 35, 18);
            this.panelContent.Size = new System.Drawing.Size(900, 495);
            this.panelContent.TabIndex = 1;
            // 
            // buttonAjouterLigne
            //
            this.buttonAjouterLigne.Animated = true;
            this.buttonAjouterLigne.BackColor = System.Drawing.Color.Transparent;
            this.buttonAjouterLigne.BorderRadius = 10;
            this.buttonAjouterLigne.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAjouterLigne.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAjouterLigne.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAjouterLigne.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAjouterLigne.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.buttonAjouterLigne.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAjouterLigne.ForeColor = System.Drawing.Color.White;
            this.buttonAjouterLigne.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(179)))), ((int)(((byte)(113)))));
            this.buttonAjouterLigne.Location = new System.Drawing.Point(40, 445);
            this.buttonAjouterLigne.Name = "buttonAjouterLigne";
            this.buttonAjouterLigne.Size = new System.Drawing.Size(250, 45);
            this.buttonAjouterLigne.TabIndex = 4;
            this.buttonAjouterLigne.Text = "+ Ajouter une indemnité";
            this.buttonAjouterLigne.Click += new System.EventHandler(this.buttonAjouterLigne_Click);
            // 
            // panelLignes
            //
            this.panelLignes.AutoScroll = true;
            this.panelLignes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelLignes.Location = new System.Drawing.Point(35, 190);
            this.panelLignes.Name = "panelLignes";
            this.panelLignes.Padding = new System.Windows.Forms.Padding(12);
            this.panelLignes.Size = new System.Drawing.Size(830, 200);
            this.panelLignes.TabIndex = 3;
            // 
            // labelIndemnites
            //
            this.labelIndemnites.AutoSize = true;
            this.labelIndemnites.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Bold);
            this.labelIndemnites.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.labelIndemnites.Location = new System.Drawing.Point(40, 175);
            this.labelIndemnites.Name = "labelIndemnites";
            this.labelIndemnites.Size = new System.Drawing.Size(250, 28);
            this.labelIndemnites.TabIndex = 2;
            this.labelIndemnites.Text = "Indemnités à enregistrer";
            // 
            // panelEmploye
            //
            this.panelEmploye.BackColor = System.Drawing.Color.Transparent;
            this.panelEmploye.Controls.Add(this.comboBoxEmploye);
            this.panelEmploye.Controls.Add(this.labelEmploye);
            this.panelEmploye.Location = new System.Drawing.Point(35, 88);
            this.panelEmploye.Name = "panelEmploye";
            this.panelEmploye.Size = new System.Drawing.Size(830, 65);
            this.panelEmploye.TabIndex = 1;
            // 
            // comboBoxEmploye
            //
            this.comboBoxEmploye.BackColor = System.Drawing.Color.Transparent;
            this.comboBoxEmploye.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.comboBoxEmploye.BorderRadius = 8;
            this.comboBoxEmploye.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEmploye.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEmploye.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(112)))));
            this.comboBoxEmploye.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(112)))));
            this.comboBoxEmploye.Font = new System.Drawing.Font("Montserrat", 9.5F);
            this.comboBoxEmploye.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comboBoxEmploye.ItemHeight = 36;
            this.comboBoxEmploye.Location = new System.Drawing.Point(0, 25);
            this.comboBoxEmploye.Name = "comboBoxEmploye";
            this.comboBoxEmploye.Size = new System.Drawing.Size(830, 42);
            this.comboBoxEmploye.TabIndex = 1;
            //
            // labelEmploye
            //
            this.labelEmploye.AutoSize = true;
            this.labelEmploye.Font = new System.Drawing.Font("Montserrat", 9.5F, System.Drawing.FontStyle.Bold);
            this.labelEmploye.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.labelEmploye.Location = new System.Drawing.Point(0, 2);
            this.labelEmploye.Name = "labelEmploye";
            this.labelEmploye.Size = new System.Drawing.Size(200, 26);
            this.labelEmploye.TabIndex = 0;
            this.labelEmploye.Text = "Sélectionner l\'employé";
            // 
            // panelEntreprise
            //
            this.panelEntreprise.BackColor = System.Drawing.Color.Transparent;
            this.panelEntreprise.Controls.Add(this.comboBoxEntreprise);
            this.panelEntreprise.Controls.Add(this.labelEntreprise);
            this.panelEntreprise.Location = new System.Drawing.Point(35, 18);
            this.panelEntreprise.Name = "panelEntreprise";
            this.panelEntreprise.Size = new System.Drawing.Size(830, 65);
            this.panelEntreprise.TabIndex = 0;
            // 
            // comboBoxEntreprise
            //
            this.comboBoxEntreprise.BackColor = System.Drawing.Color.Transparent;
            this.comboBoxEntreprise.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.comboBoxEntreprise.BorderRadius = 8;
            this.comboBoxEntreprise.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEntreprise.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEntreprise.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(112)))));
            this.comboBoxEntreprise.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(112)))));
            this.comboBoxEntreprise.Font = new System.Drawing.Font("Montserrat", 9.5F);
            this.comboBoxEntreprise.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comboBoxEntreprise.ItemHeight = 36;
            this.comboBoxEntreprise.Location = new System.Drawing.Point(0, 25);
            this.comboBoxEntreprise.Name = "comboBoxEntreprise";
            this.comboBoxEntreprise.Size = new System.Drawing.Size(830, 42);
            this.comboBoxEntreprise.TabIndex = 1;
            //
            // labelEntreprise
            //
            this.labelEntreprise.AutoSize = true;
            this.labelEntreprise.Font = new System.Drawing.Font("Montserrat", 9.5F, System.Drawing.FontStyle.Bold);
            this.labelEntreprise.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.labelEntreprise.Location = new System.Drawing.Point(0, 2);
            this.labelEntreprise.Name = "labelEntreprise";
            this.labelEntreprise.Size = new System.Drawing.Size(210, 26);
            this.labelEntreprise.TabIndex = 0;
            this.labelEntreprise.Text = "Sélectionner l\'entreprise";
            // 
            // panelHeader
            //
            this.panelHeader.Controls.Add(this.labelSubtitle);
            this.panelHeader.Controls.Add(this.labelTitre);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(900, 90);
            this.panelHeader.TabIndex = 0;
            this.panelHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHeader_Paint);
            // 
            // labelSubtitle
            //
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.labelSubtitle.Font = new System.Drawing.Font("Montserrat", 9.5F);
            this.labelSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.labelSubtitle.Location = new System.Drawing.Point(35, 65);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(440, 25);
            this.labelSubtitle.TabIndex = 1;
            this.labelSubtitle.Text = "Enregistrez plusieurs indemnités en une seule opération";
            //
            // labelTitre
            //
            this.labelTitre.AutoSize = true;
            this.labelTitre.BackColor = System.Drawing.Color.Transparent;
            this.labelTitre.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Bold);
            this.labelTitre.ForeColor = System.Drawing.Color.White;
            this.labelTitre.Location = new System.Drawing.Point(30, 25);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(300, 48);
            this.labelTitre.TabIndex = 0;
            this.labelTitre.Text = "Ajouter Indemnités";
            // 
            // AjouterIndemniteForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AjouterIndemniteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ajouter des indemnités";
            this.panelMain.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.panelEmploye.ResumeLayout(false);
            this.panelEmploye.PerformLayout();
            this.panelEntreprise.ResumeLayout(false);
            this.panelEntreprise.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.Label labelSubtitle;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelEntreprise;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxEntreprise;
        private System.Windows.Forms.Label labelEntreprise;
        private System.Windows.Forms.Panel panelEmploye;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxEmploye;
        private System.Windows.Forms.Label labelEmploye;
        private System.Windows.Forms.Label labelIndemnites;
        private System.Windows.Forms.Panel panelLignes;
        private Guna.UI2.WinForms.Guna2Button buttonAjouterLigne;
        private System.Windows.Forms.Panel panelFooter;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
        private Guna.UI2.WinForms.Guna2Button buttonValider;
    }
}
