using System;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            var countdown = new Countdown();
            var testClass = new TestClassForCountdown(countdown);
            countdown.Send += MainSender;
            testClass.AddSubscriber();
            countdown.StartCountdown(2000);
            countdown.TransmitMesage();
            Console.ReadKey();
            Thread.Sleep(2000);
            countdown.Send += MainSender;
            testClass.AddSubscriber();
            countdown.TransmitMesage();
            Console.ReadKey();
        }
        private static void MainSender(object sender, Countdown.CountdownArgs e)
        {
            Console.WriteLine($"{e.Message} in Program Class!");
        }
    }
}
