namespace RH_GRH
{
    partial class ModifierIndemniteForm
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
            this.buttonAnnuler = new Guna.UI2.WinForms.Guna2Button();
            this.buttonValider = new Guna.UI2.WinForms.Guna2Button();
            this.textBoxMontant = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelMontant = new System.Windows.Forms.Label();
            this.textBoxType = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelType = new System.Windows.Forms.Label();
            this.textBoxEmploye = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelEmploye = new System.Windows.Forms.Label();
            this.comboBoxEntreprise = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labelEntreprise = new System.Windows.Forms.Label();
            this.labelTitre = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            //
            // panelMain
            //
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMain.Controls.Add(this.buttonAnnuler);
            this.panelMain.Controls.Add(this.buttonValider);
            this.panelMain.Controls.Add(this.textBoxMontant);
            this.panelMain.Controls.Add(this.labelMontant);
            this.panelMain.Controls.Add(this.textBoxType);
            this.panelMain.Controls.Add(this.labelType);
            this.panelMain.Controls.Add(this.textBoxEmploye);
            this.panelMain.Controls.Add(this.labelEmploye);
            this.panelMain.Controls.Add(this.comboBoxEntreprise);
            this.panelMain.Controls.Add(this.labelEntreprise);
            this.panelMain.Controls.Add(this.labelTitre);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(700, 500);
            this.panelMain.TabIndex = 0;
            //
            // buttonAnnuler
            //
            this.buttonAnnuler.Animated = true;
            this.buttonAnnuler.BorderRadius = 0;
            this.buttonAnnuler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAnnuler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAnnuler.FillColor = System.Drawing.Color.Gray;
            this.buttonAnnuler.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.White;
            this.buttonAnnuler.Location = new System.Drawing.Point(340, 420);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(170, 45);
            this.buttonAnnuler.TabIndex = 10;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            //
            // buttonValider
            //
            this.buttonValider.Animated = true;
            this.buttonValider.BorderRadius = 0;
            this.buttonValider.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonValider.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonValider.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonValider.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonValider.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(0)))));
            this.buttonValider.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonValider.ForeColor = System.Drawing.Color.White;
            this.buttonValider.Location = new System.Drawing.Point(520, 420);
            this.buttonValider.Name = "buttonValider";
            this.buttonValider.Size = new System.Drawing.Size(170, 45);
            this.buttonValider.TabIndex = 9;
            this.buttonValider.Text = "Valider";
            this.buttonValider.Click += new System.EventHandler(this.buttonValider_Click);
            //
            // textBoxMontant
            //
            this.textBoxMontant.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxMontant.DefaultText = "";
            this.textBoxMontant.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxMontant.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxMontant.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxMontant.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxMontant.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxMontant.Font = new System.Drawing.Font("Montserrat", 12F);
            this.textBoxMontant.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxMontant.Location = new System.Drawing.Point(30, 343);
            this.textBoxMontant.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textBoxMontant.Name = "textBoxMontant";
            this.textBoxMontant.PlaceholderText = "Ex: 50000";
            this.textBoxMontant.SelectedText = "";
            this.textBoxMontant.Size = new System.Drawing.Size(640, 46);
            this.textBoxMontant.TabIndex = 8;
            //
            // labelMontant
            //
            this.labelMontant.AutoSize = true;
            this.labelMontant.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold);
            this.labelMontant.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.labelMontant.Location = new System.Drawing.Point(30, 305);
            this.labelMontant.Name = "labelMontant";
            this.labelMontant.Size = new System.Drawing.Size(230, 31);
            this.labelMontant.TabIndex = 7;
            this.labelMontant.Text = "Montant (modifiable) :";
            //
            // textBoxType
            //
            this.textBoxType.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxType.DefaultText = "";
            this.textBoxType.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxType.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxType.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxType.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxType.Enabled = false;
            this.textBoxType.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxType.Font = new System.Drawing.Font("Montserrat", 12F);
            this.textBoxType.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxType.Location = new System.Drawing.Point(30, 238);
            this.textBoxType.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.ReadOnly = true;
            this.textBoxType.SelectedText = "";
            this.textBoxType.Size = new System.Drawing.Size(640, 46);
            this.textBoxType.TabIndex = 6;
            //
            // labelType
            //
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Montserrat", 12F);
            this.labelType.Location = new System.Drawing.Point(30, 200);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(189, 31);
            this.labelType.TabIndex = 5;
            this.labelType.Text = "Type d\'indemnité :";
            //
            // textBoxEmploye
            //
            this.textBoxEmploye.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxEmploye.DefaultText = "";
            this.textBoxEmploye.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxEmploye.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxEmploye.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxEmploye.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxEmploye.Enabled = false;
            this.textBoxEmploye.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxEmploye.Font = new System.Drawing.Font("Montserrat", 12F);
            this.textBoxEmploye.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxEmploye.Location = new System.Drawing.Point(360, 133);
            this.textBoxEmploye.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textBoxEmploye.Name = "textBoxEmploye";
            this.textBoxEmploye.ReadOnly = true;
            this.textBoxEmploye.SelectedText = "";
            this.textBoxEmploye.Size = new System.Drawing.Size(310, 46);
            this.textBoxEmploye.TabIndex = 4;
            //
            // labelEmploye
            //
            this.labelEmploye.AutoSize = true;
            this.labelEmploye.Font = new System.Drawing.Font("Montserrat", 12F);
            this.labelEmploye.Location = new System.Drawing.Point(360, 95);
            this.labelEmploye.Name = "labelEmploye";
            this.labelEmploye.Size = new System.Drawing.Size(112, 31);
            this.labelEmploye.TabIndex = 3;
            this.labelEmploye.Text = "Employé :";
            //
            // comboBoxEntreprise
            //
            this.comboBoxEntreprise.BackColor = System.Drawing.Color.Transparent;
            this.comboBoxEntreprise.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEntreprise.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEntreprise.Enabled = false;
            this.comboBoxEntreprise.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBoxEntreprise.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBoxEntreprise.Font = new System.Drawing.Font("Montserrat", 12F);
            this.comboBoxEntreprise.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comboBoxEntreprise.ItemHeight = 40;
            this.comboBoxEntreprise.Location = new System.Drawing.Point(30, 133);
            this.comboBoxEntreprise.Name = "comboBoxEntreprise";
            this.comboBoxEntreprise.Size = new System.Drawing.Size(310, 46);
            this.comboBoxEntreprise.TabIndex = 2;
            //
            // labelEntreprise
            //
            this.labelEntreprise.AutoSize = true;
            this.labelEntreprise.Font = new System.Drawing.Font("Montserrat", 12F);
            this.labelEntreprise.Location = new System.Drawing.Point(30, 95);
            this.labelEntreprise.Name = "labelEntreprise";
            this.labelEntreprise.Size = new System.Drawing.Size(127, 31);
            this.labelEntreprise.TabIndex = 1;
            this.labelEntreprise.Text = "Entreprise :";
            //
            // labelTitre
            //
            this.labelTitre.BackColor = System.Drawing.Color.MidnightBlue;
            this.labelTitre.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitre.ForeColor = System.Drawing.Color.White;
            this.labelTitre.Location = new System.Drawing.Point(0, 0);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(700, 70);
            this.labelTitre.TabIndex = 0;
            this.labelTitre.Text = "Modifier une Indemnité";
            this.labelTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // ModifierIndemniteForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModifierIndemniteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modifier une indemnité";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.Label labelEntreprise;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxEntreprise;
        private System.Windows.Forms.Label labelEmploye;
        private Guna.UI2.WinForms.Guna2TextBox textBoxEmploye;
        private System.Windows.Forms.Label labelType;
        private Guna.UI2.WinForms.Guna2TextBox textBoxType;
        private System.Windows.Forms.Label labelMontant;
        private Guna.UI2.WinForms.Guna2TextBox textBoxMontant;
        private Guna.UI2.WinForms.Guna2Button buttonValider;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
    }
}
