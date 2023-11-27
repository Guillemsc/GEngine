using GEngine.Utils.Persistence.Serialization;

namespace GEngine.Utils.Persistence.Migrations
{
    /// <summary>
    /// Represents a migration contract for modifying internal data when <see cref="ISerializableData{T}"/> is loaded.
    /// </summary>
    public interface IMigration<T> where T : class
    {
        /// <summary>
        /// Executed when the <see cref="ISerializableData{T}"/> data is loaded.
        /// You can use this method to change/migrate whatever internal data you need.
        /// </summary>
        /// <param name="data">The instance of the data to be migrated.</param>
        void Migrate(T data);
    }
}
