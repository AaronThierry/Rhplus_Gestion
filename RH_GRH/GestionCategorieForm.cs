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
    public partial class GestionCategorieForm : Form
    {
        EntrepriseClass Entreprise = new EntrepriseClass();
        Categorie CategorieObj = new Categorie();
        Dbconnect connect = new Dbconnect();

        public GestionCategorieForm()
        {
            InitializeComponent();
            StyliserDataGridViewGestion();
            ShowTableCategorieGestion();

            // Attacher les événements pour les boutons personnalisés
            DataGridView_Categorie_Gestion.CellPainting += DataGridView_Categorie_Gestion_CellPainting;
            DataGridView_Categorie_Gestion.CellClick += DataGridView_Categorie_Gestion_CellClick;
            DataGridView_Categorie_Gestion.CellMouseMove += DataGridView_Categorie_Gestion_CellMouseMove;

            // Recherche en temps réel
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;

            // Styliser le header avec un design moderne
            StyliserHeader();
        }

        private void StyliserHeader()
        {
            // Configurer le panel1 (header)
            panel1.Height = 70;
            panel1.Paint += (s, e) =>
            {
                // Dégradé corporate élégant - Violet/prune professionnel
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    panel1.ClientRectangle,
                    Color.FromArgb(71, 50, 93),    // Prune profond
                    Color.FromArgb(96, 70, 122),   // Violet corporate
                    System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, panel1.ClientRectangle);
                }

                // Barre accent subtile
                using (var accentBrush = new SolidBrush(Color.FromArgb(123, 97, 155)))
                {
                    e.Graphics.FillRectangle(accentBrush, 0, panel1.Height - 3, panel1.Width, 3);
                }
            };

            // Styliser label1 (titre principal)
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Montserrat", 15F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Text = "CATÉGORIES DE SALAIRE";
            label1.Padding = new Padding(70, 0, 0, 0);  // Espace pour l'icône
            label1.TextAlign = ContentAlignment.MiddleLeft;
            label1.Dock = DockStyle.Fill;
            label1.AutoSize = false;

            // Dessiner une icône personnalisée
            label1.Paint += (s, e) =>
            {
                // Dessiner l'icône grille (4 carrés)
                int iconSize = 28;
                int iconX = 30;
                int iconY = (label1.Height - iconSize) / 2;
                int cellSize = 12;
                int gap = 4;

                using (var brush = new SolidBrush(Color.FromArgb(123, 97, 155)))
                {
                    // 4 carrés en grille 2x2
                    e.Graphics.FillRectangle(brush, iconX, iconY, cellSize, cellSize);
                    e.Graphics.FillRectangle(brush, iconX + cellSize + gap, iconY, cellSize, cellSize);
                    e.Graphics.FillRectangle(brush, iconX, iconY + cellSize + gap, cellSize, cellSize);
                    e.Graphics.FillRectangle(brush, iconX + cellSize + gap, iconY + cellSize + gap, cellSize, cellSize);
                }
            };

            panel1.Invalidate();
        }

        // Variables pour suivre le bouton survolé
        private int hoverRowIndex = -1;
        private string hoverButton = "";

        ///////////////////////////////////////////////////////////////////////////
        private void tabControlCategorie_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChargerTablePage2();
            StyliserDataGridViewGestion();
            ShowTableCategorieGestion();
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
                DataTable dt = CategorieObj.GetCategorieList() ?? new DataTable();

                // 2) Tri par défaut si dispo
                if (dt.Columns.Contains("Entreprise") && dt.Columns.Contains("Categorie"))
                {
                    var dv = dt.DefaultView;
                    dv.Sort = "[Entreprise] ASC, [Categorie] ASC";
                    dt = dv.ToTable();
                }

                // 3) Binding
                var grid = DataGridView_Categorie_Gestion;
                grid.AutoGenerateColumns = true;
                grid.DataSource = dt;

                // Masquer les IDs
                if (grid.Columns.Contains("ID")) grid.Columns["ID"].Visible = false;
                if (grid.Columns.Contains("ID_Entreprise")) grid.Columns["ID_Entreprise"].Visible = false;

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
        private void ClearCategorieFormGestion()
        {
            // Plus de champs à nettoyer
        }

        ///////////////////////////////////////////////////////////////////////////
        private void ShowTableCategorieGestion()
        {
            var dt = CategorieObj.GetCategorieList();
            DataGridView_Categorie_Gestion.AutoGenerateColumns = true;
            DataGridView_Categorie_Gestion.DataSource = dt;

            // Masquer les IDs
            if (DataGridView_Categorie_Gestion.Columns.Contains("ID"))
                DataGridView_Categorie_Gestion.Columns["ID"].Visible = false;
            if (DataGridView_Categorie_Gestion.Columns.Contains("ID_Entreprise"))
                DataGridView_Categorie_Gestion.Columns["ID_Entreprise"].Visible = false;

            // Supprimer la colonne d'actions existante pour éviter les doublons
            if (DataGridView_Categorie_Gestion.Columns.Contains("Actions"))
                DataGridView_Categorie_Gestion.Columns.Remove("Actions");

            // Ajouter colonne Actions (texte simple, rendu personnalisé)
            DataGridViewTextBoxColumn colActions = new DataGridViewTextBoxColumn();
            colActions.Name = "Actions";
            colActions.HeaderText = "Actions";
            colActions.Width = 180;
            colActions.ReadOnly = true;
            colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridView_Categorie_Gestion.Columns.Add(colActions);
        }

        ////////////////////////////////////////////////////////////////////////////

        private void StyliserDataGridViewGestion()
        {
            // Améliorer le rendu (anti-scintillement)
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, DataGridView_Categorie_Gestion, new object[] { true });

            // Fond général
            DataGridView_Categorie_Gestion.BackgroundColor = Color.White;

            // En-tête (header)
            var headerStyle = new DataGridViewCellStyle
            {
                BackColor = Color.MidnightBlue,
                ForeColor = Color.White,
                Font = new Font("Montserrat", 10f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                WrapMode = DataGridViewTriState.True,
                SelectionBackColor = Color.MidnightBlue,
                SelectionForeColor = Color.White
            };

            DataGridView_Categorie_Gestion.EnableHeadersVisualStyles = false;
            DataGridView_Categorie_Gestion.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridView_Categorie_Gestion.ColumnHeadersHeight = 40;

            // Cellules normales
            var cellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Montserrat", 10f),
                SelectionBackColor = Color.LightSteelBlue,
                SelectionForeColor = Color.Black,
                Padding = new Padding(5)
            };
            DataGridView_Categorie_Gestion.DefaultCellStyle = cellStyle;

            // Hauteur des lignes
            DataGridView_Categorie_Gestion.RowTemplate.Height = 45;

            // Lignes alternées
            DataGridView_Categorie_Gestion.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            // Bordures & style
            DataGridView_Categorie_Gestion.BorderStyle = BorderStyle.None;
            DataGridView_Categorie_Gestion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Categorie_Gestion.GridColor = Color.LightGray;
            DataGridView_Categorie_Gestion.RowHeadersVisible = false;

            // Sélection
            DataGridView_Categorie_Gestion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Categorie_Gestion.MultiSelect = false;
            DataGridView_Categorie_Gestion.AllowUserToResizeRows = false;
        }

        ////////////////////////////////////////////////////////////////////////////

        private void GestionCategorieForm_Load(object sender, EventArgs e)
        {
            tabControlCategorie.SelectedIndexChanged += tabControlCategorie_SelectedIndexChanged;
        }

        /////////////////////////////////////////////////////////////////////////////

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            // Recherche en temps réel
            string searchText = textBoxSearch.Text?.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                // Si la recherche est vide, afficher toutes les catégories
                ShowTableCategorieGestion();
            }
            else
            {
                // Effectuer la recherche
                DataGridView_Categorie_Gestion.DataSource = CategorieObj.RechercheCategorieConcat(searchText);

                // Re-masquer les colonnes ID après la recherche
                if (DataGridView_Categorie_Gestion.Columns.Contains("ID"))
                    DataGridView_Categorie_Gestion.Columns["ID"].Visible = false;
                if (DataGridView_Categorie_Gestion.Columns.Contains("ID_Entreprise"))
                    DataGridView_Categorie_Gestion.Columns["ID_Entreprise"].Visible = false;

                // Supprimer et recréer la colonne Actions
                if (DataGridView_Categorie_Gestion.Columns.Contains("Actions"))
                    DataGridView_Categorie_Gestion.Columns.Remove("Actions");

                DataGridViewTextBoxColumn colActions = new DataGridViewTextBoxColumn();
                colActions.Name = "Actions";
                colActions.HeaderText = "Actions";
                colActions.Width = 180;
                colActions.ReadOnly = true;
                colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataGridView_Categorie_Gestion.Columns.Add(colActions);
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        private void DataGridView_Categorie_Gestion_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            // Vérifier si c'est la colonne Actions
            if (DataGridView_Categorie_Gestion.Columns[e.ColumnIndex].Name == "Actions")
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
                Color modifierColor = modifierHover ? Color.FromArgb(218, 165, 32) : Color.FromArgb(255, 215, 0);

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

                    Rectangle iconRect = new Rectangle(btnModifier.X, btnModifier.Y + 2, btnModifier.Width, btnModifier.Height / 2);
                    e.Graphics.DrawString("✏", iconFont, iconBrush, iconRect, sf);

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

                    Rectangle iconRect = new Rectangle(btnSupprimer.X, btnSupprimer.Y + 2, btnSupprimer.Width, btnSupprimer.Height / 2);
                    e.Graphics.DrawString("🗑", iconFont, iconBrush, iconRect, sf);

                    Rectangle textRect = new Rectangle(btnSupprimer.X, btnSupprimer.Y + btnSupprimer.Height / 2 - 2, btnSupprimer.Width, btnSupprimer.Height / 2);
                    e.Graphics.DrawString("Supprimer", textFont, textBrush, textRect, sf);
                }

                e.Handled = true;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        private void DataGridView_Categorie_Gestion_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (DataGridView_Categorie_Gestion.Columns[e.ColumnIndex].Name == "Actions")
            {
                var cellBounds = DataGridView_Categorie_Gestion.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                int buttonWidth = 75;
                int buttonHeight = 32;
                int spacing = 10;
                int totalWidth = (buttonWidth * 2) + spacing;
                int startX = cellBounds.X + (cellBounds.Width - totalWidth) / 2;
                int startY = cellBounds.Y + (cellBounds.Height - buttonHeight) / 2;

                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                Rectangle btnSupprimer = new Rectangle(startX + buttonWidth + spacing, startY, buttonWidth, buttonHeight);

                Point mousePos = DataGridView_Categorie_Gestion.PointToClient(Cursor.Position);

                string newHoverButton = "";
                if (btnModifier.Contains(mousePos.X, mousePos.Y))
                    newHoverButton = "Modifier";
                else if (btnSupprimer.Contains(mousePos.X, mousePos.Y))
                    newHoverButton = "Supprimer";

                if (hoverRowIndex != e.RowIndex || hoverButton != newHoverButton)
                {
                    hoverRowIndex = e.RowIndex;
                    hoverButton = newHoverButton;
                    DataGridView_Categorie_Gestion.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    DataGridView_Categorie_Gestion.Cursor = string.IsNullOrEmpty(newHoverButton) ? Cursors.Default : Cursors.Hand;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        private void DataGridView_Categorie_Gestion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (DataGridView_Categorie_Gestion.Columns[e.ColumnIndex].Name == "Actions")
            {
                var grid = DataGridView_Categorie_Gestion;

                // Récupérer les données
                var idCell = grid.Rows[e.RowIndex].Cells["ID"];
                if (idCell.Value == null || !int.TryParse(idCell.Value.ToString(), out int idCategorie))
                {
                    CustomMessageBox.Show("Impossible de récupérer l'ID de la catégorie.", "Erreur",
                                    CustomMessageBox.MessageType.Error);
                    return;
                }

                string nomCategorie = grid.Rows[e.RowIndex].Cells["Categorie"]?.Value?.ToString() ?? "";

                var montantCell = grid.Rows[e.RowIndex].Cells["Montant"];
                if (montantCell.Value == null || !decimal.TryParse(montantCell.Value.ToString(), out decimal montant))
                {
                    CustomMessageBox.Show("Impossible de récupérer le montant de la catégorie.", "Erreur",
                                    CustomMessageBox.MessageType.Error);
                    return;
                }

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
                    ModifierCategorieAvecModale(idCategorie, nomCategorie, montant, idEntreprise);
                }
                else if (btnSupprimer.Contains(mousePos))
                {
                    SupprimerCategorie(idCategorie, nomCategorie);
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        private void ModifierCategorieAvecModale(int idCategorie, string nomActuel, decimal montantActuel, int idEntreprise)
        {
            using (var formModifier = new ModifierCategorieForm(idCategorie, nomActuel, montantActuel, idEntreprise))
            {
                var result = formModifier.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    // Rafraîchir la table après la modification
                    ShowTableCategorieGestion();
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        private void SupprimerCategorie(int idCategorie, string nomCategorie)
        {
            // Confirmation
            var confirm = CustomMessageBox.Show(
                $"Êtes-vous sûr de vouloir supprimer la catégorie « {nomCategorie} » ?\n\nCette action est irréversible.",
                "Confirmation de suppression",
                CustomMessageBox.MessageType.Question,
                CustomMessageBox.MessageButtons.YesNo);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                UseWaitCursor = true;
                if (Categorie.SupprimerCategorie(idCategorie))
                {
                    ShowTableCategorieGestion();
                    ClearCategorieFormGestion();
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
        /// Ouvre la modale pour ajouter une nouvelle catégorie
        /// </summary>
        public void AfficherModaleAjouterCategorie()
        {
            using (var formAjouter = new AjouterCategorieForm())
            {
                var result = formAjouter.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    // Rafraîchir les tables après l'ajout
                    ShowTableCategorieGestion();
                }
            }
        }

        private void buttonAjouterCategorie_Click(object sender, EventArgs e)
        {
            AfficherModaleAjouterCategorie();
        }

        private void DataGridView_Categorie_Gestion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView_Categorie_Gestion_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        ////////////////////////////////////////////////////////////////////////////
    }
}
