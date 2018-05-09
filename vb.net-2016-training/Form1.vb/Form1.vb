Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class Form1 : Inherits Form

	Protected Overrides ReadOnly Property DefaultSize As Size
		Get
			Return New Size(600, 450)
		End Get
	End Property

	Sub New()
		Me.Text = "Form1"
		'Me.ClientSize = New Size(600, 450)
		Me.StartPosition = FormStartPosition.CenterScreen
		Me.BackColor = Color.Black

		Dim lblShow As New Label()
		lblShow.Location = New Point(50, 60)
		lblShow.ForeColor = Color.White
		lblShow.AutoSize = True
		lblShow.Text = "lbl≤‚ ‘"

		Controls.Add(lblShow)
	End Sub

	'Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
	'End Sub

	Sub Form1_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles MyBase.Paint
		Dim g As Graphics
		Dim p As New Pen(Color.Blue, 2)
		g = e.Graphics
		g.DrawLine(p, 10, 10, 100, 100)
		g.DrawRectangle(p, 10, 10, 100, 100)
		g.DrawEllipse(p, 10, 10, 100, 100)
	End Sub

	Shared Sub Main
		Application.Run(New Form1())
	End Sub

End Class
