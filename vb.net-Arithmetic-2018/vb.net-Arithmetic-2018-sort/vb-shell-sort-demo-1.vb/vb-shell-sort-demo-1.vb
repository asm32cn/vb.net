' vb-shell-sort-demo-1.vb

Class ShellSortDemo1
    Shared Sub Main()
        ' Dim data() As Integer = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36}
        Dim data() As Integer = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82}

        Dim ssd As New ShellSortDemo1()
        ssd.DisplayData(data)
        ssd.ShellSort(data)
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

    Public Sub ShellSort(data As Integer())
        Dim n As Integer = data.Length
        Dim h As Integer = 0
        Dim i As Integer,j As Integer
        While h <= n
            h = 3 * h + 1
        End While
        While h >= 1
            For i = h To n - 1
                Dim nGet As Integer = data(i)
                For j = i - h To 0 Step -h
                    If data(j) <= nGet Then Exit For
                    data(j + h) = data(j)
                Next
                data(j + h) = nGet
            Next
            h = Int( (h - 1) / 3 )
        End While
    End Sub
End Class