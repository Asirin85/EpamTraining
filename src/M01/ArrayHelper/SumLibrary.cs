using System;

namespace ArrayHelper
{
    public static class SumLibrary
    {
        public static int SumPositive(int[,] array)
        {
            if (array!=null && array.Length > 0)
            {
                int sum = 0;
                foreach (var number in array)
                {
                    if (number > 0) sum += number;
                }
                return sum;
            }
            else return -1;
        }
    }
}
