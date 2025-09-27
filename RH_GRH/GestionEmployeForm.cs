using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class GestionEmployeForm : Form
    {
        public GestionEmployeForm()
        {
            InitializeComponent();
            StyliserDataGridView();
            StyliserDataGridViewGestion();
            StyliserTabControl();
            ShowTablePersonnel();
            ShowTablePersonnelGestion();
            ClearFormPersonnelGestion();
        }


        private void GestionEmployeForm_Load(object sender, EventArgs e)
        {
            // Charger la liste des entreprises
            EntrepriseClass.ChargerEntreprises(ComboBoxEntreprise, null, true);
            EntrepriseClass.ChargerEntreprises(ComboBoxEntrepriseGestion, null, true);
           // DirectionClass.ChargerDirections(ComboBoxDirectionGestion, null, true);
            //ServiceClass.ChargerServices(ComboBoxServiceGestion, ,null, true);
           // Categorie.ChargerCategories(ComboBoxCategorieGestion, null, true);

            // Recharger les combos quand l’entreprise change
            ComboBoxEntreprise.SelectedIndexChanged += ComboBoxEntreprise_SelectedIndexChanged;


            // Brancher les events
            CheckBoxCDD.CheckedChanged += (_, __) => ToggleContratAndDate();
            CheckBoxCDI.CheckedChanged += (_, __) => ToggleContratAndDate();
            CheckBoxCDDGestion.CheckedChanged += (_, __) => ToggleContratAndDate();
            CheckBoxCDIGestion.CheckedChanged += (_, __) => ToggleContratAndDate();

            // Init "nullable" (affiche vide mais ne force pas disabled ici)
            InitNullableDatePicker(dateSortiePicker);

            // IMPORTANT : appliquer l’état initial (si CDD est déjà coché au designer)
            // BeginInvoke garantit que ça s’exécute après toutes les inits du designer.
            BeginInvoke((MethodInvoker)ToggleContratAndDate);
            ChampsDisableds();


            // après avoir chargé la Combo (DataSource / Items)
            ComboBoxModePayement.SelectedIndexChanged += ComboBoxModePayement_SelectedIndexChanged;
            ComboBoxModePayement.SelectedValueChanged += ComboBoxModePayement_SelectedIndexChanged;


            //*******************************************************************************
            ComboBoxtypecontrat.SelectedIndexChanged += ComboBoxtypecontrat_SelectedIndexChanged;
            ComboBoxtypecontrat.SelectedValueChanged += ComboBoxtypecontrat_SelectedIndexChanged;



            // Appliquer l’état initial une fois le binding terminé
            this.BeginInvoke(new MethodInvoker(ApplyModePaiementUI));
            this.BeginInvoke(new MethodInvoker(ApplyTypeContratUI));

            tabControlPersonnel.Selected += tabControlPersonnel_Selected;
            tabControlPersonnel.Deselected += tabControlPersonnel_Deselected;

        }


        private void tabControlPersonnel_Deselected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabPage2) // ta TabPage Gestion
            {
                ClearFormPersonnelGestion();
                ChampsDisabledsGestion();
          //      dataGridViewPersonnelGestion.ClearSelection(); // évite un remplissage auto
            }
        }


        private void tabControlPersonnel_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabPage2)
            {
                ShowTablePersonnelGestion();               // recharge la grille
                //dataGridViewPersonnelGestion.ClearSelection(); // aucune ligne sélectionnée
                ClearFormPersonnelGestion();               // champs vides
            }
            else if (e.TabPage == tabPage1) // ton autre onglet
            {
                ShowTablePersonnel();
                ClearFormPersonnel();
            }
        }




        ////////////////////////////////////////////////////////////////////////////

        private void ShowTablePersonnel()
        {
            var dt = EmployeClass.GetEmployeList();
            DataGridView_Personnel.AutoGenerateColumns = true;
            DataGridView_Personnel.DataSource = dt;
            // Masquer les IDs si tu veux une vue clean
            if (DataGridView_Personnel.Columns.Contains("ID"))
                DataGridView_Personnel.Columns["ID"].Visible = false;
        }



        private void ShowTablePersonnelGestion()
        {
            var dt = EmployeClass.GetEmployeList();
            DataGridView_Personnel_Gestion.AutoGenerateColumns = true;
            DataGridView_Personnel_Gestion.DataSource = dt;
            // Masquer les IDs si tu veux une vue clean
            if (DataGridView_Personnel_Gestion.Columns.Contains("ID"))
                DataGridView_Personnel_Gestion.Columns["ID"].Visible = false;
        }


        private bool _loadingGestion = false;

        private void tabControlPersonnel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlPersonnel.SelectedIndex == 1) // Onglet GESTION
            {
                if (_loadingGestion) return;
                _loadingGestion = true;
                try
                {
                    // 1) Nettoie le formulaire de gestion (garde l’entreprise)
                    ClearFormPersonnelGestion();

                    // 2) Charge les entreprises avec placeholder et sélectionne-le par défaut
                    //    ⚠️ utilise la bonne signature: (combo, idSelection, ajouterPlaceholder)
                    EntrepriseClass.ChargerEntreprises(ComboBoxEntrepriseGestion, null, true);
                    ComboBoxEntrepriseGestion.SelectedIndex = 0; // placeholder "— Sélectionner —"

                    // 3) (si tu relies les combos) Vide/charge Direction/Service/Catégorie avec l’entreprise 0
                    DirectionClass.ChargerDirections(ComboBoxDirectionGestion, 0, null, true);
                    ServiceClass.ChargerServices(ComboBoxServiceGestion, 0, null, true);
                    Categorie.ChargerCategories(ComboBoxCategorieGestion, 0, null, true);

                    // 4) Style DGV puis données
                    StyliserDataGridViewGestion();

                    // 5) Affiche la table (selon entreprise sélectionnée)
                    //    Si ton ShowTablePersonnelGestion filtre par entreprise, il doit tolérer idEnt = 0 (= toutes)
                    ShowTablePersonnelGestion();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur d'initialisation de l'onglet Gestion : " + ex.Message,
                                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _loadingGestion = false;
                }
            }
            else if (tabControlPersonnel.SelectedIndex == 0) // Onglet SAISIE
            {
                ClearFormPersonnel();
                ShowTablePersonnel();
            }
        }





        ////////////////////////////////////////////////////////////////////////////

        private bool tablePage2Chargee = false;

        ///////////////////////////////////////////////////////////////////////////
        private void ChargerTablePage2(bool forcer = false)
        {
            if (tablePage2Chargee && !forcer) return;

            try
            {
                EntrepriseClass.ChargerEntreprises(ComboBoxEntrepriseGestion);
                ShowTablePersonnelGestion();
                ClearFormPersonnelGestion();
                UseWaitCursor = true;

                // 1) Récupération des données
                DataTable dt = EmployeClass.GetEmployeList() ?? new DataTable();

                // 2) Tri par défaut si dispo
                if (dt.Columns.Contains("Entreprise") && dt.Columns.Contains("Direction"))
                {
                    var dv = dt.DefaultView;
                    dv.Sort = "[Entreprise] ASC, [Direction] ASC";
                    dt = dv.ToTable();
                }

                // 3) Binding
                var grid = DataGridView_Personnel_Gestion;
                grid.AutoGenerateColumns = true;
                grid.DataSource = dt;

                // (optionnel) masquer les IDs si présents
                if (grid.Columns.Contains("ID")) grid.Columns["ID"].Visible = false;
                if (grid.Columns.Contains("ID Entreprise")) grid.Columns["ID Entreprise"].Visible = false;

                // Style perso si tu as une méthode dédiée
                StyliserDataGridView();

                tablePage2Chargee = true;
                EntrepriseClass.ChargerEntreprises(ComboBoxEntreprise);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de chargement : " + ex.Message, "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        ///////////////////////////////////////////////////////////////////////////




        private void StyliserDataGridView()
        {
            // Fond général de la grille
            DataGridView_Personnel.BackgroundColor = Color.White;

            // Désactiver le style visuel Windows
            DataGridView_Personnel.EnableHeadersVisualStyles = false;

            // Style de l'en-tête
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.MidnightBlue;
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Montserrat", 10f, FontStyle.Bold);
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            headerStyle.WrapMode = DataGridViewTriState.True;
            headerStyle.SelectionBackColor = Color.MidnightBlue;     // ← Empêche le changement au clic
            headerStyle.SelectionForeColor = Color.White;            // ← Texte toujours blanc

            DataGridView_Personnel.EnableHeadersVisualStyles = false;
            DataGridView_Personnel.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridView_Personnel.ColumnHeadersHeight = 35;

            // Style des cellules normales
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.BackColor = Color.White;
            cellStyle.ForeColor = Color.Black;
            cellStyle.Font = new Font("Montserrat", 9.5f, FontStyle.Regular);
            cellStyle.SelectionBackColor = Color.LightSteelBlue;
            cellStyle.SelectionForeColor = Color.Black;
            DataGridView_Personnel.DefaultCellStyle = cellStyle;

            // Style des lignes alternées
            DataGridView_Personnel.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;

            // Supprimer les bordures
            DataGridView_Personnel.BorderStyle = BorderStyle.None;
            DataGridView_Personnel.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Personnel.GridColor = Color.LightGray;

            // Masquer l'entête de ligne à gauche
            DataGridView_Personnel.RowHeadersVisible = false;

            // Autres options d’esthétique
            DataGridView_Personnel.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Personnel.MultiSelect = false;
        }

        ////////////////////////////////////////////////////////////////////////////

        private void StyliserDataGridViewGestion()
        {
            // Améliorer le rendu (anti-scintillement)
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, DataGridView_Personnel_Gestion, new object[] { true });

            // Fond général
            DataGridView_Personnel_Gestion.BackgroundColor = Color.White;

            // En-tête (header)
            var headerStyle = new DataGridViewCellStyle
            {
                BackColor = Color.MidnightBlue,
                ForeColor = Color.White,
                Font = new Font("Montserrat", 10f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                WrapMode = DataGridViewTriState.True,
                SelectionBackColor = Color.MidnightBlue, // Empêche changement au clic
                SelectionForeColor = Color.White
            };

            DataGridView_Personnel_Gestion.EnableHeadersVisualStyles = false;
            DataGridView_Personnel_Gestion.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridView_Personnel_Gestion.ColumnHeadersHeight = 35;

            // Cellules normales
            var cellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Montserrat", 9.5f),
                SelectionBackColor = Color.LightSteelBlue,
                SelectionForeColor = Color.Black
            };
            DataGridView_Personnel_Gestion.DefaultCellStyle = cellStyle;

            // Lignes alternées
            DataGridView_Personnel_Gestion.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;

            // Bordures & style
            DataGridView_Personnel_Gestion.BorderStyle = BorderStyle.None;
            DataGridView_Personnel_Gestion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Personnel_Gestion.GridColor = Color.LightGray;
            DataGridView_Personnel_Gestion.RowHeadersVisible = false;

            // Sélection
            DataGridView_Personnel_Gestion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Personnel_Gestion.MultiSelect = false;
        }

        ////////////////////////////////////////////////////////////////////////////

        private void StyliserTabControl()
        {
            tabControlPersonnel.Appearance = TabAppearance.Normal;
            tabControlPersonnel.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControlPersonnel.ItemSize = new Size(150, 35); // largeur, hauteur des onglets
            tabControlPersonnel.SizeMode = TabSizeMode.Fixed;
            tabControlPersonnel.DrawItem += TabControlEntreprise_DrawItem;
        }

        ////////////////////////////////////////////////////////////////////////////

        private void TabControlEntreprise_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControlPersonnel.TabPages[e.Index];
            Rectangle rect = tabControlPersonnel.GetTabRect(e.Index);
            bool isSelected = (e.Index == tabControlPersonnel.SelectedIndex);

            // Couleur de fond
            Color backColor = isSelected ? Color.MidnightBlue : Color.LightGray;
            Color foreColor = isSelected ? Color.White : Color.Black;

            using (SolidBrush brush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(brush, rect);
            }

            // Texte centré
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            using (Font font = new Font("Montserrat", 10f, FontStyle.Bold))
            using (Brush textBrush = new SolidBrush(foreColor))
            {
                e.Graphics.DrawString(page.Text, font, textBrush, rect, format);
            }
        }

        ///////////////////////////////////////////////////////////////////////////




        private void ComboBoxModePayement_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyModePaiementUI();
        }



        private void ComboBoxtypecontrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyTypeContratUI();
        }



        private void InitNullableDatePicker(Guna2DateTimePicker dtp)
        {
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = " ";     // visuellement vide
                                        // ne pas mettre dtp.Enabled = false ici
        }






        /**************************   RECUPERER CONTRAT CDD OU CDI   **********************************************/


        private string GetTypeContratOrThrow()
        {
            // CheckBox mutuellement exclusives ou RadioButton (au choix)
            if (CheckBoxCDD.Checked) return "CDD";
            if (CheckBoxCDI.Checked) return "CDI";
            throw new InvalidOperationException("Veuillez sélectionner CDD ou CDI.");
        }

        private int? ToIntOrNull(string text)
        {
            if (int.TryParse(text?.Trim(), out var v)) return v;
            return null;
        }

        private static decimal? ToDecimalFlexible(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;

            // 1) Normaliser : enlever symboles monétaires, espaces (y compris insécables)
            string s = text.Trim()
                           .Replace("\u00A0", " ")   // NBSP
                           .Replace("\u202F", " ");  // NNBSP
            s = Regex.Replace(s, @"[^\d,.\-+]", "");  // garde chiffres , . et signe

            if (s.Length == 0) return null;

            // 2) Trouver le séparateur décimal : le séparateur le plus à droite
            int lastComma = s.LastIndexOf(',');
            int lastDot = s.LastIndexOf('.');
            char decSep = '\0';
            if (lastComma >= 0 && lastDot >= 0) decSep = lastComma > lastDot ? ',' : '.';
            else if (lastComma >= 0) decSep = ',';
            else if (lastDot >= 0) decSep = '.';

            // 3) Retirer les séparateurs de milliers & unifier en '.'
            if (decSep == ',') s = s.Replace(".", "");
            else if (decSep == '.') s = s.Replace(",", "");
            if (decSep != '\0') s = s.Replace(decSep, '.');

            // 4) Parse invariant
            return decimal.TryParse(
                       s,
                       NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                       CultureInfo.InvariantCulture,
                       out var v)
                   ? v : (decimal?)null;
        }

        private object DbValue(object value) => value ?? DBNull.Value;












        /***************************   ID SERVICE   **********************************************/

        private void ChargerServices()
        {
            var connect = new Dbconnect();
            MySqlConnection cn = connect.getconnection;   // <- pas new MySqlConnection(connect)
            {
                cn.Open();
                var da = new MySqlDataAdapter("SELECT id_service, nomService FROM service", cn);
                var dt = new DataTable();
                da.Fill(dt);

                ComboBoxService.DataSource = dt;
                ComboBoxService.DisplayMember = "nomService";  // ce que l’utilisateur voit
                ComboBoxService.ValueMember = "id_service";           // la vraie clé en DB
            }
        }






        /***************************   ID DIRECTION   **********************************************/

        private void ChargerDirection()
        {
            var connect = new Dbconnect();
            MySqlConnection cn = connect.getconnection;   // <- pas new MySqlConnection(connect)
            {
                cn.Open();
                var da = new MySqlDataAdapter("SELECT id_direction, nomDirection FROM direction", cn);
                var dt = new DataTable();
                da.Fill(dt);

                ComboBoxService.DataSource = dt;
                ComboBoxService.DisplayMember = "nomDirection";  // ce que l’utilisateur voit
                ComboBoxService.ValueMember = "id_direction";           // la vraie clé en DB
            }
        }




        /***************************   ID CATEGORIE **********************************************/

        private void ChargerCategorie()
        {
            var connect = new Dbconnect();
            MySqlConnection cn = connect.getconnection;   // <- pas new MySqlConnection(connect)
            {
                cn.Open();
                var da = new MySqlDataAdapter("SELECT id_categorie, nomCategorie FROM categorie", cn);
                var dt = new DataTable();
                da.Fill(dt);

                ComboBoxService.DataSource = dt;
                ComboBoxService.DisplayMember = "nomCategorie";  // ce que l’utilisateur voit
                ComboBoxService.ValueMember = "id_categorie";           // la vraie clé en DB
            }
        }





        private void ChargerCategorieGestion()
        {
            var connect = new Dbconnect();
            MySqlConnection cn = connect.getconnection;   // <- pas new MySqlConnection(connect)
            {
                cn.Open();
                var da = new MySqlDataAdapter("SELECT id_categorie, nomCategorie FROM categorie", cn);
                var dt = new DataTable();
                da.Fill(dt);

                ComboBoxService.DataSource = dt;
                ComboBoxService.DisplayMember = "nomCategorie";  // ce que l’utilisateur voit
                ComboBoxService.ValueMember = "id_categorie";           // la vraie clé en DB
            }
        }






        /***************************   ID CATEGORIE **********************************************/

        private void ChargerEntreprise()
        {
            var connect = new Dbconnect();
            MySqlConnection cn = connect.getconnection;   // <- pas new MySqlConnection(connect)
            {
                cn.Open();
                var da = new MySqlDataAdapter("SELECT id_entreprise, nomEntreprise FROM entreprise", cn);
                var dt = new DataTable();
                da.Fill(dt);

                ComboBoxService.DataSource = dt;
                ComboBoxService.DisplayMember = "nomCategorie";  // ce que l’utilisateur voit
                ComboBoxService.ValueMember = "id_entreprise";           // la vraie clé en DB
            }
        }












        private void checkBoxCDI_CheckedChanged(object sender, EventArgs e) => ToggleContratAndDate();
        private void checkBoxCDD_CheckedChanged(object sender, EventArgs e) => ToggleContratAndDate();


        private void checkBoxCDIGestion_CheckedChanged(object sender, EventArgs e) => ToggleContratAndDate();
        private void checkBoxCDDGestion_CheckedChanged(object sender, EventArgs e) => ToggleContratAndDate();

        private void ToggleContratAndDate()
        {
            if (ComboBoxEntreprise.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez sélectionner une entreprise avant de choisir le type de contrat.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CheckBoxCDD.Checked = false;
                CheckBoxCDI.Checked = false;
                CheckBoxCDDGestion.Checked = false;
                CheckBoxCDIGestion.Checked = false;
                return;
            }
            else
            {
                // exclusivité des deux CheckBox
                if (CheckBoxCDD.Checked && CheckBoxCDI.Checked) CheckBoxCDI.Checked = false;
                if (CheckBoxCDI.Checked && CheckBoxCDD.Checked) CheckBoxCDD.Checked = false;
                if (CheckBoxCDDGestion.Checked && CheckBoxCDIGestion.Checked) CheckBoxCDIGestion.Checked = false;
                if (CheckBoxCDIGestion.Checked && CheckBoxCDDGestion.Checked) CheckBoxCDDGestion.Checked = false;

                bool cdd = CheckBoxCDDGestion.Checked;

                // activer/désactiver le picker
                dateSortiePicker.Enabled = cdd;
                dateSortiePicker.CustomFormat = cdd ? "dd/MM/yyyy" : " ";
                dateSortieGestionPicker.Enabled = cdd;
                dateSortieGestionPicker.CustomFormat = cdd ? "dd/MM/yyyy" : " ";

                // (facultatif) petit nudge d’affichage si le thème Guna2 ne se rafraîchit pas
                if (cdd && dateSortiePicker.CustomFormat == " ")
                    dateSortiePicker.CustomFormat = "dd/MM/yyyy";
                if (cdd && dateSortieGestionPicker.CustomFormat == " ")
                    dateSortieGestionPicker.CustomFormat = "dd/MM/yyyy";
            }

        }



        private void ChampsEnableds()
        {
            textBoxNomPrenom.Enabled = true;
            CheckBoxCDD.Enabled = true;
            CheckBoxCDI.Enabled = true;
            ComboBoxCivilite.Enabled = true;
            ComboBoxSexe.Enabled = true;
            dateNaissancePicker.Enabled = true;
            textBoxAdresse.Enabled = true;
            textBoxTelephone.Enabled = true;
            dateEntreePicker.Enabled = true;
            ComboBoxCategorie.Enabled = true;
            ComboBoxDirection.Enabled = true;
            ComboBoxService.Enabled = true;
            ComboBoxContrat.Enabled = true;
            ComboBoxtypecontrat.Enabled = true;
            //textBoxHeureContrat.Enabled = true;
            //textBoxJourContrat.Enabled = true;
            textBoxCnss.Enabled = true;
            ComboBoxModePayement.Enabled = true;
            //textBoxBanque.Enabled = true;
            // textBoxNumeroBancaire.Enabled = true;
            textBoxIdentification.Enabled = true;
            ComboBoxCadre.Enabled = true;
            textBoxSalaireMoyen.Enabled = true;
            textBoxPoste.Enabled = true;
        }

        private void ChampsDisableds()
        {
            textBoxNomPrenom.Enabled = false;
            CheckBoxCDD.Enabled = false;
            CheckBoxCDI.Enabled = false;
            ComboBoxCivilite.Enabled = false;
            ComboBoxSexe.Enabled = false;
            dateNaissancePicker.Enabled = false;
            textBoxAdresse.Enabled = false;
            textBoxTelephone.Enabled = false;
            dateEntreePicker.Enabled = false;
            ComboBoxCategorie.Enabled = false;
            ComboBoxDirection.Enabled = false;
            ComboBoxService.Enabled = false;
            ComboBoxContrat.Enabled = false;
            ComboBoxtypecontrat.Enabled = false;
            textBoxHeureContrat.Enabled = false;
            textBoxJourContrat.Enabled = false;
            textBoxCnss.Enabled = false;
            ComboBoxModePayement.Enabled = false;
            textBoxBanque.Enabled = false;
            textBoxNumeroBancaire.Enabled = false;
            textBoxIdentification.Enabled = false;
            ComboBoxCadre.Enabled = false;
            textBoxSalaireMoyen.Enabled = false;
            textBoxPoste.Enabled = false;

        }



        private void ChampsDisabledsGestion()
        {
            textBoxNomPrenomGestion.Enabled = false;
            CheckBoxCDDGestion.Enabled = false;
            CheckBoxCDIGestion.Enabled = false;
            ComboBoxCiviliteGestion.Enabled = false;
            ComboBoxSexeGestion.Enabled = false;
            dateNaissanceGestionPicker.Enabled = false;
            textBoxAdresseGestion.Enabled = false;
            textBoxTelephoneGestion.Enabled = false;
            dateEntreeGestionPicker.Enabled = false;
            dateSortieGestionPicker.Enabled = false; // si tu veux aussi bloquer
            ComboBoxCategorieGestion.Enabled = false;
            ComboBoxDirectionGestion.Enabled = false;
            ComboBoxServiceGestion.Enabled = false;
            ComboBoxContratGestion.Enabled = false;
            ComboBoxtypecontratGestion.Enabled = false;
            textBoxHeureContratGestion.Enabled = false;
            textBoxJourContratGestion.Enabled = false;
            textBoxCnssGestion.Enabled = false;
            ComboBoxModePayementGestion.Enabled = false;
            textBoxBanqueGestion.Enabled = false;
            textBoxNumeroBancaireGestion.Enabled = false;
            textBoxIdentificationGestion.Enabled = false;
            ComboBoxCadreGestion.Enabled = false;
            textBoxSalaireMoyenGestion.Enabled = false;
            textBoxPosteGestion.Enabled = false;
            ComboBoxEntrepriseGestion.Enabled = false;
        }




        private void ChampsEnabledsGestion()
        {
            textBoxNomPrenomGestion.Enabled = true;
            CheckBoxCDDGestion.Enabled = true;
            CheckBoxCDIGestion.Enabled = true;
            ComboBoxCiviliteGestion.Enabled = true;
            ComboBoxSexeGestion.Enabled = true;
            dateNaissanceGestionPicker.Enabled = true;
            textBoxAdresseGestion.Enabled = true;
            textBoxTelephoneGestion.Enabled = true;
            dateEntreeGestionPicker.Enabled = true;
            dateSortieGestionPicker.Enabled = true;
            ComboBoxCategorieGestion.Enabled = true;
            ComboBoxDirectionGestion.Enabled = true;
            ComboBoxServiceGestion.Enabled = true;
            ComboBoxContratGestion.Enabled = true;
            ComboBoxtypecontratGestion.Enabled = true;
            textBoxHeureContratGestion.Enabled = true;
            textBoxJourContratGestion.Enabled = true;
            textBoxCnssGestion.Enabled = true;
            ComboBoxModePayementGestion.Enabled = true;
            textBoxBanqueGestion.Enabled = true;
            textBoxNumeroBancaireGestion.Enabled = true;
            textBoxIdentificationGestion.Enabled = true;
            ComboBoxCadreGestion.Enabled = true;
            textBoxSalaireMoyenGestion.Enabled = true;
            textBoxPosteGestion.Enabled = true;
            ComboBoxEntrepriseGestion.Enabled = true;
        }





        private void ComboBoxEntreprise_SelectedIndexChanged(object sender, EventArgs e)
        {
            RechargerCombosEntreprise();
            if (ComboBoxEntreprise.SelectedIndex == 0)
            {
                ClearFormPersonnel();
                ChampsDisableds();
                buttonAjouter.Enabled = false;
            }
            else
            {
                ChampsEnableds();
                // ClearFormPersonnel();
                buttonAjouter.Enabled = true;
            }
        }

        // Recharge Catégorie + Direction pour l’entreprise sélectionnée
        private void RechargerCombosEntreprise(int? idCategorieASelectionner = null, int? idDirectionASelectionner = null, int? idServiceASelectionner = null)
        {
            int? idEnt = EntrepriseClass.GetIdEntrepriseSelectionnee(ComboBoxEntreprise);
            if (!idEnt.HasValue)
            {
                // vider les combos
                Categorie.ClearCombo(ComboBoxCategorie);

                ComboBoxDirection.DataSource = null;
                ComboBoxDirection.Items.Clear();
                ComboBoxDirection.SelectedIndex = -1;
                ComboBoxDirection.Text = string.Empty;

                ComboBoxService.DataSource = null;
                ComboBoxService.Items.Clear();
                ComboBoxService.SelectedIndex = -1;
                ComboBoxService.Text = string.Empty;
                return;
            }

            // Catégories
            Categorie.BindComboCategorie(
                ComboBoxCategorie,
                idEnt.Value,
                idCategorieASelectionner,
                placeholder: true);

            // Directions
            DirectionClass.BindComboDirection(
                ComboBoxDirection,
                idEnt.Value,
                idDirectionASelectionner,
                placeholder: true);

            // Services
            ServiceClass.BindComboService(
                ComboBoxService,
                idEnt.Value,
                idServiceASelectionner,
                placeholder: true);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }


        /* private async void buttonAjouter_Click(object sender, EventArgs e)
         {
             // 0) Récupérer l’entreprise depuis ton ComboBox
             var nomEntreprise = ComboBoxEntreprise?.Text?.Trim();
             if (string.IsNullOrWhiteSpace(nomEntreprise))
             {
                 MessageBox.Show("Sélectionnez une entreprise.", "Info",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                 return;
             }

             // 0) Connexion via ton wrapper
             var connect = new Dbconnect();
             MySqlConnection cn = connect.getconnection;

             if (cn.State != System.Data.ConnectionState.Open)
                 await cn.OpenAsync();

             // 1) Générer le matricule (format: XX001A)
             string matricule = await MatriculeGenerator.NextAsync(
                 cn, nomEntreprise, CancellationToken.None);


             // 2) Récupérer le type de contrat (CDD ou CDI)
             string dureeCD = GetTypeContratOrThrow();


             // 3) Récupérer la civilite
             string civilite = ComboBoxCivilite.SelectedItem?.ToString();


             // 4) Récupérer Nom Complet
             string nomPrenom = textBoxNomPrenom.Text.Trim();


             // 5) Récupérer le sexe 
             string sexe = ComboBoxSexe.SelectedItem?.ToString();


             // 6) Récupérer date de naissance
             DateTime dateNaissance = dateNaissancePicker.Value.Date;


             // 8) Récupérer adresse
             string adresse = textBoxAdresse.Text.Trim();


             // 9) Récupérer numero de telephone
             string telephone = textBoxTelephone.Text.Trim();


             // 10) Récupérer date d'entree
             DateTime dateEntree = dateEntreePicker.Value.Date;


             // 11) Récupérer date de sortie
             DateTime dateSortie = dateSortiePicker.Value.Date;


             // 12) Récupérer id_direction depuis ComboBox
             int directionId = Convert.ToInt32(ComboBoxDirection.SelectedValue);


             // 13) Récupérer id_service depuis ton ComboBox
             int serviceId = Convert.ToInt32(ComboBoxService.SelectedValue);


             // 14) Récupérer contrat
             string contrat = ComboBoxContrat.SelectedItem?.ToString();


             // 15) Récupérer typecontrat
             string typecontrat = ComboBoxtypecontrat.SelectedItem?.ToString();


             // 16) Récupérer Heure contrat
             int heurecontrat = Convert.ToInt32(textBoxHeureContrat.Text.Trim());


             // 17) Récupérer Jour contrat
             int jourcontrat = Convert.ToInt32(textBoxJourContrat.Text.Trim());


             // 18) Récupérer id_categorie depuis ton ComboBox
             int categorieId = Convert.ToInt32(ComboBoxCategorie.SelectedValue);


             // 19) Récupérer numéro_cnss
             string numero_cnss = textBoxCnss.Text.Trim();


             // 20) Récupérer mode_paiement
             string mode_paiement = ComboBoxModePayement.SelectedItem?.ToString();


             // 21) Récupérer banque
             string banque = textBoxBanque.Text.Trim();


             // 22) Récupérer numero_bancaire
             string numero_bancaire = textBoxNumeroBancaire.Text.Trim();


             // 23) Récupérer identification
             string identification = textBoxIdentification.Text.Trim();


             // 24) Récupérer cadre
             string cadre = ComboBoxCadre.SelectedItem?.ToString();


             // 25) Récupérer salaire_moyen
             decimal salaire_moyen = Convert.ToDecimal(textBoxSalaireMoyen.Text.Trim());


             // 26) Récupérer id_entreprise depuis ton ComboBox
             int entrepriseId = Convert.ToInt32(ComboBoxEntreprise.SelectedValue);


         }

         */


        /* private async void buttonAjouter_Click(object sender, EventArgs e)
         {
             try
             {
                 Cursor.Current = Cursors.WaitCursor;

                 // 0) Entreprise visible (utile pour générer le matricule)
                 var nomEntreprise = ComboBoxEntreprise?.Text?.Trim();
                 if (string.IsNullOrWhiteSpace(nomEntreprise))
                 {
                     MessageBox.Show("Sélectionnez une entreprise.", "Info",
                         MessageBoxButtons.OK, MessageBoxIcon.Information);
                     return;
                 }

                 // 0) Connexion
                 var connect = new Dbconnect();
                 using (MySqlConnection cn = connect.getconnection)
                 {
                     if (cn.State != ConnectionState.Open)
                         await cn.OpenAsync();

                     // 1) Matricule auto
                     string matricule = await MatriculeGenerator.NextAsync(
                         cn, nomEntreprise, CancellationToken.None);

                     // 2) Nom complet
                     string nomPrenom = textBoxNomPrenom?.Text?.Trim();
                     if (string.IsNullOrWhiteSpace(nomPrenom))
                     {
                         MessageBox.Show("Saisissez le nom complet.", "Info",
                             MessageBoxButtons.OK, MessageBoxIcon.Information);
                         return;
                     }

                     // 3) IDs sélectionnés
                     int idDirection = GetSelectedIdOrThrow(ComboBoxDirection, "direction");
                     int idService = GetSelectedIdOrThrow(ComboBoxService, "service");
                     int idCategorie = GetSelectedIdOrThrow(ComboBoxCategorie, "catégorie");
                     int idEntreprise = GetSelectedIdOrThrow(ComboBoxEntreprise, "entreprise");


                     // Assure-toi que dateSortiePicker.ShowCheckBox = true (dans le designer)
                     DateTime dateNaissance = dateNaissancePicker.Value.Date;
                     DateTime dateEntree = dateEntreePicker.Value.Date;
                     DateTime? dateSortie = dateSortiePicker.Checked ? dateSortiePicker.Value.Date : (DateTime?)null;


                     // 4) Enregistrement minimal (table 'personnel' selon ta méthode)
                     EmployeClass.EnregistrerEmployeMinimal(
                         matricule,
                         nomPrenom,
                         idDirection,
                         idService,
                         idCategorie,
                         idEntreprise,
                         dateNaissance,
                         dateEntree,
                         dateSortie
                     );

                     // 5) Préparer un nouvel enregistrement : on vide tout
                     ClearFormMinimal();


                 }
             }
             catch (ArgumentException vex)
             {
                 MessageBox.Show(vex.Message, "Information",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
             catch (MySqlException sqlEx) when (sqlEx.Number == 1062) // contrainte UNIQUE
             {
                 MessageBox.Show("Doublon: ce matricule existe déjà pour cette entreprise.",
                     "Conflit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Erreur : " + ex.Message, "Erreur",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
             finally
             {
                 Cursor.Current = Cursors.Default;
             }
         }

         */

        private string GetModePaiement()
        {
            if (ComboBoxModePayement.SelectedValue is string sv && !string.IsNullOrWhiteSpace(sv))
                return sv.Trim();

            if (!string.IsNullOrWhiteSpace(ComboBoxModePayement.Text))
                return ComboBoxModePayement.Text.Trim();

            if (ComboBoxModePayement.SelectedItem is System.Data.DataRowView drv)
            {
                var vm = ComboBoxModePayement.ValueMember;
                if (!string.IsNullOrEmpty(vm) && drv.Row.Table.Columns.Contains(vm))
                    return Convert.ToString(drv[vm])?.Trim();
                var dm = ComboBoxModePayement.DisplayMember;
                if (!string.IsNullOrEmpty(dm) && drv.Row.Table.Columns.Contains(dm))
                    return Convert.ToString(drv[dm])?.Trim();
            }
            return ComboBoxModePayement.SelectedItem?.ToString()?.Trim();
        }



        private string GetTypeContrat()
        {
            if (ComboBoxtypecontrat.SelectedValue is string sv && !string.IsNullOrWhiteSpace(sv))
                return sv.Trim();

            if (!string.IsNullOrWhiteSpace(ComboBoxtypecontrat.Text))
                return ComboBoxtypecontrat.Text.Trim();

            if (ComboBoxtypecontrat.SelectedItem is System.Data.DataRowView drv)
            {
                var vm = ComboBoxtypecontrat.ValueMember;
                if (!string.IsNullOrEmpty(vm) && drv.Row.Table.Columns.Contains(vm))
                    return Convert.ToString(drv[vm])?.Trim();
                var dm = ComboBoxtypecontrat.DisplayMember;
                if (!string.IsNullOrEmpty(dm) && drv.Row.Table.Columns.Contains(dm))
                    return Convert.ToString(drv[dm])?.Trim();
            }
            return ComboBoxtypecontrat.SelectedItem?.ToString()?.Trim();
        }




        private bool _updatingModeFields;

        private void ApplyModePaiementUI()
        {
            if (_updatingModeFields) return;
            _updatingModeFields = true;
            try
            {
                var mode = GetModePaiement(); // ta méthode ou ComboBoxModePayement.Text.Trim()
                bool isVirement = string.Equals(mode, "Virement", StringComparison.OrdinalIgnoreCase)
                               || string.Equals(mode, "Versement", StringComparison.OrdinalIgnoreCase);

                // on retient l'ancien état (était en mode virement ?)
                bool wasVirement = textBoxBanque.Enabled;

                // ⚠️ Vider AVANT de désactiver si on QUITTE le mode virement
                if (!isVirement && wasVirement)
                {
                    textBoxBanque.Text = string.Empty;
                    textBoxNumeroBancaire.Text = string.Empty;

                    // si data-binding, pousse la valeur vidée dans la source
                    textBoxBanque.DataBindings["Text"]?.WriteValue();
                    textBoxNumeroBancaire.DataBindings["Text"]?.WriteValue();
                }

                // Activer/désactiver après
                textBoxBanque.Enabled = isVirement;
                textBoxNumeroBancaire.Enabled = isVirement;
            }
            finally
            {
                _updatingModeFields = false;
            }
        }



        private bool _updatingTypeFields;

        private void ApplyTypeContratUI()
        {
            if (_updatingTypeFields) return;
            _updatingTypeFields = true;
            try
            {
                var mode = GetTypeContrat(); // ta méthode ou ComboBoxModePayement.Text.Trim()
                bool isHoraire = string.Equals(mode, "Horaire", StringComparison.OrdinalIgnoreCase);
                bool isJournalier = string.Equals(mode, "Journalier", StringComparison.OrdinalIgnoreCase);


                // on retient l'ancien état (était en mode virement ?)
                bool wasHoraire = textBoxHeureContrat.Enabled;
                bool wasJour = textBoxJourContrat.Enabled;


                // ⚠️ Vider AVANT de désactiver si on QUITTE le mode virement
                if (!isHoraire && wasHoraire)
                {
                    textBoxHeureContrat.Text = string.Empty;

                    // si data-binding, pousse la valeur vidée dans la source
                    textBoxHeureContrat.DataBindings["Text"]?.WriteValue();
                }
                else if (!isJournalier && wasJour)
                {
                    textBoxJourContrat.Text = string.Empty;

                    // si data-binding, pousse la valeur vidée dans la source
                    textBoxJourContrat.DataBindings["Text"]?.WriteValue();
                }

                // Activer/désactiver après
                textBoxHeureContrat.Enabled = isHoraire;
                textBoxJourContrat.Enabled = isJournalier;

            }
            finally
            {
                _updatingTypeFields = false;
            }
        }



        private bool TryGetSelectedId(ComboBox combo, out int id, bool placeholderAtIndex0 = true)
        {
            id = 0;

            // Combo manquante ou rien de sélectionné
            if (combo == null) return false;
            if (combo.SelectedIndex < (placeholderAtIndex0 ? 1 : 0)) return false;

            // Récupère la "vraie" valeur
            object val = combo.SelectedValue;

            // Cas DataRowView (binding)
            if (val is DataRowView drv)
            {
                var vm = combo.ValueMember;
                if (!string.IsNullOrEmpty(vm) && drv.Row.Table.Columns.Contains(vm))
                    val = drv[vm];
                else
                    return false;
            }

            // Null/DBNull => KO
            if (val == null || val == DBNull.Value) return false;

            // Déjà un int
            if (val is int v) { id = v; return true; }

            // Autres numériques courants (Int64, Int16, etc.)
            if (val is long l && l >= int.MinValue && l <= int.MaxValue) { id = (int)l; return true; }
            if (val is short s) { id = s; return true; }
            if (val is byte b) { id = b; return true; }

            // Chaîne -> parse
            if (val is string sv && int.TryParse(sv.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var p))
            { id = p; return true; }

            // Dernier recours : ToString() + parse culture courante
            var txt = val.ToString();
            return int.TryParse(txt, NumberStyles.Integer, CultureInfo.CurrentCulture, out id);
        }





        private async void buttonAjouter_Click(object sender, EventArgs e)
        {
            try
            {

                Cursor.Current = Cursors.WaitCursor;

                var nomEntreprise = ComboBoxEntreprise?.Text?.Trim();
                if (string.IsNullOrWhiteSpace(nomEntreprise))
                {
                    MessageBox.Show("Sélectionnez une entreprise.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var connect = new Dbconnect();
                using (MySqlConnection cn = connect.getconnection)
                {
                    if (cn.State != ConnectionState.Open)
                        await cn.OpenAsync();

                    // 1) Matricule auto
                    string matricule = await MatriculeGenerator.NextAsync(
                        cn, nomEntreprise, CancellationToken.None);

                    // 2) Nom complet
                    string nomPrenom = textBoxNomPrenom?.Text?.Trim();
                    /*                    if (string.IsNullOrWhiteSpace(nomPrenom))
                                        {
                                            MessageBox.Show("Saisissez le nom complet.", "Info",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                        */
                    // 3) IDs sélectionnés

                    // usage :
                    var errors = new List<string>();
                    if (!TryGetSelectedId(ComboBoxDirection, out int idDirection)) errors.Add("Sélectionnez une direction.");
                    if (!TryGetSelectedId(ComboBoxService, out int idService)) errors.Add("Sélectionnez un service.");
                    if (!TryGetSelectedId(ComboBoxCategorie, out int idCategorie)) errors.Add("Sélectionnez une catégorie.");
                    if (!TryGetSelectedId(ComboBoxEntreprise, out int idEntreprise)) errors.Add("Sélectionnez une entreprise.");



                    //if (!TryGetSelectedId(ComboBoxDirection, out var idDirection)) errors.Add("Sélectionner une direction.");
                    //if (!TryGetSelectedId(ComboBoxService, out var idService)) errors.Add("Sélectionner un service.");
                    //if (!TryGetSelectedId(ComboBoxDirection, out var idCategorie)) errors.Add("Sélectionner une direction.");
                    //int idDirection = GetSelectedIdOrThrow(ComboBoxDirection, "direction");
                    // int idService = GetSelectedIdOrThrow(ComboBoxService, "service");
                    //int idCategorie = GetSelectedIdOrThrow(ComboBoxCategorie, "catégorie");
                    //int idEntreprise = GetSelectedIdOrThrow(ComboBoxEntreprise, "entreprise");

                    // 4) Dates
                    DateTime dateNaissance = dateNaissancePicker.Value.Date;
                    DateTime dateEntree = dateEntreePicker.Value.Date;

                    // Déterminer CDI/CDD depuis les CheckBox
                    string dureeContrat = CheckBoxCDI.Checked ? "CDI"
                                        : CheckBoxCDD.Checked ? "CDD"
                                        : null;
                    /*
                                        if (string.IsNullOrEmpty(dureeContrat))
                                        {
                                            MessageBox.Show("Choisissez le type de contrat (CDI ou CDD).", "Info",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                    */
                    // Gérer dateSortie selon la règle
                    DateTime? dateSortie = null;
                    /*
                                        if (dureeContrat == "CDD")
                                        {
                                            if (!dateSortiePicker.Checked)
                                            {
                                                MessageBox.Show("Pour un CDD, la date de sortie est obligatoire.", "Info",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }

                                            dateSortie = dateSortiePicker.Value.Date;

                                            if (dateSortie <= dateEntree)
                                            {
                                                MessageBox.Show("La date de sortie doit être postérieure à la date d'entrée.", "Info",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                        }
                                        else // CDI
                                        {
                                            dateSortie = null; // restera NULL en base
                                        }
                    */

                    //Civilite et Sexe
                    string civilite = ComboBoxCivilite.SelectedItem?.ToString();
                    string sexe = ComboBoxSexe.SelectedItem?.ToString();


                    //Adresse et Telephone
                    string adresse = textBoxAdresse.Text.Trim();
                    string telephone = textBoxTelephone.Text.Trim();


                    //Indentification ,poste et contrat
                    string identification = textBoxIdentification.Text.Trim();
                    string poste = textBoxPoste.Text.Trim();
                    string contrat = ComboBoxContrat.SelectedItem?.ToString();


                    //Cadre et typecontrat
                    string cadre = ComboBoxCadre.SelectedItem?.ToString();
                    string typecontrat = ComboBoxtypecontrat.SelectedItem?.ToString();


                    //Heure Contrat et Jour Contrat et modePayement ,banque ,numeroBancaire
                    int heureContrat = ToIntOrNull(textBoxHeureContrat.Text) ?? 0;
                    int jourContrat = ToIntOrNull(textBoxJourContrat.Text) ?? 0;
                    string modePaiement = ComboBoxModePayement.SelectedItem?.ToString();
                    string banque = textBoxBanque.Text.Trim();
                    string numeroBancaire = textBoxNumeroBancaire.Text.Trim();


                    // Salaire Moyen et Numero CNSS
                    decimal salaireMoyen = ToDecimalFlexible(textBoxSalaireMoyen.Text) ?? 0m;

                    string numeroCnss = textBoxCnss.Text.Trim();


                    // 5) Insert minimal (dans 'personnel')


                    // =====================
                    // VALIDATIONS AVANT ENREGISTREMENT (avec errors)
                    // =====================



                    // 0) Entreprise
                    //int idEntreprise = 0;
                    // string nomEntreprise = null;

                    if (ComboBoxEntreprise.SelectedIndex < 0 || ComboBoxEntreprise.SelectedValue == null)
                    {
                        errors.Add("Sélectionner une entreprise.");

                    }
                    else
                    {
                        idEntreprise = Convert.ToInt32(ComboBoxEntreprise.SelectedValue);
                        nomEntreprise = ComboBoxEntreprise.Text?.Trim();
                    }


                    // 1) Type de contrat
                    if (string.IsNullOrEmpty(dureeContrat))
                        errors.Add("Choisisser le type de contrat (CDI ou CDD).");

                    // 2) Nom complet
                    //string nomPrenom = textBoxNomPrenom?.Text?.Trim();
                    if (string.IsNullOrWhiteSpace(nomPrenom))
                        errors.Add("Saisisser le nom complet.");

                    // 3) Date de sortie selon la règle
                    // DateTime? dateSortie = null;

                    if (dureeContrat == "CDD")
                    {
                        if (!dateSortiePicker.Checked)
                        {
                            errors.Add("Pour un CDD, la date de sortie est obligatoire.");
                        }
                        else
                        {
                            dateSortie = dateSortiePicker.Value.Date;

                            if (dateSortie <= dateEntree)
                                errors.Add("La date de sortie doit être postérieure à la date d'entrée.");
                        }
                    }
                    else // CDI
                    {
                        dateSortie = null; // restera NULL en base
                    }







                    // 4) Paiement 'Versement' => banque & numéro obligatoires (optionnel mais utile)
                    var mode = ComboBoxModePayement.SelectedItem?.ToString()?.Trim();
                    if (string.Equals(mode, "Virement", StringComparison.OrdinalIgnoreCase))

                    {
                        textBoxBanque.Enabled = textBoxNumeroBancaire.Enabled = true;
                        if (string.IsNullOrWhiteSpace(textBoxBanque.Text))
                            errors.Add("Pour le paiement par virement, la Banque est obligatoire.");
                        if (string.IsNullOrWhiteSpace(textBoxNumeroBancaire.Text))
                            errors.Add("Pour le paiement par virement, le numéro bancaire est obligatoire.");
                    }
                    else if (string.Equals(mode, "***", StringComparison.OrdinalIgnoreCase))
                    {
                        errors.Add("Veuillez sélectionner un mode de paiement.");
                    }








                    // Si erreurs, on affiche tout d'un coup et on stoppe
                    if (errors.Count > 0)
                    {
                        MessageBox.Show("Veuillez corriger :\n• " + string.Join("\n• ", errors),
                                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // =====================
                    // ENREGISTREMENT
                    // =====================
                    EmployeClass.EnregistrerEmploye(
                        matricule,
                        nomPrenom,
                        civilite,
                        sexe,
                        dateNaissance,
                        adresse,
                        telephone,
                        identification,
                        poste,
                        contrat,
                        cadre,
                        dateEntree,
                        dateSortie,
                        typecontrat,
                        heureContrat,
                        jourContrat,
                        modePaiement,
                        banque,
                        numeroBancaire,
                        idDirection,
                        idService,
                        idCategorie,
                        idEntreprise,
                        dureeContrat,
                        numeroCnss,
                        salaireMoyen
                    );


                    // 6) UPDATE: dureeCD + date_sortie (au cas où tu veux forcer la règle métier)
                    // UpdateContratPersonnel(matricule, idEntreprise, dureeContrat, dateSortie);

                    // 7) Nettoyage
                    ClearFormPersonnel();
                    ComboBoxEntreprise.SelectedIndex = 0;
                    ComboBoxEntreprise.Focus();
                }
            }
            catch (ArgumentException vex)
            {
                MessageBox.Show(vex.Message, "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException sqlEx) when (sqlEx.Number == 1062)
            {
                MessageBox.Show("Doublon: ce matricule existe déjà pour cette entreprise.",
                    "Conflit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }





        /// 
        /// Récupère l'ID sélectionné d'une ComboBox (ValueMember = ID).
        /// </summary>
        private int GetSelectedIdOrThrow(ComboBox combo, string label)
        {
            if (combo == null)
                throw new ArgumentException($"La liste {label} n'est pas initialisée.");

            if (combo.SelectedValue == null || combo.SelectedIndex < 0)
                throw new ArgumentException($"Veuillez sélectionner une {label}.");

            try
            {
                if (combo.SelectedValue is int id) return id;
                return Convert.ToInt32(combo.SelectedValue);
            }
            catch
            {
                throw new ArgumentException(
                    $"La {label} sélectionnée n'a pas d'identifiant valide. " +
                    $"Vérifiez ValueMember/DisplayMember de la ComboBox.");
            }
        }










        /// <summary>
        /// Vide uniquement les champs utilisés par l'enregistrement minimal.
        /// </summary>
        private void ClearFormPersonnel()
        {
            if (textBoxNomPrenom != null) textBoxNomPrenom.Clear();
            if (textBoxTelephone != null) textBoxTelephone.Clear();
            if (textBoxAdresse != null) textBoxAdresse.Clear();
            if (textBoxPoste != null) textBoxPoste.Clear();
            if (textBoxIdentification != null) textBoxIdentification.Clear();
            if (textBoxHeureContrat != null) textBoxHeureContrat.Clear();
            if (textBoxJourContrat != null) textBoxJourContrat.Clear();
            if (textBoxBanque != null) textBoxBanque.Clear();
            if (textBoxNumeroBancaire != null) textBoxNumeroBancaire.Clear();
            if (textBoxSalaireMoyen != null) textBoxSalaireMoyen.Clear();
            if (textBoxCnss != null) textBoxCnss.Clear();
            if (CheckBoxCDD.Checked != false) CheckBoxCDD.Checked = false;
            if (CheckBoxCDI.Checked != false) CheckBoxCDI.Checked = false;

            if (ComboBoxDirection != null) ComboBoxDirection.SelectedIndex = -1;
            if (ComboBoxService != null) ComboBoxService.SelectedIndex = -1;
            if (ComboBoxCategorie != null) ComboBoxCategorie.SelectedIndex = -1;
            if (ComboBoxCivilite != null) ComboBoxCivilite.SelectedIndex = -1;
            if (ComboBoxCadre != null) ComboBoxCadre.SelectedIndex = -1;
            if (ComboBoxSexe != null) ComboBoxSexe.SelectedIndex = -1;
            if (ComboBoxContrat != null) ComboBoxContrat.SelectedIndex = -1;
            if (ComboBoxtypecontrat != null) ComboBoxtypecontrat.SelectedIndex = -1;
            if (ComboBoxModePayement != null) ComboBoxModePayement.SelectedIndex = -1;


        }




        private void ClearFormPersonnelGestion()
        {
            // TextBox
            textBoxID.Clear();
            textBoxMatricule.Clear();
            textBoxNomPrenomGestion.Clear();
            textBoxTelephoneGestion.Clear();
            textBoxAdresseGestion.Clear();
            textBoxPosteGestion.Clear();
            textBoxIdentificationGestion.Clear();
            textBoxHeureContratGestion.Clear();
            textBoxJourContratGestion.Clear();
            textBoxBanqueGestion.Clear();
            textBoxNumeroBancaireGestion.Clear();
            textBoxSalaireMoyenGestion.Clear();
            textBoxCnssGestion.Clear();

            // CheckBox
            CheckBoxCDDGestion.Checked = false;
            CheckBoxCDIGestion.Checked = false;

            // ComboBox
            ComboBoxDirectionGestion.SelectedIndex = -1;
            ComboBoxEntrepriseGestion.SelectedIndex = -1;
            ComboBoxServiceGestion.SelectedIndex = -1;
            ComboBoxCategorieGestion.SelectedIndex = -1;
            ComboBoxCiviliteGestion.SelectedIndex = -1;
            ComboBoxCadreGestion.SelectedIndex = -1;
            ComboBoxSexeGestion.SelectedIndex = -1;
            ComboBoxContratGestion.SelectedIndex = -1;
            ComboBoxtypecontratGestion.SelectedIndex = -1;
            ComboBoxModePayementGestion.SelectedIndex = -1;

            // DateTimePicker (remis à aujourd'hui par défaut)
            dateNaissanceGestionPicker.Value = DateTime.Today;
            dateEntreeGestionPicker.Value = DateTime.Today;
            dateSortieGestionPicker.Value = DateTime.Today;
        }




        private void guna2RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckBoxCDD_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxCDD.Checked) CheckBoxCDI.Checked = false;
        }

        private void CheckBoxCDI_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxCDI.Checked) CheckBoxCDD.Checked = false;
        }

        private void buttonEffacer_Click(object sender, EventArgs e)
        {

        }

        private void buttonRechercher_Click(object sender, EventArgs e)
        {
            DataGridView_Personnel_Gestion.DataSource = EmployeClass.RecherchePersonnel(textBoxSearch.Text);
        }


        // --- utilitaires locaux (en haut du fichier si tu veux les réutiliser) ---
        private static int GetOrdinalSafe(IDataRecord r, params string[] names)
        {
            for (int i = 0; i < r.FieldCount; i++)
            {
                var col = r.GetName(i);
                foreach (var n in names)
                    if (string.Equals(col, n, StringComparison.OrdinalIgnoreCase))
                        return i;
            }
            return -1;
        }

        private static string NormalizeDureeContrat(object val)
        {
            if (val == null) return "";
            var s = val.ToString().Trim();

            // numérique / booléen
            if (int.TryParse(s, out var n))
                return n == 1 ? "CDD" : "CDI";
            if (bool.TryParse(s, out var b))
                return b ? "CDD" : "CDI";

            // texte
            s = s.ToUpperInvariant();
            if (s == "CDD" || s == "CDI") return s;

            // valeurs exotiques -> par défaut CDI
            return "CDI";
        }




        private void ChargerDetailsEmployeParId(string id)
        {
            try
            {
                const string sql = @"
SELECT
    p.id_personnel,
    p.matricule,
    p.dureeContrat,
    p.civilite,
    p.nomPrenom,
    p.sexe,
    p.date_naissance,
    p.adresse,
    p.telephone,
    p.date_entree,
    p.date_sortie,
    p.id_direction,
    p.id_service,
    p.contrat,
    p.typeContrat,
    p.heureContrat,
    p.jourContrat,
    p.id_categorie,
    p.numerocnss,
    p.modePayement,
    p.banque,
    p.numeroBancaire,
    p.identification,
    p.cadre,                -- TINYINT(1) attendu (0/1) ou VARCHAR selon ta table
    p.poste,                -- TINYINT(1) attendu (0/1) ou VARCHAR selon ta table
    p.salairemoyen,
    p.id_entreprise,

    -- Libellés joints (facultatif pour affichage/diagnostic)
    e.nomEntreprise,
    d.nomDirection,
    s.nomService,
    c.nomCategorie
FROM personnel p
LEFT JOIN entreprise e ON e.id_entreprise = p.id_entreprise
LEFT JOIN direction  d ON d.id_direction  = p.id_direction
LEFT JOIN service    s ON s.id_service    = p.id_service
LEFT JOIN categorie  c ON c.id_categorie  = p.id_categorie
WHERE p.id_personnel = @id
LIMIT 1;";

                // Comme pour ta méthode categorie : on repart d'une nouvelle connexion
                var connect = new Dbconnect();
                using (var con = new MySqlConnection(connect.getconnection.ConnectionString))
                using (var cmd = new MySqlCommand(sql, con))
                {
                    if (!int.TryParse(id, out int idEmp))
                    {
                        MessageBox.Show("Identifiant de l'employé invalide.");
                        return;
                    }

                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = idEmp;

                    con.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (!reader.Read())
                        {
                            // Rien trouvé → nettoyage UI minimal
                            textBoxNomPrenomGestion.Text = "";
                            textBoxAdresseGestion.Text = "";
                            textBoxTelephoneGestion.Text = "";
                            textBoxCnssGestion.Text = "";
                            textBoxBanqueGestion.Text = "";
                            textBoxNumeroBancaireGestion.Text = "";
                            textBoxIdentificationGestion.Text = "";
                            textBoxSalaireMoyenGestion.Text = "";
                            ComboBoxCiviliteGestion.SelectedIndex = -1;
                            ComboBoxSexeGestion.SelectedIndex = -1;
                            ComboBoxContratGestion.SelectedIndex = -1;
                            ComboBoxtypecontratGestion.SelectedIndex = -1;
                            ComboBoxCadreGestion.SelectedIndex = -1;
                            if (ComboBoxEntrepriseGestion.DataSource != null) ComboBoxEntreprise.SelectedIndex = -1;
                            if (ComboBoxDirectionGestion.DataSource != null) ComboBoxDirection.SelectedIndex = -1;
                            if (ComboBoxServiceGestion.DataSource != null) ComboBoxService.SelectedIndex = -1;
                            if (ComboBoxCategorieGestion.DataSource != null) ComboBoxCategorie.SelectedIndex = -1;
                            return;
                        }

                        // ----- Remplissage des TextBox -----
                        ChampsEnabledsGestion();
                        textBoxNomPrenomGestion.Text = reader["nomPrenom"]?.ToString();
                        textBoxAdresseGestion.Text = reader["adresse"]?.ToString();
                        textBoxTelephoneGestion.Text = reader["telephone"]?.ToString();
                        textBoxCnssGestion.Text = reader["numerocnss"]?.ToString();
                        textBoxBanqueGestion.Text = reader["banque"]?.ToString();
                        textBoxNumeroBancaireGestion.Text = reader["numeroBancaire"]?.ToString();
                        textBoxIdentificationGestion.Text = reader["identification"]?.ToString();
                        textBoxSalaireMoyenGestion.Text = reader["salairemoyen"]?.ToString();
                        textBoxPosteGestion.Text = reader["poste"]?.ToString();

                        // Matricule affichable si tu as un label/readonly
                        if (textBoxMatricule != null)
                            textBoxMatricule.Text = reader["matricule"]?.ToString();

                        // ----- Combos simples (valeur texte) -----
                        // Civilité / Sexe / Contrat / Type Contrat
                        ComboBoxCiviliteGestion.SelectedItem = reader["civilite"]?.ToString();
                        ComboBoxSexeGestion.SelectedItem = reader["sexe"]?.ToString();
                        ComboBoxContratGestion.SelectedItem = reader["contrat"]?.ToString();
                        ComboBoxtypecontratGestion.SelectedItem = reader["typeContrat"]?.ToString();
                        ComboBoxModePayementGestion.SelectedItem = reader["modePayement"]?.ToString();

                        // Cadre : si ta colonne est TINYINT(1) 0/1 → mappe vers "Oui"/"Non"
                        if (!reader.IsDBNull(reader.GetOrdinal("cadre")))
                        {
                            var cadreVal = reader["cadre"];
                            string cadreText = cadreVal is sbyte || cadreVal is byte || cadreVal is int
                                ? ((Convert.ToInt32(cadreVal) == 1) ? "Oui" : "Non")
                                : cadreVal.ToString();

                            ComboBoxCadreGestion.SelectedItem = cadreText; // "Oui"/"Non"
                        }
                        else
                        {
                            ComboBoxCadreGestion.SelectedIndex = -1;
                        }

                        // ----- Dates -----
                        if (!reader.IsDBNull(reader.GetOrdinal("date_naissance")))
                            dateNaissanceGestionPicker.Value = Convert.ToDateTime(reader["date_naissance"]);
                        if (!reader.IsDBNull(reader.GetOrdinal("date_entree")))
                            dateEntreeGestionPicker.Value = Convert.ToDateTime(reader["date_entree"]);

                        // Date sortie : nullable
                        if (!reader.IsDBNull(reader.GetOrdinal("date_sortie")))
                        {
                            var ds = Convert.ToDateTime(reader["date_sortie"]);
                            dateSortieGestionPicker.Value = ds;
                            // Si tu utilises un CheckBox pour activer la date de sortie :
                            //if (checkSortie != null) checkSortie.Checked = true;
                        }
                        else
                        {
                            // pas de sortie
                            //  if (checkSortie != null) checkSortie.Checked = false;
                        }


                        // ----- Durée du contrat (CDD/CDI) -----
                        string dureeDb;
                        try
                        {
                            // nom de colonne le plus courant chez toi : "dureeCD"
                            dureeDb = ReadStringOrEmpty(reader, "dureeCD");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            // si ta colonne s'appelle autrement (ex: "dureeContrat"), on retente
                            dureeDb = ReadStringOrEmpty(reader, "dureeContrat");
                        }

                        string dureeNorm = NormalizeContrat(dureeDb);

                        // Si tu utilises des CheckBox (ou RadioButton) nommés :
                        CheckBoxCDDGestion.Checked = (dureeNorm == "CDD");
                        CheckBoxCDIGestion.Checked = (dureeNorm == "CDI");

                        // (optionnel mais recommandé) assurer l’exclusivité si ce sont des CheckBox
                        if (CheckBoxCDDGestion.Checked) CheckBoxCDIGestion.Checked = false;
                        if (CheckBoxCDIGestion.Checked) CheckBoxCDDGestion.Checked = false;




                        // ----- Combos bindés (SelectedValue) -----
                        // Pour que SelectedValue fonctionne, les Combo doivent être déjà DataBindés (ValueMember/DisplayMember)
                        int idEnt = Convert.ToInt32(reader["id_entreprise"]);
                        if (!reader.IsDBNull(reader.GetOrdinal("id_entreprise")))
                        {
                           
                            if (ComboBoxEntrepriseGestion.DataSource == null)
                                EntrepriseClass.ChargerEntreprises(ComboBoxEntrepriseGestion, idEnt, true);
                            else
                                ComboBoxEntrepriseGestion.SelectedValue = idEnt;
                        }
                        else if (ComboBoxEntrepriseGestion.DataSource != null)
                            ComboBoxEntrepriseGestion.SelectedIndex = -1;



                        // ----- Direction -----
                        if (!reader.IsDBNull(reader.GetOrdinal("id_direction")))
                        {
                            int idDir = Convert.ToInt32(reader["id_direction"]);

                            // 1er chargement : DataSource == null → on charge filtré et on tente de pré-sélectionner
                            if (ComboBoxDirectionGestion.DataSource == null)
                                DirectionClass.ChargerDirections(ComboBoxDirectionGestion, idEnt, idDir, true);
                            else
                            {
                                // Si déjà bindé, on rebinde (liste filtrée par l’entreprise courante)
                                DirectionClass.ChargerDirections(ComboBoxDirectionGestion, idEnt, idDir, true);
                            }
                        }
                        else
                        {
                            // Pas de direction => recharger la liste filtrée et mettre le placeholder
                            DirectionClass.ChargerDirections(ComboBoxDirectionGestion, idEnt, null, true);
                        }





                        if (!reader.IsDBNull(reader.GetOrdinal("id_service")))
                        {
                            int idSrv = Convert.ToInt32(reader["id_service"]);

                            // 1er chargement : DataSource == null → on charge filtré et on tente de pré-sélectionner
                            if (ComboBoxServiceGestion.DataSource == null)
                                ServiceClass.ChargerServices(ComboBoxServiceGestion, idEnt, idSrv, true);
                            else
                            {
                                // Si déjà bindé, on rebinde (liste filtrée par entreprise courante)
                                ServiceClass.ChargerServices(ComboBoxServiceGestion, idEnt, idSrv, true);
                            }
                        }
                        else
                        {
                            // Pas de service => recharger la liste filtrée et mettre le placeholder
                            ServiceClass.ChargerServices(ComboBoxServiceGestion, idEnt, null, true);
                        }



                        // ----- Catégorie -----
                        if (!reader.IsDBNull(reader.GetOrdinal("id_categorie")))
                        {
                            int idCat = Convert.ToInt32(reader["id_categorie"]);

                            if (ComboBoxCategorieGestion.DataSource == null)
                                Categorie.ChargerCategories(ComboBoxCategorieGestion, idEnt, idCat, true);
                            else
                            {
                                Categorie.ChargerCategories(ComboBoxCategorieGestion, idEnt, idCat, true);
                            }
                        }
                        else
                        {
                            Categorie.ChargerCategories(ComboBoxCategorieGestion, idEnt, null, true);
                        }




                        // ----- Heures / Jours contrat (XOR) -----
                        int heure = reader.IsDBNull(reader.GetOrdinal("heureContrat"))
                            ? 0
                            : Convert.ToInt32(reader["heureContrat"]);

                        int jour = reader.IsDBNull(reader.GetOrdinal("jourContrat"))
                            ? 0
                            : Convert.ToInt32(reader["jourContrat"]);

                        if (heure > 0)
                        {
                            // Heure actif → Jour désactivé
                            textBoxHeureContratGestion.Text = heure.ToString();
                            textBoxHeureContratGestion.Enabled = true;

                            textBoxJourContratGestion.Text = "0";
                            textBoxJourContratGestion.Enabled = false;
                        }
                        else if (jour > 0)
                        {
                            // Jour actif → Heure désactivé
                            textBoxJourContratGestion.Text = jour.ToString();
                            textBoxJourContratGestion.Enabled = true;

                            textBoxHeureContratGestion.Text = "0";
                            textBoxHeureContratGestion.Enabled = false;
                        }
                        else
                        {
                            // Les deux = 0 → activés tous les deux
                            textBoxHeureContratGestion.Text = "0";
                            textBoxJourContratGestion.Text = "0";

                            textBoxHeureContratGestion.Enabled = true;
                            textBoxJourContratGestion.Enabled = true;
                        }


                        // (Optionnel) tu peux afficher e.nomEntreprise/d.nomDirection/s.nomService/c.nomCategorie
                        // dans des labels si besoin.
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement de l'employé : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



            /// <summary>
            /// Charge les personnels dans un ComboBox.
            /// </summary>
            /// <param name="combo">ComboBox cible</param>
            /// <param name="idEntreprise">Filtre par entreprise (null = toutes)</param>
            /// <param name="selectedId">Id personnel à pré-sélectionner</param>
            /// <param name="includePlaceholder">Ajoute une ligne placeholder en tête</param>
            public static void ChargerPersonnels(
                ComboBox combo,
                int? idEntreprise = null,
                int? selectedId = null,
                bool includePlaceholder = true)
            {
                if (combo == null) return;

                var connect = new Dbconnect();
                using (var con = new MySqlConnection(connect.getconnection.ConnectionString))
                using (var cmd = new MySqlCommand(@"
                SELECT 
                    p.id_personnel                                  AS Id,
                    CONCAT(p.nomPrenom, 
                          CASE WHEN p.matricule IS NULL OR p.matricule = '' 
                               THEN '' ELSE CONCAT(' [', p.matricule, ']') END) AS Libelle
                FROM personnel p
                WHERE (@idEnt IS NULL OR p.id_entreprise = @idEnt)
                ORDER BY p.nomPrenom;", con))
                {
                    cmd.Parameters.Add("@idEnt", MySqlDbType.Int32).Value = (object)idEntreprise ?? DBNull.Value;

                    var table = new DataTable();
                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(table);
                    }

                    // Optionnel : placeholder tout en haut
                    if (includePlaceholder)
                    {
                        var row = table.NewRow();
                        row["Id"] = DBNull.Value;
                        row["Libelle"] = "-- Sélectionner --";
                        table.Rows.InsertAt(row, 0);
                    }

                    combo.DataSource = table;
                    combo.DisplayMember = "Libelle";
                    combo.ValueMember = "Id";

                    // Pré-sélection
                    if (selectedId.HasValue)
                    {
                        combo.SelectedValue = selectedId.Value;
                        // Si l'id n'est pas dans la liste, SelectedValue ne bouge pas : on force à -1
                        if (combo.SelectedValue == null || combo.SelectedIndex < 0)
                            combo.SelectedIndex = includePlaceholder ? 0 : -1;
                    }
                    else
                    {
                        combo.SelectedIndex = includePlaceholder ? 0 : -1;
                    }
                }
            }

            // Surcharges pratiques si tu veux appels plus courts :
            public static void ChargerPersonnels(ComboBox combo, int? selectedId, bool includePlaceholder)
                => ChargerPersonnels(combo, null, selectedId, includePlaceholder);

            public static void ChargerPersonnels(ComboBox combo, int? idEntreprise)
                => ChargerPersonnels(combo, idEntreprise, null, true);

            public static void ChargerPersonnels(ComboBox combo)
                => ChargerPersonnels(combo, null, null, true);





    private void ComboBoxEntreprise_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }



        private static string ReadStringOrEmpty(IDataRecord r, string col)
        {
            int i = r.GetOrdinal(col);
            return r.IsDBNull(i) ? "" : r.GetValue(i).ToString().Trim();
        }

        private static string NormalizeContrat(object raw)
        {
            if (raw == null) return "CDI";               // défaut safe
            var s = raw.ToString().Trim();

            // numérique / booléen
            int n; bool b;
            if (int.TryParse(s, out n)) return n == 1 ? "CDD" : "CDI";
            if (bool.TryParse(s, out b)) return b ? "CDD" : "CDI";

            // texte
            s = s.ToUpperInvariant();
            if (s == "CDD" || s == "CDI") return s;

            return "CDI"; // fallback
        }




        private void DataGridView_Personnel_Gestion_Click(object sender, EventArgs e)
        {
            if (DataGridView_Personnel_Gestion.CurrentRow != null)
            {
                string id = DataGridView_Personnel_Gestion.CurrentRow.Cells[0].Value?.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    textBoxID.Text = id;
                    ChargerDetailsEmployeParId(id);
                    //ComboBoxEntrepriseGestion.Enabled = false;
                }
            }
        }

        private void buttonModifier_Click(object sender, EventArgs e)
        {
            try
            {
                // -------- 1) ID employé --------
                if (!int.TryParse(textBoxID.Text?.Trim(), out int idPersonnel) || idPersonnel <= 0)
                {
                    MessageBox.Show("Identifiant employé invalide.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // -------- 2) Champs obligatoires simples --------
                string matricule = (textBoxMatricule.Text ?? "").Trim();
                if (string.IsNullOrWhiteSpace(matricule))
                {
                    MessageBox.Show("Le matricule est requis.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxMatricule.Focus();
                    return;
                }

                string nomPrenom = (textBoxNomPrenomGestion.Text ?? "").Trim();
                if (string.IsNullOrWhiteSpace(nomPrenom))
                {
                    MessageBox.Show("Le nom complet est requis.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxNomPrenomGestion.Focus();
                    return;
                }

                // -------- 3) Valeurs simples (texte) --------
                string civilite = ComboBoxCiviliteGestion.SelectedItem?.ToString();
                string sexe = ComboBoxSexeGestion.SelectedItem?.ToString();
                string adresse = (textBoxAdresseGestion.Text ?? "").Trim();
                string telephone = (textBoxTelephoneGestion.Text ?? "").Trim();
                string identification = (textBoxIdentificationGestion.Text ?? "").Trim();
                string poste = (textBoxPosteGestion.Text ?? "").Trim();
                string contrat = ComboBoxContratGestion.SelectedItem?.ToString();
                string typeContrat = ComboBoxtypecontratGestion.SelectedItem?.ToString();
                string modePayement = ComboBoxModePayementGestion.SelectedItem?.ToString();
                string banque = (textBoxBanqueGestion.Text ?? "").Trim();
                string numeroBancaire = (textBoxNumeroBancaireGestion.Text ?? "").Trim();
                string numerocnss = (textBoxCnssGestion.Text ?? "").Trim();
                string cadre = ComboBoxCadreGestion.SelectedItem?.ToString(); // "Oui"/"Non"

                // -------- 4) Dates --------
                DateTime dateNaissance = dateNaissanceGestionPicker.Value.Date;
                DateTime dateEntree = dateEntreeGestionPicker.Value.Date;

                // Si tu as un toggle/checkbox pour activer la date de sortie, branche-le ici
                DateTime? dateSortie = dateSortieGestionPicker.Enabled
                    ? (DateTime?)dateSortieGestionPicker.Value.Date
                    : null;

                // -------- 5) Entreprise / Direction / Service / Catégorie --------
                int idEntreprise = Convert.ToInt32(ComboBoxEntrepriseGestion.SelectedValue);
                int idDirection = Convert.ToInt32(ComboBoxDirectionGestion.SelectedValue);
                int idService = Convert.ToInt32(ComboBoxServiceGestion.SelectedValue);
                int idCategorie = Convert.ToInt32(ComboBoxCategorieGestion.SelectedValue);

                // -------- 6) Heure / Jour (XOR) --------
                int heureContrat = 0, jourContrat = 0;
                int.TryParse((textBoxHeureContratGestion.Text ?? "0").Trim(), out heureContrat);
                int.TryParse((textBoxJourContratGestion.Text ?? "0").Trim(), out jourContrat);

                if (heureContrat > 0) jourContrat = 0;
                else if (jourContrat > 0) heureContrat = 0;
                else { heureContrat = 0; jourContrat = 0; }

                // -------- 7) Salaire --------
                decimal salairemoyen;
                if (!decimal.TryParse((textBoxSalaireMoyenGestion.Text ?? "0").Trim(), out salairemoyen))
                {
                    MessageBox.Show("Salaire moyen invalide.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxSalaireMoyenGestion.Focus();
                    textBoxSalaireMoyenGestion.SelectAll();
                    return;
                }

                // -------- 8) Durée contrat (CDD/CDI) depuis les checkboxes --------
                string dureeContrat = CheckBoxCDDGestion.Checked ? "CDD" :
                                      CheckBoxCDIGestion.Checked ? "CDI" : "CDI"; // défaut

                // -------- Confirmation avant modification --------
                var result = MessageBox.Show(
                    "Voulez-vous vraiment modifier cet employé ?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                {
                    // L’utilisateur a cliqué sur Non → on annule
                    return;
                }

                // -------- Appel métier --------
                EmployeClass.ModifierEmploye(
                    idPersonnel,
                    matricule,
                    nomPrenom,
                    civilite,
                    sexe,
                    dateNaissance,
                    adresse,
                    telephone,
                    identification,
                    poste,
                    contrat,
                    cadre,
                    dateEntree,
                    dateSortie,
                    typeContrat,
                    heureContrat,
                    jourContrat,
                    modePayement,
                    banque,
                    numeroBancaire,
                    idDirection,
                    idService,
                    idCategorie,
                    idEntreprise,
                    dureeContrat,
                    numerocnss,
                    salairemoyen
                );

                // -------- UI feedback / refresh --------
                ClearFormPersonnelGestion();
                ShowTablePersonnelGestion();
                ChampsDisabledsGestion();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification : " + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonModifier.Enabled = true;
                UseWaitCursor = false;
            }
        }

        private void button_Supprimer_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxID.Text, out int idPersonnel) || idPersonnel <= 0)
            {
                MessageBox.Show("Veuillez sélectionner un employé valide.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            EmployeClass.SupprimerEmploye(idPersonnel);

            // Rafraîchir la table et vider le formulaire après suppression
            ShowTablePersonnelGestion();
            ClearFormPersonnelGestion();
            ChampsDisabledsGestion();
        }

        private void buttonEffacerGestion_Click(object sender, EventArgs e)
        {
            ClearFormPersonnelGestion();
            ChampsDisabledsGestion();
        }
    }
}