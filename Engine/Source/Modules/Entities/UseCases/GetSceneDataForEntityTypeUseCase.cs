using GEngine.Modules.Components.Objects;
using GEngine.Modules.Entities.Data;
using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.UseCases;

public sealed class GetSceneDataForEntityTypeUseCase
{
    readonly SceneData _sceneData;

    public GetSceneDataForEntityTypeUseCase(SceneData sceneData)
    {
        _sceneData = sceneData;
    }

    public EntitiesSceneData Execute(Type type)
    {
        bool found = _sceneData.SceneDataByEntityType.TryGetValue(type, out EntitiesSceneData? entitiesSceneData);

        if (!found)
        {
            entitiesSceneData = new();
            _sceneData.SceneDataByEntityType.Add(type, entitiesSceneData);
        }

        return entitiesSceneData!;
    }
}