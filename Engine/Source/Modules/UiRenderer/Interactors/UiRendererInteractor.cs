using System.Numerics;
using GEngine.Modules.UiRenderer.UseCases;

namespace GEngine.Modules.UiRenderer.Interactors;

public sealed class UiRendererInteractor : IUiRendererInteractor
{
    readonly GetUiScaleUseCase _getUiScaleUseCase;
    readonly AddToUiRenderingQueueUseCase _addToUiRenderingQueueUseCase;
    readonly GetUiPositionFromScreenPositionUseCase _getUiPositionFromScreenPositionUseCase;
    readonly GetScreenPositionFromUiPositionUseCase _getScreenPositionFromUiPositionUseCase;
    readonly GetUiPositionFromWorldPosition2dUseCase _getUiPositionFromWorldPosition2dUseCase;
    readonly GetWorldPosition2dFromUiPositionUseCase _getWorldPosition2dFromUiPositionUseCase;

    public UiRendererInteractor(
        GetUiScaleUseCase getUiScaleUseCase,
        AddToUiRenderingQueueUseCase addToUiRenderingQueueUseCase,
        GetUiPositionFromScreenPositionUseCase getUiPositionFromScreenPositionUseCase,
        GetScreenPositionFromUiPositionUseCase getScreenPositionFromUiPositionUseCase,
        GetUiPositionFromWorldPosition2dUseCase getUiPositionFromWorldPosition2dUseCase, 
        GetWorldPosition2dFromUiPositionUseCase getWorldPosition2dFromUiPositionUseCase
        )
    {
        _getUiScaleUseCase = getUiScaleUseCase;
        _addToUiRenderingQueueUseCase = addToUiRenderingQueueUseCase;
        _getUiPositionFromScreenPositionUseCase = getUiPositionFromScreenPositionUseCase;
        _getScreenPositionFromUiPositionUseCase = getScreenPositionFromUiPositionUseCase;
        _getUiPositionFromWorldPosition2dUseCase = getUiPositionFromWorldPosition2dUseCase;
        _getWorldPosition2dFromUiPositionUseCase = getWorldPosition2dFromUiPositionUseCase;
    }

    public float GetUiScale()
        => _getUiScaleUseCase.Execute();

    public void AddToUiRenderingQueue(int sortingOrder, Action<float> action)
        => _addToUiRenderingQueueUseCase.Execute(sortingOrder, action);

    public Vector2 GetUiPositionFromScreenPosition(Vector2 position)
        => _getUiPositionFromScreenPositionUseCase.Execute(position);

    public Vector2 GetScreenPositionFromUiPosition(Vector2 position)
        => _getScreenPositionFromUiPositionUseCase.Execute(position);

    public Vector2 GetWorldPosition2dFromUiPosition(Vector2 position)
        => _getWorldPosition2dFromUiPositionUseCase.Execute(position);

    public Vector2 GetUiPositionFromWorldPosition2d(Vector2 position)
        => _getUiPositionFromWorldPosition2dUseCase.Execute(position);
}