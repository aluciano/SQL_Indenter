Imports SQL_Indenter.BEL.Extensions
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
        sqlList = QuebrarLinhaACadaTabela(sqlList)

        sqlList = Indentar(sqlList)

        sqlList = AlinharAliasDasTabelas(sqlList)

        Return String.Join(vbLf, sqlList.ToArray())
    End Function

    Private Function AlinharAliasDasTabelas(ByVal sqlList As List(Of String)) As List(Of String)
        If Not ObterValorParametro("AlinharAliasDasTabelas") Then Return sqlList

        Dim statementDetected As String = String.Empty
        Dim sql As String()
        Dim max As Integer

        For i = 1 To sqlList.Count - 1
            statementDetected = DetectStatement(sqlList(i), statementDetected)
            If statementDetected = FROM_STATEMENT OrElse statementDetected = JOIN_STATEMENT Then
                sqlList(i) = sqlList(i).Replace(" AS", "")
                sql = sqlList(i).Split(New Char() {" "}, StringSplitOptions.RemoveEmptyEntries)
                If sql.Length >= 2 Then
                    Dim j = sqlList(i).IndexOf(sql(1), StringComparison.Ordinal)
                    If j + sql(1).Length > max Then
                        max = j + sql(1).Length
                    End If
                End If
            End If

            If statementDetected = TABELA Then
                sqlList(i) = sqlList(i).Replace(" AS", "")
                sql = sqlList(i).Split(New Char() {" "}, StringSplitOptions.RemoveEmptyEntries)
                If sql.Length >= 1 Then
                    Dim j = sqlList(i).IndexOf(sql(0), StringComparison.Ordinal)
                    If j + sql(0).Length > max Then
                        max = j + sql(0).Length
                    End If
                End If
            End If
        Next

        Dim qtdEspacosInicio As Integer
        Dim qtdEspacosAlias As Integer

        For i = 1 To sqlList.Count - 1
            statementDetected = DetectStatement(sqlList(i), statementDetected)
            If statementDetected = FROM_STATEMENT OrElse statementDetected = JOIN_STATEMENT Then
                sql = sqlList(i).Split(New Char() {" "}, StringSplitOptions.RemoveEmptyEntries)
                If sql.Length = 3 Then
                    Dim j = sqlList(i).IndexOf(sql(1), StringComparison.Ordinal)
                    Dim a = j + sql(1).Length

                    'sqlList(i) = sqlList(i).Replace(sql(1), sql(1).PadRight(sql(1).Length + max - a))
                    If ObterValorParametro("UtilizarAClausulaAS") Then
                        If ObterValorParametro("AlinharComATabela") Then
                            qtdEspacosInicio = sqlList(i).IndexOf(sql(0), StringComparison.Ordinal)
                            qtdEspacosAlias = max - a

                            sqlList(i) = String.Format("{0}{1} {2} AS {3}{4}",
                                                       "".PadRight(qtdEspacosInicio),
                                                       sql(0),
                                                       sql(1),
                                                       "".PadRight(qtdEspacosAlias),
                                                       sql(2))
                        Else
                            qtdEspacosInicio = sqlList(i).IndexOf(sql(0), StringComparison.Ordinal)
                            qtdEspacosAlias = max - a

                            sqlList(i) = String.Format("{0}{1} {2}{3} AS {4}",
                                                       "".PadRight(qtdEspacosInicio),
                                                       sql(0),
                                                       sql(1),
                                                       "".PadRight(qtdEspacosAlias),
                                                       sql(2))
                        End If
                    Else
                        sqlList(i) = sqlList(i).Replace(sql(1), sql(1).PadRight(sql(1).Length + max - a))
                    End If
                End If
            End If

            If statementDetected = TABELA Then
                sql = sqlList(i).Split(New Char() {" "}, StringSplitOptions.RemoveEmptyEntries)
                If sql.Length = 2 Then
                    Dim j = sqlList(i).IndexOf(sql(0), StringComparison.Ordinal)
                    Dim a = j + sql(0).Length

                    If ObterValorParametro("UtilizarAClausulaAS") Then
                        If ObterValorParametro("AlinharComATabela") Then
                            qtdEspacosInicio = sqlList(i).IndexOf(sql(0), StringComparison.Ordinal)
                            qtdEspacosAlias = max - a

                            sqlList(i) = String.Format("{0}{1} AS {2}{3}",
                                                       "".PadRight(qtdEspacosInicio),
                                                       sql(0),
                                                       "".PadRight(qtdEspacosAlias),
                                                       sql(1))
                        Else
                            qtdEspacosInicio = sqlList(i).IndexOf(sql(0), StringComparison.Ordinal)
                            qtdEspacosAlias = max - a

                            sqlList(i) = String.Format("{0}{1}{2} AS {3}",
                                                       "".PadRight(qtdEspacosInicio),
                                                       sql(0),
                                                       "".PadRight(qtdEspacosAlias),
                                                       sql(1))
                        End If
                    Else
                        sqlList(i) = sqlList(i).Replace(sql(0), sql(0).PadRight(sql(0).Length + max - a))
                    End If
                End If
            End If
        Next



        Return sqlList
    End Function

    Private Function QuebrarLinhaACadaTabela(ByVal sqlList As List(Of String)) As List(Of String)
        Dim i As Integer = 0
        Dim idxUltimaLinha As Integer = sqlList.Count - 1
        While i <= idxUltimaLinha
            If sqlList(i).Contains(FROM_STATEMENT) Then
                Dim tableList As New List(Of String)
                tableList.AddRange(sqlList(i).Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries))
                tableList.RemoveAt(0)
                If tableList.Any() Then
                    sqlList(i) = sqlList(i).Substring(0, sqlList(i).IndexOf(",", StringComparison.Ordinal))
                    sqlList(i) = sqlList(i).Trim()

                    If ObterValorParametro("VirgulaInicioLinha") Then
                        For j = 0 To tableList.Count - 1
                            tableList(j) = tableList(j).Trim()
                            tableList(j) = ", " & tableList(j)
                        Next
                    Else
                        sqlList(i) = sqlList(i).TrimEnd() & ","
                        For j = 0 To tableList.Count - 2
                            tableList(j) = tableList(j).Trim()
                            tableList(j) = tableList(j) & ","
                        Next
                    End If

                    sqlList.InsertRange(i + 1, tableList)
                End If
            End If

            idxUltimaLinha = sqlList.Count - 1
            i += 1
        End While

        Return sqlList
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
        While text.Contains("  ")
            text = text.Replace("  ", " ")
        End While

        text = " " & text
        text = text.Replace(" " & SELECT_STATEMENT.Trim() & " ", vbLf & " " & SELECT_STATEMENT.Trim() & " ")
        text = text.Replace(" " & FROM_STATEMENT.Trim() & " ", vbLf & " " & FROM_STATEMENT.Trim() & " ")

        text = text.Replace(" " & INNERJOIN_STATEMENT.Trim() & " ", vbLf & " INNER" & vbLf & JOIN_STATEMENT.Trim() & " ")
        text = text.Replace(" " & LEFTJOIN_STATEMENT.Trim() & " ", vbLf & " LEFT" & vbLf & JOIN_STATEMENT.Trim() & " ")
        text = text.Replace(" " & LEFTOUTERJOIN_STATEMENT.Trim() & " ", vbLf & " LEFT OUTER" & vbLf & JOIN_STATEMENT.Trim() & " ")
        text = text.Replace(" " & RIGHTJOIN_STATEMENT.Trim() & " ", vbLf & " RIGHT" & vbLf & JOIN_STATEMENT.Trim() & " ")
        text = text.Replace(" " & RIGHTOUTERJOIN_STATEMENT.Trim() & " ", vbLf & " RIGHT OUTER" & vbLf & JOIN_STATEMENT.Trim() & " ")
        text = text.Replace(" " & FULLJOIN_STATEMENT.Trim() & " ", vbLf & " FULL" & vbLf & JOIN_STATEMENT.Trim() & " ")
        text = text.Replace(" " & FULLOUTERJOIN_STATEMENT.Trim() & " ", vbLf & " FULL OUTER" & vbLf & JOIN_STATEMENT.Trim() & " ")
        text = text.Replace(" " & CROSSJOIN_STATEMENT.Trim() & " ", vbLf & " CROSS" & vbLf & JOIN_STATEMENT.Trim() & " ")

        text = text.Replace(" " & JOIN_STATEMENT.Trim() & " ", vbLf & " " & JOIN_STATEMENT.Trim() & " ")
        text = text.Replace(" " & ON_STATEMENT.Trim() & " ", vbLf & " " & ON_STATEMENT.Trim() & " ")
        text = text.Replace(" " & WHERE_STATEMENT.Trim() & " ", vbLf & " " & WHERE_STATEMENT.Trim() & " ")
        text = text.Replace(" " & ANDWHERE_STATEMENT.Trim() & " ", vbLf & " " & ANDWHERE_STATEMENT.Trim() & " ")
        text = text.Replace(" " & ORWHERE_STATEMENT.Trim() & " ", vbLf & " " & ORWHERE_STATEMENT.Trim() & " ")
        text = text.Replace(" " & GROUPBY_STATEMENT.Trim() & " ", vbLf & " " & GROUPBY_STATEMENT.Trim() & " ")
        text = text.Replace(" " & HAVING_STATEMENT.Trim() & " ", vbLf & " " & HAVING_STATEMENT.Trim() & " ")
        text = text.Replace(" " & ORDERBY_STATEMENT.Trim() & " ", vbLf & " " & ORDERBY_STATEMENT.Trim() & " ")

        Dim sqlList As New List(Of String)
        sqlList.AddRange(text.Split(New Char() {vbLf}, StringSplitOptions.RemoveEmptyEntries))

        While sqlList(0).Trim = ""
            sqlList.RemoveAt(0)
        End While

        Return sqlList
    End Function

    Private Function Indentar(sqlList As List(Of String)) As List(Of String)
        Dim statementDetected As String = ""
        Dim sql As String
        Dim virgula As Integer = If(ObterValorParametro("VirgulaInicioLinha"), -2, 0)
        Dim existeGroupByOrderBy As Boolean = sqlList.Any(Function(p) p.Contains(GROUPBY_STATEMENT) OrElse p.Contains(ORDERBY_STATEMENT))
        Dim orderBySpaces As Integer
        If existeGroupByOrderBy Then orderBySpaces = 2
        Dim indexFunctionStart As Integer

        For index = 0 To sqlList.Count - 1
            sql = sqlList(index).Trim()

            statementDetected = DetectStatement(sql, statementDetected)

            Select Case statementDetected
                Case SELECT_STATEMENT, SELECT_STATEMENT_WITH_FUNCTION_START, SELECT_STATEMENT_WITH_FUNCTION_COMPLETE
                    sqlList(index) = sql.PadLeft(sql.Length + orderBySpaces)
                    indexFunctionStart = index
                Case GROUPBY_STATEMENT_WITH_FUNCTION_START, ORDERBY_STATEMENT_WITH_FUNCTION_START
                    indexFunctionStart = index
                Case COLUMN, COLUMN_WITH_FUNCTION_COMPLETE, COLUMN_WITH_FUNCTION_START
                    sqlList(index) = sql.PadLeft(sql.Length + orderBySpaces + SELECT_SPACES + virgula)
                    indexFunctionStart = index
                Case TABELA
                    sqlList(index) = sql.PadLeft(sql.Length + orderBySpaces + TABELA_SPACES + virgula)
                Case COLUMN_FUNCTION_PARAMETER, COLUMN_WITH_FUNCTION_END
                    Dim qtdCaracteresNomeFunction As Integer = sqlList(indexFunctionStart).IndexOf("(", StringComparison.Ordinal)
                    If Not ObterValorParametro("VirgulaInicioLinha") Then
                        qtdCaracteresNomeFunction += 1
                    End If
                    sqlList(index) = sql.PadLeft(sql.Length + qtdCaracteresNomeFunction)
                Case FROM_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + orderBySpaces + FROM_SPACES)
                Case INNER_STATEMENT, RIGHT_STATEMENT, RIGHTOUTER_STATEMENT, CROSS_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + orderBySpaces + INNER_SPACES)
                Case LEFT_STATEMENT, LEFTOUTER_STATEMENT, FULL_STATEMENT, FULLOUTER_STATEMENT, JOIN_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + orderBySpaces + JOIN_SPACES)
                Case ON_STATEMENT, ORJOIN_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + orderBySpaces + ON_SPACES)
                Case ANDJOIN_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + orderBySpaces + ANDJOIN_SPACES)
                Case WHERE_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + orderBySpaces + WHERE_SPACES)
                Case ANDWHERE_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + orderBySpaces + ANDWHERE_SPACES)
                Case ORWHERE_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + orderBySpaces + ORWHERE_SPACES)
                Case HAVING_STATEMENT
                    sqlList(index) = sql.PadLeft(sql.Length + HAVING_SPACES)
                Case Else
                    sqlList(index) = sql
            End Select
        Next

        Return sqlList
    End Function

    Private Function DetectStatement(txtSql As String, lastDetectedStatement As String) As String
        If txtSql.Contains(SELECT_STATEMENT) Then
            If txtSql.Contains("(") Then
                If txtSql.MesmaQuantidadeParentesisAbreFecha() Then
                    Return SELECT_STATEMENT_WITH_FUNCTION_COMPLETE
                Else
                    Return SELECT_STATEMENT_WITH_FUNCTION_START
                End If
            Else
                Return SELECT_STATEMENT
            End If
        End If

        If txtSql.Contains(FROM_STATEMENT) Then Return FROM_STATEMENT

        If txtSql.Contains(INNER_STATEMENT) Then Return INNER_STATEMENT
        If txtSql.Contains(LEFT_STATEMENT) Then Return LEFT_STATEMENT
        If txtSql.Contains(LEFTOUTER_STATEMENT) Then Return LEFTOUTER_STATEMENT
        If txtSql.Contains(RIGHT_STATEMENT) Then Return RIGHT_STATEMENT
        If txtSql.Contains(RIGHTOUTER_STATEMENT) Then Return RIGHTOUTER_STATEMENT
        If txtSql.Contains(FULL_STATEMENT) Then Return FULL_STATEMENT
        If txtSql.Contains(FULLOUTER_STATEMENT) Then Return FULLOUTER_STATEMENT
        If txtSql.Contains(CROSS_STATEMENT) Then Return CROSS_STATEMENT
        If txtSql.Contains(JOIN_STATEMENT) Then Return JOIN_STATEMENT
        If txtSql.Contains(ON_STATEMENT) Then Return ON_STATEMENT

        If txtSql.Contains(WHERE_STATEMENT) Then Return WHERE_STATEMENT

        If txtSql.Contains(GROUPBY_STATEMENT) Then
            If txtSql.Contains(GROUPBY_STATEMENT) Then
                If txtSql.Contains("(") Then
                    If txtSql.MesmaQuantidadeParentesisAbreFecha() Then
                        Return GROUPBY_STATEMENT_WITH_FUNCTION_COMPLETE
                    Else
                        Return GROUPBY_STATEMENT_WITH_FUNCTION_START
                    End If
                Else
                    Return GROUPBY_STATEMENT
                End If
            End If
        End If

        If txtSql.Contains(HAVING_STATEMENT) Then Return HAVING_STATEMENT

        If txtSql.Contains(ORDERBY_STATEMENT) Then
            If txtSql.Contains(ORDERBY_STATEMENT) Then
                If txtSql.Contains("(") Then
                    If txtSql.MesmaQuantidadeParentesisAbreFecha() Then
                        Return ORDERBY_STATEMENT_WITH_FUNCTION_COMPLETE
                    Else
                        Return ORDERBY_STATEMENT_WITH_FUNCTION_START
                    End If
                Else
                    Return ORDERBY_STATEMENT
                End If
            End If
        End If

        If lastDetectedStatement = ON_STATEMENT OrElse
           lastDetectedStatement = ANDJOIN_STATEMENT OrElse
           lastDetectedStatement = ORJOIN_STATEMENT Then
            If txtSql.Contains(ANDJOIN_STATEMENT) Then Return ANDJOIN_STATEMENT
            If txtSql.Contains(ORJOIN_STATEMENT) Then Return ORJOIN_STATEMENT
        End If

        If lastDetectedStatement = WHERE_STATEMENT OrElse
           lastDetectedStatement = ANDWHERE_STATEMENT OrElse
           lastDetectedStatement = ORWHERE_STATEMENT Then
            If txtSql.Contains(ANDWHERE_STATEMENT) Then Return ANDWHERE_STATEMENT
            If txtSql.Contains(ORWHERE_STATEMENT) Then Return ORWHERE_STATEMENT
        End If

        If lastDetectedStatement = WHERE_STATEMENT OrElse
           lastDetectedStatement = ANDWHERE_STATEMENT OrElse
           lastDetectedStatement = ORWHERE_STATEMENT Then
            If txtSql.Contains(ANDWHERE_STATEMENT) Then Return ANDWHERE_STATEMENT
            If txtSql.Contains(ORWHERE_STATEMENT) Then Return ORWHERE_STATEMENT
        End If

        If lastDetectedStatement = SELECT_STATEMENT_WITH_FUNCTION_START OrElse
           lastDetectedStatement = SELECT_STATEMENT_WITH_FUNCTION_COMPLETE OrElse
           lastDetectedStatement = GROUPBY_STATEMENT_WITH_FUNCTION_START OrElse
           lastDetectedStatement = GROUPBY_STATEMENT_WITH_FUNCTION_COMPLETE OrElse
           lastDetectedStatement = ORDERBY_STATEMENT_WITH_FUNCTION_START OrElse
           lastDetectedStatement = ORDERBY_STATEMENT_WITH_FUNCTION_COMPLETE OrElse
           lastDetectedStatement = COLUMN_WITH_FUNCTION_START Then
            If Not ObterValorParametro("QuebrarLinhaACadaParametroFuncao") Then
                If txtSql.Contains("(") Then
                    If txtSql.MesmaQuantidadeParentesisAbreFecha() Then
                        Return COLUMN_WITH_FUNCTION_COMPLETE
                    Else
                        Return COLUMN_WITH_FUNCTION_START
                    End If
                Else
                    Return COLUMN
                End If
            Else
                If txtSql.FechaMaisParentesisQueAbre() Then
                    Return COLUMN_WITH_FUNCTION_END
                Else
                    Return COLUMN_FUNCTION_PARAMETER
                End If
            End If
        End If

        If lastDetectedStatement = COLUMN_WITH_FUNCTION_START Or
           lastDetectedStatement = COLUMN_FUNCTION_PARAMETER Then
            If txtSql.FechaMaisParentesisQueAbre() Then
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
                If txtSql.MesmaQuantidadeParentesisAbreFecha() Then
                    Return COLUMN_WITH_FUNCTION_COMPLETE
                Else
                    Return COLUMN_WITH_FUNCTION_START
                End If
            Else
                Return COLUMN
            End If
        End If

        If lastDetectedStatement = FROM_STATEMENT Or
           lastDetectedStatement = TABELA Then
            Return TABELA
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
                            selectColumnsList(j) = ", " & selectColumnsList(j)
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
                    sqlList(i) = sqlList(i).Replace(" , ", ", ")
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
                Case SELECT_STATEMENT, FROM_STATEMENT, WHERE_STATEMENT, ANDWHERE_STATEMENT, GROUPBY_STATEMENT, HAVING_STATEMENT, ORDERBY_STATEMENT
                    While sqlList(i).Contains(statementDetected & "  ")
                        sqlList(i) = sqlList(i).Replace(statementDetected & "  ", statementDetected & " ")
                    End While
                Case SELECT_STATEMENT_WITH_FUNCTION_START, SELECT_STATEMENT_WITH_FUNCTION_COMPLETE
                    While sqlList(i).Contains(SELECT_STATEMENT & "  ")
                        sqlList(i) = sqlList(i).Replace(SELECT_STATEMENT & "  ", SELECT_STATEMENT & " ")
                    End While

                Case GROUPBY_STATEMENT_WITH_FUNCTION_START, GROUPBY_STATEMENT_WITH_FUNCTION_COMPLETE
                    While sqlList(i).Contains(GROUPBY_STATEMENT & "  ")
                        sqlList(i) = sqlList(i).Replace(GROUPBY_STATEMENT & "  ", GROUPBY_STATEMENT & " ")
                    End While

                Case ORDERBY_STATEMENT_WITH_FUNCTION_START, ORDERBY_STATEMENT_WITH_FUNCTION_COMPLETE
                    While sqlList(i).Contains(ORDERBY_STATEMENT & "  ")
                        sqlList(i) = sqlList(i).Replace(ORDERBY_STATEMENT & "  ", ORDERBY_STATEMENT & " ")
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
                Case COLUMN_WITH_FUNCTION_START, SELECT_STATEMENT_WITH_FUNCTION_START, GROUPBY_STATEMENT_WITH_FUNCTION_START, ORDERBY_STATEMENT_WITH_FUNCTION_START
                    If Not ObterValorParametro("QuebrarLinhaACadaParametroFuncao") Then
                        If sqlList(i).AbreMaisParentesisQueFecha() Then

                            Dim j As Integer = i + 1
                            While sqlList(j).Count(Function(p) p = "(") >= sqlList(j).Count(Function(p) p = ")")
                                sqlList(i) &= " " & sqlList(j).Trim()

                                If ObterValorParametro("VirgulaInicioLinha") Then
                                    sqlList(i) = sqlList(i).Replace(" , ", ", ")
                                End If

                                j += 1
                            End While

                            sqlList(i) &= " " & sqlList(j).Trim()

                            If ObterValorParametro("VirgulaInicioLinha") Then
                                sqlList(i) = sqlList(i).Replace(" , ", ", ")
                            End If

                            For k = i + 1 To j
                                sqlList.RemoveAt(i + 1)
                            Next
                        End If

                    End If

                Case COLUMN_FUNCTION_PARAMETER, COLUMN_WITH_FUNCTION_END
                    If ObterValorParametro("QuebrarLinhaACadaParametroFuncao") AndAlso ObterValorParametro("VirgulaInicioLinha") Then
                        sqlList(i) = sqlList(i).Replace(", ", ",")
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
