using System;
namespace Perfomance
{
    struct S : IComparable<S>
    {
        public int i { get; }
        public S(int num)
        {
            i = num;
        }
        public int CompareTo(S other)
        {
            return this.i.CompareTo(other.i);
        }
    }
}
