namespace GEngine.Utils.ActiveSource.Extensions
{
    public static class ActivableSourceExtensions
    {
        /// <summary>
        /// The same source can control different parameters.
        /// Deactivates all parameters.
        /// </summary>
        public static void DeactivateAll(this IActiveSource activeSource, object owner)
        {
            activeSource.SetActiveAll(owner, false);
        }

        /// <summary>
        /// The same source can control different parameters.
        /// Activates all parameters.
        /// </summary>
        public static void ActivateAll(this IActiveSource activeSource, object owner)
        {
            activeSource.SetActiveAll(owner, true);
        }
    }
}
