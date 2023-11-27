using GEngine.Modules.Cameras.Objects;

namespace GEngine.Modules.Cameras.UseCases;

public sealed class SetActiveCamera2dIfThereIsNoneUseCase
{
    readonly HasActiveCamera2dUseCase _hasActiveCamera2dUseCase;
    readonly SetActiveCamera2dUseCase _setActiveCamera2dUseCase;

    public SetActiveCamera2dIfThereIsNoneUseCase(
        HasActiveCamera2dUseCase hasActiveCamera2dUseCase,
        SetActiveCamera2dUseCase setActiveCamera2dUseCase
        )
    {
        _hasActiveCamera2dUseCase = hasActiveCamera2dUseCase;
        _setActiveCamera2dUseCase = setActiveCamera2dUseCase;
    }

    public void Execute(Camera2d camera2d)
    {
        bool hasActiveCamera = _hasActiveCamera2dUseCase.Execute();

        if (hasActiveCamera)
        {
            return;
        }
        
        _setActiveCamera2dUseCase.Execute(camera2d);
    }
}