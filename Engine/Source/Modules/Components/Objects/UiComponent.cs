using GEngine.Core.Interactors;
using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Components.Objects;

public abstract class UiComponent : Component
{
    public UiEntity Owner { get; }
    
    protected UiComponent(IREngineInteractor engine, Guid uid, BaseEntity<UiComponent> owner) : base(engine, uid)
    {
        Owner = (UiEntity)owner;
    }
}