Imports SQL_Indenter.BEL
Imports SQL_Indenter.BEL.ConstantesBEL

Public Class IndentadorBLL
    Private Property ParametrosUI As List(Of ParametroInterfaceBEL)

    Public Sub New(parametrosInterface As List(Of ParametroInterfaceBEL))
        ParametrosUI = parametrosInterface
    End Sub

    Private Function ObterValorParametro(nome As String) As Object
        Return ParametrosUI.First(Function(p) p.Nome = nome).Valor
    End Function

    Public Function IndentarScriptSql(scriptSql As String) As String

        scriptSql = RemoverQuebrasDeLinha(scriptSql)
        Dim sqlList As List(Of String) = ConverterScriptEmListaDeStatements(scriptSql)
        sqlList = QuebrarLinhaACadaColuna(sqlList)
        sqlList = QuebrarLinhaParametrosFuncao(sqlList)
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

    Private Shared Function RemoverEspacosAntesDepoisOperadoresLogicos(sqlLine As String) As String
        Dim indexTemp As Integer = sqlLine.IndexOfAny(ObterListaOperadoresLogicos(), 0)
        Dim operador As String = ObterOperadorLogico(sqlLine)

        While sqlLine(indexTemp - 1) = " "
            sqlLine = sqlLine.Remove(indexTemp - 1, 1)
            indexTemp -= 1
        End While

        indexTemp = sqlLine.IndexOf(operador, 0, StringComparison.Ordinal)

        While sqlLine(indexTemp + operador.Length) = " "
            sqlLine = sqlLine.Remove(indexTemp + operador.Length, 1)
        End While

        Return sqlLine
    End Function

    Private Shared Function ObterOperadorLogico(sqlLine As String) As String
        Dim indexTemp As Integer = sqlLine.IndexOfAny(ObterListaOperadoresLogicos(), 0)
        Dim operador As String = sqlLine(indexTemp)
        If operador = ">" OrElse operador = "<" OrElse operador = "=" OrElse operador = "*" Then
            If sqlLine(indexTemp + 1) = ">" OrElse sqlLine(indexTemp + 1) = "=" OrElse sqlLine(indexTemp + 1) = "*" Then
                operador = sqlLine(indexTemp) & sqlLine(indexTemp + 1)
            End If
        End If

        Return operador
    End Function

    Private Shared Function ObterListaOperadoresLogicos() As Char()
        Return New Char() {"=", "<>", ">", ">=", "<", "<=", "*=", "=*"}
    End Function

    Private Shared Function RemoverQuebrasDeLinha(txtSql As String) As String
        Return txtSql.Replace(vbLf, " ")
    End Function

    Private Shared Function ConverterScriptEmListaDeStatements(text As String) As List(Of String)
        text = " " & text
        text = text.Replace(" " & SELECT_STATEMENT & " ", vbLf & " " & SELECT_STATEMENT & " ")
        text = text.Replace(" " & FROM_STATEMENT & " ", vbLf & " " & FROM_STATEMENT & " ")
        text = text.Replace(" " & WHERE_STATEMENT & " ", vbLf & " " & WHERE_STATEMENT & " ")
        text = text.Replace(" " & AND_STATEMENT & " ", vbLf & " " & AND_STATEMENT & " ")
        text = text.Replace(" " & GROUPBY_STATEMENT & " ", vbLf & " " & GROUPBY_STATEMENT & " ")
        text = text.Replace(" " & ORDERBY_STATEMENT & " ", vbLf & " " & ORDERBY_STATEMENT & " ")
        text = text.Replace(" " & HAVING_STATEMENT & " ", vbLf & " " & HAVING_STATEMENT & " ")

        Dim sqlList As New List(Of String)
        sqlList.AddRange(text.Split(New Char() {vbLf}, StringSplitOptions.RemoveEmptyEntries))

        While sqlList(0).Trim = ""
            sqlList.RemoveAt(0)
        End While

        Return sqlList
    End Function

    Private Function Indentar(sqlList As List(Of String)) As String
        Dim statementDetected As String = ""
        Dim sql As String
        Dim virgula As Integer = If(ObterValorParametro("VirgulaInicioLinha"), -1, 0)
        Dim existeGroupByOrderBy As Boolean = sqlList.Any(Function(p) p.Contains(GROUPBY_STATEMENT) OrElse p.Contains(ORDERBY_STATEMENT))
        Dim orderBySpaces As Integer
        If existeGroupByOrderBy Then orderBySpaces = 2
        Dim indexFunctionStart As Integer

        For index = 0 To sqlList.Count - 1
            sql = sqlList(index).Trim()

            statementDetected = DetectStatement(sql, statementDetected)

            Select Case statementDetected
                Case SELECT_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + orderBySpaces)
                Case COLUMN, COLUMN_WITH_FUNCTION_COMPLETE, COLUMN_WITH_FUNCTION_START
                    sqlList(index) = sql.PadLeft(sql.Length + orderBySpaces + SELECT_SPACES + virgula)
                    indexFunctionStart = index
                Case COLUMN_FUNCTION_PARAMETER, COLUMN_WITH_FUNCTION_END
                    Dim qtdCaracteresNomeFunction As Integer = sqlList(indexFunctionStart).IndexOf("(") + 1
                    sqlList(index) = sql.PadLeft(sql.Length + qtdCaracteresNomeFunction)
                Case FROM_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + orderBySpaces + FROM_SPACES)
                Case WHERE_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + orderBySpaces + WHERE_SPACES)
                Case AND_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + orderBySpaces + AND_SPACES)
                Case HAVING_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + HAVING_SPACES)
                Case Else
                    sqlList(index) = sql
            End Select
        Next

        Return String.Join(vbLf, sqlList.ToArray())
    End Function

    Private Shared Function DetectStatement(txtSql As String, lastDetectedStatement As String) As String
        If txtSql.Contains(SELECT_STATEMENT) Then Return SELECT_STATEMENT
        If txtSql.Contains(FROM_STATEMENT) Then Return FROM_STATEMENT
        If txtSql.Contains(WHERE_STATEMENT) Then Return WHERE_STATEMENT
        If txtSql.Contains(AND_STATEMENT) Then Return AND_STATEMENT
        If txtSql.Contains(GROUPBY_STATEMENT) Then Return GROUPBY_STATEMENT
        If txtSql.Contains(ORDERBY_STATEMENT) Then Return ORDERBY_STATEMENT
        If txtSql.Contains(HAVING_STATEMENT) Then Return HAVING_STATEMENT

        If lastDetectedStatement = COLUMN_WITH_FUNCTION_START Or
           lastDetectedStatement = COLUMN_FUNCTION_PARAMETER Then
            If txtSql.Count(Function(p) p = ")") > txtSql.Count(Function(p) p = "(") Then
                Return COLUMN_WITH_FUNCTION_END
            Else
                Return COLUMN_FUNCTION_PARAMETER
            End If
        End If

        If lastDetectedStatement = SELECT_STATEMENT OrElse
           lastDetectedStatement = GROUPBY_STATEMENT OrElse
           lastDetectedStatement = ORDERBY_STATEMENT OrElse
           lastDetectedStatement = COLUMN_WITH_FUNCTION_END OrElse
           lastDetectedStatement = COLUMN_WITH_FUNCTION_COMPLETE OrElse
           lastDetectedStatement = COLUMN Then
            If txtSql.Contains("(") Then
                If txtSql.Count(Function(p) p = "(") = txtSql.Count(Function(p) p = ")") Then
                    Return COLUMN_WITH_FUNCTION_COMPLETE
                Else
                    Return COLUMN_WITH_FUNCTION_START
                End If
            Else
                Return COLUMN
            End If
        End If

        Return String.Empty
    End Function

    Private Function QuebrarLinhaACadaColuna(sqlList As List(Of String)) As List(Of String)
        If ObterValorParametro("QuebrarLinhaACadaColuna") Then
            Dim i As Integer = 0
            Dim idxUltimaLinha As Integer = sqlList.Count - 1
            While i <= idxUltimaLinha
                If sqlList(i).Contains(SELECT_STATEMENT) OrElse
                   sqlList(i).Contains(GROUPBY_STATEMENT) OrElse
                   sqlList(i).Contains(ORDERBY_STATEMENT) Then
                    Dim selectColumnsList As New List(Of String)
                    selectColumnsList.AddRange(sqlList(i).Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries))
                    selectColumnsList.RemoveAt(0)
                    sqlList(i) = sqlList(i).Substring(0, sqlList(i).IndexOf(",", StringComparison.Ordinal))
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

                idxUltimaLinha = sqlList.Count - 1
                i += 1
            End While
        Else
            For i = 0 To sqlList.Count - 1
                If sqlList(i).Contains(SELECT_STATEMENT) OrElse
                   sqlList(i).Contains(GROUPBY_STATEMENT) OrElse
                   sqlList(i).Contains(ORDERBY_STATEMENT) Then
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
        Dim sql As String

        For i = 0 To sqlList.Count - 1
            sql = sqlList(i).Trim()

            statementDetected = DetectStatement(sql, statementDetected)

            Select Case statementDetected
                Case SELECT_STATEMENT, FROM_STATEMENT, WHERE_STATEMENT, AND_STATEMENT, GROUPBY_STATEMENT, ORDERBY_STATEMENT, HAVING_STATEMENT
                    While sqlList(i).Contains(statementDetected & "  ")
                        sqlList(i) = sqlList(i).Replace(statementDetected & "  ", statementDetected & " ")
                    End While

                Case Else
                    sqlList(i) = sql

            End Select
        Next

        Return sqlList
    End Function

    Private Function QuebrarLinhaParametrosFuncao(sqlList As List(Of String)) As List(Of String)
        Dim statementDetected As String = ""
        Dim sql As String
        Dim i As Integer
        Dim idxUltimaLinha As Integer = sqlList.Count - 1

        While i <= idxUltimaLinha
            sql = sqlList(i).Trim()

            statementDetected = DetectStatement(sql, statementDetected)

            Select Case statementDetected
                Case COLUMN_WITH_FUNCTION_START
                    If Not ObterValorParametro("QuebrarLinhaACadaParametroFuncao") Then
                        If sqlList(i).Count(Function(p) p = "(") > sqlList(i).Count(Function(p) p = ")") Then

                            Dim j As Integer = i + 1
                            While sqlList(j).Count(Function(p) p = "(") >= sqlList(j).Count(Function(p) p = ")")
                                sqlList(i) &= " " & sqlList(j)
                                j += 1
                            End While

                            sqlList(i) &= " " & sqlList(j)

                            For k = i + 1 To j
                                sqlList.RemoveAt(i + 1)
                            Next
                        End If

                    End If

                Case Else
                    sqlList(i) = sql

            End Select

            idxUltimaLinha = sqlList.Count - 1
            i += 1
        End While

        Return sqlList
    End Function

End Class
