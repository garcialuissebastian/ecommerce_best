'<summary>
'EJEMPLO - Consola cliente del webservice de autenticacion y autorizacion
'</summary>
'<description>
'   Consume el metodo LoginCms del WSAA
'   Muestra en stdout el login ticket response
'</description>
'<version>10.09.30.1800</version>
'<usage>
'   clientelogincms (opciones) ...
'   opciones:
'      -s servicio      ID del servicio de negocio
'      -w url           URL del WSDL del WSAA
'      -c certificado   Ruta del certificado (con clave privada)
'      -p pasword       Password del certificado (con clave privada)
'      -x proxy         IP:port del proxy
'      -y usuario proxy Usuario del proxy
'      -z pass proxy    Password del proxy
'      -v on|off        Salida detallada on/off
'      -?               Muestra ayuda de uso
'</usage>
'<platform>.NET Framework 2.0</platform>
'<disclaimer>
' El Departamento de Arquitectura Informatica de la AFIP (DeArIn/AFIP), pone a disposicion
' el siguiente codigo para su utilizacion con el WebService de Facturacion Electronica (WSFE)
' de la AFIP.
'
' El mismo no puede ser re-distribuido, publicado o descargado en forma total o parcial, ya sea
' en forma electronica, mecanica u optica, sin la autorizacion de DeArIn/AFIP. El uso no
' autorizado del mismo esta prohibido.
'
' DeArIn/AFIP no asume ninguna responsabilidad de los errores que pueda contener el codigo ni la
' obligacion de subsanar dichos errores o informar de la existencia de los mismos.
'
' DeArIn/AFIP no asume ninguna responsabilidad que surja de la utilizacion del codigo, ya sea por
' utilizacion ilegal de patentes, perdida de beneficios, perdida de informacion o cualquier otro
' inconveniente.
'
' Bajo ninguna circunstancia DeArIn/AFIP podra ser indicada como responsable por consecuencias y/o
' incidentes ya sean directos o indirectos que puedan surgir de la utilizacion del codigo.
'
' DeArIn/AFIP no da ninguna garantia, expresa o implicita, de la utilidad del codigo, si el mismo es
' correcto, o si cumple con los requerimientos de algun proposito en particular.
'
' DeArIn/AFIP puede realizar cambios en cualquier momento en el codigo sin previo aviso.
'
' El codigo debera ser evaluado, verificado, corregido y/o adaptado por personal tecnico calificado
' de las entidades que lo utilicen.
'
' EL SIGUIENTE CODIGO ES DISTRIBUIDO PARA EVALUACION, CON TODOS SUS ERRORES Y OMISIONES. LA
' RESPONSABILIDAD DEL CORRECTO FUNCIONAMIENTO DEL MISMO YA SEA POR SI SOLO O COMO PARTE DE
' OTRA APLICACION, QUEDA A CARGO DE LAS ENTIDADES QUE LO UTILICEN. LA UTILIZACION DEL CODIGO
' SIGNIFICA LA ACEPTACION DE TODOS LOS TERMINOS Y CONDICIONES MENCIONADAS ANTERIORMENTE.
'
' DeArIn-AFIP
'</disclaimer>

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Xml
Imports System.Net
Imports System.Web



Imports System.Security
Imports System.Security.Cryptography
Imports System.Security.Cryptography.Pkcs
Imports System.Security.Cryptography.X509Certificates
Imports System.IO
Imports System.Runtime.InteropServices


''' <summary>
''' Clase para crear objetos Login Tickets
''' </summary>
''' <remarks>
''' Ver documentacion: 
'''    Especificacion Tecnica del Webservice de Autenticacion y Autorizacion
'''    Version 1.0
'''    Departamento de Seguridad Informatica - AFIP
''' </remarks>
Class LoginTicket

    Public UniqueId As UInt32 ' Entero de 32 bits sin signo que identifica el requerimiento
    Public GenerationTime As DateTime ' Momento en que fue generado el requerimiento
    Public ExpirationTime As DateTime ' Momento en el que exoira la solicitud
    Public Service As String ' Identificacion del WSN para el cual se solicita el TA
    Public Sign As String ' Firma de seguridad recibida en la respuesta
    Public Token As String ' Token de seguridad recibido en la respuesta

    Public XmlLoginTicketRequest As XmlDocument = Nothing
    Public XmlLoginTicketResponse As XmlDocument = Nothing
    Public RutaDelCertificadoFirmante As String
    Public XmlStrLoginTicketRequestTemplate As String = "<loginTicketRequest><header><uniqueId></uniqueId><generationTime></generationTime><expirationTime></expirationTime></header><service></service></loginTicketRequest>"

    Private _verboseMode As Boolean = True

    Private Shared _globalUniqueID As UInt32 = 0 ' OJO! NO ES THREAD-SAFE

    Const DEFAULT_URLWSAAWSDL As String = "https://wsaa.afip.gov.ar/ws/services/LoginCms?wsdl" 'no lo usa por usa el ws
    Const DEFAULT_SERVICIO As String = "wsfe"
    Const DEFAULT_CERTSIGNER As String = "c:\\cert.pfx"
    Const DEFAULT_PROXY As String = Nothing
    Const DEFAULT_PROXY_USER As String = Nothing
    Const DEFAULT_PROXY_PASSWORD As String = ""
    Const DEFAULT_VERBOSE As Boolean = True
    Const DEFAULT_CERTIFICADO_PASSWORD As String = ""
    Public SAL_TK As String = ""
    ''' <summary>
    ''' Construye un Login Ticket obtenido del WSAA
    ''' </summary>
    ''' <param name="argServicio">Servicio al que se desea acceder</param>
    ''' <param name="argUrlWsaa">URL del WSAA</param>
    ''' <param name="argRutaCertX509Firmante">Ruta del certificado X509 (con clave privada) usado para firmar</param>
    ''' <param name="argPassword">Password del certificado X509 (con clave privada) usado para firmar</param>
    ''' <param name="argProxy">IP:port del proxy</param>
    ''' <param name="argProxyUser">Usuario del proxy</param>''' 
    ''' <param name="argProxyPassword">Password del proxy</param>
    ''' <param name="argVerbose">Nivel detallado de descripcion? true/false</param>
    ''' <remarks></remarks>
    Public Function ObtenerLoginTicketResponse(
    ByVal argServicio As String,
    ByVal argUrlWsaa As String,
    ByVal argRutaCertX509Firmante As String,
    ByVal argPassword As SecureString,
    ByVal argProxy As String,
    ByVal argProxyUser As String,
    ByVal argProxyPassword As String,
    ByVal argVerbose As Boolean, ByVal path As String
    ) As String

        Me.RutaDelCertificadoFirmante = argRutaCertX509Firmante
        Me._verboseMode = argVerbose
        CertificadosX509Lib.VerboseMode = argVerbose

        Dim cmsFirmadoBase64 As String
        Dim loginTicketResponse As String
        Dim xmlNodoUniqueId As XmlNode
        Dim xmlNodoGenerationTime As XmlNode
        Dim xmlNodoExpirationTime As XmlNode
        Dim xmlNodoService As XmlNode

        ' PASO 1: Genero el Login Ticket Request
        Try
            _globalUniqueID += 1

            XmlLoginTicketRequest = New XmlDocument()
            XmlLoginTicketRequest.LoadXml(XmlStrLoginTicketRequestTemplate)

            xmlNodoUniqueId = XmlLoginTicketRequest.SelectSingleNode("//uniqueId")
            xmlNodoGenerationTime = XmlLoginTicketRequest.SelectSingleNode("//generationTime")
            xmlNodoExpirationTime = XmlLoginTicketRequest.SelectSingleNode("//expirationTime")
            xmlNodoService = XmlLoginTicketRequest.SelectSingleNode("//service")

            xmlNodoGenerationTime.InnerText = DateTime.Now.AddMinutes(-10).ToString("s")
            xmlNodoExpirationTime.InnerText = DateTime.Now.AddMinutes(+10).ToString("s")
            xmlNodoUniqueId.InnerText = CStr(_globalUniqueID)
            xmlNodoService.InnerText = argServicio
            Me.Service = argServicio

            ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)

            If Me._verboseMode Then
                Console.WriteLine(XmlLoginTicketRequest.OuterXml)
            End If

        Catch excepcionAlGenerarLoginTicketRequest As Exception
            Throw New Exception("***Error GENERANDO el LoginTicketRequest : " + excepcionAlGenerarLoginTicketRequest.Message + excepcionAlGenerarLoginTicketRequest.StackTrace)
        End Try

        ' PASO 2: Firmo el Login Ticket Request
        Try
            If Me._verboseMode Then
                Console.WriteLine("***Leyendo certificado: {0}", RutaDelCertificadoFirmante)
            End If

            Dim certFirmante As X509Certificate2 = CertificadosX509Lib.ObtieneCertificadoDesdeArchivo(RutaDelCertificadoFirmante, argPassword)




            If Me._verboseMode Then
                Console.WriteLine("***Firmando: ")
                Console.WriteLine(XmlLoginTicketRequest.OuterXml)
            End If

            ' Convierto el login ticket request a bytes, para firmar
            Dim EncodedMsg As Encoding = Encoding.UTF8
            Dim msgBytes As Byte() = EncodedMsg.GetBytes(XmlLoginTicketRequest.OuterXml)

            ' Firmo el msg y paso a Base64
            Dim encodedSignedCms As Byte() = CertificadosX509Lib.FirmaBytesMensaje(msgBytes, certFirmante)
            cmsFirmadoBase64 = Convert.ToBase64String(encodedSignedCms)

        Catch excepcionAlFirmar As Exception
            Throw New Exception("***Error FIRMANDO el LoginTicketRequest : " + excepcionAlFirmar.Message)
        End Try

        ' PASO 3: Invoco al WSAA para obtener el Login Ticket Response
        Try
            If Me._verboseMode Then
                Console.WriteLine("***Llamando al WSAA en URL: {0}", argUrlWsaa)
                Console.WriteLine("***Argumento en el request:")
                Console.WriteLine(cmsFirmadoBase64)
            End If

            ''''''''''''''''''''' Instancio el ws desa o produc

            '    factElectForm.WSLogin.LoginCMSClient cc = new factElectForm.WSLogin.LoginCMSClient();  ' desarrollo


            Dim servicioWsaa As New VbWsaa.WSLogin.LoginCMSClient  ' homo

            '  Dim servicioWsaa As New VbWsaa.WSLoginProduc.LoginCMSClient ' Produc



            loginTicketResponse = servicioWsaa.loginCms(cmsFirmadoBase64)

            If Me._verboseMode Then
                Console.WriteLine("***LoguinTicketResponse: ")
                Console.WriteLine(loginTicketResponse)
            End If

        Catch excepcionAlInvocarWsaa As Exception
            Throw New Exception("***Error INVOCANDO al servicio WSAA : " + excepcionAlInvocarWsaa.Message)
        End Try


        ' PASO 4: Analizo el Login Ticket Response recibido del WSAA
        Try
            XmlLoginTicketResponse = New XmlDocument()
            XmlLoginTicketResponse.LoadXml(loginTicketResponse)
            XmlLoginTicketResponse.Save(path + "\xml\\loginTicketResponse.xml")


            Me.UniqueId = UInt32.Parse(XmlLoginTicketResponse.SelectSingleNode("//uniqueId").InnerText)
            Me.GenerationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//generationTime").InnerText)
            Me.ExpirationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//expirationTime").InnerText)
            Me.Sign = XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText
            Me.Token = XmlLoginTicketResponse.SelectSingleNode("//token").InnerText
        Catch excepcionAlAnalizarLoginTicketResponse As Exception
            Throw New Exception("***Error ANALIZANDO el LoginTicketResponse : " + excepcionAlAnalizarLoginTicketResponse.Message)
        End Try

        Return loginTicketResponse

    End Function



    Public Function ObtenerLoginTicketResponseProduccion(
  ByVal argServicio As String,
  ByVal argUrlWsaa As String,
  ByVal argRutaCertX509Firmante As String,
  ByVal argPassword As SecureString,
  ByVal argProxy As String,
  ByVal argProxyUser As String,
  ByVal argProxyPassword As String,
  ByVal argVerbose As Boolean,
  ByVal argPath As String,
   ByVal argSal As String, ByVal cerfirmado As X509Certificate2
  ) As String

        Me.RutaDelCertificadoFirmante = argRutaCertX509Firmante
        Me._verboseMode = argVerbose
        CertificadosX509Lib.VerboseMode = argVerbose
        Dim sal As String = "nada"

        Dim cmsFirmadoBase64 As String
        Dim loginTicketResponse As String
        Dim xmlNodoUniqueId As XmlNode
        Dim xmlNodoGenerationTime As XmlNode
        Dim xmlNodoExpirationTime As XmlNode
        Dim xmlNodoService As XmlNode

        ' PASO 1: Genero el Login Ticket Request
        Try
            _globalUniqueID += 1

            XmlLoginTicketRequest = New XmlDocument()
            XmlLoginTicketRequest.LoadXml(XmlStrLoginTicketRequestTemplate)

            xmlNodoUniqueId = XmlLoginTicketRequest.SelectSingleNode("//uniqueId")
            xmlNodoGenerationTime = XmlLoginTicketRequest.SelectSingleNode("//generationTime")
            xmlNodoExpirationTime = XmlLoginTicketRequest.SelectSingleNode("//expirationTime")
            xmlNodoService = XmlLoginTicketRequest.SelectSingleNode("//service")

            xmlNodoGenerationTime.InnerText = DateTime.Now.AddMinutes(-10).ToString("s")
            xmlNodoExpirationTime.InnerText = DateTime.Now.AddMinutes(+10).ToString("s")
            xmlNodoUniqueId.InnerText = CStr(_globalUniqueID)
            xmlNodoService.InnerText = argServicio
            Me.Service = argServicio
            ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)

            If Me._verboseMode Then
                Console.WriteLine(XmlLoginTicketRequest.OuterXml)
            End If

        Catch excepcionAlGenerarLoginTicketRequest As Exception
            Throw New Exception("***Error GENERANDO el LoginTicketRequest : " + excepcionAlGenerarLoginTicketRequest.Message + excepcionAlGenerarLoginTicketRequest.StackTrace)
        End Try

        ' PASO 2: Firmo el Login Ticket Request
        Try
            If Me._verboseMode Then
                Console.WriteLine("***Leyendo certificado: {0}", RutaDelCertificadoFirmante)
            End If

            '  Dim certFirmante As X509Certificate2 = CertificadosX509Lib.ObtieneCertificadoDesdeArchivo(RutaDelCertificadoFirmante, argPassword)
            ' tiraba un error como este: System.Security.Cryptography.CryptographicException: An internal error occurred. at System.Security.Cryptography.CryptographicException.ThrowCryptographicException(Int32 hr)


            ' original:  Dim certFirmante As X509Certificate2 = CertificadosX509Lib.ObtieneCertificadoDesdeArchivo(RutaDelCertificadoFirmante, argPassword)


            'este cod m salvo

            '      RutaDelCertificadoFirmante = HttpContext.Current.Server.MapPath("BestProduc.p12")





            Dim cp As New CspParameters()
            cp.KeyContainerName = "BestProduc"
            ' instantiates the rsa instance accessing the key container MyKeyContainerName
            Dim rsa As New RSACryptoServiceProvider(cp)
            ' add the below line to delete the key entry in MyKeyContainerName
            ' rsa.PersistKeyInCsp = false;
            'writes out the current key pair used in the rsa instance
            Console.WriteLine("Key is : " & vbLf & "" & rsa.ToXmlString(True))




            ' si no puede firmalo desde vb lo envio dese c#
            Dim certFirmante As New X509Certificate2
            If cerfirmado Is Nothing Then
                certFirmante = New X509Certificate2(System.IO.File.ReadAllBytes(RutaDelCertificadoFirmante), argPassword,
                (X509KeyStorageFlags.MachineKeySet Or X509KeyStorageFlags.Exportable Or X509KeyStorageFlags.PersistKeySet))
            Else
                certFirmante = cerfirmado
            End If

            certFirmante = cerfirmado


            If Me._verboseMode Then
                Console.WriteLine("***Firmando: ")
                Console.WriteLine(XmlLoginTicketRequest.OuterXml)
            End If

            ' Convierto el login ticket request a bytes, para firmar
            Dim EncodedMsg As Encoding = Encoding.UTF8
            Dim msgBytes As Byte() = EncodedMsg.GetBytes(XmlLoginTicketRequest.OuterXml)

            ' Firmo el msg y paso a Base64
            Dim encodedSignedCms As Byte() = CertificadosX509Lib.FirmaBytesMensaje(msgBytes, certFirmante)
            cmsFirmadoBase64 = Convert.ToBase64String(encodedSignedCms)

        Catch excepcionAlFirmar As Exception
            Throw New Exception("***Error FIRMANDO el LoginTicketRequest :   " + excepcionAlFirmar.Message + " " + RutaDelCertificadoFirmante)
        End Try

        ' PASO 3: Invoco al WSAA para obtener el Login Ticket Response
        Try
            If Me._verboseMode Then
                Console.WriteLine("***Llamando al WSAA en URL: {0}", argUrlWsaa)
                Console.WriteLine("***Argumento en el request:")
                Console.WriteLine(cmsFirmadoBase64)
            End If

            ''''''''''''''''''''' Instancio el ws desa o produc

            '    factElectForm.WSLogin.LoginCMSClient cc = new factElectForm.WSLogin.LoginCMSClient();  ' desarrollo


            Dim servicioWsaa As New VbWsaa.WSLoginProduc.LoginCMSClient ' Produc



            loginTicketResponse = servicioWsaa.loginCms(cmsFirmadoBase64)

            If Me._verboseMode Then
                Console.WriteLine("***LoguinTicketResponse: ")
                Console.WriteLine(loginTicketResponse)
            End If

        Catch excepcionAlInvocarWsaa As Exception
            Throw New Exception("***Error INVOCANDO al servicio WSAA : " + excepcionAlInvocarWsaa.Message)
        End Try


        ' PASO 4: Analizo el Login Ticket Response recibido del WSAA
        Try
            XmlLoginTicketResponse = New XmlDocument()
            XmlLoginTicketResponse.LoadXml(loginTicketResponse)
            ' XmlLoginTicketResponse.Save(argPath + "xml\\loginTicketResponseProduccion.xml")
            XmlLoginTicketResponse.Save(argSal)

            Me.UniqueId = UInt32.Parse(XmlLoginTicketResponse.SelectSingleNode("//uniqueId").InnerText)
            Me.GenerationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//generationTime").InnerText)
            Me.ExpirationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//expirationTime").InnerText)
            Me.Sign = XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText
            Me.Token = XmlLoginTicketResponse.SelectSingleNode("//token").InnerText
        Catch excepcionAlAnalizarLoginTicketResponse As Exception
            Throw New Exception("***Error ANALIZANDO el LoginTicketResponseProduccion : " + excepcionAlAnalizarLoginTicketResponse.Message)
        End Try

        Return loginTicketResponse

    End Function


End Class

''' <summary>
''' Libreria de utilidades para manejo de certificados
''' </summary>
''' <remarks></remarks>
Class CertificadosX509Lib

    Public Shared VerboseMode As Boolean = False

    ''' <summary>
    ''' Firma mensaje
    ''' </summary>
    ''' <param name="argBytesMsg">Bytes del mensaje</param>
    ''' <param name="argCertFirmante">Certificado usado para firmar</param>
    ''' <returns>Bytes del mensaje firmado</returns>
    ''' <remarks></remarks>
    Public Shared Function FirmaBytesMensaje(
    ByVal argBytesMsg As Byte(),
    ByVal argCertFirmante As X509Certificate2
    ) As Byte()
        Try
            ' Pongo el mensaje en un objeto ContentInfo (requerido para construir el obj SignedCms)
            Dim infoContenido As New ContentInfo(argBytesMsg)
            Dim cmsFirmado As New SignedCms(infoContenido)

            ' Creo objeto CmsSigner que tiene las caracteristicas del firmante
            Dim cmsFirmante As New CmsSigner(argCertFirmante)
            cmsFirmante.IncludeOption = X509IncludeOption.EndCertOnly

            If VerboseMode Then
                Console.WriteLine("***Firmando bytes del mensaje...")
            End If
            ' Firmo el mensaje PKCS #7
            cmsFirmado.ComputeSignature(cmsFirmante)

            If VerboseMode Then
                Console.WriteLine("***OK mensaje firmado")
            End If

            ' Encodeo el mensaje PKCS #7.
            Return cmsFirmado.Encode()
        Catch excepcionAlFirmar As Exception
            Throw New Exception("***Error al firmar: " & excepcionAlFirmar.Message)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lee certificado de disco
    ''' </summary>
    ''' <param name="argArchivo">Ruta del certificado a leer.</param>
    ''' <returns>Un objeto certificado X509</returns>
    ''' <remarks></remarks>
    Public Shared Function ObtieneCertificadoDesdeArchivo(
    ByVal argArchivo As String,
    ByVal argPassword As SecureString
    ) As X509Certificate2
        Dim objCert As New X509Certificate2
        Try

            If argPassword.IsReadOnly Then
                objCert.Import(My.Computer.FileSystem.ReadAllBytes(argArchivo), argPassword, X509KeyStorageFlags.PersistKeySet)
            Else
                objCert.Import(My.Computer.FileSystem.ReadAllBytes(argArchivo))
            End If

            Return objCert
        Catch excepcionAlImportarCertificado As Exception
            Throw New Exception(excepcionAlImportarCertificado.Message & " " & excepcionAlImportarCertificado.StackTrace & " " & argArchivo)
            Return Nothing
        End Try
    End Function

End Class

''' <summary>
''' Clase principal
''' </summary>
''' <remarks></remarks>
Public Class ProgramaPrincipal




    ' Valores por defecto, globales en esta clase
    Const DEFAULT_URLWSAAWSDL As String = "https://wsaa.afip.gov.ar/ws/services/LoginCms?wsdl" 'no lo usa por usa el ws
    Const DEFAULT_SERVICIO As String = "wsfe"
    Const DEFAULT_CERTSIGNER As String = "c:\\cert.pfx"
    Const DEFAULT_PROXY As String = Nothing
    Const DEFAULT_PROXY_USER As String = Nothing
    Const DEFAULT_PROXY_PASSWORD As String = ""
    Const DEFAULT_VERBOSE As Boolean = True
    Const DEFAULT_CERTIFICADO_PASSWORD As String = ""
    Dim SAL_TK As String = ""

    Public Function Iniciar(ByVal path As String
                             ) As Boolean

        Try
            Dim sal As Boolean = False

            Dim Xmlconfig As New XmlDocument()

            Xmlconfig.Load(path + "\\xml\\config.xml")
            Dim strUrlWsaaWsdl As String = Xmlconfig.SelectSingleNode("//DEFAULT_URLWSAAWSDL").InnerText
            Dim strIdServicioNegocio As String = Xmlconfig.SelectSingleNode("//DEFAULT_SERVICIO").InnerText

            ' si es homo el certHomo y si es produc el certificado es cert.

            Dim strRutaCertSigner As String = path + Xmlconfig.SelectSingleNode("//DEFAULT_CERTSIGNER").InnerText
            Dim pass As String = Xmlconfig.SelectSingleNode("//DEFAULT_CERTIFICADO_PASSWORD").InnerText



            'Dim strUrlWsaaWsdl As String = DEFAULT_URLWSAAWSDL
            'Dim strIdServicioNegocio As String = DEFAULT_SERVICIO
            'Dim strRutaCertSigner As String = DEFAULT_CERTSIGNER
            '  Dim pass As String = DEFAULT_CERTIFICADO_PASSWORD ' contraseña del certificado firmado p12 pxf

            Dim strPasswordSecureString As New SecureString
            Dim blnVerboseMode As Boolean = DEFAULT_VERBOSE

            Dim strProxy As String = DEFAULT_PROXY
            Dim strProxyUser As String = DEFAULT_PROXY_USER
            Dim strProxyPassword As String = DEFAULT_PROXY_PASSWORD



            If Not String.IsNullOrEmpty(pass) Then
                For Each character As Char In pass.ToCharArray
                    strPasswordSecureString.AppendChar(character)
                Next
                strPasswordSecureString.MakeReadOnly()
            End If

            ' Argumentos OK, entonces procesar normalmente...

            Dim objTicketRespuesta As LoginTicket
            Dim strTicketRespuesta As String


            If blnVerboseMode Then
                Console.WriteLine("***Servicio a acceder: {0}", strIdServicioNegocio)
                Console.WriteLine("***URL del WSAA: {0}", strUrlWsaaWsdl)
                Console.WriteLine("***Ruta del certificado: {0}", strRutaCertSigner)
                Console.WriteLine("***Modo verbose: {0}", blnVerboseMode)
            End If

            objTicketRespuesta = New LoginTicket

            If blnVerboseMode Then
                Console.WriteLine("***Accediendo a {0}", strUrlWsaaWsdl)
            End If

            strTicketRespuesta = objTicketRespuesta.ObtenerLoginTicketResponse(strIdServicioNegocio, strUrlWsaaWsdl, strRutaCertSigner, strPasswordSecureString, strProxy, strProxyUser, strProxyPassword, blnVerboseMode, path
                                                                               )

            If blnVerboseMode Then
                Console.WriteLine("***CONTENIDO DEL TICKET RESPUESTA:")
                Console.WriteLine("   Token: {0}", objTicketRespuesta.Token)
                Console.WriteLine("   Sign: {0}", objTicketRespuesta.Sign)
                Console.WriteLine("   GenerationTime: {0}", CStr(objTicketRespuesta.GenerationTime))
                Console.WriteLine("   ExpirationTime: {0}", CStr(objTicketRespuesta.ExpirationTime))
                Console.WriteLine("   Service: {0}", objTicketRespuesta.Service)
                Console.WriteLine("   UniqueID: {0}", CStr(objTicketRespuesta.UniqueId))
            End If

            sal = True
            Return sal
        Catch excepcionAlObtenerTicket As Exception

            Throw excepcionAlObtenerTicket

        End Try

    End Function

    Public Function IniciarProducion(ByVal path As String, ByVal configuracion As String, ByVal response As String, ByVal cerfirmado As X509Certificate2) As Boolean

        Try
            Dim sal As Boolean = False

            Dim Xmlconfig As New XmlDocument()


            '  en config los datos de proccion
            Xmlconfig.Load(configuracion)




            Dim strUrlWsaaWsdl As String = Xmlconfig.SelectSingleNode("//DEFAULT_URLWSAAWSDL").InnerText

            Dim strIdServicioNegocio As String = Xmlconfig.SelectSingleNode("//DEFAULT_SERVICIO").InnerText
            ' si es homo el certHomo y si es produc el certificado es cert.

            Dim strRutaCertSigner As String = path + Xmlconfig.SelectSingleNode("//DEFAULT_CERTSIGNER").InnerText
            Dim pass As String = Xmlconfig.SelectSingleNode("//DEFAULT_CERTIFICADO_PASSWORD").InnerText




            'Dim strUrlWsaaWsdl As String = DEFAULT_URLWSAAWSDL
            'Dim strIdServicioNegocio As String = DEFAULT_SERVICIO
            'Dim strRutaCertSigner As String = DEFAULT_CERTSIGNER
            '  Dim pass As String = DEFAULT_CERTIFICADO_PASSWORD ' contraseña del certificado firmado p12 pxf

            Dim strPasswordSecureString As New SecureString
            Dim blnVerboseMode As Boolean = DEFAULT_VERBOSE

            Dim strProxy As String = DEFAULT_PROXY
            Dim strProxyUser As String = DEFAULT_PROXY_USER
            Dim strProxyPassword As String = DEFAULT_PROXY_PASSWORD



            If Not String.IsNullOrEmpty(pass) Then
                For Each character As Char In pass.ToCharArray
                    strPasswordSecureString.AppendChar(character)
                Next
                strPasswordSecureString.MakeReadOnly()
            End If

            ' Argumentos OK, entonces procesar normalmente...

            Dim objTicketRespuesta As LoginTicket
            Dim strTicketRespuesta As String


            If blnVerboseMode Then
                Console.WriteLine("***Servicio a acceder: {0}", strIdServicioNegocio)
                Console.WriteLine("***URL del WSAA: {0}", strUrlWsaaWsdl)
                Console.WriteLine("***Ruta del certificado: {0}", strRutaCertSigner)
                Console.WriteLine("***Modo verbose: {0}", blnVerboseMode)
            End If

            objTicketRespuesta = New LoginTicket

            If blnVerboseMode Then
                Console.WriteLine("***Accediendo a {0}", strUrlWsaaWsdl)
            End If



            ' donde crea el tk de response
            SAL_TK = response

            strTicketRespuesta = objTicketRespuesta.ObtenerLoginTicketResponseProduccion(strIdServicioNegocio, strUrlWsaaWsdl, strRutaCertSigner, strPasswordSecureString, strProxy, strProxyUser, strProxyPassword, blnVerboseMode, path, SAL_TK, cerfirmado)





            If blnVerboseMode Then
                Console.WriteLine("***CONTENIDO DEL TICKET RESPUESTA:")
                Console.WriteLine("   Token: {0}", objTicketRespuesta.Token)
                Console.WriteLine("   Sign: {0}", objTicketRespuesta.Sign)
                Console.WriteLine("   GenerationTime: {0}", CStr(objTicketRespuesta.GenerationTime))
                Console.WriteLine("   ExpirationTime: {0}", CStr(objTicketRespuesta.ExpirationTime))
                Console.WriteLine("   Service: {0}", objTicketRespuesta.Service)
                Console.WriteLine("   UniqueID: {0}", CStr(objTicketRespuesta.UniqueId))
            End If

            sal = True
            Return sal
        Catch excepcionAlObtenerTicket As Exception

            Throw excepcionAlObtenerTicket

        End Try

    End Function

    ''' <summary>
    ''' Explica el uso del comando
    ''' </summary>
    ''' <remarks></remarks>
    Shared Sub ExplicarUso()
        Console.WriteLine("")
        Console.WriteLine("Uso: {0} (opciones) ...", My.Application.Info.AssemblyName)
        Console.WriteLine("")
        Console.WriteLine("opciones:")
        Console.WriteLine("")
        Console.WriteLine("  -s servicio      ID del servicio de negocio")
        Console.WriteLine("                   Valor por defecto: " & DEFAULT_SERVICIO)
        Console.WriteLine("")
        Console.WriteLine("  -c certificado   Ruta del certificado (con clave privada)")
        Console.WriteLine("                   Valor por defecto: " & DEFAULT_CERTSIGNER)
        Console.WriteLine("")
        Console.WriteLine("  -p password      Password del certificado (con clave privada)")
        Console.WriteLine("                   Valor por defecto: sin password")
        Console.WriteLine("")
        Console.WriteLine("  -x IP:port proxy IP:port del proxy")
        Console.WriteLine("                   Valor por defecto: sin proxy")
        Console.WriteLine("")
        Console.WriteLine("  -y usuario proxy Usuario del proxy")
        Console.WriteLine("                   Valor por defecto: sin usuario proxy")
        Console.WriteLine("")
        Console.WriteLine("  -z pass proxy    Password del proxy")
        Console.WriteLine("                   Valor por defecto: sin password proxy")
        Console.WriteLine("")
        Console.WriteLine("  -w url           URL del WSDL del WSAA")
        Console.WriteLine("                   Valor por defecto: " & DEFAULT_URLWSAAWSDL)
        Console.WriteLine("")
        Console.WriteLine("  -v on|off        Reportes detallados de la ejecución")
        Console.WriteLine("                   Valor por defecto: " & IIf(DEFAULT_VERBOSE, "on", "off"))
        Console.WriteLine("")
        Console.WriteLine("  -?               Esta ayuda")
    End Sub

    Shared Sub MostrarVersion()
        Console.WriteLine("Aplicacion : " & My.Application.Info.AssemblyName)
        Console.WriteLine("Version    : " & My.Application.Info.Version.ToString)
        Console.WriteLine("Descripcion: " & My.Application.Info.Description)
    End Sub
End Class