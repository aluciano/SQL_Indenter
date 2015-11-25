Imports System.Runtime.CompilerServices

Namespace Extensions
    Public Module StringExtension

        <Extension>
        Public Function MesmaQuantidadeParentesisAbreFecha(texto As String) As Boolean
            Return texto.Count(Function(p) p = "(") = texto.Count(Function(p) p = ")")
        End Function

        <Extension>
        Public Function FechaMaisParentesisQueAbre(texto As String) As Boolean
            Return texto.Count(Function(p) p = ")") > texto.Count(Function(p) p = "(")
        End Function

        <Extension>
        Public Function AbreMaisParentesisQueFecha(texto As String) As Boolean
            Return texto.Count(Function(p) p = "(") > texto.Count(Function(p) p = ")")
        End Function

    End Module
End Namespace