namespace Figures
{
    abstract class Quadrangle : Figure
    {
        public double LeftSide { get; protected set; }
        public double RightSide { get; protected set; }
        public double UpperSide { get; protected set; }
        public double DownSide { get; protected set; }
        public override double GetPerimeter()
        {
            return LeftSide + RightSide + UpperSide + DownSide;
        }
    }
}
