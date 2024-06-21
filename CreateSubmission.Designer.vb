<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CreateSubmission
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.NameLabel = New System.Windows.Forms.Label()
        Me.EmailLabel = New System.Windows.Forms.Label()
        Me.PhoneLabel = New System.Windows.Forms.Label()
        Me.GithubLabel = New System.Windows.Forms.Label()
        Me.NameTextBox = New System.Windows.Forms.TextBox()
        Me.EmailTextBox = New System.Windows.Forms.TextBox()
        Me.PhoneTextBox = New System.Windows.Forms.TextBox()
        Me.GithubTextBox = New System.Windows.Forms.TextBox()
        Me.ToggleStopwatchButton = New System.Windows.Forms.Button()
        Me.SubmitButton = New System.Windows.Forms.Button()
        Me.StopwatchLabel = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'NameLabel
        '
        Me.NameLabel.AutoSize = True
        Me.NameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.NameLabel.Location = New System.Drawing.Point(72, 43)
        Me.NameLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.NameLabel.Name = "NameLabel"
        Me.NameLabel.Size = New System.Drawing.Size(48, 18)
        Me.NameLabel.TabIndex = 0
        Me.NameLabel.Text = "Name"
        '
        'EmailLabel
        '
        Me.EmailLabel.AutoSize = True
        Me.EmailLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.EmailLabel.Location = New System.Drawing.Point(72, 73)
        Me.EmailLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.EmailLabel.Name = "EmailLabel"
        Me.EmailLabel.Size = New System.Drawing.Size(45, 18)
        Me.EmailLabel.TabIndex = 1
        Me.EmailLabel.Text = "Email"
        '
        'PhoneLabel
        '
        Me.PhoneLabel.AutoSize = True
        Me.PhoneLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.PhoneLabel.Location = New System.Drawing.Point(72, 103)
        Me.PhoneLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.PhoneLabel.Name = "PhoneLabel"
        Me.PhoneLabel.Size = New System.Drawing.Size(51, 18)
        Me.PhoneLabel.TabIndex = 2
        Me.PhoneLabel.Text = "Phone"
        '
        'GithubLabel
        '
        Me.GithubLabel.AutoSize = True
        Me.GithubLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GithubLabel.Location = New System.Drawing.Point(72, 133)
        Me.GithubLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.GithubLabel.Name = "GithubLabel"
        Me.GithubLabel.Size = New System.Drawing.Size(82, 18)
        Me.GithubLabel.TabIndex = 3
        Me.GithubLabel.Text = "Github Link"
        Me.GithubLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'NameTextBox
        '
        Me.NameTextBox.Location = New System.Drawing.Point(221, 39)
        Me.NameTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.NameTextBox.Name = "NameTextBox"
        Me.NameTextBox.Size = New System.Drawing.Size(265, 22)
        Me.NameTextBox.TabIndex = 4
        '
        'EmailTextBox
        '
        Me.EmailTextBox.Location = New System.Drawing.Point(221, 69)
        Me.EmailTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.EmailTextBox.Name = "EmailTextBox"
        Me.EmailTextBox.Size = New System.Drawing.Size(265, 22)
        Me.EmailTextBox.TabIndex = 5
        '
        'PhoneTextBox
        '
        Me.PhoneTextBox.Location = New System.Drawing.Point(221, 99)
        Me.PhoneTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.PhoneTextBox.Name = "PhoneTextBox"
        Me.PhoneTextBox.Size = New System.Drawing.Size(265, 22)
        Me.PhoneTextBox.TabIndex = 6
        '
        'GithubTextBox
        '
        Me.GithubTextBox.Location = New System.Drawing.Point(221, 129)
        Me.GithubTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.GithubTextBox.Name = "GithubTextBox"
        Me.GithubTextBox.Size = New System.Drawing.Size(265, 22)
        Me.GithubTextBox.TabIndex = 7
        '
        'ToggleStopwatchButton
        '
        Me.ToggleStopwatchButton.BackColor = System.Drawing.Color.Yellow
        Me.ToggleStopwatchButton.FlatAppearance.BorderSize = 0
        Me.ToggleStopwatchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ToggleStopwatchButton.Location = New System.Drawing.Point(13, 159)
        Me.ToggleStopwatchButton.Margin = New System.Windows.Forms.Padding(4)
        Me.ToggleStopwatchButton.Name = "ToggleStopwatchButton"
        Me.ToggleStopwatchButton.Size = New System.Drawing.Size(258, 27)
        Me.ToggleStopwatchButton.TabIndex = 8
        Me.ToggleStopwatchButton.Text = "TOGGLE STOPWATCH (CTRL + T)"
        Me.ToggleStopwatchButton.UseVisualStyleBackColor = False
        '
        'SubmitButton
        '
        Me.SubmitButton.BackColor = System.Drawing.Color.DodgerBlue
        Me.SubmitButton.FlatAppearance.BorderSize = 0
        Me.SubmitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SubmitButton.Location = New System.Drawing.Point(202, 189)
        Me.SubmitButton.Margin = New System.Windows.Forms.Padding(4)
        Me.SubmitButton.Name = "SubmitButton"
        Me.SubmitButton.Size = New System.Drawing.Size(156, 31)
        Me.SubmitButton.TabIndex = 9
        Me.SubmitButton.Text = "SUBMIT (CTRL + S)"
        Me.SubmitButton.UseVisualStyleBackColor = False
        '
        'StopwatchLabel
        '
        Me.StopwatchLabel.BackColor = System.Drawing.Color.Gray
        Me.StopwatchLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.StopwatchLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.StopwatchLabel.ForeColor = System.Drawing.Color.White
        Me.StopwatchLabel.Location = New System.Drawing.Point(282, 159)
        Me.StopwatchLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.StopwatchLabel.Name = "StopwatchLabel"
        Me.StopwatchLabel.Size = New System.Drawing.Size(204, 27)
        Me.StopwatchLabel.TabIndex = 10
        Me.StopwatchLabel.Text = "00:00:00"
        Me.StopwatchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Label1.Location = New System.Drawing.Point(95, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(329, 17)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Ayush Godbole, Slidey Task 2 - Create Submission"
        '
        'CreateSubmission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(512, 223)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.StopwatchLabel)
        Me.Controls.Add(Me.SubmitButton)
        Me.Controls.Add(Me.ToggleStopwatchButton)
        Me.Controls.Add(Me.GithubTextBox)
        Me.Controls.Add(Me.PhoneTextBox)
        Me.Controls.Add(Me.EmailTextBox)
        Me.Controls.Add(Me.NameTextBox)
        Me.Controls.Add(Me.GithubLabel)
        Me.Controls.Add(Me.PhoneLabel)
        Me.Controls.Add(Me.EmailLabel)
        Me.Controls.Add(Me.NameLabel)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "CreateSubmission"
        Me.Text = "Create Submission"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents NameLabel As Label
    Friend WithEvents EmailLabel As Label
    Friend WithEvents PhoneLabel As Label
    Friend WithEvents GithubLabel As Label
    Friend WithEvents NameTextBox As TextBox
    Friend WithEvents EmailTextBox As TextBox
    Friend WithEvents PhoneTextBox As TextBox
    Friend WithEvents GithubTextBox As TextBox
    Friend WithEvents ToggleStopwatchButton As Button
    Friend WithEvents SubmitButton As Button
    Friend WithEvents StopwatchLabel As Label
    Friend WithEvents Label1 As Label
End Class
