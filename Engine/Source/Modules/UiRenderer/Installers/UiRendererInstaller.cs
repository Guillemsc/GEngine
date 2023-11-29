using GEngine.Modules.Cameras.UseCases;
using GEngine.Modules.UiRenderer.Data;
using GEngine.Modules.UiRenderer.Interactors;
using GEngine.Modules.UiRenderer.UseCases;
using GEngine.Modules.Windows.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Modules.UiRenderer.Installers;

public static class UiRendererInstaller
{
    public static void InstallUiRenderer(this IDiContainerBuilder builder)
    {
        builder.Bind<IUiRendererInteractor>()
            .FromFunction(c => new UiRendererInteractor(
                c.Resolve<GetUiScaleUseCase>(),
                c.Resolve<AddToUiRenderingQueueUseCase>(),
                c.Resolve<GetUiPositionFromScreenPositionUseCase>(),
                c.Resolve<GetScreenPositionFromUiPositionUseCase>(),
                c.Resolve<GetUiPositionFromWorldPosition2dUseCase>(),
                c.Resolve<GetWorldPosition2dFromUiPositionUseCase>()
            ));
        
        builder.Bind<UiRenderingQueuesData>().FromNew();
        builder.Bind<UiReferenceResolutionData>().FromNew();

        builder.Bind<AddToUiRenderingQueueUseCase>()
            .FromFunction(c => new AddToUiRenderingQueueUseCase(
                c.Resolve<UiRenderingQueuesData>()
            ));
        
        builder.Bind<RenderUiRenderingQueueUseCase>()
            .FromFunction(c => new RenderUiRenderingQueueUseCase(
                c.Resolve<UiRenderingQueuesData>()
            ));

        builder.Bind<RenderUiPipelineUseCase>()
            .FromFunction(c => new RenderUiPipelineUseCase(
                c.Resolve<GetWindowSizeUseCase>(),
                c.Resolve<RenderUiRenderingQueueUseCase>(),
                c.Resolve<GetUiScaleUseCase>()
            ));

        builder.Bind<GetUiScaleUseCase>()
            .FromFunction(c => new GetUiScaleUseCase(
                c.Resolve<UiReferenceResolutionData>(),
                c.Resolve<GetWindowSizeUseCase>()
            ));

        builder.Bind<GetUiPositionFromScreenPositionUseCase>()
            .FromFunction(c => new GetUiPositionFromScreenPositionUseCase(
                c.Resolve<GetWindowSizeUseCase>()
            ));
        
        builder.Bind<GetScreenPositionFromUiPositionUseCase>()
            .FromFunction(c => new GetScreenPositionFromUiPositionUseCase(
                c.Resolve<GetWindowSizeUseCase>()
            ));

        builder.Bind<GetUiPositionFromWorldPosition2dUseCase>()
            .FromFunction(c => new GetUiPositionFromWorldPosition2dUseCase(
                c.Resolve<GetActiveCamera2dOrDefaultUseCase>()
            ));

        builder.Bind<GetWorldPosition2dFromUiPositionUseCase>()
            .FromFunction(c => new GetWorldPosition2dFromUiPositionUseCase(
                c.Resolve<GetActiveCamera2dOrDefaultUseCase>()
            ));
    }
}