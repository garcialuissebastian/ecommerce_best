using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
    public class kx_cbtes_articulos
    {

        public Int32 Id { get; set; }

        public string Cod_Manual { get; set; }
        public string Id_Articulo { get; set; }
        public string Nombre { get; set; }
        public string Cant { get; set; }
        public string Alic { get; set; }
        public string NetoImpxUni { get; set; }
        public string NetoImpTotal { get; set; }
        public string ImpIva { get; set; }
        public string ImpxUni { get; set; }
        public string ImpTotal { get; set; }
        public string id_cbte { get; set; }

        public string DepositoSelect { get; set; }

        public string Tipo_Art  { get; set; }
        public string Unidad { get; set; }
        public string IIBB { get; set; }
       
        public string es_Stock { get; set; }
    }
}
