using GEngine.Editor.ImGuiDemoEditor.Data;
using ImGuiNET;

namespace GEngine.Editor.ImGuiDemoEditor.UseCases;

public sealed class RenderImGuiDemoEditorUseCase
{
    readonly ImGuiDemoEditorData _imGuiDemoEditorData;

    public RenderImGuiDemoEditorUseCase(
        ImGuiDemoEditorData imGuiDemoEditorData
        )
    {
        _imGuiDemoEditorData = imGuiDemoEditorData;
    }

    public void Execute()
    {
        if (!_imGuiDemoEditorData.Active)
        {
            return;
        }
        
        ImGui.ShowDemoWindow();
    }
}