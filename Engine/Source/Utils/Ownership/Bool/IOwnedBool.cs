using System;

namespace GEngine.Utils.Ownership.Bool
{
    /// <summary>
    /// Used for settings a bool value that can be controlled by several owners/systems.
    /// Gets enabled as soon as an owner sets it to true. Gets disabled when all owners set it to false.
    /// </summary>
    [Obsolete("IOwnedBool is deprecated, please use ISingleActiveSource")]
    public interface IOwnedBool
    {
        event Action<bool> OnChanged;

        bool Value { get; }

        /// <summary>
        ///  Sets the bool value with an owner.
        ///  Returns true if the internal value changed.
        /// </summary>
        bool Set(bool value, object owner);
    }
}
