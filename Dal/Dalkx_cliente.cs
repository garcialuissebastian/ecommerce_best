using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Be;
using MySql.Data.MySqlClient;

namespace Dal
{
    public class Dalkx_cliente
    {
 

         

        private MySqlConectarSqlDBVarias cnn = new MySqlConectarSqlDBVarias("kardex");


        MySqlCommand cmm;

        public Int32 WsExisteDoc(string doc, string user)
        {
            Int32 sal = 0;

            try
            {
                string cmdTxt = "SELECT count(*) cant FROM kx_cliente WHERE doc_no = '" + doc + "' and Id_Usuario= '" + user + "'";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {
                    sal = DalModelo.VeriIntMysql(lector, "cant");

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


            return sal;
        }


        public void Alta(kx_cliente v_obj)
        {
            try
            {
                string cmdTxt = " insert into kx_cliente (Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web, Id_Usuario,ListaPrecio) values ('" + v_obj.Cod_Manual + "', '" + v_obj.Tipo_Doc + "', '" + v_obj.Doc_No + "', '" + v_obj.Iva + "', '" + v_obj.Nombre + "', '" + v_obj.Apellido + "', '" + v_obj.Observacion + "', '" + v_obj.Cta_contable + "', '" + v_obj.Calle + "', '" + v_obj.Numero + "', '" + v_obj.Dpto + "', '" + v_obj.Pais + "', '" + v_obj.Cp + "', '" + v_obj.Provincia + "', '" + v_obj.Departamento + "', '" + v_obj.Distrito + "', '" + v_obj.Contac_Nomb + "', '" + v_obj.Contac_Tel + "', '" + v_obj.Contac_Cel + "', '" + v_obj.Contac_Mail + "', '" + v_obj.Emp_Tel + "', '" + v_obj.Emp_Cel + "', '" + v_obj.Emp_Mail + "', '" + v_obj.Emp_Web + "' , '" + v_obj.Id_Usuario + "', '" + v_obj.ListaP + "')  ";

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

        public void AltaPersonas(kx_cliente v_obj)
        {
            MySqlConnection cnn2 = new MySqlConnection();
            Int32 id = 0;
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = " insert into mysql_usuarios ( USUARIO, CLAVE,  HABILITADO, AUD, ROL, INTENTOS) values (  '" + v_obj.User_Usuario + "', '" + v_obj.Pass_Usuario + "', 'SI', NOW(),  '" + v_obj.Roles_Usuario+ "', '0')  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                cmm.ExecuteNonQuery();

                cmm = null;


              
                cmdTxt = " select max(ID_USUARIO) as cant from  mysql_usuarios ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                MySqlDataReader lector = cmm.ExecuteReader();

                while (lector.Read())
                {
                    id = DalModelo.VeriIntMysql(lector, "cant");
                }

                lector.Close();
                cmm = null;
                v_obj.Id_Usuario = id.ToString();
                cmdTxt = " insert into kx_personas (Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web, Id_Usuario,ListaPrecio,Id_Config) values ('" + v_obj.Cod_Manual + "', '" + v_obj.Tipo_Doc + "', '" + v_obj.Doc_No + "', '" + v_obj.Iva + "', '" + v_obj.Nombre + "', '" + v_obj.Apellido + "', '" + v_obj.Observacion + "', '" + v_obj.Cta_contable + "', '" + v_obj.Calle + "', '" + v_obj.Numero + "', '" + v_obj.Dpto + "', '" + v_obj.Pais + "', '" + v_obj.Cp + "', '" + v_obj.Provincia + "', '" + v_obj.Departamento + "', '" + v_obj.Distrito + "', '" + v_obj.Contac_Nomb + "', '" + v_obj.Contac_Tel + "', '" + v_obj.Contac_Cel + "', '" + v_obj.Contac_Mail + "', '" + v_obj.Emp_Tel + "', '" + v_obj.Emp_Cel + "', '" + v_obj.Emp_Mail + "', '" + v_obj.Emp_Web + "' , '" + v_obj.Id_Usuario + "', '" + v_obj.ListaP + "', '" + v_obj.Id_Config + "')  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                cmm.ExecuteNonQuery();

                cmm = null;

                // tiene q haber una lista de de depositos para farmacia
                cmdTxt = " insert into kx_deposito_usuario ( id_dep,id_usuario) values ('" + v_obj.Deposito_Usuario + "', '" + v_obj.Id_Usuario + "'  )  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                cmm.ExecuteNonQuery();




                cmm = null;

                // tiene q haber una lista de de configuraciones para las empresas
                cmdTxt = " insert into kx_config_usuarios ( Id_config,id_usuario) values ('" + v_obj.Id_Config+ "', '" + v_obj.Id_Usuario + "'  )  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                cmm.ExecuteNonQuery();


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



        public Int32 AltaId(kx_cliente v_obj)
        {
            MySqlConnection cnn2 = new MySqlConnection();
            Int32 id = 0;

            try
            {



                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();


                string cmdTxt = " insert into kx_cliente (Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web,Id_Usuario) values ('" + v_obj.Cod_Manual + "', '" + v_obj.Tipo_Doc + "', '" + v_obj.Doc_No + "', '" + v_obj.Iva + "', '" + v_obj.Nombre + "', '" + v_obj.Apellido + "', '" + v_obj.Observacion + "', '" + v_obj.Cta_contable + "', '" + v_obj.Calle + "', '" + v_obj.Numero + "', '" + v_obj.Dpto + "', '" + v_obj.Pais + "', '" + v_obj.Cp + "', '" + v_obj.Provincia + "', '" + v_obj.Departamento + "', '" + v_obj.Distrito + "', '" + v_obj.Contac_Nomb + "', '" + v_obj.Contac_Tel + "', '" + v_obj.Contac_Cel + "', '" + v_obj.Contac_Mail + "', '" + v_obj.Emp_Tel + "', '" + v_obj.Emp_Cel + "', '" + v_obj.Emp_Mail + "', '" + v_obj.Emp_Web + "', '" + v_obj.Id_Usuario + "')  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                cmm.ExecuteNonQuery();

                cmm = null;
                  cmdTxt = " select max(Id) as cant from kx_cliente  ";

                  cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                MySqlDataReader lector =cmm.ExecuteReader();

                while (lector.Read())
                {
                    id = DalModelo.VeriIntMysql(lector, "cant");
                }

                lector.Close();

                return id;
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
                cnn2.Close();
            }

        }

        public void ModificacionPersona(kx_cliente v_obj)
        {
            MySqlConnection cnn2 = new MySqlConnection();
            try
            {

                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "update kx_personas set Cod_Manual='" + v_obj.Cod_Manual + "' ,  ListaPrecio='" + v_obj.ListaP + "' ,Tipo_Doc='" + v_obj.Tipo_Doc + "' , Doc_No='" + v_obj.Doc_No + "' , Iva='" + v_obj.Iva + "' , Nombre='" + v_obj.Nombre + "' , Apellido='" + v_obj.Apellido + "' , Observacion='" + v_obj.Observacion + "' , Cta_contable='" + v_obj.Cta_contable + "' , Calle='" + v_obj.Calle + "' , Numero='" + v_obj.Numero + "' , Dpto='" + v_obj.Dpto + "' , Pais='" + v_obj.Pais + "' , Cp='" + v_obj.Cp + "' , Provincia='" + v_obj.Provincia + "' , Departamento='" + v_obj.Departamento + "' , Distrito='" + v_obj.Distrito + "' , Contac_Nomb='" + v_obj.Contac_Nomb + "' , Contac_Tel='" + v_obj.Contac_Tel + "' , Contac_Cel='" + v_obj.Contac_Cel + "' , Contac_Mail='" + v_obj.Contac_Mail + "' , Emp_Tel='" + v_obj.Emp_Tel + "' , Emp_Cel='" + v_obj.Emp_Cel + "' , Emp_Mail='" + v_obj.Emp_Mail + "' , Emp_Web='" + v_obj.Emp_Web + "' where id ='" + v_obj.Id + "'   ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                cmm.ExecuteNonQuery();

                cmm = null;


                cmdTxt = "update mysql_usuarios set ROL='" + v_obj.Roles_Usuario + "'   where ID_USUARIO='" + v_obj.Id_Usuario + "'   ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                cmm.ExecuteNonQuery();


                cmm = null;


                cmdTxt = "update kx_deposito_usuario set id_dep ='" + v_obj.Deposito_Usuario + "'   where id_usuario='" + v_obj.Id_Usuario + "'   ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                cmm.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }

        }


        public void Modificacion(kx_cliente v_obj)
        {
            MySqlConnection cnn2 = new MySqlConnection();
            try
            {

                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = "update kx_cliente set Cod_Manual='" + v_obj.Cod_Manual + "' ,  ListaPrecio='" + v_obj.ListaP + "' ,Tipo_Doc='" + v_obj.Tipo_Doc + "' , Doc_No='" + v_obj.Doc_No + "' , Iva='" + v_obj.Iva + "' , Nombre='" + v_obj.Nombre + "' , Apellido='" + v_obj.Apellido + "' , Observacion='" + v_obj.Observacion + "' , Cta_contable='" + v_obj.Cta_contable + "' , Calle='" + v_obj.Calle + "' , Numero='" + v_obj.Numero + "' , Dpto='" + v_obj.Dpto + "' , Pais='" + v_obj.Pais + "' , Cp='" + v_obj.Cp + "' , Provincia='" + v_obj.Provincia + "' , Departamento='" + v_obj.Departamento + "' , Distrito='" + v_obj.Distrito + "' , Contac_Nomb='" + v_obj.Contac_Nomb + "' , Contac_Tel='" + v_obj.Contac_Tel + "' , Contac_Cel='" + v_obj.Contac_Cel + "' , Contac_Mail='" + v_obj.Contac_Mail + "' , Emp_Tel='" + v_obj.Emp_Tel + "' , Emp_Cel='" + v_obj.Emp_Cel + "' , Emp_Mail='" + v_obj.Emp_Mail + "' , Emp_Web='" + v_obj.Emp_Web + "' where id ='" + v_obj.Id + "'   ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                cmm.ExecuteNonQuery();

                cmm = null;


               cmdTxt = "update mysql_usuarios set ROL='" + v_obj.Roles_Usuario + "'   where ID_USUARIO='" + v_obj.Id_Usuario+ "'   ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                cmm.ExecuteNonQuery();


                cmm = null;


                cmdTxt = "update kx_deposito_usuario set id_dep ='" + v_obj.Deposito_Usuario + "'   where id_usuario='" + v_obj.Id_Usuario + "'   ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");

                cmm.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }

        }

        public List<kx_cliente> ListarPersonas(string v_tipo, string v_valor, string v_user)
        {

            List<kx_cliente> lista = new List<kx_cliente>();
            try
            {
                string cmdTxt = " select u.ID_USUARIO, p.Id,p.Cod_Manual, p.Tipo_Doc, p.Doc_No, p.Iva, p.Nombre, p.Apellido, p.Observacion, p.Cta_contable, p.Calle, p.Numero, p.Dpto, p.Pais, p.Cp, p.Provincia, p.Departamento, p.Distrito, p.Contac_Nomb, p.Contac_Tel, p.Contac_Cel,p.Contac_Mail, p.Emp_Tel, p.Emp_Cel, p.Emp_Mail, p.Emp_Web,p.ListaPrecio , u.USUARIO, u.ROL,ud.id_dep  from kx_personas p, mysql_usuarios u,kx_deposito_usuario ud where p.ID_USUARIO= u.ID_USUARIO  and ud.id_usuario=u.ID_USUARIO  and ";

                if (v_tipo == "0")
                {
                    cmdTxt += "    p.Nombre like  '" + v_valor + "%' and p.Id_Config = '" + v_user + "'  AND p.Habilitado IS NULL ";

                }
                if (v_tipo == "1")
                {
                    cmdTxt += "   p.Id like  '" + v_valor + "%'  and p.Id_Config = '" + v_user + "'  AND p.Habilitado IS NULL ";

                }
                if (v_tipo == "2")
                {
                    cmdTxt += "    p.Cod_Manual like  '" + v_valor + "%'  and p.Id_Config = '" + v_user + "'  AND p.Habilitado IS NULL ";

                }
                if (v_tipo == "3")
                {
                    cmdTxt += "   p.Tipo_Doc='CUIT'  AND  p.Doc_No like  '" + v_valor + "%'  and p.Id_Config = '" + v_user + "'  AND p.Habilitado IS NULL ";

                }
                if (v_tipo == "4")
                {
                    cmdTxt += "   p.Tipo_Doc='DNI'  AND  p.Doc_No like  '" + v_valor + "%'  and p.Id_Config = '" + v_user + "'  AND p.Habilitado IS NULL ";

                }
                if (v_tipo == "5")
                {
                    cmdTxt += "   p.Doc_No like  '" + v_valor + "%'  and p.Id_Config = '" + v_user + "'  AND p.Habilitado IS NULL ";
                }
                if (v_tipo == "6")
                {
                    cmdTxt += "    p.Apellido like  '" + v_valor + "%'  and p.Id_Config = '" + v_user + "'  AND p.Habilitado IS NULL ";

                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    kx_cliente entidad = new kx_cliente();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Cod_Manual = DalModelo.VerifStringMysql(lector, "Cod_Manual");
                    entidad.Tipo_Doc = DalModelo.VerifStringMysql(lector, "Tipo_Doc");
                    entidad.Doc_No = DalModelo.VerifStringMysql(lector, "Doc_No");
                    entidad.Iva = DalModelo.VerifStringMysql(lector, "Iva");
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.Apellido = DalModelo.VerifStringMysql(lector, "Apellido");
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
                    entidad.ListaP = DalModelo.VerifStringMysql(lector, "ListaPrecio");
                    entidad.Deposito_Usuario = DalModelo.VerifStringMysql(lector, "id_dep");
                    entidad.User_Usuario = DalModelo.VerifStringMysql(lector, "USUARIO");
                    entidad.Id_Usuario= DalModelo.VerifStringMysql(lector, "ID_USUARIO");
                    
                    entidad.Roles_Usuario = DalModelo.VerifStringMysql(lector, "ROL");
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

        public List<kx_cliente> Listar(string v_tipo, string v_valor, string v_user)
        {

            List<kx_cliente> lista = new List<kx_cliente>();
            try
            {
                string cmdTxt = " select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web,ListaPrecio from kx_cliente where Habilitado <>'N' AND Id_Usuario = '" + v_user + "' AND Habilitado IS NULL ";

                if (v_tipo == "0")
                {
                    cmdTxt = "select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web,ListaPrecio from kx_cliente where Nombre like  '" + v_valor + "%' and Id_Usuario = '" + v_user + "'  AND Habilitado IS NULL ";

                }
                if (v_tipo == "1")
                {
                    cmdTxt = "select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web,ListaPrecio from kx_cliente where Id like  '" + v_valor + "%'  and Id_Usuario = '" + v_user + "'  AND Habilitado IS NULL ";

                }
                if (v_tipo == "2")
                {
                    cmdTxt = "select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web,ListaPrecio from kx_cliente where Cod_Manual like  '" + v_valor + "%'  and Id_Usuario = '" + v_user + "'  AND Habilitado IS NULL ";

                }
                if (v_tipo == "3")
                {
                    cmdTxt = "select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web,ListaPrecio from kx_cliente where Tipo_Doc='CUIT'  AND  Doc_No like  '" + v_valor + "%'  and Id_Usuario = '" + v_user + "'  AND Habilitado IS NULL ";

                }
                if (v_tipo == "4")
                {
                    cmdTxt = "select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web,ListaPrecio from kx_cliente where Tipo_Doc='DNI'  AND  Doc_No like  '" + v_valor + "%'  and Id_Usuario = '" + v_user + "'  AND Habilitado IS NULL ";

                }
                if (v_tipo == "5")
                {
                    cmdTxt = "select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web,ListaPrecio from kx_cliente where  Doc_No like  '" + v_valor + "%'  and Id_Usuario = '" + v_user + "'  AND Habilitado IS NULL ";
                }
                if (v_tipo == "6")
                {
                    cmdTxt = "select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web,ListaPrecio  from kx_cliente where Apellido like  '" + v_valor + "%'  and Id_Usuario = '" + v_user + "'  AND Habilitado IS NULL ";

                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    kx_cliente entidad = new kx_cliente();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Cod_Manual = DalModelo.VerifStringMysql(lector, "Cod_Manual");
                    entidad.Tipo_Doc = DalModelo.VerifStringMysql(lector, "Tipo_Doc");
                    entidad.Doc_No = DalModelo.VerifStringMysql(lector, "Doc_No");
                    entidad.Iva = DalModelo.VerifStringMysql(lector, "Iva");
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.Apellido = DalModelo.VerifStringMysql(lector, "Apellido");
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
                    entidad.ListaP = DalModelo.VerifStringMysql(lector, "ListaPrecio");
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
        public List<kx_cliente> Listar()
        {

            List<kx_cliente> lista = new List<kx_cliente>();
            try
            {



                string cmdTxt = " select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web from kx_cliente  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    kx_cliente entidad = new kx_cliente();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Cod_Manual = DalModelo.VerifStringMysql(lector, "Cod_Manual");
                    entidad.Tipo_Doc = DalModelo.VerifStringMysql(lector, "Tipo_Doc");
                    entidad.Doc_No = DalModelo.VerifStringMysql(lector, "Doc_No");
                    entidad.Iva = DalModelo.VerifStringMysql(lector, "Iva");
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.Apellido = DalModelo.VerifStringMysql(lector, "Apellido");
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


        public void Eliminar(Int32 v_id)
        {
            try
            {
                string cmdTxt = "update kx_cliente set Habilitado='N'  where Id='" + v_id + "'";

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


        public void EliminarPersonas(Int32 v_id)
        {
            try
            {
                string cmdTxt = "update kx_personas set Habilitado='N'  where Id='" + v_id + "'";

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
                string cmdTxt = " select max(Id) as cant from kx_cliente  ";

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
