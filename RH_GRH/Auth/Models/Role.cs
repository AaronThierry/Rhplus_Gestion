using System;
using System.Collections.Generic;

namespace RH_GRH.Auth.Models
{
    /// <summary>
    /// Modèle représentant un rôle utilisateur
    /// </summary>
    public class Role
    {
        public int Id { get; set; }
        public string NomRole { get; set; }
        public string Description { get; set; }
        public int NiveauAcces { get; set; }
        public DateTime DateCreation { get; set; }

        // Navigation properties
        public List<Permission> Permissions { get; set; }

        public Role()
        {
            Permissions = new List<Permission>();
            DateCreation = DateTime.Now;
            NiveauAcces = 1;
        }

        /// <summary>
        /// Retourne une représentation string du rôle
        /// </summary>
        public override string ToString()
        {
            return NomRole;
        }

        /// <summary>
        /// Vérifie si le rôle a une permission spécifique
        /// </summary>
        public bool HasPermission(string permissionName)
        {
            return Permissions?.Exists(p => p.NomPermission.Equals(permissionName, StringComparison.OrdinalIgnoreCase)) ?? false;
        }
    }
}
