using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Be;
using Dal;

namespace Bll
{
    public class Bllkx_deposito_reposicion
    {
        private Dal.Dalkx_deposito_reposicion _mapeador;

        public Bllkx_deposito_reposicion()
        {
            _mapeador = new Dalkx_deposito_reposicion();
        }

        private static Bllkx_deposito_reposicion instancia = null;

        public static Bllkx_deposito_reposicion DameInstancia()
        {
            if (instancia == null)
            {
                return new Bllkx_deposito_reposicion();
            }
            else
            {
                return instancia;
            }
        }

        public void Alta(kx_deposito_reposicion v_obj)
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
        public void Modificacion(kx_deposito_reposicion v_obj)
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
        public List<kx_deposito_reposicion> ListarFarmacia(string v_tipo, string v_valor, string v_user)
        {
            try
            {
                return this._mapeador.ListarFarmacia(v_tipo, v_valor, v_user);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<kx_deposito_reposicion> Listar(string v_tipo, string v_valor)
        {
            try
            {
                return this._mapeador. Listar( v_tipo,  v_valor);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<kx_deposito_reposicion> Listar()
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
