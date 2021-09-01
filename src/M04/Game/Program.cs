using System;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Field gamePlan = new Field(10, 10);
            Console.WriteLine($"Your end score is {gamePlan.StartGame()}");
        }
    }
}
