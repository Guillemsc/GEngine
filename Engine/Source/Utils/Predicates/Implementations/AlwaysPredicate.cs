namespace GEngine.Utils.Predicates
{
    /// <summary>
    /// A <see cref="IPredicate"/> that is always satisfied.
    /// </summary>
    /// <inheritdoc />
    public sealed class AlwaysPredicate : IPredicate
    {
        public static readonly AlwaysPredicate Instance = new();

        AlwaysPredicate()
        {

        }

        public bool IsSatisfied() => true;
    }

    /// <summary>
    /// A <see cref="IPredicate"/> that is always satisfied.
    /// </summary>
    /// <inheritdoc />
    public sealed class AlwaysPredicate<T> : IPredicate<T>
    {
        public static readonly AlwaysPredicate<T> Instance = new();

        AlwaysPredicate()
        {

        }

        public bool IsSatisfied(T arg) => true;
    }
}
