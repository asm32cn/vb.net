Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Resources

Class PaStars2016VB20 : Inherits Form
	Dim rm As New ResourceManager("PaStars2016VB20", _
		System.Reflection.Assembly.GetExecutingAssembly())
	Dim rand As New Random()
	Dim nClientWidth As Integer
	Dim nClientHeight As Integer
	Dim brush1 As New SolidBrush(Color.White)
	Dim timer1 As System.Threading.Timer

	Const nCount As Integer = 20
	Dim A_objStart(nCount) As Star2016Def
	Dim nSelectedID As Integer = 0

	Protected Overrides ReadOnly Property DefaultSize As Size
		Get
			Return New Size(600, 450)
		End Get
	End Property

	Public Sub New()
		Me.Text = "PaStars2016VB20"
		Me.BackColor = Color.Black
		Me.StartPosition = FormStartPosition.CenterScreen
		Me.DoubleBuffered = True
		Me.Icon = rm.GetObject("this.ico")

		Dim i As Integer
		For i=0 To nCount
			A_objStart(i) = New Star2016Def()
		Next

		'Me.ClientSize = New Size(600, 450)

		PA_DoFormResize()

		timer1 = New System.Threading.Timer(AddressOf PaStars2016VB20_Timer, Nothing, 0, 200)
	End Sub

	Sub PaStars2016VB20_Timer(senser As Object)
		nSelectedID = (nSelectedID+1) Mod (nCount + 1)
		PA_DoStarInit(nSelectedID)
		PA_DoInvalidate()
	End Sub

	Private Sub PA_DoInvalidate()
		Me.Invalidate()
	End Sub

	Sub PaStars2016CS20_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
		Dim g As Graphics = e.Graphics
		Dim n As Integer
		For n=0 To nCount
			brush1.Color = Color.FromArgb(A_objStart(n).cr, A_objStart(n).cg, A_objStart(n).cb)
			g.FillPolygon(brush1, A_objStart(n).pts)
		Next
	End Sub

	Sub PaStars2016CS20_Resize(sender As Object, e As EventArgs) Handles Me.Resize
		If ClientRectangle.Width>0 And ClientRectangle.Height>0 Then
			PA_DoFormResize()
		End If
	End Sub

	Private Sub PA_DoFormResize()
		nClientWidth = ClientRectangle.Width
		nClientHeight = ClientRectangle.Height
		Dim i As Integer
		For i=0 To nCount
			PA_DoStarInit(i)
		Next
	End Sub

	Private Sub PA_DoStarInit(i As Integer)
		Dim r As Integer = 20 + rand.Next(CInt(nClientWidth/20))
		A_objStart(i).init(r, _
			r + rand.Next(nClientWidth-r-r), _
			r + rand.Next(nClientHeight-r-r), rand)
	End Sub

	Shared Sub Main()
		Application.Run(New PaStars2016VB20())
	End Sub
End Class

Class Star2016Def
	Public Const PI2 As Double = Math.PI + Math.PI
	Public pts(11) As Point
	Public cr As Integer, cg As Integer, cb As Integer
	Private cx As Integer
	Private cy As Integer
	Private r As Integer

	Sub init(r As Integer, cx As Integer, cy As Integer, rand As Random)
		Me.r = r
		Me.cx = cx
		Me.cy = cy
		Dim r2 As Integer = r / 2
		Dim a1 As Double = PI2 / 10
		Dim i As Integer
		For i=0 To 4
			Dim id As Integer = i + i
			Dim a2 As Double = PI2 * i / 5
			pts(id).X = cx + CInt(Math.Sin(a2) * r)
			pts(id).Y = cy - CInt(Math.Cos(a2) * r)
			a2 = PI2 * i / 5 + a1
			pts(id+1).X = cx + CInt(Math.Sin(a2) * r2)
			pts(id+1).Y = cy - CInt(Math.Cos(a2) * r2)
		Next
		pts(10).X = pts(0).X
		pts(10).Y = pts(0).Y
		cr = rand.Next(256)
		cg = rand.Next(256)
		cb = rand.Next(256)
	End Sub
End Class