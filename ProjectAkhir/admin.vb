Imports System.Data.OleDb
Imports System.IO
Public Class admin
    Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\users.accdb")
    Dim lihatdata As OleDbDataReader
    Dim i As Integer
    Sub loadingDataGridView()
        Try

            DataGridView1.Rows.Clear()
            connection.Open()
            Dim datasiswa As New OleDbCommand("Select *from payment", connection)
            lihatdata = datasiswa.ExecuteReader
            While lihatdata.Read
                DataGridView1.Rows.Add(lihatdata.Item("ID"), lihatdata.Item("nama"), lihatdata.Item("noHP"), lihatdata.Item("waktu"), lihatdata.Item("lama") + " day", lihatdata.Item("total"), lihatdata.Item("jenis"))

            End While

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        connection.Close()
    End Sub
    Sub cmb()
        ComboBox1.Items.Add("SUV ,300000")
        ComboBox1.Items.Add("Sport ,700000")
        ComboBox1.Items.Add("Of Road ,500000")
        ComboBox1.Items.Add("Coupe ,200000")
        ComboBox1.SelectedIndex = 0
        total.Enabled = False
    End Sub

    Private Sub admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadingDataGridView()
        cmb()
        txt_ID.Text = "[AUTO]"
        txt_ID.Enabled = False
    End Sub
    Sub save()
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
        loadingDataGridView()
        clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        save()
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

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        calculate()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim txt As String = DataGridView1.CurrentRow.Cells(4).Value.ToString
        Dim lama() As String = txt.Split(" ")
        txt_ID.Text = DataGridView1.CurrentRow.Cells(0).Value
        txt_nama.Text = DataGridView1.CurrentRow.Cells(1).Value
        txt_noHP.Text = DataGridView1.CurrentRow.Cells(2).Value
        DateTimePicker1.Value = DataGridView1.CurrentRow.Cells(3).Value
        NumericUpDown1.Value = lama(0)
        total.Text = DataGridView1.CurrentRow.Cells(5).Value

    End Sub
    Sub update()
        Dim txt As String = ComboBox1.SelectedItem.ToString
        Dim htng() As String = txt.Split(",")
        Try
            connection.Open()
            If MsgBox("Apakah kamu yakin ingin mengubah data?", vbQuestion + vbYesNo) = vbYes Then
                Dim upd As New OleDbCommand("UPDATE payment SET `nama`=@nama,`noHP`=@noHP,`waktu`=@waktu,`lama`=@lama,`total`=@total,`jenis`=@jenis where ID=@ID", connection)
                upd.Parameters.Clear()
                upd.Parameters.AddWithValue("@nama", txt_nama.Text)
                upd.Parameters.AddWithValue("@noHP", txt_noHP.Text)
                upd.Parameters.AddWithValue("@waktu", CDate(DateTimePicker1.Value))
                upd.Parameters.AddWithValue("@lama", NumericUpDown1.Value)
                upd.Parameters.AddWithValue("@total", total.Text)
                upd.Parameters.AddWithValue("@jenis", htng(0))
                upd.Parameters.AddWithValue("@ID", txt_ID.Text)
                i = upd.ExecuteNonQuery
                If i > 0 Then
                    MsgBox("Data berhasil diubah", vbInformation)
                End If
            Else
                MsgBox("gagal mengubah data", vbCritical)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        connection.Close()
        loadingDataGridView()
        clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        update()

    End Sub
    Sub clear()
        txt_ID.Text = "[AUTO]"
        txt_nama.Clear()
        txt_noHP.Clear()
        total.Clear()
        NumericUpDown1.Value = 0
    End Sub
    Sub delete()
        Try
            connection.Open()
            If MsgBox("Apakah kamu yakin ingin menghapus data?", vbQuestion + vbYesNo) = vbYes Then
                Dim dlt As New OleDbCommand("Delete *from payment  where ID=@ID", connection)
                dlt.Parameters.Clear()

                dlt.Parameters.AddWithValue("@ID", txt_ID.Text)
                i = dlt.ExecuteNonQuery
                If i > 0 Then
                    MsgBox("Data berhasil dihapus", vbInformation)
                End If
            Else
                MsgBox("gagal menghapus data", vbCritical)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        connection.Close()
        loadingDataGridView()
        clear()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        delete()
    End Sub
    Sub search()
        Try

            DataGridView1.Rows.Clear()
            connection.Open()
            Dim srch As New OleDbCommand("Select *from payment where `nama` like '%" & txt_search.Text & "%' or `jenis` like '%" & txt_search.Text & "%' ", connection)
            lihatdata = srch.ExecuteReader
            While lihatdata.Read
                DataGridView1.Rows.Add(lihatdata.Item("ID"), lihatdata.Item("nama"), lihatdata.Item("noHP"), lihatdata.Item("waktu"), lihatdata.Item("lama"), lihatdata.Item("total"), lihatdata.Item("jenis"))

            End While

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        connection.Close()
    End Sub

    Private Sub txt_search_TextChanged(sender As Object, e As EventArgs) Handles txt_search.TextChanged
        search()
    End Sub
End Class