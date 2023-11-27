using System;
using System.Threading;
using System.Threading.Tasks;
using GEngine.Utils.DiscriminatedUnions;
using GEngine.Utils.Optionals;
using GEngine.Utils.Types;

namespace GEngine.Utils.Extensions
{
    public static class TryCatchExtensions
    {
        /// <summary>
        /// Invokes an action and handles any exceptions that occur.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <returns>
        /// An <see cref="Optional{T}"/> containing an <see cref="ErrorMessage"/>
        /// if an exception occurs; otherwise, it returns <see cref="Optional{T}.None"/>.
        /// </returns>
        public static Optional<ErrorMessage> InvokeCatchError(this Action action)
        {
            try
            {
                action.Invoke();
            }
            catch(Exception exception)
            {
                return new ErrorMessage(exception.Message);
            }

            return Optional<ErrorMessage>.None;
        }

        /// <summary>
        /// Invokes a function that returns an <see cref="Optional{T}"/> and handles any exceptions that occur.
        /// </summary>
        /// <param name="action">The function to invoke.</param>
        /// <returns>
        /// An <see cref="Optional{T}"/> containing the result of the function if it succeeds; otherwise,
        /// it contains an <see cref="ErrorMessage"/> with the exception message.
        /// </returns>
        public static Optional<ErrorMessage> InvokeCatchError(this Func<Optional<ErrorMessage>> action)
        {
            try
            {
                return action.Invoke();
            }
            catch(Exception exception)
            {
                return new ErrorMessage(exception.Message);
            }
        }

        /// <summary>
        /// Invokes an asynchronous function with a cancellation token parameter and handles any exceptions that occur.
        /// </summary>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="cancellationToken">The cancellation token to pass to the function.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result is an instance of
        /// <see cref="Optional{T}"/> containing an <see cref="ErrorMessage"/> if an exception occurs;
        /// otherwise, it returns <see cref="Optional{T}.None"/>.
        /// </returns>
        public static async Task<Optional<ErrorMessage>> InvokeCatchErrorAsync(
            this Func<CancellationToken, Task> func,
            CancellationToken cancellationToken
            )
        {
            try
            {
                await func.Invoke(cancellationToken);
            }
            catch(Exception exception)
            {
                return new ErrorMessage(exception.Message);
            }

            return Optional<ErrorMessage>.None;
        }

        /// <summary>
        /// Executes the specified function inside a try catch, and returns the result wrapped in an instance of the OneOf type.
        /// If an exception occurs during execution, an ErrorMessage containing the exception message is returned.
        /// </summary>
        /// <typeparam name="TResult">The type of the expected result.</typeparam>
        /// <param name="func">The function to execute.</param>
        /// <returns>An instance of OneOf that encapsulates either the result of the function or
        /// an ErrorMessage if an exception occurs.</returns>
        public static OneOf<TResult, ErrorMessage> InvokeGetResultOrCatchError<TResult>(this Func<TResult> func)
        {
            try
            {
                return func.Invoke();
            }
            catch(Exception exception)
            {
                return new ErrorMessage(exception.Message);
            }
        }

        /// <summary>
        /// Invokes an asynchronous function with a cancellation token parameter and handles any exceptions that occur.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the function.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result is an instance of
        /// <see cref="OneOf{T0, T1}"/> containing the result of the function if it succeeds; otherwise, it contains an
        /// <see cref="ErrorMessage"/> with the exception message.
        /// </returns>
        public static async Task<OneOf<TResult, ErrorMessage>> InvokeGetResultOrCatchErrorAsync<TResult>(
            this Func<Task<TResult>> func
        )
        {
            try
            {
                return await func.Invoke();
            }
            catch(Exception exception)
            {
                return new ErrorMessage(exception.Message);
            }
        }

        /// <summary>
        /// Invokes an asynchronous function with a cancellation token parameter and handles any exceptions that occur.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the function.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="cancellationToken">The cancellation token to pass to the function.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result is an instance of
        /// <see cref="OneOf{T0, T1}"/> containing the result of the function if it succeeds; otherwise, it contains an
        /// <see cref="ErrorMessage"/> with the exception message.
        /// </returns>
        public static async Task<OneOf<TResult, ErrorMessage>> InvokeGetResultOrCatchErrorAsync<TResult>(
            this Func<CancellationToken, Task<TResult>> func,
            CancellationToken cancellationToken
        )
        {
            try
            {
                return await func.Invoke(cancellationToken);
            }
            catch(Exception exception)
            {
                return new ErrorMessage(exception.Message);
            }
        }

        /// <summary>
        /// Executes the specified function inside a try catch, and returns the result wrapped in an instance of the OneOf type.
        /// If an exception occurs during execution, an ErrorMessage containing the exception message is returned.
        /// </summary>
        /// <typeparam name="TParam1">The type of the first parameter in the func.</typeparam>
        /// <typeparam name="TResult">The type of the expected result.</typeparam>
        /// <param name="func">The function to execute.</param>
        /// <param name="param1">The first parameter of the lambda</param>
        /// <returns>An instance of OneOf that encapsulates either the result of the function or
        /// an ErrorMessage if an exception occurs.</returns>
        public static OneOf<TResult, ErrorMessage> InvokeGetResultOrCatchError<TParam1, TResult>(
                this Func<TParam1, TResult> func,
                TParam1 param1
            )
        {
            try
            {
                return func.Invoke(param1);
            }
            catch(Exception exception)
            {
                return new ErrorMessage(exception.Message);
            }
        }

        /// <summary>
        /// Invokes an asynchronous function with a single parameter and handles any exceptions that occur.
        /// </summary>
        /// <typeparam name="TParam1">The type of the first parameter of the function.</typeparam>
        /// <typeparam name="TResult">The type of the result returned by the function.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <param name="param1">The value of the first parameter.</param>
        /// <param name="cancellationToken">Cancellation token that will be passed to the funciton invoke.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result is an instance of
        /// <see cref="OneOf{T0, T1}"/> containing the result of the function if it succeeds; otherwise, it contains an
        /// <see cref="ErrorMessage"/> with the exception message.
        /// </returns>
        public static async Task<OneOf<TResult, ErrorMessage>> InvokeGetResultOrCatchErrorAsync<TParam1, TResult>(
            this Func<TParam1, CancellationToken, Task<TResult>> func,
            TParam1 param1,
            CancellationToken cancellationToken
        )
        {
            try
            {
                return await func.Invoke(param1, cancellationToken);
            }
            catch(Exception exception)
            {
                return new ErrorMessage(exception.Message);
            }
        }

        /// <summary>
        /// Executes the specified function inside a try catch, and returns the result wrapped in an instance of the OneOf type.
        /// If an exception occurs during execution. it is returned.
        /// </summary>
        /// <typeparam name="T">The type of the expected result.</typeparam>
        /// <param name="func">The function to execute.</param>
        /// <returns>An instance of OneOf that encapsulates either the result of the function or
        /// an Exception if an exception occurs.</returns>
        public static OneOf<T, Exception> InvokeGetResultOrCatchException<T>(Func<T> func)
        {
            try
            {
                return func.Invoke();
            }
            catch(Exception exception)
            {
                return exception;
            }
        }

        /// <summary>
        /// Invokes the specified asynchronous function and returns either the result or the caught exception.
        /// </summary>
        /// <typeparam name="T">The type of the return value.</typeparam>
        /// <param name="func">The asynchronous function to invoke.</param>
        /// <returns>
        /// A task that represents the asynchronous operation and contains an instance of the OneOf type.
        /// The task's result will be either the result of the function if it executes successfully,
        /// or the caught exception if an exception occurs during the execution.
        /// </returns>
        public static async Task<OneOf<T, Exception>> InvokeGetResultOrCatchExceptionAsync<T>(Func<Task<T>> func)
        {
            try
            {
                return await func.Invoke();
            }
            catch(Exception exception)
            {
                return exception;
            }
        }

        /// <summary>
        /// Executes the specified function inside a try catch, and returns the result wrapped in an instance of the OneOf type.
        /// If an exception occurs during execution, an ErrorMessage containing the exception message is returned.
        /// </summary>
        /// <typeparam name="TParam1">The type of the first parameter in the func.</typeparam>
        /// <typeparam name="TResult">The type of the expected result.</typeparam>
        /// <param name="func">The function to execute.</param>
        /// <param name="param1">The first parameter of the lambda</param>
        /// <returns>An instance of OneOf that encapsulates either the result of the function or
        /// an ErrorMessage if an exception occurs.</returns>
        public static OneOf<TResult, Exception> InvokeGetResultOrCatchException<TParam1, TResult>(
            this Func<TParam1, TResult> func,
            TParam1 param1
            )
        {
            try
            {
                return func.Invoke(param1);
            }
            catch(Exception exception)
            {
                return exception;
            }
        }

        /// <summary>
        /// Invokes the specified action and catches the exception of type TException using the Optional type.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <returns>
        /// An instance of Optional that contains either no value (indicating no exception occurred)
        /// or the exception that was caught during the invocation.
        /// </returns>
        public static Optional<TException> InvokeCatchException<TException>(this Action action)
            where TException : Exception
        {
            try
            {
                action.Invoke();
                return Optional<TException>.None;
            }
            catch(TException exception)
            {
                return exception;
            }
        }

        /// <summary>
        /// Invokes the specified action and catches the specified exception type, returning either the result or the caught exception.
        /// </summary>
        /// <typeparam name="TReturn">The type of the return value.</typeparam>
        /// <typeparam name="TException">The type of the exception to catch.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <returns>
        /// An instance of the OneOf type that represents either the result of the action if it executes successfully,
        /// or the caught exception if an exception of the specified type is thrown during the execution.
        /// </returns>
        public static OneOf<TReturn, TException> InvokeCatchException<TReturn, TException>(this Func<TReturn> action)
            where TException : Exception
        {
            try
            {
                return action.Invoke();
            }
            catch(TException exception)
            {
                return exception;
            }
        }

        /// <summary>
        /// Invokes the specified action and catches the exception of type TException using the Optional type.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <returns>
        /// An instance of Optional that contains either no value (indicating no exception occurred)
        /// or the exception that was caught during the invocation.
        /// </returns>
        public static Optional<Exception> InvokeCatchException(this Action action)
        {
            return action.InvokeCatchException<Exception>();
        }

        /// <summary>
        /// Invokes the specified action with one argument and catches the exception of type TException using the Optional type.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="arg1">The first argument to pass to the action.</param>
        /// <returns>
        /// An instance of Optional that contains either no value (indicating no exception occurred)
        /// or the exception that was caught during the invocation.
        /// </returns>
        public static Optional<TException> InvokeCatchException<TException, T1>(this Action<T1> action, T1 arg1)
            where TException : Exception
        {
            try
            {
                action.Invoke(arg1);
                return Optional<TException>.None;
            }
            catch (TException exception)
            {
                return exception;
            }
        }

        /// <summary>
        /// Invokes the specified action and catches the exception of type TException using the Optional type.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="arg1">The first argument to pass to the action.</param>
        /// <returns>
        /// An instance of Optional that contains either no value (indicating no exception occurred)
        /// or the exception that was caught during the invocation.
        /// </returns>
        public static Optional<Exception> InvokeCatchException<T1>(this Action<T1> action, T1 arg1)
        {
            return action.InvokeCatchException<Exception, T1>(arg1);
        }

        /// <summary>
        /// Invokes the specified action with two arguments and catches the exception of type TException using the Optional type.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="arg1">The first argument to pass to the action.</param>
        /// <param name="arg2">The second argument to pass to the action.</param>
        /// <returns>
        /// An instance of Optional that contains either no value (indicating no exception occurred)
        /// or the exception that was caught during the invocation.
        /// </returns>
        public static Optional<TException> InvokeCatchException<TException, T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
            where TException : Exception
        {
            try
            {
                action.Invoke(arg1, arg2);
                return Optional<TException>.None;
            }
            catch (TException exception)
            {
                return exception;
            }
        }

        /// <summary>
        /// Invokes the specified action and catches the exception of type TException using the Optional type.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="arg1">The first argument to pass to the action.</param>
        /// <param name="arg2">The second argument to pass to the action.</param>
        /// <returns>
        /// An instance of Optional that contains either no value (indicating no exception occurred)
        /// or the exception that was caught during the invocation.
        /// </returns>
        public static Optional<Exception> InvokeCatchException<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            return action.InvokeCatchException<Exception, T1, T2>(arg1, arg2);
        }

        /// <summary>
        /// Invokes the specified action with three arguments and catches the exception of type TException using the Optional type.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="arg1">The first argument to pass to the action.</param>
        /// <param name="arg2">The second argument to pass to the action.</param>
        /// <param name="arg3">The third argument to pass to the action.</param>
        /// <returns>
        /// An instance of Optional that contains either no value (indicating no exception occurred)
        /// or the exception that was caught during the invocation.
        /// </returns>
        public static Optional<TException> InvokeCatchException<TException, T1, T2, T3>(
            this Action<T1, T2, T3> action,
            T1 arg1,
            T2 arg2,
            T3 arg3
            )
            where TException : Exception
        {
            try
            {
                action.Invoke(arg1, arg2, arg3);
                return Optional<TException>.None;
            }
            catch (TException exception)
            {
                return exception;
            }
        }

        /// <summary>
        /// Invokes the specified action and catches the exception of type TException using the Optional type.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="arg1">The first argument to pass to the action.</param>
        /// <param name="arg2">The second argument to pass to the action.</param>
        /// <param name="arg3">The third argument to pass to the action.</param>
        /// <returns>
        /// An instance of Optional that contains either no value (indicating no exception occurred)
        /// or the exception that was caught during the invocation.
        /// </returns>
        public static Optional<Exception> InvokeCatchException<T1, T2, T3>(
            this Action<T1, T2, T3> action,
            T1 arg1,
            T2 arg2,
            T3 arg3
            )
        {
            return action.InvokeCatchException<Exception, T1, T2, T3>(arg1, arg2, arg3);
        }
    }
}
