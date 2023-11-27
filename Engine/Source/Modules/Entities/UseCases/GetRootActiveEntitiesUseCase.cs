using GEngine.Modules.Entities.Data;
using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.UseCases;

public sealed class GetRootActiveEntitiesUseCase
{
    readonly SceneEntitiesData _sceneEntitiesData;

    public GetRootActiveEntitiesUseCase(SceneEntitiesData sceneEntitiesData)
    {
        _sceneEntitiesData = sceneEntitiesData;
    }

    public IReadOnlyList<Entity> Execute()
    {
        return _sceneEntitiesData.RootSceneEntities;
    }
}