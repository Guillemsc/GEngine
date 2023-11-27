using GEngine.Modules.Entities.Data;
using GEngine.Modules.Entities.Objects;
using GEngine.Modules.Components.Objects;

namespace GEngine.Modules.Entities.UseCases;

public sealed class RefreshEntityOnSceneUseCase
{
    readonly SceneEntitiesData _sceneEntitiesData;
    readonly RefreshEntityOnActiveEntitiesListsUseCase _refreshEntityOnActiveEntitiesListsUseCase;

    public RefreshEntityOnSceneUseCase(
        SceneEntitiesData sceneEntitiesData, 
        RefreshEntityOnActiveEntitiesListsUseCase refreshEntityOnActiveEntitiesListsUseCase
        )
    {
        _sceneEntitiesData = sceneEntitiesData;
        _refreshEntityOnActiveEntitiesListsUseCase = refreshEntityOnActiveEntitiesListsUseCase;
    }
    
    public void Execute(Entity entity)
    {
        bool alreadyOnScene = _sceneEntitiesData.SceneEntities.Contains(entity);

        bool needsToBeAdded = !alreadyOnScene && entity.IsOnScene;
        bool needsToBeRemoved = alreadyOnScene && !entity.IsOnScene;

        if (needsToBeAdded)
        {
            _sceneEntitiesData.SceneEntities.Add(entity);
        }
        
        if (needsToBeRemoved)
        {
            _sceneEntitiesData.SceneEntities.Remove(entity);
        }
        
        _refreshEntityOnActiveEntitiesListsUseCase.Execute(entity);
    }
}