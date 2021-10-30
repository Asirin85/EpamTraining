using System;
using Game;

namespace ConsoleGame
{
    class Program
    {
        static void Main()
        {
            IInputOutputHandler inputOutput = new ConsoleInputOutput();
            Field gamePlan = new Field(10, 10, inputOutput);
            Console.WriteLine($"Your end score is {gamePlan.StartGame()}");
        }
    }
}
