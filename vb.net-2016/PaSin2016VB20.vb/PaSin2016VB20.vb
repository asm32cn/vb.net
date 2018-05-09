Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Resources

Class PaSin2016VB20 : Inherits Form
	Private rm As New ResourceManager("PaSin2016VB20", _
		System.Reflection.Assembly.GetExecutingAssembly())
	Private brush As New SolidBrush(Color.FromArgb(0, 255,0))
	Private nClientWidth, nClientHeight As Integer
	Private PI2 As Double = Math.PI + Math.PI
	Private nCount As Integer = 200
	Private nOffset As Integer = 0
	Private nStartY As Integer
	Private nSizeY As Integer
	Private nWidth1 As Integer
	Private timer As System.Threading.Timer

	Protected Overrides ReadOnly Property DefaultSize As Size
		Get
			Return New Size(600, 450)
		End Get
	End Property

	Sub New()
		Me.Text = "PaSin2016VB20"
		Me.BackColor = Color.Black
		'Me.ClientSize = New Size(600, 450)
		Me.StartPosition = FormStartPosition.CenterScreen
		Me.DoubleBuffered = True
		Me.Icon = rm.GetObject("this.ico")

		PA_DoSin2016_Init()

		timer = New System.Threading.Timer(AddressOf PaSin2016VB20_Timer, Nothing, 0, 10)
	End Sub

	Sub PA_DoSin2016_Init()
		nClientWidth = ClientRectangle.Width
		nClientHeight = ClientRectangle.Height
		nStartY = nClientHeight / 2
		nWidth1 = nClientWidth / nCount / 2
		nSizeY = nClientHeight / 2
	End Sub

	Sub PaSin2016VB20_Timer(ByVal sender As Object)
		nOffset = nOffset + 5
		If nOffset>nCount Then nOffset=0
		Me.Invalidate()
	End Sub

	Sub PaSin2016VB20_Resize(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
		Dim g As Graphics = e.Graphics
		Dim i As Integer
		For i=0 To nCount-1
			Dim nStartX As Integer = nClientWidth * i / nCount
			Dim sin1 As Double = Math.Sin(PI2 * (i + nOffset) / nCount)
			Dim nHeight1 As Integer = sin1 * nSizeY
			Dim nOffsetY As Integer = IIf(nHeight1<0, nHeight1, nHeight1 * 0.9)
			Dim nHeight2 As Integer = IIf(nHeight1<0, -nHeight1, nHeight1)
			g.FillRectangle(brush, nStartX, nStartY + nOffsetY, nWidth1, CInt(nHeight2 * 0.1))
		Next
	End Sub

	Sub PaSin2016VB20_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
		PA_DoSin2016_Init()
	End Sub

	Shared Sub Main()
		Application.Run(New PaSin2016VB20())
	End Sub
End Class
