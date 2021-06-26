using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;
using System.Data;
using System.ComponentModel;
namespace Bll
{
    public class exel
    {
        public static System.Data.DataTable ConvertToDataTable<T>(IList<T> data)
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

        public static System.Data.DataTable ConvertToDataTable<T>(IList<T> data, List<string> listNo)
        {
            PropertyDescriptorCollection properties =   TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
            {
                string s = "n";
                foreach (var item in listNo)
                {
                    if (prop.Name == item)
                    {
                        s = "s";
                    }
                }
                if (s == "n")
                {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
            }
            
            foreach (T item in data)
            {
                string s = "n";
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                  s = "n";
                    foreach (var item2 in listNo)
                    {
                        if (prop.Name == item2)
                        {
                            s = "s";
                        }
                    }
                    if (s == "n")
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                }
 
                    table.Rows.Add(row);
                
            }
            return table;

        }

  
    
    
    }
}