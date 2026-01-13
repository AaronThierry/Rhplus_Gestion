using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class GestionIndemniteForm : Form
    {
        Dbconnect connect = new Dbconnect();

        // Variables pour suivre le bouton survolé
        private int hoverRowIndex = -1;
        private string hoverButton = "";

        public GestionIndemniteForm()
        {
            InitializeComponent();

            // Attacher les gestionnaires d'événements pour les Actions
            DataGridView_Indemnite.CellPainting += DataGridView_Indemnite_CellPainting;
            DataGridView_Indemnite.CellClick += DataGridView_Indemnite_CellClick;
            DataGridView_Indemnite.CellMouseMove += DataGridView_Indemnite_CellMouseMove;

            StyliserDataGridView();
            ShowTableIndemnite();

            // Styliser le header avec un design moderne
            StyliserHeader();
        }

        private void StyliserHeader()
        {
            // Configurer le panel2 (header)
            panel2.Height = 70;
            panel2.Paint += (s, e) =>
            {
                // Dégradé corporate élégant - Bleu professionnel
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    panel2.ClientRectangle,
                    Color.FromArgb(25, 25, 112),    // MidnightBlue
                    Color.FromArgb(65, 105, 225),   // RoyalBlue
                    System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, panel2.ClientRectangle);
                }

                // Barre accent subtile
                using (var accentBrush = new SolidBrush(Color.FromArgb(46, 139, 87)))
                {
                    e.Graphics.FillRectangle(accentBrush, 0, panel2.Height - 3, panel2.Width, 3);
                }
            };

            // Styliser label1 (titre principal)
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Montserrat", 15F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Text = "GESTION DES INDEMNITÉS";
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

                using (var brush = new SolidBrush(Color.FromArgb(46, 139, 87)))
                using (var pen = new Pen(Color.FromArgb(46, 139, 87), 2f))
                {
                    // 3 pièces empilées (cercles)
                    e.Graphics.DrawEllipse(pen, iconX + 8, iconY + 2, 12, 12);
                    e.Graphics.DrawEllipse(pen, iconX + 4, iconY + 8, 12, 12);
                    e.Graphics.DrawEllipse(pen, iconX + 12, iconY + 8, 12, 12);

                    // Symboles € au centre des pièces
                    using (var font = new Font("Arial", 8F, FontStyle.Bold))
                    {
                        e.Graphics.DrawString("€", font, brush, iconX + 11, iconY + 4);
                        e.Graphics.DrawString("€", font, brush, iconX + 7, iconY + 10);
                        e.Graphics.DrawString("€", font, brush, iconX + 15, iconY + 10);
                    }
                }
            };

            panel2.Invalidate();
        }

        private void StyliserDataGridView()
        {
            DataGridView_Indemnite.BorderStyle = BorderStyle.None;
            DataGridView_Indemnite.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Indemnite.RowHeadersVisible = false;
            DataGridView_Indemnite.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Indemnite.EnableHeadersVisualStyles = false;

            DataGridView_Indemnite.ColumnHeadersDefaultCellStyle.BackColor = Color.MidnightBlue;
            DataGridView_Indemnite.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DataGridView_Indemnite.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 10F, FontStyle.Bold);
            DataGridView_Indemnite.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.MidnightBlue;
            DataGridView_Indemnite.ColumnHeadersHeight = 45;

            DataGridView_Indemnite.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 250);
            DataGridView_Indemnite.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            DataGridView_Indemnite.DefaultCellStyle.SelectionForeColor = Color.Black;
            DataGridView_Indemnite.DefaultCellStyle.Font = new Font("Montserrat", 9.5F);
            DataGridView_Indemnite.RowTemplate.Height = 50;
        }

        private void ShowTableIndemnite()
        {
            var dt = IndemniteClass.GetIndemniteList(null);

            // Ajouter la colonne "Actions" en tant que texte
            if (!dt.Columns.Contains("Actions"))
            {
                dt.Columns.Add("Actions", typeof(string));
            }

            DataGridView_Indemnite.AutoGenerateColumns = true;
            DataGridView_Indemnite.DataSource = dt;

            // Masquer les IDs si tu veux une vue clean
            if (DataGridView_Indemnite.Columns.Contains("Id"))
                DataGridView_Indemnite.Columns["Id"].Visible = false;

            // Configurer la colonne Actions
            if (DataGridView_Indemnite.Columns["Actions"] != null)
            {
                DataGridView_Indemnite.Columns["Actions"].Width = 180;
                DataGridView_Indemnite.Columns["Actions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataGridView_Indemnite.Columns["Actions"].HeaderText = "Actions";
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = textBoxSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                ShowTableIndemnite();
                return;
            }

            // Recherche dans la liste complète des indemnités
            var dt = IndemniteClass.GetIndemniteList(null);

            // Filtrer les résultats
            var filteredRows = dt.AsEnumerable().Where(row =>
                row.Field<string>("Entreprise")?.ToLower().Contains(searchTerm.ToLower()) == true ||
                row.Field<string>("Employe")?.ToLower().Contains(searchTerm.ToLower()) == true ||
                row.Field<string>("Type")?.ToLower().Contains(searchTerm.ToLower()) == true ||
                row.Field<string>("Matricule")?.ToLower().Contains(searchTerm.ToLower()) == true
            );

            DataTable filteredDt;
            if (filteredRows.Any())
            {
                filteredDt = filteredRows.CopyToDataTable();
            }
            else
            {
                filteredDt = dt.Clone(); // Table vide avec la même structure
            }

            // Ajouter la colonne "Actions" si elle n'existe pas
            if (!filteredDt.Columns.Contains("Actions"))
            {
                filteredDt.Columns.Add("Actions", typeof(string));
            }

            DataGridView_Indemnite.DataSource = filteredDt;

            // Masquer les IDs
            if (DataGridView_Indemnite.Columns.Contains("Id"))
                DataGridView_Indemnite.Columns["Id"].Visible = false;

            // Configurer la colonne Actions
            if (DataGridView_Indemnite.Columns["Actions"] != null)
            {
                DataGridView_Indemnite.Columns["Actions"].Width = 180;
                DataGridView_Indemnite.Columns["Actions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataGridView_Indemnite.Columns["Actions"].HeaderText = "Actions";
            }
        }

        private void buttonAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                var formAjouter = new AjouterIndemniteFormV3();
                if (formAjouter.ShowDialog() == DialogResult.OK)
                {
                    ShowTableIndemnite();
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de l'ouverture du formulaire :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
        }

        // ========== GESTION DES BOUTONS PERSONNALISÉS DANS LE DATAGRIDVIEW ==========

        private void DataGridView_Indemnite_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && DataGridView_Indemnite.Columns[e.ColumnIndex].Name == "Actions" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int buttonWidth = 75;
                int buttonHeight = 32;
                int spacing = 10;
                int totalWidth = (buttonWidth * 2) + spacing;
                int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
                int startY = e.CellBounds.Top + (e.CellBounds.Height - buttonHeight) / 2;

                // Bouton Modifier (doré) - Style Entreprise
                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                bool modifierHover = (hoverRowIndex == e.RowIndex && hoverButton == "Modifier");
                Color modifierColor = modifierHover ? Color.FromArgb(218, 165, 32) : Color.FromArgb(255, 215, 0);

                using (SolidBrush brush = new SolidBrush(modifierColor))
                using (Pen borderPen = new Pen(Color.FromArgb(184, 134, 11), 1))
                {
                    e.Graphics.FillRoundedRectangle(brush, btnModifier, 6);
                    e.Graphics.DrawRoundedRectangle(borderPen, btnModifier, 6);
                }

                // Icône et texte Modifier empilés
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

                // Bouton Supprimer (rouge) - Style Entreprise
                Rectangle btnSupprimer = new Rectangle(startX + buttonWidth + spacing, startY, buttonWidth, buttonHeight);
                bool supprimerHover = (hoverRowIndex == e.RowIndex && hoverButton == "Supprimer");
                Color supprimerColor = supprimerHover ? Color.FromArgb(200, 35, 51) : Color.FromArgb(255, 87, 87);

                using (SolidBrush brush = new SolidBrush(supprimerColor))
                using (Pen borderPen = new Pen(Color.FromArgb(220, 53, 69), 1))
                {
                    e.Graphics.FillRoundedRectangle(brush, btnSupprimer, 6);
                    e.Graphics.DrawRoundedRectangle(borderPen, btnSupprimer, 6);
                }

                // Icône et texte Supprimer empilés
                using (Font iconFont = new Font("Segoe UI Emoji", 10f, FontStyle.Regular))
                using (Font textFont = new Font("Montserrat", 8f, FontStyle.Bold))
                using (SolidBrush iconBrush = new SolidBrush(Color.FromArgb(64, 64, 64)))
                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(64, 64, 64)))
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

        private void DataGridView_Indemnite_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && DataGridView_Indemnite.Columns[e.ColumnIndex].Name == "Actions" && e.RowIndex >= 0)
            {
                var cellRect = DataGridView_Indemnite.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                int buttonWidth = 75;
                int buttonHeight = 32;
                int spacing = 10;
                int totalWidth = (buttonWidth * 2) + spacing;
                int startX = cellRect.Left + (cellRect.Width - totalWidth) / 2;
                int startY = cellRect.Top + (cellRect.Height - buttonHeight) / 2;

                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                Rectangle btnSupprimer = new Rectangle(startX + buttonWidth + spacing, startY, buttonWidth, buttonHeight);

                Point mousePos = DataGridView_Indemnite.PointToClient(Cursor.Position);

                string newHoverButton = "";
                if (btnModifier.Contains(mousePos.X, mousePos.Y))
                {
                    newHoverButton = "Modifier";
                }
                else if (btnSupprimer.Contains(mousePos.X, mousePos.Y))
                {
                    newHoverButton = "Supprimer";
                }

                if (hoverRowIndex != e.RowIndex || hoverButton != newHoverButton)
                {
                    hoverRowIndex = e.RowIndex;
                    hoverButton = newHoverButton;
                    DataGridView_Indemnite.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    DataGridView_Indemnite.Cursor = string.IsNullOrEmpty(newHoverButton) ? Cursors.Default : Cursors.Hand;
                }
            }
            else
            {
                if (hoverRowIndex >= 0)
                {
                    int oldHoverRow = hoverRowIndex;
                    hoverRowIndex = -1;
                    hoverButton = "";
                    DataGridView_Indemnite.Cursor = Cursors.Default;
                    if (oldHoverRow < DataGridView_Indemnite.Rows.Count)
                    {
                        DataGridView_Indemnite.InvalidateRow(oldHoverRow);
                    }
                }
            }
        }

        private void DataGridView_Indemnite_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (e.RowIndex >= DataGridView_Indemnite.Rows.Count) return;

            if (DataGridView_Indemnite.Columns[e.ColumnIndex].Name == "Actions")
            {
                var cellRect = DataGridView_Indemnite.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                int buttonWidth = 75;
                int buttonHeight = 32;
                int spacing = 10;
                int totalWidth = (buttonWidth * 2) + spacing;
                int startX = cellRect.Left + (cellRect.Width - totalWidth) / 2;
                int startY = cellRect.Top + (cellRect.Height - buttonHeight) / 2;

                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                Rectangle btnSupprimer = new Rectangle(startX + buttonWidth + spacing, startY, buttonWidth, buttonHeight);

                Point mousePos = DataGridView_Indemnite.PointToClient(Cursor.Position);

                // Récupérer l'ID de l'indemnité
                int idIndemnite = 0;
                if (DataGridView_Indemnite.Rows[e.RowIndex].Cells["Id"].Value != null)
                {
                    idIndemnite = Convert.ToInt32(DataGridView_Indemnite.Rows[e.RowIndex].Cells["Id"].Value);
                }

                // Bouton Modifier
                if (btnModifier.Contains(mousePos.X, mousePos.Y))
                {
                    var formModifier = new ModifierIndemniteForm(idIndemnite);
                    if (formModifier.ShowDialog() == DialogResult.OK)
                    {
                        ShowTableIndemnite();
                    }
                }
                // Bouton Supprimer
                else if (btnSupprimer.Contains(mousePos.X, mousePos.Y))
                {
                    var confirm = CustomMessageBox.Show(
                        "Voulez-vous vraiment supprimer cette indemnité ?",
                        "Confirmation",
                        CustomMessageBox.MessageType.Question,
                        CustomMessageBox.MessageButtons.YesNo);

                    if (confirm == DialogResult.Yes)
                    {
                        try
                        {
                            IndemniteRepository.Supprimer(idIndemnite);
                            CustomMessageBox.Show("Indemnité supprimée avec succès.", "Succès",
                                CustomMessageBox.MessageType.Success);
                            ShowTableIndemnite();
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
    }
}
