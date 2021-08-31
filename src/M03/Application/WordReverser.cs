using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    static class WordReverser
    {
        public static string Reverse(string input)
        {
            if (input is { Length: > 0 })
            {
                string[] array = input.Split(' ');
                StringBuilder sb = new StringBuilder(input.Length);
                for (int i = array.Length - 1; i >= 0; i--)
                {
                    sb.Append(array[i] + " ");
                }
                return sb.ToString().Trim();
            }
            else throw new ArgumentException("Input string is null or empty");
        }
    }
}
