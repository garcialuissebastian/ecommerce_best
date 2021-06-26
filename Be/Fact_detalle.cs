using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
   public class Fact_detalle
    {
        public int Id { get; set; }
        public Int32 Concepto { get; set; }
        public Int32 DocTipo { get; set; }
        public Int64 DocNro { get; set; }
        public Int64 CbteDesde { get; set; }
        public Int64 CbteHasta { get; set; }

        public string CbteFch { get; set; }

        public double ImpTotal { get; set; }
        public double ImpTotConc { get; set; }
        public double ImpNeto { get; set; }
        public double ImpOpEx { get; set; }
        public double ImpTrib { get; set; }
        public double ImpIVA { get; set; }
        public string MonId { get; set; }
        public double MonCotiz { get; set; }

        List<Fact_AlicIva> Item1 = new List<Fact_AlicIva>();
        public List<Fact_AlicIva> AlicIva
        {
            get { return Item1; }
            set { Item1 = value; }

        }
        public string Resutado { get; set; }
        public Int64 CAE64 { get; set; }
        public string CAE { get; set; }
        public  string CAEFchVto { get; set; }
        public string Observaciones { get; set; }

        public int Id_lote { get; set; }
    }
}
