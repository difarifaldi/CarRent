Public Class rules
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim text1 As Integer = 0
        Dim text2 As Integer
        Dim jumlahkata As Integer = 0
        text2 = txtNote.Text.LastIndexOf(txt_cari.Text)
        txtNote.SelectAll()
        txtNote.SelectionBackColor = Color.White
        txtNote.Focus()

        If text1 < text2 Then
            While text1 < text2 'loop ketika kata ditemukan
                txtNote.Find(txt_cari.Text, text1, txtNote.TextLength, RichTextBoxFinds.MatchCase)
                txtNote.SelectionBackColor = Color.Cyan 'warna belakang berubah warna
                text1 = txtNote.Text.IndexOf(txt_cari.Text, text1) + 1
                jumlahkata = jumlahkata + 1 ' jumlah kata


            End While
            MessageBox.Show(String.Format("Terdapat {0} kata""{1}"".", jumlahkata, txt_cari.Text))
        Else
            While text1 = 0 'ketika kata tidak ditemukan
                MsgBox("kata tidak ditemukan", vbInformation, "Peringatan!!")
                txt_cari.Clear()
                Exit While
            End While
        End If
    End Sub

    Private Sub rules_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtNote.Text = "Syarat dan Ketentuan

Sewa Mobil Harian

Waktu penyewaan mobil mulai dari pukul 05.00 s.d 09.00
Lama sewa harian yaitu 12 Jam
Jika durasi sewa melebihi jangka waktu yang telah ditentukan maka akan dikenakan biaya overtime sebesar 10% perjam
Tambahan uang inap supir jika menginap sebesar Rp 150,000 permalam
Unit tersedia di seluruh Indonesia untuk harga berbeda harap konfirmasi lebih lanjut
Sewa Mobil Lepas Kunci

Lepas kunci hanya untuk penyewaan atas nama perusahaan
Untuk lepas kunci minimal booking H-7 sebelum pemakaian
Dokumen yang diperlukan untuk sewa lepas kunci antara lain:
Akta Perusahaan
SK KumHam
SIUP
TDP
Domisili
NPWP
FC ID Card & KTP PIC
Surat Permintaan Sewa Kendaraan (PO)
Biaya antar jemput mobil jika lepas kunci sebesar biaya Gojek + jasa Rp 75,000 sekali jala
Ketentuan Pembatalan

Pembatalan dikenakan biaya sebesar 50% sebelum tanggal sewa
Pembatalan dikenakan biaya sebesar 100% saat tanggal sewa"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        login.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) 
        main.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) 
        critic.Show()
        Me.Close()
    End Sub
End Class