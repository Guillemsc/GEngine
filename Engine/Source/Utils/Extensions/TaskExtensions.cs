using System.Collections;
using System.Runtime.CompilerServices;
using GEngine.Utils.Optionals;
using GEngine.Utils.Types;

namespace GEngine.Utils.Extensions
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Awaits for the completion of the task.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static Task AwaitAsync(this Task task, CancellationToken cancellationToken)
        {
            if (task.IsCompleted || !cancellationToken.CanBeCanceled)
            {
                return task;
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return Task.CompletedTask;
            }

            TaskCompletionSource<object> taskCompletionSource = new();
            taskCompletionSource.LinkCancellationTokenAsCompleteDefaultResult(cancellationToken);

            return Task.WhenAny(
                task,
                taskCompletionSource.Task
            );
        }

        public static async Task<Optional<T>> AsCancellable<T>(this Task<T> task)
        {
            try
            {
                T result = await task;
                return Optional<T>.Some(result);
            }
            catch (TaskCanceledException)
            {
                return Optional<T>.None;
            }
        }

        public static async Task<Optional<Nothing>> AsCancellable(this Task task)
        {
            try
            {
                await task;
                return Optional<Nothing>.Some(Nothing.Instance);
            }
            catch (TaskCanceledException)
            {
                return Optional<Nothing>.None;
            }
        }

        public static YieldAwaitable AwaitNextFrame() => Task.Yield();

        /// <summary>
        /// Asynchronously waits until the provided function returns true.
        /// </summary>
        /// <param name="func">The function to check for the condition.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task AwaitUntil(Func<bool> func)
        {
            while (!func.Invoke())
            {
                await Task.Yield();
            }
        }

        /// <summary>
        /// Asynchronously waits until the provided function returns true.
        /// </summary>
        /// <param name="func">The function to check for the condition.</param>
        /// <param name="cancellationToken">The token to cancel the waiting.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task AwaitUntil(Func<bool> func, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return;

            while (!func.Invoke())
            {
                if (cancellationToken.IsCancellationRequested) break;

                await Task.Yield();
            }
        }

        /// <summary>
        /// Runs a task asynchronously.
        /// Any exceptions thrown by the task are automatically logged to the Unity console.
        /// </summary>
        /// <param name="task">The task to run.</param>
        public static async void RunAsync(this Task task)
        {
            await task;
        }

        /// <summary>
        /// Runs a task asynchronously.
        /// If an exception is thrown, it will be caught and passed to the provided onException handler.
        /// </summary>
        /// <param name="task">The task to run asynchronously.</param>
        /// <param name="onException">Action to handle any exception thrown by the task.</param>
        public static async void RunAsync(this Task task, Action<Exception> onException)
        {
            try
            {
                await task;
            }
            catch (Exception exception)
            {
                onException?.Invoke(exception);
            }
        }

        /// <summary>
        /// Runs a task asynchronously.
        /// Any exceptions thrown by the task are automatically logged to the Unity console.
        /// </summary>
        /// <param name="task">The task to run asynchronously.</param>
        /// <param name="onComplete">Action called when the task has finished running.</param>
        public static async void RunAsync(this Task task, Action onComplete)
        {
            await task;

            onComplete.Invoke();
        }

        /// <summary>
        /// Runs a task asynchronously.
        /// Any exceptions thrown by the task are automatically logged to the Unity console.
        /// </summary>
        /// <param name="task">The task to run asynchronously.</param>
        /// <param name="onComplete">Action called when the task has finished running.</param>
        public static async void RunAsync<T>(this Task<T> task, Action<T> onComplete)
        {
            T result = await task;

            onComplete.Invoke(result);
        }
    }
}
