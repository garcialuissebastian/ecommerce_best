using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Be;
using System.Data.SqlClient;
using System.Net;
using System.Runtime.Serialization.Json;
using System.ComponentModel;
using System.IO;
namespace Dal
{
   public class DalLote
    {
        private ConectarSqlDB cnn = new ConectarSqlDB();
        DalPermiso _mapeadorPermiso = new DalPermiso();

        SqlCommand cmm;
        SqlConnection sqlcnn;
        public void InsertLote(Fact_Cab v_facts)
        {

            try
            {
                sqlcnn = cnn.CrearNuevaConexion();
                //sqlcnn.Open();
                string cmdTxt = "insert into LOTE (CantReg,CbteTipo,PtoVta , aud , usuario,Resultado) values ('" + v_facts.CantReg + "','" + v_facts.CbteTipo + "','" + v_facts.PtoVta + "', GETDATE(),'" + v_facts.Usuario + "','" + v_facts.Resultado + "')";
               
                cmm = cnn.CrearNuevoComando(cmdTxt, sqlcnn, "");
                
                
                cnn.ExecuteNonQuery(cmm);
                Int32 ids = maxId();
                foreach (var item in v_facts.Detalle)
                {
                    item.Id_lote = ids;
                    InsertLoteDetalle(item);
                }

                foreach (var item1 in v_facts.Error)
                {
                    item1.id_lote = ids;
                    InsertLoteErr(item1);
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

        public  Fact_Cab lote(string ids) {
 List<Fact_Cab> lista = new List<Fact_Cab>();
            try 
	{
        sqlcnn = cnn.CrearNuevaConexion();
        string cmdTxt = "";
            if(string.IsNullOrEmpty(ids)){
                cmdTxt = "select id, CantReg,CbteTipo,PtoVta , aud , usuario,Resultado from LOTE where id =(SELECT max(id) as cant from LOTE) ";
                }

            if (!string.IsNullOrEmpty(ids))
            {
                cmdTxt = "select id, CantReg,CbteTipo,PtoVta , aud , usuario,Resultado from LOTE where id = "+ids;
            }
            cmm = cnn.CrearNuevoComando(cmdTxt, sqlcnn, "");

            SqlDataReader lector = cnn.ExecuteReader(cmm);
            Fact_Cab ff = new Fact_Cab();
            while (lector.Read())
            {
               
                ff.aud = Convert.ToDateTime(lector["aud"]);
                ff.CantReg = VeriInt(lector, "CantReg");
                ff.CbteTipo = VeriInt(lector, "CbteTipo");
                ff.id = VeriInt(lector, "id");
                ff.PtoVta = VeriInt(lector, "PtoVta");
               string rr =  VerifString(lector, "Resultado");
                
                
                if(rr=="A"){
                    ff.Resultado = "AUTORIZADO";
                }
                if (rr == "R")
                {
                    ff.Resultado = "RECHAZADO";
                }
                if (rr == "P")
                {
                    ff.Resultado = "PARCIAL";
                }
            }
            //cerrar lector
            lector.Close();

            ff.Detalle = lote_detalle(ff.id.ToString());
            ff.Error = lote_err(ff.id.ToString());
            return ff;
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
        public string validad_cuit(Int64 tipo)
        {
#pragma warning disable CS0219 // La variable 'bb' está asignada pero su valor nunca se usa
            Boolean bb = false;
#pragma warning restore CS0219 // La variable 'bb' está asignada pero su valor nunca se usa

            var url = "https://soa.afip.gob.ar/sr-padron/v2/persona/" + tipo;

            // Syncronious Consumption
            var syncClient = new WebClient();
            var content = syncClient.DownloadString(url);

            // Create the Json serializer and parse the response
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Be.RootObject));

            string sal = "";
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content)))
            {
                // deserialize the JSON object using the WeatherData type.
                var RootObject = (Be.RootObject)serializer.ReadObject(ms);
 

                    if (RootObject.data != null)
                    {

                        // verificar q no sea null la direccion
                        //if (RootObject.data.domicilioFiscal.direccion != null) { txtDomicilio.Text = RootObject.data.domicilioFiscal.direccion + " - Cod Postal:" + RootObject.data.domicilioFiscal.codPostal; }

                        //txtNombre.Text = RootObject.data.nombre;

                        bb = true;

                    }
                    else
                    {
                        bb = false;
                        sal = "El número de cuit no existe";
                    }


                

                }
              
          

            return sal;


        }

        public string validad_doc(Int64 tipo)
        {
#pragma warning disable CS0219 // La variable 'bb' está asignada pero su valor nunca se usa
            Boolean bb = false;
#pragma warning restore CS0219 // La variable 'bb' está asignada pero su valor nunca se usa

            var url = "https://soa.afip.gob.ar/sr-padron/v2/personas/" + tipo;

            // Syncronious Consumption
            var syncClient = new WebClient();
            var content = syncClient.DownloadString(url);

            // Create the Json serializer and parse the response
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Be.RootObject));

            string sal = "";
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content)))
            {
                // deserialize the JSON object using the WeatherData type.
                var RootObject = (Be.RootObject)serializer.ReadObject(ms);


                if (RootObject.data != null)
                {

                    // verificar q no sea null la direccion
                    //if (RootObject.data.domicilioFiscal.direccion != null) { txtDomicilio.Text = RootObject.data.domicilioFiscal.direccion + " - Cod Postal:" + RootObject.data.domicilioFiscal.codPostal; }

                    //txtNombre.Text = RootObject.data.nombre;

                    bb = true;

                }
                else
                {
                    bb = false;
                    sal = "El número de documento no existe";
                }




            }



            return sal;


        }
        public List<Fact_detalle> lote_detalle(string ids)
        {
            List<Fact_detalle> lista = new List<Fact_detalle>();
            try
            {

                string cmdTxt = "";

                cmdTxt = "select  DocTipo,DocNro,CbteDesde, CbteFch,ImpTotal,ImpNeto,ImpIVA,CAE,CAEFchVto,Observaciones,Id_lote from LOTE_DETALLE where Id_lote='" + ids + "'";
               

                cmm = cnn.CrearNuevoComando(cmdTxt, sqlcnn, "");

                SqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {
                    Fact_detalle ff = new Fact_detalle();
                    ff.DocTipo = VeriInt(lector, "DocTipo");

                    ff.DocNro = Convert.ToInt64(lector["DocNro"]);
                    ff.CbteDesde = VeriInt(lector, "CbteDesde");
                    ff.ImpTotal = Convert.ToDouble(lector["ImpTotal"]);
                    ff.ImpNeto = Convert.ToDouble(lector["ImpNeto"]);
                    ff.ImpIVA = Convert.ToDouble(lector["ImpIVA"]);
                    ff.CAE = VerifString(lector, "CAE");
                     

                    ff.CbteFch = VerifString(lector, "CbteFch");
                    ff.CAEFchVto = VerifString(lector, "CAEFchVto");
                    ff.Observaciones = VerifString(lector, "Observaciones");
                    ff.Id_lote = VeriInt(lector, "Id_lote");
                    lista.Add(ff);

                }
                //cerrar lector
                lector.Close();

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
           

        }

        public List<Fact_Cab> Consulta_lote(string ids_tipo, string v_usuario)
        {
            List<Fact_Cab> lista = new List<Fact_Cab>();
            try
            {

                string cmdTxt = "";

                cmdTxt = "select Id ,CantReg, CbteTipo, PtoVta,aud, usuario, Resultado from LOTE where  CbteTipo='" + ids_tipo + "' and usuario ='" + v_usuario + "'  order by Id";


                cmm = cnn.CrearNuevoComando(cmdTxt, sqlcnn, "");

                SqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {
                    Fact_Cab ff = new Fact_Cab();
               

                    ff.aud = Convert.ToDateTime(lector["aud"]);
                    ff.CantReg = VeriInt(lector, "CantReg");
                    ff.CbteTipo = VeriInt(lector, "CbteTipo");
                    ff.id = VeriInt(lector, "id");
                    ff.PtoVta = VeriInt(lector, "PtoVta");
                    string rr = VerifString(lector, "Resultado");


                    if (rr == "A")
                    {
                        ff.Resultado = "AUTORIZADO";
                    }
                    if (rr == "R")
                    {
                        ff.Resultado = "RECHAZADO";
                    }
                    if (rr == "P")
                    {
                        ff.Resultado = "PARCIAL";
                    }
                    lista.Add(ff);

                }
                //cerrar lector
                lector.Close();

                return lista;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public List<Fact_Error> lote_err(string ids)
        {
            List<Fact_Error> lista = new List<Fact_Error>();
            try
            {

                string cmdTxt = "";

                cmdTxt = "select  id_lote,Cod,msg from LOTE_ERROR where id_lote='" + ids + "'";


                cmm = cnn.CrearNuevoComando(cmdTxt, sqlcnn, "");

                SqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {
                    Fact_Error ff = new Fact_Error();
                    ff.id_lote = VeriInt(lector, "id_lote"); 
                    ff.Cod = VerifString(lector, "Cod");
                    ff.msg = VerifString(lector, "msg");
                   
                    lista.Add(ff);

                }
                //cerrar lector
                lector.Close();

                return lista;
            }
            catch (Exception)
            {

                throw;
            }


        }



        public void InsertLoteDetalle(Fact_detalle v_facts)
        {

            try
            {
                string cmdTxt = "insert into LOTE_DETALLE (Concepto,DocTipo,DocNro,CbteDesde,CbteHasta,CbteFch,ImpTotal,ImpTotConc,ImpNeto,ImpOpEx,ImpTrib,ImpIVA,MonId,MonCotiz,CAE,CAEFchVto,Observaciones,Id_lote)  values ";

                cmdTxt += " ('" + v_facts.Concepto + "','" + v_facts.DocTipo + "','" + v_facts.DocNro + "','" + v_facts.CbteDesde + "','" + v_facts.CbteHasta + "','" + v_facts.CbteFch + "','" + v_facts.ImpTotal + "','" + v_facts.ImpTotConc + "','" + v_facts.ImpNeto + "','" + v_facts.ImpOpEx + "','" + v_facts.ImpTrib + "','" + v_facts.ImpIVA + "','" + v_facts.MonId + "','" + v_facts.MonCotiz + "','" + v_facts.CAE + "','" + v_facts.CAEFchVto + "','" + v_facts.Observaciones + "','" + v_facts.Id_lote + "')";


                cmm = cnn.CrearNuevoComando(cmdTxt, sqlcnn, ""); 
                cnn.ExecuteNonQuery(cmm);
               
            }
            catch
            {
                throw;
            }
            finally
            {
                //cnn.Close(cmm);
            }

        }

        public void InsertLoteErr(Fact_Error v_facts)
        {

            try
            {
                string cmdTxt = "insert into LOTE_ERROR (id_lote,Cod,msg)  values ('" + v_facts.id_lote + "','" + v_facts.Cod + "','" + v_facts.msg + "') ";

               

                cmm = cnn.CrearNuevoComando(cmdTxt, sqlcnn, "");
                cnn.ExecuteNonQuery(cmm);

            }
            catch
            {
                throw;
            }
            finally
            {
                //cnn.Close(cmm);
            }

        }
        public Int32 maxId( )
        {
           Int32 pp = 0;
            try
            {
                string cmdTxt = "SELECT max(id) as cant from LOTE  ";

                cmm = cnn.CrearNuevoComando(cmdTxt, sqlcnn, ""); 

                SqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    pp = VeriInt(lector, "cant");

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
               // cnn.Close(cmm);
            }
            return pp ;
        }

        public Int32 VeriInt(SqlDataReader lector, string s)
        {

            if (lector[s] == DBNull.Value)
            {

                return 0;
            }
            else
            {

                return Convert.ToInt32(lector[s]);

            }


        }
        public Int64 VeriInt64(SqlDataReader lector, string s)
        {

            if (lector[s] == DBNull.Value)
            {

                return 0;
            }
            else
            {

                return Convert.ToInt64(lector[s]);

            }


        }

        public string VerifString(SqlDataReader lector, string s)
        {

            if (lector[s] == DBNull.Value)
            {

                return "";
            }
            else
            {

                return Convert.ToString(lector[s]);

            }


        }

    }
}
