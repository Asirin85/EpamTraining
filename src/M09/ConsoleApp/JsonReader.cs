using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class JsonReader
    {
        public static List<Student> ReadFromJson(string path)
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
