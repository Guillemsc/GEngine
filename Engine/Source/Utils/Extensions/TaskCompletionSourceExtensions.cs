using GEngine.Utils.Optionals;
using GEngine.Utils.Tasks.CompletionSources;
using TaskCompletionSource = GEngine.Utils.Tasks.CompletionSources.TaskCompletionSource;

namespace GEngine.Utils.Extensions
{
    public static class TaskCompletionSourceExtensions
    {
        /// <summary>
        /// If the <see cref="CancellationToken"/> can be canceled, registers
        /// <see cref="TaskCompletionSource{T}.TrySetCanceled()"/> to the <see cref="CancellationToken"/>.
        /// </summary>
        public static void LinkCancellationToken<T>(
            this TaskCompletionSource<T> taskCompletionSource,
            CancellationToken ct)
        {
            if (!ct.CanBeCanceled)
            {
                return;
            }

            ct.Register(() => taskCompletionSource.TrySetCanceled(ct));
        }

        /// <summary>
        /// If the <see cref="CancellationToken"/> can be canceled, registers
        /// <see cref="Tasks.CompletionSources.TaskCompletionSource.TrySetResult()"/> to the <see cref="CancellationToken"/>.
        /// </summary>
        public static void LinkCancellationTokenAsComplete(
            this Tasks.CompletionSources.TaskCompletionSource taskCompletionSource,
            CancellationToken ct)
        {
            if (!ct.CanBeCanceled)
            {
                return;
            }

            ct.Register(taskCompletionSource.TrySetResult);
        }

        /// <summary>
        /// If the <see cref="CancellationToken"/> can be canceled, registers
        /// <see cref="TaskCompletionSource{T}.TrySetResult"/> to the <see cref="CancellationToken"/>,
        /// with a default value as result.
        /// </summary>
        public static void LinkCancellationTokenAsCompleteDefaultResult<T>(
            this TaskCompletionSource<T> taskCompletionSource,
            CancellationToken ct)
        {
            if (!ct.CanBeCanceled)
            {
                return;
            }

            ct.Register(() => taskCompletionSource.TrySetResult(default));
        }

        /// <summary>
        /// If the <see cref="CancellationToken"/> can be canceled, registers
        /// <see cref="TaskCompletionSource{T}.TrySetResult"/> to the <see cref="CancellationToken"/>,
        /// with the provided value as result.
        /// </summary>
        public static void LinkCancellationTokenAsCompleteWithResult<T>(
            this TaskCompletionSource<T> taskCompletionSource,
            CancellationToken ct,
            T result)
        {
            if (!ct.CanBeCanceled)
            {
                return;
            }

            ct.Register(() => taskCompletionSource.TrySetResult(result));
        }

        /// <summary>
        /// Awaits a TaskCompletionSource object, catching a TaskCanceledException and ignoring it.
        /// </summary>
        /// <typeparam name="T">The type of the result returned by the TaskCompletionSource.</typeparam>
        /// <param name="taskCompletionSource">The TaskCompletionSource object to await.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public static async Task AwaitCancellableTask<T>(this TaskCompletionSource<T> taskCompletionSource)
        {
            try
            {
                await taskCompletionSource.Task;
            }
            catch (Exception exception)
            {
                if (exception is not TaskCanceledException _)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Awaits a TaskCompletionSource object, catching TaskCanceledException exceptions, and returns the result as an <see cref="Optional{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="taskCompletionSource">The <see cref="TaskCompletionSource{T}"/> to await.</param>
        /// <returns>An <see cref="Optional{T}"/> that contains the result of the <see cref="TaskCompletionSource{T}"/>
        /// if it completed successfully, or <see cref="Optional{T}.None"/> if the task was canceled.</returns>
        public static async Task<Optional<T>> AwaitCancellableTaskWithResult<T>(this TaskCompletionSource<T> taskCompletionSource)
        {
            try
            {
                T result = await taskCompletionSource.Task;

                return Optional<T>.Some(result);
            }
            catch (Exception exception)
            {
                if (exception is not TaskCanceledException _)
                {
                    throw;
                }
            }

            return Optional<T>.None;
        }
    }
}
