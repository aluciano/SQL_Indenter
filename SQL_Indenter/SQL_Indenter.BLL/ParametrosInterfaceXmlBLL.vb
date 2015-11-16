Imports SQL_Indenter.BEL
Imports SQL_Intenter.DAL

Public Class ParametroInterfaceXmlBLL

    Public Property ParametrosUI As List(Of ParametroInterfaceBEL)

    Public Sub New()

    End Sub

    Public Sub New(parametrosInterface As List(Of ParametroInterfaceBEL))
        ParametrosUI = parametrosInterface
    End Sub

    Public Function ObterValorParametro(nome As String) As Object
        Return ParametrosUI.First(Function(p) p.Nome = nome).Valor
    End Function

    Public Sub GravarParametrosArquivoXML()
        Dim dalGravaXML As New ParametrosInterfaceXmlDAL()
        dalGravaXML.GravarParametrosArquivoXML(ParametrosUI)
    End Sub

    Public Sub LerParametrosArquivoXML()
        Dim dalGravaXML As New ParametrosInterfaceXmlDAL()
        ParametrosUI = dalGravaXML.LerParametrosArquivoXML()
    End Sub

End Class
