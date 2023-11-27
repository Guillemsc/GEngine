using GEngine.Core.UseCases;
using GEngine.Modules.Cameras.UseCases;
using GEngine.Modules.EditorRenderer.UseCases;
using GEngine.Modules.Logging.UseCases;
using GEngine.Modules.Physics2d.UseCases;
using GEngine.Modules.Rendering.UseCases;
using GEngine.Modules.Windows.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Core.Installers;

public static class CoreUseCasesInstaller
{
    public static void InstallCoreUseCases(this IDiContainerBuilder builder)
    {
        builder.Bind<InitUseCase>()
            .FromFunction(c => new InitUseCase(
                c.Resolve<GetLoggerUseCase>(),
                c.Resolve<InitWindowUseCase>(),
                c.Resolve<InitCamerasUseCase>(),
                c.Resolve<InitRenderingUseCase>(),
                c.Resolve<InitEditorRendererUseCase>(),
                c.Resolve<InitPhysics2dUseCase>()
            ))
            .WhenInit(o => o.Execute)
            .NonLazy();
        
        builder.Bind<DisposeUseCase>()
            .FromFunction(c => new DisposeUseCase(
                c.Resolve<GetLoggerUseCase>(),
                c.Resolve<DisposeEditorRendererUseCase>(),
                c.Resolve<CloseWindowUseCase>()
            ))
            .WhenDispose(o => o.Execute)
            .NonLazy();
    }
}