namespace GEngine.Utils.Predicates
{
    /// <summary>
    /// A <see cref="IPredicate"/> that inverts the child's satisfied result.
    /// </summary>
    /// <inheritdoc />
    public sealed class InvertPredicate : IPredicate
    {
        readonly IPredicate _predicate;

        public InvertPredicate(
            IPredicate predicate
        )
        {
            _predicate = predicate;
        }

        public bool IsSatisfied()
        {
            return !_predicate.IsSatisfied();
        }
    }

    /// <summary>
    /// A <see cref="IPredicate"/> that inverts the child's satisfied result.
    /// </summary>
    /// <inheritdoc />
    public sealed class InvertPredicate<T> : IPredicate<T>
    {
        readonly IPredicate<T> _predicate;

        public InvertPredicate(
            IPredicate<T> predicate
        )
        {
            _predicate = predicate;
        }

        public bool IsSatisfied(T arg)
        {
            return !_predicate.IsSatisfied(arg);
        }
    }
}
