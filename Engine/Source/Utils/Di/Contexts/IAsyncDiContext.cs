using System.Collections.Generic;
using System.Threading.Tasks;
using GEngine.Utils.Di.Container;
using GEngine.Utils.Di.Delegates;
using GEngine.Utils.Di.Installers;
using GEngine.Utils.Disposing.Disposables;
using GEngine.Utils.Loadables;

namespace GEngine.Utils.Di.Contexts
{
    public interface IAsyncDiContext<TResult>
    {
        IAsyncDiContext<TResult> AddInstallerAsyncLoadable(IAsyncLoadable<IInstaller> asyncLoadable);
        IAsyncDiContext<TResult> AddInstallerLoadable(ILoadable<IInstaller> asyncLoadable);
        IAsyncDiContext<TResult> AddInstaller(IInstaller installer);
        IAsyncDiContext<TResult> AddInstallers(IReadOnlyList<IInstaller> installers);
        IAsyncDiContext<TResult> AddInstaller(InstallDelegate installer);

        Task<ITaskDisposable<TResult>> Install();

        IDiContainer? GetContainerUnsafe();
    }
}
