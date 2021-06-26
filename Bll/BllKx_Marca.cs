using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dal;
using Be;
using System.Web;
namespace Bll
{

    public class BllKx_Rubro
    {
        private Dal.DalKx_Rubro _mapeador;

        public BllKx_Rubro()
        {
            _mapeador = new DalKx_Rubro();
        }

        private static BllKx_Rubro instancia = null;

        public static BllKx_Rubro DameInstancia()
        {
            if (instancia == null)
            {
                return new BllKx_Rubro();
            }
            else
            {
                return instancia;
            }
        }

        public void Alta(Kx_Rubro v_obj)
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
        public void Modificacion(Kx_Rubro v_obj)
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

          public List<Kx_Rubro> Listar(string v_tipo, string v_valor, string user)
        {
            try
            {
   

                return this._mapeador. Listar(  v_tipo,   v_valor,   user);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<Kx_Rubro> Listar(string use)
        {
            try
            {
                return this._mapeador.Listar(use);
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
