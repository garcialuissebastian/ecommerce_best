using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Be;
using Bll;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Net;

namespace Sport.Controllers
{
    public class AppController : Controller   
    {
        // GET: App
        public ActionResult Index()
        {
            return View();
        }

        string token = "ZXCSPORTDZXC";
        public string Articulos(string id, string codigo,string descripcion, string access_token)
        {

            try
            {
                if (access_token !=token)
                {
                    throw new Exception(" Invalid token.");
                }
                string SAL = DataTableToJSONWithJSONNet(Bll.BllBest.DameInstancia().ApiArticulos(id, codigo, descripcion));

                return SAL; //base.Json(SAL, JsonRequestBehavior.AllowGet); 
            }
            catch (Exception)
            { 
                throw;
            }
             
        }
        public ActionResult txt( )
        {
            string dirFullPath =  Server.MapPath("~/Log/");

            string text = System.IO.File.ReadAllText(dirFullPath+"seba.txt");
            return Content(text, "text/plain",Encoding.UTF8);

        }

        public   void EnviartxtAsync()
        {
            string dirFullPath = Server.MapPath("~/Log/");

            string text = System.IO.File.ReadAllText(dirFullPath + "articulos_prueba.txt");
            //  return Content(text, "text/plain", Encoding.UTF8);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://best.hardsoft.com.ar/App/TablasApiTxt?access_token=ZXCSPORTDZXC&nombre=articulos_prueba");
            httpWebRequest.ContentType = "text/plain";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = text;

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }

        }

        [HttpPost]
        public    void EnviartxtAsync2 (HttpPostedFileBase fileUpload)
        {

        }

        public ActionResult txt2()
        {
            string dirFullPath = Server.MapPath("~/Log/");

            string text = System.IO.File.ReadAllText(dirFullPath + "seba.txt");
            return Content(text, "text/plain", Encoding.Unicode);

        }


        public string TablasApi(string access_token, string Nombre, string desde , string hasta )
        {
            try
            { 
                if (access_token != token)
                {
                    throw new Exception(" Invalid token.");
                }
                string sal =DataTableToJSONWithJSONNet(Bll.BllBest.DameInstancia().TablasApi(Nombre, desde, hasta));
                return sal;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult TablasApiTxt(string access_token, string Nombre, string desde, string hasta)
        {
            try
            {
                if (access_token != token)
                {
                    throw new Exception(" Invalid token.");
                }
               
                string dirFullPath = Server.MapPath("~/Log/") + "seba.txt";

              

                Bll.BllBest.DameInstancia().TablasApiTxt(Nombre, desde, hasta, dirFullPath);
                string path = System.IO.File.ReadAllText(dirFullPath);
                return Content(path, "text/plain", Encoding.Unicode);
            }
            catch (Exception)
            {
                throw;
            }

        } 
      
        [HttpPost]
        public void TablasApiTxt(string access_token, string nombre)
        {
            string dirFullPath = Server.MapPath("~/Log/");

            string path =  dirFullPath + nombre+".txt";
            try
            {

           
            if (access_token != token)
            {
                throw new Exception(" Invalid token.");
            }

            System.IO.File.Delete(path);
            using (var reader = new StreamReader(System.Web.HttpContext.Current.Request.GetBufferedInputStream()))
            {
                string plainText = reader.ReadToEnd(); 
                    System.IO.File.WriteAllText(path, plainText);
            }
          

            Bll.BllBest.DameInstancia().TablasApiTxtimport(nombre, path);
            }

            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public ActionResult ArticulosMio(string id, string nombre)
        {

            return base.Json(true, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        //    public ActionResult Articulos(  string id  , string sucursal_id  , string tipo , string codigo , string talle , string descripcion , string proveedor_id , string precio_historico , string precio_compra , string precio_venta , string utilidad , string  fecha_lista , string fecha_compra , string fecha_baja , string estado ,
        //string codbar , string codbar_fab , string aud_ing_fec , string tipo_articulo_id , string aud_ing_por , string aud_mod_fec , string  aud_mod_por , string disponibleWeb , string etiquetaWeb , string Imagen1 , string Imagen2 , string Imagen3 , string Imagen4 , string Imagen5)

             
        public ActionResult Articulos(string access_token,List<Best_articulos> art)
        {

            try
            { 



                if (access_token != token)
                {
                    throw new Exception(" Invalid token.");
                }

                Bll.BllBest.DameInstancia().ApiArticulosNew(art);

                return  base.Json(true, JsonRequestBehavior.AllowGet); 
            }
            catch (Exception)
            {
                throw;
            }

        }

        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }

    }

    public class ArticulosMio
    {
        public string id { get; set; }
        public string nombre { get; set; }
    }
}