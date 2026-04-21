namespace RH_GRH
{
    partial class GestionRolesPermissionsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.headerEntreprise = new RH_GRH.HeaderEntreprise();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonActualiser = new System.Windows.Forms.Button();
            this.buttonSupprimer = new System.Windows.Forms.Button();
            this.buttonModifier = new System.Windows.Forms.Button();
            this.buttonAjouter = new System.Windows.Forms.Button();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.textBoxRecherche = new System.Windows.Forms.TextBox();
            this.labelRecherche = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewRoles = new System.Windows.Forms.DataGridView();
            this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNomRole = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNiveauAcces = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNbPermissions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDateCreation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxPermissions = new System.Windows.Forms.GroupBox();
            this.listBoxPermissions = new System.Windows.Forms.ListBox();
            this.panelButtons.SuspendLayout();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRoles)).BeginInit();
            this.groupBoxPermissions.SuspendLayout();
            this.SuspendLayout();
            //
            // headerEntreprise
            //
            this.headerEntreprise.AfficherHorloge = true;
            this.headerEntreprise.AfficherUtilisateur = true;
            this.headerEntreprise.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerEntreprise.Location = new System.Drawing.Point(0, 0);
            this.headerEntreprise.Name = "headerEntreprise";
            this.headerEntreprise.Size = new System.Drawing.Size(1184, 120);
            this.headerEntreprise.SousTitre = "Configuration des profils d\'accès et droits système";
            this.headerEntreprise.TabIndex = 0;
            this.headerEntreprise.Titre = "Rôles et Permissions";
            //
            // panelButtons
            //
            this.panelButtons.BackColor = System.Drawing.Color.White;
            this.panelButtons.Controls.Add(this.buttonActualiser);
            this.panelButtons.Controls.Add(this.buttonSupprimer);
            this.panelButtons.Controls.Add(this.buttonModifier);
            this.panelButtons.Controls.Add(this.buttonAjouter);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelButtons.Location = new System.Drawing.Point(0, 120);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.panelButtons.Size = new System.Drawing.Size(1184, 60);
            this.panelButtons.TabIndex = 1;
            //
            // buttonActualiser
            //
            this.buttonActualiser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.buttonActualiser.FlatAppearance.BorderSize = 0;
            this.buttonActualiser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonActualiser.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonActualiser.ForeColor = System.Drawing.Color.White;
            this.buttonActualiser.Location = new System.Drawing.Point(435, 10);
            this.buttonActualiser.Name = "buttonActualiser";
            this.buttonActualiser.Size = new System.Drawing.Size(120, 40);
            this.buttonActualiser.TabIndex = 3;
            this.buttonActualiser.Text = "🔄 Actualiser";
            this.buttonActualiser.UseVisualStyleBackColor = false;
            this.buttonActualiser.Click += new System.EventHandler(this.buttonActualiser_Click);
            //
            // buttonSupprimer
            //
            this.buttonSupprimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.buttonSupprimer.FlatAppearance.BorderSize = 0;
            this.buttonSupprimer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSupprimer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonSupprimer.ForeColor = System.Drawing.Color.White;
            this.buttonSupprimer.Location = new System.Drawing.Point(295, 10);
            this.buttonSupprimer.Name = "buttonSupprimer";
            this.buttonSupprimer.Size = new System.Drawing.Size(120, 40);
            this.buttonSupprimer.TabIndex = 2;
            this.buttonSupprimer.Text = "🗑️ Supprimer";
            this.buttonSupprimer.UseVisualStyleBackColor = false;
            this.buttonSupprimer.Click += new System.EventHandler(this.buttonSupprimer_Click);
            //
            // buttonModifier
            //
            this.buttonModifier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(139)))), ((int)(((byte)(196)))));
            this.buttonModifier.FlatAppearance.BorderSize = 0;
            this.buttonModifier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonModifier.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonModifier.ForeColor = System.Drawing.Color.White;
            this.buttonModifier.Location = new System.Drawing.Point(155, 10);
            this.buttonModifier.Name = "buttonModifier";
            this.buttonModifier.Size = new System.Drawing.Size(120, 40);
            this.buttonModifier.TabIndex = 1;
            this.buttonModifier.Text = "✏️ Modifier";
            this.buttonModifier.UseVisualStyleBackColor = false;
            this.buttonModifier.Click += new System.EventHandler(this.buttonModifier_Click);
            //
            // buttonAjouter
            //
            this.buttonAjouter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonAjouter.FlatAppearance.BorderSize = 0;
            this.buttonAjouter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAjouter.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonAjouter.ForeColor = System.Drawing.Color.White;
            this.buttonAjouter.Location = new System.Drawing.Point(15, 10);
            this.buttonAjouter.Name = "buttonAjouter";
            this.buttonAjouter.Size = new System.Drawing.Size(120, 40);
            this.buttonAjouter.TabIndex = 0;
            this.buttonAjouter.Text = "➕ Ajouter";
            this.buttonAjouter.UseVisualStyleBackColor = false;
            this.buttonAjouter.Click += new System.EventHandler(this.buttonAjouter_Click);
            //
            // panelSearch
            //
            this.panelSearch.BackColor = System.Drawing.Color.White;
            this.panelSearch.Controls.Add(this.textBoxRecherche);
            this.panelSearch.Controls.Add(this.labelRecherche);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 180);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.panelSearch.Size = new System.Drawing.Size(1184, 50);
            this.panelSearch.TabIndex = 2;
            //
            // textBoxRecherche
            //
            this.textBoxRecherche.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxRecherche.Location = new System.Drawing.Point(95, 13);
            this.textBoxRecherche.Name = "textBoxRecherche";
            this.textBoxRecherche.Size = new System.Drawing.Size(350, 25);
            this.textBoxRecherche.TabIndex = 1;
            this.textBoxRecherche.TextChanged += new System.EventHandler(this.textBoxRecherche_TextChanged);
            //
            // labelRecherche
            //
            this.labelRecherche.AutoSize = true;
            this.labelRecherche.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelRecherche.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.labelRecherche.Location = new System.Drawing.Point(15, 16);
            this.labelRecherche.Name = "labelRecherche";
            this.labelRecherche.Size = new System.Drawing.Size(75, 19);
            this.labelRecherche.TabIndex = 0;
            this.labelRecherche.Text = "Recherche:";
            //
            // splitContainer1
            //
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 230);
            this.splitContainer1.Name = "splitContainer1";
            //
            // splitContainer1.Panel1
            //
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewRoles);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(15, 10, 5, 15);
            //
            // splitContainer1.Panel2
            //
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxPermissions);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(5, 10, 15, 15);
            this.splitContainer1.Size = new System.Drawing.Size(1184, 472);
            this.splitContainer1.SplitterDistance = 750;
            this.splitContainer1.TabIndex = 3;
            //
            // dataGridViewRoles
            //
            this.dataGridViewRoles.AllowUserToAddRows = false;
            this.dataGridViewRoles.AllowUserToDeleteRows = false;
            this.dataGridViewRoles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewRoles.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewRoles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(139)))), ((int)(((byte)(196)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewRoles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRoles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnId,
            this.ColumnNomRole,
            this.ColumnDescription,
            this.ColumnNiveauAcces,
            this.ColumnNbPermissions,
            this.ColumnDateCreation});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(237)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewRoles.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewRoles.EnableHeadersVisualStyles = false;
            this.dataGridViewRoles.Location = new System.Drawing.Point(15, 10);
            this.dataGridViewRoles.MultiSelect = false;
            this.dataGridViewRoles.Name = "dataGridViewRoles";
            this.dataGridViewRoles.ReadOnly = true;
            this.dataGridViewRoles.RowHeadersVisible = false;
            this.dataGridViewRoles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRoles.Size = new System.Drawing.Size(730, 447);
            this.dataGridViewRoles.TabIndex = 0;
            this.dataGridViewRoles.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewRoles_CellDoubleClick);
            this.dataGridViewRoles.SelectionChanged += new System.EventHandler(this.dataGridViewRoles_SelectionChanged);
            //
            // ColumnId
            //
            this.ColumnId.HeaderText = "ID";
            this.ColumnId.Name = "ColumnId";
            this.ColumnId.ReadOnly = true;
            this.ColumnId.Visible = false;
            //
            // ColumnNomRole
            //
            this.ColumnNomRole.FillWeight = 120F;
            this.ColumnNomRole.HeaderText = "Nom du Rôle";
            this.ColumnNomRole.Name = "ColumnNomRole";
            this.ColumnNomRole.ReadOnly = true;
            //
            // ColumnDescription
            //
            this.ColumnDescription.FillWeight = 150F;
            this.ColumnDescription.HeaderText = "Description";
            this.ColumnDescription.Name = "ColumnDescription";
            this.ColumnDescription.ReadOnly = true;
            //
            // ColumnNiveauAcces
            //
            this.ColumnNiveauAcces.FillWeight = 80F;
            this.ColumnNiveauAcces.HeaderText = "Niveau";
            this.ColumnNiveauAcces.Name = "ColumnNiveauAcces";
            this.ColumnNiveauAcces.ReadOnly = true;
            //
            // ColumnNbPermissions
            //
            this.ColumnNbPermissions.FillWeight = 90F;
            this.ColumnNbPermissions.HeaderText = "Permissions";
            this.ColumnNbPermissions.Name = "ColumnNbPermissions";
            this.ColumnNbPermissions.ReadOnly = true;
            //
            // ColumnDateCreation
            //
            this.ColumnDateCreation.FillWeight = 90F;
            this.ColumnDateCreation.HeaderText = "Date Création";
            this.ColumnDateCreation.Name = "ColumnDateCreation";
            this.ColumnDateCreation.ReadOnly = true;
            //
            // groupBoxPermissions
            //
            this.groupBoxPermissions.Controls.Add(this.listBoxPermissions);
            this.groupBoxPermissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPermissions.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxPermissions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.groupBoxPermissions.Location = new System.Drawing.Point(5, 10);
            this.groupBoxPermissions.Name = "groupBoxPermissions";
            this.groupBoxPermissions.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxPermissions.Size = new System.Drawing.Size(410, 447);
            this.groupBoxPermissions.TabIndex = 0;
            this.groupBoxPermissions.TabStop = false;
            this.groupBoxPermissions.Text = "Permissions du rôle sélectionné";
            //
            // listBoxPermissions
            //
            this.listBoxPermissions.BackColor = System.Drawing.Color.White;
            this.listBoxPermissions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxPermissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxPermissions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.listBoxPermissions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.listBoxPermissions.FormattingEnabled = true;
            this.listBoxPermissions.ItemHeight = 15;
            this.listBoxPermissions.Location = new System.Drawing.Point(10, 26);
            this.listBoxPermissions.Name = "listBoxPermissions";
            this.listBoxPermissions.Size = new System.Drawing.Size(390, 411);
            this.listBoxPermissions.TabIndex = 0;
            //
            // GestionRolesPermissionsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 662);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.headerEntreprise);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "GestionRolesPermissionsForm";
            this.Text = "Gestion des Rôles et Permissions";
            this.panelButtons.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRoles)).EndInit();
            this.groupBoxPermissions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HeaderEntreprise headerEntreprise;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button buttonAjouter;
        private System.Windows.Forms.Button buttonModifier;
        private System.Windows.Forms.Button buttonSupprimer;
        private System.Windows.Forms.Button buttonActualiser;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.TextBox textBoxRecherche;
        private System.Windows.Forms.Label labelRecherche;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridViewRoles;
        private System.Windows.Forms.GroupBox groupBoxPermissions;
        private System.Windows.Forms.ListBox listBoxPermissions;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNomRole;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNiveauAcces;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNbPermissions;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDateCreation;
    }
}
