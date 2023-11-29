using GEngine.Modules.EditorRenderer.UseCases;
using GEngine.Modules.Renderer2d.UseCases;
using GEngine.Modules.Rendering.Interactor;
using GEngine.Modules.Rendering.UseCases;
using GEngine.Modules.UiRenderer.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Modules.Rendering.Installers;

public static class RenderingInstaller
{
    public static void InstallRendering(this IDiContainerBuilder builder)
    {
        builder.Bind<IRenderingInteractor>()
            .FromFunction(c => new RenderingInteractor(
                c.Resolve<AddToRendering2dQueueUseCase>()
            ));
        
        builder.Bind<InitRenderingUseCase>()
            .FromFunction(c => new InitRenderingUseCase(
            ));

        builder.Bind<TickRenderingUseCase>()
            .FromFunction(c => new TickRenderingUseCase(
                c.Resolve<RenderUseCase>()
            ));
        
        builder.Bind<RenderUseCase>()
            .FromFunction(c => new RenderUseCase(
                c.Resolve<Render2dPipelineUseCase>(),
                c.Resolve<RenderUiPipelineUseCase>(),
                c.Resolve<RenderEditorUseCase>()
            ));
    }
}