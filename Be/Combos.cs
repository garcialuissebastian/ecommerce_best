using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
  public  class Combos
    {
        public string valor { get; set; }
        public string descripcion { get; set; }
    }

  public class Modelo
  {

      //string MiPage = "MiPagina.aspx";
      //string Master = "Main_";
      //string TipoBase = "mysql";



      private string _tabla = "hosp_dias"; // ej tabla

      public string tabla
      {
          get { return _tabla; }
          set { _tabla = value; }
      }

      private string _base = "ORL"; // ej tabla

      public string base_datos
      {
          get { return _base; }
          set { _base = value; }
      }

      private string _MiPage = "MiPagina.aspx";

      public string MiPage
      {
          get { return _MiPage; }
          set { _MiPage = value; }
      }
      private string _Master = "Main_";

      public string Master
      {
          get { return _Master; }
          set { _Master = value; }
      }
      private TipoBase _TipoBase = TipoBase.mysql;
     
      public TipoBase TipoBase
      {
          get { return _TipoBase; }
          set { _TipoBase = value; }
      }


  } 
        public enum TipoBase
        {
            mysql,
            oracle,
            sqlserver
             
        };

     

}
