using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class SumClass
    {
        public static string SumOfStrings(string first, string second)
        {
            if (first is { Length: > 0 } && second is { Length: > 0 })
            {
                StringBuilder res = new StringBuilder();
                int carry = 0;
                int markFirst = first.Length - 1;
                int markSecond = second.Length - 1;
                while (markFirst >= 0 || markSecond >= 0)
                {
                    int numFirst = 0;
                    int numSecond = 0;
                    if (markFirst >= 0)
                    {
                        if (char.IsDigit(first[markFirst]))
                            numFirst = first[markFirst] - '0';
                        else throw new FormatException("Wrong symbol in first input");
                    }
                    if (markSecond >= 0)
                    {
                        if (char.IsDigit(second[markSecond]))
                            numSecond = second[markSecond] - '0';
                        else throw new FormatException("Wrong symbol in second input");
                    }
                    int sum = numFirst + numSecond + carry;
                    if (sum >= 10)
                    {
                        carry = sum / 10;
                        sum = sum % 10;
                    }
                    else carry = 0;
                    res.Insert(0, sum);
                    markFirst--;
                    markSecond--;
                }
                if (carry > 0) res.Insert(0, carry);
                return res.ToString();
            }
            else throw new ArgumentException("Wrong input data, either null or empty");
        }
    }
}
