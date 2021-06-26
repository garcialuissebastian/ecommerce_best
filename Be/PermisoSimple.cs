using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Be

{
    [Serializable()]
    public class PermisoSimple : PermisoBase
    {



        public override bool EsValido(string nombrePermiso)
        {
             return this.nombre.Equals(nombrePermiso);
        }

        public override bool TieneHijos()
        {
             return false;
        }

        public override void LimpiarHijos()
        {
             
        }

        public override bool AgregarHijo(PermisoBase permiso)
        {
           return false;
        }

        public override bool QuitarHijo(PermisoBase permiso)
        {
         return false;
        }

        public override List<PermisoBase> ObtenerHijos()
        {
            List<PermisoBase> list = new List<PermisoBase>();
            return list; 
        }






        

         

 
   

    



    }
}