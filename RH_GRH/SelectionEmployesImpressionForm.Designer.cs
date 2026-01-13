namespace RH_GRH
{
    partial class SelectionEmployesImpressionForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelCompteur = new System.Windows.Forms.Label();
            this.buttonGenerer = new Guna.UI2.WinForms.Guna2Button();
            this.buttonAnnuler = new Guna.UI2.WinForms.Guna2Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2DateTimePickerDebut = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.guna2DateTimePickerFin = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxTypeContrat = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDossier = new System.Windows.Forms.TextBox();
            this.buttonParcourir = new Guna.UI2.WinForms.Guna2Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxTout = new System.Windows.Forms.CheckBox();
            this.dataGridViewEmployes = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployes)).BeginInit();
            this.SuspendLayout();
            //
            // panel1
            //
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 70);
            this.panel1.TabIndex = 0;
            //
            // label1
            //
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(900, 70);
            this.label1.TabIndex = 0;
            this.label1.Text = "🖨️ IMPRESSION EN LOT DES BULLETINS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // panel2
            //
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.labelCompteur);
            this.panel2.Controls.Add(this.buttonAnnuler);
            this.panel2.Controls.Add(this.buttonGenerer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 610);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(900, 70);
            this.panel2.TabIndex = 1;
            //
            // labelCompteur
            //
            this.labelCompteur.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Bold);
            this.labelCompteur.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.labelCompteur.Location = new System.Drawing.Point(20, 15);
            this.labelCompteur.Name = "labelCompteur";
            this.labelCompteur.Size = new System.Drawing.Size(400, 40);
            this.labelCompteur.TabIndex = 2;
            this.labelCompteur.Text = "0 / 0 employé(s) sélectionné(s)";
            this.labelCompteur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // buttonGenerer
            //
            this.buttonGenerer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerer.BorderRadius = 8;
            this.buttonGenerer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonGenerer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonGenerer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonGenerer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonGenerer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonGenerer.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonGenerer.ForeColor = System.Drawing.Color.White;
            this.buttonGenerer.Location = new System.Drawing.Point(580, 15);
            this.buttonGenerer.Name = "buttonGenerer";
            this.buttonGenerer.Size = new System.Drawing.Size(150, 40);
            this.buttonGenerer.TabIndex = 0;
            this.buttonGenerer.Text = "🖨️ Générer";
            this.buttonGenerer.Click += new System.EventHandler(this.buttonGenerer_Click);
            //
            // buttonAnnuler
            //
            this.buttonAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAnnuler.BorderRadius = 8;
            this.buttonAnnuler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAnnuler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAnnuler.FillColor = System.Drawing.Color.Gray;
            this.buttonAnnuler.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.White;
            this.buttonAnnuler.Location = new System.Drawing.Point(740, 15);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(150, 40);
            this.buttonAnnuler.TabIndex = 1;
            this.buttonAnnuler.Text = "❌ Annuler";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            //
            // panel3
            //
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 70);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(15);
            this.panel3.Size = new System.Drawing.Size(900, 540);
            this.panel3.TabIndex = 2;
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.comboBoxTypeContrat);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.guna2DateTimePickerFin);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.guna2DateTimePickerDebut);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(15, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(870, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "📅 Période et Filtres";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 9F);
            this.label2.Location = new System.Drawing.Point(20, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Du :";
            //
            // guna2DateTimePickerDebut
            //
            this.guna2DateTimePickerDebut.BorderRadius = 5;
            this.guna2DateTimePickerDebut.Checked = true;
            this.guna2DateTimePickerDebut.FillColor = System.Drawing.Color.White;
            this.guna2DateTimePickerDebut.Font = new System.Drawing.Font("Montserrat", 9F);
            this.guna2DateTimePickerDebut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.guna2DateTimePickerDebut.Location = new System.Drawing.Point(60, 30);
            this.guna2DateTimePickerDebut.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.guna2DateTimePickerDebut.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.guna2DateTimePickerDebut.Name = "guna2DateTimePickerDebut";
            this.guna2DateTimePickerDebut.Size = new System.Drawing.Size(180, 36);
            this.guna2DateTimePickerDebut.TabIndex = 1;
            this.guna2DateTimePickerDebut.Value = new System.DateTime(2026, 1, 10, 0, 0, 0, 0);
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Montserrat", 9F);
            this.label3.Location = new System.Drawing.Point(260, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Au :";
            //
            // guna2DateTimePickerFin
            //
            this.guna2DateTimePickerFin.BorderRadius = 5;
            this.guna2DateTimePickerFin.Checked = true;
            this.guna2DateTimePickerFin.FillColor = System.Drawing.Color.White;
            this.guna2DateTimePickerFin.Font = new System.Drawing.Font("Montserrat", 9F);
            this.guna2DateTimePickerFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.guna2DateTimePickerFin.Location = new System.Drawing.Point(300, 30);
            this.guna2DateTimePickerFin.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.guna2DateTimePickerFin.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.guna2DateTimePickerFin.Name = "guna2DateTimePickerFin";
            this.guna2DateTimePickerFin.Size = new System.Drawing.Size(180, 36);
            this.guna2DateTimePickerFin.TabIndex = 3;
            this.guna2DateTimePickerFin.Value = new System.DateTime(2026, 1, 10, 0, 0, 0, 0);
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Montserrat", 9F);
            this.label4.Location = new System.Drawing.Point(510, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Type contrat :";
            //
            // comboBoxTypeContrat
            //
            this.comboBoxTypeContrat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTypeContrat.Font = new System.Drawing.Font("Montserrat", 9F);
            this.comboBoxTypeContrat.FormattingEnabled = true;
            this.comboBoxTypeContrat.Items.AddRange(new object[] {
            "Tous",
            "Horaire",
            "Journalier"});
            this.comboBoxTypeContrat.Location = new System.Drawing.Point(610, 32);
            this.comboBoxTypeContrat.Name = "comboBoxTypeContrat";
            this.comboBoxTypeContrat.Size = new System.Drawing.Size(180, 26);
            this.comboBoxTypeContrat.TabIndex = 5;
            this.comboBoxTypeContrat.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypeContrat_SelectedIndexChanged);
            //
            // groupBox2
            //
            this.groupBox2.Controls.Add(this.buttonParcourir);
            this.groupBox2.Controls.Add(this.textBoxDossier);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(15, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(870, 80);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "📁 Dossier de destination";
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Montserrat", 9F);
            this.label5.Location = new System.Drawing.Point(20, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Dossier :";
            //
            // textBoxDossier
            //
            this.textBoxDossier.Font = new System.Drawing.Font("Montserrat", 9F);
            this.textBoxDossier.Location = new System.Drawing.Point(90, 32);
            this.textBoxDossier.Name = "textBoxDossier";
            this.textBoxDossier.ReadOnly = true;
            this.textBoxDossier.Size = new System.Drawing.Size(600, 26);
            this.textBoxDossier.TabIndex = 1;
            //
            // buttonParcourir
            //
            this.buttonParcourir.BorderRadius = 5;
            this.buttonParcourir.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonParcourir.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonParcourir.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonParcourir.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonParcourir.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.buttonParcourir.Font = new System.Drawing.Font("Montserrat", 9F);
            this.buttonParcourir.ForeColor = System.Drawing.Color.White;
            this.buttonParcourir.Location = new System.Drawing.Point(700, 28);
            this.buttonParcourir.Name = "buttonParcourir";
            this.buttonParcourir.Size = new System.Drawing.Size(150, 34);
            this.buttonParcourir.TabIndex = 2;
            this.buttonParcourir.Text = "📁 Parcourir...";
            this.buttonParcourir.Click += new System.EventHandler(this.buttonParcourir_Click);
            //
            // groupBox3
            //
            this.groupBox3.Controls.Add(this.dataGridViewEmployes);
            this.groupBox3.Controls.Add(this.checkBoxTout);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(15, 195);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(870, 330);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "👥 Employés";
            //
            // checkBoxTout
            //
            this.checkBoxTout.AutoSize = true;
            this.checkBoxTout.Font = new System.Drawing.Font("Montserrat", 9F);
            this.checkBoxTout.Location = new System.Drawing.Point(20, 30);
            this.checkBoxTout.Name = "checkBoxTout";
            this.checkBoxTout.Size = new System.Drawing.Size(160, 24);
            this.checkBoxTout.TabIndex = 0;
            this.checkBoxTout.Text = "✓ Tout sélectionner";
            this.checkBoxTout.UseVisualStyleBackColor = true;
            this.checkBoxTout.CheckedChanged += new System.EventHandler(this.checkBoxTout_CheckedChanged);
            //
            // dataGridViewEmployes
            //
            this.dataGridViewEmployes.AllowUserToAddRows = false;
            this.dataGridViewEmployes.AllowUserToDeleteRows = false;
            this.dataGridViewEmployes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEmployes.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewEmployes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmployes.Location = new System.Drawing.Point(20, 60);
            this.dataGridViewEmployes.Name = "dataGridViewEmployes";
            this.dataGridViewEmployes.RowTemplate.Height = 28;
            this.dataGridViewEmployes.Size = new System.Drawing.Size(830, 250);
            this.dataGridViewEmployes.TabIndex = 1;
            this.dataGridViewEmployes.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEmployes_CellValueChanged);
            //
            // SelectionEmployesImpressionForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 680);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectionEmployesImpressionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Impression en lot - Sélection des employés";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployes)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2DateTimePicker guna2DateTimePickerDebut;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2DateTimePicker guna2DateTimePickerFin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxTypeContrat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDossier;
        private Guna.UI2.WinForms.Guna2Button buttonParcourir;
        private System.Windows.Forms.CheckBox checkBoxTout;
        private System.Windows.Forms.DataGridView dataGridViewEmployes;
        private System.Windows.Forms.Label labelCompteur;
        private Guna.UI2.WinForms.Guna2Button buttonGenerer;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
    }
}
