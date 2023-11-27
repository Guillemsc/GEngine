using GEngine.Utils.Di.Builder;
using GEngine.Utils.Disposing.Disposables;

namespace GEngine.Utils.Di.Installers
{
    public interface IInstaller
    {
        void Install(IDiContainerBuilder builder);
    }
}
