Public Class Confirmation
    Public x As Integer = 0

    Private Sub Confirmation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Makmende.TypeOfConfirmation = "Enter Username to Find" Then
            TextBox1.Text = "Enter Username & press enter"
            TextBox1.TextAlign = HorizontalAlignment.Center
            Me.Select()
        Else
            Timer1.Interval = 1000
            Timer1.Enabled = True

            TextBox1.Text = Makmende.TypeOfConfirmation
            TextBox1.TextAlign = HorizontalAlignment.Center
            Me.Select()

        End If
        
        Me.BringToFront()
       
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        x += 1
        If x = 2 Then
            Me.Close()
        End If


        
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not Timer1.Enabled Then
            If Trim(TextBox1.Text) = "" Then
                Me.Close()
                Exit Sub
            End If

            If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
                Makmende.LoadUsername = Trim(TextBox1.Text)
                Me.Hide()
                Makmende.showprofile()
                Me.Close()
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class