using GEngine.Modules.Modes.Enums;

namespace GEngine.Modules.Modes.Interactors;

public interface IModesInteractor
{
    void SetEngineMode(EngineModeType engineMode);
}