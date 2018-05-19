' vb-bucket-sort-demo-1.vb
Imports System

Class BucketSortDemo1
    Private Const MAX As Integer = 99
    Private Const bn As Integer = 5
    Private nFactor As Integer = Iif(MAX Mod bn = 0, MAX / bn, MAX / bn + 1)
    Private C(bn - 1) As Integer

    Shared Sub Main()
        ' Dim data() As Integer = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36}
        Dim data As Integer() = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82}
        Dim bsd As New BucketSortDemo1()
 
        bsd.DisplayData(data)
        bsd.BucketSort(data)
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

    Private Sub InsertionSort(data As Integer(), nLeft As Integer, nRight As Integer)
        Dim i As Integer
        Dim j As Integer
        For i = nLeft + 1 To nRight
            Dim nGet As Integer = data(i)
            For j = i - 1 To nLeft Step -1
                If data(j) <= nGet Then Exit For
                data(j + 1) = data(j)
            Next
            data(j + 1) = nGet
        Next
    End Sub

    Private Function MapToBucket(x As Integer) As Integer
        Return Int(x / nFactor)
    End Function

    Private Sub CountingSort(data As Integer())
        Dim i As Integer
        Dim b1 As Integer
        Dim n As Integer = data.Length
        For i = 0 To bn - 1
            C(i) = 0
        Next
        For i = 0 To n - 1
            b1 = MapToBucket(data(i))
            C(b1) = C(b1) + 1
        Next
        For i = 1 To bn - 1
            C(i) = C(i) + C(i - 1)
        Next
        Dim B(n - 1) As Integer
        For i = n - 1 To 0 Step -1
            b1 = MapToBucket(data(i))
            C(b1) = C(b1) - 1
            B(C(b1)) = data(i)
        Next
        For i = 0 To n - 1
            data(i) = B(i)
        Next
    End Sub

    Public Sub BucketSort(data As Integer())
        Dim i As Integer
        Dim n As Integer = data.Length

        CountingSort(data)
        For i = 0 To bn - 1
            Dim nLeft As Integer = C(i)
            Dim nRight As Integer
            If i = bn - 1 Then
                nRight = n - 1
            Else
                nRight = C(i + 1) - 1
            End If
            If nLeft < nRight Then
                InsertionSort(data, nLeft, nRight)
            End If
        Next
    End Sub
End Class