using System;

namespace GEngine.Utils.ActiveSource
{
    /// <summary>
    /// <see cref="IActiveSource"/> that controls a list of ids, where the id type is set by the parameter.
    /// Each id can be independently enabled/disabled.
    /// </summary>
    public interface IIdActiveSource<T> : IActiveSource
    {
        /// <summary>
        /// Called every time the active state of an id changes.
        /// </summary>
        event Action<T, bool> OnActiveChanged;

        /// <returns>True if there is no owner deactivating this specific id.</returns>
        bool IsActive(T id);

        /// <summary>
        /// Sets the active state, regarding some owner, for a specific id.
        /// If the state has been already set by the same owner, does nothing.
        /// </summary>
        /// <param name="owner">Reference to any object that acts as owner.</param>
        /// <param name="id">The id on which the change will be applied.</param>
        /// <param name="active">Active state to be set.</param>
        void SetActive(object owner, T id, bool active);

        /// <summary>
        /// Adds a new id to be controlled.
        /// </summary>
        void Track(T id);

        /// <summary>
        /// Removes an existing id that was being controlled.
        /// </summary>
        void Untrack(T id);
    }
}
