using System;
using System.Timers;
using Topshelf;

namespace Service.Core
{
    public class RecurrenceIntervalControl : ServiceControl
    {

        readonly Timer _timer;

        public RecurrenceIntervalControl(double interval)
        {
            _timer = new Timer(interval) { AutoReset = true };
        }

        public void Execute(Action action)
        {
            _timer.Elapsed += (sender, eventArgs) =>
            {
                _timer.Stop();

                action();

                _timer.Start();

            };
        }

        public bool Start(HostControl hostControl)
        {
            _timer.Start();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _timer.Stop();
            return true;
        }
    }
}
