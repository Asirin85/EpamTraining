using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApp
{
    public class Countdown
    {
        private System.Timers.Timer _timer;
        private bool _timeElapsed = false;
        public class CountdownArgs
        {
            public string Message { get; internal set; }
        }
        public event EventHandler<CountdownArgs> Send;

        private void HandleSendEvent(CountdownArgs mes)
        {
            var tmp = Send;
            if (tmp != null) tmp(this, mes);
        }
        private void TimerElapsed(object sender, EventArgs e)
        {
            _timer.Stop();
            Send = null;
            _timeElapsed = true;
        }
        public void StartCountdown(int time)
        {
            _timer = new System.Timers.Timer(time);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }
        public void TransmitMesage()
        {
            if (_timeElapsed)
                HandleSendEvent(new CountdownArgs { Message = "Hello to my subscribers!" });
        }
    }
}
