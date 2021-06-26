using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
    public class 
        turnos
    {
        public int id { get; set; }
        public string hora { get; set; }
        public string fecha { get; set; }
        public string OBRASOCIAL { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public string CUIT { get; set; }

        public string MAIL { get; set; }

        public string FECHA { get; set; }
        public string TEL { get; set; }

        public string DOCUMENTO { get; set; }
        public string SEXO { get; set; }
        public string DOMICILIO { get; set; }

        public string SERVICIO { get; set; }
        public string PROFESIONAL { get; set; }

        public string OBS { get; set; }
        public string FOTO { get; set; }

        public string OBS_Pac { get; set; }
        public string Fecha_Nac { get; set; }
        public string Pago { get; set; }
        public string Copago { get; set; }
        public string Pago_Obs { get; set; }
        public string Vino { get; set; }
        public string Vino_aud { get; set; }

        public string HC { get; set; }
        public string Visto_Hc { get; set; }
        
        public string esPaciente{ get; set; }



        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Dpto { get; set; }
        public Int32 Pais { get; set; }
        public string Cp { get; set; }
        public Int32 Provincia { get; set; }
        public Int32 Departamento { get; set; }
        public Int32 Distrito { get; set; }
        public string NroOS { get; set; }

        public string Consulta { get; set; }
        public string Practica { get; set; }
    }

    public class hosp_hc_files
    {

        public Int32 Id { get; set; }
        public string Paciente { get; set; }
        public string Descripción { get; set; }
        public string Aud { get; set; }
        public string Path { get; set; }
        public string tipo { get; set; }
    }



    public class hosp_examen_vestibular
    {

     
        public string id { get; set; }
        public string motivo { get; set; }
        public string definicion_sub { get; set; }
        public string romberg { get; set; }
        public string unterberger { get; set; }
        public string test_dix_hall { get; set; }
        public string dedo_nariz { get; set; }
        public string adiadococinesia { get; set; }

        public string aud{ get; set; }

        public string Id_pac { get; set; }
        public string Especialista { get; set; }
    }
     
    public class hosp_obra_sociales
    { 
        public Int32 Id { get; set; }
        public string Nombre { get; set; }
    }


}
