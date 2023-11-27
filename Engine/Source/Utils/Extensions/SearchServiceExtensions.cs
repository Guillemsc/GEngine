#if UNITY_EDITOR
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Search;

namespace Popcore.Core.Extensions
{
    public static class SearchServiceExtensions
    {
        /// <summary>
        /// Asynchronously performs a search for the specified text and returns a task that represents the operation.
        /// It uses <see cref="SearchService.Request(string, System.Action{SearchContext, IList{SearchItem}}, SearchFlags)"/>.
        /// </summary>
        /// <param name="searchText">Search query to be executed.</param>
        /// <param name="options">Additional search options.</param>
        /// <returns>A task that represents the asynchronous search operation and contains a list of found items when completed.</returns>
        public static Task<IList<SearchItem>> RequestAsync(string searchText, SearchFlags options = SearchFlags.None)
        {
            TaskCompletionSource<IList<SearchItem>> taskCompletionSource = new();

            void OnRequestCompleted(SearchContext context, IList<SearchItem> items)
            {
                taskCompletionSource.SetResult(items);
            }

            SearchService.Request(searchText, OnRequestCompleted, options);

            return taskCompletionSource.Task;
        }
    }
}
#endif
