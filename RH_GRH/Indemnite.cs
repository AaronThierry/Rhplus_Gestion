using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH_GRH
{
    public class IndemniteList
    {
        public int IdEmploye { get; set; }
        public string NomIndemnite { get; set; }
        public string MontantIndemnite { get; set; }
        public string TauxIndem { get; set; }

        public IndemniteList( int idEmploye, string nomIndemnite, string montantIndemnite, string tauxIndem)
        {
            IdEmploye = idEmploye;
            NomIndemnite = nomIndemnite;
            MontantIndemnite = montantIndemnite;
            TauxIndem = tauxIndem;
        }
    }

}
