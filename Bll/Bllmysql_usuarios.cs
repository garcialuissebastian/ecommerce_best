using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Be;
using System.Net.Mail;
using System.Net;
using System.Web;
namespace Bll
{
    public class Bllmysql_usuarios
    {
        private Dal.Dalmysql_usuarios _mapeador;
        
        public Bllmysql_usuarios()
        {
            _mapeador = new Dalmysql_usuarios();
        }

        private static Bllmysql_usuarios instancia = null;

        public static Bllmysql_usuarios DameInstancia()
        {
            if (instancia == null)
            {
                return new Bllmysql_usuarios();
            }
            else
            {
                return instancia;
            }
        }
        public List<Combos> ListarRoles(String SIS)
        { 
              try
            {
                return this._mapeador.ListarRoles(  SIS);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public void Modificar_pwd(string v_leg, string v_pass)
        //{
        //    try
        //    {
        //        return this._mapeador.Modificar_pwd(  v_leg, v_pass);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public string club_by_user(string v_id)
        {
            try
            {
                return this._mapeador.club_by_user(v_id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Alta(mysql_usuarios v_obj)

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

        public void Modificar_pwd(string v_mail, string v_pass)
        {
            try
            {
                this._mapeador.Modificar_pwd(v_mail, v_pass);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string envio_pwd(string v_mail)
        {

            string bb = "N";
            try
            {

                string ppp = Bll.Bllmysql_usuarios.DameInstancia().GenerateRandomCode();
                Modificar_pwd(v_mail, Encriptor.DameInstancia().GetMD5(ppp));








                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<!DOCTYPE HTML PUBLIC'-//W3C//DTD HTML 4.01 Transitional//EN' 'http://www.w3.org/TR/html4/loose.dtd'>");
                stringBuilder.Append("<html>");
                stringBuilder.Append("<head>");
                stringBuilder.Append("<meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>");
                stringBuilder.Append("<title>  Cambio de contraseña</title>");
                stringBuilder.Append("</head>");
                stringBuilder.Append("<body  >");
                stringBuilder.Append("<div style=' width:800px; margin:	auto; height:300px'>");
                
                stringBuilder.Append("<div style='color:#000000; text-align:justify; font-size:20px'>");
                //stringBuilder.Append("<h1>Sr. Contribuyente </h1>");
                stringBuilder.Append("Sr. Usuario le enviamos una nueva contraseña la cual luego de ingresar, al sistema, usted debera cambiar.");
                stringBuilder.Append("<p>Usuario: " + v_mail + "</p>");
                stringBuilder.Append("<p>Contraseña: " + ppp + "</p>");

                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");


                stringBuilder.Append("</body>");
                stringBuilder.Append("</html>");

                StringBuilder stringBuilderSmall = new StringBuilder(" BEST DEPORTES ");

                SmtpClient smtpClient = new SmtpClient();
                NetworkCredential basicCredential =
                    new NetworkCredential("sistemas@hardsoft.com.ar", "Seba2423");

                smtpClient.Host = "mail.wi271852.ferozo.com";
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = basicCredential;


                MailMessage message = new MailMessage();
                MailAddress fromAddress = new MailAddress("sistemas@hardsoft.com.ar");


                message.From = fromAddress;
                message.Subject = stringBuilderSmall.ToString();
                //Set IsBodyHtml to true means you can send HTML email.
                message.IsBodyHtml = true;
                message.Body = stringBuilder.ToString();
                message.To.Add(v_mail.Trim());

                smtpClient.Send(message);

                bb = "S";




            }
            catch (Exception)
            {

                throw;
            }
            return bb;

        }

        public string GenerateRandomCode()
        {
            try
            {
                return this._mapeador.GenerateRandomCode();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string existe_mail_hab(string v_mail)
        {
            try
            {
                return this._mapeador.existe_mail_hab(v_mail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Be.mysql_usuarios ObtenerUsuarioActual()
        {
            try
            {

                return ((Be.mysql_usuarios)HttpContext.Current.Session["UsuarioActual"]);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool TienePermisoPara(string nombrePermiso)
        {
            bool tienePermiso = false;

            mysql_usuarios usuario;
            if (this.ObtenerUsuarioActual() == null)
            {

                usuario = new mysql_usuarios();
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
        public mysql_usuarios Obtener_usuario(string v_mail)
        {
            try
            {
                return this._mapeador.Obtener_usuario(v_mail);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modificacion(mysql_usuarios v_obj)

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

        public List<mysql_usuarios> Listar()


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





        /////////////////////////////////////// 
        ///
        public void Alta_mysql_usuarios(mysql_usuarios v_obj)

        {
            try
            {
                this._mapeador.Alta_mysql_usuarios(v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Modificacion_mysql_usuarios(mysql_usuarios v_obj)

        {
            try
            {
                this._mapeador.Modificacion_mysql_usuarios(v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<mysql_usuarios> Listar_mysql_usuarios(string v_tipo, string v_valor)


        {
            try
            {
                return this._mapeador.Listar_mysql_usuarios(v_tipo, v_valor);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Eliminar_mysql_usuarios(Int32 v_id)

        {
            try
            {
                this._mapeador.Eliminar_mysql_usuarios(v_id);
            }
            catch (Exception)
            {
                throw;
            }
        }

    










}
}
