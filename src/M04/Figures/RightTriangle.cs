using System;

namespace Figures
{
    sealed class RightTriangle : Triangle
    {
        public RightTriangle(double legLeft, double legBase)
        {
            ChangeSides(legLeft, legBase);
        }
        public void ChangeSides(double legLeft, double legBase)
        {
            LeftSide = legLeft;
            BaseSide = legBase;
            RightSide = Math.Sqrt(Math.Pow(legLeft,2)+Math.Pow(legBase,2));
        }
        public override double GetArea()
        {
            return LeftSide * BaseSide / 2;
        }
    }
}
