using GEngine.Modules.Cameras.Interactors;
using GEngine.Modules.Entities.Interactors;
using GEngine.Modules.Framerate.Interactors;
using GEngine.Modules.Games.Interactors;
using GEngine.Modules.GuizmoRenderer2d.Interactors;
using GEngine.Modules.Logging.Interactors;
using GEngine.Modules.Modes.Interactors;
using GEngine.Modules.Physics2d.Interactors;
using GEngine.Modules.Rendering.Interactor;
using GEngine.Modules.Tickables.Interactors;
using GEngine.Modules.Windows.Interactors;

namespace GEngine.Core.Interactors;

public interface IREngineInteractor 
{
    public IModesInteractor Modes { get; }
    public ILoggingInteractor Logging { get; }
    public IWindowsInteractor Windows { get; }
    public IFramerateInteractor Framerate { get; }
    public ICamerasInteractor Cameras { get; }
    public IRenderingInteractor Rendering { get; }
    public ITickablesInteractor Tickables { get; }
    public IEntitiesInteractor Entities { get; }
    public IPhysics2dInteractor Physics2d { get; }
    public IGuizmoRenderer2dInteractor GuizmoRenderer2d { get; }
    
    public IGamesInteractor Games { get; }
}