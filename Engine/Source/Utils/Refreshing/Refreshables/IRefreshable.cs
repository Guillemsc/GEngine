namespace GEngine.Utils.Refreshing.Refreshables
{
    /// <summary>
    /// Represents an object that can be refreshed/updated.
    /// </summary>
    public interface IRefreshable
    {
        /// <summary>
        /// Refreshes/updates the object.
        /// </summary>
        void Refresh();
    }
}
