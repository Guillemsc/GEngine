namespace GEngine.Utils.Randomization.Generators
{
    /// <summary>
    /// Represents an generic interface for a generating random values.
    /// </summary>
    public interface IRandomGenerator
    {
        /// <summary>
        /// Generates a new random integer.
        /// </summary>
        /// <returns>A random integer.</returns>
        int NewInt();

        /// <summary>
        /// Generates a new random integer within the specified range.
        /// </summary>
        /// <param name="minInclusive">The inclusive lower bound of the range.</param>
        /// <param name="maxExclusive">The exclusive upper bound of the range.</param>
        /// <returns>A random integer within the specified range.</returns>
        int NewInt(int minInclusive, int maxExclusive);

        /// <summary>
        /// Generates a new random floating-point number.
        /// </summary>
        /// <returns>A random floating-point number.</returns>
        float NewFloat();

        /// <summary>
        /// Generates a new random floating-point number within the specified range.
        /// </summary>
        /// <param name="minInclusive">The inclusive lower bound of the range.</param>
        /// <param name="maxInclusive">The inclusive upper bound of the range.</param>
        /// <returns>A random floating-point number within the specified range.</returns>
        float NewFloat(float minInclusive, float maxInclusive);
    }
}
