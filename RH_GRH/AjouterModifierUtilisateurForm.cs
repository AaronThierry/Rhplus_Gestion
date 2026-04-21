using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using RH_GRH.Auth;
using RH_GRH.Auth.Models;

namespace RH_GRH
{
    public partial class AjouterModifierUtilisateurForm : Form
    {
        private Dbconnect db = new Dbconnect();
        private int? utilisateurId = null;
        private bool modeModification = false;
        private List<int> rolesSelectiones = new List<int>();

        public AjouterModifierUtilisateurForm(int? userId = null)
        {
            InitializeComponent();
            this.utilisateurId = userId;
            this.modeModification = userId.HasValue;

            InitialiserFormulaire();
        }

        private void InitialiserFormulaire()
        {
            // Titre du formulaire
            this.Text = modeModification ? "Modifier un utilisateur" : "Ajouter un utilisateur";
            labelTitre.Text = modeModification ? "Modification d'un utilisateur" : "Nouvel utilisateur";

            // Charger les rôles disponibles
            ChargerRoles();

            // Si mode modification, charger les données
            if (modeModification)
            {
                // En mode modification, masquer complètement la section mot de passe
                groupBoxMotDePasse.Visible = false;
                // Ajuster la position du groupBoxRoles
                groupBoxRoles.Location = new System.Drawing.Point(20, 270);

                ChargerUtilisateur(utilisateurId.Value);
            }
            else
            {
                checkBoxActif.Checked = true; // Actif par défaut pour nouveau user
            }
        }

        private void ChargerRoles()
        {
            try
            {
                db.openConnect();
                string query = "SELECT id, nom_role, description FROM roles ORDER BY nom_role";
                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                MySqlDataReader reader = cmd.ExecuteReader();

                checkedListBoxRoles.Items.Clear();

                while (reader.Read())
                {
                    int roleId = reader.GetInt32("id");
                    string nomRole = reader.GetString("nom_role");
                    string description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description");

                    string displayText = $"{nomRole} - {description}";
                    checkedListBoxRoles.Items.Add(new RoleItem { Id = roleId, Nom = nomRole, Description = description, DisplayText = displayText });
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement des rôles :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
            finally
            {
                db.closeConnect();
            }
        }

        private void ChargerUtilisateur(int userId)
        {
            try
            {
                db.openConnect();

                // Charger les infos utilisateur
                string query = @"SELECT nom_utilisateur, nom_complet, email, telephone, actif
                               FROM utilisateurs WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@id", userId);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    textBoxNomUtilisateur.Text = reader.GetString("nom_utilisateur");
                    textBoxNomComplet.Text = reader.GetString("nom_complet");
                    textBoxEmail.Text = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString("email");
                    textBoxTelephone.Text = reader.IsDBNull(reader.GetOrdinal("telephone")) ? "" : reader.GetString("telephone");
                    checkBoxActif.Checked = reader.GetBoolean("actif");
                }

                reader.Close();

                // Charger les rôles de l'utilisateur
                query = "SELECT role_id FROM utilisateur_roles WHERE utilisateur_id = @id";
                cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@id", userId);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    rolesSelectiones.Add(reader.GetInt32("role_id"));
                }

                reader.Close();

                // Cocher les rôles correspondants
                for (int i = 0; i < checkedListBoxRoles.Items.Count; i++)
                {
                    RoleItem role = (RoleItem)checkedListBoxRoles.Items[i];
                    if (rolesSelectiones.Contains(role.Id))
                    {
                        checkedListBoxRoles.SetItemChecked(i, true);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement de l'utilisateur :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
            finally
            {
                db.closeConnect();
            }
        }

        private void buttonEnregistrer_Click(object sender, EventArgs e)
        {
            if (!ValiderFormulaire())
                return;

            if (modeModification)
            {
                ModifierUtilisateur();
            }
            else
            {
                AjouterUtilisateur();
            }
        }

        private bool ValiderFormulaire()
        {
            // Nom d'utilisateur
            if (string.IsNullOrWhiteSpace(textBoxNomUtilisateur.Text))
            {
                CustomMessageBox.Show("Le nom d'utilisateur est requis",
                    "Champ requis", CustomMessageBox.MessageType.Warning);
                textBoxNomUtilisateur.Focus();
                return false;
            }

            if (textBoxNomUtilisateur.Text.Length < 3)
            {
                CustomMessageBox.Show("Le nom d'utilisateur doit contenir au moins 3 caractères",
                    "Validation", CustomMessageBox.MessageType.Warning);
                textBoxNomUtilisateur.Focus();
                return false;
            }

            // Nom complet
            if (string.IsNullOrWhiteSpace(textBoxNomComplet.Text))
            {
                CustomMessageBox.Show("Le nom complet est requis",
                    "Champ requis", CustomMessageBox.MessageType.Warning);
                textBoxNomComplet.Focus();
                return false;
            }

            // Email (optionnel mais doit être valide si fourni)
            if (!string.IsNullOrWhiteSpace(textBoxEmail.Text))
            {
                if (!textBoxEmail.Text.Contains("@") || !textBoxEmail.Text.Contains("."))
                {
                    CustomMessageBox.Show("L'adresse email n'est pas valide",
                        "Validation", CustomMessageBox.MessageType.Warning);
                    textBoxEmail.Focus();
                    return false;
                }
            }

            // En mode ajout, le mot de passe par défaut sera attribué automatiquement
            // Pas besoin de validation ici

            // Au moins un rôle doit être sélectionné
            if (checkedListBoxRoles.CheckedItems.Count == 0)
            {
                CustomMessageBox.Show("Vous devez sélectionner au moins un rôle pour l'utilisateur",
                    "Validation", CustomMessageBox.MessageType.Warning);
                return false;
            }

            return true;
        }

        private void AjouterUtilisateur()
        {
            try
            {
                db.openConnect();

                // Vérifier si le nom d'utilisateur existe déjà
                string checkQuery = "SELECT COUNT(*) FROM utilisateurs WHERE nom_utilisateur = @username";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, db.getconnection);
                checkCmd.Parameters.AddWithValue("@username", textBoxNomUtilisateur.Text.Trim());
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    CustomMessageBox.Show("Ce nom d'utilisateur existe déjà",
                        "Erreur", CustomMessageBox.MessageType.Error);
                    return;
                }

                // Utiliser le mot de passe par défaut pour les nouveaux utilisateurs
                string motDePasseParDefaut = Auth.PasswordGenerator.GenerateDefaultPassword();
                string passwordHash = PasswordHasher.HashPassword(motDePasseParDefaut);

                // Insérer l'utilisateur
                string insertQuery = @"INSERT INTO utilisateurs
                                     (nom_utilisateur, mot_de_passe_hash, nom_complet, email, telephone, actif,
                                      date_creation, date_modification, tentatives_echec, compte_verrouille,
                                      premier_connexion, mot_de_passe_par_defaut)
                                     VALUES
                                     (@username, @password, @nomComplet, @email, @telephone, @actif,
                                      NOW(), NOW(), 0, 0, TRUE, @motDePasseDefaut)";

                MySqlCommand insertCmd = new MySqlCommand(insertQuery, db.getconnection);
                insertCmd.Parameters.AddWithValue("@username", textBoxNomUtilisateur.Text.Trim());
                insertCmd.Parameters.AddWithValue("@password", passwordHash);
                insertCmd.Parameters.AddWithValue("@motDePasseDefaut", motDePasseParDefaut);
                insertCmd.Parameters.AddWithValue("@nomComplet", textBoxNomComplet.Text.Trim());
                insertCmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(textBoxEmail.Text) ? (object)DBNull.Value : textBoxEmail.Text.Trim());
                insertCmd.Parameters.AddWithValue("@telephone", string.IsNullOrWhiteSpace(textBoxTelephone.Text) ? (object)DBNull.Value : textBoxTelephone.Text.Trim());
                insertCmd.Parameters.AddWithValue("@actif", checkBoxActif.Checked);

                insertCmd.ExecuteNonQuery();

                // Récupérer l'ID du nouvel utilisateur
                long newUserId = insertCmd.LastInsertedId;

                // Assigner les rôles
                foreach (RoleItem role in checkedListBoxRoles.CheckedItems)
                {
                    string roleQuery = "INSERT INTO utilisateur_roles (utilisateur_id, role_id) VALUES (@userId, @roleId)";
                    MySqlCommand roleCmd = new MySqlCommand(roleQuery, db.getconnection);
                    roleCmd.Parameters.AddWithValue("@userId", newUserId);
                    roleCmd.Parameters.AddWithValue("@roleId", role.Id);
                    roleCmd.ExecuteNonQuery();
                }

                // Afficher le mot de passe par défaut à l'administrateur
                CustomMessageBox.Show(
                    $"L'utilisateur '{textBoxNomUtilisateur.Text}' a été créé avec succès !\n\n" +
                    $"Mot de passe par défaut : {motDePasseParDefaut}\n\n" +
                    "IMPORTANT : L'utilisateur devra changer ce mot de passe lors de sa première connexion.\n" +
                    "Veuillez communiquer ce mot de passe de manière sécurisée.",
                    "Succès", CustomMessageBox.MessageType.Success);

                AuditLogger.LogAdd("Système", "Utilisateur",
                    $"Création utilisateur: {textBoxNomUtilisateur.Text} - {textBoxNomComplet.Text}");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de la création de l'utilisateur :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
            finally
            {
                db.closeConnect();
            }
        }

        private void ModifierUtilisateur()
        {
            try
            {
                db.openConnect();

                // Mettre à jour l'utilisateur
                string updateQuery = @"UPDATE utilisateurs SET
                                     nom_complet = @nomComplet,
                                     email = @email,
                                     telephone = @telephone,
                                     actif = @actif,
                                     date_modification = NOW()
                                     WHERE id = @id";

                MySqlCommand updateCmd = new MySqlCommand(updateQuery, db.getconnection);
                updateCmd.Parameters.AddWithValue("@nomComplet", textBoxNomComplet.Text.Trim());
                updateCmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(textBoxEmail.Text) ? (object)DBNull.Value : textBoxEmail.Text.Trim());
                updateCmd.Parameters.AddWithValue("@telephone", string.IsNullOrWhiteSpace(textBoxTelephone.Text) ? (object)DBNull.Value : textBoxTelephone.Text.Trim());
                updateCmd.Parameters.AddWithValue("@actif", checkBoxActif.Checked);
                updateCmd.Parameters.AddWithValue("@id", utilisateurId.Value);

                updateCmd.ExecuteNonQuery();

                // Supprimer les anciens rôles
                string deleteRolesQuery = "DELETE FROM utilisateur_roles WHERE utilisateur_id = @userId";
                MySqlCommand deleteCmd = new MySqlCommand(deleteRolesQuery, db.getconnection);
                deleteCmd.Parameters.AddWithValue("@userId", utilisateurId.Value);
                deleteCmd.ExecuteNonQuery();

                // Assigner les nouveaux rôles
                foreach (RoleItem role in checkedListBoxRoles.CheckedItems)
                {
                    string roleQuery = "INSERT INTO utilisateur_roles (utilisateur_id, role_id) VALUES (@userId, @roleId)";
                    MySqlCommand roleCmd = new MySqlCommand(roleQuery, db.getconnection);
                    roleCmd.Parameters.AddWithValue("@userId", utilisateurId.Value);
                    roleCmd.Parameters.AddWithValue("@roleId", role.Id);
                    roleCmd.ExecuteNonQuery();
                }

                CustomMessageBox.Show($"L'utilisateur '{textBoxNomUtilisateur.Text}' a été modifié avec succès !",
                    "Succès", CustomMessageBox.MessageType.Success);

                AuditLogger.LogEdit("Système", "Utilisateur",
                    $"Modification utilisateur: {textBoxNomUtilisateur.Text} - {textBoxNomComplet.Text}");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de la modification de l'utilisateur :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
            finally
            {
                db.closeConnect();
            }
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Classe pour stocker les informations de rôle
        private class RoleItem
        {
            public int Id { get; set; }
            public string Nom { get; set; }
            public string Description { get; set; }
            public string DisplayText { get; set; }

            public override string ToString()
            {
                return DisplayText;
            }
        }
    }
}
