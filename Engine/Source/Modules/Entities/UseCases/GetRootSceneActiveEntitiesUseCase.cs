using GEngine.Modules.Entities.Data;
using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.UseCases;

public sealed class GetRootSceneActiveEntitiesUseCase
{
    readonly SceneData _sceneData;

    public GetRootSceneActiveEntitiesUseCase(SceneData sceneData)
    {
        _sceneData = sceneData;
    }

    public IReadOnlyList<IEntity> Execute<T>() where T : IEntity
    {
        Type type = typeof(T);

        bool hasEntities = _sceneData.SceneDataByEntityType.TryGetValue(
            type,
            out EntitiesSceneData? entitiesSceneData
        );

        if (!hasEntities)
        {
            return Array.Empty<IEntity>();
        }

        return entitiesSceneData!.RootSceneEntities;
    }
}