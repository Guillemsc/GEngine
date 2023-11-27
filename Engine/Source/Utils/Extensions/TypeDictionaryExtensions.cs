using GEngine.Utils.Collections;
using GEngine.Utils.Optionals;

namespace GEngine.Utils.Extensions
{
    /// <summary>
    /// Provides extension methods for the TypeDictionary class.
    /// </summary>
    public static class TypeDictionaryExtensions
    {
        /// <summary>
        /// Tries to get the value associated with the specified type key, and wraps the result in an Optional object.
        /// </summary>
        /// <typeparam name="T">The type of the value to get.</typeparam>
        /// <param name="typeDictionary">The TypeDictionary to extend.</param>
        /// <returns>An Optional object containing the value if it exists, or Optional.None if it doesn't.</returns>
        public static Optional<T> GetOptional<T>(this TypeDictionary typeDictionary)
        {
            var hasValue = typeDictionary.TryGetValue<T>(out T value);
            return Optional<T>.Maybe(hasValue, value);
        }
    }
}
