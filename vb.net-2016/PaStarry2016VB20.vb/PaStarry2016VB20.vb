Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class PaStarry2016VB20 : Inherits Form
	Private rand As New Random
	Private brush As New SolidBrush(Color.White)
	Private nClientWidth, nClientHeight As Integer
	Private nCount As Integer = 100
	Private A_objStarry(100) As PaStarryDef
	Private sel As Integer = 0
	Private timer As System.Threading.Timer
	'Shadows ClientSize As Size = New Size(600, 450)

	Protected Overrides ReadOnly Property DefaultSize As Size
		Get
			Return New Size(600, 450)
		End Get
	End Property

	Public Sub New()
		Dim i As Integer
		For i=0 To nCount
			A_objStarry(i) = New PaStarryDef()
		Next
		Me.Text = "PaStarry2016VB20"
		Me.BackColor = Color.Black
		Me.StartPosition = FormStartPosition.CenterScreen
		Me.DoubleBuffered = True
		'Me.ClientSize = New Size(600, 450)
		Me.Visible = True
		PA_DoStarryInit() ' 修改 ClientSize 也会执行一次这里
		timer = New System.Threading.Timer(AddressOf PaStarry2016VB20_Timer, Nothing, 0, 10)
	End Sub
	
	'Sub PaStarry2016VB20_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
	'End Sub

	Sub PaStarry2016VB20_Timer(ByVal sender As Object)
		sel = sel + 1
		If sel > nCount Then sel = 0
		PA_DoStarrySetItem(sel)
		Me.Invalidate()
	End Sub

	Sub PaStarry2016VB20_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
		PA_DoStarryInit()
		Me.Invalidate()
	End Sub

	Sub PaStarry2016VB20_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
		Dim g As Graphics = e.Graphics
		Dim i As Integer
		For i = 0 To nCount
			brush.Color = A_objStarry(i).color
			g.FillRectangle(brush, A_objStarry(i).rect)
		Next
	End Sub

	Sub PA_DoStarryInit()
		Dim i As Integer
		nClientWidth = ClientRectangle.Width
		nClientHeight = ClientRectangle.Height
		For i=0 To nCount
			PA_DoStarrySetItem(i)
		Next
	End Sub

	Sub PA_DoStarrySetItem(ByVal i As Integer)
		Dim d As Integer = rand.Next(2, 6)
		A_objStarry(i).rect = New Rectangle(rand.Next(nClientWidth), rand.Next(nClientHeight), d, d)
		A_objStarry(i).color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256))
	End Sub
	
	Shared Sub Main()
		Application.Run(New PaStarry2016VB20())
	End Sub
End Class

Class PaStarryDef
	Public rect As Rectangle
	Public color As Color
End Class