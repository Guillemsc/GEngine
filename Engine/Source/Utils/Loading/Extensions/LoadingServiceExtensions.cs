using GEngine.Utils.Loading.Services;
using GEngine.Utils.Visibility.Visibles;

namespace GEngine.Utils.Loading.Extensions
{
    public static class LoadingServiceExtensions
    {
        public static void AddVisibleToBeforeAndAfterLoading(this ILoadingService loadingService, IVisible visible)
        {
            loadingService.AddBeforeLoading((instantly, ct) => visible.SetVisible(true, instantly, ct));
            loadingService.AddAfterLoading((instantly, ct) => visible.SetVisible(false, instantly, ct));
        }
    }
}
