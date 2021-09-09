using System;
using System.Collections.Generic;
using System.IO;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            string testString = "  ,.affd,affd,afsd.....dffsa    .dsffa,, dffsd";
            Console.WriteLine($"Average length of word in string [{testString}] is {WordCounter.AvgWordLength(testString)}{Environment.NewLine}"); // First task

            string s = "shrek one love";
            string s2 = "sorry";
            Console.WriteLine($"Before editing line is [{s}], with characters from line [{s2}]. {Environment.NewLine}New line is [{DoubleLetter.DoubleLetters(s, s2)}] {Environment.NewLine}"); //Second task
           
            string first = "44444444445";
            string second = "55555555555";
            Console.WriteLine($"Sum of [{first}] and [{second}] is [{SumClass.SumOfStrings(first, second)}] {Environment.NewLine}"); //Third task

            string str = "Hello my name is Dima";
            Console.WriteLine($"Line before editing is [{str}], and after edit it's [{WordReverser.Reverse(str)}]"); // Fourth task

           
            string pathToRead = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Text.txt");
            string pathToWrite = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Numbers.txt");
            
        }
    }
}
