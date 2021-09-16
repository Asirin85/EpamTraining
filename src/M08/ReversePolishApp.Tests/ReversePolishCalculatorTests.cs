using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ReversePolishApp.Tests
{
    public class ReversePolishCalculatorTests
    {
        private static List<TestCaseData> _dataForRPCalculator = new List<TestCaseData>(
            new[] {
                new TestCaseData("1 2 +", 3),
                new TestCaseData("5 1 2 + 4 * + 3 -", 14),
                new TestCaseData("1 2 / 5 9 * 3 - 4 / + 2 +", 13),
            });
        private static List<TestCaseData> _dataForRPCalculator_WrongFormat = new List<TestCaseData>(
            new[] {
                new TestCaseData("1 2 ,"),
                new TestCaseData("5! 2 +"),
                new TestCaseData("5 2"),
                new TestCaseData("5 2 + +"),
            });
        [TestCaseSource(nameof(_dataForRPCalculator))]
        public void Test_For_RPCalculator_CorrectInput(string input, double expectedResult)
        {
            ReversePolishCalculator rpc = new ReversePolishCalculator();
            Assert.That(rpc.Calculate(input), Is.EqualTo(expectedResult));
        }

        [TestCaseSource(nameof(_dataForRPCalculator_WrongFormat))]
        public void Test_For_RPCalculator_WrongFormat(string input)
        {
            ReversePolishCalculator rpc = new ReversePolishCalculator();
            Assert.That(() => rpc.Calculate(input), Throws.TypeOf<FormatException>());
        }
        [Test]
        public void Test_For_RPCalculator_WrongInput([Values(null)]string input)
        {
            ReversePolishCalculator rpc = new ReversePolishCalculator();
            Assert.That(() => rpc.Calculate(input), Throws.TypeOf<ArgumentException>());
        }
    }
}