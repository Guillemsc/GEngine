using GEngine.Utils.Delegates.Animation;
using GEngine.Utils.Loading.Contexts;

namespace GEngine.Utils.Loading.Services
{
    public sealed class NopLoadingService : ILoadingService
    {
        public static readonly NopLoadingService Instance = new();

        public bool IsLoading => false;

        NopLoadingService()
        {

        }

        public void AddBeforeLoading(TaskAnimationEvent func) { }
        public void AddAfterLoading(TaskAnimationEvent func) { }
        public ILoadingContext New() => NopLoadingContext.Instance;
    }
}
