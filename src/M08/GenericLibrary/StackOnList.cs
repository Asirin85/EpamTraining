using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericLibrary
{
    public class StackOnList<T> : IEnumerable<T>
    {
        private List<T> _stack;
        private int _currentIndex = -1;
        public StackOnList(T[] array)
        {
            _stack = new List<T>(array);
            _currentIndex = array.Length - 1;
        }
        public List<T> ToList()
        {
            return _stack;
        }
        public int Count()
        {
            return _currentIndex + 1;
        }
        public StackOnList()
        {
            _stack = new List<T>();
        }
        public void Push(T item)
        {
            if (_stack.Count - 1 > _currentIndex)
                _stack[++_currentIndex] = item;
            else
            {
                _stack.Add(item);
                _currentIndex++;
            }
        }
        public T Pop()
        {
            if (_currentIndex == -1) throw new NullReferenceException("Stack is empty, can't pop any value");
            var item = _stack[_currentIndex--];
            return item;
        }
        public T Peek()
        {
            if (_currentIndex == -1) throw new NullReferenceException("Stack is empty, can't peek any value");
            var item = _stack[_currentIndex];
            return item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new StackOnListIterator<T>(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
