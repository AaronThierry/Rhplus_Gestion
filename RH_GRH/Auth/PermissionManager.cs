using System;
using System.Windows.Forms;
using RH_GRH.Auth.Models;

namespace RH_GRH.Auth
{
    /// <summary>
    /// Gestionnaire de permissions et d'autorisation
    /// </summary>
    public static class PermissionManager
    {
        /// <summary>
        /// Constantes de permissions - Module Personnel
        /// </summary>
        public const string PERSONNEL_VIEW = "PERSONNEL_VIEW";
        public const string PERSONNEL_ADD = "PERSONNEL_ADD";
        public const string PERSONNEL_EDIT = "PERSONNEL_EDIT";
        public const string PERSONNEL_DELETE = "PERSONNEL_DELETE";
        public const string PERSONNEL_IMPORT = "PERSONNEL_IMPORT";

        /// <summary>
        /// Constantes de permissions - Module Salaire
        /// </summary>
        public const string SALAIRE_VIEW = "SALAIRE_VIEW";
        public const string SALAIRE_PROCESS = "SALAIRE_PROCESS";
        public const string SALAIRE_EDIT = "SALAIRE_EDIT";
        public const string SALAIRE_EXPORT = "SALAIRE_EXPORT";

        /// <summary>
        /// Constantes de permissions - Module Administration
        /// </summary>
        public const string ADMIN_ENTREPRISE = "ADMIN_ENTREPRISE";
        public const string ADMIN_CATEGORIES = "ADMIN_CATEGORIES";
        public const string ADMIN_SERVICES = "ADMIN_SERVICES";
        public const string ADMIN_DIRECTIONS = "ADMIN_DIRECTIONS";
        public const string ADMIN_CHARGES = "ADMIN_CHARGES";
        public const string ADMIN_INDEMNITES = "ADMIN_INDEMNITES";

        /// <summary>
        /// Constantes de permissions - Module Système
        /// </summary>
        public const string SYSTEM_USERS = "SYSTEM_USERS";
        public const string SYSTEM_ROLES = "SYSTEM_ROLES";
        public const string SYSTEM_LOGS = "SYSTEM_LOGS";
        public const string SYSTEM_CONFIG = "SYSTEM_CONFIG";

        /// <summary>
        /// Vérifie si l'utilisateur courant a une permission
        /// </summary>
        public static bool HasPermission(string permissionName)
        {
            return SessionManager.Instance.HasPermission(permissionName);
        }

        /// <summary>
        /// Vérifie si l'utilisateur courant a un rôle
        /// </summary>
        public static bool HasRole(string roleName)
        {
            return SessionManager.Instance.HasRole(roleName);
        }

        /// <summary>
        /// Vérifie si l'utilisateur courant est administrateur
        /// Accepte plusieurs noms de rôles administrateur
        /// </summary>
        public static bool IsAdmin()
        {
            // Vérifier si l'utilisateur a un rôle d'administrateur
            return HasRole("Super Administrateur") ||
                   HasRole("Administrateur") ||
                   HasRole("Administrateur RH");
        }

        /// <summary>
        /// Requiert une permission - Lance une exception si non autorisé
        /// </summary>
        public static void RequirePermission(string permissionName, string actionDescription = null)
        {
            if (!SessionManager.Instance.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("Vous devez être connecté pour effectuer cette action");
            }

            if (!HasPermission(permissionName))
            {
                string message = actionDescription != null
                    ? $"Vous n'avez pas les droits nécessaires pour {actionDescription}"
                    : "Vous n'avez pas les droits nécessaires pour effectuer cette action";

                AuditLogger.LogCurrentUser("ACCESS_DENIED", "Sécurité",
                    $"Tentative d'accès non autorisé: {permissionName}", LogResultat.WARNING);

                throw new UnauthorizedAccessException(message);
            }
        }

        /// <summary>
        /// Vérifie une permission et affiche un message si refusé
        /// </summary>
        public static bool CheckPermission(string permissionName, bool showMessage = true, string actionDescription = null)
        {
            if (!SessionManager.Instance.IsAuthenticated)
            {
                if (showMessage)
                {
                    CustomMessageBox.Show("Vous devez être connecté pour effectuer cette action",
                        "Non authentifié", CustomMessageBox.MessageType.Warning);
                }
                return false;
            }

            if (!HasPermission(permissionName))
            {
                if (showMessage)
                {
                    string message = actionDescription != null
                        ? $"Vous n'avez pas les droits nécessaires pour {actionDescription}"
                        : "Vous n'avez pas les droits nécessaires pour effectuer cette action";

                    CustomMessageBox.Show(message, "Accès refusé", CustomMessageBox.MessageType.Warning);

                    AuditLogger.LogCurrentUser("ACCESS_DENIED", "Sécurité",
                        $"Tentative d'accès non autorisé: {permissionName}", LogResultat.WARNING);
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Active ou désactive un contrôle en fonction d'une permission
        /// </summary>
        public static void SetControlPermission(Control control, string permissionName)
        {
            if (control != null)
            {
                control.Enabled = HasPermission(permissionName);

                // Optionnel : ajouter un tooltip pour expliquer pourquoi c'est désactivé
                if (!control.Enabled && control is Button)
                {
                    ToolTip tooltip = new ToolTip();
                    tooltip.SetToolTip(control, "Vous n'avez pas les droits pour cette action");
                }
            }
        }

        /// <summary>
        /// Masque ou affiche un contrôle en fonction d'une permission
        /// </summary>
        public static void SetControlVisibility(Control control, string permissionName)
        {
            if (control != null)
            {
                control.Visible = HasPermission(permissionName);
            }
        }

        /// <summary>
        /// Configure les permissions d'un formulaire
        /// </summary>
        public static void ApplyFormPermissions(Form form, string moduleName)
        {
            if (!SessionManager.Instance.IsAuthenticated)
            {
                form.Close();
                return;
            }

            // Log de l'ouverture du formulaire
            AuditLogger.LogCurrentUser("FORM_OPEN", moduleName,
                $"Ouverture du formulaire {form.Name}", LogResultat.SUCCESS);

            // L'implémentation spécifique des permissions doit être faite dans chaque formulaire
        }

        /// <summary>
        /// Journalise une action sécurisée
        /// </summary>
        public static void LogSecuredAction(string action, string module, string details, LogResultat resultat = LogResultat.SUCCESS)
        {
            if (SessionManager.Instance.IsAuthenticated)
            {
                AuditLogger.LogCurrentUser(action, module, details, resultat);
            }
        }
    }
}
