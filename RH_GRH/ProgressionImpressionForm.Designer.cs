namespace RH_GRH
{
    partial class ProgressionImpressionForm
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
            this.buttonAnnuler = new Guna.UI2.WinForms.Guna2Button();
            this.labelTempsEstime = new System.Windows.Forms.Label();
            this.labelTempsEcoule = new System.Windows.Forms.Label();
            this.labelCompteurs = new System.Windows.Forms.Label();
            this.labelEmployeEnCours = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelPourcentage = new System.Windows.Forms.Label();
            this.labelStatut = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(533, 56);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(533, 56);
            this.label1.TabIndex = 0;
            this.label1.Text = "⚡ GÉNÉRATION EN COURS...";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonAnnuler);
            this.panel2.Controls.Add(this.labelTempsEstime);
            this.panel2.Controls.Add(this.labelTempsEcoule);
            this.panel2.Controls.Add(this.labelCompteurs);
            this.panel2.Controls.Add(this.labelEmployeEnCours);
            this.panel2.Controls.Add(this.progressBar);
            this.panel2.Controls.Add(this.labelPourcentage);
            this.panel2.Controls.Add(this.labelStatut);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 56);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(27, 24, 27, 24);
            this.panel2.Size = new System.Drawing.Size(533, 304);
            this.panel2.TabIndex = 1;
            // 
            // buttonAnnuler
            // 
            this.buttonAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonAnnuler.BorderRadius = 8;
            this.buttonAnnuler.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonAnnuler.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonAnnuler.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonAnnuler.FillColor = System.Drawing.Color.Red;
            this.buttonAnnuler.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAnnuler.ForeColor = System.Drawing.Color.White;
            this.buttonAnnuler.Location = new System.Drawing.Point(187, 252);
            this.buttonAnnuler.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(160, 32);
            this.buttonAnnuler.TabIndex = 7;
            this.buttonAnnuler.Text = "❌ Annuler";
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            // 
            // labelTempsEstime
            // 
            this.labelTempsEstime.Font = new System.Drawing.Font("Montserrat", 9F);
            this.labelTempsEstime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelTempsEstime.Location = new System.Drawing.Point(276, 216);
            this.labelTempsEstime.Name = "labelTempsEstime";
            this.labelTempsEstime.Size = new System.Drawing.Size(231, 20);
            this.labelTempsEstime.TabIndex = 6;
            this.labelTempsEstime.Text = "Temps estimé : 00:00";
            this.labelTempsEstime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelTempsEcoule
            // 
            this.labelTempsEcoule.Font = new System.Drawing.Font("Montserrat", 9F);
            this.labelTempsEcoule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelTempsEcoule.Location = new System.Drawing.Point(27, 216);
            this.labelTempsEcoule.Name = "labelTempsEcoule";
            this.labelTempsEcoule.Size = new System.Drawing.Size(231, 20);
            this.labelTempsEcoule.TabIndex = 5;
            this.labelTempsEcoule.Text = "Temps écoulé : 00:00";
            // 
            // labelCompteurs
            // 
            this.labelCompteurs.Font = new System.Drawing.Font("Montserrat", 9F);
            this.labelCompteurs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelCompteurs.Location = new System.Drawing.Point(27, 188);
            this.labelCompteurs.Name = "labelCompteurs";
            this.labelCompteurs.Size = new System.Drawing.Size(480, 20);
            this.labelCompteurs.TabIndex = 4;
            this.labelCompteurs.Text = "✅ Réussis : 0  |  ❌ Erreurs : 0  |  ⏳ Restants : 0";
            this.labelCompteurs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelEmployeEnCours
            // 
            this.labelEmployeEnCours.Font = new System.Drawing.Font("Montserrat", 10F);
            this.labelEmployeEnCours.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelEmployeEnCours.Location = new System.Drawing.Point(27, 160);
            this.labelEmployeEnCours.Name = "labelEmployeEnCours";
            this.labelEmployeEnCours.Size = new System.Drawing.Size(480, 20);
            this.labelEmployeEnCours.TabIndex = 3;
            this.labelEmployeEnCours.Text = "En cours : ...";
            this.labelEmployeEnCours.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(27, 128);
            this.progressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(480, 24);
            this.progressBar.TabIndex = 2;
            // 
            // labelPourcentage
            // 
            this.labelPourcentage.Font = new System.Drawing.Font("Montserrat", 36F, System.Drawing.FontStyle.Bold);
            this.labelPourcentage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.labelPourcentage.Location = new System.Drawing.Point(27, 38);
            this.labelPourcentage.Name = "labelPourcentage";
            this.labelPourcentage.Size = new System.Drawing.Size(480, 84);
            this.labelPourcentage.TabIndex = 1;
            this.labelPourcentage.Text = "0%";
            this.labelPourcentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStatut
            // 
            this.labelStatut.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Bold);
            this.labelStatut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.labelStatut.Location = new System.Drawing.Point(27, 10);
            this.labelStatut.Name = "labelStatut";
            this.labelStatut.Size = new System.Drawing.Size(480, 24);
            this.labelStatut.TabIndex = 0;
            this.labelStatut.Text = "⚡ Génération en cours...";
            this.labelStatut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgressionImpressionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 360);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressionImpressionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Génération des bulletins en cours...";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelStatut;
        private System.Windows.Forms.Label labelPourcentage;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelEmployeEnCours;
        private System.Windows.Forms.Label labelCompteurs;
        private System.Windows.Forms.Label labelTempsEcoule;
        private System.Windows.Forms.Label labelTempsEstime;
        private Guna.UI2.WinForms.Guna2Button buttonAnnuler;
    }
}
