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
using static Mysqlx.Crud.Order.Types;

namespace RH_GRH
{
    public partial class GestionServiceForm : Form
    {
        EntrepriseClass Entreprise = new EntrepriseClass();
        ServiceClass Service = new ServiceClass();
        Dbconnect connect = new Dbconnect();

        public GestionServiceForm()
        {
            InitializeComponent();
            StyliserDataGridView();
            StyliserDataGridViewGestion();
            StyliserTabControl();
            ShowTableService();
            ShowTableServiceGestion();
        }



        ///////////////////////////////////////////////////////////////////////////
        private void tabControlService_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlService.SelectedIndex == 1) // Onglet n°2 (index 1)
            {
                ChargerTablePage2();
                EntrepriseClass.ChargerEntreprises(ComboBoxEntrepriseGestion);
                StyliserDataGridViewGestion();
                ShowTableServiceGestion();
                ClearServiceForm();
            }
            else if (tabControlService.SelectedIndex == 0)
            {
                ClearServiceForm();
                ShowTableService();
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
                DataTable dt = Service.GetServiceList() ?? new DataTable();

                // 2) Tri par défaut si dispo
                if (dt.Columns.Contains("Entreprise") && dt.Columns.Contains("Direction"))
                {
                    var dv = dt.DefaultView;
                    dv.Sort = "[Entreprise] ASC, [Direction] ASC";
                    dt = dv.ToTable();
                }

                // 3) Binding
                var grid = DataGridView_Service_Gestion;
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

        private void ClearServiceForm()
        {
            textBoxNomService.Clear();
            ComboBoxEntreprise.SelectedIndex = 0;
        }

        ////////////////////////////////////////////////////////////////////////////
        private void ClearServiceFormGestion()
        {
            textBoxServiceGestion.Clear();
            ComboBoxEntrepriseGestion.SelectedIndex = 0;
            textBoxID.Clear();
        }

        ////////////////////////////////////////////////////////////////////////////

        private void ShowTableService()
        {
            var dt = Service.GetServiceList();
            DataGridView_Service.AutoGenerateColumns = true;
            DataGridView_Service.DataSource = dt;
            // Masquer les IDs si tu veux une vue clean
            if (DataGridView_Service.Columns.Contains("ID"))
                DataGridView_Service.Columns["ID"].Visible = false;
        }

        ///////////////////////////////////////////////////////////////////////////
        private void ShowTableServiceGestion()
        {
            EntrepriseClass.ChargerEntreprises(ComboBoxEntreprise);
            var dt = Service.GetServiceList();
            DataGridView_Service_Gestion.AutoGenerateColumns = true;
            DataGridView_Service_Gestion.DataSource = dt;
            // Masquer les IDs si tu veux une vue clean
            if (DataGridView_Service_Gestion.Columns.Contains("ID"))
                DataGridView_Service.Columns["ID"].Visible = false;
        }

        ///////////////////////////////////////////////////////////////////////////
        private void StyliserDataGridView()
        {
            // Fond général de la grille
            DataGridView_Service.BackgroundColor = Color.White;

            // Désactiver le style visuel Windows
            DataGridView_Service.EnableHeadersVisualStyles = false;

            // Style de l'en-tête
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.MidnightBlue;
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Montserrat", 10f, FontStyle.Bold);
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            headerStyle.WrapMode = DataGridViewTriState.True;
            headerStyle.SelectionBackColor = Color.MidnightBlue;     // ← Empêche le changement au clic
            headerStyle.SelectionForeColor = Color.White;            // ← Texte toujours blanc

            DataGridView_Service.EnableHeadersVisualStyles = false;
            DataGridView_Service.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridView_Service.ColumnHeadersHeight = 35;

            // Style des cellules normales
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.BackColor = Color.White;
            cellStyle.ForeColor = Color.Black;
            cellStyle.Font = new Font("Montserrat", 9.5f, FontStyle.Regular);
            cellStyle.SelectionBackColor = Color.LightSteelBlue;
            cellStyle.SelectionForeColor = Color.Black;
            DataGridView_Service.DefaultCellStyle = cellStyle;

            // Style des lignes alternées
            DataGridView_Service.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;

            // Supprimer les bordures
            DataGridView_Service.BorderStyle = BorderStyle.None;
            DataGridView_Service.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Service.GridColor = Color.LightGray;

            // Masquer l'entête de ligne à gauche
            DataGridView_Service.RowHeadersVisible = false;

            // Autres options d’esthétique
            DataGridView_Service.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Service.MultiSelect = false;
        }

        ////////////////////////////////////////////////////////////////////////////
        
        private void StyliserDataGridViewGestion()
        {
            // Améliorer le rendu (anti-scintillement)
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, DataGridView_Service_Gestion, new object[] { true });

            // Fond général
            DataGridView_Service_Gestion.BackgroundColor = Color.White;

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

            DataGridView_Service_Gestion.EnableHeadersVisualStyles = false;
            DataGridView_Service_Gestion.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridView_Service_Gestion.ColumnHeadersHeight = 35;

            // Cellules normales
            var cellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Montserrat", 9.5f),
                SelectionBackColor = Color.LightSteelBlue,
                SelectionForeColor = Color.Black
            };
            DataGridView_Service_Gestion.DefaultCellStyle = cellStyle;

            // Lignes alternées
            DataGridView_Service_Gestion.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;

            // Bordures & style
            DataGridView_Service_Gestion.BorderStyle = BorderStyle.None;
            DataGridView_Service_Gestion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Service_Gestion.GridColor = Color.LightGray;
            DataGridView_Service_Gestion.RowHeadersVisible = false;

            // Sélection
            DataGridView_Service_Gestion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Service_Gestion.MultiSelect = false;
        }

        ////////////////////////////////////////////////////////////////////////////
        
        private void StyliserTabControl()
        {
            tabControlService.Appearance = TabAppearance.Normal;
            tabControlService.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControlService.ItemSize = new Size(150, 35); // largeur, hauteur des onglets
            tabControlService.SizeMode = TabSizeMode.Fixed;
            tabControlService.DrawItem += TabControlEntreprise_DrawItem;
        }

        ////////////////////////////////////////////////////////////////////////////

        private void TabControlEntreprise_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControlService.TabPages[e.Index];
            Rectangle rect = tabControlService.GetTabRect(e.Index);
            bool isSelected = (e.Index == tabControlService.SelectedIndex);

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

        ////////////////////////////////////////////////////////////////////////////

        private void buttonAjouter_Click(object sender, EventArgs e)
        {
            // 1) Récupération + validations
            string nomService = textBoxNomService.Text?.Trim();

            if (string.IsNullOrWhiteSpace(nomService))
            {
                MessageBox.Show("Veuillez saisir le nom de la direction.", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxNomService.Focus();
                return;
            }

            int? idEntreprise = EntrepriseClass.GetIdEntrepriseSelectionnee(ComboBoxEntreprise);
            if (!idEntreprise.HasValue)
            {
                MessageBox.Show("Veuillez sélectionner une entreprise.", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ComboBoxEntreprise.DroppedDown = true;
                return;
            }

            // 2) Exécution
            try
            {
                Cursor = Cursors.WaitCursor;

                // ⚠️ Adapter le nom de classe si besoin (si ta méthode est dans DirectionClass par ex.)
               ServiceClass.EnregistrerService(nomService, idEntreprise.Value);
                ShowTableService();

                // 3) Reset UI (selon ton besoin, on garde l’entreprise sélectionnée)
                textBoxNomService.Clear();
                ComboBoxEntreprise.Focus();
                ComboBoxEntreprise.SelectedIndex = 0;

                // Si tu veux aussi rafraîchir un DataGridView des directions :
                // dataGridViewDirections.DataSource = DirectionClass.ListerDirectionsParEntreprise(idEntreprise.Value);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur inattendue : " + ex.Message, "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /////////////////////////////////////////////////////////////////////////////

        private void ChargerDetailsServiceParId(string id)
        {
            try
            {
                // Jointure pour récupérer aussi le nom de l'entreprise
                const string sql = @"
            SELECT 
                d.nomService,
                d.id_entreprise,
                e.nomEntreprise
            FROM service d
            INNER JOIN entreprise e ON e.id_entreprise = d.id_entreprise
            WHERE d.id_service = @id;";

                // ⚠️ IMPORTANT :
                // - Si getconnection retourne MySqlConnection, utilisez .ConnectionString :
                using (var con = new MySqlConnection(connect.getconnection.ConnectionString))
                // - Si getconnection retourne une chaîne de connexion, utilisez :
                // using (var con = new MySqlConnection(connect.getconnection))
                using (var cmd = new MySqlCommand(sql, con))
                {
                    // id numérique en base : on paramètre en Int32 (même si on reçoit une string)
                    if (!int.TryParse(id, out int idService))
                    {
                        MessageBox.Show("Identifiant du service invalide.");
                        return;
                    }

                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = idService;

                    con.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                        {
                            // Nom de la direction
                            textBoxServiceGestion.Text = reader["nomService"]?.ToString();

                            // Sélectionner l'entreprise dans le ComboBox (s'il est déjà bindé)
                            if (!reader.IsDBNull(reader.GetOrdinal("id_entreprise")))
                            {
                                int idEnt = reader.GetInt32(reader.GetOrdinal("id_entreprise"));

                                // Si le Combo n'est pas encore chargé, on peut le charger ici (optionnel)
                                if (ComboBoxEntreprise.DataSource == null)
                                {
                                    EntrepriseClass.ChargerEntreprises(ComboBoxEntreprise, idEnt, ajouterPlaceholder: true);
                                }
                                else
                                {
                                    ComboBoxEntrepriseGestion.SelectedValue = idEnt;
                                }
                            }

                            // (Optionnel) Afficher le nom de l'entreprise dans un label si tu en as un
                            // labelEntreprise.Text = reader["nomEntreprise"]?.ToString();
                        }
                        else
                        {
                            // Rien trouvé : on nettoie les champs
                            textBoxServiceGestion.Clear();
                            if (ComboBoxEntreprise.DataSource != null)
                                ComboBoxEntreprise.SelectedIndex = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur chargement infos direction : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        private void GestionServiceForm_Load(object sender, EventArgs e)
        {
            tabControlService.SelectedIndexChanged += tabControlService_SelectedIndexChanged;
        }


        /////////////////////////////////////////////////////////////////////////////

        private void DataGridView_Direction_Gestion_Click(object sender, EventArgs e)
        {
            if (DataGridView_Service_Gestion.CurrentRow != null)
            {
                string id = DataGridView_Service_Gestion.CurrentRow.Cells[0].Value?.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    textBoxID.Text = id;
                    ChargerDetailsServiceParId(id);
                    ComboBoxEntrepriseGestion.Enabled = false;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        private void buttonEffacerGestion_Click(object sender, EventArgs e)
        {
            ClearServiceFormGestion();
        }

        private void button_Supprimer_Click(object sender, EventArgs e)
        {
            // Récupère l'ID (depuis la grille ou un TextBox)
            if (!int.TryParse(textBoxID.Text?.Trim(), out int idDirection) || idDirection <= 0)
            {
                MessageBox.Show("Veuillez sélectionner un service valide.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Confirmation
            var confirm = MessageBox.Show(
                $"Supprimer définitivement le service #{idDirection} ?",
                "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (confirm != DialogResult.Yes) return;

            try
            {
                UseWaitCursor = true;
                button_Supprimer.Enabled = false;

                if (ServiceClass.SupprimerService(idDirection))
                {
                    // Rafraîchir la table après suppression
                    ShowTableServiceGestion();
                    ClearServiceFormGestion();
                }
            }
            finally
            {
                button_Supprimer.Enabled = true;
                UseWaitCursor = false;
            }
        }

        private void buttonModifier_Click(object sender, EventArgs e)
        {
            // 1) Validations rapides
            string nomService = textBoxServiceGestion.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nomService))
            {
                MessageBox.Show("Veuillez saisir le nom du service.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxServiceGestion.Focus();
                return;
            }

            if (!int.TryParse(textBoxID.Text?.Trim(), out int idService) || idService <= 0)
            {
                MessageBox.Show("ID service invalide.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2) Confirmation
            var confirm = MessageBox.Show(
                $"Confirmer la modification du service #{idService} en « {nomService} » ?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (confirm != DialogResult.Yes) return;

            // 3) Exécution + actualisation
            try
            {
                buttonModifier.Enabled = false;
                UseWaitCursor = true;

                ServiceClass.ModifierService(idService, nomService);

                // Rafraîchir la grille
                ShowTableServiceGestion();
                ClearServiceFormGestion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification : " + ex.Message, "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
                buttonModifier.Enabled = true;
            }
        }

        private void buttonRechercher_Click(object sender, EventArgs e)
        {
            DataGridView_Service_Gestion.DataSource = Service.RechercheServiceConcat(textBoxSearch.Text);
        }

        ////////////////////////////////////////////////////////////////////////////
    }
}
