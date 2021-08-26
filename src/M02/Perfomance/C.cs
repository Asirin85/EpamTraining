using System;
namespace Perfomance
{
    public class C : IComparable<C>
    {
        private int _i;
        public C(int num)
        {
            _i = num;
        }

        public int CompareTo(C other)
        {
            if (other == null) return 1;
            if(other is C)
                return this._i.CompareTo(other._i);
            else throw new ArgumentException("Object is not C");
        }
    }
}
