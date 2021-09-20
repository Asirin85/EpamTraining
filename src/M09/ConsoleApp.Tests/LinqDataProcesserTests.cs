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
            new TestCaseData("-name Alena", new List<Student>{ _studentData[3]}),
            new TestCaseData("-test Rus", new List<Student>{ _studentData[2] }),
            new TestCaseData("-minmark 4", new List<Student>{  _studentData[1],_studentData[2] }),
            new TestCaseData("-maxmark 3", new List<Student>{ _studentData[0] ,_studentData[3] }),
            new TestCaseData("-datefrom 15/09/2013", new List<Student>{_studentData[2], _studentData[3] }),
            new TestCaseData("-dateto 31/12/2012", new List<Student>{ _studentData[0], _studentData[1]}),
            new TestCaseData("",  new List<Student>{ _studentData[0] ,_studentData[1],_studentData[2], _studentData[3]}),
            new TestCaseData("-minmark 6", new List<Student>{ })
        };
        [TestCaseSource(nameof(_dataForLinqDataProcesser_CorrectInput))]
        public void Test_For_LinqDataProcesser_CorrectInput(string args, List<Student> expectedResult)
        {
            var linqDataProcesser = new LinqDataProcesser(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "testdata.json"));
            var studentsData = linqDataProcesser.GetData(args);
            Assert.That(studentsData, Is.EqualTo(expectedResult));
        }
        [Test]
        public void Test_For_LinqDataProcesser_IncorrectPath([Values(null, "", @"c:\j.json")] string path)
        {
            Assert.That(() => new LinqDataProcesser(path), Throws.TypeOf<FileNotFoundException>());
        }

        private static List<TestCaseData> _dataForLinqDataProcesser_CorrectSort = new List<TestCaseData>
        {
            new TestCaseData("-sort name asc",  new List<Student>{ _studentData[3], _studentData[1], _studentData[2], _studentData[0] }),
            new TestCaseData("-sort name desc", new List<Student>{_studentData[0], _studentData[2], _studentData[1], _studentData[3] }),
            new TestCaseData("-sort test asc", new List<Student>{_studentData[0], _studentData[1], _studentData[3], _studentData[2] }),
            new TestCaseData("-sort test desc", new List<Student>{_studentData[2], _studentData[0], _studentData[1], _studentData[3] }),
            new TestCaseData("-sort date asc", new List<Student>{_studentData[0], _studentData[1], _studentData[2], _studentData[3] }),
            new TestCaseData("-sort date desc", new List<Student>{_studentData[3], _studentData[2], _studentData[1], _studentData[0] }),
            new TestCaseData("-sort mark asc", new List<Student>{_studentData[3], _studentData[0], _studentData[1], _studentData[2] }),
            new TestCaseData("-sort mark desc", new List<Student>{_studentData[2], _studentData[1], _studentData[0], _studentData[3] }),
        };
        [TestCaseSource(nameof(_dataForLinqDataProcesser_CorrectSort))]
        public void Test_For_LinqDataProcesser_CorrectSort(string args, List<Student> expectedResult)
        {
            var linqDataProcesser = new LinqDataProcesser(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "testdata.json"));
            var studentsData = linqDataProcesser.GetData(args);
            Assert.That(studentsData, Is.EqualTo(expectedResult));
        }
        [Test]
        public void Test_For_LinqDataProcesser_IncorrectSort([Values("-sort a asc", "-sort name b")]string args)
        {
            var linqDataProcesser = new LinqDataProcesser(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "testdata.json"));
            Assert.That(() => linqDataProcesser.GetData(args), Throws.TypeOf<ArgumentException>());
        }
    }
}