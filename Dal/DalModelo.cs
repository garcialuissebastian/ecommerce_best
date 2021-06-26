using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
namespace Dal
{
  public  class DalModelo
    {

        private ConectarSqlDB cnn = new ConectarSqlDB();
        SqlCommand cmm;
#pragma warning disable CS0414 // El campo 'DalModelo.sqlcnn' está asignado pero su valor nunca se usa
        SqlConnection sqlcnn = null;
#pragma warning restore CS0414 // El campo 'DalModelo.sqlcnn' está asignado pero su valor nunca se usa


        // seteo la base mysql

     static  string NombreBase = "ORL";
        private MySqlConectarSqlDBVarias cnn2 = new MySqlConectarSqlDBVarias(NombreBase);
        MySqlCommand cmm2;

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        public string Ver_modeloMsql( Be.Modelo obj_mod)
        {
          
            ///// obj //
         string    MiPage = obj_mod.MiPage;
         string Master = obj_mod.Master;
         string TipoBase = obj_mod.TipoBase.ToString();
         string v_tabla = obj_mod.tabla;
         NombreBase = obj_mod.base_datos;
           ////  end  obj ////


            string ObjCnn = "ConectarSqlDB";
            string ObjCmm = "SqlCommand";
            string ObjDataReader = "SqlDataReader";

            string ObjNuevaCmm = "CrearNuevoComando";
            string sVeriIntSql = "VeriIntSql";
            string sVerifStringSql = "VerifStringSql";
            
            if (TipoBase == "sqlserver")
            {
                ObjCnn = "private ConectarSqlDB cnn = new ConectarSqlDB()";
                ObjCmm = "SqlCommand";
                ObjDataReader = "SqlDataReader";
                ObjNuevaCmm = "CrearNuevoComando";
                sVeriIntSql = "VeriIntSql";
                sVerifStringSql = "VerifStringSql";

            }
            if (TipoBase == "mysql")
            {
                cnn2 = new MySqlConectarSqlDBVarias(NombreBase); // PARA Q SE CONECTA A LA BASE MYSQL Q LE PASO

                ObjCnn = "MySqlConectarSqlDBVarias cnn = new MySqlConectarSqlDBVarias(\"" + NombreBase + "\")";
                ObjCmm = "MySqlCommand";
                ObjDataReader = "MySqlDataReader";
                ObjNuevaCmm = "MySqlCrearNuevoComando";
                sVeriIntSql = "VeriIntMysql";
                sVerifStringSql = "VerifStringMysql";
            }
            if (TipoBase == "oracle")
            {
                ObjCnn = "private ConectarSqlDB cnn = new ConectarSqlDB()";
                ObjCmm = "OleDbCommand";
                ObjDataReader = "OleDbDataReader";
                ObjNuevaCmm = "CrearNuevoComando";
                sVeriIntSql = "VeriIntSql";
                sVerifStringSql = "VerifStringSql";
            }


         List<string> ListaNombre = new List<string>();

            //  v_tabla = "Clientes";
            string up = "update " + v_tabla + " set ";
            string modificar = "";
            string sal = "";
            string sel1 = "";
            string sel2 = "";

            string Ent = "<br/> public Int32 Id { get; set; }";

            string EntSal = "";
            string Bll = "";
            string Dal = "";

            // todo json    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable CS0219 // La variable 'json' está asignada pero su valor nunca se usa
            string json = "";
#pragma warning restore CS0219 // La variable 'json' está asignada pero su valor nunca se usa

            string fcBuscar = ""; // llena la grilla 
            string Modal = "";
            string ModalFin = "";

            fcBuscar += " ---------------------------------------------------  js ------------------------------------------------------------------";
            fcBuscar += "<br/><br/><br/><br/>  &nbsp; function Buscar_" + v_tabla + "() {  <br/>  try {  &nbsp;&nbsp;  <br/> <br/>&nbsp;&nbsp;&nbsp;   var v_buscar = $('#cmbTipoOp').val();  <br/>  &nbsp;&nbsp;&nbsp; var json = JSON.stringify <br/> &nbsp;&nbsp;    ({ <br/>&nbsp; &nbsp; &nbsp; &nbsp;   'v_tipo': $('#cmbTipoOp').val(), <br/>   ";
            fcBuscar += "   &nbsp;&nbsp; &nbsp; &nbsp;  'v_valor': $('#" + Master + "TxtBuscar').val() <br/> <br/> &nbsp;&nbsp;   }); <br/> ";

            fcBuscar += "   $.ajax({  <br/>  &nbsp; &nbsp;   type: 'POST',  <br/> &nbsp;  &nbsp;   url: '" + MiPage + "/WsListar_" + v_tabla + "',  <br/>  &nbsp; &nbsp;  data: json,  <br/> &nbsp;  &nbsp;  contentType: 'application/json; charset=utf-8', <br/> &nbsp;  &nbsp;   dataType: 'json', <br/> &nbsp; &nbsp;  success: resultado_" + v_tabla + ", <br/>  &nbsp; &nbsp;  error: erroresJs <br/>   &nbsp;     }); <br/>  <br/> &nbsp; &nbsp;   } catch (e) { <br/> &nbsp; &nbsp; &nbsp;   alert(e.message);<br/>&nbsp; &nbsp;   }<br/> &nbsp; &nbsp;  }<br/>     ";
             fcBuscar += "  <br/> &nbsp;  function resultado_" + v_tabla + "(Resultado) {  <br/> &nbsp;&nbsp;   var v_obj = Resultado.d;  <br/> &nbsp;&nbsp;  var Tabla_Titulo = ''; <br/> &nbsp;&nbsp;   var Tabla_Datos = ''; <br/> &nbsp;&nbsp;   var Tabla_Fin = ''; <br/> &nbsp;&nbsp;    $('#Resultado_" + v_tabla + "').show();  <br/> &nbsp;&nbsp;    $('#Resultado_" + v_tabla + "').empty();   &nbsp;&nbsp;  ";

             // <xmp>  </xmp>  lo uso para ek html lo muestre como texto pq me mostrana la tabla

             var FcTabla_Titulo = "  <xmp>   Tabla_Titulo =  \"  <table    id='grilla" + v_tabla + "' class='table   table-hover table-striped' style='width: 100%;'><thead><tr><th></th><th></th> ";

             var FcTabla_DATOS = "   &nbsp;&nbsp;  $.each(v_obj, function (index, obj) {   &nbsp;&nbsp; &nbsp;&nbsp;  <xmp>   Tabla_Datos = Tabla_Datos + \" <tr >    <td style='width:30px'> <a class='tooltips' onclick='PopMod_" + v_tabla + "(\"+ obj.Id + \");' > <img src='/images/boton-editar.png' style='background-position:50%; background-repeat:no-repeat;cursor:pointer' /> <span>Editar</span></a></td><td style='width:30px'> <a class='tooltips' onclick='gvDel_" + v_tabla + "(\"+ obj.Id + \");'> <img src='/images/boton-eliminar.png' style='background-position:50%; background-repeat:no-repeat;cursor:pointer'> <span>Eliminar</span></a></td>  ";


        
             
   
 

                  
         


            try
            {
                string cmdTxt = "SELECT * ";
                cmdTxt += "from  " + v_tabla;

                cmm2 = cnn2.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn2.ExecuteReader(cmm2);
                DataTable schemaTable;

                //Retrieve column schema into a DataTable.
                schemaTable = lector.GetSchemaTable();

                string tipo = "";
                string nombre = "";


                string der = "";
                string izq = "";
                int esPrimero = 0;



                string parametros = "";
                foreach (DataRow myField in schemaTable.Rows)
                {
                    //For each property of the field...
                    foreach (DataColumn myProperty in schemaTable.Columns)
                    {

                        if (myProperty.ColumnName == "ColumnName")
                        {
                            nombre = myField[myProperty].ToString();
                            ListaNombre.Add(nombre);

                        }
                        if (myProperty.ColumnName == "DataType")
                        {
                            tipo = myField[myProperty].ToString();
                        }


                        //Display the field name and value.
                    }


                    string ss = "'\"+v_obj." + nombre + ".ToUpper()+\"'";

                    FcTabla_Titulo += "<th>" + nombre + "</th>";
                    FcTabla_DATOS += " <td>\" + obj." + nombre + " + \" </td>";


                    if (esPrimero == 0)
                    {

                        if (nombre == "Id")
                        {
                            esPrimero = 0;
                        }
                        else
                        {
                            der += "(" + nombre;
                            izq += ") values (" + ss + "";
                            up += " " + nombre + "=" + ss + "  ";

                            Ent += "<br/> public string " + nombre + " { get; set; }";

                            sel2 += "<br/>  entidad." + nombre + " = DalModelo."+sVerifStringSql+"(lector, \"" + nombre + "\"); ";
                            parametros += nombre;

                            esPrimero++;
                        }

                    }
                    else
                    {
                        der += ", " + nombre;
                        izq += ", " + ss;
                        parametros += ", " + nombre;
                        up += " , " + nombre + "=" + ss + "  ";
                        sel2 += "<br/>  entidad." + nombre + " = DalModelo." + sVerifStringSql + "(lector, \"" + nombre + "\"); ";
                        Ent += "<br/> public string " + nombre + " { get; set; }";
                    }


                    sal = der + izq + ") ";
                }
                ////////////////////////////////////Json - js

                FcTabla_Titulo += " </tr></thead><tbody> \"; </xmp>  ";

                FcTabla_DATOS += " </tr>\"; </xmp>  &nbsp;&nbsp; });     &nbsp; &nbsp;&nbsp; <xmp> Tabla_Fin = \" </tbody></table>\" ; </xmp>  &nbsp; &nbsp;&nbsp; $('#Resultado_" + v_tabla + "').html(Tabla_Titulo + Tabla_Datos + Tabla_Fin); <br/>  &nbsp; &nbsp;&nbsp; fn_cantidad_" + v_tabla + "(); <br/>  &nbsp; &nbsp;&nbsp; }   <br/> ";
                FcTabla_DATOS += " <br/>  &nbsp; &nbsp;      function fn_cantidad_" + v_tabla + "() { <br/>  &nbsp; &nbsp;&nbsp;    cantidad = $('#grilla" + v_tabla + " tbody').find('tr').length;  <br/>  &nbsp; &nbsp;&nbsp;         $('#span_cantidad').html(cantidad);  <br/>  &nbsp; &nbsp;&nbsp;     };   <br/>  &nbsp; &nbsp; ";               
                fcBuscar += FcTabla_Titulo + FcTabla_DATOS;
                // funcion delete
                fcBuscar += " <br/> <br/> &nbsp; &nbsp;    function gvDel_" + v_tabla + "(ids) { <br/> &nbsp; &nbsp;  &nbsp;  try {  <br/>  &nbsp; &nbsp;    if (confirm('Esta seguro que desea eliminar el registro ?')) { <br/>  &nbsp; &nbsp;   var json = JSON.stringify <br/>  &nbsp; &nbsp;  &nbsp; &nbsp;  ({  <br/>  &nbsp; &nbsp;  &nbsp; &nbsp;     'v_Id': ids  <br/>  &nbsp; &nbsp;  &nbsp; &nbsp;    }); <br/>  &nbsp; &nbsp; ";
                fcBuscar += " <br/>   &nbsp; &nbsp;     $.ajax({  <br/>   &nbsp; &nbsp;   &nbsp; &nbsp;   type: 'POST', <br/>   &nbsp; &nbsp;   &nbsp; &nbsp;   url: '" + MiPage + "/WmGvDel_" + v_tabla + "', <br/>   &nbsp; &nbsp;   &nbsp; &nbsp;   data: json,   contentType: 'application/json; charset=utf-8',  <br/>   &nbsp; &nbsp;   &nbsp; &nbsp;    dataType: 'json',   <br/>   &nbsp; &nbsp;   &nbsp; &nbsp;   success: (function (Result) {  <br/>    &nbsp; &nbsp;      Buscar_" + v_tabla + "() ; <br/>    &nbsp; &nbsp;   &nbsp; &nbsp;  &nbsp;    }), <br/>  &nbsp; &nbsp; error: erroresJs  <br/>  &nbsp; &nbsp;  });    <br/>  &nbsp; &nbsp; &nbsp; } <br/>  &nbsp; &nbsp;    } <br/>  &nbsp; &nbsp; catch (e) <br/>  &nbsp; &nbsp; { <br/>  &nbsp; &nbsp;  &nbsp;  alert(e);  <br/>  &nbsp; &nbsp;  }  <br/>  &nbsp;  } ";
          
                
                // funcion modificar del popup                
                fcBuscar += " <br/> <br/> &nbsp; &nbsp;   function PopMod_" + v_tabla + "(id) { <br/> &nbsp; &nbsp;  &nbsp;  try { <br/>  &nbsp; &nbsp; $(\"#btnModificar1b\").show(); <br/>  &nbsp; &nbsp;   $(\"#btnGuardar1b\").hide(); <br/> &nbsp; &nbsp;  $(\"#TituloPop\").text('Modificar'); <br/> &nbsp; &nbsp;  $('#Modal" + v_tabla + "').modal(); <br/> &nbsp; &nbsp;  $('#" + Master + "TxtId').val(id); <br/> &nbsp; &nbsp; ";
               fcBuscar += " <br/> <br/> &nbsp; &nbsp;  var json = JSON.stringify <br/> &nbsp; &nbsp; &nbsp; &nbsp;   ({  <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ";  
               fcBuscar += " 'v_tipo':'99', <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 'v_valor': id <br/> &nbsp; &nbsp; &nbsp; &nbsp;  });  <br/> &nbsp; &nbsp; &nbsp;   $.ajax({   <br/> &nbsp; &nbsp; &nbsp;       type: 'POST',   <br/> &nbsp; &nbsp; &nbsp;  url: '" + MiPage + "/WsListar_" + v_tabla + "',  <br/> &nbsp; &nbsp; &nbsp;  data: json,   <br/> &nbsp; &nbsp; &nbsp;   contentType: \"application/json; charset=utf-8\",   <br/> &nbsp; &nbsp; &nbsp;       dataType: \"json\",  <br/> &nbsp; &nbsp; &nbsp;       success: function (Resultado) {  <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;   var v_obj = Resultado.d;   <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  $.each(v_obj, function (index, obj) {   &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ";
                 
                string campos_js = "";      string campos_js1 = "";
                 string campos_js2 = "";
                foreach (var item in ListaNombre)
                {
                     campos_js += "   $('#"+Master+"Txt"+item+"').val(obj."+item+");  <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ";   
                  campos_js1 += " <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; '"+item+"':  $('#"+Master+"Txt"+item+"').val(),";
                      campos_js2 += " <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;   $('#"+Master+"Txt"+item+"').val('');";
                                                       
                } 
                fcBuscar += "  <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  " + campos_js + "   <br/> &nbsp; &nbsp; &nbsp; &nbsp;   });   <br/> &nbsp; &nbsp; &nbsp; },  <br/> &nbsp; &nbsp; &nbsp;  error: erroresJs <br/> &nbsp; &nbsp;  });    <br/> &nbsp; &nbsp;    } catch (e) { <br/> &nbsp; &nbsp; alert(e.message);<br/> &nbsp; &nbsp;   } <br/> &nbsp;  } <br/> &nbsp;<br/> &nbsp; ";
             
                  // funcion modificar del popup                
                fcBuscar += " <br/> <br/> &nbsp; &nbsp;  function ModPopJs() {   <br/> &nbsp; &nbsp; &nbsp;  try {  <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  var json = JSON.stringify   <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;   ({ <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp;   'v_obj': {  ";
                                             
           fcBuscar += "   "+ campos_js1.TrimEnd(',') +" <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp;   }  <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;    });  <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; $.ajax({   <br/> &nbsp; &nbsp; &nbsp; &nbsp;    type: \"POST\",    <br/> &nbsp; &nbsp; &nbsp; &nbsp;   url: '" + MiPage + "/WmGvMod_" + v_tabla + "',  <br/> &nbsp; &nbsp; &nbsp; &nbsp;  data: json,   <br/> &nbsp; &nbsp; &nbsp; &nbsp;      contentType:\"application/json; charset=utf-8\", ";

           fcBuscar += "     <br/> &nbsp; &nbsp; &nbsp; &nbsp;       dataType: 'json',  <br/> &nbsp; &nbsp; &nbsp; &nbsp;    success: (function (Result) {   <br/> &nbsp; &nbsp; &nbsp; &nbsp;      $('#Modal" + v_tabla + "').modal('hide');   <br/> &nbsp; &nbsp; &nbsp; &nbsp;      Buscar_" + v_tabla + "();  <br/> &nbsp; &nbsp; &nbsp; &nbsp;      LimpiarPop();  <br/> &nbsp; &nbsp;    }),  <br/> &nbsp; &nbsp; &nbsp; &nbsp;   error: erroresJs   <br/> &nbsp; &nbsp;  }); <br/> &nbsp; &nbsp;     &nbsp;     } catch (e) { <br/> &nbsp; &nbsp; &nbsp; &nbsp;  alert(e.message);  <br/> &nbsp; &nbsp; &nbsp;    }   <br/> &nbsp; &nbsp;   }  <br/> &nbsp; &nbsp;    <br/> &nbsp; &nbsp; &nbsp;   ";

      // funcion LimpiarPop()       
 fcBuscar += " function LimpiarPop() {  <br/> &nbsp; &nbsp; &nbsp; &nbsp;      "+ campos_js2 +"    <br/> &nbsp; &nbsp; &nbsp;    }   <br/> <br/>";
 

 // funcion alta del popup - js             

 fcBuscar += "   <br/> <br/> &nbsp; &nbsp; function  PopAlta_" + v_tabla + "() { <br/> &nbsp; &nbsp;  &nbsp; <br/>  &nbsp; &nbsp;   LimpiarPop();  <br/>  &nbsp; &nbsp;   $(\"#btnModificar1b\").hide();   <br/>  &nbsp; &nbsp;   $(\"#btnGuardar1b\").show();    <br/>  &nbsp; &nbsp;   $(\"#TituloPop\").text('Alta');  <br/>  &nbsp; &nbsp;     $(\"#Modal" + v_tabla + "\").modal();    <br/>  &nbsp; &nbsp;    $(\"#" + Master + "TxtId\").val('0');    <br/>  &nbsp; &nbsp;     $(\"#" + Master + "TxtId\").prop('disabled', true);   <br/>  &nbsp; &nbsp;  &nbsp;   } <br/> <br/>  ";
                 

 // boton modificar del popup                
 fcBuscar += " <br/> <br/> &nbsp; &nbsp;  function AltaPopJs() {   <br/> &nbsp; &nbsp; &nbsp;  try {  <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  var json = JSON.stringify   <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;   ({ <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp;   'v_obj': {  ";

 fcBuscar += "   " + campos_js1.TrimEnd(',') + " <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp;   }  <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;    });  <br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; $.ajax({   <br/> &nbsp; &nbsp; &nbsp; &nbsp;    type: \"POST\",    <br/> &nbsp; &nbsp; &nbsp; &nbsp;   url: '" + MiPage + "/WmGvAlta_" + v_tabla + "',  <br/> &nbsp; &nbsp; &nbsp; &nbsp;  data: json,   <br/> &nbsp; &nbsp; &nbsp; &nbsp;      contentType:\"application/json; charset=utf-8\", ";

 fcBuscar += "     <br/> &nbsp; &nbsp; &nbsp; &nbsp;       dataType: 'json',  <br/> &nbsp; &nbsp; &nbsp; &nbsp;    success: (function (Result) {   <br/> &nbsp; &nbsp; &nbsp; &nbsp;      $('#Modal" + v_tabla + "').modal('hide');   <br/> &nbsp; &nbsp; &nbsp; &nbsp;      Buscar_" + v_tabla + "();  <br/> &nbsp; &nbsp; &nbsp; &nbsp;      LimpiarPop();  <br/> &nbsp; &nbsp;    }),  <br/> &nbsp; &nbsp; &nbsp; &nbsp;   error: erroresJs   <br/> &nbsp; &nbsp;  }); <br/> &nbsp; &nbsp;     &nbsp;     } catch (e) { <br/> &nbsp; &nbsp; &nbsp; &nbsp;  alert(e.message);  <br/> &nbsp; &nbsp; &nbsp;    }   <br/> &nbsp; &nbsp;   }  <br/> &nbsp; &nbsp;    <br/> &nbsp; &nbsp; &nbsp;   ";

                
   
                      ///////////////////////////////////////////////////////////// bll


                Bll = " <br/> <br/>  //////////////////////////////////////////////////// BLL ////////////////////////////////////// <br/> <br/> <br/>  ";

                Bll += "<br/><div>&nbsp;public &nbsp;class Bll" + v_tabla + "</div><div>&nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; private &nbsp;Dal.Dal" + v_tabla + " _mapeador;</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; public Bll" + v_tabla + "()</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; _mapeador = new Dal" + v_tabla + "();</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; private static Bll" + v_tabla + " instancia = null;</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; public static Bll" + v_tabla + " DameInstancia()</div><div>&nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; if (instancia == null)</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; return new Bll" + v_tabla + "();</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; else</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; return instancia;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp;public void Alta_" + v_tabla + "(" + v_tabla + " &nbsp;v_obj)</div><div>&nbsp; &nbsp; &nbsp; &nbsp;<br/> { <br/> &nbsp; &nbsp; try</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;this._mapeador.Alta_" + v_tabla + "(v_obj);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; catch (Exception)</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; throw;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><p>&nbsp; &nbsp; &nbsp; &nbsp; }&nbsp;</p><div>&nbsp; public void Modificacion_" + v_tabla + "(" + v_tabla + " &nbsp;v_obj)</div><div>&nbsp; &nbsp; &nbsp; <br/> { <br/> &nbsp; &nbsp; &nbsp; try</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;this._mapeador.Modificacion_" + v_tabla + "( v_obj);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; catch (Exception)</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; throw;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><p>&nbsp; &nbsp; &nbsp; &nbsp; }&nbsp;</p><p><span id='msg'>&nbsp;public&nbsp;&nbsp;List&lt" + v_tabla + "&gt; Listar_" + v_tabla + "(string  &nbsp; v_tipo,  &nbsp; string  &nbsp; v_valor) </span></p><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<br/> { <br/>  &nbsp; try</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;return &nbsp; &nbsp;this._mapeador.Listar_" + v_tabla + "(  v_tipo, v_valor);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; catch (Exception)</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; throw;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><p>&nbsp; &nbsp; &nbsp; &nbsp; }&nbsp;</p><div>&nbsp; public void Eliminar_" + v_tabla + "(Int32 &nbsp;v_id)</div><div>&nbsp; &nbsp; &nbsp; &nbsp; <br/> { <br/> &nbsp; &nbsp; try</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;this._mapeador.Eliminar_" + v_tabla + "( v_id);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; catch (Exception)</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; throw;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><p>&nbsp; &nbsp; &nbsp; &nbsp; }&nbsp;</p><p>}</p>";



                sel1 = "<p>&nbsp;public&nbsp;&nbsp;List&lt;" + v_tabla + "&gt;&nbsp;&nbsp;Listar_" + v_tabla + "(string  &nbsp; v_tipo,  &nbsp; string  &nbsp; v_valor)</p><div>&nbsp; &nbsp; &nbsp; &nbsp; {&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp;</div><div>&nbsp;List&lt;" + v_tabla + "&gt; lista = new List&lt;" + v_tabla + "&gt;(); &nbsp;<br/>  try &nbsp;<br/> &nbsp; &nbsp;{</div><div>&nbsp;</div><div>&nbsp;&nbsp; &nbsp;</div><div>&nbsp;</div><div>&nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; string cmdTxt = &quot; select Id," + parametros + " from " + v_tabla + "&nbsp; &quot;;</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp;   if (v_tipo == \"99\")  &nbsp; { <br/>    &nbsp; &nbsp; &nbsp;  cmdTxt = &quot; select Id," + parametros + " from " + v_tabla + "&nbsp; where &nbsp; Id='\"+v_valor+\"' &quot;; <br/>  &nbsp; }   <br/>      &nbsp; &nbsp; &nbsp; &nbsp;     if (v_tipo == \"0\")  &nbsp; { <br/>    &nbsp; &nbsp; &nbsp;  cmdTxt = &quot; select Id," + parametros + " from " + v_tabla + "&nbsp;   &quot;; <br/>  &nbsp; }   <br/>      &nbsp; &nbsp; &nbsp; &nbsp;    if (string.IsNullOrEmpty(v_valor))  &nbsp; { <br/>    &nbsp; &nbsp; &nbsp;  cmdTxt = &quot; select Id," + parametros + " from " + v_tabla + "&nbsp;   &quot;; <br/>  &nbsp; }        <br/>  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;cmm = cnn." + ObjNuevaCmm + "(cmdTxt);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " + ObjDataReader + "  lector = cnn.ExecuteReader(cmm);</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; while (lector.Read())</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div>";
                sel1 += "<br/> &nbsp; " + v_tabla + "&nbsp; entidad = new " + v_tabla + "();  ";
                sel1 += "<br/>  entidad.Id = DalModelo."+sVeriIntSql+"(lector, \"Id\"); ";
                sel1 += sel2;
                sel1 += "<br/> lista.Add(entidad); <br/> ";
                sel1 += "<br/> <div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;}</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; lector.Close();</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; catch</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; throw;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; finally</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cnn.Close(cmm);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div></div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</div><div>&nbsp; return lista;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; }</div><br/> ";


                up += "  where id ='\"+v_obj.Id+\"'";
                modificar = "<p>&nbsp;public void Modificacion_" + v_tabla + " (" + v_tabla + " &nbsp;v_obj)</p><div>&nbsp; &nbsp; &nbsp; &nbsp; {&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; try</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;{</div><div>&nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; string cmdTxt = &quot;" + up + "   &nbsp; &quot;;</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;cmm = cnn." + ObjNuevaCmm + "(cmdTxt);</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cnn.ExecuteNonQuery(cmm);</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; catch</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; throw;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; finally</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cnn.Close(cmm);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; }</div>";

                sal = "<p>&nbsp;  " + ObjCnn + "; </p><p>&nbsp;&nbsp; " + ObjCmm + " cmm;</p><p>public void Alta_" + v_tabla + "(" + v_tabla + " &nbsp;v_obj)</p><div>&nbsp; &nbsp; &nbsp; &nbsp; {&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; try</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;{</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; string cmdTxt = &quot; insert into " + v_tabla + " " + der + izq + ") &nbsp;&quot;;</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;cmm = cnn." + ObjNuevaCmm + "(cmdTxt);</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cnn.ExecuteNonQuery(cmm);</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; catch</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; throw;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; finally</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cnn.Close(cmm);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; }</div> <br/><br/>";

                Dal = "////////////////////////////////////////  DAL ////////////////////////////////////////////////";

                Dal += "<br/> <br/> <br/>    public  class Dal" + v_tabla + " <br/> { <br/>" + sal + modificar + sel1 + " <br/> <p>&nbsp;&nbsp; public void Eliminar_" + v_tabla + "(Int32  v_id)<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; try<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; string cmdTxt = &quot;delete from " + v_tabla + "&nbsp; where Id='&quot; + v_id + &quot;'&quot;;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; cmm = cnn." + ObjNuevaCmm + "(cmdTxt);<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; cnn.ExecuteNonQuery(cmm);<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; catch<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; throw;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; finally<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; cnn.Close(cmm);<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>    <p>&nbsp; &nbsp;public Int32 maxId_" + v_tabla + "()</p> <div>&nbsp; &nbsp; &nbsp; &nbsp; {</div> <div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Int32 id = 0;</div> <div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</div> <div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; try</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {&nbsp;</div> <div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; string cmdTxt = &quot; select max(Id) as cant from " + v_tabla + " &nbsp;&quot;;</div> <div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cmm = cnn." + ObjNuevaCmm + "(cmdTxt);</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " + ObjDataReader + "  lector = cnn.ExecuteReader(cmm);</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; while (lector.Read())</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;id = DalModelo." + sVeriIntSql + "(lector, &quot;cant&quot;);&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; lector.Close();</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; catch</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; throw;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; finally</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cnn.Close(cmm);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; return id;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp;</div> }<br/>";

                EntSal = "/////////////////////////////////// BE ///////////////////////////////////////////////////";
                EntSal += " <br/> <br/> <br/>  public  class " + v_tabla + " <br/>     { <br/> " + Ent + "<br/> }";

                //cerrar lector
                lector.Close();

                string mipagina_cs = "  <br/> <br/> <br/>   /////////////////////////////////////// Mipagina.cs - codebihavior    /////////////////////////////////////////";

                mipagina_cs += " <br/> <br/> <br/> <br/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   [WebMethod()]  <br/> <br/> <br/>   public &nbsp; static &nbsp; List&lt" + v_tabla + "&gt; &nbsp; WsListar_" + v_tabla + "(string &nbsp; v_tipo,  &nbsp; string &nbsp; v_valor)  &nbsp;  <br />&nbsp;  ";
                mipagina_cs += " &nbsp;  &nbsp; <br/> {  &nbsp; &nbsp;&nbsp;&nbsp;  <br/>   try   &nbsp;   <br/>   {  &nbsp;   <br/>  &nbsp;  List&lt" + v_tabla + "&gt; &nbsp; list = new &nbsp; List&lt" + v_tabla + "&gt;();  &nbsp;  <br /> &nbsp;  <br />  &nbsp;  list = &nbsp; Bll.Bll" + v_tabla + ".DameInstancia().Listar_" + v_tabla + "(v_tipo, &nbsp; v_valor.ToUpper().Trim()); &nbsp; <br />&nbsp;  ";
                mipagina_cs += " &nbsp;  <br/> &nbsp;&nbsp;&nbsp;&nbsp;  HttpContext.Current.Session[\"Lista\"] = list; <br/> <br/>  &nbsp;&nbsp; return list; &nbsp;   <br/>  &nbsp;&nbsp;  } <br/>  &nbsp;   catch (Exception)  <br/>  &nbsp;  &nbsp;     &nbsp;  <br/>     {  &nbsp;  <br/> &nbsp;  throw;  &nbsp;  <br/>   &nbsp;&nbsp;  &nbsp;  }  &nbsp;  <br/>    &nbsp;    }  &nbsp;  <br/>  ";

                mipagina_cs += " <br/> <br/> <br/> <br/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   [WebMethod()]  <br/> <br/> <br/>    &nbsp; &nbsp; &nbsp;  public   &nbsp; static  &nbsp; void  &nbsp; WmGvDel_" + v_tabla + "(Int32   &nbsp; v_Id)  &nbsp;  <br/>  ";
                mipagina_cs += "      &nbsp;&nbsp;&nbsp; {  <br/>      &nbsp;&nbsp;&nbsp;      try &nbsp;&nbsp;&nbsp;  <br/> <br/>  &nbsp;&nbsp;&nbsp;   {   <br/> <br/>  &nbsp;&nbsp;&nbsp;     Bll.Bll" + v_tabla + ".DameInstancia().Eliminar_" + v_tabla + "(v_Id); <br/> <br/>  &nbsp;&nbsp;&nbsp;     } <br/> <br/>  &nbsp;&nbsp;&nbsp;    catch (Exception)    <br/> <br/>  &nbsp;&nbsp;&nbsp;      {   <br/> <br/>  &nbsp;&nbsp;&nbsp;     throw;   <br/> <br/>  &nbsp;&nbsp;&nbsp;    }  <br/> <br/>  &nbsp;&nbsp;&nbsp;    }  <br/>   ";
      //ws mod
                mipagina_cs += " <br/>   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   [WebMethod()]  <br/> <br/> <br/>    &nbsp; &nbsp; &nbsp;  public static void WmGvMod_" + v_tabla + "(" + v_tabla + " v_obj) <br/> &nbsp;&nbsp;  { <br/> &nbsp;&nbsp;&nbsp;  try <br/> &nbsp;&nbsp;&nbsp;    { <br/> &nbsp;&nbsp;&nbsp; &nbsp;    Bll.Bll" + v_tabla + ".DameInstancia().Modificacion_" + v_tabla + "(v_obj);  <br/> &nbsp;&nbsp;&nbsp;  } <br/> &nbsp;&nbsp;&nbsp;  catch (Exception) <br/> &nbsp;&nbsp;&nbsp;  {<br/> &nbsp;&nbsp;&nbsp; &nbsp;  throw; <br/> &nbsp;&nbsp;&nbsp;  } <br/> &nbsp;&nbsp    } <br/> <br/> &nbsp;&nbsp;&nbsp; ";

                 //ws  alta
                mipagina_cs += " <br/>   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   [WebMethod()]  <br/> <br/> <br/>    &nbsp; &nbsp; &nbsp;  public static void WmGvAlta_" + v_tabla + "(" + v_tabla + " v_obj) <br/> &nbsp;&nbsp;  { <br/> &nbsp;&nbsp;&nbsp;  try <br/> &nbsp;&nbsp;&nbsp;    { <br/> &nbsp;&nbsp;&nbsp; &nbsp;    Bll.Bll" + v_tabla + ".DameInstancia().Alta_" + v_tabla + "(v_obj);  <br/> &nbsp;&nbsp;&nbsp;  } <br/> &nbsp;&nbsp;&nbsp;  catch (Exception) <br/> &nbsp;&nbsp;&nbsp;  {<br/> &nbsp;&nbsp;&nbsp; &nbsp;  throw; <br/> &nbsp;&nbsp;&nbsp;  } <br/> &nbsp;&nbsp    } <br/> <br/> &nbsp;&nbsp;&nbsp; ";
  
   
                Bll += mipagina_cs;

                /////////////////////////////////////////////// html  ////////////////////////////////////////////////////////

                string css = "   <br/> <br/> <br/> <br/>  /////////////////////////////////////////////// html  ////////////////////////////////////////////////////////  <br/><br/><br/><br/>---------------------------------------------------------css ----------------------- <br/>  <br/>  <xmp>   <style> \n     .MarcoMio {   \n  margin-top: 5px;  \n  background-color: #333;  \n  padding: 5px; \n  border-radius: 10px 10px 10px 10px; \n  border: 1px solid; \n  box-shadow: 0 4px 5px #555; \n  color: white;  \n  padding-left: 15px;  \n   }  \n </style> </xmp>      ";

                /////////////////// popup /////////////////////////////////

                // salto de line  \n  cuando se usa  <xmp>


                Modal = css + "  <br/><br/><br/><br/>---------------------------------------------------------pop modal ----------------------- <br/>";
                Modal += "  <br/> <br/>  <xmp> <%--   modal--%>  </xmp> <br/> <br/> <xmp>  <div class='modal fade' id='Modal" + v_tabla + "' role='dialog' style='display:none'  >  \n    <div class='modal-dialog modal-lg'>  \n  <!-- Modal content-->   \n       <div class='modal-content'>  </xmp>           <xmp>        <div class='modal-header'> </xmp><br/>                  <xmp>   <button type='button' class='close' data-dismiss='modal'>&times;</button></xmp> <br/>   <xmp> </div> </xmp> <br/>      <xmp> <div class='modal-body'> </xmp> <br/>   <xmp> <div   style='margin-top:0px;text-transform:uppercase' > </xmp> <br/>    ";
                Modal += "  <br/>                             <xmp> <div class='MarcoMio' id='TituloPop' style='margin-bottom: 5px;'> </div>  </xmp> <br/>  &nbsp; <xmp> <div class='row'  style='margin-left:0px; margin-right:0px;  background-color: #e9eaea; padding:10px;border-radius: 10px 10px 10px 10px;border: 1px solid;box-shadow: 0 4px 5px #555; text-align:center;'>  </xmp>  <br/>                <xmp>   <div class='col-xs-12'> </xmp>   <br/>                  <xmp>  <div class='form-horizontal'>   </xmp>   ";
                  
                ModalFin = "   <xmp> </div>  </xmp> <br/>    <xmp> </div>   </xmp>          <br/> <xmp>  </div> </xmp>    </div>  <br/>     </div>    <xmp>  <div   style='margin-top: 5px; text-align: center;' class='MarcoMio' >  </xmp> <br/>   ";
                ModalFin += "    <xmp>  <button id='btnModificar1b' type='button' class='btn2 btn-default ' onclick='ModPopJs()'  >  </xmp> <br/>  <xmp> <span class=' glyphicon glyphicon-save'></span> </xmp> Modificar <br/>  <xmp> </button>  </xmp>     <xmp>  <button id='btnGuardar1b' type='button' class='btn2 btn-default ' onclick='AltaPopJs()'  >  </xmp> <br/>  <xmp> <span class=' glyphicon glyphicon-save'></span> </xmp> Guardar  <br/>  <xmp> </button>  </xmp>  <br/>    <xmp> <button type='button' class='btn2 btn-danger' data-dismiss='modal'>  </xmp> <br/>  <xmp> <span class=' glyphicon glyphicon-floppy-remove'></span> </xmp> Cerrar  <br/>   <xmp> </button> </xmp> <br/>   <br/>   <xmp> </div> </xmp> <br/>   <xmp> </div>  </xmp>  <br/>  <xmp>        </div> </xmp>  <br/>  <xmp>  </div>   \n      </div>  \n </div>  </xmp> <br/>  &nbsp; &nbsp; &nbsp;";
                string campos_form = "";
                foreach (var item in ListaNombre)
                {
                  campos_form = "<xmp>  <div class='form-group'>  </xmp>  <xmp>    <label for='inputName' class='control-label col-xs-4'> "+item+"</label>  </xmp> ";
                  campos_form += "<xmp>   <div class='col-xs-4'>  </xmp>  <xmp>    <asp:TextBox ID='Txt" + item + "' runat='server' CssClass='form-control'></asp:TextBox> </xmp><xmp>  </div> </xmp><xmp><div class='col-xs-4'></xmp> <xmp></div> </xmp><xmp></div> </xmp>";
                    Modal += campos_form;
                    campos_form = "";
                }

                Modal += ModalFin;


                //////////////////////////////////////////  formulario principal ////////////////////////////////

                string formulario = " --------------------------------------------       formulario principal        -----------------------------------        <xmp> \n <div class='row'>   \n      <div   style='text-align:center;   margin-bottom: 3px;' class='MarcoMio' >    \n <div class='row' > \n     <div class='form-group' >   \n       <div style='color:white;text-align:right; padding-top: 5px;' class='col-xs-3'>   \n    Buscar  \n    </div> \n ";
 formulario += " <div class='col-xs-3' >  \n                    <select id='cmbTipoOp' class='form-control'>       \n                   <option value='0'>Nombre</option>      \n                       </select>      \n   </div> \n       <div class='col-xs-3' >  \n           <asp:TextBox ID='TxtBuscar' runat='server'  class='form-control'></asp:TextBox>   \n ";
 formulario += "  </div>  \n   <div class='col-xs-3'  style='text-align:left'>   \n  <button id='btnBuscarFrm' type='button' class='btn2  btn-default '  onclick='Buscar_" + v_tabla + "() '> \n        <span class=' glyphicon glyphicon-search'></span> Buscar    \n   </button>   \n     </div>  \n            </div>    \n </div>  \n </div>   ";

  formulario += "  <div class='row' style='margin-left:0px; margin-right:0px;   padding:10px;border-radius: 10px 10px 10px 10px;border: 1px solid;box-shadow: 0 4px 5px #555; text-align:center;'>     \n    <div class='col-sm-12' style='margin-top:5px; margin-right: 0px;'> \n              <div id='Resultado_" + v_tabla + "' style='overflow: auto;  height: 220px; width: 100%; '>    \n      <div >    \n     </div>   \n     <table   id='grilla" + v_tabla + "'></table>   \n   </div>   \n   </div>    \n    </div>  \n ";

  formulario += "         <div   style='margin-top:3px;text-align:center;padding-right: 15px; ' class='MarcoMio' >  \n              <button id='btnAgregar1' type='button' class='btn2 btn-default ' onclick='PopAlta_" + v_tabla + "()' >     \n          <span class=' glyphicon glyphicon-floppy-save'></span> Nuevo   \n  </button>  \n         <button id='btnVolver1' type='button' class='btn2  btn-info  '  onclick='window.history.back();'>    \n   ";

    formulario += "     Volver <span class=' glyphicon glyphicon-log-out'></span> \n     </button>   \n            <div  style='color:white;text-align:right;width:100px;float:right'>Registros:    \n      <span id='span_cantidad'>0</span></div>   \n   </div>   \n      </div>  \n   </xmp> ";



    Modal += formulario;


                ////////////////////////////////////////// fin formulario principal ////////////////////////////////

            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }

            return EntSal + "<br/><br/>" + Dal + Bll + "<br/><br/>" + fcBuscar + Modal ;
        }
        public string Ver_modelo(string v_tabla)
        {
          //  v_tabla = "Clientes";
            string up = "update " + v_tabla + " set ";
            string modificar = "";
            string sal = "";
            string sel1 = "";
            string sel2 = "";
            string Ent = "<br/> public Int32 Id { get; set; }";
            string EntSal = "";
            string Bll = "";
            string Dal = "";
            try
            {
                string cmdTxt = "SELECT * ";
                cmdTxt += "from  " + v_tabla;

                cmm = cnn.CrearNuevoComando(cmdTxt);

                SqlDataReader lector = cnn.ExecuteReader(cmm);
                DataTable schemaTable;

                //Retrieve column schema into a DataTable.
                schemaTable = lector.GetSchemaTable();

                string tipo = "";
                string nombre = "";


                string der = "";
                string izq = "";
                int esPrimero = 0;



                string parametros = "";
                foreach (DataRow myField in schemaTable.Rows)
                {
                    //For each property of the field...
                    foreach (DataColumn myProperty in schemaTable.Columns)
                    {

                        if (myProperty.ColumnName == "ColumnName")
                        {
                            nombre = myField[myProperty].ToString();


                        }
                        if (myProperty.ColumnName == "DataType")
                        {
                            tipo = myField[myProperty].ToString();
                        }


                        //Display the field name and value.
                    }
                    string ss = "'\"+v_obj." + nombre + "+\"'";


                    if (esPrimero == 0)
                    {

                        if (nombre == "Id")
                        {
                            esPrimero = 0;
                        }
                        else
                        {
                            der += "(" + nombre;
                            izq += ") values (" + ss + "";
                            up += " " + nombre + "=" + ss + "  ";

                            Ent += "<br/> public string " + nombre + " { get; set; }";

                            sel2 += "<br/>  entidad." + nombre + " = DalModelo.VerifStringSql(lector, \"" + nombre + "\"); ";
                            parametros += nombre;

                            esPrimero++;
                        }

                    }
                    else
                    {
                        der += ", " + nombre;
                        izq += ", " + ss;
                        parametros += ", " + nombre;
                        up += " , " + nombre + "=" + ss + "  ";
                        sel2 += "<br/>  entidad." + nombre + " = DalModelo.VerifStringSql(lector, \"" + nombre + "\"); ";
                        Ent += "<br/> public string " + nombre + " { get; set; }";
                    }
                    sal = der + izq + ") ";
                }

                Bll = "<br/><div>&nbsp;public &nbsp;class Bll" + v_tabla + "</div><div>&nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; private &nbsp;Dal.Dal" + v_tabla + " _mapeador;</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; public Bll" + v_tabla + "()</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; _mapeador = new Dal" + v_tabla + "();</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; private static Bll" + v_tabla + " instancia = null;</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; public static Bll" + v_tabla + " DameInstancia()</div><div>&nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; if (instancia == null)</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; return new Bll" + v_tabla + "();</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; else</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; return instancia;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp;public void Alta_" + v_tabla + "(" + v_tabla + " &nbsp;v_obj)</div><div>&nbsp; &nbsp; &nbsp; &nbsp;<br/> { <br/> &nbsp; &nbsp; try</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;this._mapeador.Alta_" + v_tabla + "(v_obj);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; catch (Exception)</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; throw;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><p>&nbsp; &nbsp; &nbsp; &nbsp; }&nbsp;</p><div>&nbsp; public void Modificacion_" + v_tabla + "(" + v_tabla + " &nbsp;v_obj)</div><div>&nbsp; &nbsp; &nbsp; <br/> { <br/> &nbsp; &nbsp; &nbsp; try</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;this._mapeador.Modificacion_" + v_tabla + "( v_obj);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; catch (Exception)</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; throw;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><p>&nbsp; &nbsp; &nbsp; &nbsp; }&nbsp;</p><p><span id='msg'>&nbsp;public&nbsp;&nbsp;List&lt" + v_tabla + "&gt; Listar_" + v_tabla + "(string  &nbsp; v_tipo,  &nbsp; string  &nbsp; v_valor)</span></p><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<br/> { <br/>  &nbsp; try</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;return &nbsp; &nbsp;this._mapeador.Listar_" + v_tabla + "( v_tipo,  v_valor);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; catch (Exception)</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; throw;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><p>&nbsp; &nbsp; &nbsp; &nbsp; }&nbsp;</p><div>&nbsp; public void Eliminar_" + v_tabla + "(Int32 &nbsp;v_id)</div><div>&nbsp; &nbsp; &nbsp; &nbsp; <br/> { <br/> &nbsp; &nbsp; try</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;this._mapeador.Eliminar_" + v_tabla + "( v_id);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; catch (Exception)</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; throw;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><p>&nbsp; &nbsp; &nbsp; &nbsp; }&nbsp;</p><p>}</p>";



                sel1 = "<p>&nbsp;public&nbsp;&nbsp;List&lt;" + v_tabla + "&gt;&nbsp;&nbsp;Listar_" + v_tabla + "(string  &nbsp; v_tipo,  &nbsp; string  &nbsp; v_valor)</p><div>&nbsp; &nbsp; &nbsp; &nbsp; {&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp;</div><div>&nbsp;List&lt;" + v_tabla + "&gt; lista = new List&lt;" + v_tabla + "&gt;(); &nbsp;<br/>  try &nbsp;<br/> &nbsp; &nbsp;{</div><div>&nbsp;</div><div>&nbsp;&nbsp; &nbsp;</div><div>&nbsp;</div><div>&nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; string cmdTxt = &quot; select Id," + parametros + " from " + v_tabla + "&nbsp;   &quot;;</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp;    if (v_tipo == \"0\")  &nbsp; { <br/>    &nbsp; &nbsp; &nbsp;  cmdTxt = &quot; select Id," + parametros + " from " + v_tabla + "&nbsp;   &quot;; <br/>  &nbsp; }   <br/>      &nbsp; &nbsp; &nbsp; &nbsp;    if (string.IsNullOrEmpty(v_valor))  &nbsp; { <br/>    &nbsp; &nbsp; &nbsp;  cmdTxt = &quot; select Id," + parametros + " from " + v_tabla + "&nbsp;   &quot;; <br/>  &nbsp; }        <br/> <br/>       &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;cmm = cnn.CrearNuevoComando(cmdTxt);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; SqlDataReader lector = cnn.ExecuteReader(cmm);</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; while (lector.Read())</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div>";
                sel1 += "<br/> &nbsp; " + v_tabla + "&nbsp; entidad = new " + v_tabla + "();  ";
                sel1 += "<br/>  entidad.Id = DalModelo.VeriIntSql (lector, \"Id\"); ";
                sel1 += sel2;
                sel1 += "<br/> lista.Add(entidad); <br/> ";
                sel1 += "<br/> <div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;}</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; lector.Close();</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; catch</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; throw;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; finally</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cnn.Close(cmm);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div></div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</div><div>&nbsp; return lista;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; }</div><br/> ";


                up += "  where id ='\"+v_obj.Id+\"'";
                modificar = "<p>&nbsp;public void Modificacion_" + v_tabla + " (" + v_tabla + " &nbsp;v_obj)</p><div>&nbsp; &nbsp; &nbsp; &nbsp; {&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; try</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;{</div><div>&nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; string cmdTxt = &quot;" + up + "   &nbsp; &quot;;</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;cmm = cnn.CrearNuevoComando(cmdTxt);</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cnn.ExecuteNonQuery(cmm);</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; catch</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; throw;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; finally</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cnn.Close(cmm);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; }</div>";

                sal = "<p>&nbsp; private ConectarSqlDB cnn = new ConectarSqlDB();</p><p>&nbsp;&nbsp;SqlCommand cmm;</p><p>public void Alta_" + v_tabla + "(" + v_tabla + " &nbsp;v_obj)</p><div>&nbsp; &nbsp; &nbsp; &nbsp; {&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; try</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;{</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; string cmdTxt = &quot; insert into " + v_tabla + " " + der + izq + ") &nbsp;&quot;;</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;cmm = cnn.CrearNuevoComando(cmdTxt);</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cnn.ExecuteNonQuery(cmm);</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; catch</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; throw;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; finally</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cnn.Close(cmm);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; }</div> <br/><br/>";
                Dal = "public  class Dal" + v_tabla + " <br/> { <br/>" + sal + modificar + sel1 + " <br/> <p>&nbsp;&nbsp; public void Eliminar_" + v_tabla + "(Int32  v_id)<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; try<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; string cmdTxt = &quot;delete from " + v_tabla + "&nbsp; where Id='&quot; + v_id + &quot;'&quot;;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; cmm = cnn.CrearNuevoComando(cmdTxt);<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; cnn.ExecuteNonQuery(cmm);<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; catch<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; throw;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; finally<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; cnn.Close(cmm);<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>    <p>&nbsp; &nbsp;public Int32 maxId_" + v_tabla + "()</p> <div>&nbsp; &nbsp; &nbsp; &nbsp; {</div> <div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Int32 id = 0;</div> <div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</div> <div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; try</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {&nbsp;</div> <div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; string cmdTxt = &quot; select max(Id) as cant from " + v_tabla + " &nbsp;&quot;;</div> <div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cmm = cnn.CrearNuevoComando(cmdTxt);</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; SqlDataReader lector = cnn.ExecuteReader(cmm);</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; while (lector.Read())</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;id = DalModelo.VeriIntSql(lector, &quot;cant&quot;);&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; lector.Close();</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; catch</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; throw;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; finally</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cnn.Close(cmm);</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; return id;</div><div>&nbsp; &nbsp; &nbsp; &nbsp; }</div><div>&nbsp;</div> }<br/>";
                EntSal = " public  class " + v_tabla + " <br/>     { <br/> " + Ent + "<br/> }";

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

            return EntSal + "<br/><br/>" + Dal + Bll  ;
        }

        public static Int32 VeriIntSql( SqlDataReader lector, string s)
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
        public static Double VeriDoubleSql(SqlDataReader lector, string s)
        {

            if (lector[s] == DBNull.Value)
            {

                return 0;
            }
            else
            {

                return Convert.ToDouble(lector[s]);

            }


        }

        public static string VerifStringSql(SqlDataReader lector, string s)
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
        public static string VerifString2Mysql(string s)
        {

            if (s == null)
            {

                return "";
            }
            else
            {

                return Convert.ToString(s);

            }


        }



        public void salida(string v_msg)
        {
            try
            {
                // string cmdTxt = "delete from kx_proveedor  where Id='" + v_id + "'   ";

                string cmdTxt = "insert into sal(msg) values ('" + v_msg + "') ";

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
        public void bitacora(Be.Bitacora bi)
        {
            try
            {
                // string cmdTxt = "delete from kx_proveedor  where Id='" + v_id + "'   ";

                string cmdTxt = "insert into error(msg, donde,valor,aud,config_user,id_user,metodo) values ('" + bi.msg + "','" + bi.donde + "','" + bi.valor + "',now(),'" + bi.config_user + "','" + bi.id_user + "','" + bi.metodo + "') ";

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
       
      
      public static Int32 VeriIntMysql(MySqlDataReader lector, string s)
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
        public static Double VeriDoubleMysql(MySqlDataReader lector, string s)
        {

            if (lector[s] == DBNull.Value)
            {

                return 0;
            }
            else
            {

                return Convert.ToDouble(lector[s]);

            }


        }

        public static string VerifStringMysql(MySqlDataReader lector, string s)
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

      
    }
}
