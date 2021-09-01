using System;

namespace Figures
{
    sealed class Circle : Figure
    {
        public double Radius { get; private set; }
        public Circle(double radius)
        {
            Radius = radius;
        }
        public void ChangeRadius(double radius)
        {
            Radius = radius;
        }
        public override double GetArea()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }

        public override double GetPerimeter()
        {
            return Math.PI * 2 * Radius;
        }
    }
}
