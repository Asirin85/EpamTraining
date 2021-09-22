using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GenericLibrary.Tests
{
    public class GenericLibTests
    {
        private static List<TestCaseData> _dataForGenericBinarySearchWrongInput = new List<TestCaseData>{
        new TestCaseData(null, "a"),
        new TestCaseData(new string[0], "a"),
        new TestCaseData(new[]{"abc","cba","vba"}, null),
        new TestCaseData(new[]{"abc","bbc","cbc"}, "bar"),
        };
        [TestCaseSource(nameof(_dataForGenericBinarySearchWrongInput))]
        public void Test_For_GenericLib_GenericBinarySearch_WrongInputArray(string[] input, string toFind)
        {
            Assert.That(() => GenericLib.GenericBinarySearch(input, toFind), Throws.TypeOf<ArgumentException>());
        }
        private static List<TestCaseData> _dataForGenericBinarySearchCorrectInput = new List<TestCaseData>{
        new TestCaseData(new[] {5,8,12,22,40}, 12, 2),
        new TestCaseData(new[] {5,8,12,22,40}, 8, 1),
        new TestCaseData(new[] {5,8,12,22,40}, 5, 0),
        new TestCaseData(new[] {5,8,12,22,40},22, 3),
        new TestCaseData(new[] {5,8,12,22,40},40, 4),
        };
        [TestCaseSource(nameof(_dataForGenericBinarySearchCorrectInput))]
        public void Test_For_GenericLib_GenericBinarySearch(int[] input, int toFind, int expectedResult)
        {
            Assert.That(GenericLib.GenericBinarySearch(input, toFind), Is.EqualTo(expectedResult));
        }
        [Test, Sequential]
        public void Test_For_Fibonacci([Values(5, 7, 10)] int numInSequence, [Values(3, 8, 34)] int expectedResult)
        {
            var gLib = new GenericLib();
            var enumerator = gLib.Fibonacci().GetEnumerator();
            for (int i = 0; i < numInSequence; i++)
            {
                enumerator.MoveNext();
            }
            Assert.That(enumerator.Current, Is.EqualTo(expectedResult));
        }
    }
}