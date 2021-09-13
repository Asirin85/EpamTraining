using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace Application.Tests
{
    public class DoubleLetterTests
    {
        private static List<TestCaseData> _dataForDoubleLetterNullOrEmpty = new List<TestCaseData>(new[]
        {
            new TestCaseData(null, "a"),
            new TestCaseData("", "a"),
            new TestCaseData("a", null),
            new TestCaseData("a", ""),
        });
        [TestCase("shrek is love", "shrek", "sshhrreekk iss lovee")]
        public void Test_For_DoubleLetter(string input, string charToDouble, string expectedResult)
        {
            string result = DoubleLetter.DoubleLetters(input, charToDouble);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCaseSource(nameof(_dataForDoubleLetterNullOrEmpty))]
        public void Test_For_DoublerLetter_NullOrEmptyInput(string input, string charToDouble)
        {
            Assert.That(() => DoubleLetter.DoubleLetters(input, charToDouble), Throws.TypeOf<ArgumentException>());
        }
    }
}