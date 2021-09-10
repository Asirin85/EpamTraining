using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests
{
    public class WordCounterTests
    {
        [Test]
        public void Test_For_WordCounter_NullOrEmptyInput([Values("", null)] string input)
        {
            Assert.That(() => WordCounter.AvgWordLength(input), Throws.TypeOf<ArgumentException>());
        }
        [TestCase("each,, word, , equa,, four, ,", 4)]
        public void Test_For_WordCounter(string input, double expectedResult)
        {
            double result = WordCounter.AvgWordLength(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
