' vb-cocktailsort-demo-1.vb

Class CocktailSortDemo1
    Shared Sub Main()
        ' Dim data() As Integer = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36}
        Dim data() As Integer = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82}

        Dim csd As New CocktailSortDemo1()
        csd.DisplayData(data)
        csd.CocktailSort(data)
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

    Public Sub CocktailSort(data As Integer())
        Dim left As Integer = 0
        Dim right As Integer = data.Length - 1
        Dim i As Integer, temp As Integer
        While left < right
            For i = left To right - 1
                If data(i) > data(i + 1) Then
                    temp = data(i)
                    data(i) = data(i + 1)
                    data(i + 1) = temp
                End If
            Next
            right = right - 1
            For i = right To Left + 1 Step -1
                If data(i - 1) > data(i) Then
                    temp = data(i - 1)
                    data(i - 1) = data(i)
                    data(i) = temp
                End If
            Next
            left += 1
        End While
    End Sub
End Class