namespace RH_GRH
{
    partial class SelectionEntrepriseForm
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
            this.panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.labelSousTitre = new System.Windows.Forms.Label();
            this.labelTitre = new System.Windows.Forms.Label();
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.cardPeriode = new Guna.UI2.WinForms.Guna2Panel();
            this.dateTimePickerFin = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dateTimePickerDebut = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.labelFin = new System.Windows.Forms.Label();
            this.labelDebut = new System.Windows.Forms.Label();
            this.labelPeriode = new System.Windows.Forms.Label();
            this.cardDataGrid = new Guna.UI2.WinForms.Guna2Panel();
            this.dataGridViewEntreprises = new Guna.UI2.WinForms.Guna2DataGridView();
            this.panelFooter = new Guna.UI2.WinForms.Guna2Panel();
            this.buttonAnnuler = new Guna.UI2.WinForms.Guna2Button();
            this.buttonValider = new Guna.UI2.WinForms.Guna2Button();
            this.panelStats = new Guna.UI2.WinForms.Guna2Panel();
            this.labelNombreEntreprises = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.cardPeriode.SuspendLayout();
            this.cardDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntreprises)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.panelStats.SuspendLayout();
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
            this.panelHeader.Size = new System.Drawing.Size(733, 80);
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
            this.labelSousTitre.Size = new System.Drawing.Size(332, 22);
            this.labelSousTitre.TabIndex = 1;
            this.labelSousTitre.Text = "Choisissez une entreprise et la période de paie";
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
            this.labelTitre.Size = new System.Drawing.Size(322, 38);
            this.labelTitre.TabIndex = 0;
            this.labelTitre.Text = "Sélection de l\'Entreprise";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMain.Controls.Add(this.cardPeriode);
            this.panelMain.Controls.Add(this.cardDataGrid);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 80);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.panelMain.Size = new System.Drawing.Size(733, 394);
            this.panelMain.TabIndex = 1;
            // 
            // cardPeriode
            // 
            this.cardPeriode.BackColor = System.Drawing.Color.Transparent;
            this.cardPeriode.BorderRadius = 6;
            this.cardPeriode.Controls.Add(this.dateTimePickerFin);
            this.cardPeriode.Controls.Add(this.dateTimePickerDebut);
            this.cardPeriode.Controls.Add(this.labelFin);
            this.cardPeriode.Controls.Add(this.labelDebut);
            this.cardPeriode.Controls.Add(this.labelPeriode);
            this.cardPeriode.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cardPeriode.FillColor = System.Drawing.Color.White;
            this.cardPeriode.Location = new System.Drawing.Point(20, 241);
            this.cardPeriode.Margin = new System.Windows.Forms.Padding(4);
            this.cardPeriode.Name = "cardPeriode";
            this.cardPeriode.Padding = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.cardPeriode.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.cardPeriode.ShadowDecoration.Depth = 8;
            this.cardPeriode.ShadowDecoration.Enabled = true;
            this.cardPeriode.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.cardPeriode.Size = new System.Drawing.Size(693, 135);
            this.cardPeriode.TabIndex = 1;
            // 
            // dateTimePickerFin
            //
            this.dateTimePickerFin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dateTimePickerFin.BackColor = System.Drawing.Color.Transparent;
            this.dateTimePickerFin.BorderRadius = 6;
            this.dateTimePickerFin.Checked = true;
            this.dateTimePickerFin.CustomFormat = "dd/MM/yyyy";
            this.dateTimePickerFin.FillColor = System.Drawing.Color.White;
            this.dateTimePickerFin.Font = new System.Drawing.Font("Montserrat", 9.5F);
            this.dateTimePickerFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFin.Location = new System.Drawing.Point(420, 58);
            this.dateTimePickerFin.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePickerFin.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dateTimePickerFin.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerFin.Name = "dateTimePickerFin";
            this.dateTimePickerFin.Size = new System.Drawing.Size(240, 45);
            this.dateTimePickerFin.TabIndex = 4;
            this.dateTimePickerFin.Value = new System.DateTime(2026, 1, 13, 0, 0, 0, 0);
            // 
            // dateTimePickerDebut
            //
            this.dateTimePickerDebut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dateTimePickerDebut.BackColor = System.Drawing.Color.Transparent;
            this.dateTimePickerDebut.BorderRadius = 6;
            this.dateTimePickerDebut.Checked = true;
            this.dateTimePickerDebut.CustomFormat = "dd/MM/yyyy";
            this.dateTimePickerDebut.FillColor = System.Drawing.Color.White;
            this.dateTimePickerDebut.Font = new System.Drawing.Font("Montserrat", 9.5F);
            this.dateTimePickerDebut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDebut.Location = new System.Drawing.Point(100, 58);
            this.dateTimePickerDebut.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePickerDebut.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dateTimePickerDebut.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerDebut.Name = "dateTimePickerDebut";
            this.dateTimePickerDebut.Size = new System.Drawing.Size(240, 45);
            this.dateTimePickerDebut.TabIndex = 2;
            this.dateTimePickerDebut.Value = new System.DateTime(2026, 1, 13, 0, 0, 0, 0);
            // 
            // labelFin
            //
            this.labelFin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelFin.AutoSize = true;
            this.labelFin.BackColor = System.Drawing.Color.Transparent;
            this.labelFin.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.labelFin.Location = new System.Drawing.Point(365, 68);
            this.labelFin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFin.Name = "labelFin";
            this.labelFin.Size = new System.Drawing.Size(39, 24);
            this.labelFin.TabIndex = 3;
            this.labelFin.Text = "Fin:";
            // 
            // labelDebut
            //
            this.labelDebut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelDebut.AutoSize = true;
            this.labelDebut.BackColor = System.Drawing.Color.Transparent;
            this.labelDebut.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDebut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.labelDebut.Location = new System.Drawing.Point(24, 68);
            this.labelDebut.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDebut.Name = "labelDebut";
            this.labelDebut.Size = new System.Drawing.Size(62, 24);
            this.labelDebut.TabIndex = 1;
            this.labelDebut.Text = "Début:";
            // 
            // labelPeriode
            //
            this.labelPeriode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelPeriode.AutoSize = true;
            this.labelPeriode.BackColor = System.Drawing.Color.Transparent;
            this.labelPeriode.Font = new System.Drawing.Font("Montserrat", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPeriode.ForeColor = System.Drawing.Color.MidnightBlue;
            this.labelPeriode.Location = new System.Drawing.Point(252, 18);
            this.labelPeriode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPeriode.Name = "labelPeriode";
            this.labelPeriode.Size = new System.Drawing.Size(189, 28);
            this.labelPeriode.TabIndex = 0;
            this.labelPeriode.Text = "📅 Période de Paie";
            // 
            // cardDataGrid
            // 
            this.cardDataGrid.BackColor = System.Drawing.Color.Transparent;
            this.cardDataGrid.BorderRadius = 6;
            this.cardDataGrid.Controls.Add(this.dataGridViewEntreprises);
            this.cardDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardDataGrid.FillColor = System.Drawing.Color.White;
            this.cardDataGrid.Location = new System.Drawing.Point(20, 18);
            this.cardDataGrid.Margin = new System.Windows.Forms.Padding(4);
            this.cardDataGrid.Name = "cardDataGrid";
            this.cardDataGrid.Padding = new System.Windows.Forms.Padding(1);
            this.cardDataGrid.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.cardDataGrid.ShadowDecoration.Depth = 8;
            this.cardDataGrid.ShadowDecoration.Enabled = true;
            this.cardDataGrid.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.cardDataGrid.Size = new System.Drawing.Size(693, 358);
            this.cardDataGrid.TabIndex = 0;
            // 
            // dataGridViewEntreprises
            // 
            this.dataGridViewEntreprises.AllowUserToAddRows = false;
            this.dataGridViewEntreprises.AllowUserToDeleteRows = false;
            this.dataGridViewEntreprises.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Montserrat", 8.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewEntreprises.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewEntreprises.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewEntreprises.ColumnHeadersHeight = 36;
            this.dataGridViewEntreprises.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Montserrat", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewEntreprises.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewEntreprises.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewEntreprises.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dataGridViewEntreprises.Location = new System.Drawing.Point(1, 1);
            this.dataGridViewEntreprises.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewEntreprises.Name = "dataGridViewEntreprises";
            this.dataGridViewEntreprises.ReadOnly = true;
            this.dataGridViewEntreprises.RowHeadersVisible = false;
            this.dataGridViewEntreprises.RowHeadersWidth = 51;
            this.dataGridViewEntreprises.RowTemplate.Height = 32;
            this.dataGridViewEntreprises.Size = new System.Drawing.Size(691, 356);
            this.dataGridViewEntreprises.TabIndex = 0;
            this.dataGridViewEntreprises.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dataGridViewEntreprises.ThemeStyle.AlternatingRowsStyle.Font = new System.Drawing.Font("Montserrat", 8.5F);
            this.dataGridViewEntreprises.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewEntreprises.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            this.dataGridViewEntreprises.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewEntreprises.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dataGridViewEntreprises.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dataGridViewEntreprises.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.MidnightBlue;
            this.dataGridViewEntreprises.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewEntreprises.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold);
            this.dataGridViewEntreprises.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridViewEntreprises.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dataGridViewEntreprises.ThemeStyle.HeaderStyle.Height = 36;
            this.dataGridViewEntreprises.ThemeStyle.ReadOnly = true;
            this.dataGridViewEntreprises.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dataGridViewEntreprises.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewEntreprises.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Montserrat", 8.5F);
            this.dataGridViewEntreprises.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewEntreprises.ThemeStyle.RowsStyle.Height = 32;
            this.dataGridViewEntreprises.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            this.dataGridViewEntreprises.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.Transparent;
            this.panelFooter.Controls.Add(this.buttonAnnuler);
            this.panelFooter.Controls.Add(this.buttonValider);
            this.panelFooter.Controls.Add(this.panelStats);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.panelFooter.Location = new System.Drawing.Point(0, 474);
            this.panelFooter.Margin = new System.Windows.Forms.Padding(4);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.panelFooter.ShadowDecoration.BorderRadius = 0;
            this.panelFooter.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(30)))));
            this.panelFooter.ShadowDecoration.Depth = 8;
            this.panelFooter.ShadowDecoration.Enabled = true;
            this.panelFooter.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, -5, 0, 0);
            this.panelFooter.Size = new System.Drawing.Size(733, 80);
            this.panelFooter.TabIndex = 2;
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
            this.buttonAnnuler.Location = new System.Drawing.Point(467, 15);
            this.buttonAnnuler.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(113, 50);
            this.buttonAnnuler.TabIndex = 2;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            // 
            // buttonValider
            // 
            this.buttonValider.Animated = true;
            this.buttonValider.BorderRadius = 4;
            this.buttonValider.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonValider.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonValider.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonValider.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonValider.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonValider.FillColor = System.Drawing.Color.SeaGreen;
            this.buttonValider.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold);
            this.buttonValider.ForeColor = System.Drawing.Color.White;
            this.buttonValider.HoverState.FillColor = System.Drawing.Color.ForestGreen;
            this.buttonValider.Location = new System.Drawing.Point(580, 15);
            this.buttonValider.Margin = new System.Windows.Forms.Padding(4);
            this.buttonValider.Name = "buttonValider";
            this.buttonValider.Size = new System.Drawing.Size(133, 50);
            this.buttonValider.TabIndex = 1;
            this.buttonValider.Text = "Valider";
            this.buttonValider.Click += new System.EventHandler(this.buttonValider_Click);
            // 
            // panelStats
            // 
            this.panelStats.BackColor = System.Drawing.Color.Transparent;
            this.panelStats.BorderColor = System.Drawing.Color.MidnightBlue;
            this.panelStats.BorderRadius = 4;
            this.panelStats.BorderThickness = 2;
            this.panelStats.Controls.Add(this.labelNombreEntreprises);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelStats.FillColor = System.Drawing.Color.White;
            this.panelStats.Location = new System.Drawing.Point(20, 15);
            this.panelStats.Margin = new System.Windows.Forms.Padding(4);
            this.panelStats.Name = "panelStats";
            this.panelStats.Padding = new System.Windows.Forms.Padding(16, 7, 16, 7);
            this.panelStats.Size = new System.Drawing.Size(267, 50);
            this.panelStats.TabIndex = 0;
            // 
            // labelNombreEntreprises
            // 
            this.labelNombreEntreprises.AutoSize = true;
            this.labelNombreEntreprises.BackColor = System.Drawing.Color.Transparent;
            this.labelNombreEntreprises.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNombreEntreprises.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNombreEntreprises.ForeColor = System.Drawing.Color.Gray;
            this.labelNombreEntreprises.Location = new System.Drawing.Point(16, 7);
            this.labelNombreEntreprises.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNombreEntreprises.Name = "labelNombreEntreprises";
            this.labelNombreEntreprises.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.labelNombreEntreprises.Size = new System.Drawing.Size(186, 29);
            this.labelNombreEntreprises.TabIndex = 0;
            this.labelNombreEntreprises.Text = "0 entreprise(s) trouvée(s)";
            // 
            // SelectionEntrepriseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(733, 554);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectionEntrepriseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sélection d\'entreprise";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.cardPeriode.ResumeLayout(false);
            this.cardPeriode.PerformLayout();
            this.cardDataGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntreprises)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelHeader;
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.Label labelSousTitre;
        private Guna.UI2.WinForms.Guna2Panel panelMain;
        private Guna.UI2.WinForms.Guna2Panel cardDataGrid;
        private Guna.UI2.WinForms.Guna2DataGridView dataGridViewEntreprises;
        private Guna.UI2.WinForms.Guna2Panel cardPeriode;
        private Guna.UI2.WinForms.Guna2DateTimePicker dateTimePickerFin;
        private Guna.UI2.WinForms.Guna2DateTimePicker dateTimePickerDebut;
        private System.Windows.Forms.Label labelFin;
        private System.Windows.Forms.Label labelDebut;
        private System.Windows.Forms.Label labelPeriode;
        private Guna.UI2.WinForms.Guna2Panel panelFooter;
        private Guna.UI2.WinForms.Guna2Panel panelStats;
        private System.Windows.Forms.Label labelNombreEntreprises;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
        private Guna.UI2.WinForms.Guna2Button buttonValider;
    }
}
