using GEngine.Editor.EditorWindows.Data;

namespace GEngine.Editor.EditorWindows.UseCases;

public sealed class AddEditorWindowUseCase
{
    readonly EditorWindowsData _editorWindowsData;

    public AddEditorWindowUseCase(EditorWindowsData editorWindowsData)
    {
        _editorWindowsData = editorWindowsData;
    }

    public void Execute(EditorWindow editorWindow)
    {
        _editorWindowsData.EditorWindows.Add(editorWindow);
    }
}