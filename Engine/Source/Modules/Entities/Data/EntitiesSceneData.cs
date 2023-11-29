using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.Data;

public sealed class EntitiesSceneData
{
    public List<IEntity> RootSceneEntities { get; } = new();
    public HashSet<IEntity> SceneEntities { get; } = new();
    public HashSet<IEntity> SceneActiveEntities { get; } = new();
}