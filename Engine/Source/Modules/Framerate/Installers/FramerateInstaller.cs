using GEngine.Modules.Framerate.Data;
using GEngine.Modules.Framerate.Interactors;
using GEngine.Modules.Framerate.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Modules.Framerate.Installers;

public static class FramerateInstaller
{
    public static void InstallFramerate(this IDiContainerBuilder builder)
    {
        builder.Bind<IFramerateInteractor>()
            .FromFunction(c => new FramerateInteractor(
                c.Resolve<GetFpsUseCase>(),
                c.Resolve<GetSecondAverageFpsUseCase>(),
                c.Resolve<GetFrameTimeSecondsUseCase>()
            ));

        builder.Bind<SecondAverageFpsData>().FromNew();

        builder.Bind<GetFpsUseCase>()
            .FromFunction(c => new GetFpsUseCase(
            ));

        builder.Bind<GetSecondAverageFpsUseCase>()
            .FromFunction(c => new GetSecondAverageFpsUseCase(
                c.Resolve<SecondAverageFpsData>()
            ));

        builder.Bind<GetFrameTimeSecondsUseCase>()
            .FromFunction(c => new GetFrameTimeSecondsUseCase(
            ));

        builder.Bind<TickFramerateUseCase>()
            .FromFunction(c => new TickFramerateUseCase(
                c.Resolve<TickSecondAverageFpsUseCase>()
            ));
        
        builder.Bind<TickSecondAverageFpsUseCase>()
            .FromFunction(c => new TickSecondAverageFpsUseCase(
                c.Resolve<SecondAverageFpsData>()
            ));
    }
}