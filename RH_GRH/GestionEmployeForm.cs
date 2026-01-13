using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class GestionEmployeForm : Form
    {
        Dbconnect connect = new Dbconnect();

        // Variables pour suivre le bouton survolé
        private int hoverRowIndex = -1;
        private string hoverButton = "";

        public GestionEmployeForm()
        {
            InitializeComponent();

            // Attacher les gestionnaires d'événements
            DataGridView_Employe_Gestion.CellPainting += DataGridView_Employe_Gestion_CellPainting;
            DataGridView_Employe_Gestion.CellClick += DataGridView_Employe_Gestion_CellClick;
            DataGridView_Employe_Gestion.CellMouseMove += DataGridView_Employe_Gestion_CellMouseMove;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;

            ShowTableEmployeGestion();

            // Styliser le header avec un design moderne
            StyliserHeader();
        }

        private void StyliserHeader()
        {
            // Configurer le panel2 (header)
            panel2.Height = 70;
            panel2.Paint += (s, e) =>
            {
                // Dégradé corporate élégant - Cyan/Teal professionnel
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    panel2.ClientRectangle,
                    Color.FromArgb(20, 120, 140),   // Cyan profond
                    Color.FromArgb(35, 160, 180),   // Teal moderne
                    System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, panel2.ClientRectangle);
                }

                // Barre accent subtile
                using (var accentBrush = new SolidBrush(Color.FromArgb(55, 200, 220)))
                {
                    e.Graphics.FillRectangle(accentBrush, 0, panel2.Height - 3, panel2.Width, 3);
                }
            };

            // Styliser label1 (titre principal)
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Montserrat", 15F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Text = "EMPLOYÉS";
            label1.Padding = new Padding(70, 0, 0, 0);  // Espace pour l'icône
            label1.TextAlign = ContentAlignment.MiddleLeft;
            label1.Dock = DockStyle.Fill;
            label1.AutoSize = false;

            // Dessiner une icône personnalisée
            label1.Paint += (s, e) =>
            {
                // Dessiner l'icône personne/employé
                int iconSize = 28;
                int iconX = 30;
                int iconY = (label1.Height - iconSize) / 2;

                using (var brush = new SolidBrush(Color.FromArgb(55, 200, 220)))
                {
                    // Tête (cercle)
                    int headRadius = 7;
                    e.Graphics.FillEllipse(brush, iconX + 7, iconY + 2, headRadius * 2, headRadius * 2);

                    // Corps (trapèze pour silhouette)
                    Point[] body = new Point[]
                    {
                        new Point(iconX + 7, iconY + 16),      // Épaule gauche
                        new Point(iconX + 21, iconY + 16),     // Épaule droite
                        new Point(iconX + 24, iconY + iconSize - 2), // Bas droite
                        new Point(iconX + 4, iconY + iconSize - 2)   // Bas gauche
                    };
                    e.Graphics.FillPolygon(brush, body);
                }
            };

            panel2.Invalidate();
        }

        // Méthode publique pour charger les employés dans un ComboBox (utilisée par d'autres formulaires)
        public static void ChargerPersonnels(Guna.UI2.WinForms.Guna2ComboBox combo, int? idSelection = null, bool ajouterPlaceholder = false)
        {
            try
            {
                DataTable dt = EmployeClass.GetEmployeList();
                DataTable filtered = new DataTable();
                filtered.Columns.Add("id_personnel", typeof(int));
                filtered.Columns.Add("nomPrenom", typeof(string));

                if (ajouterPlaceholder)
                {
                    DataRow placeholderRow = filtered.NewRow();
                    placeholderRow["id_personnel"] = 0;
                    placeholderRow["nomPrenom"] = "-- Sélectionner --";
                    filtered.Rows.Add(placeholderRow);
                }

                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = filtered.NewRow();
                    newRow["id_personnel"] = row["Id"];
                    newRow["nomPrenom"] = row["Nom Prenom"];
                    filtered.Rows.Add(newRow);
                }

                combo.DataSource = filtered;
                combo.DisplayMember = "nomPrenom";
                combo.ValueMember = "id_personnel";

                if (idSelection.HasValue && idSelection.Value > 0)
                {
                    combo.SelectedValue = idSelection.Value;
                }
                else if (ajouterPlaceholder)
                {
                    combo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement des employés :\n{ex.Message}", "Erreur",
                    CustomMessageBox.MessageType.Error);
            }
        }

        public void ShowTableEmployeGestion()
        {
            try
            {
                DataTable table = EmployeClass.GetEmployeList();

                // Ajouter la colonne "Actions" en tant que texte
                if (!table.Columns.Contains("Actions"))
                {
                    table.Columns.Add("Actions", typeof(string));
                }

                DataGridView_Employe_Gestion.DataSource = table;

                // Masquer la colonne ID (alias Id dans le SELECT)
                if (DataGridView_Employe_Gestion.Columns.Contains("Id"))
                {
                    DataGridView_Employe_Gestion.Columns["Id"].Visible = false;
                }

                // Masquer la colonne ID Entreprise si présente
                if (DataGridView_Employe_Gestion.Columns.Contains("ID Entreprise"))
                {
                    DataGridView_Employe_Gestion.Columns["ID Entreprise"].Visible = false;
                }

                // Configurer la colonne Actions
                if (DataGridView_Employe_Gestion.Columns["Actions"] != null)
                {
                    DataGridView_Employe_Gestion.Columns["Actions"].Width = 180;
                    DataGridView_Employe_Gestion.Columns["Actions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DataGridView_Employe_Gestion.Columns["Actions"].HeaderText = "Actions";
                }

                // Masquer les colonnes techniques si nécessaires
                DataGridView_Employe_Gestion.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement des employés :\n{ex.Message}", "Erreur",
                                CustomMessageBox.MessageType.Error);
            }
        }

        private void DataGridView_Employe_Gestion_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            if (DataGridView_Employe_Gestion.Columns[e.ColumnIndex].Name == "Actions")
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
                Color modifierColor = modifierHover ? Color.FromArgb(255, 179, 0) : Color.FromArgb(255, 193, 7); // Or

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
                Color supprimerColor = supprimerHover ? Color.FromArgb(211, 47, 47) : Color.FromArgb(244, 67, 54); // Rouge

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

        private void DataGridView_Employe_Gestion_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (DataGridView_Employe_Gestion.Columns[e.ColumnIndex].Name == "Actions")
            {
                var cellBounds = DataGridView_Employe_Gestion.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                int buttonWidth = 80;
                int buttonHeight = 32;
                int buttonSpacing = 5;
                int totalWidth = (buttonWidth * 2) + buttonSpacing;
                int startX = cellBounds.X + (cellBounds.Width - totalWidth) / 2;
                int startY = cellBounds.Y + (cellBounds.Height - buttonHeight) / 2;

                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                Rectangle btnSupprimer = new Rectangle(startX + buttonWidth + buttonSpacing, startY, buttonWidth, buttonHeight);

                Point mousePos = DataGridView_Employe_Gestion.PointToClient(Cursor.Position);

                string newHoverButton = "";
                if (btnModifier.Contains(mousePos.X, mousePos.Y))
                    newHoverButton = "Modifier";
                else if (btnSupprimer.Contains(mousePos.X, mousePos.Y))
                    newHoverButton = "Supprimer";

                if (hoverRowIndex != e.RowIndex || hoverButton != newHoverButton)
                {
                    hoverRowIndex = e.RowIndex;
                    hoverButton = newHoverButton;
                    DataGridView_Employe_Gestion.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    DataGridView_Employe_Gestion.Cursor = string.IsNullOrEmpty(newHoverButton) ? Cursors.Default : Cursors.Hand;
                }
            }
        }

        private void DataGridView_Employe_Gestion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (DataGridView_Employe_Gestion.Columns[e.ColumnIndex].Name == "Actions")
            {
                // Récupérer l'ID de l'employé depuis la colonne Id
                var cellValue = DataGridView_Employe_Gestion.Rows[e.RowIndex].Cells["Id"].Value;
                if (cellValue == null || cellValue == DBNull.Value)
                {
                    CustomMessageBox.Show("Impossible de récupérer l'ID de l'employé.", "Erreur",
                                    CustomMessageBox.MessageType.Error);
                    return;
                }

                int idPersonnel = Convert.ToInt32(cellValue);

                // Déterminer quel bouton a été cliqué
                Point mousePos = DataGridView_Employe_Gestion.PointToClient(Cursor.Position);
                var cellBounds = DataGridView_Employe_Gestion.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                int buttonWidth = 80;
                int buttonHeight = 32;
                int buttonSpacing = 5;
                int totalWidth = (buttonWidth * 2) + buttonSpacing;
                int startX = cellBounds.X + (cellBounds.Width - totalWidth) / 2;
                int startY = cellBounds.Y + (cellBounds.Height - buttonHeight) / 2;

                Rectangle btnModifier = new Rectangle(startX, startY, buttonWidth, buttonHeight);
                Rectangle btnSupprimer = new Rectangle(startX + buttonWidth + buttonSpacing, startY, buttonWidth, buttonHeight);

                // Bouton Modifier cliqué
                if (btnModifier.Contains(mousePos.X, mousePos.Y))
                {
                    try
                    {
                        // Récupérer toutes les données de l'employé
                        string query = @"SELECT p.*, e.nomEntreprise
                                       FROM personnel p
                                       LEFT JOIN entreprise e ON p.id_entreprise = e.id_entreprise
                                       WHERE p.id_personnel = @id";

                        using (MySqlCommand cmd = new MySqlCommand(query, connect.getconnection))
                        {
                            cmd.Parameters.AddWithValue("@id", idPersonnel);
                            connect.openConnect();

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Récupérer toutes les données
                                    string nomPrenom = reader["nomPrenom"]?.ToString();
                                    string matricule = reader["matricule"]?.ToString();
                                    string civilite = reader["civilite"]?.ToString();
                                    string sexe = reader["sexe"]?.ToString();
                                    DateTime? dateNaissance = reader["date_naissance"] != DBNull.Value ? Convert.ToDateTime(reader["date_naissance"]) : (DateTime?)null;
                                    string adresse = reader["adresse"]?.ToString();
                                    string telephone = reader["telephone"]?.ToString();
                                    string identification = reader["identification"]?.ToString();

                                    int idEntreprise = Convert.ToInt32(reader["id_entreprise"]);
                                    int? idDirection = reader["id_direction"] != DBNull.Value ? Convert.ToInt32(reader["id_direction"]) : (int?)null;
                                    int? idService = reader["id_service"] != DBNull.Value ? Convert.ToInt32(reader["id_service"]) : (int?)null;
                                    int idCategorie = Convert.ToInt32(reader["id_categorie"]);

                                    string poste = reader["poste"]?.ToString();
                                    string cnss = reader["numerocnss"]?.ToString();
                                    string contrat = reader["contrat"]?.ToString();
                                    string typeContrat = reader["typeContrat"]?.ToString();
                                    string modePayement = reader["modePayement"]?.ToString();
                                    string cadre = reader["cadre"]?.ToString();

                                    DateTime dateEntree = Convert.ToDateTime(reader["date_entree"]);
                                    DateTime? dateSortie = reader["date_sortie"] != DBNull.Value ? Convert.ToDateTime(reader["date_sortie"]) : (DateTime?)null;

                                    decimal? heureContrat = reader["heureContrat"] != DBNull.Value ? Convert.ToDecimal(reader["heureContrat"]) : (decimal?)null;
                                    int? jourContrat = reader["jourContrat"] != DBNull.Value ? Convert.ToInt32(reader["jourContrat"]) : (int?)null;

                                    string numeroBancaire = reader["numeroBancaire"]?.ToString();
                                    string banque = reader["banque"]?.ToString();
                                    decimal? salaireMoyen = reader["salairemoyen"] != DBNull.Value ? Convert.ToDecimal(reader["salairemoyen"]) : (decimal?)null;

                                    connect.closeConnect();

                                    // Ouvrir le formulaire de modification
                                    ModifierEmployeAvecModale(
                                        idPersonnel, nomPrenom, matricule, civilite, sexe, dateNaissance,
                                        adresse, telephone, identification, idEntreprise, idDirection, idService,
                                        idCategorie, poste, cnss, contrat, typeContrat, modePayement, cadre,
                                        dateEntree, dateSortie, heureContrat, jourContrat, numeroBancaire, banque, salaireMoyen);
                                }
                                else
                                {
                                    connect.closeConnect();
                                    CustomMessageBox.Show("Employé introuvable.", "Erreur",
                                        CustomMessageBox.MessageType.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        connect.closeConnect();
                        CustomMessageBox.Show(
                            $"Erreur lors de la récupération des données de l'employé\n\n" +
                            $"Détails de l'erreur :\n{ex.Message}",
                            "Erreur",
                            CustomMessageBox.MessageType.Error);
                    }
                }
                // Bouton Supprimer cliqué
                else if (btnSupprimer.Contains(mousePos.X, mousePos.Y))
                {
                    // Récupérer le nom de l'employé pour le message de confirmation
                    string nomPrenom = DataGridView_Employe_Gestion.Rows[e.RowIndex].Cells["Nom Prenom"].Value?.ToString();
                    string matricule = DataGridView_Employe_Gestion.Rows[e.RowIndex].Cells["Matricule"].Value?.ToString();

                    // Demander confirmation
                    var result = CustomMessageBox.Show(
                        $"Êtes-vous sûr de vouloir supprimer cet employé ?\n\n" +
                        $"Nom : {nomPrenom}\n" +
                        $"Matricule : {matricule}\n\n" +
                        $"Cette action est irréversible.",
                        "Confirmer la suppression",
                        CustomMessageBox.MessageType.Warning,
                        CustomMessageBox.MessageButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            string query = "DELETE FROM personnel WHERE id_personnel = @id";
                            using (MySqlCommand cmd = new MySqlCommand(query, connect.getconnection))
                            {
                                cmd.Parameters.AddWithValue("@id", idPersonnel);
                                connect.openConnect();
                                int rowsAffected = cmd.ExecuteNonQuery();
                                connect.closeConnect();

                                if (rowsAffected > 0)
                                {
                                    CustomMessageBox.Show(
                                        $"L'employé {nomPrenom} a été supprimé avec succès.",
                                        "Succès",
                                        CustomMessageBox.MessageType.Success);

                                    // Rafraîchir la liste
                                    ShowTableEmployeGestion();
                                }
                                else
                                {
                                    CustomMessageBox.Show("Aucun employé n'a été supprimé.", "Information",
                                                    CustomMessageBox.MessageType.Info);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            connect.closeConnect();
                            CustomMessageBox.Show(
                                $"Erreur lors de la suppression de l'employé\n\n" +
                                $"Détails de l'erreur :\n{ex.Message}",
                                "Erreur",
                                CustomMessageBox.MessageType.Error);
                        }
                    }
                }
            }
        }

        private void ModifierEmployeAvecModale(
            int idPersonnel, string nomPrenom, string matricule, string civilite, string sexe,
            DateTime? dateNaissance, string adresse, string telephone, string identification,
            int idEntreprise, int? idDirection, int? idService, int idCategorie,
            string poste, string cnss, string contrat, string typeContrat, string modePayement, string cadre,
            DateTime dateEntree, DateTime? dateSortie, decimal? heureContrat, int? jourContrat,
            string numeroBancaire, string banque, decimal? salaireMoyen)
        {
            using (var formModifier = new ModifierEmployeForm(
                idPersonnel, nomPrenom, matricule, civilite, sexe, dateNaissance,
                adresse, telephone, identification, idEntreprise, idDirection, idService,
                idCategorie, poste, cnss, contrat, typeContrat, modePayement, cadre,
                dateEntree, dateSortie, heureContrat, jourContrat, numeroBancaire, banque, salaireMoyen))
            {
                var result = formModifier.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    ShowTableEmployeGestion();
                }
            }
        }

        private void buttonAjouterEmploye_Click(object sender, EventArgs e)
        {
            using (var formAjouter = new AjouterEmployeForm())
            {
                var result = formAjouter.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    ShowTableEmployeGestion();
                }
            }
        }

        private void buttonImporterExcel_Click(object sender, EventArgs e)
        {
            using (var importForm = new ImportEmployeForm())
            {
                importForm.ShowDialog(this);
                // Rafraîchir la liste après l'importation
                ShowTableEmployeGestion();
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string recherche = textBoxSearch.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(recherche))
            {
                ShowTableEmployeGestion();
            }
            else
            {
                try
                {
                    DataTable table = EmployeClass.RechercheEmploye(recherche);

                    // Ajouter la colonne "Actions"
                    if (!table.Columns.Contains("Actions"))
                    {
                        table.Columns.Add("Actions", typeof(string));
                    }

                    DataGridView_Employe_Gestion.DataSource = table;

                    // Masquer la colonne Id
                    if (DataGridView_Employe_Gestion.Columns.Contains("Id"))
                    {
                        DataGridView_Employe_Gestion.Columns["Id"].Visible = false;
                    }

                    // Masquer la colonne ID Entreprise
                    if (DataGridView_Employe_Gestion.Columns.Contains("ID Entreprise"))
                    {
                        DataGridView_Employe_Gestion.Columns["ID Entreprise"].Visible = false;
                    }

                    if (DataGridView_Employe_Gestion.Columns["Actions"] != null)
                    {
                        DataGridView_Employe_Gestion.Columns["Actions"].Width = 180;
                        DataGridView_Employe_Gestion.Columns["Actions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                catch (Exception ex)
                {
                    CustomMessageBox.Show($"Erreur lors de la recherch" +
                        $"e :\n{ex.Message}", "Erreur",
         
                        
                        CustomMessageBox.MessageType.Error);
                }
            }
        }

        private void DataGridView_Employe_Gestion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
