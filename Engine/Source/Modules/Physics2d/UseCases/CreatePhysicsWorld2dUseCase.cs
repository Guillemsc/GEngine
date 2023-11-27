using System.Numerics;
using Box2D.NetStandard.Dynamics.World;
using GEngine.Modules.Physics2d.ContactListeners;
using GEngine.Modules.Physics2d.Data;

namespace GEngine.Modules.Physics2d.UseCases;

public sealed class CreatePhysicsWorld2dUseCase
{
    readonly PhysicsWorld2dData _physicsWorld2dData;
    readonly PhysicsContactListener2d _physicsContactListener2d;

    public CreatePhysicsWorld2dUseCase(
        PhysicsWorld2dData physicsWorld2dData,
        PhysicsContactListener2d physicsContactListener2d
    )
    {
        _physicsWorld2dData = physicsWorld2dData;
        _physicsContactListener2d = physicsContactListener2d;
    }

    public void Execute()
    {
        Vector2 gravity = new Vector2(0.0f, -10.0f);
        _physicsWorld2dData.World = new World(gravity);
        _physicsWorld2dData.World.SetContactListener(_physicsContactListener2d);
    }
}