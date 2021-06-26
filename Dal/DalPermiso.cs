using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Be;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
namespace Dal
{
//    public interface IMapeadorPermiso:IMapeadorCRUD<PermisoBase, PermisoFiltro>{ 
 
//    List<PermisoBase> ListarHijos(PermisoFiltro filtro) ;
//    bool AgregarHijo(PermisoBase padre, PermisoBase hijo);
//    bool QuitarHijo(PermisoBase padre, PermisoBase hijo);
//    bool QuitarTodosHijos(PermisoBase padre);
//}



public class DalPermiso:IMapeadorPermiso{
    
      private  ConectarSqlDB  cnn  = new  ConectarSqlDB ();


      SqlConnection cnn2;
    

      SqlCommand cmm;
      SqlCommand cmm2;

     SqlDataAdapter da;




      public void conectar() {

          cnn2 = cnn .CrearNuevaConexion();

          cnn2.Open();
      }



       

    public DalPermiso() {
     
         
    }


    public string very_bool(bool v_b) { 
    
    if(v_b== true){
        return "SI";
    }else{
        return "NO";
    }
    
    
    }

    public bool Agregar(PermisoBase valor) {
        int registrosAfectados = 0;

         
        cmm = cnn.CrearNuevoComando("INSERT INTO PERMISOS VALUES  ( '" + valor.nombre + "','" + valor.descripcion + "', '" + very_bool( !valor.TieneHijos()) + "')");
    
        try{

             
            registrosAfectados = cnn.ExecuteNonQuery(cmm);
 
            // tiene hijos relacionados para agregar?
            if( registrosAfectados > 0 && valor.TieneHijos() ){
               
                this.QuitarTodosHijos(valor);
                foreach(PermisoBase hijo in valor.ObtenerHijos()){
                    this.AgregarHijo(valor, hijo);
                }
            }
        }catch{
            throw;
        }finally{
           cnn.Close(cmm);
        }
 
        return (registrosAfectados > 0);
    }  
 
    public PermisoBase ConsultarUno(PermisoFiltro filtro) {
        List<PermisoBase> lista= this.Listar(filtro)  ;
        if( lista.Count > 0 ){
            return lista[0];
        }else{
            return null;
        }
    }  
 
    public bool Eliminar(PermisoBase valor) {
        int registrosAfectados = 0;

        string str = "UPDATE USUARIO_INTRANET SET ROL=NULL WHERE ROL='" + valor.nombre + "'";
       string str1 = " DELETE FROM PERM_PERM WHERE codigoPadre='" + valor.nombre + "' OR codigoHijo='" + valor.nombre + "'"; 
        string str2 = "DELETE FROM PERMISOS WHERE codigo='" + valor.nombre + "'";

        cmm = cnn.CrearNuevoComando(str);
        try{
            cmm = cnn.CrearNuevoComando(str);
 
            registrosAfectados = cnn.ExecuteNonQuery(cmm);
            cmm = null;
            cmm = cnn.CrearNuevoComando(str1);
            registrosAfectados = cnn.ExecuteNonQuery(cmm);

            cmm = null;
            cmm = cnn.CrearNuevoComando(str2);
            registrosAfectados = cnn.ExecuteNonQuery(cmm);


        }catch{
            throw;
        }finally{
            cnn.Close(cmm);
        }
 
        return (registrosAfectados > 0);
    }  
 
    public List<PermisoBase> Listar(PermisoFiltro filtro) {
        List<PermisoBase> lista= new List< PermisoBase> ()  ;
          string cmdTxt="";
          bool bb = false;
          cnn.Close(cmm);
          try
          {



              if (filtro != null && !String.IsNullOrEmpty(filtro.nombre))
              {


                  cmdTxt = "SELECT codigo, descripcion, esAccion ";
                  cmdTxt += "FROM PERMISOS ";
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
              if (filtro != null && bb==false  && (  String.IsNullOrEmpty(filtro.nombre)))
              {

                  cmdTxt = "SELECT codigo, descripcion, esAccion ";
                  cmdTxt += "FROM PERMISOS ";
              
                  cmdTxt += "ORDER BY codigo ASC";

              }



              bb = false;



              DataSet ds = new DataSet();

              conectar();
              cmm2 = new SqlCommand(cmdTxt, cnn2);



              da = new SqlDataAdapter(cmm2);

              da.Fill(ds);

              cnn2.Close();



              foreach (DataRow dr in ds.Tables[0].Rows)
              {

                  PermisoBase permiso = this.ConvertirDataReaderEnPermiso2(dr);

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



        }catch{
            throw;
        }finally{
            cnn2.Close();
           cnn.Close(cmm);
        }
 
        return lista;
    }

 
 
    public bool Modificar(PermisoBase valor) {
        int registrosAfectados = 0;
        cmm = cnn.CrearNuevoComando("UPDATE PERMISOS SET descripcion='" + valor.descripcion + "' WHERE codigo='" + valor.nombre + "'");
        try{

 
 
            registrosAfectados = cnn.ExecuteNonQuery(cmm);
 
            // tiene hijos relacionados para agregar?
            if( registrosAfectados > 0 && valor.TieneHijos() ){
                this.QuitarTodosHijos(valor);
                foreach(PermisoBase hijo in valor.ObtenerHijos()){
                    this.AgregarHijo(valor, hijo);
                }
            }
 
        }catch{
            throw;
        }finally{
          cnn.Close(cmm);
        }
 
        return (registrosAfectados > 0);
    }


    public bool esPadre(string hijo , string padre) {
        string cmdTxt = "SELECT count(*) cant from  PERM_PERM where CODIGOPADRE = '" + hijo + "'and CODIGOHIJO  = '" + padre + "'";

         
        cmm = cnn.CrearNuevoComando(cmdTxt);
        try
        {


            Int32 v_cant= 0;
            SqlDataReader lector = cnn.ExecuteReader(cmm);
            while (lector.Read())
            {
                v_cant = Convert.ToInt32(lector[0]);
            } lector.Close();
            //cerrar lector
            if (v_cant > 0) { return true; } else { return false; }
             
             
            
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


    public bool AgregarHijo(PermisoBase padre, PermisoBase hijo) {
        int registrosAfectados = 0;


       
       if(!esPadre(hijo.nombre,padre.nombre)){


        cmm = cnn.CrearNuevoComando("INSERT INTO PERM_PERM VALUES('" + padre.nombre + "', '" + hijo.nombre + "')");
   
        
        
        try{
     
 
            registrosAfectados = cnn.ExecuteNonQuery(cmm);
        }catch{
            throw;
        }finally{
          cnn.Close(cmm);
        }
 }
        return (registrosAfectados > 0);
    }  
 
    public System.Collections.Generic.List<PermisoBase> ListarHijos(PermisoFiltro filtro) {
        List<PermisoBase>  lista= new List < PermisoBase> ();
 
        string cmdTxt = "SELECT codigo, descripcion, esAccion FROM PERMISOS P, PERM_PERM PP  WHERE PP.codigoHijo= P.codigo ";
 
        cmdTxt += " and PP.codigoPadre =  '" + filtro.nombre + "' ";
        cmdTxt += " ORDER BY codigo ASC";
     
        
        
        cmm = cnn.CrearNuevoComando(cmdTxt);
        try{

   
 
            SqlDataReader lector = cnn.ExecuteReader(cmm);
            while( lector.Read()){
                //instanciar y establecer estado de mi entidad
                PermisoBase permiso = this.ConvertirDataReaderEnPermiso(lector);
 
                //agregar entidad a mi lista a retornar
                lista.Add(permiso);
            }
            //cerrar lector
            lector.Close();
        }catch{
            throw;
        }finally{
           cnn.Close(cmm);
        }
 
        return lista;
    }  
 
    public bool QuitarHijo(PermisoBase padre, PermisoBase hijo) {
        int registrosAfectados = 0;

        cmm = cnn.CrearNuevoComando("DELETE FROM PERM_PERM WHERE codigoPadre='" + padre.nombre + "' AND codigoHijo='" + hijo.nombre + "'");
  
        
        try{

 

 
 
            registrosAfectados = cnn.ExecuteNonQuery(cmm);
        }catch{
            throw;
        }finally{
            cnn.Close(cmm);
        }
 
        return (registrosAfectados > 0);
    }  
 
    public bool QuitarTodosHijos(PermisoBase padre) {
        int registrosAfectados = 0;

        cmm = cnn.CrearNuevoComando("DELETE FROM PERM_PERM WHERE codigoPadre='" + padre.nombre + "' ");
        try{
   
            registrosAfectados = cnn.ExecuteNonQuery(cmm);
        }catch{
            throw;
        }finally{
           cnn.Close(cmm);
        }
 
        return (registrosAfectados > 0);
    }
    public Boolean to_boolean(string v_str) {
        if (v_str == "NO")
        {
            return false;

        }
        else {

            return true;
        }
    
    }

     private PermisoBase ConvertirDataReaderEnPermiso2(DataRow lector) {

        //instanciar y establecer estado de mi entidad
        PermisoBase permiso;
 
        // tiene hijos?
        if (lector[2] != DBNull.Value && Convert.ToBoolean(to_boolean(lector[2].ToString())))
        {
            permiso = new PermisoSimple();
        }else{
            permiso = new PermisoCompuesto();
        }
 
        permiso.nombre = Convert.ToString(lector[0]);
        permiso.descripcion = Convert.ToString(lector[1]);
 
        // es compuesto / tiene hijos?
        if( permiso.TieneHijos() ){
            List<PermisoBase> listaHijos= this.ListarHijos(new PermisoFiltro(permiso.nombre))  ;
            foreach(PermisoBase hijo in listaHijos){
                permiso.AgregarHijo(hijo);
            }
        }
 
        return permiso;
    }  
 
 


    private PermisoBase ConvertirDataReaderEnPermiso(SqlDataReader lector) {
        //instanciar y establecer estado de mi entidad
        PermisoBase permiso;
 
        // tiene hijos?
        if (lector["esAccion"] != DBNull.Value && Convert.ToBoolean(to_boolean(lector["esAccion"].ToString())))
        {
            permiso = new PermisoSimple();
        }else{
            permiso = new PermisoCompuesto();
        }
 
        permiso.nombre = Convert.ToString(lector["codigo"]);
        permiso.descripcion = Convert.ToString(lector["descripcion"]);
 
        // es compuesto / tiene hijos?
        if( permiso.TieneHijos() ){
            List<PermisoBase> listaHijos= this.ListarHijos(new PermisoFiltro(permiso.nombre))  ;
            foreach(PermisoBase hijo in listaHijos){
                permiso.AgregarHijo(hijo);
            }
        }
 
        return permiso;
    }  
 
} // DalPermiso













}