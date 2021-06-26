using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using Be;

namespace Bll
{
  public  class BllBest
    {


        private Dal.DalBest _mapeador;

        public BllBest()
        {
            _mapeador = new Dal.DalBest();
        }
        public string GenerateRandomCode()
        {
            Random r = new Random();
            string s = "";
            for (int j = 0; j < 5; j++)
            {
                int i = r.Next(3);
                int ch;
                switch (i)
                {
                    case 1:
                        ch = r.Next(0, 9);
                        s = s + ch.ToString();
                        break;
                    case 2:
                        ch = r.Next(65, 90);
                        s = s + Convert.ToChar(ch).ToString();
                        break;
                    case 3:
                        ch = r.Next(97, 122);
                        s = s + Convert.ToChar(ch).ToString();
                        break;
                    default:
                        ch = r.Next(97, 122);
                        s = s + Convert.ToChar(ch).ToString();
                        break;
                }
                r.NextDouble();
                r.Next(100, 1999);
            }
            return s;
        }

        private static BllBest instancia = null;

        public static BllBest DameInstancia()
        {
            if (instancia == null)
            {
                return new BllBest();
            }
            else
            {
                return instancia;
            }
        }


     
        public void ConfitmarRegistrarcion(string token)
        {
            try
            {
                this._mapeador.ConfitmarRegistrarcion( token);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void CarritoSt(List<Be.Best_articulos_carrito> v_valor)
        {
            try
            {
                this._mapeador.CarritoSt(  v_valor);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public string GetMontoTotal()
        {
            
            return HttpContext.Current.Session["pesosTotal"].ToString();
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

        public Combos envioLocal(string v_cp)
        {
            try
            {
                return this._mapeador.envioLocal( v_cp);
            }
            catch (Exception)
            {
                throw;
            }

        }


        public Be.Best_UsuariosWeb iniciarSession(string token)
        {
            try
            {
                return this._mapeador.iniciarSession(token);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Be.Best_articulos_carrito> ArtCarrito(string v_tipo, string v_valor)
        {
            try
            {
                return this._mapeador.ArtCarrito(  v_tipo,   v_valor);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<mercadopagos> Listar_mercadopagos(string v_tipo, string v_valor)
        {
            try
            {
                return this._mapeador.Listar_mercadopagos(  v_tipo,  v_valor);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public Be.Best_UsuariosWeb iniciarSessionAdmin(string token)
        {
            try
            {
                return this._mapeador.iniciarSessionAdmin(token);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public mercadopagos BsqPago(string v_mp)
        {
            try
            {
                return this._mapeador.BsqPago(  v_mp);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public string InsertMp(mercadopagos fe)
        {
            try
            {
 return this._mapeador.InsertMp(  fe);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public Best_articulos_ventaId ArticuloVentaId(int? vart)
        {

            try
            {
                return this._mapeador.ArticuloVentaId(vart);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public string envio_registracion(string v_mail,string url, string token, string nombre, string urlActual)
        {

            string bb = "N";
            try
            { 

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<!DOCTYPE HTML PUBLIC'-//W3C//DTD HTML 4.01 Transitional//EN' 'http://www.w3.org/TR/html4/loose.dtd'>");
                stringBuilder.Append("<html>");
                stringBuilder.Append("<head>");
                stringBuilder.Append("<meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>");
                stringBuilder.Append("<title>BEST DEPORTES - REGISTRACIÓN</title>");
                stringBuilder.Append("</head>");
                stringBuilder.Append("<body  >");
                stringBuilder.Append("<div style=''>");
              stringBuilder.Append("   <h4>	Estimado/a "+nombre+": </h4> ");
                stringBuilder.Append(" <p> Gracias por registrarse en nuestra web y le damos la bienvenida.<br/>");
               stringBuilder.Append(" Para finalizar haga click en el siguiente  boton.</p> <br/>");
                stringBuilder.Append(" <a href='"+ urlActual + "/Articulos/confirmarRegitro/?vtoken="+token+ "&url=" + url + "'    >  <img src='https://hardsoft.com.ar/img/btnMail.png' />   </a>");
                stringBuilder.Append("</div>");
           
            
                stringBuilder.Append("</body>");
                stringBuilder.Append("</html>");

                StringBuilder stringBuilderSmall = new StringBuilder(" BEST DEPORTES ");

                SmtpClient smtpClient = new SmtpClient();
                NetworkCredential basicCredential =
                           new NetworkCredential("sistemas@bestdeportes.com.ar", "Kilmes2423");

                smtpClient.Host = "dtc023.ferozo.com";
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = basicCredential;


                MailMessage message = new MailMessage();
                MailAddress fromAddress = new MailAddress("sistemas@bestdeportes.com.ar");


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

        public List<Combos> tipos_talles(string v_talle)
        {
            try
            {
                return this._mapeador.tipos_talles(  v_talle);
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
                    new NetworkCredential("sistemas@bestdeportes.com.ar", "Kilmes2423");

                smtpClient.Host = "dtc023.ferozo.com";
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = basicCredential;


                MailMessage message = new MailMessage();
                MailAddress fromAddress = new MailAddress("sistemas@bestdeportes.com.ar");


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
        public bool TieneUsuario(string dni)
        {

            try
            {
                return this._mapeador.TieneUsuario( dni);
            }
            catch (Exception)
            {

                throw;
            }
     

        }

        public bool TieneUsuarioMail(string mail)
        {

            try
            {
                return this._mapeador.TieneUsuarioMail(mail);
            }
            catch (Exception)
            {

                throw;
            }


        }

        public void Registrarcion(string nombre, string apellido, string fecha_nac, string dni, string mail, string password, string tel, string url,  string urlActual, string domicilio)
        {
            try
            {
                string token = Guid.NewGuid().ToString();
              this._mapeador.Registrarcion(  nombre,  apellido,  fecha_nac,   dni,  mail,  password, tel, token, domicilio );

                envio_registracion(mail, url, token, apellido +" "+ nombre, urlActual);
            }
            catch (Exception)
            {
                throw;
            }

        }


        //public List<articulos> Listar_articulos(string v_tipo, string v_valor)
        //{


        //}

        public void ActualizarArticuloWeb(List<Best_articulos_admin> lista)
        {
            try
            {
                 this._mapeador.ActualizarArticuloWeb(lista);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<Best_articulos_admin> BsqBest_articulos_admin(string cod)
        {
            try
            {
                return this._mapeador.BsqBest_articulos_admin( cod);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<Be.Combos> ListaClasificacion(string tipo_clasificacion_id, string tipo_articulo_id)
        {
            try
            {
                return this._mapeador.ListaClasificacion(  tipo_clasificacion_id,   tipo_articulo_id);
            }
            catch (Exception)
            {
                throw;
            }

        }


        public List<Be.Combos> ListarTipoArticulos(string ids)
        {
            try
            {
                return this._mapeador.ListarTipoArticulos( ids);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Be.Combos> ListarEtiquetas(string ids)
        {
            try
            {
                return this._mapeador.ListarEtiquetas(ids);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public Dictionary<string, List<string>> Wscttiventas(string v_tipoCbate, string v_tipo, string v_valor1, string v_valor2, string v_user, string v_conf)
        {
            try
            {
                return this._mapeador.Wscttiventas(v_tipoCbate, v_tipo, v_valor1, v_valor2, v_user, v_conf);
   
                }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Be.kx_cbtes> WsListarCompras(string v_tipoCbate, string v_tipo, string v_valor1, string v_valor2, string v_user, string v_conf)
        {
            try
            {
                return this._mapeador.WsListarCompras(  v_tipoCbate,   v_tipo, v_valor1,   v_valor2,  v_user,  v_conf);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Best_articulos_venta> listaArticulosAdmin(string ptipo, string pval)
        {
            try
            {
                return this._mapeador.listaArticulosAdmin(  ptipo,  pval);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public Best_articulos_ventaId listaArticuloVentaId(int? vart)
        {

            try
            {
                return this._mapeador.listaArticuloVentaId( vart);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<Best_articulos_venta> listaArticulosVenta(int? ptipo, int? pcat, int? ptipocat)
        {
            try
            {
                return this._mapeador.listaArticulosVenta(  ptipo, pcat,   ptipocat);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Best_articulos_venta> listaArticulosVentaHome(int? ptipo, int? pcat, int? ptipocat)
        {
            try
            {
                return this._mapeador.listaArticulosVentaHome(ptipo, pcat, ptipocat);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Best_Menu> best_Menu()
        {
            try
            {
                return this._mapeador.best_Menu();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public DataTable TablasApi(string Nombre, string desde, string hasta)
        {
            try
            {
             return   this._mapeador.TablasApi( Nombre,  desde,   hasta);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void TablasApiTxtimport(string Nombre, string path)
        {
            try
            {
                this._mapeador.TablasApiTxtimport(  Nombre,  path);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void TablasApiTxt(string Nombre, string desde, string hasta,string path)
        {
            try
            {
                 this._mapeador.TablasApiTxt(Nombre, desde, hasta, path);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void ApiArticulosNew(List<Best_articulos> art)
        {
            try
            {
                this._mapeador.ApiArticulosNew(  art);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public DataTable ApiArticulos(string id, string codigo, string descripcion)
        {
            try
            {
                return this._mapeador. ApiArticulos(  id,   codigo,   descripcion);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public Int32 indiceTipoArticulo(Int32 IdtipoArt) //'CALZADO-INDUMENTARIA-ACCESORIOS'
        { Int32 sal = 0;

            if (IdtipoArt==1) //'CALZADO
            {
                sal = 0;
            }

            if (IdtipoArt == 2) //'INDUMENTARIA
            {
                sal = 1;
            }

            if (IdtipoArt == 3) //'ACCESORIOS
            {
                sal = 2;
            }

            return sal;
        }
        public List<Best_categorias> best_Categorias()
        {
            try
            {
              return  this._mapeador. best_Categorias();
            }

            catch (Exception)
            {
                throw;
            }

        }


        public void Alta_envio_dimensiones(envio_dimensiones v_obj)

        {
            try
            {
                this._mapeador.Alta_envio_dimensiones(v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<envio_dimensiones> Listar_envio_dimensiones(string v_tipo, string v_valor)


        {
            try
            {
                return this._mapeador.Listar_envio_dimensiones(v_tipo, v_valor);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Eliminar_envio_dimensiones(Int32 v_id)

        {
            try
            {
                this._mapeador.Eliminar_envio_dimensiones(v_id);
            }
            catch (Exception)
            {
                throw;
            }
        }

    


}

}
