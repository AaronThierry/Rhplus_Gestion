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
        /// SBP = salaireCategoriel + primeA (base abattement).
        /// </summary>
        public static IUTSResult CalculerIUTS(
            decimal salaireBrut,
            decimal cnssEmploye,
            string statut,
            decimal deducIndem,
            decimal salaireCategoriel,
            decimal primeA,
            bool floorCentaines = true)
        {
            decimal bsoc = salaireBrut - cnssEmploye;
            decimal brutFiscal = bsoc - deducIndem;
            decimal sbp = salaireCategoriel + primeA;

            decimal abatt = 0.25m; // défaut non-cadre
            if (!string.IsNullOrWhiteSpace(statut))
            {
                var s = statut.Trim().ToLowerInvariant();
                if (s == "oui") abatt = 0.20m;   // cadre
                else if (s == "non") abatt = 0.25m;
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

            Debug.WriteLine(
                $"[IUTS] Brut={salaireBrut:N2} | CNSS(E)={cnssEmploye:N2} | BSoc={res.SalaireBrutSocial:N2} | " +
                $"Deduc={deducIndem:N2} | Statut='{statut}' | SBP={sbp:N2} | " +
                $"BaseIUTS={res.BaseIUTS:N2} | BaseIUTS(⌊100⌋)={res.BaseIUTSArrondieCent:N2}"
            );

            return res;
        }
    }

}