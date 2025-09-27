using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class Formmain : Form
    {
        EntrepriseClass Entreprise = new EntrepriseClass();
        public Formmain()
        {
            InitializeComponent();
            customDesign();
            count();
            // Form redimensionnable
            this.FormBorderStyle = FormBorderStyle.Sizable;

            // Boutons Agrandir / Réduire
            this.MaximizeBox = true;
            this.MinimizeBox = true;

            // Centrer à l'ouverture
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        public void count()
        {
            //Display count entreprie employe 
            // label_entreprise.Text = "Total des entreprises : " + Entreprise.totalEntreprie();
            label_nbreentreprise.Text = Entreprise.totalEntreprie();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_salaire_submenu);
        }

        private void Formmain_Load(object sender, EventArgs e)
        {
        }
        private void customDesign()
        {
            panel_personnel_submenu.Visible = false;
            panel_salaire_submenu.Visible = false;
            panel_administration_submenu.Visible = false;
        }
        private void hideSubmenu()
        {
            if(panel_personnel_submenu.Visible == true )
                panel_personnel_submenu.Visible=false;
            if (panel_salaire_submenu.Visible == true)
                panel_salaire_submenu.Visible = false; 
            if(panel_administration_submenu.Visible == true)
                panel_administration_submenu.Visible = false;

        }

        private void showSubmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSubmenu();
                submenu.Visible = true;
            }
            else
                submenu.Visible = false;
        }
        private void button_personnel_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_personnel_submenu);
        }

        private void button_administration_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_administration_submenu);
        }

        private void button_employe_Click(object sender, EventArgs e)
        {
            //...
            OpenChildForm(new GestionEmployeForm());
            //...
            hideSubmenu();
        }

        private void button_charge_Click(object sender, EventArgs e)
        {
            //...
            OpenChildForm(new GestionChargeForm());
            //...
            hideSubmenu();
        }

        private void button_indemnite_Click(object sender, EventArgs e)
        {
            //...
            OpenChildForm(new GestionIndemniteForm());
            hideSubmenu();
        }

        private void button_horaire_Click(object sender, EventArgs e)
        {
            //...
            OpenChildForm(new GestionSalaireHoraireForm());
            //...
            hideSubmenu();
        }

        private void button_journalier_Click(object sender, EventArgs e)
        {
            //...
            //..Le code
            //...
            hideSubmenu();
        }

        private void button_entreprise_Click(object sender, EventArgs e)
        {
            //...
            OpenChildForm(new GestionEntrepriseForm());
            //...
            hideSubmenu();
        }

        private void button_direction_Click(object sender, EventArgs e)
        {
            //...
            OpenChildForm(new GestionDirectionForm());
            //...
            hideSubmenu();
        }

        private void button_service_Click(object sender, EventArgs e)
        {
            //...
            OpenChildForm(new GestionServiceForm());
            //...
            hideSubmenu();
        }

        private void button_categorie_Click(object sender, EventArgs e)
        {
            //...
            OpenChildForm(new GestionCategorieForm());
            //...
            hideSubmenu();
        }

        private void button_utilisateur_Click(object sender, EventArgs e)
        {
            //...
            //..Le code
            //...
            hideSubmenu();
        }

        private void button_profil_Click(object sender, EventArgs e)
        {
            //...
            //..Le code
            //...
            hideSubmenu();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        //show Entreprise
        private Form activeForm = null;
        private void OpenChildForm(Form childForm)
        {
            if(activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button_exit_Click(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                activeForm.Close();
                panel2.Controls.Add(panel_main);
            }
            count();
        }
    }
}
