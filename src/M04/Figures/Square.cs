using System;

namespace Figures
{
    sealed class Square : Quadrangle
    {
        public Square(double side)
        {
            ChangeSide(side);
        }
        public void ChangeSide(double side)
        {
            LeftSide = side;
            RightSide = side;
            UpperSide = side;
            DownSide = side;
        }
        public override double GetArea()
        {
            return Math.Pow(LeftSide, 2);
        }
    }
}
