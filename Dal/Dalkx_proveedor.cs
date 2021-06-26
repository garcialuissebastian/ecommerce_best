using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Be;
using MySql.Data.MySqlClient;
namespace Dal
{
    public class Dalkx_proveedor
    {
        private MySqlConectarSqlDBVarias cnn = new MySqlConectarSqlDBVarias("kardex");


        MySqlCommand cmm;

        public void Alta(kx_proveedor v_obj)
        {
            try
            {
                string cmdTxt = " insert into kx_proveedor (Cod_Manual, Cuit, Iva, Tipo, Denominacion, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web, Id_Usuario) values ('" + v_obj.Cod_Manual + "', '" + v_obj.Cuit + "', '" + v_obj.Iva + "', '" + v_obj.Tipo + "', '" + v_obj.Denominacion + "', '" + v_obj.Observacion + "', '" + v_obj.Cta_contable + "', '" + v_obj.Calle + "', '" + v_obj.Numero + "', '" + v_obj.Dpto + "', '" + v_obj.Pais + "', '" + v_obj.Cp + "', '" + v_obj.Provincia + "', '" + v_obj.Departamento + "', '" + v_obj.Distrito + "', '" + v_obj.Contac_Nomb + "', '" + v_obj.Contac_Tel + "', '" + v_obj.Contac_Cel + "', '" + v_obj.Contac_Mail + "', '" + v_obj.Emp_Tel + "', '" + v_obj.Emp_Cel + "', '" + v_obj.Emp_Mail + "', '" + v_obj.Emp_Web + "', '" + v_obj.Id_Usuario + "')  ";

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
        public List<kx_proveedor> Listar()
        {

            List<kx_proveedor> lista = new List<kx_proveedor>();
            try
            {



                string cmdTxt = " select Id,Cod_Manual, Cuit, Iva, Tipo, Denominacion, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web from kx_proveedor  ";

 
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    kx_proveedor entidad = new kx_proveedor();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Cod_Manual = DalModelo.VerifStringMysql(lector, "Cod_Manual");
                    entidad.Cuit = DalModelo.VerifStringMysql(lector, "Cuit");
                    entidad.Iva = DalModelo.VerifStringMysql(lector, "Iva");
                    entidad.Tipo = DalModelo.VerifStringMysql(lector, "Tipo");
                    entidad.Denominacion = DalModelo.VerifStringMysql(lector, "Denominacion");
                    entidad.Observacion = DalModelo.VerifStringMysql(lector, "Observacion");
                    entidad.Cta_contable = DalModelo.VerifStringMysql(lector, "Cta_contable");
                    entidad.Calle = DalModelo.VerifStringMysql(lector, "Calle");
                    entidad.Numero = DalModelo.VerifStringMysql(lector, "Numero");
                    entidad.Dpto = DalModelo.VerifStringMysql(lector, "Dpto");
                    entidad.Pais = DalModelo.VerifStringMysql(lector, "Pais");
                    entidad.Cp = DalModelo.VerifStringMysql(lector, "Cp");
                    entidad.Provincia = DalModelo.VerifStringMysql(lector, "Provincia");
                    entidad.Departamento = DalModelo.VerifStringMysql(lector, "Departamento");
                    entidad.Distrito = DalModelo.VerifStringMysql(lector, "Distrito");
                    entidad.Contac_Nomb = DalModelo.VerifStringMysql(lector, "Contac_Nomb");
                    entidad.Contac_Tel = DalModelo.VerifStringMysql(lector, "Contac_Tel");
                    entidad.Contac_Cel = DalModelo.VerifStringMysql(lector, "Contac_Cel");
                    entidad.Contac_Mail = DalModelo.VerifStringMysql(lector, "Contac_Mail");
                    entidad.Emp_Tel = DalModelo.VerifStringMysql(lector, "Emp_Tel");
                    entidad.Emp_Cel = DalModelo.VerifStringMysql(lector, "Emp_Cel");
                    entidad.Emp_Mail = DalModelo.VerifStringMysql(lector, "Emp_Mail");
                    entidad.Emp_Web = DalModelo.VerifStringMysql(lector, "Emp_Web");
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

         
        public List<kx_proveedor> Listar(string v_tipo, string v_valor, string user)
        {

            List<kx_proveedor> lista = new List<kx_proveedor>();
            try
            {
                string cmdTxt = " select Id,Cod_Manual, Cuit, Iva, Tipo, Denominacion, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web from kx_proveedor where Id_Usuario ='" + user + "'  AND Habilitado IS NULL ";

                if (v_tipo == "0")
                {
                    cmdTxt = "select Id,Cod_Manual, Cuit, Iva, Tipo, Denominacion, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web from kx_proveedor where Denominacion like  '" + v_valor + "%'  and Id_Usuario ='" + user + "'  AND Habilitado IS NULL ";

                }
                if (v_tipo == "1")
                {
                    cmdTxt = "select Id,Cod_Manual, Cuit, Iva, Tipo, Denominacion, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web from kx_proveedor where Id like  '" + v_valor + "%'  and Id_Usuario ='" + user + "'  AND Habilitado IS NULL ";

                }
                if (v_tipo == "2")
                {
                    cmdTxt = "select Id,Cod_Manual, Cuit, Iva, Tipo, Denominacion, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web from kx_proveedor where Cod_Manual like  '" + v_valor + "%'  and Id_Usuario ='" + user + "'  AND Habilitado IS NULL ";

                }
                if (v_tipo == "3")
                {
                    cmdTxt = "select Id,Cod_Manual, Cuit, Iva, Tipo, Denominacion, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web from kx_proveedor where Cuit like  '" + v_valor + "%' and Id_Usuario ='" + user + "'  AND Habilitado IS NULL ";

                }


                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    kx_proveedor entidad = new kx_proveedor();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Cod_Manual = DalModelo.VerifStringMysql(lector, "Cod_Manual");
                    entidad.Cuit = DalModelo.VerifStringMysql(lector, "Cuit");
                    entidad.Iva = DalModelo.VerifStringMysql(lector, "Iva");
                    entidad.Tipo = DalModelo.VerifStringMysql(lector, "Tipo");
                    entidad.Denominacion = DalModelo.VerifStringMysql(lector, "Denominacion");
                    entidad.Observacion = DalModelo.VerifStringMysql(lector, "Observacion");
                    entidad.Cta_contable = DalModelo.VerifStringMysql(lector, "Cta_contable");
                    entidad.Calle = DalModelo.VerifStringMysql(lector, "Calle");
                    entidad.Numero = DalModelo.VerifStringMysql(lector, "Numero");
                    entidad.Dpto = DalModelo.VerifStringMysql(lector, "Dpto");
                    entidad.Pais = DalModelo.VerifStringMysql(lector, "Pais");
                    entidad.Cp = DalModelo.VerifStringMysql(lector, "Cp");
                    entidad.Provincia = DalModelo.VerifStringMysql(lector, "Provincia");
                    entidad.Departamento = DalModelo.VerifStringMysql(lector, "Departamento");
                    entidad.Distrito = DalModelo.VerifStringMysql(lector, "Distrito");
                    entidad.Contac_Nomb = DalModelo.VerifStringMysql(lector, "Contac_Nomb");
                    entidad.Contac_Tel = DalModelo.VerifStringMysql(lector, "Contac_Tel");
                    entidad.Contac_Cel = DalModelo.VerifStringMysql(lector, "Contac_Cel");
                    entidad.Contac_Mail = DalModelo.VerifStringMysql(lector, "Contac_Mail");
                    entidad.Emp_Tel = DalModelo.VerifStringMysql(lector, "Emp_Tel");
                    entidad.Emp_Cel = DalModelo.VerifStringMysql(lector, "Emp_Cel");
                    entidad.Emp_Mail = DalModelo.VerifStringMysql(lector, "Emp_Mail");
                    entidad.Emp_Web = DalModelo.VerifStringMysql(lector, "Emp_Web");
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
    
        public void Modificacion(kx_proveedor v_obj)
        {
            try
            {
                string cmdTxt = "update kx_proveedor set Cod_Manual='" + v_obj.Cod_Manual + "' , Cuit='" + v_obj.Cuit + "' , Iva='" + v_obj.Iva + "' , Tipo='" + v_obj.Tipo + "' , Denominacion='" + v_obj.Denominacion + "' , Observacion='" + v_obj.Observacion + "' , Cta_contable='" + v_obj.Cta_contable + "' , Calle='" + v_obj.Calle + "' , Numero='" + v_obj.Numero + "' , Dpto='" + v_obj.Dpto + "' , Pais='" + v_obj.Pais + "' , Cp='" + v_obj.Cp + "' , Provincia='" + v_obj.Provincia + "' , Departamento='" + v_obj.Departamento + "' , Distrito='" + v_obj.Distrito + "' , Contac_Nomb='" + v_obj.Contac_Nomb + "' , Contac_Tel='" + v_obj.Contac_Tel + "' , Contac_Cel='" + v_obj.Contac_Cel + "' , Contac_Mail='" + v_obj.Contac_Mail + "' , Emp_Tel='" + v_obj.Emp_Tel + "' , Emp_Cel='" + v_obj.Emp_Cel + "' , Emp_Mail='" + v_obj.Emp_Mail + "' , Emp_Web='" + v_obj.Emp_Web + "' where  id ='" + v_obj.Id + "'   ";

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

   

        public void Eliminar(Int32 v_id)
        {
            try
            {
               // string cmdTxt = "delete from kx_proveedor  where Id='" + v_id + "'   ";

                string cmdTxt = "update kx_proveedor  set Habilitado ='N' where Id='" + v_id + "'   ";

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
                string cmdTxt = " select max(Id) as cant from kx_proveedor  ";

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
