using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DoubleLetter
    {
        public static string DoubleLetters(string input, string charToDouble)
        {
            if (input is { Length: > 0 } && charToDouble is { Length: > 0 })
            {
                HashSet<char> charSet = new HashSet<char>();
                for (int i = 0; i < charToDouble.Length; i++)
                {
                    if (!charSet.Contains(charToDouble[i]))
                    {
                        charSet.Add(charToDouble[i]);
                    }
                }
                StringBuilder sb = new StringBuilder(input);
                foreach (char c in charSet)
                {
                    sb.Replace($"{c}", $"{c}{c}");
                }
                return sb.ToString();
            }
            else throw new ArgumentException("Wrong input data, either null or empty");
        }
    }
}
