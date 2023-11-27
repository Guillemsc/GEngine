using System;
using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Persistence.Serialization
{
    /// <summary>
    /// Represents serializable data that can be saved and loaded.
    /// </summary>
    public interface ISerializableData
    {
        /// <summary>
        /// Saves the serializable data. Normally this saves to disk.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to cancel the saving operation.</param>
        /// <returns>A task representing the saving operation.</returns>
        Task Save(CancellationToken cancellationToken);

        /// <summary>
        /// Loads the serializable data. Normally this loads from disk.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to cancel the loading operation.</param>
        /// <returns>A task representing the loading operation.</returns>
        Task Load(CancellationToken cancellationToken);

        /// <summary>
        /// Saves the serializable data. Normally this saves to disk.
        /// Same as <see cref="Save"/> but without task to await.
        /// </summary>
        void SaveAsync();

        /// <summary>
        /// Loads the serializable data. Normally this loads from disk.
        /// Same as <see cref="Load"/> but without task to await.
        /// </summary>
        void LoadAsync();
    }

    /// <summary>
    /// Represents serializable data of a specific type that can be saved and loaded.
    /// </summary>
    /// <typeparam name="T">The type of the serializable data.</typeparam>
    public interface ISerializableData<T> : ISerializableData where T : class
    {
        /// <summary>
        /// Event that is triggered when the data is saved.
        /// </summary>
        event Action<T> OnSave;

        /// <summary>
        /// Event that is triggered when the data is loaded.
        /// </summary>
        event Action<T, bool> OnLoad;

        /// <summary>
        /// The actual data that has been loaded and can be saved.
        /// You are always safe accessing this data, because it's automatically created internally.
        /// </summary>
        T Data { get; set; }
    }
}
