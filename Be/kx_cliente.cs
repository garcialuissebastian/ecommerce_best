using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
    public class kx_cliente
    {

        public Int32 Id { get; set; }
        public string Cod_Manual { get; set; }
        public string Fecha_Nac { get; set; }
        
        public string Tipo_Doc { get; set; }
        public string Doc_No { get; set; }
        public string Iva { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Observacion { get; set; }
        public string Cta_contable { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Dpto { get; set; }
        public string Pais { get; set; }
        public string Cp { get; set; }
        public string Provincia { get; set; }
        public string Departamento { get; set; }
        public string Distrito { get; set; }
        public string Contac_Nomb { get; set; }
        public string Contac_Tel { get; set; }
        public string Contac_Cel { get; set; }
        public string Contac_Mail { get; set; }
        public string Emp_Tel { get; set; }
        public string Emp_Cel { get; set; }
        public string Emp_Mail { get; set; }
        public string Emp_Web { get; set; }

        public string ListaP { get; set; }
        public string Id_Usuario { get; set; }
        public string Id_Config { get; set; }


        public string User_Usuario { get; set; }
        public string Pass_Usuario { get; set; }
        public string Deposito_Usuario { get; set; }
        public string Roles_Usuario { get; set; }
    }
}
