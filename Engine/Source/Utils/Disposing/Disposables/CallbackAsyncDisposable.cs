using System;
using System.Threading.Tasks;

namespace GEngine.Utils.Disposing.Disposables
{
    public sealed class CallbackAsyncDisposable : IAsyncDisposable
    {
        readonly Func<ValueTask> _onDispose;

        public CallbackAsyncDisposable(Func<ValueTask> onDispose)
        {
            _onDispose = onDispose;
        }

        public ValueTask DisposeAsync()
        {
            return _onDispose.Invoke();
        }
    }

    public sealed class CallbackAsyncDisposable<T> : IAsyncDisposable<T>
    {
        readonly Func<T, ValueTask> _onDispose;

        public T Value { get; }

        public CallbackAsyncDisposable(T value, Func<T, ValueTask> onDispose)
        {
            Value = value;
            _onDispose = onDispose;
        }

        public ValueTask DisposeAsync()
        {
            return _onDispose.Invoke(Value);
        }
    }
}
