using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dal;
using Be;
using System.Data;
using System.IO;
namespace Bll
{

    public class Bllkx_cbtes
    {
        private Dal.Dalkx_cbtes _mapeador;

        public Bllkx_cbtes()
        {
            _mapeador = new Dalkx_cbtes();
        }

        private static Bllkx_cbtes instancia = null;

        public static Bllkx_cbtes DameInstancia()
        {
            if (instancia == null)
            {
                return new Bllkx_cbtes();
            }
            else
            {
                return instancia;
            }
        }
         public List<string> serv_artXmes(string v_mes, string v_anio, string v_id_cfg)
        {
            try
            {
                return this._mapeador.serv_artXmes( v_mes,  v_anio,  v_id_cfg);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public string[] saldoXmes(string v_anio, string v_cfg)
        {
             try
             {
                 return this._mapeador.saldoXmes(v_anio, v_cfg);
             }
             catch (Exception)
             {
                 throw;
             }

         }

         public Int32 maxCbtePruebas(string tipocbte, string id_config)
        {
            try
            {
                return this._mapeador.maxCbtePruebas( tipocbte, id_config);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public string Alta(kx_cbtes v_obj)
        {
            try
            {
              return  this._mapeador.Alta(v_obj);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void Modificacion(kx_cbtes v_obj)
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

        public string PathReportCbte(string v_obj)
        {
            string ppr = "";
            try
            {
               
                if (v_obj == "6")
                {
                    ppr = "HardSoft.App.Kardex.Report.CbateB.rdlc";
                }
                if (v_obj == "8")
                {
                    ppr = "HardSoft.App.Kardex.Report.CbatenNcB.rdlc";
                }
                if (v_obj == "7")
                {
                    ppr = "HardSoft.App.Kardex.Report.CbatenNdB.rdlc";
                }

                if (v_obj == "1")
                {
                    ppr = "HardSoft.App.Kardex.Report.CbateA.rdlc";
                }
                if (v_obj == "3")
                {
                    ppr = "HardSoft.App.Kardex.Report.CbateNcA.rdlc";
                }
                if (v_obj == "2")
                {
                    ppr = "HardSoft.App.Kardex.Report.CbateNdA.rdlc";
                }
                if (v_obj  == "11")
                {
                    ppr = "HardSoft.App.Kardex.Report.CbateC.rdlc";

                }

                if (v_obj == "13")
                {
                    ppr = "HardSoft.App.Kardex.Report.CbateNcC.rdlc";
                }
                return ppr;
            }
            catch (Exception)
            {
                throw;
            }

        }
           public Be.kx_cliente WsExiste(string doc,string user)
        {
            try
            {
              return  this._mapeador.WsExiste( doc,user);
            }
            catch (Exception)
            {
                throw;
            }

        }
           public DataSet Cbate(string v_Id)
        {
            try
            {
             return   this._mapeador.Cbate(v_Id);
            }
            catch (Exception)
            {
                throw;
            }

        }


        public DataSet Cbate_Nro(string v_Id)
        {
            try
            {
                return this._mapeador.Cbate_Nro(v_Id);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<kx_cbtes> Listar()
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
          public List<Be.kx_cbtes> WssaldoXmes(string v_mes, string v_anio, string v_id_cfg)
        {
            try
            {
                return this._mapeador. WssaldoXmes(  v_mes,   v_anio,   v_id_cfg);
            }
            catch (Exception)
            {
                throw;
            }

        }
          public Dictionary<string, List<string>> Wscttiventas(string v_tipoCbate, string v_tipo, string v_valor1, string v_valor2, string v_user, string v_conf)
          {
              try
              {

 

                  return this._mapeador.Wscttiventas(v_tipoCbate, v_tipo, v_valor1, v_valor2, v_user, v_conf);
              }
              catch (Exception)
              {
                  throw;
              }

          }

        public List<Be.kx_cbtes> WsListar(string v_tipoCbate, string v_tipo, string v_valor1, string v_valor2, string v_user, string v_conf)
        {
            try
            {
                return this._mapeador.WsListar( v_tipoCbate,   v_tipo,   v_valor1,  v_valor2, v_user, v_conf);
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
