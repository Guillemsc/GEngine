using System;

namespace GEngine.Utils.Repositories
{
    /// <summary>
    /// Represents a read-only repository that provides access to a single object.
    /// </summary>
    /// <typeparam name="TObject">The type of the object stored in the repository.</typeparam>
    public interface IReadOnlySingleRepository<TObject>
    {
        /// <summary>
        /// Gets a value indicating whether the repository has a value.
        /// </summary>
        bool HasValue { get; }

        /// <summary>
        /// Determines whether the repository contains the specified object.
        /// </summary>
        /// <param name="obj">The object to locate in the repository.</param>
        /// <returns><c>true</c> if the object is found in the repository; otherwise, <c>false</c>.</returns>
        bool Contains(TObject obj);

        /// <summary>
        /// Tries to retrieve the object from the repository.
        /// </summary>
        /// <param name="obj">When this method returns, contains the retrieved object, if it exists; otherwise, the default value.</param>
        /// <returns><c>true</c> if the object is successfully retrieved; otherwise, <c>false</c>.</returns>
        bool TryGet(out TObject obj);

        /// <summary>
        /// Gets the object from the repository.
        /// </summary>
        /// <returns>The retrieved object from the repository.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the repository does not have a value.</exception>
        [Obsolete("Use GetUnsafe instead")]
        TObject Get();

        /// <summary>
        /// Gets the object from the repository.
        /// </summary>
        /// <returns>The retrieved object from the repository.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the repository does not have a value.</exception>
        TObject GetUnsafe();
    }
}
