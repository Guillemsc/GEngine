using System.Numerics;
using GEngine.Modules.Cameras.Objects;
using GEngine.Modules.Cameras.UseCases;

namespace GEngine.Modules.UiRenderer.UseCases;

public sealed class GetWorldPosition2dFromUiPositionUseCase
{
    readonly GetActiveCamera2dOrDefaultUseCase _getActiveCamera2dOrDefaultUseCase;

    public GetWorldPosition2dFromUiPositionUseCase(
        GetActiveCamera2dOrDefaultUseCase getActiveCamera2dOrDefaultUseCase
        )
    {
        _getActiveCamera2dOrDefaultUseCase = getActiveCamera2dOrDefaultUseCase;
    }

    public Vector2 Execute(Vector2 position)
    {
        Camera2d camera2d = _getActiveCamera2dOrDefaultUseCase.Execute();
        float size = camera2d.Size;

        Vector2 worldPosition = size * position;

        return worldPosition;
    }
}