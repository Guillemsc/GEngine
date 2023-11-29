using GEngine.Modules.Entities.Data;
using GEngine.Modules.Entities.Objects;
using GEngine.Modules.Components.Objects;

namespace GEngine.Modules.Entities.UseCases;

public sealed class RefreshEntityOnSceneUseCase
{
    readonly RefreshEntityOnActiveEntitiesListsUseCase _refreshEntityOnActiveEntitiesListsUseCase;
    readonly GetSceneDataForEntityTypeUseCase _getSceneDataForEntityTypeUseCase;

    public RefreshEntityOnSceneUseCase(
        RefreshEntityOnActiveEntitiesListsUseCase refreshEntityOnActiveEntitiesListsUseCase,
        GetSceneDataForEntityTypeUseCase getSceneDataForEntityTypeUseCase
        )
    {
        _refreshEntityOnActiveEntitiesListsUseCase = refreshEntityOnActiveEntitiesListsUseCase;
        _getSceneDataForEntityTypeUseCase = getSceneDataForEntityTypeUseCase;
    }
    
    public void Execute(IEntity entity)
    {
        Type type = entity.GetType();

        EntitiesSceneData entitiesSceneData = _getSceneDataForEntityTypeUseCase.Execute(type);
        
        bool alreadyOnScene = entitiesSceneData.SceneEntities.Contains(entity);

        bool needsToBeAdded = !alreadyOnScene && entity.IsOnScene;
        bool needsToBeRemoved = alreadyOnScene && !entity.IsOnScene;

        if (needsToBeAdded)
        {
            entitiesSceneData.SceneEntities.Add(entity);
        }
        
        if (needsToBeRemoved)
        {
            entitiesSceneData.SceneEntities.Remove(entity);
        }
        
        _refreshEntityOnActiveEntitiesListsUseCase.Execute(entity);
    }
}