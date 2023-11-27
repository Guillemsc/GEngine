using GEngine.Editor.EditorWindows.UseCases;
using GEngine.Editor.Examples.UseCases;
using GEngine.Editor.ToolbarEditor.UseCases;
using GEngine.Modules.Framerate.UseCases;
using GEngine.Modules.Games.UseCases;
using GEngine.Utils.Di.Builder;
using GEngine.Modules.EditorRenderer.Extensions;

namespace GEngine.Editor.ToolbarEditor.Installers;

public static class ToolbarEditorInstaller
{
    public static void InstallToolbarEditor(this IDiContainerBuilder builder)
    {
        builder.Bind<RenderToolbarEditorUseCase>()
            .FromFunction(c => new RenderToolbarEditorUseCase(
                c.Resolve<GetEditorWindowsUseCase>(),
                c.Resolve<LoadGameUseCase>(),
                c.Resolve<GetExamplesUseCase>(),
                c.Resolve<GetSecondAverageFpsUseCase>()
            ))
            .LinkEditorRenderer(o => o.Execute);
    }
}