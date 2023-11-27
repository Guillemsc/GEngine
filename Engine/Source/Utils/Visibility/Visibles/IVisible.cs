using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Visibility.Visibles
{
    /// <summary>
    /// Interface for managing the visibility of an object.
    /// </summary>
    public interface IVisible
    {
        /// <summary>
        /// Sets the visibility of the object.
        /// </summary>
        /// <param name="visible">Whether to set as visible on invisible.</param>
        /// <param name="instantly">Whether to set the visibility instantly.</param>
        /// <param name="cancellationToken">The cancellation token for the task.</param>
        Task SetVisible(bool visible, bool instantly, CancellationToken cancellationToken);
    }
}
