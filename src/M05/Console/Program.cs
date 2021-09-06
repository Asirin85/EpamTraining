using NLog;
using StringToIntLib;
using Microsoft.Extensions.Logging;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger<StringLib> consoleAppLogger = new ConsoleAppLogger().GetInstance();
            consoleAppLogger.LogInformation("Initializing my console app");
            StringLib stringLibrary = new StringLib(consoleAppLogger);
            Console.WriteLine("Press any key to start");
            var key = Console.ReadKey();
            do
            {
                try
                {
                    consoleAppLogger.LogInformation("Starting converting");
                    Console.WriteLine($"{Environment.NewLine}Write string to convert it to int");
                    string input = Console.ReadLine();
                    int intConvertedString = stringLibrary.StringToInt(input);
                    int defaultParse = int.Parse(input);                           // This method is for test purpose
                    Console.WriteLine($"Success! The number is [{intConvertedString}] and default method is [{defaultParse}]");
                    consoleAppLogger.LogInformation("Converting is a success");
                }
                catch (FormatException e)
                {
                    ErrorMessageToConsole(e.Message, consoleAppLogger);
                }
                catch (OverflowException e)
                {
                    ErrorMessageToConsole(e.Message, consoleAppLogger);
                }
                catch (ArgumentException e)
                {
                    ErrorMessageToConsole(e.Message, consoleAppLogger);
                }
                Console.WriteLine("Type 'y' if you to convert another number");
                key = Console.ReadKey();
                Console.WriteLine();
            }
            while (char.ToLowerInvariant(key.KeyChar).Equals('y'));
            consoleAppLogger.LogInformation("Closing app");
        }
        private static void ErrorMessageToConsole(string message, ILogger<StringLib> consoleAppLogger)
        {
            consoleAppLogger.LogInformation("Converting is a failure");
            Console.WriteLine(message, Console.ForegroundColor = ConsoleColor.Red);
            Console.ResetColor();
        }
    }
}
