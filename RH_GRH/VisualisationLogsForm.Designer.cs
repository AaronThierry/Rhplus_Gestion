namespace RH_GRH
{
    partial class VisualisationLogsForm
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

        private void InitializeComponent()
        {
            this.headerEntreprise = new RH_GRH.HeaderEntreprise();
            this.panelFiltres = new System.Windows.Forms.Panel();
            this.buttonFiltrer = new Guna.UI2.WinForms.Guna2Button();
            this.buttonActualiser = new Guna.UI2.WinForms.Guna2Button();
            this.buttonExporter = new Guna.UI2.WinForms.Guna2Button();
            this.comboBoxResultat = new System.Windows.Forms.ComboBox();
            this.comboBoxModule = new System.Windows.Forms.ComboBox();
            this.datePickerFin = new System.Windows.Forms.DateTimePicker();
            this.datePickerDebut = new System.Windows.Forms.DateTimePicker();
            this.labelResultat = new System.Windows.Forms.Label();
            this.labelModule = new System.Windows.Forms.Label();
            this.labelPeriode = new System.Windows.Forms.Label();
            this.panelRecherche = new System.Windows.Forms.Panel();
            this.textBoxRecherche = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelRecherche = new System.Windows.Forms.Label();
            this.dataGridViewLogs = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUtilisateur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResultat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.labelNombreLogs = new System.Windows.Forms.Label();
            this.panelFiltres.SuspendLayout();
            this.panelRecherche.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLogs)).BeginInit();
            this.panelBottom.SuspendLayout();
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
            this.headerEntreprise.SousTitre = "Historique complet des actions système et utilisateurs";
            this.headerEntreprise.TabIndex = 0;
            this.headerEntreprise.Titre = "Logs d\'Activité";
            //
            // panelFiltres
            //
            this.panelFiltres.BackColor = System.Drawing.Color.White;
            this.panelFiltres.Controls.Add(this.buttonFiltrer);
            this.panelFiltres.Controls.Add(this.buttonActualiser);
            this.panelFiltres.Controls.Add(this.buttonExporter);
            this.panelFiltres.Controls.Add(this.comboBoxResultat);
            this.panelFiltres.Controls.Add(this.comboBoxModule);
            this.panelFiltres.Controls.Add(this.datePickerFin);
            this.panelFiltres.Controls.Add(this.datePickerDebut);
            this.panelFiltres.Controls.Add(this.labelResultat);
            this.panelFiltres.Controls.Add(this.labelModule);
            this.panelFiltres.Controls.Add(this.labelPeriode);
            this.panelFiltres.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltres.Location = new System.Drawing.Point(0, 120);
            this.panelFiltres.Name = "panelFiltres";
            this.panelFiltres.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelFiltres.Size = new System.Drawing.Size(1200, 100);
            this.panelFiltres.TabIndex = 1;
            //
            // buttonFiltrer
            //
            this.buttonFiltrer.BorderRadius = 8;
            this.buttonFiltrer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.buttonFiltrer.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonFiltrer.ForeColor = System.Drawing.Color.White;
            this.buttonFiltrer.Location = new System.Drawing.Point(930, 50);
            this.buttonFiltrer.Name = "buttonFiltrer";
            this.buttonFiltrer.Size = new System.Drawing.Size(120, 36);
            this.buttonFiltrer.TabIndex = 9;
            this.buttonFiltrer.Text = "🔍 Filtrer";
            this.buttonFiltrer.Click += new System.EventHandler(this.buttonFiltrer_Click);
            //
            // buttonActualiser
            //
            this.buttonActualiser.BorderRadius = 8;
            this.buttonActualiser.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.buttonActualiser.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonActualiser.ForeColor = System.Drawing.Color.White;
            this.buttonActualiser.Location = new System.Drawing.Point(800, 50);
            this.buttonActualiser.Name = "buttonActualiser";
            this.buttonActualiser.Size = new System.Drawing.Size(120, 36);
            this.buttonActualiser.TabIndex = 8;
            this.buttonActualiser.Text = "🔄 Actualiser";
            this.buttonActualiser.Click += new System.EventHandler(this.buttonActualiser_Click);
            //
            // buttonExporter
            //
            this.buttonExporter.BorderRadius = 8;
            this.buttonExporter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.buttonExporter.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonExporter.ForeColor = System.Drawing.Color.White;
            this.buttonExporter.Location = new System.Drawing.Point(1060, 50);
            this.buttonExporter.Name = "buttonExporter";
            this.buttonExporter.Size = new System.Drawing.Size(120, 36);
            this.buttonExporter.TabIndex = 10;
            this.buttonExporter.Text = "📤 Exporter";
            this.buttonExporter.Click += new System.EventHandler(this.buttonExporter_Click);
            //
            // comboBoxResultat
            //
            this.comboBoxResultat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxResultat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBoxResultat.FormattingEnabled = true;
            this.comboBoxResultat.Location = new System.Drawing.Point(630, 56);
            this.comboBoxResultat.Name = "comboBoxResultat";
            this.comboBoxResultat.Size = new System.Drawing.Size(150, 25);
            this.comboBoxResultat.TabIndex = 7;
            //
            // comboBoxModule
            //
            this.comboBoxModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModule.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBoxModule.FormattingEnabled = true;
            this.comboBoxModule.Location = new System.Drawing.Point(430, 56);
            this.comboBoxModule.Name = "comboBoxModule";
            this.comboBoxModule.Size = new System.Drawing.Size(150, 25);
            this.comboBoxModule.TabIndex = 5;
            //
            // datePickerFin
            //
            this.datePickerFin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.datePickerFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerFin.Location = new System.Drawing.Point(270, 56);
            this.datePickerFin.Name = "datePickerFin";
            this.datePickerFin.Size = new System.Drawing.Size(110, 25);
            this.datePickerFin.TabIndex = 3;
            //
            // datePickerDebut
            //
            this.datePickerDebut.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.datePickerDebut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerDebut.Location = new System.Drawing.Point(110, 56);
            this.datePickerDebut.Name = "datePickerDebut";
            this.datePickerDebut.Size = new System.Drawing.Size(110, 25);
            this.datePickerDebut.TabIndex = 1;
            //
            // labelResultat
            //
            this.labelResultat.AutoSize = true;
            this.labelResultat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelResultat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelResultat.Location = new System.Drawing.Point(630, 30);
            this.labelResultat.Name = "labelResultat";
            this.labelResultat.Size = new System.Drawing.Size(62, 19);
            this.labelResultat.TabIndex = 6;
            this.labelResultat.Text = "Résultat:";
            //
            // labelModule
            //
            this.labelModule.AutoSize = true;
            this.labelModule.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelModule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelModule.Location = new System.Drawing.Point(430, 30);
            this.labelModule.Name = "labelModule";
            this.labelModule.Size = new System.Drawing.Size(60, 19);
            this.labelModule.TabIndex = 4;
            this.labelModule.Text = "Module:";
            //
            // labelPeriode
            //
            this.labelPeriode.AutoSize = true;
            this.labelPeriode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelPeriode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelPeriode.Location = new System.Drawing.Point(20, 30);
            this.labelPeriode.Name = "labelPeriode";
            this.labelPeriode.Size = new System.Drawing.Size(64, 19);
            this.labelPeriode.TabIndex = 0;
            this.labelPeriode.Text = "Période :";
            //
            // panelRecherche
            //
            this.panelRecherche.BackColor = System.Drawing.Color.White;
            this.panelRecherche.Controls.Add(this.textBoxRecherche);
            this.panelRecherche.Controls.Add(this.labelRecherche);
            this.panelRecherche.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRecherche.Location = new System.Drawing.Point(0, 220);
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
            this.textBoxRecherche.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxRecherche.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxRecherche.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxRecherche.Location = new System.Drawing.Point(120, 13);
            this.textBoxRecherche.Name = "textBoxRecherche";
            this.textBoxRecherche.PlaceholderText = "Rechercher par utilisateur, action ou détails...";
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
            this.labelRecherche.Text = "🔍 Recherche:";
            //
            // dataGridViewLogs
            //
            this.dataGridViewLogs.AllowUserToAddRows = false;
            this.dataGridViewLogs.AllowUserToDeleteRows = false;
            this.dataGridViewLogs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewLogs.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colDate,
            this.colUtilisateur,
            this.colModule,
            this.colAction,
            this.colDetails,
            this.colIP,
            this.colResultat});
            this.dataGridViewLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewLogs.Location = new System.Drawing.Point(0, 280);
            this.dataGridViewLogs.Name = "dataGridViewLogs";
            this.dataGridViewLogs.ReadOnly = true;
            this.dataGridViewLogs.RowHeadersWidth = 51;
            this.dataGridViewLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewLogs.Size = new System.Drawing.Size(1200, 510);
            this.dataGridViewLogs.TabIndex = 3;
            //
            // colId
            //
            this.colId.DataPropertyName = "id";
            this.colId.FillWeight = 30F;
            this.colId.HeaderText = "ID";
            this.colId.MinimumWidth = 6;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            //
            // colDate
            //
            this.colDate.DataPropertyName = "date_action";
            this.colDate.FillWeight = 80F;
            this.colDate.HeaderText = "Date/Heure";
            this.colDate.MinimumWidth = 6;
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            //
            // colUtilisateur
            //
            this.colUtilisateur.DataPropertyName = "nom_utilisateur";
            this.colUtilisateur.FillWeight = 60F;
            this.colUtilisateur.HeaderText = "Utilisateur";
            this.colUtilisateur.MinimumWidth = 6;
            this.colUtilisateur.Name = "colUtilisateur";
            this.colUtilisateur.ReadOnly = true;
            //
            // colModule
            //
            this.colModule.DataPropertyName = "module";
            this.colModule.FillWeight = 50F;
            this.colModule.HeaderText = "Module";
            this.colModule.MinimumWidth = 6;
            this.colModule.Name = "colModule";
            this.colModule.ReadOnly = true;
            //
            // colAction
            //
            this.colAction.DataPropertyName = "action";
            this.colAction.FillWeight = 80F;
            this.colAction.HeaderText = "Action";
            this.colAction.MinimumWidth = 6;
            this.colAction.Name = "colAction";
            this.colAction.ReadOnly = true;
            //
            // colDetails
            //
            this.colDetails.DataPropertyName = "details";
            this.colDetails.FillWeight = 150F;
            this.colDetails.HeaderText = "Détails";
            this.colDetails.MinimumWidth = 6;
            this.colDetails.Name = "colDetails";
            this.colDetails.ReadOnly = true;
            //
            // colIP
            //
            this.colIP.DataPropertyName = "ip_address";
            this.colIP.FillWeight = 50F;
            this.colIP.HeaderText = "IP";
            this.colIP.MinimumWidth = 6;
            this.colIP.Name = "colIP";
            this.colIP.ReadOnly = true;
            //
            // colResultat
            //
            this.colResultat.DataPropertyName = "resultat";
            this.colResultat.FillWeight = 50F;
            this.colResultat.HeaderText = "Résultat";
            this.colResultat.MinimumWidth = 6;
            this.colResultat.Name = "colResultat";
            this.colResultat.ReadOnly = true;
            //
            // panelBottom
            //
            this.panelBottom.BackColor = System.Drawing.Color.White;
            this.panelBottom.Controls.Add(this.labelNombreLogs);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 750);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelBottom.Size = new System.Drawing.Size(1200, 50);
            this.panelBottom.TabIndex = 4;
            //
            // labelNombreLogs
            //
            this.labelNombreLogs.AutoSize = true;
            this.labelNombreLogs.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.labelNombreLogs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelNombreLogs.Location = new System.Drawing.Point(20, 15);
            this.labelNombreLogs.Name = "labelNombreLogs";
            this.labelNombreLogs.Size = new System.Drawing.Size(192, 19);
            this.labelNombreLogs.TabIndex = 0;
            this.labelNombreLogs.Text = "Total : 0 log(s) (limité à 1000)";
            //
            // VisualisationLogsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.dataGridViewLogs);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelRecherche);
            this.Controls.Add(this.panelFiltres);
            this.Controls.Add(this.headerEntreprise);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VisualisationLogsForm";
            this.Text = "Logs d\'Activité";
            this.panelFiltres.ResumeLayout(false);
            this.panelFiltres.PerformLayout();
            this.panelRecherche.ResumeLayout(false);
            this.panelRecherche.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLogs)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        private HeaderEntreprise headerEntreprise;
        private System.Windows.Forms.Panel panelFiltres;
        private System.Windows.Forms.Label labelPeriode;
        private System.Windows.Forms.DateTimePicker datePickerDebut;
        private System.Windows.Forms.DateTimePicker datePickerFin;
        private System.Windows.Forms.Label labelModule;
        private System.Windows.Forms.ComboBox comboBoxModule;
        private System.Windows.Forms.Label labelResultat;
        private System.Windows.Forms.ComboBox comboBoxResultat;
        private Guna.UI2.WinForms.Guna2Button buttonFiltrer;
        private Guna.UI2.WinForms.Guna2Button buttonActualiser;
        private Guna.UI2.WinForms.Guna2Button buttonExporter;
        private System.Windows.Forms.Panel panelRecherche;
        private Guna.UI2.WinForms.Guna2TextBox textBoxRecherche;
        private System.Windows.Forms.Label labelRecherche;
        private System.Windows.Forms.DataGridView dataGridViewLogs;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label labelNombreLogs;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUtilisateur;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResultat;
    }
}
