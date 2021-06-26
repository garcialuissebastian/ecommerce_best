using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
    class Cuit_afip
    {


    }


    public class DomicilioFiscal
    {
        public string direccion { get; set; }
        public string codPostal { get; set; }
        public int idProvincia { get; set; }
    }

    public class Data
    {
        public long idPersona { get; set; }
        public string tipoPersona { get; set; }
        public string tipoClave { get; set; }
        public string estadoClave { get; set; }
        public string nombre { get; set; }
        public DomicilioFiscal domicilioFiscal { get; set; }
        public int idDependencia { get; set; }
        public int mesCierre { get; set; }
        public string fechaInscripcion { get; set; }
        public string fechaContratoSocial { get; set; }
        public List<int> impuestos { get; set; }
        public List<int> actividades { get; set; }
        public List<int> caracterizaciones { get; set; }
    }

    public class RootObject
    {
        public bool success { get; set; }
        public Data data { get; set; }
    }
}
