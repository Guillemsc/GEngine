namespace GEngine.Utils.Repositories
{
    /// <summary>
    /// Represents a repository that provides access to key-value pairs of objects.
    /// </summary>
    /// <typeparam name="TId">The type of the keys in the repository.</typeparam>
    /// <typeparam name="TObject">The type of the objects stored in the repository.</typeparam>
    public interface IKeyValueRepository<TId, TObject> : IReadOnlyKeyValueRepository<TId, TObject>
    {
        /// <summary>
        /// Adds a key-value pair to the repository.
        /// </summary>
        /// <param name="id">The key to add to the repository.</param>
        /// <param name="obj">The object to add to the repository.</param>
        void Add(TId id, TObject obj);

        /// <summary>
        /// Sets the object associated with the specified key in the repository.
        /// </summary>
        /// <param name="id">The key of the object to set.</param>
        /// <param name="obj">The object to set in the repository.</param>
        void Set(TId id, TObject obj);

        /// <summary>
        /// Removes the key-value pair associated with the specified key from the repository.
        /// </summary>
        /// <param name="id">The key of the key-value pair to remove.</param>
        /// <returns><c>true</c> if the key-value pair is successfully removed; otherwise, <c>false</c>.</returns>
        bool Remove(TId id);

        /// <summary>
        /// Removes all key-value pairs from the repository.
        /// </summary>
        void Clear();
    }
}
