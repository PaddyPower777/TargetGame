Imports MT264Sprites
Imports System.Drawing

Public Class TargetAdmin
    Private fSprite As TargetSprite
    Private fXStep As Integer
    Private fYStep As Integer
    Private fRunning As Boolean
    Private fGameImage As Bitmap
    Private fTimeLeft As Integer
    Private fMoveInterval As Integer

    ''' <summary>
    ''' True if the sprite is moving.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Running As Boolean
        Get
            Return fRunning
        End Get
    End Property

    ''' <summary>
    ''' The width of the rectangle
    ''' in which the sprite is moving.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GameWidth As Integer
        Get
            Return GameImage.Width
        End Get
    End Property

    ''' <summary>
    ''' The height of the rectangle
    ''' in which the sprite is moving.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GameHeight As Integer
        Get
            Return GameImage.Height
        End Get
    End Property

    ''' <summary>
    ''' The bitmap representing the game area
    ''' with the sprite on it.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GameImage As Bitmap
        Get
            Return fGameImage
        End Get
    End Property

    ''' <summary>
    ''' The time left in seconds.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TimeLeft As Integer
        Get
            Return fTimeLeft
        End Get
    End Property

    ''' <summary>
    ''' The unit of movement of the ball.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MoveInterval As Integer
        Get
            Return fMoveInterval
        End Get
    End Property

    ''' <summary>
    ''' Preconditions: none
    ''' Postconditions: A TargetAdmin object is created as for the parametrised 
    ''' constructor, but with the game's image of width 200 and height 200, and 
    ''' TimeLeft set to 60 initially.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyClass.New(200, 200, 60)
    End Sub

    ''' <summary>
    ''' Preconditions: w > 0, h > 0 and t > 0.
    ''' Postconditions: A TargetAdmin object is created. GameImage is created of width
    ''' w and height h. A sprite is placed so that it will appear at the top left corner of 
    ''' the game area. TimeLeft is set to t and Running is set to False initially.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal w As Integer, ByVal h As Integer, ByVal t As Integer)
        fGameImage = New Bitmap(w, h)
        fXStep = 0
        fYStep = 0
        fSprite = New TargetSprite(New Point(0, 0), GameWidth \ 30, GameWidth \ 30, fXStep, fYStep)
        fMoveInterval = 1
        fTimeLeft = t
        fRunning = False
        updateGameImage()
    End Sub

    ''' <summary>
    ''' Preconditions: none.
    ''' Postconditions: The unit of movement is set to (3,5) and Running is 
    ''' set to True.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub startGame()
        fXStep = 10
        fYStep = 13
        fSprite.setMovement(fXStep, fYStep)
        fRunning = True
    End Sub

    ''' <summary>
    ''' Preconditions: none
    ''' Postconditions: If Running, TimeLeft is decreased by 1. If TimeLeft less than 1,
    ''' Running is set to False.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub decreaseTime()
        If Running Then
            fTimeLeft = TimeLeft - 1
            If TimeLeft <= 0 Then
                fRunning = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Preconditions: none
    ''' Postconditions: If Running is True, the target sprite is moved and the GameImage
    ''' is updated. If the target sprite has hit an edge, it is reflected.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub nextMove()
        If Running Then
            fSprite.move()
            updateGameImage()
            If (fSprite.Position.X <= 0) Or _
               (fSprite.Position.X + fSprite.Width >= GameWidth) Then
                fSprite.reflectX()
            End If
            If (fSprite.Position.Y <= 0) Or _
               (fSprite.Position.Y + fSprite.Height >= GameHeight) Then
                fSprite.reflectY()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Preconditions: none.
    ''' Postconditions: If clickPos lies within the bounding box of the target sprite
    ''' True is returned. Otherwise False is returned.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function hit(ByVal clickPos As Point) As Boolean
        Return fSprite.isHit(clickPos)
    End Function

    ''' <summary>
    ''' Preconditions: none.
    ''' Postconditions: Running is set to False.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub stopGame()
        fRunning = False
    End Sub

    ''' <summary>
    ''' Preconditions: none
    ''' Postconditions: The current state of the game is drawn on g.
    ''' </summary>
    ''' <param name="g"></param>
    ''' <remarks></remarks>
    Public Sub draw(ByVal g As Graphics)
        g.DrawImage(GameImage, 0, 0)
    End Sub

    ''' <summary>
    ''' Preconditions: none.
    ''' Postconditions: GameImage is updated to match the current position of the target.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub updateGameImage()
        Dim g As Graphics
        g = Graphics.FromImage(GameImage)
        g.Clear(Color.White)
        fSprite.draw(g)
        g.Dispose()
    End Sub

    ''' <summary>
    ''' Preconditions: none.
    ''' Postconditions: The GameImage bitmap is destroyed.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub dispose()
        fGameImage.Dispose()
    End Sub
End Class
