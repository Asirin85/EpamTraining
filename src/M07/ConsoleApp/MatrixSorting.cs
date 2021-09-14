using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class MatrixSorting
    {
        public delegate void Sort(int[,] matrix, OrderType orderType);
        public void SortMatrix(int[,] matrix, OrderType orderType, ComparisonType compType)
        {
            if (matrix is { Length: > 0 })
            {
                Sort sort;
                switch (compType)
                {
                    case ComparisonType.Sum:
                        sort = SortBySum;
                        break;
                    case ComparisonType.Max:
                        sort = SortByMax;
                        break;
                    case ComparisonType.Min:
                        sort = SortByMin;
                        break;
                    default:
                        throw new ArgumentException("Wrong comparison type");
                }
                sort(matrix, orderType);
            }
            else throw new ArgumentException("Wrong matrix");
        }
        private void SortBySum(int[,] matrix, OrderType orderType)
        {
            int[] sumOfRow = new int[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    sumOfRow[i] += matrix[i, j];
                }
            }
            BubbleSort(matrix, sumOfRow, orderType);
        }
        private void SortByMax(int[,] matrix, OrderType orderType)
        {
            int[] maxInRow = new int[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                maxInRow[i] = int.MinValue;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] > maxInRow[i]) maxInRow[i] = matrix[i, j];
                }
            }
            BubbleSort(matrix, maxInRow, orderType);
        }
        private void SortByMin(int[,] matrix, OrderType orderType)
        {
            if (orderType is OrderType.Asc) orderType = OrderType.Desc;
            else if (orderType is OrderType.Desc) orderType = OrderType.Asc;
            int[] minInRow = new int[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                minInRow[i] = int.MaxValue;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] < minInRow[i]) minInRow[i] = matrix[i, j];
                }
            }
            BubbleSort(matrix, minInRow, orderType);
        }
        private void BubbleSort(int[,] matrix, int[] sortByArray, OrderType orderType)
        {
            for (int i = 0; i < sortByArray.Length - 1; i++)
            {
                for (int j = i + 1; j < sortByArray.Length; j++)
                {
                    if (CheckOrder(orderType, sortByArray, i, j))
                    {
                        int temp = sortByArray[i];
                        sortByArray[i] = sortByArray[j];
                        sortByArray[j] = temp;
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
