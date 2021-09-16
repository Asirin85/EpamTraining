using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericLibrary
{
    class StackOnListIterator<T> : IEnumerator<T>
    {
        private List<T> _stack;
        private int _position;
        public StackOnListIterator(List<T> stack)
        {
            _stack = stack;
            _position = _stack.Count();
        }

        object IEnumerator.Current { get { return _stack[_position]; } }
        T IEnumerator<T>.Current { get { return _stack[_position]; } }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (--_position >= 0)
            {
                return true;
            }
            else return false;
        }

        public void Reset()
        {
            _position = _stack.Count();
        }
    }
}
