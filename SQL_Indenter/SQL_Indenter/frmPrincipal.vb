Imports SQL_Indenter.BEL
Imports SQL_Indenter.BLL

Public Class FrmPrincipal

    Private Sub btnFormatar_Click(sender As Object, e As EventArgs) Handles btnFormatar.Click

        Dim parametrosInterface As List(Of ParametroInterfaceBEL) = ObterParametrosInterface()
        Dim indetador As New IndentadorBLL(parametrosInterface)
        txtSql.Text = indetador.IndentarScriptSql(txtSql.Text)

    End Sub

    Private Function ObterParametrosInterface() As List(Of ParametroInterfaceBEL)
        Dim parametrosInterface As New List(Of ParametroInterfaceBEL)

        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaColuna", chkQuebrarLinhaACadaColuna.Checked))
        parametrosInterface.Add(New ParametroInterfaceBEL("VirgulaInicioLinha", rdbVirgulaInicioLinha.Checked))
        parametrosInterface.Add(New ParametroInterfaceBEL("SepararOperadoresLogicosComUmEspaco", chkSepararOperadoresLogicosComUmEspaco.Checked))
        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaParametroFuncao", chkQuebrarLinhaACadaParametroFuncao.Checked))

        Return parametrosInterface
    End Function

    Private Sub chkQuebrarLinhaACadaColuna_CheckedChanged(sender As Object, e As EventArgs) Handles chkQuebrarLinhaACadaColuna.CheckedChanged
        grpQuebrarLinhaACadaColuna.Enabled = chkQuebrarLinhaACadaColuna.Checked
    End Sub

    Private Sub frmPrincipal_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim bllParametroInterfaceXml As New ParametroInterfaceXmlBLL(ObterParametrosInterface())
        bllParametroInterfaceXml.GravarParametrosArquivoXML()
    End Sub

    Private Sub frmPrincipal_Load(sender As Object, e As EventArgs) Handles Me.Load
        PreencherPropriedadesInterface()
    End Sub

    Private Sub PreencherPropriedadesInterface()
        Dim bllParametroInterfaceXml As New ParametroInterfaceXmlBLL()
        bllParametroInterfaceXml.LerParametrosArquivoXML()

        If bllParametroInterfaceXml.ParametrosUI IsNot Nothing Then
            chkQuebrarLinhaACadaColuna.Checked = bllParametroInterfaceXml.ObterValorParametro("QuebrarLinhaACadaColuna")
            rdbVirgulaInicioLinha.Checked = bllParametroInterfaceXml.ObterValorParametro("VirgulaInicioLinha")
            chkSepararOperadoresLogicosComUmEspaco.Checked = bllParametroInterfaceXml.ObterValorParametro("SepararOperadoresLogicosComUmEspaco")
        End If
    End Sub

End Class
