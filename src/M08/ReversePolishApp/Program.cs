using System;

namespace ReversePolishApp
{
    class Program
    {
        static void Main()
        {
            var rpc = new ReversePolishCalculator();
            Console.WriteLine(rpc.Calculate("5 1 2 + 4 * + 3 -"));
        }
    }
}
