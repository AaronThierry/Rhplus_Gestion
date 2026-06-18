namespace RH_GRH
{
    partial class SelectionEmployesElementsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelTitre = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.dataGridViewEmployes = new System.Windows.Forms.DataGridView();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.labelNombreEmployes = new System.Windows.Forms.Label();
            this.labelInstruction = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.buttonToutCocher = new Guna.UI2.WinForms.Guna2Button();
            this.buttonToutDecocher = new Guna.UI2.WinForms.Guna2Button();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.buttonValider = new Guna.UI2.WinForms.Guna2Button();
            this.buttonAnnuler = new Guna.UI2.WinForms.Guna2Button();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployes)).BeginInit();
            this.panelInfo.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            //
            // panelHeader
            //
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(43)))), ((int)(((byte)(132)))));
            this.panelHeader.Controls.Add(this.labelTitre);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1400, 80);
            this.panelHeader.TabIndex = 0;
            //
            // labelTitre
            //
            this.labelTitre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitre.Font = new System.Drawing.Font("Montserrat", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitre.ForeColor = System.Drawing.Color.White;
            this.labelTitre.Location = new System.Drawing.Point(0, 0);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.labelTitre.Size = new System.Drawing.Size(1400, 80);
            this.labelTitre.TabIndex = 0;
            this.labelTitre.Text = "Sélection des éléments de paie";
            this.labelTitre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelMain
            //
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.panelMain.Controls.Add(this.dataGridViewEmployes);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 145);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(40, 20, 40, 15);
            this.panelMain.Size = new System.Drawing.Size(1400, 465);
            this.panelMain.TabIndex = 1;
            //
            // dataGridViewEmployes
            //
            this.dataGridViewEmployes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmployes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewEmployes.Location = new System.Drawing.Point(40, 20);
            this.dataGridViewEmployes.Name = "dataGridViewEmployes";
            this.dataGridViewEmployes.RowHeadersWidth = 51;
            this.dataGridViewEmployes.Size = new System.Drawing.Size(1320, 430);
            this.dataGridViewEmployes.TabIndex = 0;
            //
            // panelInfo
            //
            this.panelInfo.BackColor = System.Drawing.Color.White;
            this.panelInfo.Controls.Add(this.labelNombreEmployes);
            this.panelInfo.Controls.Add(this.labelInstruction);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInfo.Location = new System.Drawing.Point(0, 80);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Padding = new System.Windows.Forms.Padding(40, 18, 40, 12);
            this.panelInfo.Size = new System.Drawing.Size(1400, 65);
            this.panelInfo.TabIndex = 2;
            //
            // labelNombreEmployes
            //
            this.labelNombreEmployes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNombreEmployes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(231)))), ((int)(((byte)(246)))));
            this.labelNombreEmployes.Font = new System.Drawing.Font("Montserrat", 8.5F, System.Drawing.FontStyle.Bold);
            this.labelNombreEmployes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(43)))), ((int)(((byte)(132)))));
            this.labelNombreEmployes.Location = new System.Drawing.Point(1185, 15);
            this.labelNombreEmployes.Name = "labelNombreEmployes";
            this.labelNombreEmployes.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.labelNombreEmployes.Size = new System.Drawing.Size(175, 32);
            this.labelNombreEmployes.TabIndex = 1;
            this.labelNombreEmployes.Text = "2 employé(s)";
            this.labelNombreEmployes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // labelInstruction
            //
            this.labelInstruction.AutoSize = true;
            this.labelInstruction.Font = new System.Drawing.Font("Montserrat", 8.5F);
            this.labelInstruction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelInstruction.Location = new System.Drawing.Point(40, 22);
            this.labelInstruction.Name = "labelInstruction";
            this.labelInstruction.Size = new System.Drawing.Size(520, 20);
            this.labelInstruction.TabIndex = 0;
            this.labelInstruction.Text = "Sélectionnez les éléments de paie à appliquer pour chaque employé";
            //
            // panelActions
            //
            this.panelActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.panelActions.Controls.Add(this.buttonToutCocher);
            this.panelActions.Controls.Add(this.buttonToutDecocher);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(0, 610);
            this.panelActions.Name = "panelActions";
            this.panelActions.Padding = new System.Windows.Forms.Padding(40, 10, 40, 10);
            this.panelActions.Size = new System.Drawing.Size(1400, 60);
            this.panelActions.TabIndex = 3;
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
            this.buttonToutCocher.Font = new System.Drawing.Font("Montserrat", 8.5F, System.Drawing.FontStyle.Bold);
            this.buttonToutCocher.ForeColor = System.Drawing.Color.White;
            this.buttonToutCocher.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(155)))), ((int)(((byte)(85)))));
            this.buttonToutCocher.Location = new System.Drawing.Point(40, 10);
            this.buttonToutCocher.Name = "buttonToutCocher";
            this.buttonToutCocher.ShadowDecoration.BorderRadius = 10;
            this.buttonToutCocher.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.buttonToutCocher.ShadowDecoration.Depth = 8;
            this.buttonToutCocher.ShadowDecoration.Enabled = true;
            this.buttonToutCocher.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.buttonToutCocher.Size = new System.Drawing.Size(170, 40);
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
            this.buttonToutDecocher.Font = new System.Drawing.Font("Montserrat", 8.5F, System.Drawing.FontStyle.Bold);
            this.buttonToutDecocher.ForeColor = System.Drawing.Color.White;
            this.buttonToutDecocher.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(68)))), ((int)(((byte)(54)))));
            this.buttonToutDecocher.Location = new System.Drawing.Point(220, 10);
            this.buttonToutDecocher.Name = "buttonToutDecocher";
            this.buttonToutDecocher.ShadowDecoration.BorderRadius = 10;
            this.buttonToutDecocher.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.buttonToutDecocher.ShadowDecoration.Depth = 8;
            this.buttonToutDecocher.ShadowDecoration.Enabled = true;
            this.buttonToutDecocher.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.buttonToutDecocher.Size = new System.Drawing.Size(170, 40);
            this.buttonToutDecocher.TabIndex = 1;
            this.buttonToutDecocher.Text = "✗ Tout décocher";
            this.buttonToutDecocher.Click += new System.EventHandler(this.buttonToutDecocher_Click);
            //
            // panelFooter
            //
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.buttonValider);
            this.panelFooter.Controls.Add(this.buttonAnnuler);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 670);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(40, 15, 40, 15);
            this.panelFooter.Size = new System.Drawing.Size(1400, 80);
            this.panelFooter.TabIndex = 4;
            //
            // buttonValider
            //
            this.buttonValider.Animated = true;
            this.buttonValider.BorderRadius = 12;
            this.buttonValider.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonValider.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonValider.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonValider.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonValider.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonValider.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(43)))), ((int)(((byte)(132)))));
            this.buttonValider.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold);
            this.buttonValider.ForeColor = System.Drawing.Color.White;
            this.buttonValider.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.buttonValider.Location = new System.Drawing.Point(1060, 20);
            this.buttonValider.Name = "buttonValider";
            this.buttonValider.ShadowDecoration.BorderRadius = 12;
            this.buttonValider.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(43)))), ((int)(((byte)(132)))));
            this.buttonValider.ShadowDecoration.Depth = 10;
            this.buttonValider.ShadowDecoration.Enabled = true;
            this.buttonValider.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 3, 0, 6);
            this.buttonValider.Size = new System.Drawing.Size(150, 40);
            this.buttonValider.TabIndex = 0;
            this.buttonValider.Text = "✓ VALIDER";
            this.buttonValider.Click += new System.EventHandler(this.buttonValider_Click);
            //
            // buttonAnnuler
            //
            this.buttonAnnuler.Animated = true;
            this.buttonAnnuler.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.buttonAnnuler.BorderRadius = 12;
            this.buttonAnnuler.BorderThickness = 1;
            this.buttonAnnuler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAnnuler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAnnuler.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonAnnuler.FillColor = System.Drawing.Color.White;
            this.buttonAnnuler.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.buttonAnnuler.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.buttonAnnuler.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.buttonAnnuler.Location = new System.Drawing.Point(1220, 20);
            this.buttonAnnuler.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(140, 40);
            this.buttonAnnuler.TabIndex = 1;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            //
            // SelectionEmployesElementsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1400, 750);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectionEmployesElementsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sélection des éléments de paie";
            this.panelHeader.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployes)).EndInit();
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.DataGridView dataGridViewEmployes;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label labelNombreEmployes;
        private System.Windows.Forms.Label labelInstruction;
        private System.Windows.Forms.Panel panelActions;
        private Guna.UI2.WinForms.Guna2Button buttonToutCocher;
        private Guna.UI2.WinForms.Guna2Button buttonToutDecocher;
        private System.Windows.Forms.Panel panelFooter;
        private Guna.UI2.WinForms.Guna2Button buttonValider;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
    }
}
