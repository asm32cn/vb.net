' vb-insertion-sort-demo-1.vb

Class InsertionSortDemo1
    Shared Sub Main()
        ' Dim data() As Integer = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36}
        Dim data() As Integer = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82}

        Dim isd As New InsertionSortDemo1()
        isd.DisplayData(data)
        isd.InsertionSort(data)
        isd.DisplayData(data)
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

    Public Sub InsertionSort(data As Integer())
        Dim n As Integer = data.Length
        Dim i As Integer, j As Integer
        For i = 1 To n - 1
            Dim nGet As Integer = data(i)
            For j = i - 1 To 0 Step -1
                If data(j) <= nGet Then Exit For
                data(j + 1) = data(j)
            Next
            data(j + 1) = nGet
        Next
    End Sub
End Class