using System.Collections.Generic;

namespace GEngine.Utils.Predicates
{
    /// <summary>
    /// A <see cref="IPredicate"/> that is satisifed when one the child predicates is satisfied.
    /// </summary>
    /// <inheritdoc />
    public sealed class OrPredicate : IPredicate
    {
        readonly IReadOnlyCollection<IPredicate> _conditions;

        public OrPredicate(
            IReadOnlyCollection<IPredicate> conditions
        )
        {
            _conditions = conditions;
        }

        public bool IsSatisfied()
        {
            foreach (IPredicate condition in _conditions)
            {
                if (condition.IsSatisfied())
                {
                    return true;
                }
            }

            return false;
        }
    }

    /// <summary>
    /// A <see cref="IPredicate"/> that is satisifed when one the child predicates is satisfied.
    /// </summary>
    /// <inheritdoc />
    public sealed class OrPredicate<T> : IPredicate<T>
    {
        readonly IReadOnlyCollection<IPredicate<T>> _conditions;

        public OrPredicate(
            IReadOnlyCollection<IPredicate<T>> conditions
        )
        {
            _conditions = conditions;
        }

        public bool IsSatisfied(T arg)
        {
            foreach (IPredicate<T> condition in _conditions)
            {
                if (condition.IsSatisfied(arg))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
