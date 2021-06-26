using NUnit.Framework;
using RestSharp;
using Sport.Models.Mercadolibre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sport.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
           HttpContext.Session["ActiveSession"] = DateTime.Now.ToString();

            ViewData["articulosventa"] = Bll.BllBest.DameInstancia().listaArticulosVentaHome(0,0,0);
        
            return View();



        }
        
    

  

        public void mio() {
            var client = new RestClient("https://api.mercadolibre.com/items?access_token=APP_USR-6240899172095397-050915-31d1ac473d78b363db6ae7e3ddc6f248-79043161");
            var request = new RestRequest();
            string body = "{   'title': 'Item de teste',     'category_id': 'MLA91727',     'price': 1200,    'currency_id': 'ARS',    'available_quantity': 2,    'buying_mode': 'buy_it_now',    'listing_type_id': 'bronze', "+
   " 'condition': 'new',    'description': 'test',  'pictures': [ {   'source': 'http://upload.wikimedia.org/wikipedia/commons/f/fd/Ray_Ban_Original_Wayfarer.jpg'        },"+
      "  {        'source': 'http://en.wikipedia.org/wiki/File:Teashades.gif'   }   ],  'shipping': {    'mode': 'me2',  'local_pick_up': false,   'free_shipping': false,"+
  " 'free_methods': [] } } ";
            request.Method = Method.POST;
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", body, ParameterType.RequestBody);
          

            var response = client.Execute(request);
            var content = response.Content; // raw content as string  }

        }
    }
}