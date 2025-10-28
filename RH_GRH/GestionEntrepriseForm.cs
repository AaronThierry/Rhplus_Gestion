using MySql.Data.MySqlClient;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RH_GRH.BulletinModel;

namespace RH_GRH
{
    public partial class GestionEntrepriseForm : Form
    {
        EntrepriseClass Entreprise = new EntrepriseClass();
        Dbconnect connect = new Dbconnect();

        public GestionEntrepriseForm()
        {
            InitializeComponent();
            StyliserDataGridView();
            StyliserDataGridViewGestion();
            StyliserTabControl();
            showTableEntreprise();

        }

        private void ManagerEntrepriseForm_Load(object sender, EventArgs e)
        {
            tabControlEntreprise.SelectedIndexChanged += tabControlEntreprise_SelectedIndexChanged;


        }
        private void tabControlEntreprise_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlEntreprise.SelectedIndex == 1) // Onglet n°2 (index 1)
            {
                ChargerTablePage2();
                StyliserDataGridView();
                showTableEntrepriseGestion();
                ClearEntrepriseFormGestion();
            }
            else if (tabControlEntreprise.SelectedIndex == 0)
            {
                ClearEntrepriseForm();
                showTableEntreprise();
            }
        }

        private bool tablePage2Chargee = false;

        private void ChargerTablePage2()
        {

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                DataTable table = Entreprise.getEntrepriseList(); // ou autre source
                DataGridView_Entreprise_Gestion.DataSource = table;
                StyliserDataGridView();
                tablePage2Chargee = true; // pour ne charger qu’une fois
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de chargement : " + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }



        public void showTableEntreprise()
        {
            DataGridView_Entreprise.DataSource = Entreprise.getEntrepriseList();
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_Entreprise.Columns[5];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        public void showTableEntrepriseGestion()
        {
            DataGridView_Entreprise_Gestion.DataSource = Entreprise.getEntrepriseList();
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
          imageColumn = (DataGridViewImageColumn)DataGridView_Entreprise_Gestion.Columns[5];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void StyliserDataGridView()
        {
            // Fond général de la grille
            DataGridView_Entreprise.BackgroundColor = Color.White;

            // Désactiver le style visuel Windows
            DataGridView_Entreprise.EnableHeadersVisualStyles = false;

            // Style de l'en-tête
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.MidnightBlue;
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Montserrat", 10f, FontStyle.Bold);
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            headerStyle.WrapMode = DataGridViewTriState.True;
            headerStyle.SelectionBackColor = Color.MidnightBlue;     // ← Empêche le changement au clic
            headerStyle.SelectionForeColor = Color.White;            // ← Texte toujours blanc

            DataGridView_Entreprise.EnableHeadersVisualStyles = false;
            DataGridView_Entreprise.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridView_Entreprise.ColumnHeadersHeight = 35;

            // Style des cellules normales
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.BackColor = Color.White;
            cellStyle.ForeColor = Color.Black;
            cellStyle.Font = new Font("Montserrat", 9.5f, FontStyle.Regular);
            cellStyle.SelectionBackColor = Color.LightSteelBlue;
            cellStyle.SelectionForeColor = Color.Black;
            DataGridView_Entreprise.DefaultCellStyle = cellStyle;

            // Style des lignes alternées
            DataGridView_Entreprise.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;

            // Supprimer les bordures
            DataGridView_Entreprise.BorderStyle = BorderStyle.None;
            DataGridView_Entreprise.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Entreprise.GridColor = Color.LightGray;

            // Masquer l'entête de ligne à gauche
            DataGridView_Entreprise.RowHeadersVisible = false;

            // Autres options d’esthétique
            DataGridView_Entreprise.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Entreprise.MultiSelect = false;
        }
        private void StyliserDataGridViewGestion()
        {
            // Améliorer le rendu (anti-scintillement)
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, DataGridView_Entreprise_Gestion, new object[] { true });

            // Fond général
            DataGridView_Entreprise_Gestion.BackgroundColor = Color.White;

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

            DataGridView_Entreprise_Gestion.EnableHeadersVisualStyles = false;
            DataGridView_Entreprise_Gestion.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridView_Entreprise_Gestion.ColumnHeadersHeight = 35;

            // Cellules normales
            var cellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Montserrat", 9.5f),
                SelectionBackColor = Color.LightSteelBlue,
                SelectionForeColor = Color.Black
            };
            DataGridView_Entreprise_Gestion.DefaultCellStyle = cellStyle;

            // Lignes alternées
            DataGridView_Entreprise_Gestion.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;

            // Bordures & style
            DataGridView_Entreprise_Gestion.BorderStyle = BorderStyle.None;
            DataGridView_Entreprise_Gestion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Entreprise_Gestion.GridColor = Color.LightGray;
            DataGridView_Entreprise_Gestion.RowHeadersVisible = false;

            // Sélection
            DataGridView_Entreprise_Gestion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Entreprise_Gestion.MultiSelect = false;
        }

        private void StyliserTabControl()
        {
            tabControlEntreprise.Appearance = TabAppearance.Normal;
            tabControlEntreprise.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControlEntreprise.ItemSize = new Size(150, 35); // largeur, hauteur des onglets
            tabControlEntreprise.SizeMode = TabSizeMode.Fixed;
            tabControlEntreprise.DrawItem += TabControlEntreprise_DrawItem;
        }

        private void TabControlEntreprise_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControlEntreprise.TabPages[e.Index];
            Rectangle rect = tabControlEntreprise.GetTabRect(e.Index);
            bool isSelected = (e.Index == tabControlEntreprise.SelectedIndex);

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

        private void buttonEffacer_Click(object sender, EventArgs e)
        {
            ClearEntrepriseForm();
        }

        private void ClearEntrepriseForm()
        {
            textBoxnomEntreprise.Clear();
            textBoxFormeJuridique.Clear();
            textBoxSigle.Clear();
            textBoxActivite.Clear();
            textBoxAdrPhysique.Clear();
            textBoxAdrPostale.Clear();
            textBoxTelephone.Clear();
            textBoxCommune.Clear();
            textBoxQuartier.Clear();
            textBoxRue.Clear();
            textBoxLot.Clear();
            textBoxCentreImpots.Clear();
            textBoxIFU.Clear();
            textBoxCNSS.Clear();
            textBoxCodeActivite.Clear();
            textBoxRegimeFiscal.Clear();
            textBoxRegistreCommerce.Clear();
            textBoxNumeroBancaire.Clear();
            textBoxTPA.Clear();
            pictureBox_logo.Image = null;
        }

        private void ClearEntrepriseFormGestion()
        {
            textBoxnomEntrepriseGestion.Clear();
            textBoxFormeJuridiqueGestion.Clear();
            textBoxSigleGestion.Clear();
            textBoxActiviteGestion.Clear();
            textBoxAdrPhysiqueGestion.Clear();
            textBoxAdrPostaleGestion.Clear();
            textBoxTelephoneGestion.Clear();
            textBoxCommuneGestion.Clear();
            textBoxQuartierGestion.Clear();
            textBoxRueGestion.Clear();
            textBoxLotGestion.Clear();
            textBoxCentreImpotsGestion.Clear();
            textBoxIFUGestion.Clear();
            textBoxCNSSGestion.Clear();
            textBoxCodeActiviteGestion.Clear();
            textBoxRegimeFiscalGestion.Clear();
            textBoxRegistreCommerceGestion.Clear();
            textBoxNumeroBancaireGestion.Clear();
            textBoxTPAGestion.Clear();
            pictureBoxGestion.Image = null;
            textBoxID.Clear();
        }


        private void buttonAjouter_Click(object sender, EventArgs e)
        {

            string nomEntreprise = textBoxnomEntreprise.Text.Trim();
            string formeJuridique = textBoxFormeJuridique.Text.Trim();
            string sigle = textBoxSigle.Text.Trim();
            string activite = textBoxActivite.Text.Trim();
            string adressePhysique = textBoxAdrPhysique.Text.Trim();
            string adressePostale = textBoxAdrPostale.Text.Trim();
            string telephone = textBoxTelephone.Text.Trim();
            string commune = textBoxCommune.Text.Trim();
            string quartier = textBoxQuartier.Text.Trim();
            string rue = textBoxRue.Text.Trim();
            string lot = textBoxLot.Text.Trim();
            string centreImpots = textBoxCentreImpots.Text.Trim();
            string numeroIfu = textBoxIFU.Text.Trim();
            string numeroCnss = textBoxCNSS.Text.Trim();
            string codeActivite = textBoxCodeActivite.Text.Trim();
            string regimeFiscal = textBoxRegimeFiscal.Text.Trim();
            string registreCommerce = textBoxRegistreCommerce.Text.Trim();
            string numeroBancaire = textBoxNumeroBancaire.Text.Trim();
            string email = textBoxEmail.Text.Trim(); // Champ email non présent dans le formulaire

            // tpa et effortPaix sont des décimaux, donc on parse
            decimal? tpa = null;
            if (decimal.TryParse(textBoxTPA.Text.Trim(), out decimal tpaParsed))
                tpa = tpaParsed;


            //Enregistrement 
            if (nomEntreprise == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {

                try
                {
                    MemoryStream ms = new MemoryStream();
                    pictureBox_logo.Image.Save(ms, pictureBox_logo.Image.RawFormat);
                    byte[] img = ms.ToArray();

                    if (Entreprise.insertEntreprise(nomEntreprise, formeJuridique, sigle, activite, adressePhysique, adressePostale, telephone, commune, quartier, rue, lot, centreImpots, numeroIfu, numeroCnss, codeActivite, regimeFiscal, registreCommerce, numeroBancaire, tpa, img, email))
                    {

                        MessageBox.Show("Entreprise enregistrée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        showTableEntreprise();
                        ClearEntrepriseForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Chmps vide", "Ajout Entreprise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



            //Function verify 
            bool verify()
            {
                if ((textBoxnomEntreprise.Text == "") || (textBoxTelephone.Text == "") || (pictureBox_logo.Image == null))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void buttonparcourir_Click(object sender, EventArgs e)
        {
            // Chrger photo
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox_logo.Image = Image.FromFile(opf.FileName);
            }
        }

        private void ChargerDetailsEntrepriseParId(string id)
        {
            try
            {
                string query = "SELECT * FROM entreprise WHERE id_entreprise = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, connect.getconnection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    connect.openConnect();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBoxnomEntrepriseGestion.Text = reader["nomEntreprise"]?.ToString();
                            textBoxFormeJuridiqueGestion.Text = reader["forme_juridique"]?.ToString();
                            textBoxSigleGestion.Text = reader["sigle"]?.ToString();
                            textBoxActiviteGestion.Text = reader["activite"]?.ToString();
                            textBoxAdrPhysiqueGestion.Text = reader["adresse_physique"]?.ToString();
                            textBoxAdrPostaleGestion.Text = reader["adresse_postale"]?.ToString();
                            textBoxTelephoneGestion.Text = reader["telephone"]?.ToString();
                            textBoxCommuneGestion.Text = reader["commune"]?.ToString();
                            textBoxQuartierGestion.Text = reader["quartier"]?.ToString();
                            textBoxRueGestion.Text = reader["rue"]?.ToString();
                            textBoxLotGestion.Text = reader["lot"]?.ToString();
                            textBoxCentreImpotsGestion.Text = reader["centre_impots"]?.ToString();
                            textBoxIFUGestion.Text = reader["numero_ifu"]?.ToString();
                            textBoxCNSSGestion.Text = reader["numero_cnss"]?.ToString();
                            textBoxCodeActiviteGestion.Text = reader["code_activite"]?.ToString();
                            textBoxRegimeFiscalGestion.Text = reader["regime_fiscal"]?.ToString();
                            textBoxRegistreCommerceGestion.Text = reader["registre_commerce"]?.ToString();
                            textBoxNumeroBancaireGestion.Text = reader["numero_bancaire"]?.ToString();
                            textBoxTPAGestion.Text = reader["tpa"]?.ToString();


                            if (DataGridView_Entreprise_Gestion.CurrentRow.Cells[5].Value != DBNull.Value)
                            {
                                byte[] img = (byte[])DataGridView_Entreprise_Gestion.CurrentRow.Cells[5].Value;

                                using (MemoryStream ms = new MemoryStream(img))
                                {
                                    pictureBoxGestion.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                pictureBoxGestion.Image = null; // aucune image
                            }


                        }
                    }

                    connect.closeConnect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur chargement infos entreprise : " + ex.Message);
            }
        }

        private void DataGridView_Entreprise_Gestion_Click(object sender, EventArgs e)
        {
            if (DataGridView_Entreprise_Gestion.CurrentRow != null)
            {
                string id = DataGridView_Entreprise_Gestion.CurrentRow.Cells[0].Value?.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    textBoxID.Text = id;
                    ChargerDetailsEntrepriseParId(id);
                }
            }
        }




        //*************Bouton rechercher**************

        private void button5_Click(object sender, EventArgs e)
        {
            // Charger les résultats de la recherche
            DataGridView_Entreprise_Gestion.DataSource = Entreprise.rechercheEntreprise(textBoxSearch.Text);

            // Appliquer le style "Zoom" à la colonne image si elle existe
            if (DataGridView_Entreprise_Gestion.Columns.Count > 5 &&
                DataGridView_Entreprise_Gestion.Columns[5] is DataGridViewImageColumn imageColumn)
            {
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
        }

        //************Fin Bouton rechercher***************









        //************Bouton charger photo Inscription*************

        private void buttonparcourir_Click_1(object sender, EventArgs e)
        {
            // Chrger photo
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox_logo.Image = Image.FromFile(opf.FileName);
            }
        }

        //*************Fin bouton charger photo Inscription*************








        //********************** Bouton Enregistrer une nouvelle entreprise**************

        private void buttonAjouter_Click_1(object sender, EventArgs e)
        {
            // --- Lecture des champs saisis ---
            string nomEntreprise = textBoxnomEntreprise.Text.Trim();
            string formeJuridique = textBoxFormeJuridique.Text.Trim();
            string sigle = textBoxSigle.Text.Trim();
            string activite = textBoxActivite.Text.Trim();
            string adressePhysique = textBoxAdrPhysique.Text.Trim();
            string adressePostale = textBoxAdrPostale.Text.Trim();
            string telephone = textBoxTelephone.Text.Trim();
            string commune = textBoxCommune.Text.Trim();
            string quartier = textBoxQuartier.Text.Trim();
            string rue = textBoxRue.Text.Trim();
            string lot = textBoxLot.Text.Trim();
            string centreImpots = textBoxCentreImpots.Text.Trim();
            string numeroIfu = textBoxIFU.Text.Trim();
            string numeroCnss = textBoxCNSS.Text.Trim();
            string codeActivite = textBoxCodeActivite.Text.Trim();
            string regimeFiscal = textBoxRegimeFiscal.Text.Trim();
            string registreCommerce = textBoxRegistreCommerce.Text.Trim();
            string numeroBancaire = textBoxNumeroBancaire.Text.Trim();
            string email = textBoxEmail.Text.Trim(); // Champ email non présent dans le formulaire

            // Lecture du TPA (nullable)
            decimal? tpa = null;
            if (decimal.TryParse(textBoxTPA.Text.Trim(), out decimal tpaParsed))
                tpa = tpaParsed;

            // --- Vérification des champs obligatoires ---
            if (!verifyRequiredFields())
            {
                MessageBox.Show("Veuillez renseigner le nom, le téléphone et sélectionner un logo.",
                                "Champs obligatoires", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- Vérification de doublons nom ou sigle ---
            if (EntrepriseClass.entrepriseExiste(nomEntreprise, sigle))
            {
                MessageBox.Show("Cette entreprise existe déjà dans la base.",
                                "Doublon détecté", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // --- Conversion du logo en byte[] ---
                MemoryStream ms = new MemoryStream();
                pictureBox_logo.Image.Save(ms, pictureBox_logo.Image.RawFormat);
                byte[] logoBytes = ms.ToArray();

                // --- Insertion en base de données ---
                bool success = Entreprise.insertEntreprise(
                    nomEntreprise, formeJuridique, sigle, activite, adressePhysique, adressePostale,
                    telephone, commune, quartier, rue, lot, centreImpots, numeroIfu, numeroCnss,
                    codeActivite, regimeFiscal, registreCommerce, numeroBancaire, tpa, logoBytes,
                    email
                );

                if (success)
                {
                    MessageBox.Show("Entreprise enregistrée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showTableEntreprise();
                    ClearEntrepriseForm();
                }
                else
                {
                    MessageBox.Show("L'enregistrement a échoué. Veuillez réessayer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'enregistrement :\n" + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Méthode interne pour la vérification des champs requis
            bool verifyRequiredFields()
            {
                return !string.IsNullOrWhiteSpace(textBoxnomEntreprise.Text)
                    && !string.IsNullOrWhiteSpace(textBoxTelephone.Text)
                    && pictureBox_logo.Image != null;
            }
        }

        //*************Fin bouton Enregistrer**********************










        //***********Bouton charger photo gestion*****************

        private void button3_Click(object sender, EventArgs e)
        {
            // Chrger photo
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxGestion.Image = Image.FromFile(opf.FileName);
            }
        }

        //**********Bouton charger photo gestion******************





        //*************Bouton Effacer Gestion****************

        private void button4_Click(object sender, EventArgs e)
        {
            ClearEntrepriseFormGestion();
        }

        //*************Fin Bouton Effacer Gestion****************






        //*************Bouton Effacer Inscription**********************

        private void buttonEffacer_Click_1(object sender, EventArgs e)
        {
            ClearEntrepriseForm();
        }

        //*************Fin Bouton Effacer Inscription****************






        //************Bouton Modifier Entreprise*****************

        private void Button2_Click(object sender, EventArgs e)
        {
            // Vérification de l'ID sélectionné
            if (!int.TryParse(textBoxID.Text, out int idEntreprise))
            {
                MessageBox.Show("Veuillez sélectionner une entreprise à modifier.", "ID manquant", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Champs essentiels
            string nomEntreprise = textBoxnomEntrepriseGestion.Text.Trim();
            string telephone = textBoxTelephoneGestion.Text.Trim();
            Image logoImage = pictureBoxGestion.Image;

            if (string.IsNullOrWhiteSpace(nomEntreprise) || string.IsNullOrWhiteSpace(telephone) || logoImage == null)
            {
                MessageBox.Show("Veuillez renseigner le nom, le téléphone et le logo de l'entreprise.", "Champs obligatoires", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmation avant modification
            DialogResult confirmation = MessageBox.Show(
                "Êtes-vous sûr de vouloir modifier les informations de cette entreprise ?",
                "Confirmation de modification",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmation != DialogResult.Yes)
            {
                return; // Annulation par l'utilisateur
            }

            // Autres champs
            string formeJuridique = textBoxFormeJuridiqueGestion.Text.Trim();
            string sigle = textBoxSigleGestion.Text.Trim();
            string activite = textBoxActiviteGestion.Text.Trim();
            string adressePhysique = textBoxAdrPhysiqueGestion.Text.Trim();
            string adressePostale = textBoxAdrPostaleGestion.Text.Trim();
            string commune = textBoxCommuneGestion.Text.Trim();
            string quartier = textBoxQuartierGestion.Text.Trim();
            string rue = textBoxRueGestion.Text.Trim();
            string lot = textBoxLotGestion.Text.Trim();
            string centreImpots = textBoxCentreImpotsGestion.Text.Trim();
            string numeroIfu = textBoxIFUGestion.Text.Trim();
            string numeroCnss = textBoxCNSSGestion.Text.Trim();
            string codeActivite = textBoxCodeActiviteGestion.Text.Trim();
            string regimeFiscal = textBoxRegimeFiscalGestion.Text.Trim();
            string registreCommerce = textBoxRegistreCommerceGestion.Text.Trim();
            string numeroBancaire = textBoxNumeroBancaireGestion.Text.Trim();
            string email = textBoxEmailGestion.Text.Trim(); // Champ email non présent dans le formulaire

            decimal? tpa = null;
            if (decimal.TryParse(textBoxTPAGestion.Text.Trim(), out decimal tpaParsed))
                tpa = tpaParsed;

            try
            {
                MemoryStream ms = new MemoryStream();
                logoImage.Save(ms, logoImage.RawFormat);
                byte[] logoBytes = ms.ToArray();

                bool success = Entreprise.updateEntreprise(
                    idEntreprise, nomEntreprise, formeJuridique, sigle, activite, adressePhysique,
                    adressePostale, telephone, commune, quartier, rue, lot, centreImpots, numeroIfu,
                    numeroCnss, codeActivite, regimeFiscal, registreCommerce, numeroBancaire, tpa, logoBytes,
                    email
                );

                if (success)
                {
                    MessageBox.Show("L'entreprise a été modifiée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showTableEntrepriseGestion();
                    ClearEntrepriseFormGestion();

                }
                else
                {
                    MessageBox.Show("La modification a échoué. Veuillez vérifier les informations.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification :\n" + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //****************Fin Bouton modifier Entreprise**************








        //*******************Bouton supprimer Entreprise************************

        private void button1_Click(object sender, EventArgs e)
 
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                var model = new BulletinModel
                {
                    NomEmploye = "OUEDRAOGO Issa",
                    Civilite = "M",
                    Matricule = "EMP001",
                    Poste = "Comptable",
                    Mois = "Août 2025",
                    SalaireDeBase = 30000.50,
                    HeuresSup = 15000,
                    CNSS = 8500,
                    SalaireNet = 256500,
                    Sigle = "RH+",
                    NomEntreprise = "Cyberlink Afrique",
                    AdressePostaleEntreprise = "05 BP 6520 Ouagadougou / Ouagadougou,sect 06, Baskuy",
                    AdresseEmploye = "Ouagadougou,sect 06, Baskuy",
                    Periode = "01/08/2025 - 16/09/2025",
                    LogoEntreprise = File.ReadAllBytes(@"C:\Users\aaron\source\repos\RH_GRH\RH_GRH\Resources\logo-genux.png"),
                    TelephoneEntreprise = "+22607122327 / 72467143",
                    courrier = "aarontamini01@gmail.com"

                };



                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Title = "Enregistrer le bulletin de paie";
                    saveDialog.Filter = "Fichier PDF (*.pdf)|*.pdf";
                    saveDialog.FileName = $"Bulletin_{model.Matricule}_{model.Mois.Replace(" ", "_")}.pdf";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {

                        var document = new BulletinDocument(model);
                        document.GeneratePdf(saveDialog.FileName);

                        // Ouvrir le PDF directement
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = saveDialog.FileName,
                            UseShellExecute = true
                        });

                        MessageBox.Show("Bulletin généré avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue :\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        //*******************Fin Bouton supprimer Entreprise************************

    }

}