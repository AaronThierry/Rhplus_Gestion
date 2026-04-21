using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RH_GRH.Auth.Models;

namespace RH_GRH.Auth
{
    /// <summary>
    /// Service d'authentification des utilisateurs
    /// </summary>
    public class AuthenticationService
    {
        private const int MAX_LOGIN_ATTEMPTS = 5;
        private const int LOCKOUT_DURATION_MINUTES = 30;

        /// <summary>
        /// Authentifie un utilisateur
        /// </summary>
        /// <param name="username">Nom d'utilisateur</param>
        /// <param name="password">Mot de passe</param>
        /// <returns>Tuple (success, user, errorMessage)</returns>
        public (bool Success, User User, string ErrorMessage) Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return (false, null, "Le nom d'utilisateur et le mot de passe sont requis");
            }

            User user = GetUserByUsername(username);

            if (user == null)
            {
                // Log tentative avec utilisateur inexistant
                AuditLogger.Log(null, username, "LOGIN_FAILED", "Authentification",
                    $"Tentative de connexion avec utilisateur inexistant: {username}", LogResultat.FAILURE);

                return (false, null, "Nom d'utilisateur ou mot de passe incorrect");
            }

            // Vérifier si le compte est verrouillé
            if (user.CompteVerrouille)
            {
                AuditLogger.Log(user.Id, user.NomUtilisateur, "LOGIN_LOCKED", "Authentification",
                    "Tentative de connexion sur compte verrouillé", LogResultat.WARNING);

                return (false, null, "Ce compte est temporairement verrouillé. Veuillez contacter l'administrateur.");
            }

            // Vérifier si le compte est actif
            if (!user.Actif)
            {
                AuditLogger.Log(user.Id, user.NomUtilisateur, "LOGIN_INACTIVE", "Authentification",
                    "Tentative de connexion sur compte inactif", LogResultat.WARNING);

                return (false, null, "Ce compte est désactivé. Veuillez contacter l'administrateur.");
            }

            // Vérifier le mot de passe
            if (!PasswordHasher.VerifyPassword(password, user.MotDePasseHash))
            {
                // Incrémenter les tentatives échouées
                IncrementFailedAttempts(user.Id);

                AuditLogger.Log(user.Id, user.NomUtilisateur, "LOGIN_FAILED", "Authentification",
                    $"Mot de passe incorrect (tentative {user.TentativesEchec + 1}/{MAX_LOGIN_ATTEMPTS})", LogResultat.FAILURE);

                if (user.TentativesEchec + 1 >= MAX_LOGIN_ATTEMPTS)
                {
                    LockAccount(user.Id);
                    return (false, null, $"Trop de tentatives échouées. Votre compte a été verrouillé.");
                }

                return (false, null, "Nom d'utilisateur ou mot de passe incorrect");
            }

            // Réinitialiser les tentatives échouées
            ResetFailedAttempts(user.Id);

            // Charger les rôles et permissions
            LoadUserRolesAndPermissions(user);

            // Créer la session
            Session session = SessionManager.Instance.CreateSession(user);

            if (session == null)
            {
                AuditLogger.Log(user.Id, user.NomUtilisateur, "SESSION_CREATE_FAILED", "Authentification",
                    "Échec de création de session", LogResultat.FAILURE);

                return (false, null, "Erreur lors de la création de la session");
            }

            // Log succès
            AuditLogger.Log(user.Id, user.NomUtilisateur, "LOGIN_SUCCESS", "Authentification",
                $"Connexion réussie depuis {session.IpAddress}", LogResultat.SUCCESS);

            return (true, user, string.Empty);
        }

        /// <summary>
        /// Déconnecte l'utilisateur courant
        /// </summary>
        public void Logout()
        {
            if (SessionManager.Instance.IsAuthenticated)
            {
                var user = SessionManager.Instance.CurrentUser;
                AuditLogger.Log(user.Id, user.NomUtilisateur, "LOGOUT", "Authentification",
                    "Déconnexion", LogResultat.SUCCESS);

                SessionManager.Instance.TerminateSession();
            }
        }

        /// <summary>
        /// Récupère un utilisateur par son nom d'utilisateur
        /// </summary>
        private User GetUserByUsername(string username)
        {
            Dbconnect db = new Dbconnect();
            try
            {
                db.openConnect();
                string query = @"SELECT id, nom_utilisateur, mot_de_passe_hash, nom_complet, email, telephone,
                                       actif, date_creation, date_modification, derniere_connexion,
                                       tentatives_echec, compte_verrouille
                                FROM utilisateurs
                                WHERE nom_utilisateur = @username";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@username", username);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    var user = new User
                    {
                        Id = reader.GetInt32("id"),
                        NomUtilisateur = reader.GetString("nom_utilisateur"),
                        MotDePasseHash = reader.GetString("mot_de_passe_hash"),
                        NomComplet = reader.GetString("nom_complet"),
                        Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                        Telephone = reader.IsDBNull(reader.GetOrdinal("telephone")) ? null : reader.GetString("telephone"),
                        Actif = reader.GetBoolean("actif"),
                        DateCreation = reader.GetDateTime("date_creation"),
                        DateModification = reader.GetDateTime("date_modification"),
                        DerniereConnexion = reader.IsDBNull(reader.GetOrdinal("derniere_connexion")) ? null : (DateTime?)reader.GetDateTime("derniere_connexion"),
                        TentativesEchec = reader.GetInt32("tentatives_echec"),
                        CompteVerrouille = reader.GetBoolean("compte_verrouille")
                    };

                    return user;
                }

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AuthenticationService] Erreur récupération utilisateur: {ex.Message}");
                return null;
            }
            finally
            {
                db.closeConnect();
            }
        }

        /// <summary>
        /// Charge les rôles et permissions d'un utilisateur
        /// </summary>
        private void LoadUserRolesAndPermissions(User user)
        {
            Dbconnect db = new Dbconnect();
            try
            {
                db.openConnect();

                // Charger les rôles
                string roleQuery = @"SELECT r.id, r.nom_role, r.description, r.niveau_acces, r.date_creation
                                    FROM roles r
                                    INNER JOIN utilisateur_roles ur ON r.id = ur.role_id
                                    WHERE ur.utilisateur_id = @userId";

                MySqlCommand roleCmd = new MySqlCommand(roleQuery, db.getconnection);
                roleCmd.Parameters.AddWithValue("@userId", user.Id);

                MySqlDataReader roleReader = roleCmd.ExecuteReader();
                var roles = new List<Role>();

                while (roleReader.Read())
                {
                    var role = new Role
                    {
                        Id = roleReader.GetInt32("id"),
                        NomRole = roleReader.GetString("nom_role"),
                        Description = roleReader.IsDBNull(roleReader.GetOrdinal("description")) ? null : roleReader.GetString("description"),
                        NiveauAcces = roleReader.GetInt32("niveau_acces"),
                        DateCreation = roleReader.GetDateTime("date_creation")
                    };

                    roles.Add(role);
                }
                roleReader.Close();

                // Charger les permissions pour chaque rôle
                foreach (var role in roles)
                {
                    string permQuery = @"SELECT p.id, p.nom_permission, p.description, p.module, p.action, p.date_creation
                                        FROM permissions p
                                        INNER JOIN role_permissions rp ON p.id = rp.permission_id
                                        WHERE rp.role_id = @roleId";

                    MySqlCommand permCmd = new MySqlCommand(permQuery, db.getconnection);
                    permCmd.Parameters.AddWithValue("@roleId", role.Id);

                    MySqlDataReader permReader = permCmd.ExecuteReader();

                    while (permReader.Read())
                    {
                        var permission = new Permission
                        {
                            Id = permReader.GetInt32("id"),
                            NomPermission = permReader.GetString("nom_permission"),
                            Description = permReader.IsDBNull(permReader.GetOrdinal("description")) ? null : permReader.GetString("description"),
                            Module = permReader.GetString("module"),
                            Action = permReader.GetString("action"),
                            DateCreation = permReader.GetDateTime("date_creation")
                        };

                        role.Permissions.Add(permission);
                    }
                    permReader.Close();
                }

                user.Roles = roles;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AuthenticationService] Erreur chargement rôles/permissions: {ex.Message}");
            }
            finally
            {
                db.closeConnect();
            }
        }

        /// <summary>
        /// Incrémente le compteur de tentatives échouées
        /// </summary>
        private void IncrementFailedAttempts(int userId)
        {
            Dbconnect db = new Dbconnect();
            try
            {
                db.openConnect();
                string query = @"UPDATE utilisateurs
                               SET tentatives_echec = tentatives_echec + 1
                               WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@id", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AuthenticationService] Erreur incrémentation tentatives: {ex.Message}");
            }
            finally
            {
                db.closeConnect();
            }
        }

        /// <summary>
        /// Réinitialise le compteur de tentatives échouées
        /// </summary>
        private void ResetFailedAttempts(int userId)
        {
            Dbconnect db = new Dbconnect();
            try
            {
                db.openConnect();
                string query = @"UPDATE utilisateurs
                               SET tentatives_echec = 0
                               WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@id", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AuthenticationService] Erreur réinitialisation tentatives: {ex.Message}");
            }
            finally
            {
                db.closeConnect();
            }
        }

        /// <summary>
        /// Verrouille un compte utilisateur
        /// </summary>
        private void LockAccount(int userId)
        {
            Dbconnect db = new Dbconnect();
            try
            {
                db.openConnect();
                string query = @"UPDATE utilisateurs
                               SET compte_verrouille = 1
                               WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@id", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AuthenticationService] Erreur verrouillage compte: {ex.Message}");
            }
            finally
            {
                db.closeConnect();
            }
        }
    }
}
