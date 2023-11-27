using System;
using GEngine.Utils.Loading.Contexts;

namespace GEngine.Utils.Loading.Extensions
{
    public static class LoadingContextExtensions
    {
        public static ILoadingContext EnqueueGCCollect(this ILoadingContext loadingContext)
        {
            return loadingContext.Enqueue(GC.Collect);
        }
    }
}
