using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dal;
using Be;
namespace Bll
{

    public class BllKx_Marca
    {
        private Dal.DalKx_Marca _mapeador;

        public BllKx_Marca()
        {
            _mapeador = new DalKx_Marca();
        }

        private static BllKx_Marca instancia = null;

        public static BllKx_Marca DameInstancia()
        {
            if (instancia == null)
            {
                return new BllKx_Marca();
            }
            else
            {
                return instancia;
            }
        }

        public void Alta(Kx_Marca v_obj)
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
        public void Modificacion(Kx_Marca v_obj)
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

          public List<Kx_Marca> Listar(string v_tipo, string v_valor, string v_user)
        {
            try
            {
                return this._mapeador. Listar(  v_tipo,   v_valor, v_user);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<Kx_Marca> Listar(string user)
        {
            try
            {
                return this._mapeador.Listar(user);
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
