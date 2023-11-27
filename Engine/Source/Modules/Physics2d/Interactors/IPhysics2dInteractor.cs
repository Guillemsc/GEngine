using Box2D.NetStandard.Dynamics.Joints;
using GEngine.Modules.Physics2d.Objects;

namespace GEngine.Modules.Physics2d.Interactors;

public interface IPhysics2dInteractor
{
    PhysicsBody2d CreatePhysicsBody2d(object owner);
    void DestroyPhysicsBody2d(PhysicsBody2d physicsBody2d);

    PhysicsDistanceJoint2d CreateDistanceJoint(PhysicsBody2d physicsBody1, PhysicsBody2d physicsBody2);
}