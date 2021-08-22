using System;

namespace ArrayHelper
{
    public static class SortLibrary
    {
        public static void BubbleSort(int[] array, bool asc)
        {
            if (array != null && array.Length > 0)
            {
                int temp;
                if (asc)
                {
                    for (int i = 0; i < array.Length - 1; i++)
                    {
                        for (int j = i + 1; j < array.Length; j++)
                        {
                            if (array[i] > array[j])
                            {
                                temp = array[j];
                                array[j] = array[i];
                                array[i] = temp;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < array.Length - 1; i++)
                    {
                        for (int j = i + 1; j < array.Length; j++)
                        {
                            if (array[i] < array[j])
                            {
                                temp = array[j];
                                array[j] = array[i];
                                array[i] = temp;
                            }
                        }
                    }
                }
            }
            else return;
        }
    }
}