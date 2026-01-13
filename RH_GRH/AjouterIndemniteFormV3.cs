using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class AjouterIndemniteFormV3 : Form
    {
        private Dbconnect connect = new Dbconnect();
        private List<IndemniteRowControl> lignesIndemnites = new List<IndemniteRowControl>();
        private const int MAX_LIGNES = 10;
        private DataTable tousLesEmployes;
        private DataTable employesFiltres;
        private Panel headerPanel;

        public AjouterIndemniteFormV3()
        {
            InitializeComponent();
            CreerHeaderIndemnites();
            InitialiserDonnees();
            AjouterPremiereLigne();

            this.Shown += (s, e) => textBoxRecherche.Focus();
        }

        private void CreerHeaderIndemnites()
        {
            // Panel Header avec colonnes
            headerPanel = new Panel
            {
                Location = new Point(15, 30),
                Size = new Size(795, 50),
                BackColor = Color.FromArgb(240, 242, 245),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Label #
            var labelNum = new Label
            {
                Text = "#",
                Location = new Point(10, 15),
                Size = new Size(30, 20),
                Font = new Font("Montserrat", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 58, 64),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Label Type
            var labelType = new Label
            {
                Text = "Type d'indemnité",
                Location = new Point(80, 15),
                Size = new Size(320, 20),
                Font = new Font("Montserrat", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 58, 64),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Label Montant
            var labelMontant = new Label
            {
                Text = "Montant (FCFA)",
                Location = new Point(465, 15),
                Size = new Size(130, 20),
                Font = new Font("Montserrat", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 58, 64),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Label Actions
            var labelActions = new Label
            {
                Text = "Action",
                Location = new Point(680, 15),
                Size = new Size(80, 20),
                Font = new Font("Montserrat", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 58, 64),
                TextAlign = ContentAlignment.MiddleCenter
            };

            headerPanel.Controls.Add(labelNum);
            headerPanel.Controls.Add(labelType);
            headerPanel.Controls.Add(labelMontant);
            headerPanel.Controls.Add(labelActions);

            groupBoxIndemnites.Controls.Add(headerPanel);
            headerPanel.BringToFront();

            // Ajuster position du FlowLayout
            flowLayoutIndemnites.Location = new Point(15, 85);
            flowLayoutIndemnites.Size = new Size(820, 180);
        }

        private void InitialiserDonnees()
        {
            ChargerTousLesEmployes();
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
                               CONCAT(p.nomPrenom, ' (', p.matricule, ') - ', e.nomEntreprise) AS Display
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

                        // Ajouter la ligne par défaut
                        DataRow row = employesFiltres.NewRow();
                        row["Id"] = 0;
                        row["Display"] = "-- Sélectionner un employé --";
                        employesFiltres.Rows.InsertAt(row, 0);

                        comboBoxEmploye.DataSource = employesFiltres;
                        comboBoxEmploye.DisplayMember = "Display";
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

        private void textBoxRecherche_TextChanged(object sender, EventArgs e)
        {
            FiltrerEmployes(textBoxRecherche.Text.Trim());
        }

        private void FiltrerEmployes(string recherche)
        {
            try
            {
                if (tousLesEmployes == null || tousLesEmployes.Rows.Count == 0)
                    return;

                // Si la recherche est vide, afficher tous les employés
                if (string.IsNullOrWhiteSpace(recherche))
                {
                    employesFiltres = tousLesEmployes.Copy();
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

                    employesFiltres = rows.Any() ? rows.CopyToDataTable() : tousLesEmployes.Clone();
                }

                // Ajouter la ligne par défaut
                DataRow defaultRow = employesFiltres.NewRow();
                defaultRow["Id"] = 0;
                defaultRow["Display"] = employesFiltres.Rows.Count > 0
                    ? $"-- {employesFiltres.Rows.Count} employé(s) trouvé(s) --"
                    : "-- Aucun employé trouvé --";
                employesFiltres.Rows.InsertAt(defaultRow, 0);

                // Mettre à jour le ComboBox
                comboBoxEmploye.DataSource = employesFiltres;
                comboBoxEmploye.DisplayMember = "Display";
                comboBoxEmploye.ValueMember = "Id";
                comboBoxEmploye.SelectedIndex = 0;

                // Si un seul résultat, le sélectionner automatiquement
                if (employesFiltres.Rows.Count == 2 && !string.IsNullOrWhiteSpace(recherche))
                {
                    comboBoxEmploye.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                // Gestion silencieuse des erreurs de filtrage
                System.Diagnostics.Debug.WriteLine($"Erreur filtrage: {ex.Message}");
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
            int numero = lignesIndemnites.Count + 1;
            var ligne = new IndemniteRowControl(numero);
            ligne.Width = flowLayoutIndemnites.ClientSize.Width - 25;
            ligne.OnSupprimer += (s, ev) =>
            {
                if (lignesIndemnites.Count > 1)
                {
                    flowLayoutIndemnites.Controls.Remove(ligne);
                    lignesIndemnites.Remove(ligne);
                    // Renuméroter les lignes restantes
                    RenumeroterLignes();
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

        private void RenumeroterLignes()
        {
            for (int i = 0; i < lignesIndemnites.Count; i++)
            {
                lignesIndemnites[i].Numero = i + 1;
            }
        }

        private bool ValiderFormulaire(out int idEmploye, out List<Indemnite> indemnites)
        {
            idEmploye = 0;
            indemnites = new List<Indemnite>();

            // Validation employé
            if (comboBoxEmploye.SelectedValue == null ||
                !int.TryParse(comboBoxEmploye.SelectedValue.ToString(), out idEmploye) || idEmploye <= 0)
            {
                CustomMessageBox.Show("Veuillez sélectionner un employé.", "Validation",
                    CustomMessageBox.MessageType.Warning);
                textBoxRecherche.Focus();
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

    // ========== CONTRÔLE LIGNE D'INDEMNITÉ ==========
    public class IndemniteRowControl : Panel
    {
        private Label labelNumero;
        private Guna.UI2.WinForms.Guna2ComboBox comboType;
        private Guna.UI2.WinForms.Guna2TextBox textMontant;
        private Guna.UI2.WinForms.Guna2Button buttonSupprimer;
        private int numero;

        public event EventHandler OnSupprimer;

        public int Numero
        {
            get => numero;
            set
            {
                numero = value;
                if (labelNumero != null)
                    labelNumero.Text = value.ToString();
            }
        }

        public IndemniteRowControl(int num)
        {
            numero = num;
            InitialiserControles();
        }

        private void InitialiserControles()
        {
            this.Height = 50;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.White;
            this.Margin = new Padding(0, 0, 0, 1);

            // Label Numéro
            labelNumero = new Label
            {
                Text = numero.ToString(),
                Location = new Point(10, 10),
                Size = new Size(30, 30),
                Font = new Font("Montserrat", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(108, 117, 125),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ComboBox Type - Centré dans colonne (50 à 430 = 380px de largeur)
            comboType = new Guna.UI2.WinForms.Guna2ComboBox
            {
                Location = new Point(80, 10),
                Size = new Size(320, 30),
                Font = new Font("Montserrat", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
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

            // TextBox Montant - Aligné avec le combo
            textMontant = new Guna.UI2.WinForms.Guna2TextBox
            {
                Location = new Point(400, 10),
                Size = new Size(130, 30),
                Font = new Font("Montserrat", 9F, FontStyle.Bold),
                PlaceholderText = "0",
                BorderRadius = 6,
                BorderColor = Color.FromArgb(46, 139, 87),
                BorderThickness = 2,
                FillColor = Color.White,
                ForeColor = Color.FromArgb(33, 37, 41),
                TextAlign = HorizontalAlignment.Right
            };
            textMontant.FocusedState.BorderColor = Color.FromArgb(40, 167, 69);

            // Bouton Supprimer - Style Super Professionnel Premium
            buttonSupprimer = new Guna.UI2.WinForms.Guna2Button
            {
                Location = new Point(680, 9),
                Size = new Size(80, 32),
                Text = "",
                FillColor = Color.White,
                BorderRadius = 8,
                Cursor = Cursors.Hand,
                Animated = true,
                BorderThickness = 2,
                BorderColor = Color.FromArgb(220, 53, 69),
                UseTransparentBackground = false,
                ShadowDecoration = { Enabled = true, Depth = 8, Color = Color.FromArgb(50, 220, 53, 69) }
            };

            // États hover et pressed sophistiqués
            buttonSupprimer.HoverState.FillColor = Color.FromArgb(255, 245, 247);
            buttonSupprimer.HoverState.BorderColor = Color.FromArgb(200, 35, 51);
            buttonSupprimer.PressedColor = Color.FromArgb(255, 235, 238);

            // Dessin personnalisé avec gradient et effets
            buttonSupprimer.Paint += (s, pe) =>
            {
                var btn = (Guna.UI2.WinForms.Guna2Button)s;
                pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                pe.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                using (Font iconFont = new Font("Segoe UI Symbol", 11f, FontStyle.Regular))
                using (Font textFont = new Font("Montserrat", 7f, FontStyle.Bold))
                using (SolidBrush iconBrush = new SolidBrush(Color.FromArgb(220, 53, 69)))
                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(108, 117, 125)))
                {
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    // Icône poubelle stylisée en haut
                    Rectangle iconRect = new Rectangle(0, 3, btn.Width, btn.Height / 2);
                    pe.Graphics.DrawString("🗑", iconFont, iconBrush, iconRect, sf);

                    // Texte élégant en bas
                    Rectangle textRect = new Rectangle(0, btn.Height / 2, btn.Width, btn.Height / 2 - 3);
                    pe.Graphics.DrawString("RETIRER", textFont, textBrush, textRect, sf);
                }
            };

            buttonSupprimer.Click += (s, e) => OnSupprimer?.Invoke(this, EventArgs.Empty);

            this.Controls.Add(labelNumero);
            this.Controls.Add(comboType);
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
