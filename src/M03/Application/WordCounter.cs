using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    static class WordCounter
    {
        public static double AvgWordLength(string input)
        {
            if (input is { Length: > 0 })
            {
                double average = 0;
                int wordCount = 0;
                string trimmedInput = input.Trim();
                bool prevIsLetter = false;
                for (int i = 0; i < trimmedInput.Length; i++)
                {
                    if (char.IsLetterOrDigit(trimmedInput[i]))
                    {
                        average++;
                        prevIsLetter = true;
                    }
                    else
                    {
                        if (prevIsLetter)
                        {
                            wordCount++;
                            prevIsLetter = false;
                        }
                    }
                }
                if (prevIsLetter) wordCount++; // if last char was letter we add one more word
                if (wordCount > 0) average /= wordCount;
                return average;
            }
            else throw new ArgumentException("Input string is null or empty");
        }
    }
}
