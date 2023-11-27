using System.Collections.Generic;

namespace GEngine.Utils.Extensions
{
    public static class EnumeratorExtensions
    {
        /// <summary>
        /// Converts a <see cref="IEnumerator{T}"/> to a <see cref="IEnumerable{T}"/>.
        /// </summary>
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
    }
}
