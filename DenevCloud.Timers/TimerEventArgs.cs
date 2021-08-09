using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenevCloud.Timers
{
    public class TimerEventArgs : EventArgs
    {
        public DateTime LastTriggered { get; set; }
        public TimeSpan TimeElapsed { get; set; }
        public TimeSpan TimeRemaining { get; set; }

    }
}
