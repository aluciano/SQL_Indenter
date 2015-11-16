Imports SQL_Indenter.BEL
Imports SQL_Indenter.BEL.ConstantesBEL

Public Class IndentadorBLL

    Public Property ParametrosUI As List(Of ParametroInterfaceBEL)

    Public Sub New(parametrosInterface As List(Of ParametroInterfaceBEL))
        ParametrosUI = parametrosInterface
    End Sub

    Public Function ObterValorParametro(nome As String) As Object
        Return ParametrosUI.First(Function(p) p.Nome = nome).Valor
    End Function

    Public Function IndentarScriptSql(scriptSql As String) As String

        scriptSql = RemoverQuebrasDeLinha(scriptSql)
        Dim sqlList As List(Of String) = ConverterScriptEmListaDeStatements(scriptSql)
        sqlList = QuebrarLinhaACadaColuna(sqlList)
        sqlList = SomenteUmEspacoAposStatement(sqlList)
        sqlList = SepararOperadoresLogicosComUmEspaco(sqlList)

        Return Indentar(sqlList)

    End Function

    Private Function SepararOperadoresLogicosComUmEspaco(sqlList As List(Of String)) As List(Of String)
        If ObterValorParametro("SepararOperadoresLogicosComUmEspaco") Then
            For i = 1 To sqlList.Count - 1
                If sqlList(i).IndexOfAny(ObterListaOperadoresLogicos(), 0) >= 0 Then
                    sqlList(i) = RemoverEspacosAntesDepoisOperadoresLogicos(sqlList(i))

                    Dim operador As String = ObterOperadorLogico(sqlList(i))
                    sqlList(i) = sqlList(i).Replace(operador, String.Format(" {0} ", operador))
                End If
            Next
        Else
            For i = 1 To sqlList.Count - 1
                If sqlList(i).IndexOfAny(ObterListaOperadoresLogicos(), 0) >= 0 Then
                    sqlList(i) = RemoverEspacosAntesDepoisOperadoresLogicos(sqlList(i))
                End If
            Next
        End If

        Return sqlList
    End Function

    Private Function RemoverEspacosAntesDepoisOperadoresLogicos(sqlLine As String) As String
        Dim indexTemp As Integer = sqlLine.IndexOfAny(ObterListaOperadoresLogicos(), 0)
        Dim operador As String = ObterOperadorLogico(sqlLine)

        While sqlLine(indexTemp - 1) = " "
            sqlLine = sqlLine.Remove(indexTemp - 1, 1)
            indexTemp -= 1
        End While

        indexTemp = sqlLine.IndexOf(operador, 0)

        While sqlLine(indexTemp + operador.Length) = " "
            sqlLine = sqlLine.Remove(indexTemp + operador.Length, 1)
        End While

        Return sqlLine
    End Function

    Public Function ObterOperadorLogico(sqlLine As String) As String
        Dim indexTemp As Integer = sqlLine.IndexOfAny(ObterListaOperadoresLogicos(), 0)
        Dim operador As String = sqlLine(indexTemp)
        If operador = ">" OrElse operador = "<" OrElse operador = "=" OrElse operador = "*" Then
            If sqlLine(indexTemp + 1) = ">" OrElse sqlLine(indexTemp + 1) = "=" OrElse sqlLine(indexTemp + 1) = "*" Then
                operador = sqlLine(indexTemp) & sqlLine(indexTemp + 1)
            End If
        End If

        Return operador
    End Function

    Private Function ObterListaOperadoresLogicos() As Char()
        Return New Char() {"=", "<>", ">", ">=", "<", "<=", "*=", "=*"}
    End Function

    Private Function RemoverQuebrasDeLinha(txtSql As String) As String
        Return txtSql.Replace(vbLf, "")
    End Function

    Private Function ConverterScriptEmListaDeStatements(text As String) As List(Of String)
        text = " " & text
        text = text.Replace(" " & SELECT_STATEMENT & " ", vbLf & " " & SELECT_STATEMENT & " ")
        text = text.Replace(" " & FROM_STATEMENT & " ", vbLf & " " & FROM_STATEMENT & " ")
        text = text.Replace(" " & WHERE_STATEMENT & " ", vbLf & " " & WHERE_STATEMENT & " ")
        text = text.Replace(" " & AND_STATEMENT & " ", vbLf & " " & AND_STATEMENT & " ")

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
                Case AND_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + AND_SPACES)

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
        If txtSql.Contains(AND_STATEMENT) Then Return AND_STATEMENT

        Return String.Empty
    End Function

    Private Function QuebrarLinhaACadaColuna(sqlList As List(Of String)) As List(Of String)
        If ObterValorParametro("QuebrarLinhaACadaColuna") Then
            For i = 0 To sqlList.Count - 1
                If sqlList(i).Contains(SELECT_STATEMENT) Then
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

    Private Function SomenteUmEspacoAposStatement(sqlList As List(Of String)) As List(Of String)
        Dim statementDetected As String = ""
        Dim sql As String = ""

        For i = 0 To sqlList.Count - 1
            sql = sqlList(i).Trim()

            statementDetected = DetectStatement(sql, statementDetected)

            Select Case statementDetected
                Case SELECT_STATEMENT, FROM_STATEMENT, WHERE_STATEMENT, AND_STATEMENT
                    While sqlList(i).Contains(statementDetected & "  ")
                        sqlList(i) = sqlList(i).Replace(statementDetected & "  ", statementDetected & " ")
                    End While

                Case Else
                    sqlList(i) = sql

            End Select
        Next

        Return sqlList
    End Function

End Class
