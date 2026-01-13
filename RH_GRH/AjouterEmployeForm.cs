using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class AjouterEmployeForm : Form
    {
        private Dbconnect connect = new Dbconnect();

        public AjouterEmployeForm()
        {
            InitializeComponent();
            InitialiserDonnees();
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

            comboBoxModePayement.Items.AddRange(new string[] { "-- Sélectionner --", "Espèces", "Virement", "Chèque" });
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

                // Génération du matricule
                System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: Avant génération matricule\n");
                string matricule = MatriculeGenerator.GenererMatricule(idEntreprise);
                System.IO.File.AppendAllText(logPath, $"{DateTime.Now}: Matricule généré: {matricule}\n");

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
