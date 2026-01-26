using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class AjouterEmployeForm : Form
    {
        private Dbconnect connect = new Dbconnect();
        private Guna.UI2.WinForms.Guna2TextBox textBoxMatricule;
        private Guna.UI2.WinForms.Guna2CheckBox checkBoxMatriculeManuel;
        private System.Windows.Forms.Label labelMatricule;

        public AjouterEmployeForm()
        {
            InitializeComponent();
            AjouterControlesMatricule();
            InitialiserDonnees();
        }

        /// <summary>
        /// Ajoute dynamiquement les contrôles pour le matricule dans le formulaire
        /// </summary>
        private void AjouterControlesMatricule()
        {
            // Trouver le GroupBox des informations personnelles
            var groupBoxInfoPerso = this.Controls.Find("groupBoxInfoPerso", true).FirstOrDefault() as GroupBox;
            if (groupBoxInfoPerso == null) return;

            // === SECTION TITRE AVEC ICÔNE ===
            var panelMatriculeHeader = new Panel
            {
                Location = new Point(30, 395),
                Size = new Size(540, 35),
                BackColor = Color.Transparent
            };

            // Label principal pour le matricule avec style moderne
            labelMatricule = new Label
            {
                AutoSize = false,
                Font = new Font("Montserrat", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 120, 140),
                Location = new Point(0, 0),
                Size = new Size(150, 30),
                Text = "● Matricule",
                TextAlign = ContentAlignment.MiddleLeft
            };

            // CheckBox élégant pour la saisie manuelle
            checkBoxMatriculeManuel = new Guna.UI2.WinForms.Guna2CheckBox
            {
                AutoSize = true,
                CheckedState = {
                    BorderColor = Color.FromArgb(20, 120, 140),
                    BorderRadius = 2,
                    BorderThickness = 2,
                    FillColor = Color.FromArgb(20, 120, 140)
                },
                Font = new Font("Montserrat", 8.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(90, 90, 90),
                Location = new Point(160, 5),
                Name = "checkBoxMatriculeManuel",
                Size = new Size(140, 22),
                TabIndex = 21,
                Text = "⚙ Saisie manuelle",
                UncheckedState = {
                    BorderColor = Color.FromArgb(189, 195, 199),
                    BorderRadius = 2,
                    BorderThickness = 2,
                    FillColor = Color.White
                }
            };
            checkBoxMatriculeManuel.CheckedChanged += CheckBoxMatriculeManuel_CheckedChanged;

            // Label d'aide contextuelle
            var labelAide = new Label
            {
                AutoSize = false,
                Font = new Font("Montserrat", 7.5F, FontStyle.Italic),
                ForeColor = Color.FromArgb(149, 165, 166),
                Location = new Point(310, 7),
                Size = new Size(230, 20),
                Text = "Auto: XX###A | Manuel: Format libre",
                TextAlign = ContentAlignment.MiddleLeft
            };

            panelMatriculeHeader.Controls.Add(labelMatricule);
            panelMatriculeHeader.Controls.Add(checkBoxMatriculeManuel);
            panelMatriculeHeader.Controls.Add(labelAide);

            // === TEXTBOX AVEC DESIGN MODERNE ===
            textBoxMatricule = new Guna.UI2.WinForms.Guna2TextBox
            {
                BorderColor = Color.FromArgb(189, 195, 199),
                BorderRadius = 3,
                BorderThickness = 2,
                Cursor = Cursors.IBeam,
                DefaultText = "",
                Enabled = false,
                Font = new Font("Consolas", 11F, FontStyle.Bold), // Police monospace pour matricules
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(30, 435),
                Margin = new Padding(5, 7, 5, 7),
                MaxLength = 20,
                Name = "textBoxMatricule",
                PlaceholderForeColor = Color.FromArgb(149, 165, 166),
                PlaceholderText = "● Généré automatiquement lors de l'enregistrement",
                SelectedText = "",
                Size = new Size(540, 45),
                TabIndex = 22,
                TextAlign = HorizontalAlignment.Left
            };
            textBoxMatricule.TextChanged += TextBoxMatricule_TextChanged;

            // Panel pour l'indicateur de validation (ajouté dynamiquement)
            var panelValidation = new Panel
            {
                Location = new Point(30, 485),
                Size = new Size(540, 25),
                BackColor = Color.Transparent,
                Name = "panelValidationMatricule",
                Visible = false
            };

            var labelValidation = new Label
            {
                AutoSize = false,
                Font = new Font("Montserrat", 7.5F, FontStyle.Regular),
                Location = new Point(0, 0),
                Name = "labelValidationMatricule",
                Size = new Size(540, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelValidation.Controls.Add(labelValidation);

            // Ajouter les contrôles au GroupBox dans le bon ordre
            groupBoxInfoPerso.Controls.Add(panelMatriculeHeader);
            groupBoxInfoPerso.Controls.Add(textBoxMatricule);
            groupBoxInfoPerso.Controls.Add(panelValidation);

            // Amener les nouveaux contrôles au premier plan (Z-order)
            panelMatriculeHeader.BringToFront();
            textBoxMatricule.BringToFront();
            panelValidation.BringToFront();

            // Ajuster la hauteur du GroupBox pour accueillir les nouveaux contrôles
            // Les contrôles matricule vont jusqu'à Y=510, donc on a besoin de 510 - 25 (Location Y du GroupBox) = 485 + 20 de padding
            int requiredHeight = 505;
            if (groupBoxInfoPerso.Height < requiredHeight)
            {
                groupBoxInfoPerso.Height = requiredHeight;
            }
        }

        /// <summary>
        /// Gère le changement d'état du CheckBox matricule manuel
        /// </summary>
        private void CheckBoxMatriculeManuel_CheckedChanged(object sender, EventArgs e)
        {
            var groupBoxInfoPerso = this.Controls.Find("groupBoxInfoPerso", true).FirstOrDefault() as GroupBox;
            var panelValidation = groupBoxInfoPerso?.Controls.Find("panelValidationMatricule", true).FirstOrDefault() as Panel;

            if (checkBoxMatriculeManuel.Checked)
            {
                // ✨ MODE MANUEL ACTIVÉ - Design élégant et interactif
                textBoxMatricule.Enabled = true;
                textBoxMatricule.PlaceholderText = "✎ Saisissez votre matricule (ex: EMP-001, STAFF123)";
                textBoxMatricule.BorderColor = Color.FromArgb(52, 152, 219); // Bleu vif
                textBoxMatricule.FocusedState.BorderColor = Color.FromArgb(20, 120, 140);
                textBoxMatricule.BackColor = Color.FromArgb(250, 252, 255); // Fond légèrement bleuté

                // Afficher le panel de validation
                if (panelValidation != null)
                    panelValidation.Visible = true;

                // Focus avec transition douce
                textBoxMatricule.Focus();
            }
            else
            {
                // 🔄 MODE AUTOMATIQUE - Design neutre et désactivé
                textBoxMatricule.Enabled = false;
                textBoxMatricule.Text = "";
                textBoxMatricule.PlaceholderText = "● Généré automatiquement lors de l'enregistrement";
                textBoxMatricule.BorderColor = Color.FromArgb(189, 195, 199);
                textBoxMatricule.BackColor = Color.FromArgb(248, 249, 250); // Fond gris très clair

                // Masquer le panel de validation
                if (panelValidation != null)
                    panelValidation.Visible = false;
            }
        }

        /// <summary>
        /// Valide le matricule en temps réel pendant la saisie avec feedback visuel élégant
        /// </summary>
        private void TextBoxMatricule_TextChanged(object sender, EventArgs e)
        {
            var groupBoxInfoPerso = this.Controls.Find("groupBoxInfoPerso", true).FirstOrDefault() as GroupBox;
            var panelValidation = groupBoxInfoPerso?.Controls.Find("panelValidationMatricule", true).FirstOrDefault() as Panel;
            var labelValidation = panelValidation?.Controls.Find("labelValidationMatricule", true).FirstOrDefault() as Label;

            if (!checkBoxMatriculeManuel.Checked || string.IsNullOrWhiteSpace(textBoxMatricule.Text))
            {
                // État neutre - pas de validation
                textBoxMatricule.BorderColor = Color.FromArgb(189, 195, 199);
                if (panelValidation != null) panelValidation.Visible = false;
                return;
            }

            string matricule = MatriculeGenerator.NormaliserMatricule(textBoxMatricule.Text);
            var validation = MatriculeGenerator.ValiderFormatMatricule(matricule);

            if (panelValidation != null) panelValidation.Visible = true;

            if (!validation.IsValid)
            {
                // 🔴 FORMAT INVALIDE - Design d'erreur élégant
                textBoxMatricule.BorderColor = Color.FromArgb(231, 76, 60);
                textBoxMatricule.FocusedState.BorderColor = Color.FromArgb(192, 57, 43);

                if (labelValidation != null)
                {
                    labelValidation.Text = $"✕ {validation.ErrorMessage}";
                    labelValidation.ForeColor = Color.FromArgb(231, 76, 60);
                }
            }
            else if (MatriculeGenerator.MatriculeExiste(matricule))
            {
                // 🟠 MATRICULE EXISTANT - Warning design
                textBoxMatricule.BorderColor = Color.FromArgb(243, 156, 18);
                textBoxMatricule.FocusedState.BorderColor = Color.FromArgb(211, 84, 0);

                if (labelValidation != null)
                {
                    labelValidation.Text = $"⚠ Ce matricule existe déjà. Veuillez en choisir un autre.";
                    labelValidation.ForeColor = Color.FromArgb(243, 156, 18);
                }
            }
            else
            {
                // ✅ MATRICULE VALIDE - Success design
                textBoxMatricule.BorderColor = Color.FromArgb(46, 204, 113);
                textBoxMatricule.FocusedState.BorderColor = Color.FromArgb(39, 174, 96);

                if (labelValidation != null)
                {
                    labelValidation.Text = $"✓ Matricule valide : {matricule}";
                    labelValidation.ForeColor = Color.FromArgb(39, 174, 96);
                }
            }
        }

        private void InitialiserDonnees()
        {
            // Charger les entreprises
            EntrepriseClass.ChargerEntreprises(comboBoxEntreprise, null, true);

            // Initialiser les combos avec des valeurs par défaut
            comboBoxCivilite.Items.AddRange(new string[] { "-- Sélectionner --", "M.", "Mme", "Mlle" });
            comboBoxCivilite.SelectedIndex = 0;

            comboBoxSexe.Items.AddRange(new string[] { "-- Sélectionner --", "Masculin", "Féminin" });
            comboBoxSexe.SelectedIndex = 0;

            comboBoxContrat.Items.AddRange(new string[] { "-- Sélectionner --", "CDI", "CDD" });
            comboBoxContrat.SelectedIndex = 0;

            comboBoxTypeContrat.Items.AddRange(new string[] { "-- Sélectionner --", "Horaire", "Journalier", "Mensuel" });
            comboBoxTypeContrat.SelectedIndex = 0;

            comboBoxModePayement.Items.AddRange(new string[] { "-- Sélectionner --", "Espèces", "Virement bancaire", "Chèque", "Mobile Money" });
            comboBoxModePayement.SelectedIndex = 0;

            comboBoxCadre.Items.AddRange(new string[] { "-- Sélectionner --", "Cadre", "Non-Cadre" });
            comboBoxCadre.SelectedIndex = 0;

            // Initialiser le ComboBox DureeContrat
            comboBoxDureeContrat.Items.AddRange(new string[] { "-- Sélectionner --", "Permanent", "Temporaire", "Occasionnel", "Volontaire National", "Stagiaire" });
            comboBoxDureeContrat.SelectedIndex = 1; // Par défaut "Permanent"

            checkBoxDateSortie.CheckedChanged += CheckBoxDateSortie_CheckedChanged;
            datePickerSortie.Enabled = false;

            comboBoxEntreprise.SelectedIndexChanged += ComboBoxEntreprise_SelectedIndexChanged;
            comboBoxDirection.SelectedIndexChanged += ComboBoxDirection_SelectedIndexChanged;
            comboBoxService.SelectedIndexChanged += ComboBoxService_SelectedIndexChanged;
            comboBoxModePayement.SelectedIndexChanged += ComboBoxModePayement_SelectedIndexChanged;

            // Initialiser l'état des champs bancaires
            UpdateModePayementFields();
        }

        private void CheckBoxDateSortie_CheckedChanged(object sender, EventArgs e)
        {
            datePickerSortie.Enabled = checkBoxDateSortie.Checked;
            if (!checkBoxDateSortie.Checked)
            {
                datePickerSortie.Value = DateTime.Now;
            }
        }

        private void ComboBoxEntreprise_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEntreprise.SelectedValue != null &&
                int.TryParse(comboBoxEntreprise.SelectedValue.ToString(), out int idEnt) && idEnt > 0)
            {
                DirectionClass.ChargerDirections(comboBoxDirection, idEnt, null, true);
                ServiceClass.ChargerServices(comboBoxService, idEnt, null, true);
                Categorie.ChargerCategories(comboBoxCategorie, idEnt, null, true);
            }
            else
            {
                comboBoxDirection.DataSource = null;
                comboBoxService.DataSource = null;
                comboBoxCategorie.DataSource = null;
            }
        }

        private void ComboBoxDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            // La relation entre direction et service n'existe pas dans la structure de la base de données
            // Les services sont liés uniquement à l'entreprise
        }

        private void ComboBoxService_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Peut être utilisé pour des actions futures
        }

        /// <summary>
        /// Gestionnaire pour le changement de mode de paiement
        /// </summary>
        private void ComboBoxModePayement_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateModePayementFields();
        }

        /// <summary>
        /// Met à jour la visibilité et les labels des champs selon le mode de paiement sélectionné
        /// </summary>
        private void UpdateModePayementFields()
        {
            string modePayement = comboBoxModePayement.SelectedItem?.ToString() ?? "";

            // Réinitialiser les champs
            textBoxBanque.Clear();
            textBoxNumeroBancaire.Clear();

            // Logique conditionnelle selon le mode de paiement
            switch (modePayement)
            {
                case "Espèces":
                    // Masquer les champs bancaires pour espèces
                    textBoxBanque.Enabled = false;
                    textBoxBanque.Visible = false;
                    textBoxNumeroBancaire.Enabled = false;
                    textBoxNumeroBancaire.Visible = false;
                    label24.Visible = false; // Label Banque
                    label23.Visible = false; // Label Numéro Bancaire
                    break;

                case "Virement bancaire":
                    // Afficher banque et numéro de compte
                    textBoxBanque.Enabled = true;
                    textBoxBanque.Visible = true;
                    textBoxBanque.PlaceholderText = "Nom de la banque";
                    textBoxNumeroBancaire.Enabled = true;
                    textBoxNumeroBancaire.Visible = true;
                    textBoxNumeroBancaire.PlaceholderText = "Numéro de compte bancaire";
                    label24.Visible = true;
                    label24.Text = "Banque :";
                    label23.Visible = true;
                    label23.Text = "Numéro de compte :";
                    break;

                case "Chèque":
                    // Afficher banque et numéro de chèque
                    textBoxBanque.Enabled = true;
                    textBoxBanque.Visible = true;
                    textBoxBanque.PlaceholderText = "Nom de la banque";
                    textBoxNumeroBancaire.Enabled = true;
                    textBoxNumeroBancaire.Visible = true;
                    textBoxNumeroBancaire.PlaceholderText = "Numéro de chèque";
                    label24.Visible = true;
                    label24.Text = "Banque :";
                    label23.Visible = true;
                    label23.Text = "Numéro de chèque :";
                    break;

                case "Mobile Money":
                    // Afficher opérateur et numéro
                    textBoxBanque.Enabled = true;
                    textBoxBanque.Visible = true;
                    textBoxBanque.PlaceholderText = "Opérateur (Orange, Moov, etc.)";
                    textBoxNumeroBancaire.Enabled = true;
                    textBoxNumeroBancaire.Visible = true;
                    textBoxNumeroBancaire.PlaceholderText = "Numéro de téléphone";
                    label24.Visible = true;
                    label24.Text = "Opérateur :";
                    label23.Visible = true;
                    label23.Text = "Numéro :";
                    break;

                default:
                    // Par défaut, masquer les champs
                    textBoxBanque.Enabled = false;
                    textBoxBanque.Visible = false;
                    textBoxNumeroBancaire.Enabled = false;
                    textBoxNumeroBancaire.Visible = false;
                    label24.Visible = false;
                    label23.Visible = false;
                    break;
            }
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonValider_Click(object sender, EventArgs e)
        {
            string logPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "rh_debug_log.txt");
            System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: DEBUT buttonValider_Click\n");
            try
            {
                System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: Dans le try\n");
                // Validation des champs obligatoires
                if (string.IsNullOrWhiteSpace(textBoxNomPrenom.Text))
                {
                    CustomMessageBox.Show("Veuillez saisir le nom et prénom de l'employé.", "Information",
                        CustomMessageBox.MessageType.Info);
                    textBoxNomPrenom.Focus();
                    return;
                }

                if (comboBoxEntreprise.SelectedValue == null ||
                    !int.TryParse(comboBoxEntreprise.SelectedValue.ToString(), out int idEntreprise) || idEntreprise <= 0)
                {
                    CustomMessageBox.Show("Veuillez sélectionner une entreprise.", "Information",
                        CustomMessageBox.MessageType.Info);
                    comboBoxEntreprise.Focus();
                    return;
                }

                if (comboBoxCategorie.SelectedValue == null ||
                    !int.TryParse(comboBoxCategorie.SelectedValue.ToString(), out int idCategorie) || idCategorie <= 0)
                {
                    CustomMessageBox.Show("Veuillez sélectionner une catégorie.", "Information",
                        CustomMessageBox.MessageType.Info);
                    comboBoxCategorie.Focus();
                    return;
                }

                // Récupération des valeurs
                string nomPrenom = textBoxNomPrenom.Text.Trim();
                string civilite = comboBoxCivilite.SelectedItem?.ToString();
                if (civilite == "-- Sélectionner --") civilite = null;

                string sexe = comboBoxSexe.SelectedItem?.ToString();
                if (sexe == "-- Sélectionner --") sexe = null;

                DateTime? dateNaissance = datePickerNaissance.Checked ? datePickerNaissance.Value : (DateTime?)null;
                string adresse = textBoxAdresse.Text.Trim();
                string telephone = textBoxTelephone.Text.Trim();
                string poste = textBoxPoste.Text.Trim();
                string cnss = textBoxCnss.Text.Trim();
                string identification = textBoxIdentification.Text.Trim();

                int? idDirection = null;
                if (comboBoxDirection.SelectedValue != null &&
                    int.TryParse(comboBoxDirection.SelectedValue.ToString(), out int dirId) && dirId > 0)
                {
                    idDirection = dirId;
                }

                int? idService = null;
                if (comboBoxService.SelectedValue != null &&
                    int.TryParse(comboBoxService.SelectedValue.ToString(), out int servId) && servId > 0)
                {
                    idService = servId;
                }

                string contrat = comboBoxContrat.SelectedItem?.ToString();
                if (contrat == "-- Sélectionner --") contrat = "CDI";

                string typeContrat = comboBoxTypeContrat.SelectedItem?.ToString();
                if (typeContrat == "-- Sélectionner --") typeContrat = null;

                string modePayement = comboBoxModePayement.SelectedItem?.ToString();
                if (modePayement == "-- Sélectionner --") modePayement = null;

                string cadre = comboBoxCadre.SelectedItem?.ToString();
                if (cadre == "-- Sélectionner --") cadre = null;

                DateTime dateEntree = datePickerEntree.Value;
                DateTime? dateSortie = checkBoxDateSortie.Checked ? datePickerSortie.Value : (DateTime?)null;

                decimal? heureContrat = null;
                if (!string.IsNullOrWhiteSpace(textBoxHeureContrat.Text) &&
                    decimal.TryParse(textBoxHeureContrat.Text.Trim(), out decimal heures))
                {
                    heureContrat = heures;
                }

                int? jourContrat = null;
                if (!string.IsNullOrWhiteSpace(textBoxJourContrat.Text) &&
                    int.TryParse(textBoxJourContrat.Text.Trim(), out int jours))
                {
                    jourContrat = jours;
                }

                string numeroBancaire = textBoxNumeroBancaire.Text.Trim();
                string banque = textBoxBanque.Text.Trim();

                decimal? salaireMoyen = null;
                if (!string.IsNullOrWhiteSpace(textBoxSalaireMoyen.Text) &&
                    decimal.TryParse(textBoxSalaireMoyen.Text.Trim(), out decimal salaire))
                {
                    salaireMoyen = salaire;
                }

                // Gestion du matricule (manuel ou automatique)
                System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: Avant gestion matricule\n");
                string matricule;

                if (checkBoxMatriculeManuel.Checked && !string.IsNullOrWhiteSpace(textBoxMatricule.Text))
                {
                    // Mode manuel - Valider et normaliser le matricule saisi
                    matricule = MatriculeGenerator.NormaliserMatricule(textBoxMatricule.Text);

                    // Validation du format
                    var validation = MatriculeGenerator.ValiderFormatMatricule(matricule);
                    if (!validation.IsValid)
                    {
                        CustomMessageBox.Show(validation.ErrorMessage, "Matricule invalide",
                            CustomMessageBox.MessageType.Warning);
                        textBoxMatricule.Focus();
                        return;
                    }

                    // Vérification d'unicité
                    if (MatriculeGenerator.MatriculeExiste(matricule))
                    {
                        CustomMessageBox.Show("Ce matricule existe déjà dans la base de données. Veuillez en saisir un autre.",
                            "Matricule en double", CustomMessageBox.MessageType.Warning);
                        textBoxMatricule.Focus();
                        return;
                    }

                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: Matricule manuel validé: {matricule}\n");
                }
                else
                {
                    // Mode automatique - Générer le matricule
                    matricule = MatriculeGenerator.GenererMatricule(idEntreprise);
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: Matricule généré automatiquement: {matricule}\n");
                }

                // Insertion dans la base de données
                System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: Avant requête SQL\n");

                // Récupérer la durée de contrat du ComboBox
                string dureeContrat = comboBoxDureeContrat.SelectedItem?.ToString();
                if (dureeContrat == "-- Sélectionner --" || string.IsNullOrWhiteSpace(dureeContrat))
                {
                    CustomMessageBox.Show("Veuillez sélectionner une durée de contrat.", "Validation",
                        CustomMessageBox.MessageType.Warning);
                    return;
                }
                dureeContrat = dureeContrat.ToLower(); // Convertir en minuscules pour correspondre aux valeurs attendues

                string query = @"INSERT INTO personnel (
                    nomPrenom, matricule, civilite, sexe, date_naissance, adresse, telephone, poste, numerocnss, identification,
                    id_entreprise, id_direction, id_service, id_categorie,
                    contrat, typeContrat, modePayement, cadre, date_entree, date_sortie,
                    heureContrat, jourContrat, numeroBancaire, banque, salairemoyen, dureeContrat
                ) VALUES (
                    @nomPrenom, @matricule, @civilite, @sexe, @dateNaissance, @adresse, @telephone, @poste, @numerocnss, @identification,
                    @idEntreprise, @idDirection, @idService, @idCategorie,
                    @contrat, @typeContrat, @modePayement, @cadre, @dateEntree, @dateSortie,
                    @heureContrat, @jourContrat, @numeroBancaire, @banque, @salaireMoyen, @dureeContrat
                )";

                System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: Requête SQL créée\n");
                using (MySqlCommand cmd = new MySqlCommand(query, connect.getconnection))
                {
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: Commande MySQL créée\n");
                    cmd.Parameters.AddWithValue("@nomPrenom", nomPrenom);
                    cmd.Parameters.AddWithValue("@matricule", matricule);
                    cmd.Parameters.AddWithValue("@civilite", (object)civilite ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@sexe", (object)sexe ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@dateNaissance", (object)dateNaissance ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@adresse", string.IsNullOrWhiteSpace(adresse) ? (object)DBNull.Value : adresse);
                    cmd.Parameters.AddWithValue("@telephone", string.IsNullOrWhiteSpace(telephone) ? (object)DBNull.Value : telephone);
                    cmd.Parameters.AddWithValue("@poste", string.IsNullOrWhiteSpace(poste) ? (object)DBNull.Value : poste);
                    cmd.Parameters.AddWithValue("@numerocnss", string.IsNullOrWhiteSpace(cnss) ? (object)DBNull.Value : cnss);
                    cmd.Parameters.AddWithValue("@identification", string.IsNullOrWhiteSpace(identification) ? (object)DBNull.Value : identification);
                    cmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);
                    cmd.Parameters.AddWithValue("@idDirection", (object)idDirection ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@idService", (object)idService ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@idCategorie", idCategorie);
                    cmd.Parameters.AddWithValue("@contrat", contrat);
                    cmd.Parameters.AddWithValue("@typeContrat", (object)typeContrat ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@modePayement", (object)modePayement ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@cadre", (object)cadre ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@dateEntree", dateEntree);
                    cmd.Parameters.AddWithValue("@dateSortie", (object)dateSortie ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@heureContrat", (object)heureContrat ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@jourContrat", (object)jourContrat ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@numeroBancaire", string.IsNullOrWhiteSpace(numeroBancaire) ? (object)DBNull.Value : numeroBancaire);
                    cmd.Parameters.AddWithValue("@banque", string.IsNullOrWhiteSpace(banque) ? (object)DBNull.Value : banque);
                    cmd.Parameters.AddWithValue("@salaireMoyen", (object)salaireMoyen ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@dureeContrat", dureeContrat);

                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: Tous les paramètres ajoutés\n");
                    connect.openConnect();
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: Connexion ouverte\n");
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: Avant ExecuteNonQuery\n");
                    int result = cmd.ExecuteNonQuery();
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: Après ExecuteNonQuery, result={result}\n");
                    connect.closeConnect();

                    if (result > 0)
                    {
                        CustomMessageBox.Show($"Employé ajouté avec succès.\nMatricule: {matricule}", "Succès",
                            CustomMessageBox.MessageType.Success);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        CustomMessageBox.Show("L'enregistrement a échoué. Veuillez réessayer.", "Erreur",
                            CustomMessageBox.MessageType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: EXCEPTION CAPTURÉE: {ex.ToString()}\n\n");
                CustomMessageBox.Show($"Erreur lors de l'enregistrement :\n{ex.Message}\n\nFichier log: {logPath}\n\nDétails:\n{ex.ToString()}", "Erreur", CustomMessageBox.MessageType.Error);
            }
            finally
            {
                System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: FIN buttonValider_Click\n\n");
            }
        }
    }
}
