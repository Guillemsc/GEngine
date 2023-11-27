using GEngine.Modules.Cameras.Data;
using GEngine.Modules.Cameras.Objects;

namespace GEngine.Modules.Cameras.UseCases;

public sealed class SetActiveCamera2dUseCase
{
    readonly ActiveCamerasData _activeCamerasData;

    public SetActiveCamera2dUseCase(
        ActiveCamerasData activeCamerasData
        )
    {
        _activeCamerasData = activeCamerasData;
    }

    public void Execute(Camera2d camera2d)
    {
        _activeCamerasData.Active2dCamera = camera2d;
    }
}