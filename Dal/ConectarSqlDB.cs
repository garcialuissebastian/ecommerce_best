using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
 
namespace Dal
{
    public class ConectarSqlDB
    {
        
        internal ConectarSqlDB()
        {
        }

        public SqlConnection CrearNuevaConexion()
        {
            string connString;
            //obtener la cadena de conexion del archivo de configuracion de la aplicacion (App.config o Web.config)
  // connString = "Data Source=SEBASTIAN\\MSSQLSERVER2;Initial Catalog=pp;Integrated Security=True";

         connString =ConfigurationManager.AppSettings.Get("sql2018").ToString(); 

   // connString = "Data Source=.;Initial Catalog=pp;Integrated Security=True";
 
            SqlConnection conexion = new SqlConnection(connString);
            return conexion;
        }

        public SqlCommand CrearNuevoComando(string textoComando, SqlConnection conexion, string v_type)
        {
            if (conexion == null)
            {
                conexion = this.CrearNuevaConexion();
            }

            SqlCommand comando = new SqlCommand(textoComando, conexion);
            if (v_type=="SP") {
                comando.CommandType = CommandType.Text;
                comando.CommandType = CommandType.StoredProcedure;

            
            
            
            }



            return comando;
        }

        public  SqlCommand CrearNuevoComando(string textoComando)
        {
            return this.CrearNuevoComando(textoComando, null, null);
        }

        public SqlCommand CrearNuevoComandoType(string textoComando, string v_type)
        {
            return this.CrearNuevoComando(textoComando, null, v_type);
        }

       
        public SqlParameter AgregarParametroAComando(SqlCommand comando, string nombreParametro, object valorParametro)
        {
            return ((SqlCommand)comando).Parameters.AddWithValue(nombreParametro, valorParametro);
        }


     
        
        public System.Data.DataTable ExecuteDataTable(SqlCommand comando, string nombreTabla)
        {
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable(nombreTabla);
            adaptador.Fill(tabla);
            return tabla;
        }

        public int ExecuteNonQuery(SqlCommand comando)
        {
            this.VerificarConexion(comando);

            return comando.ExecuteNonQuery();
        }

        public SqlDataReader ExecuteReader(SqlCommand comando)
        {
            this.VerificarConexion(comando, true);

            return comando.ExecuteReader();
        }

        private void VerificarConexion(SqlCommand comando)
        {
            this.VerificarConexion(comando, false);
        }

        private void VerificarConexion(SqlCommand comando, bool forzarNueva)
        {
            if (comando.Connection == null )
            {
                comando.Connection = this.CrearNuevaConexion();
            }

            //if (comando.Connection == null || forzarNueva)
            //{
            //    comando.Connection = this.CrearNuevaConexion();
            //}

            if (comando.Connection.State != ConnectionState.Open)
            {
                comando.Connection.Open();
            }
        }

        public void Close(SqlCommand comando)
        {


            if (comando  != null)
            {
                if (comando.Connection != null)
                {
                    if (comando.Connection.State != ConnectionState.Closed)
                    {
                        comando.Connection.Close();
                    }
                }
            }


        }
  








    }
}
