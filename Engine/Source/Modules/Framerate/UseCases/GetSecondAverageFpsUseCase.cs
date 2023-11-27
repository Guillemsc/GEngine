using GEngine.Modules.Framerate.Data;

namespace GEngine.Modules.Framerate.UseCases;

public sealed class GetSecondAverageFpsUseCase
{
    readonly SecondAverageFpsData _secondAverageFpsData;

    public GetSecondAverageFpsUseCase(
        SecondAverageFpsData secondAverageFpsData
    )
    {
        _secondAverageFpsData = secondAverageFpsData;
    }
    
    public int Execute()
    {
        return _secondAverageFpsData.PreviousSecondAverageFps;
    }
}