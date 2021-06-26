using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Be;
using System.Data;
namespace Dal
{
   public class Dalmysql_usuarios
    {
       private MySqlConectarSqlDBVarias cnn = new MySqlConectarSqlDBVarias("kardex");

        MySqlConnection cnn2;
        MySqlCommand cmm;
        MySqlCommand cmm2;
        MySqlDataAdapter da;
       public void Alta ( mysql_usuarios  v_obj)

        { 
        try
         {
                string cmdTxt = " insert into  mysql_usuarios (ID_USUARIO, USUARIO, CLAVE, TOKEN, HABILITADO, AUD, ROL, INTENTOS) values ('"+v_obj.ID_USUARIO+"', '"+v_obj.USUARIO+"', '"+v_obj.CLAVE+"', '"+v_obj.TOKEN+"', '"+v_obj.HABILITADO+"', '"+v_obj.AUD+"', '"+v_obj.ROL+"', '"+v_obj.INTENTOS+"')  ";
 
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


 public void Modificacion ( mysql_usuarios  v_obj)

        { 
        try
         {
             string cmdTxt = "update  mysql_usuarios set ID_USUARIO='" + v_obj.ID_USUARIO + "' , USUARIO='" + v_obj.USUARIO + "' , CLAVE='" + v_obj.CLAVE + "' , TOKEN='" + v_obj.TOKEN + "' , HABILITADO='" + v_obj.HABILITADO + "' , AUD='" + v_obj.AUD + "' , ROL='" + v_obj.ROL + "' , INTENTOS='" + v_obj.INTENTOS + "' where ID_USUARIO ='" + v_obj.ID_USUARIO + "'   ";
 
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

 public void conectar()
 {

     cnn2 = cnn.MySqlCrearNuevaConexion();

     cnn2.Open();
 }
      
 public List<PermisoBase> Listar(PermisoFiltro filtro)
 {
     List<PermisoBase> lista = new List<PermisoBase>();
     string cmdTxt = "";
     bool bb = false;
     cnn.Close(cmm);
     try
     {



         if (filtro != null && !String.IsNullOrEmpty(filtro.nombre))
         {


             cmdTxt = "SELECT codigo, descripcion, esAccion ";
             cmdTxt += "FROM  PERMISOS ";
             cmdTxt += "WHERE  ";
             cmdTxt += " codigo = '" + filtro.nombre + "' ";
             cmdTxt += "ORDER BY codigo ASC";
             bb = true;

         }


         if (filtro != null && bb == false && filtro.FiltrarPorSimpleCompuesto)
         {

             string v_accion = "";
             if (filtro.soloSimples == true)
             {
                 v_accion = "SI";
             }
             else
             {
                 v_accion = "NO";
             }


             cmdTxt = "SELECT codigo, descripcion, esAccion ";
             cmdTxt += "FROM PERMISOS ";
             cmdTxt += "WHERE  ";
             cmdTxt += "  esAccion = '" + v_accion + "' ";
             cmdTxt += "ORDER BY codigo ASC";
             bb = true;

         }
         if (filtro != null && bb == false && (String.IsNullOrEmpty(filtro.nombre)))
         {

             cmdTxt = "SELECT codigo, descripcion, esAccion ";
             cmdTxt += "FROM PERMISOS ";

             cmdTxt += "ORDER BY codigo ASC";

         }



         bb = false;



         DataSet ds = new DataSet();

         conectar();
         cmm2 = new MySqlCommand(cmdTxt, cnn2);



         da = new MySqlDataAdapter(cmm2);

         da.Fill(ds);

         cnn2.Close();



         foreach (DataRow dr in ds.Tables[0].Rows)
         {

             PermisoBase permiso = ConvertirDataReaderEnPermiso2(dr);

             //agregar entidad a mi lista a retornar
             lista.Add(permiso);



         }



         //while( lector.Read()){
         //    //instanciar y establecer estado de mi entidad
         //    PermisoBase permiso = this.ConvertirDataReaderEnPermiso(lector);

         //    //agregar entidad a mi lista a retornar
         //    lista.Add(permiso);
         //}



         ////cerrar lector
         //lector.Close();



     }
     catch
     {
         throw;
     }
     finally
     {
         cnn2.Close();
         cnn.Close(cmm);
     }

     return lista;
 }


 private PermisoBase ConvertirDataReaderEnPermiso2(DataRow lector)
 {

     //instanciar y establecer estado de mi entidad
     PermisoBase permiso;

     // tiene hijos?
     if (lector[2] != DBNull.Value && Convert.ToBoolean(to_boolean(lector[2].ToString())))
     {
         permiso = new PermisoSimple();
     }
     else
     {
         permiso = new PermisoCompuesto();
     }

     permiso.nombre = Convert.ToString(lector[0]);
     permiso.descripcion = Convert.ToString(lector[1]);

     // es compuesto / tiene hijos?
     if (permiso.TieneHijos())
     {
         List<PermisoBase> listaHijos = this.ListarHijos(new PermisoFiltro(permiso.nombre));
         foreach (PermisoBase hijo in listaHijos)
         {
             permiso.AgregarHijo(hijo);
         }
     }

     return permiso;
 }


 public System.Collections.Generic.List<PermisoBase> ListarHijos(PermisoFiltro filtro)
 {
     List<PermisoBase> lista = new List<PermisoBase>();

     string cmdTxt = "SELECT codigo, descripcion, esAccion FROM  PERMISOS P,  PERM_PERM PP  WHERE PP.codigoHijo= P.codigo ";

     cmdTxt += " and PP.codigoPadre =  '" + filtro.nombre + "' ";
     cmdTxt += " ORDER BY codigo ASC";



     cmm = cnn.MySqlCrearNuevoComando(cmdTxt);
     try
     {



       MySqlDataReader lector = cnn.ExecuteReader(cmm);
         while (lector.Read())
         {
             //instanciar y establecer estado de mi entidad
             PermisoBase permiso = this.ConvertirDataReaderEnPermiso(lector);

             //agregar entidad a mi lista a retornar
             lista.Add(permiso);
         }
         //cerrar lector
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

  


 public Boolean to_boolean(string v_str)
 {
     if (v_str == "NO")
     {
         return false;

     }
     else
     {

         return true;
     }

 }
 private PermisoBase ConvertirDataReaderEnPermiso(MySqlDataReader lector)
 {
     //instanciar y establecer estado de mi entidad
     PermisoBase permiso;

     // tiene hijos?
     if (lector["esAccion"] != DBNull.Value && Convert.ToBoolean(to_boolean(lector["esAccion"].ToString())))
     {
         permiso = new PermisoSimple();
     }
     else
     {
         permiso = new PermisoCompuesto();
     }

     permiso.nombre = Convert.ToString(lector["codigo"]);
     permiso.descripcion = Convert.ToString(lector["descripcion"]);

     // es compuesto / tiene hijos?
     if (permiso.TieneHijos())
     {
         List<PermisoBase> listaHijos = this.ListarHijos(new PermisoFiltro(permiso.nombre));
         foreach (PermisoBase hijo in listaHijos)
         {
             permiso.AgregarHijo(hijo);
         }
     }

     return permiso;
 }
 public mysql_usuarios Obtener_usuario(string v_mail)
 {
     mysql_usuarios entidad = new mysql_usuarios();
     kx_config entidad2 = new kx_config();
     try
     {

         string cmdTxt = " select  ID_USUARIO, USUARIO, CLAVE, TOKEN, HABILITADO, AUD, ROL, INTENTOS from mysql_usuarios  where USUARIO = '" + v_mail + "' and HABILITADO = 'SI' ";

         cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

         MySqlDataReader lector = cnn.ExecuteReader(cmm);
        
         while (lector.Read())
         {

           

             entidad.ID_USUARIO = DalModelo.VerifStringMysql(lector, "ID_USUARIO");
             entidad.USUARIO = DalModelo.VerifStringMysql(lector, "USUARIO");
             entidad.CLAVE = DalModelo.VerifStringMysql(lector, "CLAVE");
             entidad.TOKEN = DalModelo.VerifStringMysql(lector, "TOKEN");
             entidad.HABILITADO = DalModelo.VerifStringMysql(lector, "HABILITADO");
             entidad.AUD = DalModelo.VerifStringMysql(lector, "AUD");
             entidad.ROL = DalModelo.VerifStringMysql(lector, "ROL");
             entidad.INTENTOS = DalModelo.VerifStringMysql(lector, "INTENTOS");
             

         }

         //recupear los permisos del usuario
         if (lector["ROL"] != DBNull.Value)
         {
             string perfil = Convert.ToString(lector["ROL"]);
             entidad.Perfil = this. ConsultarUno(new PermisoFiltro(perfil));


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


     return entidad;
 }


 public PermisoBase ConsultarUno(PermisoFiltro filtro)
 {
     List<PermisoBase> lista = this.Listar(filtro);
     if (lista.Count > 0)
     {
         return lista[0];
     }
     else
     {
         return null;
     }
 }  
 
 public  List< mysql_usuarios>  Listar()

        { 
       
 List< mysql_usuarios> lista = new List< mysql_usuarios>();  
try  
   {



       string cmdTxt = " select  ID_USUARIO, USUARIO, CLAVE, TOKEN, HABILITADO, AUD, ROL, INTENTOS from mysql_usuarios  ";
 
                 cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                 MySqlDataReader lector = cnn.ExecuteReader(cmm);
 
                while (lector.Read())
                {

   mysql_usuarios  entidad = new  mysql_usuarios();
 
entidad.ID_USUARIO = DalModelo.VerifStringMysql(lector, "ID_USUARIO"); 
entidad.USUARIO = DalModelo.VerifStringMysql(lector, "USUARIO"); 
entidad.CLAVE = DalModelo.VerifStringMysql(lector, "CLAVE"); 
entidad.TOKEN = DalModelo.VerifStringMysql(lector, "TOKEN"); 
entidad.HABILITADO = DalModelo.VerifStringMysql(lector, "HABILITADO"); 
entidad.AUD = DalModelo.VerifStringMysql(lector, "AUD"); 
entidad.ROL = DalModelo.VerifStringMysql(lector, "ROL"); 
entidad.INTENTOS = DalModelo.VerifStringMysql(lector, "INTENTOS"); 
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
                string cmdTxt = "delete from  mysql_usuarios  where Id='" + v_id + "'";
                             
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
                string cmdTxt = " select max(Id) as cant from  mysql_usuarios  ";
 
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
   public string club_by_user(string v_id)
   {
     string    id = "";

       try
       {
           string cmdTxt = " select ID as cant from  dep_club where ID_USUARIO = '"+v_id+"'";

           cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

           MySqlDataReader lector = cnn.ExecuteReader(cmm);

           while (lector.Read())
           {
               id = DalModelo.VerifStringMysql(lector, "cant");
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

   public void Modificar_pwd(string v_mail, string v_pass)
   {

       try
       {
           string cmdTxt = "update  usuarios_web set password = '" + v_pass + "'";

           cmdTxt += " where   mail = '" + v_mail.ToUpper().Trim() + "' and habilitado = 'S'";

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

   public string existe_mail_hab(string v_mail)
   {
       string PP = "NO";
       try
       {
           string cmdTxt = "SELECT habilitado ";

           cmdTxt += " from usuarios_web  where   mail = '" + v_mail.ToUpper().Trim() + "' AND habilitado ='S' limit 1 ";

           cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

           MySqlDataReader lector = cnn.ExecuteReader(cmm);
           while (lector.Read())
           {
               PP = DalModelo.VerifStringMysql(lector, "habilitado");
           }
           //cerrar lector
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
       return PP;




   }



  public string GenerateRandomCode()
   {
       Random r = new Random();
       string s = "";
       for (int j = 0; j < 5; j++)
       {
           int i = r.Next(3);
           int ch;
           switch (i)
           {
               case 1:
                   ch = r.Next(0, 9);
                   s = s + ch.ToString();
                   break;
               case 2:
                   ch = r.Next(65, 90);
                   s = s + Convert.ToChar(ch).ToString();
                   break;
               case 3:
                   ch = r.Next(97, 122);
                   s = s + Convert.ToChar(ch).ToString();
                   break;
               default:
                   ch = r.Next(97, 122);
                   s = s + Convert.ToChar(ch).ToString();
                   break;
           }
           r.NextDouble();
           r.Next(100, 1999);
       }
       return s;
   }

        public List<Combos> ListarRoles(String SIS)

        { 
            List<Combos> lista = new List<Combos>();
            try
            {
                 

                string cmdTxt = "   select CODIGO, DESCRIPCION  from permisos  where  ESACCION='NO' AND (SISTEMA='"+ SIS + "' or ''='" + SIS + "' ) ORDER BY CODIGO ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                   Combos entidad = new Combos();

                    entidad.valor = DalModelo.VerifStringMysql(lector, "CODIGO");
                    entidad.descripcion = DalModelo.VerifStringMysql(lector, "CODIGO");
                   
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
        ///////////////////////// form usuarios
        ///


        public void Alta_mysql_usuarios(mysql_usuarios v_obj)

        {
            try
            {
                string cmdTxt = " insert into mysql_usuarios (ID_USUARIO, USUARIO, CLAVE, TOKEN, HABILITADO, AUD, ROL, INTENTOS) values ('" + v_obj.ID_USUARIO.ToUpper() + "', '" + v_obj.USUARIO.ToUpper() + "', '" + v_obj.CLAVE.ToUpper() + "', '" + v_obj.TOKEN.ToUpper() + "', '" + v_obj.HABILITADO.ToUpper() + "', '" + v_obj.AUD.ToUpper() + "', '" + v_obj.ROL.ToUpper() + "', '" + v_obj.INTENTOS.ToUpper() + "')  ";

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


     
        public void Modificacion_mysql_usuarios(mysql_usuarios v_obj)

        {
            try
            {
                string cmdTxt = "update mysql_usuarios set ID_USUARIO='" + v_obj.ID_USUARIO.ToUpper() + "' , USUARIO='" + v_obj.USUARIO.ToUpper() + "' , CLAVE='" + v_obj.CLAVE.ToUpper() + "' , TOKEN='" + v_obj.TOKEN.ToUpper() + "' , HABILITADO='" + v_obj.HABILITADO.ToUpper() + "' , AUD='" + v_obj.AUD.ToUpper() + "' , ROL='" + v_obj.ROL.ToUpper() + "' , INTENTOS='" + v_obj.INTENTOS.ToUpper() + "' where id ='" + v_obj.ID_USUARIO + "'   ";

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
        public List<mysql_usuarios> Listar_mysql_usuarios(string v_tipo, string v_valor)

        {

            List<mysql_usuarios> lista = new List<mysql_usuarios>();
            try
            {



                string cmdTxt = " select  ID_USUARIO, USUARIO, CLAVE, TOKEN, HABILITADO, AUD, ROL, INTENTOS from mysql_usuarios  ";

                if (v_tipo == "99")
                {
                    cmdTxt = " select  ID_USUARIO, USUARIO, CLAVE, TOKEN, HABILITADO, AUD, ROL, INTENTOS from mysql_usuarios  where    ID_USUARIO='" + v_valor + "' ";
                }
                if (v_tipo == "0")
                {
                    cmdTxt = " select  ID_USUARIO, USUARIO, CLAVE, TOKEN, HABILITADO, AUD, ROL, INTENTOS from mysql_usuarios  ";
                }
                if (string.IsNullOrEmpty(v_valor))
                {
                    cmdTxt = " select  ID_USUARIO, USUARIO, CLAVE, TOKEN, HABILITADO, AUD, ROL, INTENTOS from mysql_usuarios  ";
                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    mysql_usuarios entidad = new mysql_usuarios();
            
                    entidad.ID_USUARIO = DalModelo.VerifStringMysql(lector, "ID_USUARIO");
                    entidad.USUARIO = DalModelo.VerifStringMysql(lector, "USUARIO");
                    entidad.CLAVE = DalModelo.VerifStringMysql(lector, "CLAVE");
                    entidad.TOKEN = DalModelo.VerifStringMysql(lector, "TOKEN");
                    entidad.HABILITADO = DalModelo.VerifStringMysql(lector, "HABILITADO");
                    entidad.AUD = DalModelo.VerifStringMysql(lector, "AUD");
                    entidad.ROL = DalModelo.VerifStringMysql(lector, "ROL");
                    entidad.INTENTOS = DalModelo.VerifStringMysql(lector, "INTENTOS");
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


        public void Eliminar_mysql_usuarios(Int32 v_id)
        {
            try
            {
                string cmdTxt = "delete from mysql_usuarios  where Id='" + v_id + "'";

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
    }
}
