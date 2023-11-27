using System;
using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Time.Timers
{
    public sealed class NopTimer : ITimer
    {
        public static readonly NopTimer Instance = new();

        NopTimer()
        {

        }

        public bool Started => false;
        public bool Paused => false;
        public bool StartedAndNotPaused => false;
        public TimeSpan Time => TimeSpan.Zero;

        public void Start() { }
        public void Pause() { }
        public void Resume() { }
        public void Reset() { }
        public void Restart() { }
        public void Add(TimeSpan timeSpan) { }

        public bool HasReached(TimeSpan time) => true;
        public Task AwaitReach(TimeSpan time, CancellationToken cancellationToken) => Task.CompletedTask;
        public Task StartAndAwaitReach(TimeSpan time, CancellationToken cancellationToken) => Task.CompletedTask;
        public Task AwaitSpan(TimeSpan time, CancellationToken cancellationToken) => Task.CompletedTask;
        public Task StartAndAwaitSpan(TimeSpan time, CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
