using System.Numerics;
using GEngine.Utils.Events;

namespace GEngine.Modules.Windows.Interactors;

public interface IWindowsInteractor
{
    IListenEvent<Vector2> WindowSizeChangedEvent { get; }
    
    bool IsCloseWindowRequested();
    Vector2 GetScreenSize();
}