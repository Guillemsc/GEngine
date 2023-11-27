using GEngine.Utils.Time.Timers;

namespace GEngine.Modules.Framerate.Data;

public sealed class SecondAverageFpsData
{
    public bool FirstSecond = true;
    public int PreviousSecondAverageFps;
    public int CurrentSecondFrames;
    public ITimer CurrentSecondTimer = new StopwatchTimer();
}