using GEngine.Utils.Time.TimeSources;

namespace GEngine.Utils.Time.Timers
{
    /// <inheritdoc />
    /// <summary>
    /// Generic implementation of a timer, with exception of the TimeSource.
    /// </summary>
    public class TimeSourceTimer : ITimer
    {
        readonly ITimeSource _timeSource;

        TimeSpan _startTime;
        TimeSpan _pausedTime;

        public bool Started { get; private set; }
        public bool Paused { get; private set; }
        public bool StartedAndNotPaused => Started && !Paused;

        public TimeSpan Time
        {
            get
            {
                if (!Started)
                {
                    return TimeSpan.Zero;
                }

                if (Paused)
                {
                    return _pausedTime;
                }

                return _timeSource.Time - _startTime;
            }
        }

        public TimeSourceTimer(ITimeSource timeSource)
        {
            _timeSource = timeSource;
        }

        public void Start()
        {
            if (Started)
            {
                return;
            }

            if (Paused)
            {
                return;
            }

            Started = true;

            _startTime = _timeSource.Time;
        }

        public void Pause()
        {
            if (!Started)
            {
                return;
            }

            if (Paused)
            {
                return;
            }

            _pausedTime = Time;

            Paused = true;
        }

        public void Resume()
        {
            if (!Started)
            {
                return;
            }

            if (!Paused)
            {
                return;
            }

            Paused = false;

            _startTime = _timeSource.Time - _pausedTime;
        }

        public void Reset()
        {
            Started = false;
            Paused = false;

            _startTime = TimeSpan.Zero;
            _pausedTime = TimeSpan.Zero;
        }

        public void Restart()
        {
            Reset();
            Start();
        }

        public void Add(TimeSpan timeSpan)
        {
            if (!Started)
            {
                return;
            }

            if (Paused)
            {
                _pausedTime += timeSpan;
            }
            else
            {
                _startTime -= timeSpan;   
            }
        }

        public bool HasReached(TimeSpan timeSpan)
        {
            if (!Started)
            {
                return false;
            }

            return TimeSpan.Compare(timeSpan, Time) == -1;
        }

        public async Task AwaitReach(TimeSpan time, CancellationToken cancellationToken)
        {
            while (!HasReached(time) && !cancellationToken.IsCancellationRequested)
            {
                await Task.Yield();
            }
        }

        public Task StartAndAwaitReach(TimeSpan time, CancellationToken cancellationToken)
        {
            Start();

            return AwaitReach(time, cancellationToken);
        }

        public Task AwaitSpan(TimeSpan time, CancellationToken cancellationToken)
        {
            TimeSpan timeToReach = Time + time;

            return AwaitReach(timeToReach, cancellationToken);
        }

        public Task StartAndAwaitSpan(TimeSpan time, CancellationToken cancellationToken)
        {
            Start();

            return AwaitSpan(time, cancellationToken);
        }

        public static ITimer FromStarted(ITimeSource timeSource)
        {
            ITimer timer = new TimeSourceTimer(timeSource);
            timer.Start();
            return timer;
        }

        public static Task Await(ITimeSource timeSource, TimeSpan timeSpan, CancellationToken cancellationToken)
        {
            ITimer timer = new TimeSourceTimer(timeSource);
            timer.Start();
            return timer.AwaitReach(timeSpan, cancellationToken);
        }
    }
}
