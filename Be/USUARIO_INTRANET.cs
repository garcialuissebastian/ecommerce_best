using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Be
{
 public   class USUARIO_INTRANET
    {
     public PermisoBase Perfil { get; set; }
        public Int32 PERSONAL_ID { get; set; }
        public string DOC_TIPO { get; set; }
        public Int32 DOC_NO { get; set; }
        public string APELLIDO { get; set; }
        public string NOMBRE { get; set; }
        public string   USUARIO { get; set; }
        public string CLAVE { get; set; }
        public string TOKEN { get; set; }
        public string HABILITADO { get; set; }
        public string  ROL { get; set; }
        public string CUILCUIT_LIMPIO { get; set; }
             public Int32 INTENTOS { get; set; }
             public string INICIO { get; set; }
             public string MAIL { get; set; }
             public string EMPRESA { get; set; }
             public string CUIT_EMPRESA { get; set; }
    }



}
