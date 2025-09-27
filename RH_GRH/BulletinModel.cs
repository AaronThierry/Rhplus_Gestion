using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH_GRH
{
        public class BulletinModel
        {
            public string NomEmploye { get; set; }
            public string Matricule { get; set; }
            public string Poste { get; set; }
            public string Mois { get; set; }
            public double SalaireDeBase { get; set; }
            public decimal HeuresSup { get; set; }
            public decimal CNSS { get; set; }
            public decimal SalaireNet { get; set; }
            public byte[] LogoEntreprise { get; set; }
            public string Sigle { get; set; }
            public string NomEntreprise { get; set; }
            public string AdresseEntreprise { get; set; }
            public string Periode { get; set; }
            public string Civilite { get; set; }
             public string NumeroEmploye { get; set; }
        public DateTime DateNaissance { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string AdresseEmploye { get; set; }

        public string Contrat { get; set; }
        public string Categorie { get; set; }
        public string Service { get; set; }
        public string Direction { get; set; }
        public string NumeroCNSS { get; set; }

        public string Sexe { get; set; }
        public int NbJourHeure { get; set; }
        public string Duree { get; set; }
        public int NombreEnfants { get; set; }
        public string Anciennete { get; set; }
        public string DureeContrat { get; set; }
        public int Charges { get; set; }
        public string TelephoneEntreprise { get; set; }
        public string courrier { get; set; }


    }


}
