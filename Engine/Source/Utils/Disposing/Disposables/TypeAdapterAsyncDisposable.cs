using System;
using System.Threading.Tasks;

namespace GEngine.Utils.Disposing.Disposables
{
    public sealed class TypeAdapterAsyncDisposable<TSource, TDestiny> : IAsyncDisposable<TDestiny>
        where TSource : TDestiny
    {
        readonly Func<TSource, ValueTask> _onDispose;
        readonly TSource _value;

        public TDestiny Value => _value;

        public TypeAdapterAsyncDisposable(TSource value, Func<TSource, ValueTask> onDispose)
        {
            _value = value;
            _onDispose = onDispose;
        }

        public TypeAdapterAsyncDisposable(IAsyncDisposable<TSource> disposable)
        {
            _value = disposable.Value;
            _onDispose = x => disposable.DisposeAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _onDispose.Invoke(_value);
        }
    }
}
