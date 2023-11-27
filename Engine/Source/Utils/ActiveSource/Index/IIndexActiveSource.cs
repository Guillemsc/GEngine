using System;

namespace GEngine.Utils.ActiveSource
{
    /// <summary>
    /// <see cref="IActiveSource"/> that controls a list of intexes, from 0 to X.
    /// Each index can be independently enabled/disabled.
    /// <see cref="T:IIdActiveSource"/> could also be used, but this is more performant for this specific case.
    /// </summary>
    public interface IIndexActiveSource : IActiveSource
    {
        /// <summary>
        /// Called every time the active state of an index changes.
        /// </summary>
        event Action<int, bool> OnActiveStateChanged;

        /// <returns>True if there is no owner deactivating this specific index.</returns>
        bool IsActive(int index);

        /// <summary>
        /// Sets the active state, regarding some owner, for a specific index.
        /// If the state has been already set by the same owner, does nothing.
        /// </summary>
        /// <param name="owner">Reference to any object that acts as owner.</param>
        /// <param name="index">The index on which the change will be applied.</param>
        /// <param name="active">Active state to be set.</param>
        void SetActive(object owner, int index, bool active);
    }
}
