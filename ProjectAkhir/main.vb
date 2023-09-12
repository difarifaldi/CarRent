Imports System.Data.OleDb
Imports System.IO
Public Class main
    Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\users.accdb")

    Dim da As OleDbDataAdapter
    Dim dt As DataTable
    Sub cmb()
        ComboBox1.Items.Add("SUV ,300000")
        ComboBox1.Items.Add("Sport ,700000")
        ComboBox1.Items.Add("Of Road ,500000")
        ComboBox1.Items.Add("Coupe ,200000")
        ComboBox1.SelectedIndex = 0
        total.Enabled = False
    End Sub

    Private Sub main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmb()

    End Sub

    Sub calculate()
        Dim a As Integer
        a = NumericUpDown1.Value
        Dim txt As String = ComboBox1.SelectedItem.ToString
        Dim htng() As String = txt.Split(",")
        Dim calculate As Integer = CInt(htng(1)) * CInt(NumericUpDown1.Value)


        If a = 0 Then
            MsgBox("Please enter period of time", vbCritical)
            Return
        Else
            txt = calculate
            total.Text = "Rp. " + txt
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        calculate()
    End Sub

    Sub pay()
        Dim txt As String = ComboBox1.SelectedItem.ToString
        Dim htng() As String = txt.Split(",")
        Dim a As Integer
        a = NumericUpDown1.Value
        Try
            connection.Open()

            If MsgBox("Apakah kamu yakin ingin melakukan transaksi?", vbQuestion + vbYesNo) = vbYes And a > 0 Then
                Dim regis As New OleDbCommand("Insert Into payment(`nama`,`noHP`,`waktu`,`lama`,`total`,`jenis`)values(@nama,@noHP,@waktu,@lama,@total,@jenis)", connection)
                Dim i As New Integer
                regis.Parameters.Clear()
                regis.Parameters.AddWithValue("@nama", txt_nama.Text)
                regis.Parameters.AddWithValue("@noHP", txt_noHP.Text)
                regis.Parameters.AddWithValue("@waktu", CDate(DateTimePicker1.Value))
                regis.Parameters.AddWithValue("@lama", NumericUpDown1.Value)
                regis.Parameters.AddWithValue("@total", total.Text)
                regis.Parameters.AddWithValue("@jenis", htng(0))




                i = regis.ExecuteNonQuery
                If i > 0 Then
                    MsgBox("Pembayaran berhasil", vbInformation)
                End If
            Else
                MsgBox("Pembayaran gagal", vbCritical)
            End If
            total.Clear()
            NumericUpDown1.Value = 0



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        connection.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        pay()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        login.Show()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        txt_waktu.Text = Date.Now.ToString(" hh:mm:ss tt")
        txt_hari.Text = Date.Now.ToString(" dddd ")
        tanggal.Text = Date.Now.Date
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
        rules.Show()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If PictureBox1.Left < Panel2.Width Then
            PictureBox1.Left = PictureBox1.Left + 10
        Else
            PictureBox1.Left = 0
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim URL As String = "https://www.ferrari.com/"
        Dim NewProcess As Diagnostics.ProcessStartInfo = New Diagnostics.ProcessStartInfo(URL)
        NewProcess.UseShellExecute = True
        System.Diagnostics.Process.Start(NewProcess)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim picture() As Byte
        PictureBox2.Image = Nothing
        Try
            connection.Open()
            Dim cmd As New OleDbCommand("Select*from users where nama=@nama", connection)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@nama", txt_nama.Text)
            da = New OleDbDataAdapter
            dt = New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            picture = dt.Rows(0).Item("pic")
            Dim mstream As New System.IO.MemoryStream(picture)
            PictureBox2.Image = Image.FromStream(mstream)
        Catch ex As Exception

        End Try
    End Sub
End Class