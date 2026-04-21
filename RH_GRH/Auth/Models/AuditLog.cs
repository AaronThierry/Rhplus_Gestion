using System;

namespace RH_GRH.Auth.Models
{
    /// <summary>
    /// Modèle représentant un log d'activité
    /// </summary>
    public class AuditLog
    {
        public int Id { get; set; }
        public int? UtilisateurId { get; set; }
        public string NomUtilisateur { get; set; }
        public string Action { get; set; }
        public string Module { get; set; }
        public string Details { get; set; }
        public string AncienEtat { get; set; }
        public string NouvelEtat { get; set; }
        public DateTime DateAction { get; set; }
        public string IpAddress { get; set; }
        public LogResultat Resultat { get; set; }

        public AuditLog()
        {
            DateAction = DateTime.Now;
            Resultat = LogResultat.SUCCESS;
        }

        /// <summary>
        /// Retourne une représentation string du log
        /// </summary>
        public override string ToString()
        {
            return $"[{DateAction:yyyy-MM-dd HH:mm:ss}] {NomUtilisateur} - {Module}.{Action}";
        }
    }

    /// <summary>
    /// Enumération des résultats possibles d'une action
    /// </summary>
    public enum LogResultat
    {
        SUCCESS,
        FAILURE,
        WARNING
    }
}
