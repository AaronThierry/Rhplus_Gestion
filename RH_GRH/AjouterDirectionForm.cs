using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Drawing;

namespace RH_GRH
{
    public partial class AjouterDirectionForm : Form
    {
        private Dbconnect connect = new Dbconnect();
        private Guna2TextBox textBoxRechercheEntreprise;
        private Guna2ComboBox comboBoxEntreprisesFiltres;
        private DataTable toutesLesEntreprises;
        private int? idEntrepriseSelectionnee = null;

        public AjouterDirectionForm()
        {
            InitializeComponent();
            RemplacerComboEntrepriseParRecherche();
            ChargerToutesLesEntreprises();
        }

        private void RemplacerComboEntrepriseParRecherche()
        {
            var parent = comboBoxEntreprise.Parent;

            // Référencer directement le label par son nom
            labelEntreprise.Text = "Entreprise :";
            labelEntreprise.Location = new Point(30, 80);

            // Trouver les autres contrôles
            var labelNomDirection = parent.Controls.OfType<Label>()
                .FirstOrDefault(l => l.Text.Contains("direction"));
            var textBoxNomDirection = parent.Controls.OfType<Guna2TextBox>()
                .FirstOrDefault(t => t.Name == "textBoxNomDirection");

            // Créer le label de recherche
            var labelRecherche = new Label
            {
                Text = "Recherche :",
                Font = new Font("Montserrat", 10F, FontStyle.Regular),
                Location = new Point(30, 110),
                AutoSize = true
            };
            parent.Controls.Add(labelRecherche);

            // Créer le TextBox de recherche
            textBoxRechercheEntreprise = new Guna2TextBox
            {
                PlaceholderText = "🔍 Rechercher une entreprise...",
                Font = new Font("Montserrat", 10F),
                Location = new Point(30, 135),
                Size = new Size(440, 36),
                BorderRadius = 0,
                BorderThickness = 2,
                BorderColor = Color.FromArgb(64, 64, 64),
                ForeColor = Color.Black,
                FillColor = Color.White
            };
            textBoxRechercheEntreprise.TextChanged += TextBoxRechercheEntreprise_TextChanged;
            parent.Controls.Add(textBoxRechercheEntreprise);

            // Positionner le ComboBox entreprises
            comboBoxEntreprise.Location = new Point(30, 186);
            comboBoxEntreprisesFiltres = comboBoxEntreprise;
            comboBoxEntreprisesFiltres.SelectedIndexChanged += ComboBoxEntreprisesFiltres_SelectedIndexChanged;

            // Repositionner les autres éléments avec espacement
            if (labelNomDirection != null)
                labelNomDirection.Location = new Point(30, 237);
            if (textBoxNomDirection != null)
                textBoxNomDirection.Location = new Point(30, 262);

            // Mettre en premier plan
            labelRecherche.BringToFront();
            textBoxRechercheEntreprise.BringToFront();
            comboBoxEntreprisesFiltres.BringToFront();
        }

        private void ChargerToutesLesEntreprises()
        {
            try
            {
                var conn = new Dbconnect();
                using (var con = conn.getconnection)
                {
                    con.Open();
                    string query = @"
                        SELECT
                            id_entreprise,
                            nomEntreprise,
                            telephone
                        FROM entreprise
                        ORDER BY nomEntreprise";

                    using (var cmd = new MySqlCommand(query, con))
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        toutesLesEntreprises = new DataTable();
                        adapter.Fill(toutesLesEntreprises);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur : {ex.Message}", "Erreur", CustomMessageBox.MessageType.Error);
            }
        }

        private void TextBoxRechercheEntreprise_TextChanged(object sender, EventArgs e)
        {
            if (toutesLesEntreprises == null) return;

            string recherche = textBoxRechercheEntreprise.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(recherche))
            {
                AfficherEntreprisesFiltres(toutesLesEntreprises);
                return;
            }

            var filtres = toutesLesEntreprises.AsEnumerable()
                .Where(row =>
                    row.Field<string>("nomEntreprise")?.ToLower().Contains(recherche) == true ||
                    row.Field<string>("telephone")?.ToLower().Contains(recherche) == true
                );

            DataTable dtFiltre;
            if (filtres.Any())
            {
                dtFiltre = filtres.CopyToDataTable();
            }
            else
            {
                dtFiltre = toutesLesEntreprises.Clone();
            }

            AfficherEntreprisesFiltres(dtFiltre);
        }

        private void AfficherEntreprisesFiltres(DataTable dt)
        {
            comboBoxEntreprisesFiltres.DataSource = dt;
            comboBoxEntreprisesFiltres.DisplayMember = "nomEntreprise";
            comboBoxEntreprisesFiltres.ValueMember = "id_entreprise";
        }

        private void ComboBoxEntreprisesFiltres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEntreprisesFiltres.SelectedValue == null) return;
            if (!int.TryParse(comboBoxEntreprisesFiltres.SelectedValue.ToString(), out int idEntreprise)) return;

            idEntrepriseSelectionnee = idEntreprise;
        }

        private void ChargerEntreprises()
        {
            // Méthode conservée pour compatibilité mais non utilisée
        }

        private void buttonAjouter_Click(object sender, EventArgs e)
        {
            // 1) Validations
            string nomDirection = textBoxNomDirection.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nomDirection))
            {
                CustomMessageBox.Show("Veuillez saisir le nom de la direction.", "Information",
                                CustomMessageBox.MessageType.Info);
                textBoxNomDirection.Focus();
                return;
            }

            // Récupérer l'ID directement depuis le ComboBox
            int idEntreprise;
            if (comboBoxEntreprisesFiltres.SelectedValue == null ||
                !int.TryParse(comboBoxEntreprisesFiltres.SelectedValue.ToString(), out idEntreprise) ||
                idEntreprise <= 0)
            {
                CustomMessageBox.Show("Veuillez rechercher et sélectionner une entreprise.", "Information",
                                CustomMessageBox.MessageType.Info);
                textBoxRechercheEntreprise.Focus();
                return;
            }

            // 2) Enregistrement
            try
            {
                Cursor = Cursors.WaitCursor;
                buttonAjouter.Enabled = false;

                DirectionClass.EnregistrerDirection(nomDirection, idEntreprise);

                // 3) Fermer la modale avec succès
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Erreur inattendue : " + ex.Message, "Erreur",
                                CustomMessageBox.MessageType.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                buttonAjouter.Enabled = true;
            }
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AjouterDirectionForm_Load(object sender, EventArgs e)
        {
            // Configuration au chargement si nécessaire
            textBoxNomDirection.Focus();
        }
    }
}
