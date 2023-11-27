namespace GEngine.Utils.Caching.Resettables
{
    public interface IClearableCache
    {
        /// <summary>
        /// Clears any stored cache.
        /// </summary>
        void ClearCache();
    }
}
