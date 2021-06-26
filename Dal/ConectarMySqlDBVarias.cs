using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Dal
{
    public class MySqlConectarSqlDBVarias

    {

        internal MySqlConectarSqlDBVarias()
        {
        }
         string connString;
            //obtener la cadena de conexion del archivo de configuracion de la aplicacion (App.config o Web.config)
            
        internal MySqlConectarSqlDBVarias(string v_db)
        {
            

            if (v_db == "bestsport")
            {

                connString = "Server=localhost;Database=best; Uid=w230176_**;Pwd=****;check parameters=false";
            }
            if (v_db == "best")
            {
            //  string StrCnn = ConfigurationManager.AppSettings.Get("StrCnn").ToString();
                connString = "Server=localhost;Database=best; Uid=w230176_best;Pwd=***;check parameters=false";

               // connString = StrCnn;
            }
           
            
        }
        public MySqlConnection MySqlCrearNuevaConexion()
        {
           

 
            //declarar e instanciar una nueva instancia de un objeto del tipo MySqlConnection
            MySqlConnection conexion = new MySqlConnection(connString);
            return conexion;
        }

        public MySqlCommand MySqlCrearNuevoComando(string textoComando, MySqlConnection conexion, string v_type)
        {
            if (conexion == null)
            {
                conexion = this.MySqlCrearNuevaConexion();
            }
            
            MySqlCommand comando = new MySqlCommand(textoComando, conexion);
            if (v_type=="SP") {
                comando.CommandType = CommandType.Text;
                comando.CommandType = CommandType.StoredProcedure;

            
            
            
            }



            return comando;
        }

        public MySqlCommand MySqlCrearNuevoComando(string textoComando)
        {
            return this.MySqlCrearNuevoComando(textoComando, null, null);
        }

        public MySqlCommand MySqlCrearNuevoComandoType(string textoComando, string v_type)
        {
            return this.MySqlCrearNuevoComando(textoComando, null, v_type);
        }

       
        public MySqlParameter AgregarParametroAComando(MySqlCommand comando, string nombreParametro, object valorParametro)
        {
            return ((MySqlCommand)comando).Parameters.AddWithValue(nombreParametro, valorParametro);
        }

        public MySqlParameter AgregarParametroAComandoOut(MySqlCommand comando, string nombreParametro)
        {
            MySqlParameter outPutParameter = new MySqlParameter();
            outPutParameter.ParameterName = nombreParametro;
            outPutParameter.MySqlDbType = MySqlDbType.VarChar;
            outPutParameter.Size = 500;
            outPutParameter.Direction = System.Data.ParameterDirection.Output;
            ((MySqlCommand)comando).Parameters.Add(outPutParameter);

            return outPutParameter;
        }
     
        
        public System.Data.DataTable ExecuteDataTable(MySqlCommand comando, string nombreTabla)
        {
            MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
            DataTable tabla = new DataTable(nombreTabla);
            adaptador.Fill(tabla);
            return tabla;
        }

        public int ExecuteNonQuery(MySqlCommand comando)
        {
            this.VerificarConexion(comando);

            return comando.ExecuteNonQuery();
        }

        public MySqlDataReader ExecuteReader(MySqlCommand comando)
        {
            this.VerificarConexion(comando, true);

            return comando.ExecuteReader();
        }

        private void VerificarConexion(MySqlCommand comando)
        {
            this.VerificarConexion(comando, false);
        }

        private void VerificarConexion(MySqlCommand comando, bool forzarNueva)
        {
            if (comando.Connection == null || forzarNueva)
            {
                comando.Connection = this.MySqlCrearNuevaConexion();
            }

            if (comando.Connection.State != ConnectionState.Open)
            {
                comando.Connection.Open();
            }
        }

        public void Close(MySqlCommand comando)
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
