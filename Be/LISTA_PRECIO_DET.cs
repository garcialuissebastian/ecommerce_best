using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
    public class LISTA_PRECIO_DET
    {

        public Int32 Id { get; set; }
        public string Id_Producto { get; set; }
        public string Id_Lista { get; set; }
        public string Precio { get; set; }

        Be.Productos Item1 = new Productos();
        public Be.Productos Producto
        {
            get { return Item1; }
            set { Item1 = value; }

        }
     
    }
}
