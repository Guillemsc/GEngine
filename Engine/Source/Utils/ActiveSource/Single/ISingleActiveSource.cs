using System;

namespace GEngine.Utils.ActiveSource
{
    /// <summary>
    /// Simplest type of <see cref="IActiveSource"/>. Just a single enabled/disabled state.
    /// </summary>
    public interface ISingleActiveSource : IActiveSource
    {
        /// <summary>
        /// Called every time the active state of the active source changes.
        /// </summary>
        event Action<bool> OnActiveChanged;

        /// <returns>True if there is no owner deactivating it.</returns>
        bool IsActive();

        /// <summary>
        /// Sets the active state regarding some owner.
        /// If the state has been already set by the same owner, does nothing.
        /// </summary>
        /// <param name="owner">Reference to any object that acts as owner.</param>
        /// <param name="active">Active state to be set.</param>
        void SetActive(object owner, bool active);
    }
}
