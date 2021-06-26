using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bll;
using Be;
using System.Configuration;
using MercadoPago;
using MercadoPago.Resources;
using MercadoPago.DataStructures.Preference;
using System.Net;
using MercadoPago.Common;
using MercadoPago;
using MercadoPago.DataStructures.Payment;
using MercadoPago.Resources;
using Payer = MercadoPago.DataStructures.Payment.Payer;
using VbWsaa;
//using factElectForm.wsfeProd;     //Produccion
using  Sport.wsfe;  // desa Homo
using System.Globalization;
using System.Threading;
using Sport.Models;
using System.Data;
using Microsoft.Reporting.WinForms;
using System.Xml;
using System.Xml.Linq;
using System.Security;
using System.Security.Permissions;
using System.IO;

namespace Sport.Controllers
{
    public class ArticulosController : ApplicationController
    {

        //public ActionResult MercadoLIbre()
        //{
        //    //MercadoPago.SDK.ClientId = "6240899172095397";
        //    //MercadoPago.SDK.ClientSecret = "KJr36nf24calJ3SaOSc6x8jHBC0WBN0Z";
        //    // ...

        //    string ip = "";
          

        //    if (string.IsNullOrEmpty(MercadoPago.SDK.AccessToken))
        //    {
        //        MercadoPago.SDK.AccessToken = "TEST-6240899172095397-050723-aa486240a6d3013702277be660a18dc9-79043161";

        //    }

        //    Shipment sp = new Shipment();
        //    sp.Mode = ShipmentMode.Me2;
        //    sp.Dimensions = "30x30x30,500";

        //    Preference preference = new Preference();

        //    preference.Shipments = sp;
        //    Item mItem = new Item();

        //    mItem.Title = "AQ1779-100 WMNS NIKE EBERNON LOW WHITE";
        //    mItem.Quantity = 1;
        //    mItem.PictureUrl = "https://www.mercadopago.com/org-img/MP3/home/logomp3.gif"; 
        //    mItem.UnitPrice = (decimal)75.56;
            

        //    preference.Items.Add(mItem);
        //   // preference.Id = "dsa";
        //    preference.Payer = new Payer()
        //    {
        //        Email = "lsgarcia2009@gmail.com"
        //    };

        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;


        //    preference.BackUrls = new BackUrls()
        //    {
        //        Success = "https://www.tu-sitio/success",
        //        Failure = "http://www.tu-sitio/failure",
        //        Pending = "http://www.tu-sitio/pendings"
        //    };
        //    preference.AutoReturn = AutoReturnType.approved;

         

        //    preference.Save();


        //    ip = preference.Id;


        //    return base.Json(ip);


        //}
     
        public ActionResult Index()
        {
           // MercadoLIbre();


            HttpContext.Session["ActiveSession"] = DateTime.Now.ToString();

            if (HttpContext.Session["Menu"] == null)
            {
                Session["Menu"] = Bll.BllBest.DameInstancia().best_Menu();
            }
            ViewData["menu"] = (List<Be.Best_Menu>)HttpContext.Session["Menu"]; 

            if (Request.QueryString != null)
            {
                // desencriptar parametros
                QueryString obQueryString = new QueryString(Request.QueryString);
                obQueryString = Bll.Encriptor.DameInstancia().DecryptQueryString(obQueryString);
                int? vtipo = Convert.ToInt32(  obQueryString["vtipo"]);
                int? vcat = Convert.ToInt32(obQueryString["vcat"]);
                int? vtipocat = Convert.ToInt32(obQueryString["vtipocat"]);
                // int? vop = Convert.ToInt32(obQueryString["vop"]);

                
                ViewData["articulosventa"] = Bll.BllBest.DameInstancia().listaArticulosVenta(vtipo, vcat, vtipocat);

            }
            else
            {
                return Redirect("/");
            }


             

            return View();
        }
        public ActionResult Mp()
        {

            return View();
        }
        public ActionResult Checkout()
        {
            Session["Envio"] = null;
            return View();
        }

        public ActionResult confirmarRegitro(string vtoken, string url)
        {
            try
            {

                if (!string.IsNullOrEmpty(vtoken))
                {

                    BllBest.DameInstancia().ConfitmarRegistrarcion(vtoken.Replace("=", "").Replace("'", ""));

                }
                else
                {

                    return RedirectToAction("Index");
                }
                if (string.IsNullOrEmpty(url))
                {
                    RedirectToAction("Index");
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Redirect(url.Replace("_","&"));
        }
        List<Be.ItemFact> ListItem = new List<Be.ItemFact>();

        int idItem = 0;


        public ActionResult procesarPago()
        {
            if (Request.QueryString != null)
            {
                // desencriptar parametros
                QueryString obQueryString = new QueryString(Request.QueryString);
                obQueryString = Bll.Encriptor.DameInstancia().DecryptQueryString(obQueryString);
               string mp =  obQueryString["mp"];
                string tipo =  obQueryString["tipo"];
                if (mp!=null)
                {
                    mercadopagos mp1 = BllBest.DameInstancia().BsqPago(mp);
                    Session["mp"] = mp1;
                }
                

            }
            
            return View();
        }
        


        public ActionResult rechazoPago()
        {
            mercadopagos mp1 = new mercadopagos();
            mp1 = (mercadopagos)Session["mp"]?? new mercadopagos();
            return View();
        }
        public ActionResult RpCbte1()
        {


            try
            {
                LocalReport localReport = new LocalReport();
                Bll.bllFactura bllFac = new Bll.bllFactura();


                if (true)
                {


                    localReport.SetBasePermissionsForSandboxAppDomain(new PermissionSet(PermissionState.Unrestricted));
                     localReport.ReportPath = @"\\hmfsw\web\DTC023\w230176.ferozo.com\public_html\barcodeIMG\Prueba.rdlc";
                    //var reportStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Sport.Views.report.Prueba.rdlc");
                    //localReport.LoadReportDefinition(reportStream);
  
                


                    string reportType = "PDF";
                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    string deviceInfo =
                    "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +
                    "  <PageWidth>21cm</PageWidth>" +
                    "  <PageHeight>29.7cm</PageHeight>" +
                    "  <MarginTop>0.05in</MarginTop>" +
                    "  <MarginLeft>0.5cm</MarginLeft>" +
                    "  <MarginRight>0.4cm</MarginRight>" +
                    "  <MarginBottom>0.5cm</MarginBottom>" +
                    "</DeviceInfo>";
                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;


                    //Render the report
                    renderedBytes = localReport.Render(
                        reportType,
                        deviceInfo,
                        out mimeType,
                        out encoding,
                        out fileNameExtension,
                        out streams,
                        out warnings);




                    return File(renderedBytes, mimeType);

                }

            }
            catch (Exception)
            {

                throw;
            }


        }

        public ActionResult RpCbte(string cbate)
        {


            try
            {
                LocalReport localReport = new LocalReport();
                Bll.bllFactura bllFac = new Bll.bllFactura();

                DataSet ds = new DataSet();
                if (cbate != "")
                {
                    ds = bllFac.RpFc(cbate);
                }
                else
                {
                    throw new Exception("No existe.");
                }

                if (true)
                {



                    localReport.ReportEmbeddedResource = bllFac.PathReportCbte(ds.Tables["cab"].Rows[0]["Cod_Letra"].ToString());
                    string sal = ds.Tables["cab"].Rows[0]["CodBarra"].ToString();
                    string salida = GenBarra.GenerarAFIP(sal);

                    //byte[] arrImg = user.Config.Logo;
                    //string salida2 = Convert.ToBase64String(arrImg);



                    // todo esto es por el hosting no funciona cuando habilito parametros externos en el report

                    DataTable DatosExt = new DataTable("DatosExt");
                    DataColumn colDato1 = new DataColumn("dato1", typeof(String));
                    DatosExt.Columns.Add(colDato1);
                    DataColumn colDato2 = new DataColumn("dato2", typeof(String));
                    DatosExt.Columns.Add(colDato2);
                    DataColumn colDato3 = new DataColumn("dato3", typeof(String));
                    DatosExt.Columns.Add(colDato3);
                    DataRow row1 = DatosExt.NewRow();
                    row1["dato1"] = salida;
                    row1["dato2"] = "";
                    row1["dato3"] = "";
                    DatosExt.Rows.Add(row1);

                    ReportDataSource reportDataSource1 = new ReportDataSource("Cab", ds.Tables["cab"]);
                    ReportDataSource reportDataSource2 = new ReportDataSource("Det", ds.Tables["det"]);
                    ReportDataSource reportDataSource3 = new ReportDataSource("Iva", ds.Tables["Iva"]);
                    ReportDataSource reportDataSource = new ReportDataSource("DatosExt", DatosExt);




                    localReport.DataSources.Add(reportDataSource1);
                    localReport.DataSources.Add(reportDataSource2);
                    localReport.DataSources.Add(reportDataSource3);
                    localReport.DataSources.Add(reportDataSource);



                    string reportType = "PDF";
                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    string deviceInfo =
                    "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +
                    "  <PageWidth>21cm</PageWidth>" +
                    "  <PageHeight>29.7cm</PageHeight>" +
                    "  <MarginTop>0.05in</MarginTop>" +
                    "  <MarginLeft>0.5cm</MarginLeft>" +
                    "  <MarginRight>0.4cm</MarginRight>" +
                    "  <MarginBottom>0.5cm</MarginBottom>" +
                    "</DeviceInfo>";
                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;


                    //Render the report
                    renderedBytes = localReport.Render(
                        reportType,
                        deviceInfo,
                        out mimeType,
                        out encoding,
                        out fileNameExtension,
                        out streams,
                        out warnings);

             


                    return File(renderedBytes, mimeType);

                }

            }
            catch (Exception)
            {

                throw;
            }

           
        }
        string mpPrivado = ConfigurationManager.AppSettings.Get("mpPrivadoTk").ToString();

        [HttpPost]
        public ActionResult Mod_user(string v_pwd)
        {
       bool sal = false;
            try
            {
                Be.Best_UsuariosWeb user = (Best_UsuariosWeb)Session["UsuarioWeb"];
                BllBest.DameInstancia().Modificar_pwd(user.mail, Encriptor.DameInstancia().GetMD5(v_pwd.Trim()));
                sal = true;
            }
            catch (Exception ex)
            {

                return ThrowJsonError(
                           new Exception(String.Format(ex.Message, " ")));
            }
            return base.Json(sal);
        }

        [HttpPost]
        public ActionResult Select_Sucursal(string v_id, string v_nombre)
        {
            
            try
            {
                Combos cmb = new Combos();
                cmb.descripcion = v_nombre;
                cmb.valor = v_id;
                Session["SelectSuc"] = cmb;
                 
            }
            catch (Exception ex)
            {

                return ThrowJsonError(
                           new Exception(String.Format(ex.Message, " ")));
            }
            return base.Json(true);
        }

        [HttpPost]
        public ActionResult verCentros(Int32 v_cp)
        {
            List<Combos> lista = new List<Combos>();
            List<Oca_centros> listaCentro = new List<Oca_centros>();
            try
            {
                WsOca.Oep_TrackingSoapClient oca = new WsOca.Oep_TrackingSoapClient("Oep_TrackingSoap12");

                System.Xml.XmlNode root =   oca.GetCentrosImposicionConServiciosByCP(v_cp); 
                XmlNodeList nodeList;
                XmlNodeList nodeServicios; 
                nodeList = root.SelectNodes("/Centro");
              

                foreach (XmlNode lugares in nodeList)
                { 

                    nodeServicios = lugares.SelectNodes("Servicios/Servicio");
                    foreach (XmlNode servicio in nodeServicios)
                    {
                  
                        if (servicio.SelectSingleNode("IdTipoServicio").InnerText == "2") //Entrega de paquetes
                        {
                            Oca_centros entidad = new Oca_centros();
                            entidad.Calle = lugares.SelectSingleNode("Calle").InnerText;
                            entidad.CodigoPostal = lugares.SelectSingleNode("CodigoPostal").InnerText;
                            entidad.IdCentroImposicion = lugares.SelectSingleNode("IdCentroImposicion").InnerText;
                            entidad.Latitud = lugares.SelectSingleNode("Latitud").InnerText;
                            entidad.Localidad = lugares.SelectSingleNode("Localidad").InnerText;
                            entidad.Longitud = lugares.SelectSingleNode("Longitud").InnerText;
                            entidad.Numero = lugares.SelectSingleNode("Numero").InnerText;
                            entidad.Provincia = lugares.SelectSingleNode("Provincia").InnerText;
                            entidad.Sigla = lugares.SelectSingleNode("Sigla").InnerText;
                            entidad.Sucursal = lugares.SelectSingleNode("Sucursal").InnerText;
                            entidad.SucursalOCA = lugares.SelectSingleNode("SucursalOCA").InnerText;
                            entidad.Telefono = lugares.SelectSingleNode("Telefono").InnerText;
                            entidad.TipoAgencia = lugares.SelectSingleNode("TipoAgencia").InnerText;
                            listaCentro.Add(entidad);
                        }

                    }
                   


                }

                

            }
            catch (Exception ex)
            {

                return ThrowJsonError(
                           new Exception(String.Format(ex.Message, " ")));
            }

            return base.Json(listaCentro);

        }
        [HttpPost]
        public ActionResult Tipo_Talles(string v_talle)
        {
            List<Combos> list = new List<Combos>();
            try
            {
                list= BllBest.DameInstancia().tipos_talles( v_talle); 

            }
            catch (Exception ex)
            {

                return ThrowJsonError(
                           new Exception(String.Format(ex.Message, " ")));
            }

            return base.Json(list);

        }



         [HttpPost]
        public ActionResult CalEnvio(string v_cp, string CANT, string v_valor)
        { 
           string sal = "";
            
            List<Combos> lista = new List<Combos>();

            DataSet ds = new DataSet();
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-AR");
                var numberFormatInfo = new System.Globalization.NumberFormatInfo();
                numberFormatInfo.NumberDecimalSeparator = ".";
                numberFormatInfo.CurrencyPositivePattern = 0;

                WsOca.Oep_TrackingSoapClient oca = new WsOca.Oep_TrackingSoapClient("Oep_TrackingSoap12");
             
                Best_articulos_ventaId artId = (Best_articulos_ventaId)Session["vart"];
                string CP_ORIGEN = ConfigurationManager.AppSettings.Get("CP_ORIGEN").ToString();

                string OP_pap= ConfigurationManager.AppSettings.Get("OPERATIVA").ToString();
                string OP_pas = ConfigurationManager.AppSettings.Get("OPERATIVA_PAS").ToString();

                string CUIT = ConfigurationManager.AppSettings.Get("CuitEmpresa").ToString();
                double v_valor1 = @Math.Round((Double.Parse(v_valor.Replace(",", "."), numberFormatInfo)  * Double.Parse(CANT)), 2) ;
                double v_VolumenTotal = @Math.Round((Double.Parse(artId.VolumenTotal.Replace(",", "."), numberFormatInfo) * Double.Parse(CANT)), 2);
                double v_altoTotal = 0;
                double v_anchoTotal = 0;
                double v_largoTotal = 0;
                //envio local
                Combos NN = Bll.BllBest.DameInstancia().envioLocal(v_cp);
                    sal = NN.descripcion;
                Combos cmbDomicilio = new Combos();
                cmbDomicilio.valor = "";
                if (!string.IsNullOrEmpty( sal))
                {
                    cmbDomicilio.descripcion = sal;
                    lista.Add(cmbDomicilio);
                    lista.Add(cmbDomicilio);
                }
                else { // si no tiene envio local el cp m fijo en oca
                 
                if (Double.Parse(CANT) > 1)
                {
                    v_altoTotal =  @Math.Round((Double.Parse(artId.alto_embalaje.Replace(",", "."), numberFormatInfo) * Double.Parse( (CANT))), 2);
                   // siempre dejo el mayor
                            v_anchoTotal = Double.Parse(artId.ancho_embalaje.Replace(",", "."), numberFormatInfo); 
                 // siempre dejo el mayor
                            v_largoTotal = Double.Parse(artId.largo_embalaje.Replace(",", "."), numberFormatInfo); 
                    v_VolumenTotal = v_largoTotal * v_anchoTotal * v_altoTotal * 0.000001;

                }
                // puerta a puerta 
                ds =    oca.Tarifar_Envio_Corporativo(artId.Peso.Replace(",", "."), v_VolumenTotal.ToString().Replace(",", "."), Convert.ToInt32( CP_ORIGEN), Convert.ToInt32(v_cp), Convert.ToInt32(CANT), v_valor1.ToString().Replace(",", "."), CUIT, OP_pap);
                // ds.Tables[0].Rows[0]["PlazoEntrega"] , ds.Tables[0].Rows[0]["Total"] 
               
                if (ds.Tables[0].Columns.Count > 1)
                    {
                        sal = "" + string.Format("{0:c2}", Math.Round( (Double.Parse(ds.Tables[0].Rows[0]["Total"].ToString().Replace(",", "."), numberFormatInfo) * Double.Parse("1.21".Replace(",", "."), numberFormatInfo)), 2))  ;
                        cmbDomicilio.valor = "Plazo de entrega: " + ds.Tables[0].Rows[0]["PlazoEntrega"] + " días";
                    }

                    else {
                        cmbDomicilio.valor = "";
                        sal = "No se ha podido calcular el envio.  ( " + ds.Tables[0].Rows[0][0].ToString()+" )";
                }
                    cmbDomicilio.descripcion = sal;
                  
               
                ds = new DataSet();
                Combos cmdSucursal = new Combos();
               
                // puerta a puerta
                ds = oca.Tarifar_Envio_Corporativo(artId.Peso.Replace(",", "."), v_VolumenTotal.ToString().Replace(",", "."), Convert.ToInt32(CP_ORIGEN), Convert.ToInt32(v_cp), Convert.ToInt32(CANT), v_valor1.ToString().Replace(",", "."), CUIT, OP_pas);
                // ds.Tables[0].Rows[0]["PlazoEntrega"] , ds.Tables[0].Rows[0]["Total"] 

                if (ds.Tables[0].Columns.Count > 1)
                {
                    sal = "" + string.Format("{0:c2}", Math.Round((Double.Parse(ds.Tables[0].Rows[0]["Total"].ToString().Replace(",", "."), numberFormatInfo) * Double.Parse("1.21".Replace(",", "."), numberFormatInfo)), 2));
                        cmdSucursal.valor = "Plazo de entrega: " + ds.Tables[0].Rows[0]["PlazoEntrega"] + " días";
                    }
                else
                    {
                        cmdSucursal.valor = "";
                        sal = "No se ha podido calcular el envio a una sucursal .  ( " + ds.Tables[0].Rows[0][0].ToString() + " )";
                }
                // System.Xml.XmlNode dsw=   oca.GetCentrosImposicionConServiciosByCP(1882 );
                cmdSucursal.descripcion = sal;
               
                lista.Add(cmbDomicilio);
                lista.Add(cmdSucursal);
                }


            }
            catch (Exception ex)
            {

                return ThrowJsonError(
                           new Exception(String.Format(ex.Message, " ")));
            }
            return base.Json(lista);
        }

        [HttpPost]
        public ActionResult CalEnvioCkeckout(string v_cp , string v_envio)
        {
            string sal = "";
            string sal2 = "";

            List<Combos> lista = new List<Combos>();

            DataSet ds = new DataSet();
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-AR");
                var numberFormatInfo = new System.Globalization.NumberFormatInfo();
                numberFormatInfo.NumberDecimalSeparator = ".";
                numberFormatInfo.CurrencyPositivePattern = 0;

                List<Be.Best_articulos_carrito> carrito = new List<Best_articulos_carrito>();
                if (Session["carrito"] != null)
                {
                    carrito = (List<Be.Best_articulos_carrito>)Session["carrito"];
                }

                double v_valorTotal = 0;
                double v_volumenTotal = 0;
                double v_pesoTotal = 0;
                double v_altoTotal = 0;
                double v_anchoTotal = 0;
                double v_largoTotal = 0;
                string v_costoEnvio = "0";
                string tipo_envio = "";
                List<string> listVal = new List<string>();
                foreach (var item in carrito)
                {
                    v_valorTotal = v_valorTotal + @Math.Round((Double.Parse(item.precio.Replace(",", "."), numberFormatInfo) * Double.Parse(item.cant)), 2);
                    // v_volumenTotal = v_volumenTotal + (Double.Parse(item.VolumenTotal.Replace(",", "."), numberFormatInfo) * Double.Parse(item.cant)) ;
                    v_pesoTotal = v_pesoTotal + @Math.Round((Double.Parse(item.Peso.Replace(",", "."), numberFormatInfo) * Double.Parse(item.cant)), 2);
                    v_altoTotal = v_altoTotal + @Math.Round((Double.Parse(item.alto_embalaje.Replace(",", "."), numberFormatInfo) * Double.Parse(item.cant)), 2);
                    // v_anchoTotal = v_anchoTotal + @Math.Round((Double.Parse(item.ancho_embalaje.Replace(",", "."), numberFormatInfo) * Double.Parse(item.cant)), 2);
                    // v_largoTotal = v_largoTotal + @Math.Round((Double.Parse(item.largo_embalaje.Replace(",", "."), numberFormatInfo) * Double.Parse(item.cant)), 2);

                    if (v_anchoTotal == 0)
                    {
                        v_anchoTotal = Double.Parse(item.ancho_embalaje.Replace(",", "."), numberFormatInfo);
                    }
                    else
                    {
                        if (v_anchoTotal < Double.Parse(item.ancho_embalaje.Replace(",", "."), numberFormatInfo))
                        { // siempre dejo el mayor
                            v_anchoTotal = Double.Parse(item.ancho_embalaje.Replace(",", "."), numberFormatInfo);
                        }
                    }


                    if (v_largoTotal == 0)
                    {
                        v_largoTotal = Double.Parse(item.largo_embalaje.Replace(",", "."), numberFormatInfo);
                    }
                    else
                    {
                        if (v_anchoTotal < Double.Parse(item.largo_embalaje.Replace(",", "."), numberFormatInfo))
                        { // siempre dejo el mayor
                            v_largoTotal = Double.Parse(item.largo_embalaje.Replace(",", "."), numberFormatInfo);
                        }
                    }

                }

                v_volumenTotal = v_largoTotal * v_anchoTotal * v_altoTotal * 0.000001;



                WsOca.Oep_TrackingSoapClient oca = new WsOca.Oep_TrackingSoapClient("Oep_TrackingSoap12");

                Best_articulos_ventaId artId = (Best_articulos_ventaId)Session["vart"];
                string CP_ORIGEN = ConfigurationManager.AppSettings.Get("CP_ORIGEN").ToString();

                string OP_pap = ConfigurationManager.AppSettings.Get("OPERATIVA").ToString();
                string OP_pas = ConfigurationManager.AppSettings.Get("OPERATIVA_PAS").ToString();

                string CUIT = ConfigurationManager.AppSettings.Get("CuitEmpresa").ToString();
             
                //envio local
                Combos cmbenviolocal = Bll.BllBest.DameInstancia().envioLocal(v_cp);
                sal = cmbenviolocal.descripcion;
                Combos cmbDomicilio = new Combos();
                cmbDomicilio.valor = "";
                Combos cmbSelect = new Combos();
                cmbSelect.valor = "0";
                Combos cmdSucursal = new Combos();
                cmdSucursal.valor = "0";

                if (v_envio == "ENLOCAL") // lo retira en el local
                {
                    cmbSelect.valor ="0";
                    cmbSelect.descripcion ="LO RETIRA EN LOCAL";
                }
                else { // si no lo va a buscar al local se lo envio

                if (!string.IsNullOrEmpty(sal))
                {
                    cmbDomicilio.descripcion = sal;
                   // cmbDomicilio.valor = cmbenviolocal.valor;
                     
                    sal2 = cmbenviolocal.valor;

                    if (v_envio == "OP_pap")
                    {
                        cmbSelect.valor = sal2;
                        cmbSelect.descripcion = "ENVIO LOCAL";
                    }
                }
                else
                { // si no tiene envio local el cp m fijo en oca

                    // puerta a puerta 
                    ds = oca.Tarifar_Envio_Corporativo(v_pesoTotal.ToString().Replace(",", "."), v_volumenTotal.ToString().Replace(",", "."), Convert.ToInt32(CP_ORIGEN), Convert.ToInt32(v_cp), Convert.ToInt32("1"), Convert.ToInt32(v_valorTotal).ToString(), CUIT, OP_pap);

                    // ds.Tables[0].Rows[0]["PlazoEntrega"] , ds.Tables[0].Rows[0]["Total"] 

                    if (ds.Tables[0].Columns.Count > 1)
                    {
                        sal = "" + string.Format("{0:c2}", Math.Round((Double.Parse(ds.Tables[0].Rows[0]["Total"].ToString().Replace(",", "."), numberFormatInfo) * Double.Parse("1.21".Replace(",", "."), numberFormatInfo)), 2));
                        cmbDomicilio.valor = "Plazo de entrega: " + ds.Tables[0].Rows[0]["PlazoEntrega"] + " días";
                        sal2 = Math.Round((Double.Parse(ds.Tables[0].Rows[0]["Total"].ToString().Replace(",", "."), numberFormatInfo) * Double.Parse("1.21".Replace(",", "."), numberFormatInfo)), 2).ToString();
                    }

                    else
                    {
                        sal2 = "0";
                        cmbDomicilio.valor = "";
                        sal = "No se ha podido calcular el envio.  ( " + ds.Tables[0].Rows[0][0].ToString() + " )";
                    }
                    cmbDomicilio.descripcion = sal;

                    if (v_envio == "OP_pap")
                    {
                        cmbSelect.valor = sal2;
                        cmbSelect.descripcion = "PUERTA A PUERTA";
                    }


                    ds = new DataSet();
                   

                    // puerta a sucursañ
 
                    ds = oca.Tarifar_Envio_Corporativo(v_pesoTotal.ToString().Replace(",", "."), v_volumenTotal.ToString().Replace(",", "."), Convert.ToInt32(CP_ORIGEN), Convert.ToInt32(v_cp), Convert.ToInt32("1"), Convert.ToInt32(v_valorTotal).ToString(), CUIT, OP_pas);

                    // ds.Tables[0].Rows[0]["PlazoEntrega"] , ds.Tables[0].Rows[0]["Total"] 

                    if (ds.Tables[0].Columns.Count > 1)
                    {
                        sal = "" + string.Format("{0:c2}", Math.Round((Double.Parse(ds.Tables[0].Rows[0]["Total"].ToString().Replace(",", "."), numberFormatInfo) * Double.Parse("1.21".Replace(",", "."), numberFormatInfo)), 2));
                        cmdSucursal.valor = "Plazo de entrega: " + ds.Tables[0].Rows[0]["PlazoEntrega"] + " días";
                        sal2 = Math.Round((Double.Parse(ds.Tables[0].Rows[0]["Total"].ToString().Replace(",", "."), numberFormatInfo) * Double.Parse("1.21".Replace(",", "."), numberFormatInfo)), 2).ToString();
                    }
                    else
                    {
                        sal2 = "0";
                        cmdSucursal.valor = "";
                        sal = "No se ha podido calcular el envio a una sucursal .  ( " + ds.Tables[0].Rows[0][0].ToString() + " )";
                    }
                    // System.Xml.XmlNode dsw=   oca.GetCentrosImposicionConServiciosByCP(1882 );
                    cmdSucursal.descripcion = sal;

                    if (v_envio == "OP_pas")
                    {
                        cmbSelect.valor = sal2;
                        cmbSelect.descripcion = "PUERTA A SUCURSAL";
                    }

                    
                }
                }
                lista.Add(cmbDomicilio);
                lista.Add(cmdSucursal);
                lista.Add(cmbSelect);

                listVal.Add(v_volumenTotal.ToString());
                listVal.Add(v_pesoTotal.ToString());
                listVal.Add(v_valorTotal.ToString());
                listVal.Add(v_altoTotal.ToString());
                listVal.Add(v_anchoTotal.ToString());
                listVal.Add(v_largoTotal.ToString());
                listVal.Add(cmbSelect.valor);
                listVal.Add(cmbSelect.descripcion);
                Session["envioValores"] = listVal;

                Session["envioTotal"] = cmbSelect.valor;
                string vTotalP = "0";
                if (Session["pesosTotal"] != null)
                {
                    vTotalP = Session["pesosTotal"].ToString();
                }
                Session["pesosTotalConEnvio"] = Math.Round((Double.Parse(vTotalP.ToString().Replace(",", "."), numberFormatInfo)) + (Double.Parse(cmbSelect.valor.ToString().Replace(",", "."), numberFormatInfo)), 2).ToString();

            }
            catch (Exception ex)
            {

                return ThrowJsonError(
                           new Exception(String.Format(ex.Message, " ")));
            }
            return base.Json(lista);
        }

 
       
        public void  EnvioOrden()
        {
            string sal = "";
            DataSet ds = new DataSet();
            try
            { 
                WsOca.Oep_TrackingSoapClient oca = new WsOca.Oep_TrackingSoapClient("Oep_TrackingSoap12");
 
                string USROCA = ConfigurationManager.AppSettings.Get("USROCA").ToString();
                string PSWOCA = ConfigurationManager.AppSettings.Get("PSWOCA").ToString();
      
                Best_envios env =(Best_envios) Session["Envio"];
                Be.Best_UsuariosWeb user = (Best_UsuariosWeb)Session["UsuarioWeb"];
                string mXml = XmlOca(env.remito, user.apellido, user.nombre, env.calle, env.nro, env.Localidad, env.Provincia, env.piso, env.dpto, env.cp, user.tel, user.mail, env.alto.Replace(",", "."), env.ancho.Replace(",", "."), env.largo.Replace(",", "."), env.peso.Replace(".", "").Replace(",", "."), env.Monto.Replace(".", "").Replace(",","."), "1",env.TIPO_ENVIO,env.SEC_SELECT_ID);                                 

                ds = oca.IngresoORMultiplesRetiros(USROCA, PSWOCA, mXml,false,"","");
                // ds.Tables[0].Rows[0]["PlazoEntrega"] , ds.Tables[0].Rows[0]["Total"] 

                if (ds.Tables[0].Columns.Count > 1)
                {
                    sal = "  ";
                }
                else
                {
                    sal = "No se ha podido hacer el envio.  ( " + ds.Tables[0].Rows[0][0].ToString() + " )";
                }


            }
            catch (Exception ex)
            {

                throw;
            }
            
        }


        public string XmlOca(string v_remito, string dest_ape, string dest_nom,string dest_calle, string dest_nro, string dest_local, string dest_prov, string dest_piso, string dest_dpto, string dest_cp, string dest_tel, string dest_mail, string v_alto, string v_ancho, string v_largo, string v_peso, string v_valor, string v_cant, string v_tipoEnvio, string v_centro)
        {
            string operativa = "";
                if (v_tipoEnvio== "PUERTA A SUCURSAL") {
                operativa = ConfigurationManager.AppSettings.Get("OPERATIVA_PAS").ToString();
            }
            if (v_tipoEnvio == "PUERTA A PUERTA")
            {
                operativa = ConfigurationManager.AppSettings.Get("OPERATIVA").ToString();
            }
            if (string.IsNullOrEmpty(v_centro))
            {
                v_centro = "0";
            }
            

            string sal = "<?xml version='1.0' encoding='iso-8859-1' standalone='yes'?> <ROWS>";
            sal += "<cabecera ver='2.0' nrocuenta='"+ConfigurationManager.AppSettings.Get("CTAOCA").ToString()+"' />";
            sal += "<origenes>";
            sal += "<origen calle='" + ConfigurationManager.AppSettings.Get("CALLE_ORIGEN").ToString() + "' nro='" + ConfigurationManager.AppSettings.Get("NRO_ORIGEN").ToString() + "' piso='' depto='' cp='" + ConfigurationManager.AppSettings.Get("CP_ORIGEN").ToString() + "'   ";
            sal += " localidad='" + ConfigurationManager.AppSettings.Get("LOCALIDAD_ORIGEN").ToString() + "'  provincia='" + ConfigurationManager.AppSettings.Get("PROVINCIA_ORIGEN").ToString() + "' contacto='' ";
            sal += "email='" + ConfigurationManager.AppSettings.Get("MAIL_ORIGEN").ToString() + "' solicitante='' observaciones='' centrocosto='' idfranjahoraria ='" + ConfigurationManager.AppSettings.Get("franjahoraria").ToString() + "' idcentroimposicionorigen='0' fecha='"+ fechaXml() + "' > <envios> ";
            sal += " <envio idoperativa='" + operativa + "' nroremito='"+v_remito+"'> ";
            sal += "<destinatario apellido='"+ dest_ape + "'  nombre='"+dest_nom+"' calle='"+ dest_calle + "' nro='"+ dest_nro + "' piso='"+ dest_piso + "' depto='" + dest_dpto + "' localidad='" + dest_local + "' provincia='"+ dest_prov + "' ";
            sal += " cp='"+ dest_cp + "' telefono='' email='"+ dest_mail + "' idci='" + v_centro + "'    celular='" + dest_tel + "' observaciones='' /> <paquetes> ";
            sal += " <paquete alto='"+ v_alto + "' ancho='" + v_ancho + "' largo='" + v_largo + "' peso='" + v_peso + "' valor='" + v_valor + "' cant='" + v_cant + "' /> </paquetes></envio></envios></origen></origenes></ROWS>";
            return sal;
        }

        public string fechaXml()
        {
            string vf = "";
            DateTime dayToday = DateTime.Today;
            dayToday = DateTime.Today.AddDays(1);

            if ((dayToday.DayOfWeek == DayOfWeek.Saturday)  )
            {
                dayToday = DateTime.Today.AddDays(2);
            }
            if ((dayToday.DayOfWeek   == DayOfWeek.Sunday))
            {
                dayToday = DateTime.Today.AddDays(1);
            }
            return dayToday.ToString("yyyyMMdd");
        }
        [HttpPost]
        public ActionResult procesarPago(string ds)
        {
            try
            {

                // en esta accion viene cuando preciono el boton de pagar en mercado pago (cuando complete con los datos de la tarjeta)
             
                  mpPrivado = ConfigurationManager.AppSettings.Get("mpPrivadoTk").ToString();
              
              System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                var numberFormatInfo = new System.Globalization.NumberFormatInfo();
                numberFormatInfo.NumberDecimalSeparator = ".";
                numberFormatInfo.CurrencyPositivePattern = 0;


                var pesosTotal = Double.Parse(Session["pesosTotalConEnvio"].ToString().Replace(",", "."), numberFormatInfo);


    
            var token = Request["token"];
                var payment_method_id = Request["payment_method_id"];
                var installments = Request["installments"];
                var issuer_id = Request["issuer_id"];
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

               
               

               if(   MercadoPago.SDK.AccessToken==null  )
                {

                    MercadoPago.SDK.SetAccessToken(mpPrivado);
                }
                if (mpPrivado == "")
                {
                    throw new Exception("token nulo.");
                }

                Be.Best_UsuariosWeb user = (Best_UsuariosWeb)Session["UsuarioWeb"];

                Payment payment = new Payment()
                {
                    TransactionAmount = float.Parse(pesosTotal.ToString()),
                    Token = token,
                    Description = "Compra - Best Deporte",
                    Installments = Convert.ToInt32(installments),
                    PaymentMethodId = payment_method_id,
                    IssuerId = issuer_id,
                    BinaryMode = true,
                    Payer = new Payer()
                    {
                        Email = user.mail
                    }
                };
                // Guarda y postea el pago
                payment.Save();

                Payment pay = payment;
               
                MercadoPago.Common.PaymentStatus Paysal = payment.Status.Value;
            
                 mercadopagos mp = new mercadopagos();
                mp.id_mp = payment.Id.ToString();
                mp.date_approved = payment.DateApproved.Value.ToString("dd/MM/yyyy HH:MM");
                mp.transaction_amount_refunded = payment.TransactionAmountRefunded.ToString();
                mp.Card_FirstSixDigits = payment.Card.Value.FirstSixDigits ?? "";
                mp.Card_LastFourDigits = payment.Card.Value.LastFourDigits ?? "";
                mp.Email = user.mail;
                mp.FirstName = payment.Payer.FirstName ?? "";
                mp.id_usuario = user.id;
                mp.installments = installments;
                mp.issuer_id = issuer_id;
                mp.LastName = payment.Payer.LastName ?? "";
                mp.PaymentMethodId = payment.PaymentMethodId ?? "";
                mp.PaymentTypeId = payment.PaymentTypeId.Value.ToString() ?? "";
                mp.Status = Paysal.ToString() ?? "";
                mp.StatusDetail = payment.StatusDetail ?? "";
                mp.token = token;
                mp.TransactionAmount = payment.TransactionAmount.ToString();
            
                mp.id=      BllBest.DameInstancia().InsertMp(mp);
             
                if (mp.Status == "approved")
                {
              
              mp.cbte=      GenComprobante(mp.id);

                    mp.TipoEnvio = ((Best_envios)Session["Envio"]).TIPO_ENVIO;


                    if (Session["Envio"] != null)
                    {
                       mp.CostoEnvio = ((Best_envios)Session["Envio"]).CostoEnvio ;

                        if (mp.TipoEnvio == "PUERTA A SUCURSAL")
                        {
                            mp.LugarEnvio = ((Best_envios)Session["Envio"]).SEC_SELECT_ID + " - " + ((Best_envios)Session["Envio"]).SEC_SELECT_NOMBRE;
                        }
                        else
                        {
                            mp.LugarEnvio = ((Best_envios)Session["Envio"]).calle + " " + ((Best_envios)Session["Envio"]).nro + ", " + ((Best_envios)Session["Envio"]).Localidad + " " + ((Best_envios)Session["Envio"]).Provincia + " (CP: " + ((Best_envios)Session["Envio"]).cp + ").  PISO: " + ((Best_envios)Session["Envio"]).piso + " DPTO: " + ((Best_envios)Session["Envio"]).dpto;

                        };
                    }


                    //si tiene para hacer un envio x oca- xml

                    if (((Best_envios)Session["Envio"]).TIPO_ENVIO!="LO RETIRA EN LOCAL" && ((Best_envios)Session["Envio"]).TIPO_ENVIO != "ENVIO LOCAL")
                    {
                        EnvioOrden();
                    }


               



                    Session["mp"] = mp;
                }
                else
                {
                    Session["mp"] = mp;
                    Response.Redirect("/Articulos/rechazoPago/",false);
                }

               
                return View();
            }
            catch (Exception)
            {

                throw;
            }

        }
        afip afip = new afip();
        FEAuthRequest feAutReq = null;

        public string validadCk(bool v_b)
        {
            if (v_b == true)
            {

                return "X";
            }
            else { }

            return "";
        }
        public string  GenComprobante(string mpId)
        {

            try
            { 


                bllFactura fact = new bllFactura(); 
                factura fe1 = new factura();

                if (Session["Envio"]!=null) {
                    fe1.Envio = (Best_envios)Session["Envio"];
                }
                fe1.CbteInterno = mpId;
                Be.Best_UsuariosWeb user = (Be.Best_UsuariosWeb)Session["UsuarioWeb"];
                if (feAutReq == null)
                {

                    feAutReq = afip.getRq();

                }
                //fe1.CbteInterno = TxtBsq.Text;
                fe1.Treitadias = validadCk(false);
                fe1.Contado = validadCk(true);
                fe1.CtaCte = validadCk(false);
                //   fe1.Doc = Convert.ToInt64(txtDoc.Text);
                fe1.Domicilio ="";

                fe1.FechaCbte = DateTime.Now.Date;// FechaCbate.Value;
                fe1.FechaDesde = DateTime.Now.Date;//fechaDesde.Value;
                fe1.FechaHasta = DateTime.Now.Date;//fechaHast.Value;


                //fe1.exento = validadCk(Exento.Checked);
                //fe1.Monotributo = validadCk(monotributo.Checked);
                //fe1.NoRespo = validadCk(NoResp.Checked);
                //fe1.respInscrip = validadCk(Rinscrip.Checked);
               fe1.ConsumidorFinal = validadCk(true);
                fe1.ClienteIva = "Consumidor Final"; 

                fe1.Nombre = user.apellido +" "+ user.nombre; 
                fe1.TipoCbteStr = "Factura B";


               // fe1.VtoPago = VtaPago.Value;


                fe1.Item = ListItem;



             


                fe1.PtoVta = "000" + ConfigurationManager.AppSettings.Get("PtoVta").ToString();

                fe1.Cuit = afip.Ticket().Cuit.ToString(); // cuit del vendedor

                if (fe1.TipoCbteStr == "Factura C")
                {
                    fe1.TipoCbte = 11;
                    fe1.Letra = "C";
                    fe1.TipoCbteStr = "Factura C";
                }
                if (fe1.TipoCbteStr == "Nota de crédito C")
                {
                    fe1.TipoCbte = 13;
                    fe1.Letra = "C";
                    fe1.TipoCbteStr = "Nota de crédito C";
                }



                if (fe1.TipoCbteStr == "Factura A")
                {
                    fe1.TipoCbte = 1;
                    fe1.Letra = "A";
                    fe1.TipoCbteStr = "Factura A";
                }

                if (fe1.TipoCbteStr == "Nota de Crédito A")
                {
                    fe1.TipoCbte = 3;
                    fe1.Letra = "A";
                    fe1.TipoCbteStr = "Nota de Crédito A";
                }

                if (fe1.TipoCbteStr == "Nota de Débito A")
                {
                    fe1.TipoCbte = 2;
                    fe1.Letra = "A";
                    fe1.TipoCbteStr = "Nota de Débito A";
                }

                if (fe1.TipoCbteStr == "Factura B")
                {
                    fe1.TipoCbte = 6;
                    fe1.Letra = "B";
                    fe1.TipoCbteStr = "Factura B";
                }

                if (fe1.TipoCbteStr == "Nota de Crédito B")
                {
                    fe1.TipoCbte = 8;
                    fe1.Letra = "B";
                    fe1.TipoCbteStr = "Nota de Crédito B";
                }

                if (fe1.TipoCbteStr == "Nota de Débito B")
                {
                    fe1.TipoCbte = 7;
                    fe1.Letra = "B";
                    fe1.TipoCbteStr = "Nota de Débito B";
                }



                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                var numberFormatInfo = new NumberFormatInfo();
                numberFormatInfo.NumberDecimalSeparator = ".";

                //////////////// calculo del iva //////////////////////////
                // recorro la lista de productos p/ determinar <ImpNeto> < ImpIVA > < ImpTrib > < ImpTotal >

                double vImpNeto = 0;
                double vImpIVA = 0;
                double vImpTrib = 0;



                AlicIva iva21 = new AlicIva();
                AlicIva iva105 = new AlicIva();
                AlicIva iva27 = new AlicIva();
                AlicIva iva0 = new AlicIva();
                List<AlicIva> ListIva = new List<AlicIva>();
                string tiene21 = "";
                string tiene105 = "";
                string tiene27 = "";
                string tiene0 = "";

                //ID   IVA
                //3   0 %  
                //4   10.5 % 
                //5   21 %  
                //6   27 %  
                //8   5 % 
                //9   2.5 %

              
                double ImporteTotalFac = 0;
                List<ItemFact> pLista = GetItems();
                foreach (var art in pLista)
                {
                    ImporteTotalFac += Math.Round(art.Importe , 2);

                    if (bllFactura.ivaId(art.Iva) == 3 && (fe1.TipoCbte != 11 || fe1.TipoCbte != 13)) //  0% * si no es mono
                    {
                        tiene0 = "S";
                        iva0.Id = 3;
                        double base0 = Math.Round(art.Importe, 2);

                        iva0.BaseImp += base0; // IMP NETO
                        vImpNeto += Math.Round(base0, 2);
                        double importe0 = Math.Round(art.Importe - base0, 2);
                        iva0.Importe += importe0;//el  importe de iva 
                        vImpIVA += 0;

                    }



                    if (bllFactura.ivaId(art.Iva) == 5) //  21%
                    {
                        tiene21 = "S";
                        iva21.Id = 5;
                        double base21 = Math.Round(art.Importe / (1 + 0.21), 2);

                        iva21.BaseImp += base21; // IMP NETO 
                        vImpNeto += Math.Round(base21, 2);
                        double importe21 = Math.Round(art.Importe - base21, 2);
                        iva21.Importe += importe21;//el  importe de iva 
                        vImpIVA += Math.Round(importe21, 2);
                    }
                    if (bllFactura.ivaId(art.Iva) == 4) // 10.5%
                    {
                        tiene105 = "S";
                        iva105.Id = 4;
                        double base105 = Math.Round(art.Importe / (1 + 0.105), 2);
                        iva105.BaseImp += base105; // IMP NETO
                        vImpNeto += Math.Round(base105, 2);
                        double importe105 = Math.Round(art.Importe - base105, 2);
                        iva105.Importe += Math.Round(importe105, 2);
                        vImpIVA += Math.Round(importe105, 2);


                    }

                    if (bllFactura.ivaId(art.Iva) == 6) // 27%
                    {
                        tiene27 = "S";
                        iva27.Id = 6;
                        double base27 = Math.Round(art.Importe / (1 + 0.27), 2);
                        iva27.BaseImp += Math.Round(base27, 2); // IMP NETO
                        vImpNeto += Math.Round(iva27.BaseImp, 2);
                        double importe27 = Math.Round(art.Importe - base27, 2);
                        iva27.Importe += Math.Round(importe27, 2);
                        vImpIVA += Math.Round(importe27, 2);


                    }

                }
                fe1.Total = ImporteTotalFac.ToString().Replace(",", ".");
                iva0.BaseImp = Math.Round(iva0.BaseImp, 2);
                iva0.Importe = Math.Round(iva0.Importe, 2);
                iva21.BaseImp = Math.Round(iva21.BaseImp, 2);
                iva21.Importe = Math.Round(iva21.Importe, 2);
                iva105.BaseImp = Math.Round(iva105.BaseImp, 2);
                iva105.Importe = Math.Round(iva105.Importe, 2);
                iva27.BaseImp = Math.Round(iva27.BaseImp, 2);
                iva27.Importe = Math.Round(iva27.Importe, 2);


                // fe1 es el obj mio para insertar en la base
                fe1.vIva0Neto = Math.Round(iva0.BaseImp, 2).ToString();
                fe1.vIva0importe = Math.Round(iva0.Importe, 2).ToString();
                fe1.vIva21Neto = Math.Round(iva21.BaseImp, 2).ToString();
                fe1.vIva21importe = Math.Round(iva21.Importe, 2).ToString();
                fe1.vIva105Neto = Math.Round(iva105.BaseImp, 2).ToString();
                fe1.vIva105importe = Math.Round(iva105.Importe, 2).ToString();
                fe1.vIva27Neto = Math.Round(iva27.BaseImp, 2).ToString();
                fe1.vIva27importe = Math.Round(iva27.Importe, 2).ToString();

                if (tiene21 == "S")
                {
                    ListIva.Add(iva21);
                }
                if (tiene105 == "S")
                {
                    ListIva.Add(iva105);
                }
                if (tiene27 == "S")
                {
                    ListIva.Add(iva27);
                }
                if (tiene0 == "S")
                {
                    ListIva.Add(iva0);
                }
                AlicIva[] ivaArray = new AlicIva[ListIva.Count];
                int num2 = 0;
                foreach (AlicIva iva in ListIva)
                {
                    ivaArray[num2] = iva;
                    num2++;
                }
                ////////////////////////////////// fin iva /////////////////////
                ///


                ServiceSoapClient fe = new ServiceSoapClient();



                FECAERequest FeReq = new FECAERequest();
                FECAECabRequest CAB = new FECAECabRequest();
                CAB.CantReg = 1;
                CAB.CbteTipo = fe1.TipoCbte;
                CAB.PtoVta = Convert.ToInt32(fe1.PtoVta); // ptos de vtas habilitados --> ptosVtas()


                FeReq.FeCabReq = CAB;
                // detalle


                FECAEDetRequest DET = new FECAEDetRequest();

              
                    DET.Concepto = 1; //"PRODUCTOS"


                TipoDoc Tdoc = new TipoDoc();

                Tdoc.Descripcion = "DNI";
                Tdoc.Id = 96;

                //Tdoc.Descripcion = "CUIT";
                //Tdoc.Id = 80;

                // si es 1 producto , 2 servicio etc --> getFEParamGetTiposConcepto
                fe1.Concepto = DET.Concepto.ToString();

                fe1.tipoDoc = (Be.TipoDoc)Tdoc;


                DET.DocTipo = fe1.tipoDoc.Id; // 80 CUIT // 96 dni del comprador




                fe1.DocTipo_Afip = DET.DocTipo.ToString();

                //20111111112
                if (!string.IsNullOrEmpty(user.dni))
                {

                    DET.DocNro = Int64.Parse(user.dni); // del comprador

                }
              

                fe1.Doc = DET.DocNro;

                DET.CbteDesde = afip.UltimoCbte(CAB.PtoVta, CAB.CbteTipo, feAutReq) + 1;
                DET.CbteHasta = afip.UltimoCbte(CAB.PtoVta, CAB.CbteTipo, feAutReq) + 1;


                fe1.NumCbte = bllFactura.LlenarComprobante(DET.CbteDesde.ToString());
                fe1.Envio.remito = fe1.NumCbte;
                Session["Envio"] = fe1.Envio;
                DateTime dt2 = DateTime.Now;
                DateTime dt = DateTime.Now.Date;
                DET.CbteFch = dt.Date.ToString("yyyyMMdd");

                if (DET.Concepto == 2)
                {


                    if (dt < dt2.AddDays(10) && dt > dt2.AddDays(-10))
                    {
                        DET.CbteFch = dt.Date.ToString("yyyyMMdd");

                    }
                    else
                    {

                        throw new Exception("para servicios la Fecha del comprobante puede ser hasta 10 días anteriores o posteriores a la fecha de generación");
                      

                    }
                }



                //                Fecha del comprobante (yyyymmdd). Para
                //concepto igual a 1, la fecha de emisión del
                //comprobante puede ser hasta 5 días
                //anteriores o posteriores respecto de la
                //fecha de generación; si se indica
                //Concepto igual a 2 ó 3 puede ser hasta 10
                //días anteriores o posteriores a la fecha de
                //generación. (Si no se envía la fecha del
                //comprobante se asignará la fecha de proceso) 


                if (fe1.TipoCbte != 11 && fe1.TipoCbte != 13 && fe1.TipoCbte != 12 && fe1.TipoCbte != 15)
                {
                    // si no es monotributo
                    DET.Iva = ivaArray;
                    DET.ImpIVA = Math.Round(vImpIVA, 2);
                }

                DET.ImpTotal = Math.Round(Double.Parse(fe1.Total.Replace(",", "."), numberFormatInfo), 2);
                DET.ImpTotConc = 0; // COMPROBANTE C ES 0
                DET.ImpNeto = Math.Round(vImpNeto, 2);
                DET.ImpOpEx = 0;  // COMPROBANTE C ES 0


                DET.MonId = "PES";
                DET.MonCotiz = 1;

                if (DET.MonId == "PES")
                {
                    fe1.MonedaStr = "$";

                }

            



                FECAEDetRequest[] objetosDet = new FECAEDetRequest[] { DET };


                FeReq.FeDetReq = objetosDet;
             

                FECAEResponse feResponse = fe.FECAESolicitar(feAutReq, FeReq);

                ///////////////**** errores /////////////
                string errores = "";

                if (feResponse.Errors != null)
                {
                    for (int i = 0; i < feResponse.Errors.Length; i++)
                    {
                        errores += (feResponse.Errors[i].Msg + "; cod:" + feResponse.Errors[i].Code);

                    }


                }


                for (int i = 0; i < feResponse.FeDetResp.Length; i++)
                {


                    if (feResponse.FeDetResp[i].Observaciones != null)
                    {
                        for (int ii = 0; ii < feResponse.FeDetResp[i].Observaciones.Length; ii++)
                        {
                            errores += (feResponse.FeDetResp[i].Observaciones[ii].Msg + "; cod:" + feResponse.FeDetResp[i].Observaciones[ii].Code);

                        }
                    }

                }

               
                ///////////////////////////////////////////////////////////////////////





                fe1.CAE = feResponse.FeDetResp[0].CAE.ToString();
                string v_vto = feResponse.FeDetResp[0].CAEFchVto.ToString();

                string v_d = v_vto.Substring(6, 2);
                string v_m = v_vto.Substring(4, 2);
                string v_y = v_vto.Substring(0, 4);


                fe1.VtoCae = v_d + "/" + v_m + "/" + v_y;

                // — C.U.I.T. (Clave Unica de Identificación Tributaria) del emisor (11 caracteres).


                //— Código de tipo de comprobante (2 caracteres).

                //— Punto de venta (4 caracteres).

                //— Código de Autorización de Impresión (14 caracteres).

                //— Fecha de vencimiento (8 caracteres).

                //— Dígito verificador (1 carácter).




                string CodBarra = fe1.Cuit + fe1.TipoCbte.ToString() + fe1.PtoVta + fe1.CAE + v_vto;
                string digi = bllFactura.Mod10(CodBarra).ToString();
                fe1.CodBarra = CodBarra + digi;


              //  MessageBox.Show("Resultado: " + feResponse.FeCabResp.Resultado + " CAE: " + feResponse.FeDetResp[0].CAE + " Fecha Vto: " + feResponse.FeDetResp[0].CAEFchVto + "-" + digi);
                fe1.Estado_CAE = feResponse.FeCabResp.Resultado;

                if (feResponse.FeCabResp.Resultado == "A")
                {
                    fe1.Estado_CAE = "AUTORIZADO";
                }
                if (feResponse.FeCabResp.Resultado == "R")
                {
                    fe1.Estado_CAE = "RECHAZADO";
                }

 

                fe1.vImpNeto = vImpNeto.ToString();
                fe1.vImpIVA = vImpIVA.ToString();
                fe1.vImpIVA = vImpTrib.ToString();

                fe1.CondicionVenta = condicionVenta();
                fe1.errores = errores;
                ///////////////////// INSERT EN LA FACTURA EN LA BASE ////////////////////////////////////////////
               
                string idfc = fact.insert(fe1);
                Session["carrito"] = null;


                //reportFactura rep = new reportFactura(fact.Rp(idfc));
                //rep.Show();



                return idfc;


            }
            catch (Exception)
            {

                throw;
            } 

        }
 
  
        public string condicionVenta()
        {
            string salida = "Tarjeta";

            //if (Contado.Checked)
            //{
            //    salida += "Contado  ";
            //}

            //if (ctacte.Checked)
            //{
            //    salida += "CtaCte.  ";
            //}
            //if (traita.Checked)
            //{
            //    salida += "30 Días  ";
            //}

            return salida;

        }

        public ActionResult WsYaInicio()
        {

            bool sal = false;
            try
            { 
                if (Session["UsuarioWeb"] != null)
                {
                     
                    sal = true;
                }
        


            }
            catch (Exception ex)
            {
                Session["UsuarioWeb"] = null;
                return ThrowJsonError(
                           new Exception(String.Format(ex.Message, " ")));
            }


            return base.Json(sal);


        }

        public ActionResult WsIniciar(string nombre,string pass)
        {
           
            bool sal = false;
            try
            {

                Be.Best_UsuariosWeb entidad= BllBest.DameInstancia().iniciarSession(nombre.Replace("=", "").Replace("'", "").Replace("-", "").Trim().ToUpper());
                var v_clave = Encriptor.DameInstancia().GetMD5(pass.Trim());
                if (v_clave == entidad.pass)
                {
                    Session["UsuarioWeb"] = entidad;
                    sal = true;
                }
                else { Session["UsuarioWeb"] = null; }
                 
               
            }
            catch (Exception ex)
            {
                Session["UsuarioWeb"] = null;
                return ThrowJsonError(
                           new Exception(String.Format(ex.Message, " ")));
            }


            return base.Json(sal);


        }



        public ActionResult VerArticulo()
        {
            try
            {
                 
                if (HttpContext.Session["Menu"] == null)
                {
                    Session["Menu"] = Bll.BllBest.DameInstancia().best_Menu();
                }
                ViewData["menu"] = (List<Be.Best_Menu>)HttpContext.Session["Menu"];

                if (Request.QueryString != null)
                {
                    // desencriptar parametros
                    QueryString obQueryString = new QueryString(Request.QueryString);
                    obQueryString = Bll.Encriptor.DameInstancia().DecryptQueryString(obQueryString);
                    string vart = obQueryString["vart"].ToString();


                    Best_articulos_ventaId trerArt = Bll.BllBest.DameInstancia().listaArticuloVentaId(Convert.ToInt32(vart.Trim()));
                    ViewData["articulo"] = trerArt;
                    Session["vart"] = trerArt;
                }
                else
                {
                    return Redirect("/");
                }


            }
            catch (Exception)
            {

                return Redirect("/");
            }
            return View();
        }


        public ActionResult misCompras()
        {
           
  if (HttpContext.Session["UsuarioWeb"] == null)
            {
                Response.Redirect("/");
            }

            if (HttpContext.Session["Menu"] == null)
            {
                Session["Menu"] = Bll.BllBest.DameInstancia().best_Menu();
            }
            ViewData["menu"] = (List<Be.Best_Menu>)HttpContext.Session["Menu"];
            if (Session["UsuarioWeb"]==null)
            {
                Response.Redirect("/",false);
            }
            Be.Best_UsuariosWeb user = (Be.Best_UsuariosWeb)Session["UsuarioWeb"];
            List<mercadopagos> ListaMp = BllBest.DameInstancia().Listar_mercadopagos("0",user.id.ToString() );
            ViewData["listaMp"] = ListaMp;
            return View();


             
        }


        public ActionResult Tipo()
        {
           
            return View();
        }


        //public ActionResult Tipo(int id, int? cat)
        //{
        //    if (cat.HasValue)
        //    {
        //        // id present
        //    }
        //    else
        //    {
        //        // id not present
        //    }
        //    return View();
        //}

        // GET: Articulos/Details/5
        public ActionResult Details(int id, int? cat)
        {
            if (cat.HasValue)
            {
                // id present
            }
            else
            {
                // id not present
            }
            return View();
        }
         
 
        public ActionResult KeepActiveSession()
        {
            //if (HttpContext.Session["ActiveSession"] != null)
            //{
            //    DateTime inicio = DateTime.Parse(HttpContext.Session["ActiveSession"].ToString());
            //    if (inicio < DateTime.Now)

            //        return base.Json(true);
            //    else return base.Json(false);
            //}
            //else
            //    return base.Json(false);

            return base.Json(true);
        }
        public ActionResult WsTieneUser(string dni)

        {
            bool sal =false;
            try
            {
               sal = BllBest.DameInstancia().TieneUsuario(dni);
            }
            catch (Exception ex)
            {
                return ThrowJsonError(
                                        new Exception(String.Format(ex.Message, " ")));
            }
          
            return base.Json(sal); 

        }

        public ActionResult WsOlvidarUser(string mail)

        {
            bool sal = false;
            try
            {
                if (BllBest.DameInstancia().existe_mail_hab(mail.ToUpper().Trim()) == "N")
                {
                    sal = false; 
                }
                else
                {
                    BllBest.DameInstancia().envio_pwd(mail.ToUpper().Trim());
                    sal = true; 

                }
            }

            catch (Exception ex)
            {
                return ThrowJsonError(
                            new Exception(String.Format(ex.Message, " ")));
            }

            return base.Json(sal);

        }


        public ActionResult WsTieneUserMail(string mail)

        {
            bool sal = BllBest.DameInstancia().TieneUsuarioMail(mail);
            return base.Json(sal);

        }

        public ActionResult WscarritoSt(string v_calle,string v_nro,string v_cp,string v_piso,string v_dpto,string v_provincia,string v_localidad)
        {
            Best_envios env = new Best_envios();
            env.calle = v_calle;
            env.nro = v_nro;
            env.cp = v_cp;
            env.piso = v_piso;
            env.dpto = v_dpto;
            env.Provincia = v_provincia;
            env.Localidad = v_localidad;
            
          
            if (Session["envioValores"] !=null) {
                List<string> listVal = (List<string>)Session["envioValores"];
                env.volumen = listVal[0];
                env.peso = listVal[1];
                env.Monto = listVal[2];
                env.alto = listVal[3];
                env.ancho = listVal[4];
                env.largo = listVal[5];
                env.CostoEnvio = listVal[6];
                env.TIPO_ENVIO = listVal[7];
            }

            if (Session["SelectSuc"] != null && env.TIPO_ENVIO== "PUERTA A SUCURSAL")
            {
                env.SEC_SELECT_ID = ((Combos)Session["SelectSuc"]).valor??"";
                env.SEC_SELECT_NOMBRE = ((Combos)Session["SelectSuc"]).descripcion ?? "";
            }

            Session["Envio"] = env;


            // veridica si el articulo tiene aun estock y guarda el domicilio.
            List<Be.Best_articulos_carrito> carrito = new List<Be.Best_articulos_carrito>();
            bool sal = false;
            carrito = (List<Be.Best_articulos_carrito>)Session["carrito"];

            try
            {
                BllBest.DameInstancia().CarritoSt(carrito);
                sal =true;
            }
            catch (Exception ex)
            {
                Session["carrito"]=null;
                sal = false;
                return ThrowJsonError(
                              new Exception(String.Format(ex.Message, " ")));
            }
          

            return base.Json(sal);


        }


        public ActionResult WscarritoDel(string ids)
        {
            List<Be.Best_articulos_carrito> carrito = new List<Be.Best_articulos_carrito>();
            List<Be.Best_articulos_carrito> carrito2 = new List<Be.Best_articulos_carrito>();
            carrito = (List<Be.Best_articulos_carrito>)Session["carrito"];
            foreach (var item in carrito)
            {
                if (item.Id != ids)
                {

                    carrito2.Add(item);
                }
            }
            Session["carrito"] = carrito2;

            return base.Json(carrito2);


        }


        public ActionResult WsCerrar()
        {
            
            try
            {
                Session["UsuarioWeb"] = null;
            }
            catch (Exception ex)
            {

                return ThrowJsonError(
                             new Exception(String.Format(ex.Message, " ")));
            }


            return base.Json(true);


        }

        public List<Be.ItemFact> GetItems()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                var numberFormatInfo = new NumberFormatInfo();
                numberFormatInfo.NumberDecimalSeparator = ".";
                List<Be.Best_articulos_carrito> carrito = new List<Be.Best_articulos_carrito>();
                if (Session["carrito"] != null)
                {
                    carrito = (List<Be.Best_articulos_carrito>)Session["carrito"];
                }

                if (Session["carrito"] == null)
                {
                    throw new Exception("Carrito vacio");
                }


                  

                double vtotalprecio = 0;
                Best_envios Envio = null;

                if (Session["Envio"] != null)
                {// AGREGO EL ITEM DEL COSTO DEL ENVIO
                    if (((Best_envios)Session["Envio"]).TIPO_ENVIO != "LO RETIRA EN LOCAL" )
                    {
                        Envio = (Best_envios)Session["Envio"];
                    Be.Best_articulos_carrito artenvio = new Best_articulos_carrito();
                    artenvio.precio = Envio.CostoEnvio;
                    artenvio.cant = "1";
                        artenvio.Id = "99";
                        artenvio.Descripcion = "TIPO ENVIO: "+Envio.TIPO_ENVIO;

                        carrito.Add(artenvio);


                    }
                }
                foreach (var art in carrito)
                {
                   
                   
                    double vprecio = Math.Round(Double.Parse(art.precio.Replace(",", "."), numberFormatInfo), 2) * Double.Parse(art.cant.Replace(",", "."), numberFormatInfo);
                    vtotalprecio = vtotalprecio + vprecio;

                    Be.ItemFact it = new ItemFact();
                    it.Id = ListItem.Count + 1;
                    it.Detalle = art.Descripcion ;
                    it.ImpxUni = Double.Parse(art.precio.Replace(",", "."), numberFormatInfo).ToString()  ;
                    it.Importe = Math.Round(Double.Parse(art.precio.Replace(",", "."), numberFormatInfo) * Double.Parse( art.cant.ToString().Replace(",", "."), numberFormatInfo), 2); ;
                    it.Iva = "21%";
                    it.Cant = art.cant.ToString();
                    it.Id_Articulo = art.Id;
                    it.NetoImpxUni = Math.Round(Double.Parse(art.precio.Replace(",", "."), numberFormatInfo) / (1 + ivaValor(it.Iva)), 2).ToString();
                    it.NetoImpTotal = Math.Round(Double.Parse(it.NetoImpxUni.Replace(",", "."), numberFormatInfo) * Double.Parse(art.cant.ToString().Replace(",", "."), numberFormatInfo), 2).ToString(); 

                    ListItem.Add(it);
                    
                }

               
              

                return ListItem;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public double ivaValor(string valor)
        {

            if (valor == "0%")
            {
                return 0;

            }
            if (valor == "21%")
            {
                return 0.21;

            }

            if (valor == "10.5%")
            {
                return 0.105;

            }

            if (valor == "27%")
            {
                return 0.27;

            }
            return 0;
        }


        [HttpPost]

        public ActionResult WsRegistrarcion(string nombre, string apellido, string fecha_nac, Int32 dni, string mail, string  password, string tel, string url)
        {
            bool sal = false;
            try
            {

                kx_cliente cliafip= afip.BuscarPersonas("DNI", dni);


                var   v_clave = Encriptor.DameInstancia().GetMD5(password.Trim());
                string urlActual = ConfigurationManager.AppSettings.Get("PathUrl").ToString();
                string domicilio = cliafip.Calle  + " Nro:" + cliafip.Numero + ". " + cliafip.Distrito + ", " + cliafip.Provincia  + " CP:" + cliafip.Cp;
               BllBest.DameInstancia().Registrarcion(nombre.Replace("=", "").Replace("'", "").Replace("-", "").ToUpper().Trim(), apellido.Replace("=", "").Replace("'", "").Replace("-", "").ToUpper().Trim(), fecha_nac, dni.ToString(), mail.ToUpper().Trim(), v_clave, tel.Replace("=", "").Replace("'", "").Replace("-", "").ToUpper().Trim(), url.Replace("'", "").Replace("-", "").Replace("&", "_"), urlActual, domicilio);
                sal = true;
            }
            catch (Exception ex)
            {
                return ThrowJsonError(
                           new  Exception(String.Format(ex.Message, " ")));
            }


            return base.Json(sal, JsonRequestBehavior.AllowGet);


        }

        public JsonResult ThrowJsonError(Exception e)
        {
           
            Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            Response.StatusDescription = e.Message;

            return Json(new { Message = e.Message }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WscarritoCant(string ids)
        {
           string cantidad = "0";
            List<Be.Best_articulos_carrito> carrito = new List<Be.Best_articulos_carrito>();
           

            if (Session["carrito"] != null) {
                carrito = (List<Be.Best_articulos_carrito>)Session["carrito"];
            }
            foreach (var item in carrito)
            {
                if (item.Id.Trim() == ids.Trim())
                {

                    cantidad = item.cant;
                }
            } 

            return base.Json(cantidad);


        }
        public ActionResult Wscarrito()
        {
            List<Be.Best_articulos_carrito> carrito = new List<Be.Best_articulos_carrito>();
          
            if (Session["carrito"] != null)
            {
                carrito =(List<Be.Best_articulos_carrito>) Session["carrito"];
                Session["carrito"] = carrito;
            } 
            return base.Json(carrito);


        }
        [HttpPost]
        public ActionResult WsArtCarrito(string mp)
        {
            List<Be.Best_articulos_carrito> carrito = new List<Be.Best_articulos_carrito>();

            carrito = BllBest.DameInstancia().ArtCarrito("0", mp);
            return base.Json(carrito);


        }


        public ActionResult Wsaddcarrito(string vCant )
        {

            try
            {
                if (Session["vart"] == null)
                {
                    throw new Exception("obj Session[vart]  null");
                }

                Be.Best_articulos_carrito nuevo = new Be.Best_articulos_carrito();

                

                Best_articulos_ventaId art = (Best_articulos_ventaId)Session["vart"];

                if (art.VolumenTotal == "" || art.VolumenTotal == "0")
                {
                    throw new Exception(" NO SE PUEDE AGREGAR POR QUE NO POSEE UN VOLUMEN DEFINIDO ");
                }

                List<Be.Best_articulos_carrito> carrito = new List<Be.Best_articulos_carrito>();
                List<Be.Best_articulos_carrito> carritoFinal = new List<Be.Best_articulos_carrito>();

                if (Session["carrito"] != null)
            {
                carrito = (List<Be.Best_articulos_carrito>)Session["carrito"];
            }

                foreach (var item in carrito)
                {
                    if (item.Id == art.Id.ToString())
                    {
                     ///   vCant = ( Convert.ToUInt32( item.cant ) + Convert.ToUInt32(vCant)).ToString();  // agrega la suma del lo q tenia y lo actual
                    }
                    else
                    {
                        carritoFinal.Add(item);
                    }
                }

            


                nuevo.cant = vCant;
            nuevo.categoria = art.categoria;
                string mtalle = "";
                if (!string.IsNullOrEmpty(art.talle()))
                {
                    mtalle = "Talle: "+ art.talle();
                }
            nuevo.Descripcion = art.Descripcion + " " + mtalle;
            nuevo.diciplina = art.diciplina;
            nuevo.genero= art.genero;
            nuevo.Id = art.Id;
            nuevo.marca = "";
            nuevo.precio= art.precio;
                nuevo.imagen = art.Imagen1;
                nuevo.VolumenTotal = art.VolumenTotal;
                nuevo.Peso = art.Peso;
                nuevo.alto_embalaje = art.alto_embalaje;
                nuevo.ancho_embalaje = art.ancho_embalaje;
                nuevo.largo_embalaje = art.largo_embalaje;
             
        carritoFinal.Add(nuevo);

            Session["carrito"] = carritoFinal;
            return base.Json(carritoFinal);

            }
            catch (Exception ex)
            {
                return ThrowJsonError(
                           new Exception(String.Format(ex.Message, " ")));
                 
            }
        }


     


        // GET: Articulos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Articulos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult MiCuenta()
        {
            return View();
        }


        // GET: Articulos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articulos/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Articulos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Articulos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }

 

}
