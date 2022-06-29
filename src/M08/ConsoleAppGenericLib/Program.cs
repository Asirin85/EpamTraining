using GenericLibrary;
using System;
using System.Collections.Generic;

namespace ConsoleAppGenericLib
{
    class Program
    {
        static void Main()
        {
            var gLib = new GenericLib();
            //Task 2
            Console.WriteLine($"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~{Environment.NewLine}Task 2{Environment.NewLine}~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            var strArray = new string[] { "001", "005", "010", "002", "004" };
            Array.Sort(strArray);
            var intArray = new int[] { 10, 5, 2, 8, 6 };
            Array.Sort(intArray);

            foreach (string str in strArray)
            {
                Console.Write($"{str} ");
            }
            Console.WriteLine($"{Environment.NewLine}Index of 010 is {GenericLib.GenericBinarySearch(strArray, "010")}");
            foreach (int i in intArray)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine($"{Environment.NewLine}Index of 8 is {GenericLib.GenericBinarySearch(intArray, 8)}");
            //Task 3
            Console.WriteLine($"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~{Environment.NewLine}Task 3{Environment.NewLine}~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            var fibonacci = gLib.Fibonacci().GetEnumerator();
            for (int i = 0; i < 10; i++)
            {
                fibonacci.MoveNext();
                Console.WriteLine(fibonacci.Current);
            }
            //Task 4
            Console.WriteLine($"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~{Environment.NewLine}Task 4{Environment.NewLine}~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            var stcList = new StackOnList<int>(new[] {1,2,3,4,5,6 });
            stcList.Push(7);
            Console.WriteLine(stcList.Peek());
            Console.WriteLine(stcList.Pop());
            foreach(var i in stcList)
            {
                Console.Write($"{i} ");
            }
        }
    }
}
