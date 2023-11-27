using System;
using GEngine.Utils.Di.Builder;
using GEngine.Utils.Di.Container;
using GEngine.Utils.ActiveSource;

namespace GEngine.Utils.ActiveSource.Extensions
{
    public static class ActiveSourceDiExtensions
    {
        public static IDiBindingActionBuilder<T> LinkSingleActiveSourceStateChange<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Func<T, ISingleActiveSource> getActiveSource,
            Func<IDiResolveContainer, Action<bool>> getChangedAction
        )
        {
            ISingleActiveSource activeSource = null;
            Action<bool> changedAction = null;

            actionBuilder.WhenInit((c, o) =>
            {
                activeSource = getActiveSource.Invoke(o);
                changedAction = getChangedAction.Invoke(c);

                activeSource.OnActiveChanged += changedAction;
            });

            actionBuilder.WhenDispose(() =>
            {
                activeSource.OnActiveChanged -= changedAction;
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }
    }
}
