Imports System.Data.OleDb
Imports System.IO
Public Class register
    Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\users.accdb")

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            txt_password.UseSystemPasswordChar = True
        Else
            txt_password.UseSystemPasswordChar = False
        End If
    End Sub

    Private Sub btn_browse_Click(sender As Object, e As EventArgs) Handles btn_browse.Click
        Dim pop As OpenFileDialog = New OpenFileDialog
        If pop.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            PictureBox1.Image = Image.FromFile(pop.FileName)
        End If
    End Sub
    Sub regis()
        Try
            connection.Open()

            If MsgBox("Apakah kamu yakin data yang kamu masukan benar?", vbQuestion + vbYesNo) = vbYes Then
                Dim regis As New OleDbCommand("Insert Into users(`username`,`password`,`nama`,`noHP`,`pic`)values(@username,@password,@nama,@noHP,@pic)", connection)
                Dim i As New Integer
                regis.Parameters.Clear()
                regis.Parameters.AddWithValue("@username", txt_username.Text)
                regis.Parameters.AddWithValue("@password", txt_password.Text)
                regis.Parameters.AddWithValue("@nama", txt_nama.Text)
                regis.Parameters.AddWithValue("@noHP", txt_noHP.Text)

                Dim filesize As New UInt32
                Dim mstream As New System.IO.MemoryStream
                PictureBox1.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
                Dim picture() As Byte = mstream.GetBuffer
                filesize = mstream.Length
                mstream.Close()
                regis.Parameters.AddWithValue("@pic", picture)

                i = regis.ExecuteNonQuery
                If i > 0 Then
                    MsgBox("Data berhasil disimpan", vbInformation)
                End If
            Else
                MsgBox("gagal menyimpan data", vbCritical)
            End If
            txt_username.Clear()
            txt_password.Clear()
            txt_nama.Clear()
            txt_noHP.Clear()
            PictureBox1.Image = Nothing
            login.Show()
            Me.Hide()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        connection.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        regis()
    End Sub

    Private Sub register_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class