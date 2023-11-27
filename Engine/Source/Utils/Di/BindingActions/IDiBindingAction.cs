using GEngine.Utils.Di.Container;

namespace GEngine.Utils.Di.BindingActions
{
    public interface IDiBindingAction
    {
        void Execute(IDiResolveContainer resolver, object obj);
    }
}
