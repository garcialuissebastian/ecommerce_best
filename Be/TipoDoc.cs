using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
  public  class TipoDoc
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}
