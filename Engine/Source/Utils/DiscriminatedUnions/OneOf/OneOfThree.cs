#nullable enable
using System;

namespace GEngine.Utils.DiscriminatedUnions
{
    /// <summary>
    /// Discriminated union representing three possible types instances.
    /// Only one of them can be stored at the same time.
    /// </summary>
    public readonly struct OneOf<TFirst, TSecond, TThird> : IOneOf<TFirst, TSecond, TThird>
    {
        readonly TFirst _first;
        readonly TSecond _second;
        readonly TThird _third;
        readonly int _index;

        public bool HasFirst => _index == 0;
        public bool HasSecond => _index == 1;
        public bool HasThird => _index == 2;

        OneOf(TFirst first)
        {
            _first = first;
            _second = default!;
            _third = default!;
            _index = 0;
        }

        OneOf(TSecond second)
        {
            _first = default!;
            _second = second;
            _third = default!;
            _index = 1;
        }

        OneOf(TThird third)
        {
            _first = default!;
            _second = default!;
            _third = third;
            _index = 2;
        }

        public static OneOf<TFirst, TSecond, TThird> Of(TFirst value) => new(value);
        public static OneOf<TFirst, TSecond, TThird> Of(TSecond value) => new(value);
        public static OneOf<TFirst, TSecond, TThird> Of(TThird value) => new(value);

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

        public bool TryGetThird(out TThird value)
        {
            value = _third;
            return HasThird;
        }

        public TFirst UnsafeGetFirst()
        {
            if (!HasFirst)
            {
                throw new InvalidOperationException($"Tried to get first value from {nameof(OneOf<TFirst, TSecond, TThird>)}, but there was no value");
            }

            return _first;
        }

        public TSecond UnsafeGetSecond()
        {
            if (!HasSecond)
            {
                throw new InvalidOperationException($"Tried to get second value from {nameof(OneOf<TFirst, TSecond, TThird>)}, but there was no value");
            }

            return _second;
        }

        public TThird UnsafeGetThird()
        {
            if (!HasThird)
            {
                throw new InvalidOperationException($"Tried to get third value from {nameof(OneOf<TFirst, TSecond, TThird>)}, but there was no value");
            }

            return _third;
        }

        public static implicit operator OneOf<TFirst, TSecond, TThird>(TFirst value) => new(value);
        public static implicit operator OneOf<TFirst, TSecond, TThird>(TSecond value) => new(value);
        public static implicit operator OneOf<TFirst, TSecond, TThird>(TThird value) => new(value);

        public static bool operator ==(OneOf<TFirst, TSecond, TThird> left, OneOf<TFirst, TSecond, TThird> right) => left.Equals(right);
        public static bool operator !=(OneOf<TFirst, TSecond, TThird> left, OneOf<TFirst, TSecond, TThird>right) => !(left == right);

        bool Equals(OneOf<TFirst, TSecond, TThird> other)
        {
            if (other._index != _index)
            {
                return false;
            }

            return this.Match(
                o => o!.Equals(other._first),
                o => o!.Equals(other._second),
                o => o!.Equals(other._third)
            );
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is OneOf<TFirst, TSecond, TThird> o && Equals(o);
        }

        public override int GetHashCode()
        {
            return this.Match(
                o => o!.GetHashCode(),
                o => o!.GetHashCode(),
                o => o!.GetHashCode()
            );
        }

        public override string ToString()
        {
            return this.Match(
                OneOfExtensions.FormatValue,
                OneOfExtensions.FormatValue,
                OneOfExtensions.FormatValue
            );
        }
    }
}
