using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
    public class kx_cheques
    {

        public Int32 Id { get; set; }
        public string cheque_no { get; set; }
        public string banco { get; set; }
        public string tipo { get; set; }
        public string monto { get; set; }
        public string id_cbte { get; set; }
        public string Dias { get; set; }
        public string CodCliente { get; set; }
        public string CodProveedor { get; set; }
        public string Librador { get; set; }
        public string FechaEmision { get; set; }
        public string FechaCobro { get; set; }
        public string FechaVto { get; set; }
        public string CUITLibrador { get; set; }
    }
}
