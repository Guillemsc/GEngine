using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Saving.Saveables
{
    /// <summary>
    /// Represents an interface for an object that can be saved asynchronously.
    /// </summary>
    public interface ISaveable
    {
        /// <summary>
        /// Saves the object asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Save(CancellationToken cancellationToken);
    }
}
