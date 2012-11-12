Imports TwitterVB2
Imports System.Text.RegularExpressions
Imports System.IO

Public Class Introduction

    Public Token As String
    Public TokenSecret As String
    Public seconds As Integer = 0
    Public PWD As String = My.Computer.FileSystem.CurrentDirectory

    Private Sub Introduction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



        If IsOnline() = False Then
            MsgBox("Check your internet conncetion", vbOKOnly, "App closing")
            Me.Close()
            Exit Sub
        End If


       
        

           

            Dim CurrentDirectory2 As String = PWD + "\dictionary.makmende"
            If (File.Exists(CurrentDirectory2)) Then
                Dim x As Integer = 0
                ' Create an instance of StreamReader to read from a file.
                ' The using statement also closes the StreamReader.
                Using sr As New StreamReader(CurrentDirectory2)
                    Dim line As String
                    Do
                    Dim count As Integer = Makmende.HashTags.Count

                        line = sr.ReadLine()
                        If line Is Nothing Then
                            Exit Do
                        End If
                        Try
                        Makmende.HashTags.Add(line, count)
                        Catch
                        End Try

                        If Not (line Is Nothing) Then
                        End If
                        x = x + 1
                    Loop Until line Is Nothing
                End Using
            End If

        Timer1.Enabled = True
        Timer1.Interval = 1000



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

    

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        seconds += 1

        If seconds = 3 And Trim(My.Settings.OAuthSecret) <> "" Then

            Makmende.Show()
            Me.Hide()

        Else


            If seconds = 3 Then
                Code.Show()
                Me.Hide()
            End If


        End If

    End Sub
End Class