namespace GEngine.Utils.Resetting.Resetables
{
    /// <summary>
    /// Represents an object that can be reset to its initial state.
    /// </summary>
    public interface IResetable
    {
        /// <summary>
        /// Resets the object to its initial state.
        /// </summary>
        void Reset();
    }
}
