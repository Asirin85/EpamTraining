namespace Figures
{
    abstract class Triangle : Figure
    {
        public double LeftSide { get; protected set; }
        public double RightSide { get; protected set; }
        public double BaseSide { get; protected set; }

        public override double GetPerimeter()
        {
            return LeftSide + RightSide + BaseSide;
        }
    }
}
