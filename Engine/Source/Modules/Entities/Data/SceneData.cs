using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.Data;

public sealed class SceneData
{
    public Dictionary<Type, EntitiesSceneData> SceneDataByEntityType { get; } = new();
}