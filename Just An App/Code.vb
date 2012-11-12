Imports TwitterVB2

Public Class Code

    Public movefrm As Boolean = False
    Public postop As Double
    Public posleft As Double
    Public mousetop As Double
    Public mouseleft As Double

    Private Sub IgnoreB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IgnoreB.Click
        My.Settings.OAuthkey = "INSERT OUATH KEY"
        My.Settings.OAuthSecret = "INSERT OUATH SECRET"




        MsgBox("Please wait as the App redirects you to Twitter on your browser", vbOKOnly, "Redirecting")

        AuthoriseMe()


    End Sub

    Private Sub TweetB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TweetB.Click

        If Not TextBox1.Visible Then
            TextBox1.Visible = True
            IgnoreB.Visible = False
            TweetB.Text = "Proceed"
            Label1.Text = "Please enter it"
        Else
            Dim Code As String = Trim(TextBox1.Text)
            If Code = "101" Then
                My.Settings.OAuthkey = "INSERT OUATH KEY"
                My.Settings.OAuthSecret = "INSERT OUATH SECRET"

            ElseIf Code = "102" Then
                My.Settings.OAuthkey = "INSERT OUATH KEY"
                My.Settings.OAuthSecret = "INSERT OUATH SECRET"
            ElseIf Code = "103" Then
                My.Settings.OAuthkey = "INSERT OUATH KEY"
                My.Settings.OAuthSecret = "INSERT OUATH SECRET"
            ElseIf Code = "105" Then
                My.Settings.OAuthkey = "INSERT OUATH KEY"
                My.Settings.OAuthSecret = "INSERT OUATH SECRET"
            ElseIf Code = "106" Then
                My.Settings.OAuthkey = "INSERT OUATH KEY"
                My.Settings.OAuthSecret = "INSERT OUATH SECRET"
            Else
                My.Settings.OAuthkey = "INSERT OUATH KEY"
                My.Settings.OAuthSecret = "INSERT OUATH SECRET"
            End If


            Makmende.TypeOfConfirmation = "Wait as the App redirects you to Twitter"
            AuthoriseMe()
        End If
    End Sub


    Sub AuthoriseMe()
        Try

            Dim tw As New TwitterVB2.TwitterAPI
            Dim Url As String = tw.GetAuthorizationLink("INSERT KEY", My.Settings.OAuthSecret)

            System.Diagnostics.Process.Start(Url)








            Dim PIN As String = InputBox("Please wait as twitter.com loads. After which" & vbNewLine & "Enter PIN from the Twitter webpage", "Input String")


            If PIN = "" Then


                MsgBox("Blank pin not allowed", vbOKOnly, "")


                Exit Sub
            End If

            Dim IsValid As Boolean = tw.ValidatePIN(PIN)

            If IsValid Then

                My.Settings.ConsumerToken = tw.OAuth_Token()
                My.Settings.ConsumerSecret = tw.OAuth_TokenSecret()
                Dim MyAccount As TwitterUser = tw.AccountInformation
                Dim MyAccountDateCreatedAt = MyAccount.CreatedAt


                'Makmende.ListA(2) = MyAccount.ScreenName
                MsgBox("Please wait (^^,). Loading user data ", vbOKOnly, "")
                
                LoadMyShit()



            Else
                MsgBox("Try again mate. Wrong Code ", vbOKOnly, "")
                RetryB.Visible = True
            End If


        Catch ex As TwitterAPIException
            If IsOnline() = False Then
                MsgBox("Not connceted to Internet", vbOKOnly, "")
                
            Else

                MsgBox("Twitter is down ", vbOKOnly, "")


            End If


        End Try
    End Sub

    Sub LoadMyShit()
        Me.Close()
        Makmende.Show()

    End Sub




    Private Sub RetryB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RetryB.Click
        RetryB.Visible = False
        AuthoriseMe()

    End Sub


    Public Function IsOnline() As Boolean

        ' Returns True if connection is available
        ' Replace www.yoursite.com with a site that
        ' is guaranteed to be online - perhaps your
        ' corporate site, or microsoft.com
        Dim objUrl As New System.Uri("http://www.google.com/")
        ' Setup WebRequest
        Dim objWebReq As System.Net.WebRequest
        objWebReq = System.Net.WebRequest.Create(objUrl)
        Dim objResp As System.Net.WebResponse
        Try
            ' Attempt to get response and return True
            objResp = objWebReq.GetResponse
            objResp.Close()
            objWebReq = Nothing
            Return True
        Catch ex As Exception
            ' Error, exit and return False
            objWebReq = Nothing
            Return False
        End Try
    End Function


    Private Sub Code_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        Me.Cursor = Cursors.Default
        movefrm = False
    End Sub

    Private Sub Code_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If movefrm Then
            postop = (Cursor.Position.Y)
            posleft = (Cursor.Position.X)
            Me.Top = mousetop + postop
            Me.Left = mouseleft + posleft
        End If
    End Sub

    Private Sub Code_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        mousetop = Me.Top - (Cursor.Position.Y)
        mouseleft = Me.Left - (Cursor.Position.X)
        movefrm = True
    End Sub

  
    Private Sub Code_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub
End Class