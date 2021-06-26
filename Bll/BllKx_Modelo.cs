using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dal;
using Be;

namespace Bll
{
    public class BllKx_Modelo
    {
        private Dal.DalKx_Modelo _mapeador;

        public BllKx_Modelo()
        {
            _mapeador = new DalKx_Modelo();
        }

        private static BllKx_Modelo instancia = null;

        public static BllKx_Modelo DameInstancia()
        {
            if (instancia == null)
            {
                return new BllKx_Modelo();
            }
            else
            {
                return instancia;
            }
        }

        public void Alta(Kx_Modelo v_obj)
        {
            try
            {
                this._mapeador.Alta(v_obj);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void Modificacion(Kx_Modelo v_obj)
        {
            try
            {
                this._mapeador.Modificacion(v_obj);
            }
            catch (Exception)
            {
                throw;
            }

        }
       
        public List<Kx_Modelo> Listar()
        {
            try
            {
                return this._mapeador.Listar();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<Kx_Modelo> ListarSub(string id)
        {
            try
            {
                return this._mapeador.ListarSub(id);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void Eliminar(Int32 v_id)
        {
            try
            {
                this._mapeador.Eliminar(v_id);
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
