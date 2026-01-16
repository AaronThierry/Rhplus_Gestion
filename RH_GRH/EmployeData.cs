using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH_GRH
{

    public class Employe
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Civilite { get; set; }
        public DateTime DateNaissance { get; set; } // Utilisation de DateTime pour les dates
        public DateTime DateEntree { get; set; } // Utilisation de DateTime pour les dates
        public DateTime? DateSortie { get; set; } // Nullable DateTime pour la sortie
        public string Poste { get; set; }
        public string Matricule { get; set; }
        public string TelephoneEmploye { get; set; }
        public string Adresse { get; set; }
        public string Contrat { get; set; }
        public string NumeroCnssEmploye { get; set; }
        public string Sexe { get; set; }
        public string DureeContrat { get; set; }
        public string ModePaiement { get; set; }
        public string Identification { get; set; }
        public string Cadre { get; set; } // Si l'employé est cadre ou non
        public int Entreprise { get; set; } // Nom de l'entreprise
        public string Service { get; set; } // Service auquel l'employé appartient
        public string Categorie { get; set; } // Categorie auquel l'employé appartient
        public string Direction { get; set; } // Categorie auquel l'employé appartient
        public double? Montant { get; set; } // Categorie auquel l'employé appartient
        public string TypeContrat { get; set; }
        public int HeureContrat { get; set; }
        public int JourContrat { get; set; }
        public decimal? SalaireMoyen { get; set; }
        public decimal? Tpa { get; set; }


        // Autres propriétés pertinentes pour un employé peuvent être ajoutées ici

        public string Sigle  { get; set; }
        public string NomEntreprise  { get; set; }
        public string TelephoneEntreprise  { get; set; }
        public string EmailEntreprise  { get; set; }
        public string AdressePhysiqueEntreprise  { get; set; }
        public string AdressePostaleEntreprise  { get; set; }
        public string ResponsableEntreprise  { get; set; }

        // Constructeur
        public Employe(int id, string nom, DateTime dateEntree, DateTime? dateSortie,
                       string poste, string matricule, string telephone, string adresse, string contrat,
                       string modePaiement, string identification, string cadre, int entreprise, string service,
                       string categorie, double montant, string typeContrat, int heureContrat, int jourContrat,
                       decimal salairemoyen, decimal tpa, string civilite, DateTime dateNaissance, string direction,
                       string numeroCnssEmploye,string sexe,string dureeContrat, string sigle, string nomEntreprise,
                       string telephoneEntreprise, string emailEntreprise, string adressePhysiqueEntreprise,
                       string adressePostaleEntreprise, string responsableEntreprise)
        {
            Id = id;
            Nom = nom;
            DateEntree = dateEntree;
            DateSortie = dateSortie;
            Poste = poste;
            Matricule = matricule;
            TelephoneEmploye = telephone;
            Adresse = adresse;
            Contrat = contrat;
            ModePaiement = modePaiement;
            Identification = identification;
            Cadre = cadre;
            Entreprise = entreprise;
            Service = service;
            Categorie = categorie;
            Montant = montant;
            TypeContrat = typeContrat;
            HeureContrat = heureContrat;
            JourContrat = jourContrat;
            SalaireMoyen = salairemoyen;
            Tpa = tpa;
            Civilite = civilite;
            DateNaissance = dateNaissance;
            Direction = direction;
            NumeroCnssEmploye = numeroCnssEmploye;
            Sexe = sexe;
            DureeContrat = dureeContrat;
            Sigle = sigle;
            NomEntreprise = nomEntreprise;
            TelephoneEntreprise = telephoneEntreprise;
            EmailEntreprise = emailEntreprise;
            AdressePhysiqueEntreprise = adressePhysiqueEntreprise;
            AdressePostaleEntreprise = adressePostaleEntreprise;
            ResponsableEntreprise = responsableEntreprise;



        }

        // Méthode pour vérifier si l'employé est encore actif
        public bool EstActif()
        {
            return !DateSortie.HasValue || DateSortie > DateTime.Now; // Si pas de date de sortie ou sortie dans le futur, l'employé est actif
        }

        // Méthode pour vérifier si les dates sont valides pour un employé
        public static bool VerifierPeriodeSalariale(DateTime dateDebut, DateTime? dateFin, DateTime dateEntree, DateTime? dateSortie)
        {
            if (dateDebut < dateEntree)
                return false; // La date de début ne peut pas être avant la date d'entrée

            if (dateFin.HasValue && dateFin.Value < dateDebut)
                return false; // La date de fin ne peut pas être avant la date de début

            if (dateSortie.HasValue && dateFin.HasValue && dateFin.Value > dateSortie)
                return false; // La date de fin ne peut pas être après la date de sortie

            return true;
        }

        // Méthode pour afficher les informations de l'employé sous forme de chaîne

    }



}
