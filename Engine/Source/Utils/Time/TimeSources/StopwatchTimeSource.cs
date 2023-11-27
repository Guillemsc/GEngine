using System;
using System.Diagnostics;

namespace GEngine.Utils.Time.TimeSources
{
    /// <inheritdoc />
    /// <summary>
    /// Time source that uses C#'s <see cref="Stopwatch"/> as time source.
    /// Stopwatch is started when the object is constructed.
    /// </summary>
    public sealed class StopwatchTimeSource : ITimeSource
    {
        readonly Stopwatch _stopwatch;

        public StopwatchTimeSource()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public TimeSpan Time => _stopwatch.Elapsed;
    }
}
