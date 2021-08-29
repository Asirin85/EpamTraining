using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application
{
    static class PhoneSelector
    {
        public static List<string> FindAndWrite(string pathToRead, string pathToWrite)
        {
            if (!File.Exists(pathToRead))
            {
                throw new ArgumentException("Wrong input path");
            }
            List<string> listOfNumbers;
            using(StreamReader sr = File.OpenText(pathToRead))
            {
                string text = sr.ReadToEnd();
                var regex = new Regex(@"(((\+[1-9]\s\([0-9]{3}\))|([1-9]\s[0-9]{3}))\s[0-9]{3}\-[0-9]{2}\-[0-9]{2})|(\+[0-9]{3}\s\([0-9]{2}\)\s[0-9]{3}\-[0-9]{4})");
                var matchCollection = regex.Matches(text);
                listOfNumbers = matchCollection.Cast<Match>().Select(match => match.Value).ToList();
            }
            if (listOfNumbers != null && listOfNumbers.Count > 0)
            {
                using(StreamWriter sw = File.CreateText(pathToWrite))
                {
                    foreach(string number in listOfNumbers)
                    {
                        sw.WriteLine(number);
                    }
                }
            }
            else Console.WriteLine("No phone numbers were found");
            return listOfNumbers;
        }
    }
}
