using Box2D.NetStandard.Dynamics.Joints.Distance;

namespace GEngine.Modules.Physics2d.Objects;

public sealed class PhysicsDistanceJoint2d : PhysicsJoint2d
{
    readonly DistanceJoint _distanceJoint;
    
    public PhysicsDistanceJoint2d(
        DistanceJoint distanceJoint
        )
    {
        _distanceJoint = distanceJoint;
    }

    public PhysicsBody2d GetBodyA()
    {
        return PhysicsBody2d.GetFromBody(_distanceJoint.GetBodyA());
    }
    
    public PhysicsBody2d GetBodyB()
    {
        return PhysicsBody2d.GetFromBody(_distanceJoint.GetBodyB());
    }
}