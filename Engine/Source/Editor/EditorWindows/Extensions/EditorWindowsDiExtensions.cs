using GEngine.Editor.EditorWindows.Data;
using GEngine.Editor.EditorWindows.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Editor.EditorWindows.Extensions;

public static class EditorWindowsDiExtensions
{
    public static void LinkEditorWindow<T>(
        this IDiBindingActionBuilder<T> actionBuilder,
        Func<T, EditorWindow> func
    )
    {
        actionBuilder.WhenInit((c, o) =>
                {
                    EditorWindow editorWindow = func.Invoke(o);
                    AddEditorWindowUseCase useCase = c.Resolve<AddEditorWindowUseCase>();
                    useCase.Execute(editorWindow);
                }
            )
            .NonLazy();
    }
}