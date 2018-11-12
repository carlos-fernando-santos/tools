Imports WebControlLib

Public Class main
    Inherits System.Web.UI.Page

    Private Property _Tools As ToolsClass
    Private pTable As DataTable
    Private pColumns As IEnumerable(Of Object)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _Tools = New ToolsClass(Me.Page)
    End Sub

    Protected Sub btEncrypt_Click(sender As Object, e As EventArgs) Handles btEncrypt.Click
        _Tools.Encriptar()
    End Sub

    Protected Sub btDecrfypt_Click(sender As Object, e As EventArgs) Handles btDecrfypt.Click
        _Tools.Desencriptar()
    End Sub

    Protected Sub btnExecQuery_Click(sender As Object, e As EventArgs) Handles xbuttonexec.Click

        Dim msel As String = ""
        If xqueryinput.Text.IsNullOrEmpty Then
            ShowErro("Tem que escrever uma query no campo 'Query Input'.")
            xqueryinput.Focus()
            Exit Sub
        End If

        pTable = myData.getDataTable(xqueryinput.Text, xdsn.SelectedItem.Value, tableName:=xtable.Text)

        If pTable.HaveRows Then
            With xgrid
                .DataSource = pTable
                .DataBind()
            End With


            For Each r As DataRow In pTable.Rows
                If xtype.SelectedItem.Value = "2" Then
                    msel += r.SqlUpdate(r.Table.TableName)
                Else
                    msel += r.cSqlInsert + vbNewLine
                End If

            Next
            xqueryoutput.Text = msel
            If msel.IsNotNullOrEmpty Then
                xTools.CopyText(msel)
                xqueryoutput.Focus()
                xqueryoutput.Attributes.Add("onfocusin", " select();")
            End If
        Else
            ShowErro("Não foram encontrados registos")
        End If


    End Sub

    Private Sub ShowErro(msg As String)
        XcUtil.Alerta(Me.Page, msg)
        xqueryoutput.Text = msg

    End Sub

    Protected Sub xtable_TextChanged(sender As Object, e As EventArgs) Handles xtable.TextChanged
        xqueryinput.Text = "select * from [" + xtable.Text.Trim + "] with (nolock) where "
        xqueryinput.Focus()
    End Sub
End Class