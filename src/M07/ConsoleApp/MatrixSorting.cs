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
                orderType = CheckForMin(orderType, compType);
                BubbleSort(matrix, Order(orderType), Comparison(compType));
            }
            else throw new ArgumentException("Matrix is null or empty, check your inputs.");
        }
        private OrderType CheckForMin(OrderType orderType, ComparisonType compType)
        {
            if (compType is ComparisonType.Min)
            {
                if (orderType is OrderType.Asc) orderType = OrderType.Desc;
                else if (orderType is OrderType.Desc) orderType = OrderType.Asc;
                else throw new ArgumentException("Wrong order type");
            }
            return orderType;
        }
        private Func<int[], int> Comparison(ComparisonType type) => type switch
        {
            ComparisonType.Sum => row => row.Sum(),
            ComparisonType.Max => row => row.Max(),
            ComparisonType.Min => row => row.Min(),
            _ => throw new ArgumentException("No such comparison type")
        };
        private int[] GetRow(int[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
        private void BubbleSort(int[,] matrix, Func<int, int, bool> compare, Func<int[], int> rowFunc)
        {
            _arrayForHelpingSort = new int[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                _arrayForHelpingSort[i] = rowFunc(GetRow(matrix, i));
            }
            for (int i = 0; i < _arrayForHelpingSort.Length - 1; i++)
            {
                for (int j = i + 1; j < _arrayForHelpingSort.Length; j++)
                {
                    if (compare(_arrayForHelpingSort[i], _arrayForHelpingSort[j]))
                    {
                        int temp = _arrayForHelpingSort[i];
                        _arrayForHelpingSort[i] = _arrayForHelpingSort[j];
                        _arrayForHelpingSort[j] = temp;
                        SwapRows(matrix, i, j);
                    }
                }
            }
        }
        private Func<int, int, bool> Order(OrderType orderType) => orderType switch
        {
            OrderType.Asc => (x, y) => x < y,
            OrderType.Desc => (x, y) => x > y,
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
