namespace RH_GRH
{
    partial class GestionUtilisateursForm
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
            this.headerEntreprise = new RH_GRH.HeaderEntreprise();
            this.panelActions = new System.Windows.Forms.Panel();
            this.buttonActualiser = new Guna.UI2.WinForms.Guna2Button();
            this.buttonDeverrouiller = new Guna.UI2.WinForms.Guna2Button();
            this.buttonReinitialiserMdp = new Guna.UI2.WinForms.Guna2Button();
            this.buttonSupprimer = new Guna.UI2.WinForms.Guna2Button();
            this.buttonModifier = new Guna.UI2.WinForms.Guna2Button();
            this.buttonAjouter = new Guna.UI2.WinForms.Guna2Button();
            this.panelRecherche = new System.Windows.Forms.Panel();
            this.textBoxRecherche = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelRecherche = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.labelNombreUtilisateurs = new System.Windows.Forms.Label();
            this.dataGridViewUtilisateurs = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNomUtilisateur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNomComplet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTelephone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRoles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActif = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colVerrouille = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDerniereConnexion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelActions.SuspendLayout();
            this.panelRecherche.SuspendLayout();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUtilisateurs)).BeginInit();
            this.SuspendLayout();
            //
            // headerEntreprise
            //
            this.headerEntreprise.AfficherHorloge = true;
            this.headerEntreprise.AfficherUtilisateur = true;
            this.headerEntreprise.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerEntreprise.Location = new System.Drawing.Point(0, 0);
            this.headerEntreprise.Name = "headerEntreprise";
            this.headerEntreprise.Size = new System.Drawing.Size(1200, 120);
            this.headerEntreprise.SousTitre = "Administration des comptes et permissions utilisateurs";
            this.headerEntreprise.TabIndex = 0;
            this.headerEntreprise.Titre = "Gestion des Utilisateurs";
            //
            // panelActions
            //
            this.panelActions.BackColor = System.Drawing.Color.White;
            this.panelActions.Controls.Add(this.buttonActualiser);
            this.panelActions.Controls.Add(this.buttonDeverrouiller);
            this.panelActions.Controls.Add(this.buttonReinitialiserMdp);
            this.panelActions.Controls.Add(this.buttonSupprimer);
            this.panelActions.Controls.Add(this.buttonModifier);
            this.panelActions.Controls.Add(this.buttonAjouter);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(0, 120);
            this.panelActions.Name = "panelActions";
            this.panelActions.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelActions.Size = new System.Drawing.Size(1200, 70);
            this.panelActions.TabIndex = 1;
            //
            // buttonActualiser
            //
            this.buttonActualiser.BorderRadius = 8;
            this.buttonActualiser.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonActualiser.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonActualiser.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonActualiser.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonActualiser.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.buttonActualiser.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonActualiser.ForeColor = System.Drawing.Color.White;
            this.buttonActualiser.Location = new System.Drawing.Point(880, 15);
            this.buttonActualiser.Name = "buttonActualiser";
            this.buttonActualiser.Size = new System.Drawing.Size(140, 40);
            this.buttonActualiser.TabIndex = 5;
            this.buttonActualiser.Text = "🔄 Actualiser";
            this.buttonActualiser.Click += new System.EventHandler(this.buttonActualiser_Click);
            //
            // buttonDeverrouiller
            //
            this.buttonDeverrouiller.BorderRadius = 8;
            this.buttonDeverrouiller.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonDeverrouiller.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonDeverrouiller.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonDeverrouiller.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonDeverrouiller.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.buttonDeverrouiller.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonDeverrouiller.ForeColor = System.Drawing.Color.White;
            this.buttonDeverrouiller.Location = new System.Drawing.Point(720, 15);
            this.buttonDeverrouiller.Name = "buttonDeverrouiller";
            this.buttonDeverrouiller.Size = new System.Drawing.Size(150, 40);
            this.buttonDeverrouiller.TabIndex = 4;
            this.buttonDeverrouiller.Text = "🔓 Déverrouiller";
            this.buttonDeverrouiller.Click += new System.EventHandler(this.buttonDeverrouiller_Click);
            //
            // buttonReinitialiserMdp
            //
            this.buttonReinitialiserMdp.BorderRadius = 8;
            this.buttonReinitialiserMdp.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonReinitialiserMdp.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonReinitialiserMdp.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonReinitialiserMdp.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonReinitialiserMdp.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.buttonReinitialiserMdp.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonReinitialiserMdp.ForeColor = System.Drawing.Color.White;
            this.buttonReinitialiserMdp.Location = new System.Drawing.Point(520, 15);
            this.buttonReinitialiserMdp.Name = "buttonReinitialiserMdp";
            this.buttonReinitialiserMdp.Size = new System.Drawing.Size(190, 40);
            this.buttonReinitialiserMdp.TabIndex = 3;
            this.buttonReinitialiserMdp.Text = "🔑 Réinitialiser Mot de Passe";
            this.buttonReinitialiserMdp.Click += new System.EventHandler(this.buttonReinitialiserMdp_Click);
            //
            // buttonSupprimer
            //
            this.buttonSupprimer.BorderRadius = 8;
            this.buttonSupprimer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonSupprimer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonSupprimer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonSupprimer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonSupprimer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.buttonSupprimer.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonSupprimer.ForeColor = System.Drawing.Color.White;
            this.buttonSupprimer.Location = new System.Drawing.Point(360, 15);
            this.buttonSupprimer.Name = "buttonSupprimer";
            this.buttonSupprimer.Size = new System.Drawing.Size(150, 40);
            this.buttonSupprimer.TabIndex = 2;
            this.buttonSupprimer.Text = "🗑️ Supprimer";
            this.buttonSupprimer.Click += new System.EventHandler(this.buttonSupprimer_Click);
            //
            // buttonModifier
            //
            this.buttonModifier.BorderRadius = 8;
            this.buttonModifier.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonModifier.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonModifier.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonModifier.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonModifier.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.buttonModifier.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonModifier.ForeColor = System.Drawing.Color.White;
            this.buttonModifier.Location = new System.Drawing.Point(190, 15);
            this.buttonModifier.Name = "buttonModifier";
            this.buttonModifier.Size = new System.Drawing.Size(160, 40);
            this.buttonModifier.TabIndex = 1;
            this.buttonModifier.Text = "✏️ Modifier";
            this.buttonModifier.Click += new System.EventHandler(this.buttonModifier_Click);
            //
            // buttonAjouter
            //
            this.buttonAjouter.BorderRadius = 8;
            this.buttonAjouter.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAjouter.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAjouter.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAjouter.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAjouter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.buttonAjouter.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonAjouter.ForeColor = System.Drawing.Color.White;
            this.buttonAjouter.Location = new System.Drawing.Point(20, 15);
            this.buttonAjouter.Name = "buttonAjouter";
            this.buttonAjouter.Size = new System.Drawing.Size(160, 40);
            this.buttonAjouter.TabIndex = 0;
            this.buttonAjouter.Text = "➕ Ajouter";
            this.buttonAjouter.Click += new System.EventHandler(this.buttonAjouter_Click);
            //
            // panelRecherche
            //
            this.panelRecherche.BackColor = System.Drawing.Color.White;
            this.panelRecherche.Controls.Add(this.textBoxRecherche);
            this.panelRecherche.Controls.Add(this.labelRecherche);
            this.panelRecherche.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRecherche.Location = new System.Drawing.Point(0, 190);
            this.panelRecherche.Name = "panelRecherche";
            this.panelRecherche.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelRecherche.Size = new System.Drawing.Size(1200, 60);
            this.panelRecherche.TabIndex = 2;
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
            this.textBoxRecherche.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxRecherche.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxRecherche.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxRecherche.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxRecherche.Location = new System.Drawing.Point(120, 13);
            this.textBoxRecherche.Name = "textBoxRecherche";
            this.textBoxRecherche.PasswordChar = '\0';
            this.textBoxRecherche.PlaceholderText = "Rechercher par nom d\'utilisateur, nom complet ou email...";
            this.textBoxRecherche.SelectedText = "";
            this.textBoxRecherche.Size = new System.Drawing.Size(500, 36);
            this.textBoxRecherche.TabIndex = 1;
            this.textBoxRecherche.TextChanged += new System.EventHandler(this.textBoxRecherche_TextChanged);
            //
            // labelRecherche
            //
            this.labelRecherche.AutoSize = true;
            this.labelRecherche.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelRecherche.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelRecherche.Location = new System.Drawing.Point(20, 20);
            this.labelRecherche.Name = "labelRecherche";
            this.labelRecherche.Size = new System.Drawing.Size(88, 19);
            this.labelRecherche.TabIndex = 0;
            this.labelRecherche.Text = "🔍 Recherche :";
            //
            // panelBottom
            //
            this.panelBottom.BackColor = System.Drawing.Color.White;
            this.panelBottom.Controls.Add(this.labelNombreUtilisateurs);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 750);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelBottom.Size = new System.Drawing.Size(1200, 50);
            this.panelBottom.TabIndex = 4;
            //
            // labelNombreUtilisateurs
            //
            this.labelNombreUtilisateurs.AutoSize = true;
            this.labelNombreUtilisateurs.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.labelNombreUtilisateurs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelNombreUtilisateurs.Location = new System.Drawing.Point(20, 15);
            this.labelNombreUtilisateurs.Name = "labelNombreUtilisateurs";
            this.labelNombreUtilisateurs.Size = new System.Drawing.Size(140, 19);
            this.labelNombreUtilisateurs.TabIndex = 0;
            this.labelNombreUtilisateurs.Text = "Total : 0 utilisateur(s)";
            //
            // dataGridViewUtilisateurs
            //
            this.dataGridViewUtilisateurs.AllowUserToAddRows = false;
            this.dataGridViewUtilisateurs.AllowUserToDeleteRows = false;
            this.dataGridViewUtilisateurs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewUtilisateurs.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewUtilisateurs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUtilisateurs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colNomUtilisateur,
            this.colNomComplet,
            this.colEmail,
            this.colTelephone,
            this.colRoles,
            this.colActif,
            this.colVerrouille,
            this.colDerniereConnexion});
            this.dataGridViewUtilisateurs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewUtilisateurs.Location = new System.Drawing.Point(0, 250);
            this.dataGridViewUtilisateurs.Name = "dataGridViewUtilisateurs";
            this.dataGridViewUtilisateurs.ReadOnly = true;
            this.dataGridViewUtilisateurs.RowHeadersWidth = 51;
            this.dataGridViewUtilisateurs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUtilisateurs.Size = new System.Drawing.Size(1200, 540);
            this.dataGridViewUtilisateurs.TabIndex = 3;
            //
            // colId
            //
            this.colId.DataPropertyName = "id";
            this.colId.FillWeight = 40F;
            this.colId.HeaderText = "ID";
            this.colId.MinimumWidth = 6;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            //
            // colNomUtilisateur
            //
            this.colNomUtilisateur.DataPropertyName = "nom_utilisateur";
            this.colNomUtilisateur.FillWeight = 100F;
            this.colNomUtilisateur.HeaderText = "Nom d\'utilisateur";
            this.colNomUtilisateur.MinimumWidth = 6;
            this.colNomUtilisateur.Name = "colNomUtilisateur";
            this.colNomUtilisateur.ReadOnly = true;
            //
            // colNomComplet
            //
            this.colNomComplet.DataPropertyName = "nom_complet";
            this.colNomComplet.FillWeight = 120F;
            this.colNomComplet.HeaderText = "Nom complet";
            this.colNomComplet.MinimumWidth = 6;
            this.colNomComplet.Name = "colNomComplet";
            this.colNomComplet.ReadOnly = true;
            //
            // colEmail
            //
            this.colEmail.DataPropertyName = "email";
            this.colEmail.FillWeight = 120F;
            this.colEmail.HeaderText = "Email";
            this.colEmail.MinimumWidth = 6;
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;
            //
            // colTelephone
            //
            this.colTelephone.DataPropertyName = "telephone";
            this.colTelephone.FillWeight = 80F;
            this.colTelephone.HeaderText = "Téléphone";
            this.colTelephone.MinimumWidth = 6;
            this.colTelephone.Name = "colTelephone";
            this.colTelephone.ReadOnly = true;
            //
            // colRoles
            //
            this.colRoles.DataPropertyName = "roles";
            this.colRoles.FillWeight = 120F;
            this.colRoles.HeaderText = "Rôles";
            this.colRoles.MinimumWidth = 6;
            this.colRoles.Name = "colRoles";
            this.colRoles.ReadOnly = true;
            //
            // colActif
            //
            this.colActif.DataPropertyName = "actif";
            this.colActif.FillWeight = 50F;
            this.colActif.HeaderText = "Actif";
            this.colActif.MinimumWidth = 6;
            this.colActif.Name = "colActif";
            this.colActif.ReadOnly = true;
            //
            // colVerrouille
            //
            this.colVerrouille.DataPropertyName = "compte_verrouille";
            this.colVerrouille.FillWeight = 60F;
            this.colVerrouille.HeaderText = "Verrouillé";
            this.colVerrouille.MinimumWidth = 6;
            this.colVerrouille.Name = "colVerrouille";
            this.colVerrouille.ReadOnly = true;
            //
            // colDerniereConnexion
            //
            this.colDerniereConnexion.DataPropertyName = "derniere_connexion";
            this.colDerniereConnexion.FillWeight = 110F;
            this.colDerniereConnexion.HeaderText = "Dernière connexion";
            this.colDerniereConnexion.MinimumWidth = 6;
            this.colDerniereConnexion.Name = "colDerniereConnexion";
            this.colDerniereConnexion.ReadOnly = true;
            //
            // GestionUtilisateursForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.dataGridViewUtilisateurs);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelRecherche);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.headerEntreprise);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GestionUtilisateursForm";
            this.Text = "Gestion des Utilisateurs";
            this.panelActions.ResumeLayout(false);
            this.panelRecherche.ResumeLayout(false);
            this.panelRecherche.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUtilisateurs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private HeaderEntreprise headerEntreprise;
        private System.Windows.Forms.Panel panelActions;
        private Guna.UI2.WinForms.Guna2Button buttonAjouter;
        private Guna.UI2.WinForms.Guna2Button buttonModifier;
        private Guna.UI2.WinForms.Guna2Button buttonSupprimer;
        private Guna.UI2.WinForms.Guna2Button buttonReinitialiserMdp;
        private Guna.UI2.WinForms.Guna2Button buttonDeverrouiller;
        private Guna.UI2.WinForms.Guna2Button buttonActualiser;
        private System.Windows.Forms.Panel panelRecherche;
        private Guna.UI2.WinForms.Guna2TextBox textBoxRecherche;
        private System.Windows.Forms.Label labelRecherche;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label labelNombreUtilisateurs;
        private System.Windows.Forms.DataGridView dataGridViewUtilisateurs;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNomUtilisateur;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNomComplet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTelephone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoles;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colActif;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colVerrouille;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDerniereConnexion;
    }
}
