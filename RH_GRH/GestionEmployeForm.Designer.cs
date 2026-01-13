namespace RH_GRH
{
    partial class GestionEmployeForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DataGridView_Employe_Gestion = new Guna.UI2.WinForms.Guna2DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.buttonImporterExcel = new Guna.UI2.WinForms.Guna2Button();
            this.buttonAjouterEmploye = new Guna.UI2.WinForms.Guna2Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Employe_Gestion)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1667, 862);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.DataGridView_Employe_Gestion);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 86);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(20, 0, 20, 20);
            this.panel3.Size = new System.Drawing.Size(1667, 776);
            this.panel3.TabIndex = 1;
            // 
            // DataGridView_Employe_Gestion
            // 
            this.DataGridView_Employe_Gestion.AllowUserToAddRows = false;
            this.DataGridView_Employe_Gestion.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.DataGridView_Employe_Gestion.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView_Employe_Gestion.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView_Employe_Gestion.ColumnHeadersHeight = 35;
            this.DataGridView_Employe_Gestion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Montserrat", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView_Employe_Gestion.DefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridView_Employe_Gestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridView_Employe_Gestion.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.DataGridView_Employe_Gestion.Location = new System.Drawing.Point(20, 123);
            this.DataGridView_Employe_Gestion.Margin = new System.Windows.Forms.Padding(4);
            this.DataGridView_Employe_Gestion.Name = "DataGridView_Employe_Gestion";
            this.DataGridView_Employe_Gestion.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView_Employe_Gestion.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DataGridView_Employe_Gestion.RowHeadersVisible = false;
            this.DataGridView_Employe_Gestion.RowHeadersWidth = 51;
            this.DataGridView_Employe_Gestion.RowTemplate.Height = 45;
            this.DataGridView_Employe_Gestion.Size = new System.Drawing.Size(1627, 633);
            this.DataGridView_Employe_Gestion.TabIndex = 1;
            this.DataGridView_Employe_Gestion.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.DataGridView_Employe_Gestion.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.DataGridView_Employe_Gestion.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.DataGridView_Employe_Gestion.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.DataGridView_Employe_Gestion.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.DataGridView_Employe_Gestion.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.DataGridView_Employe_Gestion.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.DataGridView_Employe_Gestion.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.DataGridView_Employe_Gestion.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DataGridView_Employe_Gestion.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Bold);
            this.DataGridView_Employe_Gestion.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.DataGridView_Employe_Gestion.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.DataGridView_Employe_Gestion.ThemeStyle.HeaderStyle.Height = 35;
            this.DataGridView_Employe_Gestion.ThemeStyle.ReadOnly = true;
            this.DataGridView_Employe_Gestion.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.DataGridView_Employe_Gestion.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DataGridView_Employe_Gestion.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Montserrat", 10F);
            this.DataGridView_Employe_Gestion.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.DataGridView_Employe_Gestion.ThemeStyle.RowsStyle.Height = 45;
            this.DataGridView_Employe_Gestion.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.DataGridView_Employe_Gestion.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.DataGridView_Employe_Gestion.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_Employe_Gestion_CellContentClick);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.buttonImporterExcel);
            this.panel4.Controls.Add(this.buttonAjouterEmploye);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.textBoxSearch);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(20, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(27, 18, 27, 18);
            this.panel4.Size = new System.Drawing.Size(1627, 123);
            this.panel4.TabIndex = 0;
            // 
            // buttonImporterExcel
            // 
            this.buttonImporterExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImporterExcel.Animated = true;
            this.buttonImporterExcel.AutoRoundedCorners = true;
            this.buttonImporterExcel.BackColor = System.Drawing.Color.Transparent;
            this.buttonImporterExcel.BorderRadius = 25;
            this.buttonImporterExcel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonImporterExcel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonImporterExcel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonImporterExcel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonImporterExcel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.buttonImporterExcel.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonImporterExcel.ForeColor = System.Drawing.Color.White;
            this.buttonImporterExcel.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(200)))), ((int)(((byte)(220)))));
            this.buttonImporterExcel.Location = new System.Drawing.Point(1008, 45);
            this.buttonImporterExcel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonImporterExcel.Name = "buttonImporterExcel";
            this.buttonImporterExcel.ShadowDecoration.BorderRadius = 25;
            this.buttonImporterExcel.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.buttonImporterExcel.ShadowDecoration.Depth = 10;
            this.buttonImporterExcel.ShadowDecoration.Enabled = true;
            this.buttonImporterExcel.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 5, 5);
            this.buttonImporterExcel.Size = new System.Drawing.Size(320, 52);
            this.buttonImporterExcel.TabIndex = 3;
            this.buttonImporterExcel.Text = "📥 Importer depuis Excel";
            this.buttonImporterExcel.Click += new System.EventHandler(this.buttonImporterExcel_Click);
            // 
            // buttonAjouterEmploye
            // 
            this.buttonAjouterEmploye.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAjouterEmploye.Animated = true;
            this.buttonAjouterEmploye.AutoRoundedCorners = true;
            this.buttonAjouterEmploye.BackColor = System.Drawing.Color.Transparent;
            this.buttonAjouterEmploye.BorderRadius = 25;
            this.buttonAjouterEmploye.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAjouterEmploye.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAjouterEmploye.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAjouterEmploye.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAjouterEmploye.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.buttonAjouterEmploye.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAjouterEmploye.ForeColor = System.Drawing.Color.White;
            this.buttonAjouterEmploye.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(179)))), ((int)(((byte)(113)))));
            this.buttonAjouterEmploye.Location = new System.Drawing.Point(1348, 45);
            this.buttonAjouterEmploye.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAjouterEmploye.Name = "buttonAjouterEmploye";
            this.buttonAjouterEmploye.ShadowDecoration.BorderRadius = 25;
            this.buttonAjouterEmploye.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.buttonAjouterEmploye.ShadowDecoration.Depth = 10;
            this.buttonAjouterEmploye.ShadowDecoration.Enabled = true;
            this.buttonAjouterEmploye.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 5, 5);
            this.buttonAjouterEmploye.Size = new System.Drawing.Size(280, 52);
            this.buttonAjouterEmploye.TabIndex = 2;
            this.buttonAjouterEmploye.Text = "➕ Ajouter Employé";
            this.buttonAjouterEmploye.Click += new System.EventHandler(this.buttonAjouterEmploye_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(27, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "Rechercher un employé:";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.AutoRoundedCorners = true;
            this.textBoxSearch.BackColor = System.Drawing.Color.Transparent;
            this.textBoxSearch.BorderRadius = 25;
            this.textBoxSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxSearch.DefaultText = "";
            this.textBoxSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxSearch.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.textBoxSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxSearch.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxSearch.ForeColor = System.Drawing.Color.Black;
            this.textBoxSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxSearch.Location = new System.Drawing.Point(31, 46);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.textBoxSearch.PlaceholderText = "🔍 Rechercher par nom, matricule, poste, entreprise...";
            this.textBoxSearch.SelectedText = "";
            this.textBoxSearch.ShadowDecoration.BorderRadius = 25;
            this.textBoxSearch.ShadowDecoration.Color = System.Drawing.Color.Silver;
            this.textBoxSearch.ShadowDecoration.Depth = 8;
            this.textBoxSearch.ShadowDecoration.Enabled = true;
            this.textBoxSearch.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 4, 4);
            this.textBoxSearch.Size = new System.Drawing.Size(950, 52);
            this.textBoxSearch.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(120)))), ((int)(((byte)(140)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1667, 86);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(93, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(1667, 86);
            this.label1.TabIndex = 0;
            this.label1.Text = "EMPLOYÉS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GestionEmployeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1667, 862);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GestionEmployeForm";
            this.Text = "Gestion Employés";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Employe_Gestion)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private Guna.UI2.WinForms.Guna2TextBox textBoxSearch;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Button buttonImporterExcel;
        private Guna.UI2.WinForms.Guna2Button buttonAjouterEmploye;
        private Guna.UI2.WinForms.Guna2DataGridView DataGridView_Employe_Gestion;
    }
}
