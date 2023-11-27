using GEngine.Utils.Time.Timers;

namespace GEngine.Modules.Physics2d.Data;

public sealed class PhysicsStep2dData
{
    public float TargetTimeStepSeconds = 1.0f / 60.0f;
    public ITimer TimeStepTimer = new StopwatchTimer();
    public bool StepedThisFrame;
}