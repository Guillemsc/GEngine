using System.Collections.Generic;

namespace GEngine.Utils.Repositories
{
    /// <summary>
    /// Represents a read-only repository that provides access to a list of objects.
    /// </summary>
    /// <typeparam name="TObject">The type of objects stored in the repository.</typeparam>
    public interface IReadOnlyListRepository<TObject>
    {
        /// <summary>
        /// Gets the number of items in the repository.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets the read-only list of items in the repository.
        /// </summary>
        IReadOnlyList<TObject> Items { get; }

        /// <summary>
        /// Determines whether the repository contains a specific object.
        /// </summary>
        /// <param name="obj">The object to locate in the repository.</param>
        /// <returns><c>true</c> if the object is found in the repository; otherwise, <c>false</c>.</returns>
        bool Contains(TObject obj);
    }
}
