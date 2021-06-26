using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.Text;
using System.Threading.Tasks;

using VbWsaa;
//using factElectForm.wsfeProd;     //Produccion
using Sport.wsfe;  // desa Homo
using Be; 
using System.Xml; 
using Microsoft.SqlServer.Server;
using System.Globalization;
using System.Threading;
using Bll;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Configuration;
using Sport.PersonasA13Prod;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography;
using System.Security;

namespace Sport.Models
{
    public class afip
    {

        FEAuthRequest feAutReq = null;

        public Int32 UltimoCbte(Int32 v_ptoVta, Int32 v_tipoCbte, FEAuthRequest v_feAutReq)
        {

            try
            {
                FERecuperaLastCbteResponse recupero = this.getFECompUltimoAutorizado(v_ptoVta, v_tipoCbte, v_feAutReq);


                return recupero.CbteNro;


            }
            catch (Exception)
            {

                throw;
            }

        }
        public FERecuperaLastCbteResponse getFECompUltimoAutorizado(Int32 v_ptoVta, Int32 v_tipoCbte, FEAuthRequest v_feAutReq)
        {

            try
            {

                ServiceSoapClient fe = new ServiceSoapClient();
                return fe.FECompUltimoAutorizado(v_feAutReq, v_ptoVta, v_tipoCbte);
            }
            catch (Exception)
            {

                throw;
            }


        }



        public FEAuthRequest getRq()
        {
            try
            {
                // objeto autotificacion cn wsfe

                Be.TicketAuth v_tk = Ticket();  // pido tk al wsaa

                FEAuthRequest rq = new FEAuthRequest();
                rq.Cuit = v_tk.Cuit;
                rq.Sign = v_tk.Sign;
                rq.Token = v_tk.Token;

                return rq;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public kx_cliente BuscarPersonas(string tipo, long doc)

        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            kx_cliente cli = new kx_cliente();
            try
            {
                FEAuthRequest rq = getRqPersonas();
                PersonaServiceA13Client pp = new PersonaServiceA13Client();

                long mdoc = 0;
                if (tipo == "DNI")
                {
                    idPersonaListReturn sal1 = pp.getIdPersonaListByDocumento(rq.Token, rq.Sign, rq.Cuit, doc.ToString()); // x dni obtengo cuit 
                    mdoc = sal1.idPersona[0] ?? 0;
                }
                if (tipo == "CUIT")
                {
                    mdoc = doc;
                }
                personaReturn sPersona = pp.getPersona(rq.Token, rq.Sign, rq.Cuit, mdoc);

                cli.Apellido = sPersona.persona.apellido ?? "";
                cli.Calle = sPersona.persona.domicilio[0].calle ?? "";
                cli.Cp = sPersona.persona.domicilio[0].codigoPostal ?? "";
                cli.Doc_No = mdoc.ToString();
                cli.Pais = "1";
                
                cli.Provincia = sPersona.persona.domicilio[0].descripcionProvincia.ToString() ?? "";
                cli.Distrito = sPersona.persona.domicilio[0].localidad.ToString() ?? ""; 
                cli.Nombre = sPersona.persona.nombre ?? "";
                if (!string.IsNullOrEmpty(sPersona.persona.razonSocial))
                {
                    cli.Nombre += " -" + sPersona.persona.razonSocial;
                }
                cli.Numero = sPersona.persona.domicilio[0].numero.ToString() ?? "";
                cli.Tipo_Doc = "CUIT";
                cli.Fecha_Nac = sPersona.persona.fechaNacimiento.ToString("dd/MM/yyyy") ?? "";
                cli.Observacion = "Persona " + sPersona.persona.tipoPersona ?? "";

                if ("01/01/0001" != sPersona.persona.fechaNacimiento.ToString("dd/MM/yyyy"))
                {
                    cli.Observacion += " - Fecha de Nac.: " + sPersona.persona.fechaNacimiento.ToString("dd/MM/yyyy");
                }
                if (!string.IsNullOrEmpty(sPersona.persona.descripcionActividadPrincipal))
                {
                    cli.Observacion += " - Act:" + sPersona.persona.descripcionActividadPrincipal ?? "";

                }
                if (!string.IsNullOrEmpty(sPersona.persona.descripcionActividadPrincipal))
                {
                    cli.Observacion += " - Dir:" + sPersona.persona.domicilio[0].direccion.ToString() ?? "" + " ," + sPersona.persona.domicilio[0].localidad.ToString() ?? "";

                }
                if ("01/01/0001" != sPersona.persona.fechaFallecimiento.ToString("dd/MM/yyyy"))
                {
                    cli.Observacion += " - Fecha de Fallecimiento: " + sPersona.persona.fechaFallecimiento.ToString("dd/MM/yyyy");
                }
                cli.Observacion += " -  CUIT/CUIL: " + cli.Doc_No ?? "";



            }
            catch (Exception)
            {

                throw;
            }


            return cli;

        }
        public FEAuthRequest getRqPersonas()
        {
            FEAuthRequest request2;


            try
            {
                // si el user no es produccion

                TicketAuth auth;
                auth = this.TicketPersonas();


                FEAuthRequest request = new FEAuthRequest();
                request.Cuit = auth.Cuit;
                request.Sign = auth.Sign;
                request.Token = auth.Token;
                request2 = request;
                return request2;


            }
            catch (Exception)
            {
                throw;
            }



        }


        public TicketAuth TicketPersonas()
        {
            // tengo q cambiar el nombre del servicio para q  me de un ticket para ese servicio:

            string Path = ConfigurationManager.AppSettings.Get("PathProyecto").ToString();
            TicketAuth auth = new TicketAuth();
            try
            {
                XmlDocument document = new XmlDocument();
                string filename =  Path + @"xml\TicketResponse20310149919PV3PersonasA13.xml";
                document.Load(filename);
                //// copio toda la configuracioon mi certificado en produccion y cambio el servicio para q sea el de personas. y lo guardado en otro .xml config
               string str2 =  Path + @"xml\Config20310149919PV3Produccion.xml";
                XmlDocument document2 = new XmlDocument();
               document2.Load(str2);

                //document2.SelectSingleNode("/configuracion/certificado/DEFAULT_SERVICIO").InnerText = "ws_sr_padron_a13";
                 string config =  Path + @"xml\Config20310149919PV3ProduccionPersonasA13.xml";

                //document2.Save(config);

                string innerText = document.SelectSingleNode("/loginTicketResponse/header/expirationTime").InnerText;
                string s = innerText.Substring(0, 10) + " " + innerText.Substring(11, 8);
                DateTime time = new DateTime();
                time = DateTime.ParseExact(s, "yyyy-MM-dd HH:mm:ss", null);
                DateTime now = DateTime.Now;
               

                if (now > time)
                { 
                 // this.IniciarProducion(Path, config, filename); // este metodo carga los datoms inicio en el xml response 

                    obtenerTiketWSPersonal(filename);
                    document = new XmlDocument();
                    string str5 = filename;
                    document.Load(str5);
                }
                auth.Sign = document.SelectSingleNode("//sign").InnerText;
                auth.Token = document.SelectSingleNode("//token").InnerText;
                auth.Cuit = Convert.ToInt64(document2.SelectSingleNode("//CUIT").InnerText);
                




            }
            catch (Exception ex)
            {
                string Message = Environment.NewLine + " En afip.cs TicketPersonas() - Date : " + DateTime.Now.ToString() + " + Error Message : " + ex.ToString();
               
                Message += Environment.NewLine + Environment.NewLine + "------------------------------------------------------------------------- FIN";

                File.AppendAllText(HttpContext.Current.Server.MapPath("~/Log/Log.txt"), Message);

                throw;


            }
            return auth;
        }
        public void obtenerTiketWSPersonal(string argSal)
        {
            try
            {
                ObtenerTicket.ObtenerTicketSoapClient obj = new ObtenerTicket.ObtenerTicketSoapClient();

                string loginTicketResponse = obj.ObtenerTokenPersonaBest("066D1FF2C5329F4CB1426CDE2D64AF2D");

                XmlLoginTicketResponse = new XmlDocument();
                XmlLoginTicketResponse.LoadXml(loginTicketResponse);

               
                XmlLoginTicketResponse.Save(argSal);

             
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void obtenerTiketWSFact(string argSal)
        {
            try
            {
                ObtenerTicket.ObtenerTicketSoapClient obj = new ObtenerTicket.ObtenerTicketSoapClient();

                string loginTicketResponse = obj.ObtenerTokenFacturaBest("066D1FF2C5329F4CB1426CDE2D64AF2D");

                XmlLoginTicketResponse = new XmlDocument();
                XmlLoginTicketResponse.LoadXml(loginTicketResponse);


                XmlLoginTicketResponse.Save(argSal);


            }
            catch (Exception)
            {

                throw;
            }

        }

        public Be.TicketAuth Ticket()
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                // ticket en wsaa
                string Path = ConfigurationManager.AppSettings.Get("PathProyecto").ToString();
                XmlDocument tkResponse = new XmlDocument();
                tkResponse.Load(Path + @"\xml\\loginTicketResponse.xml");




                XmlDocument XmlConfig = new XmlDocument();
                XmlConfig.Load( Path + @"\xml\\config.xml");


                string timeEnd = tkResponse.SelectSingleNode("/loginTicketResponse/header/expirationTime").InnerText;
                string d_timeEnd = timeEnd.Substring(0, 10) + " " + timeEnd.Substring(11, 8);

                DateTime MiDateTime_fin;
                MiDateTime_fin = new DateTime();
                MiDateTime_fin = DateTime.ParseExact(d_timeEnd, "yyyy-MM-dd HH:mm:ss", null);

                DateTime d_now = DateTime.Now;

                Be.TicketAuth tk = new TicketAuth();

              
  
                if (d_now > MiDateTime_fin)
                {
                    //vencido pido un nuevo ticket 

                   // VbWsaa.ProgramaPrincipal p = new VbWsaa.ProgramaPrincipal();
                    //    p.Iniciar(Path);

                    obtenerTiketWSFact(Path + @"\xml\\loginTicketResponse.xml");

                    tkResponse = new XmlDocument();
                    tkResponse.Load(Path + @"\xml\\loginTicketResponse.xml");

                }
                tk.Sign = tkResponse.SelectSingleNode("//sign").InnerText;
                tk.Token = tkResponse.SelectSingleNode("//token").InnerText;
                tk.Cuit = Convert.ToInt64(XmlConfig.SelectSingleNode("//CUIT").InnerText);




                return tk;


            }
            catch (Exception)
            {

                throw;
            }

        }




        public UInt32 UniqueId; // Entero de 32 bits sin signo que identifica el requerimiento
        public DateTime GenerationTime; // Momento en que fue generado el requerimiento
        public DateTime ExpirationTime; // Momento en el que exoira la solicitud
        public string Service; // Identificacion del WSN para el cual se solicita el TA
        public string Sign; // Firma de seguridad recibida en la respuesta
        public string Token; // Token de seguridad recibido en la respuesta

        public XmlDocument XmlLoginTicketRequest = null/* TODO Change to default(_) if this is not a reference type */;
        public XmlDocument XmlLoginTicketResponse = null/* TODO Change to default(_) if this is not a reference type */;
        public string RutaDelCertificadoFirmante;
        public string XmlStrLoginTicketRequestTemplate = "<loginTicketRequest><header><uniqueId></uniqueId><generationTime></generationTime><expirationTime></expirationTime></header><service></service></loginTicketRequest>";

        private bool _verboseMode = true;

        private static UInt32 _globalUniqueID = 0; // OJO! NO ES THREAD-SAFE

        const string DEFAULT_URLWSAAWSDL = "https://wsaa.afip.gov.ar/ws/services/LoginCms?wsdl"; // no lo usa por usa el ws
        const string DEFAULT_SERVICIO = "wsfe";
        const string DEFAULT_CERTSIGNER = @"c:\\cert.pfx";
        const string DEFAULT_PROXY = null;
        const string DEFAULT_PROXY_USER = null;
        const string DEFAULT_PROXY_PASSWORD = "";
        const bool DEFAULT_VERBOSE = true;
        const string DEFAULT_CERTIFICADO_PASSWORD = "";
        public string SAL_TK = "";
        public bool IniciarProducion(string path, string configuracion, string response )
        {
            try
            {
                bool sal = false;

                XmlDocument Xmlconfig = new XmlDocument();


                // en config los datos de proccion
                Xmlconfig.Load(configuracion);




                string strUrlWsaaWsdl = Xmlconfig.SelectSingleNode("//DEFAULT_URLWSAAWSDL").InnerText;

                string strIdServicioNegocio = Xmlconfig.SelectSingleNode("//DEFAULT_SERVICIO").InnerText;
                // si es homo el certHomo y si es produc el certificado es cert.

                string strRutaCertSigner = path + Xmlconfig.SelectSingleNode("//DEFAULT_CERTSIGNER").InnerText;
                string pass = Xmlconfig.SelectSingleNode("//DEFAULT_CERTIFICADO_PASSWORD").InnerText;
                 

                SecureString strPasswordSecureString = new SecureString();
                bool blnVerboseMode = DEFAULT_VERBOSE;

                string strProxy = DEFAULT_PROXY;
                string strProxyUser = DEFAULT_PROXY_USER;
                string strProxyPassword = DEFAULT_PROXY_PASSWORD;



                if (!string.IsNullOrEmpty(pass))
                {
                    foreach (char character in pass.ToCharArray())
                        strPasswordSecureString.AppendChar(character);
                    strPasswordSecureString.MakeReadOnly();
                }

                // Argumentos OK, entonces procesar normalmente...

             
                string strTicketRespuesta;

                 


                // donde crea el tk de response
                SAL_TK = response;

                strTicketRespuesta = ObtenerLoginTicketResponseProduccion(strIdServicioNegocio, strUrlWsaaWsdl, strRutaCertSigner, strPasswordSecureString, strProxy, strProxyUser, strProxyPassword, blnVerboseMode, path, SAL_TK);
                 
                sal = true;
                return sal;
            }
            catch (Exception excepcionAlObtenerTicket)
            {
                throw excepcionAlObtenerTicket;
            }
        }

        public string ObtenerLoginTicketResponseProduccion(string argServicio, string argUrlWsaa, string argRutaCertX509Firmante, SecureString argPassword, string argProxy, string argProxyUser, string argProxyPassword, bool argVerbose, string argPath, string argSal 
)
        {
            this.RutaDelCertificadoFirmante = argRutaCertX509Firmante;
            this._verboseMode = argVerbose;
            CertificadosX509Lib.VerboseMode = argVerbose;
            string sal = "nada";

            string cmsFirmadoBase64 ="";
            string loginTicketResponse;
            XmlNode xmlNodoUniqueId;
            XmlNode xmlNodoGenerationTime;
            XmlNode xmlNodoExpirationTime;
            XmlNode xmlNodoService;

            // PASO 1: Genero el Login Ticket Request
            try
            {
                _globalUniqueID += 1;

                XmlLoginTicketRequest = new XmlDocument();
                XmlLoginTicketRequest.LoadXml(XmlStrLoginTicketRequestTemplate);

                xmlNodoUniqueId = XmlLoginTicketRequest.SelectSingleNode("//uniqueId");
                xmlNodoGenerationTime = XmlLoginTicketRequest.SelectSingleNode("//generationTime");
                xmlNodoExpirationTime = XmlLoginTicketRequest.SelectSingleNode("//expirationTime");
                xmlNodoService = XmlLoginTicketRequest.SelectSingleNode("//service");

                xmlNodoGenerationTime.InnerText = DateTime.Now.AddMinutes(-10).ToString("s");
                xmlNodoExpirationTime.InnerText = DateTime.Now.AddMinutes(+10).ToString("s");
                xmlNodoUniqueId.InnerText = System.Convert.ToString(_globalUniqueID);
                xmlNodoService.InnerText = argServicio;
                this.Service = argServicio;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                if (this._verboseMode)
                    Console.WriteLine(XmlLoginTicketRequest.OuterXml);
            }
            catch (Exception excepcionAlGenerarLoginTicketRequest)
            {
                throw new Exception("***Error GENERANDO el LoginTicketRequest : " + excepcionAlGenerarLoginTicketRequest.Message + excepcionAlGenerarLoginTicketRequest.StackTrace);
            }

            // PASO 2: Firmo el Login Ticket Request
            try
            {
                if (this._verboseMode)

                {

                    

                    X509Certificate2 certFirmante = new X509Certificate2();
 
      
                

                    certFirmante = new X509Certificate2(System.IO.File.ReadAllBytes(RutaDelCertificadoFirmante), argPassword,
             (X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet));

                    // Convierto el login ticket request a bytes, para firmar
                    Encoding EncodedMsg = Encoding.UTF8;
                byte[] msgBytes = EncodedMsg.GetBytes(XmlLoginTicketRequest.OuterXml);

                // Firmo el msg y paso a Base64
                byte[] encodedSignedCms = CertificadosX509Lib.FirmaBytesMensaje(msgBytes, certFirmante);
                cmsFirmadoBase64 = Convert.ToBase64String(encodedSignedCms);
                }
            }
            catch (Exception excepcionAlFirmar)
            {
                throw new Exception("***Error FIRMANDO el LoginTicketRequest c# :   " + excepcionAlFirmar.Message + " " + RutaDelCertificadoFirmante);
            }

            // PASO 3: Invoco al WSAA para obtener el Login Ticket Response
            try
            {
                 

                // '''''''''''''''''''' Instancio el ws desa o produc

                // factElectForm.WSLogin.LoginCMSClient cc = new factElectForm.WSLogin.LoginCMSClient();  ' desarrollo


               WSLoginProduc.LoginCMSClient servicioWsaa = new  WSLoginProduc.LoginCMSClient(); // Produc



                loginTicketResponse = servicioWsaa.loginCms(cmsFirmadoBase64);

                if (this._verboseMode)
                {
                    Console.WriteLine("***LoguinTicketResponse: ");
                    Console.WriteLine(loginTicketResponse);
                }
            }
            catch (Exception excepcionAlInvocarWsaa)
            {
                throw new Exception("***Error INVOCANDO al servicio WSAA : " + excepcionAlInvocarWsaa.Message);
            }


            // PASO 4: Analizo el Login Ticket Response recibido del WSAA
            try
            {
                XmlLoginTicketResponse = new XmlDocument();
                XmlLoginTicketResponse.LoadXml(loginTicketResponse);
                // XmlLoginTicketResponse.Save(argPath + "xml\\loginTicketResponseProduccion.xml")
                XmlLoginTicketResponse.Save(argSal);

                this.UniqueId = UInt32.Parse(XmlLoginTicketResponse.SelectSingleNode("//uniqueId").InnerText);
                this.GenerationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//generationTime").InnerText);
                this.ExpirationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//expirationTime").InnerText);
                this.Sign = XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText;
                this.Token = XmlLoginTicketResponse.SelectSingleNode("//token").InnerText;
            }
            catch (Exception excepcionAlAnalizarLoginTicketResponse)
            {
                throw new Exception("***Error ANALIZANDO el LoginTicketResponseProduccion : " + excepcionAlAnalizarLoginTicketResponse.Message);
            }

            return loginTicketResponse;
        }
     



    }

    
    class CertificadosX509Lib
    {
        public static bool VerboseMode = false;

        /// <summary>
        ///     ''' Firma mensaje
        ///     ''' </summary>
        ///     ''' <param name="argBytesMsg">Bytes del mensaje</param>
        ///     ''' <param name="argCertFirmante">Certificado usado para firmar</param>
        ///     ''' <returns>Bytes del mensaje firmado</returns>
        ///     ''' <remarks></remarks>
        public static byte[] FirmaBytesMensaje(byte[] argBytesMsg, X509Certificate2 argCertFirmante
    )
        {
            try
            {
                // Pongo el mensaje en un objeto ContentInfo (requerido para construir el obj SignedCms)
                ContentInfo infoContenido = new ContentInfo(argBytesMsg);
                SignedCms cmsFirmado = new SignedCms(infoContenido);

                // Creo objeto CmsSigner que tiene las caracteristicas del firmante
                CmsSigner cmsFirmante = new CmsSigner(argCertFirmante);
                cmsFirmante.IncludeOption = X509IncludeOption.EndCertOnly;

                if (VerboseMode)
                    Console.WriteLine("***Firmando bytes del mensaje...");
                // Firmo el mensaje PKCS #7
                cmsFirmado.ComputeSignature(cmsFirmante);

                if (VerboseMode)
                    Console.WriteLine("***OK mensaje firmado");

                // Encodeo el mensaje PKCS #7.
                return cmsFirmado.Encode();
            }
            catch (Exception excepcionAlFirmar)
            {
                throw new Exception("***Error al firmar: " + excepcionAlFirmar.Message);
                return null;
            }
        }

        /// <summary>
        ///     ''' Lee certificado de disco
        ///     ''' </summary>
        ///     ''' <param name="argArchivo">Ruta del certificado a leer.</param>
        ///     ''' <returns>Un objeto certificado X509</returns>
        ///     ''' <remarks></remarks>
     
    }


}