using GEngine.Editor.EditorWindows.Data;
using GEngine.Editor.ImGuiDemoEditor.Data;
using GEngine.Editor.ImGuiDemoEditor.UseCases;
using GEngine.Utils.Di.Builder;
using GEngine.Editor.EditorWindows.Extensions;
using GEngine.Modules.EditorRenderer.Extensions;

namespace GEngine.Editor.ImGuiDemoEditor.Installers;

public static class ImGuiDemoEditorInstaller
{
    public static void InstallImGuiDemoEditor(this IDiContainerBuilder builder)
    {
        builder.Bind<ImGuiDemoEditorData>()
            .FromNew()
            .LinkEditorWindow(o =>
            {
                return new EditorWindow(
                    "ImGui Demo",
                    val => o.Active = val,
                    () => o.Active
                );
            });
        
        builder.Bind<RenderImGuiDemoEditorUseCase>()
            .FromFunction(c => new RenderImGuiDemoEditorUseCase(
                c.Resolve<ImGuiDemoEditorData>()
            ))
            .LinkEditorRenderer(o => o.Execute);
    }
}