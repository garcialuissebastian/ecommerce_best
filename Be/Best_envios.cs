using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
 public   class Best_envios
    {
        public string remito { get; set; }
        public string volumen { get; set; }
        public string peso { get; set; }
        public string Monto { get; set; }
        public string CostoEnvio { get; set; }
        public string ancho { get; set; }
        public string largo { get; set; }
        public string alto { get; set; }
        public string calle { get; set; }
        public string nro { get; set; }
        public string cp { get; set; }
        public string piso { get; set; }
        public string dpto { get; set; }
        public string Provincia { get; set; }
        public string Localidad { get; set; }
        public string TIPO_ENVIO { get; set; }
        public string SEC_SELECT_ID { get; set; }
        public string SEC_SELECT_NOMBRE { get; set; }

        

    }

    public    class Oca_centros
    {

        public string IdCentroImposicion { get; set; }
        public string Sigla { get; set; }
        public string Sucursal { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Localidad { get; set; }
        public string CodigoPostal { get; set; }
        public string Provincia { get; set; } 
        public string Telefono  { get; set; }

        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string TipoAgencia { get; set; }
        public string SucursalOCA { get; set; }
        
    }      
    public class envio_dimensiones
    {

        public Int32 Id { get; set; }
        public string id { get; set; }
        public string Nombre { get; set; }
        public string alto { get; set; }
        public string largo { get; set; }
        public string ancho { get; set; }
        public string estado { get; set; }
        public string aud_mod_fec { get; set; }
      


        public string NombreCompleto()
        {
            return Nombre +" - "+alto+"x"+largo+"x"+ancho;
        }
    }
}
