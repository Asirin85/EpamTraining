using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Student
    {
        public string Name { get; set; }
        public string Test { get; set; }
        public DateTime Date { get; set; }
        public double Mark { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Student student &&
                   Name == student.Name &&
                   Test == student.Test &&
                   Date == student.Date &&
                   Mark == student.Mark;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Test, Date, Mark);
        }
    }
}
