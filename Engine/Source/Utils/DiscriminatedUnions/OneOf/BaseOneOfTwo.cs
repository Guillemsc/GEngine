#nullable enable
using System;

namespace GEngine.Utils.DiscriminatedUnions
{
    /// <summary>
    /// Discriminated union representing two possible types instances.
    /// Only one of them can be stored at the same time.
    /// Similar to <see cref="OneOf{T1, T2}"/>, but this one can be overriden to create specific classes.
    /// </summary>
    public abstract class BaseOneOf<TFirst, TSecond> : IOneOf<TFirst, TSecond>
    {
        readonly TFirst _first;
        readonly TSecond _second;
        readonly int _index;

        public bool HasFirst => _index == 0;
        public bool HasSecond => _index == 1;

        protected BaseOneOf(TFirst first)
        {
            _first = first;
            _second = default!;
            _index = 0;
        }

        protected BaseOneOf(TSecond second)
        {
            _first = default!;
            _second = second;
            _index = 1;
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

        public TFirst UnsafeGetFirst()
        {
            if (!HasFirst)
            {
                throw new InvalidOperationException($"Tried to get first value from {nameof(BaseOneOf<TFirst, TSecond>)}, but there was no value");
            }

            return _first;
        }

        public TSecond UnsafeGetSecond()
        {
            if (!HasSecond)
            {
                throw new InvalidOperationException($"Tried to get second value from {nameof(BaseOneOf<TFirst, TSecond>)}, but there was no value");
            }

            return _second;
        }

        public static bool operator ==(BaseOneOf<TFirst, TSecond> left, BaseOneOf<TFirst, TSecond> right) => left.Equals(right);
        public static bool operator !=(BaseOneOf<TFirst, TSecond> left, BaseOneOf<TFirst, TSecond> right) => !(left == right);

        bool Equals(BaseOneOf<TFirst, TSecond> other)
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

        public sealed override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is BaseOneOf<TFirst, TSecond> o && Equals(o);
        }

        public sealed override int GetHashCode()
        {
            return this.Match(
                o => o!.GetHashCode(),
                o => o!.GetHashCode()
            );
        }

        public sealed override string ToString()
        {
            return this.Match(
                OneOfExtensions.FormatValue,
                OneOfExtensions.FormatValue
            );
        }
    }
}
