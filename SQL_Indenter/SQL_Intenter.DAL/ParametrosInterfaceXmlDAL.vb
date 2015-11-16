Imports System.IO
Imports System.Xml.Serialization
Imports SQL_Indenter.BEL

Public Class ParametrosInterfaceXmlDAL

    Public Sub GravarParametrosArquivoXML(parametrosInterface As List(Of ParametroInterfaceBEL))
        Dim objStreamWriter As New StreamWriter("SQL_Indenter_Parametros.xml")
        Dim x As New XmlSerializer(parametrosInterface.GetType)
        x.Serialize(objStreamWriter, parametrosInterface)
        objStreamWriter.Close()
    End Sub

    Public Function LerParametrosArquivoXML() As List(Of ParametroInterfaceBEL)
        Try
            Dim objStreamReader As New StreamReader("SQL_Indenter_Parametros.xml")
            Dim parametrosInterface As New List(Of ParametroInterfaceBEL)
            Dim x As New XmlSerializer(parametrosInterface.GetType)
            parametrosInterface = x.Deserialize(objStreamReader)
            objStreamReader.Close()

            Return parametrosInterface
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
