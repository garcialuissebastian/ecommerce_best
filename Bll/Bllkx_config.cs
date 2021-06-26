using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Be;
using Dal;
namespace Bll
{
    public class Bllkx_config
    {
        private Dal.Dalkx_config _mapeador;

        public Bllkx_config()
        {
            _mapeador = new Dalkx_config();
        }

        private static Bllkx_config instancia = null;

        public static Bllkx_config DameInstancia()
        {
            if (instancia == null)
            {
                return new Bllkx_config();
            }
            else
            {
                return instancia;
            }
        }
        public void bitacora(Be.Bitacora bi)
        {
            try
            {
                this._mapeador.bitacora(bi);
            }
            catch (Exception)
            {

                throw;
            }

        }
    
          public  string  Descripcion_Nombre(string v_user)
        {
            try
            {
                return this._mapeador.Descripcion_Nombre(  v_user);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void ActualizarPrecioxCosto(string v_user, string vPorc)
        { 
            try
            {
             this._mapeador.ActualizarPrecioxCosto( v_user, vPorc);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<kx_config> Obtener_config(string v_user)
        {
            try
            {
           return     this._mapeador.Obtener_config(v_user);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void Alta(kx_config v_obj)
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
        public void Modificacion(kx_config v_obj)
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

        public List<kx_config> Listar(string v_id)
        {
            try
            {
                return this._mapeador.Listar(v_id);
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


  
            public void Alta_kx_iibb(kx_iibb v_obj)
            {
                try
                {
                    this._mapeador.Alta_kx_iibb(v_obj);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public void Modificacion_kx_iibb(kx_iibb v_obj)
            {
                try
                {
                    this._mapeador.Modificacion_kx_iibb(v_obj);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public List<kx_iibb> Listar_kx_iibb(string v_tipo, string v_valor)
            {
                try
                {
                    return this._mapeador.Listar_kx_iibb(v_tipo, v_valor);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public void Eliminar_kx_iibb(Int32 v_id)
            {
                try
                {
                    this._mapeador.Eliminar_kx_iibb(v_id);
                }
                catch (Exception)
                {
                    throw;
                }
            }


        public void Alta_kx_config(kx_config v_obj)

        {
            try
            {
                this._mapeador.Alta_kx_config(v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Modificacion_kx_config(kx_config v_obj)

        {
            try
            {
                this._mapeador.Modificacion_kx_config(v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<kx_config> Listar_kx_config(string v_tipo, string v_valor, string user)


        {
            try
            {
                return this._mapeador.Listar_kx_config(v_tipo, v_valor,user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Eliminar_kx_config(Int32 v_id)

        {
            try
            {
                this._mapeador.Eliminar_kx_config(v_id);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }



}
    

