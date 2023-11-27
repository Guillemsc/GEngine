using System;
using System.Threading;
using System.Threading.Tasks;
using GEngine.Utils.Extensions;

namespace GEngine.Utils.Tasks.Runners
{
    /// <inheritdoc cref="IAsyncTaskRunner" />
    public sealed class AsyncTaskRunner : IAsyncTaskRunner, IDisposable
    {
        CancellationTokenSource _cancellationTokenSource = new();
        bool _hasRunAny;
        bool _isCanceledForever;

        public Task Run(Func<CancellationToken, Task> func)
        {
            if (_isCanceledForever || _cancellationTokenSource.IsCancellationRequested)
            {
                return Task.FromCanceled(_cancellationTokenSource.Token);
            }

            _hasRunAny = true;

            var task = func.Invoke(_cancellationTokenSource.Token);
            task.RunAsync();
            return task;
        }

        [Obsolete("Use Cancel Forever Instead")]
        public void Cancel()
        {
            CancelForever();
        }

        public void CancelForever()
        {
            _isCanceledForever = true;
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        public void CancelCurrent()
        {
            if (_isCanceledForever || !_hasRunAny || !_cancellationTokenSource.IsCancellationRequested)
            {
                return;
            }

            _hasRunAny = false;
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }
    }
}
