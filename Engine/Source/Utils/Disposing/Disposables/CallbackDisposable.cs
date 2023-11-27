using System;

namespace GEngine.Utils.Disposing.Disposables
{
    /// <summary>
    /// Disposes something calling a function.
    /// </summary>
    public sealed class CallbackDisposable : IDisposable
    {
        readonly Action _onDispose;

        bool _disposed;

        public CallbackDisposable(Action onDispose)
        {
            _onDispose = onDispose;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            _onDispose?.Invoke();
        }
    }

    /// <summary>
    /// Disposes a certain object, calling a function that's passed at constructon time.
    /// </summary>
    public sealed class CallbackDisposable<T> : IDisposable<T>
    {
        readonly Action<T> _onDispose;

        bool _disposed;

        public T Value { get; }

        public CallbackDisposable(T value, Action<T> onDispose)
        {
            Value = value;
            _onDispose = onDispose;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            _onDispose?.Invoke(Value);
        }
    }
}
