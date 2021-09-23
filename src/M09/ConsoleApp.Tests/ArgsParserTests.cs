using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Tests
{
    public class ArgsParserTests
    {
        
        private static List<TestCaseData> _dataForArgsParser = new List<TestCaseData>
        {
            new TestCaseData("-test Math", new ArgsStructure{Test = "Math" }),
            new TestCaseData("-name Ivan -test", new ArgsStructure{Name = "Ivan" }),
            new TestCaseData("-minmark 3", new ArgsStructure{MinMark = 3 }),
            new TestCaseData("-maxmark 4", new ArgsStructure{MaxMark = 4 }),
            new TestCaseData("-datefrom 12/10/1999 -dateto 25/12/1999",  new ArgsStructure{DateFrom = DateTime.ParseExact("12/10/1999", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    DateTo =DateTime.ParseExact("25/12/1999", "dd/MM/yyyy", CultureInfo.InvariantCulture)}),
            new TestCaseData("-sort name asc",new ArgsStructure{ SortBy = "name", SortOrder = "asc"}),
            new TestCaseData("-sort desc", new ArgsStructure()),
            new TestCaseData("", new ArgsStructure()),
        };
        [TestCaseSource(nameof(_dataForArgsParser))]
        public void Test_For_ArgsParser(string input, ArgsStructure expectedResult)
        {
            PropertyInfo[] properties = expectedResult.GetType().GetProperties();
            ArgsStructure resultStruct = ArgsParser.ParseArgs(input);
            foreach (PropertyInfo property in properties)
            {
                object expectedValue = property.GetValue(expectedResult, null);
                object actualValue = property.GetValue(resultStruct, null);
                if (!Equals(expectedValue, actualValue))
                    Assert.Fail("Property {0}.{1} does not match. Expected: {2} but was: {3}", property.DeclaringType.Name, property.Name, expectedValue, actualValue);
            }
            Assert.Pass();
        }
    }
}
