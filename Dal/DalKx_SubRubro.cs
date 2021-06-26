using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Be;
using MySql.Data.MySqlClient;
namespace Dal
{
    public class DalKx_SubRubro
    {

        private MySqlConectarSqlDBVarias cnn = new MySqlConectarSqlDBVarias("kardex");


        MySqlCommand cmm;
        public List<Kx_SubRubro> ListarSub(string id_sub)
        {

            List<Kx_SubRubro> lista = new List<Kx_SubRubro>();
            try
            {



                string cmdTxt = " select Id,NombreSubRubro, Id_Rubro, Anulado from Kx_SubRubro where Id_Rubro ='" + id_sub + "'";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

               MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    Kx_SubRubro entidad = new Kx_SubRubro();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.NombreSubRubro = DalModelo.VerifStringMysql(lector, "NombreSubRubro");
                    entidad.Anulado = DalModelo.VerifStringMysql(lector, "Anulado");
                    entidad.Id_Rubro = DalModelo.VerifStringMysql(lector, "Id_Rubro");
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

        public void Alta(Kx_SubRubro v_obj)
        {
            try
            {
                string cmdTxt = " insert into Kx_SubRubro (NombreSubRubro, Id_Rubro, Anulado,  Id_Usuario) values ('" + v_obj.NombreSubRubro + "', '" + v_obj.Id_Rubro + "', '" + v_obj.Anulado + "', '" + v_obj.Id_Usuario + "')  ";

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


        public void Modificacion(Kx_SubRubro v_obj)
        {
            try
            {
                string cmdTxt = "update Kx_SubRubro set NombreSubRubro='" + v_obj.NombreSubRubro + "'   , Anulado='" + v_obj.Anulado + "' where id ='" + v_obj.Id + "'   ";

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

        public List<Kx_SubRubro> Listar()
        {

            List<Kx_SubRubro> lista = new List<Kx_SubRubro>();
            try
            {



                string cmdTxt = " select Id,NombreSubRubro, Id_Rubro, Anulado from Kx_SubRubro  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    Kx_SubRubro entidad = new Kx_SubRubro();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.NombreSubRubro = DalModelo.VerifStringMysql(lector, "NombreSubRubro");
                    entidad.Id_Rubro = DalModelo.VerifStringMysql(lector, "Id_Rubro");
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
                string cmdTxt = "delete from Kx_SubRubro  where Id='" + v_id + "'";

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
                string cmdTxt = " select max(Id) as cant from Kx_SubRubro  ";

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
