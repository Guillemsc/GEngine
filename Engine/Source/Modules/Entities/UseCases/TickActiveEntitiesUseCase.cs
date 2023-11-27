using GEngine.Modules.Entities.Data;
using GEngine.Modules.Entities.Objects;
using GEngine.Modules.Components.Objects;

namespace GEngine.Modules.Entities.UseCases;

public sealed class TickActiveEntitiesUseCase
{
    readonly SceneEntitiesData _sceneEntitiesData;

    public TickActiveEntitiesUseCase(
        SceneEntitiesData sceneEntitiesData
        )
    {
        _sceneEntitiesData = sceneEntitiesData;
    }

    public void Execute()
    {
        foreach (Entity entity in _sceneEntitiesData.SceneEntities)
        {
            entity.Tick();
        }
    }
}