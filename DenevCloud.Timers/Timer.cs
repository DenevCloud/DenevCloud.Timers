using DenevCloud.Timers;
using System;
using System.Threading.Tasks;

namespace DenevCloud
{
    public class Timer : IDisposable
    {
        public event EventHandler<TimerEventArgs> TriggerEvent;
        public event EventHandler<TimerEventArgs> HanldingEvent;

        public readonly int Delay;
        public readonly TimeSpan TimeSpanTrigger;

        public DateTime LastTriggered { get; private set; }

        private bool Canceled;

        public Timer(TimeSpan TimeSpanTrigger, int Delay, DateTime LastTriggered)
        {
            this.Delay = Delay;
            this.TimeSpanTrigger = TimeSpanTrigger;
            this.LastTriggered = LastTriggered;
        }

        public async Task StartAsync()
        {
            var data = new TimerEventArgs();

            Canceled = false;

            while (!Canceled)
            {
                if (LastTriggered.Add(TimeSpanTrigger) <= DateTime.Now)
                {
                    LastTriggered = DateTime.Now;
                    data.LastTriggered = LastTriggered;
                    data.TimeElapsed = TimeSpan.Zero;
                    data.TimeRemaining = TimeSpan.Zero;
                    OnTrigger(data);
                }
                else
                {
                    data.LastTriggered = LastTriggered;
                    data.TimeElapsed = -LastTriggered.Subtract(DateTime.Now);
                    data.TimeRemaining = LastTriggered.Add(TimeSpanTrigger).Subtract(DateTime.Now);
                    OnRunning(data);
                }
                await Task.Delay(Delay);
            }
        }

        public void Trigger()
        {
            var data = new TimerEventArgs();
            data.LastTriggered = LastTriggered;
            data.TimeElapsed = -LastTriggered.Subtract(DateTime.Now);
            data.TimeRemaining = TimeSpan.Zero;
            OnTrigger(data);
        }

        public void Reset()
        {
            LastTriggered = DateTime.Now;
        }

        public void Stop()
        {
            Canceled = true;
        }

        protected virtual void OnRunning(TimerEventArgs e)
        {
            HanldingEvent?.Invoke(this, e);
        }

        protected virtual void OnTrigger(TimerEventArgs e)
        {
            TriggerEvent?.Invoke(this, e);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}