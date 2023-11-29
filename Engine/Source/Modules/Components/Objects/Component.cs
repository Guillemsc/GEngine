using GEngine.Core.Interactors;
using GEngine.Utils.Tick.Tickables;

namespace GEngine.Modules.Components.Objects;

public abstract class Component : ITickable, IDisposable
{
    protected IREngineInteractor Engine { get; }
    public Guid Uid { get; }
    
    protected Component(IREngineInteractor engine, Guid uid)
    {
        Engine = engine;
        Uid = uid;
    }

    public virtual void Init() {}
    public virtual void Enable() {}
    public virtual void Tick() {}
    public virtual void Disable() {}
    public virtual void Dispose() {}
    public virtual void ParentChanged() {}
    public virtual void ChildsChanged() {}
}