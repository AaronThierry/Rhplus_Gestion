using System;

namespace RH_GRH.Auth.Models
{
    /// <summary>
    /// Modèle représentant une permission système
    /// </summary>
    public class Permission
    {
        public int Id { get; set; }
        public string NomPermission { get; set; }
        public string Description { get; set; }
        public string Module { get; set; }
        public string Action { get; set; }
        public DateTime DateCreation { get; set; }

        public Permission()
        {
            DateCreation = DateTime.Now;
        }

        /// <summary>
        /// Retourne une représentation string de la permission
        /// </summary>
        public override string ToString()
        {
            return $"{Module}.{Action}";
        }

        /// <summary>
        /// Vérifie si la permission correspond à un module et une action
        /// </summary>
        public bool Matches(string module, string action)
        {
            return Module.Equals(module, StringComparison.OrdinalIgnoreCase) &&
                   Action.Equals(action, StringComparison.OrdinalIgnoreCase);
        }
    }
}
