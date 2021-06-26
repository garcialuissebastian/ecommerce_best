using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Be;
using Dal;
namespace Bll
{
    public class Bllkx_proveedor
    {
        private Dal.Dalkx_proveedor _mapeador;

        public Bllkx_proveedor()
        {
            _mapeador = new Dalkx_proveedor();
        }

        private static Bllkx_proveedor instancia = null;

        public static Bllkx_proveedor DameInstancia()
        {
            if (instancia == null)
            {
                return new Bllkx_proveedor();
            }
            else
            {
                return instancia;
            }
        }

        public void Alta(kx_proveedor v_obj)
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
      
        public void Modificacion(kx_proveedor v_obj)
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

        public List<kx_proveedor> Listar()
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

        public List<kx_proveedor> Listar(string v_tipo, string v_valor, string user)
        {
            try
            {
                return this._mapeador.Listar(v_tipo, v_valor, user);
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
