using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Events
{
    public class SuccessiveAsyncEvent<T> : ISuccessiveAsyncEvent<T>
    {
        readonly List<Func<T, CancellationToken, Task>> _events = new();
        readonly List<Func<T, CancellationToken, Task>> _eventsCache = new();
        readonly IEvent<T> _onBeforeRaise = new Event<T>();

        IListenEvent<T> IListenAsyncEvent<T>.OnBeforeRaise => _onBeforeRaise;

        IEvent<T> IAsyncEvent<T>.OnBeforeRaise => _onBeforeRaise;

        public async Task Raise(T data, CancellationToken ct)
        {
            _eventsCache.Clear();
            _eventsCache.AddRange(_events);

            _onBeforeRaise.Raise(data);

            foreach (var func in _eventsCache)
            {
                await func.Invoke(data, ct);
                if (ct.IsCancellationRequested)
                {
                    break;
                }
            }
            _eventsCache.Clear();
        }

        public void AddListener(Func<T, CancellationToken, Task> listener)
        {
            _events.Add(listener);
        }

        public void RemoveListener(Func<T, CancellationToken, Task> listener)
        {
            _events.Remove(listener);
        }

        public void ClearListeners()
        {
            _events.Clear();
        }
    }
}
