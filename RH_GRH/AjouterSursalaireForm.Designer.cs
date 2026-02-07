namespace RH_GRH
{
    partial class AjouterSursalaireForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelRecherche = new System.Windows.Forms.Label();
            this.textBoxRecherche = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxEmploye = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNom = new Guna.UI2.WinForms.Guna2TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDescription = new Guna.UI2.WinForms.Guna2TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxMontant = new Guna.UI2.WinForms.Guna2TextBox();
            this.buttonEnregistrer = new Guna.UI2.WinForms.Guna2Button();
            this.buttonAnnuler = new Guna.UI2.WinForms.Guna2Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            //
            // panel1
            //
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 70);
            this.panel1.TabIndex = 0;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 14F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(20, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ajouter Sursalaire";
            //
            // panel2
            //
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.buttonAnnuler);
            this.panel2.Controls.Add(this.buttonEnregistrer);
            this.panel2.Controls.Add(this.textBoxMontant);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.textBoxDescription);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.textBoxNom);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.comboBoxEmploye);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.textBoxRecherche);
            this.panel2.Controls.Add(this.labelRecherche);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 70);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(30);
            this.panel2.Size = new System.Drawing.Size(600, 630);
            this.panel2.TabIndex = 1;
            //
            // labelRecherche
            //
            this.labelRecherche.AutoSize = true;
            this.labelRecherche.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.labelRecherche.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelRecherche.Location = new System.Drawing.Point(30, 30);
            this.labelRecherche.Name = "labelRecherche";
            this.labelRecherche.Size = new System.Drawing.Size(210, 24);
            this.labelRecherche.TabIndex = 0;
            this.labelRecherche.Text = "Rechercher un employé:";
            //
            // textBoxRecherche
            //
            this.textBoxRecherche.BorderRadius = 8;
            this.textBoxRecherche.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxRecherche.DefaultText = "";
            this.textBoxRecherche.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxRecherche.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxRecherche.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxRecherche.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxRecherche.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.textBoxRecherche.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxRecherche.ForeColor = System.Drawing.Color.Black;
            this.textBoxRecherche.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.textBoxRecherche.Location = new System.Drawing.Point(34, 60);
            this.textBoxRecherche.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxRecherche.Name = "textBoxRecherche";
            this.textBoxRecherche.PasswordChar = '\0';
            this.textBoxRecherche.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.textBoxRecherche.PlaceholderText = "🔍 Nom, matricule ou entreprise...";
            this.textBoxRecherche.SelectedText = "";
            this.textBoxRecherche.Size = new System.Drawing.Size(532, 45);
            this.textBoxRecherche.TabIndex = 1;
            this.textBoxRecherche.TextChanged += new System.EventHandler(this.textBoxRecherche_TextChanged);
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(30, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Employé:";
            //
            // comboBoxEmploye
            //
            this.comboBoxEmploye.BackColor = System.Drawing.Color.Transparent;
            this.comboBoxEmploye.BorderRadius = 8;
            this.comboBoxEmploye.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEmploye.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEmploye.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.comboBoxEmploye.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.comboBoxEmploye.Font = new System.Drawing.Font("Montserrat", 10F);
            this.comboBoxEmploye.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comboBoxEmploye.ItemHeight = 30;
            this.comboBoxEmploye.Location = new System.Drawing.Point(34, 150);
            this.comboBoxEmploye.Name = "comboBoxEmploye";
            this.comboBoxEmploye.Size = new System.Drawing.Size(532, 36);
            this.comboBoxEmploye.TabIndex = 3;
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(30, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nom du sursalaire:";
            //
            // textBoxNom
            //
            this.textBoxNom.BorderRadius = 8;
            this.textBoxNom.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxNom.DefaultText = "";
            this.textBoxNom.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxNom.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxNom.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxNom.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxNom.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.textBoxNom.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxNom.ForeColor = System.Drawing.Color.Black;
            this.textBoxNom.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.textBoxNom.Location = new System.Drawing.Point(34, 240);
            this.textBoxNom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxNom.MaxLength = 100;
            this.textBoxNom.Name = "textBoxNom";
            this.textBoxNom.PasswordChar = '\0';
            this.textBoxNom.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.textBoxNom.PlaceholderText = "Ex: Prime de rendement";
            this.textBoxNom.SelectedText = "";
            this.textBoxNom.Size = new System.Drawing.Size(532, 45);
            this.textBoxNom.TabIndex = 5;
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(30, 300);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(204, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "Description (optionnel):";
            //
            // textBoxDescription
            //
            this.textBoxDescription.BorderRadius = 8;
            this.textBoxDescription.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxDescription.DefaultText = "";
            this.textBoxDescription.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxDescription.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxDescription.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxDescription.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxDescription.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.textBoxDescription.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxDescription.ForeColor = System.Drawing.Color.Black;
            this.textBoxDescription.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.textBoxDescription.Location = new System.Drawing.Point(34, 330);
            this.textBoxDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxDescription.MaxLength = 255;
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.PasswordChar = '\0';
            this.textBoxDescription.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.textBoxDescription.PlaceholderText = "Décrivez le sursalaire...";
            this.textBoxDescription.SelectedText = "";
            this.textBoxDescription.Size = new System.Drawing.Size(532, 80);
            this.textBoxDescription.TabIndex = 7;
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(30, 425);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 24);
            this.label5.TabIndex = 8;
            this.label5.Text = "Montant (FCFA):";
            //
            // textBoxMontant
            //
            this.textBoxMontant.BorderRadius = 8;
            this.textBoxMontant.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxMontant.DefaultText = "";
            this.textBoxMontant.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxMontant.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxMontant.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxMontant.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxMontant.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.textBoxMontant.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxMontant.ForeColor = System.Drawing.Color.Black;
            this.textBoxMontant.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.textBoxMontant.Location = new System.Drawing.Point(34, 455);
            this.textBoxMontant.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxMontant.MaxLength = 15;
            this.textBoxMontant.Name = "textBoxMontant";
            this.textBoxMontant.PasswordChar = '\0';
            this.textBoxMontant.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.textBoxMontant.PlaceholderText = "Ex: 50000";
            this.textBoxMontant.SelectedText = "";
            this.textBoxMontant.Size = new System.Drawing.Size(250, 45);
            this.textBoxMontant.TabIndex = 9;
            this.textBoxMontant.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxMontant_KeyPress);
            //
            // buttonEnregistrer
            //
            this.buttonEnregistrer.Animated = true;
            this.buttonEnregistrer.AutoRoundedCorners = true;
            this.buttonEnregistrer.BorderRadius = 23;
            this.buttonEnregistrer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonEnregistrer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonEnregistrer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonEnregistrer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonEnregistrer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.buttonEnregistrer.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonEnregistrer.ForeColor = System.Drawing.Color.White;
            this.buttonEnregistrer.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(179)))), ((int)(((byte)(113)))));
            this.buttonEnregistrer.Location = new System.Drawing.Point(34, 540);
            this.buttonEnregistrer.Name = "buttonEnregistrer";
            this.buttonEnregistrer.Size = new System.Drawing.Size(250, 48);
            this.buttonEnregistrer.TabIndex = 10;
            this.buttonEnregistrer.Text = "✓ Enregistrer";
            this.buttonEnregistrer.Click += new System.EventHandler(this.buttonEnregistrer_Click);
            //
            // buttonAnnuler
            //
            this.buttonAnnuler.Animated = true;
            this.buttonAnnuler.AutoRoundedCorners = true;
            this.buttonAnnuler.BorderRadius = 23;
            this.buttonAnnuler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAnnuler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAnnuler.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.buttonAnnuler.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.White;
            this.buttonAnnuler.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(98)))), ((int)(((byte)(104)))));
            this.buttonAnnuler.Location = new System.Drawing.Point(316, 540);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(250, 48);
            this.buttonAnnuler.TabIndex = 11;
            this.buttonAnnuler.Text = "✕ Annuler";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            //
            // AjouterSursalaireForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 700);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AjouterSursalaireForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ajouter Sursalaire";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelRecherche;
        private Guna.UI2.WinForms.Guna2TextBox textBoxRecherche;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxEmploye;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox textBoxNom;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2TextBox textBoxDescription;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2TextBox textBoxMontant;
        private Guna.UI2.WinForms.Guna2Button buttonEnregistrer;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
    }
}
