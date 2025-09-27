using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        Categorie Categorie = new Categorie();
        Dbconnect connect = new Dbconnect();
        public GestionCategorieForm()
        {
            InitializeComponent();
            StyliserDataGridView();
            StyliserDataGridViewGestion();
            StyliserTabControl();
            ShowTableCategorie();
            ShowTableCategorieGestion();
        }


        ///////////////////////////////////////////////////////////////////////////
        private void tabControlService_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlCategorie.SelectedIndex == 1) // Onglet n°2 (index 1)
            {
                ChargerTablePage2();
                EntrepriseClass.ChargerEntreprises(ComboBoxEntrepriseGestion);
                StyliserDataGridViewGestion();
                ShowTableCategorieGestion();
                ClearCategorieForm();
            }
            else if (tabControlCategorie.SelectedIndex == 0)
            {
                ClearCategorieForm();
                ShowTableCategorie();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        private bool tablePage2Chargee = false;

        ///////////////////////////////////////////////////////////////////////////
        private void ChargerTablePage2(bool forcer = false)
        {
            if (tablePage2Chargee && !forcer) return;

            try
            {
                EntrepriseClass.ChargerEntreprises(ComboBoxEntrepriseGestion);
                UseWaitCursor = true;

                // 1) Récupération des données
                DataTable dt = Categorie.GetCategorieList() ?? new DataTable();

                // 2) Tri par défaut si dispo
                if (dt.Columns.Contains("Entreprise") && dt.Columns.Contains("Direction"))
                {
                    var dv = dt.DefaultView;
                    dv.Sort = "[Entreprise] ASC, [Direction] ASC";
                    dt = dv.ToTable();
                }

                // 3) Binding
                var grid = DataGridView_Categorie_Gestion;
                grid.AutoGenerateColumns = true;
                grid.DataSource = dt;

                // (optionnel) masquer les IDs si présents
                if (grid.Columns.Contains("ID")) grid.Columns["ID"].Visible = false;
                if (grid.Columns.Contains("ID Entreprise")) grid.Columns["ID Entreprise"].Visible = false;

                // Style perso si tu as une méthode dédiée
                StyliserDataGridView();

                tablePage2Chargee = true;
                EntrepriseClass.ChargerEntreprises(ComboBoxEntreprise);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de chargement : " + ex.Message, "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        ///////////////////////////////////////////////////////////////////////////

        private void ClearCategorieForm()
        {
            textBoxNomCategorie.Clear();
            ComboBoxEntreprise.SelectedIndex = 0;
            textBoxMontant.Clear();
        }

        ////////////////////////////////////////////////////////////////////////////
        private void ClearCategorieFormGestion()
        {
            textBoxCategorieGestion.Clear();
            ComboBoxEntrepriseGestion.SelectedIndex = 0;
            textBoxMontantGestion.Clear();
            textBoxID.Clear();
        }

        ////////////////////////////////////////////////////////////////////////////

        private void ShowTableCategorie()
        {
            var dt = Categorie.GetCategorieList();
            DataGridView_Categorie.AutoGenerateColumns = true;
            DataGridView_Categorie.DataSource = dt;
            // Masquer les IDs si tu veux une vue clean
            if (DataGridView_Categorie.Columns.Contains("ID"))
                DataGridView_Categorie.Columns["ID"].Visible = false;
        }

        ///////////////////////////////////////////////////////////////////////////
        private void ShowTableCategorieGestion()
        {
            EntrepriseClass.ChargerEntreprises(ComboBoxEntreprise);
            var dt = Categorie.GetCategorieList();
            DataGridView_Categorie_Gestion.AutoGenerateColumns = true;
            DataGridView_Categorie_Gestion.DataSource = dt;
            // Masquer les IDs si tu veux une vue clean
            if (DataGridView_Categorie_Gestion.Columns.Contains("ID"))
                DataGridView_Categorie.Columns["ID"].Visible = false;
        }

        ///////////////////////////////////////////////////////////////////////////
        private void StyliserDataGridView()
        {
            // Fond général de la grille
            DataGridView_Categorie.BackgroundColor = Color.White;

            // Désactiver le style visuel Windows
            DataGridView_Categorie.EnableHeadersVisualStyles = false;

            // Style de l'en-tête
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.MidnightBlue;
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Montserrat", 10f, FontStyle.Bold);
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            headerStyle.WrapMode = DataGridViewTriState.True;
            headerStyle.SelectionBackColor = Color.MidnightBlue;     // ← Empêche le changement au clic
            headerStyle.SelectionForeColor = Color.White;            // ← Texte toujours blanc

            DataGridView_Categorie.EnableHeadersVisualStyles = false;
            DataGridView_Categorie.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridView_Categorie.ColumnHeadersHeight = 35;

            // Style des cellules normales
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.BackColor = Color.White;
            cellStyle.ForeColor = Color.Black;
            cellStyle.Font = new Font("Montserrat", 9.5f, FontStyle.Regular);
            cellStyle.SelectionBackColor = Color.LightSteelBlue;
            cellStyle.SelectionForeColor = Color.Black;
            DataGridView_Categorie.DefaultCellStyle = cellStyle;

            // Style des lignes alternées
            DataGridView_Categorie.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;

            // Supprimer les bordures
            DataGridView_Categorie.BorderStyle = BorderStyle.None;
            DataGridView_Categorie.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Categorie.GridColor = Color.LightGray;

            // Masquer l'entête de ligne à gauche
            DataGridView_Categorie.RowHeadersVisible = false;

            // Autres options d’esthétique
            DataGridView_Categorie.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Categorie.MultiSelect = false;
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
                SelectionBackColor = Color.MidnightBlue, // Empêche changement au clic
                SelectionForeColor = Color.White
            };

            DataGridView_Categorie_Gestion.EnableHeadersVisualStyles = false;
            DataGridView_Categorie_Gestion.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridView_Categorie_Gestion.ColumnHeadersHeight = 35;

            // Cellules normales
            var cellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Montserrat", 9.5f),
                SelectionBackColor = Color.LightSteelBlue,
                SelectionForeColor = Color.Black
            };
            DataGridView_Categorie_Gestion.DefaultCellStyle = cellStyle;

            // Lignes alternées
            DataGridView_Categorie_Gestion.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;

            // Bordures & style
            DataGridView_Categorie_Gestion.BorderStyle = BorderStyle.None;
            DataGridView_Categorie_Gestion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Categorie_Gestion.GridColor = Color.LightGray;
            DataGridView_Categorie_Gestion.RowHeadersVisible = false;

            // Sélection
            DataGridView_Categorie_Gestion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Categorie_Gestion.MultiSelect = false;
        }

        ////////////////////////////////////////////////////////////////////////////

        private void StyliserTabControl()
        {
            tabControlCategorie.Appearance = TabAppearance.Normal;
            tabControlCategorie.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControlCategorie.ItemSize = new Size(150, 35); // largeur, hauteur des onglets
            tabControlCategorie.SizeMode = TabSizeMode.Fixed;
            tabControlCategorie.DrawItem += TabControlEntreprise_DrawItem;
        }

        ////////////////////////////////////////////////////////////////////////////

        private void TabControlEntreprise_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControlCategorie.TabPages[e.Index];
            Rectangle rect = tabControlCategorie.GetTabRect(e.Index);
            bool isSelected = (e.Index == tabControlCategorie.SelectedIndex);

            // Couleur de fond
            Color backColor = isSelected ? Color.MidnightBlue : Color.LightGray;
            Color foreColor = isSelected ? Color.White : Color.Black;

            using (SolidBrush brush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(brush, rect);
            }

            // Texte centré

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            using (Font font = new Font("Montserrat", 10f, FontStyle.Bold))
            using (Brush textBrush = new SolidBrush(foreColor))
            {
                e.Graphics.DrawString(page.Text, font, textBrush, rect, format);
            }
        }

        ///////////////////////////////////////////////////////////////////////////
        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        //////////////////////////////////////////////////////////////////////////

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        //////////////////////////////////////////////////////////////////////////

        private static bool TryParseMontant(string input, out decimal montant)
        {
            montant = 0m;
            if (string.IsNullOrWhiteSpace(input)) return false;

            // 1) Normaliser : retirer espaces (normales, insécables, fines) et lettres (ex. "fcfa")
            string s = input.Trim();

            // Supprimer différents types d'espaces fréquemment utilisés comme séparateurs de milliers
            s = s.Replace(" ", "")
                 .Replace("\u00A0", "") // NBSP
                 .Replace("\u202F", "") // NARROW NBSP (fr souvent)
                 .Replace("\u2009", "") // THIN SPACE
                 .Replace("\u2007", ""); // FIGURE SPACE

            // Retirer toute lettre/symbole monétaire éventuel (FCFA, f, cfa, etc.)
            s = new string(s.Where(ch => char.IsDigit(ch) || ch == ',' || ch == '.').ToArray());

            // 2) Gérer les cas avec virgule et/ou point
            int lastComma = s.LastIndexOf(',');
            int lastDot = s.LastIndexOf('.');
            char dec = '\0';
            if (lastComma >= 0 || lastDot >= 0)
                dec = (lastComma > lastDot) ? ',' : '.';

            // Si les deux existent, on considère le dernier comme séparateur décimal
            if (dec != '\0')
            {
                // Supprimer l’autre séparateur (servait aux milliers)
                char other = (dec == ',') ? '.' : ',';
                s = s.Replace(other.ToString(), "");
                // Uniformiser en point pour InvariantCulture
                if (dec == ',') s = s.Replace(',', '.');
            }

            // 3) Parse en invariant (plus prévisible)
            return decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out montant);
        }

        ///////////////////////////////////////////////////////////////////////////
        private void buttonAjouter_Click(object sender, EventArgs e)
    {
        // 1) Récupération + validations
        string nomService = textBoxNomCategorie.Text?.Trim();
        if (string.IsNullOrWhiteSpace(nomService))
        {
            MessageBox.Show("Veuillez saisir le nom du service.", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBoxNomCategorie.Focus();
            return;
        }

            decimal montant;
            if (!TryParseMontant(textBoxMontant.Text, out montant) || montant < 0m)
            {
                MessageBox.Show("Veuillez saisir un montant valide (nombre positif).",
                                "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxMontant.Focus();
                textBoxMontant.SelectAll();
                return;
            }

            int? idEntreprise = EntrepriseClass.GetIdEntrepriseSelectionnee(ComboBoxEntreprise);
        if (!idEntreprise.HasValue)
        {
            MessageBox.Show("Veuillez sélectionner une entreprise.", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            ComboBoxEntreprise.DroppedDown = true;
            return;
        }

        // 2) Exécution
        try
        {
            UseWaitCursor = true;
            buttonAjouter.Enabled = false;

            // ⚠️ Assure-toi que la méthode attend bien (string nom, decimal montant, int idEntreprise)
            Categorie.EnregistrerCategorie(nomService, montant, idEntreprise.Value);

            // Rafraîchir l'affichage
            ShowTableCategorie();

                // 3) Reset UI (on garde l’entreprise sélectionnée)
                ClearCategorieForm();
                ComboBoxEntreprise.Focus();
            // Si tu préfères vider le combo :
            // ComboBoxEntreprise.SelectedIndex = -1;
            // ComboBoxEntreprise.Text = string.Empty;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erreur inattendue : " + ex.Message, "Erreur",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            UseWaitCursor = false;
            buttonAjouter.Enabled = true;
        }
    }

        ////////////////////////////////////////////////////////////////////////////
        private void buttonEffacer_Click(object sender, EventArgs e)
        {
            ClearCategorieForm();
        }

        ////////////////////////////////////////////////////////////////////////////

        private void ChargerDetailsCategorieParId(string id)
        {
            try
            {
                // Jointure pour récupérer aussi le nom de l'entreprise
                const string sql = @"
            SELECT 
                d.nomCategorie,
                d.montant,
                d.id_entreprise,
                e.nomEntreprise
            FROM categorie d
            INNER JOIN entreprise e ON e.id_entreprise = d.id_entreprise
            WHERE d.id_categorie = @id;";

                // ⚠️ IMPORTANT :
                // - Si getconnection retourne MySqlConnection, utilisez .ConnectionString :
                using (var con = new MySqlConnection(connect.getconnection.ConnectionString))
                // - Si getconnection retourne une chaîne de connexion, utilisez :
                // using (var con = new MySqlConnection(connect.getconnection))
                using (var cmd = new MySqlCommand(sql, con))
                {
                    // id numérique en base : on paramètre en Int32 (même si on reçoit une string)
                    if (!int.TryParse(id, out int idCategorie))
                    {
                        MessageBox.Show("Identifiant de la categorie invalide.");
                        return;
                    }

                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = idCategorie;

                    con.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                        {
                            // Nom de la direction
                            textBoxCategorieGestion.Text = reader["nomCategorie"]?.ToString();
                            textBoxMontantGestion.Text = reader["montant"]?.ToString();

                            // Sélectionner l'entreprise dans le ComboBox (s'il est déjà bindé)
                            if (!reader.IsDBNull(reader.GetOrdinal("id_entreprise")))
                            {
                                int idEnt = reader.GetInt32(reader.GetOrdinal("id_entreprise"));

                                // Si le Combo n'est pas encore chargé, on peut le charger ici (optionnel)
                                if (ComboBoxEntrepriseGestion.DataSource == null)
                                {
                                    EntrepriseClass.ChargerEntreprises(ComboBoxEntrepriseGestion, idEnt, ajouterPlaceholder: true);
                                }
                                else
                                {
                                    ComboBoxEntrepriseGestion.SelectedValue = idEnt;
                                }
                            }

                            // (Optionnel) Afficher le nom de l'entreprise dans un label si tu en as un
                            // labelEntreprise.Text = reader["nomEntreprise"]?.ToString();
                        }
                        else
                        {
                            // Rien trouvé : on nettoie les champs
                            ClearCategorieFormGestion();
                            if (ComboBoxEntreprise.DataSource != null)
                                ComboBoxEntreprise.SelectedIndex = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur chargement infos direction : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        private void DataGridView_Categorie_Gestion_Click(object sender, EventArgs e)
        {
            if (DataGridView_Categorie_Gestion.CurrentRow != null)
            {
                string id = DataGridView_Categorie_Gestion.CurrentRow.Cells[0].Value?.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    textBoxID.Text = id;
                    ChargerDetailsCategorieParId(id);
                    ComboBoxEntrepriseGestion.Enabled = false;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        private void buttonModifier_Click(object sender, EventArgs e)
    {
        // 1) Validations rapides
        string nomCategorie = textBoxCategorieGestion.Text?.Trim();
        if (string.IsNullOrWhiteSpace(nomCategorie))
        {
            MessageBox.Show("Veuillez saisir le nom de la catégorie.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBoxCategorieGestion.Focus();
            return;
        }

        if (!int.TryParse(textBoxID.Text?.Trim(), out int idCategorie) || idCategorie <= 0)
        {
            MessageBox.Show("ID catégorie invalide.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        // Montant
        if (!TryParseMontant(textBoxMontantGestion.Text, out decimal montant) || montant < 0m)
        {
            MessageBox.Show("Veuillez saisir un montant valide (nombre positif).", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBoxMontantGestion.Focus();
            textBoxMontantGestion.SelectAll();
            return;
        }

        // 2) Confirmation
        var confirm = MessageBox.Show(
            $"Confirmer la modification de la catégorie #{idCategorie} en « {nomCategorie} » (montant : {montant:N0}) ?",
            "Confirmation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2);

        if (confirm != DialogResult.Yes) return;

        // 3) Exécution + actualisation
        try
        {
            buttonModifier.Enabled = false;
            UseWaitCursor = true;

                // Appelle ta méthode métier pour modifier la catégorie (adapter le nom si besoin)
                Categorie.ModifierCategorie(idCategorie, nomCategorie, montant);
                // Si ta logique exige aussi l'entreprise, ajoute idEntreprise:
                // int? idEntreprise = EntrepriseClass.GetIdEntrepriseSelectionnee(ComboBoxEntreprise);
                // CategorieClass.ModifierCategorie(idCategorie, nomCategorie, montant, idEntreprise.Value);

                // Rafraîchir l'affichage + nettoyage
                ShowTableCategorieGestion();
                ClearCategorieFormGestion();

            // (Optionnel) Re-sélectionner la ligne modifiée si souhaité
            // ReSelectRowById(DataGridView_Categorie_Gestion, "ID", idCategorie);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erreur lors de la modification : " + ex.Message, "Erreur",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            UseWaitCursor = false;
            buttonModifier.Enabled = true;
        }
    }

        private void button_Supprimer_Click(object sender, EventArgs e)
        {
            // Récupère l'ID (depuis la grille ou un TextBox)
            if (!int.TryParse(textBoxID.Text?.Trim(), out int idCategorie) || idCategorie <= 0)
            {
                MessageBox.Show("Veuillez sélectionner une categorie valide.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Confirmation
            var confirm = MessageBox.Show(
                $"Supprimer définitivement la categorie #{idCategorie} ?",
                "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (confirm != DialogResult.Yes) return;

            try
            {
                UseWaitCursor = true;
                button_Supprimer.Enabled = false;

                if (Categorie.SupprimerCategorie(idCategorie))
                {
                    // Rafraîchir la table après suppression
                    ShowTableCategorieGestion();
                    ClearCategorieFormGestion();
                }
            }
            finally
            {
                button_Supprimer.Enabled = true;
                UseWaitCursor = false;
            }
        }

        private void buttonRechercher_Click(object sender, EventArgs e)
        {
            DataGridView_Categorie_Gestion.DataSource = Categorie.RechercheCategorieConcat(textBoxSearch.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void GestionCategorieForm_Load(object sender, EventArgs e)
        {

        }

        // Helper pour re-sélectionner une ligne par ID

    }
}

