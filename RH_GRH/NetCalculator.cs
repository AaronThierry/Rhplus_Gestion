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
        public decimal NetAPayer { get; set; }         // SalaireNet - Effort, arrondi au supérieur (Ceiling)
        public decimal NetAPayerFinal { get; set; }         // SalaireNet - Effort - dette, arrondi au supérieur (Ceiling)
    }

    public static class NetCalculator
    {
        /// <summary>
        /// Calcule le Salaire Net, l'effort (par défaut 1%) et le Net à payer (ceil).
        /// Empêche les négatifs, arrondi final au supérieur sur NetAPayer.
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
                netAPayer = decimal.Ceiling(netAPayer);   // équivalent Math.ceil pour decimal

            //4) Net final
            decimal netAPayerFinal = netAPayer - Valeurdette;
            if (arrondirNetAPayerCeil)
                netAPayerFinal = decimal.Ceiling(netAPayerFinal);   // équivalent Math.ceil pour decimal

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
