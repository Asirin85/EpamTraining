namespace Figures
{
    sealed class Rectangle : Quadrangle
    {
        public Rectangle(double length, double width)
        {
            ChangeWidth(width);
            ChangeLength(length);
        }
        public void ChangeLength(double length)
        {
            UpperSide = length;
            DownSide = length;
        }
        public void ChangeWidth(double width)
        {
            LeftSide = width;
            RightSide = width;
        }
        public override double GetArea()
        {
            return LeftSide * UpperSide;
        }
    }
}
