#nullable enable
using System;

namespace GEngine.Utils.Optionals
{
    /// <summary>
    /// Struct that may or may not contain a value of type <typeparamref name="T"/>.
    /// This is prefered over using/returning null.
    /// </summary>
    public readonly struct Optional<T>
    {
        /// <summary>
        /// An empty optional value of type <typeparamref name="T"/>.
        /// </summary>
        public static readonly Optional<T> None = new();

        readonly T _value;

        public bool HasValue { get; }

        Optional(T value)
        {
            _value = value;
            HasValue = true;
        }

        /// <summary>
        /// Creates a new optional value of type <typeparamref name="T"/> with the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be wrapped in the optional.</param>
        /// <returns>
        /// A new optional value of type <typeparamref name="T"/> with the provided <paramref name="value"/>.
        /// If the provided value is null, it will return <see cref="Optional{TSource}.None"/>.
        /// </returns>
        public static Optional<T> Some(T value)
        {
            if (value == null)
            {
                return None;
            }

            return new Optional<T>(value);
        }

        /// <summary>
        /// Creates a new optional value of type <typeparamref name="T"/> with the provided <paramref name="value"/> if <paramref name="useValue"/> is true.
        /// Otherwise, returns an empty optional value of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="useValue">A boolean value indicating whether to wrap the provided <paramref name="value"/> in the optional.</param>
        /// <param name="value">The value to be wrapped in the optional.</param>
        /// <returns>
        /// A new optional value of type <typeparamref name="T"/> with the provided <paramref name="value"/> if <paramref name="useValue"/> is true. Otherwise, returns an empty optional value of type <typeparamref name="T"/>.
        /// </returns>
        public static Optional<T> Maybe(bool useValue, T value)
        {
            if (!useValue)
            {
                return None;
            }

            return Some(value);
        }

        public static Optional<T> Maybe(T? value)
        {
            if (value == null)
            {
                return None;
            }

            return Some(value);
        }

        /// <summary>
        /// Creates a new optional value of type <typeparamref name="T"/> with the provided <paramref name="value"/> if <paramref name="useValue"/> is true.
        /// Otherwise, returns an empty optional value of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="useValue">A boolean value indicating whether to wrap the provided <paramref name="value"/> in the optional.</param>
        /// <param name="valueFunc">The func where to get the value to be wrapped in the optional.</param>
        /// <returns>
        /// A new optional value of type <typeparamref name="T"/> with the provided <paramref name="value"/> if <paramref name="useValue"/> is true. Otherwise, returns an empty optional value of type <typeparamref name="T"/>.
        /// </returns>
        public static Optional<T> Maybe(bool useValue, Func<T> valueFunc)
        {
            if (!useValue)
            {
                return None;
            }

            T value = valueFunc.Invoke();
            return Some(value);
        }

        /// <summary>
        /// Attempts to get the underlying value of the optional.
        /// </summary>
        /// <param name="value">The underlying value of the optional if it exists.</param>
        /// <returns>True if the optional has a value and <paramref name="value"/> was set to that value. False otherwise.</returns>
        public bool TryGet(out T value)
        {
            if (!HasValue)
            {
                value = default;
                return false;
            }

            value = _value;
            return true;
        }

        /// <summary>
        /// Gets the underlying value of the optional.
        /// </summary>
        /// <returns>The underlying value of the optional.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the optional has no value.</exception>
        public T UnsafeGet()
        {
            if (!HasValue)
            {
                throw new InvalidOperationException($"Tried to get value from {nameof(Optional<T>)}, but there was no value");
            }

            return _value;
        }

        public static implicit operator bool(Optional<T> result) => result.HasValue;
        public static implicit operator Optional<T>(T value) => Some(value);

        public override string ToString()
        {
            return HasValue ? _value.ToString() : "Empty";
        }
    }
}
