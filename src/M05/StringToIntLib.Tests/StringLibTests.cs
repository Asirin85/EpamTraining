using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace StringToIntLib.Tests
{
    public class StringLibTests
    {
        [Test]
        public void Test_For_StringToInt_NullOrEmptyInput([Values("", null)] string input)
        {
            var logger = Mock.Of<ILogger<StringLib>>();
            StringLib stringLib = new StringLib(logger);
            Assert.That(() => stringLib.StringToInt(input), Throws.TypeOf<ArgumentException>());
        }
        [Test]
        public void Test_For_StringToInt_IntOverflow([Values("99999999999999999", "-99999999999999999")] string input)
        {
            var logger = Mock.Of<ILogger<StringLib>>();
            StringLib stringLib = new StringLib(logger);
            Assert.That(() => stringLib.StringToInt(input), Throws.TypeOf<OverflowException>());
        }
        [Test]
        public void Test_For_StringToInt_WrongInputFormat([Values("152,423", "-42-21,21", "521 512", "123!.312", "abc")] string input)
        {
            var logger = Mock.Of<ILogger<StringLib>>();
            StringLib stringLib = new StringLib(logger);
            Assert.That(() => stringLib.StringToInt(input), Throws.TypeOf<FormatException>());
        }
        [Test, Sequential]
        public void Test_For_StringToInt([Values("-123.5235", "5230.423")] string input, [Values(-123, 5230)] int expectedResult)
        {
            var logger = Mock.Of<ILogger<StringLib>>();
            StringLib stringLib = new StringLib(logger);
            var result = stringLib.StringToInt(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}