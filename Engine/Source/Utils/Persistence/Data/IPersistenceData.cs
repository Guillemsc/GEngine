using GEngine.Utils.Loading.Loadables;
using GEngine.Utils.Persistence.Services;
using GEngine.Utils.Saving.Saveables;

namespace GEngine.Utils.Persistence.Data
{
    /// <summary>
    /// Represents an interface for an object that can be used by the <see cref="IPersistenceService"/>
    /// to load and save data from it.
    /// </summary>
    public interface IPersistenceData : ILoadable, ISaveable
    {

    }
}
