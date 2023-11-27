using GEngine.Utils.Di.Builder;
using GEngine.Utils.Di.Container;

namespace GEngine.Utils.Di.Installers
{
    public sealed class DiResolveInstallerInstaller<T> : IInstaller
    {
        readonly IDiResolveContainer _diResolveContainer;

        public DiResolveInstallerInstaller(IDiResolveContainer diResolveContainer)
        {
            _diResolveContainer = diResolveContainer;
        }

        public void Install(IDiContainerBuilder builder)
        {
            builder.Bind<T>().FromFunction(() => _diResolveContainer.Resolve<T>());
        }
    }
}
