' vb-heap-sort-demo-1.vb

Class HeapSortDemo1
    Shared Sub Main()
        ' Dim data() As Integer = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36}
        Dim data() As Integer = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82}
        Dim hsd As New HeapSortDemo1()

        hsd.DisplayData(data)
        hsd.HeapSort(data)
        hsd.DisplayData(data)
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

    Private Sub Swap(data As Integer(), i As Integer, j As Integer)
        Dim temp As Integer = data(i)
        data(i) = data(j)
        data(j) = temp
    End Sub

    Private Sub Heapify(data As Integer(), i As Integer, nSize As Integer)
        Dim nLeftChild As Integer = 2 * i + 1
        Dim nRightChild As Integer = 2 * i + 2
        Dim nMax As Integer = i
        If nLeftChild < nSize Then
            If data(nLeftChild) > data(nMax) Then
                nMax = nLeftChild
            End If
        End If
        If nRightChild < nSize Then
            If data(nRightChild) > data(nMax) Then
                nMax = nRightChild
            End If
        End If
        If nMax <> i Then
            Swap(data, i, nMax)
            Heapify(data, nMax, nSize)
        End If
    End Sub

    Public Sub HeapSort(data As Integer())
        Dim nHeapSize As Integer = data.length
        ' BuildHeap
        Dim i As Integer
        For i = Int(nHeapSize / 2) - 1 To 0 Step -1
            Heapify(data, i, nHeapSize)
        Next

        ' HeapSort
        While nHeapSize > 1
            nHeapSize -= 1
            Swap(data, 0, nHeapSize)
            Heapify(data, 0, nHeapSize)
        End While
    End Sub
End Class