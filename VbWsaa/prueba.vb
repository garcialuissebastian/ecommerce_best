Public Class prueba
    Public ok As String

    Public Function msg() As String

        Try




            Dim PyI25 As Object

            PyI25 = CreateObject("PyI25")

            ' cuit, tipo_cbte, punto_vta, cae, fch_venc_cae
            Dim barras As String = "202675653930240016120303473904220110529"
            ' calculo digito verificador:
            barras = barras + PyI25.DigitoVerificadorModulo10(barras)

            ' genero imagen en png, aspecto 1x para ver en pantalla o por mail
            ok = PyI25.GenerarImagen(barras, "C:\barras.png")



            ' formato en jpg, aspecto 3x más ancho para imprimir o incrustar:
            ok = PyI25.GenerarImagen(barras, "c:\barras.jpg", 9, 0, 90, "JPEG")
        Catch ex As Exception



            MsgBox(ex.Message)



        End Try
        Return ok
    End Function




End Class
