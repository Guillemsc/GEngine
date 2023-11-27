using GEngine.Editor.HierarchyEditor.Data;
using GEngine.Modules.Entities.Objects;
using GEngine.Modules.Entities.UseCases;
using ImGuiNET;

namespace GEngine.Editor.HierarchyEditor.UseCases;

public sealed class RenderHierarchyEditorUseCase
{
    readonly HierarchyEditorData _hierarchyEditorData;
    readonly GetRootActiveEntitiesUseCase _getRootActiveEntitiesUseCase;

    public RenderHierarchyEditorUseCase(
        HierarchyEditorData hierarchyEditorData,
        GetRootActiveEntitiesUseCase getRootActiveEntitiesUseCase
    )
    {
        _hierarchyEditorData = hierarchyEditorData;
        _getRootActiveEntitiesUseCase = getRootActiveEntitiesUseCase;
    }

    public void Execute()
    {
        if (!_hierarchyEditorData.Active)
        {
            return;
        }
        
        if (ImGui.Begin("Hierarchy", ref _hierarchyEditorData.Active))
        {
            // int entitiesCount = _getActiveEntitiesUseCase.Execute().Count;
            //
            // ImGui.Text($"Entities: {entitiesCount}");
            
            IReadOnlyList<Entity> rootActiveEntities = _getRootActiveEntitiesUseCase.Execute();
            
            foreach (Entity entity in rootActiveEntities)
            {
                DrawEntity(entity);
            }

            ImGui.End();
        }
    }

    void DrawEntity(Entity entity)
    {
        ImGuiTreeNodeFlags flags = entity.Children.Count == 0 ? ImGuiTreeNodeFlags.Leaf : ImGuiTreeNodeFlags.None;
        
        if (ImGui.TreeNodeEx(entity.Name, flags))
        {
            foreach (Entity childEntity in entity.Children)
            {
                DrawEntity(childEntity);
            }
            
            ImGui.TreePop();
        }
    }
}