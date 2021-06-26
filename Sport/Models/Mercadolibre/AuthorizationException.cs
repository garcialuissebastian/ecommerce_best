using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sport.Models.Mercadolibre
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException()
        {
        }

        public AuthorizationException(string msg, Exception ex) : base(msg, ex)
        {
        }
    }
}