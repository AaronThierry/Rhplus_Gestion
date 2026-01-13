using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        // Variables pour suivre le bouton survolé
        private int hoverRowIndex = -1;
        private string hoverButton = "";

        public GestionChargeForm()
        {
            InitializeComponent();

            // Attacher les gestionnaires d'événements pour les Actions
            DataGridView_Charge.CellPainting += DataGridView_Charge_CellPainting;
            DataGridView_Charge.CellClick += DataGridView_Charge_CellClick;
            DataGridView_Charge.CellMouseMove += DataGridView_Charge_CellMouseMove;

            StyliserDataGridView();
            ShowTableCategorie();

            // Styliser le header avec un design moderne
            StyliserHeader();
        }

        private void StyliserHeader()
        {
            // Configurer le panel2 (header)
            panel2.Height = 70;
            panel2.Paint += (s, e) =>
            {
                // Dégradé corporate élégant - Rouge bordeaux professionnel
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    panel2.ClientRectangle,
                    Color.FromArgb(130, 40, 50),    // Bordeaux profond
                    Color.FromArgb(170, 60, 75),    // Rouge corporate
                    System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, panel2.ClientRectangle);
                }

                // Barre accent subtile
                using (var accentBrush = new SolidBrush(Color.FromArgb(210, 80, 95)))
                {
                    e.Graphics.FillRectangle(accentBrush, 0, panel2.Height - 3, panel2.Width, 3);
                }
            };

            // Styliser label1 (titre principal)
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Montserrat", 15F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Text = "CHARGES";
            label1.Padding = new Padding(70, 0, 0, 0);  // Espace pour l'icône
            label1.TextAlign = ContentAlignment.MiddleLeft;
            label1.Dock = DockStyle.Fill;
            label1.AutoSize = false;

            // Dessiner une icône personnalisée
            label1.Paint += (s, e) =>
            {
                // Dessiner l'icône pièces de monnaie empilées
                int iconSize = 28;
                int iconX = 30;
                int iconY = (label1.Height - iconSize) / 2;

                using (var brush = new SolidBrush(Color.FromArgb(210, 80, 95)))
                using (var pen = new Pen(Color.FromArgb(210, 80, 95), 2f))
                {
                    // 3 pièces empilées (cercles)
                    e.Graphics.DrawEllipse(pen, iconX + 8, iconY + 2, 12, 12);
                    e.Graphics.DrawEllipse(pen, iconX + 4, iconY + 8, 12, 12);
                    e.Graphics.DrawEllipse(pen, iconX + 12, iconY + 8, 12, 12);

                    // Symboles $ au centre des pièces
                    using (var font = new Font("Arial", 8F, FontStyle.Bold))
                    {
                        e.Graphics.DrawString("$", font, brush, iconX + 11, iconY + 4);
                        e.Graphics.DrawString("$", font, brush, iconX + 7, iconY + 10);
                        e.Graphics.DrawString("$", font, brush, iconX + 15, iconY + 10);
                    }
                }
            };

            panel2.Invalidate();
        }

        private void ShowTableCategorie()
        {
            var dt = ChargeClass.GetChargeList(null);

            // Ajouter la colonne "Actions" en tant que texte
            if (!dt.Columns.Contains("Actions"))
            {
                dt.Columns.Add("Actions", typeof(string));
            }

            DataGridView_Charge.AutoGenerateColumns = true;
            DataGridView_Charge.DataSource = dt;

            // Masquer les IDs si tu veux une vue clean
            if (DataGridView_Charge.Columns.Contains("Id"))
                DataGridView_Charge.Columns["Id"].Visible = false;

            // Configurer la colonne Actions
            if (DataGridView_Charge.Columns["Actions"] != null)
            {
                DataGridView_Charge.Columns["Actions"].Width = 180;
                DataGridView_Charge.Columns["Actions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataGridView_Charge.Columns["Actions"].HeaderText = "Actions";
            }
        }

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
            headerStyle.Font = new Font("Montserrat", 9f, FontStyle.Bold);
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
            cellStyle.Font = new Font("Montserrat", 9f, FontStyle.Regular);
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

            // Autres options d'esthétique
            DataGridView_Charge.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Charge.MultiSelect = false;
        }

        private void GestionChargeForm_Load(object sender, EventArgs e)
        {
            ShowTableCategorie();
        }

        // ============ GESTION DES ACTIONS DANS DATAGRIDVIEW_CHARGE ============

        private void DataGridView_Charge_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            if (DataGridView_Charge.Columns[e.ColumnIndex].Name == "Actions")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                // Calculer les dimensions des deux boutons
                int buttonWidth = 80;
                int buttonHeight = 32;
                int buttonSpacing = 5;
                int totalWidth = (buttonWidth * 2) + buttonSpacing;
                int startX = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
                int startY = e.CellBounds.Y + (e.CellBounds.Height - buttonHeight) / 2;

                // Bouton Modifier (or/gold)
                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                bool modifierHover = (hoverRowIndex == e.RowIndex && hoverButton == "Modifier");
                Color modifierColor = modifierHover ? Color.FromArgb(255, 179, 0) : Color.FromArgb(255, 193, 7);

                using (SolidBrush brush = new SolidBrush(modifierColor))
                using (Pen borderPen = new Pen(Color.FromArgb(230, 170, 0), 1))
                {
                    e.Graphics.FillRoundedRectangle(brush, btnModifier, 6);
                    e.Graphics.DrawRoundedRectangle(borderPen, btnModifier, 6);
                }

                // Icône et texte Modifier
                using (Font iconFont = new Font("Segoe UI Emoji", 10f, FontStyle.Regular))
                using (Font textFont = new Font("Montserrat", 8f, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(51, 51, 51)))
                {
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                    // Dessiner l'icône ✏
                    Rectangle iconRect = new Rectangle(btnModifier.X, btnModifier.Y + 2, btnModifier.Width, btnModifier.Height / 2);
                    e.Graphics.DrawString("✏", iconFont, textBrush, iconRect, sf);

                    // Dessiner le texte
                    Rectangle textRect = new Rectangle(btnModifier.X, btnModifier.Y + btnModifier.Height / 2 - 2, btnModifier.Width, btnModifier.Height / 2);
                    e.Graphics.DrawString("Modifier", textFont, textBrush, textRect, sf);
                }

                // Bouton Supprimer (rouge)
                Rectangle btnSupprimer = new Rectangle(startX + buttonWidth + buttonSpacing, startY, buttonWidth, buttonHeight);
                bool supprimerHover = (hoverRowIndex == e.RowIndex && hoverButton == "Supprimer");
                Color supprimerColor = supprimerHover ? Color.FromArgb(211, 47, 47) : Color.FromArgb(244, 67, 54);

                using (SolidBrush brush = new SolidBrush(supprimerColor))
                using (Pen borderPen = new Pen(Color.FromArgb(198, 40, 40), 1))
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

                    // Dessiner l'icône 🗑
                    Rectangle iconRect = new Rectangle(btnSupprimer.X, btnSupprimer.Y + 2, btnSupprimer.Width, btnSupprimer.Height / 2);
                    e.Graphics.DrawString("🗑", iconFont, iconBrush, iconRect, sf);

                    // Dessiner le texte
                    Rectangle textRect = new Rectangle(btnSupprimer.X, btnSupprimer.Y + btnSupprimer.Height / 2 - 2, btnSupprimer.Width, btnSupprimer.Height / 2);
                    e.Graphics.DrawString("Supprimer", textFont, textBrush, textRect, sf);
                }

                e.Handled = true;
            }
        }

        private void DataGridView_Charge_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (DataGridView_Charge.Columns[e.ColumnIndex].Name == "Actions")
            {
                var cellBounds = DataGridView_Charge.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                int buttonWidth = 80;
                int buttonHeight = 32;
                int buttonSpacing = 5;
                int totalWidth = (buttonWidth * 2) + buttonSpacing;
                int startX = cellBounds.X + (cellBounds.Width - totalWidth) / 2;
                int startY = cellBounds.Y + (cellBounds.Height - buttonHeight) / 2;

                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                Rectangle btnSupprimer = new Rectangle(startX + buttonWidth + buttonSpacing, startY, buttonWidth, buttonHeight);

                Point mousePos = DataGridView_Charge.PointToClient(Cursor.Position);

                string newHoverButton = "";
                if (btnModifier.Contains(mousePos.X, mousePos.Y))
                    newHoverButton = "Modifier";
                else if (btnSupprimer.Contains(mousePos.X, mousePos.Y))
                    newHoverButton = "Supprimer";

                if (hoverRowIndex != e.RowIndex || hoverButton != newHoverButton)
                {
                    hoverRowIndex = e.RowIndex;
                    hoverButton = newHoverButton;
                    DataGridView_Charge.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    DataGridView_Charge.Cursor = string.IsNullOrEmpty(newHoverButton) ? Cursors.Default : Cursors.Hand;
                }
            }
        }

        private void DataGridView_Charge_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (DataGridView_Charge.Columns[e.ColumnIndex].Name == "Actions")
            {
                // Récupérer l'ID de la charge depuis la colonne Id
                var cellValue = DataGridView_Charge.Rows[e.RowIndex].Cells["Id"].Value;
                if (cellValue == null || cellValue == DBNull.Value)
                {
                    CustomMessageBox.Show("Impossible de récupérer l'ID de la charge.", "Erreur",
                                    CustomMessageBox.MessageType.Error);
                    return;
                }

                int idCharge = Convert.ToInt32(cellValue);

                // Déterminer quel bouton a été cliqué
                Point mousePos = DataGridView_Charge.PointToClient(Cursor.Position);
                var cellBounds = DataGridView_Charge.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                int buttonWidth = 80;
                int buttonHeight = 32;
                int buttonSpacing = 5;
                int totalWidth = (buttonWidth * 2) + buttonSpacing;
                int startX = cellBounds.X + (cellBounds.Width - totalWidth) / 2;
                int startY = cellBounds.Y + (cellBounds.Height - buttonHeight) / 2;

                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                Rectangle btnSupprimer = new Rectangle(startX + buttonWidth + buttonSpacing, startY, buttonWidth, buttonHeight);

                if (btnModifier.Contains(mousePos.X, mousePos.Y))
                {
                    // Action: Modifier - Ouvrir le formulaire modal ModifierChargeForm
                    ModifierChargeForm modifierForm = new ModifierChargeForm(idCharge);
                    if (modifierForm.ShowDialog() == DialogResult.OK)
                    {
                        // Rafraîchir les données après modification
                        ShowTableCategorie();
                    }
                }
                else if (btnSupprimer.Contains(mousePos.X, mousePos.Y))
                {
                    // Récupérer les informations de la charge pour le message de confirmation
                    string nomPrenom = DataGridView_Charge.Rows[e.RowIndex].Cells["Nom et Prenom "].Value?.ToString();
                    string typeCharge = DataGridView_Charge.Rows[e.RowIndex].Cells["Type"].Value?.ToString();

                    // Action: Supprimer
                    var confirm = CustomMessageBox.Show(
                        $"Êtes-vous sûr de vouloir supprimer cette charge ?\n\n" +
                        $"Nom : {nomPrenom}\n" +
                        $"Type : {typeCharge}\n\n" +
                        $"Cette action est irréversible.",
                        "Confirmer la suppression",
                        CustomMessageBox.MessageType.Warning,
                        CustomMessageBox.MessageButtons.YesNo);

                    if (confirm == DialogResult.Yes)
                    {
                        try
                        {
                            ChargeRepository.Supprimer(idCharge);
                            CustomMessageBox.Show(
                                $"La charge de {nomPrenom} a été supprimée avec succès.",
                                "Succès",
                                CustomMessageBox.MessageType.Success);

                            // Rafraîchir les données
                            ShowTableCategorie();
                        }
                        catch (Exception ex)
                        {
                            CustomMessageBox.Show($"Erreur lors de la suppression :\n{ex.Message}", "Erreur",
                                            CustomMessageBox.MessageType.Error);
                        }
                    }
                }
            }
        }

        // Handler pour le bouton Ajouter Charge
        private void buttonAjouterCharge_Click(object sender, EventArgs e)
        {
            AjouterChargeForm ajouterForm = new AjouterChargeForm();
            if (ajouterForm.ShowDialog() == DialogResult.OK)
            {
                // Rafraîchir les données après ajout
                ShowTableCategorie();
            }
        }

        // Handler pour la recherche
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                // Si le champ de recherche est vide, afficher toutes les charges
                ShowTableCategorie();
            }
            else
            {
                // Rechercher les charges
                DataTable dt = ChargeClass.RechercheCharge(searchText);

                // Ajouter la colonne Actions si elle n'existe pas
                if (!dt.Columns.Contains("Actions"))
                {
                    dt.Columns.Add("Actions", typeof(string));
                }

                DataGridView_Charge.DataSource = dt;

                // Masquer la colonne Id
                if (DataGridView_Charge.Columns.Contains("Id"))
                    DataGridView_Charge.Columns["Id"].Visible = false;

                // Configurer la colonne Actions
                if (DataGridView_Charge.Columns["Actions"] != null)
                {
                    DataGridView_Charge.Columns["Actions"].Width = 180;
                    DataGridView_Charge.Columns["Actions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DataGridView_Charge.Columns["Actions"].HeaderText = "Actions";
                }
            }
        }
    }
}
