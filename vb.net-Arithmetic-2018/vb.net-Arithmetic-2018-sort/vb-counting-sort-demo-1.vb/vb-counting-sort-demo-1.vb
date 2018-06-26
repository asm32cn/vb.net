' vb-counting-sort-demo-1.vb

Class CountingSortDemo1
    Private Const k As Integer = 100
    Private C(k) As Integer

    Shared Sub Main()
        ' Dim data() As Integer = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36}
        Dim data() As Integer = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82}
        Dim csd As New CountingSortDemo1()

        csd.DisplayData(data)
        csd.CountingSort(data)
        csd.DisplayData(data)
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

    Sub CountingSort(data As integer())
        Dim n As Integer = data.Length
        Dim i As Integer
        For i = 0 To k - 1
            C(i) = 0
        Next
        For i = 0 To n - 1
            C(data(i)) += 1
        Next
        For i = 1 To k - 1
            C(i) += C(i - 1)
        Next
        Dim B(n) As Integer
        For i = n - 1 To 0 Step -1
            C(data(i)) -= 1
            B(C(data(i))) = data(i)
        Next
        For i = 0 To n - 1
            data(i) = B(i)
        Next
    End Sub
End Class