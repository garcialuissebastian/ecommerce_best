using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Be;
using System.Data.SqlClient;
namespace Dal
{
  public  class DalUsuario_Intranet
    {
        private ConectarSqlDB cnn = new ConectarSqlDB();
     DalPermiso _mapeadorPermiso = new  DalPermiso(); 

     SqlCommand cmm;

        public Be.USUARIO_INTRANET 
            Obtener_usuario(string v_Legajo, string v_pass)
        {
            Be.USUARIO_INTRANET entidad = new USUARIO_INTRANET();

            if (!string.IsNullOrEmpty(v_pass) &&   !string.IsNullOrEmpty(v_Legajo))
            {             
                try
                {
                                   
                entidad = BuscarUsuario(  v_Legajo);

                }
                catch (Exception)
                {

                    throw;
                }
             
             }         
             
                return entidad;
             
        }
        public void modificar_mail(Int32 v_Legajo, string v_mail)
        { 
        try
         {
                string cmdTxt = "update  RENTASVIRTUAL.USUARIO_INTRANET  set MAIL ='"+v_mail+"' where LEGAJO = '" + v_Legajo + "'    ";

                                cmm = cnn.CrearNuevoComando(cmdTxt);

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

        public USUARIO_INTRANET BuscarUsuario(string v_Legajo)
        {
            Be.USUARIO_INTRANET entidad = new USUARIO_INTRANET();
            try
            {
                string cmdTxt = "SELECT PERSONAL_ID, DOC_TIPO,DOC_NO, APELLIDO,NOMBRE,USUARIO,CLAVE, TOKEN,HABILITADO, ROL, CUILCUIT,INTENTOS,INICIO,MAIL, EMPRESA,CUIT_EMPRESA  ";

                cmdTxt += "from  USUARIO_INTRANET where USUARIO = '" + v_Legajo.ToString() + "'";

                cmm = cnn.CrearNuevoComando(cmdTxt);

                SqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {
                    entidad.PERSONAL_ID = DalModelo.VeriIntSql(lector, "PERSONAL_ID");
                    entidad.DOC_TIPO =  DalModelo.VerifStringSql(lector, "DOC_TIPO");

                    entidad.DOC_NO = VeriInt(lector, "DOC_NO");
                    entidad.APELLIDO = VerifString(lector, "APELLIDO");
                    entidad.NOMBRE = VerifString(lector, "NOMBRE");
                    entidad.USUARIO = VerifString(lector, "USUARIO");
                    entidad.CLAVE = VerifString(lector, "CLAVE");
                    entidad.TOKEN = VerifString(lector, "TOKEN");
                    entidad.HABILITADO = VerifString(lector, "HABILITADO");
                    entidad.ROL = VerifString(lector, "ROL");
                    entidad.INTENTOS= VeriInt(lector, "INTENTOS");
                    entidad.INICIO = VerifString(lector, "INICIO");
                    entidad.MAIL    = VerifString(lector, "MAIL");
                    entidad.EMPRESA = VerifString(lector, "EMPRESA");
                    entidad.CUILCUIT_LIMPIO = VerifString(lector, "CUILCUIT");
                    entidad.CUIT_EMPRESA = VerifString(lector, "CUIT_EMPRESA");

                    //recupear los permisos del usuario
                    if (lector["ROL"] != DBNull.Value)
                    {
                        string perfil = Convert.ToString(lector["ROL"]);
                        entidad.Perfil = this._mapeadorPermiso.ConsultarUno(new PermisoFiltro(perfil));
                 
                    
                    }






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
            return entidad;
        }



        public void CanbiarPW_token(string v_toke, string v_leg)

        {
          
            try
            {
                string cmdTxt = "update  RENTASVIRTUAL.USUARIO_INTRANET set TOKEN = '" + v_toke + "' where  LEGAJO = " + v_leg;



               
                cmm = cnn.CrearNuevoComando(cmdTxt);

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


        public void delete_token(string v_toke)
        {

            try
            {
                string cmdTxt = "delete from RENTASVIRTUAL.USUARIO_INTRANET where TOKEN = '" + v_toke + "' ";

                                cmm = cnn.CrearNuevoComando(cmdTxt);

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

        public string Md5(string v_pass)
        {
            string pp = "";
            try
            {
                string cmdTxt = "SELECT FuncionMD5('" + v_pass + "') as pass FROM DUAL ";



                cmm = cnn.CrearNuevoComando(cmdTxt);

                SqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    pp = VerifString(lector, "pass");

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
            return pp;
        }

        public Int32 VeriInt(SqlDataReader lector, string s)
        {

            if (lector[s] == DBNull.Value)
            {

                return 0;
            }
            else
            {

                return Convert.ToInt32(lector[s]);

            }


        }

        public string VerifString(SqlDataReader lector, string s)
        {

            if (lector[s] == DBNull.Value)
            {

                return "";
            }
            else
            {

                return Convert.ToString(lector[s]);

            }


        }
        public void Modificar_pwd(string v_leg, string v_pass)
        {

            try
            {
                string cmdTxt = "update USUARIO_INTRANET set CLAVE = '" +  v_pass+ "'";

                cmdTxt += " where USUARIO = '" + v_leg.Trim() + "' ";

                cmm = cnn.CrearNuevoComando(cmdTxt);

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


        public string date_to_string(DateTime v_fecha)
        {

            return v_fecha.ToString("dd/MM/yyyy");




        }

    }
}
