using System;
using System.Threading.Tasks;

namespace GEngine.Utils.Disposing.Disposables
{
    public sealed class CallbackTaskDisposable<T> : ITaskDisposable<T>
    {
        readonly Func<T, Task> _onDispose;

        public T Value { get; }

        public CallbackTaskDisposable(T value, Func<T, Task> onDispose)
        {
            Value = value;
            _onDispose = onDispose;
        }

        public Task Dispose()
        {
            return _onDispose.Invoke(Value);
        }
    }
}
