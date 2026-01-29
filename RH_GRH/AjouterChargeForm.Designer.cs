namespace RH_GRH
{
    partial class AjouterChargeForm
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
            this.buttonAjouter = new Guna.UI2.WinForms.Guna2Button();
            this.buttonValiderContinuer = new Guna.UI2.WinForms.Guna2Button();
            this.textBoxIdentification = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelIdentification = new System.Windows.Forms.Label();
            this.datePickerNaissance = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.labelDateNaissance = new System.Windows.Forms.Label();
            this.textBoxNomPrenom = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelNomPrenom = new System.Windows.Forms.Label();
            this.comboBoxScolarisation = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labelScolarisation = new System.Windows.Forms.Label();
            this.comboBoxType = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.comboBoxEmploye = new Guna.UI2.WinForms.Guna2ComboBox();
            this.textBoxRechercheEmploye = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelRechercheEmploye = new System.Windows.Forms.Label();
            this.labelEmploye = new System.Windows.Forms.Label();
            this.labelTitre = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMain.Controls.Add(this.buttonAnnuler);
            this.panelMain.Controls.Add(this.buttonAjouter);
            this.panelMain.Controls.Add(this.buttonValiderContinuer);
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
            this.panelMain.Controls.Add(this.textBoxRechercheEmploye);
            this.panelMain.Controls.Add(this.labelRechercheEmploye);
            this.panelMain.Controls.Add(this.labelEmploye);
            this.panelMain.Controls.Add(this.labelTitre);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(825, 560);
            this.panelMain.TabIndex = 0;
            // 
            // buttonAnnuler
            // 
            this.buttonAnnuler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAnnuler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAnnuler.FillColor = System.Drawing.Color.Gray;
            this.buttonAnnuler.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.White;
            this.buttonAnnuler.Location = new System.Drawing.Point(665, 490);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(130, 40);
            this.buttonAnnuler.TabIndex = 10;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            // 
            // buttonAjouter
            // 
            this.buttonAjouter.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAjouter.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAjouter.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAjouter.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAjouter.FillColor = System.Drawing.Color.MidnightBlue;
            this.buttonAjouter.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAjouter.ForeColor = System.Drawing.Color.White;
            this.buttonAjouter.Location = new System.Drawing.Point(525, 490);
            this.buttonAjouter.Name = "buttonAjouter";
            this.buttonAjouter.Size = new System.Drawing.Size(130, 40);
            this.buttonAjouter.TabIndex = 9;
            this.buttonAjouter.Text = "Ajouter";
            this.buttonAjouter.Click += new System.EventHandler(this.buttonValider_Click);
            // 
            // buttonValiderContinuer
            // 
            this.buttonValiderContinuer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonValiderContinuer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonValiderContinuer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonValiderContinuer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonValiderContinuer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.buttonValiderContinuer.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonValiderContinuer.ForeColor = System.Drawing.Color.White;
            this.buttonValiderContinuer.Location = new System.Drawing.Point(30, 490);
            this.buttonValiderContinuer.Name = "buttonValiderContinuer";
            this.buttonValiderContinuer.Size = new System.Drawing.Size(250, 40);
            this.buttonValiderContinuer.TabIndex = 8;
            this.buttonValiderContinuer.Text = "Ajouter et continuer";
            this.buttonValiderContinuer.Click += new System.EventHandler(this.buttonValiderContinuer_Click);
            // 
            // textBoxIdentification
            // 
            this.textBoxIdentification.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxIdentification.DefaultText = "";
            this.textBoxIdentification.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxIdentification.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxIdentification.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxIdentification.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxIdentification.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxIdentification.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxIdentification.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxIdentification.Location = new System.Drawing.Point(425, 425);
            this.textBoxIdentification.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textBoxIdentification.Name = "textBoxIdentification";
            this.textBoxIdentification.PlaceholderText = "Ex: BE123456";
            this.textBoxIdentification.SelectedText = "";
            this.textBoxIdentification.Size = new System.Drawing.Size(370, 36);
            this.textBoxIdentification.TabIndex = 7;
            // 
            // labelIdentification
            //
            this.labelIdentification.AutoSize = true;
            this.labelIdentification.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular);
            this.labelIdentification.Location = new System.Drawing.Point(425, 400);
            this.labelIdentification.Name = "labelIdentification";
            this.labelIdentification.Size = new System.Drawing.Size(189, 20);
            this.labelIdentification.TabIndex = 13;
            this.labelIdentification.Text = "N° Identification (CNI) :";
            // 
            // datePickerNaissance
            // 
            this.datePickerNaissance.Checked = true;
            this.datePickerNaissance.FillColor = System.Drawing.Color.White;
            this.datePickerNaissance.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.datePickerNaissance.Font = new System.Drawing.Font("Montserrat", 10F);
            this.datePickerNaissance.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerNaissance.Location = new System.Drawing.Point(30, 425);
            this.datePickerNaissance.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.datePickerNaissance.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.datePickerNaissance.Name = "datePickerNaissance";
            this.datePickerNaissance.Size = new System.Drawing.Size(370, 36);
            this.datePickerNaissance.TabIndex = 6;
            this.datePickerNaissance.Value = new System.DateTime(2025, 1, 8, 0, 0, 0, 0);
            // 
            // labelDateNaissance
            //
            this.labelDateNaissance.AutoSize = true;
            this.labelDateNaissance.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular);
            this.labelDateNaissance.Location = new System.Drawing.Point(30, 400);
            this.labelDateNaissance.Name = "labelDateNaissance";
            this.labelDateNaissance.Size = new System.Drawing.Size(162, 20);
            this.labelDateNaissance.TabIndex = 11;
            this.labelDateNaissance.Text = "Date de naissance :";
            // 
            // textBoxNomPrenom
            // 
            this.textBoxNomPrenom.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxNomPrenom.DefaultText = "";
            this.textBoxNomPrenom.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxNomPrenom.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxNomPrenom.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxNomPrenom.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxNomPrenom.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxNomPrenom.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxNomPrenom.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxNomPrenom.Location = new System.Drawing.Point(30, 345);
            this.textBoxNomPrenom.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textBoxNomPrenom.Name = "textBoxNomPrenom";
            this.textBoxNomPrenom.PlaceholderText = "Ex: DUPONT Marie";
            this.textBoxNomPrenom.SelectedText = "";
            this.textBoxNomPrenom.Size = new System.Drawing.Size(765, 36);
            this.textBoxNomPrenom.TabIndex = 5;
            // 
            // labelNomPrenom
            //
            this.labelNomPrenom.AutoSize = true;
            this.labelNomPrenom.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular);
            this.labelNomPrenom.Location = new System.Drawing.Point(30, 320);
            this.labelNomPrenom.Name = "labelNomPrenom";
            this.labelNomPrenom.Size = new System.Drawing.Size(124, 20);
            this.labelNomPrenom.TabIndex = 9;
            this.labelNomPrenom.Text = "Nom Prénom :";
            // 
            // comboBoxScolarisation
            // 
            this.comboBoxScolarisation.BackColor = System.Drawing.Color.Transparent;
            this.comboBoxScolarisation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxScolarisation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScolarisation.Enabled = false;
            this.comboBoxScolarisation.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBoxScolarisation.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBoxScolarisation.Font = new System.Drawing.Font("Montserrat", 10F);
            this.comboBoxScolarisation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comboBoxScolarisation.ItemHeight = 30;
            this.comboBoxScolarisation.Location = new System.Drawing.Point(425, 265);
            this.comboBoxScolarisation.Name = "comboBoxScolarisation";
            this.comboBoxScolarisation.Size = new System.Drawing.Size(370, 36);
            this.comboBoxScolarisation.TabIndex = 4;
            // 
            // labelScolarisation
            //
            this.labelScolarisation.AutoSize = true;
            this.labelScolarisation.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular);
            this.labelScolarisation.Location = new System.Drawing.Point(425, 240);
            this.labelScolarisation.Name = "labelScolarisation";
            this.labelScolarisation.Size = new System.Drawing.Size(118, 20);
            this.labelScolarisation.TabIndex = 7;
            this.labelScolarisation.Text = "Scolarisation :";
            // 
            // comboBoxType
            // 
            this.comboBoxType.BackColor = System.Drawing.Color.Transparent;
            this.comboBoxType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBoxType.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBoxType.Font = new System.Drawing.Font("Montserrat", 10F);
            this.comboBoxType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comboBoxType.ItemHeight = 30;
            this.comboBoxType.Location = new System.Drawing.Point(30, 265);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(370, 36);
            this.comboBoxType.TabIndex = 3;
            // 
            // labelType
            //
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular);
            this.labelType.Location = new System.Drawing.Point(30, 240);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(138, 20);
            this.labelType.TabIndex = 5;
            this.labelType.Text = "Type de charge :";
            //
            // textBoxRechercheEmploye
            //
            this.textBoxRechercheEmploye.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxRechercheEmploye.DefaultText = "";
            this.textBoxRechercheEmploye.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxRechercheEmploye.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxRechercheEmploye.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxRechercheEmploye.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxRechercheEmploye.Enabled = false;
            this.textBoxRechercheEmploye.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxRechercheEmploye.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxRechercheEmploye.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxRechercheEmploye.Location = new System.Drawing.Point(30, 103);
            this.textBoxRechercheEmploye.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxRechercheEmploye.Name = "textBoxRechercheEmploye";
            this.textBoxRechercheEmploye.PasswordChar = '\0';
            this.textBoxRechercheEmploye.PlaceholderText = "Tapez pour rechercher...";
            this.textBoxRechercheEmploye.SelectedText = "";
            this.textBoxRechercheEmploye.Size = new System.Drawing.Size(765, 36);
            this.textBoxRechercheEmploye.TabIndex = 1;
            //
            // labelRechercheEmploye
            //
            this.labelRechercheEmploye.AutoSize = true;
            this.labelRechercheEmploye.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Italic);
            this.labelRechercheEmploye.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelRechercheEmploye.Location = new System.Drawing.Point(30, 80);
            this.labelRechercheEmploye.Name = "labelRechercheEmploye";
            this.labelRechercheEmploye.Size = new System.Drawing.Size(285, 18);
            this.labelRechercheEmploye.TabIndex = 21;
            this.labelRechercheEmploye.Text = "Recherche rapide (nom, matricule ou entreprise)";
            //
            // comboBoxEmploye
            //
            this.comboBoxEmploye.BackColor = System.Drawing.Color.Transparent;
            this.comboBoxEmploye.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEmploye.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEmploye.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBoxEmploye.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBoxEmploye.Font = new System.Drawing.Font("Montserrat", 10F);
            this.comboBoxEmploye.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comboBoxEmploye.ItemHeight = 30;
            this.comboBoxEmploye.Location = new System.Drawing.Point(30, 183);
            this.comboBoxEmploye.Name = "comboBoxEmploye";
            this.comboBoxEmploye.Size = new System.Drawing.Size(765, 36);
            this.comboBoxEmploye.TabIndex = 2;
            //
            // labelEmploye
            //
            this.labelEmploye.AutoSize = true;
            this.labelEmploye.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular);
            this.labelEmploye.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.labelEmploye.Location = new System.Drawing.Point(30, 158);
            this.labelEmploye.Name = "labelEmploye";
            this.labelEmploye.Size = new System.Drawing.Size(198, 20);
            this.labelEmploye.TabIndex = 3;
            this.labelEmploye.Text = "Sélectionner l'employé :";
            //
            // labelTitre
            //
            this.labelTitre.BackColor = System.Drawing.Color.MidnightBlue;
            this.labelTitre.Font = new System.Drawing.Font("Montserrat", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitre.ForeColor = System.Drawing.Color.White;
            this.labelTitre.Location = new System.Drawing.Point(0, 0);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(825, 60);
            this.labelTitre.TabIndex = 0;
            this.labelTitre.Text = "Ajouter une Charge Familiale";
            this.labelTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AjouterChargeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 560);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AjouterChargeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ajouter une charge";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.Label labelEmploye;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxEmploye;
        private Guna.UI2.WinForms.Guna2TextBox textBoxRechercheEmploye;
        private System.Windows.Forms.Label labelRechercheEmploye;
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
        private Guna.UI2.WinForms.Guna2Button buttonValiderContinuer;
        private Guna.UI2.WinForms.Guna2Button buttonAjouter;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
    }
}
