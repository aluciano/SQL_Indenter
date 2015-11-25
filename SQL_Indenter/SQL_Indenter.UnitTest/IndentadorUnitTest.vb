Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports SQL_Indenter.BEL
Imports SQL_Indenter.BLL

<TestClass()> Public Class IndentadorUnitTest

#Region "Cenários de parametrizações de Interface"
    Private Function ObterParametrosInterface_Cenario_01() As List(Of ParametroInterfaceBEL)
        Dim parametrosInterface As New List(Of ParametroInterfaceBEL)

        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaColuna", True))
        parametrosInterface.Add(New ParametroInterfaceBEL("VirgulaInicioLinha", True))
        parametrosInterface.Add(New ParametroInterfaceBEL("SepararOperadoresLogicosComUmEspaco", False))
        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaParametroFuncao", True))

        Return parametrosInterface
    End Function

    Private Function ObterParametrosInterface_Cenario_02() As List(Of ParametroInterfaceBEL)
        Dim parametrosInterface As New List(Of ParametroInterfaceBEL)

        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaColuna", True))
        parametrosInterface.Add(New ParametroInterfaceBEL("VirgulaInicioLinha", False))
        parametrosInterface.Add(New ParametroInterfaceBEL("SepararOperadoresLogicosComUmEspaco", False))
        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaParametroFuncao", True))

        Return parametrosInterface
    End Function

    Private Function ObterParametrosInterface_Cenario_03() As List(Of ParametroInterfaceBEL)
        Dim parametrosInterface As New List(Of ParametroInterfaceBEL)

        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaColuna", False))
        parametrosInterface.Add(New ParametroInterfaceBEL("VirgulaInicioLinha", True))
        parametrosInterface.Add(New ParametroInterfaceBEL("SepararOperadoresLogicosComUmEspaco", False))
        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaParametroFuncao", True))

        Return parametrosInterface
    End Function

    Private Function ObterParametrosInterface_Cenario_04() As List(Of ParametroInterfaceBEL)
        Dim parametrosInterface As New List(Of ParametroInterfaceBEL)

        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaColuna", True))
        parametrosInterface.Add(New ParametroInterfaceBEL("VirgulaInicioLinha", True))
        parametrosInterface.Add(New ParametroInterfaceBEL("SepararOperadoresLogicosComUmEspaco", True))
        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaParametroFuncao", True))

        Return parametrosInterface
    End Function

    Private Function ObterParametrosInterface_Cenario_05() As List(Of ParametroInterfaceBEL)
        Dim parametrosInterface As New List(Of ParametroInterfaceBEL)

        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaColuna", True))
        parametrosInterface.Add(New ParametroInterfaceBEL("VirgulaInicioLinha", False))
        parametrosInterface.Add(New ParametroInterfaceBEL("SepararOperadoresLogicosComUmEspaco", False))
        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaParametroFuncao", True))

        Return parametrosInterface
    End Function

    Private Function ObterParametrosInterface_Cenario_06() As List(Of ParametroInterfaceBEL)
        Dim parametrosInterface As New List(Of ParametroInterfaceBEL)

        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaColuna", True))
        parametrosInterface.Add(New ParametroInterfaceBEL("VirgulaInicioLinha", False))
        parametrosInterface.Add(New ParametroInterfaceBEL("SepararOperadoresLogicosComUmEspaco", True))
        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaParametroFuncao", False))

        Return parametrosInterface
    End Function

    Private Function ObterParametrosInterface_Cenario_07() As List(Of ParametroInterfaceBEL)
        Dim parametrosInterface As New List(Of ParametroInterfaceBEL)

        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaColuna", True))
        parametrosInterface.Add(New ParametroInterfaceBEL("VirgulaInicioLinha", False))
        parametrosInterface.Add(New ParametroInterfaceBEL("SepararOperadoresLogicosComUmEspaco", True))
        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaParametroFuncao", True))

        Return parametrosInterface
    End Function

    Private Function ObterParametrosInterface_Cenario_08() As List(Of ParametroInterfaceBEL)
        Dim parametrosInterface As New List(Of ParametroInterfaceBEL)

        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaColuna", True))
        parametrosInterface.Add(New ParametroInterfaceBEL("VirgulaInicioLinha", True))
        parametrosInterface.Add(New ParametroInterfaceBEL("SepararOperadoresLogicosComUmEspaco", True))
        parametrosInterface.Add(New ParametroInterfaceBEL("QuebrarLinhaACadaParametroFuncao", False))

        Return parametrosInterface
    End Function

#End Region

    <TestMethod()> Public Sub IndentarSelectSimples_Cenario_01()
        Dim indentador As New IndentadorBLL(ObterParametrosInterface_Cenario_01())

        Const scriptSql As String = "SELECT TESTE1, TESTE2, TESTE3, TESTE4 FROM TABELA WHERE TESTE1=1"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Const retornoEsperado As String = "SELECT TESTE1" & vbLf _
                                        & "     , TESTE2" & vbLf _
                                        & "     , TESTE3" & vbLf _
                                        & "     , TESTE4" & vbLf _
                                        & "  FROM TABELA" & vbLf _
                                        & " WHERE TESTE1=1"

        Assert.AreEqual(retornoEsperado, retorno)
    End Sub

    <TestMethod()> Public Sub IndentarSelectSimples_Cenario_02()
        Dim indentador As New IndentadorBLL(ObterParametrosInterface_Cenario_02())

        Const scriptSql As String = "SELECT TESTE1, TESTE2, TESTE3, TESTE4 FROM TABELA WHERE TESTE1=1"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Const retornoEsperado As String = "SELECT TESTE1," & vbLf _
                                        & "       TESTE2," & vbLf _
                                        & "       TESTE3," & vbLf _
                                        & "       TESTE4" & vbLf _
                                        & "  FROM TABELA" & vbLf _
                                        & " WHERE TESTE1=1"

        Assert.AreEqual(retornoEsperado, retorno)
    End Sub

    <TestMethod()> Public Sub IndentarSelectSimples_Cenario_03()
        Dim indentador As New IndentadorBLL(ObterParametrosInterface_Cenario_03())

        Const scriptSql As String = "SELECT TESTE1, TESTE2, TESTE3, TESTE4 FROM TABELA WHERE TESTE1=1"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Const retornoEsperado As String = "SELECT TESTE1, TESTE2, TESTE3, TESTE4" & vbLf _
                                        & "  FROM TABELA" & vbLf _
                                        & " WHERE TESTE1=1"

        Assert.AreEqual(retornoEsperado, retorno)
    End Sub

    <TestMethod()> Public Sub IndentarSelectSimples_Cenario_04()
        Dim indentador As New IndentadorBLL(ObterParametrosInterface_Cenario_04())

        Const scriptSql As String = "SELECT TESTE1, TESTE2, TESTE3, TESTE4 FROM TABELA WHERE TESTE1>=1 AND TESTE2=100 AND TESTE3<>TESTE4 AND TESTE1<TESTE2 AND TESTE2>TESTE3 AND TESTE1*=TESTE2 AND TESTE2=*TEST4 AND TESTE2<=TESTE1"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Const retornoEsperado As String = "SELECT TESTE1" & vbLf _
                                        & "     , TESTE2" & vbLf _
                                        & "     , TESTE3" & vbLf _
                                        & "     , TESTE4" & vbLf _
                                        & "  FROM TABELA" & vbLf _
                                        & " WHERE TESTE1 >= 1" & vbLf _
                                        & "   AND TESTE2 = 100" & vbLf _
                                        & "   AND TESTE3 <> TESTE4" & vbLf _
                                        & "   AND TESTE1 < TESTE2" & vbLf _
                                        & "   AND TESTE2 > TESTE3" & vbLf _
                                        & "   AND TESTE1 *= TESTE2" & vbLf _
                                        & "   AND TESTE2 =* TEST4" & vbLf _
                                        & "   AND TESTE2 <= TESTE1"

        Assert.AreEqual(retornoEsperado, retorno)
    End Sub

    <TestMethod()> Public Sub IndentarSelectSimples_Cenario_04_1()
        Dim indentador As New IndentadorBLL(ObterParametrosInterface_Cenario_04())

        Const scriptSql As String = "SELECT   TESTE1,  TESTE2 , TESTE3,    TESTE4   FROM  TABELA WHERE TESTE1   >=1  AND TESTE2=100  AND   TESTE3    <>TESTE4    AND  TESTE1<TESTE2 AND TESTE2>  TESTE3 AND TESTE1*=TESTE2 AND TESTE2=*TEST4 AND TESTE2<=TESTE1"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Const retornoEsperado As String = "SELECT TESTE1" & vbLf _
                                        & "     , TESTE2" & vbLf _
                                        & "     , TESTE3" & vbLf _
                                        & "     , TESTE4" & vbLf _
                                        & "  FROM TABELA" & vbLf _
                                        & " WHERE TESTE1 >= 1" & vbLf _
                                        & "   AND TESTE2 = 100" & vbLf _
                                        & "   AND TESTE3 <> TESTE4" & vbLf _
                                        & "   AND TESTE1 < TESTE2" & vbLf _
                                        & "   AND TESTE2 > TESTE3" & vbLf _
                                        & "   AND TESTE1 *= TESTE2" & vbLf _
                                        & "   AND TESTE2 =* TEST4" & vbLf _
                                        & "   AND TESTE2 <= TESTE1"

        Assert.AreEqual(retornoEsperado, retorno)
    End Sub

    <TestMethod()> Public Sub IndentarSelectSimples_Cenario_05()
        Dim indentador As New IndentadorBLL(ObterParametrosInterface_Cenario_05())

        Const scriptSql As String = "SELECT   TESTE1,  TESTE2 , TESTE3,    TESTE4   FROM  TABELA WHERE TESTE1   >=1  AND TESTE2=100  AND   TESTE3    <>TESTE4    AND  TESTE1<TESTE2 AND TESTE2>  TESTE3 AND TESTE1*=TESTE2 AND TESTE2=*TEST4 AND TESTE2<=TESTE1"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Const retornoEsperado As String = "SELECT TESTE1," & vbLf _
                                        & "       TESTE2," & vbLf _
                                        & "       TESTE3," & vbLf _
                                        & "       TESTE4" & vbLf _
                                        & "  FROM TABELA" & vbLf _
                                        & " WHERE TESTE1>=1" & vbLf _
                                        & "   AND TESTE2=100" & vbLf _
                                        & "   AND TESTE3<>TESTE4" & vbLf _
                                        & "   AND TESTE1<TESTE2" & vbLf _
                                        & "   AND TESTE2>TESTE3" & vbLf _
                                        & "   AND TESTE1*=TESTE2" & vbLf _
                                        & "   AND TESTE2=*TEST4" & vbLf _
                                        & "   AND TESTE2<=TESTE1"

        Assert.AreEqual(retornoEsperado, retorno)
    End Sub

    <TestMethod()> Public Sub IndentarSelectSimples_Cenario_06()
        Dim indentador As New IndentadorBLL(ObterParametrosInterface_Cenario_06())

        Const scriptSql As String = "SELECT   TESTE1,  TESTE2 , TESTE3,    COUNT(1)   FROM  TABELA WHERE TESTE1   >=1  AND TESTE2=100  AND   TESTE3    <>TESTE4    AND  TESTE1<TESTE2 AND TESTE2>  TESTE3 AND TESTE1*=TESTE2 AND TESTE2=*TEST4 AND TESTE2<=TESTE1   GROUP BY    TESTE1,  TESTE2,    TESTE3   ORDER BY     TESTE1, TESTE2 HAVING COUNT(1) > 0"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Const retornoEsperado As String = "  SELECT TESTE1," & vbLf _
                                        & "         TESTE2," & vbLf _
                                        & "         TESTE3," & vbLf _
                                        & "         COUNT(1)" & vbLf _
                                        & "    FROM TABELA" & vbLf _
                                        & "   WHERE TESTE1 >= 1" & vbLf _
                                        & "     AND TESTE2 = 100" & vbLf _
                                        & "     AND TESTE3 <> TESTE4" & vbLf _
                                        & "     AND TESTE1 < TESTE2" & vbLf _
                                        & "     AND TESTE2 > TESTE3" & vbLf _
                                        & "     AND TESTE1 *= TESTE2" & vbLf _
                                        & "     AND TESTE2 =* TEST4" & vbLf _
                                        & "     AND TESTE2 <= TESTE1" & vbLf _
                                        & "GROUP BY TESTE1," & vbLf _
                                        & "         TESTE2," & vbLf _
                                        & "         TESTE3" & vbLf _
                                        & "ORDER BY TESTE1," & vbLf _
                                        & "         TESTE2" & vbLf _
                                        & "  HAVING COUNT(1) > 0"

        Assert.AreEqual(retornoEsperado, retorno)
    End Sub

    <TestMethod()> Public Sub IndentarSelectSimples_Cenario_06_1()
        Dim indentador As New IndentadorBLL(ObterParametrosInterface_Cenario_06())

        Const scriptSql As String = "SELECT   TESTE1,  TESTE2 , CONVERT(varchar(10), TESTE3, 112),    COUNT(1)   FROM  TABELA WHERE TESTE1   >=1  AND TESTE2=100  AND   TESTE3    <>TESTE4    AND  TESTE1<TESTE2 AND TESTE2>  TESTE3 AND TESTE1*=TESTE2 AND TESTE2=*TEST4 AND TESTE2<=TESTE1   GROUP BY    TESTE1,  TESTE2,    TESTE3   ORDER BY     TESTE1, TESTE2 HAVING COUNT(1) > 0"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Const retornoEsperado As String = "  SELECT TESTE1," & vbLf _
                                        & "         TESTE2," & vbLf _
                                        & "         CONVERT(varchar(10), TESTE3, 112)," & vbLf _
                                        & "         COUNT(1)" & vbLf _
                                        & "    FROM TABELA" & vbLf _
                                        & "   WHERE TESTE1 >= 1" & vbLf _
                                        & "     AND TESTE2 = 100" & vbLf _
                                        & "     AND TESTE3 <> TESTE4" & vbLf _
                                        & "     AND TESTE1 < TESTE2" & vbLf _
                                        & "     AND TESTE2 > TESTE3" & vbLf _
                                        & "     AND TESTE1 *= TESTE2" & vbLf _
                                        & "     AND TESTE2 =* TEST4" & vbLf _
                                        & "     AND TESTE2 <= TESTE1" & vbLf _
                                        & "GROUP BY TESTE1," & vbLf _
                                        & "         TESTE2," & vbLf _
                                        & "         TESTE3" & vbLf _
                                        & "ORDER BY TESTE1," & vbLf _
                                        & "         TESTE2" & vbLf _
                                        & "  HAVING COUNT(1) > 0"

        Assert.AreEqual(retornoEsperado, retorno)
    End Sub

    <TestMethod()> Public Sub IndentarSelectSimples_Cenario_07()
        Dim indentador As New IndentadorBLL(ObterParametrosInterface_Cenario_07())

        Const scriptSql As String = "SELECT   TESTE1,  TESTE2 , CONVERT(varchar(10), TESTE3, 112),    COUNT(1)   FROM  TABELA WHERE TESTE1   >=1  AND TESTE2=100  AND   TESTE3    <>TESTE4    AND  TESTE1<TESTE2 AND TESTE2>  TESTE3 AND TESTE1*=TESTE2 AND TESTE2=*TEST4 AND TESTE2<=TESTE1   GROUP BY    TESTE1,  TESTE2,    TESTE3   ORDER BY     TESTE1, TESTE2 HAVING COUNT(1) > 0"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Const retornoEsperado As String = "  SELECT TESTE1," & vbLf _
                                        & "         TESTE2," & vbLf _
                                        & "         CONVERT(varchar(10)," & vbLf _
                                        & "                 TESTE3," & vbLf _
                                        & "                 112)," & vbLf _
                                        & "         COUNT(1)" & vbLf _
                                        & "    FROM TABELA" & vbLf _
                                        & "   WHERE TESTE1 >= 1" & vbLf _
                                        & "     AND TESTE2 = 100" & vbLf _
                                        & "     AND TESTE3 <> TESTE4" & vbLf _
                                        & "     AND TESTE1 < TESTE2" & vbLf _
                                        & "     AND TESTE2 > TESTE3" & vbLf _
                                        & "     AND TESTE1 *= TESTE2" & vbLf _
                                        & "     AND TESTE2 =* TEST4" & vbLf _
                                        & "     AND TESTE2 <= TESTE1" & vbLf _
                                        & "GROUP BY TESTE1," & vbLf _
                                        & "         TESTE2," & vbLf _
                                        & "         TESTE3" & vbLf _
                                        & "ORDER BY TESTE1," & vbLf _
                                        & "         TESTE2" & vbLf _
                                        & "  HAVING COUNT(1) > 0"

        Assert.AreEqual(retornoEsperado, retorno)
    End Sub

    <TestMethod()> Public Sub IndentarSelectSimples_Cenario_08()
        Dim indentador As New IndentadorBLL(ObterParametrosInterface_Cenario_06())

        Const scriptSql As String = "SELECT   CONVERT(varchar(10), TESTE1, 112),  TESTE2 , CONVERT(varchar(10), TESTE3, 112),    COUNT(1)   FROM  TABELA WHERE TESTE1   >=1  AND TESTE2=100  AND   TESTE3    <>TESTE4    AND  TESTE1<TESTE2 AND TESTE2>  TESTE3 AND TESTE1*=TESTE2 AND TESTE2=*TEST4 AND TESTE2<=TESTE1   GROUP BY    CONVERT(varchar(10), TESTE1, 112),  TESTE2,    CONVERT(varchar(10), TESTE3, 112)   ORDER BY     CONVERT(varchar(10), TESTE1, 112), TESTE2 HAVING COUNT(1) > 0"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Const retornoEsperado As String = "  SELECT CONVERT(varchar(10), TESTE1, 112)," & vbLf _
                                        & "         TESTE2," & vbLf _
                                        & "         CONVERT(varchar(10), TESTE3, 112)," & vbLf _
                                        & "         COUNT(1)" & vbLf _
                                        & "    FROM TABELA" & vbLf _
                                        & "   WHERE TESTE1 >= 1" & vbLf _
                                        & "     AND TESTE2 = 100" & vbLf _
                                        & "     AND TESTE3 <> TESTE4" & vbLf _
                                        & "     AND TESTE1 < TESTE2" & vbLf _
                                        & "     AND TESTE2 > TESTE3" & vbLf _
                                        & "     AND TESTE1 *= TESTE2" & vbLf _
                                        & "     AND TESTE2 =* TEST4" & vbLf _
                                        & "     AND TESTE2 <= TESTE1" & vbLf _
                                        & "GROUP BY CONVERT(varchar(10), TESTE1, 112)," & vbLf _
                                        & "         TESTE2," & vbLf _
                                        & "         CONVERT(varchar(10), TESTE3, 112)" & vbLf _
                                        & "ORDER BY CONVERT(varchar(10), TESTE1, 112)," & vbLf _
                                        & "         TESTE2" & vbLf _
                                        & "  HAVING COUNT(1) > 0"

        Assert.AreEqual(retornoEsperado, retorno)
    End Sub

    <TestMethod()> Public Sub IndentarSelectSimples_Cenario_09()
        Dim indentador As New IndentadorBLL(ObterParametrosInterface_Cenario_07())

        Const scriptSql As String = "SELECT   CONVERT(varchar(10), TESTE1, 112),  TESTE2 , CONVERT(varchar(10), TESTE3, 112),    COUNT(1)   FROM  TABELA WHERE TESTE1   >=1  AND TESTE2=100  AND   TESTE3    <>TESTE4    AND  TESTE1<TESTE2 AND TESTE2>  TESTE3 AND TESTE1*=TESTE2 AND TESTE2=*TEST4 AND TESTE2<=TESTE1   GROUP BY    CONVERT(varchar(10), TESTE1, 112),  TESTE2,    CONVERT(varchar(10), TESTE3, 112)   ORDER BY     CONVERT(varchar(10), TESTE1, 112), TESTE2 HAVING COUNT(1) > 0"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Const retornoEsperado As String = "  SELECT CONVERT(varchar(10)," & vbLf _
                                        & "                 TESTE1," & vbLf _
                                        & "                 112)," & vbLf _
                                        & "         TESTE2," & vbLf _
                                        & "         CONVERT(varchar(10)," & vbLf _
                                        & "                 TESTE3," & vbLf _
                                        & "                 112)," & vbLf _
                                        & "         COUNT(1)" & vbLf _
                                        & "    FROM TABELA" & vbLf _
                                        & "   WHERE TESTE1 >= 1" & vbLf _
                                        & "     AND TESTE2 = 100" & vbLf _
                                        & "     AND TESTE3 <> TESTE4" & vbLf _
                                        & "     AND TESTE1 < TESTE2" & vbLf _
                                        & "     AND TESTE2 > TESTE3" & vbLf _
                                        & "     AND TESTE1 *= TESTE2" & vbLf _
                                        & "     AND TESTE2 =* TEST4" & vbLf _
                                        & "     AND TESTE2 <= TESTE1" & vbLf _
                                        & "GROUP BY CONVERT(varchar(10)," & vbLf _
                                        & "                 TESTE1," & vbLf _
                                        & "                 112)," & vbLf _
                                        & "         TESTE2," & vbLf _
                                        & "         CONVERT(varchar(10)," & vbLf _
                                        & "                 TESTE3," & vbLf _
                                        & "                 112)" & vbLf _
                                        & "ORDER BY CONVERT(varchar(10)," & vbLf _
                                        & "                 TESTE1," & vbLf _
                                        & "                 112)," & vbLf _
                                        & "         TESTE2" & vbLf _
                                        & "  HAVING COUNT(1) > 0"

        Assert.AreEqual(retornoEsperado, retorno)
    End Sub

    <TestMethod()> Public Sub IndentarSelectSimples_Cenario_10()
        Dim indentador As New IndentadorBLL(ObterParametrosInterface_Cenario_08())

        Const scriptSql As String = "SELECT   CONVERT(varchar(10), TESTE1, 112),  TESTE2 , CONVERT(varchar(10), TESTE3, 112),    COUNT(1)   FROM  TABELA WHERE TESTE1   >=1  AND TESTE2=100  AND   TESTE3    <>TESTE4    AND  TESTE1<TESTE2 AND TESTE2>  TESTE3 AND TESTE1*=TESTE2 AND TESTE2=*TEST4 AND TESTE2<=TESTE1   GROUP BY    CONVERT(varchar(10), TESTE1, 112),  TESTE2,    CONVERT(varchar(10), TESTE3, 112)   ORDER BY     CONVERT(varchar(10), TESTE1, 112), TESTE2 HAVING COUNT(1) > 0"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Const retornoEsperado As String = "  SELECT CONVERT(varchar(10), TESTE1, 112)" & vbLf _
                                        & "       , TESTE2" & vbLf _
                                        & "       , CONVERT(varchar(10), TESTE3, 112)" & vbLf _
                                        & "       , COUNT(1)" & vbLf _
                                        & "    FROM TABELA" & vbLf _
                                        & "   WHERE TESTE1 >= 1" & vbLf _
                                        & "     AND TESTE2 = 100" & vbLf _
                                        & "     AND TESTE3 <> TESTE4" & vbLf _
                                        & "     AND TESTE1 < TESTE2" & vbLf _
                                        & "     AND TESTE2 > TESTE3" & vbLf _
                                        & "     AND TESTE1 *= TESTE2" & vbLf _
                                        & "     AND TESTE2 =* TEST4" & vbLf _
                                        & "     AND TESTE2 <= TESTE1" & vbLf _
                                        & "GROUP BY CONVERT(varchar(10), TESTE1, 112)" & vbLf _
                                        & "       , TESTE2" & vbLf _
                                        & "       , CONVERT(varchar(10), TESTE3, 112)" & vbLf _
                                        & "ORDER BY CONVERT(varchar(10), TESTE1, 112)" & vbLf _
                                        & "       , TESTE2" & vbLf _
                                        & "  HAVING COUNT(1) > 0"

        Assert.AreEqual(retornoEsperado, retorno)
    End Sub

    <TestMethod()> Public Sub IndentarSelectSimples_Cenario_11()
        Dim indentador As New IndentadorBLL(ObterParametrosInterface_Cenario_04())

        Const scriptSql As String = "SELECT   CONVERT(varchar(10), TESTE1, 112),  TESTE2 , CONVERT(varchar(10), TESTE3, 112),    COUNT(1)   FROM  TABELA WHERE TESTE1   >=1  AND TESTE2=100  AND   TESTE3    <>TESTE4    AND  TESTE1<TESTE2 AND TESTE2>  TESTE3 AND TESTE1*=TESTE2 AND TESTE2=*TEST4 AND TESTE2<=TESTE1   GROUP BY    CONVERT(varchar(10), TESTE1, 112),  TESTE2,    CONVERT(varchar(10), TESTE3, 112)   ORDER BY     CONVERT(varchar(10), TESTE1, 112), TESTE2 HAVING COUNT(1) > 0"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Const retornoEsperado As String = "  SELECT CONVERT(varchar(10)" & vbLf _
                                        & "                ,TESTE1" & vbLf _
                                        & "                ,112)" & vbLf _
                                        & "       , TESTE2" & vbLf _
                                        & "       , CONVERT(varchar(10)" & vbLf _
                                        & "                ,TESTE3" & vbLf _
                                        & "                ,112)" & vbLf _
                                        & "       , COUNT(1)" & vbLf _
                                        & "    FROM TABELA" & vbLf _
                                        & "   WHERE TESTE1 >= 1" & vbLf _
                                        & "     AND TESTE2 = 100" & vbLf _
                                        & "     AND TESTE3 <> TESTE4" & vbLf _
                                        & "     AND TESTE1 < TESTE2" & vbLf _
                                        & "     AND TESTE2 > TESTE3" & vbLf _
                                        & "     AND TESTE1 *= TESTE2" & vbLf _
                                        & "     AND TESTE2 =* TEST4" & vbLf _
                                        & "     AND TESTE2 <= TESTE1" & vbLf _
                                        & "GROUP BY CONVERT(varchar(10)" & vbLf _
                                        & "                ,TESTE1" & vbLf _
                                        & "                ,112)" & vbLf _
                                        & "       , TESTE2" & vbLf _
                                        & "       , CONVERT(varchar(10)" & vbLf _
                                        & "                ,TESTE3" & vbLf _
                                        & "                ,112)" & vbLf _
                                        & "ORDER BY CONVERT(varchar(10)" & vbLf _
                                        & "                ,TESTE1" & vbLf _
                                        & "                ,112)" & vbLf _
                                        & "       , TESTE2" & vbLf _
                                        & "  HAVING COUNT(1) > 0"

        Assert.AreEqual(retornoEsperado, retorno)
    End Sub

End Class