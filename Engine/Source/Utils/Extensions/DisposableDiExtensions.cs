using System;
using GEngine.Utils.Di.Builder;

namespace GEngine.Utils.Extensions
{
    public static class DisposableDiExtensions
    {
        public static IDiBindingActionBuilder<T> LinkDisposable<T>(this IDiBindingActionBuilder<T> actionBuilder)
            where T : IDisposable
        {
            return actionBuilder.WhenDispose(o => o.Dispose);
        }
    }
}
