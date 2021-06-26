using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Be;
using Dal;
namespace Bll
{
    public class Bllkx_cliente
    {
        private Dal.Dalkx_cliente _mapeador;

        public Bllkx_cliente()
        {
            _mapeador = new Dalkx_cliente();
        }

        private static Bllkx_cliente instancia = null;

        public static Bllkx_cliente DameInstancia()
        {
            if (instancia == null)
            {
                return new Bllkx_cliente();
            }
            else
            {
                return instancia;
            }
        }
          public Int32 AltaId(kx_cliente v_obj)
        {
            try
            {
                return this._mapeador.AltaId(v_obj);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void AltaPersonas(kx_cliente v_obj)
        {
            try
            {
                this._mapeador.AltaPersonas(v_obj);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public Int32 WsExisteDoc(string doc, string user)
        {
            try
            {
             return   this._mapeador.WsExisteDoc( doc, user);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void Alta(kx_cliente v_obj)
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
        public void Modificacion(kx_cliente v_obj)
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

        public void ModificacionPersona(kx_cliente v_obj)
        {
            try
            {
                this._mapeador.ModificacionPersona(v_obj);
            }
            catch (Exception)
            {
                throw;
            }

        }


        public List<kx_cliente> Listar(string v_tipo, string v_valor, string v_user)
        {
            try
            {
                return this._mapeador.Listar(v_tipo, v_valor, v_user);
            }
            catch (Exception)
            {
                throw;
            }

        }


        public List<kx_cliente> ListarPersonas(string v_tipo, string v_valor, string v_user)
        {
            try
            {
                return this._mapeador.ListarPersonas(v_tipo, v_valor, v_user);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<kx_cliente> Listar()
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


        public void EliminarPersonas(Int32 v_id)
        {
            try
            {
                this._mapeador.EliminarPersonas(v_id);
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
