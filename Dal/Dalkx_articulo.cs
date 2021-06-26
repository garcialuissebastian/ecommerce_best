using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Be;
using MySql.Data.MySqlClient;
namespace Dal
{
    public class Dalkx_articulo
    {

        private MySqlConectarSqlDBVarias cnn = new MySqlConectarSqlDBVarias("kardex");
        MySqlCommand cmm;
        MySqlCommand cmm2;
        public void Alta(kx_articulo v_obj)
        { MySqlConnection cnn2 = new MySqlConnection();


            try
            {

               
               
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();


                string cmdTxt = " insert into kx_articulo (Tipo_Art, Nombre, Cod_Manual, Rubro, Sub_Rubro, Marca, Modelo, Barra_Proveedor, Barra_Sistema, Iva_Venta, IIBB, Moneda, Iva_Compra, Cont_Venta, Cont_Compra, P_Neto, P_Final, Util1, Util2, Util3, Util4, Util5, P_NetoL1, P_NetoL2, P_NetoL3, P_NetoL4, P_NetoL5, P_FinalL1, P_FinalL2, P_FinalL3, P_FinalL4, P_FinalL5,Unidad, Lote,Id_Usuario,es_Stock, Sistema, Aud_Ing) values ('" + v_obj.Tipo_Art + "', '" + v_obj.Nombre + "', '" + v_obj.Cod_Manual + "', '" + v_obj.Rubro + "', '" + v_obj.Sub_Rubro + "', '" + v_obj.Marca + "', '" + v_obj.Modelo + "', '" + v_obj.Barra_Proveedor + "', '" + v_obj.Barra_Sistema + "', '" + v_obj.Iva_Venta + "', '" + v_obj.IIBB + "', '" + v_obj.Moneda + "', '" + v_obj.Iva_Compra + "', '" + v_obj.Cont_Venta + "', '" + v_obj.Cont_Compra + "', '" + v_obj.P_Neto + "', '" + v_obj.P_Final + "', '" + v_obj.Util1 + "', '" + v_obj.Util2 + "', '" + v_obj.Util3 + "', '" + v_obj.Util4 + "', '" + v_obj.Util5 + "', '" + v_obj.P_NetoL1 + "', '" + v_obj.P_NetoL2 + "', '" + v_obj.P_NetoL3 + "', '" + v_obj.P_NetoL4 + "', '" + v_obj.P_NetoL5 + "', '" + v_obj.P_FinalL1 + "', '" + v_obj.P_FinalL2 + "', '" + v_obj.P_FinalL3 + "', '" + v_obj.P_FinalL4 + "', '" + v_obj.P_FinalL5 + "', '" + v_obj.Unidad + "', '" + v_obj.Lote + "', '" + v_obj.Id_Usuario + "', '" + v_obj.es_Stock + "', '" + v_obj.Sistema+ "', now())  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                cmm.ExecuteNonQuery();

                cmm = null;


                cmdTxt = " select max(Id) as cant from kx_articulo  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, ""); 

                MySqlDataReader lector = cmm.ExecuteReader();
                Int32 id = 0;
                while (lector.Read())
                {
                    id = DalModelo.VeriIntMysql(lector, "cant");
                }

                lector.Close();



                if(v_obj.Proveedores!=null){

                    foreach (var item in v_obj.Proveedores)
                    {

                        cmm = null;
                        cmdTxt = " insert into kx_articulo_proveedores (Id_Articulo,Id_Proveedor) values ('" + id+ "', '" + item.Id + "')  ";

                        cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                        cmm.ExecuteNonQuery();

                    } 
                }

                if (v_obj.Depositos != null)
                {

                    foreach (var item in v_obj.Depositos)
                    {

                        cmm = null;
                        cmdTxt = " insert into kx_deposito_reposicion (id_articulo, id_deposito, stock_minimo, stock_maximo, punto_pedido, pasillo, estanteria, nivel) values ( '" + id + "', '" + item.id_deposito + "', '" + item.stock_minimo + "', '" + item.stock_maximo + "', '" + item.punto_pedido + "', '" + item.pasillo + "', '" + item.estanteria + "', '" + item.nivel + "')  ";

                        cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                        cmm.ExecuteNonQuery();

                    }
                }




            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
                cnn2.Close();
            }

        }


        public void Modificacion(kx_articulo v_obj)
        {
            MySqlConnection cnn2 = new MySqlConnection();


            try
            {



                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();
                string cmdTxt = "update kx_articulo set    Aud_Mod=now(), es_Stock='" + v_obj.es_Stock + "' , Tipo_Art='" + v_obj.Tipo_Art + "' , Nombre='" + v_obj.Nombre + "' , Cod_Manual='" + v_obj.Cod_Manual + "' , Rubro='" + v_obj.Rubro + "' , Sub_Rubro='" + v_obj.Sub_Rubro + "' , Marca='" + v_obj.Marca + "' , Modelo='" + v_obj.Modelo + "' , Barra_Proveedor='" + v_obj.Barra_Proveedor + "' , Barra_Sistema='" + v_obj.Barra_Sistema + "' , Iva_Venta='" + v_obj.Iva_Venta + "' , IIBB='" + v_obj.IIBB + "' , Moneda='" + v_obj.Moneda + "' , Iva_Compra='" + v_obj.Iva_Compra + "' , Cont_Venta='" + v_obj.Cont_Venta + "' , Cont_Compra='" + v_obj.Cont_Compra + "' , P_Neto='" + v_obj.P_Neto + "' , P_Final='" + v_obj.P_Final + "' , Util1='" + v_obj.Util1 + "' , Util2='" + v_obj.Util2 + "' , Util3='" + v_obj.Util3 + "' , Util4='" + v_obj.Util4 + "' , Util5='" + v_obj.Util5 + "' , P_NetoL1='" + v_obj.P_NetoL1 + "' , P_NetoL2='" + v_obj.P_NetoL2 + "' , P_NetoL3='" + v_obj.P_NetoL3 + "' , P_NetoL4='" + v_obj.P_NetoL4 + "' , P_NetoL5='" + v_obj.P_NetoL5 + "' , P_FinalL1='" + v_obj.P_FinalL1 + "' , P_FinalL2='" + v_obj.P_FinalL2 + "' , P_FinalL3='" + v_obj.P_FinalL3 + "' , P_FinalL4='" + v_obj.P_FinalL4 + "' , P_FinalL5='" + v_obj.P_FinalL5 + "', Unidad='" + v_obj.Unidad + "' , Lote='" + v_obj.Lote + "'    where id ='" + v_obj.Id + "'   ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                cmm.ExecuteNonQuery();

                cmm = null;



                if (v_obj.Proveedores != null)
                {
                    cmm = null;
                    cmdTxt = " delete from kx_articulo_proveedores where   Id_Articulo='" + v_obj.Id + "' ";

                    cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                    cmm.ExecuteNonQuery();

                    foreach (var item in v_obj.Proveedores)
                    {
                     

                        cmm = null;
                        cmdTxt = " insert into kx_articulo_proveedores (Id_Articulo,Id_Proveedor) values ('" + v_obj.Id + "', '" + item.Id + "')  ";

                        cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                        cmm.ExecuteNonQuery();

                    }
                }

                if (v_obj.Depositos != null)
                {
                    cmm = null;
                    cmdTxt = " delete from kx_deposito_reposicion where id_articulo= '" + v_obj.Id + "'  ";

                    cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                    cmm.ExecuteNonQuery();

                    foreach (var item in v_obj.Depositos)
                    {

                        cmm = null;
                        cmdTxt = " insert into kx_deposito_reposicion (id_articulo, id_deposito, stock_minimo, stock_maximo, punto_pedido, pasillo, estanteria, nivel,stock) values ( '" + v_obj.Id + "', '" + item.id_deposito + "', '" + item.stock_minimo + "', '" + item.stock_maximo + "', '" + item.punto_pedido + "', '" + item.pasillo + "', '" + item.estanteria + "', '" + item.nivel + "', (select getSaldo( '" + v_obj.Id + "','" + item.id_deposito + "')))  ";

                        cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                        cmm.ExecuteNonQuery();

                    }
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
               // cnn.Close(cmm);
            }

        }

        public void Anular_articulo(string v_ids, string v_tipo)
        {
          
            MySqlConnection cnn2 = new MySqlConnection();
            cnn2 = cnn.MySqlCrearNuevaConexion();
            cnn2.Open();
            try
            {

                string cmdTxt = " update kx_articulo set Anulado='" + v_tipo + "' where Id='" + v_ids + "' ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
               cmm.ExecuteNonQuery();
                 

            }
            catch (Exception)
            {

                throw;
            }
            finally
            { 
                cnn2.Close();
            }
           

        }




        public string Barcod(string v_ids) {
            string sal = "0000000000000";
            MySqlConnection cnn2 = new MySqlConnection();
            cnn2 = cnn.MySqlCrearNuevaConexion();
            cnn2.Open();
            try
            {
               // string cmdTxt = "CodBar";
               
               // cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
               // cnn.AgregarParametroAComando(cmm, "v_art", v_ids);
               //sal = cmm.ExecuteScalar().ToString();



           string    cmdTxt = " select CodBar('"+v_ids+"') as cant ";

               cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
               MySqlDataReader lector2 = cmm.ExecuteReader();
                
               while (lector2.Read())
               {

                   sal= Convert.ToString(lector2["cant"]);

               }
               lector2.Close();

            }
            catch (Exception)
            {

                throw;
            }
            finally {
                cnn.Close(cmm);
                cnn2.Close();
            }
            return sal;
        
        }
        public List<kx_articulo> Listar()
        {

            List<kx_articulo> lista = new List<kx_articulo>();
            try
            {



                string cmdTxt = " select Id,Tipo_Art, Nombre, Cod_Manual, Rubro, Sub_Rubro, Marca, Modelo, Barra_Proveedor, Barra_Sistema, Iva_Venta, IIBB, Moneda, Iva_Compra, Cont_Venta, Cont_Compra, P_Neto, P_Final, Util1, Util2, Util3, Util4, Util5, P_NetoL1, P_NetoL2, P_NetoL3, P_NetoL4, P_NetoL5, P_FinalL1, P_FinalL2, P_FinalL3, P_FinalL4, P_FinalL5, es_Stock,Lote from kx_articulo  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    kx_articulo entidad = new kx_articulo();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Tipo_Art = DalModelo.VerifStringMysql(lector, "Tipo_Art");
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.Cod_Manual = DalModelo.VerifStringMysql(lector, "Cod_Manual");
                    entidad.Rubro = DalModelo.VerifStringMysql(lector, "Rubro");
                    entidad.Sub_Rubro = DalModelo.VerifStringMysql(lector, "Sub_Rubro");
                    entidad.Marca = DalModelo.VerifStringMysql(lector, "Marca");
                    entidad.Modelo = DalModelo.VerifStringMysql(lector, "Modelo");
                    entidad.Barra_Proveedor = DalModelo.VerifStringMysql(lector, "Barra_Proveedor");
                    entidad.Barra_Sistema = DalModelo.VerifStringMysql(lector, "Barra_Sistema");
                    entidad.Iva_Venta = DalModelo.VerifStringMysql(lector, "Iva_Venta");
                    entidad.IIBB = DalModelo.VerifStringMysql(lector, "IIBB");
                    entidad.Moneda = DalModelo.VerifStringMysql(lector, "Moneda");
                    entidad.Iva_Compra = DalModelo.VerifStringMysql(lector, "Iva_Compra");
                    entidad.Cont_Venta = DalModelo.VerifStringMysql(lector, "Cont_Venta");
                    entidad.Cont_Compra = DalModelo.VerifStringMysql(lector, "Cont_Compra");
                    entidad.P_Neto = DalModelo.VerifStringMysql(lector, "P_Neto");
                    entidad.P_Final = DalModelo.VerifStringMysql(lector, "P_Final");
                    entidad.Util1 = DalModelo.VerifStringMysql(lector, "Util1");
                    entidad.Util2 = DalModelo.VerifStringMysql(lector, "Util2");
                    entidad.Util3 = DalModelo.VerifStringMysql(lector, "Util3");
                    entidad.Util4 = DalModelo.VerifStringMysql(lector, "Util4");
                    entidad.Util5 = DalModelo.VerifStringMysql(lector, "Util5");
                    entidad.P_NetoL1 = DalModelo.VerifStringMysql(lector, "P_NetoL1");
                    entidad.P_NetoL2 = DalModelo.VerifStringMysql(lector, "P_NetoL2");
                    entidad.P_NetoL3 = DalModelo.VerifStringMysql(lector, "P_NetoL3");
                    entidad.P_NetoL4 = DalModelo.VerifStringMysql(lector, "P_NetoL4");
                    entidad.P_NetoL5 = DalModelo.VerifStringMysql(lector, "P_NetoL5");
                    entidad.P_FinalL1 = DalModelo.VerifStringMysql(lector, "P_FinalL1");
                    entidad.P_FinalL2 = DalModelo.VerifStringMysql(lector, "P_FinalL2");
                    entidad.P_FinalL3 = DalModelo.VerifStringMysql(lector, "P_FinalL3");
                    entidad.P_FinalL4 = DalModelo.VerifStringMysql(lector, "P_FinalL4");
                    entidad.P_FinalL5 = DalModelo.VerifStringMysql(lector, "P_FinalL5");
                    entidad.es_Stock = DalModelo.VerifStringMysql(lector, "es_Stock");
                    entidad.Lote = DalModelo.VerifStringMysql(lector, "Lote");
                    
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


        public List<Be.Combos> ListaLaboratorioCmb()
        {

            List<Be.Combos> lista = new List<Be.Combos>();
            try
            {

                string cmdTxt = " select id, cNombre from kx_laboratorios order by cNombre   ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    Be.Combos entidad = new Be.Combos();

                    entidad.valor = DalModelo.VerifStringMysql(lector, "id");
                    entidad.descripcion = DalModelo.VerifStringMysql(lector, "cNombre");

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

        public List<kx_articulo> Listar(string v_tipo, string v_valor, string v_user, string v_sis)
        {
            MySqlConnection cnn2 = new MySqlConnection();
            cnn2 = cnn.MySqlCrearNuevaConexion();
            cnn2.Open();

            List<kx_articulo> lista = new List<kx_articulo>();
            try
            {



                string cmdTxt = " select Id,Tipo_Art, Nombre, Cod_Manual,Rubro,getRubro(Rubro) RubroNombre, Sub_Rubro,getSub_Rubro(Sub_Rubro) Sub_RubroNombre, Marca,getMarca(Marca) MarcaNombre,getModelo(Modelo) ModeloNombre, Modelo, Barra_Proveedor, Barra_Sistema, Iva_Venta, IIBB, Moneda, Iva_Compra, Cont_Venta, Cont_Compra, P_Neto, P_Final, Util1, Util2, Util3, Util4, Util5, P_NetoL1, P_NetoL2, P_NetoL3, P_NetoL4, P_NetoL5, P_FinalL1, P_FinalL2, P_FinalL3, P_FinalL4, P_FinalL5 , Unidad, Lote,es_Stock,Anulado, DescripcionPrincipal,DescripcionSecundaria   from kx_articulo where Id_Usuario = '" + v_user + "' AND Anulado  <>'SI' and (Sistema='" + v_sis + "' or  Sistema is null) ";
                  if(v_tipo =="0"){
                      cmdTxt = " select Id,Tipo_Art, Nombre, Cod_Manual,Rubro,getRubro(Rubro) RubroNombre, Sub_Rubro,getSub_Rubro(Sub_Rubro) Sub_RubroNombre, Marca,getMarca(Marca) MarcaNombre,getModelo(Modelo) ModeloNombre, Modelo, Barra_Proveedor, Barra_Sistema, Iva_Venta, IIBB, Moneda, Iva_Compra, Cont_Venta, Cont_Compra, P_Neto, P_Final, Util1, Util2, Util3, Util4, Util5, P_NetoL1, P_NetoL2, P_NetoL3, P_NetoL4, P_NetoL5, P_FinalL1, P_FinalL2, P_FinalL3, P_FinalL4, P_FinalL5 , Unidad, Lote ,es_Stock,Anulado, DescripcionPrincipal,DescripcionSecundaria   from kx_articulo where Nombre like '" + v_valor + "%' and  Id_Usuario = '" + v_user + "' AND Anulado  <>'SI' and (Sistema='" + v_sis + "' or  Sistema is null)  order by Nombre";
                  }

                  if (v_tipo == "1")
                  {
                      cmdTxt = " select Id,Tipo_Art, Nombre, Cod_Manual,Rubro,getRubro(Rubro) RubroNombre, Sub_Rubro,getSub_Rubro(Sub_Rubro) Sub_RubroNombre, Marca,getMarca(Marca) MarcaNombre,getModelo(Modelo) ModeloNombre, Modelo, Barra_Proveedor, Barra_Sistema, Iva_Venta, IIBB, Moneda, Iva_Compra, Cont_Venta, Cont_Compra, P_Neto, P_Final, Util1, Util2, Util3, Util4, Util5, P_NetoL1, P_NetoL2, P_NetoL3, P_NetoL4, P_NetoL5, P_FinalL1, P_FinalL2, P_FinalL3, P_FinalL4, P_FinalL5, Unidad, Lote,es_Stock,Anulado, DescripcionPrincipal,DescripcionSecundaria    from kx_articulo where Id = '" + v_valor + "'  and  Id_Usuario = '" + v_user + "'  AND Anulado  <>'SI'   and (Sistema='" + v_sis + "' or  Sistema is null)  order by id desc";
                  }

                  if (v_tipo == "2")
                  {
                      cmdTxt = " select Id,Tipo_Art, Nombre, Cod_Manual,Rubro,getRubro(Rubro) RubroNombre, Sub_Rubro,getSub_Rubro(Sub_Rubro) Sub_RubroNombre, Marca,getMarca(Marca) MarcaNombre,getModelo(Modelo) ModeloNombre, Modelo, Barra_Proveedor, Barra_Sistema, Iva_Venta, IIBB, Moneda, Iva_Compra, Cont_Venta, Cont_Compra, P_Neto, P_Final, Util1, Util2, Util3, Util4, Util5, P_NetoL1, P_NetoL2, P_NetoL3, P_NetoL4, P_NetoL5, P_FinalL1, P_FinalL2, P_FinalL3, P_FinalL4, P_FinalL5 , Unidad, Lote ,es_Stock,Anulado, DescripcionPrincipal,DescripcionSecundaria   from kx_articulo where Cod_Manual = '" + v_valor + "'  and  Id_Usuario = '" + v_user + "'  AND Anulado  <>'SI' and (Sistema='" + v_sis + "' or  Sistema is null)   order by id desc";
                  }


                  if (v_tipo == "3")
                  {
                      cmdTxt = " select Id,Tipo_Art, Nombre, Cod_Manual,Rubro,getRubro(Rubro) RubroNombre, Sub_Rubro,getSub_Rubro(Sub_Rubro) Sub_RubroNombre, Marca,getMarca(Marca) MarcaNombre,getModelo(Modelo) ModeloNombre, Modelo, Barra_Proveedor, Barra_Sistema, Iva_Venta, IIBB, Moneda, Iva_Compra, Cont_Venta, Cont_Compra, P_Neto, P_Final, Util1, Util2, Util3, Util4, Util5, P_NetoL1, P_NetoL2, P_NetoL3, P_NetoL4, P_NetoL5, P_FinalL1, P_FinalL2, P_FinalL3, P_FinalL4, P_FinalL5, Unidad, Lote,es_Stock,Anulado, DescripcionPrincipal,DescripcionSecundaria    from kx_articulo where Barra_Proveedor = '" + v_valor + "'  and  Id_Usuario = '" + v_user + "'  AND Anulado  <>'SI'  and (Sistema='" + v_sis + "' or  Sistema is null)   order by id desc";
                  }


                  if (v_tipo == "4")
                  {// por rubro
                      cmdTxt = " select Id,Tipo_Art, Nombre, Cod_Manual,Rubro,getRubro(Rubro) RubroNombre, Sub_Rubro,getSub_Rubro(Sub_Rubro) Sub_RubroNombre, Marca,getMarca(Marca) MarcaNombre,getModelo(Modelo) ModeloNombre, Modelo, Barra_Proveedor, Barra_Sistema, Iva_Venta, IIBB, Moneda, Iva_Compra, Cont_Venta, Cont_Compra, P_Neto, P_Final, Util1, Util2, Util3, Util4, Util5, P_NetoL1, P_NetoL2, P_NetoL3, P_NetoL4, P_NetoL5, P_FinalL1, P_FinalL2, P_FinalL3, P_FinalL4, P_FinalL5, Unidad, Lote ,es_Stock,Anulado , DescripcionPrincipal,DescripcionSecundaria  from kx_articulo where Rubro = '" + v_valor + "'  and  Id_Usuario = '" + v_user + "'  AND Anulado  <>'SI' and (Sistema='" + v_sis + "' or  Sistema is null)  order by id desc";
                  }

                  if (v_tipo == "5")
                  {// por  subrubro
                      cmdTxt = " select Id,Tipo_Art, Nombre, Cod_Manual,Rubro,getRubro(Rubro) RubroNombre, Sub_Rubro,getSub_Rubro(Sub_Rubro) Sub_RubroNombre, Marca,getMarca(Marca) MarcaNombre,getModelo(Modelo) ModeloNombre, Modelo, Barra_Proveedor, Barra_Sistema, Iva_Venta, IIBB, Moneda, Iva_Compra, Cont_Venta, Cont_Compra, P_Neto, P_Final, Util1, Util2, Util3, Util4, Util5, P_NetoL1, P_NetoL2, P_NetoL3, P_NetoL4, P_NetoL5, P_FinalL1, P_FinalL2, P_FinalL3, P_FinalL4, P_FinalL5, Unidad, Lote,es_Stock,Anulado , DescripcionPrincipal,DescripcionSecundaria   from kx_articulo where Sub_Rubro = '" + v_valor + "'   and  Id_Usuario = '" + v_user + "' AND Anulado  <>'SI'  and (Sistema='" + v_sis + "' or  Sistema is null)   order by id desc";
                  }


                  if (v_tipo == "6")
                  {// por marca
                      cmdTxt = "select Id,Tipo_Art, Nombre, Cod_Manual,Rubro,getRubro(Rubro) RubroNombre, Sub_Rubro,getSub_Rubro(Sub_Rubro) Sub_RubroNombre, Marca,getMarca(Marca) MarcaNombre,getModelo(Modelo) ModeloNombre, Modelo, Barra_Proveedor, Barra_Sistema, Iva_Venta, IIBB, Moneda, Iva_Compra, Cont_Venta, Cont_Compra, P_Neto, P_Final, Util1, Util2, Util3, Util4, Util5, P_NetoL1, P_NetoL2, P_NetoL3, P_NetoL4, P_NetoL5, P_FinalL1, P_FinalL2, P_FinalL3, P_FinalL4, P_FinalL5, Unidad, Lote,es_Stock ,Anulado , DescripcionPrincipal,DescripcionSecundaria  from kx_articulo where Marca = '" + v_valor + "'   and  Id_Usuario = '" + v_user + "' AND Anulado  <>'SI' and (Sistema='" + v_sis + "' or  Sistema is null)   order by id desc";
                  }

                  if (v_tipo == "7")
                  {// por  mod
                      cmdTxt = " select Id,Tipo_Art, Nombre, Cod_Manual,Rubro,getRubro(Rubro) RubroNombre, Sub_Rubro,getSub_Rubro(Sub_Rubro) Sub_RubroNombre, Marca,getMarca(Marca) MarcaNombre,getModelo(Modelo) ModeloNombre, Modelo, Barra_Proveedor, Barra_Sistema, Iva_Venta, IIBB, Moneda, Iva_Compra, Cont_Venta, Cont_Compra, P_Neto, P_Final, Util1, Util2, Util3, Util4, Util5, P_NetoL1, P_NetoL2, P_NetoL3, P_NetoL4, P_NetoL5, P_FinalL1, P_FinalL2, P_FinalL3, P_FinalL4, P_FinalL5, Unidad, Lote,es_Stock ,Anulado , DescripcionPrincipal,DescripcionSecundaria  from kx_articulo where Modelo = '" + v_valor + "'  and  Id_Usuario = '" + v_user + "' AND Anulado  <>'SI'  and (Sistema='" + v_sis + "' or  Sistema is null)  order by id desc";
                  }

                  if (v_tipo == "99")
                  {
                      cmdTxt = "select Id,Tipo_Art, Nombre, Cod_Manual,Rubro,getRubro(Rubro) RubroNombre, Sub_Rubro,getSub_Rubro(Sub_Rubro) Sub_RubroNombre, Marca,getMarca(Marca) MarcaNombre,getModelo(Modelo) ModeloNombre, Modelo, Barra_Proveedor, Barra_Sistema, Iva_Venta, IIBB, Moneda, Iva_Compra, Cont_Venta, Cont_Compra, P_Neto, P_Final, Util1, Util2, Util3, Util4, Util5, P_NetoL1, P_NetoL2, P_NetoL3, P_NetoL4, P_NetoL5, P_FinalL1, P_FinalL2, P_FinalL3, P_FinalL4, P_FinalL5, Unidad, Lote,es_Stock,Anulado, DescripcionPrincipal,DescripcionSecundaria  from kx_articulo where Id ='" + v_valor + "'  and  Id_Usuario = '" + v_user + "'  AND Anulado  <>'SI'  and (Sistema='" + v_sis + "' or  Sistema is null)  ";
                  
                  }

                  if (v_tipo == "8")
                  {
                      cmdTxt = " select Id,Tipo_Art, Nombre, Cod_Manual,Rubro,getRubro(Rubro) RubroNombre, Sub_Rubro,getSub_Rubro(Sub_Rubro) Sub_RubroNombre, Marca,getMarca(Marca) MarcaNombre,getModelo(Modelo) ModeloNombre, Modelo, Barra_Proveedor, Barra_Sistema, Iva_Venta, IIBB, Moneda, Iva_Compra, Cont_Venta, Cont_Compra, P_Neto, P_Final, Util1, Util2, Util3, Util4, Util5, P_NetoL1, P_NetoL2, P_NetoL3, P_NetoL4, P_NetoL5, P_FinalL1, P_FinalL2, P_FinalL3, P_FinalL4, P_FinalL5, Unidad, Lote,es_Stock,Anulado, DescripcionPrincipal,DescripcionSecundaria    from kx_articulo where Barra_Sistema = '" + v_valor + "'  and  Id_Usuario = '" + v_user + "'  AND Anulado  <>'SI'  and (Sistema='" + v_sis + "' or  Sistema is null)  order by id desc";
                  }
                  cmm = cnn.MySqlCrearNuevoComando(cmdTxt+" limit 100", cnn2, ""); 

              

                MySqlDataReader lector =cmm.ExecuteReader();
                kx_articulo entidad2 = new kx_articulo();
                while (lector.Read())
                {

                    kx_articulo entidad = new kx_articulo();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");


                    entidad.Tipo_Art = DalModelo.VerifStringMysql(lector, "Tipo_Art");
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.DescripcionSecundaria = DalModelo.VerifStringMysql(lector, "DescripcionSecundaria");
                    entidad.DescripcionPrincipal = DalModelo.VerifStringMysql(lector, "DescripcionPrincipal");
                    entidad.Cod_Manual = DalModelo.VerifStringMysql(lector, "Cod_Manual");
                    entidad.Rubro = DalModelo.VerifStringMysql(lector, "Rubro");
                    entidad.Sub_Rubro = DalModelo.VerifStringMysql(lector, "Sub_Rubro");
                    entidad.Marca = DalModelo.VerifStringMysql(lector, "Marca");
                    entidad.Modelo = DalModelo.VerifStringMysql(lector, "Modelo");
                    entidad.Barra_Proveedor = DalModelo.VerifStringMysql(lector, "Barra_Proveedor");
                    entidad.Barra_Sistema = DalModelo.VerifStringMysql(lector, "Barra_Sistema");
                    entidad.Iva_Venta = DalModelo.VerifStringMysql(lector, "Iva_Venta").Replace(" ", ""); 
                    entidad.IIBB = DalModelo.VerifStringMysql(lector, "IIBB").Replace(" ", ""); 
                    entidad.Moneda = DalModelo.VerifStringMysql(lector, "Moneda");
                    entidad.Iva_Compra = DalModelo.VerifStringMysql(lector, "Iva_Compra");
                    entidad.Cont_Venta = DalModelo.VerifStringMysql(lector, "Cont_Venta");
                    entidad.Cont_Compra = DalModelo.VerifStringMysql(lector, "Cont_Compra");
                    entidad.P_Neto = DalModelo.VerifStringMysql(lector, "P_Neto").Replace(" ", "");
                    entidad.P_Final = DalModelo.VerifStringMysql(lector, "P_Final").Replace(" ", ""); 
                    entidad.Util1 = DalModelo.VerifStringMysql(lector, "Util1").Replace(" ", "");
                    entidad.Util2 = DalModelo.VerifStringMysql(lector, "Util2").Replace(" ", "");
                    entidad.Util3 = DalModelo.VerifStringMysql(lector, "Util3").Replace(" ", "");
                    entidad.Util4 = DalModelo.VerifStringMysql(lector, "Util4").Replace(" ", "");
                    entidad.Util5 = DalModelo.VerifStringMysql(lector, "Util5").Replace(" ", "");
                    entidad.P_NetoL1 = DalModelo.VerifStringMysql(lector, "P_NetoL1").Replace(" ", "");
                    entidad.P_NetoL2 = DalModelo.VerifStringMysql(lector, "P_NetoL2").Replace(" ", "");
                    entidad.P_NetoL3 = DalModelo.VerifStringMysql(lector, "P_NetoL3").Replace(" ", "");
                    entidad.P_NetoL4 = DalModelo.VerifStringMysql(lector, "P_NetoL4").Replace(" ", "");
                    entidad.P_NetoL5 = DalModelo.VerifStringMysql(lector, "P_NetoL5").Replace(" ", ""); 
                    entidad.P_FinalL1 = DalModelo.VerifStringMysql(lector, "P_FinalL1").Replace(" ", "");
                    entidad.P_FinalL2 = DalModelo.VerifStringMysql(lector, "P_FinalL2").Replace(" ", "");
                    entidad.P_FinalL3 = DalModelo.VerifStringMysql(lector, "P_FinalL3").Replace(" ", "");
                    entidad.P_FinalL4 = DalModelo.VerifStringMysql(lector, "P_FinalL4").Replace(" ", "");
                    entidad.P_FinalL5 = DalModelo.VerifStringMysql(lector, "P_FinalL5").Replace(" ", "");
                    entidad.Unidad = DalModelo.VerifStringMysql(lector, "Unidad").Replace(" ", ""); 
                    entidad.RubroNombre = DalModelo.VerifStringMysql(lector, "RubroNombre");
                    entidad.Sub_RubroNombre = DalModelo.VerifStringMysql(lector, "Sub_RubroNombre");
                    entidad.MarcaNombre = DalModelo.VerifStringMysql(lector, "MarcaNombre");
                    entidad.ModeloNombre = DalModelo.VerifStringMysql(lector, "ModeloNombre");
                    entidad.Lote = DalModelo.VerifStringMysql(lector, "Lote");
                    entidad.es_Stock = DalModelo.VerifStringMysql(lector, "es_Stock");
                    entidad.Anulado = DalModelo.VerifStringMysql(lector, "Anulado");

                    lista.Add(entidad);
                    entidad2 = entidad;
                } 

                lector.Close();
                if (v_tipo != "99" && lista.Count > 0)
                { 
                    foreach (var item in lista)
                    {

                        cmm = null;
                        cmdTxt = " select  id, id_articulo, id_deposito, stock_minimo, stock_maximo, punto_pedido, pasillo, estanteria, nivel, stock, getDepositoNombre(id_deposito) dep_nom from kx_deposito_reposicion where id_articulo= '" + item.Id + "'";

                        cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                        lector = cmm.ExecuteReader();
                        List<kx_deposito_reposicion> depLi = new List<kx_deposito_reposicion>();
                        while (lector.Read())
                        {
                            kx_deposito_reposicion entidadD = new kx_deposito_reposicion();

                            entidadD.id = DalModelo.VerifStringMysql(lector, "id");
                            entidadD.id_articulo = DalModelo.VerifStringMysql(lector, "id_articulo");
                            entidadD.id_deposito = DalModelo.VerifStringMysql(lector, "id_deposito");
                            entidadD.deposito_nombre = DalModelo.VerifStringMysql(lector, "dep_nom");
                            entidadD.stock_minimo = DalModelo.VerifStringMysql(lector, "stock_minimo");
                            entidadD.stock_maximo = DalModelo.VerifStringMysql(lector, "stock_maximo");
                            entidadD.punto_pedido = DalModelo.VerifStringMysql(lector, "punto_pedido");
                            entidadD.pasillo = DalModelo.VerifStringMysql(lector, "pasillo");
                            entidadD.estanteria = DalModelo.VerifStringMysql(lector, "estanteria");
                            entidadD.nivel = DalModelo.VerifStringMysql(lector, "nivel");
                            entidadD.stock = DalModelo.VerifStringMysql(lector, "stock");
                            depLi.Add(entidadD);
                        }
                        lector.Close();

                        item.Depositos = depLi;
                    }
                }
                if (v_tipo == "99" && lista.Count>0)
                {
                    
                    cmm2 = null;
                    cmdTxt = " select NombreRubro as cant from kx_rubro where id='" + entidad2.Rubro + "'";
                    cmm2 = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                    MySqlDataReader lector2 = cmm2.ExecuteReader();
                    while (lector2.Read())
                    {
                        entidad2.RubroNombre = DalModelo.VerifStringMysql(lector2, "cant");
                    }

                    lector2.Close();

                    cmm2 = null;
                    cmdTxt = " select NombreSubRubro as cant from kx_subrubro where id='" + entidad2.Sub_Rubro + "'";
                    cmm2 = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                    lector2 = cmm2.ExecuteReader();
                    while (lector2.Read())
                    {
                        entidad2.Sub_RubroNombre = DalModelo.VerifStringMysql(lector2, "cant");
                    }

                    lector2.Close();

                    cmm2 = null;
                    cmdTxt = " select Nombre as cant from kx_marca where id='" + entidad2.Marca + "'";
                    cmm2 = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                    lector2 = cmm2.ExecuteReader();
                    while (lector2.Read())
                    {
                        entidad2.MarcaNombre = DalModelo.VerifStringMysql(lector2, "cant");
                    }

                    lector2.Close();

                    cmm2 = null;
                    cmdTxt = " select Nombre as cant from kx_modelo where id='" + entidad2.Modelo + "'";
                    cmm2 = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                    lector2 = cmm2.ExecuteReader();
                    while (lector2.Read())
                    {
                        entidad2.ModeloNombre = DalModelo.VerifStringMysql(lector2, "cant");
                    }

                    lector2.Close();

                     cmm2 = null;
                     cmdTxt = "select kp.Id,kp.Cod_Manual, kp.Cuit, kp.Iva, kp.Tipo, kp.Denominacion, kp.Observacion, kp.Cta_contable, kp.Calle, kp.Numero, kp.Dpto, kp.Pais, kp.Cp, kp.Provincia, kp.Departamento, kp.Distrito, kp.Contac_Nomb, kp.Contac_Tel, kp.Contac_Cel, kp.Contac_Mail, kp.Emp_Tel, kp.Emp_Cel, kp.Emp_Mail, kp.Emp_Web from kx_proveedor kp, kx_articulo_proveedores ka where    ka.Id_Proveedor = kp.Id and ka.Id_Articulo = '" + entidad2.Id + "'"; 
               
                
                  cmm2 = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                    lector2 = cmm2.ExecuteReader();
                    while (lector2.Read())
                    {
                        kx_proveedor entidad = new kx_proveedor();
                        entidad.Id = DalModelo.VeriIntMysql(lector2, "Id");
                        entidad.Cod_Manual = DalModelo.VerifStringMysql(lector2, "Cod_Manual");
                        entidad.Cuit = DalModelo.VerifStringMysql(lector2, "Cuit");
                        entidad.Iva = DalModelo.VerifStringMysql(lector2, "Iva");
                        entidad.Tipo = DalModelo.VerifStringMysql(lector2, "Tipo");
                        entidad.Denominacion = DalModelo.VerifStringMysql(lector2, "Denominacion");
                        entidad.Observacion = DalModelo.VerifStringMysql(lector2, "Observacion");
                        entidad.Cta_contable = DalModelo.VerifStringMysql(lector2, "Cta_contable");
                        entidad.Calle = DalModelo.VerifStringMysql(lector2, "Calle");
                        entidad.Numero = DalModelo.VerifStringMysql(lector2, "Numero");
                        entidad.Dpto = DalModelo.VerifStringMysql(lector2, "Dpto");
                        entidad.Pais = DalModelo.VerifStringMysql(lector2, "Pais");
                        entidad.Cp = DalModelo.VerifStringMysql(lector2, "Cp");
                        entidad.Provincia = DalModelo.VerifStringMysql(lector2, "Provincia");
                        entidad.Departamento = DalModelo.VerifStringMysql(lector2, "Departamento");
                        entidad.Distrito = DalModelo.VerifStringMysql(lector2, "Distrito");
                        entidad.Contac_Nomb = DalModelo.VerifStringMysql(lector2, "Contac_Nomb");
                        entidad.Contac_Tel = DalModelo.VerifStringMysql(lector2, "Contac_Tel");
                        entidad.Contac_Cel = DalModelo.VerifStringMysql(lector2, "Contac_Cel");
                        entidad.Contac_Mail = DalModelo.VerifStringMysql(lector2, "Contac_Mail");
                        entidad.Emp_Tel = DalModelo.VerifStringMysql(lector2, "Emp_Tel");
                        entidad.Emp_Cel = DalModelo.VerifStringMysql(lector2, "Emp_Cel");
                        entidad.Emp_Mail = DalModelo.VerifStringMysql(lector2, "Emp_Mail");
                        entidad.Emp_Web = DalModelo.VerifStringMysql(lector2, "Emp_Web");
                        entidad2.Proveedores.Add(entidad);
                    }
                    lector2.Close();




                    cmm2 = null;
                    cmdTxt = " select  getDepositoNombre(id_deposito) dep_nom,  id, id_articulo, id_deposito, stock_minimo, stock_maximo, punto_pedido, pasillo, estanteria, nivel from kx_deposito_reposicion where id_articulo= '" + entidad2.Id + "'"; 

                    cmm2 = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                    lector2 = cmm2.ExecuteReader();
                    while (lector2.Read())
                    {
                        kx_deposito_reposicion entidad = new kx_deposito_reposicion();

                        entidad.id = DalModelo.VerifStringMysql(lector2, "id");
                        entidad.id_articulo = DalModelo.VerifStringMysql(lector2, "id_articulo");
                        entidad.id_deposito = DalModelo.VerifStringMysql(lector2, "id_deposito");
                        entidad.deposito_nombre = DalModelo.VerifStringMysql(lector2, "dep_nom");
                        entidad.stock_minimo = DalModelo.VerifStringMysql(lector2, "stock_minimo");
                        entidad.stock_maximo = DalModelo.VerifStringMysql(lector2, "stock_maximo");
                        entidad.punto_pedido = DalModelo.VerifStringMysql(lector2, "punto_pedido");
                        entidad.pasillo = DalModelo.VerifStringMysql(lector2, "pasillo");
                        entidad.estanteria = DalModelo.VerifStringMysql(lector2, "estanteria");
                        entidad.nivel = DalModelo.VerifStringMysql(lector2, "nivel");
                        entidad2.Depositos.Add(entidad);
                    }
                    lector2.Close();

                    lista.Clear();
                    lista.Add(entidad2);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
                cnn2.Close();
            }


            return lista;
        }


        public List<Kc_Lote> ListarStockFarmacia(string v_tipo, string v_valor, string v_user, string v_sis, string v_dep)

        {
            MySqlConnection cnn2 = new MySqlConnection();
            cnn2 = cnn.MySqlCrearNuevaConexion();
            cnn2.Open();

            List<Kc_Lote> list = new List<Kc_Lote>();
            try
            {
                string cmdTxt = "select  st.Lote,( IFNULL(sum(st.Entrada),0) - IFNULL(sum(st.Salida),0)) Saldo, getDepositoNombre(st.Id_Deposito) as dep, DATE_FORMAT(Vto, '%d/%m/%Y') vto, concat( ar.Cod_Manual,' - ', ar.Nombre ,' (', IFNULL(ar.DescripcionPrincipal,''),')') Articulo, lab.cNombre Laboratorio  , ar.Cod_Manual , ar.Id Id_Art, lab.Id  IdLab    ";
               cmdTxt += "  from kx_stock st, kx_articulo ar, kx_laboratorios lab where st.Id_Lab = lab.Id   and st.Id_Articulo=ar.Id and st.Id_Deposito= '" + v_dep + "' and  ";
                if (v_tipo == "0")
                {
                    cmdTxt += "   ar.Nombre like '" + v_valor + "%' and  ar.Id_Usuario = '" + v_user + "' AND ar.Anulado  <>'SI' and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)   ";
                }

                if (v_tipo == "1")
                {
                    cmdTxt += "  ar.Id = '" + v_valor + "'  and  ar.Id_Usuario = '" + v_user + "'  AND ar.Anulado  <>'SI'   and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)     ";
                }

                if (v_tipo == "2")
                {
                    cmdTxt += "  ar.Cod_Manual = '" + v_valor + "'  and  ar.Id_Usuario = '" + v_user + "'  AND ar.Anulado  <>'SI' and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)    ";
                }


                if (v_tipo == "3")
                {
                    cmdTxt += "   ar.Barra_Proveedor = '" + v_valor + "'  and  ar.Id_Usuario = '" + v_user + "'  AND ar.Anulado  <>'SI'  and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)     ";
                }


              

                if (v_tipo == "99")
                {
                    cmdTxt += "  ar.Id ='" + v_valor + "'  and  ar.Id_Usuario = '" + v_user + "'  AND ar.Anulado  <>'SI'  and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)    ";

                }

                if (v_tipo == "8")
                {
                    cmdTxt += "   ar.Barra_Sistema = '" + v_valor + "'  and  ar.Id_Usuario = '" + v_user + "'  AND ar.Anulado  <>'SI'  and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)    ";
                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt + "   group by   st.Lote,  st.Id_Deposito ,Vto, ar.Cod_Manual,  ar.Nombre ,  ar.DescripcionPrincipal, lab.cNombre   , ar.Cod_Manual , ar.Id  , lab.Id  having  ( IFNULL(sum(st.Entrada),0) - IFNULL(sum(st.Salida),0)) > 0   order by  month(Vto), DAY(Vto) , YEAR(Vto)  asc ", cnn2, "");
                 

                MySqlDataReader lector = cmm.ExecuteReader();
             
                while (lector.Read())
                {

                  Kc_Lote entidad = new Kc_Lote();
                   
                    entidad.Cant = DalModelo.VerifStringMysql(lector, "Saldo");
                    entidad.Dep_Desc = DalModelo.VerifStringMysql(lector, "dep");
                    entidad.Cod_Lot = DalModelo.VerifStringMysql(lector, "Lote");
                    entidad.Fecha_vto = DalModelo.VerifStringMysql(lector, "vto");
                    entidad.desc_Art = DalModelo.VerifStringMysql(lector, "Articulo");
                    entidad.desc_Lab = DalModelo.VerifStringMysql(lector, "Laboratorio");
                    entidad.Id_Art = DalModelo.VerifStringMysql(lector, "Id_Art");
                    entidad.Cod_Manual = DalModelo.VerifStringMysql(lector, "Cod_Manual");
                    entidad.Id_Lab = DalModelo.VerifStringMysql(lector, "IdLab");
                    list.Add(entidad);
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
                cnn2.Close();
            }


            return list;
        }


        public List<Kc_Lote> ListarStockFarmaciaAgrupado(string v_tipo, string v_valor, string v_user, string v_sis, string v_dep)

        {
            MySqlConnection cnn2 = new MySqlConnection();
            cnn2 = cnn.MySqlCrearNuevaConexion();
            cnn2.Open();

            List<Kc_Lote> list = new List<Kc_Lote>();
            try
            {
                string cmdTxt = " select  ( IFNULL(sum(st.Entrada),0) - IFNULL(sum(st.Salida),0)) Saldo  ,   concat( ar.Cod_Manual,' - ', ar.Nombre ,' (', ar.DescripcionPrincipal,')') Articulo,  ar.Cod_Manual , ar.Id Id_Art     ";
                cmdTxt += "  from kx_stock st, kx_articulo ar, kx_laboratorios lab where st.Id_Lab = lab.Id   and st.Id_Articulo=ar.Id and st.Id_Deposito= '" + v_dep + "' and  ";
                if (v_tipo == "0")
                {
                    cmdTxt += "   ar.Nombre like '" + v_valor + "%' and  ar.Id_Usuario = '" + v_user + "' AND ar.Anulado  <>'SI' and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)   ";
                }

                if (v_tipo == "1")
                {
                    cmdTxt += "  ar.Id = '" + v_valor + "'  and  ar.Id_Usuario = '" + v_user + "'  AND ar.Anulado  <>'SI'   and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)     ";
                }

                if (v_tipo == "2")
                {
                    cmdTxt += "  ar.Cod_Manual = '" + v_valor + "'  and  ar.Id_Usuario = '" + v_user + "'  AND ar.Anulado  <>'SI' and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)    ";
                }


                if (v_tipo == "3")
                {
                    cmdTxt += "   ar.Barra_Proveedor = '" + v_valor + "'  and  ar.Id_Usuario = '" + v_user + "'  AND ar.Anulado  <>'SI'  and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)     ";
                }




                if (v_tipo == "99")
                {
                    cmdTxt += "  ar.Id ='" + v_valor + "'  and  ar.Id_Usuario = '" + v_user + "'  AND ar.Anulado  <>'SI'  and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)    ";

                }

                if (v_tipo == "8")
                {
                    cmdTxt += "   ar.Barra_Sistema = '" + v_valor + "'  and  ar.Id_Usuario = '" + v_user + "'  AND ar.Anulado  <>'SI'  and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)    ";
                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt + "   group by       ar.Nombre ,  ar.DescripcionPrincipal    , ar.Cod_Manual , ar.Id     having  ( IFNULL(sum(st.Entrada),0) - IFNULL(sum(st.Salida),0)) > 0  ", cnn2, "");


                MySqlDataReader lector = cmm.ExecuteReader();

                while (lector.Read())
                {

                    Kc_Lote entidad = new Kc_Lote(); 
                    entidad.Cant = DalModelo.VerifStringMysql(lector, "Saldo"); 
                    entidad.desc_Art = DalModelo.VerifStringMysql(lector, "Articulo");
               
                    entidad.Id_Art = DalModelo.VerifStringMysql(lector, "Id_Art");
                    entidad.Cod_Manual = DalModelo.VerifStringMysql(lector, "Cod_Manual");
                 
                    list.Add(entidad);
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
                cnn2.Close();
            }


            return list;
        }

        public List<Kc_Lote> ListarStockFarmaciaRpTotal(string v_tipo, string v_valor, string v_user, string v_sis, string v_dep)

        {
            MySqlConnection cnn2 = new MySqlConnection();
            cnn2 = cnn.MySqlCrearNuevaConexion();
            cnn2.Open();

            List<Kc_Lote> list = new List<Kc_Lote>();
            try
            {
                string cmdTxt = " select   ( IFNULL(sum(st.Entrada),0) - IFNULL(sum(st.Salida),0)) Saldo, getDepositoNombre(st.Id_Deposito) as dep,   concat( ar.Cod_Manual,' - ', ar.Nombre ,' (',  IFNULL(ar.DescripcionPrincipal, ar.Nombre),')') Articulo  , ar.Cod_Manual , ar.Id Id_Art     ";
                cmdTxt += "  from kx_stock st, kx_articulo ar where     st.Id_Articulo=ar.Id and st.Id_Deposito='" + v_dep + "' and  ";

                if (v_tipo == "0")
                {
                    cmdTxt += "   ar.Nombre like '" + v_valor + "%' and  ar.Id_Usuario = '" + v_user + "' AND ar.Anulado  <>'SI' and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)   ";
                }

                if (v_tipo == "1")
                {
                    cmdTxt += "  ar.Id = '" + v_valor + "'  and  ar.Id_Usuario = '" + v_user + "'  AND ar.Anulado  <>'SI'   and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)     ";
                }

                if (v_tipo == "2")
                {
                    cmdTxt += "  ar.Cod_Manual = '" + v_valor + "'  and  ar.Id_Usuario = '" + v_user + "'  AND ar.Anulado  <>'SI' and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)    ";
                }


                if (v_tipo == "3")
                {
                    cmdTxt += "   ar.Barra_Proveedor = '" + v_valor + "'  and  ar.Id_Usuario = '" + v_user + "'  AND ar.Anulado  <>'SI'  and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)     ";
                }




                if (v_tipo == "99")
                {
                    cmdTxt += "  ar.Id ='" + v_valor + "'  and  ar.Id_Usuario = '" + v_user + "'  AND ar.Anulado  <>'SI'  and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)    ";

                }

                if (v_tipo == "8")
                {
                    cmdTxt += "   ar.Barra_Sistema = '" + v_valor + "'  and  ar.Id_Usuario = '" + v_user + "'  AND ar.Anulado  <>'SI'  and (ar.Sistema='" + v_sis + "' or  ar.Sistema is null)    ";
                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt + "   group by      st.Id_Deposito , ar.Cod_Manual,  ar.Nombre ,  ar.DescripcionPrincipal  , ar.Cod_Manual , ar.Id    having  ( IFNULL(sum(st.Entrada),0) - IFNULL(sum(st.Salida),0)) > 0   order by   ar.Nombre ", cnn2, "");


                MySqlDataReader lector = cmm.ExecuteReader();

                while (lector.Read())
                {

                    Kc_Lote entidad = new Kc_Lote();

                    entidad.Cant = DalModelo.VerifStringMysql(lector, "Saldo");
                    entidad.Dep_Desc = DalModelo.VerifStringMysql(lector, "dep"); 
                    entidad.desc_Art = DalModelo.VerifStringMysql(lector, "Articulo");
                 
                    entidad.Id_Art = DalModelo.VerifStringMysql(lector, "Id_Art");
                    entidad.Cod_Manual = DalModelo.VerifStringMysql(lector, "Cod_Manual");
               
                    list.Add(entidad);
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
                cnn2.Close();
            }


            return list;
        }


        public void WsModStock(string v_id, string v_dep, string v_cant, string v_motivo, string v_remito, string v_origen, string v_proveedor, string v_comentario, string v_tipo_ingreso, string v_lote, string v_vto)
        { 
          
        
         MySqlConnection cnn2 = new MySqlConnection();
            cnn2 = cnn.MySqlCrearNuevaConexion();
            cnn2.Open();

            try{
              //  v_motivo="INGRESO ARTICULO" ;
            string    cmdTxt = "wi271852_kardex.Stock_update";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
                cnn.AgregarParametroAComando(cmm, "v_tipo", "E");
                cnn.AgregarParametroAComando(cmm, "v_art", v_id);
                cnn.AgregarParametroAComando(cmm, "v_cant", v_cant);
                cnn.AgregarParametroAComando(cmm, "v_dep", v_dep);
                cnn.AgregarParametroAComando(cmm, "v_motivo", v_motivo);
                cnn.AgregarParametroAComando(cmm, "v_remito", v_remito.ToUpper());
                cnn.AgregarParametroAComando(cmm, "v_origen", v_origen.ToUpper());
                 cnn.AgregarParametroAComando(cmm, "v_proveedor",v_proveedor);
                  cnn.AgregarParametroAComando(cmm, "v_comentario",v_comentario.ToUpper());
                  cnn.AgregarParametroAComando(cmm, "v_tipo_ingreso", v_tipo_ingreso);
                  cnn.AgregarParametroAComando(cmm, "v_lote", v_lote);
                  cnn.AgregarParametroAComando(cmm, "v_vto", v_vto);
              
                cmm.ExecuteNonQuery();


             
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
                cnn2.Close();
            }

        
        
        
        }


        public void WsModStockLt(string v_id, string v_dep, string v_cant, string v_motivo, string v_remito, string v_origen, string v_proveedor, string v_comentario, string v_tipo_ingreso, string v_lote, string v_vto, string v_lab, string v_cbte)
        {


            MySqlConnection cnn2 = new MySqlConnection();
            cnn2 = cnn.MySqlCrearNuevaConexion();
            cnn2.Open();

            try
            {
                //  v_motivo="INGRESO ARTICULO" ;
                string cmdTxt = "wi271852_kardex.StockLt_update";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
                cnn.AgregarParametroAComando(cmm, "v_tipo", "E");
                cnn.AgregarParametroAComando(cmm, "v_art", v_id);
                cnn.AgregarParametroAComando(cmm, "v_cant", v_cant);
                cnn.AgregarParametroAComando(cmm, "v_dep", v_dep);
                cnn.AgregarParametroAComando(cmm, "v_motivo", v_motivo);
                cnn.AgregarParametroAComando(cmm, "v_remito", v_remito.ToUpper());
                cnn.AgregarParametroAComando(cmm, "v_origen", v_origen.ToUpper());
                cnn.AgregarParametroAComando(cmm, "v_proveedor", v_proveedor);
                cnn.AgregarParametroAComando(cmm, "v_comentario", v_comentario.ToUpper());
                cnn.AgregarParametroAComando(cmm, "v_tipo_ingreso", v_tipo_ingreso);
                cnn.AgregarParametroAComando(cmm, "v_lote", v_lote);
                cnn.AgregarParametroAComando(cmm, "v_vto", v_vto);
                cnn.AgregarParametroAComando(cmm, "v_lab",v_lab);
                cnn.AgregarParametroAComando(cmm, "v_cbte", v_cbte);
                cmm.ExecuteNonQuery();



            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
                cnn2.Close();
            }




        }

        public void Eliminar(Int32 v_id)
        {
            try
            {
                string cmdTxt = "delete from kx_articulo  where Id='" + v_id + "'";

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

        public DataSet ReportEgreso(string ids) {
            MySqlConnection cnn2 = new MySqlConnection();
            DataSet ds = new DataSet();
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();
              
                string cmdTxt = " select MOTIVO,getDepositoNombre(DEPOSITO_ORIGEN) DEPOSITO_ORIGEN,getDepositoNombre(DEPOSITO_DESTINO) DEPOSITO_DESTINO,REMITO,COMENTARIO,ID_USUARIO,DATE_FORMAT(AUD, '%d/%m/%y') AUD, ID from kx_egresos_lote   where  ";

                if (ids == "")
                {
                    cmdTxt += " ID =(select max(ID) from kx_egresos_lote ) ";
                }
                else {
                    cmdTxt += " ID ='"+ids+"'";
                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt); 
                ds.Tables.Add(cnn.ExecuteDataTable(cmm,"EgresoFarmacia"));
                 
                cmm = null;



                cmdTxt = " select ( SELECT concat( Cod_Manual,' - ' ,Nombre,' (',IFNULL(DescripcionPrincipal,'') ,')')  FROM  kx_articulo where Id = st.Id_Articulo) INSUMO, st.LOTE, DATE_FORMAT(st.Vto, '%d/%m/%y')  VTO, st.SALIDA CANT,  (select cNombre from kx_laboratorios where  id = st.Id_Lab) LABORATORIO        from kx_stock  st  where st.Tipo='S' AND st.Id_Cbate = '" + ds.Tables[0].Rows[0]["ID"] + "'";
 
                
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);
                ds.Tables.Add(cnn.ExecuteDataTable(cmm, "Grilla"));

                return ds;
            }
            catch (Exception)
            {

                throw;
            }



        }

        public DataSet ReportEgresoPedido(string ids, string pedido)
        {
            MySqlConnection cnn2 = new MySqlConnection();
            DataSet ds = new DataSet();
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = " select MOTIVO,getDepositoNombre(DEPOSITO_ORIGEN) DEPOSITO_ORIGEN,getDepositoNombre(DEPOSITO_DESTINO) DEPOSITO_DESTINO,REMITO,COMENTARIO,ID_USUARIO,DATE_FORMAT(AUD, '%d/%m/%y') AUD, ID from kx_egresos_lote   where  ";

                if (ids == "")
                {
                    cmdTxt += " ID =(select max(ID) from kx_egresos_lote ) ";
                }
                else
                {
                    cmdTxt += " ID ='" + ids + "'";
                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);
                ds.Tables.Add(cnn.ExecuteDataTable(cmm, "EgresoFarmacia"));

                cmm = null;



                cmdTxt = " select ( SELECT concat( Cod_Manual,' - ' ,Nombre,' (',IFNULL(DescripcionPrincipal,'') ,')')  FROM  kx_articulo where Id = st.Id_Articulo) INSUMO, st.LOTE, DATE_FORMAT(st.Vto, '%d/%m/%y')  VTO, st.SALIDA CANT,  (select cNombre from kx_laboratorios where  id = st.Id_Lab) LABORATORIO        from kx_stock  st  where st.Tipo='S' AND st.Id_Cbate = '" + ds.Tables[0].Rows[0]["ID"] + "'";


                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);
                ds.Tables.Add(cnn.ExecuteDataTable(cmm, "Grilla"));


                cmm = null;


// sin stock
                cmdTxt = "select ART_NOMBRE, (det.CANT - st.SALIDA ) total       from kx_pedidos_det det,   kx_stock  st   where  st.Tipo='S' AND st.Id_Cbate ='" + ds.Tables[0].Rows[0]["ID"] + "'  and det.ID_PEDIDO ='" + pedido + "'   ";

                cmdTxt += " and st.Id_Articulo=det.ART_ID and  (det.CANT - st.SALIDA ) > 0  union all  ";

                cmdTxt += " select ART_NOMBRE,  det.CANT total   from kx_pedidos_det det where det.ART_ID not in ( select st.Id_Articulo from kx_stock  st   where  st.Tipo='S' AND st.Id_Cbate ='" + ds.Tables[0].Rows[0]["ID"] + "'  ) and det.ID_PEDIDO ='" + pedido + "' ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);
                ds.Tables.Add(cnn.ExecuteDataTable(cmm, "GrillaSinStock"));

                return ds;
            }
            catch (Exception)
            {

                throw;
            }



        }

        public DataSet ReportPedidos(string ids)
        {
            MySqlConnection cnn2 = new MySqlConnection();
            DataSet ds = new DataSet();
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = " select (select upper(USUARIO) from mysql_usuarios where ID_USUARIO = pp.ID_USUARIO) MOTIVO,getDepositoNombre(DEPOSITO_ORIGEN) DEPOSITO_ORIGEN,getDepositoNombre(DEPOSITO_DESTINO) DEPOSITO_DESTINO,'' REMITO,COMENTARIO,ID_USUARIO,DATE_FORMAT(AUD, '%d/%m/%y') AUD, ID from kx_pedidos  pp where  ";

                if (ids == "")
                {
                    cmdTxt += " ID =(select max(ID) from kx_pedidos ) ";
                }
                else
                {
                    cmdTxt += " ID ='" + ids + "'";
                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);
                ds.Tables.Add(cnn.ExecuteDataTable(cmm, "EgresoFarmacia"));

                cmm = null;



                cmdTxt = " select  concat( Cod_Manual,' - ' , ART_NOMBRE ) INSUMO, ''  LOTE,  ''  VTO,  CANT, '' LABORATORIO        from kx_pedidos_det  st  where  ID_PEDIDO = '" + ds.Tables[0].Rows[0]["ID"] + "'";


                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);
                ds.Tables.Add(cnn.ExecuteDataTable(cmm, "Grilla"));

                return ds;
            }
            catch (Exception)
            {

                throw;
            }



        }



        public List<Be.Kc_Lote> VerPedidoDet(string ids)
        {

            List<Be.Kc_Lote> lista = new List<Be.Kc_Lote>();
            try
            {
                 
                string cmdTxt = " select   ID_PEDIDO ,  ART_ID ,  ART_NOMBRE ,  CANT ,  Cod_Manual  from kx_pedidos_det where   ID_PEDIDO = '"+ids+"'";
                 
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    Kc_Lote entidad = new Kc_Lote();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "ID_PEDIDO");
                    entidad.Id_Art = DalModelo.VerifStringMysql(lector, "ART_ID");
                    entidad.Cod_Manual= DalModelo.VerifStringMysql(lector, "Cod_Manual");
                    entidad.Cant = DalModelo.VerifStringMysql(lector, "CANT");
                    entidad.desc_Art = DalModelo.VerifStringMysql(lector, "ART_NOMBRE");
    
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

        public void egresoPedido(Kc_Lote_Cab kc)
        {
            MySqlConnection cnn2 = new MySqlConnection();

            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "insert into kx_pedidos ( DEPOSITO_ORIGEN,DEPOSITO_DESTINO, COMENTARIO,ID_USUARIO,AUD) values (  '" + kc.DEPOSITO + "','" + kc.DEPOSITO_DESTINO + "', '" + kc.COMENTARIO + "','" + kc.ID_USUARIO + "', NOW())";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                cmm.ExecuteNonQuery();

                Int32 sal = 0;
                cmm2 = null;
                cmdTxt = " select max(Id) cant from kx_pedidos ";
                cmm2 = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                MySqlDataReader lector2 = cmm2.ExecuteReader();
                while (lector2.Read())
                {
                    sal = DalModelo.VeriIntMysql(lector2, "cant");
                }

                lector2.Close();


                foreach (var item in kc.LIST_LOTES)
                {
                    cmm = null;


                    cmdTxt = " insert into kx_pedidos_det (ID_PEDIDO ,ART_ID,Cod_Manual, ART_NOMBRE ,CANT) values ('" + sal + "', '" + item.Id_Art + "', '" + item.Cod_Manual + "', '" + item.desc_Art + "' , '" + item.Cant + "') ";

                    cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
               
                     
                    cmm.ExecuteNonQuery();



                }

            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
                cnn.Close(cmm);
            }

        }
        public void actualizar_pedidos(string pedido, string estado ) {
            MySqlConnection cnn2 = new MySqlConnection();

            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = " update kx_pedidos set ESTADO ='" + estado + "', CBTE=(SELECT MAX(ID) FROM  kx_egresos_lote)    where ID =" + pedido;
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                cmm.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
                cnn.Close(cmm);
            }
        }

        public void egresoLote(Kc_Lote_Cab kc)
        {
            MySqlConnection cnn2 = new MySqlConnection();

            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "insert into kx_egresos_lote (MOTIVO,DEPOSITO_ORIGEN,DEPOSITO_DESTINO,REMITO,COMENTARIO,ID_USUARIO,AUD) values ( '" + kc.MOTIVO + "','" + kc.DEPOSITO + "','" + kc.DEPOSITO_DESTINO+ "','" + kc.REMITO + "','" + kc.COMENTARIO+ "','" + kc.ID_USUARIO + "', NOW())";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                cmm.ExecuteNonQuery();

                Int32 sal = 0;
                cmm2 = null;
                cmdTxt = " select max(Id) cant from kx_egresos_lote ";
                cmm2 = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                MySqlDataReader lector2 = cmm2.ExecuteReader();
                while (lector2.Read())
                {
                    sal = DalModelo.VeriIntMysql(lector2, "cant");
                }

                lector2.Close();


                foreach (var item in kc.LIST_LOTES)
                {
                    cmm = null;


                    cmdTxt = "wi271852_kardex.StockLt_update";

                    cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
                    cnn.AgregarParametroAComando(cmm, "v_tipo", "S");
                    cnn.AgregarParametroAComando(cmm, "v_art", item.Id_Art);
                    cnn.AgregarParametroAComando(cmm, "v_cant", item.Cant);
                    cnn.AgregarParametroAComando(cmm, "v_dep", item.Id_Dep);
                    cnn.AgregarParametroAComando(cmm, "v_motivo", item.Motivo ?? "");
                    cnn.AgregarParametroAComando(cmm, "v_remito", kc.REMITO);
                    cnn.AgregarParametroAComando(cmm, "v_origen", kc.ORIGEN ?? "");
                    cnn.AgregarParametroAComando(cmm, "v_proveedor", "");
                    cnn.AgregarParametroAComando(cmm, "v_comentario", "");
                    cnn.AgregarParametroAComando(cmm, "v_tipo_ingreso", "LOTE");
                    cnn.AgregarParametroAComando(cmm, "v_lote", item.Cod_Lot);
                    cnn.AgregarParametroAComando(cmm, "v_vto", item.Fecha_vto);
                    cnn.AgregarParametroAComando(cmm, "v_lab", item.Id_Lab);
                    cnn.AgregarParametroAComando(cmm, "v_cbte", sal);
                    cnn.AgregarParametroAComando(cmm, "v_dep_destino", kc.DEPOSITO_DESTINO ?? "");
                    

                    cmm.ExecuteNonQuery();



                }

            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
                cnn.Close(cmm);
            }

        }


        public void IngresoLote(Kc_Lote_Cab kc)
        {
            MySqlConnection cnn2 = new MySqlConnection();
           
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "insert into kx_ingresos_lote  (MOTIVO,DEPOSITO,REMITO,ORIGEN,ID_USUARIO,AUD) values ( '" + kc.MOTIVO + "','" + kc.DEPOSITO + "','" + kc.REMITO + "','" + kc.ORIGEN + "','" + kc.ID_USUARIO + "', NOW())";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                cmm.ExecuteNonQuery();

                Int32 sal = 0;
                cmm2 = null;
                cmdTxt = " select max(Id) cant from kx_ingresos_lote ";
                cmm2 = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
            MySqlDataReader   lector2 = cmm2.ExecuteReader();
                while (lector2.Read())
                {
                    sal = DalModelo.VeriIntMysql(lector2, "cant") ;
                }

                lector2.Close();


                foreach (var item in kc.LIST_LOTES)
                {
                    cmm = null;


                    cmdTxt = "wi271852_kardex.StockLt_update";

                    cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
                    cnn.AgregarParametroAComando(cmm, "v_tipo", "E");
                    cnn.AgregarParametroAComando(cmm, "v_art", item.Id_Art);
                    cnn.AgregarParametroAComando(cmm, "v_cant", item.Cant);
                    cnn.AgregarParametroAComando(cmm, "v_dep", item.Id_Dep);
                    cnn.AgregarParametroAComando(cmm, "v_motivo",item.Motivo);
                    cnn.AgregarParametroAComando(cmm, "v_remito",kc.REMITO );
                    cnn.AgregarParametroAComando(cmm, "v_origen", kc.ORIGEN.ToUpper());
                    cnn.AgregarParametroAComando(cmm, "v_proveedor","");
                    cnn.AgregarParametroAComando(cmm, "v_comentario","");
                    cnn.AgregarParametroAComando(cmm, "v_tipo_ingreso", "LOTE");
                    cnn.AgregarParametroAComando(cmm, "v_lote", item.Cod_Lot);
                    cnn.AgregarParametroAComando(cmm, "v_vto", item.Fecha_vto);
                    cnn.AgregarParametroAComando(cmm, "v_lab", item.Id_Lab);
                    cnn.AgregarParametroAComando(cmm, "v_cbte", sal);
                    cnn.AgregarParametroAComando(cmm, "v_dep_destino", kc.DEPOSITO_DESTINO ?? "");


                    cmm.ExecuteNonQuery();

              
                    
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
                cnn.Close(cmm);
            }

        }


        public Int32 maxId()
        {
            Int32 id = 0;

            try
            {
                string cmdTxt = " select max(Id) as cant from kx_articulo  ";

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


       // movimiento de stock

        public List<kx_stock> Listar_kx_stock(string v_tipo, string v_valor, string v_use)
        {

            List<kx_stock> lista = new List<kx_stock>();
            try
            { 

                string cmdTxt = " select sk.Id,art.Nombre as Id_Articulo,getDepositoNombre(sk.Id_Deposito) Id_Deposito, DATE_FORMAT(sk.Fecha, '%d/%m/%y') Fecha, sk.Entrada, sk.Salida,sk.Motivo, sk.Saldo from kx_stock sk, kx_articulo art where sk.Id_Articulo = art.Id   and art.Id_Usuario ='" + v_use + "'  ";

                if (v_tipo == "99")
                {
                    cmdTxt += "  and  Id='" + v_valor + "'   order by sk.Id desc ";
                }
                if (v_tipo == "0")
                {
                    cmdTxt += " and art.Nombre like '%" + v_valor + "%'   order by sk.Id desc  LIMIT 500";
                }
                if (v_tipo == "1")
                {
                    cmdTxt += " and art.Barra_Sistema like '%" + v_valor + "%'     order by sk.Id desc  LIMIT 500";
                }
                if (v_tipo == "3")
                {
                    cmdTxt += " and art.Barra_Proveedor like '%" + v_valor + "%'   order by sk.Id desc  LIMIT 500";
                }

                if (v_tipo == "4")
                {
                    cmdTxt += " and  sk.Id_Deposito in (select Id from kx_deposito where Nombre like '%" + v_valor + "%')   order by sk.Id desc  LIMIT 500";
                }
                //if (string.IsNullOrEmpty(v_valor))
                //{
                //    cmdTxt += "  order by sk.Id desc ";
                //}
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    kx_stock entidad = new kx_stock();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Id_Articulo = DalModelo.VerifStringMysql(lector, "Id_Articulo");
                    entidad.Id_Deposito = DalModelo.VerifStringMysql(lector, "Id_Deposito");
                    entidad.Fecha = DalModelo.VerifStringMysql(lector, "Fecha");
                    entidad.Entrada = DalModelo.VerifStringMysql(lector, "Entrada");
                    entidad.Salida = DalModelo.VerifStringMysql(lector, "Salida");
                    entidad.Motivo = DalModelo.VerifStringMysql(lector, "Motivo");
                    entidad.Saldo = DalModelo.VerifStringMysql(lector, "Saldo");
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

        public List<kx_stock> Listar_kx_stockFarmacia(string v_tipo, string v_valor, string v_use, string v_dep)
        {

            List<kx_stock> lista = new List<kx_stock>();
            try
            {

                string cmdTxt = " select sk.Id,concat( art.Cod_Manual,' - ', art.Nombre ,' (', IFNULL(art.DescripcionPrincipal,''),') ', lab.cNombre)  as Id_Articulo,getDepositoNombre(sk.Id_Deposito) Id_Deposito, DATE_FORMAT(sk.Fecha, '%d/%m/%y') Fecha, sk.Entrada, sk.Salida,sk.Motivo, sk.Saldo  from kx_stock sk, kx_articulo art , kx_laboratorios lab where sk.Id_Lab = lab.Id and  sk.Id_Articulo = art.Id   and art.Id_Usuario ='" + v_use + "' and  sk.Id_Deposito ='" + v_dep + "'  ";

                if (v_tipo == "99")
                {
                    cmdTxt += "  and  Id='" + v_valor + "'   order by sk.Id desc ";
                }
                if (v_tipo == "0") // nombre art
                {
                    cmdTxt += " and art.Nombre like '%" + v_valor + "%'   order by sk.Id desc  LIMIT 500";
                }
                if (v_tipo == "1")
                {
                    cmdTxt += " and art.Barra_Sistema like '%" + v_valor + "%'     order by sk.Id desc  LIMIT 500";
                }
                if (v_tipo == "3")
                {
                    cmdTxt += " and art.Barra_Proveedor like '%" + v_valor + "%'   order by sk.Id desc  LIMIT 500";
                }

                if (v_tipo == "4")
                {
                    cmdTxt += " and  sk.Id_Deposito in (select Id from kx_deposito where Nombre like '%" + v_valor + "%')   order by sk.Id desc  LIMIT 500";
                }

                if (v_tipo == "5")
                {
                    cmdTxt += " and art.Cod_Manual like '%" + v_valor + "%'   order by sk.Id desc  LIMIT 500";
                }
                
                //if (string.IsNullOrEmpty(v_valor))
                //{
                //    cmdTxt += "  order by sk.Id desc ";
                //}
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    kx_stock entidad = new kx_stock();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Id_Articulo = DalModelo.VerifStringMysql(lector, "Id_Articulo");
                    entidad.Id_Deposito = DalModelo.VerifStringMysql(lector, "Id_Deposito");
                    entidad.Fecha = DalModelo.VerifStringMysql(lector, "Fecha");
                    entidad.Entrada = DalModelo.VerifStringMysql(lector, "Entrada");
                    entidad.Salida = DalModelo.VerifStringMysql(lector, "Salida");
                    entidad.Motivo = DalModelo.VerifStringMysql(lector, "Motivo");
                    entidad.Saldo = DalModelo.VerifStringMysql(lector, "Saldo");
                  
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

        public List<Kc_Lote> ListarKc(string v_art) {
            try
            {
                List<Be.Kc_Lote> list = new List<Be.Kc_Lote>();
                string cmdTxt = " select Id,Lote,Saldo ,getDepositoNombre(Id_Deposito) as dep, DATE_FORMAT(Vto, '%d/%m/%Y') vto  from kx_stock where Id_Articulo = '" + v_art + "' order by   month(Vto), DAY(Vto) , year(Vto),Id_Deposito  asc ";
                
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    Kc_Lote entidad = new Kc_Lote();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Cant = DalModelo.VerifStringMysql(lector, "Saldo");
                    entidad.Dep_Desc = DalModelo.VerifStringMysql(lector, "dep");
                    entidad.Cod_Lot = DalModelo.VerifStringMysql(lector, "Lote");
                    entidad.Fecha_vto = DalModelo.VerifStringMysql(lector, "vto");
                 
                    list.Add(entidad);

                }

                lector.Close();

                return list;
            }
            catch (Exception)
            {
                
                throw;
            }
        
        }

        public List<Kc_Lote> ListarKcxDep(string v_art, string v_deo)
        {
            try
            {
                List<Be.Kc_Lote> list = new List<Be.Kc_Lote>();
              //  string cmdTxt = " select Id,Lote,Saldo ,getDepositoNombre(Id_Deposito) as dep, DATE_FORMAT(Vto, '%d/%m/%Y') vto  from kx_stock where Id_Articulo = '" + v_art + "' and Id_Deposito= '" + v_deo + "' order by  month(Vto), DAY(Vto) , YEAR(Vto)  asc";

                 string cmdTxt = " select  st.Lote, ( IFNULL(sum(st.Entrada),0) - IFNULL(sum(st.Salida),0)) Saldo ,getDepositoNombre(st.Id_Deposito) as dep, DATE_FORMAT(Vto, '%d/%m/%Y') vto, concat( ar.Cod_Manual,' - ', ar.Nombre ,' (', ar.DescripcionPrincipal,')') Articulo, lab.cNombre Laboratorio ";
                 cmdTxt += "  from kx_stock st, kx_articulo ar, kx_laboratorios lab where st.Id_Lab = lab.Id  and  st.Id_Articulo ='" + v_art + "' and st.Id_Articulo=ar.Id and st.Id_Deposito= '" + v_deo + "'  group by   st.Lote,  st.Id_Deposito ,Vto, ar.Cod_Manual,  ar.Nombre ,  ar.DescripcionPrincipal, lab.cNombre   , ar.Cod_Manual , ar.Id  , lab.Id  having  ( IFNULL(sum(st.Entrada),0) - IFNULL(sum(st.Salida),0)) > 0  order by  month(Vto), DAY(Vto) , YEAR(Vto)  asc limit 100  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    Kc_Lote entidad = new Kc_Lote();
                    //entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Cant = DalModelo.VerifStringMysql(lector, "Saldo");
                    entidad.Dep_Desc = DalModelo.VerifStringMysql(lector, "dep");
                    entidad.Cod_Lot = DalModelo.VerifStringMysql(lector, "Lote");
                    entidad.Fecha_vto = DalModelo.VerifStringMysql(lector, "vto");
                    entidad.desc_Lab = DalModelo.VerifStringMysql(lector, "Laboratorio");
                    entidad.desc_Art = DalModelo.VerifStringMysql(lector, "Articulo");
                   
                    list.Add(entidad);

                }

                lector.Close();

                return list;
            }
            catch (Exception)
            {

                throw;
            }

        }



        public void DeleteKc(string v_art)
        {
           


            MySqlConnection cnn2 = new MySqlConnection();
            cnn2 = cnn.MySqlCrearNuevaConexion();
            cnn2.Open();

            try
            { 
                string cmdTxt = "wi271852_kardex.StockLt_delete";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
                cnn.AgregarParametroAComando(cmm, "v_lote", v_art);
               

                cmm.ExecuteNonQuery();



            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
                cnn2.Close();
            }

        }



        public List<Kc_Lote_Cab> Listar_kx_pedidosFarmacia(string v_tipo, string v_valor, string userDep,bool esAdmin, string desde, string hasta)

        {

            List<Kc_Lote_Cab> lista = new List<Kc_Lote_Cab>();
            try
            {



                string cmdTxt = " select  ID, getDepositoNombre(DEPOSITO_ORIGEN) DEPOSITO_ORIGEN, getDepositoNombre(DEPOSITO_DESTINO) DEPOSITO_DESTINO,DEPOSITO_ORIGEN DEPOSITO_DESTINO2 , COMENTARIO, getUsuarioNombre(ID_USUARIO) ID_USUARIO, AUD  , ESTADO,CBTE  from kx_pedidos  ";

                string cmdTxt2 = "";
               

                if (esAdmin)
                {
                    cmdTxt2 = "    where  DEPOSITO_DESTINO =  ( select  id_dep from kx_deposito_usuario kd where      kd.id_usuario ='" + userDep + "' AND Principal ='SI' ) ";

                    if (v_tipo == "0")
                    {
                        cmdTxt2 += "    and ID_USUARIO in (select ID_USUARIO from mysql_usuarios where  USUARIO like  '" + v_valor + "%' )  ";
                    }
                    if (v_tipo == "1")
                    {
                        cmdTxt2 += "    and ID_USUARIO in (select ID_USUARIO from mysql_usuarios where  USUARIO like  '" + v_valor + "%' ) and AUD BETWEEN  STR_TO_DATE('" + desde + "' ,'%d/%m/%Y') AND  STR_TO_DATE('" + hasta + "' ,'%d/%m/%Y')    ";
                    }
                }
                else{
                    // USUARIO NORMAL

                    if (v_tipo == "0") //por usuario
                    {
                        cmdTxt2 += "    where   DEPOSITO_ORIGEN  =  ( select  id_dep from kx_deposito_usuario kd where      kd.id_usuario ='" + userDep + "' AND Principal ='SI' )  and ID_USUARIO in (select ID_USUARIO from mysql_usuarios where  USUARIO like  '" + v_valor + "%' )  ";
                    }

                    if (v_tipo == "1") //por fechas
                    {
                        cmdTxt2 += "    where   DEPOSITO_ORIGEN  =  ( select  id_dep from kx_deposito_usuario kd where      kd.id_usuario ='" + userDep + "' AND Principal ='SI' )  and ID_USUARIO in (select ID_USUARIO from mysql_usuarios where  USUARIO like  '" + v_valor + "%' )  AND AUD BETWEEN STR_TO_DATE('" + desde + "' ,'%d/%m/%Y') AND  STR_TO_DATE('" + hasta + "' ,'%d/%m/%Y')    ";
                    }

                }
             
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt+ cmdTxt2 + "  order by  ID desc ");

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    Kc_Lote_Cab entidad = new Kc_Lote_Cab();
                
                    entidad.ID = DalModelo.VerifStringMysql(lector, "ID");
                    entidad.DEPOSITO = DalModelo.VerifStringMysql(lector, "DEPOSITO_ORIGEN");
                    entidad.DEPOSITO_DESTINO = DalModelo.VerifStringMysql(lector, "DEPOSITO_DESTINO");
                    entidad.COMENTARIO = DalModelo.VerifStringMysql(lector, "COMENTARIO");
                    entidad.ID_USUARIO = DalModelo.VerifStringMysql(lector, "ID_USUARIO");
                    entidad.AUD = DalModelo.VerifStringMysql(lector, "AUD");
                    entidad.MOTIVO = DalModelo.VerifStringMysql(lector, "ESTADO");
                    entidad.REMITO = DalModelo.VerifStringMysql(lector, "DEPOSITO_DESTINO2");
                    entidad.ORIGEN= DalModelo.VerifStringMysql(lector, "CBTE");
                    
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

        public List<kx_articulo> Listar_kx_listas_precio_historial(string v_tipo, string v_valor, string v_user, string v_sis)

        {

            List<kx_articulo> lista = new List<kx_articulo>();
            try
            {



                string cmdTxt = " select  Id_Art, Nombre, Iva_Venta, Util1, Util2, Util3, Util4, Util5, P_NetoL1, P_NetoL2, P_NetoL3, P_NetoL4, P_NetoL5, P_FinalL1, P_FinalL2, P_FinalL3, P_FinalL4, P_FinalL5, Id_Usuario, Sistema,  DATE_FORMAT(Aud, '%d/%m/%y %H : %i') Aud,P_Neto,P_Final from kx_listas_precio_historial   where   Id_Usuario='" + v_user + "'  and  Sistema='" + v_sis + "'  ";

                if (v_tipo == "99")
                {
                    cmdTxt += "   and   Id='" + v_valor + "'    ";
                }
                if (v_tipo == "0" && !string.IsNullOrEmpty(v_valor))
                {
                    cmdTxt += "    and   Nombre like '" + v_valor + "%' order by     Id  desc  ";
                }
                if (string.IsNullOrEmpty(v_valor))
                {
                    cmdTxt += " order by     Id   desc  LIMIT 500; ";
                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    kx_articulo entidad = new kx_articulo();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id_Art");                   
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.Iva_Venta = DalModelo.VerifStringMysql(lector, "Iva_Venta");
                    entidad.Util1 = DalModelo.VerifStringMysql(lector, "Util1");
                    entidad.Util2 = DalModelo.VerifStringMysql(lector, "Util2");
                    entidad.Util3 = DalModelo.VerifStringMysql(lector, "Util3");
                    entidad.Util4 = DalModelo.VerifStringMysql(lector, "Util4");
                    entidad.Util5 = DalModelo.VerifStringMysql(lector, "Util5");
                    entidad.P_NetoL1 = DalModelo.VerifStringMysql(lector, "P_NetoL1");
                    entidad.P_NetoL2 = DalModelo.VerifStringMysql(lector, "P_NetoL2");
                    entidad.P_NetoL3 = DalModelo.VerifStringMysql(lector, "P_NetoL3");
                    entidad.P_NetoL4 = DalModelo.VerifStringMysql(lector, "P_NetoL4");
                    entidad.P_NetoL5 = DalModelo.VerifStringMysql(lector, "P_NetoL5");
                    entidad.P_FinalL1 = DalModelo.VerifStringMysql(lector, "P_FinalL1");
                    entidad.P_FinalL2 = DalModelo.VerifStringMysql(lector, "P_FinalL2");
                    entidad.P_FinalL3 = DalModelo.VerifStringMysql(lector, "P_FinalL3");
                    entidad.P_FinalL4 = DalModelo.VerifStringMysql(lector, "P_FinalL4");
                    entidad.P_FinalL5 = DalModelo.VerifStringMysql(lector, "P_FinalL5");
                    entidad.Id_Usuario = DalModelo.VerifStringMysql(lector, "Id_Usuario");
                    entidad.Sistema = DalModelo.VerifStringMysql(lector, "Sistema");
                    entidad.Anulado = DalModelo.VerifStringMysql(lector, "Aud");
                    entidad.P_Final = DalModelo.VerifStringMysql(lector, "P_Final");
                    entidad.P_Neto = DalModelo.VerifStringMysql(lector, "P_Neto");
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

    }
}
