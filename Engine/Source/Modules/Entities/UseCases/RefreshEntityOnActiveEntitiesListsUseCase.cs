using GEngine.Modules.Entities.Data;
using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.UseCases;

public sealed class RefreshEntityOnActiveEntitiesListsUseCase
{
    readonly GetSceneDataForEntityTypeUseCase _getSceneDataForEntityTypeUseCase;

    public RefreshEntityOnActiveEntitiesListsUseCase(
        GetSceneDataForEntityTypeUseCase getSceneDataForEntityTypeUseCase
        )
    {
        _getSceneDataForEntityTypeUseCase = getSceneDataForEntityTypeUseCase;
    }

    public void Execute(IEntity entity)
    {
        Type type = entity.GetType();

        EntitiesSceneData entitiesSceneData = _getSceneDataForEntityTypeUseCase.Execute(type);
        
        bool isOnSceneActiveEntitiesList = entitiesSceneData.SceneActiveEntities.Contains(entity);
        bool isOnSceneRootEntitiesList = entitiesSceneData.RootSceneEntities.Contains(entity);

        bool shouldBeAddedToSceneActiveEntitiesList = entity is { IsActiveInHierachy: true, IsOnScene: true } && !isOnSceneActiveEntitiesList;
        
        if (shouldBeAddedToSceneActiveEntitiesList)
        {
            entitiesSceneData.SceneActiveEntities.Add(entity);
        }

        bool shouldBeAddedToSceneActiveRootEntitiesList = entity is { IsOnScene: true, HasParent: false } && !isOnSceneRootEntitiesList;

        if (shouldBeAddedToSceneActiveRootEntitiesList)
        {
            entitiesSceneData.RootSceneEntities.Add(entity);
        }
        
        bool shouldBeRemovedFromSceneActiveEntitiesList = (!entity.IsActiveInHierachy || !entity.IsOnScene) && isOnSceneActiveEntitiesList;
        
        if(shouldBeRemovedFromSceneActiveEntitiesList)
        {
            entitiesSceneData.SceneActiveEntities.Remove(entity);
        }

        bool shouldBeRemovedFromSceneRootEntitiesList = (!entity.IsOnScene || entity.HasParent) && isOnSceneRootEntitiesList;

        if (shouldBeRemovedFromSceneRootEntitiesList)
        {
            entitiesSceneData.RootSceneEntities.Remove(entity);
        }
    }
}