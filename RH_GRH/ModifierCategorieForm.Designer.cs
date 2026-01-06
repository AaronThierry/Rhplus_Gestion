namespace RH_GRH
{
    partial class ModifierCategorieForm
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
            this.textBoxNomCategorie = new System.Windows.Forms.TextBox();
            this.labelMontant = new System.Windows.Forms.Label();
            this.textBoxMontant = new System.Windows.Forms.TextBox();
            this.labelEntreprise = new System.Windows.Forms.Label();
            this.comboBoxEntreprise = new System.Windows.Forms.ComboBox();
            this.buttonModifier = new System.Windows.Forms.Button();
            this.buttonAnnuler = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            //
            // panelMain
            //
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.buttonAnnuler);
            this.panelMain.Controls.Add(this.buttonModifier);
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
            this.panelMain.Size = new System.Drawing.Size(500, 400);
            this.panelMain.TabIndex = 0;
            //
            // labelTitre
            //
            this.labelTitre.AutoSize = false;
            this.labelTitre.BackColor = System.Drawing.Color.FromArgb(255, 165, 0);
            this.labelTitre.Font = new System.Drawing.Font("Montserrat", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitre.ForeColor = System.Drawing.Color.White;
            this.labelTitre.Location = new System.Drawing.Point(0, 0);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(500, 60);
            this.labelTitre.TabIndex = 0;
            this.labelTitre.Text = "Modifier une Catégorie";
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
            this.textBoxNomCategorie.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxNomCategorie.Location = new System.Drawing.Point(30, 105);
            this.textBoxNomCategorie.Name = "textBoxNomCategorie";
            this.textBoxNomCategorie.Size = new System.Drawing.Size(440, 24);
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
            this.textBoxMontant.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxMontant.Location = new System.Drawing.Point(30, 170);
            this.textBoxMontant.Name = "textBoxMontant";
            this.textBoxMontant.Size = new System.Drawing.Size(440, 24);
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
            this.comboBoxEntreprise.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEntreprise.Font = new System.Drawing.Font("Montserrat", 10F);
            this.comboBoxEntreprise.FormattingEnabled = true;
            this.comboBoxEntreprise.Location = new System.Drawing.Point(30, 235);
            this.comboBoxEntreprise.Name = "comboBoxEntreprise";
            this.comboBoxEntreprise.Size = new System.Drawing.Size(440, 26);
            this.comboBoxEntreprise.TabIndex = 6;
            //
            // buttonModifier
            //
            this.buttonModifier.BackColor = System.Drawing.Color.FromArgb(255, 165, 0);
            this.buttonModifier.FlatAppearance.BorderSize = 0;
            this.buttonModifier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonModifier.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonModifier.ForeColor = System.Drawing.Color.White;
            this.buttonModifier.Location = new System.Drawing.Point(250, 295);
            this.buttonModifier.Name = "buttonModifier";
            this.buttonModifier.Size = new System.Drawing.Size(110, 40);
            this.buttonModifier.TabIndex = 7;
            this.buttonModifier.Text = "Modifier";
            this.buttonModifier.UseVisualStyleBackColor = false;
            this.buttonModifier.Click += new System.EventHandler(this.buttonModifier_Click);
            //
            // buttonAnnuler
            //
            this.buttonAnnuler.BackColor = System.Drawing.Color.Gray;
            this.buttonAnnuler.FlatAppearance.BorderSize = 0;
            this.buttonAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAnnuler.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.White;
            this.buttonAnnuler.Location = new System.Drawing.Point(370, 295);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(100, 40);
            this.buttonAnnuler.TabIndex = 8;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.UseVisualStyleBackColor = false;
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            //
            // ModifierCategorieForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModifierCategorieForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modifier une Catégorie";
            this.Load += new System.EventHandler(this.ModifierCategorieForm_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.Label labelNomCategorie;
        private System.Windows.Forms.TextBox textBoxNomCategorie;
        private System.Windows.Forms.Label labelMontant;
        private System.Windows.Forms.TextBox textBoxMontant;
        private System.Windows.Forms.Label labelEntreprise;
        private System.Windows.Forms.ComboBox comboBoxEntreprise;
        private System.Windows.Forms.Button buttonModifier;
        private System.Windows.Forms.Button buttonAnnuler;
    }
}
