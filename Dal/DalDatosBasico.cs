using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Be;
namespace Dal
{
   public class DalDatosBasico
    {

        private MySqlConectarSqlDBVarias cnn = new MySqlConectarSqlDBVarias("kardex");
        MySqlCommand cmm;
        public List<pais> ListarPais()
        {

            List<pais> lista = new List<pais>();
            try
            {



                string cmdTxt = " select  Id, nombre, anulado from pais  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    pais entidad = new pais();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
               
                    entidad.nombre = DalModelo.VerifStringMysql(lector, "nombre");
                    entidad.anulado = DalModelo.VerifStringMysql(lector, "anulado");
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
        public List<provincia> ListarProvincias(string id)
        {

            List<provincia> lista = new List<provincia>();
            try
            {
                string cmdTxt="";
               

                 cmdTxt = " select Id,  Provincia, anulado, id_pais from provincia where id_pais='"+id+"' ";
                 if (id=="all")
                 {
                 cmdTxt = " select Id,  Provincia, anulado, id_pais from provincia  ";
                 }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    provincia entidad = new provincia();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                 
                    entidad.Provincia = DalModelo.VerifStringMysql(lector, "Provincia");
                    entidad.anulado = DalModelo.VerifStringMysql(lector, "anulado");
                    entidad.id_pais = DalModelo.VerifStringMysql(lector, "id_pais");
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

        public List<departamento> ListarDepartamento(string id)
        {

            List<departamento> lista = new List<departamento>();
            try
            {



                string cmdTxt = " select Id,NombreDepartamento, CodProvincia, anulado, cod_postal from departamento where CodProvincia ='" + id + "' ";
                if (id == "all")
                {
                    cmdTxt = "select Id,NombreDepartamento, CodProvincia, anulado, cod_postal from departamento   ";
                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    departamento entidad = new departamento();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.NombreDepartamento = DalModelo.VerifStringMysql(lector, "NombreDepartamento");
                    entidad.CodProvincia = DalModelo.VerifStringMysql(lector, "CodProvincia");
                    entidad.anulado = DalModelo.VerifStringMysql(lector, "anulado");
                    entidad.cod_postal = DalModelo.VerifStringMysql(lector, "cod_postal");
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

        public List<distrito> ListarDistrito(string id)
        {

            List<distrito> lista = new List<distrito>();
            try
            {



                string cmdTxt = " select Id,IDDepartamento, NombreDistrito, anulado, cod_postal from   distrito where IDDepartamento ='" + id + "' ";
                if (id == "all")
                {
                    cmdTxt = " select Id,IDDepartamento, NombreDistrito, anulado, cod_postal from   distrito   ";
                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    distrito entidad = new distrito();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.IDDepartamento = DalModelo.VerifStringMysql(lector, "IDDepartamento");
                    entidad.NombreDistrito = DalModelo.VerifStringMysql(lector, "NombreDistrito");
                    entidad.anulado = DalModelo.VerifStringMysql(lector, "anulado");
                    entidad.cod_postal = DalModelo.VerifStringMysql(lector, "cod_postal");
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
   
   }
}
