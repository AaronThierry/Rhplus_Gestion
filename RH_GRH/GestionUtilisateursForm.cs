using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using RH_GRH.Auth;
using RH_GRH.Auth.Models;

namespace RH_GRH
{
    public partial class GestionUtilisateursForm : Form
    {
        private Dbconnect db = new Dbconnect();

        public GestionUtilisateursForm()
        {
            InitializeComponent();

            this.Load += GestionUtilisateursForm_Load;
        }

        private void GestionUtilisateursForm_Load(object sender, EventArgs e)
        {
            // Vérifier les permissions
            if (!PermissionManager.CheckPermission(PermissionManager.SYSTEM_USERS))
            {
                this.Close();
                return;
            }

            ChargerUtilisateurs();
            ConfigurerDataGridView();
        }

        private void ConfigurerDataGridView()
        {
            dataGridViewUtilisateurs.AutoGenerateColumns = false;
            dataGridViewUtilisateurs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewUtilisateurs.MultiSelect = false;
            dataGridViewUtilisateurs.ReadOnly = true;
            dataGridViewUtilisateurs.AllowUserToAddRows = false;
            dataGridViewUtilisateurs.RowHeadersVisible = false;

            // Appliquer les permissions sur les boutons
            buttonAjouter.Enabled = PermissionManager.IsAdmin();
            buttonModifier.Enabled = PermissionManager.IsAdmin();
            buttonSupprimer.Enabled = PermissionManager.IsAdmin();
            buttonReinitialiserMdp.Enabled = PermissionManager.IsAdmin();
            buttonDeverrouiller.Enabled = PermissionManager.IsAdmin();
        }

        private void ChargerUtilisateurs()
        {
            try
            {
                db.openConnect();
                string query = @"SELECT
                                u.id,
                                u.nom_utilisateur,
                                u.nom_complet,
                                u.email,
                                u.telephone,
                                u.actif,
                                u.compte_verrouille,
                                u.derniere_connexion,
                                GROUP_CONCAT(r.nom_role SEPARATOR ', ') as roles
                            FROM utilisateurs u
                            LEFT JOIN utilisateur_roles ur ON u.id = ur.utilisateur_id
                            LEFT JOIN roles r ON ur.role_id = r.id
                            GROUP BY u.id
                            ORDER BY u.nom_utilisateur";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridViewUtilisateurs.DataSource = dt;

                // Mettre à jour le label du nombre d'utilisateurs
                labelNombreUtilisateurs.Text = $"Total : {dt.Rows.Count} utilisateur(s)";

                AuditLogger.LogView("Système", "Utilisateurs", "Consultation de la liste des utilisateurs");
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement des utilisateurs :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
            finally
            {
                db.closeConnect();
            }
        }

        private void buttonAjouter_Click(object sender, EventArgs e)
        {
            if (!PermissionManager.CheckPermission(PermissionManager.SYSTEM_USERS, true, "ajouter des utilisateurs"))
                return;

            using (AjouterModifierUtilisateurForm form = new AjouterModifierUtilisateurForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ChargerUtilisateurs();
                }
            }
        }

        private void buttonModifier_Click(object sender, EventArgs e)
        {
            if (!PermissionManager.CheckPermission(PermissionManager.SYSTEM_USERS, true, "modifier des utilisateurs"))
                return;

            if (dataGridViewUtilisateurs.SelectedRows.Count == 0)
            {
                CustomMessageBox.Show("Veuillez sélectionner un utilisateur",
                    "Sélection requise", CustomMessageBox.MessageType.Warning);
                return;
            }

            int userId = Convert.ToInt32(dataGridViewUtilisateurs.SelectedRows[0].Cells["colId"].Value);

            using (AjouterModifierUtilisateurForm form = new AjouterModifierUtilisateurForm(userId))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ChargerUtilisateurs();
                }
            }
        }

        private void buttonSupprimer_Click(object sender, EventArgs e)
        {
            if (!PermissionManager.CheckPermission(PermissionManager.SYSTEM_USERS, true, "supprimer des utilisateurs"))
                return;

            if (dataGridViewUtilisateurs.SelectedRows.Count == 0)
            {
                CustomMessageBox.Show("Veuillez sélectionner un utilisateur",
                    "Sélection requise", CustomMessageBox.MessageType.Warning);
                return;
            }

            int userId = Convert.ToInt32(dataGridViewUtilisateurs.SelectedRows[0].Cells["colId"].Value);
            string username = dataGridViewUtilisateurs.SelectedRows[0].Cells["colNomUtilisateur"].Value.ToString();

            // Ne pas supprimer l'utilisateur courant
            if (SessionManager.Instance.CurrentUser.Id == userId)
            {
                CustomMessageBox.Show("Vous ne pouvez pas supprimer votre propre compte",
                    "Action interdite", CustomMessageBox.MessageType.Warning);
                return;
            }

            // Ne pas supprimer l'admin principal
            if (username.ToLower() == "admin")
            {
                CustomMessageBox.Show("Le compte administrateur principal ne peut pas être supprimé",
                    "Action interdite", CustomMessageBox.MessageType.Warning);
                return;
            }

            var result = MessageBox.Show($"Voulez-vous vraiment supprimer l'utilisateur '{username}' ?\n\nCette action est irréversible.",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                SupprimerUtilisateur(userId, username);
            }
        }

        private void SupprimerUtilisateur(int userId, string username)
        {
            try
            {
                db.openConnect();
                string query = "DELETE FROM utilisateurs WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@id", userId);
                cmd.ExecuteNonQuery();

                CustomMessageBox.Show($"L'utilisateur '{username}' a été supprimé avec succès",
                    "Suppression réussie", CustomMessageBox.MessageType.Success);

                AuditLogger.LogDelete("Système", "Utilisateur", $"Suppression utilisateur: {username} (ID: {userId})");

                ChargerUtilisateurs();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de la suppression :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
            finally
            {
                db.closeConnect();
            }
        }

        private void buttonReinitialiserMdp_Click(object sender, EventArgs e)
        {
            if (!PermissionManager.CheckPermission(PermissionManager.SYSTEM_USERS, true, "réinitialiser les mots de passe"))
                return;

            if (dataGridViewUtilisateurs.SelectedRows.Count == 0)
            {
                CustomMessageBox.Show("Veuillez sélectionner un utilisateur",
                    "Sélection requise", CustomMessageBox.MessageType.Warning);
                return;
            }

            int userId = Convert.ToInt32(dataGridViewUtilisateurs.SelectedRows[0].Cells["colId"].Value);
            string username = dataGridViewUtilisateurs.SelectedRows[0].Cells["colNomUtilisateur"].Value.ToString();
            string motDePasseDefaut = Auth.PasswordGenerator.GenerateDefaultPassword();
            var result = MessageBox.Show($"Réinitialiser le mot de passe de '{username}' ?\n\nLe nouveau mot de passe sera : {motDePasseDefaut}\n\nL'utilisateur devra obligatoirement changer ce mot de passe lors de sa prochaine connexion.",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ReinitialiserMotDePasse(userId, username);
            }
        }

        private void ReinitialiserMotDePasse(int userId, string username)
        {
            try
            {
                // Utiliser le générateur de mot de passe par défaut centralisé
                string nouveauMdp = Auth.PasswordGenerator.GenerateDefaultPassword();
                string hash = PasswordHasher.HashPassword(nouveauMdp);

                db.openConnect();
                string query = @"UPDATE utilisateurs
                               SET mot_de_passe_hash = @hash,
                                   tentatives_echec = 0,
                                   compte_verrouille = 0,
                                   premier_connexion = TRUE,
                                   mot_de_passe_par_defaut = @motDePasseDefaut,
                                   date_modification = NOW()
                               WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@hash", hash);
                cmd.Parameters.AddWithValue("@motDePasseDefaut", nouveauMdp);
                cmd.Parameters.AddWithValue("@id", userId);
                cmd.ExecuteNonQuery();

                CustomMessageBox.Show($"Mot de passe réinitialisé avec succès !\n\nUtilisateur: {username}\nNouveau mot de passe: {nouveauMdp}\n\nL'utilisateur devra obligatoirement changer ce mot de passe lors de sa prochaine connexion.",
                    "Réinitialisation réussie", CustomMessageBox.MessageType.Success);

                AuditLogger.Log(SessionManager.Instance.CurrentUser.Id,
                    SessionManager.Instance.CurrentUser.NomUtilisateur,
                    "RESET_PASSWORD", "Système",
                    $"Réinitialisation mot de passe utilisateur: {username} (ID: {userId})",
                    LogResultat.SUCCESS);

                ChargerUtilisateurs();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de la réinitialisation :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
            finally
            {
                db.closeConnect();
            }
        }

        private void buttonDeverrouiller_Click(object sender, EventArgs e)
        {
            if (!PermissionManager.CheckPermission(PermissionManager.SYSTEM_USERS, true, "déverrouiller des comptes"))
                return;

            if (dataGridViewUtilisateurs.SelectedRows.Count == 0)
            {
                CustomMessageBox.Show("Veuillez sélectionner un utilisateur",
                    "Sélection requise", CustomMessageBox.MessageType.Warning);
                return;
            }

            int userId = Convert.ToInt32(dataGridViewUtilisateurs.SelectedRows[0].Cells["colId"].Value);
            string username = dataGridViewUtilisateurs.SelectedRows[0].Cells["colNomUtilisateur"].Value.ToString();
            bool estVerrouille = Convert.ToBoolean(dataGridViewUtilisateurs.SelectedRows[0].Cells["colVerrouille"].Value);

            if (!estVerrouille)
            {
                CustomMessageBox.Show("Ce compte n'est pas verrouillé",
                    "Info", CustomMessageBox.MessageType.Info);
                return;
            }

            DeverrouillerCompte(userId, username);
        }

        private void DeverrouillerCompte(int userId, string username)
        {
            try
            {
                db.openConnect();
                string query = @"UPDATE utilisateurs
                               SET compte_verrouille = 0,
                                   tentatives_echec = 0
                               WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@id", userId);
                cmd.ExecuteNonQuery();

                CustomMessageBox.Show($"Le compte '{username}' a été déverrouillé avec succès",
                    "Déverrouillage réussi", CustomMessageBox.MessageType.Success);

                AuditLogger.Log(SessionManager.Instance.CurrentUser.Id,
                    SessionManager.Instance.CurrentUser.NomUtilisateur,
                    "UNLOCK_ACCOUNT", "Système",
                    $"Déverrouillage compte utilisateur: {username} (ID: {userId})",
                    LogResultat.SUCCESS);

                ChargerUtilisateurs();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du déverrouillage :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
            finally
            {
                db.closeConnect();
            }
        }

        private void buttonActualiser_Click(object sender, EventArgs e)
        {
            ChargerUtilisateurs();
        }

        private void textBoxRecherche_TextChanged(object sender, EventArgs e)
        {
            string recherche = textBoxRecherche.Text.Trim();

            if (string.IsNullOrWhiteSpace(recherche))
            {
                ChargerUtilisateurs();
                return;
            }

            try
            {
                db.openConnect();
                string query = @"SELECT
                                u.id,
                                u.nom_utilisateur,
                                u.nom_complet,
                                u.email,
                                u.telephone,
                                u.actif,
                                u.compte_verrouille,
                                u.derniere_connexion,
                                GROUP_CONCAT(r.nom_role SEPARATOR ', ') as roles
                            FROM utilisateurs u
                            LEFT JOIN utilisateur_roles ur ON u.id = ur.utilisateur_id
                            LEFT JOIN roles r ON ur.role_id = r.id
                            WHERE u.nom_utilisateur LIKE @recherche
                               OR u.nom_complet LIKE @recherche
                               OR u.email LIKE @recherche
                            GROUP BY u.id
                            ORDER BY u.nom_utilisateur";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@recherche", $"%{recherche}%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridViewUtilisateurs.DataSource = dt;
                labelNombreUtilisateurs.Text = $"Total : {dt.Rows.Count} utilisateur(s)";
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de la recherche :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
            finally
            {
                db.closeConnect();
            }
        }
    }
}
