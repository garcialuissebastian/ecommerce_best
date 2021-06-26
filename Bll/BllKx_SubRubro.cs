using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dal;
using Be;

namespace Bll
{
    public class BllKx_SubRubro
    {
        private Dal.DalKx_SubRubro _mapeador;

        public BllKx_SubRubro()
        {
            _mapeador = new DalKx_SubRubro();
        }

        private static BllKx_SubRubro instancia = null;

        public static BllKx_SubRubro DameInstancia()
        {
            if (instancia == null)
            {
                return new BllKx_SubRubro();
            }
            else
            {
                return instancia;
            }
        }

        public void Alta(Kx_SubRubro v_obj)
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
        public void Modificacion(Kx_SubRubro v_obj)
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

        public List<Kx_SubRubro> Listar()
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
        public List<Kx_SubRubro> ListarSub(string id)
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
