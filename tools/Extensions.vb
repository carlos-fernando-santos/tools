Imports System.Runtime.CompilerServices
Imports WebControlLib
Module Extensions
    <Extension>
    Function IsNullOrEmpty(str As String) As Boolean
        Return (If(str, String.Empty) = String.Empty)
    End Function
    <Extension>
    Function IsNotNullOrEmpty(str As String) As Boolean
        Return Not (If(str, String.Empty) = String.Empty)
    End Function

    <Extension>
    Public Function ToSql(xcol As DataColumn, value As Object) As String
        Dim returnString As String = String.Empty
        Select Case xcol.DataType
            Case System.Type.GetType("System.String"), System.Type.GetType("System.Guid")
                returnString = $"'{value.ToString.Replace("'", "''")}'"
            Case System.Type.GetType("System.Int32"), System.Type.GetType("System.Int64"), System.Type.GetType("System.Integer")
                returnString = value.ToString()
            Case System.Type.GetType("System.Decimal"), System.Type.GetType("System.Double")
                returnString = $"{CDec(value).ToString(New Globalization.CultureInfo("en-US"))}"
            Case System.Type.GetType("System.DateTime")
                returnString = $"'{CDate(value).cSql}'"
            Case System.Type.GetType("System.Boolean")
                returnString = $"{CBool(value).cSql}"
        End Select
        Return returnString
    End Function

    <Extension>
    Public Function SqlUpdate(row As DataRow, tableName As String, Optional whereTo As String = "1=0") As String
        Dim msel As String = String.Empty
        Dim mcol As String = String.Empty
        tableName = tableName.ToLower.Trim
        For Each c As DataColumn In row.Table.Columns
            mcol = c.ColumnName.ToLower.Trim
            If mcol <> $"{tableName}stamp" AndAlso mcol <> $"{tableName}id" Then
                msel += $"{If(msel.IsNullOrEmpty, "", ",")}{mcol}={c.ToSql(row(mcol))} {vbNewLine}"
            End If
        Next
        msel = $"Update [{tableName.Trim}] Set {vbNewLine}{msel} {vbNewLine}Where {whereTo} ;"
        Return msel
    End Function

    <Extension>
    Public Function cSqlInsert(row As DataRow, Optional tableName As String = "", Optional includeId As Boolean = False) As String
        Dim msel, mcam, mval As New String("")
        If tableName.IsNullOrEmpty Then
            tableName = row.Table.TableName
            If tableName.IsNullOrEmpty Then
                Return ""
            End If
        End If
        tableName = tableName.ToLower.Trim
        Dim mcol As String = ""
        For Each c As DataColumn In row.Table.Columns
            mcol = c.ColumnName.ToLower.Trim
            If mcol = tableName + "id" And Not includeId Then
                Continue For
            End If
            mcam += If(mcam.IsNullOrEmpty, "", ",") + c.ColumnName
            mval += If(mval.IsNullOrEmpty, "", ",")
            mval += c.ToSql(row(mcol))
            'Select Case c.DataType
            '    Case System.Type.GetType("System.String"), System.Type.GetType("System.Guid")
            '        mval += $"'{row(mcol).ToString.Replace("'", "''")}'"
            '    Case System.Type.GetType("System.Int32"), System.Type.GetType("System.Int64"), System.Type.GetType("System.Integer")
            '        mval += row(mcol).ToString
            '    Case System.Type.GetType("System.Decimal"), System.Type.GetType("System.Double")
            '        mval += CDec(row(mcol)).ToString(New Globalization.CultureInfo("en-US"))
            '    Case System.Type.GetType("System.DateTime")
            '        mval += $"'{CDate(row(mcol)).cSql}'"
            '    Case System.Type.GetType("System.Boolean")
            '        mval += CBool(row(mcol)).cSql
            'End Select
        Next

        msel = $"Insert Into dbo.{tableName} {vbNewLine} ({mcam}) {vbNewLine} Values {vbNewLine} ({mval})"
        Return msel
    End Function




    <Extension>
    Public Function Padl(intNumber As Integer, mSize As Integer, Optional mChar As String = "0") As String
        Return CStr(intNumber).Trim.PadLeft(mSize, mChar)
    End Function

    <Extension>
    Public Function cSql(mData As Date) As String
        Return $"{Year(mData).Padl(4)}-{Month(mData).Padl(2)}-{Day(mData).Padl(2)}"
    End Function

    <Extension>
    Public Function cSql(mBol As Boolean) As String
        Return If(mBol, "1", "0")
    End Function



End Module
