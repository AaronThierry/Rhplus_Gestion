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
            StyliserDataGridViewGestion();
            ShowTableServiceGestion();

            // Attacher les événements pour les boutons personnalisés
            DataGridView_Service_Gestion.CellPainting += DataGridView_Service_Gestion_CellPainting;
            DataGridView_Service_Gestion.CellClick += DataGridView_Service_Gestion_CellClick;
            DataGridView_Service_Gestion.CellMouseMove += DataGridView_Service_Gestion_CellMouseMove;

            // Recherche en temps réel
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
        }

        // Variables pour suivre le bouton survolé
        private int hoverRowIndex = -1;
        private string hoverButton = "";



        ///////////////////////////////////////////////////////////////////////////
        private void tabControlService_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Le TabControl n'a plus qu'un seul onglet (Gestion)
            ChargerTablePage2();
            StyliserDataGridViewGestion();
            ShowTableServiceGestion();
        }

        ////////////////////////////////////////////////////////////////////////////
        
        private bool tablePage2Chargee = false;

        ///////////////////////////////////////////////////////////////////////////
        private void ChargerTablePage2(bool forcer = false)
        {
            if (tablePage2Chargee && !forcer) return;

            try
            {
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
                StyliserDataGridViewGestion();

                tablePage2Chargee = true;
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Erreur de chargement : " + ex.Message, "Erreur",
                                CustomMessageBox.MessageType.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        ///////////////////////////////////////////////////////////////////////////
        // ANCIEN ONGLET - MÉTHODE NON UTILISÉE
        // private void ClearServiceForm()
        // {
        //     textBoxNomService.Clear();
        //     ComboBoxEntreprise.SelectedIndex = 0;
        // }

        ////////////////////////////////////////////////////////////////////////////
        private void ClearServiceFormGestion()
        {
            // Plus de champs à nettoyer
        }

        ////////////////////////////////////////////////////////////////////////////
        // ANCIEN ONGLET - MÉTHODE NON UTILISÉE
        // private void ShowTableService()
        // {
        //     var dt = Service.GetServiceList();
        //     DataGridView_Service.AutoGenerateColumns = true;
        //     DataGridView_Service.DataSource = dt;
        //     // Masquer les IDs si tu veux une vue clean
        //     if (DataGridView_Service.Columns.Contains("ID"))
        //         DataGridView_Service.Columns["ID"].Visible = false;
        // }

        ///////////////////////////////////////////////////////////////////////////
        private void ShowTableServiceGestion()
        {
            var dt = Service.GetServiceList();
            DataGridView_Service_Gestion.AutoGenerateColumns = true;
            DataGridView_Service_Gestion.DataSource = dt;

            // Masquer les IDs si tu veux une vue clean
            if (DataGridView_Service_Gestion.Columns.Contains("ID"))
                DataGridView_Service_Gestion.Columns["ID"].Visible = false;
            if (DataGridView_Service_Gestion.Columns.Contains("ID_Entreprise"))
                DataGridView_Service_Gestion.Columns["ID_Entreprise"].Visible = false;

            // Supprimer la colonne d'actions existante pour éviter les doublons
            if (DataGridView_Service_Gestion.Columns.Contains("Actions"))
                DataGridView_Service_Gestion.Columns.Remove("Actions");

            // Ajouter colonne Actions (texte simple, rendu personnalisé)
            DataGridViewTextBoxColumn colActions = new DataGridViewTextBoxColumn();
            colActions.Name = "Actions";
            colActions.HeaderText = "Actions";
            colActions.Width = 180;
            colActions.ReadOnly = true;
            colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridView_Service_Gestion.Columns.Add(colActions);
        }

        ///////////////////////////////////////////////////////////////////////////
        // ANCIEN ONGLET - MÉTHODE NON UTILISÉE
        // private void StyliserDataGridView()
        // {
        //     // Fond général de la grille
        //     DataGridView_Service.BackgroundColor = Color.White;
        //
        //     // Désactiver le style visuel Windows
        //     DataGridView_Service.EnableHeadersVisualStyles = false;
        //
        //     // Style de l'en-tête
        //     DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
        //     headerStyle.BackColor = Color.MidnightBlue;
        //     headerStyle.ForeColor = Color.White;
        //     headerStyle.Font = new Font("Montserrat", 10f, FontStyle.Bold);
        //     headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //     headerStyle.WrapMode = DataGridViewTriState.True;
        //     headerStyle.SelectionBackColor = Color.MidnightBlue;     // ← Empêche le changement au clic
        //     headerStyle.SelectionForeColor = Color.White;            // ← Texte toujours blanc
        //
        //     DataGridView_Service.EnableHeadersVisualStyles = false;
        //     DataGridView_Service.ColumnHeadersDefaultCellStyle = headerStyle;
        //     DataGridView_Service.ColumnHeadersHeight = 35;
        //
        //     // Style des cellules normales
        //     DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
        //     cellStyle.BackColor = Color.White;
        //     cellStyle.ForeColor = Color.Black;
        //     cellStyle.Font = new Font("Montserrat", 9.5f, FontStyle.Regular);
        //     cellStyle.SelectionBackColor = Color.LightSteelBlue;
        //     cellStyle.SelectionForeColor = Color.Black;
        //     DataGridView_Service.DefaultCellStyle = cellStyle;
        //
        //     // Style des lignes alternées
        //     DataGridView_Service.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;
        //
        //     // Supprimer les bordures
        //     DataGridView_Service.BorderStyle = BorderStyle.None;
        //     DataGridView_Service.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        //     DataGridView_Service.GridColor = Color.LightGray;
        //
        //     // Masquer l'entête de ligne à gauche
        //     DataGridView_Service.RowHeadersVisible = false;
        //
        //     // Autres options d'esthétique
        //     DataGridView_Service.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //     DataGridView_Service.MultiSelect = false;
        // }

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
            DataGridView_Service_Gestion.ColumnHeadersHeight = 40;

            // Cellules normales
            var cellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Montserrat", 10f),
                SelectionBackColor = Color.LightSteelBlue,
                SelectionForeColor = Color.Black,
                Padding = new Padding(5) // Ajouter du padding
            };
            DataGridView_Service_Gestion.DefaultCellStyle = cellStyle;

            // Hauteur des lignes
            DataGridView_Service_Gestion.RowTemplate.Height = 45;

            // Lignes alternées
            DataGridView_Service_Gestion.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            // Bordures & style
            DataGridView_Service_Gestion.BorderStyle = BorderStyle.None;
            DataGridView_Service_Gestion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Service_Gestion.GridColor = Color.LightGray;
            DataGridView_Service_Gestion.RowHeadersVisible = false;

            // Sélection
            DataGridView_Service_Gestion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Service_Gestion.MultiSelect = false;
            DataGridView_Service_Gestion.AllowUserToResizeRows = false;
        }

        ////////////////////////////////////////////////////////////////////////////
        // MÉTHODES NON UTILISÉES - TabControl sans header
        // private void StyliserTabControl()
        // {
        //     tabControlService.Appearance = TabAppearance.Normal;
        //     tabControlService.DrawMode = TabDrawMode.OwnerDrawFixed;
        //     tabControlService.ItemSize = new Size(150, 35); // largeur, hauteur des onglets
        //     tabControlService.SizeMode = TabSizeMode.Fixed;
        //     tabControlService.DrawItem += TabControlEntreprise_DrawItem;
        // }

        ////////////////////////////////////////////////////////////////////////////

        // private void TabControlEntreprise_DrawItem(object sender, DrawItemEventArgs e)
        // {
        //     TabPage page = tabControlService.TabPages[e.Index];
        //     Rectangle rect = tabControlService.GetTabRect(e.Index);
        //     bool isSelected = (e.Index == tabControlService.SelectedIndex);
        //
        //     // Couleur de fond
        //     Color backColor = isSelected ? Color.MidnightBlue : Color.LightGray;
        //     Color foreColor = isSelected ? Color.White : Color.Black;
        //
        //     using (SolidBrush brush = new SolidBrush(backColor))
        //     {
        //         e.Graphics.FillRectangle(brush, rect);
        //     }
        //
        //     // Texte centré
        //     StringFormat format = new StringFormat();
        //     format.Alignment = StringAlignment.Center;
        //     format.LineAlignment = StringAlignment.Center;
        //
        //     using (Font font = new Font("Montserrat", 10f, FontStyle.Bold))
        //     using (Brush textBrush = new SolidBrush(foreColor))
        //     {
        //         e.Graphics.DrawString(page.Text, font, textBrush, rect, format);
        //     }
        // }

        ////////////////////////////////////////////////////////////////////////////
        // ANCIEN ONGLET - MÉTHODE NON UTILISÉE
        // private void buttonAjouter_Click(object sender, EventArgs e)
        // {
        //     // 1) Récupération + validations
        //     string nomService = textBoxNomService.Text?.Trim();
        //
        //     if (string.IsNullOrWhiteSpace(nomService))
        //     {
        //         MessageBox.Show("Veuillez saisir le nom de la direction.", "Information",
        //                         MessageBoxButtons.OK, MessageBoxIcon.Information);
        //         textBoxNomService.Focus();
        //         return;
        //     }
        //
        //     int? idEntreprise = EntrepriseClass.GetIdEntrepriseSelectionnee(ComboBoxEntreprise);
        //     if (!idEntreprise.HasValue)
        //     {
        //         MessageBox.Show("Veuillez sélectionner une entreprise.", "Information",
        //                         MessageBoxButtons.OK, MessageBoxIcon.Information);
        //         ComboBoxEntreprise.DroppedDown = true;
        //         return;
        //     }
        //
        //     // 2) Exécution
        //     try
        //     {
        //         Cursor = Cursors.WaitCursor;
        //
        //         // ⚠️ Adapter le nom de classe si besoin (si ta méthode est dans DirectionClass par ex.)
        //        ServiceClass.EnregistrerService(nomService, idEntreprise.Value);
        //         ShowTableService();
        //
        //         // 3) Reset UI (selon ton besoin, on garde l'entreprise sélectionnée)
        //         textBoxNomService.Clear();
        //         ComboBoxEntreprise.Focus();
        //         ComboBoxEntreprise.SelectedIndex = 0;
        //
        //         // Si tu veux aussi rafraîchir un DataGridView des directions :
        //         // dataGridViewDirections.DataSource = DirectionClass.ListerDirectionsParEntreprise(idEntreprise.Value);
        //
        //     }
        //     catch (Exception ex)
        //     {
        //         MessageBox.Show("Erreur inattendue : " + ex.Message, "Erreur",
        //                         MessageBoxButtons.OK, MessageBoxIcon.Error);
        //     }
        //     finally
        //     {
        //         Cursor = Cursors.Default;
        //     }
        // }

        /////////////////////////////////////////////////////////////////////////////
        // MÉTHODE NON UTILISÉE - Plus de champs pour afficher les détails
        // TODO: Créer une modale de modification si nécessaire
        // private void ChargerDetailsServiceParId(string id)
        // {
        //     try
        //     {
        //         // Jointure pour récupérer aussi le nom de l'entreprise
        //         const string sql = @"
        //     SELECT
        //         d.nomService,
        //         d.id_entreprise,
        //         e.nomEntreprise
        //     FROM service d
        //     INNER JOIN entreprise e ON e.id_entreprise = d.id_entreprise
        //     WHERE d.id_service = @id;";
        //
        //         using (var con = new MySqlConnection(connect.getconnection.ConnectionString))
        //         using (var cmd = new MySqlCommand(sql, con))
        //         {
        //             if (!int.TryParse(id, out int idService))
        //             {
        //                 MessageBox.Show("Identifiant du service invalide.");
        //                 return;
        //             }
        //
        //             cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = idService;
        //
        //             con.Open();
        //             using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
        //             {
        //                 if (reader.Read())
        //                 {
        //                     // Afficher les détails dans une modale de modification
        //                 }
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         MessageBox.Show("Erreur chargement infos service : " + ex.Message,
        //                         "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //     }
        // }

        ////////////////////////////////////////////////////////////////////////////

        private void GestionServiceForm_Load(object sender, EventArgs e)
        {
            tabControlService.SelectedIndexChanged += tabControlService_SelectedIndexChanged;
        }


        /////////////////////////////////////////////////////////////////////////////

        // MÉTHODES NON UTILISÉES - Actions maintenant dans le tableau
        // private void DataGridView_Direction_Gestion_Click(object sender, EventArgs e) { }
        // private void buttonEffacerGestion_Click(object sender, EventArgs e) { }
        // private void button_Supprimer_Click(object sender, EventArgs e) { }
        // private void buttonModifier_Click(object sender, EventArgs e) { }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            // Recherche en temps réel
            string searchText = textBoxSearch.Text?.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                // Si la recherche est vide, afficher tous les services
                ShowTableServiceGestion();
            }
            else
            {
                // Effectuer la recherche
                DataGridView_Service_Gestion.DataSource = Service.RechercheServiceConcat(searchText);

                // Re-masquer les colonnes ID après la recherche
                if (DataGridView_Service_Gestion.Columns.Contains("ID"))
                    DataGridView_Service_Gestion.Columns["ID"].Visible = false;
                if (DataGridView_Service_Gestion.Columns.Contains("ID_Entreprise"))
                    DataGridView_Service_Gestion.Columns["ID_Entreprise"].Visible = false;

                // Supprimer et recréer la colonne Actions
                if (DataGridView_Service_Gestion.Columns.Contains("Actions"))
                    DataGridView_Service_Gestion.Columns.Remove("Actions");

                DataGridViewTextBoxColumn colActions = new DataGridViewTextBoxColumn();
                colActions.Name = "Actions";
                colActions.HeaderText = "Actions";
                colActions.Width = 180;
                colActions.ReadOnly = true;
                colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataGridView_Service_Gestion.Columns.Add(colActions);
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        private void DataGridView_Service_Gestion_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            // Vérifier si c'est la colonne Actions
            if (DataGridView_Service_Gestion.Columns[e.ColumnIndex].Name == "Actions")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                // Calculer les dimensions des boutons
                int buttonWidth = 75;
                int buttonHeight = 32;
                int spacing = 10;
                int totalWidth = (buttonWidth * 2) + spacing;
                int startX = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
                int startY = e.CellBounds.Y + (e.CellBounds.Height - buttonHeight) / 2;

                // Bouton Modifier (doré)
                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                bool modifierHover = (hoverRowIndex == e.RowIndex && hoverButton == "Modifier");
                Color modifierColor = modifierHover ? Color.FromArgb(218, 165, 32) : Color.FromArgb(255, 215, 0); // Gold

                using (SolidBrush brush = new SolidBrush(modifierColor))
                using (Pen borderPen = new Pen(Color.FromArgb(184, 134, 11), 1))
                {
                    e.Graphics.FillRoundedRectangle(brush, btnModifier, 6);
                    e.Graphics.DrawRoundedRectangle(borderPen, btnModifier, 6);
                }

                // Icône et texte Modifier
                using (Font iconFont = new Font("Segoe UI Emoji", 10f, FontStyle.Regular))
                using (Font textFont = new Font("Montserrat", 8f, FontStyle.Bold))
                using (SolidBrush iconBrush = new SolidBrush(Color.FromArgb(64, 64, 64)))
                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(64, 64, 64)))
                {
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                    // Dessiner l'icône
                    Rectangle iconRect = new Rectangle(btnModifier.X, btnModifier.Y + 2, btnModifier.Width, btnModifier.Height / 2);
                    e.Graphics.DrawString("✏", iconFont, iconBrush, iconRect, sf);

                    // Dessiner le texte
                    Rectangle textRect = new Rectangle(btnModifier.X, btnModifier.Y + btnModifier.Height / 2 - 2, btnModifier.Width, btnModifier.Height / 2);
                    e.Graphics.DrawString("Modifier", textFont, textBrush, textRect, sf);
                }

                // Bouton Supprimer (rouge)
                Rectangle btnSupprimer = new Rectangle(startX + buttonWidth + spacing, startY, buttonWidth, buttonHeight);
                bool supprimerHover = (hoverRowIndex == e.RowIndex && hoverButton == "Supprimer");
                Color supprimerColor = supprimerHover ? Color.FromArgb(200, 35, 51) : Color.FromArgb(220, 53, 69);

                using (SolidBrush brush = new SolidBrush(supprimerColor))
                using (Pen borderPen = new Pen(Color.FromArgb(180, 30, 45), 1))
                {
                    e.Graphics.FillRoundedRectangle(brush, btnSupprimer, 6);
                    e.Graphics.DrawRoundedRectangle(borderPen, btnSupprimer, 6);
                }

                // Icône et texte Supprimer
                using (Font iconFont = new Font("Segoe UI Emoji", 10f, FontStyle.Regular))
                using (Font textFont = new Font("Montserrat", 8f, FontStyle.Bold))
                using (SolidBrush iconBrush = new SolidBrush(Color.White))
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                    // Dessiner l'icône
                    Rectangle iconRect = new Rectangle(btnSupprimer.X, btnSupprimer.Y + 2, btnSupprimer.Width, btnSupprimer.Height / 2);
                    e.Graphics.DrawString("🗑", iconFont, iconBrush, iconRect, sf);

                    // Dessiner le texte
                    Rectangle textRect = new Rectangle(btnSupprimer.X, btnSupprimer.Y + btnSupprimer.Height / 2 - 2, btnSupprimer.Width, btnSupprimer.Height / 2);
                    e.Graphics.DrawString("Supprimer", textFont, textBrush, textRect, sf);
                }

                e.Handled = true;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        private void DataGridView_Service_Gestion_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (DataGridView_Service_Gestion.Columns[e.ColumnIndex].Name == "Actions")
            {
                var cellBounds = DataGridView_Service_Gestion.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                int buttonWidth = 75;
                int buttonHeight = 32;
                int spacing = 10;
                int totalWidth = (buttonWidth * 2) + spacing;
                int startX = cellBounds.X + (cellBounds.Width - totalWidth) / 2;
                int startY = cellBounds.Y + (cellBounds.Height - buttonHeight) / 2;

                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                Rectangle btnSupprimer = new Rectangle(startX + buttonWidth + spacing, startY, buttonWidth, buttonHeight);

                Point mousePos = DataGridView_Service_Gestion.PointToClient(Cursor.Position);

                string newHoverButton = "";
                if (btnModifier.Contains(mousePos.X, mousePos.Y))
                    newHoverButton = "Modifier";
                else if (btnSupprimer.Contains(mousePos.X, mousePos.Y))
                    newHoverButton = "Supprimer";

                if (hoverRowIndex != e.RowIndex || hoverButton != newHoverButton)
                {
                    hoverRowIndex = e.RowIndex;
                    hoverButton = newHoverButton;
                    DataGridView_Service_Gestion.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    DataGridView_Service_Gestion.Cursor = string.IsNullOrEmpty(newHoverButton) ? Cursors.Default : Cursors.Hand;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        private void DataGridView_Service_Gestion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (DataGridView_Service_Gestion.Columns[e.ColumnIndex].Name == "Actions")
            {
                var grid = DataGridView_Service_Gestion;

                // Récupérer les données
                var idCell = grid.Rows[e.RowIndex].Cells["ID"];
                if (idCell.Value == null || !int.TryParse(idCell.Value.ToString(), out int idService))
                {
                    CustomMessageBox.Show("Impossible de récupérer l'ID du service.", "Erreur",
                                    CustomMessageBox.MessageType.Error);
                    return;
                }

                string nomService = grid.Rows[e.RowIndex].Cells["Service"]?.Value?.ToString() ?? "";

                var idEntrepriseCell = grid.Rows[e.RowIndex].Cells["ID_Entreprise"];
                if (idEntrepriseCell.Value == null || !int.TryParse(idEntrepriseCell.Value.ToString(), out int idEntreprise))
                {
                    CustomMessageBox.Show("Impossible de récupérer l'ID de l'entreprise.", "Erreur",
                                    CustomMessageBox.MessageType.Error);
                    return;
                }

                // Déterminer quel bouton a été cliqué
                Point mousePos = grid.PointToClient(Cursor.Position);
                var cellBounds = grid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                int buttonWidth = 75;
                int buttonHeight = 32;
                int spacing = 10;
                int totalWidth = (buttonWidth * 2) + spacing;
                int startX = cellBounds.X + (cellBounds.Width - totalWidth) / 2;
                int startY = cellBounds.Y + (cellBounds.Height - buttonHeight) / 2;

                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                Rectangle btnSupprimer = new Rectangle(startX + buttonWidth + spacing, startY, buttonWidth, buttonHeight);

                if (btnModifier.Contains(mousePos))
                {
                    ModifierServiceAvecModale(idService, nomService, idEntreprise);
                }
                else if (btnSupprimer.Contains(mousePos))
                {
                    SupprimerService(idService, nomService);
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        private void ModifierServiceAvecModale(int idService, string nomActuel, int idEntreprise)
        {
            using (var formModifier = new ModifierServiceForm(idService, nomActuel, idEntreprise))
            {
                var result = formModifier.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    // Rafraîchir la table après la modification
                    ShowTableServiceGestion();
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        private void SupprimerService(int idService, string nomService)
        {
            // Confirmation
            var confirm = CustomMessageBox.Show(
                $"Êtes-vous sûr de vouloir supprimer le service « {nomService} » ?\n\nCette action est irréversible.",
                "Confirmation de suppression",
                CustomMessageBox.MessageType.Question,
                CustomMessageBox.MessageButtons.YesNo);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                UseWaitCursor = true;
                if (ServiceClass.SupprimerService(idService))
                {
                    ShowTableServiceGestion();
                    ClearServiceFormGestion();
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Erreur lors de la suppression : " + ex.Message, "Erreur",
                                CustomMessageBox.MessageType.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Ouvre la modale pour ajouter un nouveau service
        /// </summary>
        public void AfficherModaleAjouterService()
        {
            using (var formAjouter = new AjouterServiceForm())
            {
                var result = formAjouter.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    // Rafraîchir les tables après l'ajout
                    ShowTableServiceGestion();
                }
            }
        }

        private void buttonAjouterService_Click(object sender, EventArgs e)
        {
            AfficherModaleAjouterService();
        }

        private void DataGridView_Service_Gestion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
    }
}
