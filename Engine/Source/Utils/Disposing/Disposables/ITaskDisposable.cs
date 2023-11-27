using System.Threading.Tasks;
using System;

namespace GEngine.Utils.Disposing.Disposables
{
    /// <summary>
    /// Same as <see cref="IDisposable"/> but the disposing is done asynchronously.
    /// </summary>
    public interface ITaskDisposable
    {
        Task Dispose();
    }

    /// <summary>
    /// Same as <see cref="ITaskDisposable"/> but with an enforced type.
    /// </summary>
    public interface ITaskDisposable<out T> : ITaskDisposable
    {
        public T Value { get; }
    }
}
