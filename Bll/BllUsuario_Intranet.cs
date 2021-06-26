using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Be;
using System.Web;

using System.Configuration;
 
using System.Net.Mail;
using System.Net;
 
namespace Bll
{
  public  class BllUsuario_Intranet
    {


         private  DalUsuario_Intranet _mapeador;

          public BllUsuario_Intranet()
          {

              _mapeador = new DalUsuario_Intranet();
          }

          private static BllUsuario_Intranet instancia = null;

          public static BllUsuario_Intranet DameInstancia()
        {

            if (instancia == null)
            {

                return new BllUsuario_Intranet();
            }
            else
            {
                return instancia;
            }
 
        }

        public USUARIO_INTRANET BuscarUsuario(string v_Legajo)
        {
            try
            {
                return this._mapeador.BuscarUsuario(v_Legajo);
            }
            catch (Exception)
            {
                throw;
            }
        }

         public void modificar_mail(Int32 v_Legajo, string v_mail)
        {
            try
            {
                this._mapeador.modificar_mail( v_Legajo,  v_mail);
            }
            catch (Exception)
            {
                throw;
            }
        }
             public void Modificar_pwd(string v_leg, string v_pass)
        {
            try
            {
                this._mapeador.Modificar_pwd( v_leg,  v_pass);
            }
            catch (Exception)
            {
                throw;
            }
        }
       public void delete_token(string v_toke)
        {
            try
            {
                  this._mapeador.delete_token( v_toke);
            }
            catch (Exception)
            {
                throw;
            }
        }
          public void EnviarMailCambiarPW(USUARIO_INTRANET v_leg)
          {

              string tokenRegistro = Guid.NewGuid().ToString();


              _mapeador.CanbiarPW_token(tokenRegistro,v_leg.USUARIO);

              try
              {

            
              
              StringBuilder stringBuilder = new StringBuilder();
              stringBuilder.Append("<!DOCTYPE HTML PUBLIC'-//W3C//DTD HTML 4.01 Transitional//EN' 'http://www.w3.org/TR/html4/loose.dtd'>");
              stringBuilder.Append("<html>");
              stringBuilder.Append("<head>");
              stringBuilder.Append("<meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>");
              stringBuilder.Append("<title>Intranet - Cambio de contraseña</title>");
              stringBuilder.Append("</head>");
              stringBuilder.Append("<body  >");
              stringBuilder.Append("<div style=' width:800px; margin:	auto; height:300px'>");
              stringBuilder.Append("  <img src='https://arquivirtual.quilmes.gov.ar/municipal/img/Header.png' border='0'> ");
              stringBuilder.Append("<div style='color:#000000; text-align:justify; font-size:20px'>");
              stringBuilder.Append("<h1>Importante </h1>");
              stringBuilder.Append("* * El sistema restablecerá por default su contraseña y lo enviara a la página de Logueo, donde para ingresar, debera introducir su Legajo y N° de documento.");
              stringBuilder.Append("<p>Para cambiar su contraseña haga click en continuar:</p>");
              stringBuilder.Append("<div style='text-align:center'>");
              stringBuilder.Append("<a href='https://arquivirtual.quilmes.gov.ar/municipal/CambiarPW.aspx?token=" + tokenRegistro + "'> ");
              stringBuilder.Append("<img src='https://arquivirtual.quilmes.gov.ar/municipal/img/botonMail.png' />");
              stringBuilder.Append("</a>");
                  stringBuilder.Append("</div>");
              stringBuilder.Append("</div>");
              stringBuilder.Append("</div>");
            
              
              stringBuilder.Append("</body>");
              stringBuilder.Append("</html>");

              StringBuilder stringBuilderSmall = new StringBuilder("Municipalidad de Quilmes - Intranet "  );

              //MailMessage mailMessage = new MailMessage();
              //mailMessage.To.Clear();
              //mailMessage.IsBodyHtml = true;
              //mailMessage.To.Add("lsgarcia2009@gmail.com");
              //mailMessage.From = new MailAddress(ConfigurationManager.AppSettings.Get("fromMail"));
              //mailMessage.Subject = stringBuilderSmall.ToString();
              //mailMessage.Body = stringBuilder.ToString();
              //SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings.Get("serverMail"));
              //client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings.Get("userMail"),
              //ConfigurationManager.AppSettings.Get("userPwd"));
              //client.Send(mailMessage);




              SmtpClient smtpClient = new SmtpClient();
              NetworkCredential basicCredential =
                  new NetworkCredential("intranet@quilmes.gov.ar", "#intranet2015");

              smtpClient.Host = "mail.quilmes.gov.ar";
              smtpClient.UseDefaultCredentials = false;
              smtpClient.Credentials = basicCredential;


                  MailMessage message = new MailMessage();
              MailAddress fromAddress = new MailAddress("intranet@quilmes.gov.ar");

            
              message.From = fromAddress;
              message.Subject = stringBuilderSmall.ToString();
              //Set IsBodyHtml to true means you can send HTML email.
              message.IsBodyHtml = true;
              message.Body = stringBuilder.ToString();
              message.To.Add(v_leg.MAIL.Trim());

              smtpClient.Send(message);



              }
              catch (Exception)
              {

                  throw;
              }
          }
          public string Md5(string v_pass)
          {
              try
              {
                  return this._mapeador.Md5(v_pass);

              }
              catch (Exception)
              {

                  throw;
              }

          }

         public void EstablecerUsuarioActual(Be.USUARIO_INTRANET usuario)
          {
              HttpContext.Current.Session["UsuarioActual"] = usuario;
          }

         public  Be.USUARIO_INTRANET ObtenerUsuarioActual()
          {
              try
              {

            

              return ((Be.USUARIO_INTRANET) HttpContext.Current.Session["UsuarioActual"]);

              }
              catch (Exception)
              {

                  throw;
              }
         }





          public bool TienePermisoPara(string nombrePermiso)
          {
              bool tienePermiso = false;

              USUARIO_INTRANET usuario;
              if (this.ObtenerUsuarioActual() == null) {

                  usuario = new USUARIO_INTRANET();
              }
              else { usuario = this.ObtenerUsuarioActual(); }
        
             
              
              
              
              
              if (usuario != null)
              {
                  if (usuario.Perfil != null)
                  {
                      tienePermiso = usuario.Perfil.EsValido(nombrePermiso);
                  }
              }

              return tienePermiso;
          }




          public Be.USUARIO_INTRANET Obtener_usuario(string v_Legajo, string v_pass)
          {
              try
              {


                  Be.USUARIO_INTRANET v_user = this._mapeador.Obtener_usuario(v_Legajo, v_pass);
                  EstablecerUsuarioActual(v_user);
                  
                  return v_user;

              }
              catch (Exception)
              {
                  throw;
              }
          }

    }
}
