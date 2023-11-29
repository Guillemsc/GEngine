using System.Numerics;

namespace GEngine.Modules.UiRenderer.Interactors;

public interface IUiRendererInteractor
{
    float GetUiScale();
    void AddToUiRenderingQueue(int sortingOrder, Action<float> action);
    Vector2 GetUiPositionFromScreenPosition(Vector2 position);
    Vector2 GetScreenPositionFromUiPosition(Vector2 position);
    Vector2 GetWorldPosition2dFromUiPosition(Vector2 position);
    Vector2 GetUiPositionFromWorldPosition2d(Vector2 position);
}