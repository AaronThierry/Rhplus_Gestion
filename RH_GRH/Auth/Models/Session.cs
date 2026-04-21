using System;

namespace RH_GRH.Auth.Models
{
    /// <summary>
    /// Modèle représentant une session utilisateur
    /// </summary>
    public class Session
    {
        public int Id { get; set; }
        public int UtilisateurId { get; set; }
        public string Token { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public bool Actif { get; set; }

        // Navigation property
        public User Utilisateur { get; set; }

        public Session()
        {
            DateDebut = DateTime.Now;
            Actif = true;
            Token = GenerateToken();
        }

        /// <summary>
        /// Génère un token de session unique
        /// </summary>
        private string GenerateToken()
        {
            return Guid.NewGuid().ToString("N") + DateTime.Now.Ticks.ToString("X");
        }

        /// <summary>
        /// Vérifie si la session est toujours valide
        /// </summary>
        public bool IsValid()
        {
            return Actif && !DateFin.HasValue;
        }

        /// <summary>
        /// Termine la session
        /// </summary>
        public void Terminate()
        {
            Actif = false;
            DateFin = DateTime.Now;
        }
    }
}
