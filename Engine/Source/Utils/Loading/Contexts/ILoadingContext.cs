using System;
using System.Threading;
using System.Threading.Tasks;
using GEngine.Utils.Loading.Services;

namespace GEngine.Utils.Loading.Contexts
{
    /// <summary>
    /// Represents a loading context that allows enqueuing loading operations and managing their execution.
    /// </summary>
    public interface ILoadingContext
    {
        /// <summary>
        /// Enqueues an asynchronous task-based loading function to be executed.
        /// </summary>
        /// <param name="function">The asynchronous task-based loading function to be executed.</param>
        /// <returns>The current loading context.</returns>
        ILoadingContext Enqueue(Func<CancellationToken, Task> function);

        /// <summary>
        /// Enqueues a synchronous loading action to be executed.
        /// </summary>
        /// <param name="action">The synchronous loading action to be executed.</param>
        /// <returns>The current loading context.</returns>
        ILoadingContext Enqueue(Action action);

        /// <summary>
        /// Enqueues synchronous loading actions to be executed after the load finishes.
        /// </summary>
        /// <param name="action">The synchronous loading actions to be executed after the load finishes.</param>
        /// <returns>The current loading context.</returns>
        ILoadingContext EnqueueAfterLoad(params Action[] action);

        [Obsolete("ShowInstantly is deprecated, please use RunBeforeLoadActionsInstantly instead.")]
        ILoadingContext ShowInstantly();

        [Obsolete("DoNotHide is deprecated, please use DontRunAfterLoadActions instead.")]
        ILoadingContext DoNotHide();

        /// <summary>
        /// Runs all the before loading actions immediately before the load begins.
        /// Before loading actions are added using <see cref="ILoadingService.AddBeforeLoading"/>
        /// </summary>
        /// <returns>The current loading context.</returns>
        ILoadingContext RunBeforeLoadActionsInstantly();

        /// <summary>
        /// Specifies that the after loading actions should not be run after the load finishes.
        /// After loading actions are added using <see cref="ILoadingService.AddAfterLoading"/>
        /// </summary>
        /// <returns>The current loading context.</returns>
        ILoadingContext DontRunAfterLoadActions();

        /// <summary>
        /// Executes the loading operations asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel the loading operations.</param>
        /// <returns>A task representing the execution of the loading operations.</returns>
        Task Execute(CancellationToken cancellationToken);

        [Obsolete("Execute is deprecated, please use ExecuteAsync instead.")]
        void Execute();

        /// <summary>
        /// Executes the loading operations asynchronously.
        /// </summary>
        void ExecuteAsync();
    }
}
