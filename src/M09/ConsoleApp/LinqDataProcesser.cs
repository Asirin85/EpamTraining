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
        private readonly List<Student> studentsData;
        private List<Student> result = new List<Student>();
        public LinqDataProcesser(string path)
        {
            studentsData = ReadFromJson(path);
        }
        private void SortResult(string sortBy, string order)
        {
            if (sortBy is not { Length: > 0 } || order is not { Length: > 0 }) throw new ArgumentException("Empty or null sort criteria");
            if (!new[] { "mark", "name", "test", "date" }.Contains(sortBy.ToLowerInvariant())) throw new ArgumentException("No such criteria for sorting");
            if (!order.ToLowerInvariant().Equals("asc") && !order.ToLowerInvariant().Equals("desc")) throw new ArgumentException("No right order specified");
            bool asc = order.ToLowerInvariant().Equals("asc");
            var correctedString = $"{sortBy[0].ToString().ToUpperInvariant()}{sortBy.ToLowerInvariant()[1..sortBy.Length]}";
            var param = typeof(Student).GetProperty(correctedString);
            if (asc) result = result.OrderBy(s => param.GetValue(s)).ToList();
            else result = result.OrderByDescending(s => param.GetValue(s)).ToList();
        }
        public List<Student> GetData(string args)
        {
            if (args is null) args = "";
            result = studentsData;
            string[] arrayOfArgs = args.Trim().Split(' ');
            string[] knownFlags = new[] { "-test", "-minmark", "-maxmark", "-name", "-datefrom", "-dateto" };
            Dictionary<string, string> argumentsDictionary = new Dictionary<string, string>();
            for (int i = 0; i < arrayOfArgs.Length - 1; i++)
            {
                if (knownFlags.Contains(arrayOfArgs[i]))
                {
                    if (!arrayOfArgs[i + 1][0].Equals('-'))
                    {
                        argumentsDictionary.Add(arrayOfArgs[i], arrayOfArgs[++i]);
                    }
                }
                else if (arrayOfArgs[i].ToLowerInvariant().Equals("-sort") && i < arrayOfArgs.Length - 2)
                {
                    if (!arrayOfArgs[i + 1][0].Equals('-') && !arrayOfArgs[i + 2][0].Equals('-'))
                        SortResult(arrayOfArgs[++i], arrayOfArgs[++i]);
                }

            }
            GetDataByQuery(argumentsDictionary);
            return result;
        }
        private void GetDataByQuery(Dictionary<string, string> argumentsDictionary)
        {
            if (result.Count > 0)
            {
                if (!argumentsDictionary.TryGetValue("-name", out string name)) name = "";
                if (!argumentsDictionary.TryGetValue("-test", out string test)) test = "";
                if (!argumentsDictionary.TryGetValue("-minmark", out string minmark)) minmark = "";
                if (!argumentsDictionary.TryGetValue("-maxmark", out string maxmark)) maxmark = "";
                if (!double.TryParse(minmark, out double numMinMark)) numMinMark = 0;
                if (!double.TryParse(maxmark, out double numMaxMark)) numMaxMark = 5;
                if (!argumentsDictionary.TryGetValue("-datefrom", out string datefrom)) datefrom = "";
                if (!argumentsDictionary.TryGetValue("-dateto", out string dateto)) dateto = "";
                if (!DateTime.TryParseExact(datefrom, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateFromTime)) dateFromTime = DateTime.MinValue;
                if (!DateTime.TryParseExact(dateto, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateToTime)) dateToTime = DateTime.MaxValue;

                var queryResult = from student in result
                                  where student.Name.Contains(name) && student.Date >= dateFromTime && student.Date <= dateToTime && student.Test.Contains(test)
                                  && student.Mark >= numMinMark && student.Mark <= numMaxMark
                                  select student;
                result = queryResult.ToList();
            }
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
