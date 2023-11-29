using System.Numerics;
using GEngine.Modules.Windows.UseCases;

namespace GEngine.Modules.UiRenderer.UseCases;

public sealed class GetScreenPositionFromUiPositionUseCase
{
    readonly GetWindowSizeUseCase _getWindowSizeUseCase;

    public GetScreenPositionFromUiPositionUseCase(
        GetWindowSizeUseCase getWindowSizeUseCase
    )
    {
        _getWindowSizeUseCase = getWindowSizeUseCase;
    }
    
    public Vector2 Execute(Vector2 position)
    {
        Vector2 screenSize = _getWindowSizeUseCase.Execute();
        Vector2 halfScreenSize = screenSize * 0.5f;
        Vector2 screenPosition = position + halfScreenSize;
        return screenPosition;
    }
}