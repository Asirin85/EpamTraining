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
            new TestCaseData("-- bad", new ArgsStructure()),
            new TestCaseData("-- very bad bad", new ArgsStructure()),
            new TestCaseData("das asd123", new ArgsStructure()),
            new TestCaseData(null, new ArgsStructure()),
            new TestCaseData("-name Ivan -test", new ArgsStructure{Name = "Ivan" }),
            new TestCaseData("-minmark 3", new ArgsStructure{MinMark = 3 }),
            new TestCaseData("-maxmark 4", new ArgsStructure{MaxMark = 4 }),
            new TestCaseData("-test Maths -name Ivan -sort name desc", new ArgsStructure{Test = "Maths", Name ="Ivan", SortBy="Name", SortOrder="desc" }),
            new TestCaseData("-datefrom 12/10/1999 -dateto 25/12/1999",  new ArgsStructure{DateFrom = "12/10/1999",
                    DateTo ="25/12/1999"}),
            new TestCaseData("-sort name asc",new ArgsStructure{ SortBy = "Name", SortOrder = "asc"}),
            new TestCaseData("-sort desc", new ArgsStructure()),
            new TestCaseData("", new ArgsStructure()),
        };
        [TestCaseSource(nameof(_dataForArgsParser))]
        public void Test_For_ArgsParser(string input, ArgsStructure expectedResult)
        {
            ArgsStructure resultStruct = ArgsParser.ParseArgs(input);

            Assert.AreEqual(resultStruct, expectedResult);
        }
    }
}
