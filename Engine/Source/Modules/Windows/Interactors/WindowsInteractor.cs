using System.Numerics;
using GEngine.Modules.Windows.Data;
using GEngine.Modules.Windows.UseCases;
using GEngine.Utils.Events;

namespace GEngine.Modules.Windows.Interactors;

public sealed class WindowsInteractor : IWindowsInteractor
{
    readonly WindowEventsData _windowEventsData;
    readonly IsCloseWindowRequestedUseCase _isCloseWindowRequestedUseCase;
    readonly GetWindowSizeUseCase _getWindowSizeUseCase;

    public IListenEvent<Vector2> WindowSizeChangedEvent => _windowEventsData.WindowSizeChangedEvent;
    
    public WindowsInteractor(
        WindowEventsData windowEventsData,
        IsCloseWindowRequestedUseCase isCloseWindowRequestedUseCase, 
        GetWindowSizeUseCase getWindowSizeUseCase)
    {
        _windowEventsData = windowEventsData;
        _isCloseWindowRequestedUseCase = isCloseWindowRequestedUseCase;
        _getWindowSizeUseCase = getWindowSizeUseCase;
    }
    
    public bool IsCloseWindowRequested() => _isCloseWindowRequestedUseCase.Execute();
    public Vector2 GetScreenSize() => _getWindowSizeUseCase.Execute();
}