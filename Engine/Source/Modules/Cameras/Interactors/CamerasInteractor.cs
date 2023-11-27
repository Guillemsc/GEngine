using GEngine.Modules.Cameras.Objects;
using GEngine.Modules.Cameras.UseCases;

namespace GEngine.Modules.Cameras.Interactors;

public sealed class CamerasInteractor : ICamerasInteractor
{
    readonly SetActiveCamera2dUseCase _setActiveCamera2dUseCase;
    readonly HasActiveCamera2dUseCase _hasActiveCamera2dUseCase;
    readonly SetActiveCamera2dIfThereIsNoneUseCase _setActiveCamera2dIfThereIsNoneUseCase;
    readonly RemoveActiveCamera2dIfMatchesUseCase _removeActiveCamera2dIfMatchesUseCase;

    public CamerasInteractor(
        SetActiveCamera2dUseCase setActiveCamera2dUseCase, 
        HasActiveCamera2dUseCase hasActiveCamera2dUseCase, 
        SetActiveCamera2dIfThereIsNoneUseCase setActiveCamera2dIfThereIsNoneUseCase, 
        RemoveActiveCamera2dIfMatchesUseCase removeActiveCamera2dIfMatchesUseCase
        )
    {
        _setActiveCamera2dUseCase = setActiveCamera2dUseCase;
        _hasActiveCamera2dUseCase = hasActiveCamera2dUseCase;
        _setActiveCamera2dIfThereIsNoneUseCase = setActiveCamera2dIfThereIsNoneUseCase;
        _removeActiveCamera2dIfMatchesUseCase = removeActiveCamera2dIfMatchesUseCase;
    }

    public void SetActiveCamera2d(Camera2d camera)
        => _setActiveCamera2dUseCase.Execute(camera);

    public bool HasActiveCamera2d()
        => _hasActiveCamera2dUseCase.Execute();

    public void SetActiveCamera2dIfThereIsNone(Camera2d camera)
        => _setActiveCamera2dIfThereIsNoneUseCase.Execute(camera);

    public void RemoveActiveCamera2dIfMatches(Camera2d camera)
        => _removeActiveCamera2dIfMatchesUseCase.Execute(camera);
}