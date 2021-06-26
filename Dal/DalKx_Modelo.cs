using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Be;
using MySql.Data.MySqlClient;
namespace Dal
{
    public class DalKx_Modelo
    {

        private MySqlConectarSqlDBVarias cnn = new MySqlConectarSqlDBVarias("kardex");


        MySqlCommand cmm;
        public List<Kx_Modelo> ListarSub(string id_sub)
        {

            List<Kx_Modelo> lista = new List<Kx_Modelo>();
            try
            {



                string cmdTxt = " select Id,Nombre, Id_Marca, Anulado from Kx_Modelo where Id_Marca ='" + id_sub + "'";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

               MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    Kx_Modelo entidad = new Kx_Modelo();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.Anulado = DalModelo.VerifStringMysql(lector, "Anulado");
                    entidad.Id_Marca = DalModelo.VerifStringMysql(lector, "Id_Marca");
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
                cnn.Close(cmm);
            }


            return lista;
        }

        public void Alta(Kx_Modelo v_obj)
        {
            try
            {
                string cmdTxt = " insert into Kx_Modelo (Nombre, Id_Marca, Anulado, Id_Usuario) values ('" + v_obj.Nombre + "', '" + v_obj.Id_Marca + "', '" + v_obj.Anulado + "', '" + v_obj.Id_Usuario + "')  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                cnn.ExecuteNonQuery(cmm);

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


        public void Modificacion(Kx_Modelo v_obj)
        {
            try
            {
                string cmdTxt = "update Kx_Modelo set Nombre='" + v_obj.Nombre + "'   , Anulado='" + v_obj.Anulado + "' where id ='" + v_obj.Id + "'   ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                cnn.ExecuteNonQuery(cmm);

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

        public List<Kx_Modelo> Listar()
        {

            List<Kx_Modelo> lista = new List<Kx_Modelo>();
            try
            {



                string cmdTxt = " select Id,Nombre, Id_Marca, Anulado from Kx_Modelo  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    Kx_Modelo entidad = new Kx_Modelo();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.Id_Marca = DalModelo.VerifStringMysql(lector, "Id_Marca");
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
                cnn.Close(cmm);
            }


            return lista;
        }


        public void Eliminar(Int32 v_id)
        {
            try
            {
                string cmdTxt = "delete from Kx_Modelo  where Id='" + v_id + "'";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                cnn.ExecuteNonQuery(cmm);
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

        public Int32 maxId()
        {
            Int32 id = 0;

            try
            {
                string cmdTxt = " select max(Id) as cant from Kx_Modelo  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

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
                cnn.Close(cmm);
            }

            return id;
        }

    }
}
