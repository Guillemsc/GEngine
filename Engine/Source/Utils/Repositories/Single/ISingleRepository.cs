namespace GEngine.Utils.Repositories
{
    /// <summary>
    /// Represents a repository that provides access to a single object
    /// </summary>
    /// <typeparam name="TObject">The type of the object stored in the repository.</typeparam>
    public interface ISingleRepository<TObject> : IReadOnlySingleRepository<TObject>
    {
        /// <summary>
        /// Sets the object in the repository.
        /// </summary>
        /// <param name="obj">The object to set in the repository.</param>
        void Set(TObject obj);

        /// <summary>
        /// Clears the object from the repository.
        /// </summary>
        void Clear();
    }
}
