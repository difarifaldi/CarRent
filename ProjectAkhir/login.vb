Imports System.Data.OleDb
Imports System.IO
Public Class login
    Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\users.accdb")
    Dim lihatdata As OleDbDataReader

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Me.Hide()
        register.ShowDialog()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            txt_password.UseSystemPasswordChar = True
        Else
            txt_password.UseSystemPasswordChar = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            connection.Open()
            Dim login As New OleDbCommand("select*from users where username=@username AND password=@password", connection)
            login.Parameters.Clear()
            login.Parameters.AddWithValue("@username", txt_username.Text)
            login.Parameters.AddWithValue("@password", txt_password.Text)
            Dim username As String = ""
            Dim password As String = ""
            lihatdata = login.ExecuteReader
            If (lihatdata.Read() = True) Then
                username = lihatdata("username")
                password = lihatdata("password")
                MsgBox("Login Success", vbInformation)
                Me.Hide()
                main.Show()
                main.Label7.Text = lihatdata.Item("nama")
                main.txt_nama.Text = lihatdata.Item("nama")
                main.txt_noHP.Text = lihatdata.Item("noHP")



            Else
                MsgBox("Login Failed", vbCritical)
            End If
            txt_username.Clear()
            txt_password.Clear()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        connection.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        GroupBox1.Visible = False
        GroupBox2.Visible = True
        Button3.Visible = True
        Button2.Visible = False






    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        GroupBox2.Visible = False
        GroupBox1.Visible = True
        Button2.Visible = True
        Button3.Visible = False

    End Sub

    Private Sub loginadmin_Click(sender As Object, e As EventArgs) Handles loginadmin.Click
        If MaskedTextBox1.Text = "111-22-3333" _
        And MaskedTextBox2.Text = "12345" Then
            MsgBox("Hello Admin !")
            Me.Hide()
            admin.Show()
        Else
            MsgBox("I don't recognize this number")
        End If
    End Sub
End Class
