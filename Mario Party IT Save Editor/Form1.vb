Imports PackageIO
Public Class Form1
    Dim filepath As String
    Dim mmp As String
    Dim mgw As String
    Dim spw As String
    Dim fdialog As New Form3
    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
        Dim open As New OpenFileDialog
        open.Title = "Open save mf1"
        fdialog.Label1.Text = "                   Load save file mf1" & vbNewLine & "        Backup your save before use this editor"
        fdialog.ShowDialog()
        open.ShowDialog()
        filepath = open.FileName
        Readfile()
    End Sub
    Private Sub Readfile()
        Try
            Dim Reader As New PackageIO.Reader(filepath, PackageIO.Endian.Little)
            Reader.Position = &H88
            mmp = Reader.Position
            NumericUpDown1.Value = Reader.ReadInt32
            Reader.Position = &H1B8
            mgw = Reader.Position
            NumericUpDown2.Value = Reader.ReadInt32
            Reader.Position = &H1BC
            spw = Reader.Position
            NumericUpDown3.Value = Reader.ReadInt32
        Catch ex As Exception
            fdialog.Label1.Text = "Save Game is Corrupted or not a Mario Party IT Save"
            fdialog.ShowDialog()
        End Try
    End Sub

    Private Sub PictureBox2_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox2.Click
        WriteFile()
    End Sub
    Private Sub WriteFile()
        Try
            Dim Writer As New PackageIO.Writer(filepath, PackageIO.Endian.Little)
            Writer.Position = mmp
            Writer.WriteInt32(NumericUpDown1.Value)
            Writer.Position = mgw
            Writer.WriteInt32(NumericUpDown2.Value)
            Writer.Position = spw
            Writer.WriteInt32(NumericUpDown3.Value)
            fdialog.Label1.Text = "                          File Save"
            fdialog.ShowDialog()
        Catch ex As Exception
            fdialog.Label1.Text = "                   An error has occured"
            fdialog.ShowDialog()
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Dim Writer As New PackageIO.Writer(filepath, PackageIO.Endian.Little)
            For i As Integer = 0 To 36
                Writer.Position = &H8D + i
                Writer.WriteInt8(1)
            Next
            fdialog.Label1.Text = "All Collectibles, Boards, Characters are now unlocked" & vbNewLine & "In the game select Gallery for unlocked all collectibles"
            fdialog.ShowDialog()
        End If
    End Sub

    Private Sub PictureBox6_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox6.Click
        Form2.Show()
    End Sub
End Class
