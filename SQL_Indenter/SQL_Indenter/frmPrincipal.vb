Imports SQL_Indenter.BEL
Imports SQL_Indenter.BLL

Public Class frmPrincipal

    Private Sub btnFormatar_Click(sender As Object, e As EventArgs) Handles btnFormatar.Click

        Dim parametrosInterface As List(Of ParametroInterface) = ObterParametrosInterface()
        Dim indetador As New Indentador(parametrosInterface)
        txtSql.Text = indetador.IndentarScriptSql(txtSql.Text)

    End Sub

    Private Function ObterParametrosInterface() As List(Of ParametroInterface)
        Dim parametrosInterface As New List(Of ParametroInterface)

        parametrosInterface.Add(New ParametroInterface("QuebrarLinhaACadaColuna", chkQuebrarLinhaACadaColuna.Checked))
        parametrosInterface.Add(New ParametroInterface("VirgulaInicioLinha", rdbVirgulaInicioLinha.Checked))
        parametrosInterface.Add(New ParametroInterface("SepararOperadoresLogicosComUmEspaco", chkSepararOperadoresLogicosComUmEspaco.Checked))

        Return parametrosInterface
    End Function

    Private Sub chkQuebrarLinhaACadaColuna_CheckedChanged(sender As Object, e As EventArgs) Handles chkQuebrarLinhaACadaColuna.CheckedChanged
        grpQuebrarLinhaACadaColuna.Enabled = chkQuebrarLinhaACadaColuna.Checked
    End Sub

End Class
