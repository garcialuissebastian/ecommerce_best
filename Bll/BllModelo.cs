using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Be;
using Dal;
using System.Data;
using System.ComponentModel;

namespace Bll
{
  public  class BllModelo
    {
      private  Dal.DalModelo _mapeador;
 
          public BllModelo()
          {
              _mapeador = new Dal.DalModelo();
          }

          private static BllModelo instancia = null;

          public static BllModelo DameInstancia()
          {

              if (instancia == null)
              {

                  return new BllModelo();
              }
              else
              {
                  return instancia;
              }

          }
        public void bitacora(Be.Bitacora bi)
          {
              try
              {
                  this._mapeador.bitacora(bi);
              }
              catch (Exception)
              {

                  throw;
              }

          }
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        public string Ver_modeloMsql(  Be.Modelo obj_mod)
          {
              try
              {
                  return this._mapeador.Ver_modeloMsql(  obj_mod);
              }
              catch (Exception)
              {

                  throw;
              }

          }

          public void Salida(string v_sal)
          {
              try
              {
                  this._mapeador.salida(v_sal);
              }
              catch (Exception)
              {
                  throw;
              }

          }
          public string Ver_modelo(string v_tabla)
          {
              try
              {
                return  this._mapeador.Ver_modelo( v_tabla);
              }
              catch (Exception)
              {
                  throw;
              }
          
          
          
          }
 
       
    }
}
