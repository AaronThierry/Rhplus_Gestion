using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class AjouterIndemniteForm : Form
    {
        private Dbconnect connect = new Dbconnect();
        private List<IndemniteLineControl> lignesIndemnites = new List<IndemniteLineControl>();
        private const int MAX_LIGNES = 10; // Limite de lignes

        public AjouterIndemniteForm()
        {
            InitializeComponent();
            InitialiserDonnees();
            ConfigurerRaccourcisClavier();
            AjouterPremiereLigne();

            // Focus automatique sur le premier champ
            this.Shown += (s, e) => comboBoxEntreprise.Focus();
        }

        private void ConfigurerRaccourcisClavier()
        {
            // Escape = Annuler
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    buttonAnnuler_Click(null, null);
                    e.Handled = true;
                }
            };
        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {
            // Dégradé élégant du header
            using (var brush = new LinearGradientBrush(
                panelHeader.ClientRectangle,
                Color.FromArgb(25, 25, 112),    // MidnightBlue
                Color.FromArgb(65, 105, 225),   // RoyalBlue
                LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, panelHeader.ClientRectangle);
            }

            // Ligne accent en bas
            using (var accentBrush = new SolidBrush(Color.FromArgb(46, 139, 87)))
            {
                e.Graphics.FillRectangle(accentBrush, 0, panelHeader.Height - 4, panelHeader.Width, 4);
            }
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
                // Charger les employés de cette entreprise
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

                        // Ajouter une ligne vide au début
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

        private void buttonAjouterLigne_Click(object sender, EventArgs e)
        {
            if (lignesIndemnites.Count >= MAX_LIGNES)
            {
                CustomMessageBox.Show($"Vous ne pouvez pas ajouter plus de {MAX_LIGNES} indemnités à la fois.", "Limite atteinte",
                    CustomMessageBox.MessageType.Warning);
                return;
            }

            AjouterLigneIndemnite();
        }

        private void AjouterLigneIndemnite()
        {
            var ligne = new IndemniteLineControl();
            ligne.Width = 800; // PanelLignes=830px - scrollbar~30px = 800px
            ligne.OnSupprimer += (s, ev) =>
            {
                if (lignesIndemnites.Count > 1)
                {
                    panelLignes.Controls.Remove(ligne);
                    lignesIndemnites.Remove(ligne);
                    ReorganiserLignes();
                }
                else
                {
                    CustomMessageBox.Show("Vous devez avoir au moins une ligne d'indemnité.", "Information",
                        CustomMessageBox.MessageType.Info);
                }
            };

            lignesIndemnites.Add(ligne);
            panelLignes.Controls.Add(ligne);
            ReorganiserLignes();

            // Scroll vers le bas pour voir la nouvelle ligne
            panelLignes.ScrollControlIntoView(ligne);
        }

        private void ReorganiserLignes()
        {
            int y = 12;
            int numero = 1;
            const int largeurLigne = 800;

            foreach (var ligne in lignesIndemnites)
            {
                ligne.SetNumero(numero);
                ligne.Location = new Point(12, y);
                ligne.Width = largeurLigne;
                y += ligne.Height + 12;
                numero++;
            }

            // Activer/Désactiver le bouton d'ajout selon la limite
            buttonAjouterLigne.Enabled = lignesIndemnites.Count < MAX_LIGNES;
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
                    CustomMessageBox.Show(erreur, "Validation",
                        CustomMessageBox.MessageType.Warning);
                    return false;
                }

                var type = ligne.TypeSelectionne;
                var montant = ligne.Montant;

                // Vérifier que le type n'est pas déjà utilisé
                if (typesUtilises.Contains(type))
                {
                    CustomMessageBox.Show($"Le type '{type}' est utilisé plusieurs fois.\nChaque type d'indemnité ne peut être ajouté qu'une seule fois.", "Validation",
                        CustomMessageBox.MessageType.Warning);
                    return false;
                }

                typesUtilises.Add(type);

                // Créer l'objet Indemnite
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

                // Enregistrer toutes les indemnités
                int compteur = 0;
                foreach (var indemnite in indemnites)
                {
                    IndemniteRepository.Ajouter(indemnite);
                    compteur++;
                }

                string message = compteur == 1
                    ? "L'indemnité a été ajoutée avec succès."
                    : $"{compteur} indemnités ont été ajoutées avec succès.";

                CustomMessageBox.Show(message, "Succès",
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

    // ========== CONTROLE PERSONNALISÉ POUR UNE LIGNE D'INDEMNITÉ ==========
    public class IndemniteLineControl : Panel
    {
        private Panel panelNumero;
        private Label labelNumero;
        private Guna.UI2.WinForms.Guna2ComboBox comboType;
        private Guna.UI2.WinForms.Guna2TextBox textMontant;
        private Guna.UI2.WinForms.Guna2Button buttonSupprimer;

        public event EventHandler OnSupprimer;
        private int numeroLigne;

        public IndemniteLineControl()
        {
            InitialiserControles();
        }

        public void SetNumero(int numero)
        {
            numeroLigne = numero;
            labelNumero.Text = $"#{numero}";
        }

        private void InitialiserControles()
        {
            this.Height = 70;
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.None;

            // Custom painting pour bordures et ombre
            this.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (var path = new GraphicsPath())
                {
                    int radius = 12;
                    var rect = new Rectangle(1, 1, this.Width - 3, this.Height - 3);

                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                    path.CloseFigure();

                    // Ombre subtile
                    using (var shadowBrush = new SolidBrush(Color.FromArgb(15, 0, 0, 0)))
                    {
                        var shadowRect = rect;
                        shadowRect.Offset(2, 2);
                        using (var shadowPath = new GraphicsPath())
                        {
                            shadowPath.AddArc(shadowRect.X, shadowRect.Y, radius, radius, 180, 90);
                            shadowPath.AddArc(shadowRect.Right - radius, shadowRect.Y, radius, radius, 270, 90);
                            shadowPath.AddArc(shadowRect.Right - radius, shadowRect.Bottom - radius, radius, radius, 0, 90);
                            shadowPath.AddArc(shadowRect.X, shadowRect.Bottom - radius, radius, radius, 90, 90);
                            shadowPath.CloseFigure();
                            e.Graphics.FillPath(shadowBrush, shadowPath);
                        }
                    }

                    // Fond blanc
                    using (var bgBrush = new SolidBrush(Color.White))
                    {
                        e.Graphics.FillPath(bgBrush, path);
                    }

                    // Bordure élégante
                    using (var borderPen = new Pen(Color.FromArgb(220, 225, 230), 1.5f))
                    {
                        e.Graphics.DrawPath(borderPen, path);
                    }
                }
            };

            // CALCUL FINAL : Largeur=800px
            // Badge(40) + gap(10) + Combo(320) + gap(10) + Text(240) + gap(10) + Btn(80) = 710px (marge 90px)

            // Badge numéro
            panelNumero = new Panel
            {
                Location = new Point(10, 15),
                Size = new Size(40, 40),
                BackColor = Color.FromArgb(25, 25, 112)
            };
            panelNumero.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = new GraphicsPath())
                {
                    int radius = 8;
                    var rect = panelNumero.ClientRectangle;
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                    path.CloseFigure();

                    using (var brush = new SolidBrush(Color.FromArgb(25, 25, 112)))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            };

            labelNumero = new Label
            {
                Text = "#1",
                Dock = DockStyle.Fill,
                Font = new Font("Montserrat", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panelNumero.Controls.Add(labelNumero);

            // ComboBox Type
            comboType = new Guna.UI2.WinForms.Guna2ComboBox
            {
                Location = new Point(60, 15),
                Size = new Size(320, 40),
                Font = new Font("Montserrat", 9.5F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BorderRadius = 8,
                BorderColor = Color.FromArgb(213, 218, 223),
                FillColor = Color.White,
                FocusedColor = Color.FromArgb(25, 25, 112)
            };

            comboType.Items.Add("-- Sélectionner un type --");
            comboType.Items.Add("Logement Numeraire");
            comboType.Items.Add("Fonction");
            comboType.Items.Add("Transport Numeraire");
            comboType.Items.Add("Logement Nature");
            comboType.Items.Add("Transport Nature");
            comboType.Items.Add("Domesticite Nationaux");
            comboType.Items.Add("Domesticite Etrangers");
            comboType.Items.Add("Autres Avantages");
            comboType.SelectedIndex = 0;

            // TextBox Montant
            textMontant = new Guna.UI2.WinForms.Guna2TextBox
            {
                Location = new Point(390, 15),
                Size = new Size(240, 40),
                Font = new Font("Montserrat", 10F, FontStyle.Bold),
                PlaceholderText = "Montant",
                BorderRadius = 8,
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
                Location = new Point(640, 15),
                Size = new Size(80, 40),
                Text = "✕",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                FillColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                BorderRadius = 8,
                Cursor = Cursors.Hand,
                Animated = true
            };
            buttonSupprimer.HoverState.FillColor = Color.FromArgb(200, 35, 51);
            buttonSupprimer.Click += (s, e) => OnSupprimer?.Invoke(this, EventArgs.Empty);

            this.Controls.Add(panelNumero);
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
