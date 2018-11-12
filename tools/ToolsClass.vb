Imports WebControlLib
Public Class ToolsClass
    Private Property _mpage As Page


    Public ReadOnly Property _LabellResultado() As Label
        Get
            Return _mpage.Master.FindControl("conteudo").FindControl("lblResultado")
        End Get
    End Property

    Public ReadOnly Property _TextBoxResultado() As TextBox
        Get
            Return _mpage.Master.FindControl("conteudo").FindControl("txtResultado")
        End Get
    End Property


    Public ReadOnly Property _TextBoxValor() As TextBox
        Get
            Return _mpage.Master.FindControl("conteudo").FindControl("txtValor")
        End Get
    End Property

    Private Property _TextToClip As String

    Public Sub New(mpage As Page)
        _mpage = mpage
        _TextBoxResultado.Attributes.Add("onfocus", "javascript:this.select();")
        _LabellResultado.BackColor = Drawing.Color.White
    End Sub

    Public Sub Encriptar()
        _TextBoxResultado.Text = xcfox.u_scrypt(_TextBoxValor.Text)
        _LabellResultado.Text = "Resultado encriptado"
        _LabellResultado.BackColor = Drawing.Color.Red
        SendToCLipBoard()
    End Sub

    Public Sub Desencriptar()
        _TextBoxResultado.Text = xcfox.u_sdecrypt(_TextBoxValor.Text)
        _LabellResultado.Text = "Resultado desencriptado"
        _LabellResultado.BackColor = Drawing.Color.LightGreen
        SendToCLipBoard()
    End Sub

    Private Sub SendToCLipBoard()
        '_TextToClip = _TextBoxResultado.Text
        Dim newThread As Threading.Thread = New Threading.Thread(New Threading.ThreadStart(AddressOf ThreadClip))
        newThread.SetApartmentState(Threading.ApartmentState.STA)
        newThread.Start()
    End Sub

    Private Sub ThreadClip()
        Windows.Forms.Clipboard.SetText(_TextBoxResultado.Text)
    End Sub

End Class
