Public Class Trends
    Public movefrm As Boolean = False
    Public postop As Double
    Public posleft As Double
    Public mousetop As Double
    Public mouseleft As Double


    Private Sub Trends_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Top = Makmende.Top
        Me.Left = Makmende.Left + Makmende.Width + 2

        If Makmende.trendDic.Count > 10 Then
            ListBox4.Items.Clear()
        End If

        Dim x As Integer = 0
        For Each element In Makmende.trendDic
            If x < 10 Then
                ListBox2.Items.Add(element.Value)
                x += 1

            ElseIf x > 9 And x < 20 Then
                ListBox3.Items.Add(element.Value)
                x += 1

            End If

        Next

        For Each element In Makmende.NairobiTrends
            If element.Key = 0 Then
            Else
                ListBox4.Items.Add(element.Value)
            End If
        Next
    End Sub

    Private Sub PictureBox10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox10.Click
        If Makmende.PostTrends.IsBusy Then
            Me.Hide()
        Else
            Me.Close()
        End If

        PictureBox10.BorderStyle = BorderStyle.None
    End Sub

    Private Sub RefreshB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshB.Click
        If Not Makmende.PostTrends.IsBusy Then
            Makmende.CallRefreshNAtive = True
            Makmende.PostTrends.RunWorkerAsync()
        Else
            Makmende.TypeOfConfirmation = "Refresh already in progress"
            Confirmation.Show()
        End If
    End Sub

    Private Sub Trends_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If movefrm Then
            postop = (Cursor.Position.Y)
            posleft = (Cursor.Position.X)
            Me.Top = mousetop + postop
            Me.Left = mouseleft + posleft
        End If
    End Sub

    Private Sub Trends_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        mousetop = Me.Top - (Cursor.Position.Y)
        mouseleft = Me.Left - (Cursor.Position.X)
        movefrm = True
    End Sub

    Private Sub Trends_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        Me.Cursor = Cursors.Default
        movefrm = False
    End Sub
End Class