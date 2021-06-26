﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sport.WsOca {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="#Oca_e_Pak", ConfigurationName="WsOca.Oep_TrackingSoap")]
    public interface Oep_TrackingSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/List_Envios", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet List_Envios(string CUIT, string FechaDesde, string FechaHasta);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/List_Envios", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> List_EnviosAsync(string CUIT, string FechaDesde, string FechaHasta);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/Tracking_Pieza", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Tracking_Pieza(string NroDocumentoCliente, string CUIT, string Pieza);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/Tracking_Pieza", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> Tracking_PiezaAsync(string NroDocumentoCliente, string CUIT, string Pieza);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/Tracking_Pieza_ConIdEstado", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Tracking_Pieza_ConIdEstado(string NumeroEnvio);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/Tracking_Pieza_ConIdEstado", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> Tracking_Pieza_ConIdEstadoAsync(string NumeroEnvio);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/Tracking_PiezaExtendido", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Tracking_PiezaExtendido(string NroDocumentoCliente, string CUIT, string Pieza);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/Tracking_PiezaExtendido", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> Tracking_PiezaExtendidoAsync(string NroDocumentoCliente, string CUIT, string Pieza);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/Tracking_PiezaNumeroEnvio", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Tracking_PiezaNumeroEnvio(string Pieza);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/Tracking_PiezaNumeroEnvio", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> Tracking_PiezaNumeroEnvioAsync(string Pieza);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetEpackUser", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetEpackUser(string usr, string psw);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetEpackUser", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetEpackUserAsync(string usr, string psw);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetUserFromLoginData", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetUserFromLoginData(string LoginData);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetUserFromLoginData", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetUserFromLoginDataAsync(string LoginData);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetCentrosImposicionConServicios", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetCentrosImposicionConServicios();
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetCentrosImposicionConServicios", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Xml.XmlNode> GetCentrosImposicionConServiciosAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetCentrosImposicionConServiciosByCP", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetCentrosImposicionConServiciosByCP(int CodigoPostal);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetCentrosImposicionConServiciosByCP", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Xml.XmlNode> GetCentrosImposicionConServiciosByCPAsync(int CodigoPostal);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetLoginData", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetLoginData(string usr, string psw);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetLoginData", ReplyAction="*")]
        System.Threading.Tasks.Task<string> GetLoginDataAsync(string usr, string psw);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetEPackUserForMail", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetEPackUserForMail(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetEPackUserForMail", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetEPackUserForMailAsync(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/Ordenretiro_CSV2XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Ordenretiro_CSV2XML(string Archivo, string NroCuenta, char[] Separador);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/Ordenretiro_CSV2XML", ReplyAction="*")]
        System.Threading.Tasks.Task<string> Ordenretiro_CSV2XMLAsync(string Archivo, string NroCuenta, char[] Separador);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetORResult", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetORResult(int idCabecera, string Usr, string Psw);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetORResult", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetORResultAsync(int idCabecera, string Usr, string Psw);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetProvincias", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetProvincias();
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetProvincias", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetProvinciasAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetCodigosPostalesXCentroImposicion", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetCodigosPostalesXCentroImposicion(int idCentroImposicion);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetCodigosPostalesXCentroImposicion", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetCodigosPostalesXCentroImposicionAsync(int idCentroImposicion);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetSucursalByProvincia", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetSucursalByProvincia(int idProvincia);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetSucursalByProvincia", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetSucursalByProvinciaAsync(int idProvincia);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetCentrosImposicion", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetCentrosImposicion();
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetCentrosImposicion", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetCentrosImposicionAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetOperativasByUsuario", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetOperativasByUsuario(string usr, string psw);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetOperativasByUsuario", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetOperativasByUsuarioAsync(string usr, string psw);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/IngresoOR", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet IngresoOR(string usr, string psw, string xml_Datos, bool ConfirmarRetiro, int DiasHastaRetiro, int idFranjaHoraria, string ArchivoCliente, string ArchivoProceso);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/IngresoOR", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> IngresoORAsync(string usr, string psw, string xml_Datos, bool ConfirmarRetiro, int DiasHastaRetiro, int idFranjaHoraria, string ArchivoCliente, string ArchivoProceso);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/IngresoORMultiplesRetiros", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet IngresoORMultiplesRetiros(string usr, string psw, string xml_Datos, bool ConfirmarRetiro, string ArchivoCliente, string ArchivoProceso);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/IngresoORMultiplesRetiros", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> IngresoORMultiplesRetirosAsync(string usr, string psw, string xml_Datos, bool ConfirmarRetiro, string ArchivoCliente, string ArchivoProceso);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/DescripcionError", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string DescripcionError(int CodigoError);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/DescripcionError", ReplyAction="*")]
        System.Threading.Tasks.Task<string> DescripcionErrorAsync(int CodigoError);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/AnularOrdenGenerada", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet AnularOrdenGenerada(string usr, string psw, int IdOrdenRetiro);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/AnularOrdenGenerada", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> AnularOrdenGeneradaAsync(string usr, string psw, int IdOrdenRetiro);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetEnvioEstadoActual", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetEnvioEstadoActual(string numeroEnvio, string ordenRetiro);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetEnvioEstadoActual", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetEnvioEstadoActualAsync(string numeroEnvio, string ordenRetiro);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/Tarifar_Envio_Corporativo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Tarifar_Envio_Corporativo(string PesoTotal, string VolumenTotal, int CodigoPostalOrigen, int CodigoPostalDestino, long CantidadPaquetes, string ValorDeclarado, string Cuit, string Operativa);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/Tarifar_Envio_Corporativo", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> Tarifar_Envio_CorporativoAsync(string PesoTotal, string VolumenTotal, int CodigoPostalOrigen, int CodigoPostalDestino, long CantidadPaquetes, string ValorDeclarado, string Cuit, string Operativa);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetReporteRemTramXNumeroTracking", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetReporteRemTramXNumeroTracking(string numeroTracking);
        
        [System.ServiceModel.OperationContractAttribute(Action="#Oca_e_Pak/GetReporteRemTramXNumeroTracking", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetReporteRemTramXNumeroTrackingAsync(string numeroTracking);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface Oep_TrackingSoapChannel : Sport.WsOca.Oep_TrackingSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Oep_TrackingSoapClient : System.ServiceModel.ClientBase<Sport.WsOca.Oep_TrackingSoap>, Sport.WsOca.Oep_TrackingSoap {
        
        public Oep_TrackingSoapClient() {
        }
        
        public Oep_TrackingSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Oep_TrackingSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Oep_TrackingSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Oep_TrackingSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataSet List_Envios(string CUIT, string FechaDesde, string FechaHasta) {
            return base.Channel.List_Envios(CUIT, FechaDesde, FechaHasta);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> List_EnviosAsync(string CUIT, string FechaDesde, string FechaHasta) {
            return base.Channel.List_EnviosAsync(CUIT, FechaDesde, FechaHasta);
        }
        
        public System.Data.DataSet Tracking_Pieza(string NroDocumentoCliente, string CUIT, string Pieza) {
            return base.Channel.Tracking_Pieza(NroDocumentoCliente, CUIT, Pieza);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> Tracking_PiezaAsync(string NroDocumentoCliente, string CUIT, string Pieza) {
            return base.Channel.Tracking_PiezaAsync(NroDocumentoCliente, CUIT, Pieza);
        }
        
        public System.Data.DataSet Tracking_Pieza_ConIdEstado(string NumeroEnvio) {
            return base.Channel.Tracking_Pieza_ConIdEstado(NumeroEnvio);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> Tracking_Pieza_ConIdEstadoAsync(string NumeroEnvio) {
            return base.Channel.Tracking_Pieza_ConIdEstadoAsync(NumeroEnvio);
        }
        
        public System.Data.DataSet Tracking_PiezaExtendido(string NroDocumentoCliente, string CUIT, string Pieza) {
            return base.Channel.Tracking_PiezaExtendido(NroDocumentoCliente, CUIT, Pieza);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> Tracking_PiezaExtendidoAsync(string NroDocumentoCliente, string CUIT, string Pieza) {
            return base.Channel.Tracking_PiezaExtendidoAsync(NroDocumentoCliente, CUIT, Pieza);
        }
        
        public System.Data.DataSet Tracking_PiezaNumeroEnvio(string Pieza) {
            return base.Channel.Tracking_PiezaNumeroEnvio(Pieza);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> Tracking_PiezaNumeroEnvioAsync(string Pieza) {
            return base.Channel.Tracking_PiezaNumeroEnvioAsync(Pieza);
        }
        
        public System.Data.DataSet GetEpackUser(string usr, string psw) {
            return base.Channel.GetEpackUser(usr, psw);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetEpackUserAsync(string usr, string psw) {
            return base.Channel.GetEpackUserAsync(usr, psw);
        }
        
        public System.Data.DataSet GetUserFromLoginData(string LoginData) {
            return base.Channel.GetUserFromLoginData(LoginData);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetUserFromLoginDataAsync(string LoginData) {
            return base.Channel.GetUserFromLoginDataAsync(LoginData);
        }
        
        public System.Xml.XmlNode GetCentrosImposicionConServicios() {
            return base.Channel.GetCentrosImposicionConServicios();
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlNode> GetCentrosImposicionConServiciosAsync() {
            return base.Channel.GetCentrosImposicionConServiciosAsync();
        }
        
        public System.Xml.XmlNode GetCentrosImposicionConServiciosByCP(int CodigoPostal) {
            return base.Channel.GetCentrosImposicionConServiciosByCP(CodigoPostal);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlNode> GetCentrosImposicionConServiciosByCPAsync(int CodigoPostal) {
            return base.Channel.GetCentrosImposicionConServiciosByCPAsync(CodigoPostal);
        }
        
        public string GetLoginData(string usr, string psw) {
            return base.Channel.GetLoginData(usr, psw);
        }
        
        public System.Threading.Tasks.Task<string> GetLoginDataAsync(string usr, string psw) {
            return base.Channel.GetLoginDataAsync(usr, psw);
        }
        
        public System.Data.DataSet GetEPackUserForMail(string email) {
            return base.Channel.GetEPackUserForMail(email);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetEPackUserForMailAsync(string email) {
            return base.Channel.GetEPackUserForMailAsync(email);
        }
        
        public string Ordenretiro_CSV2XML(string Archivo, string NroCuenta, char[] Separador) {
            return base.Channel.Ordenretiro_CSV2XML(Archivo, NroCuenta, Separador);
        }
        
        public System.Threading.Tasks.Task<string> Ordenretiro_CSV2XMLAsync(string Archivo, string NroCuenta, char[] Separador) {
            return base.Channel.Ordenretiro_CSV2XMLAsync(Archivo, NroCuenta, Separador);
        }
        
        public System.Data.DataSet GetORResult(int idCabecera, string Usr, string Psw) {
            return base.Channel.GetORResult(idCabecera, Usr, Psw);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetORResultAsync(int idCabecera, string Usr, string Psw) {
            return base.Channel.GetORResultAsync(idCabecera, Usr, Psw);
        }
        
        public System.Data.DataSet GetProvincias() {
            return base.Channel.GetProvincias();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetProvinciasAsync() {
            return base.Channel.GetProvinciasAsync();
        }
        
        public System.Data.DataSet GetCodigosPostalesXCentroImposicion(int idCentroImposicion) {
            return base.Channel.GetCodigosPostalesXCentroImposicion(idCentroImposicion);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetCodigosPostalesXCentroImposicionAsync(int idCentroImposicion) {
            return base.Channel.GetCodigosPostalesXCentroImposicionAsync(idCentroImposicion);
        }
        
        public System.Data.DataSet GetSucursalByProvincia(int idProvincia) {
            return base.Channel.GetSucursalByProvincia(idProvincia);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetSucursalByProvinciaAsync(int idProvincia) {
            return base.Channel.GetSucursalByProvinciaAsync(idProvincia);
        }
        
        public System.Data.DataSet GetCentrosImposicion() {
            return base.Channel.GetCentrosImposicion();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetCentrosImposicionAsync() {
            return base.Channel.GetCentrosImposicionAsync();
        }
        
        public System.Data.DataSet GetOperativasByUsuario(string usr, string psw) {
            return base.Channel.GetOperativasByUsuario(usr, psw);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetOperativasByUsuarioAsync(string usr, string psw) {
            return base.Channel.GetOperativasByUsuarioAsync(usr, psw);
        }
        
        public System.Data.DataSet IngresoOR(string usr, string psw, string xml_Datos, bool ConfirmarRetiro, int DiasHastaRetiro, int idFranjaHoraria, string ArchivoCliente, string ArchivoProceso) {
            return base.Channel.IngresoOR(usr, psw, xml_Datos, ConfirmarRetiro, DiasHastaRetiro, idFranjaHoraria, ArchivoCliente, ArchivoProceso);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> IngresoORAsync(string usr, string psw, string xml_Datos, bool ConfirmarRetiro, int DiasHastaRetiro, int idFranjaHoraria, string ArchivoCliente, string ArchivoProceso) {
            return base.Channel.IngresoORAsync(usr, psw, xml_Datos, ConfirmarRetiro, DiasHastaRetiro, idFranjaHoraria, ArchivoCliente, ArchivoProceso);
        }
        
        public System.Data.DataSet IngresoORMultiplesRetiros(string usr, string psw, string xml_Datos, bool ConfirmarRetiro, string ArchivoCliente, string ArchivoProceso) {
            return base.Channel.IngresoORMultiplesRetiros(usr, psw, xml_Datos, ConfirmarRetiro, ArchivoCliente, ArchivoProceso);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> IngresoORMultiplesRetirosAsync(string usr, string psw, string xml_Datos, bool ConfirmarRetiro, string ArchivoCliente, string ArchivoProceso) {
            return base.Channel.IngresoORMultiplesRetirosAsync(usr, psw, xml_Datos, ConfirmarRetiro, ArchivoCliente, ArchivoProceso);
        }
        
        public string DescripcionError(int CodigoError) {
            return base.Channel.DescripcionError(CodigoError);
        }
        
        public System.Threading.Tasks.Task<string> DescripcionErrorAsync(int CodigoError) {
            return base.Channel.DescripcionErrorAsync(CodigoError);
        }
        
        public System.Data.DataSet AnularOrdenGenerada(string usr, string psw, int IdOrdenRetiro) {
            return base.Channel.AnularOrdenGenerada(usr, psw, IdOrdenRetiro);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> AnularOrdenGeneradaAsync(string usr, string psw, int IdOrdenRetiro) {
            return base.Channel.AnularOrdenGeneradaAsync(usr, psw, IdOrdenRetiro);
        }
        
        public System.Data.DataSet GetEnvioEstadoActual(string numeroEnvio, string ordenRetiro) {
            return base.Channel.GetEnvioEstadoActual(numeroEnvio, ordenRetiro);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetEnvioEstadoActualAsync(string numeroEnvio, string ordenRetiro) {
            return base.Channel.GetEnvioEstadoActualAsync(numeroEnvio, ordenRetiro);
        }
        
        public System.Data.DataSet Tarifar_Envio_Corporativo(string PesoTotal, string VolumenTotal, int CodigoPostalOrigen, int CodigoPostalDestino, long CantidadPaquetes, string ValorDeclarado, string Cuit, string Operativa) {
            return base.Channel.Tarifar_Envio_Corporativo(PesoTotal, VolumenTotal, CodigoPostalOrigen, CodigoPostalDestino, CantidadPaquetes, ValorDeclarado, Cuit, Operativa);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> Tarifar_Envio_CorporativoAsync(string PesoTotal, string VolumenTotal, int CodigoPostalOrigen, int CodigoPostalDestino, long CantidadPaquetes, string ValorDeclarado, string Cuit, string Operativa) {
            return base.Channel.Tarifar_Envio_CorporativoAsync(PesoTotal, VolumenTotal, CodigoPostalOrigen, CodigoPostalDestino, CantidadPaquetes, ValorDeclarado, Cuit, Operativa);
        }
        
        public System.Data.DataSet GetReporteRemTramXNumeroTracking(string numeroTracking) {
            return base.Channel.GetReporteRemTramXNumeroTracking(numeroTracking);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetReporteRemTramXNumeroTrackingAsync(string numeroTracking) {
            return base.Channel.GetReporteRemTramXNumeroTrackingAsync(numeroTracking);
        }
    }
}
