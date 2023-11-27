using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Loading.Loadables
{
    /// <summary>
    /// Represents an interface for an object that can be loaded asynchronously.
    /// </summary>
    public interface ILoadable
    {
        /// <summary>
        /// Loads the object asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Load(CancellationToken cancellationToken);
    }
}
