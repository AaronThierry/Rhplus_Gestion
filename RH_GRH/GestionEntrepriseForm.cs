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

        public GestionEntrepriseForm()
        {
            InitializeComponent();

            // Attacher les gestionnaires d'événements
            DataGridView_Entreprise_Gestion.CellPainting += DataGridView_Entreprise_Gestion_CellPainting;
            DataGridView_Entreprise_Gestion.CellClick += DataGridView_Entreprise_Gestion_CellClick;
            DataGridView_Entreprise_Gestion.CellMouseMove += DataGridView_Entreprise_Gestion_CellMouseMove;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;

            ShowTableEntrepriseGestion();
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

                // Configurer la colonne Actions
                if (DataGridView_Entreprise_Gestion.Columns["Actions"] != null)
                {
                    DataGridView_Entreprise_Gestion.Columns["Actions"].Width = 180;
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
            if (e.RowIndex < 0) return;

            if (DataGridView_Entreprise_Gestion.Columns[e.ColumnIndex].Name == "Actions")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                // Zone de dessin
                Rectangle rect = e.CellBounds;
                int padding = 5;
                int buttonWidth = 80;
                int buttonHeight = 28;
                int buttonY = rect.Y + (rect.Height - buttonHeight) / 2;

                // Bouton "Modifier" (or/doré)
                Rectangle btnModifier = new Rectangle(rect.X + padding, buttonY, buttonWidth, buttonHeight);
                using (GraphicsPath path = GraphicsExtensions.CreateRoundedRectanglePath(btnModifier, 6))
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(218, 165, 32))) // Or/Doré
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.FillPath(brush, path);

                    // Texte du bouton
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    using (Font font = new Font("Montserrat", 8f, FontStyle.Bold))
                    {
                        e.Graphics.DrawString("Modifier", font, Brushes.White, btnModifier, sf);
                    }
                }

                // Bouton "Supprimer" (rouge)
                Rectangle btnSupprimer = new Rectangle(
                    btnModifier.Right + padding,
                    buttonY,
                    buttonWidth,
                    buttonHeight
                );
                using (GraphicsPath path = GraphicsExtensions.CreateRoundedRectanglePath(btnSupprimer, 6))
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(220, 53, 69))) // Rouge
                {
                    e.Graphics.FillPath(brush, path);

                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    using (Font font = new Font("Montserrat", 8f, FontStyle.Bold))
                    {
                        e.Graphics.DrawString("Supprimer", font, Brushes.White, btnSupprimer, sf);
                    }
                }

                e.Handled = true;
            }
        }

        private void DataGridView_Entreprise_Gestion_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (DataGridView_Entreprise_Gestion.Columns[e.ColumnIndex].Name == "Actions")
                {
                    DataGridView_Entreprise_Gestion.Cursor = Cursors.Hand;
                }
                else
                {
                    DataGridView_Entreprise_Gestion.Cursor = Cursors.Default;
                }
            }
        }

        private void DataGridView_Entreprise_Gestion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (DataGridView_Entreprise_Gestion.Columns[e.ColumnIndex].Name == "Actions")
            {
                // Récupérer l'ID de l'entreprise
                var cellValue = DataGridView_Entreprise_Gestion.Rows[e.RowIndex].Cells[0].Value;
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

                                // Déterminer le bouton cliqué
                                Rectangle cellRect = DataGridView_Entreprise_Gestion.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                                Point clickPoint = DataGridView_Entreprise_Gestion.PointToClient(Cursor.Position);
                                int relativeX = clickPoint.X - cellRect.Left;

                                int padding = 5;
                                int buttonWidth = 80;
                                int btnModifierEnd = padding + buttonWidth;
                                int btnSupprimerStart = btnModifierEnd + padding;

                                if (relativeX >= padding && relativeX <= btnModifierEnd)
                                {
                                    // Bouton "Modifier" cliqué
                                    ModifierEntrepriseAvecModale(
                                        idEntreprise, nomEntreprise, formeJuridique, sigle, activite,
                                        adressePhysique, adressePostale, telephone, commune, quartier,
                                        rue, lot, centreImpots, numeroIfu, numeroCnss, codeActivite,
                                        regimeFiscal, registreCommerce, numeroBancaire, tpa, email, logo
                                    );
                                }
                                else if (relativeX >= btnSupprimerStart && relativeX <= btnSupprimerStart + buttonWidth)
                                {
                                    // Bouton "Supprimer" cliqué
                                    SupprimerEntreprise(idEntreprise, nomEntreprise);
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

                    if (DataGridView_Entreprise_Gestion.Columns["Actions"] != null)
                    {
                        DataGridView_Entreprise_Gestion.Columns["Actions"].Width = 180;
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
