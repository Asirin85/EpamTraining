using System;
using ArrayHelper;
using RectangleHelper;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //RectangleLibrary check
            int widthOfRectangle = 6;
            int lengthOfRectangle = 10;
            int perimeter = RectangleLibrary.Perimeter(widthOfRectangle, lengthOfRectangle);
            int square = RectangleLibrary.Square(widthOfRectangle, lengthOfRectangle);
            if (perimeter == -1 || square == 1)
            {
                Console.WriteLine("Wrong rectangle width or length. Width = {0}, Length = {1}. \n", widthOfRectangle, lengthOfRectangle);
            }
            else
            {
                Console.WriteLine("Width and Length are: {0}, {1}. Perimeter is: {2}, Square is: {3}. \n", widthOfRectangle, lengthOfRectangle, perimeter, square);
            }

            //SortLibrary check
            const bool IS_ASC = true;
            const bool IS_DESC = false;
            int[] arrayForAsc = new int[] { 0, 2, 1, 22, 3, 15, 5, -1 };
            int[] arrayForDesc = new int[] { 0, 2, 1, 22, 3, 15, 5, -1 };
            if (arrayForAsc != null)
                Console.WriteLine("Unsorted array is: [{0}]", string.Join(", ", arrayForAsc));
            else Console.WriteLine("Error, array is null");
            SortLibrary.BubbleSort(arrayForAsc, IS_ASC);
            SortLibrary.BubbleSort(arrayForDesc, IS_DESC);
            if (arrayForAsc != null)
                Console.WriteLine("Array sorted by asc is: [{0}]", string.Join(", ", arrayForAsc));
            else Console.WriteLine("Error, array is null");
            if (arrayForDesc != null)
                Console.WriteLine("Array sorted by desc is: [{0}]\n", string.Join(", ", arrayForDesc));
            else Console.WriteLine("Error, array is null");

            //SumLibrary check
            int[,] array2D = new int[,] { { 1, 2, 4 }, { 5, -1, 0 }, { -2, -8, -1 } };
            int sumOfPositives = SumLibrary.SumPositive(array2D);
            if (sumOfPositives == -1)
            {
                Console.WriteLine("Array is null or empty");
            }
            else
            {
                Console.WriteLine("Matrix is:");
                for (int i = 0; i < array2D.GetLength(0); i++)
                {
                    for (int j = 0; j < array2D.GetLength(1); j++)
                    {
                        Console.Write(string.Format("{0} ", array2D[i,j]));
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Sum of positives is: {0}", sumOfPositives);
            }
            Console.ReadKey();
        }
    }
}
