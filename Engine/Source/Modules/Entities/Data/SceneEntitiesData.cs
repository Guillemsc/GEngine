using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.Data;

public sealed class SceneEntitiesData
{
    public List<Entity> RootSceneEntities { get; } = new();
    public HashSet<Entity> SceneEntities { get; } = new();
    public HashSet<Entity> SceneActiveEntities { get; } = new();
}