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
            var dataProc = new LinqDataProcesser(path);
            Console.WriteLine("Write your arguments (-name, -minmark, -maxmark, -datefrom, -dateto, -test, -sort): ");
            string arguments = Console.ReadLine();
            List<Student> students = dataProc.GetData(arguments);
            var query = from student in students
                        where student.Name.Contains("")
                        select student;
            students = query.ToList();
            foreach(var student in students)
            {
                Console.WriteLine($"{student.Name} {student.Test} {student.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)} {student.Mark}");
            }
        }
    }
}
