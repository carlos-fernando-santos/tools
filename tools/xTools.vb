Public Class xTools
    Private Shared Property _text As String
    <STAThread>
    Public Shared Sub CopyText(mtext As String)
        _text = mtext
        Dim newThread As Threading.Thread = New Threading.Thread(New Threading.ThreadStart(AddressOf ThreadClip))
        newThread.SetApartmentState(Threading.ApartmentState.STA)
        newThread.Start()
    End Sub
    <STAThread>
    Private Shared Sub ThreadClip()
        Windows.Forms.Clipboard.SetText(_text)
    End Sub
End Class
