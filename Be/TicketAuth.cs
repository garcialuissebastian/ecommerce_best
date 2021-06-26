using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
  public  class TicketAuth
    {

        public string Token { get; set; }
        public string Sign { get; set; }
        public long Cuit { get; set; }
    }
}
