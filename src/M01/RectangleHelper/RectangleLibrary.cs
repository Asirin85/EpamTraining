using System;

namespace RectangleHelper
{
    public static class RectangleLibrary
    {
        public static int Perimeter(int width, int length)
        {
            if (width > 0 && length > 0)
            {
                return width + length;
            }
            else return -1;
        }
        public static int Square(int width, int length)
        {
            if (width > 0 && length > 0)
            {
                return width * length;
            }
            else return -1;
        }
    }
}
