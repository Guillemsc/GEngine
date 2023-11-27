using System.Collections.Generic;

namespace GEngine.Utils.Extensions
{
    public static class HashSetExtensions
    {
        /// <summary>
        /// Adds the elements of the specified collection to the HashSet.
        /// Similar to List <see cref="List{T}.AddRange"/>.
        /// </summary>
        public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> enumerable)
        {
            foreach (T value in enumerable)
            {
                hashSet.Add(value);
            }
        }
    }
}
