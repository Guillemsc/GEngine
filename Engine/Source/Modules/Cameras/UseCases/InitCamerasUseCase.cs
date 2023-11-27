using GEngine.Modules.Rendering.UseCases;

namespace GEngine.Modules.Cameras.UseCases;

public sealed class InitCamerasUseCase
{
    readonly InitDefaultCamerasUseCase _initDefaultCamerasUseCase;

    public InitCamerasUseCase(
        InitDefaultCamerasUseCase initDefaultCamerasUseCase
        )
    {
        _initDefaultCamerasUseCase = initDefaultCamerasUseCase;
    }

    public void Execute()
    {
        _initDefaultCamerasUseCase.Execute();
    }
}