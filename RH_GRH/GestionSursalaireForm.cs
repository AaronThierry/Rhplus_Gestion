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
    public partial class GestionSursalaireForm : Form
    {
        Dbconnect connect = new Dbconnect();

        // Variables pour suivre le bouton survolé
        private int hoverRowIndex = -1;
        private string hoverButton = "";

        public GestionSursalaireForm()
        {
            InitializeComponent();

            // Attacher les gestionnaires d'événements pour les Actions
            DataGridView_Sursalaire.CellPainting += DataGridView_Sursalaire_CellPainting;
            DataGridView_Sursalaire.CellClick += DataGridView_Sursalaire_CellClick;
            DataGridView_Sursalaire.CellMouseMove += DataGridView_Sursalaire_CellMouseMove;

            StyliserDataGridView();
            ShowTableSursalaire();

            // Styliser le header avec un design moderne
            StyliserHeader();
        }

        private void StyliserHeader()
        {
            // Configurer le panel2 (header)
            panel2.Height = 70;
            panel2.Paint += (s, e) =>
            {
                // Dégradé corporate élégant - Vert professionnel
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    panel2.ClientRectangle,
                    Color.FromArgb(0, 100, 60),      // Vert foncé
                    Color.FromArgb(46, 139, 87),     // SeaGreen
                    System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, panel2.ClientRectangle);
                }

                // Barre accent subtile
                using (var accentBrush = new SolidBrush(Color.FromArgb(255, 215, 0)))
                {
                    e.Graphics.FillRectangle(accentBrush, 0, panel2.Height - 3, panel2.Width, 3);
                }
            };

            // Styliser label1 (titre principal)
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Montserrat", 15F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Text = "GESTION DES SURSALAIRES";
            label1.Padding = new Padding(70, 0, 0, 0);  // Espace pour l'icône
            label1.TextAlign = ContentAlignment.MiddleLeft;
            label1.Dock = DockStyle.Fill;
            label1.AutoSize = false;

            // Dessiner une icône personnalisée
            label1.Paint += (s, e) =>
            {
                // Dessiner l'icône d'argent avec flèche vers le haut
                int iconSize = 28;
                int iconX = 30;
                int iconY = (label1.Height - iconSize) / 2;

                using (var brush = new SolidBrush(Color.FromArgb(255, 215, 0)))
                using (var pen = new Pen(Color.FromArgb(255, 215, 0), 2.5f))
                {
                    // Flèche vers le haut
                    Point[] arrow = new Point[]
                    {
                        new Point(iconX + 14, iconY + 5),
                        new Point(iconX + 20, iconY + 12),
                        new Point(iconX + 17, iconY + 12),
                        new Point(iconX + 17, iconY + 22),
                        new Point(iconX + 11, iconY + 22),
                        new Point(iconX + 11, iconY + 12),
                        new Point(iconX + 8, iconY + 12)
                    };
                    e.Graphics.FillPolygon(brush, arrow);

                    // Symbole + pour sursalaire
                    using (var font = new Font("Arial", 12F, FontStyle.Bold))
                    {
                        e.Graphics.DrawString("+", font, brush, iconX + 22, iconY + 10);
                    }
                }
            };

            panel2.Invalidate();
        }

        private void StyliserDataGridView()
        {
            DataGridView_Sursalaire.BorderStyle = BorderStyle.None;
            DataGridView_Sursalaire.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Sursalaire.RowHeadersVisible = false;
            DataGridView_Sursalaire.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Sursalaire.EnableHeadersVisualStyles = false;

            DataGridView_Sursalaire.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 139, 87);
            DataGridView_Sursalaire.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DataGridView_Sursalaire.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 10F, FontStyle.Bold);
            DataGridView_Sursalaire.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 139, 87);
            DataGridView_Sursalaire.ColumnHeadersHeight = 45;

            DataGridView_Sursalaire.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 250);
            DataGridView_Sursalaire.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
            DataGridView_Sursalaire.DefaultCellStyle.SelectionForeColor = Color.Black;
            DataGridView_Sursalaire.DefaultCellStyle.Font = new Font("Montserrat", 9.5F);
            DataGridView_Sursalaire.RowTemplate.Height = 50;
        }

        private void ShowTableSursalaire()
        {
            var dt = SursalaireRepository.GetSursalaireList();

            // Ajouter la colonne "Actions" en tant que texte
            if (!dt.Columns.Contains("Actions"))
            {
                dt.Columns.Add("Actions", typeof(string));
            }

            DataGridView_Sursalaire.AutoGenerateColumns = true;
            DataGridView_Sursalaire.DataSource = dt;

            // Masquer les IDs si tu veux une vue clean
            if (DataGridView_Sursalaire.Columns.Contains("Id"))
                DataGridView_Sursalaire.Columns["Id"].Visible = false;

            // Configurer la colonne Actions
            if (DataGridView_Sursalaire.Columns["Actions"] != null)
            {
                DataGridView_Sursalaire.Columns["Actions"].Width = 180;
                DataGridView_Sursalaire.Columns["Actions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataGridView_Sursalaire.Columns["Actions"].HeaderText = "Actions";
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = textBoxSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                ShowTableSursalaire();
                return;
            }

            // Recherche dans la liste complète des sursalaires
            var dt = SursalaireRepository.GetSursalaireList();

            // Filtrer les résultats
            var filteredRows = dt.AsEnumerable().Where(row =>
                row.Field<string>("Entreprise")?.ToLower().Contains(searchTerm.ToLower()) == true ||
                row.Field<string>("Employe")?.ToLower().Contains(searchTerm.ToLower()) == true ||
                row.Field<string>("Nom")?.ToLower().Contains(searchTerm.ToLower()) == true ||
                row.Field<string>("Description")?.ToLower().Contains(searchTerm.ToLower()) == true ||
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

            DataGridView_Sursalaire.DataSource = filteredDt;

            // Masquer les IDs
            if (DataGridView_Sursalaire.Columns.Contains("Id"))
                DataGridView_Sursalaire.Columns["Id"].Visible = false;

            // Configurer la colonne Actions
            if (DataGridView_Sursalaire.Columns["Actions"] != null)
            {
                DataGridView_Sursalaire.Columns["Actions"].Width = 180;
                DataGridView_Sursalaire.Columns["Actions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataGridView_Sursalaire.Columns["Actions"].HeaderText = "Actions";
            }
        }

        private void buttonAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                var formAjouter = new AjouterSursalaireForm();
                if (formAjouter.ShowDialog() == DialogResult.OK)
                {
                    ShowTableSursalaire();
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de l'ouverture du formulaire :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
        }

        // ========== GESTION DES BOUTONS PERSONNALISÉS DANS LE DATAGRIDVIEW ==========

        private void DataGridView_Sursalaire_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && DataGridView_Sursalaire.Columns[e.ColumnIndex].Name == "Actions" && e.RowIndex >= 0)
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

        private void DataGridView_Sursalaire_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && DataGridView_Sursalaire.Columns[e.ColumnIndex].Name == "Actions" && e.RowIndex >= 0)
            {
                var cellRect = DataGridView_Sursalaire.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                int buttonWidth = 75;
                int buttonHeight = 32;
                int spacing = 10;
                int totalWidth = (buttonWidth * 2) + spacing;
                int startX = cellRect.Left + (cellRect.Width - totalWidth) / 2;
                int startY = cellRect.Top + (cellRect.Height - buttonHeight) / 2;

                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                Rectangle btnSupprimer = new Rectangle(startX + buttonWidth + spacing, startY, buttonWidth, buttonHeight);

                Point mousePos = DataGridView_Sursalaire.PointToClient(Cursor.Position);

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
                    DataGridView_Sursalaire.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    DataGridView_Sursalaire.Cursor = string.IsNullOrEmpty(newHoverButton) ? Cursors.Default : Cursors.Hand;
                }
            }
            else
            {
                if (hoverRowIndex >= 0)
                {
                    int oldHoverRow = hoverRowIndex;
                    hoverRowIndex = -1;
                    hoverButton = "";
                    DataGridView_Sursalaire.Cursor = Cursors.Default;
                    if (oldHoverRow < DataGridView_Sursalaire.Rows.Count)
                    {
                        DataGridView_Sursalaire.InvalidateRow(oldHoverRow);
                    }
                }
            }
        }

        private void DataGridView_Sursalaire_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (e.RowIndex >= DataGridView_Sursalaire.Rows.Count) return;

            if (DataGridView_Sursalaire.Columns[e.ColumnIndex].Name == "Actions")
            {
                var cellRect = DataGridView_Sursalaire.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                int buttonWidth = 75;
                int buttonHeight = 32;
                int spacing = 10;
                int totalWidth = (buttonWidth * 2) + spacing;
                int startX = cellRect.Left + (cellRect.Width - totalWidth) / 2;
                int startY = cellRect.Top + (cellRect.Height - buttonHeight) / 2;

                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                Rectangle btnSupprimer = new Rectangle(startX + buttonWidth + spacing, startY, buttonWidth, buttonHeight);

                Point mousePos = DataGridView_Sursalaire.PointToClient(Cursor.Position);

                // Récupérer l'ID du sursalaire
                int idSursalaire = 0;
                if (DataGridView_Sursalaire.Rows[e.RowIndex].Cells["Id"].Value != null)
                {
                    idSursalaire = Convert.ToInt32(DataGridView_Sursalaire.Rows[e.RowIndex].Cells["Id"].Value);
                }

                // Bouton Modifier
                if (btnModifier.Contains(mousePos.X, mousePos.Y))
                {
                    var formModifier = new ModifierSursalaireForm(idSursalaire);
                    if (formModifier.ShowDialog() == DialogResult.OK)
                    {
                        ShowTableSursalaire();
                    }
                }
                // Bouton Supprimer
                else if (btnSupprimer.Contains(mousePos.X, mousePos.Y))
                {
                    var confirm = CustomMessageBox.Show(
                        "Voulez-vous vraiment supprimer ce sursalaire ?",
                        "Confirmation",
                        CustomMessageBox.MessageType.Question,
                        CustomMessageBox.MessageButtons.YesNo);

                    if (confirm == DialogResult.Yes)
                    {
                        try
                        {
                            SursalaireRepository.Supprimer(idSursalaire);
                            CustomMessageBox.Show("Sursalaire supprimé avec succès.", "Succès",
                                CustomMessageBox.MessageType.Success);
                            ShowTableSursalaire();
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
