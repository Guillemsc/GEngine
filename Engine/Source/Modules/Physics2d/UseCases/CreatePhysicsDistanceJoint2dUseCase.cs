using Box2D.NetStandard.Dynamics.Joints;
using Box2D.NetStandard.Dynamics.Joints.Distance;
using GEngine.Modules.Physics2d.Data;
using GEngine.Modules.Physics2d.Objects;

namespace GEngine.Modules.Physics2d.UseCases;

public sealed class CreatePhysicsDistanceJoint2dUseCase
{
    readonly PhysicsWorld2dData _physicsWorld2dData;

    public CreatePhysicsDistanceJoint2dUseCase(
        PhysicsWorld2dData physicsWorld2dData
        )
    {
        _physicsWorld2dData = physicsWorld2dData;
    }

    public PhysicsDistanceJoint2d Execute(PhysicsBody2d physicsBody1, PhysicsBody2d physicsBody2)
    {
        DistanceJointDef distanceJointDef = new();
        
        distanceJointDef.Initialize(
            physicsBody1.Body,
            physicsBody2.Body,
            physicsBody1.GetPosition(),
            physicsBody2.GetPosition()
            );
        
        DistanceJoint distanceJoint = (DistanceJoint)_physicsWorld2dData.World!.CreateJoint(distanceJointDef);

        return new PhysicsDistanceJoint2d(distanceJoint);
    }
}