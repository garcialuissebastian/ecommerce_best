using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
    public class ItemFact
    {
        public int Id { get; set; }
        public string Id_Articulo { get; set; }
        public string Detalle { get; set; }
        public double Importe { get; set; }

        public string Iva { get; set; }
        public string ImpxUni { get; set; }

        public string NetoImpxUni { get; set; }
        public string NetoImpTotal { get; set; }

        public string Cant { get; set; }
        public string tribInterno { get; set; }

        public string ImpInt { get; set; }
    }

}
