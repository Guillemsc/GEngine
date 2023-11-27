using GEngine.Modules.Modes.Data;
using GEngine.Modules.Modes.Interactors;
using GEngine.Modules.Modes.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Modules.Modes.Installers;

public static class ModesInstallers
{
    public static void InstallModes(this IDiContainerBuilder builder)
    {
        builder.Bind<IModesInteractor>()
            .FromFunction(c => new ModesInteractor(
                c.Resolve<SetEngineModeUseCase>()
            ));
        
        builder.Bind<ModeData>().FromNew();

        builder.Bind<SetEngineModeUseCase>()
            .FromFunction(c => new SetEngineModeUseCase(
                c.Resolve<ModeData>()
            ));
        
        builder.Bind<GetEngineModeUseCase>()
            .FromFunction(c => new GetEngineModeUseCase(
                c.Resolve<ModeData>()
            ));
    }
}