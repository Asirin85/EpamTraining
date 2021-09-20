using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class TestClassForCountdown
    {
        public Countdown Countdown { get; }
        public TestClassForCountdown(Countdown countdown)
        {
            Countdown = countdown;
        }
        public void AddSubscriber()
        {
            Countdown.Send += TestClassSender;
        }
        private void TestClassSender(object sender, Countdown.CountdownArgs e)
        {
            Console.WriteLine($"{e.Message} in TestClass!");
        }
    }
}
