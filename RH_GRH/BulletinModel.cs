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
            public decimal HeuresSup { get; set; }
            public decimal CNSS { get; set; }
            public byte[] LogoEntreprise { get; set; }


            public string Sigle { get; set; }
            public string NomEntreprise { get; set; }
             public string TelephoneEntreprise { get; set; }
            public string EmailEntreprise { get; set; }
            public string AdressePhysiqueEntreprise { get; set; }
            public string AdressePostaleEntreprise { get; set; }

        //SALAIRE DE BASE
            public double baseUnitaire { get; set; }
            public double SalaireDeBase { get; set; }
            public double TauxSalaireDeBase { get; set; }



        //PRIME HEURES SUPPLEMENTAIRES
             public double PrimeHeureSupp { get; set; }
             public double TauxHeureSupp { get; set; }




        //PRIME ANCIENNETE
            public decimal PrimeAnciennete { get; set; }



        //LISTE DES INDEMNITES
            public string Numero_indemnite_1 { get; set; }
            public string Nom_Indemnite_1 { get; set; }
            public string Taux_Indemnite_1 { get; set; }
            public string Montant_Indemnite_1 { get; set; }


            public string Numero_indemnite_2 { get; set; }
            public string Nom_Indemnite_2 { get; set; }
            public string Taux_Indemnite_2 { get; set; }
            public string Montant_Indemnite_2 { get; set; }


            public string Numero_indemnite_3 { get; set; }
            public string Nom_Indemnite_3 { get; set; }
            public string Taux_Indemnite_3{ get; set; }
            public string Montant_Indemnite_3{ get; set; }


            public string Numero_indemnite_4{ get; set; }
            public string Nom_Indemnite_4{ get; set; }
            public string Taux_Indemnite_4{ get; set; }
            public string Montant_Indemnite_4 { get; set; }


            public string Numero_indemnite_5 { get; set; }
            public string Nom_Indemnite_5 { get; set; }
            public string Taux_Indemnite_5 { get; set; }
            public string Montant_Indemnite_5 { get; set; }




        //SALAIRE BRUT
            public double SalaireBrut { get; set; }




        //BASE IUTS
            public double BaseIUTS { get; set; }



        // IUTS
            public double Iuts { get; set; }



        //TPA ET TAUX TPA
            public double Tpa { get; set; }
            public double TauxTpa { get; set; }



        //CNSS EMPLOYEUR ET TAUX CNSS EMPLOYEUR
            public double CnssEmployeur { get; set; }
            public double CnssEmploye { get; set; }




        //RISQUE PROFESSIONNEL
            public double RisqueProfessionnel { get; set; }



        //PRESTATION FAMILIALE
            public double PrestationFamiliale { get; set; }



        //AVANTAGE NATURE
            public double AvantageNature { get; set; }



        //SALAIRE NET ET EFFORT DE PAIX

             public decimal SalaireNet { get; set; }
             public decimal EffortDePaix { get; set; }
             public decimal SalaireNetaPayer { get; set; }


        public string Periode { get; set; }
            public string Civilite { get; set; }
             public string NumeroEmploye { get; set; }
        public DateTime DateNaissance { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public string AdresseEmploye { get; set; }

        public string Contrat { get; set; }
        public string Categorie { get; set; }
        public string Service { get; set; }
        public string Direction { get; set; }
        public string NumeroCNSSEmploye { get; set; }

        public string Sexe { get; set; }
        public int NbJourHeure { get; set; }
        public string Duree { get; set; }
        public int Charges { get; set; }
        public string Anciennete { get; set; }
        public string DureeContrat { get; set; }

        public string courrier { get; set; }


    }


}
