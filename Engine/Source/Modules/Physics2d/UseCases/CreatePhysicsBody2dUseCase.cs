using System.Numerics;
using Box2D.NetStandard.Dynamics.Bodies;
using GEngine.Modules.Physics2d.Data;
using GEngine.Modules.Physics2d.Objects;

namespace GEngine.Modules.Physics2d.UseCases;

public sealed class CreatePhysicsBody2dUseCase
{
    readonly PhysicsWorld2dData _physics2WorldData;

    public CreatePhysicsBody2dUseCase(
        PhysicsWorld2dData physics2WorldData
    )
    {
        _physics2WorldData = physics2WorldData;
    }

    public PhysicsBody2d Execute(object owner)
    {
        BodyDef bodyDef = new BodyDef
        {
            type = BodyType.Dynamic,
            position = Vector2.Zero,
            gravityScale = 2,
            allowSleep = false,
        };
        
        Body body = _physics2WorldData.World!.CreateBody(bodyDef);

        PhysicsBody2d physicsBody2d = new(body, owner);

        body.SetUserData(physicsBody2d);

        return physicsBody2d;
    }
}