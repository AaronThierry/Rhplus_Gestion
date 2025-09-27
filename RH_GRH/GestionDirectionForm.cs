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
    public partial class GestionDirectionForm : Form
    {

        EntrepriseClass Entreprise = new EntrepriseClass();
        DirectionClass Direction = new DirectionClass();
        Dbconnect connect = new Dbconnect();

        public GestionDirectionForm()
        {
            InitializeComponent();
            StyliserDataGridView();
            StyliserDataGridViewGestion();
            StyliserTabControl();
            ShowTableDirection();
            ShowTableDirectionGestion();
        }




        private void tabControlDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlDirection.SelectedIndex == 1) // Onglet n°2 (index 1)
            {
                ChargerTablePage2();
                EntrepriseClass.ChargerEntreprises(ComboBoxEntrepriseGestion);
                StyliserDataGridViewGestion();
                ShowTableDirectionGestion();
                ClearDirectionForm();
            }
            else if (tabControlDirection.SelectedIndex == 0)
            {
                ClearDirectionForm();
                ShowTableDirection();
            }
        }

        private bool tablePage2Chargee = false;

        private void ChargerTablePage2(bool forcer = false)
        {
            if (tablePage2Chargee && !forcer) return;

            try
            {
                EntrepriseClass.ChargerEntreprises(ComboBoxEntrepriseGestion);
                UseWaitCursor = true;

                // 1) Récupération des données
                DataTable dt = Direction.GetDirectionList() ?? new DataTable();

                // 2) Tri par défaut si dispo
                if (dt.Columns.Contains("Entreprise") && dt.Columns.Contains("Direction"))
                {
                    var dv = dt.DefaultView;
                    dv.Sort = "[Entreprise] ASC, [Direction] ASC";
                    dt = dv.ToTable();
                }

                // 3) Binding
                var grid = DataGridView_Direction_Gestion;
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





        private void ClearDirectionForm()
        {
            textBoxNomDirection.Clear();
            ComboBoxEntreprise.SelectedIndex = 0;
        }

        private void ClearDirectionFormGestion()
        {
            textBoxDirectionGestion.Clear();
            ComboBoxEntrepriseGestion.SelectedIndex = 0;
            textBoxID.Clear();
        }




        private void ShowTableDirection()
        {
            var dt = Direction.GetDirectionList();
            DataGridView_Direction.AutoGenerateColumns = true;
            DataGridView_Direction.DataSource = dt;
            // Masquer les IDs si tu veux une vue clean
            if (DataGridView_Direction.Columns.Contains("ID"))
                DataGridView_Direction.Columns["ID"].Visible = false;
        }


        private void ShowTableDirectionGestion()
        {
            EntrepriseClass.ChargerEntreprises(ComboBoxEntreprise);
            var dt = Direction.GetDirectionList();
            DataGridView_Direction_Gestion.AutoGenerateColumns = true;
            DataGridView_Direction_Gestion.DataSource = dt;
            // Masquer les IDs si tu veux une vue clean
            if (DataGridView_Direction_Gestion.Columns.Contains("ID"))
                DataGridView_Direction.Columns["ID"].Visible = false;
        }



        private void StyliserDataGridView()
        {
            // Fond général de la grille
            DataGridView_Direction.BackgroundColor = Color.White;

            // Désactiver le style visuel Windows
            DataGridView_Direction.EnableHeadersVisualStyles = false;

            // Style de l'en-tête
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.MidnightBlue;
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Montserrat", 10f, FontStyle.Bold);
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            headerStyle.WrapMode = DataGridViewTriState.True;
            headerStyle.SelectionBackColor = Color.MidnightBlue;     // ← Empêche le changement au clic
            headerStyle.SelectionForeColor = Color.White;            // ← Texte toujours blanc

            DataGridView_Direction.EnableHeadersVisualStyles = false;
            DataGridView_Direction.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridView_Direction.ColumnHeadersHeight = 35;

            // Style des cellules normales
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.BackColor = Color.White;
            cellStyle.ForeColor = Color.Black;
            cellStyle.Font = new Font("Montserrat", 9.5f, FontStyle.Regular);
            cellStyle.SelectionBackColor = Color.LightSteelBlue;
            cellStyle.SelectionForeColor = Color.Black;
            DataGridView_Direction.DefaultCellStyle = cellStyle;

            // Style des lignes alternées
            DataGridView_Direction.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;

            // Supprimer les bordures
            DataGridView_Direction.BorderStyle = BorderStyle.None;
            DataGridView_Direction.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Direction.GridColor = Color.LightGray;

            // Masquer l'entête de ligne à gauche
            DataGridView_Direction.RowHeadersVisible = false;

            // Autres options d’esthétique
            DataGridView_Direction.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Direction.MultiSelect = false;
        }
        private void StyliserDataGridViewGestion()
        {
            // Améliorer le rendu (anti-scintillement)
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, DataGridView_Direction_Gestion, new object[] { true });

            // Fond général
            DataGridView_Direction_Gestion.BackgroundColor = Color.White;

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

            DataGridView_Direction_Gestion.EnableHeadersVisualStyles = false;
            DataGridView_Direction_Gestion.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridView_Direction_Gestion.ColumnHeadersHeight = 35;

            // Cellules normales
            var cellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Montserrat", 9.5f),
                SelectionBackColor = Color.LightSteelBlue,
                SelectionForeColor = Color.Black
            };
            DataGridView_Direction_Gestion.DefaultCellStyle = cellStyle;

            // Lignes alternées
            DataGridView_Direction_Gestion.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;

            // Bordures & style
            DataGridView_Direction_Gestion.BorderStyle = BorderStyle.None;
            DataGridView_Direction_Gestion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView_Direction_Gestion.GridColor = Color.LightGray;
            DataGridView_Direction_Gestion.RowHeadersVisible = false;

            // Sélection
            DataGridView_Direction_Gestion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Direction_Gestion.MultiSelect = false;
        }

        private void StyliserTabControl()
        {
            tabControlDirection.Appearance = TabAppearance.Normal;
            tabControlDirection.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControlDirection.ItemSize = new Size(150, 35); // largeur, hauteur des onglets
            tabControlDirection.SizeMode = TabSizeMode.Fixed;
            tabControlDirection.DrawItem += TabControlEntreprise_DrawItem;
        }

        private void TabControlEntreprise_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControlDirection.TabPages[e.Index];
            Rectangle rect = tabControlDirection.GetTabRect(e.Index);
            bool isSelected = (e.Index == tabControlDirection.SelectedIndex);

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

        private void RegisterDirection_Load(object sender, EventArgs e)
        {
            tabControlDirection.SelectedIndexChanged += tabControlDirection_SelectedIndexChanged;
            EntrepriseClass.ChargerEntreprises(ComboBoxEntreprise);
        }




        private void DataGridView_Entreprise_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonAjouter_Click(object sender, EventArgs e)
        {

        }

        private void buttonAjouter_Click_1(object sender, EventArgs e)
        {
            // 1) Récupération + validations
            string nomDirection = textBoxNomDirection.Text?.Trim();

            if (string.IsNullOrWhiteSpace(nomDirection))
            {
                MessageBox.Show("Veuillez saisir le nom de la direction.", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxNomDirection.Focus();
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
                Cursor = Cursors.WaitCursor;

                // ⚠️ Adapter le nom de classe si besoin (si ta méthode est dans DirectionClass par ex.)
                DirectionClass.EnregistrerDirection(nomDirection, idEntreprise.Value);
                ShowTableDirection();

                // 3) Reset UI (selon ton besoin, on garde l’entreprise sélectionnée)
                textBoxNomDirection.Clear();
                ComboBoxEntreprise.Focus();
                ComboBoxEntreprise.SelectedIndex = 0;

                // Si tu veux aussi rafraîchir un DataGridView des directions :
                // dataGridViewDirections.DataSource = DirectionClass.ListerDirectionsParEntreprise(idEntreprise.Value);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur inattendue : " + ex.Message, "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void textBoxNomDirection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                buttonAjouter.PerformClick(); // Simule le clic sur le bouton Ajouter
            }
        }

        private void buttonEffacer_Click(object sender, EventArgs e)
        {
            ClearDirectionForm();
        }

        private void buttonRechercher_Click(object sender, EventArgs e)
        {
            DataGridView_Direction_Gestion.DataSource = Direction.RechercheDirectionConcat(textBoxSearch.Text);

        }
    


private void ChargerDetailsDirectionParId(string id)
        {
            try
            {
                // Jointure pour récupérer aussi le nom de l'entreprise
                const string sql = @"
            SELECT 
                d.nomDirection,
                d.id_entreprise,
                e.nomEntreprise
            FROM direction d
            INNER JOIN entreprise e ON e.id_entreprise = d.id_entreprise
            WHERE d.id_direction = @id;";

                // ⚠️ IMPORTANT :
                // - Si getconnection retourne MySqlConnection, utilisez .ConnectionString :
                using (var con = new MySqlConnection(connect.getconnection.ConnectionString))
                // - Si getconnection retourne une chaîne de connexion, utilisez :
                // using (var con = new MySqlConnection(connect.getconnection))
                using (var cmd = new MySqlCommand(sql, con))
                {
                    // id numérique en base : on paramètre en Int32 (même si on reçoit une string)
                    if (!int.TryParse(id, out int idDirection))
                    {
                        MessageBox.Show("Identifiant de direction invalide.");
                        return;
                    }

                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = idDirection;

                    con.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                        {
                            // Nom de la direction
                            textBoxDirectionGestion.Text = reader["nomDirection"]?.ToString();

                            // Sélectionner l'entreprise dans le ComboBox (s'il est déjà bindé)
                            if (!reader.IsDBNull(reader.GetOrdinal("id_entreprise")))
                            {
                                int idEnt = reader.GetInt32(reader.GetOrdinal("id_entreprise"));

                                // Si le Combo n'est pas encore chargé, on peut le charger ici (optionnel)
                                if (ComboBoxEntreprise.DataSource == null)
                                {
                                    EntrepriseClass.ChargerEntreprises(ComboBoxEntreprise, idEnt, ajouterPlaceholder: true);
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
                            textBoxDirectionGestion.Clear();
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

        private void DataGridView_Direction_Gestion_Click(object sender, EventArgs e)
        {
            if (DataGridView_Direction_Gestion.CurrentRow != null)
            {
                string id = DataGridView_Direction_Gestion.CurrentRow.Cells[0].Value?.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    textBoxID.Text = id;
                    ChargerDetailsDirectionParId(id);
                    ComboBoxEntrepriseGestion.Enabled = false;
                }
            }
        }




        private static bool TryGetSelectedInt(DataGridView grid, string idColumnName, out int id)
        {
            id = 0;
            if (grid?.CurrentRow == null) return false;

            // priorité au nom de colonne
            if (grid.Columns.Contains(idColumnName))
            {
                var v = grid.CurrentRow.Cells[idColumnName].Value;
                return v != null && v != DBNull.Value && int.TryParse(v.ToString(), out id);
            }

            // fallback première colonne
            if (grid.CurrentRow.Cells.Count > 0)
            {
                var v0 = grid.CurrentRow.Cells[0].Value;
                return v0 != null && v0 != DBNull.Value && int.TryParse(v0.ToString(), out id);
            }
            return false;
        }




        private void buttonModifier_Click(object sender, EventArgs e)
        {
            // 1) Validations rapides
            string nomDirection = textBoxDirectionGestion.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nomDirection))
            {
                MessageBox.Show("Veuillez saisir le nom de la direction.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxDirectionGestion.Focus();
                return;
            }

            if (!int.TryParse(textBoxID.Text?.Trim(), out int idDirection) || idDirection <= 0)
            {
                MessageBox.Show("ID direction invalide.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2) Confirmation
            var confirm = MessageBox.Show(
                $"Confirmer la modification de la direction #{idDirection} en « {nomDirection} » ?",
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

                DirectionClass.ModifierDirection(idDirection, nomDirection);

                // Rafraîchir la grille
                ShowTableDirectionGestion();
                ClearDirectionFormGestion();            }
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

        private void buttonEffacerGestion_Click(object sender, EventArgs e)
        {
            ClearDirectionFormGestion();
        }

        private void button_Supprimer_Click(object sender, EventArgs e)
        {
            // Récupère l'ID (depuis la grille ou un TextBox)
            if (!int.TryParse(textBoxID.Text?.Trim(), out int idDirection) || idDirection <= 0)
            {
                MessageBox.Show("Veuillez sélectionner une direction valide.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Confirmation
            var confirm = MessageBox.Show(
                $"Supprimer définitivement la direction #{idDirection} ?",
                "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (confirm != DialogResult.Yes) return;

            try
            {
                UseWaitCursor = true;
                button_Supprimer.Enabled = false;

                if (DirectionClass.SupprimerDirection(idDirection))
                {
                    // Rafraîchir la table après suppression
                    ShowTableDirectionGestion();
                    ClearDirectionFormGestion();
                }
            }
            finally
            {
                button_Supprimer.Enabled = true;
                UseWaitCursor = false;
            }
        }

        private void DataGridView_Direction_Gestion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Helper pour re-sélectionner une ligne par ID

    }



}
