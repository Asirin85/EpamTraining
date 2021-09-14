using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ConsoleApp.Tests
{
    public class MatrixSortingTests
    {
        private static List<TestCaseData> _dataForMatrixSortingSwapRows = new List<TestCaseData>(new[]
        {
            new TestCaseData(new int[3,3]{ {1,1,1},{2,2,2},{3,3,3 } }, new int[3,3]{ {3,3,3 },{2,2,2 },{1,1,1 } },0,2),
        });

        [TestCaseSource(nameof(_dataForMatrixSortingSwapRows))]
        public void Test_For_MatrixSortingSwapRows(int[,] input, int[,] expectedResult, int rowOne, int rowTwo)
        {
            MatrixSorting ms = new MatrixSorting();
            ms.SwapRows(input, rowOne, rowTwo);
            Assert.That(input, Is.EqualTo(expectedResult));
        }
        private static List<TestCaseData> _dataForMatrixSortingSwapRows_WrongInput = new List<TestCaseData>(new[]
        {
            new TestCaseData(new int[3,3]{ {1,1,1},{2,2,2},{3,3,3 } }, 0, 0),
            new TestCaseData(new int[3,3]{ {1,1,1},{2,2,2},{3,3,3 } }, -1, 1),
            new TestCaseData(new int[3,3]{ {1,1,1},{2,2,2},{3,3,3 } }, 3, 1),
            new TestCaseData(new int[3,3]{ {1,1,1},{2,2,2},{3,3,3 } }, 1, 3),
            new TestCaseData(new int[3,3]{ {1,1,1},{2,2,2},{3,3,3 } }, 1, -1),
            new TestCaseData(null, 0, 1),
        });
        [TestCaseSource(nameof(_dataForMatrixSortingSwapRows_WrongInput))]
        public void Test_For_WrongInputMatrixSortingSwapRows(int[,] input, int rowOne, int rowTwo)
        {
            MatrixSorting ms = new MatrixSorting();
            Assert.That(() => ms.SwapRows(input, rowOne, rowTwo), Throws.TypeOf<ArgumentException>());
        }

        // Testing main sort
        private static List<TestCaseData> _dataForMatrixSorting= new List<TestCaseData>(new[]
        {
            new TestCaseData(new int[3,3]{ {1,1,1},{3,3,3},{2,2,2 } }, new int[3,3]{ {3,3,3 },{2,2,2 },{1,1,1 } }, OrderType.Asc, ComparisonType.Sum),
            new TestCaseData(new int[3,3]{ {1,1,1},{3,3,3},{2,2,2 } }, new int[3,3]{ {1,1,1 },{2,2,2 },{3,3,3 } }, OrderType.Desc, ComparisonType.Sum),
            new TestCaseData(new int[3,3]{ {1,1,1},{3,3,3},{2,2,2 } }, new int[3,3]{ {3,3,3 },{2,2,2 },{1,1,1 } }, OrderType.Asc, ComparisonType.Max),
            new TestCaseData(new int[3,3]{ {1,1,1},{3,3,3},{2,2,2 } }, new int[3,3]{ {1,1,1 },{2,2,2 },{3,3,3 } }, OrderType.Desc, ComparisonType.Max),
            new TestCaseData(new int[3,3]{ {1,1,1},{3,3,3},{2,2,2 } }, new int[3,3]{ {1,1,1 },{2,2,2 },{3,3,3 } }, OrderType.Asc, ComparisonType.Min),
            new TestCaseData(new int[3,3]{ {1,1,1},{3,3,3},{2,2,2 } }, new int[3,3]{ {3,3,3 },{2,2,2 },{1,1,1 } }, OrderType.Desc, ComparisonType.Min),
        });
        private static List<TestCaseData> _dataForMatrixSorting_WrongInput = new List<TestCaseData>(new[]
        {
            new TestCaseData(new int[3,3]{ {1,1,1},{3,3,3},{2,2,2 } },  2, ComparisonType.Sum),
            new TestCaseData(new int[3,3]{ {1,1,1},{3,3,3},{2,2,2 } },  OrderType.Desc, 3),
            new TestCaseData(null,  OrderType.Asc, ComparisonType.Max),
        });
        [TestCaseSource(nameof(_dataForMatrixSorting))]
        public void Test_For_MatrixSorting_MainSort(int[,] input, int[,] expectedResult, OrderType orderType, ComparisonType comparisonType)
        {
            MatrixSorting ms = new MatrixSorting();
            ms.SortMatrix(input, orderType, comparisonType);
            Assert.That(input, Is.EqualTo(expectedResult));
        }

        [TestCaseSource(nameof(_dataForMatrixSorting_WrongInput))]
        public void Test_For_MatrixSorting_MainSortWrongInput(int[,] input, OrderType orderType, ComparisonType comparisonType)
        {
            MatrixSorting ms = new MatrixSorting();
            Assert.That(() => ms.SortMatrix(input, orderType, comparisonType), Throws.TypeOf<ArgumentException>());
        }
    }
}