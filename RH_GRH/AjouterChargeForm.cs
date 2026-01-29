using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class AjouterChargeForm : Form
    {
        private Dbconnect connect = new Dbconnect();
        private DataTable tousLesEmployes;
        private DataTable employesFiltres;
        private Label lblStatutRecherche;

        public AjouterChargeForm()
        {
            InitializeComponent();
            CreerLabelStatut();
            InitialiserDonnees();
            ConfigurerRaccourcisClavier();

            // Focus automatique sur le champ de recherche
            this.Shown += (s, e) => textBoxRechercheEmploye.Focus();
        }

        private void CreerLabelStatut()
        {
            // Label pour afficher le statut de la recherche
            lblStatutRecherche = new Label
            {
                AutoSize = true,
                Font = new Font("Montserrat", 8f, FontStyle.Italic),
                ForeColor = Color.FromArgb(120, 144, 156),
                Location = new Point(625, 80),
                Name = "lblStatutRecherche",
                Text = "",
                Visible = false
            };
            panelMain.Controls.Add(lblStatutRecherche);
            lblStatutRecherche.BringToFront();
        }

        private void ConfigurerRaccourcisClavier()
        {
            // Enter = Valider, Escape = Annuler
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    buttonValider_Click(null, null);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    buttonAnnuler_Click(null, null);
                    e.Handled = true;
                }
            };
        }

        private void InitialiserDonnees()
        {
            // Charger tous les employés de toutes les entreprises
            ChargerTousLesEmployes();

            // Initialiser le type de charge
            comboBoxType.Items.Clear();
            comboBoxType.Items.Add("-- Sélectionner --");
            comboBoxType.Items.Add("Épouse");
            comboBoxType.Items.Add("Enfant");
            comboBoxType.SelectedIndex = 0;

            // Initialiser la scolarisation
            comboBoxScolarisation.Items.Clear();
            comboBoxScolarisation.Items.Add("-- Sélectionner --");
            comboBoxScolarisation.Items.Add("Oui");
            comboBoxScolarisation.Items.Add("Non");
            comboBoxScolarisation.SelectedIndex = 0;
            comboBoxScolarisation.Enabled = false;

            // Événements
            comboBoxType.SelectedIndexChanged += ComboBoxType_SelectedIndexChanged;

            // Événement pour la recherche (si le contrôle existe dans le designer)
            if (textBoxRechercheEmploye != null)
            {
                textBoxRechercheEmploye.TextChanged += TextBoxRechercheEmploye_TextChanged;
            }
        }

        private void ChargerTousLesEmployes()
        {
            try
            {
                var localConnect = new Dbconnect();
                using (var con = localConnect.getconnection)
                {
                    con.Open();
                    const string query = @"
                        SELECT p.id_personnel AS Id,
                               p.nomPrenom AS Nom,
                               p.matricule AS Matricule,
                               e.nomEntreprise AS Entreprise,
                               CONCAT(p.nomPrenom, ' (', p.matricule, ' - ', e.nomEntreprise, ')') AS Display
                        FROM personnel p
                        INNER JOIN entreprise e ON p.id_entreprise = e.id_entreprise
                        ORDER BY p.nomPrenom";

                    using (var cmd = new MySqlCommand(query, con))
                    {
                        var adapter = new MySqlDataAdapter(cmd);
                        tousLesEmployes = new DataTable();
                        adapter.Fill(tousLesEmployes);

                        // Créer une copie pour les filtres
                        employesFiltres = tousLesEmployes.Copy();

                        // Ajouter une ligne vide au début
                        DataRow row = employesFiltres.NewRow();
                        row["Id"] = 0;
                        row["Display"] = "-- Sélectionner un employé --";
                        employesFiltres.Rows.InsertAt(row, 0);

                        comboBoxEmploye.DataSource = employesFiltres;
                        comboBoxEmploye.DisplayMember = "Display";
                        comboBoxEmploye.ValueMember = "Id";
                        comboBoxEmploye.SelectedIndex = 0;

                        // Activer le champ de recherche
                        if (textBoxRechercheEmploye != null)
                        {
                            textBoxRechercheEmploye.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement des employés :\n{ex.Message}", "Erreur",
                    CustomMessageBox.MessageType.Error);
            }
        }

        private void TextBoxRechercheEmploye_TextChanged(object sender, EventArgs e)
        {
            FiltrerEmployes(textBoxRechercheEmploye.Text.Trim());
        }

        private void FiltrerEmployes(string recherche)
        {
            try
            {
                if (tousLesEmployes == null || tousLesEmployes.Rows.Count == 0)
                    return;

                DataTable resultats;
                int nombreResultats = 0;

                // Si la recherche est vide, afficher tous les employés
                if (string.IsNullOrWhiteSpace(recherche))
                {
                    resultats = tousLesEmployes.Copy();
                    nombreResultats = resultats.Rows.Count;

                    // Réinitialiser l'apparence
                    textBoxRechercheEmploye.FillColor = Color.White;
                    textBoxRechercheEmploye.BorderColor = Color.FromArgb(213, 218, 223);
                    lblStatutRecherche.Visible = false;
                }
                else
                {
                    // Filtrer les employés selon le texte de recherche
                    var rechercheLower = recherche.ToLower();
                    var rows = tousLesEmployes.AsEnumerable()
                        .Where(row =>
                            row.Field<string>("Nom").ToLower().Contains(rechercheLower) ||
                            row.Field<string>("Matricule").ToLower().Contains(rechercheLower) ||
                            row.Field<string>("Entreprise").ToLower().Contains(rechercheLower)
                        );

                    if (rows.Any())
                    {
                        resultats = rows.CopyToDataTable();
                        nombreResultats = resultats.Rows.Count;
                    }
                    else
                    {
                        // Aucun résultat
                        resultats = tousLesEmployes.Clone();
                        nombreResultats = 0;
                    }
                }

                employesFiltres = resultats;

                // Ajouter la ligne par défaut
                DataRow defaultRow = employesFiltres.NewRow();
                defaultRow["Id"] = 0;

                // Message adapté selon le nombre de résultats
                if (nombreResultats == 0)
                {
                    defaultRow["Display"] = "-- Aucun employé trouvé --";
                    textBoxRechercheEmploye.FillColor = Color.FromArgb(255, 240, 240); // Rouge clair
                    textBoxRechercheEmploye.BorderColor = Color.FromArgb(239, 68, 68);
                    lblStatutRecherche.Text = "✗ Aucun résultat";
                    lblStatutRecherche.ForeColor = Color.FromArgb(220, 38, 38);
                    lblStatutRecherche.Visible = !string.IsNullOrWhiteSpace(recherche);
                }
                else if (nombreResultats == 1)
                {
                    defaultRow["Display"] = $"✓ 1 employé trouvé - Sélection automatique";
                    textBoxRechercheEmploye.FillColor = Color.FromArgb(240, 255, 240); // Vert clair
                    textBoxRechercheEmploye.BorderColor = Color.FromArgb(76, 175, 80);
                    lblStatutRecherche.Text = "✓ Sélection auto";
                    lblStatutRecherche.ForeColor = Color.FromArgb(34, 197, 94);
                    lblStatutRecherche.Visible = !string.IsNullOrWhiteSpace(recherche);
                }
                else
                {
                    defaultRow["Display"] = $"-- {nombreResultats} employés trouvés --";
                    textBoxRechercheEmploye.FillColor = Color.FromArgb(240, 248, 255); // Bleu clair
                    textBoxRechercheEmploye.BorderColor = Color.FromArgb(59, 130, 246);
                    lblStatutRecherche.Text = $"ℹ {nombreResultats} résultats";
                    lblStatutRecherche.ForeColor = Color.FromArgb(59, 130, 246);
                    lblStatutRecherche.Visible = !string.IsNullOrWhiteSpace(recherche);
                }

                employesFiltres.Rows.InsertAt(defaultRow, 0);

                // Mettre à jour le ComboBox
                comboBoxEmploye.DataSource = employesFiltres;
                comboBoxEmploye.DisplayMember = "Display";
                comboBoxEmploye.ValueMember = "Id";

                // SÉLECTION AUTOMATIQUE si un seul résultat et recherche non vide
                if (nombreResultats == 1 && !string.IsNullOrWhiteSpace(recherche))
                {
                    comboBoxEmploye.SelectedIndex = 1; // Index 1 car 0 est la ligne par défaut

                    // Animation visuelle subtile
                    comboBoxEmploye.BorderColor = Color.FromArgb(76, 175, 80); // Vert
                    System.Threading.Tasks.Task.Delay(300).ContinueWith(_ =>
                    {
                        this.Invoke(new Action(() =>
                        {
                            comboBoxEmploye.BorderColor = Color.FromArgb(213, 218, 223); // Reset
                        }));
                    });
                }
                else
                {
                    comboBoxEmploye.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                // Ignorer les erreurs de filtrage
                System.Diagnostics.Debug.WriteLine($"Erreur de filtrage: {ex.Message}");
            }
        }

        private void ComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Activer la scolarisation uniquement si "Enfant" est sélectionné
            bool estEnfant = comboBoxType.SelectedItem?.ToString() == "Enfant";
            comboBoxScolarisation.Enabled = estEnfant;

            if (!estEnfant)
            {
                comboBoxScolarisation.SelectedIndex = 0;
            }
        }

        private bool ValiderFormulaire(out Charge charge)
        {
            charge = null;

            // Validation
            if (comboBoxEmploye.SelectedValue == null ||
                !int.TryParse(comboBoxEmploye.SelectedValue.ToString(), out int idEmp) || idEmp <= 0)
            {
                CustomMessageBox.Show("Veuillez sélectionner un employé.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                comboBoxEmploye.Focus();
                return false;
            }

            if (comboBoxType.SelectedIndex <= 0)
            {
                CustomMessageBox.Show("Veuillez sélectionner le type de charge.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                comboBoxType.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxNomPrenom.Text))
            {
                CustomMessageBox.Show("Veuillez saisir le nom et prénom.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                textBoxNomPrenom.Focus();
                return false;
            }

            // Si c'est un enfant, vérifier la scolarisation
            if (comboBoxType.SelectedItem.ToString() == "Enfant" && comboBoxScolarisation.SelectedIndex <= 0)
            {
                CustomMessageBox.Show("Veuillez sélectionner la scolarisation.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                comboBoxScolarisation.Focus();
                return false;
            }

            // Créer l'objet Charge
            charge = new Charge
            {
                IdPersonnel = idEmp,
                Type = comboBoxType.SelectedItem.ToString() == "Épouse" ? ChargeType.Epouse : ChargeType.Enfant,
                NomPrenom = textBoxNomPrenom.Text.Trim(),
                DateNaissance = datePickerNaissance.Value,
                Identification = textBoxIdentification.Text.Trim(),
                Scolarisation = comboBoxType.SelectedItem.ToString() == "Enfant" && comboBoxScolarisation.SelectedIndex > 0
                    ? comboBoxScolarisation.SelectedItem.ToString()
                    : null
            };

            return true;
        }

        private void ReinitialiserFormulaire()
        {
            // Réinitialiser les champs de la section 2 uniquement
            comboBoxType.SelectedIndex = 0;
            comboBoxScolarisation.SelectedIndex = 0;
            comboBoxScolarisation.Enabled = false;
            textBoxNomPrenom.Clear();
            textBoxIdentification.Clear();
            datePickerNaissance.Value = DateTime.Now;

            // Focus sur le champ Type pour saisie rapide
            comboBoxType.Focus();
        }

        private void buttonValider_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValiderFormulaire(out Charge charge))
                    return;

                // Ajouter la charge
                ChargeRepository.Ajouter(charge);

                CustomMessageBox.Show("Charge ajoutée avec succès.", "Succès",
                    CustomMessageBox.MessageType.Success);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                CustomMessageBox.Show(ex.Message, "Validation",
                    CustomMessageBox.MessageType.Warning);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de l'ajout de la charge :\n{ex.Message}", "Erreur",
                    CustomMessageBox.MessageType.Error);
            }
        }

        private void buttonValiderContinuer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValiderFormulaire(out Charge charge))
                    return;

                // Ajouter la charge
                ChargeRepository.Ajouter(charge);

                CustomMessageBox.Show($"✓ Charge de {charge.NomPrenom} ajoutée!\n\nVous pouvez ajouter une autre charge.", "Succès",
                    CustomMessageBox.MessageType.Success);

                // Réinitialiser le formulaire pour saisir une nouvelle charge
                ReinitialiserFormulaire();
            }
            catch (InvalidOperationException ex)
            {
                CustomMessageBox.Show(ex.Message, "Validation",
                    CustomMessageBox.MessageType.Warning);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de l'ajout de la charge :\n{ex.Message}", "Erreur",
                    CustomMessageBox.MessageType.Error);
            }
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
