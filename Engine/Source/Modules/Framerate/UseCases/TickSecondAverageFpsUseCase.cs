using GEngine.Modules.Framerate.Data;
using GEngine.Utils.Extensions;

namespace GEngine.Modules.Framerate.UseCases;

public sealed class TickSecondAverageFpsUseCase
{
    readonly SecondAverageFpsData _secondAverageFpsData;

    public TickSecondAverageFpsUseCase(
        SecondAverageFpsData secondAverageFpsData
        )
    {
        _secondAverageFpsData = secondAverageFpsData;
    }

    public void Execute()
    {
        _secondAverageFpsData.CurrentSecondTimer.Start();

        _secondAverageFpsData.CurrentSecondFrames += 1;

        if (_secondAverageFpsData.FirstSecond)
        {
            _secondAverageFpsData.PreviousSecondAverageFps += 1;
        }
        
        double milliseconds = _secondAverageFpsData.CurrentSecondTimer.Time.TotalMilliseconds;

        if (milliseconds < 1000)
        {
            return;
        }

        _secondAverageFpsData.FirstSecond = false;
        
        _secondAverageFpsData.PreviousSecondAverageFps = _secondAverageFpsData.CurrentSecondFrames;
        _secondAverageFpsData.CurrentSecondFrames = 0;

        double millisecondsOffset = milliseconds - 1000;
            
        _secondAverageFpsData.CurrentSecondTimer.Restart();
        _secondAverageFpsData.CurrentSecondTimer.Add(millisecondsOffset.ToMilliseconds());
    }
}