' vb-quicksort-demo-1.vb

Class QuickSortDemo1
    Shared Sub Main()
        ' Dim data() As Integer = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36}
        Dim data() As Integer = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82}
        Dim qsd As New QuickSortDemo1()

        qsd.DisplayData(data)
        qsd.QuickSort(data, 0, data.Length - 1)
        qsd.DisplayData(data)
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

    Public Sub QuickSort(data As Integer(), nLeft As Integer, nRight As Integer)
        If nLeft < nRight Then
            Dim nKey As Integer = data(nLeft)
            Dim nLow As Integer = nLeft
            Dim nHigh As Integer = nRight
            While nLow < nHigh
                While nLow < nHigh And data(nHigh) >= nKey
                    nHigh -= 1
                End While
                data(nLow) = data(nHigh)
                While nLow < nHigh And data(nLow) <= nKey
                    nLow += 1
                End While
                data(nHigh) = data(nLow)
            End While
            data(nLow) = nKey

            QuickSort(data, nLeft, nLow - 1)
            QuickSort(data, nLow + 1, nRight)
        End If
    End Sub
End Class