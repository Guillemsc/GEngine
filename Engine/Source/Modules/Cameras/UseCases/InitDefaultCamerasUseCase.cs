using GEngine.Modules.Cameras.Data;
using GEngine.Modules.Cameras.Objects;

namespace GEngine.Modules.Cameras.UseCases;

public sealed class InitDefaultCamerasUseCase
{
    readonly ActiveCamerasData _activeCamerasData;

    public InitDefaultCamerasUseCase(
        ActiveCamerasData activeCamerasData
        )
    {
        _activeCamerasData = activeCamerasData;
    }

    public void Execute()
    {
        _activeCamerasData.DefaultCamera2d = new Camera2d();
    }
}