using System;
using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Events
{
    /// <summary>
    /// Provides methods for listening to an event.
    /// </summary>
    public interface IListenAsyncEvent<out T>
    {
        IListenEvent<T> OnBeforeRaise { get; }

        void AddListener(Func<T, CancellationToken, Task> listener);
        void RemoveListener(Func<T, CancellationToken, Task> listener);
        void ClearListeners();
    }
}
