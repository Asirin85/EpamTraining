using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    static class DoubleLetter
    {
        public static string DoubleLetters(string input, string charToDouble)
        {
            string newString = input;
            List<char> list = new List<char>();
            for (int i = 0; i < charToDouble.Length; i++)
            {
                if (!list.Contains(charToDouble[i]))
                {
                    list.Add(charToDouble[i]);
                }
            }
            foreach (char c in list)
            {
                newString = newString.Replace(c.ToString(), $"{c}{c}");
            }
            return newString;
        }
    }
}
