using System;
using System.Net;
using System.Net.Sockets;
using MySql.Data.MySqlClient;
using RH_GRH.Auth.Models;

namespace RH_GRH.Auth
{
    /// <summary>
    /// Service de journalisation des activités (Audit Trail)
    /// </summary>
    public static class AuditLogger
    {
        /// <summary>
        /// Enregistre une action dans les logs
        /// </summary>
        /// <param name="utilisateurId">ID de l'utilisateur (null pour actions système)</param>
        /// <param name="nomUtilisateur">Nom de l'utilisateur</param>
        /// <param name="action">Action effectuée</param>
        /// <param name="module">Module concerné</param>
        /// <param name="details">Détails de l'action</param>
        /// <param name="resultat">Résultat de l'action</param>
        public static void Log(int? utilisateurId, string nomUtilisateur, string action, string module,
            string details, LogResultat resultat = LogResultat.SUCCESS)
        {
            Log(utilisateurId, nomUtilisateur, action, module, details, null, null, resultat);
        }

        /// <summary>
        /// Enregistre une action avec l'ancien et le nouvel état
        /// </summary>
        public static void Log(int? utilisateurId, string nomUtilisateur, string action, string module,
            string details, string ancienEtat, string nouvelEtat, LogResultat resultat = LogResultat.SUCCESS)
        {
            try
            {
                Dbconnect db = new Dbconnect();
                db.openConnect();

                string query = @"INSERT INTO logs_activite
                                (utilisateur_id, nom_utilisateur, action, module, details, ancien_etat, nouvel_etat, ip_address, resultat)
                                VALUES (@userId, @username, @action, @module, @details, @oldState, @newState, @ip, @result)";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);

                if (utilisateurId.HasValue)
                    cmd.Parameters.AddWithValue("@userId", utilisateurId.Value);
                else
                    cmd.Parameters.AddWithValue("@userId", DBNull.Value);

                cmd.Parameters.AddWithValue("@username", nomUtilisateur ?? "SYSTEM");
                cmd.Parameters.AddWithValue("@action", action);
                cmd.Parameters.AddWithValue("@module", module);
                cmd.Parameters.AddWithValue("@details", details ?? string.Empty);
                cmd.Parameters.AddWithValue("@oldState", ancienEtat ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@newState", nouvelEtat ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ip", GetLocalIPAddress());
                cmd.Parameters.AddWithValue("@result", resultat.ToString());

                cmd.ExecuteNonQuery();
                db.closeConnect();

                // Debug output
                System.Diagnostics.Debug.WriteLine($"[AUDIT] {nomUtilisateur} - {module}.{action} - {resultat}");
            }
            catch (Exception ex)
            {
                // Ne pas bloquer l'application si le log échoue
                System.Diagnostics.Debug.WriteLine($"[AuditLogger] Erreur enregistrement log: {ex.Message}");
            }
        }

        /// <summary>
        /// Log une action de l'utilisateur courant
        /// </summary>
        public static void LogCurrentUser(string action, string module, string details, LogResultat resultat = LogResultat.SUCCESS)
        {
            var session = SessionManager.Instance.CurrentSession;
            var user = SessionManager.Instance.CurrentUser;

            if (session != null && user != null)
            {
                Log(user.Id, user.NomUtilisateur, action, module, details, resultat);
            }
            else
            {
                Log(null, "ANONYMOUS", action, module, details, resultat);
            }
        }

        /// <summary>
        /// Log une modification avec ancien et nouvel état
        /// </summary>
        public static void LogModification(string action, string module, string details,
            object oldState, object newState, LogResultat resultat = LogResultat.SUCCESS)
        {
            var user = SessionManager.Instance.CurrentUser;

            string oldStateStr = oldState != null ? Newtonsoft.Json.JsonConvert.SerializeObject(oldState) : null;
            string newStateStr = newState != null ? Newtonsoft.Json.JsonConvert.SerializeObject(newState) : null;

            if (user != null)
            {
                Log(user.Id, user.NomUtilisateur, action, module, details, oldStateStr, newStateStr, resultat);
            }
            else
            {
                Log(null, "SYSTEM", action, module, details, oldStateStr, newStateStr, resultat);
            }
        }

        /// <summary>
        /// Log un ajout
        /// </summary>
        public static void LogAdd(string module, string entityName, string details)
        {
            LogCurrentUser($"ADD_{entityName.ToUpper()}", module, details, LogResultat.SUCCESS);
        }

        /// <summary>
        /// Log une modification
        /// </summary>
        public static void LogEdit(string module, string entityName, string details)
        {
            LogCurrentUser($"EDIT_{entityName.ToUpper()}", module, details, LogResultat.SUCCESS);
        }

        /// <summary>
        /// Log une suppression
        /// </summary>
        public static void LogDelete(string module, string entityName, string details)
        {
            LogCurrentUser($"DELETE_{entityName.ToUpper()}", module, details, LogResultat.SUCCESS);
        }

        /// <summary>
        /// Log une consultation
        /// </summary>
        public static void LogView(string module, string entityName, string details)
        {
            LogCurrentUser($"VIEW_{entityName.ToUpper()}", module, details, LogResultat.SUCCESS);
        }

        /// <summary>
        /// Log une exportation
        /// </summary>
        public static void LogExport(string module, string format, string details)
        {
            LogCurrentUser($"EXPORT_{format.ToUpper()}", module, details, LogResultat.SUCCESS);
        }

        /// <summary>
        /// Log une importation
        /// </summary>
        public static void LogImport(string module, string format, string details)
        {
            LogCurrentUser($"IMPORT_{format.ToUpper()}", module, details, LogResultat.SUCCESS);
        }

        /// <summary>
        /// Récupère l'adresse IP locale
        /// </summary>
        private static string GetLocalIPAddress()
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
    }
}
