using GEngine.Utils.Delegates.Animation;
using GEngine.Utils.Loading.Contexts;

namespace GEngine.Utils.Loading.Services
{
    /// <summary>
    /// Represents a loading service that provides functionality for managing loading operations.
    /// </summary>
    public interface ILoadingService
    {
        /// <summary>
        /// Gets a value indicating whether there is an active loading operation.
        /// </summary>
        bool IsLoading { get; }

        /// <summary>
        /// Adds a function to be executed before the loading operation starts.
        /// Useful for showing loading screens.
        /// </summary>
        /// <param name="func">The function to be executed.</param>
        void AddBeforeLoading(TaskAnimationEvent func);

        /// <summary>
        /// Adds a function to be executed after the loading operation starts.
        /// Useful for hiding loading screens.
        /// </summary>
        void AddAfterLoading(TaskAnimationEvent func);

        /// <summary>
        /// Creates a new loading context builder for managing loading operations.
        /// Executed loading contexts will be sequenced one after the other.
        /// </summary>
        /// <returns>The created loading context.</returns>
        ILoadingContext New();
    }
}
