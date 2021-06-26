using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
    public class kx_cbtes  
    {

        public Int32 Id { get; set; }
        public string CantReg { get; set; }
        public string CbteTipo { get; set; }
        public string PtoVta { get; set; }
        public string Fecha { get; set; }
        public string usuario { get; set; }
        public string Resultado { get; set; }
        public string Vendedor { get; set; }
        public string Cliente { get; set; }
        public string aud { get; set; }
        public string Concepto { get; set; }
        public string DocTipo { get; set; }
        public string DocNro { get; set; }
        public string CbteDesde { get; set; }
        public string CbteHasta { get; set; }
        public string CbteFch { get; set; }
   
        public string ImpTotConc { get; set; }
        public string ImpNeto { get; set; }
        public string ImpIVA { get; set; }
        public string ImpTotal { get; set; }
        public string ImpOpEx { get; set; }
        public string ImpTrib { get; set; }
       
        public string MonId { get; set; }
        
        public string MonCotiz { get; set; }
        public string CAE { get; set; }
        public string CAEFchVto { get; set; }
        public string Observaciones { get; set; }

        public string CodBarra { get; set; }
        public string Cuit { get; set; }

        public string Id_config { get; set; }

        public string Pago_Efectivo { get; set; }
        public string Pago_Bancos { get; set; }
        public string Pago_Cheques { get; set; }
        public string Pago_Tarjetas { get; set; }
        public string ImporteLetra { get; set; }

        public string Contado { get; set; }

        public string treintaDias { get; set; }
        public string Tj_Debito { get; set; }
        public string Tj_Credito { get; set; }
        public string CtaCte { get; set; } 
        public string Cheque { get; set; }
        public string Otra  { get; set; }
        public string Remito { get; set; }
        public string CantAlic { get; set; }
        public string FchVtoPago { get; set; }
        
       
        List<Be.kx_cbtes_articulos> Item1 = new List<Be.kx_cbtes_articulos>();

        public List<Be.kx_cbtes_articulos> Articulos
        {
            get { return Item1; }
            set { Item1 = value; }
        }


        List<Be.kx_cheques> Item3 = new List<Be.kx_cheques>();

        public List<Be.kx_cheques> Cheques
        {
            get { return Item3; }
            set { Item3 = value; }
        }

        List<Be.kx_tarjetas> Item5 = new List<Be.kx_tarjetas>();

        public List<Be.kx_tarjetas> Tarjetas
        {
            get { return Item5; }
            set { Item5 = value; }
        }


        List<Fact_AlicIva> Item6 = new List<Fact_AlicIva>();

        public List<Fact_AlicIva> AlicIva
        {
            get { return Item6; }
            set { Item6 = value; }
        }

    }
}
