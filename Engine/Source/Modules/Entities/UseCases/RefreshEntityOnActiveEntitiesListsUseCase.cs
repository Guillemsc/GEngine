using GEngine.Modules.Entities.Data;
using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.UseCases;

public sealed class RefreshEntityOnActiveEntitiesListsUseCase
{
    readonly SceneEntitiesData _sceneEntitiesData;

    public RefreshEntityOnActiveEntitiesListsUseCase(SceneEntitiesData sceneEntitiesData)
    {
        _sceneEntitiesData = sceneEntitiesData;
    }

    public void Execute(Entity entity)
    {
        bool isOnSceneActiveEntitiesList = _sceneEntitiesData.SceneActiveEntities.Contains(entity);
        bool isOnSceneRootEntitiesList = _sceneEntitiesData.RootSceneEntities.Contains(entity);

        bool shouldBeAddedToSceneActiveEntitiesList = entity is { ActiveInHierachy: true, IsOnScene: true } && !isOnSceneActiveEntitiesList;
        
        if (shouldBeAddedToSceneActiveEntitiesList)
        {
            _sceneEntitiesData.SceneActiveEntities.Add(entity);
        }

        bool shouldBeAddedToSceneActiveRootEntitiesList = entity is { IsOnScene: true, Parent.HasValue: false } && !isOnSceneRootEntitiesList;

        if (shouldBeAddedToSceneActiveRootEntitiesList)
        {
            _sceneEntitiesData.RootSceneEntities.Add(entity);
        }
        
        bool shouldBeRemovedFromSceneActiveEntitiesList = (!entity.ActiveInHierachy || !entity.IsOnScene) && isOnSceneActiveEntitiesList;
        
        if(shouldBeRemovedFromSceneActiveEntitiesList)
        {
            _sceneEntitiesData.SceneActiveEntities.Remove(entity);
        }

        bool shouldBeRemovedFromSceneRootEntitiesList = (!entity.IsOnScene || entity.Parent.HasValue) && isOnSceneRootEntitiesList;

        if (shouldBeRemovedFromSceneRootEntitiesList)
        {
            _sceneEntitiesData.RootSceneEntities.Remove(entity);
        }
    }
}