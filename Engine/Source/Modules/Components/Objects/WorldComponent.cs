using GEngine.Core.Interactors;
using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Components.Objects;

public abstract class WorldComponent : Component
{
    public WorldEntity Owner { get; }
    
    protected WorldComponent(IREngineInteractor engine, Guid uid, BaseEntity<WorldComponent> owner) : base(engine, uid)
    {
        Owner = (WorldEntity)owner;
    }
}