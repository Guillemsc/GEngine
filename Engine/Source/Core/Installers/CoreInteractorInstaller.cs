using GEngine.Core.Interactors;
using GEngine.Modules.Cameras.Interactors;
using GEngine.Modules.Entities.Interactors;
using GEngine.Modules.Framerate.Interactors;
using GEngine.Modules.Games.Interactors;
using GEngine.Modules.GuizmoRenderer2d.Interactors;
using GEngine.Modules.Logging.Interactors;
using GEngine.Modules.Modes.Interactors;
using GEngine.Modules.Physics2d.Interactors;
using GEngine.Modules.Rendering.Interactor;
using GEngine.Modules.Tickables.Interactors;
using GEngine.Modules.Windows.Interactors;
using GEngine.Utils.Di.Builder;

namespace GEngine.Core.Installers;

public static class CoreInteractorInstaller
{
    public static void InstallCoreInteractor(this IDiContainerBuilder builder)
    {
        builder.Bind<IREngineInteractor>()
            .FromFunction(c => new REngineInteractor(
                c.Resolve<IModesInteractor>(),
                c.Resolve<ILoggingInteractor>(),
                c.Resolve<IWindowsInteractor>(),
                c.Resolve<IFramerateInteractor>(),
                c.Resolve<ICamerasInteractor>(),
                c.Resolve<IRenderingInteractor>(),
                c.Resolve<ITickablesInteractor>(),
                c.Resolve<IEntitiesInteractor>(),
                c.Resolve<IPhysics2dInteractor>(),
                c.Resolve<IGuizmoRenderer2dInteractor>(),
                c.Resolve<IGamesInteractor>()
            ));
    }
}