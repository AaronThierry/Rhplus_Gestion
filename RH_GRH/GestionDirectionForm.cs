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
    public partial class GestionDirectionForm : Form
    {
        EntrepriseClass Entreprise = new EntrepriseClass();
        DirectionClass Direction = new DirectionClass();
        Dbconnect connect = new Dbconnect();

        public GestionDirectionForm()
        {
            InitializeComponent();
            StyliserDataGridViewGestion();
            ShowTableDirectionGestion();

            // Attacher les événements pour les boutons personnalisés
            DataGridView_Direction_Gestion.CellPainting += DataGridView_Direction_Gestion_CellPainting;
            DataGridView_Direction_Gestion.CellClick += DataGridView_Direction_Gestion_CellClick;
            DataGridView_Direction_Gestion.CellMouseMove += DataGridView_Direction_Gestion_CellMouseMove;

            // Recherche en temps réel
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;

            // Styliser le header avec un design moderne
            StyliserHeader();
        }

        private void StyliserHeader()
        {
            // Configurer le panel2 (header)
            panel2.Height = 70;
            panel2.Paint += (s, e) =>
            {
                // Dégradé corporate élégant - Gris anthracite professionnel
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    panel2.ClientRectangle,
                    Color.FromArgb(52, 58, 64),    // Gris anthracite
                    Color.FromArgb(73, 80, 87),    // Gris moderne
                    System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, panel2.ClientRectangle);
                }

                // Barre accent subtile
                using (var accentBrush = new SolidBrush(Color.FromArgb(108, 117, 125)))
                {
                    e.Graphics.FillRectangle(accentBrush, 0, panel2.Height - 3, panel2.Width, 3);
                }
            };

            // Masquer label1 (ancien label)
            if (label1 != null)
                label1.Visible = false;

            // Styliser label2 (titre principal)
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Montserrat", 15F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Text = "DIRECTIONS";
            label2.Padding = new Padding(70, 0, 0, 0);  // Espace pour l'icône
            label2.TextAlign = ContentAlignment.MiddleLeft;
            label2.Dock = DockStyle.Fill;
            label2.AutoSize = false;

            // Dessiner une icône personnalisée
            label2.Paint += (s, e) =>
            {
                // Dessiner l'icône flèche directionnelle
                int iconSize = 28;
                int iconX = 30;
                int iconY = (label2.Height - iconSize) / 2;

                using (var brush = new SolidBrush(Color.FromArgb(108, 117, 125)))
                using (var pen = new Pen(Color.FromArgb(108, 117, 125), 2.5f))
                {
                    // Flèche vers la droite
                    Point[] arrow = new Point[]
                    {
                        new Point(iconX, iconY + 8),
                        new Point(iconX + 15, iconY + 8),
                        new Point(iconX + 15, iconY + 2),
                        new Point(iconX + 26, iconY + iconSize / 2),
                        new Point(iconX + 15, iconY + iconSize - 2),
                        new Point(iconX + 15, iconY + iconSize - 8),
                        new Point(iconX, iconY + iconSize - 8)
                    };
                    e.Graphics.FillPolygon(brush, arrow);
                }
            };

            panel2.Invalidate();
        }

        // Variables pour suivre le bouton survolé
        private int hoverRowIndex = -1;
        private string hoverButton = "";



        ///////////////////////////////////////////////////////////////////////////
        private void tabControlDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Le TabControl n'a plus qu'un seul onglet (Gestion)
            ChargerTablePage2();
            StyliserDataGridViewGestion();
            ShowTableDirectionGestion();
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
                DataTable dt = Direction.GetDirectionList() ?? new DataTable();

                // 2) Tri par défaut si dispo
                if (dt.Columns.Contains("Entreprise") && dt.Columns.Contains("Direction"))
                {
                    var dv = dt.DefaultView;
                    dv.Sort = "[Entreprise] ASC, [Direction] ASC";
                    dt = dv.ToTable();
                }

                // 3) Binding
                var grid = DataGridView_Direction_Gestion;
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



        ////////////////////////////////////////////////////////////////////////////
        private void ClearDirectionFormGestion()
        {
            // Plus de champs à nettoyer
        }

        ///////////////////////////////////////////////////////////////////////////
        private void ShowTableDirectionGestion()
        {
            var dt = Direction.GetDirectionList();
            DataGridView_Direction_Gestion.AutoGenerateColumns = true;
            DataGridView_Direction_Gestion.DataSource = dt;

            // Masquer les IDs si tu veux une vue clean
            if (DataGridView_Direction_Gestion.Columns.Contains("ID"))
                DataGridView_Direction_Gestion.Columns["ID"].Visible = false;
            if (DataGridView_Direction_Gestion.Columns.Contains("ID_Entreprise"))
                DataGridView_Direction_Gestion.Columns["ID_Entreprise"].Visible = false;

            // Supprimer la colonne d'actions existante pour éviter les doublons
            if (DataGridView_Direction_Gestion.Columns.Contains("Actions"))
                DataGridView_Direction_Gestion.Columns.Remove("Actions");

            // Ajouter colonne Actions (texte simple, rendu personnalisé)
            DataGridViewTextBoxColumn colActions = new DataGridViewTextBoxColumn();
            colActions.Name = "Actions";
            colActions.HeaderText = "Actions";
            colActions.Width = 180;
            colActions.ReadOnly = true;
            colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridView_Direction_Gestion.Columns.Add(colActions);
        }

        ////////////////////////////////////////////////////////////////////////////

        private void StyliserDataGridViewGestion()
        {
            // Améliorer le rendu (anti-scintillement)
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, DataGridView_Direction_Gestion, new object[] { true });

            // Fond général
            DataGridView_Direction_Gestion.BackgroundColor = Color.White;

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

            DataGridView_Direction_Gestion.EnableHeadersVisualStyles = false;
            DataGridView_Direction_Gestion.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridView_Direction_Gestion.ColumnHeadersHeight = 40;

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
            DataGridView_Direction_Gestion.DefaultCellStyle = cellStyle;

            // Hauteur des lignes
            DataGridView_Direction_Gestion.RowTemplate.Height = 45;

            // Lignes alternées
            DataGridView_Direction_Gestion.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            // Bordures & style
            DataGridView_Direction_Gestion.BorderStyle = BorderStyle.None;
            DataGridView_Direction_Gestion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Direction_Gestion.GridColor = Color.LightGray;
            DataGridView_Direction_Gestion.RowHeadersVisible = false;

            // Sélection
            DataGridView_Direction_Gestion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Direction_Gestion.MultiSelect = false;
            DataGridView_Direction_Gestion.AllowUserToResizeRows = false;
        }

        ////////////////////////////////////////////////////////////////////////////

        private void GestionDirectionForm_Load(object sender, EventArgs e)
        {
            tabControlDirection.SelectedIndexChanged += tabControlDirection_SelectedIndexChanged;
        }

        /////////////////////////////////////////////////////////////////////////////

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            // Recherche en temps réel
            string searchText = textBoxSearch.Text?.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                // Si la recherche est vide, afficher toutes les directions
                ShowTableDirectionGestion();
            }
            else
            {
                // Effectuer la recherche
                DataGridView_Direction_Gestion.DataSource = Direction.RechercheDirectionConcat(searchText);

                // Re-masquer les colonnes ID après la recherche
                if (DataGridView_Direction_Gestion.Columns.Contains("ID"))
                    DataGridView_Direction_Gestion.Columns["ID"].Visible = false;
                if (DataGridView_Direction_Gestion.Columns.Contains("ID_Entreprise"))
                    DataGridView_Direction_Gestion.Columns["ID_Entreprise"].Visible = false;

                // Supprimer et recréer la colonne Actions
                if (DataGridView_Direction_Gestion.Columns.Contains("Actions"))
                    DataGridView_Direction_Gestion.Columns.Remove("Actions");

                DataGridViewTextBoxColumn colActions = new DataGridViewTextBoxColumn();
                colActions.Name = "Actions";
                colActions.HeaderText = "Actions";
                colActions.Width = 180;
                colActions.ReadOnly = true;
                colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataGridView_Direction_Gestion.Columns.Add(colActions);
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        private void DataGridView_Direction_Gestion_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            // Vérifier si c'est la colonne Actions
            if (DataGridView_Direction_Gestion.Columns[e.ColumnIndex].Name == "Actions")
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

        private void DataGridView_Direction_Gestion_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (DataGridView_Direction_Gestion.Columns[e.ColumnIndex].Name == "Actions")
            {
                var cellBounds = DataGridView_Direction_Gestion.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                int buttonWidth = 75;
                int buttonHeight = 32;
                int spacing = 10;
                int totalWidth = (buttonWidth * 2) + spacing;
                int startX = cellBounds.X + (cellBounds.Width - totalWidth) / 2;
                int startY = cellBounds.Y + (cellBounds.Height - buttonHeight) / 2;

                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                Rectangle btnSupprimer = new Rectangle(startX + buttonWidth + spacing, startY, buttonWidth, buttonHeight);

                Point mousePos = DataGridView_Direction_Gestion.PointToClient(Cursor.Position);

                string newHoverButton = "";
                if (btnModifier.Contains(mousePos.X, mousePos.Y))
                    newHoverButton = "Modifier";
                else if (btnSupprimer.Contains(mousePos.X, mousePos.Y))
                    newHoverButton = "Supprimer";

                if (hoverRowIndex != e.RowIndex || hoverButton != newHoverButton)
                {
                    hoverRowIndex = e.RowIndex;
                    hoverButton = newHoverButton;
                    DataGridView_Direction_Gestion.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    DataGridView_Direction_Gestion.Cursor = string.IsNullOrEmpty(newHoverButton) ? Cursors.Default : Cursors.Hand;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        private void DataGridView_Direction_Gestion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (DataGridView_Direction_Gestion.Columns[e.ColumnIndex].Name == "Actions")
            {
                var grid = DataGridView_Direction_Gestion;

                // Récupérer les données
                var idCell = grid.Rows[e.RowIndex].Cells["ID"];
                if (idCell.Value == null || !int.TryParse(idCell.Value.ToString(), out int idDirection))
                {
                    CustomMessageBox.Show("Impossible de récupérer l'ID de la direction.", "Erreur",
                                    CustomMessageBox.MessageType.Error);
                    return;
                }

                string nomDirection = grid.Rows[e.RowIndex].Cells["Direction"]?.Value?.ToString() ?? "";

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
                    ModifierDirectionAvecModale(idDirection, nomDirection, idEntreprise);
                }
                else if (btnSupprimer.Contains(mousePos))
                {
                    SupprimerDirection(idDirection, nomDirection);
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        private void ModifierDirectionAvecModale(int idDirection, string nomActuel, int idEntreprise)
        {
            using (var formModifier = new ModifierDirectionForm(idDirection, nomActuel, idEntreprise))
            {
                var result = formModifier.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    // Rafraîchir la table après la modification
                    ShowTableDirectionGestion();
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        private void SupprimerDirection(int idDirection, string nomDirection)
        {
            // Confirmation
            var confirm = CustomMessageBox.Show(
                $"Êtes-vous sûr de vouloir supprimer la direction « {nomDirection} » ?\n\nCette action est irréversible.",
                "Confirmation de suppression",
                CustomMessageBox.MessageType.Question,
                CustomMessageBox.MessageButtons.YesNo);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                UseWaitCursor = true;
                if (DirectionClass.SupprimerDirection(idDirection))
                {
                    ShowTableDirectionGestion();
                    ClearDirectionFormGestion();
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
        /// Ouvre la modale pour ajouter une nouvelle direction
        /// </summary>
        public void AfficherModaleAjouterDirection()
        {
            using (var formAjouter = new AjouterDirectionForm())
            {
                var result = formAjouter.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    // Rafraîchir les tables après l'ajout
                    ShowTableDirectionGestion();
                }
            }
        }

        private void buttonAjouterDirection_Click(object sender, EventArgs e)
        {
            AfficherModaleAjouterDirection();
        }

        private void DataGridView_Direction_Gestion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
    }
}
