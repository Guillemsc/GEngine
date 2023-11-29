using GEngine.Core.Interactors;
using GEngine.Modules.Components.Objects;

namespace GEngine.Modules.Entities.Objects;

public sealed class WorldEntity : BaseEntity<WorldComponent>
{
    public TransformComponent Transform { get; }
    
    public WorldEntity(
        IREngineInteractor engine, 
        Guid uid, 
        Action<BaseEntity<WorldComponent>> refreshEntityOnSceneAction, 
        Action<BaseEntity<WorldComponent>> refreshEntityOnActiveEntitiesListsAction) 
        : base(engine, uid, refreshEntityOnSceneAction, refreshEntityOnActiveEntitiesListsAction)
    {
        Transform = AddComponent<TransformComponent>();
    }
}