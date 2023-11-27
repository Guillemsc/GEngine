using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Extensions
{
    public static class CancellationTokenExtensions
    {
        /// <summary>
        /// Awaits until cancellation get's requested.
        /// </summary>
        public static Task AwaitCancellationRequested(this CancellationToken cancellationToken)
        {
            return TaskExtensions.AwaitUntil(() => cancellationToken.IsCancellationRequested);
        }
    }
}
