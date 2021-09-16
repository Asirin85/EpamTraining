using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class MatrixSorting
    {
        private int[] _arrayForHelpingSort;
        public void SortMatrix(int[,] matrix, OrderType orderType, ComparisonType compType)
        {
            if (matrix is { Length: > 0 })
            {
                Action<int, int> sort;
                switch (compType)
                {
                    case ComparisonType.Sum:
                        _arrayForHelpingSort = new int[matrix.GetLength(0)];
                        sort = SortBySum;
                        break;
                    case ComparisonType.Max:
                        _arrayForHelpingSort = Enumerable.Repeat(int.MinValue, matrix.GetLength(0)).ToArray();
                        sort = SortByMax;
                        break;
                    case ComparisonType.Min:
                        if (orderType is OrderType.Asc) orderType = OrderType.Desc;
                        else if (orderType is OrderType.Desc) orderType = OrderType.Asc;
                        _arrayForHelpingSort = Enumerable.Repeat(int.MaxValue, matrix.GetLength(0)).ToArray();
                        sort = SortByMin;
                        break;
                    default:
                        throw new ArgumentException("Wrong comparison type");
                }
                BubbleSort(matrix, orderType, sort);
            }
            else throw new ArgumentException("Wrong matrix");
        }
        private void SortBySum(int matrixElement, int index)
        {
            _arrayForHelpingSort[index] += matrixElement;
        }
        private void SortByMax(int matrixElement, int index)
        {
            if (matrixElement > _arrayForHelpingSort[index]) _arrayForHelpingSort[index] = matrixElement;
        }
        private void SortByMin(int matrixElement, int index)
        {
            if (matrixElement < _arrayForHelpingSort[index]) _arrayForHelpingSort[index] = matrixElement;
        }
        private void BubbleSort(int[,] matrix, OrderType orderType, Action<int, int> fillArray)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    fillArray(matrix[i, j], i);
                }
            }
            for (int i = 0; i < _arrayForHelpingSort.Length - 1; i++)
            {
                for (int j = i + 1; j < _arrayForHelpingSort.Length; j++)
                {
                    if (CheckOrder(orderType, _arrayForHelpingSort, i, j))
                    {
                        int temp = _arrayForHelpingSort[i];
                        _arrayForHelpingSort[i] = _arrayForHelpingSort[j];
                        _arrayForHelpingSort[j] = temp;
                        SwapRows(matrix, i, j);
                    }
                }
            }
        }
        private bool CheckOrder(OrderType orderType, int[] array, int firstIndex, int secondIndex) => orderType switch
        {
            OrderType.Asc => array[firstIndex] < array[secondIndex],
            OrderType.Desc => array[firstIndex] > array[secondIndex],
            _ => throw new ArgumentException("Wrong order type")
        };
        public void SwapRows(int[,] matrix, int rowOne, int rowTwo)
        {
            if (matrix is { Length: > 0 } && rowOne >= 0 && rowTwo >= 0 && rowOne != rowTwo && matrix.GetLength(0) > rowOne && matrix.GetLength(0) > rowTwo)
            {
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    int temp = matrix[rowOne, i];
                    matrix[rowOne, i] = matrix[rowTwo, i];
                    matrix[rowTwo, i] = temp;
                }
            }
            else throw new ArgumentException("Wrong input data in SwapRows");
        }
    }
}
