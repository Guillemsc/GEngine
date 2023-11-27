#nullable enable
using System;

namespace GEngine.Utils.DiscriminatedUnions
{
    /// <summary>
    /// Discriminated union representing three possible types instances.
    /// Only one of them can be stored at the same time.
    /// Similar to <see cref="OneOf{T1, T2, T3}"/>, but this one can be overriden to create specific classes.
    /// </summary>
    public abstract class BaseOneOf<TFirst, TSecond, TThird> : IOneOf<TFirst, TSecond, TThird>
    {
        readonly TFirst _first;
        readonly TSecond _second;
        readonly TThird _third;
        readonly int _index;

        public bool HasFirst => _index == 0;
        public bool HasSecond => _index == 1;
        public bool HasThird => _index == 2;

        protected BaseOneOf(TFirst first)
        {
            _first = first;
            _second = default!;
            _third = default!;
            _index = 0;
        }

        protected BaseOneOf(TSecond second)
        {
            _first = default!;
            _second = second;
            _third = default!;
            _index = 1;
        }

        protected BaseOneOf(TThird third)
        {
            _first = default!;
            _second = default!;
            _third = third;
            _index = 2;
        }

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
                throw new InvalidOperationException($"Tried to get first value from {nameof(BaseOneOf<TFirst, TSecond, TThird>)}, but there was no value");
            }

            return _first;
        }

        public TSecond UnsafeGetSecond()
        {
            if (!HasSecond)
            {
                throw new InvalidOperationException($"Tried to get second value from {nameof(BaseOneOf<TFirst, TSecond, TThird>)}, but there was no value");
            }

            return _second;
        }

        public TThird UnsafeGetThird()
        {
            if (!HasThird)
            {
                throw new InvalidOperationException($"Tried to get third value from {nameof(BaseOneOf<TFirst, TSecond, TThird>)}, but there was no value");
            }

            return _third;
        }

        public static bool operator ==(BaseOneOf<TFirst, TSecond, TThird> left, BaseOneOf<TFirst, TSecond, TThird> right) => left.Equals(right);
        public static bool operator !=(BaseOneOf<TFirst, TSecond, TThird> left, BaseOneOf<TFirst, TSecond, TThird>right) => !(left == right);

        bool Equals(BaseOneOf<TFirst, TSecond, TThird> other)
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

        public sealed override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is BaseOneOf<TFirst, TSecond, TThird> o && Equals(o);
        }

        public sealed override int GetHashCode()
        {
            return this.Match(
                o => o!.GetHashCode(),
                o => o!.GetHashCode(),
                o => o!.GetHashCode()
            );
        }

        public sealed override string ToString()
        {
            return this.Match(
                OneOfExtensions.FormatValue,
                OneOfExtensions.FormatValue,
                OneOfExtensions.FormatValue
            );
        }
    }
}
