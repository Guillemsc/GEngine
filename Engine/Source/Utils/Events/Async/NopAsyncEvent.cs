using System;
using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Events
{
    public sealed class NopAsyncEvent<T> : IAsyncEvent<T>
    {
        public static readonly NopAsyncEvent<T> Instance = new();

        IListenEvent<T> IListenAsyncEvent<T>.OnBeforeRaise => NopEvent<T>.Instance;
        IEvent<T> IAsyncEvent<T>.OnBeforeRaise => NopEvent<T>.Instance;

        NopAsyncEvent() { }

        public void AddListener(Func<T, CancellationToken, Task> listener) { }
        public void RemoveListener(Func<T, CancellationToken, Task> listener) { }
        public void ClearListeners() { }
        public Task Raise(T data, CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
