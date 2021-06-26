using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
    public class kx_config  
    {
        public string Usuario { get; set; }
        public string id { get; set; }
        public string l1 { get; set; }
        public string l1_desc { get; set; }
        public string l2 { get; set; }
        public string l2_desc { get; set; }
        public string l3 { get; set; }
        public string l3_desc { get; set; }
        public string l4 { get; set; }
        public string l4_desc { get; set; }
        public string l5 { get; set; }
        public string l5_desc { get; set; }
        public string Nombre { get; set; }
        public string Cuit { get; set; }

        public string DomicilioComercial { get; set; }
        public string CondicionIVA { get; set; }
        public string RazonSocial { get; set; }
        public string RazonSocial2 { get; set; }
        public byte[] Logo { get; set; }
        public string IIBB { get; set; }
        public string FechaIniActividad { get; set; }

        public string PtoVta { get; set; }
         public string Modo { get; set; }

         public string Principal { get; set; }
    
    }
    public class kx_iibb
    {

        public Int32 Id { get; set; }
        public string Nombre { get; set; }
        public string valor { get; set; }
        public string Id_Usuario { get; set; }
    }

}
