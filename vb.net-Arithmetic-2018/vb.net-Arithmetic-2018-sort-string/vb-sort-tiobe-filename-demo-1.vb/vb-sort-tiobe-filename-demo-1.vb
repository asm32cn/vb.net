' vb-sort-tiobe-filename-demo-1.vb
Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Class vb_sort_tiobe_filename_demo_1
	'Delegate Function _GetValue(ByVal i As Integer) As Integer
	Dim _month As New Dictionary(Of String, Integer)

	Sub New()
		_month.Add("January", 1)
		_month.Add("February", 2)
		_month.Add("March", 3)
		_month.Add("April", 4)
		_month.Add("May", 5)
		_month.Add("June", 6)
		_month.Add("July", 7)
		_month.Add("August", 8)
		_month.Add("September", 9)
		_month.Add("October", 10)
		_month.Add("November", 11)
		_month.Add("December", 12)
	End Sub

	Shared Sub Main
		Dim A_strKeys As String() = New String() { _
			"TIOBE Index for April 2018.html", _
			"TIOBE Index for February 2018.html", _
			"TIOBE Index for January 2018.html", _
			"TIOBE Index for June 2018.html", _
			"TIOBE Index for March 2018.html", _
			"TIOBE Index for May 2018.html", _
			"TIOBE-exchange-matrix-data.html", _
			"TIOBE-exchange-matrix-data.py", _
			"TIOBE-gernate-index-py2.py", _
			"TIOBE-index.html", _
			"TIOBE_matrixData.txt" }
		Dim nCount As Integer = A_strKeys.Length
		Dim strFormat As String = "{0,-2} {1,-36} {2,-36}"

		Dim i As Integer
		Dim stfd As New vb_sort_tiobe_filename_demo_1()

		Dim sorted As String() = stfd.sort_tiobe_filename_demo(A_strKeys)

		Console.WriteLine ( String.Format(strFormat, "@", "Source data", "Sorted data") )
		Console.WriteLine ( String.Format(strFormat, "==", "==================", "==================") )
		For i = 0 To nCount - 1
			Console.WriteLine ( String.Format(strFormat, i, A_strKeys(i), sorted(i) ) )
		Next
	End Sub

	Public Function sort_tiobe_filename_demo(files As String()) As String()
		Dim nCount As Integer = files.Length
		Dim sorted(nCount - 1) As String
		Dim buff(nCount - 1, 2) As Integer
		Dim i As Integer, j As Integer, n As Integer = 0

		Dim regex As New Regex("^TIOBE Index for (\w+) (\d{4})\.html$")
		' init buff
		For i = 0 To nCount - 1
			buff(i, 0) = i
			Dim nDatespan As Integer = 0
			Dim ms As MatchCollection = regex.Matches(files(i))
			If ms.Count = 1 Then
				If ms(0).Groups.Count = 3 Then
					Dim nMonth As Integer = 0
					_month.TryGetValue(ms(0).Groups(1).ToString(), nMonth)
					nDatespan = Convert.ToInt32(ms(0).Groups(2).ToString()) * 100 + nMonth
				End If
			End If
			If nDatespan = 0 Then
				n += 1
				nDatespan = n
			End If
			buff(i, 1) = nDatespan
		Next

		' insertion sort
		For i = 0 To nCount - 2
			Dim nMin As Integer = i
			For j = i + 1 To nCount - 1
				If buff(buff(j, 0), 1) < buff(buff(nMin, 0), 1) Then
					nMin = j
				End If
			Next
			If i <> nMin Then
				Dim t As Integer = buff(i, 0)
				buff(i, 0) = buff(nMin, 0)
				buff(nMin, 0) = t
			End If
		Next

		' generate result
		For i = 0 To nCount - 1
			sorted(i) = files(buff(i, 0))
		Next

		Return sorted

	End Function

End Class

'@  Source data                          Sorted data
'== ==================                   ==================
'0  TIOBE Index for April 2018.html      TIOBE-exchange-matrix-data.html
'1  TIOBE Index for February 2018.html   TIOBE-exchange-matrix-data.py
'2  TIOBE Index for January 2018.html    TIOBE-gernate-index-py2.py
'3  TIOBE Index for June 2018.html       TIOBE-index.html
'4  TIOBE Index for March 2018.html      TIOBE_matrixData.txt
'5  TIOBE Index for May 2018.html        TIOBE Index for January 2018.html
'6  TIOBE-exchange-matrix-data.html      TIOBE Index for February 2018.html
'7  TIOBE-exchange-matrix-data.py        TIOBE Index for March 2018.html
'8  TIOBE-gernate-index-py2.py           TIOBE Index for April 2018.html
'9  TIOBE-index.html                     TIOBE Index for May 2018.html
'10 TIOBE_matrixData.txt                 TIOBE Index for June 2018.html
