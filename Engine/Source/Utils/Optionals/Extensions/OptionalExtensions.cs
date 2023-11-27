#nullable enable
using System;

namespace GEngine.Utils.Optionals
{
    public static class OptionalExtensions
    {
        public static T? ToNullableReference<T>(this Optional<T> optional)
            where T : class
        {
            return optional.TryGet(out var value)
                ? value
                : null;
        }

        public static T? ToNullableValue<T>(this Optional<T> optional)
            where T : struct
        {
            return optional.TryGet(out var value)
                ? value
                : null;
        }

        /// <summary>
        /// Maps the optional value of type <typeparamref name="TSource"/> to an optional value of type <typeparamref name="TDestiny"/>
        /// by invoking the provided function <paramref name="func"/>.
        /// </summary>
        /// <param name="optional">The optional value of type <typeparamref name="TSource"/> to map.</param>
        /// <param name="func">The function to apply to the optional value.</param>
        /// <returns>An optional value of type <typeparamref name="TDestiny"/>.</returns>
        public static Optional<TDestiny> Map<TSource, TDestiny>(this Optional<TSource> optional, Func<TSource, TDestiny> func)
        {
            if (!optional.TryGet(out TSource value))
            {
                return Optional<TDestiny>.None;
            }

            TDestiny mapped = func.Invoke(value);
            return Optional<TDestiny>.Some(mapped);
        }

        /// <summary>
        /// Maps the optional value of type <typeparamref name="TSource"/> to an optional value of type <typeparamref name="TDestiny"/>
        /// by invoking the provided function <paramref name="func"/> that returns an optional value of type <typeparamref name="TDestiny"/>.
        /// </summary>
        /// <param name="optional">The optional value of type <typeparamref name="TSource"/> to map.</param>
        /// <param name="func">The function to apply to the optional value.</param>
        /// <returns>An optional value of type <typeparamref name="TDestiny"/>.</returns>
        public static Optional<TDestiny> Map<TSource, TDestiny>(this Optional<TSource> optional, Func<TSource, Optional<TDestiny>> func)
        {
            if (!optional.TryGet(out TSource value))
            {
                return Optional<TDestiny>.None;
            }

            Optional<TDestiny> mapped = func.Invoke(value);
            return mapped;
        }

        /// <summary>
        /// Maps the value of the <see cref="Optional{TSource}"/> object to an <see cref="Optional{TDestiny}"/>
        /// object based on the provided functions.
        /// </summary>
        /// <typeparam name="TSource">The type of the source value contained in the <see cref="Optional{TSource}"/> object.</typeparam>
        /// <typeparam name="TDestiny">The type of the mapped value contained in the resulting <see cref="Optional{TDestiny}"/> object.</typeparam>
        /// <param name="optional">The source <see cref="Optional{TSource}"/> object.</param>
        /// <param name="useFunc">A function that determines whether the value should be used for mapping.</param>
        /// <param name="func">A function that maps the value from <typeparamref name="TSource"/> to <typeparamref name="TDestiny"/>.</param>
        /// <returns>
        /// An <see cref="Optional{TDestiny}"/> object that contains the mapped value if the source <see cref="Optional{TSource}"/>
        /// object has a value,
        /// <paramref name="useFunc"/> returns <see langword="true"/> for that value, and the mapping function <paramref name="func"/> succeeds;
        /// otherwise, an <see cref="Optional{TDestiny}"/> object with no value.
        /// </returns>
        public static Optional<TDestiny> Map<TSource, TDestiny>(
            this Optional<TSource> optional,
            Func<TSource, bool> useFunc,
            Func<TSource, TDestiny> func
        )
        {
            if (!optional.TryGet(out var value))
            {
                return Optional<TDestiny>.None;
            }

            var use = useFunc.Invoke(value);

            if (!use)
            {
                return Optional<TDestiny>.None;
            }

            var mapped = func.Invoke(value);
            return Optional<TDestiny>.Some(mapped);
        }

        /// <summary>
        /// Maps the value of the optional to a new value if it is None, using the specified function.
        /// </summary>
        /// <typeparam name="T">The type of the optional value.</typeparam>
        /// <param name="optional">The optional value.</param>
        /// <param name="func">The function to invoke if the optional value is None.</param>
        /// <returns>
        /// If the optional value is Some, returns the original optional value.
        /// If the optional value is None, invokes the specified function and returns the resulting optional value.
        /// </returns>
        public static Optional<T> MapNone<T>(this Optional<T> optional, Func<Optional<T>> func)
        {
            if (optional.HasValue)
            {
                return optional;
            }

            Optional<T> mapped = func.Invoke();
            return mapped;
        }

        /// <summary>
        /// Maps the value of the optional to a new value if it is None, using the specified optional.
        /// </summary>
        /// <typeparam name="T">The type of the optional value.</typeparam>
        /// <param name="optional">The optional value.</param>
        /// <param name="other">The Optional to return if the optional value is None.</param>
        /// <returns>
        /// If the optional value is Some, returns the original optional value.
        /// If the optional value is None, invokes the specified function and returns the resulting optional value.
        /// </returns>
        public static Optional<T> MapNone<T>(this Optional<T> optional, Optional<T> other)
        {
            if (optional.HasValue)
            {
                return optional;
            }

            return other;
        }

        /// <summary>
        /// Maps the value of the optional to a new value if it is None, using the specified value.
        /// </summary>
        /// <typeparam name="T">The type of the optional value.</typeparam>
        /// <param name="optional">The optional value.</param>
        /// <param name="value">The Optional.Some value to return if the optional value is None.</param>
        /// <returns>
        /// If the optional value is Some, returns the original optional value.
        /// If the optional value is None, invokes the specified function and returns the resulting optional value.
        /// </returns>
        public static Optional<T> MapNone<T>(this Optional<T> optional, T value)
        {
            if (optional.HasValue)
            {
                return optional;
            }

            return Optional<T>.Some(value);
        }

        /// <summary>
        /// Maps the value of the optional to a new value if it is None, using the specified function.
        /// </summary>
        /// <typeparam name="T">The type of the optional value.</typeparam>
        /// <param name="optional">The optional value.</param>
        /// <param name="func">The function to invoke if the optional value is None.</param>
        /// <returns>
        /// If the optional value is Some, returns the original optional value.
        /// If the optional value is None, invokes the specified function and returns the resulting optional value.
        /// </returns>
        public static Optional<T> MapNone<T>(this Optional<T> optional, Func<T> func)
        {
            if (optional.HasValue)
            {
                return optional;
            }

            Optional<T> mapped = func.Invoke();
            return mapped;
        }

        /// <summary>
        /// Gets the value of the optional if it is Some, or invokes the specified function to get a default value if it is None.
        /// </summary>
        /// <typeparam name="T">The type of the optional value.</typeparam>
        /// <param name="optional">The optional value.</param>
        /// <param name="defaultValueFunc">The function to invoke to get a default value if the optional value is None.</param>
        /// <returns>
        /// If the optional value is Some, returns the value of the optional.
        /// If the optional value is None, invokes the specified function and returns the resulting value.
        /// </returns>
        public static T GetValueOrDefault<T>(this Optional<T> optional, Func<T> defaultValueFunc)
        {
            if (optional.TryGet(out T value))
            {
                return value;
            }

            value = defaultValueFunc.Invoke();
            return value;
        }

        /// <summary>
        /// Gets the value of the optional if it is Some, or returns the specified default value if it is None.
        /// </summary>
        /// <typeparam name="T">The type of the optional value.</typeparam>
        /// <param name="optional">The optional value.</param>
        /// <param name="defaultValue">The default value to return if the optional value is None.</param>
        /// <returns>
        /// If the optional value is Some, returns the value of the optional.
        /// If the optional value is None, returns the specified default value.
        /// </returns>
        public static T GetValueOrDefault<T>(this Optional<T> optional, T defaultValue)
        {
            if (optional.TryGet(out T value))
            {
                return value;
            }

            value = defaultValue;
            return value;
        }
    }
}
