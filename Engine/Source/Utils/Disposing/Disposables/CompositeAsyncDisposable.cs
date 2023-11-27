using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEngine.Utils.Disposing.Disposables
{
    public sealed class CompositeAsyncDisposable : IAsyncDisposable
    {
        readonly IReadOnlyList<IAsyncDisposable> _disposables;

        public CompositeAsyncDisposable(IReadOnlyList<IAsyncDisposable> disposables)
        {
            _disposables = disposables;
        }

        public ValueTask DisposeAsync()
        {
            if (_disposables.Count == 0)
            {
                return default;
            }

            return new ValueTask(
                Task.WhenAll(_disposables.Select(async x => await x.DisposeAsync()))
            );
        }
    }
}
