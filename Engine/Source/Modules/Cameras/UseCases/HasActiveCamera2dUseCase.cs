using GEngine.Modules.Cameras.Data;

namespace GEngine.Modules.Cameras.UseCases;

public sealed class HasActiveCamera2dUseCase
{
    readonly ActiveCamerasData _activeCamerasData;

    public HasActiveCamera2dUseCase(
        ActiveCamerasData activeCamerasData
    )
    {
        _activeCamerasData = activeCamerasData;
    }

    public bool Execute()
    {
        return _activeCamerasData.Active2dCamera.HasValue;
    }
}