using GEngine.Utils.Di.Container;

namespace GEngine.Utils.Di.Delegates
{
    public delegate T BindingResolverDelegate<out T>(IDiResolveContainer resolveContainer);
}
