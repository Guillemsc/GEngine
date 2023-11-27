using System;
using GEngine.Utils.Di.Container;

namespace GEngine.Utils.Di.BindingActions
{
    public sealed class ActionWithoutContainerDiBindingAction : IDiBindingAction
    {
        readonly Action<object> _action;

        public ActionWithoutContainerDiBindingAction(Action<object> action)
        {
            _action = action;
        }

        public void Execute(IDiResolveContainer resolver, object obj)
        {
            _action?.Invoke(obj);
        }
    }
}
