imports System
Imports System.Drawing
imports System.Windows.Forms

Class PaForm2016VB20 : Inherits Form
	Protected Overrides ReadOnly Property DefaultSize As Size
		Get
			Return New Size(600, 450)
		End Get
	End Property

	Sub New()
		Me.Text = "PaForm2016VB20"
		Me.StartPosition = FormStartPosition.CenterScreen
		Me.Visible = True
		Me.ClientSize = New Size(600, 450)
		Me.BackColor = Color.Black
	End Sub

	'Public Sub PaForm2016VB20_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
	'End Sub

	Shared Sub Main
		Application.Run(New PaForm2016VB20())
	End Sub
End Class 