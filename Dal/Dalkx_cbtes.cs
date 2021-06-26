using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Be;
using MySql.Data.MySqlClient;
using System.Data;
namespace Dal
{
    [Serializable()]
    
    public class Dalkx_cbtes
    {

        private MySqlConectarSqlDBVarias cnn = new MySqlConectarSqlDBVarias("kardex");


        MySqlCommand cmm;


        public DataSet Cbate(string v_Id)
        {
            DataSet ds = new DataSet();
              MySqlConnection cnn2 = new MySqlConnection();
             
            try
            {

                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();


                string cmdTxt = " select cf.Cuit,UPPER( cf.DomicilioComercial) DomicilioComercial, UPPER(cf.CondicionIVA) CondicionIVA,UPPER(cf.RazonSocial) RazonSocial,UPPER(cf.RazonSocial2) RazonSocial2,cf.IIBB,DATE_FORMAT(cf.FechaIniActividad,  '%d/%m/%y') FechaIniActividad,CAST(LPAD(cb.CbteDesde,8,'0')AS CHAR)   CbteDesde,CAST(LPAD(cb.PtoVta,4,'0') AS CHAR) PtoVta,DATE_FORMAT(cb.Fecha, '%d/%m/%y') Fecha,'Letra', cb.CbteTipo 'Cod_Letra',  concat(cl.Tipo_Doc,':')  Cli_Tipo_Doc,cl.Iva Cli_Iva,  UPPER(concat(cl.Apellido ,' ',cl.Nombre))  Cli_Nombre, UPPER(concat(cl.Calle,' ',cl.Numero) ) Cli_Domicilio,cb.CAE Cae,cb.CAEFchVto  CaeVto,FormaPrecio(cb.ImpNeto) SubTotal,FormaPrecio(cb.ImpTotal) Total,cl.Doc_No Cli_DocNO, cb.CodBarra  CodBarra,cb.Observaciones Afip_Observacion,getProvincia(cl.Provincia) Cli_Provincia,getDepartamento(cl.Departamento) Cli_Departamento, UPPER(if(cb.ImporteLetra is null,'',cb.ImporteLetra )) ImporteLetra,Contado,Tj_Debito,Tj_Credito,CtaCte,Cheque,Otra,Remito,treintaDias   ";
                cmdTxt += "   from kx_cbtes cb, kx_config cf,kx_cliente cl    where     cf.id =cb.Id_config   and cb.Cliente =  cl.Id and cb.Id = '" + v_Id + "'";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                MySqlDataAdapter adaptador = new MySqlDataAdapter(cmm);
                DataTable tabla = new DataTable("cab");
                adaptador.Fill(tabla);
                ds.Tables.Add(tabla);
                 
                cmm = null;
                cmdTxt = " select  Nombre, Id_Articulo,Cant,FormaPrecio(NetoImpxUni) NetoImpxUni,concat(Alic,' ',CAST(ImpIva AS CHAR)) Iva,FormaPrecio(NetoImpTotal) NetoImpTotal,FormaPrecio(ImpxUni) ImpxUni,FormaPrecio(ImpTotal) ImpTotal from kx_cbtes_articulos where id_cbte= '" + v_Id + "' ";
             
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                MySqlDataAdapter adaptador2 = new MySqlDataAdapter(cmm);
                DataTable tabla2 = new DataTable("det");

                adaptador2.Fill(tabla2);
                ds.Tables.Add(tabla2);



                cmm = null;
                cmdTxt = " select getIva(4,'" + v_Id + "') iva105,  getIva(5,'" + v_Id + "') iva21,  getIva(6,'" + v_Id + "') iva27,  getIva(3,'" + v_Id + "') iva0  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                MySqlDataAdapter adaptador3 = new MySqlDataAdapter(cmm);
                DataTable tabla3 = new DataTable("Iva");

                adaptador3.Fill(tabla3);
                ds.Tables.Add(tabla3);



                return ds;
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

        public DataSet Cbate_Nro(string v_nro)
        {
            DataSet ds = new DataSet();
            MySqlConnection cnn2 = new MySqlConnection();

            try
            {

                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = " select  Id  as cant  from   kx_cbtes  where  CbteDesde = '" + v_nro + "' ";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                MySqlDataReader lector = cmm.ExecuteReader();
                Int32 v_Id = 0;
                while (lector.Read())
                {
                    v_Id = DalModelo.VeriIntMysql(lector, "cant");
                }
                lector.Close ();
                cmm = null;

                cmdTxt = " select cf.Cuit,UPPER( cf.DomicilioComercial) DomicilioComercial, UPPER(cf.CondicionIVA) CondicionIVA,UPPER(cf.RazonSocial) RazonSocial,UPPER(cf.RazonSocial2) RazonSocial2,cf.IIBB,DATE_FORMAT(cf.FechaIniActividad,  '%d/%m/%y') FechaIniActividad,CAST(LPAD(cb.CbteDesde,8,'0')AS CHAR)   CbteDesde,CAST(LPAD(cb.PtoVta,4,'0') AS CHAR) PtoVta,DATE_FORMAT(cb.Fecha, '%d/%m/%y') Fecha,'Letra', cb.CbteTipo 'Cod_Letra',  concat(cl.Tipo_Doc,':')  Cli_Tipo_Doc,cl.Iva Cli_Iva,  UPPER(concat(cl.Apellido ,' ',cl.Nombre))  Cli_Nombre, UPPER(concat(cl.Calle,' ',cl.Numero) ) Cli_Domicilio,cb.CAE Cae,cb.CAEFchVto  CaeVto,FormaPrecio(cb.ImpNeto) SubTotal,FormaPrecio(cb.ImpTotal) Total,cl.Doc_No Cli_DocNO, cb.CodBarra  CodBarra,cb.Observaciones Afip_Observacion,getProvincia(cl.Provincia) Cli_Provincia,getDepartamento(cl.Departamento) Cli_Departamento, UPPER(if(cb.ImporteLetra is null,'',cb.ImporteLetra )) ImporteLetra,Contado,Tj_Debito,Tj_Credito,CtaCte,Cheque,Otra,Remito,treintaDias   ";
                cmdTxt += "   from kx_cbtes cb, kx_config cf,kx_cliente cl    where     cf.id =cb.Id_config   and cb.Cliente =  cl.Id and cb.Id = '" + v_Id + "'";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                MySqlDataAdapter adaptador = new MySqlDataAdapter(cmm);
                DataTable tabla = new DataTable("cab");
                adaptador.Fill(tabla);
                ds.Tables.Add(tabla);

                cmm = null;
                cmdTxt = " select  Nombre, Id_Articulo,Cant,FormaPrecio(NetoImpxUni) NetoImpxUni,concat(Alic,' ',CAST(ImpIva AS CHAR)) Iva,FormaPrecio(NetoImpTotal) NetoImpTotal,FormaPrecio(ImpxUni) ImpxUni,FormaPrecio(ImpTotal) ImpTotal from kx_cbtes_articulos where id_cbte= '" + v_Id + "' ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                MySqlDataAdapter adaptador2 = new MySqlDataAdapter(cmm);
                DataTable tabla2 = new DataTable("det");

                adaptador2.Fill(tabla2);
                ds.Tables.Add(tabla2);



                cmm = null;
                cmdTxt = " select getIva(4,'" + v_Id + "') iva105,  getIva(5,'" + v_Id + "') iva21,  getIva(6,'" + v_Id + "') iva27,  getIva(3,'" + v_Id + "') iva0  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                MySqlDataAdapter adaptador3 = new MySqlDataAdapter(cmm);
                DataTable tabla3 = new DataTable("Iva");

                adaptador3.Fill(tabla3);
                ds.Tables.Add(tabla3);



                return ds;
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





        public Be.kx_cliente WsExiste(string doc, string user)
        {
            MySqlConnection cnn2 = new MySqlConnection();
#pragma warning disable CS0219 // La variable 'sal' está asignada pero su valor nunca se usa
            Int32 sal = 0;
#pragma warning restore CS0219 // La variable 'sal' está asignada pero su valor nunca se usa
            kx_cliente entidad2 = new kx_cliente();
            try
            {

                string cmdTxt = "select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web, ListaPrecio from kx_cliente where   ";


                if (doc == "99")
                {
                    cmdTxt += "  Doc_No like  '" + doc + "' ";
                }
                else {
                    cmdTxt += "  Doc_No like  '" + doc + "' and Id_Usuario= '" + user + "' ";
                }
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
               
            MySqlDataReader    lector = cmm.ExecuteReader();
             
                while (lector.Read())
                { 

                    entidad2.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad2.Cod_Manual = DalModelo.VerifStringMysql(lector, "Cod_Manual");
                    entidad2.Tipo_Doc = DalModelo.VerifStringMysql(lector, "Tipo_Doc");
                    entidad2.Doc_No = DalModelo.VerifStringMysql(lector, "Doc_No");
                    entidad2.Iva = DalModelo.VerifStringMysql(lector, "Iva");
                    entidad2.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad2.Apellido = DalModelo.VerifStringMysql(lector, "Apellido");
                    entidad2.Observacion = DalModelo.VerifStringMysql(lector, "Observacion");
                    entidad2.Cta_contable = DalModelo.VerifStringMysql(lector, "Cta_contable");
                    entidad2.Calle = DalModelo.VerifStringMysql(lector, "Calle");
                    entidad2.Numero = DalModelo.VerifStringMysql(lector, "Numero");
                    entidad2.Dpto = DalModelo.VerifStringMysql(lector, "Dpto");
                    entidad2.Pais = DalModelo.VerifStringMysql(lector, "Pais");
                    entidad2.Cp = DalModelo.VerifStringMysql(lector, "Cp");
                    entidad2.Provincia = DalModelo.VerifStringMysql(lector, "Provincia");
                    entidad2.Departamento = DalModelo.VerifStringMysql(lector, "Departamento");
                    entidad2.Distrito = DalModelo.VerifStringMysql(lector, "Distrito");
                    entidad2.Contac_Nomb = DalModelo.VerifStringMysql(lector, "Contac_Nomb");
                    entidad2.Contac_Tel = DalModelo.VerifStringMysql(lector, "Contac_Tel");
                    entidad2.Contac_Cel = DalModelo.VerifStringMysql(lector, "Contac_Cel");
                    entidad2.Contac_Mail = DalModelo.VerifStringMysql(lector, "Contac_Mail");
                    entidad2.Emp_Tel = DalModelo.VerifStringMysql(lector, "Emp_Tel");
                    entidad2.Emp_Cel = DalModelo.VerifStringMysql(lector, "Emp_Cel");
                    entidad2.Emp_Mail = DalModelo.VerifStringMysql(lector, "Emp_Mail");
                    entidad2.Emp_Web = DalModelo.VerifStringMysql(lector, "Emp_Web");
                    entidad2.ListaP = DalModelo.VerifStringMysql(lector, "ListaPrecio");
                     
 
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


            return entidad2 ;
        }
        public int    Mod10(string str)
        {
            // segun rg1702 afip

            int bb = 0;
            Int32 impar = 0;
            Int32 par = 0;
            Int32 etapa2 = 0;
            Int32 etapa4 = 0;
            Int32 digito = 0;

            foreach (char c in str.ToCharArray())
            {

                // 1 paso sumo los q estan en posicion impar

                bb++;
              
                if (!(bb % 2 == 0))
                {
                    impar += Convert.ToInt32(c.ToString());
                }
                else
                {
                    par += Convert.ToInt32(c.ToString());

                }



                // multiplico el resultado por 3
                etapa2 = impar * 3;
                // sumo etap2 mas pares

                etapa4 = par + etapa2;


                digito = 10 - (etapa4 - ((etapa4 / 10) * 10));


            }
            if (digito == 10) { return 0; }
            else
            { return digito; }

        }

        public string Alta(kx_cbtes v_obj)
        {
            Int32 ids = 0;
                MySqlConnection cnn2 = new MySqlConnection();
             
            try
            {
#pragma warning disable CS0219 // La variable 'serv' está asignada pero su valor nunca se usa
                string serv = "N";
#pragma warning restore CS0219 // La variable 'serv' está asignada pero su valor nunca se usa
                string CodBarra = v_obj.Cuit + string.Format("{0:d2}", Convert.ToInt32(v_obj.CbteTipo)) + string.Format("{0:d4}", Convert.ToInt32(v_obj.PtoVta)) + v_obj.CAE + v_obj.CAEFchVto;
                string digi = Mod10(CodBarra).ToString();
                v_obj.CodBarra = CodBarra + digi;


                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();
                string cmdTxt = "";

                if (v_obj.DocNro == "99") // se fija si tiene como cliente un consumidor final sino lo agrega
                {
                    cmm = null;
                    cmdTxt = "wi271852_kardex.consumidor_final";
                    cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
                    cnn.AgregarParametroAComando(cmm, "v_config", v_obj.Id_config);

                    MySqlParameter outSal22 = cnn.AgregarParametroAComandoOut(cmm, "v_sal");
                    cmm.ExecuteNonQuery();
                    v_obj.Cliente = outSal22.Value.ToString();

                    cmm.ExecuteNonQuery();

                }

                cmm = null;

                cmdTxt = "insert into kx_cbtes (CantReg, CbteTipo, PtoVta, Fecha, usuario, Resultado, Vendedor, Cliente, aud, Concepto, DocTipo, DocNro, CbteDesde, CbteHasta, CbteFch, ImpTotal, ImpTotConc, ImpNeto, ImpOpEx, ImpTrib, ImpIVA, MonId, MonCotiz, CAE, CAEFchVto, Observaciones,CodBarra, Id_config,Pago_Efectivo,Pago_Bancos,Pago_Cheques,Pago_Tarjetas,ImporteLetra,Contado,Tj_Debito,Tj_Credito,CtaCte,Cheque,Otra, Remito,treintaDias, RazonSocial, CantAlic ,FchVtoPago) values ('" + v_obj.CantReg + "', '" + v_obj.CbteTipo + "', '" + v_obj.PtoVta + "', STR_TO_DATE('" + v_obj.Fecha + "','%d/%m/%Y'), '" + v_obj.usuario + "', '" + v_obj.Resultado + "', '" + v_obj.Vendedor + "', '" + v_obj.Cliente + "', now(), '" + v_obj.Concepto + "', '" + v_obj.DocTipo + "', '" + v_obj.DocNro + "', '" + v_obj.CbteDesde + "', '" + v_obj.CbteHasta + "', '" + v_obj.CbteFch + "', '" + v_obj.ImpTotal + "', '" + v_obj.ImpTotConc + "', '" + v_obj.ImpNeto + "', '" + v_obj.ImpOpEx + "', '" + v_obj.ImpTrib + "', '" + v_obj.ImpIVA + "', '" + v_obj.MonId + "', '" + v_obj.MonCotiz + "', '" + v_obj.CAE + "', '" + v_obj.CAEFchVto + "', '" + v_obj.Observaciones + "','" + v_obj.CodBarra + "' ,'" + v_obj.Id_config + "','" + v_obj.Pago_Efectivo + "','" + v_obj.Pago_Bancos + "'  ,'" + v_obj.Pago_Cheques + "' ,'" + v_obj.Pago_Tarjetas + "','" + v_obj.ImporteLetra + "','" + v_obj.Contado + "','" + v_obj.Tj_Debito + "','" + v_obj.Tj_Credito + "' ,'" + v_obj.CtaCte + "' ,'" + v_obj.Cheque + "' ,'" + v_obj.Otra + "'  ,'" + v_obj.Remito + "'  ,'" + v_obj.treintaDias + "',(select concat(Nombre,' ', Apellido)   from kx_cliente where  id ='" + v_obj.Cliente + "') , '" + v_obj.CantAlic + "', STR_TO_DATE('" + v_obj.FchVtoPago + "','%d/%m/%Y'))  ";




     cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
     cmm.ExecuteNonQuery();


     cmm = null;

     cmdTxt = " select max(Id) cant from kx_cbtes ";

     cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
     MySqlDataReader lector2 = cmm.ExecuteReader();

     while (lector2.Read())
     {

         ids = Convert.ToInt32(lector2["cant"]);

     }
     lector2.Close();

     foreach (var obj in v_obj.Articulos)
     {
         cmm = null;
         cmdTxt = " insert into kx_cbtes_articulos (Id_Articulo,Cod_Manual, Nombre, Cant, Alic, ImpxUni, ImpTotal, id_cbte,NetoImpxUni,NetoImpTotal,ImpIva,Tipo_Art,Unidad,IIBB,ID_DEPOSITO) values ('" + obj.Id_Articulo + "', '" + obj.Cod_Manual + "', '" + obj.Nombre + "', '" + obj.Cant + "', '" + obj.Alic + "', '" + obj.ImpxUni + "', '" + obj.ImpTotal + "', '" + ids + "', '" + obj.NetoImpxUni + "', '" + obj.NetoImpTotal + "', '" + obj.ImpIva + "', '" + obj.Tipo_Art + "', '" + obj.Unidad + "', '" + obj.IIBB + "', '" + obj.DepositoSelect + "')";

         cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
         cmm.ExecuteNonQuery();

         string ds = DateTime.Now.ToString("dd/MM/yyyy");
         // PARA Q no DESCUENTE DEL STOCK
         //serv = "S";

         //if (obj.es_Stock == "SI")
         //{
         //    // PARA Q DESCUENTE DEL STOCK
         //    serv = "N"; //si es servicio  N pq es articulo  y descuenta
         //}
         //if (obj.Cod_Manual == "MANUAL")
         //{
         //    serv = "S";
         //}
         //if (obj.Tipo_Art == "SERVICIO")
         //{
         //    // PARA Q no DESCUENTE DEL STOCK
         //    serv = "S";
         //}

         if (obj.Cod_Manual == "GFXOS")
         {
             serv = "S";
             cmm = null;
             cmdTxt = "update ordenes_servicio  set Pagado ='" + ids + "' where  Id='" + obj.Id_Articulo + "'";
             cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
             cmm.ExecuteNonQuery();
         }

         if (obj.Cod_Manual == "GFXCO0")
         {
             serv = "S";
             cmm = null;
             cmdTxt = "update contraordenes_servicio cs,  ordenes_servicio os  set os.Pagado ='" + ids + "', cs.Pagado ='" + ids + "'  where cs.Orden_Id = os.Id and cs.Id='" + obj.Id_Articulo + "'";
             cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
             cmm.ExecuteNonQuery();
         }
         if (obj.Cod_Manual == "GFXCO1")
         {
             serv = "S";
             cmm = null;
             cmdTxt = "update contraordenes_servicio  set Pagado ='" + ids + "' where  Id='" + obj.Id_Articulo + "'";
             cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
             cmm.ExecuteNonQuery();

         }


         // actualizo el stock
         if (obj.es_Stock == "SI" && v_obj.Resultado != "R")
      
         {
             cmm = null;
             cmdTxt = "wi271852_kardex.Stock_update";

             cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
             cnn.AgregarParametroAComando(cmm, "v_tipo", "S");
             cnn.AgregarParametroAComando(cmm, "v_art", obj.Id_Articulo);
             cnn.AgregarParametroAComando(cmm, "v_cant", obj.Cant);
             cnn.AgregarParametroAComando(cmm, "v_dep", obj.DepositoSelect);
             cnn.AgregarParametroAComando(cmm, "v_motivo", "Factura n°: " + v_obj.CbteDesde+ " - Cbte id: " + ids);
              
      
             cnn.AgregarParametroAComando(cmm, "v_remito", "");
             cnn.AgregarParametroAComando(cmm, "v_origen", "");
             cnn.AgregarParametroAComando(cmm, "v_proveedor", "");
             cnn.AgregarParametroAComando(cmm, "v_comentario", "");
             cnn.AgregarParametroAComando(cmm, "v_tipo_ingreso", "");
             cnn.AgregarParametroAComando(cmm, "v_lote","");
             cnn.AgregarParametroAComando(cmm, "v_vto", "");


             cmm.ExecuteNonQuery();
         }

     }

     if (v_obj.Tarjetas != null)
     {
         foreach (var v_obj1 in v_obj.Tarjetas)
         {
                          cmm = null;
          
                 cmdTxt = " insert into kx_tarjetas (Tarjeta, Tarjeta_No, Cuotas, Cupon_No, Monto, id_cbte) values ('" + v_obj1.Tarjeta + "', '" + v_obj1.Tarjeta_No + "', '" + v_obj1.Cuotas + "', '" + v_obj1.Cupon_No + "', '" + v_obj1.Monto + "', '" + ids + "') ";
         
             cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
             cmm.ExecuteNonQuery();

           
         }
     }
     if (v_obj.AlicIva != null )
     {
         foreach (var v_obj1 in v_obj.AlicIva)
         {

             
             cmm = null;
             cmdTxt = " insert into kx_cbtes_AlicIva (Id,BaseImp,Importe,id_cbte) values ('" + v_obj1.Id + "', '" + v_obj1.BaseImp + "', '" + v_obj1.Importe + "', '" + ids + "') ";
             cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
             cmm.ExecuteNonQuery();
         }
     }

     if (v_obj.Cheques != null)
     {
         foreach (var v_obj2 in v_obj.Cheques)
         {
             cmm = null; 
                 cmdTxt = "insert into kx_cheques (cheque_no, banco, tipo, monto, id_cbte, Dias, CodCliente, CodProveedor, Librador, FechaEmision, FechaCobro, FechaVto, CUITLibrador) values ('" + v_obj2.cheque_no + "', '" + v_obj2.banco + "', '" + v_obj2.tipo + "', '" + v_obj2.monto + "', '" + ids + "', '" + v_obj2.Dias + "', '" + v_obj2.CodCliente + "', '" + v_obj2.CodProveedor + "', '" + v_obj2.Librador + "',   STR_TO_DATE('" + v_obj2.FechaEmision + "','%d/%m/%Y') ,STR_TO_DATE('" + v_obj2.FechaCobro + "','%d/%m/%Y'), STR_TO_DATE('" + v_obj2.FechaVto + "','%d/%m/%Y'), '" + v_obj2.CUITLibrador + "') ";
             
             cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
             cmm.ExecuteNonQuery();
         }

     }


     return ids.ToString();
              
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

        public List<Be.kx_cbtes> WssaldoXmes(string v_mes, string v_anio, string v_id_cfg)
        {

            List<kx_cbtes> lista = new List<kx_cbtes>();
            try
            {
                string cmdTxt = "";

                //    cmdTxt = " select Id,CantReg, CbteTipo, PtoVta,DATE_FORMAT(Fecha,  '%d/%m/%y') Fecha, usuario, Resultado, Vendedor, Cliente, aud, Concepto, DocTipo, DocNro, CbteDesde, CbteHasta, CbteFch, ImpTotal, ImpTotConc, ImpNeto, ImpOpEx, ImpTrib, ImpIVA, MonId, MonCotiz, CAE, CAEFchVto, Observaciones from kx_cbtes where CbteDesde 	='" + v_valor1 + "' and  CbteTipo ='" + v_tipoCbate + "' order by CbteHasta desc";

                    cmdTxt = " SELECT cb.Id, cb.CantReg, ct.nombre CbteTipo, cb.PtoVta, DATE_FORMAT( cb.Fecha, '%d/%m/%y' ) Fecha, cb.usuario, cb.Resultado, cb.Vendedor, cb.Cliente, cb.aud, cb.Concepto, cb.DocTipo, concat( dt.nombre, ' ', cb.DocNro ) DocNro, cb.CbteDesde, cb.CbteHasta, cb.CbteFch, cb.ImpTotal, cb.ImpTotConc, cb.ImpNeto, cb.ImpOpEx, cb.ImpTrib, cb.ImpIVA, cb.MonId, cb.MonCotiz, cb.CAE, cb.CAEFchVto, cb.Observaciones FROM kx_cbtes cb, kx_doc_tipo dt, kx_cbtes_tipos ct  ";
                    cmdTxt += "  WHERE   ct.id = cb.CbteTipo AND dt.id = cb.DocTipo and   Year( Fecha ) ='" + v_anio + "'  AND Month( Fecha ) = '" + v_mes + "' AND Resultado <> 'R' and CbteTipo in(1,6,11,15, 3,8,13)  and Id_config='" + v_id_cfg + "'   ORDER BY cb.CbteHasta DESC ";
                


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
                    entidad.CbteHasta = DalModelo.VerifStringMysql(lector, "CbteHasta");
                    entidad.CbteFch = DalModelo.VerifStringMysql(lector, "CbteFch");
                    entidad.ImpTotal = DalModelo.VerifStringMysql(lector, "ImpTotal");
                    entidad.ImpTotConc = DalModelo.VerifStringMysql(lector, "ImpTotConc");
                    entidad.ImpNeto = DalModelo.VerifStringMysql(lector, "ImpNeto");
                    entidad.ImpOpEx = DalModelo.VerifStringMysql(lector, "ImpOpEx");
                    entidad.ImpTrib = DalModelo.VerifStringMysql(lector, "ImpTrib");
                    entidad.ImpIVA = DalModelo.VerifStringMysql(lector, "ImpIVA");
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
          string     cmdTxt = "";

          cmdTxt = " select concat(LPAD(CbteTipo, 3, 0)  ,LPAD(PtoVta, 5, 0) ,LPAD(CbteDesde, 20, 0) ,LPAD(REPLACE(REPLACE(FormaPrecio(BaseImp),'.',''),',','') , 15, 0) ,LPAD(ali.Id, 4, 0) ,LPAD(REPLACE(REPLACE(FormaPrecio(Importe),'.',''),',','') , 15, 0) )  sal  from kx_cbtes , kx_cbtes_aliciva ali where ali.id_cbte= kx_cbtes.Id  and Id_config ='" + v_conf + "' and Fecha between STR_TO_DATE( '" + v_valor1 + "', '%d/%m/%Y' ) AND STR_TO_DATE( '" + v_valor2 + "', '%d/%m/%Y' )  and resultado <> 'R'  order by Fecha ";    
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
                cmdTxt = " select concat( DATE_FORMAT(FECHA, '%Y%m%d') ,LPAD(CbteTipo, 3, 0) ,LPAD(PtoVta, 5, 0),LPAD(CbteDesde, 20, 0),LPAD(CbteHasta, 20, 0) ,LPAD(DocTipo, 2, 0) ,LPAD(DocNro, 20, 0) ,RPAD(RazonSocial, 30,' ') ,LPAD(REPLACE(REPLACE(FormaPrecio(ImpTotal),'.',''),',','') , 15, 0)  ,LPAD(REPLACE(REPLACE(FormaPrecio(ImpTotConc),'.',''),',','') , 15, 0)   ,LPAD(REPLACE(REPLACE(FormaPrecio(0),'.',''),',','') , 15, 0)  ,LPAD(REPLACE(REPLACE(FormaPrecio(ImpOpEx),'.',''),',','') , 15, 0)    ,LPAD(REPLACE(REPLACE(FormaPrecio(0),'.',''),',','') , 15, 0) ,LPAD(REPLACE(REPLACE(FormaPrecio(0),'.',''),',','') , 15, 0) ,LPAD(REPLACE(REPLACE(FormaPrecio(ImpTrib),'.',''),',','') , 15, 0)   ,LPAD(REPLACE(REPLACE(FormaPrecio(0),'.',''),',','') , 15, 0)  ,LPAD(MonId, 3, 0)     , '0001000000'  , CantAlic  , '0'  ,LPAD(REPLACE(REPLACE(FormaPrecio(0),'.',''),',','') , 15, 0)   ,concat( DATE_FORMAT(FchVtoPago, '%Y%m%d')  )  )  sal  from kx_cbtes   where  Id_config ='" + v_conf + "' and Fecha between STR_TO_DATE( '" + v_valor1 + "', '%d/%m/%Y' ) AND STR_TO_DATE( '" + v_valor2 + "', '%d/%m/%Y' )  and resultado <> 'R'  order by Fecha ";
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


        public List<Be.kx_cbtes> WsListar(string v_tipoCbate, string v_tipo, string v_valor1, string v_valor2, string v_user, string v_conf)

        {

            List<kx_cbtes> lista = new List<kx_cbtes>();
            try
            {
                string cmdTxt = "";
                 
                if (v_tipo == "1")
                {
                //    cmdTxt = " select Id,CantReg, CbteTipo, PtoVta,DATE_FORMAT(Fecha,  '%d/%m/%y') Fecha, usuario, Resultado, Vendedor, Cliente, aud, Concepto, DocTipo, DocNro, CbteDesde, CbteHasta, CbteFch, ImpTotal, ImpTotConc, ImpNeto, ImpOpEx, ImpTrib, ImpIVA, MonId, MonCotiz, CAE, CAEFchVto, Observaciones from kx_cbtes where CbteDesde 	='" + v_valor1 + "' and  CbteTipo ='" + v_tipoCbate + "' order by CbteHasta desc";

                    cmdTxt = " SELECT cb.Id, ct.id CantReg, ct.nombre CbteTipo, cb.PtoVta, DATE_FORMAT( cb.Fecha, '%d/%m/%y' ) Fecha, cb.usuario, cb.Resultado, cb.Vendedor, cb.Cliente, cb.aud, cb.Concepto, cb.DocTipo, concat( dt.nombre, ' ', cb.DocNro ) DocNro, cb.CbteDesde, cb.CbteHasta, cb.CbteFch, formaPrecioCbte(cb.ImpTotal , cb.Id)  ImpTotal,  formaPrecioCbte(cb.ImpTotConc , cb.Id)    ImpTotConc, formaPrecioCbte(cb.ImpNeto , cb.Id)   ImpNeto, formaPrecioCbte(cb.ImpOpEx , cb.Id)    ImpOpEx, formaPrecioCbte(cb.ImpTrib , cb.Id)     ImpTrib,formaPrecioCbte(cb.ImpIVA , cb.Id) ImpIVA,cb.MonId, cb.MonCotiz, cb.CAE, cb.CAEFchVto, cb.Observaciones FROM kx_cbtes cb, kx_doc_tipo dt, kx_cbtes_tipos ct WHERE CbteDesde 	='" + v_valor1 + "' AND ct.id = cb.CbteTipo AND dt.id = cb.DocTipo and  ( CbteTipo ='" + v_tipoCbate + "' OR 99= '" + v_tipoCbate + "' ) and resultado <> 'R' and cb.Id_config='" + v_conf + "'    ORDER BY cb.CbteHasta DESC ";
                
                }
                if (v_tipo == "2")
                {
                    cmdTxt = " SELECT cb.Id, ct.id CantReg, ct.nombre CbteTipo, cb.PtoVta, DATE_FORMAT( cb.Fecha, '%d/%m/%y' ) Fecha, cb.usuario, cb.Resultado, cb.Vendedor, cb.Cliente, cb.aud, cb.Concepto, cb.DocTipo, concat( dt.nombre, ' ', cb.DocNro ) DocNro, concat( CAST(LPAD(cb.PtoVta,4,'0') AS CHAR),'-', CAST(LPAD(cb.CbteDesde,8,'0')AS CHAR))   CbteDesde, cb.CbteHasta, cb.CbteFch, formaPrecioCbte(cb.ImpTotal , cb.Id)    ImpTotal,formaPrecioCbte(cb.ImpTotConc , cb.Id)  ImpTotConc,formaPrecioCbte(cb.ImpNeto , cb.Id) ImpNeto, formaPrecioCbte(cb.ImpOpEx , cb.Id)  ImpOpEx, formaPrecioCbte(cb.ImpTrib , cb.Id)   ImpTrib,formaPrecioCbte(cb.ImpIVA , cb.Id)    ImpIVA, cb.MonId, cb.MonCotiz, cb.CAE, cb.CAEFchVto, cb.Observaciones FROM kx_cbtes cb, kx_doc_tipo dt, kx_cbtes_tipos ct WHERE   ct.id = cb.CbteTipo AND dt.id = cb.DocTipo and (cb.CbteTipo ='" + v_tipoCbate + "' OR 99= '" + v_tipoCbate + "' ) and resultado <> 'R'   and cb.Id_config='" + v_conf + "'  and cb.Fecha between STR_TO_DATE( '" + v_valor1 + "', '%d/%m/%Y' ) AND STR_TO_DATE( '" + v_valor2 + "', '%d/%m/%Y' )  order by cb.Fecha desc";
                  

                }
                if (v_tipo == "3")
                {
                    //    cmdTxt = " select Id,CantReg, CbteTipo, PtoVta,DATE_FORMAT(Fecha,  '%d/%m/%y') Fecha, usuario, Resultado, Vendedor, Cliente, aud, Concepto, DocTipo, DocNro, CbteDesde, CbteHasta, CbteFch, ImpTotal, ImpTotConc, ImpNeto, ImpOpEx, ImpTrib, ImpIVA, MonId, MonCotiz, CAE, CAEFchVto, Observaciones from kx_cbtes where CbteDesde 	='" + v_valor1 + "' and  CbteTipo ='" + v_tipoCbate + "' order by CbteHasta desc";

                    cmdTxt = " SELECT cb.Id, ct.id CantReg, ct.nombre CbteTipo, cb.PtoVta, DATE_FORMAT( cb.Fecha, '%d/%m/%y' ) Fecha, cb.usuario, cb.Resultado, cb.Vendedor, cb.Cliente, cb.aud, cb.Concepto, cb.DocTipo, concat( dt.nombre, ' ', cb.DocNro ) DocNro, cb.CbteDesde, cb.CbteHasta, formaPrecioCbte(cb.ImpTotal , cb.Id)   ImpTotal,formaPrecioCbte(cb.ImpTotConc , cb.Id)    ImpTotConc,formaPrecioCbte(cb.ImpNeto , cb.Id))     ImpNeto, formaPrecioCbte(cb.ImpOpEx , cb.Id))      ImpOpEx, formaPrecio(cb.ImpTrib) ImpTrib,formaPrecio( cb.ImpIVA) ImpIVA,cb.MonId, cb.MonCotiz, cb.CAE, cb.CAEFchVto, cb.Observaciones FROM kx_cbtes cb, kx_doc_tipo dt, kx_cbtes_tipos ct,kx_cliente cl WHERE cl.Doc_No ='" + v_valor1 + "' AND ct.id = cb.CbteTipo AND dt.id = cb.DocTipo and  (cb.CbteTipo ='" + v_tipoCbate + "' OR 99= '" + v_tipoCbate + "' ) and resultado <> 'R'  AND cl.id = cb.Cliente  and cb.Id_config='" + v_conf + "'   ORDER BY cb.CbteHasta DESC ";

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
                    entidad.CbteHasta = DalModelo.VerifStringMysql(lector, "CbteHasta");
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
        public void Modificacion(kx_cbtes v_obj)
        {
            try
            {
                string cmdTxt = "update kx_cbtes set CantReg='" + v_obj.CantReg + "' , CbteTipo='" + v_obj.CbteTipo + "' , PtoVta='" + v_obj.PtoVta + "' , Fecha='" + v_obj.Fecha + "' , usuario='" + v_obj.usuario + "' , Resultado='" + v_obj.Resultado + "' , Vendedor='" + v_obj.Vendedor + "' , Cliente='" + v_obj.Cliente + "' , aud='" + v_obj.aud + "' , Concepto='" + v_obj.Concepto + "' , DocTipo='" + v_obj.DocTipo + "' , DocNro='" + v_obj.DocNro + "' , CbteDesde='" + v_obj.CbteDesde + "' , CbteHasta='" + v_obj.CbteHasta + "' , CbteFch='" + v_obj.CbteFch + "' , ImpTotal='" + v_obj.ImpTotal + "' , ImpTotConc='" + v_obj.ImpTotConc + "' , ImpNeto='" + v_obj.ImpNeto + "' , ImpOpEx='" + v_obj.ImpOpEx + "' , ImpTrib='" + v_obj.ImpTrib + "' , ImpIVA='" + v_obj.ImpIVA + "' , MonId='" + v_obj.MonId + "' , MonCotiz='" + v_obj.MonCotiz + "' , CAE='" + v_obj.CAE + "' , CAEFchVto='" + v_obj.CAEFchVto + "' , Observaciones='" + v_obj.Observaciones + "' where id ='" + v_obj.Id + "'   ";

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

        public List<kx_cbtes> Listar()
        {

            List<kx_cbtes> lista = new List<kx_cbtes>();
            try
            {



                string cmdTxt = " select Id,CantReg, CbteTipo, PtoVta, Fecha, usuario, Resultado, Vendedor, Cliente, aud, Concepto, DocTipo, DocNro, CbteDesde, CbteHasta, CbteFch, ImpTotal, ImpTotConc, ImpNeto, ImpOpEx, ImpTrib, ImpIVA, MonId, MonCotiz, CAE, CAEFchVto, Observaciones from kx_cbtes  ";

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
                    entidad.CbteHasta = DalModelo.VerifStringMysql(lector, "CbteHasta");
                    entidad.CbteFch = DalModelo.VerifStringMysql(lector, "CbteFch");
                    entidad.ImpTotal = DalModelo.VerifStringMysql(lector, "ImpTotal");
                    entidad.ImpTotConc = DalModelo.VerifStringMysql(lector, "ImpTotConc");
                    entidad.ImpNeto = DalModelo.VerifStringMysql(lector, "ImpNeto");
                    entidad.ImpOpEx = DalModelo.VerifStringMysql(lector, "ImpOpEx");
                    entidad.ImpTrib = DalModelo.VerifStringMysql(lector, "ImpTrib");
                    entidad.ImpIVA = DalModelo.VerifStringMysql(lector, "ImpIVA");
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


        public void Eliminar(Int32 v_id)
        {
            try
            {
                string cmdTxt = "delete from kx_cbtes  where Id='" + v_id + "'";

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


        public string[] saldoXmes(string v_anio,  string cfg) {

            try
            {

                string query = "select   saldoXmes('1','" + v_anio + "','" + cfg + "') as enero, ";
                query += " saldoXmes('2','" + v_anio + "','" + cfg + "') as Febrero,";
                query += "   saldoXmes('3','" + v_anio + "','" + cfg + "') as Marzo,";
                query += "     saldoXmes('4','" + v_anio + "','" + cfg + "') as Abril, ";
                query += "   saldoXmes('5','" + v_anio + "','" + cfg + "') as Mayo, ";
                query += "    saldoXmes('6','" + v_anio + "','" + cfg + "') as Junio, ";
                query += "     saldoXmes('7','" + v_anio + "','" + cfg + "') as Julio, ";
                query += "   saldoXmes('8','" + v_anio + "','" + cfg + "') as Agosto, ";
                query += "    saldoXmes('9','" + v_anio + "','" + cfg + "') as Septiembre, ";
                query += "    saldoXmes('10','" + v_anio + "','" + cfg + "') as Octubre, ";
                query += "     saldoXmes('11','" + v_anio + "','" + cfg + "') as Noviembre, ";
                query += "    saldoXmes('12','" + v_anio + "','" + cfg + "') as Diciembre";

                cmm = cnn.MySqlCrearNuevoComando(query);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);
                string[] arr4 = new string[12];
                while (lector.Read())
                {
                    arr4[0] = VerifStringMysql33(lector, "Enero");
                    arr4[1] = VerifStringMysql33(lector, "Febrero");
                    arr4[2] = VerifStringMysql33(lector, "Marzo");
                    arr4[3] = VerifStringMysql33(lector, "Abril");
                    arr4[4] = VerifStringMysql33(lector, "Mayo");
                    arr4[5] = VerifStringMysql33(lector, "Junio");
                    arr4[6] = VerifStringMysql33(lector, "Julio");
                    arr4[7] = VerifStringMysql33(lector, "Agosto");
                    arr4[8] = VerifStringMysql33(lector, "Septiembre");
                    arr4[9] = VerifStringMysql33(lector, "Octubre");
                    arr4[10] = VerifStringMysql33(lector, "Noviembre");
                    arr4[11] = VerifStringMysql33(lector, "Diciembre");
                }

                return arr4;


            }
            catch (Exception)
            {

                throw;
            }
            finally {
                cnn.Close(cmm);
            }
        }

        public static string VerifStringMysql33(MySqlDataReader lector, string s)
        {

            if (lector[s] == DBNull.Value)
            {

                return "0";
            }
            else
            {

                return Convert.ToString(lector[s]);

            }


        }

        public Int32 maxCbtePruebas(string tipocbte, string id_config)
        {
            Int32 id = 0;

            try
            {
                string cmdTxt = " select (ifnull( max(CbteHasta),0)+1 ) as cant from kx_cbtes  where CbteTipo ='" + tipocbte + "' and Id_config ='" + id_config + "' ";

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

        public Int32 maxId()
        {
            Int32 id = 0;

            try
            {
                string cmdTxt = " select max(Id) as cant from kx_cbtes  ";

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


        public List<string> serv_artXmes(string v_mes, string v_anio, string v_id_cfg)
        {   MySqlConnection         cnn2 = null ;
            try
            {
                List<string> lista = new List<string>();
                cmm = null;
           cnn2 = cnn.MySqlCrearNuevaConexion();
           cnn2.Open();
                string cmdTxt = "wi271852_kardex.sp_serv_artXmes";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
                cnn.AgregarParametroAComando(cmm, "v_mes", v_mes);
                cnn.AgregarParametroAComando(cmm, "v_anio", v_anio);
                cnn.AgregarParametroAComando(cmm, "v_id_cfg", v_id_cfg);

                MySqlParameter outSalser = cnn.AgregarParametroAComandoOut(cmm, "sal_serv");
                MySqlParameter outSalart = cnn.AgregarParametroAComandoOut(cmm, "sal_art");
                MySqlParameter outSaliibb = cnn.AgregarParametroAComandoOut(cmm, "sal_iibb");

                cmm.ExecuteNonQuery();
                lista.Add(outSalser.Value.ToString());
                lista.Add(outSalart.Value.ToString());
                lista.Add(outSaliibb.Value.ToString());  

                cmm.ExecuteNonQuery();

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
            finally {
                cnn2.Close();
            
            }
    
    }
    }

}
