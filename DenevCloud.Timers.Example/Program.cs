using System;
using System.Threading.Tasks;
using DenevCloud.Timers;

namespace DenevCloud.Timers.Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            TimeSpan TimeToPass = TimeSpan.FromSeconds(5);
            //Trigger event will occur every 5 seconds and an update/handle event will occur every 1000ms (1 second)
            Timer timer = new Timer(TimeToPass, 1000, DateTime.Now);

            timer.TriggerEvent += Timer_TriggerEvent;
            timer.HandlingEvent += Timer_HandlingEvent;

            timer.StartAsync().GetAwaiter();

            await Task.Delay(7000);

            timer.Trigger();

            Console.ReadKey();
        }

        private static void Timer_HandlingEvent(object sender, TimerEventArgs e)
        {
            Console.WriteLine($"Update: {e.TimeRemaining}");
        }

        private static void Timer_TriggerEvent(object sender, TimerEventArgs e)
        {
            Console.WriteLine($"Triggered");
        }
    }
}