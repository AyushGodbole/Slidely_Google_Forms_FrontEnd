Imports Newtonsoft.Json
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Net.Http
Imports System.Windows.Forms

Public Class ViewSubmissionForm
    Inherits System.Windows.Forms.Form

    Private WithEvents NextButton As Button
    Private WithEvents PrevButton As Button
    Private WithEvents deleteButton As Button
    Private WithEvents EditButton As Button
    Private NameTextBox As TextBox
    Private EmailTextBox As TextBox
    Private PhoneTextBox As TextBox
    Private GithubTextBox As TextBox
    Private StopwatchTextBox As TextBox
    Private NameLabel As Label
    Private EmailLabel As Label
    Private PhoneLabel As Label
    Private GithubLabel As Label
    Private StopwatchLabel As Label

    Private submissions As List(Of Dictionary(Of String, Object))
    Private currentIndex As Integer = 0
    Private isEditing As Boolean = False ' Track whether editing mode is active

    Public Sub New()
        InitializeComponent()
    End Sub

    ' Display submissions in the TextBoxes
    Public Sub DisplaySubmissions(json As String)
        Try
            submissions = JsonConvert.DeserializeObject(Of List(Of Dictionary(Of String, Object)))(json)
            currentIndex = 0
            UpdateSubmission()
        Catch ex As Exception
            MessageBox.Show($"Error deserializing JSON: {ex.Message}")
        End Try
    End Sub

    Private Sub UpdateSubmission()
        If submissions IsNot Nothing AndAlso submissions.Count > 0 Then
            Dim submission = submissions(currentIndex)
            NameTextBox.Text = submission("name").ToString()
            EmailTextBox.Text = submission("email").ToString()
            PhoneTextBox.Text = submission("phone").ToString()
            GithubTextBox.Text = submission("githubLink").ToString()
            If submission.ContainsKey("stopwatchTime") Then
                StopwatchTextBox.Text = submission("stopwatchTime").ToString()
            Else
                StopwatchTextBox.Text = ""
            End If

            ' Reset editing state and button appearance when changing submission
            isEditing = False
            ResetEditButton()
        End If
    End Sub

    Private Sub MoveToNextSubmission()
        If submissions IsNot Nothing AndAlso currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            UpdateSubmission()
        End If
    End Sub

    Private Sub MoveToPreviousSubmission()
        If submissions IsNot Nothing AndAlso currentIndex > 0 Then
            currentIndex -= 1
            UpdateSubmission()
        End If
    End Sub

    Private Sub ToggleEditing()
        ' Toggle editing mode
        isEditing = Not isEditing

        If isEditing Then
            EnableEditing()
        Else
            DisableEditing()
        End If
    End Sub

    Private Sub EnableEditing()
        ' Enable editing for specific TextBox controls
        NameTextBox.ReadOnly = False
        NameTextBox.Enabled = True
        NameTextBox.BackColor = Color.White

        EmailTextBox.ReadOnly = False
        EmailTextBox.Enabled = True
        EmailTextBox.BackColor = Color.White

        PhoneTextBox.ReadOnly = False
        PhoneTextBox.Enabled = True
        PhoneTextBox.BackColor = Color.White

        GithubTextBox.ReadOnly = False
        GithubTextBox.Enabled = True
        GithubTextBox.BackColor = Color.White

        StopwatchTextBox.ReadOnly = False
        StopwatchTextBox.Enabled = True
        StopwatchTextBox.BackColor = Color.White

        ' Change EditButton appearance to indicate saving
        EditButton.BackColor = Color.PaleGreen
        EditButton.Text = "SAVE (CTRL + S)"
    End Sub

    Private Sub DisableEditing()
        ' Save changes (if any) and disable editing controls
        SaveChanges()

        ' Disable editing for all TextBox controls
        NameTextBox.ReadOnly = True
        NameTextBox.Enabled = False
        NameTextBox.BackColor = SystemColors.Control

        EmailTextBox.ReadOnly = True
        EmailTextBox.Enabled = False
        EmailTextBox.BackColor = SystemColors.Control

        PhoneTextBox.ReadOnly = True
        PhoneTextBox.Enabled = False
        PhoneTextBox.BackColor = SystemColors.Control

        GithubTextBox.ReadOnly = True
        GithubTextBox.Enabled = False
        GithubTextBox.BackColor = SystemColors.Control

        StopwatchTextBox.ReadOnly = True
        StopwatchTextBox.Enabled = False
        StopwatchTextBox.BackColor = SystemColors.Control

        ' Change EditButton appearance back to normal editing mode
        EditButton.BackColor = Color.Lime
        EditButton.Text = "EDIT (CTRL + E)"
    End Sub

    Private Sub SaveChanges()
        ' Save changes to the submission details
        If submissions IsNot Nothing AndAlso currentIndex < submissions.Count Then
            Dim submission = submissions(currentIndex)
            submission("name") = NameTextBox.Text
            submission("email") = EmailTextBox.Text
            submission("phone") = PhoneTextBox.Text
            submission("githubLink") = GithubTextBox.Text

            ' Update UI if needed
            UpdateSubmission()

            ' Send PUT request to update data on the backend
            Try
                Dim id As String = submission("id").ToString() ' Get the id from the submission
                Dim httpClient As New HttpClient()
                Dim jsonContent = JsonConvert.SerializeObject(submission)
                Dim httpContent As New StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json")
                Dim response = httpClient.PutAsync($"http://localhost:1337/api/edit/{id}", httpContent).Result

                If response.IsSuccessStatusCode Then
                    ' Only show message box on success
                    MessageBox.Show("Submission updated successfully.")
                Else
                    MessageBox.Show($"Error updating submission: {response.StatusCode}")
                End If

            Catch ex As Exception
                MessageBox.Show($"Error updating submission: {ex.Message}")
            End Try
        End If
    End Sub

    Private Sub ResetEditButton()
        ' Reset the EditButton appearance to default (Edit mode)
        EditButton.BackColor = Color.Lime
        EditButton.Text = "EDIT (CTRL + E)"
    End Sub

    Private Sub DeleteSubmission()
        ' Delete the current submission
        If submissions IsNot Nothing AndAlso currentIndex < submissions.Count Then
            Dim submission = submissions(currentIndex)
            Dim id As String = submission("id").ToString()

            Try
                Dim httpClient As New HttpClient()
                Dim response = httpClient.DeleteAsync($"http://localhost:1337/api/delete/{id}").Result

                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Submission deleted successfully.")

                    ' Remove the submission from the list
                    submissions.RemoveAt(currentIndex)

                    ' Check if there are any submissions left
                    If submissions.Count = 0 Then
                        ' Close the form when no submissions are left
                        Me.Close()
                    Else
                        ' Update UI after deletion
                        If currentIndex >= submissions.Count Then
                            currentIndex = submissions.Count - 1
                        End If
                        UpdateSubmission()
                    End If
                Else
                    MessageBox.Show($"Error deleting submission: {response.StatusCode}")
                End If

            Catch ex As Exception
                MessageBox.Show($"Error deleting submission: {ex.Message}")
            End Try
        End If
    End Sub


    Private Sub InitializeComponent()
        Me.NameTextBox = New System.Windows.Forms.TextBox()
        Me.EmailTextBox = New System.Windows.Forms.TextBox()
        Me.PhoneTextBox = New System.Windows.Forms.TextBox()
        Me.GithubTextBox = New System.Windows.Forms.TextBox()
        Me.StopwatchTextBox = New System.Windows.Forms.TextBox()
        Me.NextButton = New System.Windows.Forms.Button()
        Me.PrevButton = New System.Windows.Forms.Button()
        Me.deleteButton = New System.Windows.Forms.Button()
        Me.EditButton = New System.Windows.Forms.Button()
        Me.NameLabel = New System.Windows.Forms.Label()
        Me.EmailLabel = New System.Windows.Forms.Label()
        Me.PhoneLabel = New System.Windows.Forms.Label()
        Me.GithubLabel = New System.Windows.Forms.Label()
        Me.StopwatchLabel = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'NameTextBox
        '
        Me.NameTextBox.BackColor = System.Drawing.Color.LightGray
        Me.NameTextBox.Enabled = False
        Me.NameTextBox.Location = New System.Drawing.Point(145, 50)
        Me.NameTextBox.Name = "NameTextBox"
        Me.NameTextBox.ReadOnly = True
        Me.NameTextBox.Size = New System.Drawing.Size(231, 22)
        Me.NameTextBox.TabIndex = 0
        Me.NameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'EmailTextBox
        '
        Me.EmailTextBox.BackColor = System.Drawing.Color.LightGray
        Me.EmailTextBox.Enabled = False
        Me.EmailTextBox.Location = New System.Drawing.Point(145, 78)
        Me.EmailTextBox.Name = "EmailTextBox"
        Me.EmailTextBox.ReadOnly = True
        Me.EmailTextBox.Size = New System.Drawing.Size(231, 22)
        Me.EmailTextBox.TabIndex = 1
        Me.EmailTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PhoneTextBox
        '
        Me.PhoneTextBox.BackColor = System.Drawing.Color.LightGray
        Me.PhoneTextBox.Enabled = False
        Me.PhoneTextBox.Location = New System.Drawing.Point(145, 110)
        Me.PhoneTextBox.Name = "PhoneTextBox"
        Me.PhoneTextBox.ReadOnly = True
        Me.PhoneTextBox.Size = New System.Drawing.Size(231, 22)
        Me.PhoneTextBox.TabIndex = 2
        Me.PhoneTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GithubTextBox
        '
        Me.GithubTextBox.BackColor = System.Drawing.Color.LightGray
        Me.GithubTextBox.Enabled = False
        Me.GithubTextBox.Location = New System.Drawing.Point(145, 142)
        Me.GithubTextBox.Name = "GithubTextBox"
        Me.GithubTextBox.ReadOnly = True
        Me.GithubTextBox.Size = New System.Drawing.Size(231, 22)
        Me.GithubTextBox.TabIndex = 3
        Me.GithubTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'StopwatchTextBox
        '
        Me.StopwatchTextBox.BackColor = System.Drawing.Color.LightGray
        Me.StopwatchTextBox.Enabled = False
        Me.StopwatchTextBox.Location = New System.Drawing.Point(145, 174)
        Me.StopwatchTextBox.Name = "StopwatchTextBox"
        Me.StopwatchTextBox.ReadOnly = True
        Me.StopwatchTextBox.Size = New System.Drawing.Size(231, 22)
        Me.StopwatchTextBox.TabIndex = 4
        Me.StopwatchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'NextButton
        '
        Me.NextButton.BackColor = System.Drawing.Color.DodgerBlue
        Me.NextButton.FlatAppearance.BorderSize = 0
        Me.NextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.NextButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.NextButton.ForeColor = System.Drawing.Color.Black
        Me.NextButton.Location = New System.Drawing.Point(221, 253)
        Me.NextButton.Name = "NextButton"
        Me.NextButton.Size = New System.Drawing.Size(167, 35)
        Me.NextButton.TabIndex = 5
        Me.NextButton.Text = "NEXT (CTRL + N)"
        Me.NextButton.UseVisualStyleBackColor = False
        '
        'PrevButton
        '
        Me.PrevButton.BackColor = System.Drawing.Color.Yellow
        Me.PrevButton.FlatAppearance.BorderSize = 0
        Me.PrevButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PrevButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.PrevButton.ForeColor = System.Drawing.Color.Black
        Me.PrevButton.Location = New System.Drawing.Point(12, 253)
        Me.PrevButton.Name = "PrevButton"
        Me.PrevButton.Size = New System.Drawing.Size(192, 35)
        Me.PrevButton.TabIndex = 6
        Me.PrevButton.Text = "PREVIOUS (CTRL + P)"
        Me.PrevButton.UseVisualStyleBackColor = False
        '
        'deleteButton
        '
        Me.deleteButton.BackColor = System.Drawing.Color.Red
        Me.deleteButton.FlatAppearance.BorderSize = 0
        Me.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.deleteButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.deleteButton.Location = New System.Drawing.Point(230, 213)
        Me.deleteButton.Name = "deleteButton"
        Me.deleteButton.Size = New System.Drawing.Size(158, 35)
        Me.deleteButton.TabIndex = 7
        Me.deleteButton.Text = "DELETE (CTRL + D)"
        Me.deleteButton.UseVisualStyleBackColor = False
        '
        'EditButton
        '
        Me.EditButton.BackColor = System.Drawing.Color.Lime
        Me.EditButton.FlatAppearance.BorderSize = 0
        Me.EditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EditButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.EditButton.ForeColor = System.Drawing.Color.Black
        Me.EditButton.Location = New System.Drawing.Point(15, 212)
        Me.EditButton.Name = "EditButton"
        Me.EditButton.Size = New System.Drawing.Size(167, 35)
        Me.EditButton.TabIndex = 8
        Me.EditButton.Text = "EDIT (CTRL + E)"
        Me.EditButton.UseVisualStyleBackColor = False
        '
        'NameLabel
        '
        Me.NameLabel.AutoSize = True
        Me.NameLabel.Location = New System.Drawing.Point(12, 53)
        Me.NameLabel.Name = "NameLabel"
        Me.NameLabel.Size = New System.Drawing.Size(44, 16)
        Me.NameLabel.TabIndex = 9
        Me.NameLabel.Text = "Name"
        '
        'EmailLabel
        '
        Me.EmailLabel.AutoSize = True
        Me.EmailLabel.Location = New System.Drawing.Point(12, 81)
        Me.EmailLabel.Name = "EmailLabel"
        Me.EmailLabel.Size = New System.Drawing.Size(41, 16)
        Me.EmailLabel.TabIndex = 10
        Me.EmailLabel.Text = "Email"
        '
        'PhoneLabel
        '
        Me.PhoneLabel.AutoSize = True
        Me.PhoneLabel.Location = New System.Drawing.Point(12, 113)
        Me.PhoneLabel.Name = "PhoneLabel"
        Me.PhoneLabel.Size = New System.Drawing.Size(46, 16)
        Me.PhoneLabel.TabIndex = 11
        Me.PhoneLabel.Text = "Phone"
        '
        'GithubLabel
        '
        Me.GithubLabel.AutoSize = True
        Me.GithubLabel.Location = New System.Drawing.Point(12, 145)
        Me.GithubLabel.Name = "GithubLabel"
        Me.GithubLabel.Size = New System.Drawing.Size(45, 16)
        Me.GithubLabel.TabIndex = 12
        Me.GithubLabel.Text = "Github"
        '
        'StopwatchLabel
        '
        Me.StopwatchLabel.AutoSize = True
        Me.StopwatchLabel.Location = New System.Drawing.Point(12, 177)
        Me.StopwatchLabel.Name = "StopwatchLabel"
        Me.StopwatchLabel.Size = New System.Drawing.Size(69, 16)
        Me.StopwatchLabel.TabIndex = 13
        Me.StopwatchLabel.Text = "Stopwatch"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Label1.Location = New System.Drawing.Point(34, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(326, 17)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Ayush Godbole, Slidely Task 2 - View Submissions"
        '
        'ViewSubmissionForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 300)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.StopwatchLabel)
        Me.Controls.Add(Me.GithubLabel)
        Me.Controls.Add(Me.PhoneLabel)
        Me.Controls.Add(Me.EmailLabel)
        Me.Controls.Add(Me.NameLabel)
        Me.Controls.Add(Me.deleteButton)
        Me.Controls.Add(Me.PrevButton)
        Me.Controls.Add(Me.NextButton)
        Me.Controls.Add(Me.StopwatchTextBox)
        Me.Controls.Add(Me.GithubTextBox)
        Me.Controls.Add(Me.PhoneTextBox)
        Me.Controls.Add(Me.EmailTextBox)
        Me.Controls.Add(Me.NameTextBox)
        Me.Controls.Add(Me.EditButton)
        Me.Name = "ViewSubmissionForm"
        Me.Text = "View Submissions"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub


    Private Sub NextButton_Click(sender As Object, e As EventArgs) Handles NextButton.Click
        MoveToNextSubmission()
    End Sub

    Private Sub PrevButton_Click(sender As Object, e As EventArgs) Handles PrevButton.Click
        MoveToPreviousSubmission()
    End Sub

    Private Sub EditButton_Click(sender As Object, e As EventArgs) Handles EditButton.Click
        ToggleEditing()
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles deleteButton.Click
        DeleteSubmission()
    End Sub

    ' Key handling for Ctrl + key shortcuts
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Control Or Keys.N
                MoveToNextSubmission()
                Return True
            Case Keys.Control Or Keys.P
                MoveToPreviousSubmission()
                Return True
            Case Keys.Control Or Keys.E
                ToggleEditing()
                Return True
            Case Keys.Control Or Keys.S
                If isEditing Then
                    ToggleEditing()
                    Return True
                End If
                Return True
            Case Keys.Control Or Keys.D
                DeleteSubmission()
                Return True
            Case Else
                Return MyBase.ProcessCmdKey(msg, keyData)
        End Select
    End Function

    Friend WithEvents Label1 As Label
End Class
