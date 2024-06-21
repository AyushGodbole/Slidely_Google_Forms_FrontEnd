Imports System.Net.Http
Imports System.Text
Imports System.Threading.Tasks

Public Class CreateSubmission
    Inherits System.Windows.Forms.Form

    Private ReadOnly client As HttpClient
    Private ReadOnly stopwatch As System.Diagnostics.Stopwatch
    Private ReadOnly timer As System.Windows.Forms.Timer

    ' Constructor
    Public Sub New(httpClient As HttpClient)
        ' Initialize HttpClient with the provided instance
        client = httpClient

        ' Initialize Stopwatch
        stopwatch = New System.Diagnostics.Stopwatch()

        ' Initialize Timer
        timer = New System.Windows.Forms.Timer()
        timer.Interval = 1000 ' Set the timer interval to 1 second
        AddHandler timer.Tick, AddressOf Timer_Tick

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add event handlers
        AddHandler ToggleStopwatchButton.Click, AddressOf ToggleStopwatchButton_Click
        AddHandler SubmitButton.Click, AddressOf SubmitButton_Click

        ' Add keyboard shortcuts
        Me.KeyPreview = True ' Enable key preview for form
        AddHandler Me.KeyDown, AddressOf CreateSubmission_KeyDown

        ' Add FormClosing event handler
        AddHandler Me.FormClosing, AddressOf CreateSubmission_FormClosing
    End Sub

    Public Sub StartStopwatch()
        ' Start the stopwatch and timer if not already running
        If Not stopwatch.IsRunning Then
            stopwatch.Start()
            timer.Start()
        End If
    End Sub

    Private Sub ToggleStopwatchButton_Click(sender As Object, e As EventArgs)
        If stopwatch.IsRunning Then
            stopwatch.Stop()
            timer.Stop()
        Else
            stopwatch.Start()
            timer.Start()
        End If
        UpdateStopwatchLabel()
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        UpdateStopwatchLabel()
    End Sub

    Private Sub UpdateStopwatchLabel()
        StopwatchLabel.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    Private Async Sub SubmitButton_Click(sender As Object, e As EventArgs)
        ' Check if all fields are filled
        If Not ValidateForm() Then
            MessageBox.Show("Please fill in all fields before submitting.")
            Return
        End If

        ' Stop the stopwatch
        stopwatch.Stop()
        timer.Stop()
        UpdateStopwatchLabel()

        ' Retrieve form data
        Dim name As String = NameTextBox.Text
        Dim email As String = EmailTextBox.Text
        Dim phone As String = PhoneTextBox.Text
        Dim githubLink As String = GithubTextBox.Text
        Dim stopwatchTime As String = stopwatch.Elapsed.ToString("hh\:mm\:ss")
        Dim uniqueId As String = Guid.NewGuid().ToString()

        ' Post data to backend asynchronously
        Dim response As String = Await PostBackendDataAsync(uniqueId, name, email, phone, githubLink, stopwatchTime)
        MessageBox.Show(response)

        ' Reset form fields
        ResetForm()

        ' Close the form after successful submission
        If Not response.StartsWith("Request error:") Then
            Me.Close()
        End If
    End Sub

    Private Function ValidateForm() As Boolean
        ' Validate that all fields are filled
        If String.IsNullOrWhiteSpace(NameTextBox.Text) Then
            Return False
        End If

        If String.IsNullOrWhiteSpace(EmailTextBox.Text) Then
            Return False
        End If

        If String.IsNullOrWhiteSpace(PhoneTextBox.Text) Then
            Return False
        End If

        If String.IsNullOrWhiteSpace(GithubTextBox.Text) Then
            Return False
        End If

        ' All fields are filled
        Return True
    End Function


    Private Async Function PostBackendDataAsync(id As String, name As String, email As String, phone As String, githubLink As String, stopwatchTime As String) As Task(Of String)
        Try
            ' Create data object
            Dim data = New With {
                Key .id = id,
                Key .name = name,
                Key .email = email,
                Key .phone = phone,
                Key .githubLink = githubLink,
                Key .stopwatchTime = stopwatchTime
            }

            ' Serialize to JSON
            Dim json = Newtonsoft.Json.JsonConvert.SerializeObject(data)

            ' Create StringContent with JSON payload
            Dim content As New StringContent(json, Encoding.UTF8, "application/json")

            ' Post data to API endpoint
            Dim response As HttpResponseMessage = Await client.PostAsync("http://localhost:1337/api/submit", content)
            response.EnsureSuccessStatusCode()

            ' Read response content
            Dim responseBody As String = Await response.Content.ReadAsStringAsync()

            Return responseBody
        Catch ex As Exception
            Return $"Request error: {ex.Message}"
        End Try
    End Function

    Private Sub CreateSubmission_KeyDown(sender As Object, e As KeyEventArgs)
        ' Handle keyboard shortcuts
        If e.Control AndAlso e.KeyCode = Keys.S Then
            ' Ctrl+S pressed, simulate click on SubmitButton
            SubmitButton.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.T Then
            ' Ctrl+T pressed, simulate click on ToggleStopwatchButton
            ToggleStopwatchButton.PerformClick()
        End If
    End Sub

    Private Sub CreateSubmission_FormClosing(sender As Object, e As FormClosingEventArgs)
        ' Reset form fields when form is closing
        ResetForm()
    End Sub

    Private Sub ResetForm()
        ' Reset all text boxes and stopwatch
        NameTextBox.Text = ""
        EmailTextBox.Text = ""
        PhoneTextBox.Text = ""
        GithubTextBox.Text = ""
        stopwatch.Reset()
        timer.Stop()
        StopwatchLabel.Text = "00:00:00"
    End Sub
End Class
