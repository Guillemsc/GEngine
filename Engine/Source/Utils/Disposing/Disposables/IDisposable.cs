using System;

namespace GEngine.Utils.Disposing.Disposables
{
    /// <summary>
    /// Same as <see cref="IDisposable"/> but with an enforced type.
    /// </summary>
    /// <remarks>
    /// Useful for decoupling the disposal of an object from its implementation
    /// (an object does not need to know how it's going to be disposed).
    /// </remarks>
    public interface IDisposable<out T> : IDisposable
    {
        public T Value { get; }
    }
}
