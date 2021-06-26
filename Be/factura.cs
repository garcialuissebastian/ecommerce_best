using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
    public class factura
    {
        public string id { get; set; }
        public string CbteInterno { get; set; }
        public int TipoCbte { get; set; }
        public string TipoCbteStr { get; set; }
        public string FechaCbteStr { get; set; }
        public DateTime FechaCbte { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }

        // public List<Be.ItemFact> Item { get; set; }

        List<Be.ItemFact> Item1 = new List<Be.ItemFact>();

        public List<Be.ItemFact> Item
        {
            get { return Item1; }
            set { Item1 = value; }
        }

        Best_envios Itemv = new Best_envios();

        public Best_envios Envio
        {
            get { return Itemv; }
            set { Itemv = value; }
        }

        public DateTime VtoPago { get; set; }

        public TipoDoc tipoDoc { get; set; }
        public Int64 Doc { get; set; }
        public string Domicilio { get; set; }

        public string Nombre { get; set; }
        public string respInscrip { get; set; }
        public string exento { get; set; }
        public string ConsumidorFinal { get; set; }

        public string ClienteIva { get; set; }
        public string Total { get; set; }
        public double SubTotal { get; set; }
        public string ImpOpEx { get; set; }

        public string NoRespo { get; set; }
        public string Monotributo { get; set; }
        public string CondicionVenta { get; set; }
        public string Contado { get; set; }
        public string CtaCte { get; set; }
        public string Treitadias { get; set; }
        public string CAE { get; set; }
        public string VtoCae { get; set; }

        public string Letra { get; set; }

        public string PtoVta { get; set; }
        public string NumCbte { get; set; }
        public string Cuit { get; set; }

        public string CodBarra { get; set; }
        public string MonedaStr { get; set; }

        public string DocTipo_Afip { get; set; }

        public string Concepto { get; set; }
        public string Neto { get; set; }
        public string Estado_CAE { get; set; }

        // totales
        public string vImpNeto { get; set; }
        public string vImpIVA { get; set; }
        public string vImpTri { get; set; }

        public string vIva21Neto { get; set; }
        public string vIva21importe { get; set; }

        public string vIva105Neto { get; set; }
        public string vIva105importe { get; set; }

        public string vIva27Neto { get; set; }
        public string vIva27importe { get; set; }
        public string vIva0Neto { get; set; }
        public string vIva0importe { get; set; }
        public string errores { get; set; }

    }

}
