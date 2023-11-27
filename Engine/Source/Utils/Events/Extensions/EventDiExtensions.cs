using System;
using GEngine.Utils.Di.Builder;
using GEngine.Utils.Di.Container;
using GEngine.Utils.Events;

namespace GEngine.Utils.Events.Extensions
{
    public static class EventDiExtensions
    {
        public static IDiContainerBuilder LinkEvent<TEventData>(
            this IDiContainerBuilder builder,
            Func<IDiResolveContainer?, IListenEvent<TEventData>> getEvent,
            Func<IDiResolveContainer?, Action<TEventData>> func
        )
        {
            IListenEvent<TEventData> listenEvent = null;
            Action<TEventData> action = null;

            builder.WhenBuild(c =>
            {
                listenEvent = getEvent.Invoke(c);
                action = func.Invoke(c);

                listenEvent.AddListener(action);
            });

            builder.WhenDispose(c =>
            {
                listenEvent.RemoveListener(action);
            });

            return builder;
        }

        public static IDiBindingActionBuilder<T> LinkEvent<T, TEventData>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Func<IDiResolveContainer, IListenEvent<TEventData>> getEvent,
            Func<T, Action<TEventData>> func
        )
        {
            IListenEvent<TEventData> listenEvent = null;
            Action<TEventData> action = null;

            actionBuilder.WhenInit((c, o) =>
            {
                listenEvent = getEvent.Invoke(c);
                action = func.Invoke(o);

                listenEvent.AddListener(action);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                listenEvent.RemoveListener(action);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkEvent<T, TEventData>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Func<IDiResolveContainer, IListenEvent<TEventData>> getEvent,
            Func<T, Action<TEventData>> func,
            Predicate<TEventData> eventFiresPredicate)
        {
            IListenEvent<TEventData> listenEvent = null;
            Action<TEventData> action = null;

            void Listener(TEventData eventData)
            {
                if (!eventFiresPredicate.Invoke(eventData))
                {
                    return;
                }
                action.Invoke(eventData);
            }

            actionBuilder.WhenInit((c, o) =>
            {
                listenEvent = getEvent.Invoke(c);
                action = func.Invoke(o);

                listenEvent.AddListener(Listener);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                listenEvent.RemoveListener(Listener);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkEvent<T, TEventData>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Func<IDiResolveContainer, IListenEvent<TEventData>> getEvent,
            Func<T, Action> func
        )
        {
            IListenEvent<TEventData> listenEvent = null;
            Action action = null;

            void Listener(TEventData eventData)
            {
                action.Invoke();
            }

            actionBuilder.WhenInit((c, o) =>
            {
                listenEvent = getEvent.Invoke(c);
                action = func.Invoke(o);

                listenEvent.AddListener(Listener);
            });

            actionBuilder.WhenDispose(o =>
            {
                listenEvent.RemoveListener(Listener);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkEvent<T, TEventData>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Func<IDiResolveContainer, IListenEvent<TEventData>> getEvent,
            Func<T, Action> func,
            Predicate<TEventData> eventFiresPredicate)
        {
            IListenEvent<TEventData> listenEvent = null;
            Action action = null;

            void Listener(TEventData eventData)
            {
                if (!eventFiresPredicate.Invoke(eventData))
                {
                    return;
                }
                action.Invoke();
            }

            actionBuilder.WhenInit((c, o) =>
            {
                listenEvent = getEvent.Invoke(c);
                action = func.Invoke(o);

                listenEvent.AddListener(Listener);
            });

            actionBuilder.WhenDispose(o =>
            {
                listenEvent.RemoveListener(Listener);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }
    }
}
