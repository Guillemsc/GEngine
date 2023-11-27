using GEngine.Modules.Modes.Data;
using GEngine.Modules.Modes.Enums;

namespace GEngine.Modules.Modes.UseCases;

public sealed class GetEngineModeUseCase
{
    readonly ModeData _modeData;

    public GetEngineModeUseCase(ModeData modeData)
    {
        _modeData = modeData;
    }

    public EngineModeType Execute()
    {
        return _modeData.EngineModeType;
    }
}