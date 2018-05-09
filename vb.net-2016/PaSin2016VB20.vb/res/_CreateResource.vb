Imports System
Imports System.IO
Imports System.Drawing
Imports System.Resources

Class _CreateResource
	Shared Sub Main()
		Dim strCrlf As String = Chr(10) + Chr(13)
		Dim strOutputFile As String = Nothing
		Dim isReady As Boolean = False
		Try
			Dim strFile_xml As String = "_CreateResource.xml"
			Dim xtr As New System.Xml.XmlTextReader(strFile_xml)
			Do While xtr.Read()
				If xtr.Name.Equals("strOutputFile") Then
					strOutputFile = xtr.GetAttribute("strFile")
					isReady = True
					Exit Do
				End If
			Loop
			If isReady Then
				Dim rw As New ResourceWriter(strOutputFile)
				Console.Write("OutputFile: " + strOutputFile + strCrlf)
				Console.Write("------------------------------" + strCrlf)
				While xtr.Read()
					Dim strTypeName As String = xtr.Name
					Dim strItemName, strResource As String
					If strTypeName.Equals("Icon") Then
						strItemName = xtr.GetAttribute("strFile")
						strResource = xtr.GetAttribute("strResourceName")
						Dim ico1 As New Icon(strItemName)
						rw.AddResource(strResource, ico1)
						Console.Write(strTypeName + ": """ + strItemName + """ => """ + strResource + """" + strCrlf)
					ElseIf strTypeName.Equals("Image") Then
						strItemName = xtr.GetAttribute("strFile")
						strResource = xtr.GetAttribute("strResourceName")
						Dim image1 As Image = Image.FromFile(strItemName)
						rw.AddResource(strResource, image1)
						Console.Write(strTypeName + ": """ + strItemName + """ => """ + strResource + """" + strCrlf)
					ElseIf strTypeName.Equals("Data") Then
						strItemName = xtr.GetAttribute("strFile")
						strResource = xtr.GetAttribute("strResourceName")
						Dim fs As New FileStream(strItemName, FileMode.Open, FileAccess.Read, FileShare.Read)
						Dim nFileLength As Integer = fs.Length
						Dim br As New BinaryReader(fs)
						Dim byteBuffer() As Byte = br.ReadBytes(nFileLength)
						If byteBuffer.Length=nFileLength Then
							rw.AddResource(strResource, byteBuffer)
						Else
							Console.Write("Read fail." + strCrlf)
						End If
						br.Close()
						fs.Close()
						Console.Write(strTypeName + ": """ + strItemName + """ => """ + strResource + """" + strCrlf)
					ElseIf strTypeName.Equals("String") Then
						strItemName = xtr.GetAttribute("strResourceName")
						strResource = xtr.GetAttribute("strContent")
						rw.AddResource(strItemName, strResource)
						Console.Write(strTypeName + ": """ + strItemName + """ => """ + strResource + """" + strCrlf)
					End If
				End While
				rw.Generate()
				rw.Close()
				Console.Write("------------------------------" + strCrlf)
				Console.Write("Done." + strCrlf)
			End If
			xtr.Close()
		Catch ex As Exception
			Console.Write("Exception: " + ex.Message + strCrlf)
		End Try
	End Sub
End Class