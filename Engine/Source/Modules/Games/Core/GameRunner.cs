using GEngine.Core.Interactors;
using GEngine.Modules.Entities.Interactors;
using GEngine.Modules.Framerate.Interactors;
using GEngine.Utils.Starting.Startables;
using GEngine.Utils.Tick.Tickables;

namespace GEngine.Modules.Games.Core;

public abstract class GameRunner : IStartable, ITickable, IDisposable
{
    protected IREngineInteractor Engine { get; }
    
    protected IFramerateInteractor Framerate => Engine.Framerate;
    protected IEntitiesInteractor Entities => Engine.Entities;
    
    protected GameRunner(IREngineInteractor engine)
    {
        Engine = engine;
    }
    
    public abstract void Start();
    public abstract void Tick();
    public abstract void Dispose();
}