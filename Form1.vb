Imports System.Net.Http
Imports System.Threading.Tasks

Public Class Form1
    Inherits System.Windows.Forms.Form

    Private ReadOnly client As HttpClient
    Private createSubmissionForm As CreateSubmission
    Private viewSubmissionForm As ViewSubmissionForm
    Private submissionsData As String ' Store fetched data here

    ' Constructor
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Initialize HttpClient
        client = New HttpClient()

        ' Initialize CreateSubmissionForm with HttpClient instance
        createSubmissionForm = New CreateSubmission(client)
        AddHandler createSubmissionForm.Shown, AddressOf CreateSubmissionForm_Shown ' Add event handler for Shown event

        ' Initialize ViewSubmissionForm
        viewSubmissionForm = New ViewSubmissionForm()
    End Sub

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Fetch data from backend and store it
        submissionsData = Await GetBackendDataAsync()

        ' Check if there was an error
        If submissionsData.StartsWith("Request error:") Then
            MessageBox.Show(submissionsData, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf String.IsNullOrEmpty(submissionsData.Trim()) Then
            MessageBox.Show("No submissions yet!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            ' Display data in ViewSubmissionForm
            viewSubmissionForm.DisplaySubmissions(submissionsData)
            viewSubmissionForm.ShowDialog()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Open CreateSubmission form
        createSubmissionForm.ShowDialog()
    End Sub

    Private Async Function GetBackendDataAsync() As Task(Of String)
        Try
            Dim response As HttpResponseMessage = Await client.GetAsync("http://localhost:1337/api/details")
            response.EnsureSuccessStatusCode()
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            ' Check if response is an empty array
            If responseBody.Trim() = "[]" Then
                Return String.Empty
            Else
                Return responseBody
            End If
        Catch ex As Exception
            Return $"Request error: {ex.Message}"
        End Try
    End Function

    Private Sub CreateSubmissionForm_Shown(sender As Object, e As EventArgs)
        ' Start the stopwatch and timer when CreateSubmission form is shown
        createSubmissionForm.StartStopwatch()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.V) Then
            Button1.PerformClick() ' View Submissions
            Return True
        ElseIf keyData = (Keys.Control Or Keys.N) Then
            Button2.PerformClick() ' Create New Submission
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class
