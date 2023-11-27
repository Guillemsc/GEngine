using GEngine.Modules.Cameras.Data;
using GEngine.Modules.Cameras.Objects;
using GEngine.Utils.Optionals;

namespace GEngine.Modules.Cameras.UseCases;

public sealed class RemoveActiveCamera2dIfMatchesUseCase
{
    readonly ActiveCamerasData _activeCamerasData;

    public RemoveActiveCamera2dIfMatchesUseCase(
        ActiveCamerasData activeCamerasData
    )
    {
        _activeCamerasData = activeCamerasData;
    }
    
    public void Execute(Camera2d camera2d)
    {
        bool hasCamera = _activeCamerasData.Active2dCamera.TryGet(out Camera2d activeCamera2d);

        if (!hasCamera)
        {
            return;
        }

        bool matches = camera2d == activeCamera2d;

        if (!matches)
        {
            return;
        }
        
        _activeCamerasData.Active2dCamera = Optional<Camera2d>.None;
    }
}