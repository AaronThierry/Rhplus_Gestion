namespace RH_GRH
{
    partial class AjouterCategorieForm
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
            this.labelTitre = new System.Windows.Forms.Label();
            this.labelNomCategorie = new System.Windows.Forms.Label();
            this.textBoxNomCategorie = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelMontant = new System.Windows.Forms.Label();
            this.textBoxMontant = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelEntreprise = new System.Windows.Forms.Label();
            this.comboBoxEntreprise = new Guna.UI2.WinForms.Guna2ComboBox();
            this.buttonAjouter = new Guna.UI2.WinForms.Guna2Button();
            this.buttonAnnuler = new Guna.UI2.WinForms.Guna2Button();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            //
            // panelMain
            //
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.panelMain.Controls.Add(this.buttonAnnuler);
            this.panelMain.Controls.Add(this.buttonAjouter);
            this.panelMain.Controls.Add(this.comboBoxEntreprise);
            this.panelMain.Controls.Add(this.labelEntreprise);
            this.panelMain.Controls.Add(this.textBoxMontant);
            this.panelMain.Controls.Add(this.labelMontant);
            this.panelMain.Controls.Add(this.textBoxNomCategorie);
            this.panelMain.Controls.Add(this.labelNomCategorie);
            this.panelMain.Controls.Add(this.labelTitre);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(500, 365);
            this.panelMain.TabIndex = 0;
            //
            // labelTitre
            //
            this.labelTitre.AutoSize = false;
            this.labelTitre.BackColor = System.Drawing.Color.MidnightBlue;
            this.labelTitre.Font = new System.Drawing.Font("Montserrat", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitre.ForeColor = System.Drawing.Color.White;
            this.labelTitre.Location = new System.Drawing.Point(0, 0);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(500, 60);
            this.labelTitre.TabIndex = 0;
            this.labelTitre.Text = "Ajouter une Catégorie";
            this.labelTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // labelNomCategorie
            //
            this.labelNomCategorie.AutoSize = true;
            this.labelNomCategorie.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular);
            this.labelNomCategorie.Location = new System.Drawing.Point(30, 80);
            this.labelNomCategorie.Name = "labelNomCategorie";
            this.labelNomCategorie.Size = new System.Drawing.Size(170, 20);
            this.labelNomCategorie.TabIndex = 1;
            this.labelNomCategorie.Text = "Nom de la catégorie :";
            //
            // textBoxNomCategorie
            //
            this.textBoxNomCategorie.BorderRadius = 0;
            this.textBoxNomCategorie.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxNomCategorie.DefaultText = "";
            this.textBoxNomCategorie.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
            this.textBoxNomCategorie.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
            this.textBoxNomCategorie.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            this.textBoxNomCategorie.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            this.textBoxNomCategorie.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.textBoxNomCategorie.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxNomCategorie.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.textBoxNomCategorie.Location = new System.Drawing.Point(30, 105);
            this.textBoxNomCategorie.Name = "textBoxNomCategorie";
            this.textBoxNomCategorie.PasswordChar = '\0';
            this.textBoxNomCategorie.PlaceholderText = "";
            this.textBoxNomCategorie.SelectedText = "";
            this.textBoxNomCategorie.Size = new System.Drawing.Size(440, 36);
            this.textBoxNomCategorie.TabIndex = 2;
            //
            // labelMontant
            //
            this.labelMontant.AutoSize = true;
            this.labelMontant.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular);
            this.labelMontant.Location = new System.Drawing.Point(30, 145);
            this.labelMontant.Name = "labelMontant";
            this.labelMontant.Size = new System.Drawing.Size(87, 20);
            this.labelMontant.TabIndex = 3;
            this.labelMontant.Text = "Montant :";
            //
            // textBoxMontant
            //
            this.textBoxMontant.BorderRadius = 0;
            this.textBoxMontant.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxMontant.DefaultText = "";
            this.textBoxMontant.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
            this.textBoxMontant.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
            this.textBoxMontant.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            this.textBoxMontant.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            this.textBoxMontant.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.textBoxMontant.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxMontant.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.textBoxMontant.Location = new System.Drawing.Point(30, 170);
            this.textBoxMontant.Name = "textBoxMontant";
            this.textBoxMontant.PasswordChar = '\0';
            this.textBoxMontant.PlaceholderText = "";
            this.textBoxMontant.SelectedText = "";
            this.textBoxMontant.Size = new System.Drawing.Size(440, 36);
            this.textBoxMontant.TabIndex = 4;
            //
            // labelEntreprise
            //
            this.labelEntreprise.AutoSize = true;
            this.labelEntreprise.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular);
            this.labelEntreprise.Location = new System.Drawing.Point(30, 210);
            this.labelEntreprise.Name = "labelEntreprise";
            this.labelEntreprise.Size = new System.Drawing.Size(87, 20);
            this.labelEntreprise.TabIndex = 5;
            this.labelEntreprise.Text = "Entreprise :";
            //
            // comboBoxEntreprise
            //
            this.comboBoxEntreprise.BackColor = System.Drawing.Color.Transparent;
            this.comboBoxEntreprise.BorderRadius = 0;
            this.comboBoxEntreprise.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEntreprise.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEntreprise.FocusedColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.comboBoxEntreprise.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.comboBoxEntreprise.Font = new System.Drawing.Font("Montserrat", 10F);
            this.comboBoxEntreprise.ForeColor = System.Drawing.Color.FromArgb(68, 88, 112);
            this.comboBoxEntreprise.ItemHeight = 30;
            this.comboBoxEntreprise.Location = new System.Drawing.Point(30, 235);
            this.comboBoxEntreprise.Name = "comboBoxEntreprise";
            this.comboBoxEntreprise.Size = new System.Drawing.Size(440, 36);
            this.comboBoxEntreprise.TabIndex = 6;
            //
            // buttonAjouter
            //
            this.buttonAjouter.BorderRadius = 0;
            this.buttonAjouter.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAjouter.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAjouter.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            this.buttonAjouter.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            this.buttonAjouter.FillColor = System.Drawing.Color.MidnightBlue;
            this.buttonAjouter.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAjouter.ForeColor = System.Drawing.Color.White;
            this.buttonAjouter.Location = new System.Drawing.Point(250, 295);
            this.buttonAjouter.Name = "buttonAjouter";
            this.buttonAjouter.Size = new System.Drawing.Size(110, 40);
            this.buttonAjouter.TabIndex = 7;
            this.buttonAjouter.Text = "Ajouter";
            this.buttonAjouter.Click += new System.EventHandler(this.buttonAjouter_Click);
            //
            // buttonAnnuler
            //
            this.buttonAnnuler.BorderRadius = 0;
            this.buttonAnnuler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            this.buttonAnnuler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            this.buttonAnnuler.FillColor = System.Drawing.Color.Gray;
            this.buttonAnnuler.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.White;
            this.buttonAnnuler.Location = new System.Drawing.Point(370, 295);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(100, 40);
            this.buttonAnnuler.TabIndex = 8;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            //
            // AjouterCategorieForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 365);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AjouterCategorieForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ajouter une Catégorie";
            this.Load += new System.EventHandler(this.AjouterCategorieForm_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.Label labelNomCategorie;
        private Guna.UI2.WinForms.Guna2TextBox textBoxNomCategorie;
        private System.Windows.Forms.Label labelMontant;
        private Guna.UI2.WinForms.Guna2TextBox textBoxMontant;
        private System.Windows.Forms.Label labelEntreprise;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxEntreprise;
        private Guna.UI2.WinForms.Guna2Button buttonAjouter;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
    }
}
