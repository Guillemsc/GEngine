using GEngine.Modules.Entities.Data;
using GEngine.Modules.Entities.Objects;
using GEngine.Modules.Components.Objects;

namespace GEngine.Modules.Entities.UseCases;

public sealed class TickActiveEntitiesUseCase
{
    readonly SceneData _sceneData;

    public TickActiveEntitiesUseCase(
        SceneData sceneData
        )
    {
        _sceneData = sceneData;
    }

    public void Execute()
    {
        foreach (EntitiesSceneData entitiesSceneData in _sceneData.SceneDataByEntityType.Values)
        {
            foreach (IEntity entity in entitiesSceneData.SceneActiveEntities)
            {
                entity.Tick();
            }
        }
    }
}