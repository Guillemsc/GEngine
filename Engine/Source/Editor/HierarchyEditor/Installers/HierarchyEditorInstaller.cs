using GEngine.Editor.EditorWindows.Data;
using GEngine.Editor.HierarchyEditor.Data;
using GEngine.Editor.HierarchyEditor.UseCases;
using GEngine.Modules.Entities.UseCases;
using GEngine.Utils.Di.Builder;
using GEngine.Editor.EditorWindows.Extensions;
using GEngine.Modules.EditorRenderer.Extensions;

namespace GEngine.Editor.HierarchyEditor.Installers;

public static class HierarchyEditorInstaller
{
    public static void InstallHierarchyEditor(this IDiContainerBuilder builder)
    {
        builder.Bind<HierarchyEditorData>()
            .FromNew()
            .LinkEditorWindow(o =>
            {
                return new EditorWindow(
                    "Hierarchy",
                    val => o.Active = val,
                    () => o.Active
                );
            });
        
        builder.Bind<RenderHierarchyEditorUseCase>()
            .FromFunction(c => new RenderHierarchyEditorUseCase(
                c.Resolve<HierarchyEditorData>(),
                c.Resolve<GetRootActiveEntitiesUseCase>()
            ))
            .LinkEditorRenderer(o => o.Execute);
    }
}