using GEngine.Modules.Modes.Enums;
using GEngine.Modules.Modes.UseCases;

namespace GEngine.Modules.Modes.Interactors;

public sealed class ModesInteractor : IModesInteractor
{
    readonly SetEngineModeUseCase _setEngineModeUseCase;

    public ModesInteractor(
        SetEngineModeUseCase setEngineModeUseCase
        )
    {
        _setEngineModeUseCase = setEngineModeUseCase;
    }

    public void SetEngineMode(EngineModeType engineMode)
        => _setEngineModeUseCase.Execute(engineMode);
}