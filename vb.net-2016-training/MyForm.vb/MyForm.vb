Imports System
Imports System.Drawing
Imports System.Windows.Forms

ClAss MyForm : Inherits Form
	Private box As TextBox
	Private WithEvents button As Button

	Sub New()
		me.Text = "MyForm"
		Me.StartPosition = FormStartPosition.CenterScreen
		'me.ClientSize = New Size(600, 450)
		me.Visible = True

		box = New Textbox()
		box.BackColor = Color.Cyan
		box.Location = New Point(50, 50)
		box.Text = "hello"
		button = New Button()
		button.Location = New Point(50, 100)
		button.Text = "Click me"
		controls.Add(box)
		controls.Add(button)
	End Sub

	'Sub MyForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBAse.Load
	'End Sub

	Private Sub button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button.Click
		box.BackColor = Color.Green
	End Sub
	
	Shared Sub Main
		Application.Run(New MyForm())
	End Sub
End ClAss 