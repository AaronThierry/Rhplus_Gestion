using System;

namespace RH_GRH
{
    public class Abonnement
    {
        public int IdAbonnement { get; set; }
        public int IdPersonnel { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public decimal Montant { get; set; }
    }
}
