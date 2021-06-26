using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
namespace Be
{
    public abstract class PermisoBase
    {
        public String nombre { get; set; }
        public String descripcion { get; set; }


        public abstract Boolean EsValido(string nombrePermiso);
        public abstract Boolean TieneHijos();
        public abstract void LimpiarHijos();
        public abstract Boolean AgregarHijo(PermisoBase permiso);
        public abstract Boolean QuitarHijo(PermisoBase permiso);
        public abstract List<PermisoBase> ObtenerHijos();

        public   String ToString2()

        {
            
            return nombre  ;
        }

        public override String ToString()
        {
            string es = "";
            if (!TieneHijos()) {
                es = " (Simple)   " + "- ver más <img src='../../img/ver_mas.png'    onclick='ver_datos(this)' id='"+nombre+"'/>";
            } else {
                es = " (Compuesto) " + "- ver más <img src='../../img/ver_mas.png'    onclick='ver_datos(this)' id='" + nombre + "'/>";
            }
            return nombre+es;
        }

        public override Boolean Equals(Object obj)
        {


            if (obj != null)
            {

                

                if (Object.Equals(obj.GetType(), typeof(PermisoBase))) {

#pragma warning disable CS0219 // La variable 'tt' está asignada pero su valor nunca se usa
                    int tt = 0;
#pragma warning restore CS0219 // La variable 'tt' está asignada pero su valor nunca se usa
                }

                if (obj.GetType().BaseType == typeof(PermisoBase))
                {

                    PermisoBase ds = (PermisoBase)obj;


                    return nombre.Equals(ds.nombre);

                }

                else if (obj.GetType() == typeof(String))
                {


                    return nombre.Equals(obj);

                }
                else
                {
                    return false;
                }

            }
            else { return false; }

        }

        public override int GetHashCode()
        {


            return base.GetHashCode();


        }


    }




    public class PermisoFiltro : PermisoSimple
    {
        public Boolean FiltrarPorSimpleCompuesto { get; set; }

        public Boolean soloSimples { get; set; }

        public PermisoFiltro()
    {
        this.nombre = String.Empty;

    }

        public PermisoFiltro(string nombre) {
            this.nombre = nombre;
        
        }

        public PermisoFiltro(Boolean soloSimples)
        {
            this.nombre = String.Empty;
             this.FiltrarPorSimpleCompuesto = true;
             this.soloSimples = soloSimples;
        }

    }


}