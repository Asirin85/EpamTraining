using System;
namespace Perfomance
{
    public class C : IComparable
    {
        private int _i;
        public C(int num)
        {
            _i = num;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            C classVariable = obj as C;
            if (classVariable != null)
                return this._i.CompareTo(classVariable._i);
            else throw new ArgumentException("Object is not C");
        }
    }
}
