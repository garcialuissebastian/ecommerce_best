using Be;
using Bll;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;



using System.IO.Compression;
namespace Sport.Controllers
{
    public class BestController : Controller
    {
        // GET: Best
        public ActionResult Index()
        {
            if ( HttpContext.Session["UsuarioWeb"] == null)
            {
                return Redirect("/");
            }


           
            return View();
        }

        public ActionResult VerCompras()
        {
            if (HttpContext.Session["UsuarioWeb"] == null)
            {
                return Redirect("/");
            }



            return View();
        }
        public ActionResult KeepActiveSession()
        {
            if (HttpContext.Session["ActiveSession"] != null)
            {
                DateTime inicio = DateTime.Parse(HttpContext.Session["ActiveSession"].ToString());
                if (inicio < DateTime.Now)

                    return base.Json(true);
                else return base.Json(false);
            }
            else
                return base.Json(false);


        }

        [HttpPost]
        public ActionResult Subir_archivo(HttpPostedFileBase file)
        {
            string imgPath = "";
            if (file != null && file.ContentLength > 0)
                try
                {
                     imgPath = Path.GetFileName(DateTime.Now.ToString("hhmmss") + file.FileName);
                    string path = Path.Combine(Server.MapPath("~/img/subidas"),   imgPath);
    

                    file.SaveAs(path);
                    
                }
                catch (Exception ex)
                {
                    imgPath = "";
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "Debe seleccionar una imagen";
            }
            return base.Json(imgPath);
        }


       
        public ActionResult getPath()
        {
            string imgPath = "";
          
                try
                {
                imgPath = Server.MapPath("~/");
                ViewBag.Message = imgPath;
            }
                catch (Exception ex)
                {
                    imgPath = "";
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
           
            return View();

            
        }

        public ActionResult Articulos()
        {

            //Be.Best_UsuariosWeb entidad = BllBest.DameInstancia().iniciarSessionAdmin( "LSGARCIA2009@GMAIL.COM");
            //HttpContext.Session["ActiveSession"] = DateTime.Now.ToString();
            //HttpContext.Session["UsuarioWeb"] = entidad;



            if (HttpContext.Session["UsuarioWeb"] == null)
            {
                return Redirect("/");
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WsListar_articulos(string v_tipo, string v_valor)
        {
            List<Best_articulos_venta> list = new List<Best_articulos_venta>(); 
                try
            {
                list = BllBest.DameInstancia().listaArticulosAdmin(v_tipo,  v_valor);
            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(list);


        }


        [HttpPost]
        public ActionResult WsListar_compras(string v_tipoCbate, string v_tipo, string v_valor1, string v_valor2)
        {
            List<Be.kx_cbtes> list = new List<Be.kx_cbtes>();
            try
            {
                if ( Session["UsuarioWeb"] == null)
                {

                    throw new Exception("Acceso no valido.");

                }

                string user = "";
                string conf = "";



                list = Bll.BllBest.DameInstancia().WsListarCompras(v_tipoCbate, v_tipo, v_valor1.ToUpper(), v_valor2.ToUpper(), user, conf);
                Session["Grilla"] = list;


            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(list);


        }

        [HttpPost]
        public ActionResult  Wscttiventas(string v_tipoCbate, string v_tipo, string v_valor1, string v_valor2)
        {
            try
            {

                Citti ct = new Citti();

                ct.Wscttiventas(v_tipoCbate,   v_tipo,  v_valor1,  v_valor2);

            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(true);

        }



        public void RpExcel()
        {


            try
            {
                List<string> noo = new List<string>();
                noo.Add("Concepto");
                noo.Add("DocTipo");
                noo.Add("usuario");
                noo.Add("Id");
                noo.Add("CantReg");
                noo.Add("aud");
                noo.Add("Vendedor");
                noo.Add("Cliente");
                noo.Add("CbteHasta");
                noo.Add("CbteFch");

                noo.Add("MonId");
                noo.Add("MonCotiz");
                noo.Add("CodBarra");
                noo.Add("Cuit");

                noo.Add("Id_config");
                noo.Add("Pago_Efectivo");
                noo.Add("Pago_Bancos");
                noo.Add("Pago_Cheques");

                noo.Add("Pago_Tarjetas");
                noo.Add("ImporteLetra");
                noo.Add("Contado");
                noo.Add("treintaDias");


                noo.Add("Tj_Debito");
                noo.Add("Tj_Credito");
                noo.Add("CtaCte");
                noo.Add("Cheque");
                noo.Add("Otra");
                noo.Add("Remito");
                noo.Add("ImpTotConc");
                noo.Add("CAE");
                noo.Add("CAEFchVto");
                noo.Add("Observaciones");
                noo.Add("CantAlic");
                noo.Add("FchVtoPago");
                noo.Add("ImpOpEx");
                noo.Add("ImpTrib");



                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=Cbtes.xls");
                Response.AddHeader("Content-Type", "application/excel");

                GridView grid = new GridView();
                StringWriter stringWriter = new StringWriter();
                HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

                DataTable dts = exel.ConvertToDataTable(((List<Be.kx_cbtes>) Session["Grilla"]), noo);

                // para que haga la funcion SUM()  desde execl tengo que cambiar la configuracion reginal y pasar . como separador de mil y , como decimal


                // TITULO
              //  htmlTextWriter.Write("<div> </div> <table border='1' style='margin-top: 10px;'  >    <thead>   <tr><th style='background-color: #ddd;' rowspan='5' colspan='14'><img src='http://www.hardsoft.com.ar/images/logo.png'></th></tr>  </thead></table> ");

                grid.DataSource = dts;
                grid.DataBind();
                grid.RenderControl(htmlTextWriter);
                Response.Write(stringWriter.ToString());

                htmlTextWriter.Close();
                stringWriter.Close();


                Response.End();




               

             

            }
            catch (Exception)
            {

                throw;
            }


        }

        [HttpPost]
        public ActionResult EncriptarDesdeJS(string valueJS)
        {
            Bll.QueryString obQueryString = new Bll.QueryString();
            obQueryString.Add("mp", valueJS);
            obQueryString.Add("tipo", "0");
            string Purl = Bll.Encriptor.DameInstancia().EncryptQueryString(obQueryString).ToString();
            return base.Json(Purl); 
        }

        [HttpPost]
        public ActionResult WsBsqBest_articulos_admin(string v_valor)
        {
            List<Best_articulos_admin> list = new List<Best_articulos_admin>();
            try
            {
                list = BllBest.DameInstancia().BsqBest_articulos_admin(v_valor);
                Session["GrillaArt"] = list;
            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(list); 

        }

        [HttpPost]
        public ActionResult WsActualizarArticuloWeb( string v_tipo_art, string v_img1, string v_img2, string v_img3, string v_img4, string v_img5)
        {
            List<Best_articulos_admin> list = new List<Best_articulos_admin>();
            List<Best_articulos_admin> listFinal = new List<Best_articulos_admin>();
            try
            {
                list = (List<Best_articulos_admin>)Session["GrillaArt"];
                foreach (var item in list)
                {
                    item.tipo_articulo_id = v_tipo_art;
                    item.Imagen1 = v_img1;
                        item.Imagen2 = v_img2;
                    item.Imagen3 = v_img3;
                    item.Imagen4 = v_img4;
                    item.Imagen5 = v_img5;
                    listFinal.Add(item);
                  
                    
                }

                BllBest.DameInstancia().ActualizarArticuloWeb(listFinal);
                Session["GrillaArt"] = null;
            }
            catch (Exception )
            {
                throw;
                
            }
            return base.Json(true);

        }

        [HttpPost]
        public ActionResult WsActualizarDisponible(string Id, string v_valor, string v_todos)
        {
            List<Best_articulos_admin> list = new List<Best_articulos_admin>();
            List<Best_articulos_admin> listFinal = new List<Best_articulos_admin>();
            try
            {
                list =(List<Best_articulos_admin>) Session["GrillaArt"];
                foreach (var item in list)
                {
                    if (v_todos=="SI")
                    {
                        item.disponibleWeb = v_valor;
                        listFinal.Add(item);
                    }
                    else
                    {
                        if (item.Id ==Id)
                        {
                            item.disponibleWeb = v_valor;
                            listFinal.Add(item);
                        }
                        else { listFinal.Add(item); }

                    }
                }


                Session["GrillaArt"] = listFinal;
            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(true);

        }

        [HttpPost]
        public ActionResult WsActualizarListaDiciplina(string Id, string v_valor)
        {
            List<Best_articulos_admin> list = new List<Best_articulos_admin>();
            List<Best_articulos_admin> listFinal = new List<Best_articulos_admin>();
            try
            {
                list = (List<Best_articulos_admin>)Session["GrillaArt"];
                Combos cmb = new Combos();
                cmb.descripcion = v_valor;
                cmb.valor = Id;

                List<Combos> mlist = new List<Combos>();

                mlist.Add(cmb);
                foreach (var item1 in list[0].ListaDiciplina)
                {
                    if (item1.valor != Id)
                    {
                        mlist.Add(item1);
                    }
                }


                foreach (var item in list)
                {
                    item.ListaDiciplina = mlist;
                    listFinal.Add(item);
                }
                Session["GrillaArt"] = listFinal;
            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(listFinal);

        }

        [HttpPost]
        public ActionResult WsActualizarListaGenero(string Id, string v_valor )
        {
            List<Best_articulos_admin> list = new List<Best_articulos_admin>();
            List<Best_articulos_admin> listFinal = new List<Best_articulos_admin>();
            try
            {
                list = (List<Best_articulos_admin>)Session["GrillaArt"];
                Combos cmb = new Combos();
                cmb.descripcion = v_valor;
                cmb.valor = Id;

                List<Combos> mlist = new List<Combos>();

                mlist.Add(cmb);
                foreach (var item1 in list[0].ListaGeneros  )
                { 
                    if (item1.valor!=Id) {
                        mlist.Add(item1);
                    } 
                }
                 

                foreach (var item in list)
                { 
                    item.ListaGeneros = mlist;
                    listFinal.Add(item);
                }
                Session["GrillaArt"] = listFinal;
            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(listFinal);

        }

        [HttpPost]
        public ActionResult WsEliminarListaGenero(string Id )
        {
            List<Best_articulos_admin> list = new List<Best_articulos_admin>();
            List<Best_articulos_admin> listFinal = new List<Best_articulos_admin>();
            try
            {
                list = (List<Best_articulos_admin>)Session["GrillaArt"];
                
                List<Combos> mlist = new List<Combos>(); 
               
                foreach (var item1 in list[0].ListaGeneros)
                {
                    if (item1.valor != Id)
                    {
                        mlist.Add(item1);
                    }
                } 

                foreach (var item in list)
                {
                    item.ListaGeneros = mlist;
                    listFinal.Add(item);
                }
                Session["GrillaArt"] = listFinal;
            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(listFinal);

        }

        [HttpPost]
        public ActionResult WsEliminarListaDiciplina(string Id)
        {
            List<Best_articulos_admin> list = new List<Best_articulos_admin>();
            List<Best_articulos_admin> listFinal = new List<Best_articulos_admin>();
            try
            {
                list = (List<Best_articulos_admin>)Session["GrillaArt"];

                List<Combos> mlist = new List<Combos>();

                foreach (var item1 in list[0].ListaDiciplina)
                {
                    if (item1.valor != Id)
                    {
                        mlist.Add(item1);
                    }
                }

                foreach (var item in list)
                {
                    item.ListaDiciplina = mlist;
                    listFinal.Add(item);
                }
                Session["GrillaArt"] = listFinal;
            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(listFinal);

        }

        [HttpPost]
        public ActionResult WsActualizarEtiquetas(string Id, string v_valor, string v_todos)
        {
            List<Best_articulos_admin> list = new List<Best_articulos_admin>();
            List<Best_articulos_admin> listFinal = new List<Best_articulos_admin>();
            try
            {
                list = (List<Best_articulos_admin>)Session["GrillaArt"];
                foreach (var item in list)
                {
                    if (v_todos == "SI")
                    {
                        item.etiquetaWeb = v_valor;
                        listFinal.Add(item);
                    }
                    else
                    {
                        if (item.Id == Id)
                        {
                            item.etiquetaWeb = v_valor;
                            listFinal.Add(item);
                        }
                        else { listFinal.Add(item); }

                    }
                }


                Session["GrillaArt"] = listFinal;
            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(true);

        }

        [HttpPost]
        public ActionResult WsActualizarGenero(string Id, string v_valor, string v_todos)
        {
            List<Best_articulos_admin> list = new List<Best_articulos_admin>();
            List<Best_articulos_admin> listFinal = new List<Best_articulos_admin>();
            try
            {
                list = (List<Best_articulos_admin>)Session["GrillaArt"];
                foreach (var item in list)
                {
                    if (v_todos == "SI")
                    {
                        item.genero = v_valor;
                        Combos c1 = new Combos();
                        c1.valor = v_valor;
                        item.ListaGeneros.Add(c1);
                        listFinal.Add(item);

                    }
                    else
                    {
                        if (item.Id == Id)
                        {
                            item.genero = v_valor;
                            listFinal.Add(item);
                        }
                        else { listFinal.Add(item); }

                    }
                }


                Session["GrillaArt"] = listFinal;
            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(true);

        }

        [HttpPost]
        public ActionResult WsActualizarCATEGORIA(string Id, string v_valor, string v_todos)
        {
            List<Best_articulos_admin> list = new List<Best_articulos_admin>();
            List<Best_articulos_admin> listFinal = new List<Best_articulos_admin>();
            try
            {
                list = (List<Best_articulos_admin>)Session["GrillaArt"];
                foreach (var item in list)
                {
                    if (v_todos == "SI")
                    {
                        item.categoria = v_valor;
                        listFinal.Add(item);
                    }
                    else
                    {
                        if (item.Id == Id)
                        {
                            item.categoria = v_valor;
                            listFinal.Add(item);
                        }
                        else { listFinal.Add(item); }

                    }
                }


                Session["GrillaArt"] = listFinal;
            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(true);

        }

        [HttpPost]
        public ActionResult WsActualizarDICIPLINA(string Id, string v_valor, string v_todos)
        {
            List<Best_articulos_admin> list = new List<Best_articulos_admin>();
            List<Best_articulos_admin> listFinal = new List<Best_articulos_admin>();
            try
            {
                list = (List<Best_articulos_admin>)Session["GrillaArt"];
                foreach (var item in list)
                {
                    if (v_todos == "SI")
                    {
                        item.diciplina = v_valor;
                        listFinal.Add(item);
                    }
                    else
                    {
                        if (item.Id == Id)
                        {
                            item.diciplina = v_valor;
                            listFinal.Add(item);
                        }
                        else { listFinal.Add(item); }

                    }
                }


                Session["GrillaArt"] = listFinal;
            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(true);

        }

        [HttpPost]
        public ActionResult WsActualizarMARCA(string Id, string v_valor, string v_todos)
        {
            List<Best_articulos_admin> list = new List<Best_articulos_admin>();
            List<Best_articulos_admin> listFinal = new List<Best_articulos_admin>();
            try
            {
                list = (List<Best_articulos_admin>)Session["GrillaArt"];
                foreach (var item in list)
                {
                    if (v_todos == "SI")
                    {
                        item.marca = v_valor;
                        listFinal.Add(item);
                    }
                    else
                    {
                        if (item.Id == Id)
                        {
                            item.marca = v_valor;
                            listFinal.Add(item);
                        }
                        else { listFinal.Add(item); }

                    }
                }


                Session["GrillaArt"] = listFinal;
            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(true);

        }


        [HttpPost]
        public ActionResult WsListarTipoArticulos(string v_valor)
        {
            List<Be.Combos> list = new List<Be.Combos>();
            try
            {
                list = BllBest.DameInstancia().ListarTipoArticulos(  v_valor);
            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(list); 
        }


        [HttpPost]
        public ActionResult WsListarCmbDimensiones(string v_tipo, string v_valor)
        {
            List<Be.Combos> list = new List<Be.Combos>();
            try
            {
                List<envio_dimensiones> listD = BllBest.DameInstancia().Listar_envio_dimensiones("0", v_valor);

                foreach (var item in listD)
                {
                    Be.Combos obj = new Be.Combos();
                    obj.descripcion = item.NombreCompleto();
                    obj.valor = item.id;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(list);
        }
        [HttpPost]
        public ActionResult WsListaClasificacion(string tipo_clasificacion_id, string tipo_articulo_id)
        {// `tipo_clasificacion_id`   'genero-categoria-disciplina-marca',
            // `tipo_articulo_id`   'calzado_indumentaria-accesorios',
            List<Be.Combos> list = new List<Be.Combos>();
            try
            {
                list = BllBest.DameInstancia().ListaClasificacion( tipo_clasificacion_id, tipo_articulo_id);
            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(list);
        }
        [HttpPost]
        public ActionResult WsListarEtiquetas(string v_valor)
        {
            List<Be.Combos> list = new List<Be.Combos>();
            try
            {
                list = BllBest.DameInstancia().ListarEtiquetas(v_valor);
            }
            catch (Exception)
            {

                throw;
            }
            return base.Json(list);
        }
        public ActionResult WsIniciar(string nombre, string pass)
        { 
            bool sal = false;
            try
            { 
                Be.Best_UsuariosWeb entidad = BllBest.DameInstancia().iniciarSessionAdmin(nombre.Replace("=", "").Replace("'", "").Replace("-", "").Trim());
                var v_clave = Encriptor.DameInstancia().GetMD5(pass.Trim());
                if (v_clave == entidad.pass)
                {
                    Session["UsuarioWeb"] = entidad;
                    sal = true;
                }
                else { Session["UsuarioWeb"] = null; }


            }
            catch (Exception)
            {
                Session["UsuarioWeb"] = null;
                throw;
            }


            return base.Json(sal);


        }

    }
}