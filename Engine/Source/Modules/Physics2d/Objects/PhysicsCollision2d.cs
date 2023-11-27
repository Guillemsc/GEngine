namespace GEngine.Modules.Physics2d.Objects;

public readonly struct PhysicsCollision2d
{
    public PhysicsBody2d PhysicsBody2d1 { get; }
    public PhysicsBody2d PhysicsBody2d2 { get; }
    
    public PhysicsCollision2d(
        PhysicsBody2d physicsBody2d1, 
        PhysicsBody2d physicsBody2d2
        )
    {
        PhysicsBody2d1 = physicsBody2d1;
        PhysicsBody2d2 = physicsBody2d2;
    }
}