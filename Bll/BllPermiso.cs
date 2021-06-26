using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Be;
using Dal;

namespace Bll
{
     public interface IGestorPermiso{
    bool Agregar(PermisoBase permiso);
    bool Modificar(PermisoBase permiso);
    bool Eliminar(PermisoBase permiso);
    List<PermisoBase> Listar(PermisoFiltro filtro) ;
    PermisoBase ObtenerUno(PermisoFiltro filtro);
    PermisoBase ObtenerUnoPorNombre(string nombre);
}
 
public class BllPermiso:IGestorPermiso{
 
    private DalPermiso _mapeador;
 
    public BllPermiso() {
        _mapeador = new DalPermiso();
    }
    public BllPermiso(DalPermiso mapeador)
    {
        _mapeador = mapeador;
    }  
 
    public bool Agregar(PermisoBase permiso) {
        try{
            return this._mapeador.Agregar(permiso);
        }catch(Exception  ){
            throw;
        }
    }  
 
    public bool Eliminar(PermisoBase permiso) {
        try{
            return this._mapeador.Eliminar(permiso);
        }catch(Exception  ){
            throw;
        }
    }  
 
    public List<PermisoBase > Listar(PermisoFiltro filtro) {
        try{
            return this._mapeador.Listar(filtro);
        }catch(Exception){
            throw;
        }
    }  
 
    public bool Modificar(PermisoBase permiso) {
        try{
            return this._mapeador.Modificar(permiso);
        }catch(Exception  ){
             
            throw  ;
        }
    }  
     public bool esPadre(string hijo , string padre) {
         try
         {
             return this._mapeador.esPadre( hijo ,   padre);
         }
         catch (Exception)
         {

             throw;
         }
     }  
    public PermisoBase ObtenerUno(PermisoFiltro filtro) {
        List<PermisoBase> lista= this.Listar(filtro)  ;
        if( lista.Count > 0 ){
            return lista[0];
        }else{
            return null;
        }
    }  
 
    public PermisoBase ObtenerUnoPorNombre(string nombre) {
        return this.ObtenerUno(new PermisoFiltro(nombre));
    }  
}
}