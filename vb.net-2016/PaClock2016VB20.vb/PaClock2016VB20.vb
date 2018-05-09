Imports System
Imports System.Drawing
imports System.Windows.Forms
Imports System.Resources

Class PaClock2016VB20 : Inherits Form
	Private pen1 As New Pen(Color.Black)
	Private timer1 As System.Threading.Timer
	Private nClientWidth, nClientHeight As Integer
	Private nClockRadius, nClockDiameter As Integer
	Private PI2 As Double = Math.PI + Math.PI
	Private font1 As New Font("Arial Black", 24)
	Private brush1 As New SolidBrush(Color.FromArgb(87, 183, 119))
	Private rm As New ResourceManager("PaClock2016VB20", _
		System.Reflection.Assembly.GetExecutingAssembly())
	Private imgInterface As Image
	Private bmpInterface As Bitmap = Nothing

	Protected Overrides ReadOnly Property DefaultSize As Size
		Get
			Return New Size(600, 450)
		End Get
	End Property

	Sub New()
		Me.Text = "PaClock2016VB20"
		Me.BackColor = Color.White
		Me.StartPosition = FormStartPosition.CenterScreen
		Me.DoubleBuffered = True
		Me.Icon = rm.GetObject("this.ico")

		PA_DoAppInitialize()

		Me.MinimumSize = New Size(450, 450)
		'Me.ClientSize = New Size(600, 450)

		PA_DoFormResize()

		timer1 = New System.Threading.Timer(AddressOf PaClock2016VB20_Timer, Nothing, 0, 500)
	End Sub

	Sub PA_DoAppInitialize()
		'imgInterface = CType(rm.GetObject("interface.gif"), Image)
		imgInterface = rm.GetObject("interface.gif")
	End Sub

	Sub PA_DoFormResize()
		nClientWidth  = ClientRectangle.Width
		nClientHeight = ClientRectangle.Height
		If nClientWidth>0 And nClientHeight>0 Then
			nClockRadius = Fix(IIf(nClientWidth>nClientHeight, nClientHeight, nClientWidth) *9 / 10 / 2)
			nClockDiameter = nClockRadius + nClockRadius
			If Not bmpInterface Is Nothing Then bmpInterface.Dispose()
			bmpInterface = New Bitmap(nClockDiameter, nClockDiameter)
			Using g1 As Graphics = Graphics.FromImage(bmpInterface)
				Dim x As Integer = (nClockDiameter - imgInterface.Width) / 2 + 15
				Dim y As Integer = (nClockDiameter - imgInterface.Height) / 2
				g1.DrawImage(imgInterface, x, y)
				pen1.Color = Color.Black
				pen1.Width = 7
				g1.DrawEllipse(pen1, 3, 3, nClockDiameter-7, nClockDiameter-7)
				g1.DrawEllipse(pen1, nClockRadius-5, nClockRadius-5, 10, 10)
				Dim i As Integer
				For i=0 To 59
					Dim angle1 As Double = PI2 * i / 60
					Dim dx1 As Double = Math.Sin(angle1) * nClockRadius
					Dim dy1 As Double = Math.Cos(angle1) * nClockRadius
					Dim s1 As Double
					If i Mod 5=0 Then
						pen1.Width = 5
						s1 = 0.9
					Else
						pen1.Width = 3
						s1 = 0.94
					End If
					g1.DrawLine(pen1, nClockRadius + CInt(dx1), nClockRadius - CInt(dy1), _
						nClockRadius + CInt(dx1 * s1), nClockRadius - CInt(dy1 * s1))
					Dim str As String = "" & Fix(i/5)
					if i Mod 5=0 Then
						g1.DrawString(str, font1, brush1, _
							CDbl(nClockRadius + dx1 * 0.8 - 15), _
							CDbl(nClockRadius - dy1 * 0.8) -25)
					End If
				Next
			End Using
		End If
	End Sub

	Sub PaClock2016VB20_Timer(ByVal sender As Object)
		Me.Invalidate()
	End Sub

	Sub PaClock2016VB20_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
		PA_DoFormResize()
		Me.Invalidate()
	End Sub

	Sub PaClock2016VB20_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
		Dim g As Graphics = e.Graphics
		'g.DrawImage(bmpInterface, 0, 0)
		Dim dtNow As DateTime = DateTime.Now
		Dim x As Integer = (nClientWidth - nClockDiameter)/2
		Dim y As Integer = (nClientHeight - nClockDiameter)/2
		g.DrawImage(bmpInterface, x, y)
		
		Dim cx As Integer = nClientWidth / 2
		Dim cy As Integer = nClientHeight / 2

		Dim fStart As Double = 0.05
		Dim penWidth() As Integer = {3, 5, 7}
		Dim A_fEnd() As Double = {0.7, 0.5, 0.4}
		Dim A_fAngle() As Double = { _
			PI2 * dtNow.Second / 60, _
				PI2 * (60 * dtNow.Minute + dtNow.Second) / 3600, _
				PI2 * (60 * ( 60 * ( dtNow.Hour Mod 12 ) + dtNow.Minute) + dtNow.Second) / 43200}
		Dim i As Integer
		For i=0 To 2
			Dim dx1 As Double = Math.Sin(A_fAngle(i)) * nClockRadius
			Dim dy1 As double = Math.Cos(A_fAngle(i)) * nClockRadius
			pen1.Width = penWidth(i)
			g.DrawLine(pen1, _
				CSng(cx + dx1 * fStart), CSng(cy - dy1 * fStart), _
				CSng(cx + dx1 * A_fEnd(i)), CSng(cy - dy1 * A_fEnd(i)))
		Next
	End Sub

	Shared Sub Main()
		Application.Run(New PaClock2016VB20())
	End Sub
End Class
