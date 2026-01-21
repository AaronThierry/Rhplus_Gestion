using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using QuestPDF.Fluent;

namespace RH_GRH
{
    public partial class SaisiePayeLotForm : Form
    {
        private int idEntreprise;
        private DateTime periodeDebut;
        private DateTime periodeFin;
        private DataTable dtEmployes;
        private string typeContrat; // "Horaire" ou "Journalier"

        public SaisiePayeLotForm(int idEntreprise, DateTime periodeDebut, DateTime periodeFin, string typeContrat = "Horaire")
        {
            InitializeComponent();
            this.idEntreprise = idEntreprise;
            this.periodeDebut = periodeDebut;
            this.periodeFin = periodeFin;
            this.typeContrat = typeContrat;

            ConfigurerDataGridView();
            ChargerEmployes();

            // Gérer l'événement CellEndEdit pour remettre 0 quand la cellule est vide
            dataGridViewEmployes.CellEndEdit += DataGridViewEmployes_CellEndEdit;

            // Mettre à jour le titre selon le type de contrat
            labelTitre.Text = $"Saisie de Paie par Lot - {typeContrat}";
        }

        private void DataGridViewEmployes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Vérifier si la cellule est éditable (pas la colonne heures_travaillees)
            var columnName = dataGridViewEmployes.Columns[e.ColumnIndex].Name;

            if (columnName != "heures_travaillees" && columnName != "matricule" &&
                columnName != "nom_prenom" && columnName != "id_personnel")
            {
                var cell = dataGridViewEmployes.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // Si la cellule est vide ou null, mettre 0
                if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    cell.Value = 0;
                }
                else
                {
                    // Valider que c'est un nombre valide
                    decimal valeur;
                    if (!decimal.TryParse(cell.Value.ToString(), out valeur))
                    {
                        cell.Value = 0;
                    }
                }
            }
        }

        private void ConfigurerDataGridView()
        {
            // Configuration du DataGridView - Style moderne et épuré (comme SelectionEntrepriseForm)
            dataGridViewEmployes.AutoGenerateColumns = false;
            dataGridViewEmployes.AllowUserToAddRows = false;
            dataGridViewEmployes.AllowUserToDeleteRows = false;
            dataGridViewEmployes.AllowUserToResizeRows = false;
            dataGridViewEmployes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEmployes.MultiSelect = false;
            dataGridViewEmployes.RowHeadersVisible = false;
            dataGridViewEmployes.BackgroundColor = Color.White;
            dataGridViewEmployes.BorderStyle = BorderStyle.None;
            dataGridViewEmployes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewEmployes.EnableHeadersVisualStyles = false;
            dataGridViewEmployes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewEmployes.ColumnHeadersHeight = 42;
            dataGridViewEmployes.RowTemplate.Height = 40;

            // Style de l'en-tête - Élégant avec MidnightBlue
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.BackColor = Color.MidnightBlue;
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 9.5F, FontStyle.Bold);
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.Padding = new Padding(12, 0, 12, 0);
            dataGridViewEmployes.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.MidnightBlue;

            // Style des cellules par défaut - Soft et lisible
            dataGridViewEmployes.DefaultCellStyle.BackColor = Color.White;
            dataGridViewEmployes.DefaultCellStyle.ForeColor = Color.FromArgb(50, 50, 50);
            dataGridViewEmployes.DefaultCellStyle.Font = new Font("Montserrat", 9F, FontStyle.Regular);
            dataGridViewEmployes.DefaultCellStyle.Padding = new Padding(12, 8, 12, 8);
            dataGridViewEmployes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(176, 196, 222); // LightSteelBlue
            dataGridViewEmployes.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 25, 112); // MidnightBlue foncé

            // Style des lignes alternées - Très subtil
            dataGridViewEmployes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 252);
            dataGridViewEmployes.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(50, 50, 50);
            dataGridViewEmployes.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(176, 196, 222);
            dataGridViewEmployes.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 25, 112);

            // Couleur de la grille - Très légère
            dataGridViewEmployes.GridColor = Color.FromArgb(235, 237, 242);

            // Colonnes avec style épuré
            var colIdPersonnel = new DataGridViewTextBoxColumn
            {
                Name = "id_personnel",
                HeaderText = "ID",
                DataPropertyName = "id_personnel",
                Visible = false
            };

            var colMatricule = new DataGridViewTextBoxColumn
            {
                Name = "matricule",
                HeaderText = "Matricule",
                DataPropertyName = "matricule",
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Montserrat", 9F, FontStyle.Bold),
                    ForeColor = Color.MidnightBlue
                }
            };

            var colNomPrenom = new DataGridViewTextBoxColumn
            {
                Name = "nom_prenom",
                HeaderText = "Nom & Prénom",
                DataPropertyName = "nom_prenom",
                ReadOnly = true,
                Width = 250,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Font = new Font("Montserrat", 9F, FontStyle.Regular)
                }
            };

            // Colonnes éditables avec couleur jaune soft
            var editableCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 252, 230), // Jaune très soft et élégant
                SelectionBackColor = Color.FromArgb(255, 245, 180),
                SelectionForeColor = Color.FromArgb(25, 25, 112),
                Font = new Font("Montserrat", 9F, FontStyle.Regular),
                Alignment = DataGridViewContentAlignment.MiddleRight
            };

            var colHeuresTravaillees = new DataGridViewTextBoxColumn
            {
                Name = "heures_travaillees",
                HeaderText = "H/J Travaillés",
                DataPropertyName = "heures_travaillees",
                Width = 110,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Montserrat", 9F, FontStyle.Regular),
                    BackColor = Color.FromArgb(245, 247, 250), // Gris très clair pour indiquer non-éditable
                    ForeColor = Color.FromArgb(80, 80, 80)
                }
            };

            // Colonnes différentes selon le type de contrat
            DataGridViewTextBoxColumn colHsNormJour = null;
            DataGridViewTextBoxColumn colHsNormNuit = null;
            DataGridViewTextBoxColumn colHsFerieJour = null;
            DataGridViewTextBoxColumn colHsFerieNuit = null;

            if (typeContrat == "Horaire")
            {
                // Pour les horaires : HS Jour, HS Nuit, HS Férié Jour, HS Férié Nuit
                colHsNormJour = new DataGridViewTextBoxColumn
                {
                    Name = "hs_norm_jour",
                    HeaderText = "HS Jour",
                    DataPropertyName = "hs_norm_jour",
                    Width = 80,
                    DefaultCellStyle = (DataGridViewCellStyle)editableCellStyle.Clone()
                };

                colHsNormNuit = new DataGridViewTextBoxColumn
                {
                    Name = "hs_norm_nuit",
                    HeaderText = "HS Nuit",
                    DataPropertyName = "hs_norm_nuit",
                    Width = 80,
                    DefaultCellStyle = (DataGridViewCellStyle)editableCellStyle.Clone()
                };

                colHsFerieJour = new DataGridViewTextBoxColumn
                {
                    Name = "hs_ferie_jour",
                    HeaderText = "HS Férié J",
                    DataPropertyName = "hs_ferie_jour",
                    Width = 90,
                    DefaultCellStyle = (DataGridViewCellStyle)editableCellStyle.Clone()
                };

                colHsFerieNuit = new DataGridViewTextBoxColumn
                {
                    Name = "hs_ferie_nuit",
                    HeaderText = "HS Férié N",
                    DataPropertyName = "hs_ferie_nuit",
                    Width = 90,
                    DefaultCellStyle = (DataGridViewCellStyle)editableCellStyle.Clone()
                };
            }
            else // Journalier
            {
                // Pour les journaliers : une seule colonne Jours Supp
                colHsNormJour = new DataGridViewTextBoxColumn
                {
                    Name = "hs_norm_jour",
                    HeaderText = "Jours Supp",
                    DataPropertyName = "hs_norm_jour",
                    Width = 100,
                    DefaultCellStyle = (DataGridViewCellStyle)editableCellStyle.Clone()
                };
            }

            var colAbsences = new DataGridViewTextBoxColumn
            {
                Name = "absences",
                HeaderText = "Absences",
                DataPropertyName = "absences",
                Width = 90,
                DefaultCellStyle = (DataGridViewCellStyle)editableCellStyle.Clone()
            };

            var colDette = new DataGridViewTextBoxColumn
            {
                Name = "dette",
                HeaderText = "Dette",
                DataPropertyName = "dette",
                Width = 100,
                DefaultCellStyle = (DataGridViewCellStyle)editableCellStyle.Clone()
            };

            // Ajouter les colonnes selon le type de contrat
            dataGridViewEmployes.Columns.Add(colIdPersonnel);
            dataGridViewEmployes.Columns.Add(colMatricule);
            dataGridViewEmployes.Columns.Add(colNomPrenom);
            dataGridViewEmployes.Columns.Add(colHeuresTravaillees);

            if (typeContrat == "Horaire")
            {
                // Pour les horaires : toutes les colonnes d'heures supplémentaires
                dataGridViewEmployes.Columns.Add(colHsNormJour);
                dataGridViewEmployes.Columns.Add(colHsNormNuit);
                dataGridViewEmployes.Columns.Add(colHsFerieJour);
                dataGridViewEmployes.Columns.Add(colHsFerieNuit);
            }
            else // Journalier
            {
                // Pour les journaliers : une seule colonne Jours Supp
                dataGridViewEmployes.Columns.Add(colHsNormJour);
            }

            dataGridViewEmployes.Columns.Add(colAbsences);
            dataGridViewEmployes.Columns.Add(colDette);
        }

        private void ChargerEmployes()
        {
            try
            {
                dtEmployes = new DataTable();
                dtEmployes.Columns.Add("id_personnel", typeof(int));
                dtEmployes.Columns.Add("matricule", typeof(string));
                dtEmployes.Columns.Add("nom_prenom", typeof(string));
                // Colonnes cachées mais nécessaires pour les calculs
                dtEmployes.Columns.Add("poste", typeof(string));
                dtEmployes.Columns.Add("type_contrat", typeof(string));
                // Colonnes éditables avec valeurs par défaut à 0
                var colHeuresTrav = dtEmployes.Columns.Add("heures_travaillees", typeof(decimal));
                colHeuresTrav.DefaultValue = 0m;
                var colHsNormJour = dtEmployes.Columns.Add("hs_norm_jour", typeof(decimal));
                colHsNormJour.DefaultValue = 0m;
                var colHsNormNuit = dtEmployes.Columns.Add("hs_norm_nuit", typeof(decimal));
                colHsNormNuit.DefaultValue = 0m;
                var colHsFerieJour = dtEmployes.Columns.Add("hs_ferie_jour", typeof(decimal));
                colHsFerieJour.DefaultValue = 0m;
                var colHsFerieNuit = dtEmployes.Columns.Add("hs_ferie_nuit", typeof(decimal));
                colHsFerieNuit.DefaultValue = 0m;
                var colAbsences = dtEmployes.Columns.Add("absences", typeof(decimal));
                colAbsences.DefaultValue = 0m;
                var colDette = dtEmployes.Columns.Add("dette", typeof(decimal));
                colDette.DefaultValue = 0m;

                var connect = new Dbconnect();
                using (var con = connect.getconnection)
                {
                    con.Open();

                    string sql = @"
                        SELECT
                            p.id_personnel,
                            p.matricule,
                            p.nomPrenom as nom_prenom,
                            p.poste,
                            p.typeContrat as type_contrat,
                            CASE
                                WHEN p.typeContrat = 'Horaire' THEN p.heureContrat
                                WHEN p.typeContrat = 'Journalier' THEN p.jourContrat
                                ELSE 0
                            END as heures_travaillees_default
                        FROM personnel p
                        WHERE p.id_entreprise = @idEntreprise
                        AND p.typeContrat = @typeContrat
                        AND (
                            p.date_sortie IS NULL
                            OR p.date_sortie >= @periodeFin
                        )
                        ORDER BY p.nomPrenom";

                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);
                        cmd.Parameters.AddWithValue("@typeContrat", typeContrat);
                        cmd.Parameters.AddWithValue("@periodeFin", periodeFin);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var row = dtEmployes.NewRow();
                                row["id_personnel"] = reader.GetInt32("id_personnel");
                                row["matricule"] = reader.GetString("matricule");
                                row["nom_prenom"] = reader.GetString("nom_prenom");
                                row["poste"] = reader.IsDBNull(reader.GetOrdinal("poste")) ? "" : reader.GetString("poste");
                                row["type_contrat"] = reader.GetString("type_contrat");
                                row["heures_travaillees"] = reader.IsDBNull(reader.GetOrdinal("heures_travaillees_default")) ? 0 : reader.GetDecimal("heures_travaillees_default");
                                row["hs_norm_jour"] = 0;
                                row["hs_norm_nuit"] = 0;
                                row["hs_ferie_jour"] = 0;
                                row["hs_ferie_nuit"] = 0;
                                row["absences"] = 0;
                                row["dette"] = 0;
                                dtEmployes.Rows.Add(row);
                            }
                        }
                    }
                }

                dataGridViewEmployes.DataSource = dtEmployes;
                int count = dtEmployes.Rows.Count;
                if (count == 0)
                {
                    labelNombreEmployes.Text = "👥 Aucun employé trouvé";
                }
                else if (count == 1)
                {
                    labelNombreEmployes.Text = "👥 1 employé sélectionné";
                }
                else
                {
                    labelNombreEmployes.Text = $"👥 {count} employés sélectionnés";
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(
                    $"Erreur lors du chargement des employés :\n{ex.Message}",
                    "Erreur",
                    CustomMessageBox.MessageType.Error,
                    CustomMessageBox.MessageButtons.OK);
            }
        }

        private void buttonGenerer_Click(object sender, EventArgs e)
        {
            try
            {
                // Valider les données saisies
                if (!ValiderDonnees())
                {
                    return;
                }

                // Demander le dossier de destination pour les bulletins
                using (var folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "Choisir le dossier où enregistrer les bulletins";
                    folderDialog.ShowNewFolderButton = true;

                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Afficher la barre de progression avec animation
                        panelProgression.Visible = true;
                        guna2ProgressBar1.Value = 0;
                        labelProgression.Text = "🔄 Préparation de la génération...";
                        Application.DoEvents();

                        // Générer les bulletins individuels
                        GenererPDFConsolide(folderDialog.SelectedPath);

                        // Masquer la barre de progression
                        panelProgression.Visible = false;

                        CustomMessageBox.Show(
                            $"✅ Génération terminée avec succès !\n\n" +
                            $"📊 {dtEmployes.Rows.Count} bulletin(s) généré(s)\n" +
                            $"📁 Dossier : {folderDialog.SelectedPath}\n\n" +
                            $"Le dossier va s'ouvrir automatiquement.",
                            "Succès",
                            CustomMessageBox.MessageType.Success,
                            CustomMessageBox.MessageButtons.OK);

                        // Ouvrir automatiquement le dossier contenant les bulletins
                        System.Diagnostics.Process.Start("explorer.exe", folderDialog.SelectedPath);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                panelProgression.Visible = false;
                CustomMessageBox.Show(
                    $"❌ Une erreur s'est produite lors de la génération :\n\n{ex.Message}",
                    "Erreur",
                    CustomMessageBox.MessageType.Error,
                    CustomMessageBox.MessageButtons.OK);
            }
        }

        private bool ValiderDonnees()
        {
            foreach (DataRow row in dtEmployes.Rows)
            {
                string nomEmploye = row["nom_prenom"].ToString();

                // Valider que les valeurs numériques sont correctes
                if (Convert.ToDecimal(row["heures_travaillees"]) < 0)
                {
                    CustomMessageBox.Show(
                        $"⚠️ Validation échouée\n\n" +
                        $"Employé : {nomEmploye}\n" +
                        $"Problème : Les heures/jours travaillés ne peuvent pas être négatifs.",
                        "Validation des données",
                        CustomMessageBox.MessageType.Warning,
                        CustomMessageBox.MessageButtons.OK);
                    return false;
                }

                if (Convert.ToDecimal(row["hs_norm_jour"]) < 0)
                {
                    CustomMessageBox.Show(
                        $"⚠️ Validation échouée\n\n" +
                        $"Employé : {nomEmploye}\n" +
                        $"Problème : Les heures/jours supplémentaires ne peuvent pas être négatifs.",
                        "Validation des données",
                        CustomMessageBox.MessageType.Warning,
                        CustomMessageBox.MessageButtons.OK);
                    return false;
                }

                if (row.Table.Columns.Contains("hs_norm_nuit") && Convert.ToDecimal(row["hs_norm_nuit"]) < 0)
                {
                    CustomMessageBox.Show(
                        $"⚠️ Validation échouée\n\n" +
                        $"Employé : {nomEmploye}\n" +
                        $"Problème : Les heures supplémentaires de nuit ne peuvent pas être négatives.",
                        "Validation des données",
                        CustomMessageBox.MessageType.Warning,
                        CustomMessageBox.MessageButtons.OK);
                    return false;
                }

                if (row.Table.Columns.Contains("hs_ferie_jour") && Convert.ToDecimal(row["hs_ferie_jour"]) < 0)
                {
                    CustomMessageBox.Show(
                        $"⚠️ Validation échouée\n\n" +
                        $"Employé : {nomEmploye}\n" +
                        $"Problème : Les heures supplémentaires férié jour ne peuvent pas être négatives.",
                        "Validation des données",
                        CustomMessageBox.MessageType.Warning,
                        CustomMessageBox.MessageButtons.OK);
                    return false;
                }

                if (row.Table.Columns.Contains("hs_ferie_nuit") && Convert.ToDecimal(row["hs_ferie_nuit"]) < 0)
                {
                    CustomMessageBox.Show(
                        $"⚠️ Validation échouée\n\n" +
                        $"Employé : {nomEmploye}\n" +
                        $"Problème : Les heures supplémentaires férié nuit ne peuvent pas être négatives.",
                        "Validation des données",
                        CustomMessageBox.MessageType.Warning,
                        CustomMessageBox.MessageButtons.OK);
                    return false;
                }

                if (Convert.ToDecimal(row["absences"]) < 0)
                {
                    CustomMessageBox.Show(
                        $"⚠️ Validation échouée\n\n" +
                        $"Employé : {nomEmploye}\n" +
                        $"Problème : Les absences ne peuvent pas être négatives.",
                        "Validation des données",
                        CustomMessageBox.MessageType.Warning,
                        CustomMessageBox.MessageButtons.OK);
                    return false;
                }
            }

            return true;
        }

        private void GenererPDFConsolide(string dossierDestination)
        {
            int index = 0;
            int totalEmployes = dtEmployes.Rows.Count;
            int successCount = 0;
            int errorCount = 0;

            // Créer le dossier de destination s'il n'existe pas
            if (!System.IO.Directory.Exists(dossierDestination))
            {
                System.IO.Directory.CreateDirectory(dossierDestination);
            }

            foreach (DataRow row in dtEmployes.Rows)
            {
                index++;
                int pourcentage = (index * 100) / totalEmployes;
                guna2ProgressBar1.Value = pourcentage;

                // Texte de progression élégant avec emoji et informations claires
                string nomEmploye = row["nom_prenom"].ToString();
                if (nomEmploye.Length > 30) nomEmploye = nomEmploye.Substring(0, 30) + "...";

                labelProgression.Text = $"📄 Génération du bulletin {index}/{totalEmployes} • {nomEmploye} • {pourcentage}%";
                Application.DoEvents();

                try
                {
                    // Générer un bulletin individuel pour cet employé
                    GenererBulletinIndividuel(row, dossierDestination);
                    successCount++;
                }
                catch (Exception ex)
                {
                    errorCount++;
                    CustomMessageBox.Show(
                        $"❌ Erreur lors de la génération\n\n" +
                        $"Employé : {row["nom_prenom"]}\n" +
                        $"Erreur : {ex.Message}",
                        "Erreur de génération",
                        CustomMessageBox.MessageType.Warning,
                        CustomMessageBox.MessageButtons.OK);
                }
            }

            // Message de fin avec résumé
            if (errorCount == 0)
            {
                labelProgression.Text = $"✅ Génération terminée avec succès ! {successCount} bulletin(s) généré(s)";
            }
            else
            {
                labelProgression.Text = $"⚠️ Génération terminée : {successCount} réussi(s), {errorCount} erreur(s)";
            }
            Application.DoEvents();
            System.Threading.Thread.Sleep(1500); // Afficher le message final pendant 1.5 secondes
        }

        private void GenererBulletinIndividuel(DataRow row, string dossierDestination)
        {
            // Récupérer les données saisies
            int idEmploye = Convert.ToInt32(row["id_personnel"]);
            string matricule = row["matricule"].ToString();
            decimal heuresTravaillees = Convert.ToDecimal(row["heures_travaillees"]);
            string typeContratRow = row["type_contrat"].ToString();

            // Récupérer les valeurs selon le type de contrat
            decimal hsNormJour = 0m;
            decimal hsNormNuit = 0m;
            decimal hsFerieJour = 0m;
            decimal hsFerieNuit = 0m;

            if (typeContratRow == "Horaire")
            {
                // Pour les horaires : 4 colonnes
                hsNormJour = row["hs_norm_jour"] != DBNull.Value ? Convert.ToDecimal(row["hs_norm_jour"]) : 0m;
                hsNormNuit = row["hs_norm_nuit"] != DBNull.Value ? Convert.ToDecimal(row["hs_norm_nuit"]) : 0m;
                hsFerieJour = row["hs_ferie_jour"] != DBNull.Value ? Convert.ToDecimal(row["hs_ferie_jour"]) : 0m;
                hsFerieNuit = row["hs_ferie_nuit"] != DBNull.Value ? Convert.ToDecimal(row["hs_ferie_nuit"]) : 0m;
            }
            else // Journalier
            {
                // Pour les journaliers : 1 seule colonne "Jours Supp" dans hs_norm_jour
                hsNormJour = row["hs_norm_jour"] != DBNull.Value ? Convert.ToDecimal(row["hs_norm_jour"]) : 0m;
                // Les autres restent à 0
            }

            decimal absences = row["absences"] != DBNull.Value ? Convert.ToDecimal(row["absences"]) : 0m;
            decimal dette = row["dette"] != DBNull.Value ? Convert.ToDecimal(row["dette"]) : 0m;
            string typeContrat = typeContratRow;

            // Debug: Afficher la dette
            System.Diagnostics.Debug.WriteLine($"Employé: {matricule}, Dette: {dette}");

            // Récupérer les détails de l'employé
            var employe = EmployeService.GetEmployeDetails(idEmploye);
            if (employe == null) return;

            // Récupérer le nombre de charges depuis ChargeClass (comme dans GestionSalaireHoraireForm)
            int nombreCharges = ChargeClass.CountTotalCharges(idEmploye);

            // Créer le snapshot en utilisant la même logique que GestionSalaireHoraireForm
            var snapshot = new PayrollSnapshot
            {
                IdEmploye = idEmploye,
                IdEntreprise = idEntreprise,
                NomPrenom = employe.Nom,
                Matricule = employe.Matricule,
                Civilite = employe.Civilite,
                Poste = employe.Poste,
                Sexe = employe.Sexe,
                DateNaissance = employe.DateNaissance,
                DateEntree = employe.DateEntree,
                DateSortie = employe.DateSortie,
                Contrat = employe.Contrat ?? "",
                PeriodeSalaire = $"{periodeDebut:dd/MM/yyyy} - {periodeFin:dd/MM/yyyy}",
                NumeroEmploye = employe.Matricule, // Utiliser matricule comme numéro employé
                AdresseEmploye = "", // Peut être récupéré de la BD si nécessaire
                DureeContrat = employe.DureeContrat ?? "", // Durée du contrat (Permanent, Temporaire, etc.)
                NombreCharges = nombreCharges,

                // Info entreprise
                Sigle = employe.Sigle,
                NomEntreprise = employe.NomEntreprise,
                TelephoneEntreprise = employe.TelephoneEntreprise,
                EmailEntreprise = employe.EmailEntreprise,
                AdressePhysiqueEntreprise = employe.AdressePhysiqueEntreprise,
                AdressePostaleEntreprise = employe.AdressePostaleEntreprise,
                ResponsableEntreprise = employe.ResponsableEntreprise,

                // Catégorie/Service/Direction
                Categorie = employe.Categorie,
                Service = employe.Service,
                Direction = employe.Direction,
                NumeroCnssEmploye = employe.NumeroCnssEmploye
            };

            // Calculer le salaire en fonction du type de contrat
            if (typeContrat == "Horaire")
            {
                CalculerSalaireHoraire(snapshot, employe, heuresTravaillees, hsNormJour, hsNormNuit, hsFerieJour, hsFerieNuit, absences, dette);
            }
            else if (typeContrat == "Journalier")
            {
                CalculerSalaireJournalier(snapshot, employe, heuresTravaillees, hsNormJour, hsNormNuit, hsFerieJour, hsFerieNuit, absences, dette);
            }

            // === COPIE EXACTE DU CODE DU BOUTON "VALIDER ET ENREGISTRER IMPRIMER" ===

            // Récupérer les indemnités de l'employé
            var listeIndemnites = IndemniteClass.GetIndemnitesByEmploye(snapshot.IdEmploye);

            // Initialiser les variables avec des valeurs par défaut
            string Numero_indemnite_1 = string.Empty;
            string Nom_Indemnite_1 = string.Empty;
            string Montant_Indemnite_1 = string.Empty;
            string Taux_Indemnite_1 = string.Empty;

            string Numero_indemnite_2 = string.Empty;
            string Nom_Indemnite_2 = string.Empty;
            string Montant_Indemnite_2 = string.Empty;
            string Taux_Indemnite_2 = string.Empty;

            string Numero_indemnite_3 = string.Empty;
            string Nom_Indemnite_3 = string.Empty;
            string Montant_Indemnite_3 = string.Empty;
            string Taux_Indemnite_3 = string.Empty;

            string Numero_indemnite_4 = string.Empty;
            string Nom_Indemnite_4 = string.Empty;
            string Montant_Indemnite_4 = string.Empty;
            string Taux_Indemnite_4 = string.Empty;

            string Numero_indemnite_5 = string.Empty;
            string Nom_Indemnite_5 = string.Empty;
            string Montant_Indemnite_5 = string.Empty;
            string Taux_Indemnite_5 = string.Empty;

            // Récupérer les indemnités
            if (listeIndemnites.Count > 0)
            {
                var indemnite1 = listeIndemnites[0];
                Numero_indemnite_1 = "04";
                Nom_Indemnite_1 = indemnite1.NomIndemnite;
                Montant_Indemnite_1 = indemnite1.MontantIndemnite;
                Taux_Indemnite_1 = indemnite1.TauxIndem;

                if (listeIndemnites.Count > 1)
                {
                    var indemnite2 = listeIndemnites[1];
                    Numero_indemnite_2 = "05";
                    Nom_Indemnite_2 = indemnite2.NomIndemnite;
                    Montant_Indemnite_2 = indemnite2.MontantIndemnite;
                    Taux_Indemnite_2 = indemnite2.TauxIndem;
                }

                if (listeIndemnites.Count > 2)
                {
                    var indemnite3 = listeIndemnites[2];
                    Numero_indemnite_3 = "06";
                    Nom_Indemnite_3 = indemnite3.NomIndemnite;
                    Montant_Indemnite_3 = indemnite3.MontantIndemnite;
                    Taux_Indemnite_3 = indemnite3.TauxIndem;
                }

                if (listeIndemnites.Count > 3)
                {
                    var indemnite4 = listeIndemnites[3];
                    Numero_indemnite_4 = "07";
                    Nom_Indemnite_4 = indemnite4.NomIndemnite;
                    Montant_Indemnite_4 = indemnite4.MontantIndemnite;
                    Taux_Indemnite_4 = indemnite4.TauxIndem;
                }

                if (listeIndemnites.Count > 4)
                {
                    var indemnite5 = listeIndemnites[4];
                    Numero_indemnite_5 = "05";
                    Nom_Indemnite_5 = indemnite5.NomIndemnite;
                    Montant_Indemnite_5 = indemnite5.MontantIndemnite;
                    Taux_Indemnite_5 = indemnite5.TauxIndem;
                }
            }

            // Récupérer le logo de l'entreprise
            byte[] logo = EntrepriseClass.GetLogoEntreprise(snapshot.IdEntreprise);

            // Créer le modèle de bulletin EXACTEMENT comme dans buttonparcourir_Click
            var model = new BulletinModel
            {
                NomEmploye = snapshot.NomPrenom,
                Civilite = snapshot.Civilite,
                Matricule = snapshot.Matricule,
                Poste = snapshot.Poste,
                NumeroEmploye = snapshot.NumeroEmploye,
                Mois = "Août 2025",
                HeuresSup = 15000,
                CNSS = (decimal)snapshot.CNSS_Employe,
                AdresseEmploye = snapshot.AdresseEmploye,
                Periode = snapshot.PeriodeSalaire,
                LogoEntreprise = logo,
                DateNaissance = snapshot.DateNaissance,
                DateDebut = snapshot.DateEntree,
                DateFin = snapshot.DateSortie,
                Contrat = snapshot.Contrat,
                Categorie = snapshot.Categorie,
                Service = snapshot.Service,
                Direction = snapshot.Direction,
                NumeroCNSSEmploye = snapshot.NumeroCnssEmploye,
                Sexe = snapshot.Sexe,
                Charges = snapshot.NombreCharges,
                DureeContrat = snapshot.DureeContrat,
                Anciennete = snapshot.AncienneteStr,
                NbJourHeure = snapshot.HeureContrat,
                Sigle = snapshot.Sigle,
                NomEntreprise = snapshot.NomEntreprise,
                AdressePhysiqueEntreprise = snapshot.AdressePhysiqueEntreprise,
                AdressePostaleEntreprise = snapshot.AdressePostaleEntreprise,
                TelephoneEntreprise = snapshot.TelephoneEntreprise,
                EmailEntreprise = snapshot.EmailEntreprise,
                ResponsableEntreprise = snapshot.ResponsableEntreprise,
                Numero_indemnite_1 = Numero_indemnite_1,
                Nom_Indemnite_1 = Nom_Indemnite_1,
                Montant_Indemnite_1 = Montant_Indemnite_1,
                Taux_Indemnite_1 = Taux_Indemnite_1,
                Numero_indemnite_2 = Numero_indemnite_2,
                Nom_Indemnite_2 = Nom_Indemnite_2,
                Montant_Indemnite_2 = Montant_Indemnite_2,
                Taux_Indemnite_2 = Taux_Indemnite_2,
                Numero_indemnite_3 = Numero_indemnite_3,
                Nom_Indemnite_3 = Nom_Indemnite_3,
                Montant_Indemnite_3 = Montant_Indemnite_3,
                Taux_Indemnite_3 = Taux_Indemnite_3,
                Numero_indemnite_4 = Numero_indemnite_4,
                Nom_Indemnite_4 = Nom_Indemnite_4,
                Montant_Indemnite_4 = Montant_Indemnite_4,
                Taux_Indemnite_4 = Taux_Indemnite_4,
                Numero_indemnite_5 = Numero_indemnite_5,
                Nom_Indemnite_5 = Nom_Indemnite_5,
                Montant_Indemnite_5 = Montant_Indemnite_5,
                Taux_Indemnite_5 = Taux_Indemnite_5,
                //SALAIRE DE BASE
                baseUnitaire = (double)snapshot.BaseUnitaire,
                SalaireDeBase = (double)snapshot.SalaireBase,
                TauxSalaireDeBase = (double)snapshot.TauxSalaireDeBase,
                //HEURES SUPPLEMENTAIRES
                PrimeHeureSupp = (double)snapshot.PrimeHeuressupp,
                TauxHeureSupp = (double)snapshot.TauxHeureSupp,
                //PRIME ANCIENNETE
                PrimeAnciennete = (decimal)snapshot.PrimeAnciennete,
                //SALAIRE BRUT
                SalaireBrut = (double)snapshot.SalaireBrut,
                //BASE IUTS
                BaseIUTS = (double)snapshot.BaseIUTS,
                //IUTS
                Iuts = (double)snapshot.IUTS_Final,
                //TPA ET TAUX TPA
                Tpa = (double)snapshot.TPA,
                TauxTpa = (double)snapshot.TauxTPA,
                //CNSS EMPLOYE ET EMPLOYEUR
                CnssEmploye = (double)snapshot.CNSS_Employe,
                CnssEmployeur = (double)snapshot.PensionEmployeur,
                //RISQUE PROFESSIONNEL EMPLOYEUR
                RisqueProfessionnel = (double)snapshot.RisqueProEmployeur,
                //PRESTATION FAMILIALE EMPLOYEUR
                PrestationFamiliale = (double)snapshot.PFEmployeur,
                //AVANTAGES EN NATURE
                AvantageNature = (double)snapshot.IndemNat,
                //SALAIRE NET A PAYER
                SalaireNet = snapshot.SalaireNet,
                EffortDePaix = snapshot.EffortPaix,
                SalaireNetaPayer = snapshot.SalaireNetaPayer,
                ValeurDette = dette,
                SalaireNetaPayerFinal = snapshot.SalaireNetaPayer
            };

            // Debug: Vérifier les valeurs du modèle
            System.Diagnostics.Debug.WriteLine($"=== Modèle BulletinModel créé ===");
            System.Diagnostics.Debug.WriteLine($"SalaireNet dans model: {model.SalaireNet}");
            System.Diagnostics.Debug.WriteLine($"EffortDePaix dans model: {model.EffortDePaix}");
            System.Diagnostics.Debug.WriteLine($"SalaireNetaPayer dans model: {model.SalaireNetaPayer}");

            // Nettoyer la période pour le nom de fichier
            string periodeSafe = model.Periode
                .Replace("/", "-")
                .Replace(" ", "_")
                .Replace(":", "-");

            // Format: Bulletin_Periode_NomEntreprise_NomEmploye.pdf
            string nomEntrepriseSafe = model.NomEntreprise?.Replace(" ", "_") ?? "Entreprise";
            string nomEmployeSafe = model.NomEmploye?.Replace(" ", "_") ?? "Employe";
            string nomFichier = $"Bulletin_{periodeSafe}_{nomEntrepriseSafe}_{nomEmployeSafe}.pdf";
            string cheminComplet = System.IO.Path.Combine(dossierDestination, nomFichier);

            // Debug: Afficher le chemin complet
            System.Diagnostics.Debug.WriteLine($"Génération du fichier : {cheminComplet}");

            // Générer le PDF avec BulletinDocument (EXACTEMENT comme buttonparcourir_Click)
            var document = new BulletinDocument(model);
            document.GeneratePdf(cheminComplet);

            // Vérifier que le fichier a bien été créé
            if (System.IO.File.Exists(cheminComplet))
            {
                System.Diagnostics.Debug.WriteLine($"✓ Fichier créé : {cheminComplet}");
            }
            else
            {
                throw new Exception($"Le fichier n'a pas été créé : {cheminComplet}");
            }
        }

        private void CalculerSalaireHoraire(PayrollSnapshot snapshot, Employe employe,
            decimal heuresTravaillees, decimal hsNormJour, decimal hsNormNuit, decimal hsFerieJour, decimal hsFerieNuit, decimal absences, decimal dette)
        {
            // ============================================================================
            // UTILISATION DES MÉTHODES EXACTES DU SYSTÈME INDIVIDUEL (GestionSalaireHoraireForm)
            // ============================================================================

            // 1) Récupérer le salaire catégoriel depuis employe.Montant (comme ligne 1126)
            decimal salaireCategoriel = (decimal)(employe.Montant ?? 0.0);
            decimal unitesTotales = employe.HeureContrat; // heures contractuelles (ligne 1127)
            decimal unitesAbsences = absences;
            int nbreHC = employe.HeureContrat;

            if (salaireCategoriel <= 0m || unitesTotales <= 0m)
            {
                // Valeurs par défaut si données invalides
                snapshot.SalaireBase = 0;
                snapshot.SalaireBrut = 0;
                snapshot.SalaireNet = 0;
                snapshot.SalaireNetaPayer = 0;
                return;
            }

            // 2) Calcul du salaire de base - MÉTHODE EXACTE (lignes 1156-1162)
            decimal baseUnitaire, unitesPayees;
            decimal salaireBase = GestionSalaireHoraireForm.CalculerSalaireBase(
                salaireCategoriel,
                unitesTotales,
                unitesAbsences,
                out baseUnitaire,
                out unitesPayees
            );

            snapshot.BaseUnitaire = baseUnitaire;
            snapshot.TauxSalaireDeBase = unitesPayees; // Nombre d'unités payées (heures - absences)
            snapshot.HeureContrat = employe.HeureContrat;
            snapshot.SalaireBase = salaireBase;

            // 3) Calcul heures supplémentaires - MÉTHODE EXACTE avec paliers (lignes 1171-1177)
            // Utilise les 4 types de HS avec leurs taux respectifs:
            // - HS normales jour: 1.15 (1-8h) puis 1.35 (>8h)
            // - HS normales nuit: 1.50
            // - HS férié jour: 1.60
            // - HS férié nuit: 2.20
            decimal primeHS = GestionSalaireHoraireForm.CalculerHeuresSupp(
                nbreHC,
                salaireCategoriel,
                (int)hsNormJour,    // hsNormJour avec paliers 1.15/1.35
                (int)hsNormNuit,    // hsNormNuit à 1.50
                (int)hsFerieJour,   // hsFerieJour à 1.60
                (int)hsFerieNuit    // hsFerieNuit à 2.20
            );
            decimal totalHS = hsNormJour + hsNormNuit + hsFerieJour + hsFerieNuit;
            snapshot.HeuresSupp = totalHS;
            snapshot.PrimeHeuressupp = primeHS;
            snapshot.TauxHeureSupp = totalHS; // Le nombre total d'heures supplémentaires

            // 4) Prime d'ancienneté - MÉTHODE EXACTE (ligne 1185)
            string ancStr;
            decimal primeAnciennete = GestionSalaireHoraireForm.CalculerAncienneteEtPrime(employe.Id, out ancStr);
            snapshot.AncienneteStr = ancStr;
            snapshot.PrimeAnciennete = primeAnciennete;

            // 5) Indemnités - MÉTHODE EXACTE avec séparation numéraire/nature (lignes 1195-1202)
            var sums = GestionSalaireHoraireForm.GetSommeIndemnitesParIds(employe.Id);
            decimal indemniteNumeraire = (decimal)sums["somme_numeraire"];
            decimal indemniteNature = (decimal)sums["somme_nature"];
            snapshot.IndemNum = indemniteNumeraire;
            snapshot.IndemNat = indemniteNature;

            // 6) Salaire brut - MÉTHODE EXACTE (lignes 1196-1202)
            decimal salaireBrut = GestionSalaireHoraireForm.CalculerSalaireBrut(
                salaireBase,
                primeHS,
                indemniteNumeraire,
                indemniteNature,
                primeAnciennete
            );
            snapshot.SalaireBrut = salaireBrut;

            // 7) CNSS - MÉTHODE EXACTE (lignes 1235-1240)
            string dureeContrat = employe.DureeContrat ?? string.Empty;
            decimal tauxTpa = employe.Tpa ?? 0m;

            decimal cnssEmploye = CNSSCalculator.CalculerCNSSEmploye(salaireBrut, dureeContrat);
            decimal pensionEmployeur = CNSSCalculator.CalculerPensionEmployeur(salaireBrut);
            decimal risqueProEmployeur = CNSSCalculator.CalculerRisqueProEmployeur(salaireBrut);
            decimal pfEmployeur = CNSSCalculator.CalculerPFEmployeur(salaireBrut);
            decimal cnssEmployeur = pensionEmployeur + risqueProEmployeur + pfEmployeur;
            decimal tpa = CNSSCalculator.CalculerTpa(salaireBrut, tauxTpa);

            snapshot.CNSS_Employe = cnssEmploye;
            snapshot.CNSS_Employeur_Total = cnssEmployeur;
            snapshot.PensionEmployeur = pensionEmployeur;
            snapshot.RisqueProEmployeur = risqueProEmployeur;
            snapshot.PFEmployeur = pfEmployeur;
            snapshot.TPA = tpa;
            snapshot.TauxTPA = tauxTpa;

            // 8) Salaire brut social - MÉTHODE EXACTE (ligne 1251)
            decimal salaireBrutSocial = salaireBrut - cnssEmploye;
            snapshot.SalaireBrutSocial = salaireBrutSocial;

            // 9) Déductibilités pour IUTS - MÉTHODE EXACTE (lignes 1262-1280)
            var noms = new[] {
                "transport nature",
                "logement nature",
                "transport numeraire",
                "logement numeraire",
                "fonction"
            };
            var indemMap = GestionSalaireHoraireForm.GetIndemnitesParNoms(employe.Id, noms);

            decimal montantTransportNature = indemMap.ContainsKey("transport nature") ? indemMap["transport nature"] : 0;
            decimal montantLogementNature = indemMap.ContainsKey("logement nature") ? indemMap["logement nature"] : 0;
            decimal montantTransportNumeraire = indemMap.ContainsKey("transport numeraire") ? indemMap["transport numeraire"] : 0;
            decimal montantLogementNumeraire = indemMap.ContainsKey("logement numeraire") ? indemMap["logement numeraire"] : 0;
            decimal montantFonction = indemMap.ContainsKey("fonction") ? indemMap["fonction"] : 0;

            decimal deductibiliteIndem = GestionSalaireHoraireForm.DeductibilitesIndemnites.ComputeDeductibiliteTotale(
                salaireBrutSocial,
                montantLogementNumeraire,
                montantLogementNature,
                montantTransportNumeraire,
                montantTransportNature,
                montantFonction
            );

            // 10) IUTS - MÉTHODE EXACTE (lignes 1286-1292)
            var r = IUTSCalculator.CalculerIUTS(
                salaireBrut,
                cnssEmploye,
                employe.Cadre,
                deductibiliteIndem,
                salaireCategoriel,
                primeAnciennete,
                floorCentaines: true
            );

            decimal baseIutsArr = r.BaseIUTSArrondieCent;
            int nombreCharges = ChargeClass.CountTotalCharges(employe.Id);
            decimal iutsBrut;
            decimal iutsFinal = GestionSalaireHoraireForm.IUTS.Calculer(baseIutsArr, nombreCharges, out iutsBrut);

            snapshot.IUTS_Brut = iutsBrut;
            snapshot.IUTS_Final = iutsFinal;
            snapshot.BaseIUTS = baseIutsArr;
            snapshot.BaseIUTS_Arrondie = baseIutsArr;
            snapshot.NombreCharges = nombreCharges;

            // 11) Salaire net - MÉTHODE EXACTE
            snapshot.SalaireNet = salaireBrut - cnssEmploye - iutsFinal;
            snapshot.EffortPaix = snapshot.SalaireNet * 0.01m;
            snapshot.SalaireNetaPayer = snapshot.SalaireNet - snapshot.EffortPaix - dette;
        }

        private void CalculerSalaireJournalier(PayrollSnapshot snapshot, Employe employe,
            decimal joursTravailles, decimal jsNormJour, decimal jsNormNuit, decimal jsFerieJour, decimal jsFerieNuit, decimal absences, decimal dette)
        {
            // ============================================================================
            // UTILISATION DES MÉTHODES EXACTES DU SYSTÈME INDIVIDUEL (GestionSalaireHoraireForm)
            // Les journaliers utilisent les MÊMES méthodes que les horaires
            // ============================================================================

            // Debug
            System.Diagnostics.Debug.WriteLine($"=== CalculerSalaireJournalier pour {employe.Nom} ===");
            System.Diagnostics.Debug.WriteLine($"Dette passée: {dette}");
            System.Diagnostics.Debug.WriteLine($"JoursTravailles: {joursTravailles}, jsNormJour: {jsNormJour}, Absences: {absences}");

            // 1) Récupérer le salaire catégoriel depuis employe.Montant
            decimal salaireCategoriel = (decimal)(employe.Montant ?? 0.0);
            decimal unitesTotales = employe.JourContrat; // jours contractuels
            decimal unitesAbsences = absences;
            int nbreHC = employe.JourContrat;

            System.Diagnostics.Debug.WriteLine($"SalaireCategoriel: {salaireCategoriel}, UnitesTotales: {unitesTotales}, JourContrat: {employe.JourContrat}");

            if (salaireCategoriel <= 0m || unitesTotales <= 0m)
            {
                System.Diagnostics.Debug.WriteLine("ERREUR: Salaire catégoriel ou unités totales = 0, retour avec valeurs à 0");
                snapshot.SalaireBase = 0;
                snapshot.SalaireBrut = 0;
                snapshot.SalaireNet = 0;
                snapshot.SalaireNetaPayer = 0;
                return;
            }

            // 2) Calcul du salaire de base - MÉTHODE EXACTE
            decimal baseUnitaire, unitesPayees;
            decimal salaireBase = GestionSalaireHoraireForm.CalculerSalaireBase(
                salaireCategoriel,
                unitesTotales,
                unitesAbsences,
                out baseUnitaire,
                out unitesPayees
            );

            snapshot.BaseUnitaire = baseUnitaire;
            snapshot.TauxSalaireDeBase = unitesPayees; // Nombre d'unités payées (jours - absences)
            snapshot.HeureContrat = employe.JourContrat;
            snapshot.SalaireBase = salaireBase;

            // 3) Jours supplémentaires - MÉTHODE EXACTE avec tous les types
            // Utilise les 4 types de jours supplémentaires avec leurs taux respectifs:
            // - Jours normaux jour: 1.15 (1-8j) puis 1.35 (>8j)
            // - Jours normaux nuit: 1.50
            // - Jours férié jour: 1.60
            // - Jours férié nuit: 2.20
            decimal primeHS = GestionSalaireHoraireForm.CalculerHeuresSupp(
                nbreHC,
                salaireCategoriel,
                (int)jsNormJour,    // jsNormJour avec paliers 1.15/1.35
                (int)jsNormNuit,    // jsNormNuit à 1.50
                (int)jsFerieJour,   // jsFerieJour à 1.60
                (int)jsFerieNuit    // jsFerieNuit à 2.20
            );
            decimal totalJS = jsNormJour + jsNormNuit + jsFerieJour + jsFerieNuit;
            snapshot.HeuresSupp = totalJS;
            snapshot.PrimeHeuressupp = primeHS;
            snapshot.TauxHeureSupp = totalJS; // Le nombre total de jours supplémentaires

            // 4) Prime d'ancienneté - MÉTHODE EXACTE
            string ancStr;
            decimal primeAnciennete = GestionSalaireHoraireForm.CalculerAncienneteEtPrime(employe.Id, out ancStr);
            snapshot.AncienneteStr = ancStr;
            snapshot.PrimeAnciennete = primeAnciennete;

            // 5) Indemnités - MÉTHODE EXACTE
            var sums = GestionSalaireHoraireForm.GetSommeIndemnitesParIds(employe.Id);
            decimal indemniteNumeraire = (decimal)sums["somme_numeraire"];
            decimal indemniteNature = (decimal)sums["somme_nature"];
            snapshot.IndemNum = indemniteNumeraire;
            snapshot.IndemNat = indemniteNature;

            // 6) Salaire brut - MÉTHODE EXACTE
            decimal salaireBrut = GestionSalaireHoraireForm.CalculerSalaireBrut(
                salaireBase,
                primeHS,
                indemniteNumeraire,
                indemniteNature,
                primeAnciennete
            );
            snapshot.SalaireBrut = salaireBrut;

            // 7) CNSS - MÉTHODE EXACTE
            string dureeContrat = employe.DureeContrat ?? string.Empty;
            decimal tauxTpa = employe.Tpa ?? 0m;

            decimal cnssEmploye = CNSSCalculator.CalculerCNSSEmploye(salaireBrut, dureeContrat);
            decimal pensionEmployeur = CNSSCalculator.CalculerPensionEmployeur(salaireBrut);
            decimal risqueProEmployeur = CNSSCalculator.CalculerRisqueProEmployeur(salaireBrut);
            decimal pfEmployeur = CNSSCalculator.CalculerPFEmployeur(salaireBrut);
            decimal cnssEmployeur = pensionEmployeur + risqueProEmployeur + pfEmployeur;
            decimal tpa = CNSSCalculator.CalculerTpa(salaireBrut, tauxTpa);

            snapshot.CNSS_Employe = cnssEmploye;
            snapshot.CNSS_Employeur_Total = cnssEmployeur;
            snapshot.PensionEmployeur = pensionEmployeur;
            snapshot.RisqueProEmployeur = risqueProEmployeur;
            snapshot.PFEmployeur = pfEmployeur;
            snapshot.TPA = tpa;
            snapshot.TauxTPA = tauxTpa;

            // 8) Salaire brut social - MÉTHODE EXACTE
            decimal salaireBrutSocial = salaireBrut - cnssEmploye;
            snapshot.SalaireBrutSocial = salaireBrutSocial;

            // 9) Déductibilités pour IUTS - MÉTHODE EXACTE
            var noms = new[] {
                "transport nature",
                "logement nature",
                "transport numeraire",
                "logement numeraire",
                "fonction"
            };
            var indemMap = GestionSalaireHoraireForm.GetIndemnitesParNoms(employe.Id, noms);

            decimal montantTransportNature = indemMap.ContainsKey("transport nature") ? indemMap["transport nature"] : 0;
            decimal montantLogementNature = indemMap.ContainsKey("logement nature") ? indemMap["logement nature"] : 0;
            decimal montantTransportNumeraire = indemMap.ContainsKey("transport numeraire") ? indemMap["transport numeraire"] : 0;
            decimal montantLogementNumeraire = indemMap.ContainsKey("logement numeraire") ? indemMap["logement numeraire"] : 0;
            decimal montantFonction = indemMap.ContainsKey("fonction") ? indemMap["fonction"] : 0;

            decimal deductibiliteIndem = GestionSalaireHoraireForm.DeductibilitesIndemnites.ComputeDeductibiliteTotale(
                salaireBrutSocial,
                montantLogementNumeraire,
                montantLogementNature,
                montantTransportNumeraire,
                montantTransportNature,
                montantFonction
            );

            // 10) IUTS - MÉTHODE EXACTE
            var r = IUTSCalculator.CalculerIUTS(
                salaireBrut,
                cnssEmploye,
                employe.Cadre,
                deductibiliteIndem,
                salaireCategoriel,
                primeAnciennete,
                floorCentaines: true
            );

            decimal baseIutsArr = r.BaseIUTSArrondieCent;
            int nombreCharges = ChargeClass.CountTotalCharges(employe.Id);
            decimal iutsBrut;
            decimal iutsFinal = GestionSalaireHoraireForm.IUTS.Calculer(baseIutsArr, nombreCharges, out iutsBrut);

            snapshot.IUTS_Brut = iutsBrut;
            snapshot.IUTS_Final = iutsFinal;
            snapshot.BaseIUTS = baseIutsArr;
            snapshot.BaseIUTS_Arrondie = baseIutsArr;
            snapshot.NombreCharges = nombreCharges;

            // 11) Salaire net - MÉTHODE EXACTE
            snapshot.SalaireNet = salaireBrut - cnssEmploye - iutsFinal;
            snapshot.EffortPaix = snapshot.SalaireNet * 0.01m;
            snapshot.SalaireNetaPayer = snapshot.SalaireNet - snapshot.EffortPaix - dette;

            // Debug final
            System.Diagnostics.Debug.WriteLine($"SalaireBrut: {salaireBrut}, SalaireNet: {snapshot.SalaireNet}");
            System.Diagnostics.Debug.WriteLine($"Dette finale: {dette}, SalaireNetaPayer: {snapshot.SalaireNetaPayer}");
            System.Diagnostics.Debug.WriteLine("=== Fin CalculerSalaireJournalier ===");
        }

        // ============================================================================
        // TOUTES LES MÉTHODES DE CALCUL ONT ÉTÉ SUPPRIMÉES
        // On utilise maintenant directement les méthodes statiques de GestionSalaireHoraireForm
        // pour garantir une cohérence à 100% avec le système individuel
        // ============================================================================

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
