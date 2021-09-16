using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversePolishApp
{
    public class ReversePolishCalculator
    {
        public double Sum(double a, double b)
        {
            return a + b;
        }
        public double Minus(double a, double b)
        {
            return a - b;
        }
        public double Multiply(double a, double b)
        {
            return a * b;
        }
        public double Divide(double a, double b)
        {
            if (b == 0) throw new ArithmeticException("Trying to divide by zero");
            return a / b;
        }
        public double Calculate(string input)
        {
            if (input is not null)
            {
                if (input.Length == 0) return 0;
                var split = input.Split(' ');
                var map = new Dictionary<string, Func<double,double,double>>() { { "+", Sum }, { "-", Minus }, { "*", Multiply }, { "/", Divide } };
                var stackOfNumbers = new Stack<double>();
                for (int i = 0; i < split.Length; i++)
                {
                    double foundNumber;
                    Func<double,double,double> op;
                    if (double.TryParse(split[i], out foundNumber))
                    {
                        stackOfNumbers.Push(foundNumber);
                    }
                    else if (map.TryGetValue(split[i], out op))
                    {
                        if (stackOfNumbers.Count > 1)
                        {
                            var second = stackOfNumbers.Pop();
                            var first = stackOfNumbers.Pop();
                            var result = op(first, second);
                            Console.WriteLine($"{first} {second} {result}");
                            stackOfNumbers.Push(result);
                        }
                        else throw new FormatException($"Wrong input string, not enough numbers for an operation, index is {i + 1}");
                    }
                    else throw new FormatException("Wrong symbol in input string");
                }
                if (stackOfNumbers.Count == 1)
                    return stackOfNumbers.Pop();
                else throw new FormatException("Not enough operators for numbers");
            }
            else throw new ArgumentException("Wrong input in ReversePolishCalculator -> Calculate");

        }
    }
}
