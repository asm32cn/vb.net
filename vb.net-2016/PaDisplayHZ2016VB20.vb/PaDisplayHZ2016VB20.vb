Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Resources

Class PaDisplayHZ2016VB20 : Inherits Form
	Private rm As New ResourceManager("PaDisplayHZ2016VB20", _
		System.Reflection.Assembly.GetExecutingAssembly())

	Private nClientWidth, nClientHeight As Integer
	'Private brush1 As New SolidBrush(Color.FromArgb(255, 255, 0))
	'Private brush2 As New SolidBrush(Color.FromArgb(31, 31, 31))
	Private A_nMatrixBuffer(23, 47) As Byte
	Private A_nDisplayBuffer(23, 47) As Byte
	Private A_nDisplayCache(23, 47) As Byte
	Private nPoints As Integer = 24
	Private nPointsHF As Integer = 12
	Private nCountX As Integer = 48
	Private A_mask() As Byte = {128, 64, 32, 16, 8, 4, 2, 1}
	Private timer1 As System.Threading.Timer
	Private nStart As Integer = 0
	Private nStart1 As Integer = 0
	Private nActionID As Integer = 4
	Private nActionID1 As Integer = 0
	Private nActionCount As Integer = 20
	Private nSleep As integer = 0
	Private rand As New Random()
	Private d, d1, nStartX, nStartY As Integer
	Private A_matrix As Byte()
	Private isRefresh As Boolean = True
	Private isReady As Boolean = False
	Private strCrlf As String = Chr(13) + Chr(10)

	Protected Overrides ReadOnly Property DefaultSize As Size
		Get
			Return New Size(600, 450)
		End Get
	End Property

	Public Sub New()
		Me.Text = "PaDisplayHZ2016VB20"
		Me.BackColor = Color.Black
		Me.StartPosition = FormStartPosition.CenterScreen
		Me.DoubleBuffered = True
		Me.MinimumSize = new Size(550, 350)
		Me.Icon = rm.GetObject("this.ico")

		A_matrix = rm.GetObject("matrix24f.bin")
		Dim n, stx, y, x, pos, id As Integer
		id = 0
		For n=0 To 1
			stx = n * nPoints
			For y=0 To nPoints - 1
				For x=0 To nPoints - 1
					pos = x Mod 8
					A_nMatrixBuffer(y, stx + x) = IIF((A_matrix(id) And A_mask(pos))>0, 1, 0)
					'A_nDisplayBuffer(y, stx + x) = A_nMatrixBuffer(y, stx + x)
					If pos=7 Then
						id = id + 1
					End If
				Next
			Next
		Next

		PA_DoFormResize()

		'Me.ClientSize = New Size(600, 450)

		timer1 = new System.Threading.Timer(AddressOf PaDisplayHZ2016CS20_Timer, Nothing, 0, 20)

	End Sub

	Private Sub PaDisplayHZ2016CS20_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
		If ClientRectangle.Width>0 And ClientRectangle.Height>0 Then
			PA_DoFormResize()
			Me.Invalidate()
		End If
		isRefresh = True
	End Sub

	Private Sub PaDisplayHZ2016VB20_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
		'Dim g As Graphics = e.Graphics
		isRefresh = True
	End Sub

	Private Sub PaDisplayHZ2016CS20_Timer(ByVal sender As Object)
		PA_DoAction()
		PA_DoDisplay()
	End Sub

	Private Sub PA_DoDisplay()
		Try

			Using g As Graphics = Me.CreateGraphics()
				Dim brush1 As New SolidBrush(Color.FromArgb(255, 255, 0))
				Dim brush2 As New SolidBrush(Color.FromArgb(31, 31, 31))

				Dim y, x As Integer
				For y = 0 To nPoints - 1
					For x = 0 To nCountX - 1
						If isRefresh Or A_nDisplayBuffer(y, x)<>A_nDisplayCache(y, x) Then
							g.FillEllipse(IIF(A_nDisplayBuffer(y, x)=1, brush1, brush2), _
								nStartX + x*d1, nStartY + y*d1, d, d)
						End If
						A_nDisplayCache(y, x) = A_nDisplayBuffer(y, x)
					Next
				Next
			End Using
			isRefresh = False
		Catch ex As Exception
			Console.Write("Exception: " + ex.Message + strCrlf)
			isRefresh = true
		End Try
	End Sub

	Private Sub PA_DoFormResize()
		nClientWidth = ClientRectangle.Width
		nClientHeight = ClientRectangle.Height
		d1 = Fix(nClientWidth / 48)
		d = d1 - 1
		nStartX = (nClientWidth - d1 * 48) / 2
		nStartY = (nClientHeight - d1 * 24) / 2
		isReady = True
	End Sub

	Private Sub PA_DoGetNextAction()
		Do
			nActionID = rand.Next(nActionCount)
		Loop While (nActionID1=nActionID)
		nActionID1 = nActionID
		'nActionID = (nActionID + 1) Mod nActionCount
	End Sub

	Private Sub PA_DoAction()
		If nSleep>0 Then
			nSleep = nSleep-1
		Else
			Select nActionID
				Case 1 : PA_DoAction1()
				Case 2 : PA_DoAction2()
				case 3 : PA_DoAction3()
				case 4 : PA_DoAction4()
				case 5 : PA_DoAction5()
				case 6 : PA_DoAction6()
				case 7 : PA_DoAction7()
				case 8 : PA_DoAction8()
				case 9 : PA_DoAction9()
				case 10 : PA_DoAction10()
				case 11 : PA_DoAction11()
				case 12 : PA_DoAction12()
				case 13 : PA_DoAction13()
				case 14 : PA_DoAction14()
				case 15 : PA_DoAction15()
				case 16 : PA_DoAction16()
				case 17 : PA_DoAction17()
				case 18 : PA_DoAction18()
				case 19 : PA_DoAction19()
				Case Else : PA_DoAction0()
			End Select
			If nStart1=0 Then
				nSleep = 25
				PA_DoGetNextAction()
			End If
		End If
	End Sub

	Private Sub PA_DoAction0()
		Dim y, x As Integer
		nStart = nCountX - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If x<nStart Then
					A_nDisplayBuffer(y, x) = 0
				Else
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, x-nStart)
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nCountX
	End Sub

	Private Sub PA_DoAction1()
		Dim y, x As Integer
		nStart = nCountX - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If x<nStart1 Then
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, (nStart + x - 1))
				Else
					A_nDisplayBuffer(y, x) = 0
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nCountX
	End Sub

	Private Sub PA_DoAction2()
		Dim y, x As Integer
		nStart = nCountX - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, (x + nStart1) Mod nCountX)
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nCountX
	End Sub

	Private Sub PA_DoAction3()
		Dim y, x As Integer
		nStart = nCountX - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, (nCountX + x + nStart - 1) Mod nCountX)
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nCountX
	End Sub

	Private Sub PA_DoAction4()
		Dim y, x As Integer
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If x < nPoints - nStart1 Or x > nPoints + nStart1 Then
					A_nDisplayBuffer(y, x) = 0
				Else
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, x)
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nPoints
	End Sub

	Private Sub PA_DoAction5()
		Dim y, x As Integer
		nStart = nPoints - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If x < nPoints-nStart + 1 Or x > nPoints + nStart - 1 Then
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, x)
				Else
					A_nDisplayBuffer(y, x) = 0
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nPoints
	End Sub

	Private Sub PA_DoAction6()
		Dim y, x As Integer
		nStart = nCountX - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If x<nStart1 Then
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, x)
				Else
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, nStart1)
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nCountX
	End Sub

	Private Sub PA_DoAction7()
		Dim y, x As Integer
		nStart = nCountX - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If x<nStart Then
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, nStart - 1)
				Else
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, x)
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nCountX
	End Sub

	Private Sub PA_DoAction8()
		Dim y, x As Integer
		nStart = nCountX - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If x<nStart1 Then
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, x)
				Else
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, nStart1 + Int((x - nStart1) / 2) )
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nCountX
	End Sub

	Private Sub PA_DoAction9()
		Dim y, x As Integer
		nStart = nCountX - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If x<nStart Then
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, nStart1 + Int((nStart-x) / 2))
				Else
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, x)
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nCountX
	End Sub

	Private Sub PA_DoAction10()
		Dim y, x As Integer
		nStart = nPoints - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If y<nStart1 Then
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, x)
				Else
					A_nDisplayBuffer(y, x) = 0
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nPoints
	End Sub

	Private Sub PA_DoAction11()
		Dim y, x As Integer
		nStart = nPoints - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If y<nStart Then
					A_nDisplayBuffer(y, x) = 0
				Else
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, x)
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nPoints
	End Sub

	Private Sub PA_DoAction12()
		Dim y, x As Integer
		nStart = nPoints - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If y<nStart1 Then
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y  + nStart, x)
				Else
					A_nDisplayBuffer(y, x) = 0
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nPoints
	End Sub

	Private Sub PA_DoAction13()
		Dim y, x As Integer
		nStart = nPoints - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If y<nStart Then
					A_nDisplayBuffer(y, x) = 0
				Else
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y - nStart, x)
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nPoints
	End Sub

	Private Sub PA_DoAction14()
		Dim y, x As Integer
		nStart = nPointsHF - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If y<nStart Or y>nPointsHF + nStart1 Then
					A_nDisplayBuffer(y, x) = 0
				Else
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, x)
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nPointsHF
	End Sub

	Private Sub PA_DoAction15()
		Dim y, x As Integer
		nStart = nPointsHF - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If y<nPointsHF - nStart + 1 Or y>nPointsHF + nStart - 2 Then
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, x)
				Else
					A_nDisplayBuffer(y, x) = 0
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nPointsHF
	End Sub

	Private Sub PA_DoAction16()
		Dim y, x As Integer
		nStart = nPoints - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If y<nStart1 Then
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, x)
				Else
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(nStart1 + Int((y - nStart1) / 2), x)
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nPoints
	End Sub

	Private Sub PA_DoAction17()
		Dim y, x As Integer
		nStart = nPoints - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If y<nStart Then
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(nStart1 + Int((nStart - y) / 2), x)
				Else
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, x)
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nPoints
	End Sub

	Private Sub PA_DoAction18()
		Dim y, x As Integer
		nStart = nPoints - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If y<nStart1 Then
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, x)
				Else
					Dim y1 As Integer = nStart1 + Int((y - nStart1) / 2)
					A_nDisplayBuffer(y, x) = IIF((y1 Mod 2=0) Xor (x Mod 2=0), A_nMatrixBuffer(y1, x), 0)
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nPoints
	End Sub

	Private Sub PA_DoAction19()
		Dim y, x As Integer
		nStart = nPoints - nStart1
		For y = 0 To nPoints - 1
			For x = 0 To nCountX - 1
				If y<nStart Then
					Dim y1 As Integer = nStart1 + Int((nStart - y) / 2)
					A_nDisplayBuffer(y, x) = IIF((y1 Mod 2=0) Xor (x Mod 2=0), A_nMatrixBuffer(y1 , x), 0)
				Else
					A_nDisplayBuffer(y, x) = A_nMatrixBuffer(y, x)
				End If
			Next
		Next
		nStart1 = (nStart1 + 1) Mod nPoints
	End Sub

	Shared Sub Main()
		Application.Run(New PaDisplayHZ2016VB20())
	End Sub
End Class