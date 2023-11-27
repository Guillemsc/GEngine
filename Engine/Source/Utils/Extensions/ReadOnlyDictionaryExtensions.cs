using System.Collections.Generic;

namespace GEngine.Utils.Extensions
{
    public static class ReadOnlyDictionaryExtensions
    {
        /// <summary>
        /// Merges two IReadOnlyDictionaries into a new Dictionary, with values from the second dictionary
        /// (overlayDictionary) overriding the values from the first dictionary (dictionary) if the keys are the same.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in both dictionaries.</typeparam>
        /// <typeparam name="TValue">The type of the values in both dictionaries.</typeparam>
        /// <param name="dictionary">The base IReadOnlyDictionary whose values will be combined with the overlayDictionary.</param>
        /// <param name="overlayDictionary">The IReadOnlyDictionary whose values will override the values of the
        /// base dictionary if the keys are the same.</param>
        /// <returns>A new Dictionary containing the combined key-value pairs of the input dictionaries,
        /// with values from the overlayDictionary overriding those from the base dictionary for the same keys.</returns>
        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary,
            IReadOnlyDictionary<TKey, TValue> overlayDictionary
            )
        {
            Dictionary<TKey, TValue> result = new(dictionary);

            foreach (var kvp in overlayDictionary)
            {
                result[kvp.Key] = kvp.Value;
            }

            return result;
        }
    }
}
