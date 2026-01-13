namespace RH_GRH
{
    partial class ModifierChargeForm
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
            this.labelEntreprise = new System.Windows.Forms.Label();
            this.comboBoxEntreprise = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labelEmploye = new System.Windows.Forms.Label();
            this.comboBoxEmploye = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.comboBoxType = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labelScolarisation = new System.Windows.Forms.Label();
            this.comboBoxScolarisation = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labelNomPrenom = new System.Windows.Forms.Label();
            this.textBoxNomPrenom = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelDateNaissance = new System.Windows.Forms.Label();
            this.datePickerNaissance = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.labelIdentification = new System.Windows.Forms.Label();
            this.textBoxIdentification = new Guna.UI2.WinForms.Guna2TextBox();
            this.buttonModifier = new Guna.UI2.WinForms.Guna2Button();
            this.buttonAnnuler = new Guna.UI2.WinForms.Guna2Button();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            //
            // panelMain
            //
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.panelMain.Controls.Add(this.buttonAnnuler);
            this.panelMain.Controls.Add(this.buttonModifier);
            this.panelMain.Controls.Add(this.textBoxIdentification);
            this.panelMain.Controls.Add(this.labelIdentification);
            this.panelMain.Controls.Add(this.datePickerNaissance);
            this.panelMain.Controls.Add(this.labelDateNaissance);
            this.panelMain.Controls.Add(this.textBoxNomPrenom);
            this.panelMain.Controls.Add(this.labelNomPrenom);
            this.panelMain.Controls.Add(this.comboBoxScolarisation);
            this.panelMain.Controls.Add(this.labelScolarisation);
            this.panelMain.Controls.Add(this.comboBoxType);
            this.panelMain.Controls.Add(this.labelType);
            this.panelMain.Controls.Add(this.comboBoxEmploye);
            this.panelMain.Controls.Add(this.labelEmploye);
            this.panelMain.Controls.Add(this.comboBoxEntreprise);
            this.panelMain.Controls.Add(this.labelEntreprise);
            this.panelMain.Controls.Add(this.labelTitre);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(825, 780);
            this.panelMain.TabIndex = 0;
            //
            // labelTitre
            //
            this.labelTitre.AutoSize = false;
            this.labelTitre.BackColor = System.Drawing.Color.MidnightBlue;
            this.labelTitre.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Bold);
            this.labelTitre.ForeColor = System.Drawing.Color.White;
            this.labelTitre.Location = new System.Drawing.Point(0, 0);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(825, 90);
            this.labelTitre.TabIndex = 0;
            this.labelTitre.Text = "Modifier une Charge";
            this.labelTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // labelEntreprise
            //
            this.labelEntreprise.AutoSize = true;
            this.labelEntreprise.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular);
            this.labelEntreprise.Location = new System.Drawing.Point(45, 120);
            this.labelEntreprise.Name = "labelEntreprise";
            this.labelEntreprise.Size = new System.Drawing.Size(110, 24);
            this.labelEntreprise.TabIndex = 1;
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
            this.comboBoxEntreprise.Font = new System.Drawing.Font("Montserrat", 12F);
            this.comboBoxEntreprise.ForeColor = System.Drawing.Color.FromArgb(68, 88, 112);
            this.comboBoxEntreprise.ItemHeight = 40;
            this.comboBoxEntreprise.Location = new System.Drawing.Point(45, 158);
            this.comboBoxEntreprise.Name = "comboBoxEntreprise";
            this.comboBoxEntreprise.Size = new System.Drawing.Size(735, 46);
            this.comboBoxEntreprise.TabIndex = 1;
            //
            // labelEmploye
            //
            this.labelEmploye.AutoSize = true;
            this.labelEmploye.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular);
            this.labelEmploye.Location = new System.Drawing.Point(45, 225);
            this.labelEmploye.Name = "labelEmploye";
            this.labelEmploye.Size = new System.Drawing.Size(92, 24);
            this.labelEmploye.TabIndex = 3;
            this.labelEmploye.Text = "Employé :";
            //
            // comboBoxEmploye
            //
            this.comboBoxEmploye.BackColor = System.Drawing.Color.Transparent;
            this.comboBoxEmploye.BorderRadius = 0;
            this.comboBoxEmploye.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEmploye.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEmploye.FocusedColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.comboBoxEmploye.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.comboBoxEmploye.Font = new System.Drawing.Font("Montserrat", 12F);
            this.comboBoxEmploye.ForeColor = System.Drawing.Color.FromArgb(68, 88, 112);
            this.comboBoxEmploye.ItemHeight = 40;
            this.comboBoxEmploye.Location = new System.Drawing.Point(45, 263);
            this.comboBoxEmploye.Name = "comboBoxEmploye";
            this.comboBoxEmploye.Size = new System.Drawing.Size(735, 46);
            this.comboBoxEmploye.TabIndex = 2;
            //
            // labelType
            //
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular);
            this.labelType.Location = new System.Drawing.Point(45, 330);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(150, 24);
            this.labelType.TabIndex = 5;
            this.labelType.Text = "Type de charge :";
            //
            // comboBoxType
            //
            this.comboBoxType.BackColor = System.Drawing.Color.Transparent;
            this.comboBoxType.BorderRadius = 0;
            this.comboBoxType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FocusedColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.comboBoxType.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.comboBoxType.Font = new System.Drawing.Font("Montserrat", 12F);
            this.comboBoxType.ForeColor = System.Drawing.Color.FromArgb(68, 88, 112);
            this.comboBoxType.ItemHeight = 40;
            this.comboBoxType.Location = new System.Drawing.Point(45, 368);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(353, 46);
            this.comboBoxType.TabIndex = 3;
            //
            // labelScolarisation
            //
            this.labelScolarisation.AutoSize = true;
            this.labelScolarisation.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular);
            this.labelScolarisation.Location = new System.Drawing.Point(428, 330);
            this.labelScolarisation.Name = "labelScolarisation";
            this.labelScolarisation.Size = new System.Drawing.Size(129, 24);
            this.labelScolarisation.TabIndex = 7;
            this.labelScolarisation.Text = "Scolarisation :";
            //
            // comboBoxScolarisation
            //
            this.comboBoxScolarisation.BackColor = System.Drawing.Color.Transparent;
            this.comboBoxScolarisation.BorderRadius = 0;
            this.comboBoxScolarisation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxScolarisation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScolarisation.Enabled = false;
            this.comboBoxScolarisation.FocusedColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.comboBoxScolarisation.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.comboBoxScolarisation.Font = new System.Drawing.Font("Montserrat", 12F);
            this.comboBoxScolarisation.ForeColor = System.Drawing.Color.FromArgb(68, 88, 112);
            this.comboBoxScolarisation.ItemHeight = 40;
            this.comboBoxScolarisation.Location = new System.Drawing.Point(428, 368);
            this.comboBoxScolarisation.Name = "comboBoxScolarisation";
            this.comboBoxScolarisation.Size = new System.Drawing.Size(353, 46);
            this.comboBoxScolarisation.TabIndex = 4;
            //
            // labelNomPrenom
            //
            this.labelNomPrenom.AutoSize = true;
            this.labelNomPrenom.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular);
            this.labelNomPrenom.Location = new System.Drawing.Point(45, 435);
            this.labelNomPrenom.Name = "labelNomPrenom";
            this.labelNomPrenom.Size = new System.Drawing.Size(137, 24);
            this.labelNomPrenom.TabIndex = 9;
            this.labelNomPrenom.Text = "Nom Prénom :";
            //
            // textBoxNomPrenom
            //
            this.textBoxNomPrenom.BorderRadius = 0;
            this.textBoxNomPrenom.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxNomPrenom.DefaultText = "";
            this.textBoxNomPrenom.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
            this.textBoxNomPrenom.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
            this.textBoxNomPrenom.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            this.textBoxNomPrenom.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            this.textBoxNomPrenom.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.textBoxNomPrenom.Font = new System.Drawing.Font("Montserrat", 12F);
            this.textBoxNomPrenom.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.textBoxNomPrenom.Location = new System.Drawing.Point(45, 473);
            this.textBoxNomPrenom.Name = "textBoxNomPrenom";
            this.textBoxNomPrenom.PasswordChar = '\0';
            this.textBoxNomPrenom.PlaceholderText = "Ex: DUPONT Marie";
            this.textBoxNomPrenom.SelectedText = "";
            this.textBoxNomPrenom.Size = new System.Drawing.Size(735, 46);
            this.textBoxNomPrenom.TabIndex = 5;
            //
            // labelDateNaissance
            //
            this.labelDateNaissance.AutoSize = true;
            this.labelDateNaissance.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular);
            this.labelDateNaissance.Location = new System.Drawing.Point(45, 540);
            this.labelDateNaissance.Name = "labelDateNaissance";
            this.labelDateNaissance.Size = new System.Drawing.Size(183, 24);
            this.labelDateNaissance.TabIndex = 11;
            this.labelDateNaissance.Text = "Date de naissance :";
            //
            // datePickerNaissance
            //
            this.datePickerNaissance.BorderRadius = 0;
            this.datePickerNaissance.Checked = true;
            this.datePickerNaissance.FillColor = System.Drawing.Color.White;
            this.datePickerNaissance.FocusedColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.datePickerNaissance.Font = new System.Drawing.Font("Montserrat", 12F);
            this.datePickerNaissance.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerNaissance.Location = new System.Drawing.Point(45, 578);
            this.datePickerNaissance.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.datePickerNaissance.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.datePickerNaissance.Name = "datePickerNaissance";
            this.datePickerNaissance.Size = new System.Drawing.Size(353, 46);
            this.datePickerNaissance.TabIndex = 6;
            this.datePickerNaissance.Value = new System.DateTime(2025, 1, 8, 0, 0, 0, 0);
            //
            // labelIdentification
            //
            this.labelIdentification.AutoSize = true;
            this.labelIdentification.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular);
            this.labelIdentification.Location = new System.Drawing.Point(428, 540);
            this.labelIdentification.Name = "labelIdentification";
            this.labelIdentification.Size = new System.Drawing.Size(215, 24);
            this.labelIdentification.TabIndex = 13;
            this.labelIdentification.Text = "N° Identification (CNI) :";
            //
            // textBoxIdentification
            //
            this.textBoxIdentification.BorderRadius = 0;
            this.textBoxIdentification.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxIdentification.DefaultText = "";
            this.textBoxIdentification.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
            this.textBoxIdentification.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
            this.textBoxIdentification.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            this.textBoxIdentification.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            this.textBoxIdentification.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.textBoxIdentification.Font = new System.Drawing.Font("Montserrat", 12F);
            this.textBoxIdentification.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.textBoxIdentification.Location = new System.Drawing.Point(428, 578);
            this.textBoxIdentification.Name = "textBoxIdentification";
            this.textBoxIdentification.PasswordChar = '\0';
            this.textBoxIdentification.PlaceholderText = "Ex: BE123456";
            this.textBoxIdentification.SelectedText = "";
            this.textBoxIdentification.Size = new System.Drawing.Size(353, 46);
            this.textBoxIdentification.TabIndex = 7;
            //
            // buttonModifier
            //
            this.buttonModifier.BorderRadius = 0;
            this.buttonModifier.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonModifier.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonModifier.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            this.buttonModifier.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            this.buttonModifier.FillColor = System.Drawing.Color.MidnightBlue;
            this.buttonModifier.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold);
            this.buttonModifier.ForeColor = System.Drawing.Color.White;
            this.buttonModifier.Location = new System.Drawing.Point(465, 675);
            this.buttonModifier.Name = "buttonModifier";
            this.buttonModifier.Size = new System.Drawing.Size(180, 55);
            this.buttonModifier.TabIndex = 8;
            this.buttonModifier.Text = "Modifier";
            this.buttonModifier.Click += new System.EventHandler(this.buttonValider_Click);
            //
            // buttonAnnuler
            //
            this.buttonAnnuler.BorderRadius = 0;
            this.buttonAnnuler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            this.buttonAnnuler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            this.buttonAnnuler.FillColor = System.Drawing.Color.Gray;
            this.buttonAnnuler.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.White;
            this.buttonAnnuler.Location = new System.Drawing.Point(660, 675);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(120, 55);
            this.buttonAnnuler.TabIndex = 9;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            //
            // ModifierChargeForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 780);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModifierChargeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modifier une charge";
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
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxEmploye;
        private System.Windows.Forms.Label labelType;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxType;
        private System.Windows.Forms.Label labelScolarisation;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxScolarisation;
        private System.Windows.Forms.Label labelNomPrenom;
        private Guna.UI2.WinForms.Guna2TextBox textBoxNomPrenom;
        private System.Windows.Forms.Label labelDateNaissance;
        private Guna.UI2.WinForms.Guna2DateTimePicker datePickerNaissance;
        private System.Windows.Forms.Label labelIdentification;
        private Guna.UI2.WinForms.Guna2TextBox textBoxIdentification;
        private Guna.UI2.WinForms.Guna2Button buttonModifier;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
    }
}
