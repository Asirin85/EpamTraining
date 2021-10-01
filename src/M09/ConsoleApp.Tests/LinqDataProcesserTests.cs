using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
namespace ConsoleApp.Tests
{
    public class LinqDataProcesserTests
    {
        private static List<Student> _studentData = new List<Student>
        {
            new Student() {Name = "Ivan Petrov", Date = DateTime.ParseExact("25/11/2012", "dd/MM/yyyy", CultureInfo.InvariantCulture), Mark = 3 , Test = "Maths" },
            new Student() {Name = "Anton Petrov", Date = DateTime.ParseExact("31/12/2012", "dd/MM/yyyy", CultureInfo.InvariantCulture), Mark = 4, Test = "Maths"},
            new Student() { Name = "Irina Vasilievna", Date = DateTime.ParseExact("15/09/2013", "dd/MM/yyyy", CultureInfo.InvariantCulture), Mark = 5, Test = "Rus" },
            new Student() {Name = "Alena Vasilievna", Date = DateTime.ParseExact("18/09/2014", "dd/MM/yyyy", CultureInfo.InvariantCulture), Mark = 2, Test = "Maths" }
        };
        private static List<TestCaseData> _dataForLinqDataProcesser_CorrectInput = new List<TestCaseData>
        {
            new TestCaseData(_studentData,new ArgsStructure{Name="Alena" }, new List<Student>{ _studentData[3]}),
            new TestCaseData(_studentData,new ArgsStructure{Test="Rus" }, new List<Student>{ _studentData[2] }),
            new TestCaseData(_studentData,new ArgsStructure{MinMark=4 }, new List<Student>{  _studentData[1],_studentData[2] }),
            new TestCaseData(_studentData,new ArgsStructure{MaxMark=3 }, new List<Student>{ _studentData[3] ,_studentData[0] }),
            new TestCaseData(_studentData,new ArgsStructure{DateFrom="15/09/2013"}, new List<Student>{_studentData[3], _studentData[2] }),
            new TestCaseData(_studentData,new ArgsStructure{DateTo="31/12/2012"}, new List<Student>{ _studentData[1], _studentData[0]}),
            new TestCaseData(_studentData,new ArgsStructure(),  new List<Student>{ _studentData[3] ,_studentData[1],_studentData[2], _studentData[0]}),
            new TestCaseData(_studentData,new ArgsStructure{MinMark=6 }, new List<Student>{_studentData[3] ,_studentData[1],_studentData[2], _studentData[0]})
        };
        [TestCaseSource(nameof(_dataForLinqDataProcesser_CorrectInput))]
        public void Test_For_LinqDataProcesser_CorrectInput(List<Student> inputData, ArgsStructure args, List<Student> expectedResult)
        {
            var linqDataProcesser = new LinqDataProcesser(inputData);
            var studentsData = linqDataProcesser.GetData(args);
            Assert.That(studentsData, Is.EqualTo(expectedResult));
        }
        private static List<TestCaseData> _dataForLinqDataProcesser_CorrectSort = new List<TestCaseData>
        {
            new TestCaseData(_studentData,new ArgsStructure{SortBy="name", SortOrder="asc"},  new List<Student>{ _studentData[3], _studentData[1], _studentData[2], _studentData[0] }),
            new TestCaseData(_studentData,new ArgsStructure{SortBy="name", SortOrder="desc"}, new List<Student>{_studentData[0], _studentData[2], _studentData[1], _studentData[3] }),
            new TestCaseData(_studentData,new ArgsStructure{SortBy="test", SortOrder="asc"}, new List<Student>{_studentData[0], _studentData[1], _studentData[3], _studentData[2] }),
            new TestCaseData(_studentData,new ArgsStructure{SortBy="test", SortOrder="desc"}, new List<Student>{_studentData[2], _studentData[0], _studentData[1], _studentData[3] }),
            new TestCaseData(_studentData,new ArgsStructure{SortBy="date", SortOrder="asc"}, new List<Student>{_studentData[0], _studentData[1], _studentData[2], _studentData[3] }),
            new TestCaseData(_studentData,new ArgsStructure{SortBy="date", SortOrder="desc"}, new List<Student>{_studentData[3], _studentData[2], _studentData[1], _studentData[0] }),
            new TestCaseData(_studentData,new ArgsStructure{SortBy="mark", SortOrder="asc"}, new List<Student>{_studentData[3], _studentData[0], _studentData[1], _studentData[2] }),
            new TestCaseData(_studentData,new ArgsStructure{SortBy="mark", SortOrder="desc"}, new List<Student>{_studentData[2], _studentData[1], _studentData[0], _studentData[3] }),
        };
        [TestCaseSource(nameof(_dataForLinqDataProcesser_CorrectSort))]
        public void Test_For_LinqDataProcesser_CorrectSort(List<Student> inputData,ArgsStructure args, List<Student> expectedResult)
        {
            var linqDataProcesser = new LinqDataProcesser(inputData);
            var studentsData = linqDataProcesser.GetData(args);
            Assert.That(studentsData, Is.EqualTo(expectedResult));
        }
    }
}