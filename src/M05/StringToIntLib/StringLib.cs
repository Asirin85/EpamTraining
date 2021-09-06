using Microsoft.Extensions.Logging;
using System;
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
                if (input is { Length: > 0 })
                {
                    string stringToInt = input;
                    if (input.Contains('.')) stringToInt = input[0..input.IndexOf('.')];
                    for (int i = stringToInt.Length - 1; i >= 0; i--)
                    {
                        if (char.IsDigit(stringToInt[i]))
                        {

                            result += checked((stringToInt[i] - SUBSTRACT_FROM_CHAR) * digit);
                            digit *= 10;
                        }
                        else if (i == 0 && stringToInt[i].Equals('-')) result = 0 - result;
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
