using System;
using System.Collections.Generic;

namespace RH_GRH.Auth.Models
{
    /// <summary>
    /// Modèle représentant un utilisateur du système
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string NomUtilisateur { get; set; }
        public string MotDePasseHash { get; set; }
        public string NomComplet { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public bool Actif { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }
        public DateTime? DerniereConnexion { get; set; }
        public int TentativesEchec { get; set; }
        public bool CompteVerrouille { get; set; }

        // Navigation properties
        public List<Role> Roles { get; set; }

        public User()
        {
            Roles = new List<Role>();
            Actif = true;
            TentativesEchec = 0;
            CompteVerrouille = false;
            DateCreation = DateTime.Now;
            DateModification = DateTime.Now;
        }

        /// <summary>
        /// Retourne une représentation string de l'utilisateur
        /// </summary>
        public override string ToString()
        {
            return $"{NomComplet} ({NomUtilisateur})";
        }

        /// <summary>
        /// Vérifie si l'utilisateur a un rôle spécifique
        /// </summary>
        public bool HasRole(string roleName)
        {
            return Roles?.Exists(r => r.NomRole.Equals(roleName, StringComparison.OrdinalIgnoreCase)) ?? false;
        }

        /// <summary>
        /// Vérifie si l'utilisateur est administrateur
        /// Accepte plusieurs noms de rôles administrateur
        /// </summary>
        public bool IsAdmin()
        {
            // Vérifier si l'utilisateur a un rôle d'administrateur
            // (compatible avec ancien système et nouveau système de rôles)
            return HasRole("Super Administrateur") ||
                   HasRole("Administrateur") ||
                   HasRole("Administrateur RH");
        }

        /// <summary>
        /// Vérifie si l'utilisateur est Super Administrateur
        /// Seul le Super Administrateur a accès à la gestion des utilisateurs, logs et rôles
        /// </summary>
        public bool IsSuperAdmin()
        {
            return HasRole("Super Administrateur");
        }
    }
}
