using GEngine.Modules.Entities.UseCases;
using GEngine.Modules.Framerate.UseCases;
using GEngine.Modules.Physics2d.UseCases;
using GEngine.Modules.Rendering.UseCases;
using GEngine.Modules.Tickables.Data;
using GEngine.Modules.Tickables.Interactors;
using GEngine.Modules.Tickables.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Modules.Tickables.Installers;

public static class TickablesInstaller
{
    public static void InstallTickables(this IDiContainerBuilder builder)
    {
        builder.Bind<ITickablesInteractor>()
            .FromFunction(c => new TickablesInteractor(
                c.Resolve<AddTickableUseCase>(),
                c.Resolve<RemoveTickableUseCase>(),
                c.Resolve<TickUseCase>()
            ));

        builder.Bind<TickablesData>().FromNew();

        builder.Bind<TickTickablesUseCase>()
            .FromFunction(c => new TickTickablesUseCase(
                c.Resolve<TickablesData>()
            ));

        builder.Bind<AddTickableUseCase>()
            .FromFunction(c => new AddTickableUseCase(
                c.Resolve<TickablesData>()
            ));

        builder.Bind<RemoveTickableUseCase>()
            .FromFunction(c => new RemoveTickableUseCase(
                c.Resolve<TickablesData>()
            ));

        builder.Bind<RemoveAllTickablesUseCase>()
            .FromFunction(c => new RemoveAllTickablesUseCase(
                c.Resolve<TickablesData>()
            ));

        builder.Bind<TickUseCase>()
            .FromFunction(c => new TickUseCase(
                c.Resolve<TickFramerateUseCase>(),
                c.Resolve<TickEntitiesUseCase>(),
                c.Resolve<TickTickablesUseCase>(),
                c.Resolve<TickPhysics2dUseCase>(),
                c.Resolve<TickRenderingUseCase>()
            ));
    }
}