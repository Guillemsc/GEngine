using System;
using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Events.Extensions
{
    public static class AsyncEventExtensions
    {
        public static void ClearAndAddListener<T>(this IListenAsyncEvent<T> @event, Func<T, CancellationToken, Task> action)
        {
            @event.ClearListeners();
            @event.AddListener(action);
        }

        public static Task Raise(this IRaiseAsyncEvent<EventArgs> @event, CancellationToken ct)
        {
            return @event.Raise(EventArgs.Empty, ct);
        }
    }
}
