using System;

namespace GEngine.Utils.ActiveSource
{
    /// <summary>
    /// <see cref="IActiveSource"/> that controls a list of all values of an enum type.
    /// Each enum value can be independently enabled/disabled.
    /// </summary>
    public interface IEnumActiveSource<T> : IActiveSource where  T : Enum
    {
        /// <summary>
        /// Called every time the active state of an enum value changes.
        /// </summary>
        event Action<T, bool> OnActiveChanged;

        /// <returns>True if there is no owner deactivating this specific enum value.</returns>
        bool IsActive(T type);

        /// <summary>
        /// Sets the active state, regarding some owner, for a specific enum value.
        /// If the state has been already set by the same owner, does nothing.
        /// </summary>
        /// <param name="owner">Reference to any object that acts as owner.</param>
        /// <param name="type">The enum value on which the change will be applied.</param>
        /// <param name="active">Active state to be set.</param>
        void SetActive(object owner, T type, bool active);
    }
}
