namespace RH_GRH
{
    partial class AjouterModifierRoleForm
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.groupBoxPermissions = new System.Windows.Forms.GroupBox();
            this.panelPermissionsButtons = new System.Windows.Forms.Panel();
            this.buttonDeselectionnerTout = new System.Windows.Forms.Button();
            this.buttonSelectionnerTout = new System.Windows.Forms.Button();
            this.textBoxRecherche = new System.Windows.Forms.TextBox();
            this.labelRecherchePermission = new System.Windows.Forms.Label();
            this.checkedListBoxPermissions = new System.Windows.Forms.CheckedListBox();
            this.groupBoxInfos = new System.Windows.Forms.GroupBox();
            this.numericUpDownNiveauAcces = new System.Windows.Forms.NumericUpDown();
            this.labelNiveauAcces = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxNomRole = new System.Windows.Forms.TextBox();
            this.labelNomRole = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonAnnuler = new System.Windows.Forms.Button();
            this.buttonEnregistrer = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.groupBoxPermissions.SuspendLayout();
            this.panelPermissionsButtons.SuspendLayout();
            this.groupBoxInfos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNiveauAcces)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // panelTop
            //
            this.panelTop.BackColor = System.Drawing.Color.White;
            this.panelTop.Controls.Add(this.labelTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(884, 60);
            this.panelTop.TabIndex = 0;
            //
            // labelTitle
            //
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Montserrat", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.labelTitle.Location = new System.Drawing.Point(20, 18);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(179, 23);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Ajouter un Rôle";
            //
            // panelMain
            //
            this.panelMain.Controls.Add(this.groupBoxPermissions);
            this.panelMain.Controls.Add(this.groupBoxInfos);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 60);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(884, 500);
            this.panelMain.TabIndex = 1;
            //
            // groupBoxPermissions
            //
            this.groupBoxPermissions.Controls.Add(this.panelPermissionsButtons);
            this.groupBoxPermissions.Controls.Add(this.textBoxRecherche);
            this.groupBoxPermissions.Controls.Add(this.labelRecherchePermission);
            this.groupBoxPermissions.Controls.Add(this.checkedListBoxPermissions);
            this.groupBoxPermissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPermissions.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxPermissions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.groupBoxPermissions.Location = new System.Drawing.Point(20, 200);
            this.groupBoxPermissions.Name = "groupBoxPermissions";
            this.groupBoxPermissions.Padding = new System.Windows.Forms.Padding(15);
            this.groupBoxPermissions.Size = new System.Drawing.Size(844, 280);
            this.groupBoxPermissions.TabIndex = 1;
            this.groupBoxPermissions.TabStop = false;
            this.groupBoxPermissions.Text = "Permissions";
            //
            // panelPermissionsButtons
            //
            this.panelPermissionsButtons.Controls.Add(this.buttonDeselectionnerTout);
            this.panelPermissionsButtons.Controls.Add(this.buttonSelectionnerTout);
            this.panelPermissionsButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPermissionsButtons.Location = new System.Drawing.Point(15, 235);
            this.panelPermissionsButtons.Name = "panelPermissionsButtons";
            this.panelPermissionsButtons.Size = new System.Drawing.Size(814, 30);
            this.panelPermissionsButtons.TabIndex = 3;
            //
            // buttonDeselectionnerTout
            //
            this.buttonDeselectionnerTout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.buttonDeselectionnerTout.FlatAppearance.BorderSize = 0;
            this.buttonDeselectionnerTout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeselectionnerTout.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.buttonDeselectionnerTout.ForeColor = System.Drawing.Color.White;
            this.buttonDeselectionnerTout.Location = new System.Drawing.Point(160, 0);
            this.buttonDeselectionnerTout.Name = "buttonDeselectionnerTout";
            this.buttonDeselectionnerTout.Size = new System.Drawing.Size(150, 28);
            this.buttonDeselectionnerTout.TabIndex = 1;
            this.buttonDeselectionnerTout.Text = "✗ Tout désélectionner";
            this.buttonDeselectionnerTout.UseVisualStyleBackColor = false;
            this.buttonDeselectionnerTout.Click += new System.EventHandler(this.buttonDeselectionnerTout_Click);
            //
            // buttonSelectionnerTout
            //
            this.buttonSelectionnerTout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.buttonSelectionnerTout.FlatAppearance.BorderSize = 0;
            this.buttonSelectionnerTout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSelectionnerTout.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.buttonSelectionnerTout.ForeColor = System.Drawing.Color.White;
            this.buttonSelectionnerTout.Location = new System.Drawing.Point(0, 0);
            this.buttonSelectionnerTout.Name = "buttonSelectionnerTout";
            this.buttonSelectionnerTout.Size = new System.Drawing.Size(150, 28);
            this.buttonSelectionnerTout.TabIndex = 0;
            this.buttonSelectionnerTout.Text = "✓ Tout sélectionner";
            this.buttonSelectionnerTout.UseVisualStyleBackColor = false;
            this.buttonSelectionnerTout.Click += new System.EventHandler(this.buttonSelectionnerTout_Click);
            //
            // textBoxRecherche
            //
            this.textBoxRecherche.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRecherche.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxRecherche.Location = new System.Drawing.Point(95, 25);
            this.textBoxRecherche.Name = "textBoxRecherche";
            this.textBoxRecherche.Size = new System.Drawing.Size(730, 23);
            this.textBoxRecherche.TabIndex = 2;
            this.textBoxRecherche.TextChanged += new System.EventHandler(this.textBoxRecherche_TextChanged);
            //
            // labelRecherchePermission
            //
            this.labelRecherchePermission.AutoSize = true;
            this.labelRecherchePermission.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelRecherchePermission.Location = new System.Drawing.Point(15, 28);
            this.labelRecherchePermission.Name = "labelRecherchePermission";
            this.labelRecherchePermission.Size = new System.Drawing.Size(65, 15);
            this.labelRecherchePermission.TabIndex = 1;
            this.labelRecherchePermission.Text = "Recherche:";
            //
            // checkedListBoxPermissions
            //
            this.checkedListBoxPermissions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxPermissions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkedListBoxPermissions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkedListBoxPermissions.FormattingEnabled = true;
            this.checkedListBoxPermissions.Location = new System.Drawing.Point(15, 55);
            this.checkedListBoxPermissions.Name = "checkedListBoxPermissions";
            this.checkedListBoxPermissions.Size = new System.Drawing.Size(810, 174);
            this.checkedListBoxPermissions.TabIndex = 0;
            this.checkedListBoxPermissions.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxPermissions_ItemCheck);
            //
            // groupBoxInfos
            //
            this.groupBoxInfos.Controls.Add(this.numericUpDownNiveauAcces);
            this.groupBoxInfos.Controls.Add(this.labelNiveauAcces);
            this.groupBoxInfos.Controls.Add(this.textBoxDescription);
            this.groupBoxInfos.Controls.Add(this.labelDescription);
            this.groupBoxInfos.Controls.Add(this.textBoxNomRole);
            this.groupBoxInfos.Controls.Add(this.labelNomRole);
            this.groupBoxInfos.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxInfos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxInfos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.groupBoxInfos.Location = new System.Drawing.Point(20, 20);
            this.groupBoxInfos.Name = "groupBoxInfos";
            this.groupBoxInfos.Padding = new System.Windows.Forms.Padding(15);
            this.groupBoxInfos.Size = new System.Drawing.Size(844, 180);
            this.groupBoxInfos.TabIndex = 0;
            this.groupBoxInfos.TabStop = false;
            this.groupBoxInfos.Text = "Informations du Rôle";
            //
            // numericUpDownNiveauAcces
            //
            this.numericUpDownNiveauAcces.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numericUpDownNiveauAcces.Location = new System.Drawing.Point(150, 138);
            this.numericUpDownNiveauAcces.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownNiveauAcces.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNiveauAcces.Name = "numericUpDownNiveauAcces";
            this.numericUpDownNiveauAcces.Size = new System.Drawing.Size(120, 25);
            this.numericUpDownNiveauAcces.TabIndex = 5;
            this.numericUpDownNiveauAcces.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            //
            // labelNiveauAcces
            //
            this.labelNiveauAcces.AutoSize = true;
            this.labelNiveauAcces.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelNiveauAcces.Location = new System.Drawing.Point(18, 140);
            this.labelNiveauAcces.Name = "labelNiveauAcces";
            this.labelNiveauAcces.Size = new System.Drawing.Size(99, 17);
            this.labelNiveauAcces.TabIndex = 4;
            this.labelNiveauAcces.Text = "Niveau d\'accès:";
            //
            // textBoxDescription
            //
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxDescription.Location = new System.Drawing.Point(150, 75);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(675, 50);
            this.textBoxDescription.TabIndex = 3;
            //
            // labelDescription
            //
            this.labelDescription.AutoSize = true;
            this.labelDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelDescription.Location = new System.Drawing.Point(18, 78);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(79, 17);
            this.labelDescription.TabIndex = 2;
            this.labelDescription.Text = "Description:";
            //
            // textBoxNomRole
            //
            this.textBoxNomRole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNomRole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxNomRole.Location = new System.Drawing.Point(150, 35);
            this.textBoxNomRole.Name = "textBoxNomRole";
            this.textBoxNomRole.Size = new System.Drawing.Size(675, 25);
            this.textBoxNomRole.TabIndex = 1;
            //
            // labelNomRole
            //
            this.labelNomRole.AutoSize = true;
            this.labelNomRole.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelNomRole.Location = new System.Drawing.Point(18, 38);
            this.labelNomRole.Name = "labelNomRole";
            this.labelNomRole.Size = new System.Drawing.Size(88, 17);
            this.labelNomRole.TabIndex = 0;
            this.labelNomRole.Text = "Nom du rôle:";
            //
            // panelButtons
            //
            this.panelButtons.BackColor = System.Drawing.Color.White;
            this.panelButtons.Controls.Add(this.buttonAnnuler);
            this.panelButtons.Controls.Add(this.buttonEnregistrer);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 560);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelButtons.Size = new System.Drawing.Size(884, 60);
            this.panelButtons.TabIndex = 2;
            //
            // buttonAnnuler
            //
            this.buttonAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAnnuler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.buttonAnnuler.FlatAppearance.BorderSize = 0;
            this.buttonAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAnnuler.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.White;
            this.buttonAnnuler.Location = new System.Drawing.Point(724, 10);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(140, 40);
            this.buttonAnnuler.TabIndex = 1;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.UseVisualStyleBackColor = false;
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            //
            // buttonEnregistrer
            //
            this.buttonEnregistrer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEnregistrer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.buttonEnregistrer.FlatAppearance.BorderSize = 0;
            this.buttonEnregistrer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEnregistrer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.buttonEnregistrer.ForeColor = System.Drawing.Color.White;
            this.buttonEnregistrer.Location = new System.Drawing.Point(564, 10);
            this.buttonEnregistrer.Name = "buttonEnregistrer";
            this.buttonEnregistrer.Size = new System.Drawing.Size(140, 40);
            this.buttonEnregistrer.TabIndex = 0;
            this.buttonEnregistrer.Text = "💾 Enregistrer";
            this.buttonEnregistrer.UseVisualStyleBackColor = false;
            this.buttonEnregistrer.Click += new System.EventHandler(this.buttonEnregistrer_Click);
            //
            // AjouterModifierRoleForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 620);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelTop);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "AjouterModifierRoleForm";
            this.Text = "Ajouter un Rôle";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.groupBoxPermissions.ResumeLayout(false);
            this.groupBoxPermissions.PerformLayout();
            this.panelPermissionsButtons.ResumeLayout(false);
            this.groupBoxInfos.ResumeLayout(false);
            this.groupBoxInfos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNiveauAcces)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.GroupBox groupBoxInfos;
        private System.Windows.Forms.TextBox textBoxNomRole;
        private System.Windows.Forms.Label labelNomRole;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.NumericUpDown numericUpDownNiveauAcces;
        private System.Windows.Forms.Label labelNiveauAcces;
        private System.Windows.Forms.GroupBox groupBoxPermissions;
        private System.Windows.Forms.CheckedListBox checkedListBoxPermissions;
        private System.Windows.Forms.TextBox textBoxRecherche;
        private System.Windows.Forms.Label labelRecherchePermission;
        private System.Windows.Forms.Panel panelPermissionsButtons;
        private System.Windows.Forms.Button buttonSelectionnerTout;
        private System.Windows.Forms.Button buttonDeselectionnerTout;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button buttonEnregistrer;
        private System.Windows.Forms.Button buttonAnnuler;
    }
}
