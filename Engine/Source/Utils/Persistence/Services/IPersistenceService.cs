using System.Threading;
using System.Threading.Tasks;
using GEngine.Utils.Persistence.Data;

namespace GEngine.Utils.Persistence.Services
{
    /// <summary>
    /// Represents a service that manages the registration, loading, and saving of persistence data.
    /// </summary>
    public interface IPersistenceService
    {
        /// <summary>
        /// Registers a <see cref="IPersistenceData"/>. You can only register one object of the same type.
        /// </summary>
        /// <exception cref="System.Exception">Thrown when type already added.</exception>
        void Add<T>(T persistenceData) where T : IPersistenceData;

        /// <summary>
        /// Registers a <see cref="IPersistenceData"/>. You can only register one object of the same type.
        /// </summary>
        /// <exception cref="System.Exception">Thrown when type already added.</exception>
        void Add(IPersistenceData persistenceData);

        /// <summary>
        /// Loads all registered <see cref="IPersistenceData"/>.
        /// </summary>
        Task LoadAll(CancellationToken cancellationToken);

        /// <summary>
        /// Gets a registered <see cref="IPersistenceData"/>.
        /// </summary>
        /// <exception cref="System.Exception">Thrown when type not found.</exception>
        T Get<T>() where T : IPersistenceData;

        /// <summary>
        /// Saves a <see cref="IPersistenceData"/> of the specified type.
        /// </summary>
        Task Save<T>(CancellationToken cancellationToken) where T : IPersistenceData;

        /// <summary>
        /// Saves asyncronously a <see cref="IPersistenceData"/> of the specified type.
        /// Use when you don't want to wait for the saving to finish to continue execution.
        /// </summary>
        void SaveAsync<T>() where T : IPersistenceData;
    }
}
