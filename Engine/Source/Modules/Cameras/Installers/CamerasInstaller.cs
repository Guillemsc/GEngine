using GEngine.Modules.Cameras.Data;
using GEngine.Modules.Cameras.Interactors;
using GEngine.Modules.Cameras.UseCases;
using GEngine.Utils.Di.Builder;
using GEngine.Modules.Rendering.UseCases;
using GEngine.Modules.Windows.UseCases;

namespace GEngine.Modules.Cameras.Installers;

public static class CamerasInstaller
{
    public static void InstallCameras(this IDiContainerBuilder builder)
    {
        builder.Bind<ICamerasInteractor>()
            .FromFunction(c => new CamerasInteractor(
                c.Resolve<SetActiveCamera2dUseCase>(),
                c.Resolve<HasActiveCamera2dUseCase>(),
                c.Resolve<SetActiveCamera2dIfThereIsNoneUseCase>(),
                c.Resolve<RemoveActiveCamera2dIfMatchesUseCase>()
            ));
        
        builder.Bind<ActiveCamerasData>().FromNew();
        
        builder.Bind<InitCamerasUseCase>()
            .FromFunction(c => new InitCamerasUseCase(
                c.Resolve<InitDefaultCamerasUseCase>()
            ));

        builder.Bind<InitDefaultCamerasUseCase>()
            .FromFunction(c => new InitDefaultCamerasUseCase(
                c.Resolve<ActiveCamerasData>()
            ));

        builder.Bind<GetActiveCamera2dOrDefaultUseCase>()
            .FromFunction(c => new GetActiveCamera2dOrDefaultUseCase(
                c.Resolve<ActiveCamerasData>(),
                c.Resolve<GetWindowSizeUseCase>()
            ));

        builder.Bind<SetActiveCamera2dUseCase>()
            .FromFunction(c => new SetActiveCamera2dUseCase(
                c.Resolve<ActiveCamerasData>()
            ));

        builder.Bind<HasActiveCamera2dUseCase>()
            .FromFunction(c => new HasActiveCamera2dUseCase(
                c.Resolve<ActiveCamerasData>()
            ));

        builder.Bind<SetActiveCamera2dIfThereIsNoneUseCase>()
            .FromFunction(c => new SetActiveCamera2dIfThereIsNoneUseCase(
                c.Resolve<HasActiveCamera2dUseCase>(),
                c.Resolve<SetActiveCamera2dUseCase>()
            ));

        builder.Bind<RemoveActiveCamera2dIfMatchesUseCase>()
            .FromFunction(c => new RemoveActiveCamera2dIfMatchesUseCase(
                c.Resolve<ActiveCamerasData>()
            ));
    }
}