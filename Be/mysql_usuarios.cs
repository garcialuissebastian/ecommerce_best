using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Be
{
  public  class mysql_usuarios
    {
      public PermisoBase Perfil { get; set; }
        public string ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string CLAVE { get; set; }
        public string TOKEN { get; set; }
        public string HABILITADO { get; set; }
        public string AUD { get; set; }
        public string ROL { get; set; }
        public string INTENTOS { get; set; }

        public string Descripcion { get; set; }

        List<kx_config> Item3 = new List<kx_config>();

        public List<kx_config>  ListaConfig
        {
            get { return Item3; }
            set { Item3 = value; }
        }


        List<Kx_Deposito> Item32 = new List<Kx_Deposito>();

        public List<Kx_Deposito> ListaDeposito
        {
            get { return Item32; }
            set { Item32 = value; }
        }

        public kx_config  Config { get; set; }

      
    }
}
