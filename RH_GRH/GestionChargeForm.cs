using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RH_GRH.ChargeClass;

namespace RH_GRH
{
    public partial class GestionChargeForm : Form
    {
        Dbconnect connect = new Dbconnect();

        // Champ privé du form pour éviter la récursion
        private bool _changingType;
        public GestionChargeForm()
        {
            InitializeComponent();
            EntrepriseClass.ChargerEntreprises(ComboBoxEntreprise);
            StyliserDataGridView();
            StyliserDataGridViewGestion();
            StyliserTabControl();
            ShowTableCategorie();
            GetChargeList(null);
            ShowTableCategorieGestion();
            CheckBoxEpouse.CheckedChanged += CheckBoxEpouse_CheckedChanged;
            CheckBoxEnfant.CheckedChanged += CheckBoxEnfant_CheckedChanged;

            // (Optionnel) état par défaut : rien coché
            CheckBoxEpouse.Checked = false;
            CheckBoxEnfant.Checked = false;
        }


        

        private void CheckBoxEpouse_CheckedChanged(object sender, EventArgs e)
        {
            if (_changingType) return;
            try
            {
                _changingType = true;

                if (CheckBoxEpouse.Checked)
                {
                    // exclusivité
                    CheckBoxEnfant.Checked = false;
                    ComboBoxScolarisation.Enabled = false;
                    textBoxIdentification.Enabled = true;
                }
                else
                {
                    // si on décoche tout, on laisse les deux à false (ou forcer Enfant si tu veux un défaut)
                    // CheckBoxEnfant.Checked = true;
                }
            }
            finally { _changingType = false; }
        }

        private void CheckBoxEnfant_CheckedChanged(object sender, EventArgs e)
        {
            if (_changingType) return;
            try
            {
                _changingType = true;

                if (CheckBoxEnfant.Checked)
                {
                    // exclusivité
                    CheckBoxEpouse.Checked = false;
                    textBoxIdentification.Enabled = false;
                    ComboBoxScolarisation.Enabled = true;
                }
                else
                {
                    // idem remarque ci-dessus
                    // CheckBoxEpouse.Checked = true;
                }
            }
            finally { _changingType = false; }
        }











        private void tabControlCharge_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlCharge.SelectedIndex == 1) // Onglet n°2 (index 1)
            {
                ChargerTablePage2();
                EntrepriseClass.ChargerEntreprises(ComboBoxEntrepriseGestion);
                StyliserDataGridViewGestion();
                ShowTableCategorieGestion();
                ClearChargeForm();
            }
            else if (tabControlCharge.SelectedIndex == 0)
            {
                ClearChargeForm();
                ShowTableCategorie();
                EntrepriseClass.ChargerEntreprises(ComboBoxEntreprise);
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
                UseWaitCursor = true;

                // 1) Récupération des données
                //DataTable dt = Categorie.GetCategorieList() ?? new DataTable();

                // 2) Tri par défaut si dispo
                // if (dt.Columns.Contains("Entreprise") && dt.Columns.Contains("Direction"))
                {
                    //  var dv = dt.DefaultView;
                    //  dv.Sort = "[Entreprise] ASC, [Direction] ASC";
                    //  dt = dv.ToTable();
                }

                // 3) Binding
                var grid = DataGridView_Charge_Gestion;
                grid.AutoGenerateColumns = true;
                //    grid.DataSource = dt;

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

        private void ClearChargeForm()
        {
            ComboBoxEntreprise.SelectedIndex = -1;
            ComboBoxEmploye.SelectedIndex = -1;
            ComboBoxScolarisation.SelectedIndex = -1;

            CheckBoxEnfant.Checked = false;
            CheckBoxEpouse.Checked = false;

            textBoxNomPrenom.Clear();
            textBoxIdentification.Clear();

        }



        private void DisableChamps()
        {
            ComboBoxEntreprise.Enabled = true;
            ComboBoxEmploye.Enabled = false;
            ComboBoxScolarisation.Enabled = false;

            CheckBoxEnfant.Enabled = false;
            CheckBoxEpouse.Enabled = false;

            textBoxNomPrenom.Enabled = false;
            textBoxIdentification.Enabled = false;

            dateNaissanceGestionPicker.Enabled = false;
        }



        private void EnableChamps()
        {
            ComboBoxEmploye.Enabled = true;
            ComboBoxScolarisation.Enabled = true;

            CheckBoxEnfant.Enabled = true;
            CheckBoxEpouse.Enabled = true;

            textBoxNomPrenom.Enabled = true;
            textBoxIdentification.Enabled = true;

            dateNaissanceGestionPicker.Enabled = true;
        }



        ////////////////////////////////////////////////////////////////////////////
        private void ClearChargeFormGestion()
        {
            ComboBoxEntrepriseGestion.SelectedIndex = 0;
            ComboBoxEmployeGestion.SelectedIndex = 0;
            ComboBoxScolarisationGestion.SelectedIndex = 0;

            CheckBoxEnfantGestion.Checked = false;
            CheckBoxEpouseGestion.Checked = false;

            textBoxNomPrenomGestion.Clear();
            textBoxMatricule.Clear();
            textBoxID.Clear();
            textBoxIdentificationGestion.Clear();
        }

        ////////////////////////////////////////////////////////////////////////////

        private void ShowTableCategorie()
        {
            var dt = ChargeClass.GetChargeList(null);
            DataGridView_Charge.AutoGenerateColumns = true;
            DataGridView_Charge.DataSource = dt;
            // Masquer les IDs si tu veux une vue clean
            if (DataGridView_Charge.Columns.Contains("ID"))
                DataGridView_Charge.Columns["ID"].Visible = false;
        }

        ///////////////////////////////////////////////////////////////////////////
        private void ShowTableCategorieGestion()
        {
            EntrepriseClass.ChargerEntreprises(ComboBoxEntrepriseGestion);
            var dt = ChargeClass.GetChargeList();
            DataGridView_Charge_Gestion.AutoGenerateColumns = true;
            DataGridView_Charge_Gestion.DataSource = dt;
           // Masquer les IDs si tu veux une vue clean
            if (DataGridView_Charge_Gestion.Columns.Contains("ID"))
                DataGridView_Charge_Gestion.Columns["ID"].Visible = false;
        }

        ///////////////////////////////////////////////////////////////////////////
        private void StyliserDataGridView()
        {
            // Fond général de la grille
            DataGridView_Charge.BackgroundColor = Color.White;

            // Désactiver le style visuel Windows
            DataGridView_Charge.EnableHeadersVisualStyles = false;

            // Style de l'en-tête
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.MidnightBlue;
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Montserrat", 10f, FontStyle.Bold);
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            headerStyle.WrapMode = DataGridViewTriState.True;
            headerStyle.SelectionBackColor = Color.MidnightBlue;     // ← Empêche le changement au clic
            headerStyle.SelectionForeColor = Color.White;            // ← Texte toujours blanc

            DataGridView_Charge.EnableHeadersVisualStyles = false;
            DataGridView_Charge.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridView_Charge.ColumnHeadersHeight = 35;

            // Style des cellules normales
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.BackColor = Color.White;
            cellStyle.ForeColor = Color.Black;
            cellStyle.Font = new Font("Montserrat", 9.5f, FontStyle.Regular);
            cellStyle.SelectionBackColor = Color.LightSteelBlue;
            cellStyle.SelectionForeColor = Color.Black;
            DataGridView_Charge.DefaultCellStyle = cellStyle;

            // Style des lignes alternées
            DataGridView_Charge.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;

            // Supprimer les bordures
            DataGridView_Charge.BorderStyle = BorderStyle.None;
            DataGridView_Charge.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Charge.GridColor = Color.LightGray;

            // Masquer l'entête de ligne à gauche
            DataGridView_Charge.RowHeadersVisible = false;

            // Autres options d’esthétique
            DataGridView_Charge.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Charge.MultiSelect = false;
        }

        ////////////////////////////////////////////////////////////////////////////

        private void StyliserDataGridViewGestion()
        {
            // Améliorer le rendu (anti-scintillement)
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, DataGridView_Charge_Gestion, new object[] { true });

            // Fond général
            DataGridView_Charge_Gestion.BackgroundColor = Color.White;

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

            DataGridView_Charge_Gestion.EnableHeadersVisualStyles = false;
            DataGridView_Charge_Gestion.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridView_Charge_Gestion.ColumnHeadersHeight = 35;

            // Cellules normales
            var cellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Montserrat", 9.5f),
                SelectionBackColor = Color.LightSteelBlue,
                SelectionForeColor = Color.Black
            };
            DataGridView_Charge_Gestion.DefaultCellStyle = cellStyle;

            // Lignes alternées
            DataGridView_Charge_Gestion.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;

            // Bordures & style
            DataGridView_Charge_Gestion.BorderStyle = BorderStyle.None;
            DataGridView_Charge_Gestion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Charge_Gestion.GridColor = Color.LightGray;
            DataGridView_Charge_Gestion.RowHeadersVisible = false;

            // Sélection
            DataGridView_Charge_Gestion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Charge_Gestion.MultiSelect = false;
        }

        ////////////////////////////////////////////////////////////////////////////

        private void StyliserTabControl()
        {
            tabControlCharge.Appearance = TabAppearance.Normal;
            tabControlCharge.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControlCharge.ItemSize = new Size(150, 35); // largeur, hauteur des onglets
            tabControlCharge.SizeMode = TabSizeMode.Fixed;
            tabControlCharge.DrawItem += TabControlEntreprise_DrawItem;
        }

        ////////////////////////////////////////////////////////////////////////////

        private void TabControlEntreprise_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControlCharge.TabPages[e.Index];
            Rectangle rect = tabControlCharge.GetTabRect(e.Index);
            bool isSelected = (e.Index == tabControlCharge.SelectedIndex);

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








        private void CheckBoxCDIGestion_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView_Categorie_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxMatricule_TextChanged(object sender, EventArgs e)
        {

        }

        private void ComboBoxEntreprise_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? idEnt = GetSelectedIntOrNull(ComboBoxEntreprise, "id_entreprise");
            if (idEnt == null || idEnt.Value <= 0)
            {
                // rien de sélectionné / placeholder
                ComboBoxEmploye.DataSource = null;
                return;
            }

            // Charge les employés filtrés par entreprise
            EmployeClass.ChargerEmployesParEntreprise(ComboBoxEmploye, idEnt.Value, null, true);
            EnableChamps();
        }

        /// <summary>
        /// Récupère l'ID sélectionné d'un ComboBox même si SelectedValue est un DataRowView.
        /// </summary>
        private static int? GetSelectedIntOrNull(ComboBox combo, string valueColumnName)
        {
            if (combo.SelectedValue == null) return null;

            // Cas normal : déjà un int
            if (combo.SelectedValue is int i) return i;

            // Certaines configs renvoient un DataRowView
            if (combo.SelectedValue is DataRowView drv)
            {
                var val = drv[valueColumnName];
                return val == DBNull.Value ? (int?)null : Convert.ToInt32(val);
            }

            // Fallback: essayer de parser
            if (int.TryParse(combo.SelectedValue.ToString(), out var parsed))
                return parsed;

            return null;
        }





        private void RefreshGridCharges()
        {
            if (ComboBoxEmploye.SelectedValue == null) return;
            int empId = Convert.ToInt32(ComboBoxEmploye.SelectedValue);
            var list = ChargeClass.GetChargeList(empId);
            DataGridView_Charge.DataSource = list;
            DataGridView_Charge.ClearSelection();
        }





        private void ChargerDetailsChargeParId(string idCharge)
        {
            try
            {
                const string sql = @"
SELECT
    c.id_charge,
    c.id_personnel,
    c.type,             
    c.nom_prenom,
    c.date_naissance,
    c.identification,
    c.scolarisation,
    p.nomPrenom      AS NomPersonnel,
    p.id_entreprise  AS IdEntreprise,
    e.nomEntreprise  AS NomEntreprise,
    p.matricule
FROM charge c
LEFT JOIN personnel p  ON p.id_personnel  = c.id_personnel
LEFT JOIN entreprise e ON e.id_entreprise = p.id_entreprise
WHERE c.id_charge = @id
LIMIT 1;";

                var connect = new Dbconnect();
                using (var con = new MySqlConnection(connect.getconnection.ConnectionString))
                using (var cmd = new MySqlCommand(sql, con))
                {
                    if (!int.TryParse(idCharge, out int idc))
                    {
                        MessageBox.Show("Identifiant de la charge invalide.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = idc;

                    con.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (!reader.Read())
                        {
                            // Nettoyage minimal
                            textBoxNomPrenomGestion.Text = "";
                            ComboBoxEntrepriseGestion.SelectedIndex = -1;
                            ComboBoxEmployeGestion.SelectedIndex = -1;
                            return;
                        }

                        // ----- Infos de la charge -----
                        string type = reader["type"]?.ToString();
                        textBoxNomPrenomGestion.Text = reader["nom_prenom"]?.ToString() ?? "";
                        textBoxMatricule.Text = reader["matricule"]?.ToString() ?? "";
                        textBoxIdentificationGestion.Text = reader["identification"]?.ToString() ?? "";

                        if (!reader.IsDBNull(reader.GetOrdinal("date_naissance")))
                            dateNaissanceGestionPicker.Value = Convert.ToDateTime(reader["date_naissance"]);
                        else
                            dateNaissanceGestionPicker.Value = DateTime.Today;

                        // Type = Epouse/Enfant
                        bool isEpouse = string.Equals(type, "Épouse", StringComparison.OrdinalIgnoreCase) ||
                                        string.Equals(type, "Epouse", StringComparison.OrdinalIgnoreCase);
                        CheckBoxEpouseGestion.Checked = isEpouse;
                        CheckBoxEnfantGestion.Checked = !isEpouse;

                        // Scolarisation
                        if (isEpouse)
                        {
                            ComboBoxScolarisationGestion.SelectedIndex = -1;
                            ComboBoxScolarisationGestion.Enabled = false;
                        }
                        else
                        {
                            ComboBoxScolarisationGestion.Enabled = true;
                            string scolarisationDb = reader["scolarisation"]?.ToString();
                            if (scolarisationDb == "Oui") ComboBoxScolarisationGestion.SelectedItem = "Oui";
                            else if (scolarisationDb == "Non") ComboBoxScolarisationGestion.SelectedItem = "Non";
                            else ComboBoxScolarisationGestion.SelectedIndex = -1;
                        }

                        // ----- Combo Entreprise -----
                        if (!reader.IsDBNull(reader.GetOrdinal("IdEntreprise")))
                        {
                            int idEnt = Convert.ToInt32(reader["IdEntreprise"]);
                            // Charger la liste d’entreprises si nécessaire
                            if (ComboBoxEntrepriseGestion.DataSource == null)
                                EntrepriseClass.ChargerEntreprises(ComboBoxEntrepriseGestion, idEnt, true);
                            else
                                ComboBoxEntrepriseGestion.SelectedValue = idEnt;
                        }
                        else
                        {
                            ComboBoxEntrepriseGestion.SelectedIndex = -1;
                        }

                        // ----- Combo Employé -----
                        if (!reader.IsDBNull(reader.GetOrdinal("id_personnel")))
                        {
                            int idPers = Convert.ToInt32(reader["id_personnel"]);
                            if (ComboBoxEmployeGestion.DataSource == null)
                                GestionEmployeForm.ChargerPersonnels(ComboBoxEmployeGestion, idPers, true); // tu dois avoir une méthode similaire à ChargerEntreprises
                            else
                                ComboBoxEmployeGestion.SelectedValue = idPers;
                        }
                        else
                        {
                            ComboBoxEmployeGestion.SelectedIndex = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement de la charge : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






        private void buttonAjouter_Click(object sender, EventArgs e)
        {

            try
            {
                // Sélection des valeurs d'entreprise et employé
                int? idEnt = GetSelectedIntOrNull(ComboBoxEntreprise, "id_entreprise");
                if (idEnt == null || idEnt <= 0)
                {
                    MessageBox.Show("Sélectionnez une entreprise.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int? idEmp = GetSelectedIntOrNull(ComboBoxEmploye, "id_personnel");
                if (idEmp == null || idEmp <= 0)
                {
                    MessageBox.Show("Sélectionnez un employé.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Validation du type (épouse ou enfant)
                bool isEpouse = CheckBoxEpouse.Checked;
                bool isEnfant = CheckBoxEnfant.Checked;

                if (isEpouse == isEnfant) // Cas où aucune case ou les deux cases sont cochées
                {
                    MessageBox.Show("Sélectionnez exactement une option : Épouse ou Enfant.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Récupérer les informations saisies
                string nomPrenom = textBoxNomPrenom.Text.Trim();
                if (string.IsNullOrWhiteSpace(nomPrenom))
                {
                    MessageBox.Show("Saisissez le Nom & Prénom.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DateTime dateNaissance = dateNaissancePicker.Value;
                string identification = textBoxIdentification.Text.Trim();

                string scolarisation = ComboBoxScolarisation.SelectedItem?.ToString();


                if (string.IsNullOrWhiteSpace(identification) && isEpouse == true)
                {
                    MessageBox.Show(
                        "Veuillez renseigner les coordonnées d'identification de l'épouse",
                        "Info",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }


                if (isEpouse == true)
                {
                   scolarisation="";
                }


                // Appeler la méthode d'enregistrement avec la validation métier
                ChargeType type = isEpouse ? ChargeType.Epouse : ChargeType.Enfant;
                ChargeClass.EnregistrerCharge(idEmp.Value, type, nomPrenom, dateNaissance, identification, scolarisation);

                MessageBox.Show("Charge ajoutée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshGridCharges();  // Rafraîchir la grille des charges
                ClearChargeForm();      // Vider les champs du formulaire
                DisableChamps();    
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Règle métier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }







        private void DataGridView_Categorie_Gestion_Click(object sender, EventArgs e)
        {
            if (DataGridView_Charge_Gestion.CurrentRow != null)
            {
                string id = DataGridView_Charge_Gestion.CurrentRow.Cells[0].Value?.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    textBoxID.Text = id;
                    ChargerDetailsChargeParId(id);
                    ComboBoxEntrepriseGestion.Enabled = false;
                    button_Supprimer.Enabled = true;
                    EnableChargeFormForEdit();
                }
            }
        }

        private void buttonModifier_Click(object sender, EventArgs e)
        {
            try
            {
                // --- Id charge ---
                if (!int.TryParse(textBoxID.Text, out int idCharge) || idCharge <= 0)
                {
                    MessageBox.Show("Identifiant de la charge invalide.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Type (Épouse / Enfant)
                bool isEpouse = CheckBoxEpouseGestion.Checked;
                var typeEnum = isEpouse ? ChargeType.Epouse : ChargeType.Enfant;

                // Saisie
                string nomPrenom = (textBoxNomPrenomGestion.Text ?? "").Trim();
                string identification = (textBoxIdentificationGestion.Text ?? "").Trim();

                // Scolarisation (string)
                string scolarisation = null; // null par défaut
                if (!isEpouse)
                {
                    var s = ComboBoxScolarisationGestion.SelectedItem?.ToString();
                    if (s == "Oui") scolarisation = "Oui";
                    else if (s == "Non") scolarisation = "Non";
                } // si épouse => reste null

                // Date de naissance
                DateTime dateNaissance = dateNaissanceGestionPicker.Value;

                // --- Validations minimales UI ---
                if (string.IsNullOrWhiteSpace(nomPrenom))
                {
                    MessageBox.Show("Le nom/prénom de la charge est requis.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (isEpouse && string.IsNullOrWhiteSpace(identification))
                {
                    MessageBox.Show("Veuillez renseigner les coordonnées d'identification de l'épouse.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // --- Construction de l'objet à modifier ---
                var charge = new Charge
                {
                    IdCharge = idCharge,
                    Type = typeEnum,
                    NomPrenom = nomPrenom,
                    DateNaissance = dateNaissance,
                    Identification = identification,
                    Scolarisation = scolarisation  // "Oui" / "Non" / null
                };

                // --- Confirmation utilisateur ---
                var confirm = MessageBox.Show(
                    "Voulez-vous vraiment enregistrer ces modifications ?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (confirm != DialogResult.Yes)
                {
                    // L'utilisateur a annulé
                    return;
                }

                // (Optionnel) désactiver le bouton le temps de l'opération
                buttonModifier.Enabled = false;

                try
                {
                    // --- Update repo ---
                    ChargeRepository.ModifierCharge(charge);

                    MessageBox.Show("Modification enregistrée avec succès.", "Succès",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowTableCategorieGestion();

                    // --- Rafraîchir la liste (si vous avez une grille) ---
                    // ReloadChargesGrid();  // décommentez si vous avez une méthode de reload

                    // --- Nettoyer & désactiver le formulaire ---
                    ClearAndDisableChargeForm();
                }
                catch (InvalidOperationException ex) // règles métier (épouse unique, max 4, etc.)
                {
                    MessageBox.Show(ex.Message, "Règle métier",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la modification : " + ex.Message, "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    buttonModifier.Enabled = true; // on réactive le bouton
                }

            }
            catch (InvalidOperationException ex) // règles métier (épouse unique, max 4, etc.)
            {
                MessageBox.Show(ex.Message, "Règle métier",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ClearAndDisableChargeForm()
        {
            // Vider
            textBoxID.Text = "";
            textBoxMatricule.Text = "";
            textBoxNomPrenomGestion.Text = "";
            textBoxIdentificationGestion.Text = "";
            if (ComboBoxScolarisationGestion != null)
            {
                ComboBoxScolarisationGestion.SelectedIndex = -1;
                ComboBoxScolarisationGestion.Enabled = false; // par défaut on laisse désactivé après update
            }

            CheckBoxEpouseGestion.Checked = false;
            CheckBoxEnfantGestion.Checked = false;

            dateNaissanceGestionPicker.Value = DateTime.Today;

            if (ComboBoxEntrepriseGestion != null) ComboBoxEntrepriseGestion.SelectedIndex = -1;
            if (ComboBoxEmployeGestion != null) ComboBoxEmployeGestion.SelectedIndex = -1;

            // Désactiver
            SetChargeInputsEnabled(false);
        }

        private void SetChargeInputsEnabled(bool enabled)
        {
            textBoxNomPrenomGestion.Enabled = enabled;
            textBoxIdentificationGestion.Enabled = enabled;

            // La combo scolarisation ne s’active que si Enfant
            if (enabled)
                ComboBoxScolarisationGestion.Enabled = CheckBoxEnfantGestion.Checked;
            else
                ComboBoxScolarisationGestion.Enabled = false;

            dateNaissanceGestionPicker.Enabled = enabled;

            // Selon votre UX, vous pouvez aussi désactiver ces combos après update
            if (ComboBoxEntrepriseGestion != null) ComboBoxEntrepriseGestion.Enabled = enabled;
            if (ComboBoxEmployeGestion != null) ComboBoxEmployeGestion.Enabled = enabled;

            CheckBoxEpouseGestion.Enabled = enabled;
            CheckBoxEnfantGestion.Enabled = enabled;

            // Boutons (au choix)
            buttonModifier.Enabled = enabled;
            // btnAjouter.Enabled = enabled; // adaptez selon votre flux
        }

        private void EnableChargeFormForEdit()
        {
            // Active uniquement les champs autorisés
            textBoxNomPrenomGestion.Enabled = true;
            textBoxIdentificationGestion.Enabled = true;
            dateNaissanceGestionPicker.Enabled = true;

            // Gestion scolarisation : activée seulement si Enfant
            if (CheckBoxEnfantGestion.Checked)
            {
                ComboBoxScolarisationGestion.Enabled = true;
            }
            else
            {
                ComboBoxScolarisationGestion.SelectedIndex = -1;
                ComboBoxScolarisationGestion.Enabled = false; // épouse => null
            }

            // Ces champs restent désactivés en mode édition
            if (ComboBoxEntrepriseGestion != null) ComboBoxEntrepriseGestion.Enabled = false;
            if (ComboBoxEmployeGestion != null) ComboBoxEmployeGestion.Enabled = false;
            CheckBoxEpouseGestion.Enabled = false;
            CheckBoxEnfantGestion.Enabled = false;

            // Boutons d’action
            buttonModifier.Enabled = true;
            // buttonAjouter.Enabled = false; // si tu veux bloquer l'ajout pendant modification
        }

        private void button_Supprimer_Click(object sender, EventArgs e)
        {
            try
            {
                // Vérifier qu’un ID est bien présent
                if (!int.TryParse(textBoxID.Text, out int idCharge) || idCharge <= 0)
                {
                    MessageBox.Show("Veuillez sélectionner une charge valide à supprimer.",
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Confirmation
                var confirm = MessageBox.Show(
                    "Voulez-vous vraiment supprimer cette charge ?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                if (confirm != DialogResult.Yes)
                    return; // Annulation utilisateur

                // Suppression via repository
                ChargeRepository.Supprimer(idCharge);

                MessageBox.Show("Charge supprimée avec succès.",
                    "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Rafraîchir la liste si tu as une DataGridView
                ShowTableCategorieGestion();

                // Vider et désactiver les champs
                ClearAndDisableChargeForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression : " + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRechercher_Click(object sender, EventArgs e)
        {
           DataGridView_Charge_Gestion.DataSource = ChargeClass.RechercheCharge(textBoxSearch.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void GestionChargeForm_Load(object sender, EventArgs e)
        {
            tabControlCharge.SelectedIndexChanged += tabControlCharge_SelectedIndexChanged;
        }
    }
}
