using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ArgsParser
    {
        public static ArgsStructure ParseArgs(string args)
        {
            args ??= "";
            string[] arrayOfArgs = args.Trim().Split(' ');
            string[] knownFlags = new[] { "-test", "-minmark", "-maxmark", "-name", "-datefrom", "-dateto", "-sort" };
            Dictionary<string, string> argumentsDictionary = new Dictionary<string, string>();
            var pairs = from arg in arrayOfArgs
                        where knownFlags.Contains(arg)
                        let index = Array.IndexOf(arrayOfArgs, arg)
                        let value = index + 1 < arrayOfArgs.Length && !arrayOfArgs[index + 1][0].Equals('-') ? arrayOfArgs[index + 1] : null
                        let result = arg.Equals("-sort") && index + 2 < arrayOfArgs.Length && !arrayOfArgs[index + 2][0].Equals('-') ? $"{value} {arrayOfArgs[index + 2]}" : value
                        select (arg, result);
            foreach (var element in pairs)
            {
                argumentsDictionary.Add(element.arg, element.result);
            }
            return CreateStruct(argumentsDictionary);
        }
        private static ArgsStructure CreateStruct(Dictionary<string, string> argsDictionary)
        {
            argsDictionary.TryGetValue("-name", out string name);
            argsDictionary.TryGetValue("-test", out string test);
            argsDictionary.TryGetValue("-sort", out string sortConditions);
            string sortBy = null;
            string sortOrder = null;
            if (sortConditions is not null && sortConditions.Split(' ').Length == 2)
            {
                sortBy = sortConditions.Split(' ')[0];
                sortOrder = sortConditions.Split(' ')[1];
            }
            string minmark = argsDictionary.TryGetValue("-minmark", out minmark) ? minmark : "";
            string maxmark = argsDictionary.TryGetValue("-maxmark", out maxmark) ? maxmark : "";
            double doubleMinMark = double.TryParse(minmark, out doubleMinMark) ? doubleMinMark : 0;
            double doubleMaxMark = double.TryParse(maxmark, out doubleMaxMark) ? doubleMaxMark : 5;
            string datefrom = argsDictionary.TryGetValue("-datefrom", out datefrom) ? datefrom : "";
            string dateto = argsDictionary.TryGetValue("-dateto", out dateto) ? dateto : "";
            DateTime dateFromTime = DateTime.TryParseExact(datefrom, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateFromTime) ? dateFromTime : DateTime.MinValue;
            DateTime dateToTime = DateTime.TryParseExact(dateto, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateToTime) ? dateToTime : DateTime.MaxValue;

            return new ArgsStructure { Name = name, MinMark = doubleMinMark, MaxMark = doubleMaxMark, DateFrom = dateFromTime, DateTo = dateToTime, Test = test, SortBy = sortBy, SortOrder = sortOrder };
        }
    }
}
