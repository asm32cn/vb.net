Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Resources

Class PaTriangle2016VB20 : Inherits Form
	Private rm As New ResourceManager("PaTriangle2016VB20", _
		System.Reflection.Assembly.GetExecutingAssembly())
	Private pts As New PaTriangleDef
	Private pts_t As PaTriangleDef
	Private rand As New Random
	Private nClientWidth As Integer
	Private nClientHeight As Integer
	Private nCount As Integer = 50
	Private timer As System.Threading.Timer
	Private  pen As New Pen(Color.White)

	Protected Overrides ReadOnly Property DefaultSize As Size
		Get
			Return New Size(600, 450)
		End Get
	End Property

	Public Sub New() ' 构造函数执行在 Form_Load 之前, 此时窗口还未显示出来
		Me.Text = "PaTriangle2016VB20"
		'Me.ClientSize = New Size(600, 450)
		Me.StartPosition = FormStartPosition.CenterScreen
		Me.BackColor = Color.Black
		Me.DoubleBuffered = True
		Me.Icon = rm.GetObject("this.ico")

		PA_DoTriangleInit() ' 在 ClientSize 修改的时候也会激发了 resize 执行了本过程
		timer = New System.Threading.Timer(AddressOf PaTriangle2016VB20_Timer, Nothing, 0, 10)
	End Sub

	'Sub PaTriangle2016VB20_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
	'End Sub

	Sub PaTriangle2016VB20_Timer(ByVal sender As Object)
		Me.Invalidate()
	End Sub

	Sub PA_DoTriangleInit()
		Dim i As Integer
		nClientWidth = ClientRectangle.Width
		nClientHeight = ClientRectangle.Height
		For i=0 To 2
			pts.points(i).x = rand.Next(nClientWidth)
			pts.points(i).y = rand.Next(nClientHeight)
			pts.dx(i) = rand.Next(2, 5)
			pts.dy(i) = rand.Next(2, 5)
		Next
		pts.color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256))
		pts.dr = 5
		pts.dg = 5
		pts.db = 5
	End Sub

	Sub PA_DoTriangleNextColor()
		Dim r, g, b, nr, ng, nb As Integer
		r = pts.color.R : g = pts.color.G : b = pts.color.B
		nb = b + pts.db
		If pts.db>0 And nb>255 Or pts.db<0 And nb<0 Then
			pts.db = - pts.db
			ng = g + pts.dg
			If pts.dg>0 And ng>255 Or pts.dg<0 And ng<0 Then
				pts.dg = - pts.dg
				nr = r + pts.dr
				If pts.dr>0 And nr>255 Or pts.dr<0 And pts.dr<0 Then
					pts.dr = - pts.dr
				Else
					r = nr
				End If
			Else
				g = ng
			End If
		Else
			b = nb
		End If
		pts.color = Color.FromArgb(r, g, b)
	End Sub

	Sub PA_DoTriangleMoveItem()
		Dim i, nx, ny as Integer
		For i= 0 To 2
			nx = pts.points(i).x + pts.dx(i)
			If pts.dx(i)>0 And nx>nClientWidth Or pts.dx(i)<0 And nx<0 Then
				pts.dx(i) = -pts.dx(i)
			Else
				pts.points(i).x = nx
			End If
			ny = pts.points(i).y + pts.dy(i)
			If pts.dy(i)>0 And ny>nClientHeight Or pts.dy(i)<0 And ny<0 Then
				pts.dy(i) = -pts.dy(i)
			Else
				pts.points(i).y = ny
			End If
		Next
	End Sub

	Sub PaTriangle2016VB20_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
		PA_DoTriangleInit()
		Me.Invalidate()
	End Sub 

	Sub PaTriangle2016VB20_MouseDown(ByVal sender As Object,ByVal e As MouseEventArgs) Handles Me.MouseDown
		If e.Button = System.Windows.Forms.MouseButtons.Right Then
			PA_DoTriangleInit()
		End If
	End Sub

	Sub PaTriangle2016VB20_Paint(ByVal sender As Object,ByVal e As PaintEventArgs) Handles Me.Paint
		Dim g As Graphics = e.Graphics
		Dim i As Integer
		For i=0 To nCount - 1
			If i=3 Then
				pts_t = pts.Clone()
			End If
			pen.Color = Color.FromArgb(pts.color.R*i/nCount,pts.color.G*i/nCount, pts.color.B*i/nCount)
			g.DrawLines(pen, pts.points)
			g.DrawLine(pen, pts.points(0), pts.points(2))
			PA_DoTriangleMoveItem()
		Next
		pts = pts_t.Clone()
		PA_DoTriangleNextColor()
	End Sub

	Shared Sub Main
		Application.Run(New PaTriangle2016VB20())
	End Sub
End Class

Class PaTriangleDef : Implements ICloneable
	Public points(2) as Point
	Public dx(2) As Integer
	Public dy(2) As Integer
	Public color As Color
	Public dr, dg, db as Integer
	Function Clone() As Object Implements System.ICloneable.Clone
		Dim obj As New PaTriangleDef()
		Dim i As Integer
		For i=0 To 2
			obj.points(i) = Me.points(i)
			obj.dx(i) = Me.dx(i)
			obj.dy(i) = Me.dy(i)
		Next
		obj.color = Me.color
		obj.dr = Me.dr
		obj.dg = Me.dg
		obj.db = Me.db
		'MsgBox("clone")
		Return obj
	End Function
End Class
