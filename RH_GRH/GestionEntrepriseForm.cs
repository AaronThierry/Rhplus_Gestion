using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class GestionEntrepriseForm : Form
    {
        EntrepriseClass Entreprise = new EntrepriseClass();
        Dbconnect connect = new Dbconnect();

        // Variables pour suivre le bouton survolé
        private int hoverRowIndex = -1;
        private string hoverButton = "";

        public GestionEntrepriseForm()
        {
            InitializeComponent();

            // Attacher les gestionnaires d'événements
            DataGridView_Entreprise_Gestion.CellPainting += DataGridView_Entreprise_Gestion_CellPainting;
            DataGridView_Entreprise_Gestion.CellClick += DataGridView_Entreprise_Gestion_CellClick;
            DataGridView_Entreprise_Gestion.CellMouseMove += DataGridView_Entreprise_Gestion_CellMouseMove;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;

            ShowTableEntrepriseGestion();

            // Styliser le header avec un design moderne
            StyliserHeader();
        }

        private void StyliserHeader()
        {
            // Configurer le panel2 (header)
            panel2.Height = 70;
            panel2.Paint += (s, e) =>
            {
                // Dégradé corporate élégant - Vert sapin professionnel
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    panel2.ClientRectangle,
                    Color.FromArgb(32, 87, 70),    // Vert sapin foncé
                    Color.FromArgb(46, 125, 100),  // Vert corporate
                    System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, panel2.ClientRectangle);
                }

                // Barre accent subtile
                using (var accentBrush = new SolidBrush(Color.FromArgb(72, 156, 128)))
                {
                    e.Graphics.FillRectangle(accentBrush, 0, panel2.Height - 3, panel2.Width, 3);
                }
            };

            // Styliser labelBandeEntreprise (titre principal)
            labelBandeEntreprise.BackColor = Color.Transparent;
            labelBandeEntreprise.Font = new Font("Montserrat", 15F, FontStyle.Bold);
            labelBandeEntreprise.ForeColor = Color.White;
            labelBandeEntreprise.Text = "ENTREPRISES";
            labelBandeEntreprise.Padding = new Padding(70, 0, 0, 0);  // Espace pour l'icône
            labelBandeEntreprise.TextAlign = ContentAlignment.MiddleLeft;
            labelBandeEntreprise.Dock = DockStyle.Fill;
            labelBandeEntreprise.AutoSize = false;

            // Dessiner une icône personnalisée
            labelBandeEntreprise.Paint += (s, e) =>
            {
                // Dessiner l'icône bâtiment
                int iconSize = 28;
                int iconX = 30;
                int iconY = (labelBandeEntreprise.Height - iconSize) / 2;

                using (var brush = new SolidBrush(Color.FromArgb(72, 156, 128)))
                using (var pen = new Pen(Color.FromArgb(72, 156, 128), 2f))
                {
                    // Contour du bâtiment
                    e.Graphics.DrawRectangle(pen, iconX + 2, iconY + 4, iconSize - 4, iconSize - 4);
                    // Fenêtres (6 petits carrés)
                    int windowSize = 4;
                    int spacing = 6;
                    for (int row = 0; row < 2; row++)
                    {
                        for (int col = 0; col < 3; col++)
                        {
                            e.Graphics.FillRectangle(brush,
                                iconX + 6 + (col * spacing),
                                iconY + 8 + (row * spacing),
                                windowSize, windowSize);
                        }
                    }
                }
            };

            panel2.Invalidate();
        }

        public void ShowTableEntrepriseGestion()
        {
            try
            {
                DataTable table = Entreprise.getEntrepriseList();

                // Copier toutes les colonnes sauf "Logo"
                DataTable tableFiltered = new DataTable();
                foreach (DataColumn col in table.Columns)
                {
                    if (col.ColumnName != "Logo")
                    {
                        tableFiltered.Columns.Add(col.ColumnName, col.DataType);
                    }
                }

                // Copier les lignes
                foreach (DataRow row in table.Rows)
                {
                    DataRow newRow = tableFiltered.NewRow();
                    foreach (DataColumn col in tableFiltered.Columns)
                    {
                        newRow[col.ColumnName] = row[col.ColumnName];
                    }
                    tableFiltered.Rows.Add(newRow);
                }

                // Ajouter la colonne "Actions" en tant que texte
                if (!tableFiltered.Columns.Contains("Actions"))
                {
                    tableFiltered.Columns.Add("Actions", typeof(string));
                }

                DataGridView_Entreprise_Gestion.DataSource = tableFiltered;

                // Masquer la colonne N° (avec symbole degré)
                if (DataGridView_Entreprise_Gestion.Columns.Contains("N°"))
                {
                    DataGridView_Entreprise_Gestion.Columns["N°"].Visible = false;
                }

                // Masquer la colonne ID
                if (DataGridView_Entreprise_Gestion.Columns.Contains("id_entreprise"))
                {
                    DataGridView_Entreprise_Gestion.Columns["id_entreprise"].Visible = false;
                }

                // Configurer la colonne Actions
                if (DataGridView_Entreprise_Gestion.Columns["Actions"] != null)
                {
                    DataGridView_Entreprise_Gestion.Columns["Actions"].Width = 120;
                    DataGridView_Entreprise_Gestion.Columns["Actions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DataGridView_Entreprise_Gestion.Columns["Actions"].HeaderText = "Actions";
                }

                // Masquer les colonnes techniques si nécessaires
                DataGridView_Entreprise_Gestion.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement des entreprises :\n{ex.Message}", "Erreur",
                                CustomMessageBox.MessageType.Error);
            }
        }

        private void DataGridView_Entreprise_Gestion_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            if (DataGridView_Entreprise_Gestion.Columns[e.ColumnIndex].Name == "Actions")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                // Calculer les dimensions du bouton Modifier (centré)
                int buttonWidth = 75;
                int buttonHeight = 32;
                int startX = e.CellBounds.X + (e.CellBounds.Width - buttonWidth) / 2;
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

                e.Handled = true;
            }
        }

        private void DataGridView_Entreprise_Gestion_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (DataGridView_Entreprise_Gestion.Columns[e.ColumnIndex].Name == "Actions")
            {
                var cellBounds = DataGridView_Entreprise_Gestion.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                int buttonWidth = 75;
                int buttonHeight = 32;
                int startX = cellBounds.X + (cellBounds.Width - buttonWidth) / 2;
                int startY = cellBounds.Y + (cellBounds.Height - buttonHeight) / 2;

                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);

                Point mousePos = DataGridView_Entreprise_Gestion.PointToClient(Cursor.Position);

                string newHoverButton = "";
                if (btnModifier.Contains(mousePos.X, mousePos.Y))
                    newHoverButton = "Modifier";

                if (hoverRowIndex != e.RowIndex || hoverButton != newHoverButton)
                {
                    hoverRowIndex = e.RowIndex;
                    hoverButton = newHoverButton;
                    DataGridView_Entreprise_Gestion.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    DataGridView_Entreprise_Gestion.Cursor = string.IsNullOrEmpty(newHoverButton) ? Cursors.Default : Cursors.Hand;
                }
            }
        }

        private void DataGridView_Entreprise_Gestion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (DataGridView_Entreprise_Gestion.Columns[e.ColumnIndex].Name == "Actions")
            {
                // Récupérer l'ID de l'entreprise depuis la colonne N°
                var cellValue = DataGridView_Entreprise_Gestion.Rows[e.RowIndex].Cells["N°"].Value;
                if (cellValue == null || cellValue == DBNull.Value)
                {
                    CustomMessageBox.Show("Impossible de récupérer l'ID de l'entreprise.", "Erreur",
                                    CustomMessageBox.MessageType.Error);
                    return;
                }

                int idEntreprise = Convert.ToInt32(cellValue);

                // Récupérer toutes les données de l'entreprise depuis la base
                try
                {
                    string query = "SELECT * FROM entreprise WHERE id_entreprise = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connect.getconnection))
                    {
                        cmd.Parameters.AddWithValue("@id", idEntreprise);
                        connect.openConnect();

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string nomEntreprise = reader["nomEntreprise"]?.ToString();
                                string formeJuridique = reader["forme_juridique"]?.ToString();
                                string sigle = reader["sigle"]?.ToString();
                                string activite = reader["activite"]?.ToString();
                                string adressePhysique = reader["adresse_physique"]?.ToString();
                                string adressePostale = reader["adresse_postale"]?.ToString();
                                string telephone = reader["telephone"]?.ToString();
                                string commune = reader["commune"]?.ToString();
                                string quartier = reader["quartier"]?.ToString();
                                string rue = reader["rue"]?.ToString();
                                string lot = reader["lot"]?.ToString();
                                string centreImpots = reader["centre_impots"]?.ToString();
                                string numeroIfu = reader["numero_ifu"]?.ToString();
                                string numeroCnss = reader["numero_cnss"]?.ToString();
                                string codeActivite = reader["code_activite"]?.ToString();
                                string regimeFiscal = reader["regime_fiscal"]?.ToString();
                                string registreCommerce = reader["registre_commerce"]?.ToString();
                                string numeroBancaire = reader["numero_bancaire"]?.ToString();
                                decimal? tpa = reader["tpa"] != DBNull.Value ? Convert.ToDecimal(reader["tpa"]) : (decimal?)null;
                                string email = reader["email"]?.ToString();
                                byte[] logo = reader["logo_entreprise"] != DBNull.Value ? (byte[])reader["logo_entreprise"] : null;

                                connect.closeConnect();

                                // Déterminer si le bouton Modifier a été cliqué
                                Point mousePos = DataGridView_Entreprise_Gestion.PointToClient(Cursor.Position);
                                var cellBounds = DataGridView_Entreprise_Gestion.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                                int buttonWidth = 75;
                                int buttonHeight = 32;
                                int startX = cellBounds.X + (cellBounds.Width - buttonWidth) / 2;
                                int startY = cellBounds.Y + (cellBounds.Height - buttonHeight) / 2;

                                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);

                                if (btnModifier.Contains(mousePos.X, mousePos.Y))
                                {
                                    // Bouton "Modifier" cliqué
                                    ModifierEntrepriseAvecModale(
                                        idEntreprise, nomEntreprise, formeJuridique, sigle, activite,
                                        adressePhysique, adressePostale, telephone, commune, quartier,
                                        rue, lot, centreImpots, numeroIfu, numeroCnss, codeActivite,
                                        regimeFiscal, registreCommerce, numeroBancaire, tpa, email, logo
                                    );
                                }
                            }
                            else
                            {
                                connect.closeConnect();
                                CustomMessageBox.Show("Entreprise introuvable.", "Erreur",
                                                CustomMessageBox.MessageType.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    connect.closeConnect();
                    CustomMessageBox.Show($"Erreur lors de la récupération des données :\n{ex.Message}", "Erreur",
                                    CustomMessageBox.MessageType.Error);
                }
            }
        }

        private void ModifierEntrepriseAvecModale(
            int idEntreprise, string nomEntreprise, string formeJuridique, string sigle,
            string activite, string adressePhysique, string adressePostale, string telephone,
            string commune, string quartier, string rue, string lot, string centreImpots,
            string numeroIfu, string numeroCnss, string codeActivite, string regimeFiscal,
            string registreCommerce, string numeroBancaire, decimal? tpa, string email, byte[] logo)
        {
            using (var formModifier = new ModifierEntrepriseForm(
                idEntreprise, nomEntreprise, formeJuridique, sigle, activite,
                adressePhysique, adressePostale, telephone, commune, quartier, rue, lot,
                centreImpots, numeroIfu, numeroCnss, codeActivite, regimeFiscal,
                registreCommerce, numeroBancaire, tpa, email, logo))
            {
                var result = formModifier.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    ShowTableEntrepriseGestion();
                }
            }
        }

        private void SupprimerEntreprise(int idEntreprise, string nomEntreprise)
        {
            var confirmation = CustomMessageBox.Show(
                $"Êtes-vous sûr de vouloir supprimer l'entreprise \"{nomEntreprise}\" ?\n\nCette action est irréversible.",
                "Confirmation de suppression",
                CustomMessageBox.MessageType.Warning,
                CustomMessageBox.MessageButtons.YesNo
            );

            if (confirmation == DialogResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM entreprise WHERE id_entreprise = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connect.getconnection))
                    {
                        cmd.Parameters.AddWithValue("@id", idEntreprise);
                        connect.openConnect();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        connect.closeConnect();

                        if (rowsAffected > 0)
                        {
                            CustomMessageBox.Show("Entreprise supprimée avec succès.", "Succès",
                                            CustomMessageBox.MessageType.Success);
                            ShowTableEntrepriseGestion();
                        }
                        else
                        {
                            CustomMessageBox.Show("Échec de la suppression.", "Erreur",
                                            CustomMessageBox.MessageType.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    connect.closeConnect();
                    CustomMessageBox.Show($"Erreur lors de la suppression :\n{ex.Message}", "Erreur",
                                    CustomMessageBox.MessageType.Error);
                }
            }
        }

        private void buttonAjouterEntreprise_Click(object sender, EventArgs e)
        {
            using (var formAjouter = new AjouterEntrepriseForm())
            {
                var result = formAjouter.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    ShowTableEntrepriseGestion();
                }
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string recherche = textBoxSearch.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(recherche))
            {
                ShowTableEntrepriseGestion();
            }
            else
            {
                try
                {
                    DataTable table = Entreprise.rechercheEntreprise(recherche);

                    // Copier toutes les colonnes sauf "Logo"
                    DataTable tableFiltered = new DataTable();
                    foreach (DataColumn col in table.Columns)
                    {
                        if (col.ColumnName != "Logo")
                        {
                            tableFiltered.Columns.Add(col.ColumnName, col.DataType);
                        }
                    }

                    // Copier les lignes
                    foreach (DataRow row in table.Rows)
                    {
                        DataRow newRow = tableFiltered.NewRow();
                        foreach (DataColumn col in tableFiltered.Columns)
                        {
                            newRow[col.ColumnName] = row[col.ColumnName];
                        }
                        tableFiltered.Rows.Add(newRow);
                    }

                    // Ajouter la colonne "Actions"
                    if (!tableFiltered.Columns.Contains("Actions"))
                    {
                        tableFiltered.Columns.Add("Actions", typeof(string));
                    }

                    DataGridView_Entreprise_Gestion.DataSource = tableFiltered;

                    // Masquer la colonne N° (avec symbole degré)
                    if (DataGridView_Entreprise_Gestion.Columns.Contains("N°"))
                    {
                        DataGridView_Entreprise_Gestion.Columns["N°"].Visible = false;
                    }

                    // Masquer la colonne ID
                    if (DataGridView_Entreprise_Gestion.Columns.Contains("id_entreprise"))
                    {
                        DataGridView_Entreprise_Gestion.Columns["id_entreprise"].Visible = false;
                    }

                    if (DataGridView_Entreprise_Gestion.Columns["Actions"] != null)
                    {
                        DataGridView_Entreprise_Gestion.Columns["Actions"].Width = 120;
                        DataGridView_Entreprise_Gestion.Columns["Actions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                catch (Exception ex)
                {
                    CustomMessageBox.Show($"Erreur lors de la recherche :\n{ex.Message}", "Erreur",
                                    CustomMessageBox.MessageType.Error);
                }
            }
        }
    }
}
