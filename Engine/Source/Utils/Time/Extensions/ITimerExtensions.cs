using System;
using GEngine.Utils.Time.Timers;

namespace GEngine.Utils.Time.Extensions
{
    public static class ITimerExtensions
    {
        public static bool HasReachedOrNotStarted(this ITimer timer, TimeSpan timeSpan)
        {
            return !timer.Started || timer.HasReached(timeSpan);
        }
    }
}
