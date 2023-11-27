using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEngine.Utils.Disposing.Disposables
{
    public sealed class CompositeFuncAsyncDisposable : IAsyncDisposable
    {
        readonly IReadOnlyList<Func<ValueTask>> _disposables;

        public CompositeFuncAsyncDisposable(IReadOnlyList<Func<ValueTask>> disposables)
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
                Task.WhenAll(_disposables.Select(async x => await x.Invoke()))
            );
        }
    }
}
