using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericLibrary
{
    public class GenericLib
    {
        public static int GenericBinarySearch<T>(T[] array, T toFind) where T : IComparable<T>
        {
            if (array is not { Length: > 0 }) throw new ArgumentException(nameof(array));
            if (toFind is null) throw new ArgumentException(nameof(toFind));
            int min = 0;
            int max = array.Length - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (array[mid].CompareTo(toFind) == 0)
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
            throw new ArgumentException("Item was not found in array");
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
