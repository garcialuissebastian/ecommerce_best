using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Be;
using MySql.Data.MySqlClient;
namespace Dal
{
    public class DalKx_Marca
    {

        private ConectarSqlDB cnn = new ConectarSqlDB();

#pragma warning disable CS0649 // El campo 'DalKx_Marca.cmm' nunca se asigna y siempre tendrá el valor predeterminado null
        SqlCommand cmm;
#pragma warning restore CS0649 // El campo 'DalKx_Marca.cmm' nunca se asigna y siempre tendrá el valor predeterminado null

        private MySqlConectarSqlDBVarias cnn2 = new MySqlConectarSqlDBVarias("kardex");


        MySqlCommand cmm2;
        public void Alta(Be.Kx_Marca v_obj)
        {
            try
            {
                string cmdTxt = " insert into Kx_Marca (Nombre, Anulado, Id_Usuario) values ('" + v_obj.Nombre + "', '" + v_obj.Anulado + "', '" + v_obj.Id_Usuario + "')  ";

                cmm2 = cnn2.MySqlCrearNuevoComando(cmdTxt);

                cnn2.ExecuteNonQuery(cmm2);

            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close(cmm2);
            }

        }


        public void Modificacion(Kx_Marca v_obj)
        {
            try
            {
                string cmdTxt = "update Kx_Marca set Nombre='" + v_obj.Nombre+ "' , Anulado='" + v_obj.Anulado + "'  where id ='" + v_obj.Id+ "'   ";

                cmm2 = cnn2.MySqlCrearNuevoComando(cmdTxt);

                cnn2.ExecuteNonQuery(cmm2);

            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }

        }

        public List<Kx_Marca> Listar(string v_tipo, string v_valor, string v_user)
        {

            List<Kx_Marca> lista = new List<Kx_Marca>();
            try
            {
                string cmdTxt = " select Id,NombreRubro, Anulado, tipo_rubro from Kx_Marca where Id_Usuario= '" + v_user + "' ";
                if (v_tipo == "0")
                {
                    cmdTxt = " select Id,Nombre, Anulado, tipo_rubro from Kx_Marca where Nombre like '" + v_valor + "%' and  Id_Usuario= '" + v_user + "'  ";

                }

                cmm2 = cnn2.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn2.ExecuteReader(cmm2);

                while (lector.Read())
                {

                    Kx_Marca entidad = new Kx_Marca();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.Anulado = DalModelo.VerifStringMysql(lector, "Anulado");
              
                    lista.Add(entidad);

                }

                lector.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close(cmm2);
            }


            return lista;
        }
    
        public List<Kx_Marca> Listar(string user)
        {

            List<Kx_Marca> lista = new List<Kx_Marca>();
            try
            {


                string cmdTxt = " select Id,Nombre, Anulado from Kx_Marca where Id_Usuario = '" + user + "' ";

                cmm2 = cnn2.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn2.ExecuteReader(cmm2);

                while (lector.Read())
                {

                    Kx_Marca entidad = new Kx_Marca();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.Anulado = DalModelo.VerifStringMysql(lector, "Anulado");
               
                    lista.Add(entidad);

                }

                lector.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close(cmm2);
            }


            return lista;
        }


        public void Eliminar(Int32 v_id)
        {
            try
            {
                string cmdTxt = "delete from Kx_Marca  where Id='" + v_id + "'";

                cmm2 = cnn2.MySqlCrearNuevoComando(cmdTxt);

                cnn2.ExecuteNonQuery(cmm2);
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close(cmm2);
            }

        }

        public Int32 maxId()
        {
            Int32 id = 0;

            try
            {
                string cmdTxt = " select max(Id) as cant from Kx_Marca  ";

                cmm2 = cnn2.MySqlCrearNuevoComando(cmdTxt);

               MySqlDataReader lector = cnn2.ExecuteReader(cmm2);

                while (lector.Read())
                {
                    id = DalModelo.VeriIntMysql(lector, "cant");
                }

                lector.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close(cmm2);
            }

            return id;
        }

    }

}
