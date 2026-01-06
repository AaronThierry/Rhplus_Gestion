using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class ModifierEntrepriseForm : Form
    {
        private readonly int idEntrepriseActuelle;
        private readonly string nomEntrepriseActuel;
        private readonly string formeJuridiqueActuelle;
        private readonly string sigleActuel;
        private readonly string activiteActuelle;
        private readonly string adressePhysiqueActuelle;
        private readonly string adressePostaleActuelle;
        private readonly string telephoneActuel;
        private readonly string communeActuelle;
        private readonly string quartierActuel;
        private readonly string rueActuelle;
        private readonly string lotActuel;
        private readonly string centreImpotsActuel;
        private readonly string numeroIfuActuel;
        private readonly string numeroCnssActuel;
        private readonly string codeActiviteActuel;
        private readonly string regimeFiscalActuel;
        private readonly string registreCommerceActuel;
        private readonly string numeroBancaireActuel;
        private readonly decimal? tpaActuelle;
        private readonly string emailActuel;
        private readonly byte[] logoActuel;

        public ModifierEntrepriseForm(
            int idEntreprise, string nomEntreprise, string formeJuridique, string sigle,
            string activite, string adressePhysique, string adressePostale, string telephone,
            string commune, string quartier, string rue, string lot, string centreImpots,
            string numeroIfu, string numeroCnss, string codeActivite, string regimeFiscal,
            string registreCommerce, string numeroBancaire, decimal? tpa, string email, byte[] logo)
        {
            InitializeComponent();

            this.idEntrepriseActuelle = idEntreprise;
            this.nomEntrepriseActuel = nomEntreprise;
            this.formeJuridiqueActuelle = formeJuridique;
            this.sigleActuel = sigle;
            this.activiteActuelle = activite;
            this.adressePhysiqueActuelle = adressePhysique;
            this.adressePostaleActuelle = adressePostale;
            this.telephoneActuel = telephone;
            this.communeActuelle = commune;
            this.quartierActuel = quartier;
            this.rueActuelle = rue;
            this.lotActuel = lot;
            this.centreImpotsActuel = centreImpots;
            this.numeroIfuActuel = numeroIfu;
            this.numeroCnssActuel = numeroCnss;
            this.codeActiviteActuel = codeActivite;
            this.regimeFiscalActuel = regimeFiscal;
            this.registreCommerceActuel = registreCommerce;
            this.numeroBancaireActuel = numeroBancaire;
            this.tpaActuelle = tpa;
            this.emailActuel = email;
            this.logoActuel = logo;

            ChargerDonnees();
        }

        private void ChargerDonnees()
        {
            textBoxNomEntreprise.Text = nomEntrepriseActuel;
            textBoxFormeJuridique.Text = formeJuridiqueActuelle;
            textBoxSigle.Text = sigleActuel;
            textBoxActivite.Text = activiteActuelle;
            textBoxAdressePhysique.Text = adressePhysiqueActuelle;
            textBoxAdressePostale.Text = adressePostaleActuelle;
            textBoxTelephone.Text = telephoneActuel;
            textBoxCommune.Text = communeActuelle;
            textBoxQuartier.Text = quartierActuel;
            textBoxRue.Text = rueActuelle;
            textBoxLot.Text = lotActuel;
            textBoxCentreImpots.Text = centreImpotsActuel;
            textBoxNumeroIfu.Text = numeroIfuActuel;
            textBoxNumeroCnss.Text = numeroCnssActuel;
            textBoxCodeActivite.Text = codeActiviteActuel;
            textBoxRegimeFiscal.Text = regimeFiscalActuel;
            textBoxRegistreCommerce.Text = registreCommerceActuel;
            textBoxNumeroBancaire.Text = numeroBancaireActuel;
            textBoxTPA.Text = tpaActuelle.HasValue ? tpaActuelle.Value.ToString("F2") : "";
            textBoxEmail.Text = emailActuel;

            // Charger le logo
            if (logoActuel != null && logoActuel.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(logoActuel))
                {
                    pictureBoxLogo.Image = Image.FromStream(ms);
                }
            }
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonValider_Click(object sender, EventArgs e)
        {
            // Récupération des valeurs
            string nomEntreprise = textBoxNomEntreprise.Text?.Trim();
            string formeJuridique = textBoxFormeJuridique.Text?.Trim();
            string sigle = textBoxSigle.Text?.Trim();
            string activite = textBoxActivite.Text?.Trim();
            string adressePhysique = textBoxAdressePhysique.Text?.Trim();
            string adressePostale = textBoxAdressePostale.Text?.Trim();
            string telephone = textBoxTelephone.Text?.Trim();
            string commune = textBoxCommune.Text?.Trim();
            string quartier = textBoxQuartier.Text?.Trim();
            string rue = textBoxRue.Text?.Trim();
            string lot = textBoxLot.Text?.Trim();
            string centreImpots = textBoxCentreImpots.Text?.Trim();
            string numeroIfu = textBoxNumeroIfu.Text?.Trim();
            string numeroCnss = textBoxNumeroCnss.Text?.Trim();
            string codeActivite = textBoxCodeActivite.Text?.Trim();
            string regimeFiscal = textBoxRegimeFiscal.Text?.Trim();
            string registreCommerce = textBoxRegistreCommerce.Text?.Trim();
            string numeroBancaire = textBoxNumeroBancaire.Text?.Trim();
            string email = textBoxEmail.Text?.Trim();

            // Validation du nom (obligatoire)
            if (string.IsNullOrWhiteSpace(nomEntreprise))
            {
                CustomMessageBox.Show("Veuillez saisir le nom de l'entreprise.", "Information",
                                CustomMessageBox.MessageType.Info);
                textBoxNomEntreprise.Focus();
                return;
            }

            // Validation du téléphone (obligatoire)
            if (string.IsNullOrWhiteSpace(telephone))
            {
                CustomMessageBox.Show("Veuillez saisir le téléphone de l'entreprise.", "Information",
                                CustomMessageBox.MessageType.Info);
                textBoxTelephone.Focus();
                return;
            }

            // Validation du logo (obligatoire)
            if (pictureBoxLogo.Image == null)
            {
                CustomMessageBox.Show("Veuillez sélectionner un logo pour l'entreprise.", "Information",
                                CustomMessageBox.MessageType.Info);
                return;
            }

            // Validation du TPA (nullable decimal)
            decimal? tpa = null;
            if (!string.IsNullOrWhiteSpace(textBoxTPA.Text?.Trim()))
            {
                if (!decimal.TryParse(textBoxTPA.Text.Trim(), out decimal tpaValue) || tpaValue < 0)
                {
                    CustomMessageBox.Show("Veuillez saisir un TPA valide (nombre positif).", "Information",
                                    CustomMessageBox.MessageType.Info);
                    textBoxTPA.Focus();
                    return;
                }
                tpa = tpaValue;
            }

            // Détection des changements
            bool changed = false;
            byte[] logoBytes = null;

            using (MemoryStream ms = new MemoryStream())
            {
                pictureBoxLogo.Image.Save(ms, pictureBoxLogo.Image.RawFormat);
                logoBytes = ms.ToArray();

                if (nomEntreprise != nomEntrepriseActuel ||
                    formeJuridique != formeJuridiqueActuelle ||
                    sigle != sigleActuel ||
                    activite != activiteActuelle ||
                    adressePhysique != adressePhysiqueActuelle ||
                    adressePostale != adressePostaleActuelle ||
                    telephone != telephoneActuel ||
                    commune != communeActuelle ||
                    quartier != quartierActuel ||
                    rue != rueActuelle ||
                    lot != lotActuel ||
                    centreImpots != centreImpotsActuel ||
                    numeroIfu != numeroIfuActuel ||
                    numeroCnss != numeroCnssActuel ||
                    codeActivite != codeActiviteActuel ||
                    regimeFiscal != regimeFiscalActuel ||
                    registreCommerce != registreCommerceActuel ||
                    numeroBancaire != numeroBancaireActuel ||
                    tpa != tpaActuelle ||
                    email != emailActuel ||
                    !AreBytesEqual(logoBytes, logoActuel))
                {
                    changed = true;
                }
            }

            if (!changed)
            {
                CustomMessageBox.Show("Aucune modification détectée.", "Information",
                                CustomMessageBox.MessageType.Info);
                return;
            }

            try
            {
                // Modification
                EntrepriseClass entreprise = new EntrepriseClass();
                bool success = entreprise.updateEntreprise(
                    idEntrepriseActuelle, nomEntreprise, formeJuridique, sigle, activite,
                    adressePhysique, adressePostale, telephone, commune, quartier, rue, lot,
                    centreImpots, numeroIfu, numeroCnss, codeActivite, regimeFiscal,
                    registreCommerce, numeroBancaire, tpa, logoBytes, email
                );

                if (success)
                {
                    CustomMessageBox.Show("Entreprise modifiée avec succès.", "Succès",
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
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de la modification :\n{ex.Message}", "Erreur",
                                CustomMessageBox.MessageType.Error);
            }
        }

        private bool AreBytesEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            if (a.Length != b.Length) return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i]) return false;
            }

            return true;
        }

        private void buttonParcourir_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Images (*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            opf.Title = "Sélectionner le logo de l'entreprise";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxLogo.Image = Image.FromFile(opf.FileName);
            }
        }
    }
}
