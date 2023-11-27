using System;
using System.Threading;
using System.Threading.Tasks;
using GEngine.Utils.Sequencing.Instructions;

namespace GEngine.Utils.Sequencing.Sequencer
{
    /// <summary>
    /// Represents an interface enqueing the execution of instructions or actions.
    /// Execution is done sequentally (played one after the other), where one
    /// instruction/action does not start until the previous one has finished running.
    /// </summary>
    public interface ISequencer
    {
        /// <summary>
        /// Gets a value indicating whether the sequencer is currently running.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the sequencer is enabled.
        /// When the it's not enabled, it does not enqueue instructions nor actions.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Enqueues the specified instruction.
        /// </summary>
        /// <param name="instruction">The instruction to play.</param>
        void Play(IInstruction instruction);

        /// <summary>
        /// Enqueues the specified action.
        /// </summary>
        /// <param name="action">The action to play.</param>
        void Play(Action action);

        /// <summary>
        /// Enqueues the specified asynchronous function.
        /// </summary>
        /// <param name="function">The asynchronous function to play.</param>
        void Play(Func<CancellationToken, Task> function);

        /// <summary>
        /// Stops the sequencer and cancels any pending instructions or actions.
        /// </summary>
        void Kill();

        /// <summary>
        /// Waits for the sequencer to complete all enqueued instructions or actions.
        /// </summary>
        /// <returns>A task representing the completion of the sequencer.</returns>
        Task WaitCompletition();
    }
}
