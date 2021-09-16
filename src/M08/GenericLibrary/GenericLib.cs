using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericLibrary
{
    public class GenericLib
    {
        public static int GenericBinarySearch<T>(T[] array, T toFind) where T : IComparable<T>
        {
            if (array is { Length: > 0 } && toFind is not null)
            {
                int min = 0;
                int max = array.Length - 1;
                while (min <= max)
                {
                    int mid = (min + max) / 2;
                    if (array[mid].Equals(toFind))
                    {
                        return mid;
                    }
                    else if (array[mid].CompareTo(toFind) > 0)
                    {
                        max = mid - 1;
                    }
                    else
                    {
                        min = mid + 1;
                    }
                }
            }
            else throw new ArgumentException("Wrong input");
            throw new ArgumentException("Item not found in array");
        }

        private int _firstFibb = 0;
        private int _secondFibb = 1;
        public IEnumerable Fibonacci()
        {
            yield return _firstFibb;
            yield return _secondFibb;
            while (true)
            {
                var nextNumber = checked(_firstFibb + _secondFibb);
                _firstFibb = _secondFibb;
                _secondFibb = nextNumber;
                yield return nextNumber;
            }
        }

        
    }
}
