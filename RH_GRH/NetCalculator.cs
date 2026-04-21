using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH_GRH
{
    public sealed class NetResult
    {
        public decimal SalaireNet { get; set; }        // Brut - CNSS(E) - IUTS(Final) - Indemnités Nature
        public decimal Effort { get; set; }            // 1% du SalaireNet (par défaut)
        public decimal NetAPayer { get; set; }         // SalaireNet - Effort, arrondi au 1 FCFA supérieur
        public decimal NetAPayerFinal { get; set; }    // SalaireNet - Effort - dette, arrondi au 1 FCFA supérieur
    }

    public static class NetCalculator
    {
        /// <summary>
        /// Calcule le Salaire Net, l'effort (par défaut 1%) et le Net à payer (arrondi au 1 FCFA supérieur).
        /// Empêche les négatifs, arrondi final au 1 FCFA supérieur sur NetAPayer.
        /// </summary>
        public static NetResult Calculer(
            decimal salaireBrut,
            decimal cnssEmploye,
            decimal iutsFinal,
            decimal avantagesNature,
            decimal Valeurdette,
            decimal totalAbonnements = 0m,
            decimal tauxEffort = 0.01m,       // 1%
            bool arrondirNetAPayerCeil = true // ceil comme dans ton Java (Math.ceil)
        )
        {
            // 1) Salaire net avant effort - Arrondi au franc supérieur dès que > 0.5
            decimal salaireNet = salaireBrut - cnssEmploye - iutsFinal - avantagesNature;
            if (salaireNet < 0m) salaireNet = 0m;
            salaireNet = decimal.Round(salaireNet, 0, MidpointRounding.AwayFromZero);

            // 2) Effort - Arrondi au franc supérieur dès que > 0.5
            decimal effort = salaireNet * tauxEffort;
            effort = decimal.Round(effort, 0, MidpointRounding.AwayFromZero);

            // 3) Net à payer
            decimal netAPayer = salaireNet - effort;
            if (arrondirNetAPayerCeil)
                netAPayer = decimal.Ceiling(netAPayer);   // Arrondi au 1 FCFA supérieur

            //4) Net final (déduction dette + abonnements)
            decimal netAPayerFinal = netAPayer - Valeurdette - totalAbonnements;
            if (arrondirNetAPayerCeil)
                netAPayerFinal = decimal.Ceiling(netAPayerFinal);   // Arrondi au 1 FCFA supérieur

            return new NetResult
            {
                SalaireNet = salaireNet,  // déjà arrondi
                Effort = effort,          // déjà arrondi
                NetAPayer = netAPayer,    // déjà ceiling
                NetAPayerFinal = netAPayerFinal
            };
        }
    }

}
