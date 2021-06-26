using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
    public class LISTA_PRECIOS
    { 
        public Int32 Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Activa { get; set; }

        List<LISTA_PRECIO_DET> Item1 = new List<LISTA_PRECIO_DET>();
        public List<LISTA_PRECIO_DET> Detalle
        {
            get { return Item1; }
            set { Item1 = value; }

        }
    }
}
