<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class About
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(About))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.LogOutB = New System.Windows.Forms.Button()
        Me.CloseB = New System.Windows.Forms.PictureBox()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BlockedUsersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsersWhoDontFollowBackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerateRandomFFToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UpdatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearCacheToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerateRandomFFToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearCacheToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BlockedUsersToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        CType(Me.CloseB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.DimGray
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button1.Enabled = False
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(12, 34)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(219, 110)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "This a beta seed of a project of mine that's on hiatus. The delayed release suppo" & _
            "rts: Lists, View Timelines , Facebook chat etc. I detail other cool stuff I'm wo" & _
            "rking on, on my site http://saisi.me"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.Enabled = False
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.Crimson
        Me.Button2.Location = New System.Drawing.Point(12, 151)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(219, 86)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "If you have any suggestions, ideas or bugs reports tweet me @Saisi_. If you have " & _
            "greater ideas in mind email me Saisi@saisi.me"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'LogOutB
        '
        Me.LogOutB.Font = New System.Drawing.Font("Arial Black", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogOutB.Location = New System.Drawing.Point(237, 199)
        Me.LogOutB.Name = "LogOutB"
        Me.LogOutB.Size = New System.Drawing.Size(99, 38)
        Me.LogOutB.TabIndex = 2
        Me.LogOutB.Text = "Log Out"
        Me.LogOutB.UseVisualStyleBackColor = True
        '
        'CloseB
        '
        Me.CloseB.BackColor = System.Drawing.Color.Transparent
        Me.CloseB.BackgroundImage = CType(resources.GetObject("CloseB.BackgroundImage"), System.Drawing.Image)
        Me.CloseB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CloseB.Location = New System.Drawing.Point(310, 1)
        Me.CloseB.Name = "CloseB"
        Me.CloseB.Size = New System.Drawing.Size(26, 26)
        Me.CloseB.TabIndex = 134
        Me.CloseB.TabStop = False
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolsToolStripMenuItem.ForeColor = System.Drawing.Color.DarkRed
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(53, 20)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        '
        'BlockedUsersToolStripMenuItem
        '
        Me.BlockedUsersToolStripMenuItem.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlockedUsersToolStripMenuItem.Name = "BlockedUsersToolStripMenuItem"
        Me.BlockedUsersToolStripMenuItem.Size = New System.Drawing.Size(253, 22)
        Me.BlockedUsersToolStripMenuItem.Text = "Blocked Users"
        '
        'UsersWhoDontFollowBackToolStripMenuItem
        '
        Me.UsersWhoDontFollowBackToolStripMenuItem.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsersWhoDontFollowBackToolStripMenuItem.Name = "UsersWhoDontFollowBackToolStripMenuItem"
        Me.UsersWhoDontFollowBackToolStripMenuItem.Size = New System.Drawing.Size(253, 22)
        Me.UsersWhoDontFollowBackToolStripMenuItem.Text = "Users Who don't follow back"
        '
        'GenerateRandomFFToolStripMenuItem
        '
        Me.GenerateRandomFFToolStripMenuItem.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GenerateRandomFFToolStripMenuItem.Name = "GenerateRandomFFToolStripMenuItem"
        Me.GenerateRandomFFToolStripMenuItem.Size = New System.Drawing.Size(253, 22)
        Me.GenerateRandomFFToolStripMenuItem.Text = "Generate random #FF"
        '
        'UpdatesToolStripMenuItem
        '
        Me.UpdatesToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UpdatesToolStripMenuItem.ForeColor = System.Drawing.Color.DarkRed
        Me.UpdatesToolStripMenuItem.Name = "UpdatesToolStripMenuItem"
        Me.UpdatesToolStripMenuItem.Size = New System.Drawing.Size(69, 20)
        Me.UpdatesToolStripMenuItem.Text = "Updates"
        '
        'ClearCacheToolStripMenuItem
        '
        Me.ClearCacheToolStripMenuItem.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClearCacheToolStripMenuItem.Name = "ClearCacheToolStripMenuItem"
        Me.ClearCacheToolStripMenuItem.Size = New System.Drawing.Size(253, 22)
        Me.ClearCacheToolStripMenuItem.Text = "Clear Cache"
        '
        'ToolsToolStripMenuItem1
        '
        Me.ToolsToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GenerateRandomFFToolStripMenuItem1, Me.ClearCacheToolStripMenuItem1, Me.BlockedUsersToolStripMenuItem1})
        Me.ToolsToolStripMenuItem1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolsToolStripMenuItem1.ForeColor = System.Drawing.Color.DarkRed
        Me.ToolsToolStripMenuItem1.Name = "ToolsToolStripMenuItem1"
        Me.ToolsToolStripMenuItem1.Size = New System.Drawing.Size(49, 20)
        Me.ToolsToolStripMenuItem1.Text = "Tools"
        '
        'GenerateRandomFFToolStripMenuItem1
        '
        Me.GenerateRandomFFToolStripMenuItem1.Name = "GenerateRandomFFToolStripMenuItem1"
        Me.GenerateRandomFFToolStripMenuItem1.Size = New System.Drawing.Size(209, 22)
        Me.GenerateRandomFFToolStripMenuItem1.Text = "Generate random #FF"
        '
        'ClearCacheToolStripMenuItem1
        '
        Me.ClearCacheToolStripMenuItem1.Name = "ClearCacheToolStripMenuItem1"
        Me.ClearCacheToolStripMenuItem1.Size = New System.Drawing.Size(209, 22)
        Me.ClearCacheToolStripMenuItem1.Text = "Clear Cache"
        '
        'BlockedUsersToolStripMenuItem1
        '
        Me.BlockedUsersToolStripMenuItem1.Name = "BlockedUsersToolStripMenuItem1"
        Me.BlockedUsersToolStripMenuItem1.Size = New System.Drawing.Size(209, 22)
        Me.BlockedUsersToolStripMenuItem1.Text = "Blocked Users"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AboutToolStripMenuItem.ForeColor = System.Drawing.Color.DarkRed
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.AboutToolStripMenuItem.Text = "Update"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.Transparent
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolsToolStripMenuItem1, Me.AboutToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(337, 24)
        Me.MenuStrip1.TabIndex = 135
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'About
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Makmende.My.Resources.Resources.DigitalSmoke
        Me.ClientSize = New System.Drawing.Size(337, 249)
        Me.Controls.Add(Me.CloseB)
        Me.Controls.Add(Me.LogOutB)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "About"
        Me.Opacity = 0.92R
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "About"
        CType(Me.CloseB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents LogOutB As System.Windows.Forms.Button
    Friend WithEvents CloseB As System.Windows.Forms.PictureBox
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BlockedUsersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsersWhoDontFollowBackToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UpdatesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerateRandomFFToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearCacheToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerateRandomFFToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearCacheToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents BlockedUsersToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
End Class
