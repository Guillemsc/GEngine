using GEngine.Modules.Logging.UseCases;
using GEngine.Modules.Windows.Data;
using GEngine.Modules.Windows.Interactors;
using GEngine.Modules.Windows.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Modules.Windows.Installers;

public static class WindowsInstaller
{
    public static void InstallWindows(this IDiContainerBuilder builder)
    {
        builder.Bind<IWindowsInteractor>()
            .FromFunction(c => new WindowsInteractor(
                c.Resolve<WindowEventsData>(),
                c.Resolve<IsCloseWindowRequestedUseCase>(),
                c.Resolve<GetWindowSizeUseCase>()
            ));

        builder.Bind<WindowSizeData>().FromNew();
        builder.Bind<WindowEventsData>().FromNew();
        
        builder.Bind<InitWindowUseCase>()
            .FromFunction(c => new InitWindowUseCase(
                c.Resolve<WindowSizeData>(),
                c.Resolve<GetLoggerUseCase>()    
            ));

        builder.Bind<TickWindowsUseCase>()
            .FromFunction(c => new TickWindowsUseCase(
                c.Resolve<TickWindowSizeChangeCheckUseCase>()
            ));

        builder.Bind<CloseWindowUseCase>()
            .FromFunction(c => new CloseWindowUseCase(
                c.Resolve<GetLoggerUseCase>()
            ));

        builder.Bind<IsCloseWindowRequestedUseCase>()
            .FromFunction(c => new IsCloseWindowRequestedUseCase(
            ));

        builder.Bind<TickWindowSizeChangeCheckUseCase>()
            .FromFunction(c => new TickWindowSizeChangeCheckUseCase(
                c.Resolve<WindowSizeData>(),
                c.Resolve<WindowEventsData>()
            ));

        builder.Bind<GetWindowSizeUseCase>()
            .FromFunction(c => new GetWindowSizeUseCase(
                c.Resolve<WindowSizeData>()
            ));
        
        builder.Bind<GetMSAAEnabledUseCase>()
            .FromFunction(c => new GetMSAAEnabledUseCase());

        builder.Bind<SetVSyncEnabledUseCase>()
            .FromFunction(c => new SetVSyncEnabledUseCase());

        builder.Bind<GetVSyncEnabledUseCase>()
            .FromFunction(c => new GetVSyncEnabledUseCase());

        builder.Bind<SetWindowFullscreenEnabledUseCase>()
            .FromFunction(c => new SetWindowFullscreenEnabledUseCase());

        builder.Bind<GetWindowFullscreenEnabledUseCase>()
            .FromFunction(c => new GetWindowFullscreenEnabledUseCase());

        builder.Bind<SetWindowResizableEnabledUseCase>()
            .FromFunction(c => new SetWindowResizableEnabledUseCase());
        
        builder.Bind<GetWindowResizableEnabledUseCase>()
            .FromFunction(c => new GetWindowResizableEnabledUseCase());
    }
}