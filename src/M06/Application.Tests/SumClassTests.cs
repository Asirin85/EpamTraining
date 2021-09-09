using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests
{
    public class SumClassTests
    {
        private static List<TestCaseData> _dataForSumClassNullOrEmpty = new List<TestCaseData>(new[]
        {
            new TestCaseData(null, "1"),
            new TestCaseData("", "1"),
            new TestCaseData("1", null),
            new TestCaseData("1", ""),
        });
        [TestCaseSource(nameof(_dataForSumClassNullOrEmpty))]
        public void Test_For_SumClass_NullOrEmptyInput(string firstNum, string secondNum)
        {
            Assert.That(() => SumClass.SumOfStrings(firstNum, secondNum), Throws.TypeOf<ArgumentException>());
        }
        [TestCase("44444444445", "55555555555", "100000000000")]
        public void Test_For_SumClass(string firstNum, string secondNum, string expectedResult)
        {
            string result = SumClass.SumOfStrings(firstNum, secondNum);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
