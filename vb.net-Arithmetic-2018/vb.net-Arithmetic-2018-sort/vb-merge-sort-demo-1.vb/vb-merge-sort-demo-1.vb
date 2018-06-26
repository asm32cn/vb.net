' vb-merge-sort-demo-1.vb
Imports System

Class MergeSortDemo1
	Shared Sub Main()
		' Dim data As Integer() = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36}
		Dim data1 As Integer() = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82}
		Dim data2 As Integer() = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82}

		Dim msd As New MergeSortDemo1()
		msd.DisplayData(data1)
		msd.MergeSortRecursion(data1, 0, data1.Length - 1)
		msd.DisplayData(data1)

		Console.WriteLine()
		msd.DisplayData(data2)
		msd.MergeSortIteration(data2)
		msd.DisplayData(data2)
	End Sub

	Public Sub DisplayData(data As Integer())
		Dim n As Integer = data.Length
		Dim i As Integer
		For i = 0 To n - 1
			If i > 0 Then Console.Write(", ")
			Console.Write(data(i))
		Next
		Console.WriteLine()
	End Sub

	Private Sub Merge(data As Integer(), nLeft As Integer, nMid As Integer, nRight As Integer)
		Dim nLen As Integer = nRight - nLeft + 1
		Dim temp(nLen - 1) As Integer
		Dim nIndex As Integer = 0
		Dim i As Integer = nLeft
		Dim j As Integer = nMid + 1
		While i <= nMid And j <= nRight
			If data(i) <= data(j) Then
				temp(nIndex) = data(i)
				i += 1
			Else
				temp(nIndex) = data(j)
				j += 1
			End If
			nIndex += 1
		End While
		While i <= nMid
			temp(nIndex) = data(i)
			nIndex += 1
			i += 1
		End While
		While j <= nRight
			temp(nIndex) = data(j)
			nIndex += 1
			j += 1
		End While
		Dim k As Integer
		For k = 0 To nLen - 1
			data(nLeft) = temp(k)
			nLeft += 1
		Next
	End Sub

	' 递归实现的归并排序(自顶向下)
	Public Sub MergeSortRecursion(data As Integer(), nLeft As Integer, nRight As Integer)
		' 当待排序的序列长度为1时，递归开始回溯，进行merge操作
		If nLeft = nRight Then Return

		Dim nMid As Integer = Int((nLeft + nRight) / 2)
		MergeSortRecursion(data, nLeft, nMid)
		MergeSortRecursion(data, nMid + 1, nRight)
		Merge(data, nLeft, nMid, nRight)
	End Sub

	' 非递归(迭代)实现的归并排序(自底向上)
	Public Sub MergeSortIteration(data As Integer())
		Dim n As Integer = data.Length
		' 子数组索引，前一个为A[left ... mid]，后一个为A[mid + 1 ... right]
		Dim nLeft As Integer, nMid As Integer, nRight As Integer
		Dim i As Integer = 1
		' 子数组的大小i初始为1，没轮翻倍
		While i < n
			nLeft = 0
			' 后一个子数组存在(需要归并)
			While nLeft + i < n
				nMid = nLeft + i - 1
				' 后一个子数组大小可能不够
				nRight = Iif(nMid + i < n, nMid + i, n - 1)
				' 前一个子数组索引向后移动
				Merge(data, nLeft, nMid, nRight)
				nLeft = nRight + 1
			End While
			i *= 2
		End While
	End Sub
End Class

