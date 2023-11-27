using GEngine.Core.Interactors;
using GEngine.Modules.Entities.Objects;
using GEngine.Utils.Tick.Tickables;

namespace GEngine.Modules.Components.Objects;

public abstract class Component : ITickable, IDisposable
{
    protected IREngineInteractor Engine { get; }
    public Guid Uid { get; }
    public Entity Owner { get; }
    
    protected Component(IREngineInteractor engine, Guid uid, Entity owner)
    {
        Engine = engine;
        Owner = owner;
        Uid = uid;
    }

    public virtual void Init() {}
    public virtual void Enable() {}
    public virtual void Tick() {}
    public virtual void Disable() {}
    public virtual void Dispose() {}
}