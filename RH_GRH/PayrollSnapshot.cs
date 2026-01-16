using System;

namespace RH_GRH
{
    public sealed class PayrollSnapshot
    {
        // Identifiants
        public int IdEntreprise { get; set; }
        public int IdEmploye { get; set; }
        public string AncienneteStr { get; set; } = "";

        //Salaire Base
        public decimal BaseUnitaire { get; set; }
        public decimal SalaireBase { get; set; }
        public decimal TauxSalaireDeBase { get; set; }

        //HEURES SUPPLEMENTAIRES
        public decimal PrimeHeuressupp { get; set; }
        public decimal TauxHeureSupp { get; set; }

        //PRIME ANCIENNETE
        public decimal PrimeAnciennete { get; set; }

        //Infos Employe
        public string NomPrenom { get; set; } = "";
        public string Civilite { get; set; } = "";
        public string Poste { get; set; } = "";
        public string Matricule { get; set; } = "";
        public string NumeroEmploye { get; set; } = "";
        public string AdresseEmploye { get; set; } = "";
        public string PeriodeSalaire { get; set; } = "";
        public string Contrat { get; set; } = "";
        public string Sexe { get; set; } = "";
        public string DureeContrat { get; set; } = "";
        public int HeureContrat { get; set; }

        public string Sigle { get; set; } = "";
        public string NomEntreprise { get; set; } = "";
        public string TelephoneEntreprise { get; set; } = "";
        public string EmailEntreprise { get; set; } = "";
        public string AdressePhysiqueEntreprise { get; set; } = "";
        public string AdressePostaleEntreprise { get; set; } = "";
        public string ResponsableEntreprise { get; set; } = "";

        public DateTime DateNaissance { get; set; }
        public DateTime DateEntree { get; set; }
        public DateTime? DateSortie { get; set; }

        // Composantes de gains
        public decimal HeuresSupp { get; set; }
        public decimal IndemNum { get; set; }
        public decimal IndemNat { get; set; }

        // Totaux bruts / sociaux
        public decimal SalaireBrut { get; set; }
        public decimal SalaireBrutSocial { get; set; }

        // CNSS & TPA (employé / employeur)
        public decimal CNSS_Employe { get; set; }
        public decimal PensionEmployeur { get; set; }
        public decimal RisqueProEmployeur { get; set; }
        public decimal PFEmployeur { get; set; }
        public decimal CNSS_Employeur_Total { get; set; }
        public decimal TPA { get; set; }

        // IUTS
        public decimal DeductibiliteIndemnites { get; set; }
        public decimal BaseIUTS { get; set; }
        public decimal BaseIUTS_Arrondie { get; set; }
        public int NombreCharges { get; set; }
        public decimal IUTS_Brut { get; set; }
        public decimal IUTS_Final { get; set; }

        // Net
        public decimal SalaireNet { get; set; }
        public decimal EffortPaix { get; set; }
        public decimal SalaireNetaPayer { get; set; }
        public decimal ValeurDette { get; set; }
        public decimal SalaireNetaPayerFinal { get; set; }

        // Méta (facultatif)
        public string Categorie { get; set; } = "";
        public string Direction { get; set; } = "";
        public string Service { get; set; } = "";
        public string NumeroCnssEmploye { get; set; } = "";
        public decimal TauxTPA { get; set; }
        public string StatutCadre { get; set; } = ""; // "oui"/"non"
    }
}
