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
            return Name.Equals((obj as Student).Name) && Test.Equals((obj as Student).Test) && Date.Equals((obj as Student).Date) && Mark == (obj as Student).Mark;
        }
    }
}
