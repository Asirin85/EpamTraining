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
        private static List<TestCaseData> _dataForStackOnListIteration = new List<TestCaseData>{
            new TestCaseData(new StackOnList<int>(new[]{1,2,3,4,5,6 }),3,4),
            new TestCaseData(new StackOnList<int>(new[]{1,2,3,4,5,6 }),4,3),
            new TestCaseData(new StackOnList<int>(new[]{1,2,3,4,5,6 }),1,6),
        };
        [TestCaseSource(nameof(_dataForStackOnListIteration))]
        public void Test_For_StackOnList_Iteration(StackOnList<int> stackList, int iterations, int expectedResult)
        {
            var enumerator = stackList.GetEnumerator();
            for (int i = 0; i < iterations; i++)
            {
                enumerator.MoveNext();
            }
            Assert.That(enumerator.Current, Is.EqualTo(expectedResult));
        }
        [Test]
        public void Test_For_StackOnList_Enumerator_EmptyStack()
        {
            StackOnList<int> stackList = new StackOnList<int>();
            var enumerator = stackList.GetEnumerator();
            Assert.That(() => enumerator.Current, Throws.TypeOf<ArgumentOutOfRangeException>());
        }
        private static List<TestCaseData> _dataForStackOnListPush = new List<TestCaseData>
        {
            new TestCaseData(new StackOnList<int>(new[]{1,2,3,4 }),5,new StackOnList<int>(new[]{1,2,3,4,5 })),
            new TestCaseData(new StackOnList<int>(new[]{1,2,3,4 }),-1,new StackOnList<int>(new[]{1,2,3,4,-1 })),
            new TestCaseData(new StackOnList<int>(),1,new StackOnList<int>(new[]{1})),
        };
        [TestCaseSource(nameof(_dataForStackOnListPush))]
        public void Test_For_StackOnList_Push(StackOnList<int> input, int pushValue, StackOnList<int> expectedResult)
        {
            input.Push(pushValue);
            Assert.That(input, Is.EqualTo(expectedResult));
        }
        private static List<TestCaseData> _dataForStackOnListPeek = new List<TestCaseData>
        {
            new TestCaseData(new StackOnList<int>(new[]{32,51 }),51,new StackOnList<int>(new[]{32,51 })),
            new TestCaseData(new StackOnList<int>(new[]{0,124,624 }),624,new StackOnList<int>(new[]{0,124,624 })),
        };
        [TestCaseSource(nameof(_dataForStackOnListPeek))]
        public void Test_For_StackOnList_Peek(StackOnList<int> input, int expectedPeek, StackOnList<int> expectedStack)
        {
            Assert.That(input.Peek(), Is.EqualTo(expectedPeek));
            Assert.That(input, Is.EqualTo(expectedStack));
        }
        private static List<TestCaseData> _dataForStackOnListPop = new List<TestCaseData>
        {
            new TestCaseData(new StackOnList<int>(new[]{32,51 }),51,new StackOnList<int>(new[]{32 })),
            new TestCaseData(new StackOnList<int>(new[]{0,124,624 }),624,new StackOnList<int>(new[]{0,124 })),
        };
        [TestCaseSource(nameof(_dataForStackOnListPop))]
        public void Test_For_StackOnList_Pop(StackOnList<int> input, int expectedPop, StackOnList<int> expectedStack)
        {
            Assert.That(input.Pop(), Is.EqualTo(expectedPop));
            Assert.That(input, Is.EqualTo(expectedStack));
        }
        [Test]
        public void Test_For_StackOnList_PeekPop_EmptyStack()
        {
            StackOnList<int> emptyStack = new StackOnList<int>();
            Assert.That(() => emptyStack.Peek(), Throws.TypeOf<NullReferenceException>());
            Assert.That(() => emptyStack.Pop(), Throws.TypeOf<NullReferenceException>());
        }
    }
}
