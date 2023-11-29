using GEngine.Editor.HierarchyEditor.Data;
using GEngine.Modules.Components.Objects;
using GEngine.Modules.Entities.Objects;
using GEngine.Modules.Entities.UseCases;
using ImGuiNET;

namespace GEngine.Editor.HierarchyEditor.UseCases;

public sealed class RenderHierarchyEditorUseCase
{
    readonly HierarchyEditorData _hierarchyEditorData;
    readonly GetRootSceneActiveEntitiesUseCase _getRootSceneActiveEntitiesUseCase;

    public RenderHierarchyEditorUseCase(
        HierarchyEditorData hierarchyEditorData,
        GetRootSceneActiveEntitiesUseCase getRootSceneActiveEntitiesUseCase
    )
    {
        _hierarchyEditorData = hierarchyEditorData;
        _getRootSceneActiveEntitiesUseCase = getRootSceneActiveEntitiesUseCase;
    }

    public void Execute()
    {
        if (!_hierarchyEditorData.Active)
        {
            return;
        }
        
        if (ImGui.Begin("Hierarchy", ref _hierarchyEditorData.Active))
        {
            ImGui.Text($"World:");
            
            IReadOnlyList<IEntity> worldEntities = _getRootSceneActiveEntitiesUseCase.Execute<WorldEntity>();
            
            foreach (IEntity entity in worldEntities)
            {
                WorldEntity castedEntity = (WorldEntity)entity;
                
                DrawEntity(castedEntity);
            }
            
            ImGui.Separator();
            
            ImGui.Text($"Ui:");
            
            IReadOnlyList<IEntity> uiEntities = _getRootSceneActiveEntitiesUseCase.Execute<UiEntity>();
            
            foreach (IEntity entity in uiEntities)
            {
                UiEntity castedEntity = (UiEntity)entity;
                
                DrawEntity(castedEntity);
            }

            ImGui.End();
        }
    }

    void DrawEntity<T>(BaseEntity<T> entity) where T : Component
    {
        ImGuiTreeNodeFlags flags = entity.Children.Count == 0 ? ImGuiTreeNodeFlags.Leaf : ImGuiTreeNodeFlags.None;
        
        if (ImGui.TreeNodeEx(entity.Name, flags))
        {
            foreach (BaseEntity<T> childEntity in entity.Children)
            {
                DrawEntity(childEntity);
            }
            
            ImGui.TreePop();
        }
    }
}