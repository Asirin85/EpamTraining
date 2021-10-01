using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class LinqDataProcesser
    {
        private readonly IReadOnlyCollection<Student> studentsData;
        public LinqDataProcesser(IReadOnlyCollection<Student> data)
        {
            studentsData = data;
        }
        private List<Student> SortResult(string sortBy, bool asc, IEnumerable<Student> data)
        {
            var param = typeof(Student).GetProperty(sortBy);
            return asc ? data.OrderBy(s => param.GetValue(s)).ToList() : data.OrderByDescending(s => param.GetValue(s)).ToList();

        }
        public List<Student> GetData(ArgsStructure args)
        {
            return GetDataByQuery(args, studentsData);
        }
        private List<Student> GetDataByQuery(ArgsStructure arguments, IEnumerable<Student> data)
        {
            DateTime datefrom = DateTime.Parse(arguments.DateFrom);
            DateTime dateto = DateTime.Parse(arguments.DateTo);
            var queryResult = from student in data
                              where student.Name.Contains(arguments.Name) && student.Date >= datefrom && student.Date <= dateto && student.Test.Contains(arguments.Test)
                              && student.Mark >= arguments.MinMark && student.Mark <= arguments.MaxMark
                              select student;
            return SortResult(arguments.SortBy, arguments.SortAsc, queryResult);
        }

    }
}
