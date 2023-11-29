using System.Numerics;
using GEngine.Modules.Cameras.Objects;
using GEngine.Modules.Cameras.UseCases;
using GEngine.Utils.Extensions;

namespace GEngine.Modules.UiRenderer.UseCases;

public sealed class GetUiPositionFromWorldPosition2dUseCase
{
    readonly GetActiveCamera2dOrDefaultUseCase _getActiveCamera2dOrDefaultUseCase;

    public GetUiPositionFromWorldPosition2dUseCase(
        GetActiveCamera2dOrDefaultUseCase getActiveCamera2dOrDefaultUseCase
        )
    {
        _getActiveCamera2dOrDefaultUseCase = getActiveCamera2dOrDefaultUseCase;
    }

    public Vector2 Execute(Vector2 position)
    {
        Camera2d camera2d = _getActiveCamera2dOrDefaultUseCase.Execute();

        Vector2 uiPosition = new Vector2(
            MathExtensions.Divide(position.X, camera2d.Size),
            MathExtensions.Divide(position.Y, camera2d.Size)
        );

        return uiPosition;
    }
}