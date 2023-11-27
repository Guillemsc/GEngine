using GEngine.Editor.Physics2dEditor.Data;
using GEngine.Modules.Physics2d.UseCases;
using ImGuiNET;

namespace GEngine.Editor.Physics2dEditor.UseCases;

public sealed class RenderPhysics2dEditorUseCase
{
    readonly Physics2dEditorData _physics2dEditorData;
    readonly GetPhysics2dContactsCountUseCase _getPhysics2dContactsCountUseCase;

    public RenderPhysics2dEditorUseCase(
        Physics2dEditorData physics2dEditorData,
        GetPhysics2dContactsCountUseCase getPhysics2dContactsCountUseCase
    )
    {
        _physics2dEditorData = physics2dEditorData;
        _getPhysics2dContactsCountUseCase = getPhysics2dContactsCountUseCase;
    }

    public void Execute()
    {
        if (!_physics2dEditorData.Active)
        {
            return;
        }
        
        if (ImGui.Begin("Physics", ref _physics2dEditorData.Active))
        {
            int contacts = _getPhysics2dContactsCountUseCase.Execute();
            
            ImGui.Text($"Contacts: {contacts}");

            ImGui.End();
        }
    }
}