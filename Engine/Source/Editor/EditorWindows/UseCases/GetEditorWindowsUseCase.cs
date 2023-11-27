using GEngine.Editor.EditorWindows.Data;

namespace GEngine.Editor.EditorWindows.UseCases;

public sealed class GetEditorWindowsUseCase
{
    readonly EditorWindowsData _editorWindowsData;

    public GetEditorWindowsUseCase(EditorWindowsData editorWindowsData)
    {
        _editorWindowsData = editorWindowsData;
    }

    public IReadOnlyList<EditorWindow> Execute()
    {
        return _editorWindowsData.EditorWindows;
    }
}