namespace GEngine.Modules.Physics2d.Objects;

public readonly struct PhysicsContact2d
{
    public PhysicsBody2d PhysicsBody2dA { get; }
    public PhysicsBody2d PhysicsBody2dB { get; }
    
    public PhysicsContact2d(
        PhysicsBody2d physicsBody2dA, 
        PhysicsBody2d physicsBody2dB
        )
    {
        PhysicsBody2dA = physicsBody2dA;
        PhysicsBody2dB = physicsBody2dB;
    }

    public override int GetHashCode()
    {
        return PhysicsBody2dA.GetHashCode() + PhysicsBody2dB.GetHashCode();
    }
}