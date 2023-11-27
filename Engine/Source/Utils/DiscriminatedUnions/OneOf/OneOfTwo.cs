#nullable enable
using System;

namespace GEngine.Utils.DiscriminatedUnions
{
    /// <summary>
    /// Discriminated union representing two possible types instances.
    /// Only one of them can be stored at the same time.
    /// </summary>
    public readonly struct OneOf<TFirst, TSecond> : IOneOf<TFirst, TSecond>
    {
        readonly TFirst _first;
        readonly TSecond _second;
        readonly int _index;

        public bool HasFirst => _index == 0;
        public bool HasSecond => _index == 1;

        OneOf(TFirst first)
        {
            _first = first;
            _second = default!;
            _index = 0;
        }

        OneOf(TSecond second)
        {
            _first = default!;
            _second = second;
            _index = 1;
        }

        public static OneOf<TFirst, TSecond> Of(TFirst value) => new(value);
        public static OneOf<TFirst, TSecond> Of(TSecond value) => new(value);

        public bool TryGetFirst(out TFirst value)
        {
            value = _first;
            return HasFirst;
        }

        public bool TryGetSecond(out TSecond value)
        {
            value = _second;
            return HasSecond;
        }

        public TFirst UnsafeGetFirst()
        {
            if (!HasFirst)
            {
                throw new InvalidOperationException($"Tried to get first value from {nameof(OneOf<TFirst, TSecond>)}, but there was no value");
            }

            return _first;
        }

        public TSecond UnsafeGetSecond()
        {
            if (!HasSecond)
            {
                throw new InvalidOperationException($"Tried to get second value from {nameof(OneOf<TFirst, TSecond>)}, but there was no value");
            }

            return _second;
        }

        public static implicit operator OneOf<TFirst, TSecond>(TFirst value) => new(value);
        public static implicit operator OneOf<TFirst, TSecond>(TSecond value) => new(value);

        public static bool operator ==(OneOf<TFirst, TSecond> left, OneOf<TFirst, TSecond> right) => left.Equals(right);
        public static bool operator !=(OneOf<TFirst, TSecond> left, OneOf<TFirst, TSecond> right) => !(left == right);

        bool Equals(OneOf<TFirst, TSecond> other)
        {
            if (other._index != _index)
            {
                return false;
            }

            return this.Match(
                o => o!.Equals(other._first),
                o => o!.Equals(other._second)
            );
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is OneOf<TFirst, TSecond> o && Equals(o);
        }

        public override int GetHashCode()
        {
            return this.Match(
                o => o!.GetHashCode(),
                o => o!.GetHashCode()
            );
        }

        public override string ToString()
        {
            return this.Match(
                OneOfExtensions.FormatValue,
                OneOfExtensions.FormatValue
            );
        }
    }
}
