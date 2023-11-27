namespace GEngine.Utils.Repositories
{
    /// <summary>
    /// Represents a repository that provides access to a list of objects.
    /// </summary>
    /// <typeparam name="TObject">The type of objects stored in the repository.</typeparam>
    public interface IListRepository<TObject> : IReadOnlyListRepository<TObject>
    {
        /// <summary>
        /// Adds an object to the repository.
        /// </summary>
        /// <param name="obj">The object to add to the repository.</param>
        void Add(TObject obj);

        /// <summary>
        /// Inserts an object at the specified index in the repository.
        /// </summary>
        /// <param name="obj">The object to insert into the repository.</param>
        /// <param name="index">The index at which the object should be inserted.</param>
        void Insert(TObject obj, int index);

        /// <summary>
        /// Removes the first occurrence of a specific object from the repository.
        /// </summary>
        /// <param name="obj">The object to remove from the repository.</param>
        /// <returns><c>true</c> if the object is successfully removed; otherwise, <c>false</c>.</returns>
        bool Remove(TObject obj);

        /// <summary>
        /// Removes all objects from the repository.
        /// </summary>
        void Clear();
    }
}
