namespace RH_GRH
{
    partial class AjouterIndemniteFormV3
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelTitre = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.buttonAnnuler = new Guna.UI2.WinForms.Guna2Button();
            this.buttonValider = new Guna.UI2.WinForms.Guna2Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.groupBoxIndemnites = new System.Windows.Forms.GroupBox();
            this.flowLayoutIndemnites = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAjouterIndemnite = new Guna.UI2.WinForms.Guna2Button();
            this.groupBoxEmploye = new System.Windows.Forms.GroupBox();
            this.labelEmploye = new System.Windows.Forms.Label();
            this.textBoxRecherche = new Guna.UI2.WinForms.Guna2TextBox();
            this.comboBoxEmploye = new Guna.UI2.WinForms.Guna2ComboBox();
            this.panelHeader.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.groupBoxIndemnites.SuspendLayout();
            this.groupBoxEmploye.SuspendLayout();
            this.SuspendLayout();
            //
            // panelHeader
            //
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(112)))));
            this.panelHeader.Controls.Add(this.labelTitre);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(900, 70);
            this.panelHeader.TabIndex = 0;
            //
            // labelTitre
            //
            this.labelTitre.AutoSize = true;
            this.labelTitre.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Bold);
            this.labelTitre.ForeColor = System.Drawing.Color.White;
            this.labelTitre.Location = new System.Drawing.Point(25, 20);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(280, 33);
            this.labelTitre.TabIndex = 0;
            this.labelTitre.Text = "Ajouter Indemnités";
            //
            // panelFooter
            //
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelFooter.Controls.Add(this.buttonAnnuler);
            this.panelFooter.Controls.Add(this.buttonValider);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 630);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.panelFooter.Size = new System.Drawing.Size(900, 70);
            this.panelFooter.TabIndex = 1;
            //
            // buttonAnnuler
            //
            this.buttonAnnuler.Animated = true;
            this.buttonAnnuler.BorderRadius = 8;
            this.buttonAnnuler.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAnnuler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAnnuler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAnnuler.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.buttonAnnuler.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.White;
            this.buttonAnnuler.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(98)))), ((int)(((byte)(104)))));
            this.buttonAnnuler.Location = new System.Drawing.Point(620, 15);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(130, 40);
            this.buttonAnnuler.TabIndex = 1;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            //
            // buttonValider
            //
            this.buttonValider.Animated = true;
            this.buttonValider.BorderRadius = 8;
            this.buttonValider.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonValider.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonValider.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonValider.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonValider.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonValider.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(112)))));
            this.buttonValider.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonValider.ForeColor = System.Drawing.Color.White;
            this.buttonValider.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.buttonValider.Location = new System.Drawing.Point(755, 15);
            this.buttonValider.Name = "buttonValider";
            this.buttonValider.Size = new System.Drawing.Size(130, 40);
            this.buttonValider.TabIndex = 0;
            this.buttonValider.Text = "Valider";
            this.buttonValider.Click += new System.EventHandler(this.buttonValider_Click);
            //
            // panelMain
            //
            this.panelMain.AutoScroll = true;
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.groupBoxIndemnites);
            this.panelMain.Controls.Add(this.groupBoxEmploye);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 70);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(30);
            this.panelMain.Size = new System.Drawing.Size(900, 560);
            this.panelMain.TabIndex = 2;
            //
            // groupBoxIndemnites
            //
            this.groupBoxIndemnites.Controls.Add(this.flowLayoutIndemnites);
            this.groupBoxIndemnites.Controls.Add(this.buttonAjouterIndemnite);
            this.groupBoxIndemnites.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxIndemnites.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.groupBoxIndemnites.Location = new System.Drawing.Point(30, 200);
            this.groupBoxIndemnites.Name = "groupBoxIndemnites";
            this.groupBoxIndemnites.Padding = new System.Windows.Forms.Padding(15);
            this.groupBoxIndemnites.Size = new System.Drawing.Size(840, 330);
            this.groupBoxIndemnites.TabIndex = 1;
            this.groupBoxIndemnites.TabStop = false;
            this.groupBoxIndemnites.Text = "Indemnités";
            //
            // flowLayoutIndemnites
            //
            this.flowLayoutIndemnites.AutoScroll = true;
            this.flowLayoutIndemnites.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutIndemnites.Location = new System.Drawing.Point(15, 30);
            this.flowLayoutIndemnites.Name = "flowLayoutIndemnites";
            this.flowLayoutIndemnites.Size = new System.Drawing.Size(810, 230);
            this.flowLayoutIndemnites.TabIndex = 0;
            this.flowLayoutIndemnites.WrapContents = false;
            //
            // buttonAjouterIndemnite
            //
            this.buttonAjouterIndemnite.Animated = true;
            this.buttonAjouterIndemnite.BorderRadius = 8;
            this.buttonAjouterIndemnite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAjouterIndemnite.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAjouterIndemnite.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAjouterIndemnite.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAjouterIndemnite.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAjouterIndemnite.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.buttonAjouterIndemnite.Font = new System.Drawing.Font("Montserrat", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonAjouterIndemnite.ForeColor = System.Drawing.Color.White;
            this.buttonAjouterIndemnite.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(179)))), ((int)(((byte)(113)))));
            this.buttonAjouterIndemnite.Location = new System.Drawing.Point(15, 270);
            this.buttonAjouterIndemnite.Name = "buttonAjouterIndemnite";
            this.buttonAjouterIndemnite.Size = new System.Drawing.Size(200, 40);
            this.buttonAjouterIndemnite.TabIndex = 1;
            this.buttonAjouterIndemnite.Text = "+ Ajouter";
            this.buttonAjouterIndemnite.Click += new System.EventHandler(this.buttonAjouterIndemnite_Click);
            //
            // groupBoxEmploye
            //
            this.groupBoxEmploye.Controls.Add(this.comboBoxEmploye);
            this.groupBoxEmploye.Controls.Add(this.textBoxRecherche);
            this.groupBoxEmploye.Controls.Add(this.labelEmploye);
            this.groupBoxEmploye.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxEmploye.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.groupBoxEmploye.Location = new System.Drawing.Point(30, 30);
            this.groupBoxEmploye.Name = "groupBoxEmploye";
            this.groupBoxEmploye.Padding = new System.Windows.Forms.Padding(20);
            this.groupBoxEmploye.Size = new System.Drawing.Size(840, 155);
            this.groupBoxEmploye.TabIndex = 0;
            this.groupBoxEmploye.TabStop = false;
            this.groupBoxEmploye.Text = "Sélection Employé";
            //
            // labelEmploye
            //
            this.labelEmploye.AutoSize = true;
            this.labelEmploye.Font = new System.Drawing.Font("Montserrat", 9F);
            this.labelEmploye.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.labelEmploye.Location = new System.Drawing.Point(23, 30);
            this.labelEmploye.Name = "labelEmploye";
            this.labelEmploye.Size = new System.Drawing.Size(150, 16);
            this.labelEmploye.TabIndex = 0;
            this.labelEmploye.Text = "Rechercher un employé";
            //
            // textBoxRecherche
            //
            this.textBoxRecherche.Animated = true;
            this.textBoxRecherche.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.textBoxRecherche.BorderRadius = 8;
            this.textBoxRecherche.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxRecherche.DefaultText = "";
            this.textBoxRecherche.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxRecherche.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxRecherche.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxRecherche.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxRecherche.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(112)))));
            this.textBoxRecherche.Font = new System.Drawing.Font("Montserrat", 9.5F);
            this.textBoxRecherche.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.textBoxRecherche.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxRecherche.Location = new System.Drawing.Point(23, 50);
            this.textBoxRecherche.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxRecherche.Name = "textBoxRecherche";
            this.textBoxRecherche.PasswordChar = '\0';
            this.textBoxRecherche.PlaceholderText = "Tapez le nom, prénom ou matricule...";
            this.textBoxRecherche.SelectedText = "";
            this.textBoxRecherche.Size = new System.Drawing.Size(794, 42);
            this.textBoxRecherche.TabIndex = 1;
            this.textBoxRecherche.TextChanged += new System.EventHandler(this.textBoxRecherche_TextChanged);
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
            this.comboBoxEmploye.Location = new System.Drawing.Point(23, 100);
            this.comboBoxEmploye.Name = "comboBoxEmploye";
            this.comboBoxEmploye.Size = new System.Drawing.Size(794, 42);
            this.comboBoxEmploye.TabIndex = 2;
            //
            // AjouterIndemniteFormV3
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 700);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AjouterIndemniteFormV3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ajouter Indemnités";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.groupBoxIndemnites.ResumeLayout(false);
            this.groupBoxEmploye.ResumeLayout(false);
            this.groupBoxEmploye.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.Panel panelFooter;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
        private Guna.UI2.WinForms.Guna2Button buttonValider;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.GroupBox groupBoxEmploye;
        private System.Windows.Forms.Label labelEmploye;
        private Guna.UI2.WinForms.Guna2TextBox textBoxRecherche;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxEmploye;
        private System.Windows.Forms.GroupBox groupBoxIndemnites;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutIndemnites;
        private Guna.UI2.WinForms.Guna2Button buttonAjouterIndemnite;
    }
}
