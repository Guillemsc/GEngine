using GEngine.Modules.Entities.Data;
using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.UseCases;

public sealed class DestroyAllActiveEntitiesUseCase
{
    readonly SceneEntitiesData _sceneEntitiesData;
    readonly DestroyEntityUseCase _destroyEntityUseCase;

    public DestroyAllActiveEntitiesUseCase(
        SceneEntitiesData sceneEntitiesData, 
        DestroyEntityUseCase destroyEntityUseCase
        )
    {
        _sceneEntitiesData = sceneEntitiesData;
        _destroyEntityUseCase = destroyEntityUseCase;
    }

    public void Execute()
    {
        for (int i = _sceneEntitiesData.RootSceneEntities.Count - 1; i >= 0; --i)
        {
            Entity entity = _sceneEntitiesData.RootSceneEntities[i];
            
            _destroyEntityUseCase.Execute(entity);
        }
    }
}