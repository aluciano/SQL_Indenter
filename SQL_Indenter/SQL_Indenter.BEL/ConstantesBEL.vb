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
    Public Const TABELA As String = "TABELA"

    Public Const INNER_STATEMENT As String = "INNER"
    Public Const INNERJOIN_STATEMENT As String = "INNER JOIN"
    Public Const LEFT_STATEMENT As String = "LEFT"
    Public Const LEFTJOIN_STATEMENT As String = "LEFT JOIN"
    Public Const LEFTOUTER_STATEMENT As String = "LEFT OUTER"
    Public Const LEFTOUTERJOIN_STATEMENT As String = "LEFT OUTER JOIN"
    Public Const RIGHT_STATEMENT As String = "RIGHT"
    Public Const RIGHTJOIN_STATEMENT As String = "RIGHT JOIN"
    Public Const RIGHTOUTER_STATEMENT As String = "RIGHT OUTER"
    Public Const RIGHTOUTERJOIN_STATEMENT As String = "RIGHT OUTER JOIN"
    Public Const FULL_STATEMENT As String = "FULL"
    Public Const FULLJOIN_STATEMENT As String = "FULL JOIN"
    Public Const FULLOUTER_STATEMENT As String = "FULL OUTER"
    Public Const FULLOUTERJOIN_STATEMENT As String = "FULL OUTER JOIN"
    Public Const CROSS_STATEMENT As String = "CROSS"
    Public Const CROSSJOIN_STATEMENT As String = "CROSS JOIN"
    Public Const JOIN_STATEMENT As String = "JOIN"
    Public Const ON_STATEMENT As String = "ON "
    Public Const ANDJOIN_STATEMENT As String = "AND"
    Public Const ORJOIN_STATEMENT As String = "OR"
    
    Public Const WHERE_STATEMENT As String = "WHERE"
    Public Const ANDWHERE_STATEMENT As String = "AND"
    Public Const ORWHERE_STATEMENT As String = "OR"

    Public Const GROUPBY_STATEMENT As String = "GROUP BY"
    Public Const GROUPBY_STATEMENT_WITH_FUNCTION_START As String = "GROUPBY_STATEMENT_WITH_FUNCTION_START"
    Public Const GROUPBY_STATEMENT_WITH_FUNCTION_COMPLETE As String = "GROUPBY_STATEMENT_WITH_FUNCTION_COMPLETE"

    Public Const ORDERBY_STATEMENT As String = "ORDER BY"
    Public Const ORDERBY_STATEMENT_WITH_FUNCTION_START As String = "ORDERBY_STATEMENT_WITH_FUNCTION_START"
    Public Const ORDERBY_STATEMENT_WITH_FUNCTION_COMPLETE As String = "ORDERBY_STATEMENT_WITH_FUNCTION_COMPLETE"

    Public Const HAVING_STATEMENT As String = "HAVING"

#End Region

    
    
#Region "Spaces"
    Public Const SELECT_SPACES As Integer = 7
    Public Const TABELA_SPACES As Integer = 7
    Public Const FROM_SPACES As Integer = 2

    Public Const INNER_SPACES As Integer = 1
    Public Const JOIN_SPACES As Integer = 2
    Public Const ON_SPACES As Integer = 4
    Public Const ANDJOIN_SPACES As Integer = 3
    
    Public Const WHERE_SPACES As Integer = 1
    Public Const ANDWHERE_SPACES As Integer = 3
    Public Const ORWHERE_SPACES As Integer = 4
    Public Const HAVING_SPACES As Integer = 2

#End Region

End Class
