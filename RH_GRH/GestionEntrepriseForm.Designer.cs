namespace RH_GRH
{
    partial class GestionEntrepriseForm
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControlEntreprise = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.buttonAjouterEntreprise = new Guna.UI2.WinForms.Guna2Button();
            this.textBoxSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.DataGridView_Entreprise_Gestion = new System.Windows.Forms.DataGridView();
            this.panel3.SuspendLayout();
            this.tabControlEntreprise.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Entreprise_Gestion)).BeginInit();
            this.SuspendLayout();
            //
            // panel3
            //
            this.panel3.Controls.Add(this.tabControlEntreprise);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1584, 861);
            this.panel3.TabIndex = 2;
            //
            // tabControlEntreprise
            //
            this.tabControlEntreprise.Controls.Add(this.tabPage2);
            this.tabControlEntreprise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEntreprise.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlEntreprise.ItemSize = new System.Drawing.Size(0, 1);
            this.tabControlEntreprise.Location = new System.Drawing.Point(0, 0);
            this.tabControlEntreprise.Name = "tabControlEntreprise";
            this.tabControlEntreprise.SelectedIndex = 0;
            this.tabControlEntreprise.Size = new System.Drawing.Size(1584, 861);
            this.tabControlEntreprise.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlEntreprise.TabIndex = 0;
            //
            // tabPage2
            //
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.panel6);
            this.tabPage2.Location = new System.Drawing.Point(4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1576, 852);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Gestion";
            //
            // panel6
            //
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1570, 846);
            this.panel6.TabIndex = 0;
            //
            // panel7
            //
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Controls.Add(this.buttonAjouterEntreprise);
            this.panel7.Controls.Add(this.textBoxSearch);
            this.panel7.Controls.Add(this.DataGridView_Entreprise_Gestion);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1570, 846);
            this.panel7.TabIndex = 0;
            //
            // buttonAjouterEntreprise
            //
            this.buttonAjouterEntreprise.Animated = true;
            this.buttonAjouterEntreprise.AutoRoundedCorners = true;
            this.buttonAjouterEntreprise.BorderRadius = 26;
            this.buttonAjouterEntreprise.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAjouterEntreprise.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAjouterEntreprise.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAjouterEntreprise.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAjouterEntreprise.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.buttonAjouterEntreprise.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Bold);
            this.buttonAjouterEntreprise.ForeColor = System.Drawing.Color.White;
            this.buttonAjouterEntreprise.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(179)))), ((int)(((byte)(113)))));
            this.buttonAjouterEntreprise.Location = new System.Drawing.Point(11, 6);
            this.buttonAjouterEntreprise.Name = "buttonAjouterEntreprise";
            this.buttonAjouterEntreprise.ShadowDecoration.BorderRadius = 26;
            this.buttonAjouterEntreprise.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.buttonAjouterEntreprise.ShadowDecoration.Depth = 10;
            this.buttonAjouterEntreprise.ShadowDecoration.Enabled = true;
            this.buttonAjouterEntreprise.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 5, 5);
            this.buttonAjouterEntreprise.Size = new System.Drawing.Size(270, 55);
            this.buttonAjouterEntreprise.TabIndex = 1;
            this.buttonAjouterEntreprise.Text = "➕  Ajouter entreprise";
            this.buttonAjouterEntreprise.Click += new System.EventHandler(this.buttonAjouterEntreprise_Click);
            //
            // textBoxSearch
            //
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.AutoRoundedCorners = true;
            this.textBoxSearch.BorderRadius = 23;
            this.textBoxSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxSearch.DefaultText = "";
            this.textBoxSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxSearch.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.textBoxSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxSearch.Font = new System.Drawing.Font("Montserrat", 9F);
            this.textBoxSearch.ForeColor = System.Drawing.Color.Black;
            this.textBoxSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxSearch.Location = new System.Drawing.Point(1171, 11);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.PasswordChar = '\0';
            this.textBoxSearch.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.textBoxSearch.PlaceholderText = "🔍 Rechercher une entreprise...";
            this.textBoxSearch.SelectedText = "";
            this.textBoxSearch.ShadowDecoration.BorderRadius = 23;
            this.textBoxSearch.ShadowDecoration.Color = System.Drawing.Color.Silver;
            this.textBoxSearch.ShadowDecoration.Depth = 8;
            this.textBoxSearch.ShadowDecoration.Enabled = true;
            this.textBoxSearch.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 4, 4);
            this.textBoxSearch.Size = new System.Drawing.Size(393, 49);
            this.textBoxSearch.TabIndex = 54;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            //
            // DataGridView_Entreprise_Gestion
            //
            this.DataGridView_Entreprise_Gestion.AllowUserToAddRows = false;
            this.DataGridView_Entreprise_Gestion.AllowUserToDeleteRows = false;
            this.DataGridView_Entreprise_Gestion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView_Entreprise_Gestion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView_Entreprise_Gestion.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView_Entreprise_Gestion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridView_Entreprise_Gestion.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView_Entreprise_Gestion.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView_Entreprise_Gestion.ColumnHeadersHeight = 35;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView_Entreprise_Gestion.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView_Entreprise_Gestion.EnableHeadersVisualStyles = false;
            this.DataGridView_Entreprise_Gestion.GridColor = System.Drawing.Color.LightGray;
            this.DataGridView_Entreprise_Gestion.Location = new System.Drawing.Point(0, 73);
            this.DataGridView_Entreprise_Gestion.MultiSelect = false;
            this.DataGridView_Entreprise_Gestion.Name = "DataGridView_Entreprise_Gestion";
            this.DataGridView_Entreprise_Gestion.ReadOnly = true;
            this.DataGridView_Entreprise_Gestion.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.DataGridView_Entreprise_Gestion.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridView_Entreprise_Gestion.RowTemplate.Height = 45;
            this.DataGridView_Entreprise_Gestion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView_Entreprise_Gestion.Size = new System.Drawing.Size(1570, 773);
            this.DataGridView_Entreprise_Gestion.TabIndex = 0;
            this.DataGridView_Entreprise_Gestion.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_Entreprise_Gestion_CellClick);
            this.DataGridView_Entreprise_Gestion.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_Entreprise_Gestion_CellMouseMove);
            this.DataGridView_Entreprise_Gestion.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DataGridView_Entreprise_Gestion_CellPainting);
            //
            // GestionEntrepriseForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GestionEntrepriseForm";
            this.Text = "Gestion des entreprises";
            this.panel3.ResumeLayout(false);
            this.tabControlEntreprise.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Entreprise_Gestion)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl tabControlEntreprise;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private Guna.UI2.WinForms.Guna2Button buttonAjouterEntreprise;
        private Guna.UI2.WinForms.Guna2TextBox textBoxSearch;
        private System.Windows.Forms.DataGridView DataGridView_Entreprise_Gestion;
    }
}
