using GEngine.Modules.Cameras.UseCases;
using GEngine.Modules.GuizmoRenderer2d.UseCases;
using GEngine.Modules.Modes.UseCases;
using GEngine.Modules.Renderer2d.Data;
using GEngine.Modules.Renderer2d.UseCases;
using GEngine.Modules.Rendering.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Modules.Renderer2d.Installers;

public static class Renderer2dInstaller
{
    public static void InstallRenderer2d(this IDiContainerBuilder builder)
    {
        builder.Bind<Rendering2dQueuesData>().FromNew();
        
        builder.Bind<Render2dPipelineUseCase>()
            .FromFunction(c => new Render2dPipelineUseCase(
                c.Resolve<GetEngineModeUseCase>(),
                c.Resolve<GetActiveCamera2dOrDefaultUseCase>(),
                c.Resolve<RenderRendering2dQueueUseCase>(),
                c.Resolve<RenderGuizmos2dUseCase>()
            ));
        
        builder.Bind<AddToRendering2dQueueUseCase>()
            .FromFunction(c => new AddToRendering2dQueueUseCase(
                c.Resolve<Rendering2dQueuesData>()
            ));

        builder.Bind<RenderRendering2dQueueUseCase>()
            .FromFunction(c => new RenderRendering2dQueueUseCase(
                c.Resolve<Rendering2dQueuesData>()
            ));
    }
}