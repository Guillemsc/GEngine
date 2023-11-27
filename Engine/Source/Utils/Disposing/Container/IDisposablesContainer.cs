using System;

namespace GEngine.Utils.Disposing.Container
{
    /// <summary>
    /// Container for grouping <see cref="IDisposable"/>s to be able to dispose them all together.
    /// </summary>
    public interface IDisposablesContainer : IDisposable
    {
        void Add(Action dispose);
        void Add(IDisposable disposable);
    }
}
