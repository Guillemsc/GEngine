using System;
using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Extensions
{
    public static class DelegateExtensions
    {
        /// <summary>
        /// Returns the value passed to it. Useful for converting some value to: a function returning that same value.
        /// </summary>
        public static T Self<T>(T value) => value;

        /// <summary>
        /// Does not do anything. Useful for passing it as a dummy action.
        /// </summary>
        public static void DoNothing() { }

        /// <summary>
        /// Does not do anything. Useful for passing it as a dummy action.
        /// </summary>
        public static void DoNothing<T>(T value) { }

        /// <summary>
        /// Does not do anything. Useful for passing it as a dummy action.
        /// </summary>
        public static void DoNothing<T1, T2>(T1 value1, T2 value2) { }

        /// <summary>
        /// Returns true. Useful for passing it as a predicate.
        /// </summary>
        public static bool True() => true;

        /// <summary>
        /// Returns true. Useful for passing it as a predicate.
        /// </summary>
        public static bool True<T>(T value) => true;

        /// <summary>
        /// Returns <see cref="Task.CompletedTask"/>. Useful for passing it as a dummy Task.
        /// </summary>
        public static Task CompletedTask()
            => Task.CompletedTask;

        /// <summary>
        /// Returns <see cref="Task.CompletedTask"/>. Useful for passing it as a dummy Task.
        /// </summary>
        public static Task CompletedTask(CancellationToken cancellationToken)
            => Task.CompletedTask;

        /// <summary>
        /// Returns <see cref="Task.CompletedTask"/>. Useful for passing it as a dummy Task.
        /// </summary>
        public static Task CompletedTask<T1>(T1 arg1, CancellationToken cancellationToken)
            => Task.CompletedTask;

        /// <summary>
        /// Returns <see cref="Task.CompletedTask"/>. Useful for passing it as a dummy Task.
        /// </summary>
        public static Task CompletedTask<T1, T2>(T1 arg1, T2 arg2, CancellationToken cancellationToken)
            => Task.CompletedTask;

        /// <summary>
        /// Returns <see cref="Task.CompletedTask"/>. Useful for passing it as a dummy Task.
        /// </summary>
        public static Task CompletedTask<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3, CancellationToken cancellationToken)
            => Task.CompletedTask;

        /// <summary>
        /// Utility for converting a callback into a task.
        /// </summary>
        public static Task<TReturn> FromCallbackToTask<TArg1, TReturn>(
            Action<TArg1, Action<TReturn>> actionDelegate,
            TArg1 arg1,
            CancellationToken cancellationToken
            )
        {
            TaskCompletionSource<TReturn> taskCompletionSource = new();

            taskCompletionSource.LinkCancellationToken(cancellationToken);

            void Complete(TReturn result)
            {
                taskCompletionSource.TrySetResult(result);
            }

            actionDelegate.Invoke(arg1, Complete);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Utility for converting a callback into a task.
        /// </summary>
        public static Task<TReturn> FromCallbackToTask<TArg1, TArg2, TReturn>(
            Action<TArg1, TArg2, Action<TReturn>> actionDelegate,
            TArg1 arg1,
            TArg2 arg2,
            CancellationToken cancellationToken)
        {
            TaskCompletionSource<TReturn> taskCompletionSource = new();

            taskCompletionSource.LinkCancellationToken(cancellationToken);

            void Complete(TReturn result)
            {
                taskCompletionSource.TrySetResult(result);
            }

            actionDelegate.Invoke(arg1, arg2, Complete);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Generates a tuple from the requested parameters.
        /// </summary>
        public static (T1, T2) ParamsToTuple<T1, T2>(T1 p1, T2 p2) => (p1, p2);

        /// <summary>
        /// Generates a tuple from the requested parameters.
        /// </summary>
        public static (T1, T2, T3) ParamsToTuple<T1, T2, T3>(T1 p1, T2 p2, T3 p3) => (p1, p2, p3);

        /// <summary>
        /// Function that takes one extra generic parameter, and then <see cref="Func{TParam1, TReturn}.Invoke"/>s normally.
        /// </summary>
        public static Func<TExtra, TParam1, TReturn> PrependParameterInvoke<TExtra, TParam1, TReturn>(this Func<TParam1, TReturn> func)
            => (_, param1) => func.Invoke(param1);
    }
}
