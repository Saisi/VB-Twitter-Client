Imports TwitterVB2
Imports System.Text.RegularExpressions
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Net




Public Class Makmende
    Implements IDisposable

    Public LastMentionID As Long
    Public LastDM As Long

    Public Bored As New Dictionary(Of Integer, Long)

    Public TheTXT As String



    Public ListA(20) As String
    Public HashTags As New Dictionary(Of String, Long)
    Public MentionsDic As New Dictionary(Of Long, String)
    Public MentionsUsersDic As New Dictionary(Of Long, String)
    Public DMDic As New Dictionary(Of Long, String)
    Public DMUsersDic As New Dictionary(Of Long, String)
    Public UserID As New Dictionary(Of Long, String)

    Public Singletick As Integer = 0

    Public LatestDM As String
    Public LatestMention As String

    Public HoverPicBox As PictureBox

    Public firstrun As Integer = 0
    Public Appending As Boolean = False
    Public deleting As Boolean = False
    Public collection As String
    Public LastCollection As String

    Public LoadUsername As String
    Public LoadID As String
    Public TypeOfConfirmation As String

    Public TweetOfUser As String
    Public TweetUsername As String
    Public TweetIndex As Long

    Public TweetOfUser1 As String
    Public TweetUsername1 As String
    Public TweetIndex1 As Long

    Public trendDic As New Dictionary(Of Integer, String)
    Public NairobiTrends As New Dictionary(Of Integer, String)
    Public Str As System.IO.Stream
    Public srRead As System.IO.StreamReader

    Public TweetOfUser2 As String
    Public TweetUsername2 As String
    Public TweetIndex2 As Long

    Public MentionQueue As New Dictionary(Of String, String)
    Public DMQueue As New Dictionary(Of String, String)

    Public CallRefreshNAtive As Boolean = False

    Public ClickedRichTextBox0 As RichTextBox
    Public LoadedYet As Boolean = False

    Public Status As Integer = 0

    Public TimeOfTweet As Date
    Public TimeRange As TimeSpan
    Public TimeRangeString As String

    Public movefrm As Boolean = False
    Public postop As Double
    Public posleft As Double
    Public mousetop As Double
    Public mouseleft As Double

    Public PWD As String = My.Computer.FileSystem.CurrentDirectory

    Private Sub Makmende_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim tw As New TwitterVB2.TwitterAPI
        tw.AuthenticateWith(My.Settings.OAuthkey, My.Settings.OAuthSecret, My.Settings.ConsumerToken, My.Settings.ConsumerSecret)



        For Each Control In FlowLayoutPanel1.Controls
            If Control.GetType.Name = "RichTextBox" Then
                Control.BackColor = Color.DimGray
                Control.cursor = Cursors.Hand
            End If
        Next

        For Each Control In FlowLayoutPanel2.Controls
            If Control.GetType.Name = "RichTextBox" Then
                Control.BackColor = Color.DimGray
            End If
        Next
        Bio.Cursor = Cursors.No

        DirectoryJunk()

        Timer2.Enabled = True

        Try
            Dim MyAccount As TwitterUser = tw.AccountInformation
            ListA(2) = MyAccount.ScreenName
            LoadedYet = True
            Timer2.Enabled = False
        Catch ex As Exception
            If IsOnline() = False Then
                MsgBox("Check your internet conncetion", vbOKOnly, "App closing")
            Else
                MsgBox(ex.Message)
            End If
            'Me.Close()
            'Introduction.Close()
            Exit Sub
        End Try

        Dim CurrentDirectory As String = PWD + "\Avi0"
        If Not Directory.Exists(CurrentDirectory) Then
            Directory.CreateDirectory(CurrentDirectory)
        Else
        End If


        Dim covert As New TwitterVB2.TwitterAPI

       

        PostMentions()
        PostDMs()
        PostTrends.RunWorkerAsync()

        Timer1.Enabled = True


        Me.Size = New System.Drawing.Point(412, 508)
        CloseB.Location = New System.Drawing.Point(376, 0)
        MinB.Location = New System.Drawing.Point(376, 36)
    End Sub

    Sub checkUpdate()

        Dim webClient1 As System.Net.WebClient


        Try


            webClient1 = New System.Net.WebClient()
            Dim result As String = webClient1.DownloadString("http://saisi.me/ver.html")
            Dim x As Integer = CInt(result)

            If x > 1 Then
                If MsgBox("You are using an outdated veriosn of this program" & vbNewLine & "Would you like to update?", vbYesNo, "Update App") = vbYes Then
                    System.Diagnostics.Process.Start("http://saisi.me/makmende")

                End If
            End If
            webClient1.Dispose()
        Catch ex As Exception

        End Try





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

    Sub PostMentions()




        Dim mytext As RichTextBox = RichTextBox1
        Try
            Dim tw As New TwitterVB2.TwitterAPI
            Dim Username As String = ""
            Dim UpdatedTweet As String = ""
            Dim c As Integer
            Dim StatusID As Long = 0


            tw.AuthenticateWith(My.Settings.OAuthkey, My.Settings.OAuthSecret, My.Settings.ConsumerToken, My.Settings.ConsumerSecret)

            Dim PlaceHolder As String = " "

            Dim tp As New TwitterParameters
            tp.Add(TwitterParameterNames.Count, 16)

           
            Bored.Clear()



            For Each tweet As TwitterStatus In tw.Mentions(tp)



                Username = tweet.User.ScreenName

                Dim MyPictureBox As PictureBox = PictureBox1


                UpdatedTweet = tweet.Text
                Dim SearchReturn As Integer = UpdatedTweet.IndexOf("&lt;")
                If SearchReturn <> -1 Then
                    UpdatedTweet = Replace(UpdatedTweet, "&lt;", ">")
                End If
                SearchReturn = UpdatedTweet.IndexOf("&gt;")
                If SearchReturn <> -1 Then
                    UpdatedTweet = Replace(UpdatedTweet, "&gt;", ">")
                End If
                c = Len(UpdatedTweet)
                StatusID = tweet.ID

                Dim ProfileImageUrl As String






                Dim CurrentDirectory As String = PWD + "\Avi0\" + Username + ".jpg"

                If File.Exists(CurrentDirectory) Then

                    For Each control As Control In FlowLayoutPanel1.Controls


                        If control.Name <> "GroupBox1" Then
                            Try
                                MyPictureBox = control.Controls.Item(0)
                            Catch ex As Exception
                                MyPictureBox = control.Controls.Item(1)
                            End Try

                            If MyPictureBox.Image Is Nothing Then

                                Exit For

                            End If

                        End If



                    Next

                    CurrentDirectory = PWD + "\Avi0\" + Username + ".jpg"
                    MyPictureBox.Image = New System.Drawing.Bitmap(CurrentDirectory)


                Else



                    ProfileImageUrl = tw.ShowUser(Username).ProfileImageUrl

                    Dim MyWebClient As New System.Net.WebClient
                    CurrentDirectory = PWD + "\Avi0\" + Username + ".jpg"

                    MyWebClient.DownloadFile(ProfileImageUrl, CurrentDirectory)



                    For Each control As Control In FlowLayoutPanel1.Controls
                        If control.Name <> "GroupBox1" Then

                            Try
                                MyPictureBox = control.Controls.Item(0)
                            Catch ex As Exception
                                MyPictureBox = control.Controls.Item(1)
                            End Try

                            If MyPictureBox.Image Is Nothing Then

                                Exit For

                            End If

                        End If

                    Next

                    CurrentDirectory = PWD + "\Avi0\" + Username + ".jpg"
                    MyPictureBox.Image = Image.FromFile(CurrentDirectory)


                End If



                TimeOfTweet = CDate(tweet.CreatedAtLocalTime)
                TimeRange = Date.Now - TimeOfTweet

                TimeRanger()

                While Len(PlaceHolder) < 116
                    PlaceHolder += " "
                End While


                Dim group As New GroupBox


                For Each control As Control In FlowLayoutPanel1.Controls
                    If control.Name <> "GroupBox1" Then

                        Try
                            mytext = control.Controls.Item(0)
                        Catch ex As Exception
                            mytext = control.Controls.Item(1)
                        End Try



                        If Trim(mytext.Text) = "" Then
                            group = control
                            Exit For

                        End If

                    End If

                Next

                group.Visible = True


                mytext.Text = Username & " "
                mytext.AppendText(vbNewLine & UpdatedTweet)
                mytext.AppendText(vbNewLine & PlaceHolder & TimeRangeString)

                Dim startindex As Integer = 0



                Dim endindex As Integer = Len(Username)
                mytext.Select(0, endindex)
                mytext.SelectionColor = Color.Blue
                mytext.SelectionFont = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                startindex = mytext.Text.IndexOf(UpdatedTweet, 0)
                endindex = Len(UpdatedTweet)
                mytext.Select(startindex, endindex)
                mytext.SelectionFont = New System.Drawing.Font("Verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                startindex = mytext.Text.IndexOf(PlaceHolder & TimeRangeString, 0)
                endindex = Len(PlaceHolder & TimeRangeString)
                mytext.Select(startindex, endindex)
                mytext.SelectionFont = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                Dim yoh As String = UpdatedTweet

                Dim xr As New Regex("@\w*")
                Dim matches As MatchCollection = xr.Matches(yoh)
                Dim xx As Integer = 0
                Dim f As Integer = 0
                Dim hr As String
                Dim cummulating As String = ""

                For Each Match As Match In matches
                    f = yoh.IndexOf(Match.Value)
                    f -= 1

                    If Match.Value = "@" Then Continue For


                    If f > 0 Then

                        hr = yoh.Substring(f, 1)
                        If hr = " " Then
                            cummulating += " " & Match.Value


                            If Not HashTags.ContainsKey(Match.Value) Then
                                HashTags.Add(Match.Value, HashTags.Count)
                            End If

                        End If

                    ElseIf f = -1 Then
                        cummulating += " " & Match.Value

                        If Not HashTags.ContainsKey(Match.Value) Then
                            HashTags.Add(Match.Value, HashTags.Count)
                        End If



                    End If

                    startindex = mytext.Text.IndexOf(Match.Value)
                    endindex = Len(Match.Value)
                    mytext.Select(startindex, endindex)
                    mytext.SelectionFont = New System.Drawing.Font("verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    mytext.SelectionColor = Color.Blue
                Next

                'If Not MentionsUsersDic.ContainsKey(tweet.ID) Then
                'MentionsUsersDic.Add(tweet.ID, cummulating)
                'End If


                Dim xHashtags As New Regex("\x23\w*")
                Dim matchesHashtags As MatchCollection = xHashtags.Matches(yoh)
                xx = 0
                cummulating = ""
                For Each Match As Match In matchesHashtags
                    xx += 1
                    f = yoh.IndexOf(Match.Value)
                    f -= 1

                    If Match.Value = "#" Then Continue For

                    cummulating += " " & Match.Value



                    If Not HashTags.ContainsKey(Match.Value) Then
                        HashTags.Add(Match.Value, HashTags.Count)
                    End If

                    startindex = mytext.Text.IndexOf(Match.Value)
                    endindex = Len(Match.Value)
                    mytext.Select(startindex, endindex)
                    mytext.SelectionFont = New System.Drawing.Font("verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    mytext.SelectionColor = Color.Blue


                Next



                'If Not HashTags.ContainsKey(cummulating) Then
                'HashTags.Add(cummulating, tweet.ID)
                'End If

                UpdatedTweet = Trim(UpdatedTweet)


                MentionsDic.Add(StatusID, UpdatedTweet)
                MentionsUsersDic.Add(StatusID, Username)

                'MsgBox(CStr(StatusID))
                Bored.Add(Bored.Count, StatusID)


            Next



            For Each control As Control In FlowLayoutPanel1.Controls
                If control.Name <> "GroupBox1" Then
                    Try
                        mytext = control.Controls.Item(0)
                    Catch ex As Exception
                        mytext = control.Controls.Item(1)
                    End Try


                    If Trim(mytext.Text) = "" Then
                        mytext.Visible = False
                        control.Visible = False
                    Else
                        mytext.Visible = True
                        control.Visible = True
                    End If

                    mytext.ReadOnly = True


                End If
            Next




            LastMentionID = Bored(0)

        Catch ex As TwitterAPIException
            If IsOnline() = False Then
                TypeOfConfirmation = "Not connceted to Internet"
                Confirmation.Show()
            Else

                TypeOfConfirmation = ex.Message
                Confirmation.Show()
            End If


        End Try

        Bored.Clear()

    End Sub

    Sub PostDMs()
        Bored.Clear()

        Dim stop0 As Boolean = False


        Dim mytext As RichTextBox = RichTextBox36
        Try
            Dim tw As New TwitterVB2.TwitterAPI
            Dim Username As String = ""
            Dim UpdatedTweet As String = ""
            Dim c As Integer
            Dim StatusID As Long = 0


            tw.AuthenticateWith(My.Settings.OAuthkey, My.Settings.OAuthSecret, My.Settings.ConsumerToken, My.Settings.ConsumerSecret)

            Dim PlaceHolder As String = " "

            Dim tp As New TwitterParameters
            tp.Add(TwitterParameterNames.Count, 15)




            Dim z0 As Integer = 0


            For Each Message As TwitterDirectMessage In tw.DirectMessages(tp)


                z0 += 1

                Username = Message.SenderScreenName

                Dim MyPictureBox As PictureBox = PictureBox37


                UpdatedTweet = Message.Text
                Dim SearchReturn As Integer = UpdatedTweet.IndexOf("&lt;")
                If SearchReturn <> -1 Then
                    UpdatedTweet = Replace(UpdatedTweet, "&lt;", ">")
                End If
                SearchReturn = UpdatedTweet.IndexOf("&gt;")
                If SearchReturn <> -1 Then
                    UpdatedTweet = Replace(UpdatedTweet, "&gt;", ">")
                End If
                c = Len(UpdatedTweet)
                StatusID = Message.ID



                DMDic.Add(StatusID, UpdatedTweet)
                DMUsersDic.Add(StatusID, Username)

                Dim ProfileImageUrl As String

                Dim CurrentDirectory As String = PWD + "\Avi0\" + Username + ".jpg"

                If File.Exists(CurrentDirectory) Then

                    For Each control As Control In FlowLayoutPanel2.Controls


                        If control.Name <> "GroupBox1" Then
                            Try
                                MyPictureBox = control.Controls.Item(0)
                            Catch ex As Exception
                                MyPictureBox = control.Controls.Item(1)
                            End Try

                            If MyPictureBox.Image Is Nothing Then

                                Exit For

                            End If

                        End If



                    Next

                    CurrentDirectory = PWD + "\Avi0\" + Username + ".jpg"
                    MyPictureBox.Image = New System.Drawing.Bitmap(CurrentDirectory)


                Else



                    ProfileImageUrl = tw.ShowUser(Username).ProfileImageUrl

                    Dim MyWebClient As New System.Net.WebClient
                    CurrentDirectory = PWD + "\Avi0\" + Username + ".jpg"

                    MyWebClient.DownloadFile(ProfileImageUrl, CurrentDirectory)




                    For Each control As Control In FlowLayoutPanel2.Controls


                        If control.Name <> "GroupBox1" Then

                            Try
                                mytext = control.Controls.Item(0)
                            Catch ex As Exception
                                mytext = control.Controls.Item(1)
                            End Try

                            If MyPictureBox.Image Is Nothing Then

                                Exit For

                            End If

                        End If



                    Next

                    CurrentDirectory = PWD + "\Avi0\" + Username + ".jpg"
                    MyPictureBox.Image = Image.FromFile(CurrentDirectory)


                End If


                TimeOfTweet = CDate(Message.CreatedAtLocalTime)
                TimeRange = Date.Now - TimeOfTweet

                TimeRanger()

                While Len(PlaceHolder) < 116
                    PlaceHolder += " "
                End While



                Dim group As New GroupBox


                For Each control As Control In FlowLayoutPanel2.Controls
                    If control.Name <> "GroupBox1" Then

                        Try
                            mytext = control.Controls.Item(0)
                        Catch ex As Exception
                            mytext = control.Controls.Item(1)
                        End Try



                        If Trim(mytext.Text) = "" Then
                            group = control
                            Exit For

                        End If

                    End If

                Next

                group.Visible = True

                mytext.Text = Username & " "
                mytext.AppendText(vbNewLine & UpdatedTweet)
                mytext.AppendText(vbNewLine & PlaceHolder & TimeRangeString)

                Dim startindex As Integer = 0



                Dim endindex As Integer = Len(Username)
                mytext.Select(0, endindex)
                mytext.SelectionColor = Color.Blue
                mytext.SelectionFont = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                Dim splittr As Regex = New Regex("\s")

                Dim substrings() As String = splittr.Split(mytext.Text)



                startindex = mytext.Text.IndexOf(substrings(1), 0)
                endindex = Len(UpdatedTweet)
                mytext.Select(startindex, endindex)
                mytext.SelectionFont = New System.Drawing.Font("Verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                startindex = mytext.Text.IndexOf(PlaceHolder & TimeRangeString, 0)
                endindex = Len(PlaceHolder & TimeRangeString)
                mytext.Select(startindex, endindex)
                mytext.SelectionFont = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                Dim yoh As String = UpdatedTweet

                Dim xr As New Regex("@\w*")
                Dim matches As MatchCollection = xr.Matches(yoh)
                Dim xx As Integer = 0
                Dim f As Integer = 0
                Dim hr As String
                Dim cummulating As String = ""

                For Each Match As Match In matches
                    f = yoh.IndexOf(Match.Value)
                    f -= 1

                    If Match.Value = "@" Then Continue For


                    If f > 0 Then

                        hr = yoh.Substring(f, 1)
                        If hr = " " Then
                            cummulating += " " & Match.Value


                            If Not HashTags.ContainsKey(Match.Value) Then
                                HashTags.Add(Match.Value, HashTags.Count)
                            End If

                        End If

                    ElseIf f = -1 Then
                        cummulating += " " & Match.Value

                        If Not HashTags.ContainsKey(Match.Value) Then
                            HashTags.Add(Match.Value, HashTags.Count)
                        End If



                    End If

                    startindex = mytext.Text.IndexOf(Match.Value)
                    endindex = Len(Match.Value)
                    mytext.Select(startindex, endindex)
                    mytext.SelectionFont = New System.Drawing.Font("verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    mytext.SelectionColor = Color.Blue
                Next

                'If Not MentionsUsersDic.ContainsKey(tweet.ID) Then
                'MentionsUsersDic.Add(tweet.ID, cummulating)
                'End If


                Dim xHashtags As New Regex("\x23\w*")
                Dim matchesHashtags As MatchCollection = xHashtags.Matches(yoh)
                xx = 0
                cummulating = ""
                For Each Match As Match In matchesHashtags
                    xx += 1
                    f = yoh.IndexOf(Match.Value)
                    f -= 1

                    If Match.Value = "#" Then Continue For

                    cummulating += " " & Match.Value



                    If Not HashTags.ContainsKey(Match.Value) Then
                        HashTags.Add(Match.Value, HashTags.Count)
                    End If

                    startindex = mytext.Text.IndexOf(Match.Value)
                    endindex = Len(Match.Value)
                    mytext.Select(startindex, endindex)
                    mytext.SelectionFont = New System.Drawing.Font("verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    mytext.SelectionColor = Color.Blue


                Next



                'If Not HashTags.ContainsKey(cummulating) Then
                'HashTags.Add(cummulating, tweet.ID)
                'End If
                Bored.Add(Bored.Count, StatusID)


            Next

            For Each control As Control In FlowLayoutPanel2.Controls
                If control.Name <> "GroupBox1" Then
                    Try
                        mytext = control.Controls.Item(0)
                    Catch ex As Exception
                        mytext = control.Controls.Item(1)
                    End Try


                    If Trim(mytext.Text) = "" Then
                        mytext.Visible = False
                        control.Visible = False
                    Else
                        mytext.Visible = True
                        control.Visible = True
                    End If

                    mytext.ReadOnly = True


                End If
            Next

            LastDM = Bored(0)



        Catch ex As TwitterAPIException
            If IsOnline() = False Then


                TypeOfConfirmation = "Not connceted to Internet"
                Confirmation.Show()

            Else

                TypeOfConfirmation = ex.Message
                Confirmation.Show()



            End If


        End Try

        Bored.Clear()


    End Sub


    Sub TimeRanger()



        TimeRangeString = ""
        If TimeRange.TotalSeconds < 10 Then

            TimeRangeString = TimeRange.TotalSeconds
            TimeRangeString = TimeRangeString.Substring(0, 1)
            TimeRangeString += " secs ago"
        ElseIf TimeRange.TotalSeconds < 60 Then

            TimeRangeString = TimeRange.TotalSeconds
            TimeRangeString = TimeRangeString.Substring(0, 2)
            TimeRangeString += " secs ago"
        ElseIf TimeRange.TotalMinutes >= 1 And TimeRange.TotalMinutes < 10 Then
            TimeRangeString = TimeRange.TotalMinutes
            TimeRangeString = TimeRangeString.Substring(0, 1)
            If TimeRangeString = "1" Then
                TimeRangeString += " min ago"
            Else
                TimeRangeString += " mins ago"
            End If
        ElseIf TimeRange.TotalMinutes >= 10 And TimeRange.TotalMinutes < 60 Then
            TimeRangeString = TimeRange.TotalMinutes
            TimeRangeString = TimeRangeString.Substring(0, 2)
            TimeRangeString += " mins ago"
        ElseIf TimeRange.TotalHours >= 1 And TimeRange.TotalHours < 10 Then
            TimeRangeString = TimeRange.TotalHours
            TimeRangeString = TimeRangeString.Substring(0, 1)
            If TimeRangeString = "1" Then
                TimeRangeString += " hr ago"
            Else
                TimeRangeString += " hrs ago"
            End If
        ElseIf TimeRange.TotalHours >= 10 And TimeRange.TotalHours < 24 Then
            TimeRangeString = TimeRange.TotalHours
            TimeRangeString = TimeRangeString.Substring(0, 2)
            TimeRangeString += " hrs ago"
        ElseIf TimeRange.TotalDays >= 1 And TimeRange.TotalDays < 10 Then
            TimeRangeString = TimeRange.TotalDays
            TimeRangeString = TimeRangeString.Substring(0, 1)
            If TimeRangeString = "1" Then
                TimeRangeString += " day ago"
            Else
                TimeRangeString += " days ago"
            End If
        ElseIf TimeRange.TotalDays >= 10 And TimeRange.TotalDays < 100 Then
            TimeRangeString = TimeRange.TotalDays
            TimeRangeString = TimeRangeString.Substring(0, 2)
            TimeRangeString += " days ago"
        ElseIf TimeRange.TotalDays >= 100 And TimeRange.TotalDays < 366 Then
            TimeRangeString = TimeRange.TotalDays
            TimeRangeString = TimeRangeString.Substring(0, 3)
            TimeRangeString += " days ago"
        ElseIf TimeRange.TotalDays > 365 And TimeRange.TotalDays < 730 Then
            TimeRangeString += "1 yr ago"
        ElseIf TimeRange.TotalDays > 730 And TimeRange.TotalDays < 1095 Then
            TimeRangeString += "2 yrs ago"
        ElseIf TimeRange.TotalDays > 1095 And TimeRange.TotalDays < 1460 Then
            TimeRangeString += "3 yrs ago"
        ElseIf TimeRange.TotalDays > 1460 And TimeRange.TotalDays < 1825 Then
            TimeRangeString += "4 yrs ago"
        ElseIf TimeRange.TotalDays > 1825 And TimeRange.TotalDays < 2190 Then
            TimeRangeString += "5 yrs ago"
        ElseIf TimeRange.TotalDays > 2190 And TimeRange.TotalDays < 730 Then
            TimeRangeString += " 600BC"
        End If
    End Sub

    Private Sub PictureBox11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseB.Click
        normalizeb()
        CloseB.BorderStyle = BorderStyle.FixedSingle
        Me.Close()
        Trends.Close()
        About.Close()
        Introduction.Close()
    End Sub

    Private Sub Makmende_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If movefrm Then
            postop = (Cursor.Position.Y)
            posleft = (Cursor.Position.X)
            Me.Top = mousetop + postop
            Me.Left = mouseleft + posleft
        End If

    End Sub

    Private Sub Makmende_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        Me.Cursor = Cursors.Default
        movefrm = False
    End Sub

    Private Sub Makmende_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        mousetop = Me.Top - (Cursor.Position.Y)
        mouseleft = Me.Left - (Cursor.Position.X)
        movefrm = True
    End Sub



    Sub showprofile()

        If IsOnline() Then
        Else

            TypeOfConfirmation = "You aren't connected to the Internet mate"
            Confirmation.Show()

            Exit Sub
        End If


        If LoadProfile.IsBusy Then
            LoadProfile.CancelAsync()
        End If

        Dim tw As New TwitterVB2.TwitterAPI
        tw.AuthenticateWith(My.Settings.OAuthkey, My.Settings.OAuthSecret, My.Settings.ConsumerToken, My.Settings.ConsumerSecret)


        Try
            Label2.Text = tw.ShowUser(LoadUsername).Name
        Catch ex As Exception
            If Not IsOnline() Then
                TypeOfConfirmation = "You aren't connected to the Internet mate"
                Confirmation.Show()
            Else
                TypeOfConfirmation = "Profile doesn't exist"
                Confirmation.Show()
            End If
            Exit Sub
        End Try


        Label2.Visible = True

        Me.Cursor = Cursors.WaitCursor

        Me.Size = New System.Drawing.Point(576, 508)

        CloseB.Location = New System.Drawing.Point(514, 0)
        MinB.Location = New System.Drawing.Point(503, 0)

        ListBox1.Visible = True




        FollowersLabel.Text = "Followers ... " & CStr(tw.ShowUser(LoadUsername).FollowersCount)
        FollowersLabel.Visible = True
        FollowingLabel.Text = "Following ..." & CStr(tw.ShowUser(LoadUsername).FriendsCount)
        FollowingLabel.Visible = True
        TweetsLabel.Text = "Tweets..." & CStr(tw.ShowUser(LoadUsername).StatusesCount)
        TweetsLabel.Visible = True

        Bio.ReadOnly = False
        Bio.Text = CStr(tw.ShowUser(LoadUsername).Description)
        Bio.Visible = True
        Bio.ReadOnly = True

        LoadProfile.RunWorkerAsync()

















    End Sub


    Private Sub ReplyB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplyB.Click

        normalizeb()
        ReplyB.BorderStyle = BorderStyle.FixedSingle

        If FlowLayoutPanel1.Visible Then

            TweetUsername1 = TweetUsername
            TweetIndex1 = TweetIndex
            TweetOfUser1 = TweetOfUser




            Status = 1

           

            TextBox1.Text = "@" & TweetUsername1 & " "

            TextBox1.SelectionStart = TextBox1.Text.Length + 1
            TextBox1.Focus()


        Else

            TweetUsername2 = TweetUsername
            TweetIndex2 = TweetIndex
            TweetOfUser2 = TweetOfUser


            TextBox1.Text = ""
            Label4.Visible = True

            Status = 2


            TextBox1.SelectionStart = TextBox1.Text.Length + 1
            TextBox1.Focus()

            Dim x As Integer = FlowLayoutPanel2.Controls.IndexOf(GroupBox1)
            Dim Group As GroupBox = FlowLayoutPanel2.Controls.Item(x - 1)
            



            TweetB.Size = New System.Drawing.Point(90, 21)
            TweetB.Text = "Send DM"
        End If
    End Sub

    Sub normalizeb()
        Dim mypic As PictureBox = PictureBox1

        For Each Control0 As Control In Me.Controls
            If Control0.GetType.Name = "PictureBox" Then
                mypic = DirectCast(Control0, PictureBox)
                mypic.BorderStyle = BorderStyle.None
            End If
        Next


        For Each Control In GroupBox1.Controls
            If Control.GetType.Name = "PictureBox" Then
                mypic = DirectCast(Control, PictureBox)
                mypic.BorderStyle = BorderStyle.None
            End If

        Next


       

    End Sub

    Private Sub TweetB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TweetB.Click

        If TextBox1.Text.Length > 140 Then
            TypeOfConfirmation = "Tweet longer than 140 Characters!"
            Confirmation.Show()
            Exit Sub
        End If

        Dim tw As New TwitterVB2.TwitterAPI
        tw.AuthenticateWith(My.Settings.OAuthkey, My.Settings.OAuthSecret, My.Settings.ConsumerToken, My.Settings.ConsumerSecret)

        If Status = 0 Then


            Try
                tw.Update(Trim(TextBox1.Text))
                TextBox1.Text = ""
            Catch ex As Exception
                If Not IsOnline() Then

                    TypeOfConfirmation = "You aren't connected to the Internet mate"
                    Confirmation.Show()

                Else
                    TypeOfConfirmation = ex.Message
                    Confirmation.Show()

                End If
            End Try

        ElseIf Status = 1 Then


            Try
                tw.ReplyToUpdate(Trim(TextBox1.Text), TweetIndex1)
                TextBox1.Text = ""
            Catch ex As Exception
                Status = 0
                If Not IsOnline() Then

                    TypeOfConfirmation = ex.Message
                    Confirmation.Show()

                Else
                    MsgBox(ex.Message, vbOKOnly, "Error")

                End If
            End Try

            Status = 0

        Else
            Try
                Dim SentMessage As TwitterDirectMessage = tw.SendDirectMessage(TweetUsername2, Trim(TextBox1.Text))

                TweetB.Text = "Post Tweet"
                TweetB.Size = New System.Drawing.Point(83, 21)
                Status = 0
                TextBox1.Text = ""
                Label4.Visible = False
            Catch ex As Exception

                If Not IsOnline() Then


                    TypeOfConfirmation = "You aren't connected to the Internet mate"
                    Confirmation.Show()

                Else
                    TypeOfConfirmation = ex.Message
                    Confirmation.Show()

                End If
            End Try


        End If


    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Dim x0 As Integer
        x0 = Len(TextBox1.Text)

        x0 -= 140

        If x0 < 0 Then
            x0 *= -1
            Remaining.Text = CStr(x0)
            Remaining.ForeColor = Color.Black
        Else
            Remaining.Text = "-" & CStr(x0)
            Remaining.ForeColor = Color.DarkRed
        End If



        Dim x As String = TextBox1.Text


        If Trim(x) = "" Then
            Exit Sub
        End If
        x = x.Substring(Len(x) - 1, 1)

        If x = "#" Then
            Hashtag()
            Exit Sub
        ElseIf x = "@" Then
            mentions()
            Exit Sub
        End If



        If ListBox9.Items.Count > 0 Then
            x = ListBox9.Items.Item(0)
            x = x.Substring(0, 1)
            If x = "#" Then
                Hashtag()
            ElseIf x = "@" Then
                mentions()
            End If
        End If

    End Sub

    Private Sub RetweetB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RetweetB.Click
        normalizeb()
        RetweetB.BorderStyle = BorderStyle.FixedSingle

        Dim tw As New TwitterVB2.TwitterAPI
        tw.AuthenticateWith(My.Settings.OAuthkey, My.Settings.OAuthSecret, My.Settings.ConsumerToken, My.Settings.ConsumerSecret)




        Try
            tw.Retweet(TweetIndex)
            TypeOfConfirmation = "Retweeted!"
            Confirmation.Show()


        Catch ex As Exception
            If Not IsOnline() Then

                TypeOfConfirmation = "You aren't connected to the Internet mate"
                Confirmation.Show()

            Else


                TypeOfConfirmation = ex.Message
                Confirmation.Show()

            End If
        End Try

    End Sub

    Private Sub Quote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Quote.Click
        normalizeb()
        Quote.BorderStyle = BorderStyle.FixedSingle



        TweetB.Size = New System.Drawing.Point(83, 21)
        TweetB.Text = "Post Tweet"

        Status = 0

        TextBox1.Text = "@" & TweetUsername & " " & TweetOfUser
        TextBox1.Text = """" & TextBox1.Text & """"


    End Sub

    Private Sub FavoriteB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FavoriteB.Click
        normalizeb()
        FavoriteB.BorderStyle = BorderStyle.FixedSingle


        Dim tw As New TwitterVB2.TwitterAPI
        tw.AuthenticateWith(My.Settings.OAuthkey, My.Settings.OAuthSecret, My.Settings.ConsumerToken, My.Settings.ConsumerSecret)


        Try
            tw.AddFavorite(TweetIndex)
            TypeOfConfirmation = "Tweet Fav'ed!"
            Confirmation.Show()


        Catch ex As Exception

            If Not IsOnline() Then



                TypeOfConfirmation = "You aren't connected to the Internet mate"
                Confirmation.Show()

            Else
                TypeOfConfirmation = ex.Message
                Confirmation.Show()

            End If
        End Try


    End Sub

    Private Sub ReduceB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReduceB.Click
        normalizeb()
        ReduceB.BorderStyle = BorderStyle.FixedSingle
        CloseB.Location = New System.Drawing.Point(376, 0)
        MinB.Location = New System.Drawing.Point(376, 36)

        Label3.Visible = False
        Label2.Visible = False
        TweetsLabel.Visible = False
        FollowingLabel.Visible = False
        FollowersLabel.Visible = False
        Label1.Visible = False
        ListBox9.Visible = False
        Avi.Visible = False
        Bio.Visible = False


        Me.Size = New System.Drawing.Point(412, 508)
        ReduceB.Visible = False
        ExpandB.Visible = True
    End Sub

    Private Sub MinB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MinB.Click
        normalizeb()
        MinB.BorderStyle = BorderStyle.FixedSingle

        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub ExpandB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpandB.Click
        normalizeb()
        ExpandB.BorderStyle = BorderStyle.FixedSingle
        Me.Size = New System.Drawing.Point(574, 508)

        CloseB.Location = New System.Drawing.Point(542, 0)
        MinB.Location = New System.Drawing.Point(514, 0)

        ExpandB.Visible = False
        ReduceB.Visible = True

    End Sub






    Sub ClickRichTextBox()

        Try

            ClickedRichTextBox0.Select(0, 0)






            FavoriteB.Visible = False
            'Dim SearchIndex As long = TweetIndex

            'Dim substitute As Integer = SearchIndex




            Dim p0 As Integer = 0






            Dim xPos As Integer = System.Windows.Forms.Cursor.Position.X
            Dim yPos As Integer = System.Windows.Forms.Cursor.Position.Y

            Dim p As New Point(xPos, yPos)

            Dim clientPoint As Point = ClickedRichTextBox0.PointToClient(p)


            Dim charIndex As Integer = ClickedRichTextBox0.GetCharIndexFromPosition(clientPoint)
            Dim c As Char
            Dim erroroccurred As Integer = 0


            Try
                c = ClickedRichTextBox0.Text(charIndex)
            Catch ex As Exception
                Try
                    c = ClickedRichTextBox0.Text(charIndex - 1)
                    charIndex -= 1
                Catch ex1 As Exception
                    erroroccurred = 1
                End Try
            End Try


            If erroroccurred = 1 Then
                Exit Sub
            End If

            If c = " " Then
                Exit Sub
            Else



                Dim endindex As Integer = 1

                Dim regex As Regex = New Regex("\s")
                Dim TheText = Trim(ClickedRichTextBox0.Text)
                Dim substrings() As String = regex.Split(TheText)
                Dim startindex As Integer = 0
                Dim endindex0 As Integer = 0
                Dim s As String = ""
                Dim a0 As String = ""
                Dim x As Integer = 0
                Dim x0 As Integer = 0




                For Each Match As String In substrings


                    s += " " & Match
                    s = Trim(s)
                    x = Len(s) - 1

                    If x >= charIndex Then

                        endindex = x

                        Exit For
                    End If
                Next


                ClickedRichTextBox0.Select(charIndex, endindex - charIndex + 1)




                a0 = s

                If ClickedRichTextBox0.SelectionColor = Color.Blue And ClickedRichTextBox0.SelectedText.IndexOf(".") = -1 Then


                    substrings = regex.Split(a0)
                    LoadUsername = substrings(1)
                    LoadUsername.Remove(0, 1)

                    showprofile()
                    Exit Sub


                    If ClickedRichTextBox0.SelectionColor = Color.Blue And ClickedRichTextBox0.SelectionFont.Size = 7.0! Then

                        a0 = a0.Substring(0, 1)
                        If a0 = "#" Then

                        ElseIf a0 = "@" Then
                            LoadUsername = a0.Substring(1, Len(a0) - 1)
                            showprofile()
                        Else
                            p0 = s.IndexOf("www", 0)
                            If p0 = 0 Then


                            Else
                                p0 = s.IndexOf("http", 0)
                                If p0 = 0 Then
                                Else
                                    s = "http://" & s
                                End If

                            End If

                            System.Diagnostics.Process.Start(s)
                        End If
                    End If

                End If

            End If

        Catch ex As Exception
        End Try



    End Sub

    Private Sub HoverRichTextBox()
        Dim regex As Regex = New Regex("\s")

        Dim substrings() As String = regex.Split(ClickedRichTextBox0.Text)

        Dim group As New GroupBox
        group = ClickedRichTextBox0.Parent

        Dim reached As Boolean = False



        If FlowLayoutPanel2.Visible Then
            Dim ControlIndex As Integer = FlowLayoutPanel2.Controls.IndexOf(group) + 1
            FlowLayoutPanel2.Controls.SetChildIndex(GroupBox1, ControlIndex)
            GroupBox1.Visible = True
        Else
            Dim ControlIndex As Integer = FlowLayoutPanel1.Controls.IndexOf(group) + 1
            FlowLayoutPanel1.Controls.SetChildIndex(GroupBox1, ControlIndex)
            GroupBox1.Visible = True
        End If



  
        Dim hold As New Dictionary(Of Integer, String)

        For Each element In substrings
            hold.Add(hold.Count, element)
        Next

        Dim g As Integer = hold.Count


        Dim TestUsername As String = hold(0)



        hold.Remove(0)
        hold.Remove(g - 1)
        hold.Remove(g - 2)
        hold.Remove(g - 3)


        Dim x As Integer = 2
        Dim y As Integer
        Dim k As String = ""
        Dim k1 As String = ""
        Dim collection As String = ""


        For Each element00 In hold
            k1 += element00.Value & " "
        Next

        k1 = Trim(k1)

        If FlowLayoutPanel1.Visible Then




            For Each element In MentionsDic
                k = hold(x)



                substrings = regex.Split(element.Value)

                For Each element1 In substrings
                    collection += element1 & " "
                Next
                collection = Trim(collection)


                y = collection.IndexOf(Trim(k1))
                If y = -1 Then
                    Continue For
                Else




                    y = collection.IndexOf(k1)
                    If y = -1 Then
                        Continue For

                    Else

                        TweetUsername = MentionsUsersDic.Item(element.Key)


                        If TweetUsername = TestUsername Then
                            TweetIndex = element.Key
                            TweetOfUser = element.Value
                            reached = True

                            Exit For
                        End If

                    End If



                End If

                x += 1
            Next
        ElseIf FlowLayoutPanel2.Visible Then
            For Each element In DMDic
                k = hold(x)



                substrings = regex.Split(element.Value)

                For Each element1 In substrings
                    collection += element1 & " "
                Next
                collection = Trim(collection)


                y = collection.IndexOf(Trim(k1))
                If y = -1 Then
                    Continue For
                Else




                    y = collection.IndexOf(k1)
                    If y = -1 Then
                        Continue For

                    Else

                        TweetUsername = DMUsersDic.Item(element.Key)


                        If TweetUsername = TestUsername Then
                            TweetIndex = element.Key
                            TweetOfUser = element.Value
                            reached = True

                            Exit For
                        End If

                    End If



                End If

                x += 1
            Next

        End If
    End Sub




    Private Sub LoadProfile_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadProfile.RunWorkerCompleted
        Dim CurrentDirectory As String = PWD + "\Avi0\" + LoadUsername + ".jpg"
        Avi.Image = New System.Drawing.Bitmap(CurrentDirectory)
        Me.Cursor = Cursors.Default
        Avi.Visible = True
    End Sub

    Private Sub PictureBox11_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SettingsB.Click
        normalizeb()

        About.Show()
        SettingsB.BorderStyle = BorderStyle.FixedSingle
    End Sub

    Private Sub PictureBox11_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DMB.Click
        normalizeb()

        Status = 2

        FlowLayoutPanel1.Controls.Remove(GroupBox1)
        FlowLayoutPanel2.Controls.Add(GroupBox1)

        FlowLayoutPanel2.Size = FlowLayoutPanel1.Size
        FlowLayoutPanel2.Location = FlowLayoutPanel1.Location
        FlowLayoutPanel1.Visible = False
        FlowLayoutPanel2.Visible = True

        DMB.Visible = False
        mentionsB.Visible = True

        DMB.BorderStyle = BorderStyle.FixedSingle

        RetweetB.Visible = False
        Quote.Visible = False
        FavoriteB.Visible = False

    End Sub

    Sub GetOneMention()
     
               

        LatestMention = ""

        Bored.Clear()

        Dim mytext As New RichTextBox

        Try
            Dim tw As New TwitterVB2.TwitterAPI
            Dim Username As String = ""
            Dim UpdatedTweet As String = ""
            Dim c As Integer
            Dim StatusID As Long = 0


            tw.AuthenticateWith(My.Settings.OAuthkey, My.Settings.OAuthSecret, My.Settings.ConsumerToken, My.Settings.ConsumerSecret)

            Dim PlaceHolder As String = " "


            Dim tps As New TwitterParameters
            tps.Add(TwitterParameterNames.Count, 2)




            For Each tweet As TwitterStatus In tw.Mentions(tps)



                Username = tweet.User.ScreenName

                Dim MyPictureBox As PictureBox = PictureBox1


                UpdatedTweet = tweet.Text
                Dim SearchReturn As Integer = UpdatedTweet.IndexOf("&lt;")
                If SearchReturn <> -1 Then
                    UpdatedTweet = Replace(UpdatedTweet, "&lt;", ">")
                End If
                SearchReturn = UpdatedTweet.IndexOf("&gt;")
                If SearchReturn <> -1 Then
                    UpdatedTweet = Replace(UpdatedTweet, "&gt;", ">")
                End If
                c = Len(UpdatedTweet)
                StatusID = tweet.ID

                If MentionsDic.ContainsKey(StatusID) Then Exit Sub

                LatestMention = Username & " " & UpdatedTweet

                MentionsDic.Add(StatusID, UpdatedTweet)
                MentionsUsersDic.Add(StatusID, Username)




                TimeOfTweet = CDate(tweet.CreatedAtLocalTime)
                TimeRange = Date.Now - TimeOfTweet

                TimeRanger()

                While Len(PlaceHolder) < 116
                    PlaceHolder += " "
                End While

                Dim profileImageUrl As String





                'Dim k As Integer = FlowLayoutPanel1.Controls.Count
                'Dim pic As PictureBox = FlowLayoutPanel1.Controls.Item(k - 2)
                'mytext = FlowLayoutPanel1.Controls.Item(k - 1)
                'mytext.Clear()

                'FlowLayoutPanel1.Controls.SetChildIndex(pic, 0)
                'FlowLayoutPanel1.Controls.SetChildIndex(mytext, 1)


                Dim CurrentDirectory As String = PWD + "\Avi0\" + Username + ".jpg"

                If Not File.Exists(CurrentDirectory) Then


                    profileImageUrl = tw.ShowUser(Username).ProfileImageUrl

                    Dim MyWebClient As New System.Net.WebClient
                    CurrentDirectory = PWD + "\Avi0\" + Username + ".jpg"

                    MyWebClient.DownloadFile(profileImageUrl, CurrentDirectory)


                End If


                mytext.Text = Username & " "
                mytext.AppendText(vbNewLine & UpdatedTweet)
                mytext.AppendText(vbNewLine & PlaceHolder & TimeRangeString)

                Dim startindex As Integer = 0



                Dim endindex As Integer = Len(Username)
                mytext.Select(0, endindex)
                mytext.SelectionColor = Color.Blue
                mytext.SelectionFont = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                startindex = mytext.Text.IndexOf(UpdatedTweet, 0)
                endindex = Len(UpdatedTweet)
                mytext.Select(startindex, endindex)
                mytext.SelectionFont = New System.Drawing.Font("verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                startindex = mytext.Text.IndexOf(PlaceHolder & TimeRangeString, 0)
                endindex = Len(PlaceHolder & TimeRangeString)
                mytext.Select(startindex, endindex)
                mytext.SelectionFont = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                Dim yoh As String = UpdatedTweet

                Dim xr As New Regex("@\w*")
                Dim matches As MatchCollection = xr.Matches(yoh)
                Dim xx As Integer = 0
                Dim f As Integer = 0
                Dim hr As String
                Dim cummulating As String = ""

                For Each Match As Match In matches
                    f = yoh.IndexOf(Match.Value)
                    f -= 1

                    If Match.Value = "@" Then Continue For


                    If f > 0 Then

                        hr = yoh.Substring(f, 1)
                        If hr = " " Then
                            cummulating += " " & Match.Value


                            If Not HashTags.ContainsKey(Match.Value) Then
                                HashTags.Add(Match.Value, HashTags.Count)
                            End If

                        End If

                    ElseIf f = -1 Then
                        cummulating += " " & Match.Value

                        If Not HashTags.ContainsKey(Match.Value) Then
                            HashTags.Add(Match.Value, HashTags.Count)
                        End If



                    End If

                    startindex = mytext.Text.IndexOf(Match.Value)
                    endindex = Len(Match.Value)
                    mytext.Select(startindex, endindex)
                    mytext.SelectionFont = New System.Drawing.Font("verdana", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    mytext.SelectionColor = Color.Blue


                Next

                'If Not MentionsUsersDic.ContainsKey(tweet.ID) Then
                'MentionsUsersDic.Add(tweet.ID, cummulating)
                'End If


                Dim xHashtags As New Regex("\x23\w*")
                Dim matchesHashtags As MatchCollection = xHashtags.Matches(yoh)
                xx = 0
                cummulating = ""
                For Each Match As Match In matchesHashtags
                    xx += 1
                    f = yoh.IndexOf(Match.Value)
                    f -= 1

                    If Match.Value = "#" Then Continue For

                    cummulating += " " & Match.Value



                    If Not HashTags.ContainsKey(Match.Value) Then
                        HashTags.Add(Match.Value, HashTags.Count)
                    End If

                    startindex = mytext.Text.IndexOf(Match.Value)
                    endindex = Len(Match.Value)
                    mytext.Select(startindex, endindex)
                    mytext.SelectionFont = New System.Drawing.Font("verdana", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    mytext.SelectionColor = Color.Blue


                Next




                'If Not HashTags.ContainsKey(cummulating) Then
                'HashTags.Add(cummulating, tweet.ID)
                'End If


                MentionQueue.Add(mytext.Rtf, Username)
                mytext.Clear()
                mytext.Text = ""
                Bored.Add(Bored.Count, StatusID)
            Next

            'For Each control As Control In FlowLayoutPanel1.Controls
            'If Control.GetType.Name = "RichTextBox" Then
            'mytext = DirectCast(Control, RichTextBox)
            'If Trim(mytext.Text) = "" Then
            'mytext.Visible = False
            ' End If
            'mytext.ReadOnly = True
            'End If
            'Next


        Catch ex As TwitterAPIException
            If IsOnline() = False Then
                TypeOfConfirmation = "Not connected to Internet"
                Confirmation.Show()
            Else

                TypeOfConfirmation = ex.Message
                Confirmation.Show()
            End If


        End Try

        Bored.Clear()


    End Sub

    Sub mentions()
        Dim x As Integer = Len(Trim(TextBox1.Text))
        Dim h As Integer

        Dim lastcharacter As String = ""
        Dim random As String = ""

        If x = 0 Then
            Exit Sub
        End If

        Dim y As Integer = 0
        y = HashTags.Count

        If x = 1 Then
            lastcharacter = Trim(TextBox1.Text)
            If lastcharacter = "@" Then
                ListBox9.Visible = True
                Label1.Visible = True
                If Me.Size = New System.Drawing.Size(412, 508) Then
                    Me.Size = New System.Drawing.Size(574, 508)
                End If
                ListBox9.Items.Clear()
                For Each element In HashTags
                    h = element.Key.IndexOf("@", 0)
                    If h > -1 Then
                        ListBox9.Items.Add(element.Key)
                    End If
                Next
                Appending = True
                Exit Sub
            End If
        Else
            lastcharacter = TextBox1.Text.Substring(x - 1, 1)
            If lastcharacter = "@" Then

                Appending = True
                ListBox9.Visible = True
                Label1.Visible = True
                If Me.Size = New System.Drawing.Size(412, 508) Then
                    Me.Size = New System.Drawing.Size(574, 508)
                End If
                ListBox9.Items.Clear()
                For Each element In HashTags
                    h = element.Key.IndexOf("@", 0)
                    If h > -1 Then
                        ListBox9.Items.Add(element.Key)
                    End If
                Next
                firstrun = 1
                Exit Sub
            End If

        End If

        ListBox9.Items.Clear()
        If Appending And lastcharacter <> " " Then

            If firstrun = 1 Then
                If deleting Then

                Else
                    collection = "@" & lastcharacter
                End If
                ListBox9.Items.Clear()
                For Each element In HashTags
                    h = element.Key.IndexOf(collection, 0)
                    If h > -1 Then
                        ListBox9.Items.Add(element.Key)
                    End If
                Next
                firstrun = 2
            Else

                If deleting Then
                    collection = ""
                Else
                    collection += lastcharacter
                End If


                ListBox9.Items.Clear()
                For Each element In HashTags
                    h = element.Key.IndexOf(collection, 0)
                    If h > -1 Then
                        ListBox9.Items.Add(element.Key)
                    End If
                Next

            End If

        End If

        If lastcharacter = " " Then
            firstrun = 0
            Appending = False
            ListBox9.Visible = False
            Label1.Visible = False
        End If
    End Sub

    Sub Hashtag()
        Dim x As Integer = Len(Trim(TextBox1.Text))
        Dim h As Integer

        Dim lastcharacter As String = ""
        Dim random As String = ""

        If x = 0 Then
            Exit Sub
        End If

        Dim y As Integer = 0
        y = HashTags.Count

        If x = 1 Then
            lastcharacter = Trim(TextBox1.Text)
            If lastcharacter = "#" Then
                ListBox9.Visible = True
                Label1.Visible = True
                If Me.Size = New System.Drawing.Size(412, 508) Then
                    Me.Size = New System.Drawing.Size(574, 508)
                End If
                ListBox9.Items.Clear()
                For Each element In HashTags
                    h = element.Key.IndexOf("#", 0)
                    If h > -1 Then
                        ListBox9.Items.Add(element.Key)
                    End If
                Next
                Appending = True
                Exit Sub
            End If
        Else
            lastcharacter = TextBox1.Text.Substring(x - 1, 1)
            If lastcharacter = "#" Then

                Appending = True
                ListBox9.Visible = True
                Label1.Visible = True
                If Me.Size = New System.Drawing.Size(412, 508) Then
                    Me.Size = New System.Drawing.Size(574, 508)
                End If
                ListBox9.Items.Clear()
                For Each element In HashTags
                    h = element.Key.IndexOf("#", 0)
                    If h > -1 Then
                        ListBox9.Items.Add(element.Key)
                    End If
                Next
                firstrun = 1
                Exit Sub
            End If

        End If


        ListBox9.Items.Clear()
        If Appending And lastcharacter <> " " Then

            If firstrun = 1 Then
                If deleting Then
                Else
                    collection = "#" & lastcharacter
                End If
                ListBox9.Items.Clear()
                For Each element In HashTags
                    h = element.Key.IndexOf(collection, 0)
                    If h > -1 Then
                        ListBox9.Items.Add(element.Key)
                    End If
                Next
                firstrun = 2
            Else

                If deleting Then
                    collection = LastCollection
                Else
                    collection += lastcharacter
                End If


                ListBox9.Items.Clear()
                For Each element In HashTags
                    h = element.Key.IndexOf(collection, 0)
                    If h > -1 Then
                        ListBox9.Items.Add(element.Key)
                    End If
                Next

            End If

        End If

        If lastcharacter = " " Then
            firstrun = 0
            Appending = False
            ListBox9.Visible = False
            Label1.Visible = False
        End If
    End Sub


    Private Sub ListBox9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox9.Click
        Dim x As String = ListBox9.SelectedItem
        Dim y As Integer = Len(TextBox1.Text)
        Dim z As String = TextBox1.Text


        If y = 0 Then
            TextBox1.Text = x
            Exit Sub
        End If

        Dim exitter As String = "#"
        Dim a As Integer = 1


        While exitter <> "poo"
            Dim h As String = z.Substring(y - a, 1)
            If h = "@" Or h = "#" Then
                Exit While
            End If
            a = a + 1
        End While
        y -= a

        Dim retrieved = z.Substring(y, a)

        z = Replace(z, retrieved, x)
        TextBox1.Text = z
    End Sub



    Private Sub PostTrends_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles PostTrends.DoWork
        If trendDic.Count > 0 Then
            Dim toptrend As String = trendDic(0)
        End If
        trendDic.Clear()




        Dim webClient As System.Net.WebClient = New System.Net.WebClient()
        Dim result As String = webClient.DownloadString("http://api.twitter.com/1/trends/current.json")


        Dim extract As String = ""

        Dim quote As String
        quote = result.Substring(2, 1)
        extract = quote & "name" & quote & ":" & quote

        Dim k() As String

        Dim reg As Regex = New Regex(":")

        k = reg.Split(result)
        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim z As Integer = 0
        Dim gotit As Boolean = False
        Dim h As String = ""
        Dim l As Integer = 0

        For Each Match As String In k
            quote = Match.Substring(0, 1)

            If gotit Then
                l = Match.IndexOf(quote, 1)
                l -= 1

                trendDic.Add(trendDic.Count, Match.Substring(1, l))
                x += 1
                gotit = False
                Continue For
            End If

            y = Match.IndexOf("name", 0)
            z = Match.IndexOf(":", 0)

            If y + z > -1 Then
                gotit = True
            End If

            x += 1

        Next



        result = webClient.DownloadString("http://api.twitter.com/1/trends/daily.json")

        k = reg.Split(result)
        x = 0
        y = 0
        z = 0
        gotit = False
        h = ""
        l = 0

        For Each Match As String In k
            quote = Match.Substring(0, 1)

            If gotit Then
                l = Match.IndexOf(quote, 1)
                l -= 1


                h = h.Replace(Match.Substring(1, l), "")
                trendDic.Add(trendDic.Count, Match.Substring(1, l))
                x += 1
                gotit = False
                Continue For
            End If

            y = Match.IndexOf("name", 0)
            z = Match.IndexOf(":", 0)

            If y + z > -1 Then
                gotit = True
            End If

            x += 1

        Next





        Try
            ' make a Web request
            Dim req As System.Net.WebRequest = System.Net.WebRequest.Create("http://trendsmap.com/local/ke/nairobi")
            Dim resp As System.Net.WebResponse = req.GetResponse
            Str = resp.GetResponseStream
            srRead = New System.IO.StreamReader(Str)
            ' read all the text 
            Dim s As String = srRead.ReadToEnd
            Dim x0 As Integer = s.IndexOf("Weekly Top Nairobi Tags</h5>", 0)

            s = s.Substring(x0, 1150)


            Dim v As Regex = New Regex("title='#")
            Dim substrings() As String = v.Split(s)


            s = ""
            For Each Match As String In substrings
                s += Match & " "
            Next


            Dim y0 As Regex = New Regex(">#")
            substrings = y0.Split(s)

            s = ""


            For Each Match As String In substrings
                Dim p As Integer = Match.IndexOf("<", 0)
                If p > -1 Then
                    NairobiTrends.Add(NairobiTrends.Count, Match.Substring(0, p))
                End If


            Next



        Catch ex As Exception

        Finally
            '  Close Stream and StreamReader when done
            srRead.Close()
            Str.Close()
        End Try




    End Sub

    Private Sub PostTrends_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles PostTrends.RunWorkerCompleted
        If CallRefreshNAtive Then

            If trendDic.Count > 10 Then
                Trends.ListBox4.Items.Clear()
            End If

            Dim x As Integer = 0
            For Each element In trendDic
                If x < 10 Then
                    Trends.ListBox2.Items.Add(element.Value)
                    x += 1

                ElseIf x > 9 And x < 20 Then
                    Trends.ListBox3.Items.Add(element.Value)
                    x += 1

                End If

            Next

            For Each element In NairobiTrends
                If element.Key = 0 Then
                Else
                    Trends.ListBox4.Items.Add(element.Value)
                End If
            Next

            CallRefreshNAtive = False
        End If

        TrendsB.Visible = True

        'checkUpdate()xoxo
    End Sub

    Private Sub Push_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Push.DoWork
        PushMentions()
        If Singletick = 1 Then
            PushDM()
        End If

    End Sub

    Sub PushMentions()

        LatestMention = ""

        Bored.Clear()

        Dim mytext As New RichTextBox

        Try
            Dim tw As New TwitterVB2.TwitterAPI
            Dim Username As String = ""
            Dim UpdatedTweet As String = ""
            Dim c As Integer
            Dim StatusID As Long = 0


            tw.AuthenticateWith(My.Settings.OAuthkey, My.Settings.OAuthSecret, My.Settings.ConsumerToken, My.Settings.ConsumerSecret)

            Dim PlaceHolder As String = " "


            Dim tps As New TwitterParameters
            tps.Add(TwitterParameterNames.Count, 2)

            For Each tweet As TwitterStatus In tw.Mentions(tps)
                StatusID = tweet.ID

                If MentionsDic.ContainsKey(StatusID) Then
                    Exit Sub
                End If
            Next


            Dim tp As New TwitterParameters
            tp.Add(TwitterParameterNames.SinceID, LastMentionID)

         


            For Each tweet As TwitterStatus In tw.Mentions(tp)



                Username = tweet.User.ScreenName

                Dim MyPictureBox As PictureBox = PictureBox1


                UpdatedTweet = tweet.Text
                Dim SearchReturn As Integer = UpdatedTweet.IndexOf("&lt;")
                If SearchReturn <> -1 Then
                    UpdatedTweet = Replace(UpdatedTweet, "&lt;", ">")
                End If
                SearchReturn = UpdatedTweet.IndexOf("&gt;")
                If SearchReturn <> -1 Then
                    UpdatedTweet = Replace(UpdatedTweet, "&gt;", ">")
                End If
                c = Len(UpdatedTweet)
                StatusID = tweet.ID

                If MentionsDic.ContainsKey(StatusID) Then Continue For

                LatestMention = Username & " " & UpdatedTweet

                MentionsDic.Add(StatusID, UpdatedTweet)
                MentionsUsersDic.Add(StatusID, Username)




                TimeOfTweet = CDate(tweet.CreatedAtLocalTime)
                TimeRange = Date.Now - TimeOfTweet

                TimeRanger()

                While Len(PlaceHolder) < 116
                    PlaceHolder += " "
                End While

                Dim profileImageUrl As String





                'Dim k As Integer = FlowLayoutPanel1.Controls.Count
                'Dim pic As PictureBox = FlowLayoutPanel1.Controls.Item(k - 2)
                'mytext = FlowLayoutPanel1.Controls.Item(k - 1)
                'mytext.Clear()

                'FlowLayoutPanel1.Controls.SetChildIndex(pic, 0)
                'FlowLayoutPanel1.Controls.SetChildIndex(mytext, 1)


                Dim CurrentDirectory As String = PWD + "\Avi0\" + Username + ".jpg"

                If Not File.Exists(CurrentDirectory) Then


                    profileImageUrl = tw.ShowUser(Username).ProfileImageUrl

                    Dim MyWebClient As New System.Net.WebClient
                    CurrentDirectory = PWD + "\Avi0\" + Username + ".jpg"

                    MyWebClient.DownloadFile(profileImageUrl, CurrentDirectory)


                End If


                mytext.Text = Username & " "
                mytext.AppendText(vbNewLine & UpdatedTweet)
                mytext.AppendText(vbNewLine & PlaceHolder & TimeRangeString)

                Dim startindex As Integer = 0



                Dim endindex As Integer = Len(Username)
                mytext.Select(0, endindex)
                mytext.SelectionColor = Color.Blue
                mytext.SelectionFont = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                startindex = mytext.Text.IndexOf(UpdatedTweet, 0)
                endindex = Len(UpdatedTweet)
                mytext.Select(startindex, endindex)
                mytext.SelectionFont = New System.Drawing.Font("verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                startindex = mytext.Text.IndexOf(PlaceHolder & TimeRangeString, 0)
                endindex = Len(PlaceHolder & TimeRangeString)
                mytext.Select(startindex, endindex)
                mytext.SelectionFont = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                Dim yoh As String = UpdatedTweet

                Dim xr As New Regex("@\w*")
                Dim matches As MatchCollection = xr.Matches(yoh)
                Dim xx As Integer = 0
                Dim f As Integer = 0
                Dim hr As String
                Dim cummulating As String = ""

                For Each Match As Match In matches
                    f = yoh.IndexOf(Match.Value)
                    f -= 1

                    If Match.Value = "@" Then Continue For


                    If f > 0 Then

                        hr = yoh.Substring(f, 1)
                        If hr = " " Then
                            cummulating += " " & Match.Value


                            If Not HashTags.ContainsKey(Match.Value) Then
                                HashTags.Add(Match.Value, HashTags.Count)
                            End If

                        End If

                    ElseIf f = -1 Then
                        cummulating += " " & Match.Value

                        If Not HashTags.ContainsKey(Match.Value) Then
                            HashTags.Add(Match.Value, HashTags.Count)
                        End If



                    End If

                    startindex = mytext.Text.IndexOf(Match.Value)
                    endindex = Len(Match.Value)
                    mytext.Select(startindex, endindex)
                    mytext.SelectionFont = New System.Drawing.Font("verdana", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    mytext.SelectionColor = Color.Blue


                Next

                'If Not MentionsUsersDic.ContainsKey(tweet.ID) Then
                'MentionsUsersDic.Add(tweet.ID, cummulating)
                'End If


                Dim xHashtags As New Regex("\x23\w*")
                Dim matchesHashtags As MatchCollection = xHashtags.Matches(yoh)
                xx = 0
                cummulating = ""
                For Each Match As Match In matchesHashtags
                    xx += 1
                    f = yoh.IndexOf(Match.Value)
                    f -= 1

                    If Match.Value = "#" Then Continue For

                    cummulating += " " & Match.Value



                    If Not HashTags.ContainsKey(Match.Value) Then
                        HashTags.Add(Match.Value, HashTags.Count)
                    End If

                    startindex = mytext.Text.IndexOf(Match.Value)
                    endindex = Len(Match.Value)
                    mytext.Select(startindex, endindex)
                    mytext.SelectionFont = New System.Drawing.Font("verdana", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    mytext.SelectionColor = Color.Blue


                Next




                'If Not HashTags.ContainsKey(cummulating) Then
                'HashTags.Add(cummulating, tweet.ID)
                'End If


                MentionQueue.Add(mytext.Rtf, Username)
                mytext.Clear()
                mytext.Text = ""
                Bored.Add(Bored.Count, StatusID)
            Next

            'For Each control As Control In FlowLayoutPanel1.Controls
            'If Control.GetType.Name = "RichTextBox" Then
            'mytext = DirectCast(Control, RichTextBox)
            'If Trim(mytext.Text) = "" Then
            'mytext.Visible = False
            ' End If
            'mytext.ReadOnly = True
            'End If
            'Next


        Catch ex As TwitterAPIException
            If IsOnline() = False Then
                TypeOfConfirmation = "Not connected to Internet"
                Confirmation.Show()
            Else

                TypeOfConfirmation = ex.Message
                Confirmation.Show()
            End If


        End Try

        Bored.Clear()


    End Sub


    Sub PushDM()

        LatestDM = ""

        Dim mytext As New RichTextBox
        Try
            Dim tw As New TwitterVB2.TwitterAPI
            Dim Username As String = ""
            Dim UpdatedTweet As String = ""
            Dim c As Integer
            Dim StatusID As Long = 0


            tw.AuthenticateWith(My.Settings.OAuthkey, My.Settings.OAuthSecret, My.Settings.ConsumerToken, My.Settings.ConsumerSecret)

            Dim PlaceHolder As String = " "

            Dim tps As New TwitterParameters
            tps.Add(TwitterParameterNames.Count, 6)




            Bored.Clear()




            For Each Message As TwitterDirectMessage In tw.DirectMessages(tps)


                Username = Message.SenderScreenName
                Dim MyPictureBox As PictureBox = PictureBox1


                UpdatedTweet = Message.Text
                Dim SearchReturn As Integer = UpdatedTweet.IndexOf("&lt;")
                If SearchReturn <> -1 Then
                    UpdatedTweet = Replace(UpdatedTweet, "&lt;", ">")
                End If
                SearchReturn = UpdatedTweet.IndexOf("&gt;")
                If SearchReturn <> -1 Then
                    UpdatedTweet = Replace(UpdatedTweet, "&gt;", ">")
                End If
                c = Len(UpdatedTweet)
                StatusID = Message.ID

                If DMDic.ContainsKey(StatusID) Then
                    Exit Sub
                End If

                LatestDM = Username & " " & UpdatedTweet

                DMDic.Add(StatusID, UpdatedTweet)
                DMUsersDic.Add(StatusID, Username)




                TimeOfTweet = CDate(Message.CreatedAtLocalTime)
                TimeRange = Date.Now - TimeOfTweet

                TimeRanger()

                While Len(PlaceHolder) < 116
                    PlaceHolder += " "
                End While




                'Dim k As Integer = FlowLayoutPanel2.Controls.Count
                'Dim pic As PictureBox = FlowLayoutPanel2.Controls.Item(k - 2)
                'mytext = FlowLayoutPanel1.Controls.Item(k - 1)
                'mytext.Clear()

                'FlowLayoutPanel2.Controls.SetChildIndex(pic, 0)
                'FlowLayoutPanel2.Controls.SetChildIndex(mytext, 1)

                Dim profileImageUrl As String

                Dim CurrentDirectory As String = PWD + "\Avi0\" + Username + ".jpg"

                If Not File.Exists(CurrentDirectory) Then




                    profileImageUrl = tw.ShowUser(Username).ProfileImageUrl

                    Dim MyWebClient As New System.Net.WebClient
                    CurrentDirectory = PWD + "\Avi0\" + Username + ".jpg"

                    MyWebClient.DownloadFile(profileImageUrl, CurrentDirectory)






                End If

                mytext.Text = Username & " "
                mytext.AppendText(vbNewLine & UpdatedTweet)
                mytext.AppendText(vbNewLine & PlaceHolder & TimeRangeString)

                Dim startindex As Integer = 0



                Dim endindex As Integer = Len(Username)
                mytext.Select(0, endindex)
                mytext.SelectionColor = Color.Blue
                mytext.SelectionFont = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                startindex = mytext.Text.IndexOf(UpdatedTweet, 0)
                endindex = Len(UpdatedTweet)
                mytext.Select(startindex, endindex)
                mytext.SelectionFont = New System.Drawing.Font("verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                startindex = mytext.Text.IndexOf(PlaceHolder & TimeRangeString, 0)
                endindex = Len(PlaceHolder & TimeRangeString)
                mytext.Select(startindex, endindex)
                mytext.SelectionFont = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                Dim yoh As String = UpdatedTweet

                Dim xr As New Regex("@\w*")
                Dim matches As MatchCollection = xr.Matches(yoh)
                Dim xx As Integer = 0
                Dim f As Integer = 0
                Dim hr As String
                Dim cummulating As String = ""

                For Each Match As Match In matches
                    f = yoh.IndexOf(Match.Value)
                    f -= 1

                    If Match.Value = "@" Then Continue For


                    If f > 0 Then

                        hr = yoh.Substring(f, 1)
                        If hr = " " Then
                            cummulating += " " & Match.Value


                            If Not HashTags.ContainsKey(Match.Value) Then
                                HashTags.Add(Match.Value, HashTags.Count)
                            End If

                        End If

                    ElseIf f = -1 Then
                        cummulating += " " & Match.Value

                        If Not HashTags.ContainsKey(Match.Value) Then
                            HashTags.Add(Match.Value, HashTags.Count)
                        End If



                    End If

                    startindex = mytext.Text.IndexOf(Match.Value)
                    endindex = Len(Match.Value)
                    mytext.Select(startindex, endindex)
                    mytext.SelectionFont = New System.Drawing.Font("verdana", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    mytext.SelectionColor = Color.Blue


                Next

                'If Not MentionsUsersDic.ContainsKey(tweet.ID) Then
                'MentionsUsersDic.Add(tweet.ID, cummulating)
                'End If


                Dim xHashtags As New Regex("\x23\w*")
                Dim matchesHashtags As MatchCollection = xHashtags.Matches(yoh)
                xx = 0
                cummulating = ""
                For Each Match As Match In matchesHashtags
                    xx += 1
                    f = yoh.IndexOf(Match.Value)
                    f -= 1

                    If Match.Value = "#" Then Continue For

                    cummulating += " " & Match.Value



                    If Not HashTags.ContainsKey(Match.Value) Then
                        HashTags.Add(Match.Value, HashTags.Count)
                    End If

                    startindex = mytext.Text.IndexOf(Match.Value)
                    endindex = Len(Match.Value)
                    mytext.Select(startindex, endindex)
                    mytext.SelectionFont = New System.Drawing.Font("verdana", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    mytext.SelectionColor = Color.Blue


                Next



                'If Not HashTags.ContainsKey(cummulating) Then
                'HashTags.Add(cummulating, tweet.ID)
                'End If

                DMQueue.Add(mytext.Rtf, Username)
                mytext.Clear()
                mytext.Text = ""
                Bored.Add(Bored.Count, StatusID)

            Next

            'For Each control As Control In FlowLayoutPanel2.Controls
            'If control.GetType.Name = "RichTextBox" Then
            '  mytext = DirectCast(control, RichTextBox)
            ' If Trim(mytext.Text) = "" Then
            '    mytext.Visible = False
            'End If
            'mytext.ReadOnly = True
            'End If
            'Next



            LastDM = Bored(0)



        Catch ex As TwitterAPIException
            If IsOnline() = False Then
                TypeOfConfirmation = "Not connceted to Internet"
                Confirmation.Show()
            Else

                TypeOfConfirmation = ex.Message
                Confirmation.Show()
            End If


        End Try


        Bored.Clear()

    End Sub

    Private Sub Push_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Push.RunWorkerCompleted

        Dim MyText As New RichTextBox
        Dim Pic As New PictureBox
        Dim Group As New GroupBox

        Dim f As Integer = 0

        If MentionQueue.Count = 0 And Singletick = 1 Then
            GetOneMention()
            Singletick = 0
        End If

        For Each element In MentionQueue




            For Each Control In FlowLayoutPanel1.Controls
                If Control.name <> "GroupBox1" Then
                    Group = Control
                End If
            Next

            For Each Control In Group.Controls
                If Control.GetType.Name = "PictureBox" Then
                    Pic = Control
                Else
                    MyText = Control
                End If
            Next

            MyText.Clear()


            MyText.Rtf = element.Key
            MyText.Visible = True



            Dim CurrentDirectory As String = PWD + "\Avi0\" + element.Value + ".jpg"
            Pic.Image = New System.Drawing.Bitmap(CurrentDirectory)

            FlowLayoutPanel1.Controls.SetChildIndex(Group, f)
            Group.Visible = True

            f += 1
        Next




        Singletick += 1

        For Each element In DMQueue

            For Each Control In FlowLayoutPanel2.Controls
                If Control.Name <> "GroupBox1" Then
                    Group = Control
                End If
            Next

            For Each Control In Group.Controls
                If Control.GetType.Name = "PictureBox" Then
                    Pic = Control
                Else
                    MyText = Control
                End If
            Next

            MyText.Clear()


            MyText.Rtf = element.Key
            MyText.Visible = True

            Dim CurrentDirectory As String = PWD + "\Avi0\" + element.Value + ".jpg"
            Pic.Image = New System.Drawing.Bitmap(CurrentDirectory)

            FlowLayoutPanel2.Controls.SetChildIndex(Group, f)

            Group.Visible = True

            f += 1

        Next

        Dim regex As Regex = New Regex("\s")

        Dim substrings() As String = regex.Split(Trim(LatestMention))
        Dim substrings2() As String = regex.Split(Trim(LatestDM))

        Dim author As String
        Dim message As String

        If LatestMention <> "" Then
            author = substrings(0)
            message = Trim(LatestMention.Replace(author, ""))


            MentionIcon.BalloonTipTitle = author
            MentionIcon.BalloonTipText = "New Mention: " & message
            MentionIcon.Visible = True
            MentionIcon.ShowBalloonTip(2)

        End If

        If LatestDM <> "" Then
            author = substrings2(0)
            message = Trim(LatestDM.Replace(author, ""))


            DMIcon.BalloonTipTitle = author
            DMIcon.BalloonTipText = message
            DMIcon.BalloonTipText = "New DM"
            DMIcon.Visible = True
            DMIcon.ShowBalloonTip(2)
        End If


        MentionQueue.Clear()
        DMQueue.Clear()

    End Sub



    Private Sub MentionIcon_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MentionIcon.MouseDoubleClick
        MentionBallon()

    End Sub

    Private Sub MentionIcon_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MentionIcon.MouseClick
        MentionBallon()

    End Sub

    Sub MentionBallon()


        MentionIcon.Visible = False
        MentionIcon.Dispose()

    End Sub

    Sub DMBallon()


        DMIcon.Visible = False

        DMIcon.Dispose()
    End Sub

    Private Sub MentionIcon_BalloonTipClicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MentionIcon.BalloonTipClicked
        MentionBallon()
    End Sub



    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Not Push.IsBusy Then
            Push.RunWorkerAsync()
        End If
    End Sub

    Private Sub PictureBox11_Click_3(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrendsB.Click
        normalizeb()
        TrendsB.BorderStyle = BorderStyle.FixedSingle
        Trends.Show()
    End Sub

    Sub DirectoryJunk()
        HashTags.Clear()

        Dim CurrentDirectory2 As String = PWD + "\dictionary.makmende"
        If (File.Exists(CurrentDirectory2)) Then
            Dim x As Integer = 0
            ' Create an instance of StreamReader to read from a file.
            ' The using statement also closes the StreamReader.
            Using sr As New StreamReader(CurrentDirectory2)
                Dim line As String
                Do
                    Dim count As Integer = HashTags.Count

                    line = sr.ReadLine()
                    If line Is Nothing Then
                        Exit Do
                    End If
                    Try
                        HashTags.Add(line, count)
                    Catch
                    End Try

                    If Not (line Is Nothing) Then
                    End If
                    x = x + 1
                Loop Until line Is Nothing
            End Using
        Else
            File.Create(CurrentDirectory2)
        End If

        File.SetAttributes(CurrentDirectory2, FileAttributes.Hidden)
        File.SetAttributes(CurrentDirectory2, FileAttributes.NotContentIndexed)

    End Sub



    Private Sub Makmende_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        Dim CurrentDirectory2 As String = PWD + "\dictionary.makmende"
        Dim x As Integer = 0
        Dim count As Integer = HashTags.Count
        If File.Exists(CurrentDirectory2) Then
            My.Computer.FileSystem.DeleteFile(CurrentDirectory2)
        End If


        If HashTags.Count = 0 Then
        Else
            For Each element In HashTags
                My.Computer.FileSystem.WriteAllText(CurrentDirectory2, element.Key & vbCrLf, True)
                x += 1
            Next

            File.SetAttributes(CurrentDirectory2, FileAttributes.Hidden)
            File.SetAttributes(CurrentDirectory2, FileAttributes.NotContentIndexed)
        End If



        Dim CurrentDirectory As String = PWD + "\Avi0"
        If Not Directory.Exists(CurrentDirectory) Then
            Directory.CreateDirectory(CurrentDirectory)
        Else
            File.SetAttributes(CurrentDirectory2, FileAttributes.Hidden)
            File.SetAttributes(CurrentDirectory2, FileAttributes.NotContentIndexed)

        End If


    End Sub


    Private Sub mentionsB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mentionsB.Click
        normalizeb()

        TweetB.Size = New System.Drawing.Point(83, 21)
        TweetB.Text = "Post Tweet"
        Label4.Visible = False
        TextBox1.Text = ""


        Status = 0

        FlowLayoutPanel1.Visible = True
        FlowLayoutPanel2.Visible = False

        FlowLayoutPanel2.Controls.Remove(GroupBox1)
        FlowLayoutPanel1.Controls.Add(GroupBox1)

        mentionsB.Visible = False

        DMB.Visible = True




        RetweetB.Visible = True
        Quote.Visible = True
        FavoriteB.Visible = True
        mentionsB.BorderStyle = BorderStyle.FixedSingle
    End Sub

    Private Sub FindB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindB.Click
        normalizeb()

        TypeOfConfirmation = "Enter Username to Find"
        Confirmation.Show()
        FindB.BorderStyle = BorderStyle.FixedSingle
    End Sub

    Private Sub LoadProfile_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles LoadProfile.DoWork
        Dim CurrentDirectory As String = PWD + "\Avi0\" + LoadUsername + ".jpg"


        Dim ProfileImageUrl As String


        If Not File.Exists(CurrentDirectory) Then

            Dim tw As New TwitterVB2.TwitterAPI
            tw.AuthenticateWith(My.Settings.OAuthkey, My.Settings.OAuthSecret, My.Settings.ConsumerToken, My.Settings.ConsumerSecret)

            ProfileImageUrl = tw.ShowUser(LoadUsername).ProfileImageUrl

            Dim MyWebClient As New System.Net.WebClient
            CurrentDirectory = PWD + "\Avi0\" + LoadUsername + ".jpg"

            MyWebClient.DownloadFile(ProfileImageUrl, CurrentDirectory)
        End If


    End Sub
































    Private Sub RichTextBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.Click
        ClickedRichTextBox0 = RichTextBox1
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox2.Click
        ClickedRichTextBox0 = RichTextBox2
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox3.Click
        ClickedRichTextBox0 = RichTextBox3
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox4.Click
        ClickedRichTextBox0 = RichTextBox4
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox5.Click
        ClickedRichTextBox0 = RichTextBox5
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox6.Click
        ClickedRichTextBox0 = RichTextBox6
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox7.Click
        ClickedRichTextBox0 = RichTextBox7
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox8.Click
        ClickedRichTextBox0 = RichTextBox8
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox9.Click
        ClickedRichTextBox0 = RichTextBox9
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox10.Click
        ClickedRichTextBox0 = RichTextBox10
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox11.Click
        ClickedRichTextBox0 = RichTextBox11
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox12.Click
        ClickedRichTextBox0 = RichTextBox12
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox13.Click
        ClickedRichTextBox0 = RichTextBox13
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox14.Click
        ClickedRichTextBox0 = RichTextBox14
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox15.Click
        ClickedRichTextBox0 = RichTextBox15
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox16.Click
        ClickedRichTextBox0 = RichTextBox16
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox17.Click
        ClickedRichTextBox0 = RichTextBox17
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox18.Click
        ClickedRichTextBox0 = RichTextBox18
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox19.Click
        ClickedRichTextBox0 = RichTextBox19
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox20.Click
        ClickedRichTextBox0 = RichTextBox20
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox21.Click
        ClickedRichTextBox0 = RichTextBox21
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox22.Click
        ClickedRichTextBox0 = RichTextBox22
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox23.Click
        ClickedRichTextBox0 = RichTextBox23
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox24.Click
        ClickedRichTextBox0 = RichTextBox24
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox25.Click
        ClickedRichTextBox0 = RichTextBox25
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox26.Click
        ClickedRichTextBox0 = RichTextBox26
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox27.Click
        ClickedRichTextBox0 = RichTextBox27
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox28.Click
        ClickedRichTextBox0 = RichTextBox28
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox29.Click
        ClickedRichTextBox0 = RichTextBox29
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox30.Click
        ClickedRichTextBox0 = RichTextBox30
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox31.Click
        ClickedRichTextBox0 = RichTextBox31
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox32.Click
        ClickedRichTextBox0 = RichTextBox32
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox33.Click
        ClickedRichTextBox0 = RichTextBox33
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox34.Click
        ClickedRichTextBox0 = RichTextBox34
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox35.Click
        ClickedRichTextBox0 = RichTextBox35
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox36.Click
        ClickedRichTextBox0 = RichTextBox36
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox37_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox37.Click
        ClickedRichTextBox0 = RichTextBox37
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox38.Click
        ClickedRichTextBox0 = RichTextBox38
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox39.Click
        ClickedRichTextBox0 = RichTextBox39
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox40_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox40.Click
        ClickedRichTextBox0 = RichTextBox40
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox41.Click
        ClickedRichTextBox0 = RichTextBox41
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox42_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox42.Click
        ClickedRichTextBox0 = RichTextBox42
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox43_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox43.Click
        ClickedRichTextBox0 = RichTextBox43
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox44_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox44.Click
        ClickedRichTextBox0 = RichTextBox44
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox45.Click
        ClickedRichTextBox0 = RichTextBox45
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox46_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox46.Click
        ClickedRichTextBox0 = RichTextBox46
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox47_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox47.Click
        ClickedRichTextBox0 = RichTextBox47
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox48_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox48.Click
        ClickedRichTextBox0 = RichTextBox48
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox49_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox49.Click
        ClickedRichTextBox0 = RichTextBox49
        ClickRichTextBox()
    End Sub

    Private Sub RichTextBox50_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox50.Click
        ClickedRichTextBox0 = RichTextBox50
        ClickRichTextBox()
    End Sub

  

   
  

  

  


    Private Sub RichTextBox1_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.MouseHover
        ClickedRichTextBox0 = RichTextBox1
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox2_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox2.MouseHover
        ClickedRichTextBox0 = RichTextBox2
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox3_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox3.MouseHover
        ClickedRichTextBox0 = RichTextBox3
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox4_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox4.MouseHover
        ClickedRichTextBox0 = RichTextBox4
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox5_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox5.MouseHover
        ClickedRichTextBox0 = RichTextBox5
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox6_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox6.MouseHover
        ClickedRichTextBox0 = RichTextBox6
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox7_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox7.MouseHover
        ClickedRichTextBox0 = RichTextBox7
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox8_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox8.MouseHover
        ClickedRichTextBox0 = RichTextBox8
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox9_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox9.MouseHover
        ClickedRichTextBox0 = RichTextBox9
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox10_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox10.MouseHover
        ClickedRichTextBox0 = RichTextBox10
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox11_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox11.MouseHover
        ClickedRichTextBox0 = RichTextBox11
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox12_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox12.MouseHover
        ClickedRichTextBox0 = RichTextBox12
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox13_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox13.MouseHover
        ClickedRichTextBox0 = RichTextBox13
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox14_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox14.MouseHover
        ClickedRichTextBox0 = RichTextBox14
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox15_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox15.MouseHover
        ClickedRichTextBox0 = RichTextBox15
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox16_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox16.MouseHover
        ClickedRichTextBox0 = RichTextBox16
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox17_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox17.MouseHover
        ClickedRichTextBox0 = RichTextBox17
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox18_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox18.MouseHover
        ClickedRichTextBox0 = RichTextBox18
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox19_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox19.MouseHover
        ClickedRichTextBox0 = RichTextBox19
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox20_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox20.MouseHover
        ClickedRichTextBox0 = RichTextBox20
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox21_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox21.MouseHover
        ClickedRichTextBox0 = RichTextBox21
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox22_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox22.MouseHover
        ClickedRichTextBox0 = RichTextBox22
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox23_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox23.MouseHover
        ClickedRichTextBox0 = RichTextBox23
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox24_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox24.MouseHover
        ClickedRichTextBox0 = RichTextBox24
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox25_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox25.MouseHover
        ClickedRichTextBox0 = RichTextBox25
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox26_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox26.MouseHover
        ClickedRichTextBox0 = RichTextBox26
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox27_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox27.MouseHover
        ClickedRichTextBox0 = RichTextBox27
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox28_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox28.MouseHover
        ClickedRichTextBox0 = RichTextBox28
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox29_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox29.MouseHover
        ClickedRichTextBox0 = RichTextBox29
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox30_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox30.MouseHover
        ClickedRichTextBox0 = RichTextBox30
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox31_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox31.MouseHover
        ClickedRichTextBox0 = RichTextBox31
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox32_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox32.MouseHover
        ClickedRichTextBox0 = RichTextBox32
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox33_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox33.MouseHover
        ClickedRichTextBox0 = RichTextBox33
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox34_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox34.MouseHover
        ClickedRichTextBox0 = RichTextBox34
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox35_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox35.MouseHover
        ClickedRichTextBox0 = RichTextBox35
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox36_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox36.MouseHover
        ClickedRichTextBox0 = RichTextBox36
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox37_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox37.MouseHover
        ClickedRichTextBox0 = RichTextBox37
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox38_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox38.MouseHover
        ClickedRichTextBox0 = RichTextBox38
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox39_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox39.MouseHover
        ClickedRichTextBox0 = RichTextBox39
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox40_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox40.MouseHover
        ClickedRichTextBox0 = RichTextBox40
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox41_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox41.MouseHover
        ClickedRichTextBox0 = RichTextBox41
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox42_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox42.MouseHover
        ClickedRichTextBox0 = RichTextBox42
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox43_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox43.MouseHover
        ClickedRichTextBox0 = RichTextBox43
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox44_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox44.MouseHover
        ClickedRichTextBox0 = RichTextBox44
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox45_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox45.MouseHover
        ClickedRichTextBox0 = RichTextBox45
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox46_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox46.MouseHover
        ClickedRichTextBox0 = RichTextBox46
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox47_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox47.MouseHover
        ClickedRichTextBox0 = RichTextBox47
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox48_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox48.MouseHover
        ClickedRichTextBox0 = RichTextBox48
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox49_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox49.MouseHover
        ClickedRichTextBox0 = RichTextBox49
        HoverRichTextBox()
    End Sub

    Private Sub RichTextBox50_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox50.MouseHover
        ClickedRichTextBox0 = RichTextBox50
        HoverRichTextBox()
    End Sub

  
 

    Public LoadedTick As Integer = 0
    Public thetext As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If LoadedTick > 0 Then
            If LoadedYet = False Then
                MsgBox("Twitter seems to be Overcapacity", vbOKOnly, "")
            Else
                Timer2.Enabled = False
            End If
        End If
        LoadedTick += 1

    End Sub
End Class
