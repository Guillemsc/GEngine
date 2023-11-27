using System.Numerics;
using GEngine.Modules.Windows.UseCases;

namespace GEngine.Modules.Windows.Interactors;

public sealed class WindowsInteractor : IWindowsInteractor
{
    readonly InitWindowUseCase _initWindowUseCase;
    readonly IsCloseWindowRequestedUseCase _isCloseWindowRequestedUseCase;
    readonly GetWindowSizeUseCase _getWindowSizeUseCase;

    public WindowsInteractor(
        InitWindowUseCase initWindowUseCase, 
        IsCloseWindowRequestedUseCase isCloseWindowRequestedUseCase, 
        GetWindowSizeUseCase getWindowSizeUseCase
        )
    {
        _initWindowUseCase = initWindowUseCase;
        _isCloseWindowRequestedUseCase = isCloseWindowRequestedUseCase;
        _getWindowSizeUseCase = getWindowSizeUseCase;
    }

    public void ShowWindow(int width, int height, string title) => _initWindowUseCase.Execute(width, height, title);
    public bool IsCloseWindowRequested() => _isCloseWindowRequestedUseCase.Execute();
    public Vector2 GetScreenSize() => _getWindowSizeUseCase.Execute();
}