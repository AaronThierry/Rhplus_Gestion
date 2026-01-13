using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class ModifierEmployeForm : Form
    {
        private Dbconnect connect = new Dbconnect();
        private int idPersonnel;

        public ModifierEmployeForm(int idPersonnel, string nomPrenom, string matricule, string civilite, string sexe,
            DateTime? dateNaissance, string adresse, string telephone, string identification,
            int idEntreprise, int? idDirection, int? idService, int idCategorie,
            string poste, string cnss, string contrat, string typeContrat, string modePayement, string cadre,
            DateTime dateEntree, DateTime? dateSortie, decimal? heureContrat, int? jourContrat,
            string numeroBancaire, string banque, decimal? salaireMoyen, string dureeContrat = "Permanent")
        {
            InitializeComponent();
            this.idPersonnel = idPersonnel;

            InitialiserDonnees();
            ChargerDonnees(nomPrenom, matricule, civilite, sexe, dateNaissance, adresse, telephone, identification,
                idEntreprise, idDirection, idService, idCategorie, poste, cnss, contrat, typeContrat, modePayement,
                cadre, dateEntree, dateSortie, heureContrat, jourContrat, numeroBancaire, banque, salaireMoyen, dureeContrat);
        }

        private void InitialiserDonnees()
        {
            // Charger les entreprises
            EntrepriseClass.ChargerEntreprises(comboBoxEntreprise, null, true);

            // Initialiser les combos avec des valeurs par défaut
            comboBoxCivilite.Items.AddRange(new string[] { "-- Sélectionner --", "M.", "Mme", "Mlle" });
            comboBoxSexe.Items.AddRange(new string[] { "-- Sélectionner --", "Masculin", "Féminin" });
            comboBoxContrat.Items.AddRange(new string[] { "-- Sélectionner --", "CDI", "CDD" });
            comboBoxTypeContrat.Items.AddRange(new string[] { "-- Sélectionner --", "Horaire", "Journalier", "Mensuel" });
            comboBoxModePayement.Items.AddRange(new string[] { "-- Sélectionner --", "Espèces", "Virement", "Chèque" });
            comboBoxCadre.Items.AddRange(new string[] { "-- Sélectionner --", "Cadre", "Non-Cadre" });

            // Initialiser le ComboBox DureeContrat
            comboBoxDureeContrat.Items.AddRange(new string[] { "-- Sélectionner --", "Permanent", "Temporaire", "Occasionnel", "Volontaire National", "Stagiaire" });

            checkBoxDateSortie.CheckedChanged += CheckBoxDateSortie_CheckedChanged;
            comboBoxEntreprise.SelectedIndexChanged += ComboBoxEntreprise_SelectedIndexChanged;
            comboBoxDirection.SelectedIndexChanged += ComboBoxDirection_SelectedIndexChanged;
        }

        private void ChargerDonnees(string nomPrenom, string matricule, string civilite, string sexe,
            DateTime? dateNaissance, string adresse, string telephone, string identification,
            int idEntreprise, int? idDirection, int? idService, int idCategorie,
            string poste, string cnss, string contrat, string typeContrat, string modePayement, string cadre,
            DateTime dateEntree, DateTime? dateSortie, decimal? heureContrat, int? jourContrat,
            string numeroBancaire, string banque, decimal? salaireMoyen, string dureeContrat = "Permanent")
        {
            // Informations personnelles
            textBoxNomPrenom.Text = nomPrenom;
            textBoxMatricule.Text = matricule;
            textBoxMatricule.ReadOnly = true; // Le matricule ne se modifie pas

            if (!string.IsNullOrWhiteSpace(civilite))
                comboBoxCivilite.SelectedItem = civilite;

            if (!string.IsNullOrWhiteSpace(sexe))
                comboBoxSexe.SelectedItem = sexe;

            if (dateNaissance.HasValue)
            {
                datePickerNaissance.Value = dateNaissance.Value;
                datePickerNaissance.Checked = true;
            }
            else
            {
                datePickerNaissance.Checked = false;
            }

            textBoxAdresse.Text = adresse ?? "";
            textBoxTelephone.Text = telephone ?? "";
            textBoxIdentification.Text = identification ?? "";

            // Informations professionnelles
            comboBoxEntreprise.SelectedValue = idEntreprise;

            // Attendre que les combos soient chargés
            Application.DoEvents();

            if (idDirection.HasValue)
                comboBoxDirection.SelectedValue = idDirection.Value;

            if (idService.HasValue)
                comboBoxService.SelectedValue = idService.Value;

            comboBoxCategorie.SelectedValue = idCategorie;
            textBoxPoste.Text = poste ?? "";
            textBoxCnss.Text = cnss ?? "";

            // Informations contractuelles
            if (!string.IsNullOrWhiteSpace(contrat))
                comboBoxContrat.SelectedItem = contrat;

            if (!string.IsNullOrWhiteSpace(typeContrat))
                comboBoxTypeContrat.SelectedItem = typeContrat;

            if (!string.IsNullOrWhiteSpace(modePayement))
                comboBoxModePayement.SelectedItem = modePayement;

            if (!string.IsNullOrWhiteSpace(cadre))
                comboBoxCadre.SelectedItem = cadre;

            // Charger la durée de contrat
            if (!string.IsNullOrWhiteSpace(dureeContrat))
            {
                // Capitaliser la première lettre pour correspondre aux items du ComboBox
                string dureeContratCapitalized = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dureeContrat.ToLower());
                comboBoxDureeContrat.SelectedItem = dureeContratCapitalized;
            }
            else
            {
                comboBoxDureeContrat.SelectedItem = "Permanent";
            }

            datePickerEntree.Value = dateEntree;

            if (dateSortie.HasValue)
            {
                checkBoxDateSortie.Checked = true;
                datePickerSortie.Value = dateSortie.Value;
                datePickerSortie.Enabled = true;
            }
            else
            {
                checkBoxDateSortie.Checked = false;
                datePickerSortie.Enabled = false;
            }

            if (heureContrat.HasValue)
                textBoxHeureContrat.Text = heureContrat.Value.ToString();

            if (jourContrat.HasValue)
                textBoxJourContrat.Text = jourContrat.Value.ToString();

            // Informations financières
            if (salaireMoyen.HasValue)
                textBoxSalaireMoyen.Text = salaireMoyen.Value.ToString();

            textBoxNumeroBancaire.Text = numeroBancaire ?? "";
            textBoxBanque.Text = banque ?? "";
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

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonValider_Click(object sender, EventArgs e)
        {
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

            try
            {
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

                // Mise à jour dans la base de données
                // Récupérer la durée de contrat du ComboBox
                string dureeContrat = comboBoxDureeContrat.SelectedItem?.ToString();
                if (dureeContrat == "-- Sélectionner --" || string.IsNullOrWhiteSpace(dureeContrat))
                {
                    CustomMessageBox.Show("Veuillez sélectionner une durée de contrat.", "Validation",
                        CustomMessageBox.MessageType.Warning);
                    return;
                }
                dureeContrat = dureeContrat.ToLower(); // Convertir en minuscules

                string query = @"UPDATE personnel SET
                    nomPrenom = @nomPrenom,
                    civilite = @civilite,
                    sexe = @sexe,
                    date_naissance = @dateNaissance,
                    adresse = @adresse,
                    telephone = @telephone,
                    poste = @poste,
                    numerocnss = @numerocnss,
                    identification = @identification,
                    id_entreprise = @idEntreprise,
                    id_direction = @idDirection,
                    id_service = @idService,
                    id_categorie = @idCategorie,
                    contrat = @contrat,
                    typeContrat = @typeContrat,
                    modePayement = @modePayement,
                    cadre = @cadre,
                    date_entree = @dateEntree,
                    date_sortie = @dateSortie,
                    heureContrat = @heureContrat,
                    jourContrat = @jourContrat,
                    numeroBancaire = @numeroBancaire,
                    banque = @banque,
                    salairemoyen = @salaireMoyen,
                    dureeContrat = @dureeContrat
                WHERE id_personnel = @idPersonnel";

                using (MySqlCommand cmd = new MySqlCommand(query, connect.getconnection))
                {
                    cmd.Parameters.AddWithValue("@idPersonnel", idPersonnel);
                    cmd.Parameters.AddWithValue("@nomPrenom", nomPrenom);
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

                    connect.openConnect();
                    int result = cmd.ExecuteNonQuery();
                    connect.closeConnect();

                    if (result > 0)
                    {
                        CustomMessageBox.Show("Employé modifié avec succès.", "Succès",
                            CustomMessageBox.MessageType.Success);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        CustomMessageBox.Show("La modification a échoué. Veuillez réessayer.", "Erreur",
                            CustomMessageBox.MessageType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de la modification :\n{ex.Message}", "Erreur",
                    CustomMessageBox.MessageType.Error);
            }
        }
    }
}
