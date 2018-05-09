Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Resources

Class PaQix2016VB20 : Inherits Form
	Private rm As New ResourceManager("PaQix2016VB20", _
		System.Reflection.Assembly.GetExecutingAssembly())
	Private pen As New Pen(Color.White)
	Private nClientWidth, nClientHeight As Integer
	Private timer As System.Threading.Timer
	Private rand As New Random
	Private pqs As New PaQixDef()
	Private pqs_t As PaQixDef
	Private nCount As Integer = 200
	Private isColorEx As Boolean

	Protected Overrides ReadOnly Property DefaultSize As Size
		Get
			Return New Size(600, 450)
		End Get
	End Property

	Sub New()
		Me.Text = "PaQix2016VB20"
		'Me.ClientSize = New Size(600, 450)
		Me.BackColor = Color.Black
		Me.StartPosition = FormStartPosition.CenterScreen
		Me.DoubleBuffered = True
		Me.Icon = rm.GetObject("this.ico")

		PA_DoQixInit()

		timer = New System.Threading.Timer(AddressOf PaQix2016VB20_Timer, Nothing, 0, 10)
	End Sub

	Sub PA_DoQixInit()
		nClientWidth = ClientRectangle.Width
		nClientHeight = ClientRectangle.Height
		Dim i As Integer
		For i=0 To 1
			pqs.points(i).x = rand.Next(nClientWidth)
			pqs.points(i).y = rand.Next(nClientHeight)
			pqs.dx(i) = rand.Next(2, 5)
			pqs.dy(i) = rand.Next(2, 5)
		Next
		pqs.color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256))
		pqs.dr = 5
		pqs.dg = 5
		pqs.db = 5
	End Sub

	Sub PA_DoQixMove()
		Dim i, nx, ny As Integer
		For i=0 To 1
			nx = pqs.points(i).x + pqs.dx(i)
			If pqs.dx(i)>0 And nx>nClientWidth Or pqs.dx(i)<0 And nx<0 Then
				pqs.dx(i) = - pqs.dx(i)
			Else
				pqs.points(i).x = nx
			End If
			ny = pqs.points(i).y + pqs.dy(i)
			If pqs.dy(i)>0 And ny>nClientHeight Or pqs.dy(i)<0 And ny<0 Then
				pqs.dy(i) = - pqs.dy(i)
			Else
				pqs.points(i).y = ny
			End If
		Next
	End Sub

	Sub PA_DoQixNextColor()
		Dim r, g, b, nr, ng, nb As Integer
		r = pqs.color.R : g = pqs.color.G : b = pqs.color.B
		nb = b + pqs.db
		If pqs.db>0 And nb>255 Or pqs.db<0 And nb<0 Then
			pqs.db = - pqs.db
			ng = g + pqs.dg
			If pqs.dg>0 And ng>255 Or pqs.dg<0 And ng<0 Then
				pqs.dg = - pqs.dg
				nr = r + pqs.dr
				If pqs.dr>0 And nr>255 Or pqs.dr<0 And pqs.dr<0 Then
					pqs.dr = - pqs.dr
				Else
					r = nr
				End If
			Else
				g = ng
			End If
		Else
			b = nb
		End If
		pqs.color = Color.FromArgb(r, g, b)
	End Sub

	Sub PaQix2016VB20_Timer(ByVal sender As Object)
		Me.Invalidate()
	End Sub

	Sub PaQix2016VB20_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseDown
		If e.Button = System.Windows.Forms.MouseButtons.Right Then
			PA_DoQixInit()
			Me.Invalidate()
		End If
	End Sub

	Sub PaQix2016VB20_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
		PA_DoQixInit()
	End Sub

	Sub PaQix2016VB20_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
		Dim g As Graphics = e.Graphics
		Dim i As Integer
		For i=0 To nCount-1
			If i=5 Then pqs_t = pqs.Clone()
			pen.Color = Color.FromArgb(pqs.color.r * i / nCount, pqs.color.g * i / nCount, pqs.color.b * i / nCount)
			g.DrawLine(pen, pqs.points(0), pqs.points(1))
			PA_DoQixMove()
		Next
		pqs = pqs_t.Clone()
		PA_DoQixNextColor()
	End Sub

	Shared Sub Main()
		Application.Run(New PaQix2016VB20())
	End Sub
End Class

Class PaQixDef : Implements ICloneable
	Public points(1) As Point
	Public dx(1) As Integer
	Public dy(1) As Integer
	Public color As New Color()
	Public dr, dg, db As Integer

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Dim obj As New PaQixDef()
		Dim i As Integer
		For i=0 To 1
			obj.points(i) = Me.points(i)
			obj.dx(i) = Me.dx(i)
			obj.dy(i) = Me.dy(i)
		Next
		obj.color = Me.color
		obj.dr = Me.dr
		obj.dg = Me.dg
		obj.db = Me.db
		Return obj
	End Function
End Class
