namespace RH_GRH
{
    partial class SelectionEntrepriseModernForm
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
            this.components = new System.ComponentModel.Container();
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.panelContent = new Guna.UI2.WinForms.Guna2Panel();
            this.flowPanelEntreprises = new System.Windows.Forms.FlowLayoutPanel();
            this.cardSearch = new Guna.UI2.WinForms.Guna2Panel();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelSearchIcon = new System.Windows.Forms.Label();
            this.labelCounter = new System.Windows.Forms.Label();
            this.panelFooter = new Guna.UI2.WinForms.Guna2Panel();
            this.cardPeriod = new Guna.UI2.WinForms.Guna2Panel();
            this.labelPeriodIcon = new System.Windows.Forms.Label();
            this.labelPeriodTitle = new System.Windows.Forms.Label();
            this.datePickerDebut = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.labelArrow = new System.Windows.Forms.Label();
            this.datePickerFin = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.buttonCancel = new Guna.UI2.WinForms.Guna2Button();
            this.buttonValidate = new Guna.UI2.WinForms.Guna2Button();
            this.labelSelection = new System.Windows.Forms.Label();
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.panelMain.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.cardSearch.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.cardPeriod.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(253)))));
            this.panelMain.Controls.Add(this.panelHeader);
            this.panelMain.Controls.Add(this.panelContent);
            this.panelMain.Controls.Add(this.panelFooter);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1375, 875);
            this.panelMain.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.MidnightBlue;
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Controls.Add(this.labelSubtitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(62, 25, 62, 19);
            this.panelHeader.Size = new System.Drawing.Size(1375, 119);
            this.panelHeader.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("Montserrat", 20F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(62, 25);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(541, 53);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Sélectionnez votre entreprise";
            // 
            // labelSubtitle
            // 
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.labelSubtitle.Font = new System.Drawing.Font("Montserrat", 9F);
            this.labelSubtitle.ForeColor = System.Drawing.Color.White;
            this.labelSubtitle.Location = new System.Drawing.Point(69, 78);
            this.labelSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(448, 24);
            this.labelSubtitle.TabIndex = 1;
            this.labelSubtitle.Text = "Choisissez une entreprise pour démarrer la gestion de paie";
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelContent.Controls.Add(this.flowPanelEntreprises);
            this.panelContent.Controls.Add(this.cardSearch);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 0);
            this.panelContent.Margin = new System.Windows.Forms.Padding(4);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(50, 0, 50, 0);
            this.panelContent.Size = new System.Drawing.Size(1375, 700);
            this.panelContent.TabIndex = 1;
            // 
            // flowPanelEntreprises
            // 
            this.flowPanelEntreprises.AutoScroll = true;
            this.flowPanelEntreprises.BackColor = System.Drawing.Color.Transparent;
            this.flowPanelEntreprises.Location = new System.Drawing.Point(50, 127);
            this.flowPanelEntreprises.Margin = new System.Windows.Forms.Padding(4);
            this.flowPanelEntreprises.Name = "flowPanelEntreprises";
            this.flowPanelEntreprises.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.flowPanelEntreprises.Size = new System.Drawing.Size(1275, 554);
            this.flowPanelEntreprises.TabIndex = 1;
            // 
            // cardSearch
            // 
            this.cardSearch.BackColor = System.Drawing.Color.Transparent;
            this.cardSearch.BorderRadius = 12;
            this.cardSearch.Controls.Add(this.txtSearch);
            this.cardSearch.Controls.Add(this.labelSearchIcon);
            this.cardSearch.Controls.Add(this.labelCounter);
            this.cardSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.cardSearch.FillColor = System.Drawing.Color.White;
            this.cardSearch.Location = new System.Drawing.Point(50, 0);
            this.cardSearch.Margin = new System.Windows.Forms.Padding(4);
            this.cardSearch.Name = "cardSearch";
            this.cardSearch.Padding = new System.Windows.Forms.Padding(19, 15, 19, 15);
            this.cardSearch.ShadowDecoration.BorderRadius = 12;
            this.cardSearch.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cardSearch.ShadowDecoration.Depth = 12;
            this.cardSearch.ShadowDecoration.Enabled = true;
            this.cardSearch.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 2, 0, 6);
            this.cardSearch.Size = new System.Drawing.Size(1275, 69);
            this.cardSearch.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.Color.Transparent;
            this.txtSearch.BorderColor = System.Drawing.Color.Transparent;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.DefaultText = "";
            this.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.FocusedState.BorderColor = System.Drawing.Color.Transparent;
            this.txtSearch.Font = new System.Drawing.Font("Montserrat", 9.5F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtSearch.HoverState.BorderColor = System.Drawing.Color.Transparent;
            this.txtSearch.Location = new System.Drawing.Point(56, 10);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.txtSearch.PlaceholderText = "Rechercher une entreprise...";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(850, 48);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // labelSearchIcon
            // 
            this.labelSearchIcon.AutoSize = true;
            this.labelSearchIcon.BackColor = System.Drawing.Color.Transparent;
            this.labelSearchIcon.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.labelSearchIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelSearchIcon.Location = new System.Drawing.Point(19, 16);
            this.labelSearchIcon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSearchIcon.Name = "labelSearchIcon";
            this.labelSearchIcon.Size = new System.Drawing.Size(47, 32);
            this.labelSearchIcon.TabIndex = 1;
            this.labelSearchIcon.Text = "🔍";
            // 
            // labelCounter
            // 
            this.labelCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCounter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.labelCounter.Font = new System.Drawing.Font("Montserrat", 8.5F, System.Drawing.FontStyle.Bold);
            this.labelCounter.ForeColor = System.Drawing.Color.White;
            this.labelCounter.Location = new System.Drawing.Point(1100, 15);
            this.labelCounter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCounter.Name = "labelCounter";
            this.labelCounter.Padding = new System.Windows.Forms.Padding(19, 8, 19, 8);
            this.labelCounter.Size = new System.Drawing.Size(156, 38);
            this.labelCounter.TabIndex = 2;
            this.labelCounter.Text = "0 entreprise";
            this.labelCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.MidnightBlue;
            this.panelFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.panelFooter.Controls.Add(this.cardPeriod);
            this.panelFooter.Controls.Add(this.buttonCancel);
            this.panelFooter.Controls.Add(this.buttonValidate);
            this.panelFooter.Controls.Add(this.labelSelection);
            this.panelFooter.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.panelFooter.CustomBorderThickness = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 700);
            this.panelFooter.Margin = new System.Windows.Forms.Padding(4);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(50, 25, 50, 25);
            this.panelFooter.Size = new System.Drawing.Size(1375, 175);
            this.panelFooter.TabIndex = 2;
            // 
            // cardPeriod
            // 
            this.cardPeriod.BackColor = System.Drawing.Color.Transparent;
            this.cardPeriod.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.cardPeriod.BorderRadius = 12;
            this.cardPeriod.BorderThickness = 1;
            this.cardPeriod.Controls.Add(this.labelPeriodIcon);
            this.cardPeriod.Controls.Add(this.labelPeriodTitle);
            this.cardPeriod.Controls.Add(this.datePickerDebut);
            this.cardPeriod.Controls.Add(this.labelArrow);
            this.cardPeriod.Controls.Add(this.datePickerFin);
            this.cardPeriod.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.cardPeriod.Location = new System.Drawing.Point(50, 25);
            this.cardPeriod.Margin = new System.Windows.Forms.Padding(4);
            this.cardPeriod.Name = "cardPeriod";
            this.cardPeriod.Padding = new System.Windows.Forms.Padding(22, 18, 22, 18);
            this.cardPeriod.Size = new System.Drawing.Size(713, 98);
            this.cardPeriod.TabIndex = 0;
            // 
            // labelPeriodIcon
            // 
            this.labelPeriodIcon.AutoSize = true;
            this.labelPeriodIcon.BackColor = System.Drawing.Color.Transparent;
            this.labelPeriodIcon.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.labelPeriodIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.labelPeriodIcon.Location = new System.Drawing.Point(19, 28);
            this.labelPeriodIcon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPeriodIcon.Name = "labelPeriodIcon";
            this.labelPeriodIcon.Size = new System.Drawing.Size(43, 30);
            this.labelPeriodIcon.TabIndex = 0;
            this.labelPeriodIcon.Text = "📅";
            // 
            // labelPeriodTitle
            // 
            this.labelPeriodTitle.AutoSize = true;
            this.labelPeriodTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelPeriodTitle.Font = new System.Drawing.Font("Montserrat", 8.5F, System.Drawing.FontStyle.Bold);
            this.labelPeriodTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.labelPeriodTitle.Location = new System.Drawing.Point(60, 32);
            this.labelPeriodTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPeriodTitle.Name = "labelPeriodTitle";
            this.labelPeriodTitle.Size = new System.Drawing.Size(130, 24);
            this.labelPeriodTitle.TabIndex = 1;
            this.labelPeriodTitle.Text = "Période de paie";
            // 
            // datePickerDebut
            // 
            this.datePickerDebut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.datePickerDebut.BackColor = System.Drawing.Color.Transparent;
            this.datePickerDebut.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.datePickerDebut.BorderRadius = 10;
            this.datePickerDebut.BorderThickness = 1;
            this.datePickerDebut.Checked = true;
            this.datePickerDebut.CustomFormat = "dd/MM/yyyy";
            this.datePickerDebut.FillColor = System.Drawing.Color.White;
            this.datePickerDebut.Font = new System.Drawing.Font("Montserrat", 8F);
            this.datePickerDebut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.datePickerDebut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerDebut.Location = new System.Drawing.Point(288, 22);
            this.datePickerDebut.Margin = new System.Windows.Forms.Padding(4);
            this.datePickerDebut.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.datePickerDebut.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.datePickerDebut.Name = "datePickerDebut";
            this.datePickerDebut.Size = new System.Drawing.Size(156, 42);
            this.datePickerDebut.TabIndex = 2;
            this.datePickerDebut.Value = new System.DateTime(2026, 6, 18, 0, 0, 0, 0);
            this.datePickerDebut.ValueChanged += new System.EventHandler(this.datePickerDebut_ValueChanged);
            // 
            // labelArrow
            // 
            this.labelArrow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelArrow.AutoSize = true;
            this.labelArrow.BackColor = System.Drawing.Color.Transparent;
            this.labelArrow.Font = new System.Drawing.Font("Montserrat", 10F);
            this.labelArrow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.labelArrow.Location = new System.Drawing.Point(478, 31);
            this.labelArrow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelArrow.Name = "labelArrow";
            this.labelArrow.Size = new System.Drawing.Size(22, 27);
            this.labelArrow.TabIndex = 3;
            this.labelArrow.Text = "→";
            // 
            // datePickerFin
            // 
            this.datePickerFin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.datePickerFin.BackColor = System.Drawing.Color.Transparent;
            this.datePickerFin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.datePickerFin.BorderRadius = 10;
            this.datePickerFin.BorderThickness = 1;
            this.datePickerFin.Checked = true;
            this.datePickerFin.CustomFormat = "dd/MM/yyyy";
            this.datePickerFin.FillColor = System.Drawing.Color.White;
            this.datePickerFin.Font = new System.Drawing.Font("Montserrat", 8F);
            this.datePickerFin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.datePickerFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerFin.Location = new System.Drawing.Point(529, 22);
            this.datePickerFin.Margin = new System.Windows.Forms.Padding(4);
            this.datePickerFin.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.datePickerFin.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.datePickerFin.Name = "datePickerFin";
            this.datePickerFin.Size = new System.Drawing.Size(156, 42);
            this.datePickerFin.TabIndex = 4;
            this.datePickerFin.Value = new System.DateTime(2026, 6, 18, 0, 0, 0, 0);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Animated = true;
            this.buttonCancel.BackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.buttonCancel.BorderRadius = 12;
            this.buttonCancel.BorderThickness = 1;
            this.buttonCancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonCancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonCancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonCancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonCancel.FillColor = System.Drawing.Color.White;
            this.buttonCancel.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.buttonCancel.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.buttonCancel.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.buttonCancel.Location = new System.Drawing.Point(1000, 38);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(150, 56);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Annuler";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonValidate
            // 
            this.buttonValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonValidate.Animated = true;
            this.buttonValidate.BackColor = System.Drawing.Color.Transparent;
            this.buttonValidate.BorderRadius = 12;
            this.buttonValidate.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonValidate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonValidate.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.buttonValidate.DisabledState.ForeColor = System.Drawing.Color.White;
            this.buttonValidate.Enabled = false;
            this.buttonValidate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.buttonValidate.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold);
            this.buttonValidate.ForeColor = System.Drawing.Color.White;
            this.buttonValidate.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.buttonValidate.Location = new System.Drawing.Point(1175, 38);
            this.buttonValidate.Margin = new System.Windows.Forms.Padding(4);
            this.buttonValidate.Name = "buttonValidate";
            this.buttonValidate.ShadowDecoration.BorderRadius = 12;
            this.buttonValidate.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.buttonValidate.ShadowDecoration.Depth = 12;
            this.buttonValidate.ShadowDecoration.Enabled = true;
            this.buttonValidate.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 3, 0, 6);
            this.buttonValidate.Size = new System.Drawing.Size(150, 56);
            this.buttonValidate.TabIndex = 2;
            this.buttonValidate.Text = "Valider ✓";
            this.buttonValidate.Click += new System.EventHandler(this.buttonValidate_Click);
            // 
            // labelSelection
            // 
            this.labelSelection.AutoSize = true;
            this.labelSelection.BackColor = System.Drawing.Color.Transparent;
            this.labelSelection.Font = new System.Drawing.Font("Montserrat", 9F);
            this.labelSelection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelSelection.Location = new System.Drawing.Point(62, 169);
            this.labelSelection.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSelection.Name = "labelSelection";
            this.labelSelection.Size = new System.Drawing.Size(0, 24);
            this.labelSelection.TabIndex = 3;
            this.labelSelection.Visible = false;
            // 
            // animationTimer
            // 
            this.animationTimer.Interval = 50;
            this.animationTimer.Tick += new System.EventHandler(this.animationTimer_Tick);
            // 
            // SelectionEntrepriseModernForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1375, 875);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SelectionEntrepriseModernForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sélection d\'entreprise";
            this.Load += new System.EventHandler(this.SelectionEntrepriseModernForm_Load);
            this.panelMain.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.cardSearch.ResumeLayout(false);
            this.cardSearch.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.cardPeriod.ResumeLayout(false);
            this.cardPeriod.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelMain;
        private Guna.UI2.WinForms.Guna2Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelSubtitle;
        private Guna.UI2.WinForms.Guna2Panel panelContent;
        private Guna.UI2.WinForms.Guna2Panel cardSearch;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private System.Windows.Forms.Label labelSearchIcon;
        private System.Windows.Forms.Label labelCounter;
        private System.Windows.Forms.FlowLayoutPanel flowPanelEntreprises;
        private Guna.UI2.WinForms.Guna2Panel panelFooter;
        private Guna.UI2.WinForms.Guna2Panel cardPeriod;
        private System.Windows.Forms.Label labelPeriodIcon;
        private System.Windows.Forms.Label labelPeriodTitle;
        private Guna.UI2.WinForms.Guna2DateTimePicker datePickerDebut;
        private System.Windows.Forms.Label labelArrow;
        private Guna.UI2.WinForms.Guna2DateTimePicker datePickerFin;
        private Guna.UI2.WinForms.Guna2Button buttonCancel;
        private Guna.UI2.WinForms.Guna2Button buttonValidate;
        private System.Windows.Forms.Label labelSelection;
        private System.Windows.Forms.Timer animationTimer;
    }
}
