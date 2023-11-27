using Box2D.NetStandard.Dynamics.Contacts;
using GEngine.Modules.Physics2d.Data;
using GEngine.Modules.Physics2d.Objects;

namespace GEngine.Modules.Physics2d.UseCases;

public sealed class Physics2dEndContactUseCase
{
    readonly PhysicsContacts2dData _physicsContacts2dData;

    public Physics2dEndContactUseCase(
        PhysicsContacts2dData physicsContacts2dData
    )
    {
        _physicsContacts2dData = physicsContacts2dData;
    }
    
    public void Execute(in Contact contact)
    {
        PhysicsBody2d bodyA = (PhysicsBody2d)contact.FixtureA.Body.UserData;
        PhysicsBody2d bodyB = (PhysicsBody2d)contact.FixtureB.Body.UserData;

        _physicsContacts2dData.EndContacts.Add(new PhysicsContact2d(
            bodyA,
            bodyB
        ));
    }
}