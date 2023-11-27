using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using GEngine.Utils.Time.TimeSources;

namespace GEngine.Utils.Time.Timers
{
    /// <inheritdoc />
    /// <summary>
    /// Timer that uses the C#'s <see cref="Stopwatch"/> as time source.
    /// </summary>
    public sealed class StopwatchTimer : TimeSourceTimer
    {
        public StopwatchTimer() : base(new StopwatchTimeSource())
        {

        }

        public static ITimer FromStarted()
        {
            ITimer timer = new StopwatchTimer();
            timer.Start();
            return timer;
        }

        public static Task Await(TimeSpan timeSpan, CancellationToken cancellationToken)
        {
            ITimer timer = new StopwatchTimer();
            timer.Start();
            return timer.AwaitReach(timeSpan, cancellationToken);
        }
    }
}
