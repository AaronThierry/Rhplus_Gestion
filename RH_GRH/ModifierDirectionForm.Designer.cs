namespace RH_GRH
{
    partial class ModifierDirectionForm
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
            this.labelNomDirection = new System.Windows.Forms.Label();
            this.textBoxNomDirection = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelEntreprise = new System.Windows.Forms.Label();
            this.comboBoxEntreprise = new Guna.UI2.WinForms.Guna2ComboBox();
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
            this.panelMain.Controls.Add(this.comboBoxEntreprise);
            this.panelMain.Controls.Add(this.labelEntreprise);
            this.panelMain.Controls.Add(this.textBoxNomDirection);
            this.panelMain.Controls.Add(this.labelNomDirection);
            this.panelMain.Controls.Add(this.labelTitre);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(500, 300);
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
            this.labelTitre.Text = "Modifier une Direction";
            this.labelTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // labelNomDirection
            //
            this.labelNomDirection.AutoSize = true;
            this.labelNomDirection.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular);
            this.labelNomDirection.Location = new System.Drawing.Point(30, 80);
            this.labelNomDirection.Name = "labelNomDirection";
            this.labelNomDirection.Size = new System.Drawing.Size(140, 20);
            this.labelNomDirection.TabIndex = 1;
            this.labelNomDirection.Text = "Nom de la direction :";
            //
            // textBoxNomDirection
            //
            this.textBoxNomDirection.BorderRadius = 0;
            this.textBoxNomDirection.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxNomDirection.DefaultText = "";
            this.textBoxNomDirection.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
            this.textBoxNomDirection.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
            this.textBoxNomDirection.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            this.textBoxNomDirection.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            this.textBoxNomDirection.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.textBoxNomDirection.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxNomDirection.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.textBoxNomDirection.Location = new System.Drawing.Point(30, 105);
            this.textBoxNomDirection.Name = "textBoxNomDirection";
            this.textBoxNomDirection.PasswordChar = '\0';
            this.textBoxNomDirection.PlaceholderText = "";
            this.textBoxNomDirection.SelectedText = "";
            this.textBoxNomDirection.Size = new System.Drawing.Size(440, 36);
            this.textBoxNomDirection.TabIndex = 2;
            //
            // labelEntreprise
            //
            this.labelEntreprise.AutoSize = true;
            this.labelEntreprise.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular);
            this.labelEntreprise.Location = new System.Drawing.Point(30, 145);
            this.labelEntreprise.Name = "labelEntreprise";
            this.labelEntreprise.Size = new System.Drawing.Size(87, 20);
            this.labelEntreprise.TabIndex = 3;
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
            this.comboBoxEntreprise.Location = new System.Drawing.Point(30, 170);
            this.comboBoxEntreprise.Name = "comboBoxEntreprise";
            this.comboBoxEntreprise.Size = new System.Drawing.Size(440, 36);
            this.comboBoxEntreprise.TabIndex = 4;
            //
            // buttonModifier
            //
            this.buttonModifier.BorderRadius = 0;
            this.buttonModifier.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonModifier.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonModifier.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            this.buttonModifier.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            this.buttonModifier.FillColor = System.Drawing.Color.FromArgb(255, 165, 0);
            this.buttonModifier.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonModifier.ForeColor = System.Drawing.Color.White;
            this.buttonModifier.Location = new System.Drawing.Point(250, 230);
            this.buttonModifier.Name = "buttonModifier";
            this.buttonModifier.Size = new System.Drawing.Size(110, 40);
            this.buttonModifier.TabIndex = 5;
            this.buttonModifier.Text = "Modifier";
            this.buttonModifier.Click += new System.EventHandler(this.buttonModifier_Click);
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
            this.buttonAnnuler.Location = new System.Drawing.Point(370, 230);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(100, 40);
            this.buttonAnnuler.TabIndex = 6;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            //
            // ModifierDirectionForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModifierDirectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modifier une Direction";
            this.Load += new System.EventHandler(this.ModifierDirectionForm_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.Label labelNomDirection;
        private Guna.UI2.WinForms.Guna2TextBox textBoxNomDirection;
        private System.Windows.Forms.Label labelEntreprise;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxEntreprise;
        private Guna.UI2.WinForms.Guna2Button buttonModifier;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
    }
}
