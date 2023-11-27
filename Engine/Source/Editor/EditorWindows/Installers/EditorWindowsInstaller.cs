using GEngine.Editor.EditorWindows.Data;
using GEngine.Editor.EditorWindows.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Editor.EditorWindows.Installers;

public static class EditorWindowsInstaller
{
    public static void InstallEditorWindows(this IDiContainerBuilder builder)
    {
        builder.Bind<EditorWindowsData>().FromNew();

        builder.Bind<AddEditorWindowUseCase>()
            .FromFunction(c => new AddEditorWindowUseCase(
                c.Resolve<EditorWindowsData>()
            ));

        builder.Bind<GetEditorWindowsUseCase>()
            .FromFunction(c => new GetEditorWindowsUseCase(
                c.Resolve<EditorWindowsData>()
            ));
    }
}