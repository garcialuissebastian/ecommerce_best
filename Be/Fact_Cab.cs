using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
  public  class Fact_Cab
    {

      public Int32 id { get; set; }
      public Int32 CantReg { get; set; }
      public Int32 CbteTipo { get; set; }

      public Int32 PtoVta { get; set; }

      public DateTime aud { get; set; }
      public string Usuario { get; set; }
      public string Resultado { get; set; }
      public string MODO { get; set; }

      List<Fact_detalle> Item1 = new List<Fact_detalle>();
      public List<Fact_detalle> Detalle
      {
          get { return Item1; }
          set { Item1 = value; }

      }
      List<Fact_Error> Item2 = new List<Fact_Error>();
      public List<Fact_Error> Error
      {
          get { return Item2; }
          set { Item2 = value; }

      }
    }
}
