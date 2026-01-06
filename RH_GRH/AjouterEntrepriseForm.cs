using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class AjouterEntrepriseForm : Form
    {
        public AjouterEntrepriseForm()
        {
            InitializeComponent();
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

            // Vérification de doublon
            if (EntrepriseClass.entrepriseExiste(nomEntreprise, sigle))
            {
                CustomMessageBox.Show("Une entreprise avec ce nom ou ce sigle existe déjà.", "Attention",
                                CustomMessageBox.MessageType.Warning);
                textBoxNomEntreprise.Focus();
                return;
            }

            try
            {
                // Conversion du logo en byte[]
                MemoryStream ms = new MemoryStream();
                pictureBoxLogo.Image.Save(ms, pictureBoxLogo.Image.RawFormat);
                byte[] logoBytes = ms.ToArray();

                // Enregistrement
                EntrepriseClass entreprise = new EntrepriseClass();
                bool success = entreprise.insertEntreprise(
                    nomEntreprise, formeJuridique, sigle, activite, adressePhysique, adressePostale,
                    telephone, commune, quartier, rue, lot, centreImpots, numeroIfu, numeroCnss,
                    codeActivite, regimeFiscal, registreCommerce, numeroBancaire, tpa, logoBytes, email
                );

                if (success)
                {
                    CustomMessageBox.Show("Entreprise ajoutée avec succès.", "Succès",
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
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de l'enregistrement :\n{ex.Message}", "Erreur",
                                CustomMessageBox.MessageType.Error);
            }
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
