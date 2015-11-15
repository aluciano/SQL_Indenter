Imports SQL_Indenter.BEL
Imports SQL_Indenter.BEL.Constantes

Public Class Indentador

    Public Property ParametrosUI As List(Of ParametroInterface)

    Public Sub New(parametrosInterface As List(Of ParametroInterface))
        ParametrosUI = parametrosInterface
    End Sub

    Public Function ObterValorParametro(nome As String) As Object
        Return ParametrosUI.First(Function(p) p.Nome = nome).Valor
    End Function

    Public Function IndentarScriptSql(scriptSql As String) As String

        scriptSql = RemoverQuebrasDeLinha(scriptSql)
        Dim sqlList As List(Of String) = ConverterScriptEmListaDeStatements(scriptSql)
        sqlList = QuebrarLinhaACadaColuna(sqlList)
        Return Indentar(sqlList)

    End Function

    Private Function RemoverQuebrasDeLinha(txtSql As String) As String
        Return txtSql.Replace(vbLf, "")
    End Function

    Private Function ConverterScriptEmListaDeStatements(text As String) As List(Of String)
        text = text.Replace(SELECT_STATEMENT, vbLf & SELECT_STATEMENT)
        text = text.Replace(FROM_STATEMENT, vbLf & FROM_STATEMENT)
        text = text.Replace(WHERE_STATEMENT, vbLf & WHERE_STATEMENT)

        Dim sqlList As New List(Of String)
        sqlList.AddRange(text.Split(New Char() {vbLf}, StringSplitOptions.RemoveEmptyEntries))

        Return sqlList
    End Function

    Private Function Indentar(sqlList As List(Of String)) As String
        Dim statementDetected As String = ""
        Dim sql As String = ""
        Dim virgula As Integer = If(ObterValorParametro("VirgulaInicioLinha"), -1, 0)

        For index = 0 To sqlList.Count - 1
            sql = sqlList(index).Trim()

            statementDetected = DetectStatement(sql, statementDetected)

            Select Case statementDetected
                Case SELECT_COLUMN
                    sqlList(index) = sql.PadLeft(sql.Length + SELECT_SPACES + virgula)
                Case FROM_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + FROM_SPACES)
                Case WHERE_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + WHERE_SPACES)

                Case Else
                    sqlList(index) = sql

            End Select
        Next

        Return String.Join(vbLf, sqlList.ToArray())
    End Function

    Private Function DetectStatement(txtSql As String, lastDetectedStatement As String) As String
        If txtSql.Contains(SELECT_STATEMENT) Then Return SELECT_STATEMENT
        If lastDetectedStatement = SELECT_STATEMENT OrElse
           lastDetectedStatement = SELECT_COLUMN Then
            If Not txtSql.Contains(FROM_STATEMENT) Then
                Return SELECT_COLUMN
            End If
        End If
        If txtSql.Contains(FROM_STATEMENT) Then Return FROM_STATEMENT
        If txtSql.Contains(WHERE_STATEMENT) Then Return WHERE_STATEMENT

        Return String.Empty
    End Function

    Private Function QuebrarLinhaACadaColuna(sqlList As List(Of String)) As List(Of String)
        If ObterValorParametro("QuebrarLinhaACadaColuna") Then
            For i = 0 To sqlList.Count - 1
                If sqlList(i).Contains(SELECT_STATEMENT) Then
                    'sqlList(i) = sqlList(i).Replace(",", "," & vbLf)

                    Dim selectColumnsList As New List(Of String)
                    selectColumnsList.AddRange(sqlList(i).Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries))
                    selectColumnsList.RemoveAt(0)
                    sqlList(i) = sqlList(i).Substring(0, sqlList(i).IndexOf(","))
                    sqlList(i) = sqlList(i).Trim()

                    If ObterValorParametro("VirgulaInicioLinha") Then
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
                If sqlList(i).Contains(SELECT_STATEMENT) Then
                    While sqlList(i).Contains("  ")
                        sqlList(i) = sqlList(i).Replace("  ", " ")
                    End While
                    sqlList(i) = sqlList(i).Replace(" ,", ", ")
                End If
            Next
        End If

        Return sqlList
    End Function

End Class
