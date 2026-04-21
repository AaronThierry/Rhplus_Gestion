using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH_GRH
{
    public sealed class IUTSResult
    {
        public decimal SalaireBrutSocial { get; set; }
        public decimal BaseIUTS { get; set; }
        public decimal BaseIUTSArrondieCent { get; set; } // floor à la centaine
    }

    public static class IUTSCalculator
    {
        /// <summary>
        /// Calcule BSoc et Base IUTS.
        /// statut: "oui" = cadre (20%), "non" = non-cadre (25%), autre = 25% par défaut.
        /// deducIndem = déductibilité indemnités.
        /// SBP = salaireCategoriel + primeA + sursalaire (base abattement).
        /// </summary>
        public static IUTSResult CalculerIUTS(
            decimal salaireBrut,
            decimal cnssEmploye,
            string statut,
            decimal deducIndem,
            decimal salaireCategoriel,
            decimal primeA,
            decimal salaireDeBase = 0m,
            decimal sursalaire = 0m,
            bool floorCentaines = true)
        {
            // Calcul CNSS Exonérée (doit être calculé AVANT le salaire brut social)
            decimal baseCnssExonere1 = salaireDeBase + primeA + sursalaire;
            decimal cnssExonere1Brut = baseCnssExonere1 * 0.08m;  // 8%
            decimal cnssExonere1 = Math.Min(cnssExonere1Brut, 44000m); // Plafonné à 44 000 FCFA

            decimal cnssExonere2Brut = salaireBrut * 0.055m;       // 5.5%
            decimal cnssExonere2 = Math.Min(cnssExonere2Brut, 44000m); // Plafonné à 44 000 FCFA

            // Prendre le minimum entre les deux CNSS exonérées
            decimal cnssExonereeRetenue = Math.Min(cnssExonere1, cnssExonere2);

            // Nouveau calcul du salaire brut social
            decimal bsoc = salaireBrut - cnssExonereeRetenue;
            decimal brutFiscal = bsoc - deducIndem;
            // SBP = Salaire catégoriel + Prime ancienneté + Sursalaire (base pour l'abattement)
            decimal sbp = salaireCategoriel + primeA + sursalaire;

            decimal abatt = 0.25m; // défaut non-cadre
            if (!string.IsNullOrWhiteSpace(statut))
            {
                var s = statut.Trim().ToLowerInvariant();
                if (s == "oui" || s == "cadre") abatt = 0.20m;   // cadre
                else if (s == "non" || s == "non-cadre") abatt = 0.25m;
            }

            decimal baseIuts = brutFiscal - (abatt * sbp);

            decimal baseIutsArr = baseIuts;
            if (floorCentaines)
                baseIutsArr = Math.Floor(baseIuts / 100m) * 100m;

            var res = new IUTSResult
            {
                SalaireBrutSocial = Math.Round(bsoc, 2, MidpointRounding.AwayFromZero),
                BaseIUTS = Math.Round(baseIuts, 2, MidpointRounding.AwayFromZero),
                BaseIUTSArrondieCent = baseIutsArr
            };

            // Debug détaillé avec tous les composants du calcul Base IUTS
            Debug.WriteLine("═══════════════════════════════════════════════════════════════════════");
            Debug.WriteLine("                    CALCUL BASE IUTS - DÉTAIL COMPLET                 ");
            Debug.WriteLine("═══════════════════════════════════════════════════════════════════════");
            Debug.WriteLine("");
            Debug.WriteLine("┌─ ÉTAPE 1: CNSS EXONÉRÉE - CALCUL ET SÉLECTION");
            Debug.WriteLine($"│  CNSS Exonérée 1 (finale) : {cnssExonere1,15:N2} FCFA");
            Debug.WriteLine($"│  CNSS Exonérée 2 (finale) : {cnssExonere2,15:N2} FCFA");
            Debug.WriteLine($"│  ────────────────────────────────────────────");
            if (cnssExonere1 < cnssExonere2)
            {
                Debug.WriteLine($"│  ✓ CNSS retenue (MIN)     : {cnssExonereeRetenue,15:N2} FCFA (Exonérée 1)");
            }
            else if (cnssExonere2 < cnssExonere1)
            {
                Debug.WriteLine($"│  ✓ CNSS retenue (MIN)     : {cnssExonereeRetenue,15:N2} FCFA (Exonérée 2)");
            }
            else
            {
                Debug.WriteLine($"│  ✓ CNSS retenue (MIN)     : {cnssExonereeRetenue,15:N2} FCFA (Égales)");
            }
            Debug.WriteLine("");
            Debug.WriteLine("┌─ ÉTAPE 2: SALAIRE BRUT SOCIAL (BSoc)");
            Debug.WriteLine($"│  Salaire Brut             : {salaireBrut,15:N2} FCFA");
            Debug.WriteLine($"│  CNSS Exonérée (-)        : {cnssExonereeRetenue,15:N2} FCFA");
            Debug.WriteLine($"│  ────────────────────────────────────────────");
            Debug.WriteLine($"│  Brut Social (BSoc)       : {res.SalaireBrutSocial,15:N2} FCFA");
            Debug.WriteLine("");
            Debug.WriteLine("┌─ ÉTAPE 3: BRUT FISCAL");
            Debug.WriteLine($"│  Brut Social              : {res.SalaireBrutSocial,15:N2} FCFA");
            Debug.WriteLine($"│  Indemnités déductibles(-): {deducIndem,15:N2} FCFA");
            Debug.WriteLine($"│  ────────────────────────────────────────────");
            Debug.WriteLine($"│  Brut Fiscal              : {brutFiscal,15:N2} FCFA");
            Debug.WriteLine("");
            Debug.WriteLine("┌─ ÉTAPE 4: DÉTAIL CNSS EXONÉRÉE");
            Debug.WriteLine($"│  ┌─ CNSS Exonérée 1 (8%)");
            Debug.WriteLine($"│  │  Salaire de base      : {salaireDeBase,15:N2} FCFA");
            Debug.WriteLine($"│  │  Prime ancienneté     : {primeA,15:N2} FCFA");
            Debug.WriteLine($"│  │  Sursalaire           : {sursalaire,15:N2} FCFA");
            Debug.WriteLine($"│  │  ────────────────────────────────────────────");
            Debug.WriteLine($"│  │  Base CNSS Exo 1      : {baseCnssExonere1,15:N2} FCFA");
            Debug.WriteLine($"│  │  Calcul 8%            : {cnssExonere1Brut,15:N2} FCFA");
            if (cnssExonere1Brut > 44000m)
            {
                Debug.WriteLine($"│  │  ⚠ Plafond appliqué   :       44,000.00 FCFA");
                Debug.WriteLine($"│  │  CNSS Exonérée 1      : {cnssExonere1,15:N2} FCFA (PLAFONNÉ)");
            }
            else
            {
                Debug.WriteLine($"│  │  CNSS Exonérée 1      : {cnssExonere1,15:N2} FCFA");
            }
            Debug.WriteLine($"│  │");
            Debug.WriteLine($"│  └─ CNSS Exonérée 2 (5.5%)");
            Debug.WriteLine($"│     Salaire Brut         : {salaireBrut,15:N2} FCFA");
            Debug.WriteLine($"│     Calcul 5.5%          : {cnssExonere2Brut,15:N2} FCFA");
            if (cnssExonere2Brut > 44000m)
            {
                Debug.WriteLine($"│     ⚠ Plafond appliqué   :       44,000.00 FCFA");
                Debug.WriteLine($"│     CNSS Exonérée 2      : {cnssExonere2,15:N2} FCFA (PLAFONNÉ)");
            }
            else
            {
                Debug.WriteLine($"│     CNSS Exonérée 2      : {cnssExonere2,15:N2} FCFA");
            }
            Debug.WriteLine("");
            Debug.WriteLine("┌─ ÉTAPE 5: ABATTEMENT");
            Debug.WriteLine($"│  Statut                   : {statut ?? "non-cadre"}");
            Debug.WriteLine($"│  Taux abattement          : {abatt * 100,15:N2} %");
            Debug.WriteLine($"│  Salaire catégoriel       : {salaireCategoriel,15:N2} FCFA");
            Debug.WriteLine($"│  Prime ancienneté         : {primeA,15:N2} FCFA");
            Debug.WriteLine($"│  Sursalaire               : {sursalaire,15:N2} FCFA");
            Debug.WriteLine($"│  ────────────────────────────────────────────");
            Debug.WriteLine($"│  SBP (Sal.Cat+Prime+Surs) : {sbp,15:N2} FCFA");
            Debug.WriteLine($"│  Montant abattement       : {abatt * sbp,15:N2} FCFA ({abatt * 100:N2}% de {sbp:N2})");
            Debug.WriteLine("");
            Debug.WriteLine("┌─ ÉTAPE 6: BASE IUTS");
            Debug.WriteLine($"│  Brut Fiscal              : {brutFiscal,15:N2} FCFA");
            Debug.WriteLine($"│  Abattement (-)           : {abatt * sbp,15:N2} FCFA");
            Debug.WriteLine($"│  ────────────────────────────────────────────");
            Debug.WriteLine($"│  BASE IUTS (exacte)       : {res.BaseIUTS,15:N2} FCFA");
            Debug.WriteLine($"│  BASE IUTS (⌊100⌋)        : {res.BaseIUTSArrondieCent,15:N2} FCFA");
            Debug.WriteLine("");
            Debug.WriteLine("═══════════════════════════════════════════════════════════════════════");
            Debug.WriteLine("");

            return res;
        }
    }

}