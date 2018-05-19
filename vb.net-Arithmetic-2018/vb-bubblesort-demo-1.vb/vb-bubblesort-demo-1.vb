' vb-bubblesort-demo-1.vb

Class BubbleSortDemo1
    Shared Sub Main
        ' Dim data() As Integer = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36}
        Dim data() As Integer = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82}
        Dim bsd As New BubbleSortDemo1()

        bsd.DisplayData(data)
        bsd.BubbleSort(data)
        bsd.DisplayData(data)
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

    Public Sub BubbleSort(data As Integer())
        Dim n As Integer = data.Length
        Dim i As Integer, j As Integer, temp As Integer
        For j = 0 To n - 2
            For i = 0 To n - j - 2
                If data(i) > data(i + 1) Then
                    temp = data(i)
                    data(i) = data(i + 1)
                    data(i + 1) = temp
                End If
            Next
        Next
    End Sub
End Class