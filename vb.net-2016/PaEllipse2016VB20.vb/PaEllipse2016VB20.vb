Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Resources

Class PaEllipse2016VB20 : Inherits Form
	Private rm As New ResourceManager("PaEllipse2016VB20", _
		System.Reflection.Assembly.GetExecutingAssembly())
	Private rand As New Random
	Private nClientWidth, nClientHeight As Integer
	Private brush1 As New SolidBrush(Color.Yellow)
	Private PI2 As Double = Math.PI + Math.PI
	Private timer As System.Threading.Timer
	Private fStartAngle As Double
	Private pes1 As New PaEllipseDef(0, 0, 300, 75, 0, 0)
	Private pes2 As New PaEllipseDef(0, 0, 50, 200, 0, 0)

	Protected Overrides ReadOnly Property DefaultSize As Size
		Get
			Return New Size(600, 450)
		End Get
	End Property

	Sub New()
		Me.Text = "PaEllipse2016VB20"
		'Me.ClientSize = New Size(600, 450)
		Me.BackColor = Color.Black
		Me.StartPosition = FormStartPosition.CenterScreen
		Me.DoubleBuffered = True
		Me.Icon = rm.GetObject("this.ico")

		timer = New System.Threading.Timer(AddressOf PaEllipse2016VB20_Timer, Nothing, 0, 20)
	End Sub

	Sub PaEllipse2016VB20_Timer(ByVal sender As Object)
		PA_DoEllipseRotate()
		Me.Invalidate()
	End Sub

	Sub PaEllipse2016VB20_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
		Dim g As Graphics = e.Graphics
		Dim x0 As Integer = ClientRectangle.Width / 2
		Dim y0 As Integer = ClientRectangle.Height / 2
		Dim step1 As Double = PI2 / 40
		Dim r As Integer = 0
		Dim d As Integer = 0
		Dim point1 As New Point()
		Dim i As Double
		For i=0 To PI2 Step step1
			Dim sin1 As Double = Math.Sin(i + fStartAngle)
			Dim cos1 As Double = Math.Cos(i + fStartAngle)
			r = IIf(r=3, 6, 3)
			d = r + r

			point1.X = pes1.a * sin1
			point1.Y = pes1.b * cos1
			point1 = Rotate(point1, fStartAngle)
			point1.X = point1.X + x0
			point1.Y = point1.Y + y0
			g.FillEllipse(brush1, point1.X-r, point1.Y-r, d, d)

			point1.X = pes2.a * sin1
			point1.Y = pes2.b * cos1
			point1 = Rotate(point1, fStartAngle)
			point1.X = point1.X + x0
			point1.Y = point1.Y + y0
			g.FillEllipse(brush1, point1.X-r, point1.Y-r, d, d)

		Next
	End Sub

	Function Rotate(ByVal point1 As Point, ByVal angle As Double) As Point
		Dim sin1 As Double = Math.Sin(angle)
		Dim cos1 As Double = Math.Cos(angle)
		Dim x1 As Integer = point1.X
		Dim y1 As Integer = Point1.Y
		point1.X = cos1 * x1 - sin1 * y1
		point1.Y = cos1 * y1 + sin1 * x1
		Return point1
	End Function

	Sub PA_DoEllipseRotate()
		fStartAngle += PI2 / 160
		If fStartAngle>=PI2 Then fStartAngle=0
	End Sub

	Shared Sub Main()
		Application.Run(New PaEllipse2016VB20())
	End Sub

End Class

Class PaEllipseDef
	Public x, y As Integer
	Public a, b As Integer
	Public angle As Double
	Public rotate As Double

	Sub New(ByVal x As Integer, ByVal y As Integer, ByVal a As Integer, ByVal b As Integer, _
		ByVal angle As Double, ByVal rotate As double)
		Me.x = x
		Me.y = y
		Me.a = a
		Me.b = b
		Me.angle = angle
		me.rotate = rotate
	End Sub
End Class
