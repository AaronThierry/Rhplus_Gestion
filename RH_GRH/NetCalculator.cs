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
        public decimal NetAPayer { get; set; }         // SalaireNet - Effort, arrondi au 100 FCFA supérieur
        public decimal NetAPayerFinal { get; set; }    // SalaireNet - Effort - dette, arrondi au 100 FCFA supérieur
    }

    public static class NetCalculator
    {
        /// <summary>
        /// Arrondit un montant au 100 FCFA supérieur.
        /// Exemple: 45678.50 → 45700, 45600 → 45600, 45601 → 45700
        /// </summary>
        private static decimal ArrondiAuCent(decimal montant)
        {
            // Divise par 100, arrondit à l'entier supérieur, multiplie par 100
            return Math.Ceiling(montant / 100m) * 100m;
        }

        /// <summary>
        /// Calcule le Salaire Net, l'effort (par défaut 1%) et le Net à payer (arrondi au 100 FCFA supérieur).
        /// Empêche les négatifs, arrondi final au 100 FCFA supérieur sur NetAPayer.
        /// </summary>
        public static NetResult Calculer(
            decimal salaireBrut,
            decimal cnssEmploye,
            decimal iutsFinal,
            decimal avantagesNature,
            decimal Valeurdette,
            decimal tauxEffort = 0.01m,       // 1%
            bool arrondirNetAPayerCeil = true // ceil comme dans ton Java (Math.ceil)
        )
        {
            // 1) Salaire net avant effort
            decimal salaireNet = salaireBrut - cnssEmploye - iutsFinal - avantagesNature;
            if (salaireNet < 0m) salaireNet = 0m;

            // 2) Effort
            decimal effort = salaireNet * tauxEffort;

            // 3) Net à payer
            decimal netAPayer = salaireNet - effort;
            if (arrondirNetAPayerCeil)
                netAPayer = ArrondiAuCent(netAPayer);   // Arrondi au 100 FCFA supérieur

            //4) Net final
            decimal netAPayerFinal = netAPayer - Valeurdette;
            if (arrondirNetAPayerCeil)
                netAPayerFinal = ArrondiAuCent(netAPayerFinal);   // Arrondi au 100 FCFA supérieur

            return new NetResult
            {
                SalaireNet = decimal.Round(salaireNet, 2, MidpointRounding.AwayFromZero),
                Effort = decimal.Round(effort, 2, MidpointRounding.AwayFromZero),
                NetAPayer = netAPayer, // déjà ceiling
                NetAPayerFinal = netAPayerFinal
            };
        }
    }

}
