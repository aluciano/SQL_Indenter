Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports SQL_Indenter.BEL
Imports SQL_Indenter.BLL

<TestClass()> Public Class IndentadorUnitTest

#Region "Cenários de parametrizações de Interface"
    Private Function ObterParametrosInterface_Cenario_01() As List(Of ParametroInterface)
        Dim parametrosInterface As New List(Of ParametroInterface)

        parametrosInterface.Add(New ParametroInterface("QuebrarLinhaACadaColuna", True))
        parametrosInterface.Add(New ParametroInterface("VirgulaInicioLinha", True))
        parametrosInterface.Add(New ParametroInterface("SepararOperadoresLogicosComUmEspaco", False))

        Return parametrosInterface
    End Function

    Private Function ObterParametrosInterface_Cenario_02() As List(Of ParametroInterface)
        Dim parametrosInterface As New List(Of ParametroInterface)

        parametrosInterface.Add(New ParametroInterface("QuebrarLinhaACadaColuna", True))
        parametrosInterface.Add(New ParametroInterface("VirgulaInicioLinha", False))
        parametrosInterface.Add(New ParametroInterface("SepararOperadoresLogicosComUmEspaco", False))

        Return parametrosInterface
    End Function

    Private Function ObterParametrosInterface_Cenario_03() As List(Of ParametroInterface)
        Dim parametrosInterface As New List(Of ParametroInterface)

        parametrosInterface.Add(New ParametroInterface("QuebrarLinhaACadaColuna", False))
        parametrosInterface.Add(New ParametroInterface("VirgulaInicioLinha", True))
        parametrosInterface.Add(New ParametroInterface("SepararOperadoresLogicosComUmEspaco", False))

        Return parametrosInterface
    End Function

    Private Function ObterParametrosInterface_Cenario_04() As List(Of ParametroInterface)
        Dim parametrosInterface As New List(Of ParametroInterface)

        parametrosInterface.Add(New ParametroInterface("QuebrarLinhaACadaColuna", True))
        parametrosInterface.Add(New ParametroInterface("VirgulaInicioLinha", True))
        parametrosInterface.Add(New ParametroInterface("SepararOperadoresLogicosComUmEspaco", True))

        Return parametrosInterface
    End Function

    Private Function ObterParametrosInterface_Cenario_05() As List(Of ParametroInterface)
        Dim parametrosInterface As New List(Of ParametroInterface)

        parametrosInterface.Add(New ParametroInterface("QuebrarLinhaACadaColuna", True))
        parametrosInterface.Add(New ParametroInterface("VirgulaInicioLinha", False))
        parametrosInterface.Add(New ParametroInterface("SepararOperadoresLogicosComUmEspaco", False))

        Return parametrosInterface
    End Function

#End Region

    <TestMethod()> Public Sub IndentarSelectSimples_Cenario_01()
        Dim indentador As New Indentador(ObterParametrosInterface_Cenario_01())

        Dim scriptSql As String = "SELECT TESTE1, TESTE2, TESTE3, TESTE4 FROM TABELA WHERE TESTE1=1"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Dim retornoEsperado As String = "SELECT TESTE1" & vbLf _
                                      & "      ,TESTE2" & vbLf _
                                      & "      ,TESTE3" & vbLf _
                                      & "      ,TESTE4" & vbLf _
                                      & "  FROM TABELA" & vbLf _
                                      & " WHERE TESTE1=1"

        Assert.AreEqual(retornoEsperado, retorno)
    End Sub

    <TestMethod()> Public Sub IndentarSelectSimples_Cenario_02()
        Dim indentador As New Indentador(ObterParametrosInterface_Cenario_02())

        Dim scriptSql As String = "SELECT TESTE1, TESTE2, TESTE3, TESTE4 FROM TABELA WHERE TESTE1=1"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Dim retornoEsperado As String = "SELECT TESTE1," & vbLf _
                                      & "       TESTE2," & vbLf _
                                      & "       TESTE3," & vbLf _
                                      & "       TESTE4" & vbLf _
                                      & "  FROM TABELA" & vbLf _
                                      & " WHERE TESTE1=1"

        Assert.AreEqual(retornoEsperado, retorno)
    End Sub

    <TestMethod()> Public Sub IndentarSelectSimples_Cenario_03()
        Dim indentador As New Indentador(ObterParametrosInterface_Cenario_03())

        Dim scriptSql As String = "SELECT TESTE1, TESTE2, TESTE3, TESTE4 FROM TABELA WHERE TESTE1=1"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Dim retornoEsperado As String = "SELECT TESTE1, TESTE2, TESTE3, TESTE4" & vbLf _
                                      & "  FROM TABELA" & vbLf _
                                      & " WHERE TESTE1=1"

        Assert.AreEqual(retornoEsperado, retorno)
    End Sub

    <TestMethod()> Public Sub IndentarSelectSimples_Cenario_04()
        Dim indentador As New Indentador(ObterParametrosInterface_Cenario_04())

        Dim scriptSql As String = "SELECT TESTE1, TESTE2, TESTE3, TESTE4 FROM TABELA WHERE TESTE1>=1 AND TESTE2=100 AND TESTE3<>TESTE4 AND TESTE1<TESTE2 AND TESTE2>TESTE3 AND TESTE1*=TESTE2 AND TESTE2=*TEST4 AND TESTE2<=TESTE1"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Dim retornoEsperado As String = "SELECT TESTE1" & vbLf _
                                      & "      ,TESTE2" & vbLf _
                                      & "      ,TESTE3" & vbLf _
                                      & "      ,TESTE4" & vbLf _
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
        Dim indentador As New Indentador(ObterParametrosInterface_Cenario_04())

        Dim scriptSql As String = "SELECT   TESTE1,  TESTE2 , TESTE3,    TESTE4   FROM  TABELA WHERE TESTE1   >=1  AND TESTE2=100  AND   TESTE3    <>TESTE4    AND  TESTE1<TESTE2 AND TESTE2>  TESTE3 AND TESTE1*=TESTE2 AND TESTE2=*TEST4 AND TESTE2<=TESTE1"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Dim retornoEsperado As String = "SELECT TESTE1" & vbLf _
                                      & "      ,TESTE2" & vbLf _
                                      & "      ,TESTE3" & vbLf _
                                      & "      ,TESTE4" & vbLf _
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
        Dim indentador As New Indentador(ObterParametrosInterface_Cenario_05())

        Dim scriptSql As String = "SELECT   TESTE1,  TESTE2 , TESTE3,    TESTE4   FROM  TABELA WHERE TESTE1   >=1  AND TESTE2=100  AND   TESTE3    <>TESTE4    AND  TESTE1<TESTE2 AND TESTE2>  TESTE3 AND TESTE1*=TESTE2 AND TESTE2=*TEST4 AND TESTE2<=TESTE1"
        Dim retorno As String = indentador.IndentarScriptSql(scriptSql)
        Dim retornoEsperado As String = "SELECT TESTE1," & vbLf _
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

End Class