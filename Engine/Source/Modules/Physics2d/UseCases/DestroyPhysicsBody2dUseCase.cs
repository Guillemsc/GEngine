using Box2D.NetStandard.Dynamics.Bodies;
using GEngine.Modules.Physics2d.Data;
using GEngine.Modules.Physics2d.Objects;

namespace GEngine.Modules.Physics2d.UseCases;

public class DestroyPhysicsBody2dUseCase
{
    readonly PhysicsWorld2dData _physics2WorldData;

    public DestroyPhysicsBody2dUseCase(
        PhysicsWorld2dData physics2WorldData
    )
    {
        _physics2WorldData = physics2WorldData;
    }

    public void Execute(PhysicsBody2d physicsBody2d)
    {
        physicsBody2d.Invalidate();
        
        physicsBody2d.OnCollisionBegin = null;
        physicsBody2d.OnCollisionStay = null;
        physicsBody2d.OnCollisionEnd = null;
        
        _physics2WorldData.World!.DestroyBody(physicsBody2d.Body);
    }
}