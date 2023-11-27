using GEngine.Modules.Modes.Data;
using GEngine.Modules.Modes.Enums;

namespace GEngine.Modules.Modes.UseCases;

public sealed class SetEngineModeUseCase
{
    readonly ModeData _modeData;

    public SetEngineModeUseCase(ModeData modeData)
    {
        _modeData = modeData;
    }

    public void Execute(EngineModeType engineMode)
    {
        _modeData.EngineModeType = engineMode;
    }
}