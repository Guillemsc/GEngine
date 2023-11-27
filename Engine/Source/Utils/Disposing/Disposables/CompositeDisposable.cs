using System;

namespace GEngine.Utils.Disposing.Disposables
{
    public sealed class CompositeDisposable<T> : IDisposable<T>
    {
        readonly IDisposable[] _disposables;

        public T Value { get; }

        public CompositeDisposable(T value, params IDisposable[] disposables)
        {
            Value = value;
            _disposables = disposables;
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}
