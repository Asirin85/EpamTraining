using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace StringToIntLib
{
    public class StringLib
    {
        private readonly ILogger _logger;
        private const int SUBSTRACT_FROM_CHAR = 48;
        public StringLib(ILogger<StringLib> logger)
        {
            _logger = logger;
        }
        public int StringToInt(string input)
        {
            int result = 0;
            int digit = 1;
            try
            {
                _logger.LogInformation($"Received input [{input}]");
                if (input is { Length: > 0 })
                {
                    string stringToInt = input;
                    if (input.Contains('.')) stringToInt = input[0..input.IndexOf('.')];
                    _logger.LogInformation($"Input was cut to [{stringToInt}]");
                    _logger.LogInformation("String symbols are being checked..");
                    for (int i = stringToInt.Length - 1; i >= 0; i--)
                    {
                        _logger.LogInformation($"Current symbol is {stringToInt[i]}");
                        if (char.IsDigit(stringToInt[i]))
                        {
                            _logger.LogInformation($"Current symbol is digit, current number is [{result}]");
                            result = checked(result+((stringToInt[i] - SUBSTRACT_FROM_CHAR) * digit));
                            _logger.LogInformation($"Adding current symbol to previous number, result is [{result}]");
                            digit *= 10;
                        }
                        else if (i == 0 && stringToInt[i].Equals('-'))
                        {
                            _logger.LogInformation("Current symbol is minus, reversing number");
                            result = 0 - result;
                        }
                        else throw new FormatException("Wrong symbol in string that can't be cast to int!");
                    }
                }
                else throw new ArgumentException("Input string is null or empty!");
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
            catch (FormatException e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
            catch (OverflowException e)
            {
                string newMessage = "Number you passed was bigger/lesser than max/min value of int!";
                _logger.LogError(e, newMessage);
                throw new OverflowException(newMessage, e);
            }
            return result;
        }
    }
}
