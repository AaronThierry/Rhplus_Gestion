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

namespace RH_GRH
{
    public partial class GestionIndemniteForm : Form
    {
        public GestionIndemniteForm()
        {
            InitializeComponent();
            ShowTableIndemnite();
            StyliserDataGridView();
            StyliserDataGridViewGestion();
            StyliserTabControl();
            ShowTableIndemniteGestion();
        }






        private void tabControlIndemnite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlIndemnite.SelectedIndex == 1) // Gestion
            {
                ChargerTablePage2(forcer: true);  // << force la réactualisation
                ResetChamps();
            }
            else if (tabControlIndemnite.SelectedIndex == 0) // Liste
            {
                // Recharge aussi la liste si besoin
                EntrepriseClass.ChargerEntreprises(ComboBoxEntreprise);
                RefreshIndemniteGrid(DataGridView_Indemnite, ComboBoxEntreprise, textBoxSearch?.Text);
                if (DataGridView_Indemnite.Columns.Contains("Id"))
                    DataGridView_Indemnite.Columns["Id"].Visible = false;
                StyliserDataGridView();
                ResetChamps();
            }
            if (tabControlIndemnite.SelectedTab == tabPage2)
            {
                ChargerTablePage2(forcer: true);
            }

        }


        ////////////////////////////////////////////////////////////////////////////

        private bool tablePage2Chargee = false;

        ///////////////////////////////////////////////////////////////////////////
        private void ChargerTablePage2(bool forcer = false)
        {
            // Si tu veux vraiment éviter les recharges inutiles, garde ce garde-fou.
            if (tablePage2Chargee && !forcer) return;

            try
            {
                UseWaitCursor = true;

                // Charger la liste d’entreprises une seule fois si nécessaire
                if (ComboBoxEntrepriseGestion.DataSource == null)
                    EntrepriseClass.ChargerEntreprises(ComboBoxEntrepriseGestion);

                // Récupérer le texte de recherche de l’onglet gestion (si tu en as un)
                string qGestion = textBoxSearch?.Text;

                // >>> ACTUALISATION EFFECTIVE DU DATAGRID <<<
                RefreshIndemniteGrid(
                    grid: DataGridView_IndemniteGestion,
                    comboEntreprise: ComboBoxEntrepriseGestion,
                    recherche: qGestion,
                    idEntrepriseOverride: null
                );

                // Masquer la colonne Id si présente (selon ton schéma c’est "Id", pas "ID")
                if (DataGridView_IndemniteGestion.Columns.Contains("Id"))
                    DataGridView_IndemniteGestion.Columns["Id"].Visible = false;

                StyliserDataGridViewGestion();  // ton style spécifique à la page 2

                tablePage2Chargee = true;       // page chargée au moins une fois
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



        private void RefreshIndemniteGrid(
    DataGridView grid,
    ComboBox comboEntreprise = null,
    string recherche = null,
    int? idEntrepriseOverride = null)
        {
            if (grid == null) return;

            void DoWork()
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    // 1) Déterminer l’entreprise à filtrer
                    int? idEnt = idEntrepriseOverride;
                    if (!idEnt.HasValue && comboEntreprise != null && comboEntreprise.SelectedValue != null)
                    {
                        if (int.TryParse(comboEntreprise.SelectedValue.ToString(), out var idParsed) && idParsed > 0)
                            idEnt = idParsed;
                    }

                    // 2) Sauvegarder l’état visuel (Id sélectionné + scroll)
                    object selectedId = null;
                    int firstDisplayedRowIndex = grid.FirstDisplayedScrollingRowIndex >= 0
                        ? grid.FirstDisplayedScrollingRowIndex
                        : 0;

                    if (grid.CurrentRow != null && grid.Columns.Contains("Id"))
                        selectedId = grid.CurrentRow.Cells["Id"].Value;

                    // 3) Charger les données
                    // - si une recherche est fournie → on utilise la recherche
                    // - sinon → on prend la liste simple (avec filtre entreprise via recherche "" pour garder la cohérence)
                    DataTable dt;
                    string q = (recherche ?? string.Empty).Trim();

                    if (string.IsNullOrEmpty(q) && idEnt == null)
                    {
                        // Pas de filtre → liste complète
                        dt = IndemniteClass.GetIndemniteList(null);
                    }
                    else
                    {
                        // Filtre entreprise et/ou texte → passer par la recherche
                        dt = RechercheIndemnite(q, idEnt);
                    }

                    // 4) Bind
                    bool firstBind = grid.DataSource == null;
                    grid.AutoGenerateColumns = true;
                    grid.DataSource = dt;

                    // 5) Masquer Id si présent
                    if (grid.Columns.Contains("Id"))
                        grid.Columns["Id"].Visible = false;

                    // 6) Restaurer scroll
                    if (firstDisplayedRowIndex >= 0 && firstDisplayedRowIndex < grid.RowCount)
                        grid.FirstDisplayedScrollingRowIndex = firstDisplayedRowIndex;

                    // 7) Restaurer la sélection précédente si possible
                    if (selectedId != null && grid.Columns.Contains("Id"))
                    {
                        for (int i = 0; i < grid.Rows.Count; i++)
                        {
                            var val = grid.Rows[i].Cells["Id"].Value;
                            if (val != null && val.ToString() == selectedId.ToString())
                            {
                                grid.ClearSelection();
                                // Mettre un CurrentCell valide
                                foreach (DataGridViewColumn col in grid.Columns)
                                {
                                    if (col.Visible)
                                    {
                                        grid.CurrentCell = grid.Rows[i].Cells[col.Index];
                                        grid.Rows[i].Selected = true;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de l’actualisation : " + ex.Message,
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }

            // Exécuter côté UI thread si nécessaire
            if (grid.InvokeRequired) grid.Invoke((Action)DoWork);
            else DoWork();
        }






        public static DataTable RechercheIndemnite(string recherche, int? idEntreprise = null)
        {
            var table = new DataTable();
            var connect = new Dbconnect();

            using (var con = connect.getconnection)
            {
                con.Open();

                const string sql = @"
SELECT
    i.id_indemnite           AS `Id`,
    e.nomEntreprise          AS `Entreprise`,
    p.matricule              AS `Matricule`,
    p.nomPrenom              AS `Employe`,
    i.type                   AS `Type`,
    i.valeur                 AS `Valeur`
FROM indemnite i
LEFT JOIN personnel  p ON p.id_personnel  = i.id_personnel
LEFT JOIN entreprise e ON e.id_entreprise = p.id_entreprise
WHERE
    (@idEnt IS NULL OR p.id_entreprise = @idEnt)
AND (
    @q = '' OR
    i.type          LIKE @like OR
    CAST(i.valeur AS CHAR) LIKE @like OR
    p.nomPrenom     LIKE @like OR
    p.matricule     LIKE @like OR
    e.nomEntreprise LIKE @like
)
ORDER BY p.nomPrenom, i.type;";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@idEnt", MySqlDbType.Int32).Value = (object)idEntreprise ?? DBNull.Value;

                    var q = (recherche ?? "").Trim();
                    var like = "%" + q + "%";

                    cmd.Parameters.Add("@q", MySqlDbType.VarChar).Value = q;
                    cmd.Parameters.Add("@like", MySqlDbType.VarChar).Value = like;

                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(table);
                    }
                }
            }

            return table;
        }




        ////////////////////////////////////////////////////////////////////////////

        private void ShowTableIndemnite()
        {
            EntrepriseClass.ChargerEntreprises(ComboBoxEntreprise);
            var dt = IndemniteClass.GetIndemniteList(null);
            DataGridView_Indemnite.AutoGenerateColumns = true;
            DataGridView_Indemnite.DataSource = dt;
            // Masquer les IDs si tu veux une vue clean
            if (DataGridView_Indemnite.Columns.Contains("ID"))
                DataGridView_Indemnite.Columns["ID"].Visible = false;
        }


        private void ShowTableIndemniteGestion()
        {
            EntrepriseClass.ChargerEntreprises(ComboBoxEntrepriseGestion);
            var dt = IndemniteClass.GetIndemniteList(null);
            DataGridView_IndemniteGestion.AutoGenerateColumns = true;
            DataGridView_IndemniteGestion.DataSource = dt;
            // Masquer les IDs si tu veux une vue clean
            if (DataGridView_IndemniteGestion.Columns.Contains("ID"))
                DataGridView_IndemniteGestion.Columns["ID"].Visible = false;
        }

        ///////////////////////////////////////////////////////////////////////////


        private static bool ExisteDejaPourType(MySqlConnection con, MySqlTransaction tx, int idPers, string typeLibelle)
        {
            const string sql = @"SELECT COUNT(*) FROM indemnite WHERE id_personnel = @p AND type = @t;";

             var cmd = new MySqlCommand(sql, con, tx);
            cmd.Parameters.AddWithValue("@p", idPers);
            cmd.Parameters.AddWithValue("@t", typeLibelle);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        private static string ToDbType(IndemniteType t)
        {
            switch (t)
            {
                case IndemniteType.LogementNumeraire: return "Logement Numeraire";
                case IndemniteType.Fonction: return "Fonction";
                case IndemniteType.TransportNumeraire: return "Transport Numeraire";
                case IndemniteType.LogementNature: return "Logement Nature";
                case IndemniteType.TransportNature: return "Transport Nature";
                case IndemniteType.DomesticiteNationaux: return "Domesticite Nationaux";
                case IndemniteType.DomesticiteEtrangers: return "Domesticite Etrangers";
                case IndemniteType.AutresAvantages: return "Autres Avantages";
                default: return "Autres Avantages";
            }
        }



        ///////////////////////////////////////////////////////////////////////////
        private void StyliserDataGridView()
        {
            // Fond général de la grille
            DataGridView_Indemnite.BackgroundColor = Color.White;

            // Désactiver le style visuel Windows
            DataGridView_Indemnite.EnableHeadersVisualStyles = false;

            // Style de l'en-tête
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.MidnightBlue;
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Montserrat", 10f, FontStyle.Bold);
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            headerStyle.WrapMode = DataGridViewTriState.True;
            headerStyle.SelectionBackColor = Color.MidnightBlue;     // ← Empêche le changement au clic
            headerStyle.SelectionForeColor = Color.White;            // ← Texte toujours blanc

            DataGridView_Indemnite.EnableHeadersVisualStyles = false;
            DataGridView_Indemnite.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridView_Indemnite.ColumnHeadersHeight = 35;

            // Style des cellules normales
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.BackColor = Color.White;
            cellStyle.ForeColor = Color.Black;
            cellStyle.Font = new Font("Montserrat", 9.5f, FontStyle.Regular);
            cellStyle.SelectionBackColor = Color.LightSteelBlue;
            cellStyle.SelectionForeColor = Color.Black;
            DataGridView_Indemnite.DefaultCellStyle = cellStyle;

            // Style des lignes alternées
            DataGridView_Indemnite.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;

            // Supprimer les bordures
            DataGridView_Indemnite.BorderStyle = BorderStyle.None;
            DataGridView_Indemnite.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Indemnite.GridColor = Color.LightGray;

            // Masquer l'entête de ligne à gauche
            DataGridView_Indemnite.RowHeadersVisible = false;

            // Autres options d’esthétique
            DataGridView_Indemnite.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Indemnite.MultiSelect = false;
        }

        ////////////////////////////////////////////////////////////////////////////

        private void StyliserDataGridViewGestion()
        {
            // Améliorer le rendu (anti-scintillement)
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, DataGridView_IndemniteGestion, new object[] { true });

            // Fond général
            DataGridView_IndemniteGestion.BackgroundColor = Color.White;

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

            DataGridView_IndemniteGestion.EnableHeadersVisualStyles = false;
            DataGridView_IndemniteGestion.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridView_IndemniteGestion.ColumnHeadersHeight = 35;

            // Cellules normales
            var cellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Montserrat", 9.5f),
                SelectionBackColor = Color.LightSteelBlue,
                SelectionForeColor = Color.Black
            };
            DataGridView_IndemniteGestion.DefaultCellStyle = cellStyle;

            // Lignes alternées
            DataGridView_IndemniteGestion.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;

            // Bordures & style
            DataGridView_IndemniteGestion.BorderStyle = BorderStyle.None;
            DataGridView_IndemniteGestion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_IndemniteGestion.GridColor = Color.LightGray;
            DataGridView_IndemniteGestion.RowHeadersVisible = false;

            // Sélection
            DataGridView_IndemniteGestion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_IndemniteGestion.MultiSelect = false;
        }

        ////////////////////////////////////////////////////////////////////////////

        private void StyliserTabControl()
        {
            tabControlIndemnite.Appearance = TabAppearance.Normal;
            tabControlIndemnite.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControlIndemnite.ItemSize = new Size(150, 35); // largeur, hauteur des onglets
            tabControlIndemnite.SizeMode = TabSizeMode.Fixed;
            tabControlIndemnite.DrawItem += TabControlEntreprise_DrawItem;
        }

        ////////////////////////////////////////////////////////////////////////////

        private void TabControlEntreprise_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControlIndemnite.TabPages[e.Index];
            Rectangle rect = tabControlIndemnite.GetTabRect(e.Index);
            bool isSelected = (e.Index == tabControlIndemnite.SelectedIndex);

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




        private void ChargerDetailsIndemniteParId(string idIndemnite)
        {
            try
            {
                const string sql = @"
SELECT
    i.id_indemnite,
    i.id_personnel,
    i.type,                 -- ex: 'Logement Numeraire', 'Transport Nature', ...
    i.valeur,
    p.nomPrenom      AS NomPersonnel,
    p.matricule,
    p.id_entreprise  AS IdEntreprise,
    e.nomEntreprise  AS NomEntreprise
FROM indemnite i
LEFT JOIN personnel p  ON p.id_personnel  = i.id_personnel
LEFT JOIN entreprise e ON e.id_entreprise = p.id_entreprise
WHERE i.id_indemnite = @id
LIMIT 1;";

                var connect = new Dbconnect();
                using (var con = new MySqlConnection(connect.getconnection.ConnectionString))
                using (var cmd = new MySqlCommand(sql, con))
                {
                    if (!int.TryParse(idIndemnite, out int id))
                    {
                        MessageBox.Show("Identifiant de l'indemnité invalide.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

                    con.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (!reader.Read())
                        {
                            // Nettoyage minimal si rien trouvé
                            textBoxvaleurGestion.Text = "";
                            textBoxMatricule.Text = "";
                            if (ComboBoxIndemniteGestion != null) ComboBoxIndemniteGestion.SelectedIndex = -1;
                            if (ComboBoxEntrepriseGestion != null) ComboBoxEntrepriseGestion.SelectedIndex = -1;
                            if (ComboBoxEmployeGestion != null) ComboBoxEmployeGestion.SelectedIndex = -1;
                            return;
                        }

                        // ----- Champs simples -----
                        textBoxMatricule.Text = reader["matricule"]?.ToString() ?? "";

                        // Valeur
                        if (!reader.IsDBNull(reader.GetOrdinal("valeur")))
                            textBoxvaleurGestion.Text = Convert.ToDecimal(reader["valeur"]).ToString("0.##");
                        else
                            textBoxvaleur.Text = "";

                        // Type (texte) -> ComboBoxIndemniteGestion
                        string typeLib = reader["type"]?.ToString() ?? "";
                        if (ComboBoxIndemniteGestion != null)
                        {
                            // Si la DataSource est déjà alimentée avec la liste des types, on tente une sélection
                            if (ComboBoxIndemniteGestion.DataSource != null)
                            {
                                ComboBoxIndemniteGestion.SelectedItem = typeLib;
                                if (ComboBoxIndemniteGestion.SelectedItem == null)
                                {
                                    // Valeur non présente : on force l’affichage
                                    ComboBoxIndemniteGestion.SelectedIndex = -1;
                                    ComboBoxIndemniteGestion.Text = typeLib;
                                }
                            }
                            else
                            {
                                // Pas de DataSource : on met le texte brut
                                ComboBoxIndemniteGestion.SelectedIndex = -1;
                                ComboBoxIndemniteGestion.Text = typeLib;
                            }
                        }

                        // ----- Entreprise -----
                        if (!reader.IsDBNull(reader.GetOrdinal("IdEntreprise")))
                        {
                            int idEnt = Convert.ToInt32(reader["IdEntreprise"]);
                            if (ComboBoxEntrepriseGestion.DataSource == null)
                                EntrepriseClass.ChargerEntreprises(ComboBoxEntrepriseGestion, idEnt, true);
                            else
                                ComboBoxEntrepriseGestion.SelectedValue = idEnt;
                        }
                        else
                        {
                            if (ComboBoxEntrepriseGestion != null) ComboBoxEntrepriseGestion.SelectedIndex = -1;
                        }

                        // ----- Employé -----
                        if (!reader.IsDBNull(reader.GetOrdinal("id_personnel")))
                        {
                            int idPers = Convert.ToInt32(reader["id_personnel"]);
                            if (ComboBoxEmployeGestion.DataSource == null)
                                GestionEmployeForm.ChargerPersonnels(ComboBoxEmployeGestion, null, idPers, true);
                            else
                                ComboBoxEmployeGestion.SelectedValue = idPers;
                        }
                        else
                        {
                            if (ComboBoxEmployeGestion != null) ComboBoxEmployeGestion.SelectedIndex = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement de l'indemnité : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






        private void buttonAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                // --- Récupérations UI ---
                // Entreprise (facultatif pour l'insert si tu n'en as pas besoin ici, mais on valide la sélection)
                int? idEntreprise = null;
                if (ComboBoxEntreprise.SelectedValue != null &&
                    int.TryParse(ComboBoxEntreprise.SelectedValue.ToString(), out var idEntParsed))
                    idEntreprise = idEntParsed;

                // Employé (obligatoire)
                if (ComboBoxEmploye.SelectedValue == null ||
                    !int.TryParse(ComboBoxEmploye.SelectedValue.ToString(), out var idPersonnel) ||
                    idPersonnel <= 0)
                {
                    MessageBox.Show("Veuillez sélectionner un employé.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Type d'indemnité (obligatoire)
                var typeLabel = ComboBoxIndemnite.SelectedItem?.ToString();
                if (string.IsNullOrWhiteSpace(typeLabel) || typeLabel == "-- Sélectionner --")
                {
                    MessageBox.Show("Veuillez choisir un type d'indemnité.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Montant (obligatoire, > 0)
                if (string.IsNullOrWhiteSpace(textBoxvaleur.Text))
                {
                    MessageBox.Show("Veuillez saisir le montant de l'indemnité.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Parse décimal avec culture actuelle OU invariant (accepte 12,34 ou 12.34)
                decimal valeur;
                if (!decimal.TryParse(textBoxvaleur.Text.Trim(),
                                      System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowThousands,
                                      System.Globalization.CultureInfo.CurrentCulture, out valeur))
                {
                    if (!decimal.TryParse(textBoxvaleur.Text.Trim(),
                                          System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowThousands,
                                          System.Globalization.CultureInfo.InvariantCulture, out valeur))
                    {
                        MessageBox.Show("Le montant saisi est invalide.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                if (valeur <= 0)
                {
                    MessageBox.Show("Le montant doit être supérieur à 0.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Map label -> enum
                IndemniteType typeEnum;
                switch ((typeLabel ?? "").Trim())
                {
                    case "Logement Numeraire": typeEnum = IndemniteType.LogementNumeraire; break;
                    case "Fonction": typeEnum = IndemniteType.Fonction; break;
                    case "Transport Numeraire": typeEnum = IndemniteType.TransportNumeraire; break;
                    case "Logement Nature": typeEnum = IndemniteType.LogementNature; break;
                    case "Transport Nature": typeEnum = IndemniteType.TransportNature; break;
                    case "Domesticite Nationaux": typeEnum = IndemniteType.DomesticiteNationaux; break;
                    case "Domesticite Etrangers": typeEnum = IndemniteType.DomesticiteEtrangers; break;
                    case "Autres Avantages": typeEnum = IndemniteType.AutresAvantages; break;
                    default:
                        MessageBox.Show("Type d'indemnité inconnu.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                }

                // (Optionnel) Valider l'entreprise sélectionnée si c'est requis dans votre flux
                if (!idEntreprise.HasValue || idEntreprise.Value <= 0)
                {
                    MessageBox.Show("Veuillez sélectionner une entreprise.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // --- Confirmation ---
                var confirm = MessageBox.Show(
                    $"Confirmer l'ajout de l'indemnité ?\n\n" +
                    $"Employé : {ComboBoxEmploye.Text}\n" +
                    $"Type : {typeLabel}\n" +
                    $"Montant : {valeur}",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (confirm != DialogResult.Yes)
                    return;

                // --- Enregistrement ---
                IndemniteClass.EnregistrerIndemnite(idPersonnel, typeEnum, valeur);

                MessageBox.Show("Indemnité enregistrée avec succès.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // --- Rafraîchir la grille (si vous avez une méthode) ---
                // ReloadIndemnitesGrid();

                ShowTableIndemnite();
                // Si vous avez une méthode dédiée :
                 ResetChamps();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        ////////////////////////////////////////////////////////////////////////////


        private void ComboBoxEmploye_SelectedIndexChanged(object sender, EventArgs e)
        {
        }




        ////////////////////////////////////////////////////////////////////////////


        private static int? GetSelectedIntOrNull(ComboBox combo, string valueColumnName)
        {
            if (combo.SelectedValue == null) return null;

            // Cas normal : déjà un int
            if (combo.SelectedValue is int i) return i;

            // Certaines configs renvoient un DataRowView
            if (combo.SelectedValue is DataRowView drv)
            {
                var val = drv[valueColumnName];
                return val == DBNull.Value ? (int?)null : Convert.ToInt32(val);
            }

            // Fallback: essayer de parser
            if (int.TryParse(combo.SelectedValue.ToString(), out var parsed))
                return parsed;

            return null;
        }
        private void EnableChamps()
        {
            ComboBoxEmploye.Enabled = true;
            ComboBoxIndemnite.Enabled = true;
            textBoxvaleur.Enabled = true;
            buttonAjouter.Enabled = true;
        }




        private void DisableChamps()
        {
            ComboBoxEmploye.Enabled = false;
            ComboBoxIndemnite.Enabled = false;
            textBoxvaleur.Enabled = false;
            buttonAjouter.Enabled = false;

            ComboBoxEmployeGestion.Enabled = false;
            ComboBoxIndemniteGestion.Enabled = false;
            textBoxvaleurGestion.Enabled = false;
           
        }




        private void ResetChamps()
        {

            // --- Reset simple du formulaire (mode ajout) ---
            if (ComboBoxEntreprise != null) ComboBoxEntreprise.SelectedIndex = -1;
            if (ComboBoxEmploye != null) ComboBoxEmploye.SelectedIndex = -1;
            if (ComboBoxIndemnite != null) ComboBoxIndemnite.SelectedIndex = -1;
            textBoxvaleur.Text = string.Empty;

            // --- Reset simple du formulaire (mode ajout) ---
            if (ComboBoxEntrepriseGestion != null) ComboBoxEntrepriseGestion.SelectedIndex = -1;
            if (ComboBoxEmployeGestion != null) ComboBoxEmployeGestion.SelectedIndex = -1;
            if (ComboBoxIndemniteGestion != null) ComboBoxIndemniteGestion.SelectedIndex = -1;
            textBoxvaleurGestion.Text = string.Empty;
        }

        private void ComboBoxEntreprise_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? idEnt = GetSelectedIntOrNull(ComboBoxEntreprise, "id_entreprise");
            if (idEnt == null || idEnt.Value <= 0)
            {
                // rien de sélectionné / placeholder
                ComboBoxEmploye.DataSource = null;
                return;
            }

            // Charge les employés filtrés par entreprise
            EmployeClass.ChargerEmployesParEntreprise(ComboBoxEmploye, idEnt.Value, null, true);
            EnableChamps();
        }

        private void DataGridView_IndemniteGestion_Click(object sender, EventArgs e)
        {
            if (DataGridView_IndemniteGestion.CurrentRow != null)
            {
                string id = DataGridView_IndemniteGestion.CurrentRow.Cells[0].Value?.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    textBoxID.Text = id;
                    ChargerDetailsIndemniteParId(id);
                    ComboBoxEntrepriseGestion.Enabled = false;
                    ComboBoxIndemniteGestion.Enabled = false;
                    textBoxvaleurGestion.Enabled = true;
                }
            }
        }

        private void buttonRechercher_Click(object sender, EventArgs e)
        {
            DataGridView_IndemniteGestion.DataSource = IndemniteClass.RechercheIndemnite(textBoxSearch.Text);
        }

        private void button_Supprimer_Click(object sender, EventArgs e)
        {
            try
            {
                // Vérifier qu’un ID est bien présent dans le champ
                if (string.IsNullOrWhiteSpace(textBoxID.Text) || !int.TryParse(textBoxID.Text, out int idIndemnite))
                {
                    MessageBox.Show("Veuillez sélectionner une indemnité à supprimer.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Demander confirmation à l’utilisateur
                var confirm = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer cette indemnité ?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (confirm != DialogResult.Yes)
                    return;

                // Appel au repository pour suppression
                IndemniteClass.SupprimerIndemnite(idIndemnite);

                MessageBox.Show("Indemnité supprimée avec succès.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Rafraîchir la grille (si tu as une méthode dédiée)
                // ReloadIndemnitesGrid();

                // Nettoyer et désactiver le formulaire
                ShowTableIndemniteGestion();
                // Si vous avez une méthode dédiée :
                ResetChamps();
                DisableChamps();    
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression : " + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataClearGestion()
        {
            ComboBoxEmployeGestion.SelectedIndex = -1;
            ComboBoxEntrepriseGestion.SelectedIndex = -1;
            textBoxvaleurGestion.Clear();
            ComboBoxIndemniteGestion.SelectedIndex = -1;
            textBoxvaleurGestion.Enabled = false;
        }


        private void GestionIndemniteForm_Load(object sender, EventArgs e)
        {
            tabControlIndemnite.SelectedIndexChanged += tabControlIndemnite_SelectedIndexChanged;
        }

        private void buttonModifier_Click(object sender, EventArgs e)
        {
            try
            {
                // Vérif ID
                if (string.IsNullOrWhiteSpace(textBoxID.Text) || !int.TryParse(textBoxID.Text, out int idIndemnite))
                {
                    MessageBox.Show("Veuillez sélectionner une indemnité à modifier.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Utiliser NumberStyles + CultureInfo.InvariantCulture
                string saisieValeur = textBoxvaleurGestion.Text.Replace(",", "."); // uniformiser
                if (!decimal.TryParse(saisieValeur, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valeur) || valeur <= 0)
                {
                    MessageBox.Show("Veuillez saisir une valeur numérique valide.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Confirmation
                var confirm = MessageBox.Show(
                    "Voulez-vous vraiment modifier la valeur de cette indemnité ?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (confirm != DialogResult.Yes)
                    return;

                // --- Update SQL : uniquement la valeur ---
                var db = new Dbconnect();
                using (var con = db.getconnection)
                {
                    con.Open();

                    const string sql = @"UPDATE indemnite SET valeur = @valeur WHERE id_indemnite = @id;";
                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", idIndemnite);
                        cmd.Parameters.AddWithValue("@valeur", valeur);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Valeur modifiée avec succès.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Nettoyer et désactiver le formulaire
                ShowTableIndemniteGestion();
                // Si vous avez une méthode dédiée :
                ResetChamps();
                DisableChamps();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification : " + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonEffacerGestion_Click(object sender, EventArgs e)
        {
            dataClearGestion();
        }
    }
}