namespace GEngine.Utils.Predicates
{
    /// <summary>
    /// A <see cref="IPredicate"/> that is never satisfied.
    /// </summary>
    /// <inheritdoc />
    public sealed class NeverPredicate : IPredicate
    {
        public static readonly NeverPredicate Instance = new();

        NeverPredicate()
        {

        }

        public bool IsSatisfied() => false;
    }

    /// <summary>
    /// A <see cref="IPredicate"/> that is never satisfied.
    /// </summary>
    /// <inheritdoc />
    public sealed class NeverPredicate<T> : IPredicate<T>
    {
        public static readonly NeverPredicate<T> Instance = new();

        NeverPredicate()
        {

        }

        public bool IsSatisfied(T arg) => false;
    }
}
