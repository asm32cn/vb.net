' vb-selectionsort-demo-1.vb

Class SelectionSortDemo1
    Shared Sub Main()
        ' Dim data() As Integer = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36}
        Dim data() As Integer = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82}

        Dim ssd As New SelectionSortDemo1()
        ssd.DisplayData(data)
        ssd.SelectionSort(data)
        ssd.DisplayData(data)
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

    Public Sub SelectionSort(data As Integer())
        Dim n As Integer = data.Length
        Dim i As Integer, j As Integer, temp As Integer
        For i = 0 To n - 2
            Dim nMin As Integer = i
            For j = i + 1 To n - 1
                If data(j) < data(nMin) Then
                    nMin = j
                End If
            Next
            If nMin <> i Then
                temp = data(nMin)
                data(nMin) = data(i)
                data(i) = temp
            End If
        Next
    End Sub
End Class