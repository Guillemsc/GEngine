using GEngine.Modules.Entities.Data;
using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.UseCases;

public sealed class DestroyAllActiveEntitiesUseCase
{
    readonly SceneData _sceneData;
    readonly DestroyEntityUseCase _destroyEntityUseCase;

    public DestroyAllActiveEntitiesUseCase(
        SceneData sceneData, 
        DestroyEntityUseCase destroyEntityUseCase
        )
    {
        _sceneData = sceneData;
        _destroyEntityUseCase = destroyEntityUseCase;
    }

    public void Execute()
    {
        foreach (EntitiesSceneData entitiesSceneData in _sceneData.SceneDataByEntityType.Values)
        {
            for (int i = entitiesSceneData.RootSceneEntities.Count - 1; i >= 0; --i)
            {
                IEntity entity = entitiesSceneData.RootSceneEntities[i];
            
                _destroyEntityUseCase.Execute(entity);
            }   
        }
    }
}