namespace RH_GRH
{
    partial class ImportEmployeForm
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelTitre = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.buttonImporter = new Guna.UI2.WinForms.Guna2Button();
            this.buttonAnnuler = new Guna.UI2.WinForms.Guna2Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.groupBoxPreview = new System.Windows.Forms.GroupBox();
            this.dataGridViewPreview = new System.Windows.Forms.DataGridView();
            this.progressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.labelProgres = new System.Windows.Forms.Label();
            this.groupBoxFichier = new System.Windows.Forms.GroupBox();
            this.buttonTelechargerModele = new Guna.UI2.WinForms.Guna2Button();
            this.buttonParcourir = new Guna.UI2.WinForms.Guna2Button();
            this.textBoxFichier = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelInstruction = new System.Windows.Forms.Label();
            this.panelStats = new System.Windows.Forms.Panel();
            this.labelTotalEmployes = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelSucces = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelErreurs = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.groupBoxPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPreview)).BeginInit();
            this.groupBoxFichier.SuspendLayout();
            this.panelStats.SuspendLayout();
            this.SuspendLayout();
            //
            // panelHeader
            //
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.panelHeader.Controls.Add(this.labelTitre);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1200, 70);
            this.panelHeader.TabIndex = 0;
            //
            // labelTitre
            //
            this.labelTitre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitre.Font = new System.Drawing.Font("Montserrat", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitre.ForeColor = System.Drawing.Color.White;
            this.labelTitre.Location = new System.Drawing.Point(0, 0);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(1200, 70);
            this.labelTitre.TabIndex = 0;
            this.labelTitre.Text = "IMPORTATION D\'EMPLOYÉS PAR LOT";
            this.labelTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // panelFooter
            //
            this.panelFooter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelFooter.Controls.Add(this.buttonImporter);
            this.panelFooter.Controls.Add(this.buttonAnnuler);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 714);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1200, 86);
            this.panelFooter.TabIndex = 1;
            //
            // buttonImporter
            //
            this.buttonImporter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonImporter.BorderRadius = 0;
            this.buttonImporter.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonImporter.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonImporter.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonImporter.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonImporter.Enabled = false;
            this.buttonImporter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.buttonImporter.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonImporter.ForeColor = System.Drawing.Color.White;
            this.buttonImporter.Location = new System.Drawing.Point(620, 18);
            this.buttonImporter.Name = "buttonImporter";
            this.buttonImporter.Size = new System.Drawing.Size(200, 49);
            this.buttonImporter.TabIndex = 0;
            this.buttonImporter.Text = "📥 Importer";
            this.buttonImporter.Click += new System.EventHandler(this.buttonImporter_Click);
            //
            // buttonAnnuler
            //
            this.buttonAnnuler.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAnnuler.BorderRadius = 0;
            this.buttonAnnuler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAnnuler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAnnuler.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.buttonAnnuler.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.White;
            this.buttonAnnuler.Location = new System.Drawing.Point(380, 18);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(200, 49);
            this.buttonAnnuler.TabIndex = 1;
            this.buttonAnnuler.Text = "Fermer";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            //
            // panelMain
            //
            this.panelMain.AutoScroll = true;
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.groupBoxPreview);
            this.panelMain.Controls.Add(this.groupBoxFichier);
            this.panelMain.Controls.Add(this.panelStats);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 70);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(1200, 644);
            this.panelMain.TabIndex = 2;
            //
            // groupBoxPreview
            //
            this.groupBoxPreview.Controls.Add(this.dataGridViewPreview);
            this.groupBoxPreview.Controls.Add(this.progressBar);
            this.groupBoxPreview.Controls.Add(this.labelProgres);
            this.groupBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPreview.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.groupBoxPreview.Location = new System.Drawing.Point(20, 220);
            this.groupBoxPreview.Name = "groupBoxPreview";
            this.groupBoxPreview.Padding = new System.Windows.Forms.Padding(15);
            this.groupBoxPreview.Size = new System.Drawing.Size(1160, 324);
            this.groupBoxPreview.TabIndex = 2;
            this.groupBoxPreview.TabStop = false;
            this.groupBoxPreview.Text = "Aperçu des données";
            //
            // dataGridViewPreview
            //
            this.dataGridViewPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPreview.Location = new System.Drawing.Point(15, 29);
            this.dataGridViewPreview.Name = "dataGridViewPreview";
            this.dataGridViewPreview.RowTemplate.Height = 24;
            this.dataGridViewPreview.Size = new System.Drawing.Size(1130, 227);
            this.dataGridViewPreview.TabIndex = 0;
            //
            // progressBar
            //
            this.progressBar.BorderRadius = 0;
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.progressBar.Location = new System.Drawing.Point(15, 256);
            this.progressBar.Name = "progressBar";
            this.progressBar.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.progressBar.ProgressColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.progressBar.Size = new System.Drawing.Size(1130, 20);
            this.progressBar.TabIndex = 2;
            this.progressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.progressBar.Visible = false;
            //
            // labelProgres
            //
            this.labelProgres.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelProgres.Font = new System.Drawing.Font("Montserrat", 9F);
            this.labelProgres.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelProgres.Location = new System.Drawing.Point(15, 276);
            this.labelProgres.Name = "labelProgres";
            this.labelProgres.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.labelProgres.Size = new System.Drawing.Size(1130, 33);
            this.labelProgres.TabIndex = 1;
            this.labelProgres.Text = "Sélectionnez un fichier CSV ou Excel pour commencer";
            this.labelProgres.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelProgres.Visible = false;
            //
            // groupBoxFichier
            //
            this.groupBoxFichier.Controls.Add(this.buttonTelechargerModele);
            this.groupBoxFichier.Controls.Add(this.buttonParcourir);
            this.groupBoxFichier.Controls.Add(this.textBoxFichier);
            this.groupBoxFichier.Controls.Add(this.labelInstruction);
            this.groupBoxFichier.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxFichier.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxFichier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.groupBoxFichier.Location = new System.Drawing.Point(20, 100);
            this.groupBoxFichier.Name = "groupBoxFichier";
            this.groupBoxFichier.Padding = new System.Windows.Forms.Padding(15);
            this.groupBoxFichier.Size = new System.Drawing.Size(1160, 120);
            this.groupBoxFichier.TabIndex = 0;
            this.groupBoxFichier.TabStop = false;
            this.groupBoxFichier.Text = "Sélection du fichier";
            //
            // buttonTelechargerModele
            //
            this.buttonTelechargerModele.BorderRadius = 0;
            this.buttonTelechargerModele.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonTelechargerModele.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonTelechargerModele.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonTelechargerModele.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonTelechargerModele.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.buttonTelechargerModele.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold);
            this.buttonTelechargerModele.ForeColor = System.Drawing.Color.White;
            this.buttonTelechargerModele.Location = new System.Drawing.Point(930, 67);
            this.buttonTelechargerModele.Name = "buttonTelechargerModele";
            this.buttonTelechargerModele.Size = new System.Drawing.Size(200, 38);
            this.buttonTelechargerModele.TabIndex = 3;
            this.buttonTelechargerModele.Text = "📄 Télécharger modèle";
            this.buttonTelechargerModele.Click += new System.EventHandler(this.buttonTelechargerModele_Click);
            //
            // buttonParcourir
            //
            this.buttonParcourir.BorderRadius = 0;
            this.buttonParcourir.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonParcourir.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonParcourir.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonParcourir.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonParcourir.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.buttonParcourir.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold);
            this.buttonParcourir.ForeColor = System.Drawing.Color.White;
            this.buttonParcourir.Location = new System.Drawing.Point(724, 67);
            this.buttonParcourir.Name = "buttonParcourir";
            this.buttonParcourir.Size = new System.Drawing.Size(180, 38);
            this.buttonParcourir.TabIndex = 2;
            this.buttonParcourir.Text = "📁 Parcourir...";
            this.buttonParcourir.Click += new System.EventHandler(this.buttonParcourir_Click);
            //
            // textBoxFichier
            //
            this.textBoxFichier.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxFichier.BorderRadius = 0;
            this.textBoxFichier.BorderThickness = 2;
            this.textBoxFichier.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxFichier.DefaultText = "";
            this.textBoxFichier.Font = new System.Drawing.Font("Montserrat", 9F);
            this.textBoxFichier.ForeColor = System.Drawing.Color.Black;
            this.textBoxFichier.Location = new System.Drawing.Point(18, 67);
            this.textBoxFichier.Name = "textBoxFichier";
            this.textBoxFichier.PlaceholderText = "Aucun fichier sélectionné";
            this.textBoxFichier.ReadOnly = true;
            this.textBoxFichier.SelectedText = "";
            this.textBoxFichier.Size = new System.Drawing.Size(680, 38);
            this.textBoxFichier.TabIndex = 1;
            //
            // labelInstruction
            //
            this.labelInstruction.AutoSize = true;
            this.labelInstruction.Font = new System.Drawing.Font("Montserrat", 9F);
            this.labelInstruction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelInstruction.Location = new System.Drawing.Point(18, 32);
            this.labelInstruction.Name = "labelInstruction";
            this.labelInstruction.Size = new System.Drawing.Size(850, 21);
            this.labelInstruction.TabIndex = 0;
            this.labelInstruction.Text = "Sélectionnez un fichier CSV ou Excel (.csv, .xls ou .xlsx) contenant les données" +
    " des employés à importer.";
            //
            // panelStats
            //
            this.panelStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStats.Controls.Add(this.labelErreurs);
            this.panelStats.Controls.Add(this.label5);
            this.panelStats.Controls.Add(this.labelSucces);
            this.panelStats.Controls.Add(this.label3);
            this.panelStats.Controls.Add(this.labelTotalEmployes);
            this.panelStats.Controls.Add(this.label1);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStats.Location = new System.Drawing.Point(20, 20);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(1160, 80);
            this.panelStats.TabIndex = 3;
            //
            // labelTotalEmployes
            //
            this.labelTotalEmployes.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Bold);
            this.labelTotalEmployes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.labelTotalEmployes.Location = new System.Drawing.Point(20, 5);
            this.labelTotalEmployes.Name = "labelTotalEmployes";
            this.labelTotalEmployes.Size = new System.Drawing.Size(350, 40);
            this.labelTotalEmployes.TabIndex = 0;
            this.labelTotalEmployes.Text = "0";
            this.labelTotalEmployes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // label1
            //
            this.label1.Font = new System.Drawing.Font("Montserrat", 8F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(20, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(350, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Total employés";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // labelSucces
            //
            this.labelSucces.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Bold);
            this.labelSucces.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.labelSucces.Location = new System.Drawing.Point(400, 5);
            this.labelSucces.Name = "labelSucces";
            this.labelSucces.Size = new System.Drawing.Size(350, 40);
            this.labelSucces.TabIndex = 2;
            this.labelSucces.Text = "0";
            this.labelSucces.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // label3
            //
            this.label3.Font = new System.Drawing.Font("Montserrat", 8F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(400, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(350, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Importés avec succès";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // labelErreurs
            //
            this.labelErreurs.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Bold);
            this.labelErreurs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.labelErreurs.Location = new System.Drawing.Point(780, 5);
            this.labelErreurs.Name = "labelErreurs";
            this.labelErreurs.Size = new System.Drawing.Size(350, 40);
            this.labelErreurs.TabIndex = 4;
            this.labelErreurs.Text = "0";
            this.labelErreurs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // label5
            //
            this.label5.Font = new System.Drawing.Font("Montserrat", 8F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(780, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(350, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Erreurs";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // ImportEmployeForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportEmployeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Importation d\'employés";
            this.panelHeader.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.groupBoxPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPreview)).EndInit();
            this.groupBoxFichier.ResumeLayout(false);
            this.groupBoxFichier.PerformLayout();
            this.panelStats.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.Panel panelFooter;
        private Guna.UI2.WinForms.Guna2Button buttonImporter;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.GroupBox groupBoxFichier;
        private System.Windows.Forms.Label labelInstruction;
        private Guna.UI2.WinForms.Guna2TextBox textBoxFichier;
        private Guna.UI2.WinForms.Guna2Button buttonParcourir;
        private System.Windows.Forms.GroupBox groupBoxPreview;
        private System.Windows.Forms.DataGridView dataGridViewPreview;
        private System.Windows.Forms.Label labelProgres;
        private Guna.UI2.WinForms.Guna2ProgressBar progressBar;
        private Guna.UI2.WinForms.Guna2Button buttonTelechargerModele;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label labelTotalEmployes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelErreurs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelSucces;
        private System.Windows.Forms.Label label3;
    }
}
