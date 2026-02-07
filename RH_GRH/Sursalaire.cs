using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH_GRH
{
    // ------------------ MODEL ------------------
    public class Sursalaire
    {
        public int IdSursalaire { get; set; }
        public int IdPersonnel { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public decimal Montant { get; set; }
    }
}
