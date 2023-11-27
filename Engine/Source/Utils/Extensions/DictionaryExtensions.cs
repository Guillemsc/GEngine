using System.Collections.Generic;

namespace GEngine.Utils.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Tries to get the value corresponding to the key.
        /// If it cannot be found, it retuns the default value for the type;
        /// </summary>
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary,
            TKey key
            )
        {
            bool valueFound = dictionary.TryGetValue(
                key,
                out TValue value
            );

            if (!valueFound)
            {
                value = default;
            }

            return value;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the Dictionary.
        /// Similar to List <see cref="List{T}.AddRange"/>.
        /// </summary>
        public static void AddRange<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            IEnumerable<KeyValuePair<TKey, TValue>> toAdd
            )
        {
            foreach (KeyValuePair<TKey, TValue> item in toAdd)
            {
                dictionary.Add(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Adds the elements of the specified collection to the Dictionary.
        /// Similar to List <see cref="List{T}.AddRange"/>.
        /// </summary>
        public static void AddRange<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            IReadOnlyDictionary<TKey, TValue> toAdd
            )
        {
            foreach (KeyValuePair<TKey, TValue> item in toAdd)
            {
                dictionary.Add(item.Key, item.Value);
            }
        }
    }
}
