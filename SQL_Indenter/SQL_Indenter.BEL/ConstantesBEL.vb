Public Class ConstantesBEL

#Region "Statements"
    Public Const SELECT_STATEMENT As String = "SELECT"
    Public Const SELECT_STATEMENT_WITH_FUNCTION_START As String = "SELECT_STATEMENT_WITH_FUNCTION_START"
    Public Const SELECT_STATEMENT_WITH_FUNCTION_COMPLETE As String = "SELECT_STATEMENT_WITH_FUNCTION_COMPLETE"

    Public Const COLUMN As String = "COLUMN"
    Public Const COLUMN_WITH_FUNCTION_START As String = "COLUMN_WITH_FUNCTION_START"
    Public Const COLUMN_WITH_FUNCTION_COMPLETE As String = "COLUMN_WITH_FUNCTION_COMPLETE"
    Public Const COLUMN_FUNCTION_PARAMETER As String = "COLUMN_FUNCTION_PARAMETER"
    Public Const COLUMN_WITH_FUNCTION_END As String = "COLUMN_WITH_FUNCTION_END"

    Public Const FROM_STATEMENT As String = "FROM"
    Public Const WHERE_STATEMENT As String = "WHERE"
    Public Const AND_STATEMENT As String = "AND"

    Public Const GROUPBY_STATEMENT As String = "GROUP BY"
    Public Const GROUPBY_STATEMENT_WITH_FUNCTION_START As String = "GROUPBY_STATEMENT_WITH_FUNCTION_START"
    Public Const GROUPBY_STATEMENT_WITH_FUNCTION_COMPLETE As String = "GROUPBY_STATEMENT_WITH_FUNCTION_COMPLETE"

    Public Const ORDERBY_STATEMENT As String = "ORDER BY"
    Public Const HAVING_STATEMENT As String = "HAVING"

#End Region

    
    
#Region "Spaces"
    Public Const SELECT_SPACES As Integer = 7
    Public Const FROM_SPACES As Integer = 2
    Public Const WHERE_SPACES As Integer = 1
    Public Const AND_SPACES As Integer = 3
    Public Const HAVING_SPACES As Integer = 2

#End Region

End Class
