using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sport.Views
{
    public class LogCustomExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {

           


                var exceptionMessage = filterContext.Exception.Message;
                var stackTrace = filterContext.Exception.StackTrace;
                var controllerName = filterContext.RouteData.Values["controller"].ToString();
                var actionName = filterContext.RouteData.Values["action"].ToString();

                string Message = Environment.NewLine +  "Date : " + DateTime.Now.ToString() + ", Controller: " + controllerName + ", Action: " + actionName +
                                 ",  Error Message : " + exceptionMessage;
                filterContext.HttpContext.Session["error"] = Message;
                Message += Environment.NewLine + "Stack Trace : " + stackTrace + Environment.NewLine +"------------------------------------------------------------------------- FIN";
                
                File.AppendAllText(HttpContext.Current.Server.MapPath("~/Log/Log.txt"), Message);

                //filterContext.RouteData.DataTokens["area"] = "Public";
                filterContext.RouteData.Values["controller"] = "Error";
                filterContext.RouteData.Values["action"] = "Error";

               
                filterContext.ExceptionHandled = true;
               
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error" 
                  
            };
            }
        }
    }
}