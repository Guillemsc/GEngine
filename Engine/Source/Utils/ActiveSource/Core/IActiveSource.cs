namespace GEngine.Utils.ActiveSource
{
    /// <summary>
    /// Represents some value that's controlled by different parts of the code.
    /// Owners can deactivate the source, blocking it for everyone.
    /// Source won't be active until all owners activate it.
    /// </summary>
    public interface IActiveSource
    {
        /// <summary>
        /// The same source can control different parameters.
        /// Activates or deactivates all parameters.
        /// </summary>
        /// <param name="owner">Reference to any object that acts as owner.</param>
        /// <param name="active">Active state to be set.</param>
        void SetActiveAll(object owner, bool active);
    }
}
