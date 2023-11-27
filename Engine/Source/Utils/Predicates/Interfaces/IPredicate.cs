namespace GEngine.Utils.Predicates
{
    /// <summary>
    /// Represents any type of predicate that can be satisfied or not.
    /// </summary>
    public interface IPredicate
    {
        /// <summary>
        /// Evaluates the predicate and returns a value indicating whether the specified argument satisfies the predicate.
        /// </summary>
        /// <returns><c>true</c> if the specified argument satisfies the predicate; otherwise, <c>false</c>.</returns>
        bool IsSatisfied();
    }

    /// <summary>
    /// Represents any type of predicate that can be satisfied or not.
    /// </summary>
    /// <typeparam name="T">The type of the object to be passed as argument to check if it is satisfied.</typeparam>
    public interface IPredicate<in T>
    {
        /// <summary>
        /// Evaluates the predicate and returns a value indicating whether the specified argument satisfies the predicate.
        /// </summary>
        /// <param name="arg">The argument data passed for the evaluation</param>
        /// <returns><c>true</c> if the specified argument satisfies the predicate; otherwise, <c>false</c>.</returns>
        bool IsSatisfied(T arg);
    }
}
