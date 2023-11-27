using GEngine.Editor.EditorWindows.Data;
using GEngine.Editor.WindowSettingsEditor.Data;
using GEngine.Editor.WindowSettingsEditor.UseCases;
using GEngine.Modules.Windows.UseCases;
using GEngine.Utils.Di.Builder;
using GEngine.Editor.EditorWindows.Extensions;
using GEngine.Modules.EditorRenderer.Extensions;

namespace GEngine.Editor.WindowSettingsEditor.Installers;

public static class WindowSettingsEditorInstaller
{
    public static void InstallWindowSettingsEditor(this IDiContainerBuilder builder)
    {
        builder.Bind<WindowSettingsEditorData>()
            .FromNew()
            .LinkEditorWindow(o =>
            {
                return new EditorWindow(
                    "Window Settings",
                    val => o.Active = val,
                    () => o.Active
                );
            });
        
        builder.Bind<RenderWindowSettingsEditorUseCase>()
            .FromFunction(c => new RenderWindowSettingsEditorUseCase(
                c.Resolve<WindowSettingsEditorData>(),
                c.Resolve<GetWindowSizeUseCase>(),
                c.Resolve<GetMSAAEnabledUseCase>(),
                c.Resolve<SetVSyncEnabledUseCase>(),
                c.Resolve<GetVSyncEnabledUseCase>(),
                c.Resolve<SetWindowFullscreenEnabledUseCase>(),
                c.Resolve<GetWindowFullscreenEnabledUseCase>(),
                c.Resolve<SetWindowResizableEnabledUseCase>(),
                c.Resolve<GetWindowResizableEnabledUseCase>()
            ))
            .LinkEditorRenderer(o => o.Execute);
    }
}