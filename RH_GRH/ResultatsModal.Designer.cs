namespace RH_GRH
{
    partial class ResultatsModal
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
            this.panelBackground = new Guna.UI2.WinForms.Guna2Panel();
            this.panelContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.panelHeader = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.labelTitre = new System.Windows.Forms.Label();
            this.labelSousTitre = new System.Windows.Forms.Label();
            this.buttonFermer = new Guna.UI2.WinForms.Guna2CircleButton();
            this.groupBoxResultatsCalcul = new Guna.UI2.WinForms.Guna2GroupBox();
            this.labelTitreNet = new System.Windows.Forms.Label();
            this.labelNetAPayer = new System.Windows.Forms.Label();
            this.labelNetEnLettres = new System.Windows.Forms.Label();
            this.groupBoxGains = new Guna.UI2.WinForms.Guna2GroupBox();
            this.listViewGains = new System.Windows.Forms.ListView();
            this.columnGain = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMontantGain = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxRetenues = new Guna.UI2.WinForms.Guna2GroupBox();
            this.listViewRetenues = new System.Windows.Forms.ListView();
            this.columnRetenue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMontantRetenue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelInfoCalcul = new System.Windows.Forms.Label();
            this.panelButtons = new Guna.UI2.WinForms.Guna2Panel();
            this.buttonImprimer = new Guna.UI2.WinForms.Guna2Button();
            this.panelBackground.SuspendLayout();
            this.panelContainer.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.groupBoxResultatsCalcul.SuspendLayout();
            this.groupBoxGains.SuspendLayout();
            this.groupBoxRetenues.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBackground
            // 
            this.panelBackground.BackColor = System.Drawing.Color.Transparent;
            this.panelBackground.Controls.Add(this.panelContainer);
            this.panelBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBackground.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.panelBackground.Location = new System.Drawing.Point(0, 0);
            this.panelBackground.Margin = new System.Windows.Forms.Padding(4);
            this.panelBackground.Name = "panelBackground";
            this.panelBackground.Size = new System.Drawing.Size(900, 800);
            this.panelBackground.TabIndex = 0;
            // 
            // panelContainer
            // 
            this.panelContainer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelContainer.BackColor = System.Drawing.Color.Transparent;
            this.panelContainer.Controls.Add(this.panelHeader);
            this.panelContainer.Controls.Add(this.groupBoxResultatsCalcul);
            this.panelContainer.Controls.Add(this.groupBoxGains);
            this.panelContainer.Controls.Add(this.groupBoxRetenues);
            this.panelContainer.Controls.Add(this.labelInfoCalcul);
            this.panelContainer.Controls.Add(this.panelButtons);
            this.panelContainer.FillColor = System.Drawing.Color.White;
            this.panelContainer.Location = new System.Drawing.Point(25, 25);
            this.panelContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.ShadowDecoration.BorderRadius = 0;
            this.panelContainer.Size = new System.Drawing.Size(850, 750);
            this.panelContainer.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.Transparent;
            this.panelHeader.Controls.Add(this.labelTitre);
            this.panelHeader.Controls.Add(this.labelSousTitre);
            this.panelHeader.Controls.Add(this.buttonFermer);
            this.panelHeader.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.panelHeader.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(850, 87);
            this.panelHeader.TabIndex = 0;
            // 
            // labelTitre
            // 
            this.labelTitre.BackColor = System.Drawing.Color.Transparent;
            this.labelTitre.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitre.ForeColor = System.Drawing.Color.White;
            this.labelTitre.Location = new System.Drawing.Point(40, 17);
            this.labelTitre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(700, 43);
            this.labelTitre.TabIndex = 0;
            this.labelTitre.Text = "📊 RÉSULTATS DU CALCUL";
            this.labelTitre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelSousTitre
            // 
            this.labelSousTitre.BackColor = System.Drawing.Color.Transparent;
            this.labelSousTitre.Font = new System.Drawing.Font("Montserrat", 10F);
            this.labelSousTitre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.labelSousTitre.Location = new System.Drawing.Point(48, 52);
            this.labelSousTitre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSousTitre.Name = "labelSousTitre";
            this.labelSousTitre.Size = new System.Drawing.Size(700, 31);
            this.labelSousTitre.TabIndex = 1;
            this.labelSousTitre.Text = "Détail complet de la paie calculée";
            this.labelSousTitre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonFermer
            // 
            this.buttonFermer.BackColor = System.Drawing.Color.Transparent;
            this.buttonFermer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonFermer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonFermer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonFermer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonFermer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.buttonFermer.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.buttonFermer.ForeColor = System.Drawing.Color.White;
            this.buttonFermer.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.buttonFermer.Location = new System.Drawing.Point(770, 7);
            this.buttonFermer.Margin = new System.Windows.Forms.Padding(4);
            this.buttonFermer.Name = "buttonFermer";
            this.buttonFermer.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.buttonFermer.Size = new System.Drawing.Size(67, 62);
            this.buttonFermer.TabIndex = 2;
            this.buttonFermer.Text = "✕";
            this.buttonFermer.Click += new System.EventHandler(this.buttonFermer_Click);
            // 
            // groupBoxResultatsCalcul
            // 
            this.groupBoxResultatsCalcul.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.groupBoxResultatsCalcul.BorderThickness = 0;
            this.groupBoxResultatsCalcul.Controls.Add(this.labelTitreNet);
            this.groupBoxResultatsCalcul.Controls.Add(this.labelNetAPayer);
            this.groupBoxResultatsCalcul.Controls.Add(this.labelNetEnLettres);
            this.groupBoxResultatsCalcul.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.groupBoxResultatsCalcul.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(252)))), ((int)(((byte)(250)))));
            this.groupBoxResultatsCalcul.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxResultatsCalcul.ForeColor = System.Drawing.Color.White;
            this.groupBoxResultatsCalcul.Location = new System.Drawing.Point(30, 95);
            this.groupBoxResultatsCalcul.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxResultatsCalcul.Name = "groupBoxResultatsCalcul";
            this.groupBoxResultatsCalcul.Size = new System.Drawing.Size(790, 160);
            this.groupBoxResultatsCalcul.TabIndex = 1;
            this.groupBoxResultatsCalcul.Text = "💰 NET À PAYER";
            // 
            // labelTitreNet
            // 
            this.labelTitreNet.BackColor = System.Drawing.Color.Transparent;
            this.labelTitreNet.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.labelTitreNet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.labelTitreNet.Location = new System.Drawing.Point(13, 47);
            this.labelTitreNet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTitreNet.Name = "labelTitreNet";
            this.labelTitreNet.Size = new System.Drawing.Size(770, 27);
            this.labelTitreNet.TabIndex = 0;
            this.labelTitreNet.Text = "Salaire Net à Payer";
            this.labelTitreNet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelNetAPayer
            // 
            this.labelNetAPayer.BackColor = System.Drawing.Color.Transparent;
            this.labelNetAPayer.Font = new System.Drawing.Font("Montserrat", 28F, System.Drawing.FontStyle.Bold);
            this.labelNetAPayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.labelNetAPayer.Location = new System.Drawing.Point(13, 65);
            this.labelNetAPayer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNetAPayer.Name = "labelNetAPayer";
            this.labelNetAPayer.Size = new System.Drawing.Size(770, 64);
            this.labelNetAPayer.TabIndex = 1;
            this.labelNetAPayer.Text = "0 FCFA";
            this.labelNetAPayer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelNetEnLettres
            // 
            this.labelNetEnLettres.BackColor = System.Drawing.Color.Transparent;
            this.labelNetEnLettres.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Italic);
            this.labelNetEnLettres.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.labelNetEnLettres.Location = new System.Drawing.Point(13, 123);
            this.labelNetEnLettres.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNetEnLettres.Name = "labelNetEnLettres";
            this.labelNetEnLettres.Size = new System.Drawing.Size(770, 32);
            this.labelNetEnLettres.TabIndex = 2;
            this.labelNetEnLettres.Text = "(zéro francs CFA)";
            this.labelNetEnLettres.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBoxGains
            // 
            this.groupBoxGains.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.groupBoxGains.BorderThickness = 0;
            this.groupBoxGains.Controls.Add(this.listViewGains);
            this.groupBoxGains.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.groupBoxGains.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxGains.ForeColor = System.Drawing.Color.White;
            this.groupBoxGains.Location = new System.Drawing.Point(30, 253);
            this.groupBoxGains.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxGains.Name = "groupBoxGains";
            this.groupBoxGains.Size = new System.Drawing.Size(790, 194);
            this.groupBoxGains.TabIndex = 2;
            this.groupBoxGains.Text = "💚 GAINS ET INDEMNITÉS";
            // 
            // listViewGains
            // 
            this.listViewGains.BackColor = System.Drawing.Color.White;
            this.listViewGains.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewGains.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnGain,
            this.columnMontantGain});
            this.listViewGains.Font = new System.Drawing.Font("Montserrat", 9F);
            this.listViewGains.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.listViewGains.FullRowSelect = true;
            this.listViewGains.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewGains.HideSelection = false;
            this.listViewGains.Location = new System.Drawing.Point(20, 50);
            this.listViewGains.Margin = new System.Windows.Forms.Padding(4);
            this.listViewGains.Name = "listViewGains";
            this.listViewGains.Size = new System.Drawing.Size(770, 137);
            this.listViewGains.TabIndex = 0;
            this.listViewGains.UseCompatibleStateImageBehavior = false;
            this.listViewGains.View = System.Windows.Forms.View.Details;
            this.listViewGains.SelectedIndexChanged += new System.EventHandler(this.listViewGains_SelectedIndexChanged);
            // 
            // columnGain
            // 
            this.columnGain.Text = "Description";
            this.columnGain.Width = 425;
            // 
            // columnMontantGain
            // 
            this.columnMontantGain.Text = "Montant";
            this.columnMontantGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnMontantGain.Width = 130;
            // 
            // groupBoxRetenues
            // 
            this.groupBoxRetenues.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.groupBoxRetenues.BorderThickness = 0;
            this.groupBoxRetenues.Controls.Add(this.listViewRetenues);
            this.groupBoxRetenues.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.groupBoxRetenues.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxRetenues.ForeColor = System.Drawing.Color.White;
            this.groupBoxRetenues.Location = new System.Drawing.Point(30, 456);
            this.groupBoxRetenues.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxRetenues.Name = "groupBoxRetenues";
            this.groupBoxRetenues.Size = new System.Drawing.Size(790, 193);
            this.groupBoxRetenues.TabIndex = 3;
            this.groupBoxRetenues.Text = "🔴 RETENUES ET COTISATIONS";
            // 
            // listViewRetenues
            // 
            this.listViewRetenues.BackColor = System.Drawing.Color.White;
            this.listViewRetenues.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewRetenues.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnRetenue,
            this.columnMontantRetenue});
            this.listViewRetenues.Font = new System.Drawing.Font("Montserrat", 9F);
            this.listViewRetenues.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.listViewRetenues.FullRowSelect = true;
            this.listViewRetenues.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewRetenues.HideSelection = false;
            this.listViewRetenues.Location = new System.Drawing.Point(20, 51);
            this.listViewRetenues.Margin = new System.Windows.Forms.Padding(4);
            this.listViewRetenues.Name = "listViewRetenues";
            this.listViewRetenues.Size = new System.Drawing.Size(770, 148);
            this.listViewRetenues.TabIndex = 0;
            this.listViewRetenues.UseCompatibleStateImageBehavior = false;
            this.listViewRetenues.View = System.Windows.Forms.View.Details;
            // 
            // columnRetenue
            // 
            this.columnRetenue.Text = "Description";
            this.columnRetenue.Width = 425;
            // 
            // columnMontantRetenue
            // 
            this.columnMontantRetenue.Text = "Montant";
            this.columnMontantRetenue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnMontantRetenue.Width = 130;
            // 
            // labelInfoCalcul
            // 
            this.labelInfoCalcul.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Italic);
            this.labelInfoCalcul.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.labelInfoCalcul.Location = new System.Drawing.Point(30, 659);
            this.labelInfoCalcul.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInfoCalcul.Name = "labelInfoCalcul";
            this.labelInfoCalcul.Size = new System.Drawing.Size(790, 27);
            this.labelInfoCalcul.TabIndex = 4;
            this.labelInfoCalcul.Text = "📅 Période : --/--/---- au --/--/----";
            this.labelInfoCalcul.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.Transparent;
            this.panelButtons.Controls.Add(this.buttonImprimer);
            this.panelButtons.Location = new System.Drawing.Point(30, 690);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(4);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(790, 55);
            this.panelButtons.TabIndex = 5;
            // 
            // buttonImprimer
            // 
            this.buttonImprimer.Animated = true;
            this.buttonImprimer.BorderRadius = 8;
            this.buttonImprimer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonImprimer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonImprimer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonImprimer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonImprimer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.buttonImprimer.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Bold);
            this.buttonImprimer.ForeColor = System.Drawing.Color.White;
            this.buttonImprimer.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.buttonImprimer.Location = new System.Drawing.Point(195, 5);
            this.buttonImprimer.Margin = new System.Windows.Forms.Padding(4);
            this.buttonImprimer.Name = "buttonImprimer";
            this.buttonImprimer.Size = new System.Drawing.Size(400, 45);
            this.buttonImprimer.TabIndex = 0;
            this.buttonImprimer.Text = "🖨️ IMPRIMER LE BULLETIN";
            this.buttonImprimer.Click += new System.EventHandler(this.buttonImprimer_Click);
            // 
            // ResultatsModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(900, 800);
            this.Controls.Add(this.panelBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ResultatsModal";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Résultats du Calcul";
            this.panelBackground.ResumeLayout(false);
            this.panelContainer.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.groupBoxResultatsCalcul.ResumeLayout(false);
            this.groupBoxGains.ResumeLayout(false);
            this.groupBoxRetenues.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelBackground;
        private Guna.UI2.WinForms.Guna2Panel panelContainer;
        private Guna.UI2.WinForms.Guna2GradientPanel panelHeader;
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.Label labelSousTitre;
        private Guna.UI2.WinForms.Guna2CircleButton buttonFermer;
        private Guna.UI2.WinForms.Guna2GroupBox groupBoxResultatsCalcul;
        private System.Windows.Forms.Label labelTitreNet;
        private System.Windows.Forms.Label labelNetAPayer;
        private System.Windows.Forms.Label labelNetEnLettres;
        private Guna.UI2.WinForms.Guna2GroupBox groupBoxGains;
        private System.Windows.Forms.ListView listViewGains;
        private System.Windows.Forms.ColumnHeader columnGain;
        private System.Windows.Forms.ColumnHeader columnMontantGain;
        private Guna.UI2.WinForms.Guna2GroupBox groupBoxRetenues;
        private System.Windows.Forms.ListView listViewRetenues;
        private System.Windows.Forms.ColumnHeader columnRetenue;
        private System.Windows.Forms.ColumnHeader columnMontantRetenue;
        private System.Windows.Forms.Label labelInfoCalcul;
        private Guna.UI2.WinForms.Guna2Panel panelButtons;
        private Guna.UI2.WinForms.Guna2Button buttonImprimer;
    }
}
