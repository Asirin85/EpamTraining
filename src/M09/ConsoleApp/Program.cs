using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "testdata.json");
            IReadOnlyCollection<Student> students = JsonReader.ReadFromJson(path);
            var dataProc = new LinqDataProcesser(students);
            Console.WriteLine("Write your arguments (-name, -minmark, -maxmark, -datefrom, -dateto, -test, -sort): ");
            string arguments = Console.ReadLine();
            List<Student> result = dataProc.GetData(ArgsParser.ParseArgs(arguments));
            foreach(var student in result)
            {
                Console.WriteLine($"{student.Name} {student.Test} {student.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)} {student.Mark}");
            }
        }
    }
}
