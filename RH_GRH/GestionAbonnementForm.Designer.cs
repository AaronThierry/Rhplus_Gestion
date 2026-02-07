namespace RH_GRH
{
    partial class GestionAbonnementForm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DataGridView_Abonnement = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.buttonAjouter = new Guna.UI2.WinForms.Guna2Button();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Abonnement)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            //
            // panel2
            //
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1200, 70);
            this.panel2.TabIndex = 0;
            //
            // label1
            //
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Montserrat", 15F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(70, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(1200, 70);
            this.label1.TabIndex = 0;
            this.label1.Text = "GESTION DES ABONNEMENTS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panel1
            //
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.DataGridView_Abonnement);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 160);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(20);
            this.panel1.Size = new System.Drawing.Size(1200, 540);
            this.panel1.TabIndex = 1;
            //
            // DataGridView_Abonnement
            //
            this.DataGridView_Abonnement.AllowUserToAddRows = false;
            this.DataGridView_Abonnement.AllowUserToDeleteRows = false;
            this.DataGridView_Abonnement.AllowUserToResizeRows = false;
            this.DataGridView_Abonnement.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView_Abonnement.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView_Abonnement.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridView_Abonnement.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DataGridView_Abonnement.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView_Abonnement.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView_Abonnement.ColumnHeadersHeight = 45;
            this.DataGridView_Abonnement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Montserrat", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView_Abonnement.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView_Abonnement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridView_Abonnement.EnableHeadersVisualStyles = false;
            this.DataGridView_Abonnement.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.DataGridView_Abonnement.Location = new System.Drawing.Point(20, 20);
            this.DataGridView_Abonnement.MultiSelect = false;
            this.DataGridView_Abonnement.Name = "DataGridView_Abonnement";
            this.DataGridView_Abonnement.ReadOnly = true;
            this.DataGridView_Abonnement.RowHeadersVisible = false;
            this.DataGridView_Abonnement.RowHeadersWidth = 51;
            this.DataGridView_Abonnement.RowTemplate.Height = 50;
            this.DataGridView_Abonnement.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView_Abonnement.Size = new System.Drawing.Size(1160, 500);
            this.DataGridView_Abonnement.TabIndex = 0;
            //
            // panel3
            //
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.textBoxSearch);
            this.panel3.Controls.Add(this.buttonAjouter);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 70);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(20);
            this.panel3.Size = new System.Drawing.Size(1200, 90);
            this.panel3.TabIndex = 2;
            //
            // textBoxSearch
            //
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.BorderRadius = 10;
            this.textBoxSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxSearch.DefaultText = "";
            this.textBoxSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.textBoxSearch.Font = new System.Drawing.Font("Montserrat", 10F);
            this.textBoxSearch.ForeColor = System.Drawing.Color.Black;
            this.textBoxSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(165)))), ((int)(((byte)(245)))));
            this.textBoxSearch.Location = new System.Drawing.Point(23, 23);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.PasswordChar = '\0';
            this.textBoxSearch.PlaceholderText = "🔍 Rechercher un abonnement...";
            this.textBoxSearch.SelectedText = "";
            this.textBoxSearch.Size = new System.Drawing.Size(820, 45);
            this.textBoxSearch.TabIndex = 0;
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            //
            // buttonAjouter
            //
            this.buttonAjouter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAjouter.Animated = true;
            this.buttonAjouter.AutoRoundedCorners = true;
            this.buttonAjouter.BorderRadius = 21;
            this.buttonAjouter.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAjouter.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAjouter.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAjouter.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAjouter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.buttonAjouter.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAjouter.ForeColor = System.Drawing.Color.White;
            this.buttonAjouter.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(165)))), ((int)(((byte)(245)))));
            this.buttonAjouter.Location = new System.Drawing.Point(870, 23);
            this.buttonAjouter.Name = "buttonAjouter";
            this.buttonAjouter.Size = new System.Drawing.Size(310, 45);
            this.buttonAjouter.TabIndex = 1;
            this.buttonAjouter.Text = "➕ Ajouter Abonnement";
            this.buttonAjouter.Click += new System.EventHandler(this.buttonAjouter_Click);
            //
            // GestionAbonnementForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GestionAbonnementForm";
            this.Text = "Gestion des Abonnements";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Abonnement)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView DataGridView_Abonnement;
        private System.Windows.Forms.Panel panel3;
        private Guna.UI2.WinForms.Guna2TextBox textBoxSearch;
        private Guna.UI2.WinForms.Guna2Button buttonAjouter;
    }
}
