' vb-radix-sort-demo-1.vb

Class LsdRedixSortDemo1
    Private Const dn As Integer = 3
    Private Const k As Integer = 10
    Private C(k) As Integer
    Private radix() As Integer = {1, 1, 10, 100}

    Shared Sub Main()
        ' Dim data() As Integer = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36}
        Dim data() As Integer = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82}

        Dim lrsd As New LsdRedixSortDemo1()
        lrsd.DisplayData(data)
        lrsd.LsdRedixSort(data)
        lrsd.DisplayData(data)
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

    Private Function GetDigit(x As Integer, d As Integer) As Integer
        return Int(x / radix(d)) Mod 10
    End Function

    Public Sub LsdRedixSort(data As Integer())
        Dim n As Integer = data.Length
        ' LsdRedixSort
        Dim i As Integer, d As Integer
        For d = 1 To dn - 1
            ' CountingSort
            For i = 0 To k - 1
                C(i) = 0
            Next
            For i = 0 To n - 1
                C(GetDigit(data(i), d)) += 1
            Next
            For i = 1 To k - 1
                C(i) += C(i - 1)
            Next
            Dim B(n) As Integer
            For i = n - 1 To 0 Step -1
                Dim dight As Integer = GetDigit(data(i), d)
                C(dight) -= 1
                B(C(dight)) = data(i)
            Next
            For i = 0 To n - 1
                data(i) = B(i)
            Next
        Next
    End sub
End Class