using GEngine.Editor.EditorWindows.Data;
using GEngine.Editor.Physics2dEditor.Data;
using GEngine.Editor.Physics2dEditor.UseCases;
using GEngine.Modules.Physics2d.UseCases;
using GEngine.Utils.Di.Builder;
using GEngine.Editor.EditorWindows.Extensions;
using GEngine.Modules.EditorRenderer.Extensions;

namespace GEngine.Editor.Physics2dEditor.Installers;

public static class Physics2dEditorInstaller
{
    public static void InstallPhysics2dEditor(this IDiContainerBuilder builder)
    {
        builder.Bind<Physics2dEditorData>()
            .FromNew()
            .LinkEditorWindow(o =>
            {
                return new EditorWindow(
                    "Physics2d",
                    val => o.Active = val,
                    () => o.Active
                );
            });
        
        builder.Bind<RenderPhysics2dEditorUseCase>()
            .FromFunction(c => new RenderPhysics2dEditorUseCase(
                c.Resolve<Physics2dEditorData>(),
                c.Resolve<GetPhysics2dContactsCountUseCase>()
                ))
            .LinkEditorRenderer(o => o.Execute);
    }
}