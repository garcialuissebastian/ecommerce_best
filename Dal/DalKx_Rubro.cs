using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Be;
using MySql.Data.MySqlClient;
namespace Dal
{
    public class DalKx_Rubro
    {

        private ConectarSqlDB cnn = new ConectarSqlDB();

#pragma warning disable CS0649 // El campo 'DalKx_Rubro.cmm' nunca se asigna y siempre tendrá el valor predeterminado null
        SqlCommand cmm;
#pragma warning restore CS0649 // El campo 'DalKx_Rubro.cmm' nunca se asigna y siempre tendrá el valor predeterminado null

        private MySqlConectarSqlDBVarias cnn2 = new MySqlConectarSqlDBVarias("kardex");


        MySqlCommand cmm2;
        public void Alta(Kx_Rubro v_obj)
        {
            try
            {
                string cmdTxt = " insert into Kx_Rubro (NombreRubro, Anulado, tipo_rubro, Id_Usuario) values ('" + v_obj.NombreRubro + "', '" + v_obj.Anulado + "', '" + v_obj.tipo_rubro + "', '" + v_obj.Id_Usuario + "')  ";

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


        public void Modificacion(Kx_Rubro v_obj)
        {
            try
            {
                string cmdTxt = "update Kx_Rubro set NombreRubro='" + v_obj.NombreRubro + "' , Anulado='" + v_obj.Anulado + "' , tipo_rubro='" + v_obj.tipo_rubro + "' where id ='" + v_obj.Id+ "'   ";

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

        public List<Kx_Rubro> Listar(string v_tipo, string v_valor, string v_user)
        {

            List<Kx_Rubro> lista = new List<Kx_Rubro>();
            try
            {
                string cmdTxt = " select Id,NombreRubro, Anulado, tipo_rubro from Kx_Rubro where Id_Usuario = '" + v_user + "' and Anulado ='NO' ";
                if (v_tipo == "0")
                {
                    cmdTxt = " select Id,NombreRubro, Anulado, tipo_rubro from Kx_Rubro where NombreRubro like '" + v_valor + "%'  AND Id_Usuario = '" + v_user + "' and Anulado ='NO' ";

                }

                cmm2 = cnn2.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn2.ExecuteReader(cmm2);

                while (lector.Read())
                {

                    Kx_Rubro entidad = new Kx_Rubro();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.NombreRubro = DalModelo.VerifStringMysql(lector, "NombreRubro");
                    entidad.Anulado = DalModelo.VerifStringMysql(lector, "Anulado");
                    entidad.tipo_rubro = DalModelo.VerifStringMysql(lector, "tipo_rubro");
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
    
        public List<Kx_Rubro> Listar(string user)
        {

            List<Kx_Rubro> lista = new List<Kx_Rubro>();
            try
            {



                string cmdTxt = " select Id,NombreRubro, Anulado, tipo_rubro from Kx_Rubro where Id_Usuario ='" + user + "'  ";

                cmm2 = cnn2.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn2.ExecuteReader(cmm2);

                while (lector.Read())
                {

                    Kx_Rubro entidad = new Kx_Rubro();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.NombreRubro = DalModelo.VerifStringMysql(lector, "NombreRubro");
                    entidad.Anulado = DalModelo.VerifStringMysql(lector, "Anulado");
                    entidad.tipo_rubro = DalModelo.VerifStringMysql(lector, "tipo_rubro");
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
                string cmdTxt = "delete from Kx_Rubro  where Id='" + v_id + "'";

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
                string cmdTxt = " select max(Id) as cant from Kx_Rubro  ";

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
