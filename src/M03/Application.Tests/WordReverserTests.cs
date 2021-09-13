using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests
{
    public class WordReverserTests
    {
        [Test]
        public void Test_For_WordReverser_NullOrEmptyInput([Values("", null)] string input)
        {
            Assert.That(() => WordReverser.Reverse(input), Throws.TypeOf<ArgumentException>());
        }
        [TestCase("Each word is nothing new", "new nothing is word Each")]
        public void Test_For_WordReverser(string input, string expectedResult)
        {
            string result = WordReverser.Reverse(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
