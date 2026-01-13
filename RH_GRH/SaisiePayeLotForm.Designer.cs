namespace RH_GRH
{
    partial class SaisiePayeLotForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.labelSousTitre = new System.Windows.Forms.Label();
            this.labelTitre = new System.Windows.Forms.Label();
            this.panelFooter = new Guna.UI2.WinForms.Guna2Panel();
            this.buttonAnnuler = new Guna.UI2.WinForms.Guna2Button();
            this.buttonGenerer = new Guna.UI2.WinForms.Guna2Button();
            this.panelStatistiques = new Guna.UI2.WinForms.Guna2Panel();
            this.labelNombreEmployes = new System.Windows.Forms.Label();
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.cardDataGrid = new Guna.UI2.WinForms.Guna2Panel();
            this.dataGridViewEmployes = new Guna.UI2.WinForms.Guna2DataGridView();
            this.panelProgression = new Guna.UI2.WinForms.Guna2Panel();
            this.labelProgression = new System.Windows.Forms.Label();
            this.guna2ProgressBar1 = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.panelInfoBanner = new Guna.UI2.WinForms.Guna2Panel();
            this.labelInfo = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelStatistiques.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.cardDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployes)).BeginInit();
            this.panelProgression.SuspendLayout();
            this.panelInfoBanner.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.Transparent;
            this.panelHeader.Controls.Add(this.labelSousTitre);
            this.panelHeader.Controls.Add(this.labelTitre);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.FillColor = System.Drawing.Color.MidnightBlue;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.ShadowDecoration.BorderRadius = 0;
            this.panelHeader.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(50)))));
            this.panelHeader.ShadowDecoration.Depth = 8;
            this.panelHeader.ShadowDecoration.Enabled = true;
            this.panelHeader.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panelHeader.Size = new System.Drawing.Size(1333, 80);
            this.panelHeader.TabIndex = 0;
            // 
            // labelSousTitre
            // 
            this.labelSousTitre.AutoSize = true;
            this.labelSousTitre.BackColor = System.Drawing.Color.Transparent;
            this.labelSousTitre.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSousTitre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(220)))));
            this.labelSousTitre.Location = new System.Drawing.Point(29, 53);
            this.labelSousTitre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSousTitre.Name = "labelSousTitre";
            this.labelSousTitre.Size = new System.Drawing.Size(394, 22);
            this.labelSousTitre.TabIndex = 1;
            this.labelSousTitre.Text = "Saisissez les données de paie pour générer les bulletins";
            // 
            // labelTitre
            // 
            this.labelTitre.AutoSize = true;
            this.labelTitre.BackColor = System.Drawing.Color.Transparent;
            this.labelTitre.Font = new System.Drawing.Font("Montserrat", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitre.ForeColor = System.Drawing.Color.White;
            this.labelTitre.Location = new System.Drawing.Point(27, 18);
            this.labelTitre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(286, 38);
            this.labelTitre.TabIndex = 0;
            this.labelTitre.Text = "Saisie de Paie par Lot";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.Transparent;
            this.panelFooter.Controls.Add(this.buttonAnnuler);
            this.panelFooter.Controls.Add(this.buttonGenerer);
            this.panelFooter.Controls.Add(this.panelStatistiques);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.panelFooter.Location = new System.Drawing.Point(0, 695);
            this.panelFooter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.panelFooter.ShadowDecoration.BorderRadius = 0;
            this.panelFooter.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(30)))));
            this.panelFooter.ShadowDecoration.Depth = 8;
            this.panelFooter.ShadowDecoration.Enabled = true;
            this.panelFooter.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, -5, 0, 0);
            this.panelFooter.Size = new System.Drawing.Size(1333, 80);
            this.panelFooter.TabIndex = 1;
            // 
            // buttonAnnuler
            // 
            this.buttonAnnuler.Animated = true;
            this.buttonAnnuler.BorderRadius = 4;
            this.buttonAnnuler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAnnuler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAnnuler.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonAnnuler.FillColor = System.Drawing.Color.Gray;
            this.buttonAnnuler.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.White;
            this.buttonAnnuler.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.buttonAnnuler.Location = new System.Drawing.Point(1027, 15);
            this.buttonAnnuler.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(113, 50);
            this.buttonAnnuler.TabIndex = 2;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            // 
            // buttonGenerer
            // 
            this.buttonGenerer.Animated = true;
            this.buttonGenerer.BorderRadius = 4;
            this.buttonGenerer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonGenerer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonGenerer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonGenerer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonGenerer.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonGenerer.FillColor = System.Drawing.Color.SeaGreen;
            this.buttonGenerer.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold);
            this.buttonGenerer.ForeColor = System.Drawing.Color.White;
            this.buttonGenerer.HoverState.FillColor = System.Drawing.Color.ForestGreen;
            this.buttonGenerer.Location = new System.Drawing.Point(1140, 15);
            this.buttonGenerer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonGenerer.Name = "buttonGenerer";
            this.buttonGenerer.Size = new System.Drawing.Size(173, 50);
            this.buttonGenerer.TabIndex = 1;
            this.buttonGenerer.Text = "Générer PDF";
            // 
            // panelStatistiques
            // 
            this.panelStatistiques.BackColor = System.Drawing.Color.Transparent;
            this.panelStatistiques.BorderColor = System.Drawing.Color.MidnightBlue;
            this.panelStatistiques.BorderRadius = 4;
            this.panelStatistiques.BorderThickness = 2;
            this.panelStatistiques.Controls.Add(this.labelNombreEmployes);
            this.panelStatistiques.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelStatistiques.FillColor = System.Drawing.Color.White;
            this.panelStatistiques.Location = new System.Drawing.Point(20, 15);
            this.panelStatistiques.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelStatistiques.Name = "panelStatistiques";
            this.panelStatistiques.Padding = new System.Windows.Forms.Padding(16, 7, 16, 7);
            this.panelStatistiques.Size = new System.Drawing.Size(293, 50);
            this.panelStatistiques.TabIndex = 0;
            // 
            // labelNombreEmployes
            // 
            this.labelNombreEmployes.AutoSize = true;
            this.labelNombreEmployes.BackColor = System.Drawing.Color.Transparent;
            this.labelNombreEmployes.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelNombreEmployes.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNombreEmployes.ForeColor = System.Drawing.Color.MidnightBlue;
            this.labelNombreEmployes.Location = new System.Drawing.Point(16, 7);
            this.labelNombreEmployes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNombreEmployes.Name = "labelNombreEmployes";
            this.labelNombreEmployes.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.labelNombreEmployes.Size = new System.Drawing.Size(28, 36);
            this.labelNombreEmployes.TabIndex = 0;
            this.labelNombreEmployes.Text = "0";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMain.Controls.Add(this.cardDataGrid);
            this.panelMain.Controls.Add(this.panelProgression);
            this.panelMain.Controls.Add(this.panelInfoBanner);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 80);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.panelMain.Size = new System.Drawing.Size(1333, 615);
            this.panelMain.TabIndex = 2;
            // 
            // cardDataGrid
            // 
            this.cardDataGrid.BackColor = System.Drawing.Color.Transparent;
            this.cardDataGrid.BorderRadius = 6;
            this.cardDataGrid.Controls.Add(this.dataGridViewEmployes);
            this.cardDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardDataGrid.FillColor = System.Drawing.Color.White;
            this.cardDataGrid.Location = new System.Drawing.Point(20, 65);
            this.cardDataGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cardDataGrid.Name = "cardDataGrid";
            this.cardDataGrid.Padding = new System.Windows.Forms.Padding(1);
            this.cardDataGrid.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.cardDataGrid.ShadowDecoration.Depth = 8;
            this.cardDataGrid.ShadowDecoration.Enabled = true;
            this.cardDataGrid.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.cardDataGrid.Size = new System.Drawing.Size(1293, 470);
            this.cardDataGrid.TabIndex = 1;
            // 
            // dataGridViewEmployes
            // 
            this.dataGridViewEmployes.AllowUserToAddRows = false;
            this.dataGridViewEmployes.AllowUserToDeleteRows = false;
            this.dataGridViewEmployes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Montserrat", 8.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewEmployes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewEmployes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewEmployes.ColumnHeadersHeight = 40;
            this.dataGridViewEmployes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Montserrat", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewEmployes.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewEmployes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewEmployes.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dataGridViewEmployes.Location = new System.Drawing.Point(1, 1);
            this.dataGridViewEmployes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewEmployes.Name = "dataGridViewEmployes";
            this.dataGridViewEmployes.RowHeadersVisible = false;
            this.dataGridViewEmployes.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Montserrat", 8.5F);
            this.dataGridViewEmployes.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewEmployes.RowTemplate.Height = 35;
            this.dataGridViewEmployes.Size = new System.Drawing.Size(1291, 468);
            this.dataGridViewEmployes.TabIndex = 0;
            this.dataGridViewEmployes.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dataGridViewEmployes.ThemeStyle.AlternatingRowsStyle.Font = new System.Drawing.Font("Montserrat", 9F);
            this.dataGridViewEmployes.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewEmployes.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            this.dataGridViewEmployes.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewEmployes.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dataGridViewEmployes.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dataGridViewEmployes.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.MidnightBlue;
            this.dataGridViewEmployes.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewEmployes.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.dataGridViewEmployes.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridViewEmployes.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dataGridViewEmployes.ThemeStyle.HeaderStyle.Height = 40;
            this.dataGridViewEmployes.ThemeStyle.ReadOnly = false;
            this.dataGridViewEmployes.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dataGridViewEmployes.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewEmployes.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Montserrat", 9F);
            this.dataGridViewEmployes.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewEmployes.ThemeStyle.RowsStyle.Height = 35;
            this.dataGridViewEmployes.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            this.dataGridViewEmployes.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            // 
            // panelProgression
            // 
            this.panelProgression.BackColor = System.Drawing.Color.Transparent;
            this.panelProgression.BorderRadius = 4;
            this.panelProgression.Controls.Add(this.labelProgression);
            this.panelProgression.Controls.Add(this.guna2ProgressBar1);
            this.panelProgression.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelProgression.FillColor = System.Drawing.Color.White;
            this.panelProgression.Location = new System.Drawing.Point(20, 535);
            this.panelProgression.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelProgression.Name = "panelProgression";
            this.panelProgression.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.panelProgression.ShadowDecoration.BorderRadius = 4;
            this.panelProgression.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.panelProgression.ShadowDecoration.Depth = 5;
            this.panelProgression.ShadowDecoration.Enabled = true;
            this.panelProgression.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 1, 0, 2);
            this.panelProgression.Size = new System.Drawing.Size(1293, 62);
            this.panelProgression.TabIndex = 2;
            this.panelProgression.Visible = false;
            // 
            // labelProgression
            // 
            this.labelProgression.AutoSize = true;
            this.labelProgression.BackColor = System.Drawing.Color.Transparent;
            this.labelProgression.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelProgression.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProgression.ForeColor = System.Drawing.Color.Gray;
            this.labelProgression.Location = new System.Drawing.Point(16, 10);
            this.labelProgression.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelProgression.Name = "labelProgression";
            this.labelProgression.Padding = new System.Windows.Forms.Padding(0, 2, 0, 5);
            this.labelProgression.Size = new System.Drawing.Size(186, 29);
            this.labelProgression.TabIndex = 1;
            this.labelProgression.Text = "Génération en cours... 0%";
            // 
            // guna2ProgressBar1
            // 
            this.guna2ProgressBar1.BorderRadius = 3;
            this.guna2ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2ProgressBar1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.guna2ProgressBar1.Location = new System.Drawing.Point(16, 45);
            this.guna2ProgressBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.guna2ProgressBar1.Name = "guna2ProgressBar1";
            this.guna2ProgressBar1.ProgressColor = System.Drawing.Color.SeaGreen;
            this.guna2ProgressBar1.ProgressColor2 = System.Drawing.Color.MediumSeaGreen;
            this.guna2ProgressBar1.Size = new System.Drawing.Size(1261, 7);
            this.guna2ProgressBar1.TabIndex = 0;
            this.guna2ProgressBar1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // panelInfoBanner
            // 
            this.panelInfoBanner.BackColor = System.Drawing.Color.Transparent;
            this.panelInfoBanner.BorderRadius = 4;
            this.panelInfoBanner.Controls.Add(this.labelInfo);
            this.panelInfoBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInfoBanner.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelInfoBanner.Location = new System.Drawing.Point(20, 18);
            this.panelInfoBanner.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelInfoBanner.Name = "panelInfoBanner";
            this.panelInfoBanner.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.panelInfoBanner.Size = new System.Drawing.Size(1293, 47);
            this.panelInfoBanner.TabIndex = 0;
            // 
            // labelInfo
            // 
            this.labelInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInfo.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.labelInfo.Location = new System.Drawing.Point(16, 10);
            this.labelInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(1261, 27);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "ℹ️  Remplissez les informations de paie pour chaque employé, puis cliquez sur \"Gé" +
    "nérer PDF\"";
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SaisiePayeLotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1333, 775);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaisiePayeLotForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Saisie de Paie par Lot";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelStatistiques.ResumeLayout(false);
            this.panelStatistiques.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.cardDataGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployes)).EndInit();
            this.panelProgression.ResumeLayout(false);
            this.panelProgression.PerformLayout();
            this.panelInfoBanner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelHeader;
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.Label labelSousTitre;
        private Guna.UI2.WinForms.Guna2Panel panelFooter;
        private Guna.UI2.WinForms.Guna2Panel panelStatistiques;
        private System.Windows.Forms.Label labelNombreEmployes;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
        private Guna.UI2.WinForms.Guna2Button buttonGenerer;
        private Guna.UI2.WinForms.Guna2Panel panelMain;
        private Guna.UI2.WinForms.Guna2Panel cardDataGrid;
        private Guna.UI2.WinForms.Guna2DataGridView dataGridViewEmployes;
        private Guna.UI2.WinForms.Guna2Panel panelInfoBanner;
        private System.Windows.Forms.Label labelInfo;
        private Guna.UI2.WinForms.Guna2Panel panelProgression;
        private Guna.UI2.WinForms.Guna2ProgressBar guna2ProgressBar1;
        private System.Windows.Forms.Label labelProgression;
    }
}
