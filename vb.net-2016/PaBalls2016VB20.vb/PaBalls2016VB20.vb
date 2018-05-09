Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Resources
Imports System.Drawing.Imaging

Class PaBalls2016VB20 : Inherits Form
	Private rm As New ResourceManager("PaBalls2016VB20", _
		System.Reflection.Assembly.GetExecutingAssembly())
	Private rand As New Random
	Private imgBgJpeg As Image = Nothing
	Private A_imgBalls(9) As Image
	Private timer1 As System.Threading.Timer

	Private nCount As Integer = 50
	Private A_objBalls(49) As BallDef

	Private nBgJpegWidth, nBgJpegHeight As Integer
	Private nClientWidth, nClientHeight As Integer
	Private nBallRadius As Integer = 40, nBallDiameter As Integer = 80
	Private nRangeSpaceX, nRangeSpaceY As Integer
	Private nMinD As Integer = 2, nRangeD As Integer = 20
	Private nBallIconID As Integer = 7

	Private isRunning As Boolean = False
	Private isMinimized As Boolean = False

	Private WithEvents toolbar1 As New ToolBar()
	Private imagelist1 As New ImageList()

	Private nTimerInterval As Integer = 20
	Private nPathID As Integer = 0
	Private PI2 As Double = Math.PI + Math.PI
	Private nCount1, nCount2 As Integer
	Private fRotate1 As Double = 0, fRotate2 As Double = 0

	Private imgAttrib As New ImageAttributes()

	Private WithEvents panel1 As New MyPanel()

	Protected Overrides ReadOnly Property DefaultSize As Size
		Get
			Return New Size(600, 450)
		End Get
	End Property

	Sub New()
		Me.Text = "PaBalls2016VB20"
		Me.BackColor = Color.Black
		Me.StartPosition = FormStartPosition.CenterScreen
		Me.DoubleBuffered = True
		Me.Icon = rm.GetObject("this.ico")

		PA_DoAppInitialize()

		panel1.BackColor = System.Drawing.Color.Black
		panel1.Dock = DockStyle.Fill
		Me.Controls.Add(panel1)

		'Me.ClientSize = New Size(600, 450)
		PA_DoInitToolbar()

		PA_DoBallsInit()

		timer1 = New System.Threading.Timer(AddressOf PaBalls2016VB20_Timer, _
			Nothing, 0, nTimerInterval)
		isRunning = True
	End Sub

	Sub PA_DoInitToolbar()
		imagelist1.ImageSize = New Size(16, 15)
		imagelist1.Images.Add(rm.GetObject("button-01.png"))
		imagelist1.Images.Add(rm.GetObject("button-02.png"))
		imagelist1.Images.Add(rm.GetObject("button-03.png"))
		imagelist1.Images.Add(rm.GetObject("button-04.png"))
		imagelist1.Images.Add(rm.GetObject("button-05.png"))

		toolbar1.ImageList = imagelist1
		toolbar1.ButtonSize = New Size(16, 15)
		Dim nButtonsCount As Integer = 6
		Dim i As Integer
		Dim n As Integer = 0
		For i=0 To nButtonsCount
			Dim tbb1 As New ToolBarButton()
			If i=2 Or i=5 Then
				tbb1.Style = ToolBarButtonStyle.Separator
			Else
				tbb1.ImageIndex = n
				n = n + 1
			End If
			toolbar1.Buttons.Add(tbb1)
		Next
		Controls.Add(toolbar1)
	End Sub

	Sub PA_DoAppInitialize()
		Dim A_strBalls() As String = {"ball-01.png", "ball-02.png", "ball-03.png", _
			"ball-04.png", "ball-05.png", "ball-06.png", "ball-07.png", _
			"ball-08.png", "ball-09.png", "ball-10.png"}
		Dim i As Integer

		nCount1 = Fix(nCount / 3)
		nCount2 = nCount - nCount1

		imgBgJpeg = rm.GetObject("bg.jpg")
		nBgJpegWidth = imgBgJpeg.Width
		nBgJpegHeight = imgBgJpeg.Height

		For i=0 To 9
			A_imgBalls(i) = rm.GetObject(A_strBalls(i))
		Next

		imgAttrib.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY)

		For i=0 To nCount - 1
			A_objBalls(i) = New BallDef()
		Next
	End Sub

	Sub PA_DoBallsMovePlaced()
		nPathID = 0
		Dim i As Integer
		For i=0 To nCount - 1
			A_objBalls(i).Init(nRangeSpaceX, nRangeSpaceY, nMinD, nRangeD, rand.Next(10), rand)
		Next
		PA_DoInvalidate()
	End Sub

	Sub PA_DoBallsMove()
		Dim i As Integer
		For i=0 To nCount - 1
			A_objBalls(i).Move()
		Next
		PA_DoInvalidate()
	End Sub

	Sub PA_DoBallsSwitchImage()
		nBallIconID = (nBallIconID+1) Mod 10
		Dim i As Integer
		For i=0 To nCount - 1
			A_objBalls(i).SetIcon(nBallIconID)
		Next
		PA_DoInvalidate()
	End Sub

	Sub PA_DoBallsSwitchImageX()
		Dim i As Integer
		For i=0 To nCount - 1
			A_objBalls(i).SetIcon(rand.Next(10))
		Next
		PA_DoInvalidate()
	End Sub

	Sub PA_DoEllipsePath()
		nPathID = 1
		PA_DoEllipsePathPlaced()
	End Sub

	Sub PA_DoEllipsePathPlaced()
		Dim i, a, b As Integer
		Dim n As Integer = 0
		Dim fAngle As Double
		Dim cx As Integer = (nClientWidth - nBallDiameter)/2
		Dim cy As Integer = (nClientHeight - nBallDiameter)/2
		a = nClientWidth / 4
		b = nClientHeight / 8
		For i=0 To nCount1 - 1
			fAngle = PI2 * i / nCount1 + fRotate1
			A_objBalls(n).x = cx + (a * Math.Sin(fAngle))
			A_objBalls(n).y = cy + (b * Math.Cos(fAngle))
			A_objBalls(n).dx=0
			A_objBalls(n).dy=0
			n = n + 1
		Next

		a = nClientWidth * 3 / 7
		b = nClientHeight * 2 / 7
		For i=0 To nCount2 - 1
			fAngle = PI2 - PI2 * i / nCount2 - fRotate2
			A_objBalls(n).x = cx + (a * Math.Sin(fAngle))
			A_objBalls(n).y = cy + (b * Math.Cos(fAngle))
			A_objBalls(n).dx=0
			A_objBalls(n).dy=0
			n = n + 1
		Next
		PA_DoInvalidate()
	End Sub

	Sub PA_DoEllipsePathMove()
		fRotate1 = fRotate1 + PI2 / 100
		fRotate2 = fRotate2 + PI2 / 200
		If fRotate1>PI2 Then fRotate1=0
		If fRotate2>PI2 Then fRotate2=0
		PA_DoEllipsePathPlaced()
	End Sub

	Sub PA_DoBallsInit()
		If panel1.Width>0 And panel1.Height>0 Then
			nClientWidth = panel1.Width
			nClientHeight = panel1.Height
			nRangeSpaceX = nClientWidth - nBallDiameter
			nRangeSpaceY = nClientHeight - nBallDiameter
			If nRangeSpaceX>0 And nRangeSpaceY>0 Then
				If nPathID=0 Then
					PA_DoBallsMovePlaced()
				Else
					PA_DoEllipsePathPlaced()
				End If
				PA_DoInvalidate()
				isMinimized = False
			Else
				isMinimized = True
			End If
		Else
			isMinimized = True
		End If
	End Sub

	Sub panel1_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles panel1.Resize
		PA_DoBallsInit()
	End Sub

	Sub PaBalls2016VB20_Timer(ByVal sender As Object)
		If nPathID=0 Then
			PA_DoBallsMove()
		Else
			PA_DoEllipsePathMove()
		End If
	End Sub

	Sub panel1_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles panel1.Paint
		Dim g As Graphics = e.Graphics
		If isMinimized Then Exit Sub

		g.DrawImage(imgBgJpeg, New Rectangle((nClientWidth-nBgJpegWidth)/2, _
			(nClientHeight-nBgJpegHeight)/2, nBgJpegWidth, nBgJpegHeight), _
			0, 0, nBgJpegWidth, nBgJpegHeight, GraphicsUnit.Pixel,imgAttrib)

		Dim i As Integer
		For i=0 To nCount - 1
			'g.DrawImage(A_imgBalls(A_objBalls(i).nIconID), A_objBalls(i).x, A_objBalls(i).y)
			g.DrawImage(A_imgBalls(A_objBalls(i).nIconID), New Rectangle(A_objBalls(i).x, _
				A_objBalls(i).y, nBallDiameter, nBallDiameter), 0, 0, _
				nBallDiameter, nBallDiameter, GraphicsUnit.Pixel,imgAttrib)
		Next
	End Sub

	Sub PA_DoInvalidate()
		panel1.Invalidate()
	End Sub

	Sub PA_DoSwitchTimer()
		If isRunning Then
			timer1.Change(System.Threading.Timeout.Infinite, nTimerInterval)
		Else
			timer1.Change(0, nTimerInterval)
		End If
		isRunning = Not isRunning
	End Sub

	Sub toolbar1_ButtonClick(ByVal sender As Object, ByVal e As ToolBarButtonClickEventArgs) _
			Handles toolbar1.ButtonClick
		Dim nIndex As Integer = toolbar1.Buttons.IndexOf(e.Button)

		Select nIndex
		Case 0
			PA_DoBallsSwitchImage()
		Case 1
			PA_DoBallsSwitchImageX()
		Case 3
			PA_DoEllipsePath()
		Case 4
			PA_DoBallsMovePlaced()
		Case 6
			PA_DoSwitchTimer()
		End Select
	End Sub

	Shared Sub Main()
		Application.Run(New PaBalls2016VB20())
	End Sub
End Class

Class MyPanel : Inherits Panel
	Public Sub New()
		Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, true)
		Me.SetStyle(ControlStyles.AllPaintingInWmPaint, true)
		Me.SetStyle(ControlStyles.UserPaint, true)
	End Sub
End Class

Class BallDef
	Public x, y, dx, dy, nIconID As Integer
	Public nMaxX, nMaxY As Integer

	Public Sub SetIcon(ByVal nIconID As Integer)
		Me.nIconID = nIconID
	End Sub

	Public Sub Init(ByVal nMaxX As Integer, ByVal nMaxY As Integer, _
		ByVal nMinD As Integer, ByVal nRangeD As Integer, _
		ByVal nIconID As Integer, ByVal rand As Random)

		Me.nMaxX = nMaxX : Me.nMaxY = nMaxY : Me.nIconID = nIconID
		Me.x = rand.Next(nMaxX) : Me.y = rand.Next(nMaxY)
		Me.dx = nMinD + rand.Next(nRangeD) : Me.dy = nMinD + rand.Next(nRangeD)
	End Sub

	Public Sub Move()
		Dim nx, ny As Integer
		nx = Me.x + Me.dx
		If Me.dx>0 And nx>Me.nMaxX Or Me.dx<0 And nx<0 Then
			Me.dx = - Me.dx
		Else
			Me.x = nx
		End If
		ny = Me.y + Me.dy
		If Me.dy>0 And ny>Me.nMaxY Or Me.dy<0 And ny<0 Then
			Me.dy = -Me.dy
		Else
			Me.y = ny
		End If
	End Sub
End Class