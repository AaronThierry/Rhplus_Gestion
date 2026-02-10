using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH_GRH
{
    public static class CNSSCalculator
    {
        // Plafond CNSS selon la législation du Burkina Faso
        public const decimal PLAFOND_CNSS = 800000m;
        public const decimal TAUX_CNSS_EMPLOYE = 0.055m; // 5.5%
        public const decimal MONTANT_MAX_CNSS_EMPLOYE = 44000m; // 800000 * 5.5%

        public sealed class TauxCNSS
        {
            public decimal PF; // Pension Vieillesse (employeur)
            public decimal RP; // Risque Professionnel (employeur)
            public decimal PS; // Prestations Sociales (employeur)

            public TauxCNSS(decimal pf, decimal rp, decimal ps)
            {
                PF = pf; RP = rp; PS = ps;
            }

            public decimal Total() { return PF + RP + PS; }
        }

        public static TauxCNSS GetTauxParContrat(string contrat)
        {
            if (contrat == null) contrat = "";
            contrat = contrat.Trim().ToLowerInvariant();

            switch (contrat)
            {
                case "permanent":
                case "temporaire":
                case "occasionnel":
                case "volontaire national":
                    return new TauxCNSS(0.06m, 0.015m, 0.14m);
                case "fonctionnaire détaché":
                    return new TauxCNSS(0.06m, 0.00m, 0.00m);
                case "stagiaire":
                    return new TauxCNSS(0.00m, 0.015m, 0.14m);
                case "eleve":
                case "etudiant":
                    return new TauxCNSS(0.00m, 0.015m, 0.00m);
                default:
                    return new TauxCNSS(0.00m, 0.00m, 0.00m);
            }
        }

        // Employé : 5,5% pour les contrats éligibles avec plafond de 800 000 FCFA
        public static decimal CalculerCNSSEmploye(decimal salaireBrut, string contrat)
        {
            if (salaireBrut <= 0)
            {
                Debug.WriteLine($"[CNSS] Salaire brut <= 0: {salaireBrut}");
                return 0m;
            }

            if (string.IsNullOrWhiteSpace(contrat))
            {
                Debug.WriteLine($"[CNSS] Contrat vide ou null pour salaire: {salaireBrut}");
                return 0m;
            }

            var c = contrat.Trim().ToLowerInvariant();
            Debug.WriteLine($"[CNSS] Calcul CNSS Employé - Contrat: '{contrat}' -> '{c}', Salaire brut: {salaireBrut}");

            switch (c)
            {
                case "permanent":
                case "temporaire":
                case "occasionnel":
                case "volontaire national":
                case "stagiaire":
                    // Appliquer le plafond de 800 000 FCFA
                    decimal salaireAssiette = Math.Min(salaireBrut, PLAFOND_CNSS);
                    decimal cnss = Math.Round(salaireAssiette * TAUX_CNSS_EMPLOYE, 0, MidpointRounding.AwayFromZero);

                    // Le montant ne peut pas dépasser 44 000 FCFA
                    cnss = Math.Min(cnss, MONTANT_MAX_CNSS_EMPLOYE);

                    Debug.WriteLine($"[CNSS] Salaire brut: {salaireBrut}, Assiette (plafonnée): {salaireAssiette}, CNSS Employé: {cnss}");
                    return cnss;
                default:
                    Debug.WriteLine($"[CNSS] Contrat non éligible: '{c}'");
                    return 0m;
            }
        }

        // Employeur (mêmes formules que ton Java)
        public static decimal CalculerPensionEmployeur(decimal salaireBrut)    // 8,5%
            => Math.Round(salaireBrut * 0.085m, 0, MidpointRounding.AwayFromZero);

        public static decimal CalculerRisqueProEmployeur(decimal salaireBrut)  // 1,5%
            => Math.Round(salaireBrut * 0.015m, 0, MidpointRounding.AwayFromZero);

        public static decimal CalculerPFEmployeur(decimal salaireBrut)         // 6%
            => Math.Round(salaireBrut * 0.06m, 0, MidpointRounding.AwayFromZero);

        public static decimal CalculerTpa(decimal salaireBrut, decimal tauxTpaPourcent)
            => Math.Round(salaireBrut * (tauxTpaPourcent / 100m), 0, MidpointRounding.AwayFromZero);
    }







}
