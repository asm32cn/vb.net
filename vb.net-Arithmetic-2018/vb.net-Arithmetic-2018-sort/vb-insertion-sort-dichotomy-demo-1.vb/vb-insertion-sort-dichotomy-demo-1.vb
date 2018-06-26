' vb-insertion-sort-dichotomy-demo-1.vb

Class InsertionSortDichotomyDemo1
    Shared Sub Main()
        ' Dim data() As Integer = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36}
        Dim data() As Integer = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82}
        Dim isdd As New InsertionSortDichotomyDemo1()

        isdd.DisplayData(data)
        isdd.InsertionSortDichotomy(data)
        isdd.DisplayData(data)
    End Sub

    Public Sub DisplayData(data As Integer())
        Dim n As Integer = data.Length
        Dim i As Integer
        For i = 0 To n - 1
            If i > 0 Then Console.Write(", ")
            Console.Write(data(i))
        Next
        Console.WriteLine
    End Sub

    Public Sub InsertionSortDichotomy(data As Integer())
        Dim n As Integer = data.Length
        Dim i As Integer, j As Integer
        For i = 1 To n - 1
            Dim nGet As Integer = data(i)
            Dim nLeft As Integer = 0
            Dim nRight As Integer = i - 1
            While nLeft <= nRight
                Dim nMid As Integer = Int( (nLeft + nRight) / 2 )
                If data(nMid) > nGet Then
                    nRight = nMid - 1
                Else
                    nLeft = nMid + 1
                End If
            End While
            For j = i - 1 To nLeft Step -1
                data(j + 1) = data(j)
            Next
            data(nLeft) = nGet
        Next
    End Sub
End Class