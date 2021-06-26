using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
    public class contraordenes_servicio
    {

        public Int32 Id { get; set; } 
        public string Orden_Id { get; set; }
        public string Observacion { get; set; }
        public string Motivo { get; set; }
        public string Info_Tecnico { get; set; }
        public string Tiempo { get; set; }
        public string Valor { get; set; }
        public string Fecha { get; set; }

        public string Pagado { get; set; }

        public string No{ get; set; }

        List<Be.kx_articulo> Item1 = new List<Be.kx_articulo>();

        public List<Be.kx_articulo> Repuestos
        {
            get { return Item1; }
            set { Item1 = value; }
        }

        public string Tactil { get; set; }
        public string Boton_Home { get; set; }
        public string Camara_Tracera { get; set; }
        public string Camara_Delantera { get; set; }
        public string Sensor_Proximidad { get; set; }
        public string Parlantes { get; set; }
        public string Auricular { get; set; }
        public string Boton_Power { get; set; }
        public string Volumen { get; set; }
        

    }
}
