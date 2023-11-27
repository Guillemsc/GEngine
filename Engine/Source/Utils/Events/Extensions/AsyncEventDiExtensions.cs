using System;
using System.Threading;
using System.Threading.Tasks;
using GEngine.Utils.Di.Builder;
using GEngine.Utils.Di.Container;
using GEngine.Utils.Extensions;

namespace GEngine.Utils.Events.Extensions
{
    public static class AsyncEventDiExtensions
    {
        public static IDiBindingActionBuilder<T> LinkAsyncEvent<T, TEventData>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Func<IDiResolveContainer, IAsyncEvent<TEventData>> getEvent,
            Func<T, Func<TEventData, CancellationToken, Task>> func
        )
        {
            IAsyncEvent<TEventData> listenEvent = null;
            Func<TEventData, CancellationToken, Task> function = null;

            actionBuilder.WhenInit((c, o) =>
            {
                listenEvent = getEvent.Invoke(c);
                function = func.Invoke(o);

                listenEvent.AddListener(function);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                listenEvent.RemoveListener(function);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkAsyncEvent<T, TEventData>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Func<IDiResolveContainer, IAsyncEvent<TEventData>> getEvent,
            Func<T, Func<CancellationToken, Task>> func
        )
        {
            IAsyncEvent<TEventData> listenEvent = null;
            Func<TEventData, CancellationToken, Task> function = null;

            actionBuilder.WhenInit((c, o) =>
            {
                listenEvent = getEvent.Invoke(c);
                function = func.Invoke(o).PrependParameterInvoke<TEventData, CancellationToken, Task>();

                listenEvent.AddListener(function);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                listenEvent.RemoveListener(function);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }
    }
}
