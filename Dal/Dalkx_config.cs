using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Be;
using MySql.Data.MySqlClient;
namespace Dal
{
    public class Dalkx_config
    {

        private MySqlConectarSqlDBVarias cnn = new MySqlConectarSqlDBVarias("kardex"); 
        MySqlCommand cmm;


        public void bitacora(Be.Bitacora bi)
        {
            try
            {
                // string cmdTxt = "delete from kx_proveedor  where Id='" + v_id + "'   ";

                string cmdTxt = "insert into error(msg, donde,valor,aud,config_user,id_user,metodo) values ('" + bi.msg + "','" + bi.donde + "','" + bi.valor + "',now(),'" + bi.config_user + "','" + bi.id_user + "','" + bi.metodo + "') ";

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

        public void  ActualizarPrecioxCosto(string v_user, string vPorc)
        {
            MySqlConnection cnn2;
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                cmm = null;
             string   cmdTxt = "actualizar_precios";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
                cnn.AgregarParametroAComando(cmm, "v_config",Int32.Parse( v_user));
                cnn.AgregarParametroAComando(cmm, "vPorc", Int32.Parse(vPorc));
                cmm.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
         

        }
        public List<kx_config> Obtener_config(string v_user)
        {
            try
            {
                List<kx_config> entidad2 = new List<kx_config>();
                string cmdTxt = " SELECT cf.id, cf.PtoVta, cf.Modo , cf.Usuario, cf.l1, cf.l1_desc, cf.l2, cf.l2_desc, cf.l3, cf.l3_desc, cf.l4, cf.l4_desc, cf.l5, cf.l5_desc, cf.Nombre, cf.Cuit, cf.DomicilioComercial, cf.CondicionIVA, cf.RazonSocial, cf.RazonSocial2, cf.Logo, cf.IIBB, DATE_FORMAT(cf.FechaIniActividad,  '%d/%m/%y') FechaIniActividad, cf.Principal FROM kx_config_usuarios cu, kx_config cf WHERE cu.Id_usuario ='" + v_user + "' AND cu.Id_config = cf.Id ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                  kx_config entidad = new kx_config ();

                    entidad.id = DalModelo.VerifStringMysql(lector, "id");
                    entidad.IIBB = DalModelo.VerifStringMysql(lector, "IIBB");
                    entidad.l1 = DalModelo.VerifStringMysql(lector, "l1");
                    entidad.l1_desc = DalModelo.VerifStringMysql(lector, "l1_desc");
                    entidad.l2 = DalModelo.VerifStringMysql(lector, "l2");
                    entidad.l2_desc = DalModelo.VerifStringMysql(lector, "l2_desc");
                    entidad.l3 = DalModelo.VerifStringMysql(lector, "l3");
                    entidad.l3_desc = DalModelo.VerifStringMysql(lector, "l3_desc");
                    entidad.l4 = DalModelo.VerifStringMysql(lector, "l4");
                    entidad.l4_desc = DalModelo.VerifStringMysql(lector, "l4_desc");
                    entidad.l5 = DalModelo.VerifStringMysql(lector, "l5");
                    entidad.l5_desc = DalModelo.VerifStringMysql(lector, "l5_desc");
                    entidad.Logo = (byte[]) lector["Logo"];
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.RazonSocial = DalModelo.VerifStringMysql(lector, "RazonSocial");
                    entidad.RazonSocial2 = DalModelo.VerifStringMysql(lector, "RazonSocial2");
                    entidad.CondicionIVA = DalModelo.VerifStringMysql(lector, "CondicionIVA");
                    entidad.Cuit = DalModelo.VerifStringMysql(lector, "Cuit");
                    entidad.DomicilioComercial = DalModelo.VerifStringMysql(lector, "DomicilioComercial");
                    entidad.FechaIniActividad = DalModelo.VerifStringMysql(lector, "FechaIniActividad");
                    entidad.PtoVta = DalModelo.VerifStringMysql(lector, "PtoVta");
                    entidad.Modo = DalModelo.VerifStringMysql(lector, "Modo");
                    entidad.Principal = DalModelo.VerifStringMysql(lector, "Principal");
                    entidad2.Add(entidad);
                }
                return entidad2;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }
        }

        public  string  Descripcion_Nombre(string v_user)
        {
            try
            {
                string cmdTxt = " SELECT Nombre from wi271852_orl.hosp_personal  WHERE Usuario ='" + v_user + "'  ";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);
                MySqlDataReader lector = cnn.ExecuteReader(cmm);
                string sal = "";
                while (lector.Read())
                { 
                    sal = DalModelo.VerifStringMysql(lector, "Nombre");                   
                }
                return sal;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }
        }


        public void Alta(kx_config v_obj)
        {
            try
            {
                string cmdTxt = " insert into kx_config (id, l1, l1_desc, l2, l2_desc, l3, l3_desc, l4, l4_desc, l5, l5_desc, Nombre, Cuit) values ('" + v_obj.id + "', '" + v_obj.l1 + "', '" + v_obj.l1_desc + "', '" + v_obj.l2 + "', '" + v_obj.l2_desc + "', '" + v_obj.l3 + "', '" + v_obj.l3_desc + "', '" + v_obj.l4 + "', '" + v_obj.l4_desc + "', '" + v_obj.l5 + "', '" + v_obj.l5_desc + "', '" + v_obj.Nombre + "', '" + v_obj.Cuit + "')  ";

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


        public void Modificacion(kx_config v_obj)
        {
            try
            {
                string cmdTxt = "update kx_config set id='" + v_obj.id + "' , l1='" + v_obj.l1 + "' , l1_desc='" + v_obj.l1_desc + "' , l2='" + v_obj.l2 + "' , l2_desc='" + v_obj.l2_desc + "' , l3='" + v_obj.l3 + "' , l3_desc='" + v_obj.l3_desc + "' , l4='" + v_obj.l4 + "' , l4_desc='" + v_obj.l4_desc + "' , l5='" + v_obj.l5 + "' , l5_desc='" + v_obj.l5_desc + "' , Nombre='" + v_obj.Nombre + "' , Cuit='" + v_obj.Cuit + "' where id ='" + v_obj.id + "'   ";

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

        public List<kx_config> Listar(string v_id)
        {

            List<kx_config> lista = new List<kx_config>();
            try
            {

                string cmdTxt = " select id, l1, l1_desc, l2, l2_desc, l3, l3_desc, l4, l4_desc, l5, l5_desc, Nombre, Cuit from kx_config where id = " + v_id;

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    kx_config entidad = new kx_config();
              
                    entidad.id = DalModelo.VerifStringMysql(lector, "id");
                    entidad.l1 = DalModelo.VerifStringMysql(lector, "l1");
                    entidad.l1_desc = DalModelo.VerifStringMysql(lector, "l1_desc");
                    entidad.l2 = DalModelo.VerifStringMysql(lector, "l2");
                    entidad.l2_desc = DalModelo.VerifStringMysql(lector, "l2_desc");
                    entidad.l3 = DalModelo.VerifStringMysql(lector, "l3");
                    entidad.l3_desc = DalModelo.VerifStringMysql(lector, "l3_desc");
                    entidad.l4 = DalModelo.VerifStringMysql(lector, "l4");
                    entidad.l4_desc = DalModelo.VerifStringMysql(lector, "l4_desc");
                    entidad.l5 = DalModelo.VerifStringMysql(lector, "l5");
                    entidad.l5_desc = DalModelo.VerifStringMysql(lector, "l5_desc");
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.Cuit = DalModelo.VerifStringMysql(lector, "Cuit");
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
                string cmdTxt = "delete from kx_config  where Id='" + v_id + "'";

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
                string cmdTxt = " select max(Id) as cant from kx_config  ";

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
        // // // // // 

        public void Alta_kx_config(kx_config v_obj)
        {
            MySqlConnection cnn2 = new MySqlConnection();
            try
            {
               
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = " UPDATE kx_config SET Principal ='N' WHERE id IN  (SELECT Id_config FROM kx_config_usuarios WHERE Id_usuario='" + v_obj.Usuario + "' ) ";
                if (v_obj.Principal == "S")
                {
                    cmm = null;                  
                    cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                    cmm.ExecuteNonQuery();
                }
                cmm = null;   
                  cmdTxt = " insert into kx_config (  Usuario, l1, l1_desc, l2, l2_desc, l3, l3_desc, l4, l4_desc, l5, l5_desc, Nombre, Cuit, DomicilioComercial, CondicionIVA, RazonSocial, Logo, IIBB, FechaIniActividad, PtoVta, Modo, RazonSocial2, Principal) values ";
               cmdTxt +=" ( ";
               cmdTxt += " '" + v_obj.Usuario  + "', ";
               cmdTxt += "'" + v_obj.l1 + "', ";
               cmdTxt += " '" + v_obj.l1_desc  + "', ";
               cmdTxt += " '" + v_obj.l2  + "', ";
               cmdTxt += " '" + v_obj.l2_desc  + "', ";
               cmdTxt += " '" + v_obj.l3 + "', ";
               cmdTxt += "'" + v_obj.l3_desc  + "',";
               cmdTxt += "'" + v_obj.l4 + "', ";
               cmdTxt += " '" + v_obj.l4_desc + "',";
               cmdTxt += "'" + v_obj.l5  + "', ";
               cmdTxt += " '" + v_obj.l5_desc  + "', ";
               cmdTxt += " '" + v_obj.Nombre  + "', ";
               cmdTxt += " '" + v_obj.Cuit  + "', ";
               cmdTxt += " '" + v_obj.DomicilioComercial + "',";
               cmdTxt += "'" + v_obj.CondicionIVA  + "',";
               cmdTxt += " '" + v_obj.RazonSocial  + "', ";
               cmdTxt += " @imagen,";
               cmdTxt += " '" + v_obj.IIBB + "', ";
               cmdTxt += " STR_TO_DATE('" + v_obj.FechaIniActividad + "','%d/%m/%Y') , ";
               cmdTxt += "'" + v_obj.PtoVta.ToUpper() + "',";
               cmdTxt += " '" + v_obj.Modo + "', ";
               cmdTxt += " '" + v_obj.RazonSocial2 + "', ";
               cmdTxt += " '" + v_obj.Principal  + "')  ";             
                
               // cmm.Parameters.Add("@imagen", MySqlDbType.VarBinary).Value = v_obj.Logo;

                //cmm = cnn.MySqlCrearNuevoComando(cmdTxt);
                //cnn.AgregarParametroAComando(cmm, "@imagen", v_obj.Logo);
                //cnn.ExecuteNonQuery(cmm);
                
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
#pragma warning disable CS0618 // 'MySqlParameterCollection.Add(string, object)' está obsoleto: 'Add(String parameterName, Object value) has been deprecated.  Use AddWithValue(String parameterName, Object value)'
                cmm.Parameters.Add("@imagen", v_obj.Logo);
#pragma warning restore CS0618 // 'MySqlParameterCollection.Add(string, object)' está obsoleto: 'Add(String parameterName, Object value) has been deprecated.  Use AddWithValue(String parameterName, Object value)'
                cmm.ExecuteNonQuery();


                cmdTxt = "select ( max(id) )  cant from   kx_config  ";
                    cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
               
            MySqlDataReader    lector = cmm.ExecuteReader();
            Int32 sal = 0;
            while (lector.Read())
            {
                sal =  Convert.ToInt32( lector["cant"]);
            }

            lector.Close();

            cmdTxt = "insert into kx_config_usuarios (Id_config,Id_usuario)  values ('"+sal+"','"+v_obj.Usuario+"') ";
            cmm = null;
            cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
            cmm.ExecuteNonQuery();


            }
            catch
            {
                throw;
            }finally{
             cnn2.Close();
            }
            

        }


        public void Modificacion_kx_config(kx_config v_obj)
        {
            MySqlConnection cnn2 = new MySqlConnection();
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = " UPDATE kx_config SET Principal ='N' WHERE id IN  (SELECT Id_config FROM kx_config_usuarios WHERE Id_usuario='" + v_obj.Usuario + "' ) ";
                if (v_obj.Principal == "S")
                {
                    cmm = null;
                    cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                    cmm.ExecuteNonQuery();
                }

                cmm = null;   
             cmdTxt = "update kx_config set  Usuario='" + v_obj.Usuario  + "' , l1='" + v_obj.l1  + "' , l1_desc='" + v_obj.l1_desc  + "' , l2='" + v_obj.l2  + "' , l2_desc='" + v_obj.l2_desc + "' , l3='" + v_obj.l3 + "' , l3_desc='" + v_obj.l3_desc + "' , l4='" + v_obj.l4  + "' , l4_desc='" + v_obj.l4_desc  + "' , l5='" + v_obj.l5  + "' , l5_desc='" + v_obj.l5_desc  + "' , Nombre='" + v_obj.Nombre  + "' , Cuit='" + v_obj.Cuit  + "' , DomicilioComercial='" + v_obj.DomicilioComercial + "' , CondicionIVA='" + v_obj.CondicionIVA  + "' , RazonSocial='" + v_obj.RazonSocial.ToUpper() + "' , Logo= @imagen, IIBB='" + v_obj.IIBB + "' , FechaIniActividad= STR_TO_DATE('" + v_obj.FechaIniActividad  + "','%d/%m/%Y')  , PtoVta='" + v_obj.PtoVta  + "' , Modo='" + v_obj.Modo + "' , RazonSocial2='" + v_obj.RazonSocial2  + "' , Principal='" + v_obj.Principal + "' where id ='" + v_obj.id + "'   ";
         
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
#pragma warning disable CS0618 // 'MySqlParameterCollection.Add(string, object)' está obsoleto: 'Add(String parameterName, Object value) has been deprecated.  Use AddWithValue(String parameterName, Object value)'
             cmm.Parameters.Add("@imagen", v_obj.Logo);
#pragma warning restore CS0618 // 'MySqlParameterCollection.Add(string, object)' está obsoleto: 'Add(String parameterName, Object value) has been deprecated.  Use AddWithValue(String parameterName, Object value)'
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
        public List<kx_config> Listar_kx_config(string v_tipo, string v_valor, string user)

        {

            List<kx_config> lista = new List<kx_config>();
            try
            {

              //  string user = ((mysql_usuarios)HttpContext.Current.Session["UsuarioActual"]).ID_USUARIO;

                string cmdTxt = " select id, Usuario, l1, l1_desc, l2, l2_desc, l3, l3_desc, l4, l4_desc, l5, l5_desc, Nombre, Cuit, DomicilioComercial, CondicionIVA, RazonSocial, Logo, IIBB, DATE_FORMAT(FechaIniActividad, '%d/%m/%y') FechaIniActividad, PtoVta, Modo, RazonSocial2, Principal from kx_config where Usuario ='" + user + "' ";

                if (v_tipo == "99")
                {
                    cmdTxt = " select id, Usuario, l1, l1_desc, l2, l2_desc, l3, l3_desc, l4, l4_desc, l5, l5_desc, Nombre, Cuit, DomicilioComercial, CondicionIVA, RazonSocial, Logo, IIBB,DATE_FORMAT(FechaIniActividad, '%d/%m/%y') FechaIniActividad, PtoVta, Modo, RazonSocial2, Principal from kx_config  where    Id='" + v_valor + "' ";
                }
                if (v_tipo == "0")
                {
                    cmdTxt = " select id, Usuario, l1, l1_desc, l2, l2_desc, l3, l3_desc, l4, l4_desc, l5, l5_desc, Nombre, Cuit, DomicilioComercial, CondicionIVA, RazonSocial, Logo, IIBB,DATE_FORMAT(FechaIniActividad, '%d/%m/%y') FechaIniActividad, PtoVta, Modo, RazonSocial2, Principal from kx_config where Usuario ='" + user + "'  ";
                }
                //if (string.IsNullOrEmpty(v_valor))
                //{
                //    cmdTxt = " select id, Usuario, l1, l1_desc, l2, l2_desc, l3, l3_desc, l4, l4_desc, l5, l5_desc, Nombre, Cuit, DomicilioComercial, CondicionIVA, RazonSocial, Logo, IIBB,DATE_FORMAT(FechaIniActividad, '%d/%m/%y') FechaIniActividad, PtoVta, Modo, RazonSocial2, Principal from kx_config  where Id = (select  Id_config from kx_config_usuarios where Id_usuario  ='" + user + "' ) ";
                //}
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    kx_config entidad = new kx_config();
 
                    entidad.id = DalModelo.VerifStringMysql(lector, "id");
                    entidad.Usuario = DalModelo.VerifStringMysql(lector, "Usuario");
                    entidad.l1 = DalModelo.VerifStringMysql(lector, "l1");
                    entidad.l1_desc = DalModelo.VerifStringMysql(lector, "l1_desc");
                    entidad.l2 = DalModelo.VerifStringMysql(lector, "l2");
                    entidad.l2_desc = DalModelo.VerifStringMysql(lector, "l2_desc");
                    entidad.l3 = DalModelo.VerifStringMysql(lector, "l3");
                    entidad.l3_desc = DalModelo.VerifStringMysql(lector, "l3_desc");
                    entidad.l4 = DalModelo.VerifStringMysql(lector, "l4");
                    entidad.l4_desc = DalModelo.VerifStringMysql(lector, "l4_desc");
                    entidad.l5 = DalModelo.VerifStringMysql(lector, "l5");
                    entidad.l5_desc = DalModelo.VerifStringMysql(lector, "l5_desc");
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.Cuit = DalModelo.VerifStringMysql(lector, "Cuit");
                    entidad.DomicilioComercial = DalModelo.VerifStringMysql(lector, "DomicilioComercial");
                    entidad.CondicionIVA = DalModelo.VerifStringMysql(lector, "CondicionIVA");
                    entidad.RazonSocial = DalModelo.VerifStringMysql(lector, "RazonSocial");
                    entidad.Logo = (byte[])lector["Logo"];
                    entidad.IIBB = DalModelo.VerifStringMysql(lector, "IIBB");
                    entidad.FechaIniActividad = DalModelo.VerifStringMysql(lector, "FechaIniActividad");
                    entidad.PtoVta = DalModelo.VerifStringMysql(lector, "PtoVta");
                    entidad.Modo = DalModelo.VerifStringMysql(lector, "Modo");
                    entidad.RazonSocial2 = DalModelo.VerifStringMysql(lector, "RazonSocial2");
                    entidad.Principal = DalModelo.VerifStringMysql(lector, "Principal");
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


        public void Eliminar_kx_config(Int32 v_id)
        {
            try
            {
                string cmdTxt = "delete from kx_config  where Id='" + v_id + "'";

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

        // tabla iibb

        public void Alta_kx_iibb(kx_iibb v_obj)
        {
            try
            {
                string cmdTxt = " insert into kx_iibb (Nombre, valor, Id_Usuario) values ('" + v_obj.Nombre.ToUpper() + "', '" + v_obj.valor.ToUpper() + "', '" + v_obj.Id_Usuario.ToUpper() + "')  ";

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

        public void Modificacion_kx_iibb(kx_iibb v_obj)
        {
            try
            {
                string cmdTxt = "update kx_iibb set Nombre='" + v_obj.Nombre.ToUpper() + "' , valor='" + v_obj.valor.ToUpper() + "' , Id_Usuario='" + v_obj.Id_Usuario.ToUpper() + "' where id ='" + v_obj.Id + "'   ";

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
        public List<kx_iibb> Listar_kx_iibb(string v_tipo, string v_valor)
        {

            List<kx_iibb> lista = new List<kx_iibb>();
            try
            {



                string cmdTxt = " select Id,Nombre, valor, Id_Usuario from kx_iibb  ";

                if (v_tipo == "99")
                {
                    cmdTxt = " select Id,Nombre, valor, Id_Usuario from kx_iibb  where   Id='"+v_valor+"' ";
                }
                if (v_tipo == "0")
                {
                    cmdTxt = " select Id,Nombre, valor, Id_Usuario from kx_iibb  where upper(Nombre) like  '%" + v_valor + "%' ";
                }
                if (string.IsNullOrEmpty(v_valor))
                {
                    cmdTxt = " select Id,Nombre, valor, Id_Usuario from kx_iibb  ";
                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    kx_iibb entidad = new kx_iibb();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.valor = DalModelo.VerifStringMysql(lector, "valor");
                    entidad.Id_Usuario = DalModelo.VerifStringMysql(lector, "Id_Usuario");
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


        public void Eliminar_kx_iibb(Int32 v_id)
        {
            try
            {
                string cmdTxt = "delete from kx_iibb  where Id='" + v_id + "'";

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

        public Int32 maxId_kx_iibb()
        {
            Int32 id = 0;

            try
            {
                string cmdTxt = " select max(Id) as cant from kx_iibb  ";

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
