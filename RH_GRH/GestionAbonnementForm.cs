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
    public partial class GestionAbonnementForm : Form
    {
        Dbconnect connect = new Dbconnect();

        // Variables pour suivre le bouton survolé
        private int hoverRowIndex = -1;
        private string hoverButton = "";

        public GestionAbonnementForm()
        {
            InitializeComponent();

            // Attacher les gestionnaires d'événements pour les Actions
            DataGridView_Abonnement.CellPainting += DataGridView_Abonnement_CellPainting;
            DataGridView_Abonnement.CellClick += DataGridView_Abonnement_CellClick;
            DataGridView_Abonnement.CellMouseMove += DataGridView_Abonnement_CellMouseMove;

            StyliserDataGridView();
            ShowTableAbonnement();

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
                    Color.FromArgb(25, 118, 210),      // Bleu foncé
                    Color.FromArgb(66, 165, 245),      // Bleu clair
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
            label1.Text = "GESTION DES ABONNEMENTS";
            label1.Padding = new Padding(70, 0, 0, 0);  // Espace pour l'icône
            label1.TextAlign = ContentAlignment.MiddleLeft;
            label1.Dock = DockStyle.Fill;
            label1.AutoSize = false;

            // Dessiner une icône personnalisée
            label1.Paint += (s, e) =>
            {
                // Dessiner l'icône d'abonnement (cycle/refresh)
                int iconSize = 28;
                int iconX = 30;
                int iconY = (label1.Height - iconSize) / 2;

                using (var pen = new Pen(Color.FromArgb(255, 215, 0), 2.5f))
                {
                    pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

                    // Cercle avec flèche (symbole de renouvellement)
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.DrawArc(pen, iconX + 2, iconY + 2, iconSize - 4, iconSize - 4, 45, 270);

                    // Flèche
                    Point[] arrow = new Point[]
                    {
                        new Point(iconX + iconSize - 5, iconY + 8),
                        new Point(iconX + iconSize - 5, iconY + 15),
                        new Point(iconX + iconSize - 10, iconY + 10)
                    };
                    using (var brush = new SolidBrush(Color.FromArgb(255, 215, 0)))
                    {
                        e.Graphics.FillPolygon(brush, arrow);
                    }
                }
            };

            panel2.Invalidate();
        }

        private void StyliserDataGridView()
        {
            DataGridView_Abonnement.BorderStyle = BorderStyle.None;
            DataGridView_Abonnement.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Abonnement.RowHeadersVisible = false;
            DataGridView_Abonnement.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Abonnement.EnableHeadersVisualStyles = false;

            DataGridView_Abonnement.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            DataGridView_Abonnement.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DataGridView_Abonnement.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 10F, FontStyle.Bold);
            DataGridView_Abonnement.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(25, 118, 210);
            DataGridView_Abonnement.ColumnHeadersHeight = 45;

            DataGridView_Abonnement.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 250);
            DataGridView_Abonnement.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            DataGridView_Abonnement.DefaultCellStyle.SelectionForeColor = Color.Black;
            DataGridView_Abonnement.DefaultCellStyle.Font = new Font("Montserrat", 9.5F);
            DataGridView_Abonnement.RowTemplate.Height = 50;
        }

        private void ShowTableAbonnement()
        {
            var dt = AbonnementRepository.GetAbonnementList();

            // Ajouter la colonne "Actions" en tant que texte
            if (!dt.Columns.Contains("Actions"))
            {
                dt.Columns.Add("Actions", typeof(string));
            }

            DataGridView_Abonnement.AutoGenerateColumns = true;
            DataGridView_Abonnement.DataSource = dt;

            // Masquer les IDs si tu veux une vue clean
            if (DataGridView_Abonnement.Columns.Contains("Id"))
                DataGridView_Abonnement.Columns["Id"].Visible = false;

            // Configurer les en-têtes de colonnes
            if (DataGridView_Abonnement.Columns["DateDebut"] != null)
                DataGridView_Abonnement.Columns["DateDebut"].HeaderText = "Date Début";

            if (DataGridView_Abonnement.Columns["DateFin"] != null)
                DataGridView_Abonnement.Columns["DateFin"].HeaderText = "Date Fin";

            if (DataGridView_Abonnement.Columns["Entreprise"] != null)
                DataGridView_Abonnement.Columns["Entreprise"].HeaderText = "Entreprise";

            if (DataGridView_Abonnement.Columns["Employe"] != null)
                DataGridView_Abonnement.Columns["Employe"].HeaderText = "Employé";

            // Configurer la colonne Actions
            if (DataGridView_Abonnement.Columns["Actions"] != null)
            {
                DataGridView_Abonnement.Columns["Actions"].Width = 180;
                DataGridView_Abonnement.Columns["Actions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataGridView_Abonnement.Columns["Actions"].HeaderText = "Actions";
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = textBoxSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                ShowTableAbonnement();
                return;
            }

            // Recherche dans la liste complète des abonnements
            var dt = AbonnementRepository.GetAbonnementList();

            // Filtrer les résultats
            var filteredRows = dt.AsEnumerable().Where(row =>
                row.Field<string>("Entreprise")?.ToLower().Contains(searchTerm.ToLower()) == true ||
                row.Field<string>("Employe")?.ToLower().Contains(searchTerm.ToLower()) == true ||
                row.Field<string>("Nom")?.ToLower().Contains(searchTerm.ToLower()) == true ||
                row.Field<string>("Description")?.ToLower().Contains(searchTerm.ToLower()) == true ||
                row.Field<string>("DateDebut")?.ToLower().Contains(searchTerm.ToLower()) == true ||
                row.Field<string>("DateFin")?.ToLower().Contains(searchTerm.ToLower()) == true ||
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

            DataGridView_Abonnement.DataSource = filteredDt;

            // Masquer les IDs
            if (DataGridView_Abonnement.Columns.Contains("Id"))
                DataGridView_Abonnement.Columns["Id"].Visible = false;

            // Configurer la colonne Actions
            if (DataGridView_Abonnement.Columns["Actions"] != null)
            {
                DataGridView_Abonnement.Columns["Actions"].Width = 180;
                DataGridView_Abonnement.Columns["Actions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataGridView_Abonnement.Columns["Actions"].HeaderText = "Actions";
            }
        }

        private void buttonAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                var formAjouter = new AjouterAbonnementForm();
                if (formAjouter.ShowDialog() == DialogResult.OK)
                {
                    ShowTableAbonnement();
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de l'ouverture du formulaire :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
        }

        // ========== GESTION DES BOUTONS PERSONNALISÉS DANS LE DATAGRIDVIEW ==========

        private void DataGridView_Abonnement_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && DataGridView_Abonnement.Columns[e.ColumnIndex].Name == "Actions" && e.RowIndex >= 0)
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

        private void DataGridView_Abonnement_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && DataGridView_Abonnement.Columns[e.ColumnIndex].Name == "Actions" && e.RowIndex >= 0)
            {
                var cellRect = DataGridView_Abonnement.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                int buttonWidth = 75;
                int buttonHeight = 32;
                int spacing = 10;
                int totalWidth = (buttonWidth * 2) + spacing;
                int startX = cellRect.Left + (cellRect.Width - totalWidth) / 2;
                int startY = cellRect.Top + (cellRect.Height - buttonHeight) / 2;

                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                Rectangle btnSupprimer = new Rectangle(startX + buttonWidth + spacing, startY, buttonWidth, buttonHeight);

                Point mousePos = DataGridView_Abonnement.PointToClient(Cursor.Position);

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
                    DataGridView_Abonnement.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    DataGridView_Abonnement.Cursor = string.IsNullOrEmpty(newHoverButton) ? Cursors.Default : Cursors.Hand;
                }
            }
            else
            {
                if (hoverRowIndex >= 0)
                {
                    int oldHoverRow = hoverRowIndex;
                    hoverRowIndex = -1;
                    hoverButton = "";
                    DataGridView_Abonnement.Cursor = Cursors.Default;
                    if (oldHoverRow < DataGridView_Abonnement.Rows.Count)
                    {
                        DataGridView_Abonnement.InvalidateRow(oldHoverRow);
                    }
                }
            }
        }

        private void DataGridView_Abonnement_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (e.RowIndex >= DataGridView_Abonnement.Rows.Count) return;

            if (DataGridView_Abonnement.Columns[e.ColumnIndex].Name == "Actions")
            {
                var cellRect = DataGridView_Abonnement.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                int buttonWidth = 75;
                int buttonHeight = 32;
                int spacing = 10;
                int totalWidth = (buttonWidth * 2) + spacing;
                int startX = cellRect.Left + (cellRect.Width - totalWidth) / 2;
                int startY = cellRect.Top + (cellRect.Height - buttonHeight) / 2;

                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                Rectangle btnSupprimer = new Rectangle(startX + buttonWidth + spacing, startY, buttonWidth, buttonHeight);

                Point mousePos = DataGridView_Abonnement.PointToClient(Cursor.Position);

                // Récupérer l'ID de l'abonnement
                int idAbonnement = 0;
                if (DataGridView_Abonnement.Rows[e.RowIndex].Cells["Id"].Value != null)
                {
                    idAbonnement = Convert.ToInt32(DataGridView_Abonnement.Rows[e.RowIndex].Cells["Id"].Value);
                }

                // Bouton Modifier
                if (btnModifier.Contains(mousePos.X, mousePos.Y))
                {
                    var formModifier = new ModifierAbonnementForm(idAbonnement);
                    if (formModifier.ShowDialog() == DialogResult.OK)
                    {
                        ShowTableAbonnement();
                    }
                }
                // Bouton Supprimer
                else if (btnSupprimer.Contains(mousePos.X, mousePos.Y))
                {
                    var confirm = CustomMessageBox.Show(
                        "Voulez-vous vraiment supprimer cet abonnement ?",
                        "Confirmation",
                        CustomMessageBox.MessageType.Question,
                        CustomMessageBox.MessageButtons.YesNo);

                    if (confirm == DialogResult.Yes)
                    {
                        try
                        {
                            AbonnementRepository.Supprimer(idAbonnement);
                            CustomMessageBox.Show("Abonnement supprimé avec succès.", "Succès",
                                CustomMessageBox.MessageType.Success);
                            ShowTableAbonnement();
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
