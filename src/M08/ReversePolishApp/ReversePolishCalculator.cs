using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversePolishApp
{
    public class ReversePolishCalculator
    {
        public static double Sum(double a, double b)
        {
            return a + b;
        }
        public static double Minus(double a, double b)
        {
            return a - b;
        }
        public static double Multiply(double a, double b)
        {
            return a * b;
        }
        public static double Divide(double a, double b)
        {
            if (b == 0) throw new ArithmeticException("Trying to divide by zero");
            return a / b;
        }
        private static Dictionary<string, Func<double, double, double>> mapOfOperations = new Dictionary<string, Func<double, double, double>>() { { "+", Sum }, { "-", Minus }, { "*", Multiply }, { "/", Divide } };
        public double Calculate(string input)
        {
            if (input is null) throw new ArgumentException("Wrong input in ReversePolishCalculator -> Calculate");
            if (input.Length == 0) return 0;
            var splitInput = input.Split(' ');
            var stackOfNumbers = new Stack<double>();
            for (int i = 0; i < splitInput.Length; i++)
            {
                TryEvaluate(splitInput[i], stackOfNumbers);
            }
            if (stackOfNumbers.Count == 1)
                return stackOfNumbers.Pop();
            throw new FormatException("Not enough operators for numbers");
        }
        private bool TryUseOperationOnStack(Stack<double> stackOfNumbers, Func<double, double, double> operation)
        {
            if (stackOfNumbers.Count > 1)
            {
                var second = stackOfNumbers.Pop();
                var first = stackOfNumbers.Pop();
                var result = operation(first, second);
                stackOfNumbers.Push(result);
                return true;
            }
            return false;
        }
        private void TryEvaluate(string inputSymbol, Stack<double> stackOfNumbers)
        {
            if (double.TryParse(inputSymbol, out double foundNumber))
            {
                stackOfNumbers.Push(foundNumber);
            }
            else if (mapOfOperations.TryGetValue(inputSymbol, out Func<double, double, double> op))
            {
                if (!TryUseOperationOnStack(stackOfNumbers, op)) throw new FormatException($"Wrong input symbol, not enough numbers for an operation");
            }
            else throw new FormatException("Wrong symbol in input string");
        }
    }
}
