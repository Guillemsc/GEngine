using System;

namespace GEngine.Utils.Disposing.Disposables
{
    public interface IAsyncDisposable<out T> : IAsyncDisposable
    {
        public T Value { get; }
    }
}
