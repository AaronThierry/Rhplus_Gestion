using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RH_GRH.Auth.Models;

namespace RH_GRH.Auth
{
    /// <summary>
    /// Service de gestion des rôles et permissions
    /// </summary>
    public class RoleService
    {
        /// <summary>
        /// Récupère tous les rôles
        /// </summary>
        public List<Role> GetAllRoles()
        {
            List<Role> roles = new List<Role>();
            Dbconnect db = new Dbconnect();

            try
            {
                db.openConnect();
                string query = @"SELECT id, nom_role, description, niveau_acces, date_creation
                               FROM roles
                               ORDER BY niveau_acces DESC, nom_role";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Role role = new Role
                    {
                        Id = reader.GetInt32("id"),
                        NomRole = reader.GetString("nom_role"),
                        Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                        NiveauAcces = reader.GetInt32("niveau_acces"),
                        DateCreation = reader.GetDateTime("date_creation")
                    };

                    roles.Add(role);
                }

                reader.Close();

                // Charger les permissions pour chaque rôle
                foreach (var role in roles)
                {
                    role.Permissions = GetRolePermissions(role.Id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la récupération des rôles: {ex.Message}");
            }
            finally
            {
                db.closeConnect();
            }

            return roles;
        }

        /// <summary>
        /// Récupère un rôle par son ID
        /// </summary>
        public Role GetRoleById(int roleId)
        {
            Dbconnect db = new Dbconnect();

            try
            {
                db.openConnect();
                string query = @"SELECT id, nom_role, description, niveau_acces, date_creation
                               FROM roles
                               WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@id", roleId);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Role role = new Role
                    {
                        Id = reader.GetInt32("id"),
                        NomRole = reader.GetString("nom_role"),
                        Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                        NiveauAcces = reader.GetInt32("niveau_acces"),
                        DateCreation = reader.GetDateTime("date_creation")
                    };

                    reader.Close();

                    // Charger les permissions
                    role.Permissions = GetRolePermissions(role.Id);

                    return role;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la récupération du rôle: {ex.Message}");
            }
            finally
            {
                db.closeConnect();
            }
        }

        /// <summary>
        /// Récupère les permissions d'un rôle
        /// </summary>
        public List<Permission> GetRolePermissions(int roleId)
        {
            List<Permission> permissions = new List<Permission>();
            Dbconnect db = new Dbconnect();

            try
            {
                db.openConnect();
                string query = @"SELECT p.id, p.nom_permission, p.description, p.module, p.action, p.date_creation
                               FROM permissions p
                               INNER JOIN role_permissions rp ON p.id = rp.permission_id
                               WHERE rp.role_id = @roleId
                               ORDER BY p.module, p.action";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@roleId", roleId);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Permission permission = new Permission
                    {
                        Id = reader.GetInt32("id"),
                        NomPermission = reader.GetString("nom_permission"),
                        Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                        Module = reader.GetString("module"),
                        Action = reader.GetString("action"),
                        DateCreation = reader.GetDateTime("date_creation")
                    };

                    permissions.Add(permission);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur GetRolePermissions: {ex.Message}");
            }
            finally
            {
                db.closeConnect();
            }

            return permissions;
        }

        /// <summary>
        /// Récupère toutes les permissions disponibles
        /// </summary>
        public List<Permission> GetAllPermissions()
        {
            List<Permission> permissions = new List<Permission>();
            Dbconnect db = new Dbconnect();

            try
            {
                db.openConnect();
                string query = @"SELECT id, nom_permission, description, module, action, date_creation
                               FROM permissions
                               ORDER BY module, action";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Permission permission = new Permission
                    {
                        Id = reader.GetInt32("id"),
                        NomPermission = reader.GetString("nom_permission"),
                        Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                        Module = reader.GetString("module"),
                        Action = reader.GetString("action"),
                        DateCreation = reader.GetDateTime("date_creation")
                    };

                    permissions.Add(permission);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la récupération des permissions: {ex.Message}");
            }
            finally
            {
                db.closeConnect();
            }

            return permissions;
        }

        /// <summary>
        /// Crée un nouveau rôle
        /// </summary>
        public bool CreateRole(Role role, List<int> permissionIds)
        {
            Dbconnect db = new Dbconnect();

            try
            {
                db.openConnect();

                // Vérifier si le nom existe déjà
                if (RoleNameExists(role.NomRole))
                {
                    throw new Exception("Un rôle avec ce nom existe déjà.");
                }

                // Insérer le rôle
                string query = @"INSERT INTO roles (nom_role, description, niveau_acces, date_creation)
                               VALUES (@nom, @description, @niveau, @date)";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@nom", role.NomRole);
                cmd.Parameters.AddWithValue("@description", role.Description ?? "");
                cmd.Parameters.AddWithValue("@niveau", role.NiveauAcces);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);

                cmd.ExecuteNonQuery();
                int roleId = (int)cmd.LastInsertedId;

                // Ajouter les permissions
                if (permissionIds != null && permissionIds.Count > 0)
                {
                    AssignPermissionsToRole(roleId, permissionIds);
                }

                // Log de l'action
                var user = SessionManager.Instance.CurrentUser;
                AuditLogger.Log(user?.Id, user?.NomUtilisateur, "Création de rôle", "Rôles", $"Rôle créé: {role.NomRole}");

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la création du rôle: {ex.Message}");
            }
            finally
            {
                db.closeConnect();
            }
        }

        /// <summary>
        /// Met à jour un rôle existant
        /// </summary>
        public bool UpdateRole(Role role, List<int> permissionIds)
        {
            Dbconnect db = new Dbconnect();

            try
            {
                db.openConnect();

                // Vérifier si le nom existe déjà (sauf pour ce rôle)
                if (RoleNameExistsExcept(role.NomRole, role.Id))
                {
                    throw new Exception("Un autre rôle avec ce nom existe déjà.");
                }

                // Mettre à jour le rôle
                string query = @"UPDATE roles
                               SET nom_role = @nom,
                                   description = @description,
                                   niveau_acces = @niveau
                               WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@nom", role.NomRole);
                cmd.Parameters.AddWithValue("@description", role.Description ?? "");
                cmd.Parameters.AddWithValue("@niveau", role.NiveauAcces);
                cmd.Parameters.AddWithValue("@id", role.Id);

                cmd.ExecuteNonQuery();

                // Supprimer les anciennes permissions
                RemoveAllRolePermissions(role.Id);

                // Ajouter les nouvelles permissions
                if (permissionIds != null && permissionIds.Count > 0)
                {
                    AssignPermissionsToRole(role.Id, permissionIds);
                }

                // Log de l'action
                var user = SessionManager.Instance.CurrentUser;
                AuditLogger.Log(user?.Id, user?.NomUtilisateur, "Modification de rôle", "Rôles", $"Rôle modifié: {role.NomRole}");

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la mise à jour du rôle: {ex.Message}");
            }
            finally
            {
                db.closeConnect();
            }
        }

        /// <summary>
        /// Supprime un rôle
        /// </summary>
        public bool DeleteRole(int roleId)
        {
            Dbconnect db = new Dbconnect();

            try
            {
                db.openConnect();

                // Vérifier si le rôle est utilisé
                if (IsRoleInUse(roleId))
                {
                    throw new Exception("Ce rôle est assigné à des utilisateurs et ne peut pas être supprimé.");
                }

                // Supprimer les permissions du rôle
                RemoveAllRolePermissions(roleId);

                // Supprimer le rôle
                string query = "DELETE FROM roles WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@id", roleId);

                int affected = cmd.ExecuteNonQuery();

                // Log de l'action
                var user = SessionManager.Instance.CurrentUser;
                AuditLogger.Log(user?.Id, user?.NomUtilisateur, "Suppression de rôle", "Rôles", $"Rôle supprimé ID: {roleId}");

                return affected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la suppression du rôle: {ex.Message}");
            }
            finally
            {
                db.closeConnect();
            }
        }

        /// <summary>
        /// Assigne des permissions à un rôle
        /// </summary>
        private void AssignPermissionsToRole(int roleId, List<int> permissionIds)
        {
            Dbconnect db = new Dbconnect();

            try
            {
                db.openConnect();

                foreach (int permissionId in permissionIds)
                {
                    string query = @"INSERT INTO role_permissions (role_id, permission_id, date_attribution)
                                   VALUES (@roleId, @permissionId, @date)";

                    MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                    cmd.Parameters.AddWithValue("@roleId", roleId);
                    cmd.Parameters.AddWithValue("@permissionId", permissionId);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                db.closeConnect();
            }
        }

        /// <summary>
        /// Supprime toutes les permissions d'un rôle
        /// </summary>
        private void RemoveAllRolePermissions(int roleId)
        {
            Dbconnect db = new Dbconnect();

            try
            {
                db.openConnect();
                string query = "DELETE FROM role_permissions WHERE role_id = @roleId";
                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@roleId", roleId);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                db.closeConnect();
            }
        }

        /// <summary>
        /// Vérifie si un nom de rôle existe déjà
        /// </summary>
        private bool RoleNameExists(string nomRole)
        {
            Dbconnect db = new Dbconnect();

            try
            {
                db.openConnect();
                string query = "SELECT COUNT(*) FROM roles WHERE nom_role = @nom";
                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@nom", nomRole);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            finally
            {
                db.closeConnect();
            }
        }

        /// <summary>
        /// Vérifie si un nom de rôle existe déjà (sauf pour un ID spécifique)
        /// </summary>
        private bool RoleNameExistsExcept(string nomRole, int exceptId)
        {
            Dbconnect db = new Dbconnect();

            try
            {
                db.openConnect();
                string query = "SELECT COUNT(*) FROM roles WHERE nom_role = @nom AND id != @id";
                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@nom", nomRole);
                cmd.Parameters.AddWithValue("@id", exceptId);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            finally
            {
                db.closeConnect();
            }
        }

        /// <summary>
        /// Vérifie si un rôle est assigné à des utilisateurs
        /// </summary>
        private bool IsRoleInUse(int roleId)
        {
            Dbconnect db = new Dbconnect();

            try
            {
                db.openConnect();
                string query = "SELECT COUNT(*) FROM utilisateur_roles WHERE role_id = @roleId";
                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@roleId", roleId);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            finally
            {
                db.closeConnect();
            }
        }

        /// <summary>
        /// Initialise les permissions par défaut si elles n'existent pas
        /// </summary>
        public void InitializeDefaultPermissions()
        {
            Dbconnect db = new Dbconnect();

            try
            {
                db.openConnect();

                // Vérifier si des permissions existent déjà
                string checkQuery = "SELECT COUNT(*) FROM permissions";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, db.getconnection);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    return; // Les permissions existent déjà
                }

                // Créer les permissions par défaut
                var defaultPermissions = new List<(string nom, string description, string module, string action)>
                {
                    // Administration
                    ("Administration.Entreprise", "Gérer les entreprises", "Administration", "Entreprise"),
                    ("Administration.Direction", "Gérer les directions", "Administration", "Direction"),
                    ("Administration.Service", "Gérer les services", "Administration", "Service"),
                    ("Administration.Categorie", "Gérer les catégories", "Administration", "Categorie"),

                    // Gestion
                    ("Gestion.Personnel", "Accéder à la section Personnel", "Gestion", "Personnel"),
                    ("Gestion.Salaire", "Accéder à la section Salaire", "Gestion", "Salaire"),

                    // Personnel
                    ("Personnel.Employes", "Gérer les employés", "Personnel", "Employes"),
                    ("Personnel.Charges", "Gérer les charges familiales", "Personnel", "Charges"),
                    ("Personnel.Indemnites", "Gérer les indemnités", "Personnel", "Indemnites"),

                    // Salaire
                    ("Salaire.Sursalaires", "Gérer les sursalaires", "Salaire", "Sursalaires"),
                    ("Salaire.Horaires", "Gérer les horaires", "Salaire", "Horaires"),
                    ("Salaire.Journalier", "Gérer le journalier", "Salaire", "Journalier")
                };

                foreach (var perm in defaultPermissions)
                {
                    string insertQuery = @"INSERT INTO permissions (nom_permission, description, module, action, date_creation)
                                         VALUES (@nom, @description, @module, @action, @date)";

                    MySqlCommand cmd = new MySqlCommand(insertQuery, db.getconnection);
                    cmd.Parameters.AddWithValue("@nom", perm.nom);
                    cmd.Parameters.AddWithValue("@description", perm.description);
                    cmd.Parameters.AddWithValue("@module", perm.module);
                    cmd.Parameters.AddWithValue("@action", perm.action);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }

                System.Diagnostics.Debug.WriteLine("Permissions par défaut initialisées avec succès");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur InitializeDefaultPermissions: {ex.Message}");
            }
            finally
            {
                db.closeConnect();
            }
        }
    }
}
