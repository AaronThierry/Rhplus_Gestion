using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class AjouterIndemniteFormV2 : Form
    {
        private Dbconnect connect = new Dbconnect();
        private List<IndemniteRowControl> lignesIndemnites = new List<IndemniteRowControl>();
        private const int MAX_LIGNES = 10;

        public AjouterIndemniteFormV2()
        {
            InitializeComponent();
            InitialiserDonnees();
            AjouterPremiereLigne();

            this.Shown += (s, e) => comboBoxEntreprise.Focus();
        }

        private void InitialiserDonnees()
        {
            // Charger les entreprises
            EntrepriseClass.ChargerEntreprises(comboBoxEntreprise, null, true);

            // Événements
            comboBoxEntreprise.SelectedIndexChanged += ComboBoxEntreprise_SelectedIndexChanged;
        }

        private void ComboBoxEntreprise_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEntreprise.SelectedValue != null &&
                int.TryParse(comboBoxEntreprise.SelectedValue.ToString(), out int idEnt) && idEnt > 0)
            {
                ChargerEmployes(idEnt);
            }
            else
            {
                comboBoxEmploye.DataSource = null;
            }
        }

        private void ChargerEmployes(int idEntreprise)
        {
            try
            {
                var localConnect = new Dbconnect();
                using (var con = localConnect.getconnection)
                {
                    con.Open();
                    const string query = @"
                        SELECT p.id_personnel AS Id,
                               CONCAT(p.nomPrenom, ' (', p.matricule, ')') AS Nom
                        FROM personnel p
                        WHERE p.id_entreprise = @idEnt
                        ORDER BY p.nomPrenom";

                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@idEnt", idEntreprise);
                        var adapter = new MySqlDataAdapter(cmd);
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        DataRow row = dt.NewRow();
                        row["Id"] = 0;
                        row["Nom"] = "-- Sélectionner un employé --";
                        dt.Rows.InsertAt(row, 0);

                        comboBoxEmploye.DataSource = dt;
                        comboBoxEmploye.DisplayMember = "Nom";
                        comboBoxEmploye.ValueMember = "Id";
                        comboBoxEmploye.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement des employés :\n{ex.Message}", "Erreur",
                    CustomMessageBox.MessageType.Error);
            }
        }

        private void AjouterPremiereLigne()
        {
            AjouterLigneIndemnite();
        }

        private void buttonAjouterIndemnite_Click(object sender, EventArgs e)
        {
            if (lignesIndemnites.Count >= MAX_LIGNES)
            {
                CustomMessageBox.Show($"Vous ne pouvez pas ajouter plus de {MAX_LIGNES} indemnités à la fois.",
                    "Limite atteinte", CustomMessageBox.MessageType.Warning);
                return;
            }

            AjouterLigneIndemnite();
        }

        private void AjouterLigneIndemnite()
        {
            var ligne = new IndemniteRowControl();
            ligne.Width = flowLayoutIndemnites.ClientSize.Width - 25;
            ligne.OnSupprimer += (s, ev) =>
            {
                if (lignesIndemnites.Count > 1)
                {
                    flowLayoutIndemnites.Controls.Remove(ligne);
                    lignesIndemnites.Remove(ligne);
                }
                else
                {
                    CustomMessageBox.Show("Vous devez avoir au moins une ligne d'indemnité.", "Information",
                        CustomMessageBox.MessageType.Info);
                }
            };

            lignesIndemnites.Add(ligne);
            flowLayoutIndemnites.Controls.Add(ligne);
            flowLayoutIndemnites.ScrollControlIntoView(ligne);

            // Gérer bouton d'ajout
            buttonAjouterIndemnite.Enabled = lignesIndemnites.Count < MAX_LIGNES;
        }

        private bool ValiderFormulaire(out int idEmploye, out List<Indemnite> indemnites)
        {
            idEmploye = 0;
            indemnites = new List<Indemnite>();

            // Validation entreprise
            if (comboBoxEntreprise.SelectedValue == null ||
                !int.TryParse(comboBoxEntreprise.SelectedValue.ToString(), out int idEnt) || idEnt <= 0)
            {
                CustomMessageBox.Show("Veuillez sélectionner une entreprise.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                comboBoxEntreprise.Focus();
                return false;
            }

            // Validation employé
            if (comboBoxEmploye.SelectedValue == null ||
                !int.TryParse(comboBoxEmploye.SelectedValue.ToString(), out idEmploye) || idEmploye <= 0)
            {
                CustomMessageBox.Show("Veuillez sélectionner un employé.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                comboBoxEmploye.Focus();
                return false;
            }

            // Validation des lignes d'indemnités
            var typesUtilises = new HashSet<string>();

            foreach (var ligne in lignesIndemnites)
            {
                if (!ligne.EstValide(out string erreur))
                {
                    CustomMessageBox.Show(erreur, "Validation", CustomMessageBox.MessageType.Warning);
                    return false;
                }

                var type = ligne.TypeSelectionne;
                var montant = ligne.Montant;

                if (typesUtilises.Contains(type))
                {
                    CustomMessageBox.Show($"Le type '{type}' est utilisé plusieurs fois.\nChaque type d'indemnité ne peut être ajouté qu'une seule fois.",
                        "Validation", CustomMessageBox.MessageType.Warning);
                    return false;
                }

                typesUtilises.Add(type);

                var typeEnum = ConvertirTypeString(type);
                indemnites.Add(new Indemnite
                {
                    IdPersonnel = idEmploye,
                    Type = typeEnum,
                    Valeur = montant
                });
            }

            if (indemnites.Count == 0)
            {
                CustomMessageBox.Show("Veuillez ajouter au moins une indemnité.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                return false;
            }

            return true;
        }

        private IndemniteType ConvertirTypeString(string typeLabel)
        {
            switch ((typeLabel ?? "").Trim())
            {
                case "Logement Numeraire": return IndemniteType.LogementNumeraire;
                case "Fonction": return IndemniteType.Fonction;
                case "Transport Numeraire": return IndemniteType.TransportNumeraire;
                case "Logement Nature": return IndemniteType.LogementNature;
                case "Transport Nature": return IndemniteType.TransportNature;
                case "Domesticite Nationaux": return IndemniteType.DomesticiteNationaux;
                case "Domesticite Etrangers": return IndemniteType.DomesticiteEtrangers;
                case "Autres Avantages": return IndemniteType.AutresAvantages;
                default: return IndemniteType.AutresAvantages;
            }
        }

        private void buttonValider_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValiderFormulaire(out int idEmploye, out List<Indemnite> indemnites))
                    return;

                int compteur = 0;
                foreach (var indemnite in indemnites)
                {
                    IndemniteRepository.Ajouter(indemnite);
                    compteur++;
                }

                string message = compteur == 1
                    ? "L'indemnité a été ajoutée avec succès."
                    : $"{compteur} indemnités ont été ajoutées avec succès.";

                CustomMessageBox.Show(message, "Succès", CustomMessageBox.MessageType.Success);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                CustomMessageBox.Show(ex.Message, "Validation", CustomMessageBox.MessageType.Warning);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de l'ajout des indemnités :\n{ex.Message}", "Erreur",
                    CustomMessageBox.MessageType.Error);
            }
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    // ========== CONTRÔLE LIGNE D'INDEMNITÉ (SIMPLE) ==========
    public class IndemniteRowControl : Panel
    {
        private Label labelType;
        private Guna.UI2.WinForms.Guna2ComboBox comboType;
        private Label labelMontant;
        private Guna.UI2.WinForms.Guna2TextBox textMontant;
        private Guna.UI2.WinForms.Guna2Button buttonSupprimer;

        public event EventHandler OnSupprimer;

        public IndemniteRowControl()
        {
            InitialiserControles();
        }

        private void InitialiserControles()
        {
            this.Height = 70;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.Margin = new Padding(0, 0, 0, 10);

            // Label Type
            labelType = new Label
            {
                Text = "Type d'indemnité",
                Location = new Point(15, 10),
                Size = new Size(120, 20),
                Font = new Font("Montserrat", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(73, 80, 87)
            };

            // ComboBox Type
            comboType = new Guna.UI2.WinForms.Guna2ComboBox
            {
                Location = new Point(15, 32),
                Size = new Size(350, 30),
                Font = new Font("Montserrat", 9F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BorderRadius = 6,
                BorderColor = Color.FromArgb(213, 218, 223),
                FillColor = Color.White,
                FocusedColor = Color.FromArgb(25, 25, 112)
            };

            comboType.Items.Add("-- Sélectionner --");
            comboType.Items.Add("Logement Numeraire");
            comboType.Items.Add("Fonction");
            comboType.Items.Add("Transport Numeraire");
            comboType.Items.Add("Logement Nature");
            comboType.Items.Add("Transport Nature");
            comboType.Items.Add("Domesticite Nationaux");
            comboType.Items.Add("Domesticite Etrangers");
            comboType.Items.Add("Autres Avantages");
            comboType.SelectedIndex = 0;

            // Label Montant
            labelMontant = new Label
            {
                Text = "Montant (FCFA)",
                Location = new Point(380, 10),
                Size = new Size(120, 20),
                Font = new Font("Montserrat", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(73, 80, 87)
            };

            // TextBox Montant
            textMontant = new Guna.UI2.WinForms.Guna2TextBox
            {
                Location = new Point(380, 32),
                Size = new Size(280, 30),
                Font = new Font("Montserrat", 9.5F, FontStyle.Bold),
                PlaceholderText = "0",
                BorderRadius = 6,
                BorderColor = Color.FromArgb(46, 139, 87),
                BorderThickness = 2,
                FillColor = Color.White,
                ForeColor = Color.FromArgb(33, 37, 41),
                TextAlign = HorizontalAlignment.Right
            };
            textMontant.FocusedState.BorderColor = Color.FromArgb(40, 167, 69);

            // Bouton Supprimer
            buttonSupprimer = new Guna.UI2.WinForms.Guna2Button
            {
                Location = new Point(675, 28),
                Size = new Size(100, 35),
                Text = "Supprimer",
                Font = new Font("Montserrat", 8F, FontStyle.Bold),
                FillColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                BorderRadius = 6,
                Cursor = Cursors.Hand,
                Animated = true
            };
            buttonSupprimer.HoverState.FillColor = Color.FromArgb(200, 35, 51);
            buttonSupprimer.Click += (s, e) => OnSupprimer?.Invoke(this, EventArgs.Empty);

            this.Controls.Add(labelType);
            this.Controls.Add(comboType);
            this.Controls.Add(labelMontant);
            this.Controls.Add(textMontant);
            this.Controls.Add(buttonSupprimer);
        }

        public string TypeSelectionne => comboType.SelectedItem?.ToString() ?? "";

        public decimal Montant
        {
            get
            {
                if (decimal.TryParse(textMontant.Text.Trim().Replace(",", "."),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out decimal val))
                {
                    return val;
                }
                return 0;
            }
        }

        public bool EstValide(out string erreur)
        {
            erreur = "";

            if (comboType.SelectedIndex <= 0)
            {
                erreur = "Veuillez sélectionner un type d'indemnité.";
                comboType.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textMontant.Text))
            {
                erreur = "Veuillez saisir un montant.";
                textMontant.Focus();
                return false;
            }

            if (Montant <= 0)
            {
                erreur = "Le montant doit être supérieur à 0.";
                textMontant.Focus();
                return false;
            }

            return true;
        }
    }
}
