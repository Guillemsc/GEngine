using System;
using GEngine.Utils.Di.Builder;

namespace GEngine.Utils.Di.Installers
{
    public sealed class CallbackInstaller : IInstaller
    {
        readonly Action<IDiContainerBuilder> _callback;

        public CallbackInstaller(
            Action<IDiContainerBuilder> callback
            )
        {
            _callback = callback;
        }

        public void Install(IDiContainerBuilder container)
        {
            _callback.Invoke(container);
        }
    }
}
