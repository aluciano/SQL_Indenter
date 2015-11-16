Public Class ParametroInterfaceBEL

    Public Sub New()

    End Sub

    Public Sub New(nomeParametro As String, valorParametro As Object)
        Nome = nomeParametro
        Valor = valorParametro
    End Sub

    Public Property Nome As String
    Public Property Valor As Object

End Class
