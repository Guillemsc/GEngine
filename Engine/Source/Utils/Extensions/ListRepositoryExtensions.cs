using System.Collections.Generic;
using GEngine.Utils.Repositories;

namespace GEngine.Utils.Extensions
{
    public static class ListRepositoryExtensions
    {
        /// <summary>
        /// Adds the elements of the specified collection to the ListRepository.
        /// Similar to List <see cref="List{T}.AddRange"/>.
        /// </summary>
        public static void AddRange<T>(this IListRepository<T> listRepository, IEnumerable<T> enumerable)
        {
            foreach (T element in enumerable)
            {
                listRepository.Add(element);
            }
        }
    }
}
