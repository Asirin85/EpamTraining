using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericLibrary.Tests
{
    public class StackOnListTests
    {
        private static List<TestCaseData> _dataForStackOnListIteration = new List<TestCaseData>(new[]{
            new TestCaseData(new StackOnList<int>(new[]{1,2,3,4,5,6 }),3,4),
            new TestCaseData(new StackOnList<int>(new[]{1,2,3,4,5,6 }),4,3),
            new TestCaseData(new StackOnList<int>(new[]{1,2,3,4,5,6 }),1,6),
        });
        [TestCaseSource(nameof(_dataForStackOnListIteration))]
        public void Test_For_StackOnList_Iteration(StackOnList<int> stackList, int iterations, int expectedResult)
        {
            var enumerator = stackList.GetEnumerator();
            for(int i =0; i < iterations; i++)
            {
                enumerator.MoveNext();
            }
            Assert.That(enumerator.Current, Is.EqualTo(expectedResult));
        }
    }
}
