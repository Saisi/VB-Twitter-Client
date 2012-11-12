Imports System.IO

Public Class About
    Public movefrm As Boolean = False
    Public postop As Double
    Public posleft As Double
    Public mousetop As Double
    Public mouseleft As Double


    Private Sub LogOutB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOutB.Click
        My.Settings.OAuthkey = ""
        My.Settings.OAuthSecret = ""
        Makmende.Close()
        Trends.Close()
        Introduction.Close()
    End Sub

    Private Sub CloseB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseB.Click
        CloseB.BorderStyle = BorderStyle.FixedSingle
        Me.Close()
    End Sub

    Private Sub About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    

    Private Sub About_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If movefrm Then
            postop = (Cursor.Position.Y)
            posleft = (Cursor.Position.X)
            Me.Top = mousetop + postop
            Me.Left = mouseleft + posleft
        End If

    End Sub

    Private Sub About_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        Me.Cursor = Cursors.Default
        movefrm = False
    End Sub

    Private Sub About_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown

        mousetop = Me.Top - (Cursor.Position.Y)
        mouseleft = Me.Left - (Cursor.Position.X)
        movefrm = True
    End Sub

  

   

    

    
   
    Private Sub BlockedUsersToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BlockedUsersToolStripMenuItem1.Click
        Me.Cursor = Cursors.WaitCursor

        Dim tw As New TwitterVB2.TwitterAPI


        Dim k As String = ""
        Try
            For Each element In tw.BlockedUsers
                k += vbNewLine & element.ScreenName


            Next
        Catch ex As Exception
        End Try

        Me.Cursor = Cursors.Default
        If Trim(k) = "" Then
            MsgBox("You haven't blocked any users", vbOKOnly, "")
        Else
            MsgBox("You have blocked the following users" & k, vbOKOnly, "Blocked Users")

        End If
    End Sub

    Private Sub ClearCacheToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearCacheToolStripMenuItem1.Click
        Dim CurrentDirectory2 As String = Makmende.PWD + "\dictionary.makmende"
        File.Delete(CurrentDirectory2)

        CurrentDirectory2 = Makmende.PWD + "\Avi0"
        Directory.Delete(CurrentDirectory2)

    End Sub

    Private Sub GenerateRandomFFToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GenerateRandomFFToolStripMenuItem1.Click
        Dim username As String = "#FF"
        Dim User = Makmende.ListA(2)
        Dim tw As New TwitterVB2.TwitterAPI
        Dim FollowersCount As Integer = tw.ShowUser(User).FollowersCount
        Randomize()
        Dim rand As New Random
        Randomize()
        Dim f As Integer
        Dim previous As String = ""

        Makmende.TypeOfConfirmation = "Might take a few moments"
        Confirmation.Show()

        While Len(username) < 140
            f = rand.Next(FollowersCount + 1)
            previous = username
            username += " @" & tw.ShowUser(tw.FriendsIDs.ElementAt(f)).ScreenName

        End While

        Makmende.TypeOfConfirmation = "Random #FF has been tweeted!"

        Try
            tw.Update(previous)
        Catch ex As Exception
            Makmende.TypeOfConfirmation = "An error has occurred"
        End Try
        Confirmation.Show()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Dim webClient1 As System.Net.WebClient = New System.Net.WebClient()
        Dim x As Integer = 0

        Try
            Dim result As String = webClient1.DownloadString("http://saisi.me/ver.html")
            x = CInt(result)
        Catch ex As Exception
        End Try

        webClient1.Dispose()


        If x > 1 Then
            If MsgBox("You are using an outdated veriosn of this program" & vbNewLine & "Would you like to update?", vbYesNo, "Update App") = vbYes Then
                System.Diagnostics.Process.Start("http://saisi.me/makmende")

            End If
        Else
            Makmende.TypeOfConfirmation = "Your Program is up to date"
            Confirmation.Show()

        End If

    End Sub
End Class