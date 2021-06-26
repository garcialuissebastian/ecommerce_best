using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Be;
using Dal;
namespace Bll
{
  public  class BllDatosBasico
    {

    


     private   Dal.DalDatosBasico _mapeador;

     public BllDatosBasico()
          {
              _mapeador = new DalDatosBasico();
          }
 
        private static BllDatosBasico instancia = null;

        public static BllDatosBasico DameInstancia()
        {
            if (instancia == null)
            {
                return new BllDatosBasico();
            }
            else
            {
                return instancia;
            } 
        }

         public List<departamento> ListarDepartamento(string id)
        {
            try
            {
                return this._mapeador.ListarDepartamento(id);
            }
            catch (Exception)
            {
                throw;
            }

        } 

        public List<distrito> ListarDistrito(string id)
         {
             try
             {
                 return this._mapeador. ListarDistrito( id);
             }
             catch (Exception)
             {
                 throw;
             }

         } 

        public List<Be.provincia> ListarProvincias(string id)
        {
            try
            {
                return this._mapeador.ListarProvincias(id);
            }
            catch (Exception)
            {
                throw;
            }

        } 
        public List<pais> ListarPais()
        {
            try
            {
                return this._mapeador.ListarPais();
            }
            catch (Exception)
            {
                throw;
            }

        } 
  }
}
