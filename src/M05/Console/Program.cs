using NLog;
using StringToIntLib;
using Microsoft.Extensions.Logging;
using System;
using Ninject;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel ninjectKernel = new StandardKernel(new IOCConfigModule());
            var consoleAppLogger = ninjectKernel.Get<ConsoleAppLogger>();
            consoleAppLogger.LogInformation("Initializing my console app");
            var stringLibrary = new StringLib(consoleAppLogger);
            Console.WriteLine("Press any key to start");
            var key = Console.ReadKey();
            do
            {
                try
                {
                    consoleAppLogger.LogInformation("Starting converting");
                    Console.WriteLine($"{Environment.NewLine}Write string to convert it to int");
                    var input = Console.ReadLine();
                    var intConvertedString = stringLibrary.StringToInt(input);
                    Console.WriteLine($"Success! The number is [{intConvertedString}]");
                    consoleAppLogger.LogInformation("Converting was a success");
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
            consoleAppLogger.LogInformation("Converting was a failure");
            Console.WriteLine(message, Console.ForegroundColor = ConsoleColor.Red);
            Console.ResetColor();
        }
    }
}
