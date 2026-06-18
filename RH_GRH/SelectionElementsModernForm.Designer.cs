namespace RH_GRH
{
    partial class SelectionElementsModernForm
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
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.labelCounter = new System.Windows.Forms.Label();
            this.panelContent = new Guna.UI2.WinForms.Guna2Panel();
            this.panelGrid = new Guna.UI2.WinForms.Guna2Panel();
            this.dataGridViewEmployes = new System.Windows.Forms.DataGridView();
            this.panelActions = new Guna.UI2.WinForms.Guna2Panel();
            this.buttonToutCocher = new Guna.UI2.WinForms.Guna2Button();
            this.buttonToutDecocher = new Guna.UI2.WinForms.Guna2Button();
            this.panelFooter = new Guna.UI2.WinForms.Guna2Panel();
            this.buttonAnnuler = new Guna.UI2.WinForms.Guna2Button();
            this.buttonValider = new Guna.UI2.WinForms.Guna2Button();
            this.panelMain.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployes)).BeginInit();
            this.panelActions.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            //
            // panelMain
            //
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(253)))));
            this.panelMain.Controls.Add(this.panelContent);
            this.panelMain.Controls.Add(this.panelFooter);
            this.panelMain.Controls.Add(this.panelHeader);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1200, 700);
            this.panelMain.TabIndex = 0;
            //
            // panelHeader
            //
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(43)))), ((int)(((byte)(132)))));
            this.panelHeader.Controls.Add(this.labelCounter);
            this.panelHeader.Controls.Add(this.labelSubtitle);
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(40, 20, 40, 20);
            this.panelHeader.Size = new System.Drawing.Size(1200, 100);
            this.panelHeader.TabIndex = 0;
            //
            // labelTitle
            //
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(40, 24);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(385, 37);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Sélection des éléments de paie";
            //
            // labelSubtitle
            //
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.Font = new System.Drawing.Font("Montserrat", 8.5F);
            this.labelSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(230)))));
            this.labelSubtitle.Location = new System.Drawing.Point(43, 64);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(450, 20);
            this.labelSubtitle.TabIndex = 1;
            this.labelSubtitle.Text = "Sélectionnez les éléments de paie à appliquer pour chaque employé";
            //
            // labelCounter
            //
            this.labelCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCounter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(20)))));
            this.labelCounter.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.labelCounter.ForeColor = System.Drawing.Color.White;
            this.labelCounter.Location = new System.Drawing.Point(980, 30);
            this.labelCounter.Name = "labelCounter";
            this.labelCounter.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.labelCounter.Size = new System.Drawing.Size(180, 44);
            this.labelCounter.TabIndex = 2;
            this.labelCounter.Text = "0 employé(s)";
            this.labelCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // panelContent
            //
            this.panelContent.BackColor = System.Drawing.Color.Transparent;
            this.panelContent.Controls.Add(this.panelGrid);
            this.panelContent.Controls.Add(this.panelActions);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 100);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(40, 25, 40, 15);
            this.panelContent.Size = new System.Drawing.Size(1200, 520);
            this.panelContent.TabIndex = 1;
            //
            // panelGrid
            //
            this.panelGrid.BackColor = System.Drawing.Color.White;
            this.panelGrid.BorderRadius = 12;
            this.panelGrid.Controls.Add(this.dataGridViewEmployes);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(40, 25);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new System.Windows.Forms.Padding(2);
            this.panelGrid.ShadowDecoration.BorderRadius = 12;
            this.panelGrid.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.panelGrid.ShadowDecoration.Depth = 15;
            this.panelGrid.ShadowDecoration.Enabled = true;
            this.panelGrid.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 3, 0, 8);
            this.panelGrid.Size = new System.Drawing.Size(1120, 420);
            this.panelGrid.TabIndex = 0;
            //
            // dataGridViewEmployes
            //
            this.dataGridViewEmployes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmployes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewEmployes.Location = new System.Drawing.Point(2, 2);
            this.dataGridViewEmployes.Name = "dataGridViewEmployes";
            this.dataGridViewEmployes.RowHeadersWidth = 51;
            this.dataGridViewEmployes.Size = new System.Drawing.Size(1116, 416);
            this.dataGridViewEmployes.TabIndex = 0;
            //
            // panelActions
            //
            this.panelActions.BackColor = System.Drawing.Color.Transparent;
            this.panelActions.Controls.Add(this.buttonToutDecocher);
            this.panelActions.Controls.Add(this.buttonToutCocher);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(40, 445);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(1120, 60);
            this.panelActions.TabIndex = 1;
            //
            // buttonToutCocher
            //
            this.buttonToutCocher.Animated = true;
            this.buttonToutCocher.BorderRadius = 10;
            this.buttonToutCocher.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonToutCocher.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonToutCocher.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonToutCocher.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonToutCocher.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.buttonToutCocher.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold);
            this.buttonToutCocher.ForeColor = System.Drawing.Color.White;
            this.buttonToutCocher.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(155)))), ((int)(((byte)(85)))));
            this.buttonToutCocher.Location = new System.Drawing.Point(0, 10);
            this.buttonToutCocher.Name = "buttonToutCocher";
            this.buttonToutCocher.ShadowDecoration.BorderRadius = 10;
            this.buttonToutCocher.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.buttonToutCocher.ShadowDecoration.Depth = 8;
            this.buttonToutCocher.ShadowDecoration.Enabled = true;
            this.buttonToutCocher.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.buttonToutCocher.Size = new System.Drawing.Size(170, 42);
            this.buttonToutCocher.TabIndex = 0;
            this.buttonToutCocher.Text = "✓ Tout cocher";
            this.buttonToutCocher.Click += new System.EventHandler(this.buttonToutCocher_Click);
            //
            // buttonToutDecocher
            //
            this.buttonToutDecocher.Animated = true;
            this.buttonToutDecocher.BorderRadius = 10;
            this.buttonToutDecocher.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonToutDecocher.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonToutDecocher.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonToutDecocher.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonToutDecocher.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.buttonToutDecocher.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold);
            this.buttonToutDecocher.ForeColor = System.Drawing.Color.White;
            this.buttonToutDecocher.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(68)))), ((int)(((byte)(54)))));
            this.buttonToutDecocher.Location = new System.Drawing.Point(180, 10);
            this.buttonToutDecocher.Name = "buttonToutDecocher";
            this.buttonToutDecocher.ShadowDecoration.BorderRadius = 10;
            this.buttonToutDecocher.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.buttonToutDecocher.ShadowDecoration.Depth = 8;
            this.buttonToutDecocher.ShadowDecoration.Enabled = true;
            this.buttonToutDecocher.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.buttonToutDecocher.Size = new System.Drawing.Size(170, 42);
            this.buttonToutDecocher.TabIndex = 1;
            this.buttonToutDecocher.Text = "✗ Tout décocher";
            this.buttonToutDecocher.Click += new System.EventHandler(this.buttonToutDecocher_Click);
            //
            // panelFooter
            //
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.panelFooter.Controls.Add(this.buttonValider);
            this.panelFooter.Controls.Add(this.buttonAnnuler);
            this.panelFooter.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.panelFooter.CustomBorderThickness = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 620);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(40, 20, 40, 20);
            this.panelFooter.Size = new System.Drawing.Size(1200, 80);
            this.panelFooter.TabIndex = 2;
            //
            // buttonAnnuler
            //
            this.buttonAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAnnuler.Animated = true;
            this.buttonAnnuler.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.buttonAnnuler.BorderRadius = 12;
            this.buttonAnnuler.BorderThickness = 1;
            this.buttonAnnuler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAnnuler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAnnuler.FillColor = System.Drawing.Color.White;
            this.buttonAnnuler.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.buttonAnnuler.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.buttonAnnuler.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.buttonAnnuler.Location = new System.Drawing.Point(900, 18);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(140, 44);
            this.buttonAnnuler.TabIndex = 0;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            //
            // buttonValider
            //
            this.buttonValider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonValider.Animated = true;
            this.buttonValider.BorderRadius = 12;
            this.buttonValider.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonValider.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonValider.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonValider.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonValider.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(43)))), ((int)(((byte)(132)))));
            this.buttonValider.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold);
            this.buttonValider.ForeColor = System.Drawing.Color.White;
            this.buttonValider.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.buttonValider.Location = new System.Drawing.Point(1050, 18);
            this.buttonValider.Name = "buttonValider";
            this.buttonValider.ShadowDecoration.BorderRadius = 12;
            this.buttonValider.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(43)))), ((int)(((byte)(132)))));
            this.buttonValider.ShadowDecoration.Depth = 12;
            this.buttonValider.ShadowDecoration.Enabled = true;
            this.buttonValider.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 3, 0, 6);
            this.buttonValider.Size = new System.Drawing.Size(150, 44);
            this.buttonValider.TabIndex = 1;
            this.buttonValider.Text = "✓ VALIDER";
            this.buttonValider.Click += new System.EventHandler(this.buttonValider_Click);
            //
            // SelectionElementsModernForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectionElementsModernForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sélection des éléments de paie";
            this.panelMain.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployes)).EndInit();
            this.panelActions.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelMain;
        private Guna.UI2.WinForms.Guna2Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelSubtitle;
        private System.Windows.Forms.Label labelCounter;
        private Guna.UI2.WinForms.Guna2Panel panelContent;
        private Guna.UI2.WinForms.Guna2Panel panelGrid;
        private System.Windows.Forms.DataGridView dataGridViewEmployes;
        private Guna.UI2.WinForms.Guna2Panel panelActions;
        private Guna.UI2.WinForms.Guna2Button buttonToutCocher;
        private Guna.UI2.WinForms.Guna2Button buttonToutDecocher;
        private Guna.UI2.WinForms.Guna2Panel panelFooter;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
        private Guna.UI2.WinForms.Guna2Button buttonValider;
    }
}
