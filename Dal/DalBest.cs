using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Be;
using System.Web;
using System.Data;
using System.IO;

namespace Dal
{



    public class DalBest
    {

        private MySqlConectarSqlDBVarias cnn = new MySqlConectarSqlDBVarias("best");
        MySqlCommand cmm;


        public  Combos envioLocal(string v_cp)
        {
            Combos cmbLocal = new Combos();
            string sal = "";
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();


                cmm = null;
                string cmdTxt = "select codigo_postal,importe ,Localidad , Promo_SinCargo from  envio_local where codigo_postal='" +v_cp + "' and  Estado=1  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                MySqlDataReader lector = cmm.ExecuteReader();
               
                while (lector.Read())
                {
                    if (Convert.ToBoolean(lector["Promo_SinCargo"]))
                    {
                        sal = "ENVIO LOCAL: GRATIS";
                        cmbLocal.descripcion = sal;
                        cmbLocal.valor = "0";
                    }
                    else
                    {
                        sal = "ENVIO LOCAL: $ " + DalModelo.VerifStringMysql(lector, "importe");
                        cmbLocal.descripcion = sal;
                        cmbLocal.valor = DalModelo.VerifStringMysql(lector, "importe");
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
            }
            return cmbLocal;

        }
        public Be.Best_UsuariosWeb iniciarSession(string mail)
        {
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();


                cmm = null;
                string cmdTxt = "select  id,nombre,apellido,fecha_nac,dni,  mail, tel,password from  usuarios_web where mail='" + mail + "' and  habilitado='S' ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                MySqlDataReader lector = cmm.ExecuteReader();
                Be.Best_UsuariosWeb entidad = new Best_UsuariosWeb();
                while (lector.Read())
                {

                    entidad.id = DalModelo.VerifStringMysql(lector, "id");
                    entidad.nombre = DalModelo.VerifStringMysql(lector, "nombre");
                    entidad.apellido = DalModelo.VerifStringMysql(lector, "apellido");
                    entidad.fecha_nac = DalModelo.VerifStringMysql(lector, "fecha_nac");
                    entidad.dni = DalModelo.VerifStringMysql(lector, "dni");
                    entidad.mail = DalModelo.VerifStringMysql(lector, "mail");
                    entidad.tel = DalModelo.VerifStringMysql(lector, "tel");
                    entidad.pass = DalModelo.VerifStringMysql(lector, "password");

                }
                //     HttpContext.Current.Session["UsuarioWeb"] = entidad;

                return entidad;
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }

        }
        public Be.Best_UsuariosWeb iniciarSessionAdmin(string mail)
        {
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();


                cmm = null;
                string cmdTxt = "select  id,nombre,apellido,fecha_nac,dni,  mail, tel,password from  usuarios_web where mail='" + mail + "' and  habilitado='S' and rol <> 'USUARIO_WEB' limit 1 ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                MySqlDataReader lector = cmm.ExecuteReader();
                Be.Best_UsuariosWeb entidad = new Best_UsuariosWeb();
                while (lector.Read())
                {

                    entidad.id = DalModelo.VerifStringMysql(lector, "id");
                    entidad.nombre = DalModelo.VerifStringMysql(lector, "nombre");
                    entidad.apellido = DalModelo.VerifStringMysql(lector, "apellido");
                    entidad.fecha_nac = DalModelo.VerifStringMysql(lector, "fecha_nac");
                    entidad.dni = DalModelo.VerifStringMysql(lector, "dni");
                    entidad.mail = DalModelo.VerifStringMysql(lector, "mail");
                    entidad.tel = DalModelo.VerifStringMysql(lector, "tel");
                    entidad.pass = DalModelo.VerifStringMysql(lector, "password");

                }
                //     HttpContext.Current.Session["UsuarioWeb"] = entidad;

                return entidad;
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }

        }


        public string InsertMp(mercadopagos fe)
        {
            string sal = "";
            try
            {

                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                cmm = null;
                string query = "insert into mercadopagos (   id_usuario,  token,  PaymentMethodId ,  installments ,  issuer_id ,  TransactionAmount,  Status,  PaymentTypeId,  Card_FirstSixDigits ,  Card_LastFourDigits , ";
                query += "    StatusDetail ,  FirstName ,  Email ,  LastName,transaction_amount_refunded, id_mp , date_approved ) values (";
                query += " '" + fe.id_usuario + "','" + fe.token + "','" + fe.PaymentMethodId + "','" + fe.installments + "','" + fe.issuer_id + "','" + fe.TransactionAmount+ "','" + fe.Status + "','" + fe.PaymentTypeId + "', '" + fe.Card_FirstSixDigits+ "','" + fe.Card_LastFourDigits + "' ,";
                query += " '" + fe.StatusDetail + "','" + fe.FirstName + "','" + fe.Email + "','" + fe.LastName + "','" + fe.transaction_amount_refunded + "','" + fe.id_mp + "' ,'" + fe.date_approved + "'  ) ";
                cmm = cnn.MySqlCrearNuevoComando(query, cnn2, "");
                cmm.ExecuteNonQuery();

               



                cmm = null;
                query = "select  max(id) cant from mercadopagos ";

                cmm = cnn.MySqlCrearNuevoComando(query, cnn2, "");
                MySqlDataReader lector = cmm.ExecuteReader();
                Be.Best_UsuariosWeb entidad = new Best_UsuariosWeb();
                while (lector.Read())
                { 
                    sal = DalModelo.VerifStringMysql(lector, "cant"); 
                }
                return sal;
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

        public mercadopagos BsqPago(string v_mp)
        {
            mercadopagos entidad = new mercadopagos();
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "select mp.id,id_usuario,  token,  PaymentMethodId ,  installments ,  issuer_id ,  TransactionAmount,  Status,  PaymentTypeId,  Card_FirstSixDigits ,  Card_LastFourDigits , ";
                cmdTxt += "    StatusDetail ,  FirstName ,  Email ,  LastName,transaction_amount_refunded, id_mp , date_approved,fc.id cbte, getDireccionDestino(fc.id) LugarEnvio,fc.TIPO_ENVIO TipoEnvio, fc.CostoEnvio from mercadopagos mp, factura fc where mp.id='" + v_mp + "' and fc.CbteInterno=mp.id ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
        
                MySqlDataReader lector = cmm.ExecuteReader();

                while (lector.Read())
                {

                    entidad.id = DalModelo.VerifStringMysql(lector, "id");
                    entidad.id_usuario = DalModelo.VerifStringMysql(lector, "id_usuario");
                    entidad.token = DalModelo.VerifStringMysql(lector, "token");
                    entidad.PaymentMethodId = DalModelo.VerifStringMysql(lector, "PaymentMethodId");                   

                    entidad.installments = DalModelo.VerifStringMysql(lector, "installments");
                    entidad.issuer_id = DalModelo.VerifStringMysql(lector, "issuer_id");
                    entidad.TransactionAmount = DalModelo.VerifStringMysql(lector, "TransactionAmount");
                    entidad.Status = DalModelo.VerifStringMysql(lector, "Status");
                    entidad.PaymentTypeId = DalModelo.VerifStringMysql(lector, "PaymentTypeId");//ptipo
                    entidad.Card_FirstSixDigits = DalModelo.VerifStringMysql(lector, "Card_FirstSixDigits");//pcat
                    entidad.Card_LastFourDigits = DalModelo.VerifStringMysql(lector, "Card_LastFourDigits");//ptipocat
                    entidad.StatusDetail = DalModelo.VerifStringMysql(lector, "StatusDetail");
                    entidad.FirstName = DalModelo.VerifStringMysql(lector, "FirstName");
                    entidad.Email = DalModelo.VerifStringMysql(lector, "Email");
                    entidad.LastName = DalModelo.VerifStringMysql(lector, "LastName");
                    entidad.transaction_amount_refunded = DalModelo.VerifStringMysql(lector, "transaction_amount_refunded");
                    entidad.id_mp = DalModelo.VerifStringMysql(lector, "id_mp");
                    entidad.date_approved = DalModelo.VerifStringMysql(lector, "date_approved");
                    entidad.cbte = DalModelo.VerifStringMysql(lector, "cbte");

                    entidad.LugarEnvio= DalModelo.VerifStringMysql(lector, "LugarEnvio");
                    entidad.TipoEnvio = DalModelo.VerifStringMysql(lector, "TipoEnvio");
                    entidad.CostoEnvio = DalModelo.VerifStringMysql(lector, "CostoEnvio");
                }

                lector.Close();
                
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }


            return entidad;

        }

        public List<Be.Best_articulos_carrito> ArtCarrito(string v_tipo, string v_valor)
        {

            List<Be.Best_articulos_carrito> lista = new List<Best_articulos_carrito>();
            try
            {

                string cmdTxt = " select Detalle , Cant ,Importe ,getImagen(Id_Articulo) imagen, getMontoEnvio(id_factura) envio from factura_item  where ";
 
                if (v_tipo == "0")
                {
                    cmdTxt += " id_factura =getIdFactura_by_mp('" + v_valor + "')   ";
                }

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    Best_articulos_carrito  entidad = new Best_articulos_carrito();

                    entidad.Descripcion= DalModelo.VerifStringMysql(lector, "Detalle");
                    entidad.cant = DalModelo.VerifStringMysql(lector, "cant");
                    entidad.imagen = DalModelo.VerifStringMysql(lector, "imagen");
                    entidad.precio = DalModelo.VerifStringMysql(lector, "Importe");
                    entidad.VolumenTotal = DalModelo.VerifStringMysql(lector, "envio");
                    


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
        public void  CarritoSt(List<Be.Best_articulos_carrito> v_valor)
        {
            try { 

            List<Be.Best_articulos_carrito> lista = new List<Best_articulos_carrito>();
            foreach (var item in v_valor)
            {

                string cmdTxt = " select stockActual  from stock where articulo_id='"+item.Id+ "' and sucursal_id='51'";
                    int sal = 0;

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {
                        sal = DalModelo.VeriIntMysql(lector, "stockActual");
                  }

                lector.Close();

                    if ((sal- Convert.ToInt32(item.cant) )  <0)
                    {
                        throw new Exception("UNOS DE LOS ARTICULOS SE ENCUENTRA SIN STOCK EN ESTE MOMENTO, SEPA DISCULPARNOS.");
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
            }


            
        }
        public List<mercadopagos> Listar_mercadopagos(string v_tipo, string v_valor) 
        {

            List<mercadopagos> lista = new List<mercadopagos>();
            try
            { 

                string cmdTxt = " select id, id_usuario, token, PaymentMethodId, installments, issuer_id, TransactionAmount, Status, PaymentTypeId, Card_FirstSixDigits, Card_LastFourDigits, StatusDetail, FirstName, Email, LastName, aud_mod_fec, cbte, transaction_amount_refunded, id_mp, date_approved from mercadopagos  where ";

                if (v_tipo == "99")
                {
                    cmdTxt += "     id='" + v_valor + "' ";
                }
                if (v_tipo == "0")
                {
                    cmdTxt += "  id_usuario='" + v_valor + "' and Status='approved' order by id desc ";
                }
                 
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    mercadopagos entidad = new mercadopagos();
                 
                    entidad.id = DalModelo.VerifStringMysql(lector, "id");
                    entidad.id_usuario = DalModelo.VerifStringMysql(lector, "id_usuario");
                    entidad.token = DalModelo.VerifStringMysql(lector, "token");
                    entidad.PaymentMethodId = DalModelo.VerifStringMysql(lector, "PaymentMethodId");
                    entidad.installments = DalModelo.VerifStringMysql(lector, "installments");
                    entidad.issuer_id = DalModelo.VerifStringMysql(lector, "issuer_id");
                    entidad.TransactionAmount = DalModelo.VerifStringMysql(lector, "TransactionAmount");
                    entidad.Status = DalModelo.VerifStringMysql(lector, "Status");
                    entidad.PaymentTypeId = DalModelo.VerifStringMysql(lector, "PaymentTypeId");
                    entidad.Card_FirstSixDigits = DalModelo.VerifStringMysql(lector, "Card_FirstSixDigits");
                    entidad.Card_LastFourDigits = DalModelo.VerifStringMysql(lector, "Card_LastFourDigits");
                    entidad.StatusDetail = DalModelo.VerifStringMysql(lector, "StatusDetail");
                    entidad.FirstName = DalModelo.VerifStringMysql(lector, "FirstName");
                    entidad.Email = DalModelo.VerifStringMysql(lector, "Email");
                    entidad.LastName = DalModelo.VerifStringMysql(lector, "LastName");
               
                    entidad.cbte = DalModelo.VerifStringMysql(lector, "cbte");
                    entidad.transaction_amount_refunded = DalModelo.VerifStringMysql(lector, "transaction_amount_refunded");
                    entidad.id_mp = DalModelo.VerifStringMysql(lector, "id_mp");
                    entidad.date_approved = DalModelo.VerifStringMysql(lector, "date_approved");
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
        public string Insert(factura fe)
        {


            try
            {

                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();
                Int32 VtaId = 0;
                string queryVta = "select NextVal('ventas', 51) cant ";
                cmm = cnn.MySqlCrearNuevoComando(queryVta, cnn2, "");
                MySqlDataReader dr1 = cmm.ExecuteReader();
                while (dr1.Read())
                {
                    VtaId = Convert.ToInt32( dr1["cant"]) ;
                }
                dr1.Close();


                cmm = null; 
                string query = "insert into factura (id,TipoCbte, TipoCbteStr,FechaDesde,FechaHasta,VtoPago,tipoDoc,Doc,Domicilio,Nombre	,respInscrip,exento,ConsumidorFinal,Total,NoRespo,Monotributo,";
                query += "     CondicionVenta,Contado	,CtaCte	,Treitadias,CAE	,VtoCae	,FechaCbte,Letra,PtoVta	,NumCbte,Cuit_Vendedor	,CodBarra,MonedaStr,Concepto,DocTipo_Afip, Neto, Estado_CAE, vImpNeto, vImpIVA , vImpTri , vIva21Neto, vIva21importe,   vIva105Neto , vIva105importe, ClienteIva ,   vIva0Neto , vIva0importe, vIva27Neto, vIva27importe	, ImpOpEx, ImpTrib, SubTotal, CbteInterno,errores ,envio_calle,  envio_nro,  envio_cp ,envio_piso ,envio_dpto ,  envio_provincia , envio_localidad ,envio_peso,envio_volumen,envio_valor,CostoEnvio,TIPO_ENVIO,SEC_SELECT_ID,SEC_SELECT_NOMBRE) values (";
                query += " " + VtaId + ", '" + fe.TipoCbte + "','" + fe.TipoCbteStr + "','" + fe.FechaDesde.ToString("dd/MM/yyyy") + "','" + fe.FechaHasta.ToString("dd/MM/yyyy") + "','" + fe.VtoPago + "','" + fe.tipoDoc + "','" + fe.Doc + "', getDomicilio(" + fe.Doc + "),'" + fe.Nombre + "','" + fe.respInscrip + "','" + fe.exento + "','" + fe.ConsumidorFinal + "','" + fe.Total + "','" + fe.NoRespo + "','" + fe.Monotributo + "',";
                query += " '" + fe.CondicionVenta + "','" + fe.Contado + "','" + fe.CtaCte + "','" + fe.Treitadias + "','" + fe.CAE + "','" + fe.VtoCae + "','" + fe.FechaCbte.ToString("dd/MM/yyyy") + "','" + fe.Letra + "','" + fe.PtoVta + "','" + fe.NumCbte + "','" + fe.Cuit + "','" + fe.CodBarra + "','" + fe.MonedaStr + "','" + fe.Concepto + "','" + fe.DocTipo_Afip + "','" + fe.Neto + "','" + fe.Estado_CAE + "'   ,'" + fe.vImpNeto + "','" + fe.vImpIVA + "','" + fe.vImpTri + "','" + fe.vIva21Neto + "','" + fe.vIva21importe + "','" + fe.vIva105Neto + "','" + fe.vIva105importe + "' ,'" + fe.ClienteIva + "','" + fe.vIva0Neto + "','" + fe.vIva0importe + "','" + fe.vIva27Neto + "','" + fe.vIva27importe + "','" + fe.ImpOpEx + "','" + fe.vImpTri + "','" + fe.SubTotal + "','" + fe.CbteInterno + "','" + fe.errores+ "' ,'" + fe.Envio.calle.ToUpper() + "' ,'" + fe.Envio.nro + "' ,'" + fe.Envio.cp + "','" + fe.Envio.piso + "','" + fe.Envio.dpto + "','" + fe.Envio.Provincia.ToUpper() + "' ,'" + fe.Envio.Localidad.ToUpper() + "' ,'" + fe.Envio.peso + "' ,'" + fe.Envio.volumen + "','" + fe.Envio.Monto + "','" + fe.Envio.CostoEnvio.Replace(",",".") + "','" + fe.Envio.TIPO_ENVIO  + "','" + fe.Envio.SEC_SELECT_ID  + "','" + fe.Envio.SEC_SELECT_NOMBRE  + "')";
                cmm = cnn.MySqlCrearNuevoComando(query, cnn2, "");
                cmm.ExecuteNonQuery();

                string id = "0";
                cmm = null;
                string queryId = "select max(id) id  from factura";
                cmm = cnn.MySqlCrearNuevoComando(queryId, cnn2, "");
                MySqlDataReader dr = cmm.ExecuteReader();
                while (dr.Read())
                { 
                    id = dr["id"].ToString(); 
                }
                dr.Close();


                cmm = null;
               query = "insert into ventas (id,sucursal_id,tipoComprobante_id,letra,credito,cliente_id,importeFactura,importeDescuento,importeNeto,importeIva1,importeIva2, ";
                query += "   importeTotal,porcentajeDescuento,autorizacion_id,estado,documentoAdicional, tipoVenta,aud_ing_por,ImportePercepcion,aud_mod_fec,sucursal) values (";
                query += "  '" + id + "',51,11,'B','" + id + "',(select id from  clientes where NroDocumento='" + fe.Doc + "'), '" + fe.Total+ "',0, '" + fe.Total + "',0,0,";
                query += " '" + fe.Total + "',0,0,'P',0,0,'WEB DOC: " + fe.Doc + "',0,now(),5)";
                cmm = cnn.MySqlCrearNuevoComando(query, cnn2, "");
                cmm.ExecuteNonQuery();


                cmm = null;
                query = "insert into caja (  id,sucursal_id ,credito ,  cliente_id ,concepto_id   ,fecha,importeNominal ,importeInteres ";
                query += "    , importe,formaPago_id,NroComprobante,estado   ,observaciones,aud_ing_fec,aud_ing_por ,aud_mod_fec ) values (";
                query += " NextVal('caja',51),51," + VtaId + ", (select id from  clientes where NroDocumento='" + fe.Doc + "'), 11,now(), '" + fe.Total + "','0', ";
                query += " '" + fe.Total + "',161, '" + fe.CbteInterno + "',1,'PAGO WEB MERCADO PAGO',NOW(),'WEB DOC: " + fe.Doc + "' , now() ) ";
                cmm = cnn.MySqlCrearNuevoComando(query, cnn2, "");
                cmm.ExecuteNonQuery();


                foreach (Be.ItemFact item in fe.Item)
                {
                    cmm = null;
                    string Queryitem = "insert into factura_item (id_factura,Detalle,Importe,Iva,Cant,ImpxUni,NetoImpxUni,NetoImpTotal,Id_Articulo, ImpInt) values ('" + id + "','" + item.Detalle + "','" + item.Importe + "','" + item.Iva + "','" + item.Cant + "','" + item.ImpxUni + "','" + item.NetoImpxUni + "','" + item.NetoImpTotal + "','" + item.Id_Articulo + "','" + item.ImpInt + "')";

                    cmm = cnn.MySqlCrearNuevoComando(Queryitem, cnn2, ""); 
                    cmm.ExecuteNonQuery();

                    cmm = null;
                    Queryitem = "INSERT INTO detalle_ventas (id, ventas_id,  sucursal_id, estado, credito, tipo, articulo_id, artCodigo, artTalle, precioVenta, cantidad, valorizacion,";
                    Queryitem += "porcentaje, precioAnterior, importeIva, autorizacion_id, aud_ing_por, aud_mod_por,aud_mod_fec,sucursal) VALUES ";
                    Queryitem += "(NextVal('ventas', 51),'" + id + "',51,'1','" + id + "','A', '" + item.Id_Articulo + "',(select codigo from articulos where id='" + item.Id_Articulo + "') , (select talle from articulos where id='" + item.Id_Articulo + "'), '" + item.ImpxUni + "','" + item.Cant + "', '" + item.Importe + "' , ";
                    Queryitem += " 0, '" + item.ImpxUni + "',  (cast('" + item.Importe + "' AS DECIMAL(10,2)) - cast('" + item.NetoImpTotal + "' AS DECIMAL(10,2))) , 0, 'WEB DOC: " + fe.Doc + "','',now(),5)";
                    cmm = cnn.MySqlCrearNuevoComando(Queryitem, cnn2, "");
                    cmm.ExecuteNonQuery();


                    cmm = null;
                    Queryitem = "spStockVtas";
                    cmm = cnn.MySqlCrearNuevoComando(Queryitem, cnn2, "SP");
                    cnn.AgregarParametroAComando(cmm, "particulo_id",   item.Id_Articulo );
                    cnn.AgregarParametroAComando(cmm, "pcant", item.Cant);
                    cnn.AgregarParametroAComando(cmm, "puser", "WEB DOC: "+fe.Doc);
                    
                    cmm.ExecuteNonQuery();



                }



                return id;

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

        public void Modificar_pwd(string v_mail, string v_pass)
        {

            try
            {
                string cmdTxt = "update  usuarios_web set password = '" + v_pass + "'";

                cmdTxt += " where   mail = '" + v_mail.ToUpper().Trim() + "' and habilitado = 'S'";

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

        public DataSet RpFc(string ids)
        {
    
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open(); 
                DataSet ds = new DataSet();
                //cab
                string cmdTxt = "select cfg.Cuit, cfg.DomicilioComercial,cfg.CondicionIVA CondicionIVA, cfg.RazonSocial,cfg.RazonSocial2 , cfg.IIBB, cfg.FechaIniActividad , fa.NumCbte CbteDesde, fa.PtoVta, fa.FechaCbte Fecha,Letra,TipoCbte 'Cod_Letra',concat( fa.tipoDoc,': ') AS Cli_Tipo_Doc, fa.ClienteIva Cli_Iva, fa.Nombre Cli_Nombre, fa.Domicilio Cli_Domicilio, fa.Cae,fa.VtoCae CaeVto, fa.vImpNeto Neto, fa.SubTotal SubTotal, fa.Total,fa.Doc Cli_DocNO, fa.CodBarra,fa.Estado_CAE Afip_Observacion, '' Cli_Provincia , '' Cli_Departamento,'' ImporteLetra, fa.Contado,'' Tj_Debito,'' Tj_Credito,fa.CtaCte,'' Cheque,'' Otra,'' Remito,fa.TreitaDias , fa.CondicionVenta, fa.Concepto , fa.ImpTrib , fa.ImpOpEx from factura fa, Config cfg where cfg.Id = fa.Config and fa.id='" + ids + "'";
                MySqlCommand cmm = new MySqlCommand(cmdTxt, cnn2);
                MySqlDataAdapter adaptador = new MySqlDataAdapter(cmm);
                DataTable tabla = new DataTable("cab");
                adaptador.Fill(tabla);
                ds.Tables.Add(tabla);
                cmm = null;
                // item
                cmdTxt = "select Detalle Nombre,  Id_Articulo, Cant,NetoImpxUni, Iva, NetoImpTotal,ImpxUni,Importe ImpTotal , ImpInt from factura_item where id_factura='" + ids + "'";
                cmm = new MySqlCommand(cmdTxt, cnn2);

                MySqlDataAdapter adaptador2 = new MySqlDataAdapter(cmm);
                DataTable tabla2 = new DataTable("det");
                adaptador2.Fill(tabla2);
                ds.Tables.Add(tabla2);
                cmm = null;
                // iva

                cmdTxt = " select vIva105Importe iva105, vIva21Importe iva21,vIva27Importe  iva27, vIva0Importe iva0   from factura where id='" + ids + "'";
                cmm = new MySqlCommand(cmdTxt, cnn2);

                MySqlDataAdapter adaptador3 = new MySqlDataAdapter(cmm);
                DataTable tabla3 = new DataTable("Iva");
                adaptador3.Fill(tabla3);
                ds.Tables.Add(tabla3);
            
                return ds;

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

        public void ConfitmarRegistrarcion( string token)
        {
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "update  usuarios_web set habilitado='S' WHERE  token='" + token + "' and  habilitado='N'";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
           Int32   cant=   cmm.ExecuteNonQuery();


                cmm = null;
                cmdTxt = "select  id,nombre,apellido,fecha_nac,dni,  mail, tel from  usuarios_web where token='" + token + "' ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                MySqlDataReader lector = cmm.ExecuteReader();
                Be.Best_UsuariosWeb entidad = new Best_UsuariosWeb();
                while (lector.Read())
                {

                    entidad.id = DalModelo.VerifStringMysql(lector, "id");
                    entidad.nombre = DalModelo.VerifStringMysql(lector, "nombre");
                    entidad.apellido = DalModelo.VerifStringMysql(lector, "apellido");
                    entidad.fecha_nac = DalModelo.VerifStringMysql(lector, "fecha_nac");
                    entidad.dni = DalModelo.VerifStringMysql(lector, "dni");
                    entidad.mail = DalModelo.VerifStringMysql(lector, "mail");
                    entidad.tel = DalModelo.VerifStringMysql(lector, "tel");

                }
                HttpContext.Current.Session["UsuarioWeb"] = entidad;
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }

        }
        public bool TieneUsuarioMail(string mail)
        {
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "select count(*) cant  from  usuarios_web where  mail ='" + mail + "' ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                MySqlDataReader lector = cmm.ExecuteReader();
                Int32 sal = 0;
                while (lector.Read())
                {
                    sal = DalModelo.VeriIntMysql(lector, "cant");
                }

                lector.Close();


                return (sal > 0);
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }

        }

        public List<Combos> tipos_talles(string v_talle)
        {
            List<Combos> list = new List<Combos>();
            try
            {
                string cmdTxt = "select UK,US,CM from talles where descripcion ='" + v_talle.ToUpper().Replace("-","").Replace("'", "").Trim() + "' limit 1 ";
  
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);
                Combos UK = new Combos();
                UK.descripcion = "UK";

                Combos US = new Combos();
                US.descripcion = "US";

                Combos CM = new Combos();
                CM.descripcion = "CM";

                while (lector.Read())
                {
                    UK.valor = DalModelo.VerifStringMysql(lector, "UK");
                    US.valor = DalModelo.VerifStringMysql(lector, "US");
                    CM.valor = DalModelo.VerifStringMysql(lector, "CM");
                }
                list.Add(UK);
                list.Add(US);
                list.Add(CM);
                //cerrar lector
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
            return list; 

        }

        public string existe_mail_hab(string v_mail)
        {
            string PP = "NO";
            try
            {
                string cmdTxt = "SELECT habilitado ";

                cmdTxt += " from usuarios_web  where   mail = '" + v_mail.ToUpper().Trim() + "' AND habilitado ='S' limit 1 ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);
                while (lector.Read())
                {
                    PP = DalModelo.VerifStringMysql(lector, "habilitado");
                }
                //cerrar lector
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
            return PP;




        }


        public bool TieneUsuario( string dni )
        {
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "select count(*) cant  from  usuarios_web where  dni ='" + dni + "' ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                MySqlDataReader lector = cmm.ExecuteReader();
                Int32 sal = 0;
                while (lector.Read())
                { 
                   sal= DalModelo.VeriIntMysql(lector, "cant"); 
                }

                lector.Close();


                return (sal > 0);
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }

        }
        public void Registrarcion(string nombre, string apellido, string fecha_nac, string dni, string mail, string password, string tel, string token, string domicilio)
        {
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();
              
                string cmdTxt = "insert into usuarios_web (nombre,  apellido,  fecha_nac, dni,   mail,   password, aud, token, tel,domicilio, rol ) values ('" + nombre + "', '" + apellido + "', '" + fecha_nac + "', '" + dni + "', '" + mail + "', '" + password + "', now() , '" + token + "', '" + tel + "' , '" + domicilio + "', 'USUARIO_WEB')";

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
            }

        }
        public Best_articulos_ventaId listaArticuloVentaId(int? vart)
        {
            Best_articulos_ventaId entidad = new Best_articulos_ventaId();
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "spArticulosVenta";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
                cnn.AgregarParametroAComando(cmm, "ptipo", "");
                cnn.AgregarParametroAComando(cmm, "pcat", "");
                cnn.AgregarParametroAComando(cmm, "ptipocat", "");
                cnn.AgregarParametroAComando(cmm, "part", intTostring(vart));
                MySqlDataReader lector = cmm.ExecuteReader();
               
                while (lector.Read())
                {
                    
                    entidad.Id = DalModelo.VerifStringMysql(lector, "id_articulo");
                    entidad.tipo_articulo = DalModelo.VerifStringMysql(lector, "tipo_articulo");
                    entidad.precio = DalModelo.VerifStringMysql(lector, "precio_venta");
                    entidad.Descripcion = DalModelo.VerifStringMysql(lector, "nombre_articulo");  // para que se vea bien
                    if (entidad.Descripcion.Length > 52)
                    {
                        entidad.Descripcion = DalModelo.VerifStringMysql(lector, "nombre_articulo").Substring(0, 52);
                    }
                    entidad.codigo= DalModelo.VerifStringMysql(lector, "codigo");
                    entidad.genero = DalModelo.VerifStringMysql(lector, "GENERO");
                    entidad.categoria = DalModelo.VerifStringMysql(lector, "CATEGORIA");
                    entidad.diciplina = DalModelo.VerifStringMysql(lector, "DISCIPLINA");
                    entidad.marca = DalModelo.VerifStringMysql(lector, "MARCA"); 
                    entidad.tipo_articulo_id = DalModelo.VerifStringMysql(lector, "tipo_articulo_id");//ptipo
                    entidad.clasificacion_id = DalModelo.VerifStringMysql(lector, "clasificacion_id");//pcat
                    entidad.tipo_clasificacion_id = DalModelo.VerifStringMysql(lector, "tipo_clasificacion_id");//ptipocat
                    entidad.Imagen1 = DalModelo.VerifStringMysql(lector, "Imagen1");
                    entidad.Imagen2 = DalModelo.VerifStringMysql(lector, "Imagen2");
                    entidad.Imagen3 = DalModelo.VerifStringMysql(lector, "Imagen3");
                    entidad.Imagen4 = DalModelo.VerifStringMysql(lector, "Imagen4");
                    entidad.Imagen5 = DalModelo.VerifStringMysql(lector, "Imagen5");
                    entidad.VolumenTotal = DalModelo.VerifStringMysql(lector, "VolumenTotal") == "" ? "0": DalModelo.VerifStringMysql(lector, "VolumenTotal");
                    entidad.largo_embalaje = DalModelo.VerifStringMysql(lector, "largo_embalaje");
                    entidad.ancho_embalaje = DalModelo.VerifStringMysql(lector, "ancho_embalaje");
                    entidad.alto_embalaje = DalModelo.VerifStringMysql(lector, "alto_embalaje");
                    if (entidad.VolumenTotal=="")
                    {
                        entidad.VolumenTotal = "0";
                    }
                    entidad.Peso = DalModelo.VerifStringMysql(lector, "Peso");
                    if (entidad.Peso == "")
                    {
                        entidad.Peso = "0";
                    }
                }

                lector.Close();
                lector = null;
                cmm = null;
                //cmdTxt = " select   a2.id, a2.talle, s.stockActual ,getTalleUK(a2.talle) UK,getTalleCM(a2.talle) CM,getTalleUS(a2.talle) US from articulos a1  ";
                //cmdTxt += " left join articulos a2 on a1.codigo=a2.codigo and a1.estado=1 ";
                //cmdTxt += "   and a2.id in (select articulo_id from articulos_clasificacion where articulo_id = a2.id ) "; 
                //cmdTxt += "   left join stock s on a2.id=s.articulo_id  ";
                //cmdTxt += " and s.stockActual > 0 "; // q tenga stock
                //cmdTxt += "  and a2.id in ( select articulo_id  from articulos_clasificacion where articulo_id=a2.id ) and a2.disponibleWeb= True "; // q este clasificado
                //cmdTxt += " and s.sucursal_id=51 where a1.id='"+entidad.Id+ "' and s.stockActual > 0  ";
                /////////////
                ///
                cmdTxt = "spStockTemp";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
                cnn.AgregarParametroAComando(cmm, "particulo_id",  entidad.Id);
              
                 lector = cmm.ExecuteReader();
               List< Best_articulos_venta_stock> Lstock = new List<Best_articulos_venta_stock>();
                while (lector.Read())
                { 
                    Best_articulos_venta_stock bstock = new Best_articulos_venta_stock();
                    var mStock = DalModelo.VerifStringMysql(lector, "stockActual");
                    if (mStock== "")
                    {
                        mStock = "0";
                    }
                    bstock.stock = DalModelo.VerifStringMysql(lector, "stockActual");
                    bstock.talle = DalModelo.VerifStringMysql(lector, "talle");
                    bstock.id_art = DalModelo.VerifStringMysql(lector, "id");
                    bstock.CM = DalModelo.VerifStringMysql(lector, "CM");
                    bstock.UK = DalModelo.VerifStringMysql(lector, "UK");
                    bstock.US = DalModelo.VerifStringMysql(lector, "US");
                    bstock.ARG = DalModelo.VerifStringMysql(lector, "ARG");
                    Lstock.Add(bstock);

                } 
                lector.Close();
                entidad.Talles = Lstock;
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }


            return entidad;

        }


        public List<Best_articulos_admin> BsqBest_articulos_admin(string cod)
        {
            List<Best_articulos_admin> Lista = new List<Best_articulos_admin>();
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();


                // generos
               string cmdTxt = "select     cc.id  ,  cc.descripcion   from articulos_clasificacion ac, clasificacion cc , tipos_clasificacion tc  ";
                cmdTxt += "   where    ac.clasificacion_id = cc.id   and tc.id =cc.tipo_clasificacion_id  and tc.descripcion= 'GENERO'   and ac.articulo_id   = ( select  min(id)  from articulos where  estado= 1 and  codigo='" + cod + "' )   ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                MySqlDataReader lector1 = cmm.ExecuteReader();
                List<Combos> Lgeneros = new List<Combos>();

                while (lector1.Read())
                {
                    Combos entidad = new Combos();
                    entidad.valor = DalModelo.VerifStringMysql(lector1, "id");
                    entidad.descripcion = DalModelo.VerifStringMysql(lector1, "descripcion");
                    Lgeneros.Add(entidad);
                }
                lector1.Close();


                cmm = null;

                // disciplinas
             cmdTxt = "select     cc.id  ,  cc.descripcion   from articulos_clasificacion ac, clasificacion cc , tipos_clasificacion tc  ";
                cmdTxt += "   where    ac.clasificacion_id = cc.id   and tc.id =cc.tipo_clasificacion_id  and tc.descripcion= 'DISCIPLINA'   and ac.articulo_id   = ( select  min(id)  from articulos where  estado= 1 and  codigo='" + cod + "' )   ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                 lector1 = cmm.ExecuteReader();
                List<Combos> Ldisciplina= new List<Combos>();

                while (lector1.Read())
                {
                    Combos entidad = new Combos();
                    entidad.valor = DalModelo.VerifStringMysql(lector1, "id");
                    entidad.descripcion = DalModelo.VerifStringMysql(lector1, "descripcion");
                    Ldisciplina.Add(entidad);
                }
                lector1.Close();


                cmm = null;

                cmdTxt = "select  id,codigo,talle,descripcion,tipo_articulo_id,disponibleWeb,etiquetaWeb,Imagen1,Imagen2,Imagen3,Imagen4,Imagen5,getCategoria(id) Categoria,getDisciplina(id) Disciplina,getGenero(id) Genero,getMarca(id) Marca , if( getStock(id) ='','0',getStock(id))  Stock,precio_venta  from articulos  where  estado= 1 and  codigo='" + cod + "'";
  
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                MySqlDataReader lector = cmm.ExecuteReader();
                while (lector.Read())
                {
                    Best_articulos_admin entidad = new Best_articulos_admin();
                    entidad.Id = DalModelo.VerifStringMysql(lector, "id");
                    entidad.codigo = DalModelo.VerifStringMysql(lector, "codigo");
                    entidad.precio = DalModelo.VerifStringMysql(lector, "precio_venta");
                    entidad.talle = DalModelo.VerifStringMysql(lector, "talle");
                    entidad.descripcion = DalModelo.VerifStringMysql(lector, "descripcion");
                    entidad.tipo_articulo_id = DalModelo.VerifStringMysql(lector, "tipo_articulo_id");
                    entidad.disponibleWeb = DalModelo.VerifStringMysql(lector, "disponibleWeb");
                    entidad.etiquetaWeb = DalModelo.VerifStringMysql(lector, "etiquetaWeb");
                    entidad.Imagen1 = DalModelo.VerifStringMysql(lector, "Imagen1");
                    entidad.Imagen2 = DalModelo.VerifStringMysql(lector, "Imagen2");
                    entidad.Imagen3 = DalModelo.VerifStringMysql(lector, "Imagen3");
                    entidad.Imagen4 = DalModelo.VerifStringMysql(lector, "Imagen4");
                    entidad.Imagen5 = DalModelo.VerifStringMysql(lector, "Imagen5");

                    entidad.categoria = DalModelo.VerifStringMysql(lector, "Categoria");
                  //  entidad.diciplina = DalModelo.VerifStringMysql(lector, "Disciplina");
                  //  entidad.genero = DalModelo.VerifStringMysql(lector, "Genero");
                    entidad.marca = DalModelo.VerifStringMysql(lector, "Marca");  
                           entidad.stock = DalModelo.VerifStringMysql(lector, "Stock");
                    entidad.ListaGeneros = Lgeneros;
                    entidad.ListaDiciplina = Ldisciplina;
                    Lista.Add(entidad);

                }

                lector.Close();

                cmm = null;

             
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }


            return Lista;

        }

        public List<Be.Combos> ListarTipoArticulos(string ids)
        {
            List<Be.Combos> Lista = new List<Be.Combos>();
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "select  id,descripcion from tipos_articulos  where  estado= 1 ";

                if (!string.IsNullOrEmpty(ids))
                {
                    cmdTxt += " and  id ='"+ ids +"' ";

                }
                

                cmdTxt += "  order by descripcion ";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                MySqlDataReader lector = cmm.ExecuteReader();
                while (lector.Read())
                {
                    Be.Combos entidad = new Be.Combos();
                    entidad.valor = DalModelo.VerifStringMysql(lector, "id");
                    entidad.descripcion = DalModelo.VerifStringMysql(lector, "descripcion");
                

                    Lista.Add(entidad);

                }

                lector.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }


            return Lista;

        }
        public List<Be.Combos> ListarEtiquetas(string ids)
        {
            List<Be.Combos> Lista = new List<Be.Combos>();
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "select  id,descripcion from etiquetas where  estado= 1 ";

                if (!string.IsNullOrEmpty(ids))
                {
                    cmdTxt += " and  id ='" + ids + "' ";

                }


                cmdTxt += "  order by descripcion ";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                MySqlDataReader lector = cmm.ExecuteReader();
                while (lector.Read())
                {
                    Be.Combos entidad = new Be.Combos();
                    entidad.valor = DalModelo.VerifStringMysql(lector, "id");
                    entidad.descripcion = DalModelo.VerifStringMysql(lector, "descripcion");


                    Lista.Add(entidad);

                }

                lector.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }


            return Lista;

        }


        public List<Be.Combos> ListaClasificacion(string tipo_clasificacion_id, string tipo_articulo_id)
          {// `tipo_clasificacion_id`   'genero-categoria-disciplina-marca',
            // `tipo_articulo_id`   'calzado_indumentaria-accesorios',
            List<Be.Combos> Lista = new List<Be.Combos>();
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "select  id,descripcion from clasificacion where  estado= 1 and tipo_clasificacion_id='"+ tipo_clasificacion_id + "' and tipo_articulo_id='" + tipo_articulo_id + "' order by descripcion ";
 
                 
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                MySqlDataReader lector = cmm.ExecuteReader();
                while (lector.Read())
                {
                    Be.Combos entidad = new Be.Combos();
                    entidad.valor = DalModelo.VerifStringMysql(lector, "id");
                    entidad.descripcion = DalModelo.VerifStringMysql(lector, "descripcion");


                    Lista.Add(entidad);

                }

                lector.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }


            return Lista;

        }

        public Best_articulos_ventaId  ArticuloVentaId(int? vart)
        {
            Best_articulos_ventaId entidad = new Best_articulos_ventaId();
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "spArticulosVenta";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
                cnn.AgregarParametroAComando(cmm, "ptipo", "");
                cnn.AgregarParametroAComando(cmm, "pcat", "");
                cnn.AgregarParametroAComando(cmm, "ptipocat", "");
                cnn.AgregarParametroAComando(cmm, "part", intTostring(vart));
                MySqlDataReader lector = cmm.ExecuteReader();

                while (lector.Read())
                {

                    entidad.Id = DalModelo.VerifStringMysql(lector, "id_articulo");
                    entidad.tipo_articulo = DalModelo.VerifStringMysql(lector, "tipo_articulo");
                    entidad.precio = DalModelo.VerifStringMysql(lector, "precio_venta");
                    entidad.Descripcion = DalModelo.VerifStringMysql(lector, "nombre_articulo");  // para que se vea bien
                    if (entidad.Descripcion.Length > 52)
                    {
                        entidad.Descripcion = DalModelo.VerifStringMysql(lector, "nombre_articulo").Substring(0, 52);
                    }

                    entidad.genero = DalModelo.VerifStringMysql(lector, "GENERO");
                    entidad.categoria = DalModelo.VerifStringMysql(lector, "CATEGORIA");
                    entidad.diciplina = DalModelo.VerifStringMysql(lector, "DISCIPLINA");
                    entidad.marca = DalModelo.VerifStringMysql(lector, "MARCA");
                 
                    entidad.tipo_articulo_id = DalModelo.VerifStringMysql(lector, "tipo_articulo_id");//ptipo
                    entidad.clasificacion_id = DalModelo.VerifStringMysql(lector, "clasificacion_id");//pcat
                    entidad.tipo_clasificacion_id = DalModelo.VerifStringMysql(lector, "tipo_clasificacion_id");//ptipocat

                }

                lector.Close();
                lector = null;
                cmm = null;
                
                
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }


            return entidad;

        }


        public List<Be.kx_cbtes> WsListarCompras(string v_tipoCbate, string v_tipo, string v_valor1, string v_valor2, string v_user, string v_conf)

        {

            List<kx_cbtes> lista = new List<kx_cbtes>();
            try
            {
                string cmdTxt = " SELECT distinct cb.Id, cb.TipoCbte CantReg, TipoCbteStr CbteTipo, cb.PtoVta,   cb.FechaDesde  Fecha,''  usuario, cb. Estado_CAE Resultado,'' Vendedor, cb.Doc  Cliente, cb.aud_mod_fec  aud, cb.Concepto, cb.DocTipo_Afip DocTipo, concat( cb.tipoDoc, ' ', cb.Doc ) DocNro, concat( CAST(LPAD(cb.PtoVta,4,'0') AS CHAR),'-', CAST(LPAD(cb.NumCbte,8,'0')AS CHAR))   CbteDesde, cb.NumCbte,DATE_FORMAT( cb.FechaDesde, '%d/%m/%y' )  CbteFch, formaPrecioCbte(cb.Total , cb.Id)    ImpTotal, '0' ImpTotConc,formaPrecioCbte(cb.vImpNeto , cb.Id) ImpNeto, formaPrecioCbte(cb.ImpOpEx , cb.Id)  ImpOpEx, formaPrecioCbte(cb.ImpTrib , cb.Id)   ImpTrib,formaPrecioCbte((cb.vIva21importe + cb.vIva105importe + cb.vIva27importe ), cb.Id)    ImpIVA,'' MonId,''  MonCotiz,'' CAE, '' CAEFchVto,getIdMp_by_factura(cb.Id) Observaciones FROM  factura cb   ";

                if (v_tipo == "1")
                {
                    //    cmdTxt = " select Id,CantReg, CbteTipo, PtoVta,DATE_FORMAT(Fecha,  '%d/%m/%y') Fecha, usuario, Resultado, Vendedor, Cliente, aud, Concepto, DocTipo, DocNro, CbteDesde, CbteHasta, CbteFch, ImpTotal, ImpTotConc, ImpNeto, ImpOpEx, ImpTrib, ImpIVA, MonId, MonCotiz, CAE, CAEFchVto, Observaciones from kx_cbtes where CbteDesde 	='" + v_valor1 + "' and  CbteTipo ='" + v_tipoCbate + "' order by CbteHasta desc";

                    cmdTxt += "  WHERE   NumCbte 	like '%" + v_valor1 + "%'  and  ( cb.TipoCbte  ='" + v_tipoCbate + "' OR 99= '" + v_tipoCbate + "' ) and Estado_CAE= 'AUTORIZADO'     ORDER BY  month( cb.FechaDesde),day( cb.FechaDesde) DESC ";

                }
                if (v_tipo == "2")
                {
                    cmdTxt += "   WHERE  ( cb.TipoCbte  ='" + v_tipoCbate + "' OR 99= '" + v_tipoCbate + "' ) and Estado_CAE = 'AUTORIZADO'   and  STR_TO_DATE(cb.FechaDesde, '%d/%m/%y' )  between STR_TO_DATE( '" + v_valor1 + "', '%Y-%m-%d' ) AND STR_TO_DATE( '" + v_valor2 + "', '%Y-%m-%d' )  ORDER BY month(STR_TO_DATE(cb.FechaDesde, '%d/%m/%y' )),day( STR_TO_DATE(cb.FechaDesde, '%d/%m/%y' )) desc   ";


                }
                if (v_tipo == "3")
                {

                    cmdTxt += "  WHERE  Doc like '%" + v_valor1 + "%' AND    ( cb.TipoCbte  ='" + v_tipoCbate + "' OR 99= '" + v_tipoCbate + "' ) and Estado_CAE = 'AUTORIZADO'   ORDER BY month(STR_TO_DATE(cb.FechaDesde, '%d/%m/%y' )),day( STR_TO_DATE(cb.FechaDesde, '%d/%m/%y' )) desc   ";

                }

                if (v_tipo == "4")
                {

                    cmdTxt += "   WHERE  Nombre like '%" + v_valor1 + "%' AND    ( cb.TipoCbte  ='" + v_tipoCbate + "' OR 99= '" + v_tipoCbate + "' ) and Estado_CAE = 'AUTORIZADO'     ORDER BY month(STR_TO_DATE(cb.FechaDesde, '%d/%m/%y' )),day( STR_TO_DATE(cb.FechaDesde, '%d/%m/%y' )) desc   ";

                }
             



                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    kx_cbtes entidad = new kx_cbtes();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.CantReg = DalModelo.VerifStringMysql(lector, "CantReg");
                    entidad.CbteTipo = DalModelo.VerifStringMysql(lector, "CbteTipo");
                    entidad.PtoVta = DalModelo.VerifStringMysql(lector, "PtoVta");
                    entidad.Fecha = DalModelo.VerifStringMysql(lector, "Fecha");
                    entidad.usuario = DalModelo.VerifStringMysql(lector, "usuario");
                    entidad.Resultado = DalModelo.VerifStringMysql(lector, "Resultado");
                    entidad.Vendedor = DalModelo.VerifStringMysql(lector, "Vendedor");
                    entidad.Cliente = DalModelo.VerifStringMysql(lector, "Cliente");
                    entidad.aud = DalModelo.VerifStringMysql(lector, "aud");
                    entidad.Concepto = DalModelo.VerifStringMysql(lector, "Concepto");
                    entidad.DocTipo = DalModelo.VerifStringMysql(lector, "DocTipo");
                    entidad.DocNro = DalModelo.VerifStringMysql(lector, "DocNro");
                    entidad.CbteDesde = DalModelo.VerifStringMysql(lector, "CbteDesde");
                   // entidad.CbteHasta = DalModelo.VerifStringMysql(lector, "CbteHasta");
                    entidad.CbteFch = DalModelo.VerifStringMysql(lector, "CbteFch");
                    entidad.ImpTotal = DalModelo.VerifStringMysql(lector, "ImpTotal").Replace(" ", "");
                    entidad.ImpTotConc = DalModelo.VerifStringMysql(lector, "ImpTotConc").Replace(" ", "");
                    entidad.ImpNeto = DalModelo.VerifStringMysql(lector, "ImpNeto").Replace(" ", "");
                    entidad.ImpOpEx = DalModelo.VerifStringMysql(lector, "ImpOpEx").Replace(" ", "");
                    entidad.ImpTrib = DalModelo.VerifStringMysql(lector, "ImpTrib").Replace(" ", "");
                    entidad.ImpIVA = DalModelo.VerifStringMysql(lector, "ImpIVA").Replace(" ", "");
                    entidad.MonId = DalModelo.VerifStringMysql(lector, "MonId");
                    entidad.MonCotiz = DalModelo.VerifStringMysql(lector, "MonCotiz");
                    entidad.CAE = DalModelo.VerifStringMysql(lector, "CAE");
                    entidad.CAEFchVto = DalModelo.VerifStringMysql(lector, "CAEFchVto");
                    entidad.Observaciones = DalModelo.VerifStringMysql(lector, "Observaciones");
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

        public Dictionary<string, List<string>> Wscttiventas(string v_tipoCbate, string v_tipo, string v_valor1, string v_valor2, string v_user, string v_conf)
        {
            try
            {
                List<string> listAlic = new List<string>();
                List<string> listVentas = new List<string>();
                var diccionario = new Dictionary<string, List<string>>();
                string cmdTxt = "";

                cmdTxt = "select concat(LPAD(cb.TipoCbte, 3, 0)  ,LPAD(cb.PtoVta, 5, 0) ,LPAD(NumCbte, 20, 0) ,LPAD(REPLACE(REPLACE(FormaPrecio( cb.vIva21Neto),'.',''),',','') , 15, 0) ,LPAD('5', 4, 0) ,LPAD(REPLACE(REPLACE(FormaPrecio(cb.vIva21importe),'.',''),',','') , 15, 0) )  sal  from factura cb   WHERE  ( cb.TipoCbte  ='" + v_tipoCbate + "' OR 99= '" + v_tipoCbate + "' ) and Estado_CAE = 'AUTORIZADO'   and  STR_TO_DATE(cb.FechaDesde, '%d/%m/%y' )  between STR_TO_DATE( '" + v_valor1 + "', '%Y-%m-%d' ) AND STR_TO_DATE( '" + v_valor2 + "', '%Y-%m-%d' )  ORDER BY month(STR_TO_DATE(cb.FechaDesde, '%d/%m/%y' )),day( STR_TO_DATE(cb.FechaDesde, '%d/%m/%y' )) desc  ";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {
                    listAlic.Add(lector.GetString(0));
                }
                diccionario.Add("alic", listAlic);
                lector.Close();

                // vtas
                cmm = null;
                cmdTxt = "select   concat( DATE_FORMAT(STR_TO_DATE(cb.FechaDesde, '%d/%m/%y' ), '%Y%m%d') ,LPAD(cb.TipoCbte , 3, 0) ,LPAD(cb.PtoVta, 5, 0),LPAD(NumCbte, 20, 0),LPAD(NumCbte, 20, 0) ,LPAD(cb.DocTipo_Afip, 2, 0) ,LPAD(cb.Doc, 20, 0) ,RPAD(Nombre, 30,' ') ,LPAD(REPLACE(REPLACE(FormaPrecio(cb.Total),'.',''),',','') , 15, 0)  ,LPAD(REPLACE(REPLACE(FormaPrecio(0),'.',''),',','') , 15, 0)   ,LPAD(REPLACE(REPLACE(FormaPrecio(0),'.',''),',','') , 15, 0)  ,LPAD(REPLACE(REPLACE(FormaPrecio(0),'.',''),',','') , 15, 0)    ,LPAD(REPLACE(REPLACE(FormaPrecio(0),'.',''),',','') , 15, 0) ,LPAD(REPLACE(REPLACE(FormaPrecio(0),'.',''),',','') , 15, 0) ,LPAD(REPLACE(REPLACE(FormaPrecio(0),'.',''),',','') , 15, 0)   ,LPAD(REPLACE(REPLACE(FormaPrecio(0),'.',''),',','') , 15, 0)  ,LPAD('PES', 3, 0)     , '0001000000'  , '1'  , '0'  ,LPAD(REPLACE(REPLACE(FormaPrecio(0),'.',''),',','') , 15, 0)   ,concat( DATE_FORMAT(STR_TO_DATE(cb.FechaDesde, '%d/%m/%y' ), '%Y%m%d')  )  )  sal    from factura cb   WHERE  ( cb.TipoCbte  ='" + v_tipoCbate + "' OR 99= '" + v_tipoCbate + "' ) and Estado_CAE = 'AUTORIZADO'   and  STR_TO_DATE(cb.FechaDesde, '%d/%m/%y' )  between STR_TO_DATE( '" + v_valor1 + "', '%Y-%m-%d' ) AND STR_TO_DATE( '" + v_valor2 + "', '%Y-%m-%d' )  ORDER BY month(STR_TO_DATE(cb.FechaDesde, '%d/%m/%y' )),day( STR_TO_DATE(cb.FechaDesde, '%d/%m/%y' )) desc  ";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                lector = null;
                lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {
                    listVentas.Add(lector.GetString(0));
                }

                diccionario.Add("vtas", listVentas);
                lector.Close();

                return diccionario;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }

        }



        public List<Best_articulos_venta> listaArticulosAdmin(string ptipo, string pval )
        {
            List<Best_articulos_venta> Lista = new List<Best_articulos_venta>();
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "select distinct codigo, descripcion , getTipo_articulo(tipo_articulo_id) tipo_articulo_id , if(Imagen1 is null or  Imagen1 like '/img/subidas/SinImagen.png','NO','SI') IMAGEN ,  getEtiqueta( art.etiquetaWeb) etiquetaWeb , disponibleWeb,alto_embalaje,peso_embalaje,ancho_embalaje,largo_embalaje from articulos art";

                if (ptipo=="0")
                {
                    cmdTxt += " where descripcion like '%"+pval+"%' ";

                }
                if (ptipo == "1")
                {
                    cmdTxt += " where codigo like '" + pval + "%' ";

                }
                if (ptipo == "3")
                {
                    cmdTxt += " where id = '" + pval + "' ";

                }
                if (ptipo == "2")
                {
                    cmdTxt += " where  (Imagen1 is null or  Imagen1 like '/img/subidas/SinImagen.png') and  ( descripcion like '%" + pval + "%' or '' ='" + pval + "' or  codigo like '" + pval + "%' ) ";

                }
                if (ptipo == "4")
                {
                    cmdTxt += " where   id in ( select sk.articulo_id from stock sk where sk.articulo_id = art.id and sk.stockActual> 0 and sk.sucursal_id= 51 )  and  ( descripcion like '%" + pval + "%' or '' ='" + pval + "' or  codigo like '" + pval + "%' )  ";

                }

                if (ptipo == "5") // disponibles web
                {
                    cmdTxt += " where  disponibleWeb = True and (  codigo like '%" + pval + "%'  OR descripcion like '%" + pval + "%')   ";

                }

                if (ptipo == "6") // SIN TIPO
                {
                    cmdTxt += " where (  tipo_articulo_id IS NULL OR tipo_articulo_id='' OR tipo_articulo_id='0' )   ";

                }

                cmdTxt += " and  estado= 1  order by tipo_articulo_id desc limit 100";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
             
                MySqlDataReader lector = cmm.ExecuteReader();
                while (lector.Read())
                {
                   // disponibleWeb,alto_embalaje,peso_embalaje,ancho_embalaje
                    Best_articulos_venta entidad = new Best_articulos_venta();
                    entidad.codigo= DalModelo.VerifStringMysql(lector, "codigo");
                    entidad.Descripcion = DalModelo.VerifStringMysql(lector, "descripcion");
                    entidad.tipo_articulo = DalModelo.VerifStringMysql(lector, "tipo_articulo_id");
                    entidad.categoria = DalModelo.VerifStringMysql(lector, "IMAGEN");
                    entidad.etiquetaWeb= DalModelo.VerifStringMysql(lector, "etiquetaWeb").ToUpper();
                    entidad.disponibleWeb = DalModelo.VerifStringMysql(lector, "disponibleWeb");
                    entidad.peso_embalaje = DalModelo.VerifStringMysql(lector, "peso_embalaje");
                    entidad.alto_embalaje = DalModelo.VerifStringMysql(lector, "alto_embalaje");
                    entidad.ancho_embalaje = DalModelo.VerifStringMysql(lector, "ancho_embalaje");
                    entidad.largo_embalaje = DalModelo.VerifStringMysql(lector, "largo_embalaje");
                    Lista.Add(entidad);

                }

                lector.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }


            return Lista;

        }

        public List<Best_articulos_venta> listaArticulosVentaHome(int? ptipo, int? pcat, int? ptipocat)
        {
            List<Best_articulos_venta> Lista = new List<Best_articulos_venta>();
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "spArticulosVentaHome";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
                cnn.AgregarParametroAComando(cmm, "ptipo", intTostring(ptipo));
                cnn.AgregarParametroAComando(cmm, "pcat", intTostring(pcat));
                cnn.AgregarParametroAComando(cmm, "ptipocat", intTostring(ptipocat));
                cnn.AgregarParametroAComando(cmm, "part", "");
                MySqlDataReader lector = cmm.ExecuteReader();
                while (lector.Read())
                {
                    Best_articulos_venta entidad = new Best_articulos_venta();
                    entidad.Id = DalModelo.VerifStringMysql(lector, "id_articulo");
                    entidad.tipo_articulo = DalModelo.VerifStringMysql(lector, "tipo_articulo");
                    entidad.precio = DalModelo.VerifStringMysql(lector, "precio_venta");
                    entidad.Descripcion = DalModelo.VerifStringMysql(lector, "nombre_articulo");  // para que se vea bien
                    if (entidad.Descripcion.Length > 52)
                    {
                        entidad.Descripcion = DalModelo.VerifStringMysql(lector, "nombre_articulo").Substring(0, 52);
                    }

                    entidad.genero = DalModelo.VerifStringMysql(lector, "GENERO");
                    entidad.categoria = DalModelo.VerifStringMysql(lector, "CATEGORIA");
                    entidad.diciplina = DalModelo.VerifStringMysql(lector, "DISCIPLINA");
                    entidad.marca = DalModelo.VerifStringMysql(lector, "MARCA");
                    entidad.Imagen1 = DalModelo.VerifStringMysql(lector, "Imagen1");
                    entidad.Imagen2 = DalModelo.VerifStringMysql(lector, "Imagen2");
                    entidad.Imagen3 = DalModelo.VerifStringMysql(lector, "Imagen3");
                    entidad.Imagen4 = DalModelo.VerifStringMysql(lector, "Imagen4");
                    entidad.Imagen5 = DalModelo.VerifStringMysql(lector, "Imagen5");
                    entidad.etiquetaWeb = DalModelo.VerifStringMysql(lector, "etiquetaWeb");
                    Lista.Add(entidad);

                }

                lector.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }


            return Lista;

        }


        public List<Best_articulos_venta> listaArticulosVenta(int? ptipo, int? pcat, int? ptipocat)
        {
            List<Best_articulos_venta> Lista = new List<Best_articulos_venta>();
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "spArticulosVenta";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
                cnn.AgregarParametroAComando(cmm, "ptipo", intTostring(ptipo) );
                cnn.AgregarParametroAComando(cmm, "pcat", intTostring(pcat) );
                cnn.AgregarParametroAComando(cmm, "ptipocat", intTostring(ptipocat));
                cnn.AgregarParametroAComando(cmm, "part", "");
                MySqlDataReader lector = cmm.ExecuteReader();
                while (lector.Read())
                { 
                    Best_articulos_venta entidad = new Best_articulos_venta();
                    entidad.Id = DalModelo.VerifStringMysql(lector, "id_articulo");
                    entidad.tipo_articulo = DalModelo.VerifStringMysql(lector, "tipo_articulo");
                    entidad.precio = DalModelo.VerifStringMysql(lector, "precio_venta");
                    entidad.Descripcion = DalModelo.VerifStringMysql(lector, "nombre_articulo");  // para que se vea bien
                    if (entidad.Descripcion.Length > 52)
                    {
                        entidad.Descripcion = DalModelo.VerifStringMysql(lector, "nombre_articulo").Substring(0,52);
                    }
                    
                    entidad.genero = DalModelo.VerifStringMysql(lector, "GENERO");
                    entidad.categoria = DalModelo.VerifStringMysql(lector, "CATEGORIA");
                    entidad.diciplina = DalModelo.VerifStringMysql(lector, "DISCIPLINA");
                    entidad.marca = DalModelo.VerifStringMysql(lector, "MARCA");
                    entidad.Imagen1 = DalModelo.VerifStringMysql(lector, "Imagen1");
                    entidad.Imagen2 = DalModelo.VerifStringMysql(lector, "Imagen2");
                    entidad.Imagen3 = DalModelo.VerifStringMysql(lector, "Imagen3");
                    entidad.Imagen4 = DalModelo.VerifStringMysql(lector, "Imagen4");
                    entidad.Imagen5 = DalModelo.VerifStringMysql(lector, "Imagen5");
                    entidad.etiquetaWeb = DalModelo.VerifStringMysql(lector, "etiquetaWeb");
                    Lista.Add(entidad);

                }

                lector.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }


            return Lista;

        }

        public string intTostring(Int32? val)
        {
            string sal = "";
            try
            {
                if (val != null && val !=0) {
                    if (  val > 0)
                    {
                        sal = Convert.ToString(val);
                    }
                }

               
            }
            catch (Exception)
            {

                throw;
            }
            return sal;
        }
        public List<Best_categorias> best_Categorias()
        {
            List<Best_categorias> Lista = new List<Best_categorias>();
            try
            { 
                string cmdTxt = " SELECT  cat.id catId,cat.Descripcion catNombre, tipo_cat.Id TipoCatId,tipo_cat.Descripcion TipoCatNombre ,tipo_articulos.Id TipoArtId,tipo_articulos.Descripcion TipoArtNombre  ";
                cmdTxt += "    FROM   best_categorias cat , best_tipos_categorias tipo_cat , best_tipos_articulos tipo_articulos ";
                cmdTxt += "  where cat.Tipo_categoria_id = tipo_cat.Id   and tipo_articulos.Id = cat.tipo_articulo_id ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    Best_categorias  entidad = new Best_categorias();

                    entidad.Id = DalModelo.VerifStringMysql(lector, "catId"); 
                    entidad.Nombre= DalModelo.VerifStringMysql(lector, "catNombre");

                    //Best_tipos_categorias tipocat = new Best_tipos_categorias();
                    //tipocat.Id = DalModelo.VerifStringMysql(lector, "TipoCatId");
                    //tipocat.Nombre = DalModelo.VerifStringMysql(lector, "TipoCatNombre");

                    //Best_tipos_articulos tipoArt = new Best_tipos_articulos();

                    //tipoArt.Id = DalModelo.VerifStringMysql(lector, "TipoArtId");
                    //tipoArt.Descripcion = DalModelo.VerifStringMysql(lector, "TipoArtNombre");

                    //entidad.tipos_articulos = tipoArt;
                    //entidad.tipos_categorias = tipocat;

                    Lista.Add(entidad);

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


            return Lista;

        }
        public void ActualizarArticuloWeb(List<Best_articulos_admin> lista)

        {
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();
                string cmdTxt = "";
                string user =( (Be.Best_UsuariosWeb)HttpContext.Current.Session["UsuarioWeb"]).mail.ToString();
                foreach (var item in lista)
                {
                    cmdTxt = "update articulos set tipo_articulo_id= '"+item.tipo_articulo_id+ "', disponibleWeb= " + item.disponibleWeb+ " , etiquetaWeb= '" + item.etiquetaWeb+ "', Imagen1= '" + item.Imagen1 + "' , Imagen2= '" + item.Imagen2 + "', Imagen3= '" + item.Imagen3 + "', Imagen4= '" + item.Imagen4 + "', Imagen5= '" + item.Imagen5 + "', aud_mod_por='"+ user + "',aud_mod_fec=now(),sucursal_id= 51 where id='" + item.Id + "'  ";
                    cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                    cmm.ExecuteNonQuery();

                    cmdTxt = " delete from articulos_clasificacion where articulo_id='" + item.Id + "'  ";
                    cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                    cmm.ExecuteNonQuery();

                    //if (!string.IsNullOrEmpty(item.genero) ) { 

                    //cmdTxt = " insert into  articulos_clasificacion (articulo_id,clasificacion_id,estado,sucursal_id,aud_ing_fec,aud_ing_por,aud_mod_fec,aud_mod_por) values ('" + item.Id + "','" + item.genero + "',1,51,now(),'" + user + "',now(),'" + user + "')   ";
                    //cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                    //cmm.ExecuteNonQuery();
                    //}

                    foreach (var itemg in item.ListaGeneros)
                    {
                        cmm = null;
                          cmdTxt = " insert into  articulos_clasificacion (articulo_id,clasificacion_id,estado,sucursal_id,aud_ing_fec,aud_ing_por,aud_mod_fec,aud_mod_por) values ('" + item.Id + "','" + itemg.valor+ "',1,51,now(),'" + user + "',now(),'" + user + "')   ";
                        cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                        cmm.ExecuteNonQuery();

                    }
                    if (!string.IsNullOrEmpty(item.categoria))
                    {
                        cmdTxt = " insert into   articulos_clasificacion (articulo_id,clasificacion_id,estado,sucursal_id,aud_ing_fec,aud_ing_por,aud_mod_fec,aud_mod_por) values ('" + item.Id + "','" + item.categoria + "',1,51,now(),'" + user + "',now(),'" + user + "')   ";
                    cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                    cmm.ExecuteNonQuery();
                    }
                //    if (!string.IsNullOrEmpty(item.diciplina))
                //    {
                //        cmdTxt = " insert into  articulos_clasificacion (articulo_id,clasificacion_id,estado, sucursal_id,aud_ing_fec,aud_ing_por,aud_mod_fec,aud_mod_por) values ('" + item.Id + "','" + item.diciplina + "',1,51,now(),'" + user + "',now(),'" + user + "')   ";
                //    cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                //    cmm.ExecuteNonQuery();
                //}

                    foreach (var itemd in item.ListaDiciplina)
                    {
                        cmdTxt = " insert into  articulos_clasificacion (articulo_id,clasificacion_id,estado, sucursal_id,aud_ing_fec,aud_ing_por,aud_mod_fec,aud_mod_por) values ('" + item.Id + "','" + itemd.valor + "',1,51,now(),'" + user + "',now(),'" + user + "')   ";
                        cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                        cmm.ExecuteNonQuery();
                    }
                    if (!string.IsNullOrEmpty(item.marca))
                    {
                        cmdTxt = " insert into articulos_clasificacion (articulo_id,clasificacion_id,estado, sucursal_id,aud_ing_fec,aud_ing_por,aud_mod_fec,aud_mod_por) values ('" + item.Id + "','" + item.marca + "',1,51,now(),'" + user + "',now(),'" + user + "')   ";
                    cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                    cmm.ExecuteNonQuery();
                    }
 


                }

                

              
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

        MySqlConnection cnn2 = new MySqlConnection();
        public List<Best_Menu> best_Menu()
        {
            List<Best_Menu> Lista = new List<Best_Menu>();
            List<Best_tipos_categorias> _TipoCat = new List<Best_tipos_categorias>(); // genero, MARCA
            List<Best_categorias> _Cat = new List<Best_categorias>(); //hombre mujer
            try
            {



                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                // genero, MARCA
                string cmdTxt = " SELECT  tipo_cat.id TipoCatId,tipo_cat.descripcion TipoCatNombre   from tipos_clasificacion tipo_cat  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, ""); 
                MySqlDataReader lector =cmm.ExecuteReader();

                while (lector.Read())
                {

                    Best_tipos_categorias entidad = new Best_tipos_categorias();

                    entidad.Id = DalModelo.VerifStringMysql(lector, "TipoCatId");
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "TipoCatNombre"); 

                 _TipoCat.Add(entidad);

                }

                lector.Close(); 
                lector = null;




                ////// categorias  mujer, hombre
                cmm = null;
                cmdTxt = " SELECT  cat.id catId,cat.descripcion catNombre, cat.tipo_clasificacion_id Tipo_categoria_id,cat.tipo_articulo_id tipo_articulo_id   from clasificacion cat  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                lector = cmm.ExecuteReader();
              
                while (lector.Read())
                {
               
                    Best_categorias entidad = new Best_categorias(); 
                    entidad.Id = DalModelo.VerifStringMysql(lector, "catId");
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "catNombre");
                    entidad.tipo_articulo_id = DalModelo.VerifStringMysql(lector, "tipo_articulo_id");
                    entidad.Tipo_categoria_id = DalModelo.VerifStringMysql(lector, "Tipo_categoria_id");
                    _Cat.Add(entidad);

                }

                lector.Close();
                cmm = null;
                 
                // menu
                // si es calzado, indumentario etc.
                cmm = null;
                lector = null;
                cmdTxt = " SELECT  tipo_art.id TipoArtId,tipo_art.descripcion TipoArtNombre   from tipos_articulos tipo_art  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                lector = cmm.ExecuteReader();
                List<Best_Menu> Lista0 = new List<Best_Menu>();
                while (lector.Read())
                {

                    Best_Menu entidad = new Best_Menu();

                    entidad.Id = DalModelo.VerifStringMysql(lector, "TipoArtId");
                    entidad.Descripcion = DalModelo.VerifStringMysql(lector, "TipoArtNombre");

                    Lista0.Add(entidad);

                }

                lector.Close();
                cmm = null;

              
                foreach (var itemMenu in Lista0)   // calzado
                {
                    Best_Menu miMenu = new Best_Menu();
                    miMenu.Id = itemMenu.Id;
                    miMenu.Descripcion = itemMenu.Descripcion;
                    List<Best_tipos_categorias> misTipoCategotias = new List<Best_tipos_categorias>();
                    foreach (var TipoCat in _TipoCat)  // genero, marca
                    {
                        List<Best_categorias> misCategotias = new List<Best_categorias>();
                        foreach (var Cat in _Cat)  // hombre, mujer
                        {
                            if (TipoCat.Id == Cat.Tipo_categoria_id && itemMenu.Id == Cat.tipo_articulo_id)
                            { 
                                misCategotias.Add(Cat);
                            }

                        }
                        Best_tipos_categorias vtipo = new Best_tipos_categorias();
                        vtipo.Categoria = misCategotias;
                        vtipo.Id = TipoCat.Id;
                        vtipo.Nombre = TipoCat.Nombre;
                        misTipoCategotias.Add(vtipo); 
                    }
                   
                    miMenu.TipoCat = misTipoCategotias;
                    Lista.Add(miMenu);
                }


            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }


            return Lista;

        }

        /// para las api
        /// 

        public DataTable TablasApi(  string Nombre, string desde, string hasta)
        {
            DataSet ds = new DataSet();
            List<Best_articulos> Listentidad = new List<Best_articulos>();
            try
            {

                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "select  * from  "+ Nombre + "  where aud_mod_fec  between   STR_TO_DATE('"+desde+ "', '%d/%m/%Y %H : %i')  and STR_TO_DATE('" + hasta+ "', '%d/%m/%Y %H : %i')  "; 
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                MySqlDataAdapter da = new MySqlDataAdapter(cmm);
                da.Fill(ds);



            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }


            return ds.Tables[0];

        }

        public void TablasApiTxt(string Nombre, string desde, string hasta, string path)
        {
            DataSet ds = new DataSet();
            List<Best_articulos> Listentidad = new List<Best_articulos>();
            try
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception)
                {
 
                }
            

                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();
              
                string cmdTxt = @"select  *  INTO OUTFILE '"+ @path.Replace("\\", "/") + "'  FIELDS TERMINATED BY ','  OPTIONALLY ENCLOSED BY '\"' ";
                cmdTxt += "  LINES TERMINATED BY '\\n' from  " + Nombre + "  where    ";
              //  cmdTxt += " (Aud_ing_fec >  STR_TO_DATE('" + desde + "', '%d/%m/%Y %H : %i')) or ";
                cmdTxt += "    ( Aud_mod_fec >=  STR_TO_DATE('" + desde + "', '%d/%m/%Y %H : %i')) ";
              //  cmdTxt += "  and (Aud_ing_fec <= STR_TO_DATE('" + hasta + "', '%d/%m/%Y %H : %i'))  or ";
 cmdTxt += " and ( Aud_mod_fec <= STR_TO_DATE('" + hasta + "', '%d/%m/%Y %H : %i'))";
// cmdTxt += "  and sucursal_id = 51 ";
                
                
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
            }

 

        }


        public void TablasApiTxtimport(string Nombre, string path)
        {
            DataSet ds = new DataSet();
          
            try
            {
                

                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = @"LOAD DATA LOCAL INFILE '" + @path.Replace("\\", "/") + "' REPLACE  INTO TABLE " + Nombre + "  FIELDS TERMINATED BY ','  OPTIONALLY ENCLOSED BY '\"' ";
                cmdTxt += "  LINES TERMINATED BY '\\n'     "; 
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
            }



        }


        public DataTable ApiArticulos(string id, string codigo, string descripcion)
        {
            DataSet ds = new DataSet();
            List<Best_articulos> Listentidad = new List< Best_articulos>();
            try
            { 

                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = " select id, sucursal_id, tipo, codigo, talle, descripcion, proveedor_id, precio_historico, precio_compra, precio_venta, utilidad, DATE_FORMAT(fecha_lista, '%d/%m/%Y %H:%i')  fecha_lista ,DATE_FORMAT(fecha_compra, '%d/%m/%Y %H:%i') fecha_compra ,DATE_FORMAT(fecha_baja, '%d/%m/%Y %H:%i') fecha_baja ,   estado , ";
                cmdTxt += "codbar , codbar_fab ,DATE_FORMAT(aud_ing_fec, '%d/%m/%Y %H:%i')  aud_ing_fec ,  tipo_articulo_id ,  aud_ing_por ,  DATE_FORMAT(aud_mod_fec, '%d/%m/%Y %H:%i ')  aud_mod_fec ,  aud_mod_por ,  disponibleWeb , etiquetaWeb , Imagen1 , Imagen2 ,  Imagen3 ,  Imagen4 , Imagen5 from articulos  ";

                cmdTxt += "   where (id in ("+ ParametrosIn(id) + ") or ''='" + id + "') and  (codigo in (" + ParametrosIn(codigo) + ") or ''='" + codigo   + "') and (descripcion like '%" + descripcion + "%' or ''='" + descripcion + "')";

               cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                MySqlDataAdapter da = new MySqlDataAdapter(cmm);
                da.Fill(ds);
 


            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }

           
            return ds.Tables[0];

        }


        public void ApiArticulosNew(List<Best_articulos> listart)
        {
            DataSet ds = new DataSet();
            List<Best_articulos> Listentidad = new List<Best_articulos>();
            try
            {

                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                foreach (var art in  listart)
                { 
                string cmdTxt = "insert into  articulos (  id  , sucursal_id  ,  tipo ,  codigo , talle , descripcion ,    proveedor_id , precio_historico ,  precio_compra ,  precio_venta ,   utilidad ,  fecha_lista , fecha_compra , fecha_baja ,   estado , ";
                cmdTxt += " codbar , codbar_fab ,  aud_ing_fec ,  tipo_articulo_id ,  aud_ing_por ,  aud_mod_fec ,  aud_mod_por ,  disponibleWeb , etiquetaWeb , Imagen1 , Imagen2 ,  Imagen3 ,  Imagen4 , Imagen5 )";
                cmdTxt += " values ( '"+ art.id + "' , '" + art.sucursal_id + "'  ,  '" + art.tipo + "',  '" + art.codigo + "' , '" + art.talle + "' , '" + art.descripcion + "' ,    '" + art.proveedor_id + "', '" + art.precio_historico + "' ,  '" + art.precio_compra + "' ,  '" + art.precio_venta + "',  '" + art. utilidad + "',  '" + art.fecha_lista + "' , '" + art.fecha_compra + "' , '" + art.fecha_baja + "',   '" + art.estado + "', ";
                cmdTxt += " '" + art.codbar + "', '" + art.codbar_fab + "',  '" + art.aud_ing_fec + "',  '" + art.tipo_articulo_id + "',  '" + art.aud_ing_por + "' , '" + art.aud_mod_fec + "' ,  '" + art.aud_mod_por + "',  '" + art.disponibleWeb + "' , '" + art.etiquetaWeb + "' , '" + art.Imagen1 + "', '" + art.Imagen2+ "' ,  '"+ art.Imagen3 + "',  '" + art.Imagen4 + "', '" + art.Imagen5+ "' )";



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
            }


          

        }

        public string ParametrosIn(string var)
        { string sal = "";

            if (!string.IsNullOrEmpty(var))
            {


                string[] dd = var.Replace("'","").Replace("-", "").Replace("=", "").Split(',');


            for (int i = 0; i < dd.Length; i++)
            {
                sal += "'"+dd[i]+ "',";
            }
            return sal.Remove(sal.Length-1);
            }
            else
            {
                return "''";

            }
        }


        public void Alta_envio_dimensiones(envio_dimensiones v_obj)

        {
            try
            {
                string cmdTxt = " insert into envio_dimensiones ( Nombre, alto, largo, ancho, estado ) values ('" + v_obj.Nombre.ToUpper() + "', '" + v_obj.alto.ToUpper() + "', '" + v_obj.largo.ToUpper() + "', '" + v_obj.ancho.ToUpper() + "', '1' )  ";

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

        public List<envio_dimensiones> Listar_envio_dimensiones(string v_tipo, string v_valor)
             
        {

            List<envio_dimensiones> lista = new List<envio_dimensiones>();
            try
            {



                string cmdTxt = " select id, Nombre, alto, largo, ancho, estado, aud_mod_fec from envio_dimensiones  ";

                if (v_tipo == "99")
                {
                    cmdTxt = " select  id, Nombre, alto, largo, ancho, estado, aud_mod_fec from envio_dimensiones  where   Id='" + v_valor + "' ";
                }
                if (v_tipo == "0")
                {
                    cmdTxt = " select  id, Nombre, alto, largo, ancho, estado, aud_mod_fec from envio_dimensiones where estado='1' order by Nombre   ";
                }
                
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    envio_dimensiones entidad = new envio_dimensiones();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.id = DalModelo.VerifStringMysql(lector, "id");
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.alto = DalModelo.VerifStringMysql(lector, "alto");
                    entidad.largo = DalModelo.VerifStringMysql(lector, "largo");
                    entidad.ancho = DalModelo.VerifStringMysql(lector, "ancho");
                    entidad.estado = DalModelo.VerifStringMysql(lector, "estado");
                    entidad.aud_mod_fec = DalModelo.VerifStringMysql(lector, "aud_mod_fec");
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

        public void Eliminar_envio_dimensiones(Int32 v_id)
        {
            try
            {
                string cmdTxt = "delete from envio_dimensiones  where Id='" + v_id + "'";

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
    }
}

