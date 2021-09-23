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
        public LinqDataProcesser(string path)
        {
            studentsData = ReadFromJson(path);
        }
        private List<Student> SortResult(string sortBy, string order, List<Student> data)
        {
            if (sortBy is null) throw new ArgumentException(nameof(sortBy));
            if (order is null) throw new ArgumentException(nameof(order));
            if (sortBy is { Length: > 0 } && order is { Length: > 0 })
            {
                if (!new[] { nameof(Student.Mark), nameof(Student.Name), nameof(Student.Test), nameof(Student.Date) }.Contains(sortBy, StringComparer.InvariantCultureIgnoreCase)) throw new ArgumentException("No such criteria for sorting");
                if (!order.Equals("asc", StringComparison.InvariantCultureIgnoreCase) && !order.Equals("desc", StringComparison.InvariantCultureIgnoreCase)) throw new ArgumentException("No right order specified");
                bool asc = order.Equals("asc", StringComparison.InvariantCultureIgnoreCase);
                var correctedString = $"{sortBy[0].ToString().ToUpperInvariant()}{sortBy.ToLowerInvariant()[1..sortBy.Length]}";
                var param = typeof(Student).GetProperty(correctedString);
                return asc ? data.OrderBy(s => param.GetValue(s)).ToList() : data.OrderByDescending(s => param.GetValue(s)).ToList();
            }
            return data;
        }
        public List<Student> GetData(string args)
        {
            ArgsStructure arguments = ArgsParser.ParseArgs(args);
            return GetDataByQuery(arguments, studentsData.ToList());
        }
        private List<Student> GetDataByQuery(ArgsStructure arguments, List<Student> data)
        {
            List<Student> result = new List<Student>();
            if (data.Count > 0)
            {
                var queryResult = from student in data
                                  where student.Name.Contains(arguments.Name) && student.Date >= arguments.DateFrom && student.Date <= arguments.DateTo && student.Test.Contains(arguments.Test)
                                  && student.Mark >= arguments.MinMark && student.Mark <= arguments.MaxMark
                                  select student;
                result = SortResult(arguments.SortBy, arguments.SortOrder, queryResult.ToList());
            }
            return result;
        }
        private List<Student> ReadFromJson(string path)
        {
            if (path is not { Length: > 0 }) throw new FileNotFoundException("Empty or null path to file");
            using (StreamReader r = new StreamReader(path))
            {
                string read = r.ReadToEnd();
                Console.WriteLine(read);
                var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" };
                List<Student> students = JsonConvert.DeserializeObject<List<Student>>(read, dateTimeConverter);
                return students;
            }
        }
    }
}
