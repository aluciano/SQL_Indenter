Public Class Form1
    Private Sub btnFormatar_Click(sender As Object, e As EventArgs) Handles btnFormatar.Click

        RemoverQuebrasDeLinha()
        Dim sqlList As List(Of String) = ObterListaDeStatements(txtSql.Text)
        sqlList = QuebrarLinhaACadaColuna(sqlList)
        Indentar(sqlList)

    End Sub

    Private Function ObterListaDeStatements(text As String) As List(Of String)
        text = text.Replace(Constantes.SELECT_STATEMENT, vbLf & Constantes.SELECT_STATEMENT)
        text = text.Replace(Constantes.FROM_STATEMENT, vbLf & Constantes.FROM_STATEMENT)
        text = text.Replace(Constantes.WHERE_STATEMENT, vbLf & Constantes.WHERE_STATEMENT)

        Dim sqlList As New List(Of String)
        sqlList.AddRange(text.Split(New Char() {vbLf}, StringSplitOptions.RemoveEmptyEntries))

        Return sqlList
    End Function

    Private Sub Indentar(sqlList As List(Of String))
        'txtSql.Text = txtSql.Text.Replace("FROM", vbLf & "FROM")
        'txtSql.Text = txtSql.Text.Replace("WHERE", vbLf & "WHERE")

        'Dim sqlList As New List(Of String)
        'sqlList.AddRange(txtSql.Text.Split(New Char() {vbLf}, StringSplitOptions.RemoveEmptyEntries))

        Dim statementDetected As String = ""
        Dim sql As String = ""
        Dim virgula As Integer = If(rdbVirgulaInicioLinha.Checked, -1, 0)

        For index = 0 To sqlList.Count - 1
            sql = sqlList(index).Trim()

            statementDetected = DetectStatement(sql, statementDetected)

            Select Case statementDetected
                Case Constantes.SELECT_COLUMN
                    sqlList(index) = sql.PadLeft(sql.Length + Constantes.SELECT_SPACES + virgula)
                Case Constantes.FROM_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + Constantes.FROM_SPACES)
                Case Constantes.WHERE_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + Constantes.WHERE_SPACES)

                Case Else
                    sqlList(index) = sql

            End Select
        Next


        txtSql.Text = String.Join(vbLf, sqlList.ToArray())
    End Sub

    Private Function DetectStatement(sql As String, lastDetectedStatement As String) As String
        If sql.Contains(Constantes.SELECT_STATEMENT) Then Return Constantes.SELECT_STATEMENT
        If lastDetectedStatement = Constantes.SELECT_STATEMENT OrElse
           lastDetectedStatement = Constantes.SELECT_COLUMN Then
            If Not sql.Contains(Constantes.FROM_STATEMENT) Then
                Return Constantes.SELECT_COLUMN
            End If
        End If
        If sql.Contains(Constantes.FROM_STATEMENT) Then Return Constantes.FROM_STATEMENT
        If sql.Contains(Constantes.WHERE_STATEMENT) Then Return Constantes.WHERE_STATEMENT

        Return String.Empty
    End Function

    Private Function QuebrarLinhaACadaColuna(sqlList As List(Of String)) As List(Of String)
        If chkQuebrarLinhaACadaColuna.Checked Then
            For i = 0 To sqlList.Count - 1
                If sqlList(i).Contains(Constantes.SELECT_STATEMENT) Then
                    'sqlList(i) = sqlList(i).Replace(",", "," & vbLf)

                    Dim selectColumnsList As New List(Of String)
                    selectColumnsList.AddRange(sqlList(i).Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries))
                    selectColumnsList.RemoveAt(0)
                    sqlList(i) = sqlList(i).Substring(0, sqlList(i).IndexOf(","))
                    sqlList(i) = sqlList(i).Trim()

                    If rdbVirgulaInicioLinha.Checked Then
                        For j = 0 To selectColumnsList.Count - 1
                            selectColumnsList(j) = selectColumnsList(j).Trim()
                            selectColumnsList(j) = "," & selectColumnsList(j)
                        Next
                    Else
                        sqlList(i) = sqlList(i).TrimEnd() & ","
                        For j = 0 To selectColumnsList.Count - 2
                            selectColumnsList(j) = selectColumnsList(j).Trim()
                            selectColumnsList(j) = selectColumnsList(j) & ","
                        Next
                    End If

                    sqlList.InsertRange(i + 1, selectColumnsList)

                End If
            Next
        Else
            For i = 0 To sqlList.Count - 1
                If sqlList(i).Contains(Constantes.SELECT_STATEMENT) Then
                    While sqlList(i).Contains("  ")
                        sqlList(i) = sqlList(i).Replace("  ", " ")
                    End While
                    sqlList(i) = sqlList(i).Replace(" ,", ", ")
                End If
            Next
        End If

        Return sqlList
    End Function

    Private Sub RemoverQuebrasDeLinha()
        txtSql.Text = txtSql.Text.Replace(vbLf, "")
    End Sub

    Private Sub chkQuebrarLinhaACadaColuna_CheckedChanged(sender As Object, e As EventArgs) Handles chkQuebrarLinhaACadaColuna.CheckedChanged
        grpQuebrarLinhaACadaColuna.Enabled = chkQuebrarLinhaACadaColuna.Checked
    End Sub
End Class
