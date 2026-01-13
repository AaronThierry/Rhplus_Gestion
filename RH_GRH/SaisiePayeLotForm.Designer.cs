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
            this.panelButtonsContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.buttonAnnuler = new Guna.UI2.WinForms.Guna2Button();
            this.panelSpacer = new Guna.UI2.WinForms.Guna2Panel();
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
            this.panelButtonsContainer.SuspendLayout();
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
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
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
            this.panelFooter.Controls.Add(this.panelButtonsContainer);
            this.panelFooter.Controls.Add(this.panelStatistiques);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelFooter.Location = new System.Drawing.Point(0, 687);
            this.panelFooter.Margin = new System.Windows.Forms.Padding(4);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(24, 18, 24, 18);
            this.panelFooter.ShadowDecoration.BorderRadius = 0;
            this.panelFooter.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(40)))));
            this.panelFooter.ShadowDecoration.Depth = 12;
            this.panelFooter.ShadowDecoration.Enabled = true;
            this.panelFooter.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, -6, 0, 0);
            this.panelFooter.Size = new System.Drawing.Size(1333, 88);
            this.panelFooter.TabIndex = 1;
            // 
            // panelButtonsContainer
            // 
            this.panelButtonsContainer.BackColor = System.Drawing.Color.Transparent;
            this.panelButtonsContainer.Controls.Add(this.buttonAnnuler);
            this.panelButtonsContainer.Controls.Add(this.panelSpacer);
            this.panelButtonsContainer.Controls.Add(this.buttonGenerer);
            this.panelButtonsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtonsContainer.FillColor = System.Drawing.Color.Transparent;
            this.panelButtonsContainer.Location = new System.Drawing.Point(384, 18);
            this.panelButtonsContainer.Name = "panelButtonsContainer";
            this.panelButtonsContainer.Size = new System.Drawing.Size(925, 52);
            this.panelButtonsContainer.TabIndex = 4;
            // 
            // buttonAnnuler
            // 
            this.buttonAnnuler.Animated = true;
            this.buttonAnnuler.BackColor = System.Drawing.Color.Transparent;
            this.buttonAnnuler.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.buttonAnnuler.BorderRadius = 10;
            this.buttonAnnuler.BorderThickness = 1;
            this.buttonAnnuler.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.buttonAnnuler.DisabledState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.buttonAnnuler.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.buttonAnnuler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.buttonAnnuler.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonAnnuler.FillColor = System.Drawing.Color.White;
            this.buttonAnnuler.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(99)))), ((int)(((byte)(104)))));
            this.buttonAnnuler.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.buttonAnnuler.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.buttonAnnuler.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.buttonAnnuler.Location = new System.Drawing.Point(469, 0);
            this.buttonAnnuler.Margin = new System.Windows.Forms.Padding(4, 4, 20, 4);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.buttonAnnuler.ShadowDecoration.BorderRadius = 10;
            this.buttonAnnuler.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(35)))));
            this.buttonAnnuler.ShadowDecoration.Depth = 12;
            this.buttonAnnuler.ShadowDecoration.Enabled = true;
            this.buttonAnnuler.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.buttonAnnuler.Size = new System.Drawing.Size(208, 52);
            this.buttonAnnuler.TabIndex = 2;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            // 
            // panelSpacer
            // 
            this.panelSpacer.BackColor = System.Drawing.Color.Transparent;
            this.panelSpacer.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSpacer.FillColor = System.Drawing.Color.Transparent;
            this.panelSpacer.Location = new System.Drawing.Point(677, 0);
            this.panelSpacer.Name = "panelSpacer";
            this.panelSpacer.Size = new System.Drawing.Size(20, 52);
            this.panelSpacer.TabIndex = 3;
            // 
            // buttonGenerer
            // 
            this.buttonGenerer.Animated = true;
            this.buttonGenerer.AnimatedGIF = true;
            this.buttonGenerer.BackColor = System.Drawing.Color.Transparent;
            this.buttonGenerer.BorderRadius = 10;
            this.buttonGenerer.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.buttonGenerer.DisabledState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.buttonGenerer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.buttonGenerer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.buttonGenerer.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonGenerer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.buttonGenerer.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonGenerer.ForeColor = System.Drawing.Color.White;
            this.buttonGenerer.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(142)))), ((int)(((byte)(60)))));
            this.buttonGenerer.Location = new System.Drawing.Point(697, 0);
            this.buttonGenerer.Margin = new System.Windows.Forms.Padding(4);
            this.buttonGenerer.Name = "buttonGenerer";
            this.buttonGenerer.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.buttonGenerer.PressedDepth = 20;
            this.buttonGenerer.ShadowDecoration.BorderRadius = 10;
            this.buttonGenerer.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))), ((int)(((byte)(80)))));
            this.buttonGenerer.ShadowDecoration.Depth = 15;
            this.buttonGenerer.ShadowDecoration.Enabled = true;
            this.buttonGenerer.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 3, 0, 6);
            this.buttonGenerer.Size = new System.Drawing.Size(228, 52);
            this.buttonGenerer.TabIndex = 1;
            this.buttonGenerer.Text = "📄 Générer PDF";
            this.buttonGenerer.Click += new System.EventHandler(this.buttonGenerer_Click);
            // 
            // panelStatistiques
            // 
            this.panelStatistiques.BackColor = System.Drawing.Color.Transparent;
            this.panelStatistiques.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(149)))), ((int)(((byte)(237)))));
            this.panelStatistiques.BorderRadius = 10;
            this.panelStatistiques.BorderThickness = 1;
            this.panelStatistiques.Controls.Add(this.labelNombreEmployes);
            this.panelStatistiques.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelStatistiques.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelStatistiques.Location = new System.Drawing.Point(24, 18);
            this.panelStatistiques.Margin = new System.Windows.Forms.Padding(4);
            this.panelStatistiques.Name = "panelStatistiques";
            this.panelStatistiques.Padding = new System.Windows.Forms.Padding(18, 12, 18, 12);
            this.panelStatistiques.ShadowDecoration.BorderRadius = 10;
            this.panelStatistiques.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(133)))), ((int)(((byte)(244)))), ((int)(((byte)(60)))));
            this.panelStatistiques.ShadowDecoration.Depth = 10;
            this.panelStatistiques.ShadowDecoration.Enabled = true;
            this.panelStatistiques.Size = new System.Drawing.Size(360, 52);
            this.panelStatistiques.TabIndex = 0;
            // 
            // labelNombreEmployes
            // 
            this.labelNombreEmployes.AutoSize = true;
            this.labelNombreEmployes.BackColor = System.Drawing.Color.Transparent;
            this.labelNombreEmployes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNombreEmployes.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNombreEmployes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.labelNombreEmployes.Location = new System.Drawing.Point(18, 12);
            this.labelNombreEmployes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNombreEmployes.Name = "labelNombreEmployes";
            this.labelNombreEmployes.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.labelNombreEmployes.Size = new System.Drawing.Size(247, 31);
            this.labelNombreEmployes.TabIndex = 0;
            this.labelNombreEmployes.Text = "👥 0 employé sélectionné";
            this.labelNombreEmployes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMain.Controls.Add(this.cardDataGrid);
            this.panelMain.Controls.Add(this.panelProgression);
            this.panelMain.Controls.Add(this.panelInfoBanner);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 80);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.panelMain.Size = new System.Drawing.Size(1333, 607);
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
            this.cardDataGrid.Margin = new System.Windows.Forms.Padding(4);
            this.cardDataGrid.Name = "cardDataGrid";
            this.cardDataGrid.Padding = new System.Windows.Forms.Padding(1);
            this.cardDataGrid.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.cardDataGrid.ShadowDecoration.Depth = 8;
            this.cardDataGrid.ShadowDecoration.Enabled = true;
            this.cardDataGrid.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.cardDataGrid.Size = new System.Drawing.Size(1293, 444);
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
            this.dataGridViewEmployes.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewEmployes.Name = "dataGridViewEmployes";
            this.dataGridViewEmployes.RowHeadersVisible = false;
            this.dataGridViewEmployes.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Montserrat", 8.5F);
            this.dataGridViewEmployes.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewEmployes.RowTemplate.Height = 35;
            this.dataGridViewEmployes.Size = new System.Drawing.Size(1291, 442);
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
            this.panelProgression.BorderRadius = 8;
            this.panelProgression.Controls.Add(this.labelProgression);
            this.panelProgression.Controls.Add(this.guna2ProgressBar1);
            this.panelProgression.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelProgression.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelProgression.Location = new System.Drawing.Point(20, 509);
            this.panelProgression.Margin = new System.Windows.Forms.Padding(4);
            this.panelProgression.Name = "panelProgression";
            this.panelProgression.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
            this.panelProgression.ShadowDecoration.BorderRadius = 8;
            this.panelProgression.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(40)))));
            this.panelProgression.ShadowDecoration.Depth = 12;
            this.panelProgression.ShadowDecoration.Enabled = true;
            this.panelProgression.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 3, 0, 6);
            this.panelProgression.Size = new System.Drawing.Size(1293, 80);
            this.panelProgression.TabIndex = 2;
            this.panelProgression.Visible = false;
            // 
            // labelProgression
            // 
            this.labelProgression.AutoSize = true;
            this.labelProgression.BackColor = System.Drawing.Color.Transparent;
            this.labelProgression.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelProgression.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProgression.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.labelProgression.Location = new System.Drawing.Point(24, 16);
            this.labelProgression.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelProgression.Name = "labelProgression";
            this.labelProgression.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.labelProgression.Size = new System.Drawing.Size(232, 32);
            this.labelProgression.TabIndex = 1;
            this.labelProgression.Text = "🔄 Génération en cours... 0%";
            // 
            // guna2ProgressBar1
            // 
            this.guna2ProgressBar1.BorderRadius = 6;
            this.guna2ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2ProgressBar1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(240)))));
            this.guna2ProgressBar1.Location = new System.Drawing.Point(24, 52);
            this.guna2ProgressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.guna2ProgressBar1.Name = "guna2ProgressBar1";
            this.guna2ProgressBar1.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(179)))), ((int)(((byte)(113)))));
            this.guna2ProgressBar1.ProgressColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.guna2ProgressBar1.Size = new System.Drawing.Size(1245, 12);
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
            this.panelInfoBanner.Margin = new System.Windows.Forms.Padding(4);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaisiePayeLotForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Saisie de Paie par Lot";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelButtonsContainer.ResumeLayout(false);
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
        private Guna.UI2.WinForms.Guna2Panel panelButtonsContainer;
        private Guna.UI2.WinForms.Guna2Panel panelStatistiques;
        private System.Windows.Forms.Label labelNombreEmployes;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
        private Guna.UI2.WinForms.Guna2Panel panelSpacer;
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
