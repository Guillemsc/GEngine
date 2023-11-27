using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Events
{
    public class ParallelAsyncEvent<T> : ISuccessiveAsyncEvent<T>
    {
        readonly List<Func<T, CancellationToken, Task>> _events = new();
        readonly IEvent<T> _onBeforeRaise = new Event<T>();

        IListenEvent<T> IListenAsyncEvent<T>.OnBeforeRaise => _onBeforeRaise;

        IEvent<T> IAsyncEvent<T>.OnBeforeRaise => _onBeforeRaise;

        public Task Raise(T data, CancellationToken ct)
        {
            _onBeforeRaise.Raise(data);

            return Task.WhenAll(
                _events.Select(x => x.Invoke(data, ct))
            );
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
