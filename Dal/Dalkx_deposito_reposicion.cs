using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Be;
using MySql.Data.MySqlClient;
namespace Dal
{
    public class Dalkx_deposito_reposicion
    {

        private MySqlConectarSqlDBVarias cnn = new MySqlConectarSqlDBVarias("kardex");


        MySqlCommand cmm;

        public void Alta(kx_deposito_reposicion v_obj)
        {
            try
            {
                string cmdTxt = " insert into kx_deposito_reposicion (id_articulo, id_deposito, stock_minimo, stock_maximo, punto_pedido, pasillo, estanteria, nivel) values ( '" + v_obj.id_articulo + "', '" + v_obj.id_deposito + "', '" + v_obj.stock_minimo + "', '" + v_obj.stock_maximo + "', '" + v_obj.punto_pedido + "', '" + v_obj.pasillo + "', '" + v_obj.estanteria + "', '" + v_obj.nivel + "')  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                cnn.ExecuteNonQuery(cmm);

            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }

        }
            public List<kx_deposito_reposicion> Listar(string v_tipo, string v_valor)
        {

            List<kx_deposito_reposicion> lista = new List<kx_deposito_reposicion>();
            try
            {
                string cmdTxt = " select  id, id_articulo, id_deposito, stock_minimo, stock_maximo, punto_pedido, pasillo, estanteria, nivel from kx_deposito_reposicion  ";

                if (v_tipo == "0")
                {
                    cmdTxt = " select ar.Nombre Articulo, getRubro(ar.Rubro) RubroNombre, getSub_Rubro(ar.Sub_Rubro) Sub_RubroNombre, getMarca(ar.Marca) MarcaNombre,getModelo(ar.Modelo) ModeloNombre  ,  d.stock, d.id, d.id_articulo, d.id_deposito, d.stock_minimo, d.stock_maximo, d.punto_pedido, d.pasillo, d.estanteria, d.nivel, dp.Nombre, ar.Lote from kx_deposito_reposicion d, kx_deposito dp, kx_articulo ar where d.id_deposito = dp.Id and d.id_articulo= '" + v_valor + "' and ar.Id = d.id_articulo ";

                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    kx_deposito_reposicion entidad = new kx_deposito_reposicion();

                    entidad.id = DalModelo.VerifStringMysql(lector, "id");
                    entidad.id_articulo = DalModelo.VerifStringMysql(lector, "id_articulo");
                    entidad.Lote = DalModelo.VerifStringMysql(lector, "Lote");
                    entidad.id_deposito = DalModelo.VerifStringMysql(lector, "id_deposito");
                    entidad.stock_minimo = DalModelo.VerifStringMysql(lector, "stock_minimo");
                    entidad.stock_maximo = DalModelo.VerifStringMysql(lector, "stock_maximo");
                    entidad.punto_pedido = DalModelo.VerifStringMysql(lector, "punto_pedido");
                    entidad.pasillo = DalModelo.VerifStringMysql(lector, "pasillo");
                    entidad.estanteria = DalModelo.VerifStringMysql(lector, "estanteria");
                    entidad.nivel = DalModelo.VerifStringMysql(lector, "nivel");
                    entidad.stock = DalModelo.VerifStringMysql(lector, "stock");

                    entidad.Rubro = DalModelo.VerifStringMysql(lector, "RubroNombre");
                    entidad.Sub_Rubro = DalModelo.VerifStringMysql(lector, "Sub_RubroNombre");
                    entidad.Marca = DalModelo.VerifStringMysql(lector, "MarcaNombre");
                    entidad.Modelo = DalModelo.VerifStringMysql(lector, "ModeloNombre");
                    entidad.Articulo = DalModelo.VerifStringMysql(lector, "Articulo");

                    
                    if (v_tipo == "0")
                    { 
                        entidad.deposito_nombre = DalModelo.VerifStringMysql(lector, "Nombre"); 
                    }
                    lista.Add(entidad);

                }

                lector.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }


            return lista;
        }

        public List<kx_deposito_reposicion> ListarFarmacia(string v_tipo, string v_valor, string v_user)
        {

            List<kx_deposito_reposicion> lista = new List<kx_deposito_reposicion>();
            try
            {
                string cmdTxt = " select  id, id_articulo, id_deposito, stock_minimo, stock_maximo, punto_pedido, pasillo, estanteria, nivel from kx_deposito_reposicion  ";

                if (v_tipo == "0")
                {
                    cmdTxt = " select ar.Nombre Articulo, getRubro(ar.Rubro) RubroNombre, getSub_Rubro(ar.Sub_Rubro) Sub_RubroNombre, getMarca(ar.Marca) MarcaNombre,getModelo(ar.Modelo) ModeloNombre  ,  d.stock, d.id, d.id_articulo, d.id_deposito, d.stock_minimo, d.stock_maximo, d.punto_pedido, d.pasillo, d.estanteria, d.nivel, dp.Nombre, ar.Lote from kx_deposito_reposicion d, kx_deposito dp, kx_articulo ar where d.id_deposito = dp.Id and d.id_articulo= '" + v_valor + "' and ar.Id = d.id_articulo and dp.Id  in (select ds.id_dep from kx_deposito_usuario ds where ds.id_usuario = '" + v_user + "')";

                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    kx_deposito_reposicion entidad = new kx_deposito_reposicion();

                    entidad.id = DalModelo.VerifStringMysql(lector, "id");
                    entidad.id_articulo = DalModelo.VerifStringMysql(lector, "id_articulo");
                    entidad.Lote = DalModelo.VerifStringMysql(lector, "Lote");
                    entidad.id_deposito = DalModelo.VerifStringMysql(lector, "id_deposito");
                    entidad.stock_minimo = DalModelo.VerifStringMysql(lector, "stock_minimo");
                    entidad.stock_maximo = DalModelo.VerifStringMysql(lector, "stock_maximo");
                    entidad.punto_pedido = DalModelo.VerifStringMysql(lector, "punto_pedido");
                    entidad.pasillo = DalModelo.VerifStringMysql(lector, "pasillo");
                    entidad.estanteria = DalModelo.VerifStringMysql(lector, "estanteria");
                    entidad.nivel = DalModelo.VerifStringMysql(lector, "nivel");
                    entidad.stock = DalModelo.VerifStringMysql(lector, "stock");

                    entidad.Rubro = DalModelo.VerifStringMysql(lector, "RubroNombre");
                    entidad.Sub_Rubro = DalModelo.VerifStringMysql(lector, "Sub_RubroNombre");
                    entidad.Marca = DalModelo.VerifStringMysql(lector, "MarcaNombre");
                    entidad.Modelo = DalModelo.VerifStringMysql(lector, "ModeloNombre");
                    entidad.Articulo = DalModelo.VerifStringMysql(lector, "Articulo");


                    if (v_tipo == "0")
                    {
                        entidad.deposito_nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    }
                    lista.Add(entidad);

                }

                lector.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }


            return lista;
        }


        public void Modificacion(kx_deposito_reposicion v_obj)
        {
            try
            {
                string cmdTxt = "update kx_deposito_reposicion set  id_articulo='" + v_obj.id_articulo + "' , id_deposito='" + v_obj.id_deposito + "' , stock_minimo='" + v_obj.stock_minimo + "' , stock_maximo='" + v_obj.stock_maximo + "' , punto_pedido='" + v_obj.punto_pedido + "' , pasillo='" + v_obj.pasillo + "' , estanteria='" + v_obj.estanteria + "' , nivel='" + v_obj.nivel + "' where id ='" + v_obj.id + "'   ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                cnn.ExecuteNonQuery(cmm);

            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }

        }

        public List<kx_deposito_reposicion> Listar()
        {

            List<kx_deposito_reposicion> lista = new List<kx_deposito_reposicion>();
            try
            {



                string cmdTxt = " select  id, id_articulo, id_deposito, stock_minimo, stock_maximo, punto_pedido, pasillo, estanteria, nivel from kx_deposito_reposicion  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

              MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    kx_deposito_reposicion entidad = new kx_deposito_reposicion();
                
                    entidad.id = DalModelo.VerifStringMysql(lector, "id");
                    entidad.id_articulo = DalModelo.VerifStringMysql(lector, "id_articulo");
                    entidad.id_deposito = DalModelo.VerifStringMysql(lector, "id_deposito");
                    entidad.stock_minimo = DalModelo.VerifStringMysql(lector, "stock_minimo");
                    entidad.stock_maximo = DalModelo.VerifStringMysql(lector, "stock_maximo");
                    entidad.punto_pedido = DalModelo.VerifStringMysql(lector, "punto_pedido");
                    entidad.pasillo = DalModelo.VerifStringMysql(lector, "pasillo");
                    entidad.estanteria = DalModelo.VerifStringMysql(lector, "estanteria");
                    entidad.nivel = DalModelo.VerifStringMysql(lector, "nivel");
                    lista.Add(entidad);

                }

                lector.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }


            return lista;
        }


        public void Eliminar(Int32 v_id)
        {
            try
            {
                string cmdTxt = "delete from kx_deposito_reposicion  where Id='" + v_id + "'";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                cnn.ExecuteNonQuery(cmm);
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }

        }

        public Int32 maxId()
        {
            Int32 id = 0;

            try
            {
                string cmdTxt = " select max(Id) as cant from kx_deposito_reposicion  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {
                    id = DalModelo.VeriIntMysql(lector, "cant");
                }

                lector.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }

            return id;
        }

    }
}
