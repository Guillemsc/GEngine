using System.Collections.Generic;
using GEngine.Utils.Di.Delegates;
using GEngine.Utils.Di.Installers;
using GEngine.Utils.Disposing.Disposables;
using GEngine.Utils.Loadables;

namespace GEngine.Utils.Di.Contexts
{
    public interface IDiContext<out TResult>
    {
        IDiContext<TResult> AddSettingLoadable<T>(ILoadable<T> loadable);
        IDiContext<TResult> AddInstallerLoadable(ILoadable<IInstaller> loadable);
        IDiContext<TResult> AddInstaller(IInstaller installer);
        IDiContext<TResult> AddInstaller(InstallDelegate installer);
        IDiContext<TResult> AddInstallers(IReadOnlyList<IInstaller> installers);

        IDisposable<TResult> Install();
    }
}
