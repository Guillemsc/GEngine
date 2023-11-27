namespace GEngine.Utils.Dirtyables
{
    /// <summary>
    /// Represents some value T that is set to dirty when setting its value.
    /// </summary>
    public interface IDirtyable<T>
    {
        /// <summary>
        /// Has the value changed, and thus been set to dirty.
        /// </summary>
        bool IsDirty { get; }

        /// <summary>
        /// Current value.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Modifies the stored value, and sets it to dirty if requested.
        /// </summary>
        void SetValue(T value, bool setDirty = true);

        /// <summary>
        /// Marks the value as no longer dirty.
        /// </summary>
        void Clean();
    }
}
