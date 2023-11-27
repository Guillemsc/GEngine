using System;
using System.Threading;
using System.Threading.Tasks;
using GEngine.Utils.Optionals;
using GEngine.Utils.Extensions;

namespace GEngine.Utils.Events.Extensions
{
    public static class EventExtensions
    {
        /// <summary>
        /// Awaits until the event is raised once, and then continues.
        /// </summary>
        public static async Task AwaitRaise<T>(this IListenEvent<T> @event, CancellationToken cancellationToken)
        {
            TaskCompletionSource<T> taskCompletionSource = new();
            taskCompletionSource.LinkCancellationToken(cancellationToken);

            void Complete(T value) => taskCompletionSource.TrySetResult(value);

            @event.AddListener(Complete);

            await taskCompletionSource.AwaitCancellableTask();

            @event.RemoveListener(Complete);
        }

        [Obsolete("This method is obsolete. Use AwaitRaise instead")]
        public static Task Wait<T>(this IListenEvent<T> @event, CancellationToken cancellationToken)
        {
            return @event.AwaitRaise(cancellationToken);
        }

        /// <summary>
        /// Awaits until the event is raised once, and then continues, returning the raised arguments.
        /// </summary>
        /// <returns>Optional return of the raised event arguments. In case the awaiting is cancelled, the optional result is empty.</returns>
        public static async Task<Optional<T>> AwaitRaiseWithResult<T>(this IListenEvent<T> @event, CancellationToken cancellationToken)
        {
            TaskCompletionSource<Optional<T>> taskCompletionSource = new();

            taskCompletionSource.LinkCancellationTokenAsCompleteWithResult(cancellationToken, Optional<T>.None);

            void Complete(T value) => taskCompletionSource.TrySetResult(value);

            @event.AddListener(Complete);

            Optional<T> result = await taskCompletionSource.Task;

            @event.RemoveListener(Complete);

            return result;
        }

        [Obsolete("This method is obsolete. Use AwaitRaiseWithResult instead")]
        public static Task<Optional<T>> WaitWithResult<T>(this IListenEvent<T> @event, CancellationToken cancellationToken)
        {
            return @event.AwaitRaiseWithResult(cancellationToken);
        }

        /// <summary>
        /// Awaits until the event is raised once, and then continues, returning the raised arguments.
        /// Warning: If task gets canceled and it's being awaited, it will throw.
        /// </summary>
        /// <returns>Raised event arguments.</returns>
        public static Task<T> AwaitRaiseWithResultUnsafe<T>(
            this IListenEvent<T> @event,
            Predicate<T> predicate,
            CancellationToken cancellationToken
            )
        {
            TaskCompletionSource<T> taskCompletionSource = new();
            taskCompletionSource.LinkCancellationToken(cancellationToken);

            void Complete(T value)
            {
                if (!predicate.Invoke(value))
                {
                    return;
                }

                @event.RemoveListener(Complete);
                taskCompletionSource.TrySetResult(value);
            }

            @event.AddListener(Complete);

            return taskCompletionSource.Task;
        }

        [Obsolete("This method is obsolete. Use AwaitRaiseWithResultUnsafe instead")]
        public static Task<T> AwaitRaise<T>(this IListenEvent<T> @event, Predicate<T> predicate, CancellationToken cancellationToken)
        {
            return @event.AwaitRaiseWithResultUnsafe(predicate, cancellationToken);
        }

        [Obsolete("This method is obsolete. Use AwaitRaise instead")]
        public static Task<T> Wait<T>(this IListenEvent<T> @event, Predicate<T> predicate, CancellationToken cancellationToken)
        {
            return @event.AwaitRaise(predicate, cancellationToken);
        }

        /// <summary>
        /// Clears all the listeners and then adds a new listener.
        /// </summary>
        public static void ClearAndAddListener<T>(this IListenEvent<T> @event, Action<T> action)
        {
            @event.ClearListeners();
            @event.AddListener(action);
        }

        /// <summary>
        /// Raises the event with empty <see cref="EventArgs"/>.
        /// </summary>
        public static void Raise(this IRaiseEvent<EventArgs> @event)
        {
            @event.Raise(EventArgs.Empty);
        }
    }
}
