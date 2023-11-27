using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GEngine.Utils.TimeSlicing.Awaiting
{
    /// <inheritdoc />
    public sealed class TimeSlicingAwaiter : ITimeSlicingAwaiter
    {
        readonly Stopwatch _stopwatch = new();

        readonly TimeSpan _budget;

        public TimeSlicingAwaiter(
            TimeSpan budget
            )
        {
            _budget = budget;
        }

        /// <summary>
        /// Creates a time slicer that has already been started.
        /// </summary>
        public static TimeSlicingAwaiter FromStarted(TimeSpan budget)
        {
            TimeSlicingAwaiter awaiter = new(budget);
            awaiter.Start();
            return awaiter;
        }

        public void Start()
        {
            _stopwatch.Restart();
        }

        public async ValueTask TryTimeSlice()
        {
            if (_stopwatch.Elapsed < _budget)
            {
                return;
            }

            await Task.Yield();

            _stopwatch.Restart();
        }
    }
}
