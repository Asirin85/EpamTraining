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
            if (other == null) throw new ArgumentException("Object is null");
            return this._i.CompareTo(other._i);
        }
    }
}
