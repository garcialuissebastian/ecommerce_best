using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Be;
using Dal;
namespace Bll
{
    public class Bllkx_articulo
    {
        private Dal.Dalkx_articulo _mapeador;

        public Bllkx_articulo()
        {
            _mapeador = new Dalkx_articulo();
        }

        private static Bllkx_articulo instancia = null;

        public static Bllkx_articulo DameInstancia()
        {
            if (instancia == null)
            {
                return new Bllkx_articulo();
            }
            else
            {
                return instancia;
            }
        }

        public void Alta(kx_articulo v_obj)
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

        public DataSet ReportPedidos(string ids)
        {
            try
            {
                return this._mapeador.ReportPedidos(ids);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public DataSet ReportEgresoPedido(string ids, string pedido)
        {
            try
            {
                return this._mapeador.ReportEgresoPedido(ids, pedido);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public DataSet ReportEgreso(string ids)
        {
            try
            {
             return   this._mapeador.ReportEgreso( ids);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Be.Kc_Lote> VerPedidoDet(string ids)
        {
            try
            {
                return this._mapeador.VerPedidoDet(  ids);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<Kc_Lote_Cab> Listar_kx_pedidosFarmacia(string v_tipo, string v_valor, string userDep, bool esAdmin, string v_desde, string v_hasta)

        {
            try
            {
              return  this._mapeador.Listar_kx_pedidosFarmacia(  v_tipo, v_valor ,userDep,  esAdmin , v_desde,   v_hasta);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void egresoPedidos(Kc_Lote_Cab kc)
        {
            try
            {
                this._mapeador.egresoPedido(kc);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void actualizar_pedidos(string pedido, string estado)
        {
            try
            {
                this._mapeador.actualizar_pedidos( pedido,   estado);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void egresoLote(Kc_Lote_Cab kc)
        {
            try
            {
                this._mapeador.egresoLote(kc);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void IngresoLote(Kc_Lote_Cab kc)
        {
            try
            {
                this._mapeador.IngresoLote( kc);
            }
            catch (Exception)
            {
                throw;
            }

        }
            public List<Be.Combos> ListaLaboratorioCmb()
        {
            try
            {
                return this._mapeador.ListaLaboratorioCmb();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void Modificacion(kx_articulo v_obj)
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

        public void WsModStock(string v_id, string v_dep, string v_cant, string v_motivo, string v_remito, string v_origen, string v_proveedor, string v_comentario, string v_tipo_ingreso, string v_lote, string v_vto)
        {

              try
              {
                  this._mapeador.WsModStock(  v_id,  v_dep,   v_cant,  v_motivo, v_remito,  v_origen,  v_proveedor,  v_comentario,   v_tipo_ingreso  ,   v_lote,  v_vto);
              }
              catch (Exception)
              {
                  throw;
              }

          }


        public void WsModStockLt(string v_id, string v_dep, string v_cant, string v_motivo, string v_remito, string v_origen, string v_proveedor, string v_comentario, string v_tipo_ingreso, string v_lote, string v_vto, string v_lab, string v_cbte)
        {

            try
            {
                this._mapeador.WsModStockLt(v_id, v_dep, v_cant, v_motivo, v_remito, v_origen, v_proveedor, v_comentario, v_tipo_ingreso, v_lote, v_vto,v_lab,   v_cbte);
            }
            catch (Exception)
            {
                throw;
            }

        }
          public string Barcod(string v_ids) {
              try
              {
                  return this._mapeador.Barcod( v_ids);
              }
              catch (Exception)
              {
                  throw;
              }


          }

          public void Anular_articulo(string v_ids, string v_tipo){

              try
            {
                this._mapeador.Anular_articulo( v_ids,   v_tipo);
            }
            catch (Exception)
            {
                throw;
            }

        }
          public List<kx_articulo> Listar(string v_tipo, string v_valor, string v_user, string v_sis)
        {
            try
            {
                return this._mapeador.Listar(v_tipo, v_valor, v_user,v_sis);
            }
            catch (Exception)
            {
                throw;
            }

        }

          public void DeleteKc(string v_art)
          {

              try
              {
                  this._mapeador.DeleteKc(v_art);
              }
              catch (Exception)
              {
                  throw;
              }

          }
        public List<Kc_Lote> ListarStockFarmaciaAgrupado(string v_tipo, string v_valor, string v_user, string v_sis, string v_dep)
        {

            try
            {
                return this._mapeador.ListarStockFarmaciaAgrupado(v_tipo, v_valor, v_user, v_sis, v_dep);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Kc_Lote> ListarStockFarmacia(string v_tipo, string v_valor, string v_user, string v_sis, string v_dep)
        {

            try
            {
                return this._mapeador. ListarStockFarmacia( v_tipo,  v_valor,  v_user,  v_sis,  v_dep);
            }
            catch (Exception)
            {
                throw;
            }

        }
          public List<Kc_Lote> ListarKcxDep(string v_art, string v_dep)
          {
              try
              {
                  return this._mapeador.ListarKcxDep(v_art,  v_dep);
              }
              catch (Exception)
              {
                  throw;
              }

          }

          public List<Kc_Lote> ListarKc(string v_art)
          {
              try
              {
                  return this._mapeador.ListarKc(  v_art);
              }
              catch (Exception)
              {
                  throw;
              }

          }

        public List<kx_articulo> Listar()
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

        // mov stock
        public List<kx_stock> Listar_kx_stock(string v_tipo, string v_valor, string v_use)
        {
            try
            {
                return this._mapeador.Listar_kx_stock(v_tipo, v_valor, v_use);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Kc_Lote> Listar_kx_stockFarmaciaRptotal(string v_tipo, string v_valor, string v_use, string v_sis, string v_dep)
        {
            try
            {
                return this._mapeador.ListarStockFarmaciaRpTotal(v_tipo, v_valor, v_use, v_sis, v_dep);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<kx_stock> Listar_kx_stockFarmacia(string v_tipo, string v_valor, string v_use, string v_dep)
        {
            try
            {
                return this._mapeador.Listar_kx_stockFarmacia(v_tipo, v_valor, v_use, v_dep);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<kx_articulo> Listar_kx_listas_precio_historial(string v_tipo, string v_valor, string v_user, string v_sis)

        {
            try
            {
                return this._mapeador.Listar_kx_listas_precio_historial(  v_tipo, v_valor,v_user,  v_sis);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
