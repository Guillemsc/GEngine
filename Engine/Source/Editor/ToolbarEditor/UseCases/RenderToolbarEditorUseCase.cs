using GEngine.Editor.EditorWindows.Data;
using GEngine.Editor.EditorWindows.UseCases;
using GEngine.Editor.Examples.Data;
using GEngine.Editor.Examples.UseCases;
using GEngine.Modules.Framerate.UseCases;
using GEngine.Modules.Games.UseCases;
using ImGuiNET;
using GEngine.Core.Interactors;
using GEngine.Editor.HierarchyEditor.Data;
using GEngine.Editor.Physics2dEditor.Data;

namespace GEngine.Editor.ToolbarEditor.UseCases;

public sealed class RenderToolbarEditorUseCase
{
    readonly GetEditorWindowsUseCase _getEditorWindowsUseCase;
    readonly LoadGameUseCase _loadGameUseCase;
    readonly GetExamplesUseCase _getExamplesUseCase;
    readonly GetSecondAverageFpsUseCase _getSecondAverageFpsUseCase;

    public RenderToolbarEditorUseCase(
        GetEditorWindowsUseCase getEditorWindowsUseCase,
        LoadGameUseCase loadGameUseCase, 
        GetExamplesUseCase getExamplesUseCase,
        GetSecondAverageFpsUseCase getSecondAverageFpsUseCase
    )
    {
        _getEditorWindowsUseCase = getEditorWindowsUseCase;
        _loadGameUseCase = loadGameUseCase;
        _getExamplesUseCase = getExamplesUseCase;
        _getSecondAverageFpsUseCase = getSecondAverageFpsUseCase;
    }

    public void Execute()
    {
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("Windows"))
            {
                IReadOnlyList<EditorWindow> editorWindows = _getEditorWindowsUseCase.Execute();

                foreach (EditorWindow editorWindow in editorWindows)
                {
                    bool active = editorWindow.GetActive.Invoke();
                    bool wasActive = active;

                    ImGui.MenuItem(
                        editorWindow.Name,
                        string.Empty,
                        ref active
                    );

                    if (wasActive != active)
                    {
                        editorWindow.SetActive.Invoke(active);
                    }
                }
                
                ImGui.EndMenu();
            }

            if (ImGui.BeginMenu("Examples"))
            {
                IReadOnlyList<ExampleData> examples = _getExamplesUseCase.Execute();
                
                foreach (ExampleData exampleData in examples)
                {
                    if (ImGui.MenuItem(exampleData.Name))
                    {
                        _loadGameUseCase.Execute(exampleData.GameRunner);
                    }
                }

                ImGui.EndMenu();
            }
            
            ImGui.Text($"FPS {_getSecondAverageFpsUseCase.Execute()}");
            
            ImGui.EndMainMenuBar();
        }
    }
}