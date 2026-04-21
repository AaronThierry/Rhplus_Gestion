namespace RH_GRH
{
    partial class AjouterModifierUtilisateurForm
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
            this.panelTitre = new System.Windows.Forms.Panel();
            this.labelTitre = new System.Windows.Forms.Label();
            this.groupBoxInfos = new System.Windows.Forms.GroupBox();
            this.checkBoxActif = new System.Windows.Forms.CheckBox();
            this.textBoxTelephone = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelTelephone = new System.Windows.Forms.Label();
            this.textBoxEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.textBoxNomComplet = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelNomComplet = new System.Windows.Forms.Label();
            this.textBoxNomUtilisateur = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelNomUtilisateur = new System.Windows.Forms.Label();
            this.groupBoxMotDePasse = new System.Windows.Forms.GroupBox();
            this.labelMotDePasse = new System.Windows.Forms.Label();
            this.groupBoxRoles = new System.Windows.Forms.GroupBox();
            this.checkedListBoxRoles = new System.Windows.Forms.CheckedListBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonAnnuler = new Guna.UI2.WinForms.Guna2Button();
            this.buttonEnregistrer = new Guna.UI2.WinForms.Guna2Button();
            this.panelTitre.SuspendLayout();
            this.groupBoxInfos.SuspendLayout();
            this.groupBoxMotDePasse.SuspendLayout();
            this.groupBoxRoles.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // panelTitre
            //
            this.panelTitre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(112)))));
            this.panelTitre.Controls.Add(this.labelTitre);
            this.panelTitre.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitre.Location = new System.Drawing.Point(0, 0);
            this.panelTitre.Name = "panelTitre";
            this.panelTitre.Size = new System.Drawing.Size(700, 60);
            this.panelTitre.TabIndex = 0;
            //
            // labelTitre
            //
            this.labelTitre.AutoSize = true;
            this.labelTitre.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitre.ForeColor = System.Drawing.Color.White;
            this.labelTitre.Location = new System.Drawing.Point(15, 15);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(200, 30);
            this.labelTitre.TabIndex = 0;
            this.labelTitre.Text = "Nouvel utilisateur";
            //
            // groupBoxInfos
            //
            this.groupBoxInfos.Controls.Add(this.checkBoxActif);
            this.groupBoxInfos.Controls.Add(this.textBoxTelephone);
            this.groupBoxInfos.Controls.Add(this.labelTelephone);
            this.groupBoxInfos.Controls.Add(this.textBoxEmail);
            this.groupBoxInfos.Controls.Add(this.labelEmail);
            this.groupBoxInfos.Controls.Add(this.textBoxNomComplet);
            this.groupBoxInfos.Controls.Add(this.labelNomComplet);
            this.groupBoxInfos.Controls.Add(this.textBoxNomUtilisateur);
            this.groupBoxInfos.Controls.Add(this.labelNomUtilisateur);
            this.groupBoxInfos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxInfos.Location = new System.Drawing.Point(20, 80);
            this.groupBoxInfos.Name = "groupBoxInfos";
            this.groupBoxInfos.Size = new System.Drawing.Size(660, 180);
            this.groupBoxInfos.TabIndex = 1;
            this.groupBoxInfos.TabStop = false;
            this.groupBoxInfos.Text = "Informations générales";
            //
            // checkBoxActif
            //
            this.checkBoxActif.AutoSize = true;
            this.checkBoxActif.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBoxActif.Location = new System.Drawing.Point(480, 145);
            this.checkBoxActif.Name = "checkBoxActif";
            this.checkBoxActif.Size = new System.Drawing.Size(113, 19);
            this.checkBoxActif.TabIndex = 8;
            this.checkBoxActif.Text = "Compte actif";
            this.checkBoxActif.UseVisualStyleBackColor = true;
            //
            // textBoxTelephone
            //
            this.textBoxTelephone.BorderRadius = 5;
            this.textBoxTelephone.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxTelephone.DefaultText = "";
            this.textBoxTelephone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxTelephone.Location = new System.Drawing.Point(480, 105);
            this.textBoxTelephone.Name = "textBoxTelephone";
            this.textBoxTelephone.PasswordChar = '\0';
            this.textBoxTelephone.PlaceholderText = "Téléphone (optionnel)";
            this.textBoxTelephone.SelectedText = "";
            this.textBoxTelephone.Size = new System.Drawing.Size(160, 30);
            this.textBoxTelephone.TabIndex = 7;
            //
            // labelTelephone
            //
            this.labelTelephone.AutoSize = true;
            this.labelTelephone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelTelephone.Location = new System.Drawing.Point(480, 85);
            this.labelTelephone.Name = "labelTelephone";
            this.labelTelephone.Size = new System.Drawing.Size(67, 15);
            this.labelTelephone.TabIndex = 6;
            this.labelTelephone.Text = "Téléphone:";
            //
            // textBoxEmail
            //
            this.textBoxEmail.BorderRadius = 5;
            this.textBoxEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxEmail.DefaultText = "";
            this.textBoxEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxEmail.Location = new System.Drawing.Point(20, 105);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.PasswordChar = '\0';
            this.textBoxEmail.PlaceholderText = "email@example.com (optionnel)";
            this.textBoxEmail.SelectedText = "";
            this.textBoxEmail.Size = new System.Drawing.Size(440, 30);
            this.textBoxEmail.TabIndex = 5;
            //
            // labelEmail
            //
            this.labelEmail.AutoSize = true;
            this.labelEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelEmail.Location = new System.Drawing.Point(20, 85);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(39, 15);
            this.labelEmail.TabIndex = 4;
            this.labelEmail.Text = "Email:";
            //
            // textBoxNomComplet
            //
            this.textBoxNomComplet.BorderRadius = 5;
            this.textBoxNomComplet.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxNomComplet.DefaultText = "";
            this.textBoxNomComplet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxNomComplet.Location = new System.Drawing.Point(320, 45);
            this.textBoxNomComplet.Name = "textBoxNomComplet";
            this.textBoxNomComplet.PasswordChar = '\0';
            this.textBoxNomComplet.PlaceholderText = "Nom et prénom complet";
            this.textBoxNomComplet.SelectedText = "";
            this.textBoxNomComplet.Size = new System.Drawing.Size(320, 30);
            this.textBoxNomComplet.TabIndex = 3;
            //
            // labelNomComplet
            //
            this.labelNomComplet.AutoSize = true;
            this.labelNomComplet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelNomComplet.Location = new System.Drawing.Point(320, 25);
            this.labelNomComplet.Name = "labelNomComplet";
            this.labelNomComplet.Size = new System.Drawing.Size(85, 15);
            this.labelNomComplet.TabIndex = 2;
            this.labelNomComplet.Text = "Nom complet*:";
            //
            // textBoxNomUtilisateur
            //
            this.textBoxNomUtilisateur.BorderRadius = 5;
            this.textBoxNomUtilisateur.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxNomUtilisateur.DefaultText = "";
            this.textBoxNomUtilisateur.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxNomUtilisateur.Location = new System.Drawing.Point(20, 45);
            this.textBoxNomUtilisateur.Name = "textBoxNomUtilisateur";
            this.textBoxNomUtilisateur.PasswordChar = '\0';
            this.textBoxNomUtilisateur.PlaceholderText = "Nom d\'utilisateur unique";
            this.textBoxNomUtilisateur.SelectedText = "";
            this.textBoxNomUtilisateur.Size = new System.Drawing.Size(280, 30);
            this.textBoxNomUtilisateur.TabIndex = 1;
            //
            // labelNomUtilisateur
            //
            this.labelNomUtilisateur.AutoSize = true;
            this.labelNomUtilisateur.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelNomUtilisateur.Location = new System.Drawing.Point(20, 25);
            this.labelNomUtilisateur.Name = "labelNomUtilisateur";
            this.labelNomUtilisateur.Size = new System.Drawing.Size(114, 15);
            this.labelNomUtilisateur.TabIndex = 0;
            this.labelNomUtilisateur.Text = "Nom d\'utilisateur*:";
            //
            // groupBoxMotDePasse
            //
            this.groupBoxMotDePasse.Controls.Add(this.labelMotDePasse);
            this.groupBoxMotDePasse.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxMotDePasse.Location = new System.Drawing.Point(20, 270);
            this.groupBoxMotDePasse.Name = "groupBoxMotDePasse";
            this.groupBoxMotDePasse.Size = new System.Drawing.Size(660, 80);
            this.groupBoxMotDePasse.TabIndex = 2;
            this.groupBoxMotDePasse.TabStop = false;
            this.groupBoxMotDePasse.Text = "Mot de passe par défaut";
            //
            // labelMotDePasse
            //
            this.labelMotDePasse.AutoSize = false;
            this.labelMotDePasse.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.labelMotDePasse.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.labelMotDePasse.Location = new System.Drawing.Point(20, 30);
            this.labelMotDePasse.Name = "labelMotDePasse";
            this.labelMotDePasse.Size = new System.Drawing.Size(620, 40);
            this.labelMotDePasse.TabIndex = 0;
            this.labelMotDePasse.Text = "Le mot de passe par défaut \"RHPlus2026!\" sera attribué automatiquement.\r\nL\'utilisateur devra le changer lors de sa première connexion.";
            //
            // groupBoxRoles
            //
            this.groupBoxRoles.Controls.Add(this.checkedListBoxRoles);
            this.groupBoxRoles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxRoles.Location = new System.Drawing.Point(20, 360);
            this.groupBoxRoles.Name = "groupBoxRoles";
            this.groupBoxRoles.Size = new System.Drawing.Size(660, 180);
            this.groupBoxRoles.TabIndex = 3;
            this.groupBoxRoles.TabStop = false;
            this.groupBoxRoles.Text = "Rôles et permissions*";
            //
            // checkedListBoxRoles
            //
            this.checkedListBoxRoles.CheckOnClick = true;
            this.checkedListBoxRoles.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkedListBoxRoles.FormattingEnabled = true;
            this.checkedListBoxRoles.Location = new System.Drawing.Point(20, 30);
            this.checkedListBoxRoles.Name = "checkedListBoxRoles";
            this.checkedListBoxRoles.Size = new System.Drawing.Size(620, 130);
            this.checkedListBoxRoles.TabIndex = 0;
            //
            // panelButtons
            //
            this.panelButtons.Controls.Add(this.buttonAnnuler);
            this.panelButtons.Controls.Add(this.buttonEnregistrer);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 580);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(700, 70);
            this.panelButtons.TabIndex = 4;
            //
            // buttonAnnuler
            //
            this.buttonAnnuler.BorderRadius = 5;
            this.buttonAnnuler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAnnuler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAnnuler.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.buttonAnnuler.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.White;
            this.buttonAnnuler.Location = new System.Drawing.Point(365, 15);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(150, 40);
            this.buttonAnnuler.TabIndex = 1;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            //
            // buttonEnregistrer
            //
            this.buttonEnregistrer.BorderRadius = 5;
            this.buttonEnregistrer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonEnregistrer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonEnregistrer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonEnregistrer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonEnregistrer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.buttonEnregistrer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.buttonEnregistrer.ForeColor = System.Drawing.Color.White;
            this.buttonEnregistrer.Location = new System.Drawing.Point(185, 15);
            this.buttonEnregistrer.Name = "buttonEnregistrer";
            this.buttonEnregistrer.Size = new System.Drawing.Size(150, 40);
            this.buttonEnregistrer.TabIndex = 0;
            this.buttonEnregistrer.Text = "Enregistrer";
            this.buttonEnregistrer.Click += new System.EventHandler(this.buttonEnregistrer_Click);
            //
            // AjouterModifierUtilisateurForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 650);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.groupBoxRoles);
            this.Controls.Add(this.groupBoxMotDePasse);
            this.Controls.Add(this.groupBoxInfos);
            this.Controls.Add(this.panelTitre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AjouterModifierUtilisateurForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestion Utilisateur";
            this.panelTitre.ResumeLayout(false);
            this.panelTitre.PerformLayout();
            this.groupBoxInfos.ResumeLayout(false);
            this.groupBoxInfos.PerformLayout();
            this.groupBoxMotDePasse.ResumeLayout(false);
            this.groupBoxMotDePasse.PerformLayout();
            this.groupBoxRoles.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitre;
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.GroupBox groupBoxInfos;
        private System.Windows.Forms.CheckBox checkBoxActif;
        private Guna.UI2.WinForms.Guna2TextBox textBoxTelephone;
        private System.Windows.Forms.Label labelTelephone;
        private Guna.UI2.WinForms.Guna2TextBox textBoxEmail;
        private System.Windows.Forms.Label labelEmail;
        private Guna.UI2.WinForms.Guna2TextBox textBoxNomComplet;
        private System.Windows.Forms.Label labelNomComplet;
        private Guna.UI2.WinForms.Guna2TextBox textBoxNomUtilisateur;
        private System.Windows.Forms.Label labelNomUtilisateur;
        private System.Windows.Forms.GroupBox groupBoxMotDePasse;
        private System.Windows.Forms.Label labelMotDePasse;
        private System.Windows.Forms.GroupBox groupBoxRoles;
        private System.Windows.Forms.CheckedListBox checkedListBoxRoles;
        private System.Windows.Forms.Panel panelButtons;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
        private Guna.UI2.WinForms.Guna2Button buttonEnregistrer;
    }
}
