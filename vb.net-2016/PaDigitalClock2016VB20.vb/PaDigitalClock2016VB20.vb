Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Resources  ' ResourceManager

Class PaDigitalClock2016VB20 : Inherits Form
	Private brush As New SolidBrush(Color.FromArgb(255, 255, 0))
	Private rm As New ResourceManager("PaDigitalClock2016VB20", _
		System.Reflection.Assembly.GetExecutingAssembly())
	Private charMask() As UShort = {2048, 1024, 512, 256, 128, 64, 32, 16, 8, 4, 2, 1}
	Private byteDigitMatrix() As Byte
	Private nClientWidth, nClientHeight As Integer
	Private bmpDigits As Bitmap
	Private nItemW, nItemH As Integer
	Private nStartX, nStartY As Integer
	Private d, nSecond1 As Integer
	Private timer As System.Threading.Timer
	Private A_nDigits(10) As Byte
	Private A_nDigits1(10) As Byte
	Private strCrlf As String = Chr(13) + Chr(10)
	Private isRefresh As Boolean = True

	Protected Overrides ReadOnly Property DefaultSize As Size
		Get
			Return New Size(750, 450)
		End Get
	End Property

	Sub New()
		Me.Text = "PaDigitalClock2016VB20"
		Me.BackColor = Color.Black
		Me.StartPosition = FormStartPosition.CenterScreen
		Me.DoubleBuffered = True
		'Me.Icon = CType(rm.GetObject("this.ico"), Icon)
		Me.Icon = rm.GetObject("this.ico")

		PA_DoAppInitialize()

		Me.MinimumSize = New Size(300, 180)
		'Me.ClientSize = New Size(750, 450)

		PA_DoFormResize()

		timer = New System.Threading.Timer(AddressOf PaDigitalClock2016VB20_Timer, Nothing, 0, 10)
	End Sub

	Sub PA_DoAppInitialize()
		byteDigitMatrix = rm.GetObject("matrix.bin")
		Dim i As Integer
		For i=0 To 10
			A_nDigits1(i) = 0
		Next
	End Sub

	Sub PA_DoFormResize()
		nClientWidth = ClientRectangle.Width
		nClientHeight = ClientRectangle.Height
		If nClientWidth>0 And nClientHeight>0 Then
			d = Fix(nClientWidth / 12 / 11)	' 显示 12:00:00.00格式 (11字符,每个字符12点宽)
			If d<2 Then d=2
			nItemW = d * 12
			nItemH = d * 22
			nStartX = (nClientWidth - nItemW * 11)/2
			nStartY = (nClientHeight - nItemH)/2
			Dim nBitmapDigitW As Integer = nItemW * 13

			If Not bmpDigits Is Nothing Then bmpDigits.Dispose()
			bmpDigits = New Bitmap(nBitmapDigitW, nItemH)
			Using g1 As Graphics = Graphics.FromImage(bmpDigits)
				brush.Color = Color.FromArgb(0, 0, 255)
				g1.FillRectangle(brush, 0, 0, nBitmapDigitW, nItemH)
				brush.Color = Color.FromArgb(255, 255, 255)
				Dim nOffset As Integer =0
				Dim n, i, ii As Integer
				For n=0 To 11
					Dim x As integer = n * nItemW
					For i=0 To 21
						Dim ch As UShort = byteDigitMatrix(nOffset) Or (CUShort(byteDigitMatrix(nOffset+1))<<8)
						Dim y As Integer = i * d
						For ii=0 To 11
							If (ch And charMask(ii))>0 Then
								g1.FillEllipse(brush, x + ii * d, y, d, d)
							End If
						Next
						nOffset = nOffset + 2
					Next
				Next
			End Using
			Me.Invalidate()
		End If
	End Sub

	Sub PA_DoDisplay()
		Dim g As Graphics = Me.CreateGraphics()
		'g.DrawImage(bmpDigits, 0, 0)
		Dim dtNow As DateTime = DateTime.Now
		Dim nSplitter1 As Integer = IIf(dtNow.Millisecond>500, 12, 10)

		If nSecond1<>dtNow.Second Then
			nSecond1 = dtNow.Second
			isRefresh = True	'每秒钟强制重绘所有字符
		End If

		A_nDigits(0) = Fix(dtNow.Hour / 10)
		A_nDigits(1) = dtNow.Hour Mod 10
		A_nDigits(2) = nSplitter1
		A_nDigits(3) = Fix(dtNow.Minute / 10)
		A_nDigits(4) = dtNow.Minute Mod 10
		A_nDigits(5) = nSplitter1
		A_nDigits(6) = Fix(dtNow.Second / 10)
		A_nDigits(7) = dtNow.Second Mod 10
		A_nDigits(8) = 11
		A_nDigits(9) = Fix(dtNow.Millisecond / 100) Mod 10
		A_nDigits(10) = Fix(dtNow.Millisecond / 10) Mod 10

		Dim rectDest As New Rectangle(0, nStartY, nItemW, nItemH)
		Try
			Dim i As Integer
			For i=0 To 10
				If isRefresh Or A_nDigits(i)<>A_nDigits1(i) Then
					rectDest.X = nStartX + i * nItemW
					g.DrawImage(bmpDigits, rectDest, _
						A_nDigits(i)*nItemW, 0, nItemW, nItemH, GraphicsUnit.Pixel)
				End If
			Next
			Array.Copy(A_nDigits, A_nDigits1, A_nDigits.Length)
			isRefresh = False
		Catch ex As Exception
			Console.Write("Exception: " + ex.Message + strCrlf)
			isRefresh = true
		End Try
	End Sub

	Sub PaDigitalClock2016VB20_Timer(ByVal sender As Object)
		PA_DoDisplay()
	End Sub

	Sub PaDigitalClock2016VB20_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
		PA_DoFormResize()
	End Sub

	Sub PaDigitalClock2016VB20_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
		isRefresh = True
	End Sub

	Shared Sub Main()
		Application.Run(New PaDigitalClock2016VB20())
	End Sub

End Class
