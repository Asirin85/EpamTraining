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
            if (obj is Student stud)
                return Name.Equals(stud.Name) && Test.Equals(stud.Test) && Date.Equals(stud.Date) && Mark == stud.Mark;
            return false;
        }
    }
}
