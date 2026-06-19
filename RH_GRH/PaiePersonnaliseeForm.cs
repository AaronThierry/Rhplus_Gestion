using MySql.Data.MySqlClient;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class PaiePersonnaliseeForm : Form
    {
        // API Windows pour les coins arrondis
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        private PayrollSnapshot _lastSnapshot;
        private DataTable tousLesEmployes;

        public PaiePersonnaliseeForm()
        {
            InitializeComponent();
            StyliserHeader();
            InitPeriode();
        }

        private void StyliserHeader()
        {
            panel2.Height = 85;
            panel2.Paint += (s, e) =>
            {
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    panel2.ClientRectangle,
                    Color.FromArgb(142, 68, 173),    // Violet
                    Color.FromArgb(155, 89, 182),    // Violet clair
                    System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, panel2.ClientRectangle);
                }
            };

            label1.Text = "⚙️ PAIE PERSONNALISÉE";
            label1.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.ForeColor = System.Drawing.Color.White;
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label1.Padding = new System.Windows.Forms.Padding(70, 0, 0, 0);

            label1.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int iconX = 15;
                int iconY = (label1.Height - 32) / 2;

                // Icône : Engrenage
                using (var pen = new Pen(Color.White, 2.2f))
                using (var brush = new SolidBrush(Color.White))
                {
                    // Cercle central
                    e.Graphics.FillEllipse(brush, iconX + 12, iconY + 12, 12, 12);

                    // 6 dents autour
                    float centerX = iconX + 18;
                    float centerY = iconY + 18;
                    float innerRadius = 8;
                    float outerRadius = 14;

                    for (int i = 0; i < 6; i++)
                    {
                        float angle = (float)(i * Math.PI / 3);
                        float x1 = centerX + innerRadius * (float)Math.Cos(angle);
                        float y1 = centerY + innerRadius * (float)Math.Sin(angle);
                        float x2 = centerX + outerRadius * (float)Math.Cos(angle);
                        float y2 = centerY + outerRadius * (float)Math.Sin(angle);

                        e.Graphics.DrawLine(pen, x1, y1, x2, y2);
                    }
                }
            };

            panel2.Invalidate();
            label1.Invalidate();
        }

        private void InitPeriode()
        {
            guna2DateTimePickerDebut.Format = DateTimePickerFormat.Custom;
            guna2DateTimePickerDebut.CustomFormat = "dd/MM/yyyy";
            guna2DateTimePickerFin.Format = DateTimePickerFormat.Custom;
            guna2DateTimePickerFin.CustomFormat = "dd/MM/yyyy";

            guna2DateTimePickerDebut.Value = DateTime.Now;
            guna2DateTimePickerFin.Value = DateTime.Now;

            guna2DateTimePickerFin.MinDate = guna2DateTimePickerDebut.Value.Date;
        }

        private void PaiePersonnaliseeForm_Load(object sender, EventArgs e)
        {
            ChargerTousLesEmployes();
        }

        private void ChargerTousLesEmployes()
        {
            try
            {
                var dbConnect = new Dbconnect();
                using (var con = dbConnect.getconnection)
                {
                    con.Open();

                    string query = @"
                        SELECT
                            p.id_personnel,
                            p.nomPrenom AS Nom,
                            p.matricule AS Matricule,
                            e.nomEntreprise AS Entreprise,
                            p.poste AS Poste,
                            p.telephone AS Telephone,
                            p.adresse AS Adresse,
                            p.contrat AS Contrat,
                            p.identification AS Identification,
                            p.cadre AS Cadre,
                            p.civilite AS Civilite,
                            p.sexe AS Sexe,
                            s.nomService AS Service,
                            d.nomDirection AS Direction,
                            c.nomCategorie AS Categorie,
                            p.numerocnss AS NumeroCNSS,
                            p.typeContrat AS TypeContrat,
                            CONCAT(p.nomPrenom, ' (', p.matricule, ') - ', e.nomEntreprise) AS Display
                        FROM personnel p
                        INNER JOIN entreprise e ON p.id_entreprise = e.id_entreprise
                        LEFT JOIN service s ON s.id_service = p.id_service
                        LEFT JOIN direction d ON d.id_direction = p.id_direction
                        LEFT JOIN categorie c ON c.id_categorie = p.id_categorie
                        WHERE p.Conformite = 0
                        ORDER BY p.nomPrenom";

                    using (var cmd = new MySqlCommand(query, con))
                    {
                        var adapter = new MySqlDataAdapter(cmd);
                        tousLesEmployes = new DataTable();
                        adapter.Fill(tousLesEmployes);

                        AfficherEmployesFiltres(tousLesEmployes);
                        ActiverTousLesChamps();
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement des employés :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
        }

        private void ActiverTousLesChamps()
        {
            textBoxRechercheEmploye.Enabled = true;
            ComboBoxEmploye.Enabled = true;
            textBoxMatricule.Enabled = true;
            textBoxPoste.Enabled = true;
            textBoxtypeContrat.Enabled = true;
            textBoxContrat.Enabled = true;
            textBoxCategorie.Enabled = true;
            textBoxSalaire.Enabled = true;
            textBoxHcontrat.Enabled = true;
            textBoxNP.Enabled = true;
            textBoxAbsences.Enabled = true;
            textBoxJoursFD.Enabled = true;
            textBoxDette.Enabled = true;
            checkBoxCNSS.Enabled = true;
            checkBoxIUTS.Enabled = true;

            buttonAjouter.Enabled = true;
            buttonValider.Enabled = true;
        }

        private void AfficherEmployesFiltres(DataTable dtFiltres)
        {
            if (dtFiltres == null) return;

            DataTable dtAvecDefault = dtFiltres.Copy();

            DataRow defaultRow = dtAvecDefault.NewRow();
            defaultRow["id_personnel"] = 0;
            defaultRow["Display"] = "--- Sélectionner un employé ---";
            dtAvecDefault.Rows.InsertAt(defaultRow, 0);

            ComboBoxEmploye.DataSource = dtAvecDefault;
            ComboBoxEmploye.DisplayMember = "Display";
            ComboBoxEmploye.ValueMember = "id_personnel";

            if (dtFiltres.Rows.Count == 1)
            {
                ComboBoxEmploye.SelectedIndex = 1;
            }
            else
            {
                ComboBoxEmploye.SelectedIndex = 0;
            }
        }

        private void TextBoxRechercheEmploye_TextChanged(object sender, EventArgs e)
        {
            if (tousLesEmployes == null) return;

            string recherche = textBoxRechercheEmploye.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(recherche))
            {
                AfficherEmployesFiltres(tousLesEmployes);
                return;
            }

            var rows = tousLesEmployes.Select(string.Format(
                "Nom LIKE '%{0}%' OR Matricule LIKE '%{0}%' OR Entreprise LIKE '%{0}%'",
                recherche.Replace("'", "''")));

            if (rows.Length > 0)
            {
                DataTable dtFiltre = tousLesEmployes.Clone();
                foreach (var row in rows)
                {
                    dtFiltre.ImportRow(row);
                }
                AfficherEmployesFiltres(dtFiltre);
            }
            else
            {
                DataTable dtVide = tousLesEmployes.Clone();
                DataRow noResultRow = dtVide.NewRow();
                noResultRow["id_personnel"] = 0;
                noResultRow["Display"] = "❌ Aucun employé trouvé";
                dtVide.Rows.Add(noResultRow);

                ComboBoxEmploye.DataSource = dtVide;
                ComboBoxEmploye.DisplayMember = "Display";
                ComboBoxEmploye.ValueMember = "id_personnel";
                ComboBoxEmploye.SelectedIndex = 0;
            }
        }

        private static int? GetSelectedIntOrNull(ComboBox combo, string valueColumnName)
        {
            if (combo.SelectedValue == null) return null;

            if (combo.SelectedValue is int i) return i;

            if (combo.SelectedValue is DataRowView drv)
            {
                var val = drv[valueColumnName];
                return val == DBNull.Value ? (int?)null : Convert.ToInt32(val);
            }

            if (int.TryParse(combo.SelectedValue.ToString(), out var parsed))
                return parsed;

            return null;
        }

        private void ComboBoxEmploye_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? idEmploye = GetSelectedIntOrNull(ComboBoxEmploye, "id_personnel");

            if (!idEmploye.HasValue || idEmploye.Value <= 0)
            {
                return;
            }

            try
            {
                var employe = EmployeService.GetEmployeDetails(idEmploye.Value);

                if (employe != null)
                {
                    textBoxPoste.Text = employe.Poste ?? string.Empty;
                    textBoxMatricule.Text = employe.Matricule ?? string.Empty;
                    textBoxContrat.Text = employe.Contrat?.ToString() ?? string.Empty;
                    textBoxCategorie.Text = employe.Categorie?.ToString() ?? string.Empty;
                    textBoxNP.Text = employe.Nom ?? string.Empty;
                    textBoxtypeContrat.Text = employe.TypeContrat ?? string.Empty;

                    // Déterminer le nombre d'unités contractuelles selon le type
                    if (employe.TypeContrat == "Horaire")
                    {
                        textBoxHcontrat.Text = employe.HeureContrat.ToString();
                    }
                    else if (employe.TypeContrat == "Journalier")
                    {
                        textBoxHcontrat.Text = employe.JourContrat.ToString();
                    }
                    else
                    {
                        textBoxHcontrat.Text = employe.HeureContrat.ToString(); // Par défaut
                    }

                    textBoxSalaire.Text = employe.Montant.HasValue ? employe.Montant.Value.ToString("N2", CultureInfo.CurrentCulture) : string.Empty;
                }
                else
                {
                    MessageBox.Show("L'employé sélectionné n'a pas été trouvé.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la récupération des informations de l'employé : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DateTimePickerFin_ValueChanged(object sender, EventArgs e)
        {
            var d0 = guna2DateTimePickerDebut.Value.Date;
            var d1 = guna2DateTimePickerFin.Value.Date;

            if (d1 < d0)
                guna2DateTimePickerFin.Value = d0;
            textBoxAbsences.Enabled = (d1 > d0);
            textBoxJoursFD.Enabled = (d1 > d0);
        }

        private void guna2DateTimePickerDebut_ValueChanged(object sender, EventArgs e)
        {
            // Mettre à jour la MinDate
            guna2DateTimePickerFin.MinDate = guna2DateTimePickerDebut.Value.Date;

            // Calculer le dernier jour du mois sélectionné
            DateTime dateDebut = guna2DateTimePickerDebut.Value;
            int dernierJour = DateTime.DaysInMonth(dateDebut.Year, dateDebut.Month);
            DateTime dateFin = new DateTime(dateDebut.Year, dateDebut.Month, dernierJour);

            // Mettre automatiquement la date de fin au dernier jour du mois
            guna2DateTimePickerFin.Value = dateFin;
        }

        private void buttonValider_Click(object sender, EventArgs e)
        {
            if (ComboBoxEmploye.SelectedValue == null
                || !int.TryParse(ComboBoxEmploye.SelectedValue.ToString(), out int idEmploye)
                || idEmploye <= 0)
            {
                CustomMessageBox.Show("Sélectionnez un employé valide.", "Validation", CustomMessageBox.MessageType.Warning);
                return;
            }

            var employe = EmployeService.GetEmployeDetails(idEmploye);
            if (employe == null)
            {
                MessageBox.Show("L'employé sélectionné n'a pas été trouvé.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal salaireCategoriel = (decimal)(employe.Montant ?? 0.0);

            // Déterminer le type de contrat et les unités correspondantes
            int unitesTotales;
            decimal unitesAbsences = ParseDecimal(textBoxAbsences.Text);

            if (employe.TypeContrat == "Horaire")
            {
                unitesTotales = employe.HeureContrat;
            }
            else if (employe.TypeContrat == "Journalier")
            {
                unitesTotales = employe.JourContrat;
            }
            else
            {
                unitesTotales = employe.HeureContrat; // Par défaut
            }

            int jsFD = ParseInt(textBoxJoursFD.Text);

            if (salaireCategoriel <= 0m)
            {
                MessageBox.Show("Le salaire catégoriel de l'employé est invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (unitesTotales <= 0m)
            {
                MessageBox.Show("Le nombre d'unités contractuelles doit être > 0.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Calcul du salaire de base
                decimal baseUnitaire, unitesPayees;
                decimal salaireBase = CalculerSalaireBase(
                    salaireCategoriel,
                    unitesTotales,
                    unitesAbsences,
                    out baseUnitaire,
                    out unitesPayees
                );

                // Calcul des heures/jours supplémentaires
                decimal primeSupp;
                decimal tauxSupp;

                if (employe.TypeContrat == "Horaire")
                {
                    primeSupp = GestionSalaireHoraireForm.CalculerHeuresSupp(
                        unitesTotales,
                        salaireCategoriel,
                        0,  // hsNormJour
                        0,  // hsNormNuit
                        jsFD,  // hsFerieJour
                        0   // hsFerieNuit
                    );
                }
                else // Journalier
                {
                    primeSupp = GestionSalaireJournalierForm.CalculerJourSupp(
                        unitesTotales,
                        salaireCategoriel,
                        jsFD
                    );
                }
                tauxSupp = jsFD;

                // Prime Ancienneté
                string anc;
                decimal primeAnciennete = GestionSalaireJournalierForm.CalculerAncienneteEtPrime(idEmploye, out anc);

                // Indemnités
                var sums = GestionSalaireJournalierForm.GetSommeIndemnitesParIds(idEmploye);

                // Sursalaire
                decimal sursalaire = SursalaireRepository.CalculerTotalParPersonnel(idEmploye);

                decimal salaireBrut = GestionSalaireJournalierForm.CalculerSalaireBrut(
                    salaireBase,
                    primeSupp,
                    (decimal)sums["somme_numeraire"],
                    (decimal)sums["somme_nature"],
                    primeAnciennete,
                    sursalaire
                );

                // CNSS et TPA - Calcul selon la case cochée
                decimal cnssEmploye = 0m;
                decimal pensionEmployeur = 0m;
                decimal risqueProEmployeur = 0m;
                decimal pfEmployeur = 0m;
                decimal cnssEmployeur = 0m;
                decimal tpa = 0m;
                decimal tauxTpa = 0m;

                if (checkBoxCNSS.Checked)
                {
                    // Calculer normalement la CNSS
                    var emp = EmployeService.GetEmployeDetails(idEmploye);
                    string dureeContrat = emp.DureeContrat ?? string.Empty;
                    tauxTpa = emp.Tpa.HasValue ? emp.Tpa.Value : 0m;

                    cnssEmploye = CNSSCalculator.CalculerCNSSEmploye(salaireBrut, dureeContrat);
                    pensionEmployeur = CNSSCalculator.CalculerPensionEmployeur(salaireBrut);
                    risqueProEmployeur = CNSSCalculator.CalculerRisqueProEmployeur(salaireBrut);
                    pfEmployeur = CNSSCalculator.CalculerPFEmployeur(salaireBrut);
                    cnssEmployeur = pensionEmployeur + risqueProEmployeur + pfEmployeur;
                    tpa = CNSSCalculator.CalculerTpa(salaireBrut, tauxTpa);
                }

                // Calcul du Salaire Brut Social
                decimal SalairebrutSocial = IUTSCalculator.CalculerSalaireBrutSocial(
                    salaireBrut,
                    salaireDeBase: salaireBase,
                    primeA: primeAnciennete,
                    sursalaire: sursalaire);

                // Déductibilité indemnités
                var noms = new[]
                {
                    "transport nature",
                    "logement nature",
                    "transport numeraire",
                    "logement numeraire",
                    "fonction"
                };

                var indemMap = GestionSalaireJournalierForm.GetIndemnitesParNoms(idEmploye, noms);

                decimal montantTransportNature = indemMap["transport nature"];
                decimal montantLogementNature = indemMap["logement nature"];
                decimal montantTransportNumeraire = indemMap["transport numeraire"];
                decimal montantLogementNumeraire = indemMap["logement numeraire"];
                decimal montantFonction = indemMap["fonction"];

                decimal deductibiliteIndem = GestionSalaireJournalierForm.DeductibilitesIndemnites.ComputeDeductibiliteTotale(
                    SalairebrutSocial, montantLogementNumeraire, montantLogementNature, montantTransportNumeraire, montantTransportNature, montantFonction);

                // Calcul IUTS - Calculer uniquement si la case est cochée
                decimal baseIutsArr = 0m;
                decimal iutsBrut = 0m;
                decimal iutsFinal = 0m;
                int nombreCharges = ChargeClass.CountTotalCharges(idEmploye);

                if (checkBoxIUTS.Checked)
                {
                    var r = IUTSCalculator.CalculerIUTS(
                        salaireBrut, cnssEmploye, employe.Cadre, deductibiliteIndem, salaireCategoriel, primeAnciennete,
                        salaireDeBase: salaireBase, sursalaire: sursalaire, floorCentaines: true);

                    baseIutsArr = r.BaseIUTSArrondieCent;
                    iutsFinal = GestionSalaireJournalierForm.IUTS.Calculer(baseIutsArr, nombreCharges, out iutsBrut);
                }

                // Période
                DateTime d0 = guna2DateTimePickerDebut.Value.Date;
                DateTime d1 = guna2DateTimePickerFin.Value.Date;
                string periode = $"{d0:dd/MM/yyyy} - {d1:dd/MM/yyyy}";

                // Salaire Net
                decimal IndemNat = (decimal)sums["somme_nature"];
                decimal ValeurDette = ParseDecimal(textBoxDette.Text);

                decimal totalAbonnements = 0m;
                int nombreAbonnements = 0;
                if (idEmploye > 0)
                {
                    var abonnements = AbonnementRepository.ListerParPersonnel(idEmploye);
                    totalAbonnements = abonnements.Sum(a => a.Montant);
                    nombreAbonnements = abonnements.Count;
                }

                var res = NetCalculator.Calculer(salaireBrut, cnssEmploye, iutsFinal, IndemNat, ValeurDette, totalAbonnements, 0.01m, true);

                // Créer le snapshot
                var snapshot = new PayrollSnapshot
                {
                    NomPrenom = employe.Nom ?? "",
                    Civilite = employe.Civilite ?? "",
                    Police = employe.Police ?? "",
                    Matricule = employe.Matricule,
                    Identification = employe.Identification ?? "",
                    Poste = employe.Poste,
                    NumeroEmploye = employe.TelephoneEmploye,
                    AdresseEmploye = employe.Adresse,
                    PeriodeSalaire = periode,
                    Categorie = employe.Categorie,
                    Service = employe.Service,
                    Direction = employe.Direction,
                    NumeroCnssEmploye = employe.NumeroCnssEmploye,
                    Sexe = employe.Sexe,
                    Contrat = employe.Contrat,
                    DureeContrat = employe.DureeContrat,
                    HeureContrat = unitesTotales,

                    ModePayement = employe.ModePaiement ?? "",
                    Banque = employe.Banque ?? "",
                    NumeroBancaire = employe.NumeroBancaire ?? "",

                    Sigle = employe.Sigle,
                    NomEntreprise = employe.NomEntreprise,
                    TelephoneEntreprise = employe.TelephoneEntreprise,
                    EmailEntreprise = employe.EmailEntreprise,
                    AdressePhysiqueEntreprise = employe.AdressePhysiqueEntreprise,
                    AdressePostaleEntreprise = employe.AdressePostaleEntreprise,
                    ResponsableEntreprise = employe.ResponsableEntreprise,
                    ResponsablePaie = employe.ResponsablePaie ?? "",
                    RegistreCommerce = employe.RegistreCommerce ?? "",
                    NumeroIFU = employe.NumeroIFU ?? "",
                    NumeroCNSSEntreprise = employe.NumeroCNSSEntreprise ?? "",

                    DateNaissance = employe.DateNaissance,
                    DateEntree = employe.DateEntree,
                    DateSortie = employe?.DateSortie,

                    IdEntreprise = employe.Entreprise,
                    IdEmploye = idEmploye,
                    AncienneteStr = anc,

                    HeuresSupp = primeSupp,
                    IndemNum = (decimal)sums["somme_numeraire"],
                    IndemNat = (decimal)sums["somme_nature"],

                    SalaireBrut = salaireBrut,
                    SalaireBrutSocial = SalairebrutSocial,

                    CNSS_Employe = cnssEmploye,
                    PensionEmployeur = pensionEmployeur,
                    RisqueProEmployeur = risqueProEmployeur,
                    PFEmployeur = pfEmployeur,
                    CNSS_Employeur_Total = cnssEmployeur,
                    TPA = tpa,
                    TauxTPA = tauxTpa,

                    DeductibiliteIndemnites = deductibiliteIndem,
                    BaseIUTS = baseIutsArr,
                    BaseIUTS_Arrondie = baseIutsArr,
                    NombreCharges = nombreCharges,
                    IUTS_Brut = iutsBrut,
                    IUTS_Final = iutsFinal,

                    StatutCadre = employe.Cadre,

                    BaseUnitaire = baseUnitaire,
                    SalaireBase = salaireBase,
                    TauxSalaireDeBase = unitesPayees,

                    PrimeHeuressupp = primeSupp,
                    TauxHeureSupp = tauxSupp,

                    PrimeAnciennete = primeAnciennete,
                    Sursalaire = sursalaire,

                    SalaireNet = res.SalaireNet,
                    EffortPaix = res.Effort,
                    SalaireNetaPayer = res.NetAPayer,
                    ValeurDette = ValeurDette,
                    TotalAbonnements = totalAbonnements,
                    NombreAbonnements = nombreAbonnements,
                    SalaireNetaPayerFinal = res.NetAPayerFinal
                };

                _lastSnapshot = snapshot;

                // Afficher les résultats
                AfficherResultats();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du calcul : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static decimal CalculerSalaireBase(
            decimal salaireCategoriel,
            decimal unitesContractuellesTotales,
            decimal unitesAbsences,
            out decimal baseUnitaire,
            out decimal unitesPayees,
            bool clampAbsences = true)
        {
            if (unitesContractuellesTotales <= 0)
                throw new ArgumentOutOfRangeException(nameof(unitesContractuellesTotales),
                    "Le nombre d'unités contractuelles doit être > 0.");

            if (clampAbsences)
            {
                if (unitesAbsences < 0) unitesAbsences = 0;
                if (unitesAbsences > unitesContractuellesTotales)
                    unitesAbsences = unitesContractuellesTotales;
            }
            else
            {
                if (unitesAbsences < 0 || unitesAbsences > unitesContractuellesTotales)
                    throw new ArgumentOutOfRangeException(nameof(unitesAbsences),
                        "Les absences doivent être comprises entre 0 et les unités totales.");
            }

            baseUnitaire = salaireCategoriel / unitesContractuellesTotales;
            unitesPayees = unitesContractuellesTotales - unitesAbsences;
            decimal salaireBase = baseUnitaire * unitesPayees;

            return Math.Round(salaireBase, 2, MidpointRounding.AwayFromZero);
        }

        private static decimal ParseDecimal(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0m;

            string clean = input.ToUpperInvariant()
                                .Replace("FCFA", "")
                                .Replace(" ", "")
                                .Replace("\u00A0", "")
                                .Trim()
                                .Replace(',', '.');

            return decimal.TryParse(clean, NumberStyles.Any, CultureInfo.InvariantCulture, out var v) ? v : 0m;
        }

        private static int ParseInt(string s) => int.TryParse((s ?? "").Trim(), out var v) ? v : 0;

        private void buttonAjouter_Click(object sender, EventArgs e)
        {
            // Réinitialiser tous les champs
            textBoxJoursFD.Clear();
            textBoxAbsences.Clear();
            textBoxDette.Clear();
            textBoxMatricule.Clear();
            textBoxNP.Clear();
            textBoxPoste.Clear();
            textBoxCategorie.Clear();
            textBoxHcontrat.Clear();
            textBoxSalaire.Clear();
            textBoxtypeContrat.Clear();
            textBoxContrat.Clear();

            ComboBoxEmploye.SelectedIndex = -1;
            textBoxRechercheEmploye.Clear();
            checkBoxCNSS.Checked = true; // Par défaut activé
            checkBoxIUTS.Checked = true; // Par défaut activé

            textBoxJoursFD.Enabled = true;
            textBoxAbsences.Enabled = true;
            textBoxDette.Enabled = true;

            buttonAjouter.Enabled = true;
            buttonValider.Enabled = true;

            CustomMessageBox.Show("✓ Formulaire réinitialisé avec succès.\n\nVous pouvez saisir un nouveau calcul de paie personnalisée.",
                "Nouveau Calcul",
                CustomMessageBox.MessageType.Success,
                CustomMessageBox.MessageButtons.OK);
        }

        private void AfficherResultats()
        {
            if (_lastSnapshot == null)
            {
                return;
            }

            using (var modal = new ResultatsModal(_lastSnapshot))
            {
                var result = modal.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    ImprimerBulletin();
                }
            }
        }


        private void panel3_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Gestionnaire d'événement vide pour le Paint du panel3
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Gestionnaire d'événement vide pour le Click du label1
        }

        private void ImprimerBulletin()
        {
            if (_lastSnapshot == null)
            {
                MessageBox.Show("Effectuez d'abord le calcul pour constituer les valeurs à enregistrer.",
                    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var listeIndemnites = IndemniteClass.GetIndemnitesByEmploye(_lastSnapshot.IdEmploye);

                // Initialiser les variables pour les indemnités
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
                        Numero_indemnite_5 = "08";
                        Nom_Indemnite_5 = indemnite5.NomIndemnite;
                        Montant_Indemnite_5 = indemnite5.MontantIndemnite;
                        Taux_Indemnite_5 = indemnite5.TauxIndem;
                    }
                }

                byte[] logo = EntrepriseClass.GetLogoEntreprise(_lastSnapshot.IdEntreprise);
                var model = new BulletinModel
                {
                    NomEmploye = _lastSnapshot.NomPrenom,
                    Civilite = _lastSnapshot.Civilite,
                    Police = _lastSnapshot.Police,
                    Matricule = _lastSnapshot.Matricule,
                    Identification = _lastSnapshot.Identification,
                    Poste = _lastSnapshot.Poste,
                    NumeroEmploye = _lastSnapshot.NumeroEmploye,
                    Mois = "Paie Personnalisée",
                    HeuresSup = 15000,
                    CNSS = (decimal)_lastSnapshot.CNSS_Employe,
                    AdresseEmploye = _lastSnapshot.AdresseEmploye,
                    Periode = _lastSnapshot.PeriodeSalaire,
                    LogoEntreprise = logo,
                    DateNaissance = _lastSnapshot.DateNaissance,
                    DateDebut = _lastSnapshot.DateEntree,
                    DateFin = _lastSnapshot.DateSortie,
                    Contrat = _lastSnapshot.Contrat,
                    Categorie = _lastSnapshot.Categorie,
                    Service = _lastSnapshot.Service,
                    Direction = _lastSnapshot.Direction,
                    NumeroCNSSEmploye = _lastSnapshot.NumeroCnssEmploye,
                    Sexe = _lastSnapshot.Sexe,
                    Charges = _lastSnapshot.NombreCharges,
                    DureeContrat = _lastSnapshot.DureeContrat,
                    Anciennete = _lastSnapshot.AncienneteStr,
                    NbJourHeure = _lastSnapshot.HeureContrat,
                    Sigle = _lastSnapshot.Sigle,
                    NomEntreprise = _lastSnapshot.NomEntreprise,
                    AdressePhysiqueEntreprise = _lastSnapshot.AdressePhysiqueEntreprise,
                    AdressePostaleEntreprise = _lastSnapshot.AdressePostaleEntreprise,
                    TelephoneEntreprise = _lastSnapshot.TelephoneEntreprise,
                    EmailEntreprise = _lastSnapshot.EmailEntreprise,
                    ResponsableEntreprise = _lastSnapshot.ResponsableEntreprise,
                    ResponsablePaie = _lastSnapshot.ResponsablePaie,
                    RegistreCommerce = _lastSnapshot.RegistreCommerce,
                    NumeroIFU = _lastSnapshot.NumeroIFU,
                    NumeroCNSSEntreprise = _lastSnapshot.NumeroCNSSEntreprise,
                    ModePayement = _lastSnapshot.ModePayement,
                    Banque = _lastSnapshot.Banque,
                    NumeroBancaire = _lastSnapshot.NumeroBancaire,
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
                    baseUnitaire = (double)_lastSnapshot.BaseUnitaire,
                    SalaireDeBase = (double)_lastSnapshot.SalaireBase,
                    TauxSalaireDeBase = (double)_lastSnapshot.TauxSalaireDeBase,
                    PrimeHeureSupp = (double)_lastSnapshot.HeuresSupp,
                    TauxHeureSupp = (double)_lastSnapshot.TauxHeureSupp,
                    PrimeAnciennete = (decimal)_lastSnapshot.PrimeAnciennete,
                    Sursalaire = _lastSnapshot.Sursalaire,
                    SalaireBrut = (double)_lastSnapshot.SalaireBrut,
                    BaseCNSSPlafonnee = (double)Math.Min(_lastSnapshot.SalaireBrut, CNSSCalculator.PLAFOND_CNSS),
                    BaseIUTS = (double)_lastSnapshot.BaseIUTS,
                    Iuts = (double)_lastSnapshot.IUTS_Final,
                    Tpa = (double)_lastSnapshot.TPA,
                    TauxTpa = (double)_lastSnapshot.TauxTPA,
                    CnssEmploye = (double)_lastSnapshot.CNSS_Employe,
                    CnssEmployeur = (double)_lastSnapshot.PensionEmployeur,
                    RisqueProfessionnel = (double)_lastSnapshot.RisqueProEmployeur,
                    PrestationFamiliale = (double)_lastSnapshot.PFEmployeur,
                    AvantageNature = (double)_lastSnapshot.IndemNat,
                    SalaireNet = _lastSnapshot.SalaireNet,
                    EffortDePaix = _lastSnapshot.EffortPaix,
                    SalaireNetaPayer = _lastSnapshot.SalaireNetaPayer,
                    ValeurDette = _lastSnapshot.ValeurDette,
                    TotalAbonnements = _lastSnapshot.TotalAbonnements,
                    NombreAbonnements = _lastSnapshot.NombreAbonnements,
                    SalaireNetaPayerFinal = _lastSnapshot.SalaireNetaPayerFinal,
                    AfficherCNSS = checkBoxCNSS.Checked,  // Afficher CNSS seulement si la case est cochée
                    AfficherIUTS = checkBoxIUTS.Checked,  // Afficher IUTS seulement si la case est cochée
                    AfficherTPA = checkBoxTPA.Checked,    // Afficher TPA seulement si la case est cochée
                    AfficherEffortPaix = checkBoxEffortPaix.Checked  // Afficher Effort de Paix seulement si la case est cochée
                };

                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Title = "Enregistrer le bulletin de paie personnalisée";
                    saveDialog.Filter = "Fichier PDF (*.pdf)|*.pdf";

                    DateTime d0 = guna2DateTimePickerDebut.Value.Date;
                    DateTime d1 = guna2DateTimePickerFin.Value.Date;
                    string periodeSafe = $"{d0:yyyy-MM-dd}_au_{d1:yyyy-MM-dd}";

                    string policeSafe = (model.Police ?? "SANS_POLICE").Replace(" ", "_").Replace("/", "-");
                    string nomEmployeSafe = (model.NomEmploye ?? "Employe").Replace(" ", "_").Replace("/", "-");
                    saveDialog.FileName = $"Bulletin_Personnalise_{policeSafe}_{nomEmployeSafe}_{periodeSafe}.pdf";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        var document = new BulletinDocument(model);
                        document.GeneratePdf(saveDialog.FileName);

                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = saveDialog.FileName,
                            UseShellExecute = true
                        });

                        CustomMessageBox.Show("Bulletin généré avec succès !", "Succès", CustomMessageBox.MessageType.Success, CustomMessageBox.MessageButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue :\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Crée le modèle de bulletin à partir du snapshot calculé
        /// </summary>
        private BulletinModel CreerModèleBulletin(PayrollSnapshot snapshot, int idEmploye)
        {
            // Pour la génération en masse, on laisse les indemnités vides (simplification)
            string Numero_indemnite_1 = string.Empty, Nom_Indemnite_1 = string.Empty, Montant_Indemnite_1 = string.Empty, Taux_Indemnite_1 = string.Empty;
            string Numero_indemnite_2 = string.Empty, Nom_Indemnite_2 = string.Empty, Montant_Indemnite_2 = string.Empty, Taux_Indemnite_2 = string.Empty;
            string Numero_indemnite_3 = string.Empty, Nom_Indemnite_3 = string.Empty, Montant_Indemnite_3 = string.Empty, Taux_Indemnite_3 = string.Empty;
            string Numero_indemnite_4 = string.Empty, Nom_Indemnite_4 = string.Empty, Montant_Indemnite_4 = string.Empty, Taux_Indemnite_4 = string.Empty;
            string Numero_indemnite_5 = string.Empty, Nom_Indemnite_5 = string.Empty, Montant_Indemnite_5 = string.Empty, Taux_Indemnite_5 = string.Empty;

            byte[] logo = EntrepriseClass.GetLogoEntreprise(snapshot.IdEntreprise);
            var model = new BulletinModel
            {
                NomEmploye = snapshot.NomPrenom,
                Civilite = snapshot.Civilite,
                Police = snapshot.Police,
                Matricule = snapshot.Matricule,
                Identification = snapshot.Identification,
                Poste = snapshot.Poste,
                NumeroEmploye = snapshot.NumeroEmploye,
                Mois = "Paie Personnalisée",
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
                ResponsablePaie = snapshot.ResponsablePaie,
                RegistreCommerce = snapshot.RegistreCommerce,
                NumeroIFU = snapshot.NumeroIFU,
                NumeroCNSSEntreprise = snapshot.NumeroCNSSEntreprise,
                ModePayement = snapshot.ModePayement,
                Banque = snapshot.Banque,
                NumeroBancaire = snapshot.NumeroBancaire,
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
                baseUnitaire = (double)snapshot.BaseUnitaire,
                SalaireDeBase = (double)snapshot.SalaireBase,
                TauxSalaireDeBase = (double)snapshot.TauxSalaireDeBase,
                PrimeHeureSupp = (double)snapshot.HeuresSupp,
                TauxHeureSupp = (double)snapshot.TauxHeureSupp,
                PrimeAnciennete = (decimal)snapshot.PrimeAnciennete,
                Sursalaire = snapshot.Sursalaire,
                SalaireBrut = (double)snapshot.SalaireBrut,
                BaseCNSSPlafonnee = (double)Math.Min(snapshot.SalaireBrut, CNSSCalculator.PLAFOND_CNSS),
                BaseIUTS = (double)snapshot.BaseIUTS,
                Iuts = (double)snapshot.IUTS_Final,
                Tpa = (double)snapshot.TPA,
                TauxTpa = (double)snapshot.TauxTPA,
                CnssEmploye = (double)snapshot.CNSS_Employe,
                CnssEmployeur = (double)snapshot.PensionEmployeur,
                RisqueProfessionnel = (double)snapshot.RisqueProEmployeur,
                PrestationFamiliale = (double)snapshot.PFEmployeur,
                AvantageNature = (double)snapshot.IndemNat,
                SalaireNet = snapshot.SalaireNet,
                EffortDePaix = snapshot.EffortPaix,
                SalaireNetaPayer = snapshot.SalaireNetaPayer,
                ValeurDette = snapshot.ValeurDette,
                TotalAbonnements = snapshot.TotalAbonnements,
                NombreAbonnements = snapshot.NombreAbonnements,
                SalaireNetaPayerFinal = snapshot.SalaireNetaPayerFinal,
                AfficherCNSS = checkBoxCNSS.Checked,
                AfficherIUTS = checkBoxIUTS.Checked,
                AfficherTPA = checkBoxTPA.Checked,
                AfficherEffortPaix = checkBoxEffortPaix.Checked
            };

            return model;
        }

        /// <summary>
        /// Calcule automatiquement la paie personnalisée pour un employé sans interaction UI
        /// </summary>
        private PayrollSnapshot CalculerPaieAutomatique(
            int idEmploye,
            DateTime periodeDebut,
            DateTime periodeFin,
            decimal unitesAbsences = 0m,
            int joursFD = 0,
            decimal valeurDette = 0m,
            bool appliquerCNSS = true,
            bool appliquerIUTS = true,
            bool appliquerTPA = true,
            bool appliquerEffortPaix = true)
        {
            var employe = EmployeService.GetEmployeDetails(idEmploye);
            if (employe == null)
            {
                throw new Exception("L'employé n'a pas été trouvé.");
            }

            decimal salaireCategoriel = (decimal)(employe.Montant ?? 0.0);

            // Déterminer le type de contrat et les unités correspondantes
            int unitesTotales;
            if (employe.TypeContrat == "Horaire")
            {
                unitesTotales = employe.HeureContrat;
            }
            else if (employe.TypeContrat == "Journalier")
            {
                unitesTotales = employe.JourContrat;
            }
            else
            {
                unitesTotales = employe.HeureContrat; // Par défaut
            }

            if (salaireCategoriel <= 0m)
            {
                throw new Exception("Le salaire catégoriel de l'employé est invalide.");
            }
            if (unitesTotales <= 0m)
            {
                throw new Exception("Le nombre d'unités contractuelles doit être > 0.");
            }

            // Calcul du salaire de base
            decimal baseUnitaire, unitesPayees;
            decimal salaireBase = CalculerSalaireBase(
                salaireCategoriel,
                unitesTotales,
                unitesAbsences,
                out baseUnitaire,
                out unitesPayees
            );

            // Calcul des heures/jours supplémentaires
            decimal primeSupp;
            decimal tauxSupp;

            if (employe.TypeContrat == "Horaire")
            {
                primeSupp = GestionSalaireHoraireForm.CalculerHeuresSupp(
                    unitesTotales,
                    salaireCategoriel,
                    0,  // hsNormJour
                    0,  // hsNormNuit
                    joursFD,  // hsFerieJour
                    0   // hsFerieNuit
                );
            }
            else // Journalier
            {
                primeSupp = GestionSalaireJournalierForm.CalculerJourSupp(
                    unitesTotales,
                    salaireCategoriel,
                    joursFD
                );
            }
            tauxSupp = joursFD;

            // Prime Ancienneté
            string anc;
            decimal primeAnciennete = GestionSalaireJournalierForm.CalculerAncienneteEtPrime(idEmploye, out anc);

            // Indemnités
            var sums = GestionSalaireJournalierForm.GetSommeIndemnitesParIds(idEmploye);

            // Sursalaire
            decimal sursalaire = SursalaireRepository.CalculerTotalParPersonnel(idEmploye);

            decimal salaireBrut = GestionSalaireJournalierForm.CalculerSalaireBrut(
                salaireBase,
                primeSupp,
                (decimal)sums["somme_numeraire"],
                (decimal)sums["somme_nature"],
                primeAnciennete,
                sursalaire
            );

            // CNSS et TPA - Calcul selon les paramètres
            decimal cnssEmploye = 0m;
            decimal pensionEmployeur = 0m;
            decimal risqueProEmployeur = 0m;
            decimal pfEmployeur = 0m;
            decimal cnssEmployeur = 0m;
            decimal tpa = 0m;
            decimal tauxTpa = 0m;

            if (appliquerCNSS)
            {
                string dureeContrat = employe.DureeContrat ?? string.Empty;
                tauxTpa = employe.Tpa.HasValue ? employe.Tpa.Value : 0m;

                cnssEmploye = CNSSCalculator.CalculerCNSSEmploye(salaireBrut, dureeContrat);
                pensionEmployeur = CNSSCalculator.CalculerPensionEmployeur(salaireBrut);
                risqueProEmployeur = CNSSCalculator.CalculerRisqueProEmployeur(salaireBrut);
                pfEmployeur = CNSSCalculator.CalculerPFEmployeur(salaireBrut);
                cnssEmployeur = pensionEmployeur + risqueProEmployeur + pfEmployeur;

                if (appliquerTPA)
                {
                    tpa = CNSSCalculator.CalculerTpa(salaireBrut, tauxTpa);
                }
            }

            // Calcul du Salaire Brut Social
            decimal SalairebrutSocial = IUTSCalculator.CalculerSalaireBrutSocial(
                salaireBrut,
                salaireDeBase: salaireBase,
                primeA: primeAnciennete,
                sursalaire: sursalaire);

            // Déductibilité indemnités
            var noms = new[]
            {
                "transport nature",
                "logement nature",
                "transport numeraire",
                "logement numeraire",
                "fonction"
            };

            var indemMap = GestionSalaireJournalierForm.GetIndemnitesParNoms(idEmploye, noms);

            decimal montantTransportNature = indemMap["transport nature"];
            decimal montantLogementNature = indemMap["logement nature"];
            decimal montantTransportNumeraire = indemMap["transport numeraire"];
            decimal montantLogementNumeraire = indemMap["logement numeraire"];
            decimal montantFonction = indemMap["fonction"];

            decimal deductibiliteIndem = GestionSalaireJournalierForm.DeductibilitesIndemnites.ComputeDeductibiliteTotale(
                SalairebrutSocial, montantLogementNumeraire, montantLogementNature, montantTransportNumeraire, montantTransportNature, montantFonction);

            // Calcul IUTS - Calculer uniquement si demandé
            decimal baseIutsArr = 0m;
            decimal iutsBrut = 0m;
            decimal iutsFinal = 0m;
            int nombreCharges = ChargeClass.CountTotalCharges(idEmploye);

            if (appliquerIUTS)
            {
                var r = IUTSCalculator.CalculerIUTS(
                    salaireBrut, cnssEmploye, employe.Cadre, deductibiliteIndem, salaireCategoriel, primeAnciennete,
                    salaireDeBase: salaireBase, sursalaire: sursalaire, floorCentaines: true);

                baseIutsArr = r.BaseIUTSArrondieCent;
                iutsFinal = GestionSalaireJournalierForm.IUTS.Calculer(baseIutsArr, nombreCharges, out iutsBrut);
            }

            // Période
            string periode = $"{periodeDebut:dd/MM/yyyy} - {periodeFin:dd/MM/yyyy}";

            // Salaire Net
            decimal IndemNat = (decimal)sums["somme_nature"];

            decimal totalAbonnements = 0m;
            int nombreAbonnements = 0;
            var abonnements = AbonnementRepository.ListerParPersonnel(idEmploye);
            totalAbonnements = abonnements.Sum(a => a.Montant);
            nombreAbonnements = abonnements.Count;

            decimal effortPaix = appliquerEffortPaix ? 0.01m : 0m;
            var res = NetCalculator.Calculer(salaireBrut, cnssEmploye, iutsFinal, IndemNat, valeurDette, totalAbonnements, effortPaix, true);

            // Créer le snapshot
            var snapshot = new PayrollSnapshot
            {
                NomPrenom = employe.Nom ?? "",
                Civilite = employe.Civilite ?? "",
                Police = employe.Police ?? "",
                Matricule = employe.Matricule,
                Identification = employe.Identification ?? "",
                Poste = employe.Poste,
                NumeroEmploye = employe.TelephoneEmploye,
                AdresseEmploye = employe.Adresse,
                PeriodeSalaire = periode,
                Categorie = employe.Categorie,
                Service = employe.Service,
                Direction = employe.Direction,
                NumeroCnssEmploye = employe.NumeroCnssEmploye,
                Sexe = employe.Sexe,
                Contrat = employe.Contrat,
                DureeContrat = employe.DureeContrat,
                HeureContrat = unitesTotales,

                ModePayement = employe.ModePaiement ?? "",
                Banque = employe.Banque ?? "",
                NumeroBancaire = employe.NumeroBancaire ?? "",

                Sigle = employe.Sigle,
                NomEntreprise = employe.NomEntreprise,
                TelephoneEntreprise = employe.TelephoneEntreprise,
                EmailEntreprise = employe.EmailEntreprise,
                AdressePhysiqueEntreprise = employe.AdressePhysiqueEntreprise,
                AdressePostaleEntreprise = employe.AdressePostaleEntreprise,
                ResponsableEntreprise = employe.ResponsableEntreprise,
                ResponsablePaie = employe.ResponsablePaie ?? "",
                RegistreCommerce = employe.RegistreCommerce ?? "",
                NumeroIFU = employe.NumeroIFU ?? "",
                NumeroCNSSEntreprise = employe.NumeroCNSSEntreprise ?? "",

                DateNaissance = employe.DateNaissance,
                DateEntree = employe.DateEntree,
                DateSortie = employe?.DateSortie,

                IdEntreprise = employe.Entreprise,
                IdEmploye = idEmploye,
                AncienneteStr = anc,

                HeuresSupp = primeSupp,
                IndemNum = (decimal)sums["somme_numeraire"],
                IndemNat = (decimal)sums["somme_nature"],

                SalaireBrut = salaireBrut,
                SalaireBrutSocial = SalairebrutSocial,

                CNSS_Employe = cnssEmploye,
                PensionEmployeur = pensionEmployeur,
                RisqueProEmployeur = risqueProEmployeur,
                PFEmployeur = pfEmployeur,
                CNSS_Employeur_Total = cnssEmployeur,
                TPA = tpa,
                TauxTPA = tauxTpa,

                DeductibiliteIndemnites = deductibiliteIndem,
                BaseIUTS = baseIutsArr,
                BaseIUTS_Arrondie = baseIutsArr,
                NombreCharges = nombreCharges,
                IUTS_Brut = iutsBrut,
                IUTS_Final = iutsFinal,

                StatutCadre = employe.Cadre,

                BaseUnitaire = baseUnitaire,
                SalaireBase = salaireBase,
                TauxSalaireDeBase = unitesPayees,

                PrimeHeuressupp = primeSupp,
                TauxHeureSupp = tauxSupp,

                PrimeAnciennete = primeAnciennete,
                Sursalaire = sursalaire,

                SalaireNet = res.SalaireNet,
                EffortPaix = res.Effort,
                SalaireNetaPayer = res.NetAPayer,
                ValeurDette = valeurDette,
                TotalAbonnements = totalAbonnements,
                NombreAbonnements = nombreAbonnements,
                SalaireNetaPayerFinal = res.NetAPayerFinal
            };

            return snapshot;
        }

        /// <summary>
        /// Charge les employés NON conformes pour une entreprise spécifique
        /// </summary>
        private DataTable ChargerEmployesNonConformesParEntreprise(int idEntreprise)
        {
            try
            {
                var dbConnect = new Dbconnect();
                using (var con = dbConnect.getconnection)
                {
                    con.Open();

                    string query = @"
                        SELECT
                            p.id_personnel AS IdPersonnel,
                            p.nomPrenom AS NomPrenom,
                            p.matricule AS Matricule,
                            e.nomEntreprise AS Entreprise,
                            p.poste AS Poste,
                            p.telephone AS Telephone,
                            p.adresse AS Adresse,
                            p.contrat AS Contrat,
                            p.identification AS Identification,
                            p.cadre AS Cadre,
                            p.civilite AS Civilite,
                            p.sexe AS Sexe,
                            s.nomService AS Service,
                            d.nomDirection AS Direction,
                            c.nomCategorie AS Categorie,
                            p.numerocnss AS NumeroCNSS,
                            p.typeContrat AS TypeContrat,
                            p.id_entreprise AS IdEntreprise
                        FROM personnel p
                        INNER JOIN entreprise e ON p.id_entreprise = e.id_entreprise
                        LEFT JOIN service s ON s.id_service = p.id_service
                        LEFT JOIN direction d ON d.id_direction = p.id_direction
                        LEFT JOIN categorie c ON c.id_categorie = p.id_categorie
                        WHERE p.Conformite = 0 AND p.id_entreprise = @idEntreprise
                        ORDER BY p.nomPrenom";

                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);

                        var adapter = new MySqlDataAdapter(cmd);
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement des employés non conformes :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
                return null;
            }
        }

        /// <summary>
        /// Génération en masse des bulletins de paie personnalisés pour tous les employés non conformes
        /// </summary>
        private void buttonGenererMasse_Click(object sender, EventArgs e)
        {
            try
            {
                // Ouvrir le formulaire de sélection d'entreprise et de période
                using (var formSelection = new SelectionEntrepriseModernForm())
                {
                    if (formSelection.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    int idEntreprise = formSelection.EntrepriseSelectionneeId;
                    string nomEntreprise = formSelection.EntrepriseSelectionneeNom;
                    DateTime periodeDebut = formSelection.PeriodeDebut;
                    DateTime periodeFin = formSelection.PeriodeFin;

                    // Définir les dates dans le formulaire
                    guna2DateTimePickerDebut.Value = periodeDebut;
                    guna2DateTimePickerFin.Value = periodeFin;

                    // Charger les employés NON conformes pour cette entreprise
                    DataTable employesNonConformes = ChargerEmployesNonConformesParEntreprise(idEntreprise);

                    if (employesNonConformes == null || employesNonConformes.Rows.Count == 0)
                    {
                        CustomMessageBox.Show($"Aucun employé non conforme trouvé pour l'entreprise {nomEntreprise}.", "Aucun employé",
                            CustomMessageBox.MessageType.Info);
                        return;
                    }

                    // Ouvrir le formulaire de sélection des éléments pour chaque employé
                    List<SelectionElementsModernForm.EmployeElement> employesSelectionnes;
                    using (var formElements = new SelectionElementsModernForm(employesNonConformes, nomEntreprise))
                    {
                        if (formElements.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                        employesSelectionnes = formElements.EmployesSelectionnes;
                    }

                    if (employesSelectionnes == null || employesSelectionnes.Count == 0)
                    {
                        return;
                    }

                    // Demander confirmation
                    var confirmation = CustomMessageBox.Show(
                        $"Vous allez générer les bulletins de paie pour {employesSelectionnes.Count} employé(s) non conforme(s).\n\n" +
                        $"Entreprise: {nomEntreprise}\n" +
                        $"Période: du {periodeDebut:dd/MM/yyyy} au {periodeFin:dd/MM/yyyy}\n\n" +
                        "Voulez-vous continuer ?",
                        "Confirmation génération en masse",
                        CustomMessageBox.MessageType.Question,
                        CustomMessageBox.MessageButtons.YesNo);

                    if (confirmation != DialogResult.Yes)
                    {
                        return;
                    }

                    // Choisir le dossier de destination
                    using (var folderDialog = new FolderBrowserDialog())
                    {
                        folderDialog.Description = "Sélectionnez le dossier où enregistrer l'archive ZIP des bulletins";
                        folderDialog.ShowNewFolderButton = true;

                        if (folderDialog.ShowDialog() == DialogResult.OK)
                        {
                            string dossierDestination = folderDialog.SelectedPath;

                            // Créer un dossier temporaire pour les PDFs
                            string periodeSafe = $"{periodeDebut:yyyy-MM-dd}_au_{periodeFin:yyyy-MM-dd}";
                            string nomEntrepriseSafe = nomEntreprise.Replace(" ", "_").Replace("/", "-").Replace("\\", "-");
                            string dossierTemp = Path.Combine(Path.GetTempPath(), $"Bulletins_Personnalises_{nomEntrepriseSafe}_{periodeSafe}_{DateTime.Now:HHmmss}");

                            if (!Directory.Exists(dossierTemp))
                            {
                                Directory.CreateDirectory(dossierTemp);
                            }

                            int nbSuccess = 0;
                            int nbErrors = 0;
                            List<string> erreurs = new List<string>();

                            // Créer une belle modale de progression moderne
                            using (var progressForm = new Form())
                            {
                                // Configuration du formulaire
                                progressForm.Text = "";
                                progressForm.Size = new Size(600, 220);
                                progressForm.StartPosition = FormStartPosition.CenterScreen;
                                progressForm.FormBorderStyle = FormBorderStyle.None;
                                progressForm.BackColor = Color.White;
                                progressForm.MaximizeBox = false;
                                progressForm.MinimizeBox = false;
                                progressForm.ShowInTaskbar = false;
                                progressForm.TopMost = true;

                                // Ombre portée
                                var shadowPanel = new Panel
                                {
                                    Dock = DockStyle.Fill,
                                    BackColor = Color.White,
                                    Padding = new Padding(2)
                                };
                                progressForm.Controls.Add(shadowPanel);

                                // Header avec couleur violet
                                var headerPanel = new Panel
                                {
                                    Dock = DockStyle.Top,
                                    Height = 60,
                                    BackColor = Color.FromArgb(142, 68, 173)
                                };
                                shadowPanel.Controls.Add(headerPanel);

                                var labelTitre = new Label
                                {
                                    Text = $"⚙️ Génération en masse - {nomEntreprise}",
                                    Dock = DockStyle.Fill,
                                    Font = new Font("Montserrat", 11F, FontStyle.Bold),
                                    ForeColor = Color.White,
                                    TextAlign = ContentAlignment.MiddleCenter
                                };
                                headerPanel.Controls.Add(labelTitre);

                                // Panel principal
                                var mainPanel = new Panel
                                {
                                    Dock = DockStyle.Fill,
                                    BackColor = Color.White,
                                    Padding = new Padding(30, 20, 30, 20)
                                };
                                shadowPanel.Controls.Add(mainPanel);

                                // Label de compteur
                                var labelCompteur = new Label
                                {
                                    Text = $"0 / {employesSelectionnes.Count}",
                                    Location = new Point(0, 10),
                                    Size = new Size(540, 25),
                                    Font = new Font("Montserrat", 9F, FontStyle.Bold),
                                    ForeColor = Color.FromArgb(142, 68, 173),
                                    TextAlign = ContentAlignment.MiddleCenter
                                };
                                mainPanel.Controls.Add(labelCompteur);

                                // Barre de progression Guna2
                                var progressBar = new Guna.UI2.WinForms.Guna2ProgressBar
                                {
                                    Minimum = 0,
                                    Maximum = employesSelectionnes.Count,
                                    Value = 0,
                                    Location = new Point(0, 45),
                                    Size = new Size(540, 30),
                                    FillColor = Color.FromArgb(230, 230, 240),
                                    ProgressColor = Color.FromArgb(142, 68, 173),
                                    ProgressColor2 = Color.FromArgb(155, 89, 182),
                                    BorderRadius = 8,
                                    TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias
                                };
                                mainPanel.Controls.Add(progressBar);

                                // Label de pourcentage
                                var labelPourcentage = new Label
                                {
                                    Text = "0%",
                                    Location = new Point(490, 45),
                                    Size = new Size(50, 30),
                                    Font = new Font("Montserrat", 9F, FontStyle.Bold),
                                    ForeColor = Color.FromArgb(142, 68, 173),
                                    TextAlign = ContentAlignment.MiddleRight
                                };
                                mainPanel.Controls.Add(labelPourcentage);

                                // Label de progression détaillé
                                var labelProgress = new Label
                                {
                                    Text = "Initialisation...",
                                    Location = new Point(0, 85),
                                    Size = new Size(540, 40),
                                    Font = new Font("Montserrat", 8.5F, FontStyle.Regular),
                                    ForeColor = Color.FromArgb(80, 80, 80),
                                    TextAlign = ContentAlignment.TopCenter
                                };
                                mainPanel.Controls.Add(labelProgress);

                                // Effet d'arrondi pour le formulaire
                                progressForm.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, progressForm.Width, progressForm.Height, 15, 15));

                                // Animation d'entrée
                                progressForm.Opacity = 0;
                                progressForm.Show();

                                var timer = new System.Windows.Forms.Timer { Interval = 10 };
                                timer.Tick += (s, args) =>
                                {
                                    if (progressForm.Opacity < 1)
                                    {
                                        progressForm.Opacity += 0.1;
                                    }
                                    else
                                    {
                                        timer.Stop();
                                        timer.Dispose();
                                    }
                                };
                                timer.Start();
                                Application.DoEvents();

                                // Générer pour chaque employé avec les éléments sélectionnés
                                foreach (var employe in employesSelectionnes)
                            {
                                string nomPrenom = employe.NomPrenom;
                                string matricule = employe.Matricule;
                                int idEmploye = employe.IdPersonnel;

                                try
                                {

                                    labelProgress.Text = $"Génération pour {nomPrenom} ({matricule})...";
                                    Application.DoEvents();

                                    // Calculer automatiquement la paie selon les éléments sélectionnés
                                    var snapshot = CalculerPaieAutomatique(
                                        idEmploye,
                                        periodeDebut,
                                        periodeFin,
                                        unitesAbsences: 0m,
                                        joursFD: 0,
                                        valeurDette: 0m,
                                        appliquerCNSS: employe.CNSS,
                                        appliquerIUTS: employe.IUTS,
                                        appliquerTPA: employe.TPA,
                                        appliquerEffortPaix: employe.EffortPaix
                                    );

                                    // Vérifier si le calcul a réussi
                                    if (snapshot != null && snapshot.SalaireNet > 0)
                                    {
                                        // Mettre à jour les checkboxes pour le modèle de bulletin
                                        checkBoxCNSS.Checked = employe.CNSS;
                                        checkBoxIUTS.Checked = employe.IUTS;
                                        checkBoxTPA.Checked = employe.TPA;
                                        checkBoxEffortPaix.Checked = employe.EffortPaix;

                                        // Créer le modèle de bulletin
                                        var model = CreerModèleBulletin(snapshot, idEmploye);

                                        // Générer le PDF dans le dossier temporaire
                                        string matriculeSafe = matricule.Replace(" ", "_").Replace("/", "-");
                                        string nomEmployeSafe = nomPrenom.Replace(" ", "_").Replace("/", "-");

                                        string fileName = $"Bulletin_Personnalise_{matriculeSafe}_{nomEmployeSafe}_{periodeSafe}.pdf";
                                        string fullPath = Path.Combine(dossierTemp, fileName);

                                        var document = new BulletinDocument(model);
                                        document.GeneratePdf(fullPath);

                                        nbSuccess++;
                                    }
                                    else
                                    {
                                        nbErrors++;
                                        erreurs.Add($"{nomPrenom} ({matricule}): Erreur de calcul");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    nbErrors++;
                                    erreurs.Add($"{nomPrenom} ({matricule}): {ex.Message}");
                                }

                                // Mettre à jour la progression
                                progressBar.Value++;
                                int pourcentage = (int)((double)progressBar.Value / employesSelectionnes.Count * 100);
                                labelCompteur.Text = $"{progressBar.Value} / {employesSelectionnes.Count}";
                                labelPourcentage.Text = $"{pourcentage}%";
                                Application.DoEvents();
                            }

                                // Animation de fermeture
                                var closeTimer = new System.Windows.Forms.Timer { Interval = 10 };
                                closeTimer.Tick += (s, args) =>
                                {
                                    if (progressForm.Opacity > 0)
                                    {
                                        progressForm.Opacity -= 0.15;
                                    }
                                    else
                                    {
                                        closeTimer.Stop();
                                        closeTimer.Dispose();
                                        progressForm.Close();
                                    }
                                };
                                closeTimer.Start();

                                // Attendre que l'animation soit terminée
                                while (progressForm.Opacity > 0)
                                {
                                    Application.DoEvents();
                                    System.Threading.Thread.Sleep(15);
                                }
                            }

                            // Créer le fichier ZIP si des bulletins ont été générés avec succès
                            string nomZip = "";
                            if (nbSuccess > 0)
                            {
                                try
                                {
                                    nomZip = $"Bulletins_Personnalises_{nomEntrepriseSafe}_{periodeSafe}.zip";
                                    string cheminZip = Path.Combine(dossierDestination, nomZip);

                                    // Supprimer le ZIP s'il existe déjà
                                    if (File.Exists(cheminZip))
                                    {
                                        File.Delete(cheminZip);
                                    }

                                    // Créer l'archive ZIP
                                    ZipFile.CreateFromDirectory(dossierTemp, cheminZip);

                                    // Nettoyer le dossier temporaire
                                    try
                                    {
                                        Directory.Delete(dossierTemp, true);
                                    }
                                    catch
                                    {
                                        // Ignorer les erreurs de nettoyage
                                    }
                                }
                                catch (Exception ex)
                                {
                                    nbErrors++;
                                    erreurs.Add($"Erreur lors de la création du ZIP: {ex.Message}");
                                }
                            }

                            // Afficher le résultat
                            string message = $"Génération terminée !\n\n" +
                                           $"Entreprise: {nomEntreprise}\n" +
                                           $"✓ Réussis: {nbSuccess}\n" +
                                           $"✗ Échecs: {nbErrors}\n\n";

                            if (nbSuccess > 0 && !string.IsNullOrEmpty(nomZip))
                            {
                                message += $"📦 Archive ZIP : {nomZip}\n" +
                                          $"📁 Dossier : {dossierDestination}";
                            }
                            else
                            {
                                message += $"Les bulletins ont été enregistrés dans:\n{dossierDestination}";
                            }

                            if (erreurs.Count > 0 && erreurs.Count <= 5)
                            {
                                message += "\n\nErreurs:\n" + string.Join("\n", erreurs);
                            }
                            else if (erreurs.Count > 5)
                            {
                                message += $"\n\n{erreurs.Count} erreurs détectées. Consultez les logs pour plus de détails.";
                            }

                            CustomMessageBox.Show(message, "Résultat génération en masse",
                                nbErrors == 0 ? CustomMessageBox.MessageType.Success : CustomMessageBox.MessageType.Warning);

                            // Ouvrir le dossier
                            if (nbSuccess > 0)
                            {
                                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                                {
                                    FileName = dossierDestination,
                                    UseShellExecute = true
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de la génération en masse:\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
        }
    }
}
