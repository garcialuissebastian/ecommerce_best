using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{

    public class Kc_Lote_Cab
    {

        public string ID { get; set; }
        public string MOTIVO { get; set; }

        public string DEPOSITO { get; set; }
        public string DEPOSITO_DESTINO { get; set; }
        public string REMITO  { get; set; }

        public string ORIGEN { get; set; }

        public string COMENTARIO { get; set; }
        public string ID_USUARIO  { get; set; }


        public string AUD { get; set; }


        List<Be.Kc_Lote> Item1 = new List<Be.Kc_Lote>();

        public List<Be.Kc_Lote> LIST_LOTES
        {
            get { return Item1; }
            set { Item1 = value; }
        }



    }

  public  class Kc_Lote
    {
        public Int32 Id { get; set; }
        public string Id_Art { get; set; }
        public string desc_Art { get; set; }
        public string Cod_Manual { get; set; }
        public string Id_Dep { get; set; }
        public string Dep_Desc { get; set; }
        public string Fecha_vto { get; set; }
        public string Cant { get; set; }
        public string Cod_Lot { get; set; }
        public string Id_Prov { get; set; }
        public string Barra { get; set; }
        public string Remito { get; set; }
        public string Motivo { get; set; }

        public string TipoIngreso { get; set; }
        public string Id_Lab { get; set; }
        public string desc_Lab { get; set; }
       
    }
}
