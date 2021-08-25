using System;
namespace Perfomance
{
    struct S : IComparable
    {
        public int i { get; }
        public S(int num)
        {
            i = num;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (obj is not S) throw new ArgumentException("Object is not S");
            S structVariable = (S)obj;
            return this.i.CompareTo(structVariable.i);

        }
    }
}
