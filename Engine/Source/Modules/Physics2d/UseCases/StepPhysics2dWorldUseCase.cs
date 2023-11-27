using GEngine.Modules.Physics2d.Data;
using GEngine.Utils.Extensions;

namespace GEngine.Modules.Physics2d.UseCases;

public sealed class StepPhysics2dWorldUseCase
{
    readonly PhysicsWorld2dData _physicsWorld2dData;
    readonly PhysicsStep2dData _physicsStep2dData;

    public StepPhysics2dWorldUseCase(
        PhysicsWorld2dData physicsWorld2dData,
        PhysicsStep2dData physicsStep2dData
    )
    {
        _physicsWorld2dData = physicsWorld2dData;
        _physicsStep2dData = physicsStep2dData;
    }
    
    public void Execute()
    {
        _physicsStep2dData.StepedThisFrame = false;
        
        int velocityIterations = 6;
        int positionIterations = 2;
        
        _physicsStep2dData.TimeStepTimer.Start();

        bool canStep = _physicsStep2dData.TimeStepTimer.Time.TotalSeconds >=
                       _physicsStep2dData.TargetTimeStepSeconds;

        if (!canStep)
        {
            return;
        }

        float carryOverSeconds = (float)_physicsStep2dData.TimeStepTimer.Time.TotalSeconds 
                                 - _physicsStep2dData.TargetTimeStepSeconds;
        
        _physicsStep2dData.TimeStepTimer.Restart();
        _physicsStep2dData.TimeStepTimer.Add(carryOverSeconds.ToSeconds());

        _physicsWorld2dData.World!.Step(
            _physicsStep2dData.TargetTimeStepSeconds,
            velocityIterations,
            positionIterations
        );
        
        _physicsStep2dData.StepedThisFrame = true;
    }
}