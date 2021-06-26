using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dal;
using Be;
namespace Bll
{
  public  class BllLote
    {

        private  Dal.DalLote _mapeador;

        public BllLote()
          {

              _mapeador = new DalLote();
          }

        private static BllLote instancia = null;

        public static BllLote DameInstancia()
        {

            if (instancia == null)
            {

                return new BllLote();
            }
            else
            {
                return instancia;
            }
 
        }

       public void InsertLote(Fact_Cab v_facts){
            try
            {
                 this._mapeador.InsertLote(v_facts);
            }
            catch (Exception)
            {
                throw;
            }
        }
       public  string validad_cuit(Int64 tipo)
        { 
           
           try{
              return  this._mapeador.validad_cuit(  tipo);
            }
            catch (Exception)
            {
                throw;
            }
        }

       public string validad_doc(Int64 tipo)
       {

           try
           {
               return this._mapeador.validad_doc(tipo);
           }
           catch (Exception)
           {
               throw;
           }
       }

       public List<Fact_Cab> Consulta_lote(string ids_tipo, string v_usuario)
       {

           try
           {
               return this._mapeador.Consulta_lote( ids_tipo,  v_usuario);
           }
           catch (Exception)
           {
               throw;
           }
       }

       public Fact_Cab lote(string ids)
       {
           try
           {
             return  this._mapeador.lote(ids);
           }
           catch (Exception)
           {
               throw;
           }
       }

    }
}
