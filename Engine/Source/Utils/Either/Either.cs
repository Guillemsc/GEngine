#nullable enable
using System;

namespace GEngine.Utils.Either
{
    /// <summary>
    /// Represents two possible types instances. Only one of them can be stored at the same time.
    /// </summary>
    [Obsolete("This method is obsolete. Use OneOf<TFirst, TSecond> instead.")]
    public readonly struct Either<TFirst, TSecond>
    {
        readonly TFirst? _first;
        readonly TSecond? _second;

        Either(TFirst first)
        {
            _first = first;
            _second = default;
        }

        Either(TSecond second)
        {
            _second = second;
            _first = default;
        }

        public static Either<TFirst, TSecond> Of(
            TFirst value) => new(value);

        public static Either<TFirst, TSecond> Of(
            TSecond value) => new(value);

        public bool HasFirst()
        {
            return _first != null;
        }

        public bool HasSecond()
        {
            return _second != null;
        }

        public bool TryGetFirst(out TFirst value)
        {
            value = _first!;
            return _first != null;
        }

        public bool TryGetSecond(out TSecond value)
        {
            value = _second!;
            return _second != null;
        }

        public TFirst UnsafeGetFirst()
        {
            return _first ?? throw new InvalidOperationException($"Tried to get first value from {nameof(Either<TFirst, TSecond>)}, but there was no value");
        }

        public TSecond UnsafeGetSecond()
        {
            return _second ?? throw new InvalidOperationException($"Tried to get second value from {nameof(Either<TFirst, TSecond>)}, but there was no value");
        }

        public static implicit operator Either<TFirst, TSecond>(TFirst value) => new(value);
        public static implicit operator Either<TFirst, TSecond>(TSecond value) => new(value);
    }
}
