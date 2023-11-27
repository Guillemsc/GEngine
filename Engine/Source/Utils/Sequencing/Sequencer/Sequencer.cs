using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GEngine.Utils.Sequencing.Instructions;
using GEngine.Utils.Tasks.CompletionSources;

namespace GEngine.Utils.Sequencing.Sequencer
{
    /// <inheritdoc />
    public sealed class Sequencer : ISequencer
    {
        readonly Queue<Func<CancellationToken, Task>> _instructionQueue = new();

        TaskCompletionSource<object> _taskCompletionSource;
        CancellationTokenSource _cancellationTokenSource;

        public bool IsRunning { get; private set; }
        public bool Enabled { get; set; } = true;

        public void Play(IInstruction instruction)
        {
            Play(instruction.Execute);
        }

        public void Play(Action action)
        {
            Play(ct =>
            {
                action.Invoke();
                return Task.CompletedTask;
            });
        }

        public void Play(Func<CancellationToken, Task> function)
        {
            if (!Enabled)
            {
                return;
            }

            _instructionQueue.Enqueue(function);

            TryRunInstructions();
        }

        public void Kill()
        {
            if (_cancellationTokenSource == null)
            {
                return;
            }

            _instructionQueue.Clear();

            _cancellationTokenSource.Cancel();
        }

        public Task WaitCompletition()
        {
            if (_taskCompletionSource == null)
            {
                return Task.CompletedTask;
            }

            return _taskCompletionSource.Task;
        }

        async void TryRunInstructions()
        {
            if (_instructionQueue.Count == 0)
            {
                return;
            }

            if (_taskCompletionSource != null)
            {
                return;
            }

            IsRunning = true;

            _taskCompletionSource = new TaskCompletionSource<object>();
            _cancellationTokenSource = new CancellationTokenSource();

            while (_instructionQueue.Count > 0)
            {
                Func<CancellationToken, Task> currentInstruction = _instructionQueue.Dequeue();

                await currentInstruction.Invoke(_cancellationTokenSource.Token);

                if (_cancellationTokenSource.IsCancellationRequested)
                {
                    break;
                }
            }

            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;

            _taskCompletionSource.SetResult(null);
            _taskCompletionSource = null;

            // We check if we can play again to avoid issues with
            // TaskCompletionSource instant instructions
            TryRunInstructions();

            IsRunning = false;
        }
    }
}
