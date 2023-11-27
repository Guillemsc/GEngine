using GEngine.Modules.EditorRenderer.Data;
using GEngine.Modules.EditorRenderer.UseCases;
using GEngine.Modules.Modes.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Modules.EditorRenderer.Installers;

public static class EditorRendererInstaller
{
    public static void InstallEditorRenderer(this IDiContainerBuilder builder)
    {
        builder.Bind<EditorRendererFontData>().FromNew();
        builder.Bind<EditorRendererRenderListData>().FromNew();
        
        builder.Bind<InitEditorRendererUseCase>()
            .FromFunction(c => new InitEditorRendererUseCase(
                c.Resolve<EditorRendererFontData>()
            ));

        builder.Bind<RenderEditorUseCase>()
            .FromFunction(c => new RenderEditorUseCase(
                c.Resolve<EditorRendererFontData>(),
                c.Resolve<EditorRendererRenderListData>(),
                c.Resolve<GetEngineModeUseCase>()
            ));

        builder.Bind<DisposeEditorRendererUseCase>()
            .FromFunction(c => new DisposeEditorRendererUseCase(
            ));

        builder.Bind<AddEditorRendererActionUseCase>()
            .FromFunction(c => new AddEditorRendererActionUseCase(
                c.Resolve<EditorRendererRenderListData>()
            ));
    }
}