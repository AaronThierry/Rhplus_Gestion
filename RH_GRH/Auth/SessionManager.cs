using System;
using System.Net;
using System.Net.Sockets;
using MySql.Data.MySqlClient;
using RH_GRH.Auth.Models;

namespace RH_GRH.Auth
{
    /// <summary>
    /// Gestionnaire de sessions utilisateur
    /// </summary>
    public class SessionManager
    {
        private static SessionManager _instance;
        private static readonly object _lock = new object();

        private Session _currentSession;
        private User _currentUser;

        private SessionManager() { }

        /// <summary>
        /// Instance singleton du gestionnaire de sessions
        /// </summary>
        public static SessionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SessionManager();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Session courante
        /// </summary>
        public Session CurrentSession => _currentSession;

        /// <summary>
        /// Utilisateur courant
        /// </summary>
        public User CurrentUser => _currentUser;

        /// <summary>
        /// Vérifie si un utilisateur est connecté
        /// </summary>
        public bool IsAuthenticated => _currentSession != null && _currentSession.IsValid() && _currentUser != null;

        /// <summary>
        /// Crée une nouvelle session pour un utilisateur
        /// </summary>
        public Session CreateSession(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Terminer la session précédente si elle existe
            if (_currentSession != null)
            {
                TerminateSession();
            }

            var session = new Session
            {
                UtilisateurId = user.Id,
                IpAddress = GetLocalIPAddress(),
                UserAgent = Environment.MachineName,
                Utilisateur = user
            };

            // Enregistrer la session en base de données
            if (SaveSessionToDatabase(session))
            {
                _currentSession = session;
                _currentUser = user;

                // Mettre à jour la dernière connexion
                UpdateLastLogin(user.Id);

                return session;
            }

            return null;
        }

        /// <summary>
        /// Termine la session courante
        /// </summary>
        public void TerminateSession()
        {
            if (_currentSession != null)
            {
                _currentSession.Terminate();
                UpdateSessionInDatabase(_currentSession);

                _currentSession = null;
                _currentUser = null;
            }
        }

        /// <summary>
        /// Récupère l'adresse IP locale
        /// </summary>
        private string GetLocalIPAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                return "127.0.0.1";
            }
            catch
            {
                return "Unknown";
            }
        }

        /// <summary>
        /// Enregistre une session en base de données
        /// </summary>
        private bool SaveSessionToDatabase(Session session)
        {
            Dbconnect db = new Dbconnect();
            try
            {
                db.openConnect();
                string query = @"INSERT INTO sessions (utilisateur_id, token, date_debut, ip_address, user_agent, actif)
                               VALUES (@userId, @token, @dateDebut, @ip, @userAgent, @actif)";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@userId", session.UtilisateurId);
                cmd.Parameters.AddWithValue("@token", session.Token);
                cmd.Parameters.AddWithValue("@dateDebut", session.DateDebut);
                cmd.Parameters.AddWithValue("@ip", session.IpAddress);
                cmd.Parameters.AddWithValue("@userAgent", session.UserAgent);
                cmd.Parameters.AddWithValue("@actif", session.Actif);

                cmd.ExecuteNonQuery();
                session.Id = (int)cmd.LastInsertedId;

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SessionManager] Erreur création session: {ex.Message}");
                return false;
            }
            finally
            {
                db.closeConnect();
            }
        }

        /// <summary>
        /// Met à jour une session en base de données
        /// </summary>
        private void UpdateSessionInDatabase(Session session)
        {
            Dbconnect db = new Dbconnect();
            try
            {
                db.openConnect();
                string query = @"UPDATE sessions
                               SET date_fin = @dateFin, actif = @actif
                               WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@dateFin", session.DateFin);
                cmd.Parameters.AddWithValue("@actif", session.Actif);
                cmd.Parameters.AddWithValue("@id", session.Id);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SessionManager] Erreur mise à jour session: {ex.Message}");
            }
            finally
            {
                db.closeConnect();
            }
        }

        /// <summary>
        /// Met à jour la date de dernière connexion d'un utilisateur
        /// </summary>
        private void UpdateLastLogin(int userId)
        {
            Dbconnect db = new Dbconnect();
            try
            {
                db.openConnect();
                string query = @"UPDATE utilisateurs
                               SET derniere_connexion = @now
                               WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@now", DateTime.Now);
                cmd.Parameters.AddWithValue("@id", userId);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SessionManager] Erreur mise à jour dernière connexion: {ex.Message}");
            }
            finally
            {
                db.closeConnect();
            }
        }

        /// <summary>
        /// Vérifie si l'utilisateur courant a une permission spécifique
        /// </summary>
        public bool HasPermission(string permissionName)
        {
            if (!IsAuthenticated || _currentUser == null)
            {
                return false;
            }

            foreach (var role in _currentUser.Roles)
            {
                if (role.HasPermission(permissionName))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Vérifie si l'utilisateur courant a un rôle spécifique
        /// </summary>
        public bool HasRole(string roleName)
        {
            return IsAuthenticated && _currentUser != null && _currentUser.HasRole(roleName);
        }
    }
}
