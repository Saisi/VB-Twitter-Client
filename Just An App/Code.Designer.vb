<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Code
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Code))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TweetB = New System.Windows.Forms.Button()
        Me.IgnoreB = New System.Windows.Forms.Button()
        Me.RetryB = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(155, 19)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Do you have the code?"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(4, 41)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(91, 27)
        Me.TextBox1.TabIndex = 2
        Me.TextBox1.Visible = False
        '
        'TweetB
        '
        Me.TweetB.BackgroundImage = CType(resources.GetObject("TweetB.BackgroundImage"), System.Drawing.Image)
        Me.TweetB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.TweetB.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TweetB.Location = New System.Drawing.Point(161, 59)
        Me.TweetB.Name = "TweetB"
        Me.TweetB.Size = New System.Drawing.Size(103, 43)
        Me.TweetB.TabIndex = 98
        Me.TweetB.Text = "Yeah !"
        Me.TweetB.UseVisualStyleBackColor = True
        '
        'IgnoreB
        '
        Me.IgnoreB.BackgroundImage = CType(resources.GetObject("IgnoreB.BackgroundImage"), System.Drawing.Image)
        Me.IgnoreB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.IgnoreB.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IgnoreB.Location = New System.Drawing.Point(161, 9)
        Me.IgnoreB.Name = "IgnoreB"
        Me.IgnoreB.Size = New System.Drawing.Size(103, 43)
        Me.IgnoreB.TabIndex = 99
        Me.IgnoreB.Text = "What are you talking about?"
        Me.IgnoreB.UseVisualStyleBackColor = True
        '
        'RetryB
        '
        Me.RetryB.BackgroundImage = CType(resources.GetObject("RetryB.BackgroundImage"), System.Drawing.Image)
        Me.RetryB.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RetryB.Location = New System.Drawing.Point(4, 74)
        Me.RetryB.Name = "RetryB"
        Me.RetryB.Size = New System.Drawing.Size(91, 27)
        Me.RetryB.TabIndex = 100
        Me.RetryB.Text = "Retry"
        Me.RetryB.UseVisualStyleBackColor = True
        Me.RetryB.Visible = False
        '
        'Code
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(269, 110)
        Me.Controls.Add(Me.RetryB)
        Me.Controls.Add(Me.IgnoreB)
        Me.Controls.Add(Me.TweetB)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Code"
        Me.Opacity = 0.92R
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Code"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TweetB As System.Windows.Forms.Button
    Friend WithEvents IgnoreB As System.Windows.Forms.Button
    Friend WithEvents RetryB As System.Windows.Forms.Button
End Class
