using System.Collections.Generic;

namespace GEngine.Utils.Repositories
{
    /// <summary>
    /// Represents a read-only repository that provides access to key-value pairs of objects.
    /// </summary>
    /// <typeparam name="TId">The type of the keys in the repository.</typeparam>
    /// <typeparam name="TObject">The type of the objects stored in the repository.</typeparam>
    public interface IReadOnlyKeyValueRepository<TId, TObject>
    {
        /// <summary>
        /// Gets an enumerable collection of key-value pairs in the repository.
        /// </summary>
        IEnumerable<KeyValuePair<TId, TObject>> Items { get; }

        /// <summary>
        /// Gets an enumerable collection of keys in the repository.
        /// </summary>
        IEnumerable<TId> Keys { get; }

        /// <summary>
        /// Gets an enumerable collection of values in the repository.
        /// </summary>
        IEnumerable<TObject> Values { get; }

        /// <summary>
        /// Gets the number of key-value pairs in the repository.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Determines whether the repository contains the specified key.
        /// </summary>
        /// <param name="id">The key to locate in the repository.</param>
        /// <returns><c>true</c> if the key is found in the repository; otherwise, <c>false</c>.</returns>
        bool Contains(TId id);

        /// <summary>
        /// Tries to retrieve the object associated with the specified key from the repository.
        /// </summary>
        /// <param name="id">The key of the object to retrieve.</param>
        /// <param name="obj">When this method returns, contains the object associated with
        /// the specified key, if it exists; otherwise, the default value.</param>
        /// <returns><c>true</c> if the object is successfully retrieved; otherwise, <c>false</c>.</returns>
        bool TryGet(TId id, out TObject obj);

        /// <summary>
        /// Gets the object associated with the specified key from the repository.
        /// </summary>
        /// <param name="id">The key of the object to retrieve.</param>
        /// <returns>The object associated with the specified key.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the key is not found in the repository.</exception>
        TObject Get(TId id);
    }
}
