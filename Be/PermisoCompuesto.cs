using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Be
{
    public class PermisoCompuesto: PermisoBase

    {

        private List<PermisoBase>  _listaHijos  = new  List<PermisoBase>();


public override bool  EsValido(string nombrePermiso)
{
  Boolean tieneUnValido    = false;
        //'recorrer los hijos a ver si tiene al menos uno valido
        //'TODO: esta funcion debe ser optimizada para que deje de recorrer al encontrar el primer permiso que sea valido!
      
    

    foreach (var p in _listaHijos )
	{
		 tieneUnValido = tieneUnValido ||  p.EsValido(nombrePermiso);

	}
    
     
        return tieneUnValido;

}

public override bool  TieneHijos()
{
 	 return true;
}

public override void  LimpiarHijos()
{
   _listaHijos.Clear();
}

public override bool  AgregarHijo(PermisoBase permiso)
{
 	   if ( ! _listaHijos.Contains(permiso)){
             _listaHijos.Add(permiso);
            return true;
       
       }else{
         return false;
       }

       
}

public override bool  QuitarHijo(PermisoBase permiso)
{
return _listaHijos.Remove(permiso);
}

public override List<PermisoBase>  ObtenerHijos()
{
   return _listaHijos;
}
}
}