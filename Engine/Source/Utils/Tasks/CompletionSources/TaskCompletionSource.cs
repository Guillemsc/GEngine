using System.Threading.Tasks;
using GEngine.Utils.Types;

namespace GEngine.Utils.Tasks.CompletionSources
{
    /// <summary>
    /// Represents the producer side of a <see cref="T:System.Threading.Tasks.Task" /> unbound to a
    /// delegate, providing access to the consumer side through the
    /// <see cref="P:System.Threading.Tasks.TaskCompletionSource.Task" /> property.
    /// </summary>
    public sealed class TaskCompletionSource : TaskCompletionSource<Nothing>
    {
        /// <summary>
        /// Sets the result of the task to completion.
        /// </summary>
        public void SetResult()
        {
            base.SetResult(Nothing.Instance);
        }

        /// <summary>
        /// Attempts to set the result of the task to completion.
        /// </summary>
        public void TrySetResult()
        {
            base.TrySetResult(Nothing.Instance);
        }
    }
}
