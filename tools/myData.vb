Imports WebControlLib
Imports System.Data.SqlClient

Public Class myData
    Public Enum enumDsn
        DSN_Testes = 1
        DSN_Real = 2
        DSN_Testes_RH = 3
        DSN_Real_RH = 4
    End Enum

    Private Shared ReadOnly Property propConnectinonString(qualDsn As myData.enumDsn) As String
        Get
            Return CConfig.Valor(qualDsn.ToString())
        End Get
    End Property

    Private Shared ReadOnly Property propSqlConnection(qualDsn As myData.enumDsn) As SqlConnection
        Get
            Return New SqlConnection(propConnectinonString(qualDsn))
        End Get
    End Property


    Public Shared Function dtosql(mdata As Date) As String
        Return $"{Year(mdata).Padl(4)}-{Month(mdata).Padl(2)}-{Day(mdata).Padl(2)}"
    End Function


    Public Shared Function getDataTable(msel As String, Optional qualDsn As enumDsn = enumDsn.DSN_Testes, Optional sqlParameters() As SqlParameter = Nothing, Optional tableName As String = "temp") As DataTable
        Dim dt As New DataTable(tableName)
        Using cn As SqlConnection = propSqlConnection(qualDsn)
            cn.Open()
            Using cmd As New SqlCommand(msel, cn)
                cmd.CommandText = msel
                cmd.CommandType = CommandType.Text

                If sqlParameters.HaveItens Then
                    For Each p As SqlParameter In sqlParameters
                        cmd.Parameters.Add(p)
                    Next
                End If
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function


    Public Shared Function getTypesCollection(tabela As DataTable) As IEnumerable(Of Object)
        Dim resultado = {New With {Key .nome = "", .stipo = "", .sdefault = ""}}.ToList()
        For Each col As DataColumn In tabela.Columns
            Dim mtipo, mdefault As New String("")
            Select Case col.DataType.Name.ToString.ToLower.Trim
                Case "string", "char"
                    mtipo = "C"
                    mdefault = "''"
                Case "integer", "decimal", "double", "int16""int32", "int64", "single"
                    mtipo = "N"
                    mdefault = "0"
                Case "boolean"
                    mtipo = "L"
                    mdefault = "0"
                Case "datetime", "date"
                    mtipo = "D"
                    mdefault = "'1900-01-01'"
                Case Else
                    Continue For
            End Select

            resultado.Add(New With {Key .nome = col.ColumnName, .stipo = mtipo, .sdefault = mdefault})
        Next
        Return resultado
    End Function

End Class
