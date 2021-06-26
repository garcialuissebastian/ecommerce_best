using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Be;
namespace Sport.Controllers
{
    public abstract class ApplicationController : Controller
    {
      
        // para q cargue una sola vez el menu
        public ApplicationController()
        {
            // if (HttpContext  == null)
            List<Best_articulos_carrito> carrito = new List<Best_articulos_carrito>();
            //{ 
            //   Session["Menu"] = Bll.BllBest.DameInstancia().best_Menu(); 
            //}
            //ViewData["menu"] = (Be.Best_Menu)HttpContext.Session["Menu"];
        }

    }
}