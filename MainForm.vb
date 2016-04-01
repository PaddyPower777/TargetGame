Public Class MainForm
    Private fTargetAdmin As TargetAdmin
    Private Sub exitMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles exitMenuItem.Click
        Close()
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        fTargetAdmin = New TargetAdmin(gamePanel.Width, gamePanel.Height, 60)
        updateView()
    End Sub

    Public Sub updateView()
        moveTimer.Enabled = fTargetAdmin.Running
        gameTimer.Enabled = fTargetAdmin.Running
        startButton.Enabled = Not fTargetAdmin.Running
        stopButton.Enabled = fTargetAdmin.Running
        gamePanel.Image = fTargetAdmin.GameImage
        timeTextBox.Text = fTargetAdmin.TimeLeft.ToString()
    End Sub

    Private Sub startButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startButton.Click
        fTargetAdmin.startGame()
        updateView()
    End Sub

    Private Sub stopButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles stopButton.Click
        fTargetAdmin.stopGame()
        updateView()
    End Sub

    Private Sub moveTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moveTimer.Tick
        fTargetAdmin.nextMove()
        updateView()
    End Sub

    Private Sub gameTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gameTimer.Tick
        fTargetAdmin.decreaseTime()
        updateView()
    End Sub

    Private Sub gamePanel_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gamePanel.MouseClick
        If fTargetAdmin.hit(e.Location) Then
            fTargetAdmin.stopGame()
            updateView()
        End If
    End Sub

    Private Sub MainForm_ResizeBegin(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ResizeBegin
        fTargetAdmin.stopGame()
    End Sub

    Private Sub MainForm_ResizeEnd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ResizeEnd
        fTargetAdmin = New TargetAdmin(gamePanel.Width, gamePanel.Height, 60)
        updateView()
    End Sub
End Class
