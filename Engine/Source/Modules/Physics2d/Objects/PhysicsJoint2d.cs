namespace GEngine.Modules.Physics2d.Objects;

public abstract class PhysicsJoint2d
{
    public bool IsValid { get; private set; } = true;

    public void Invalidate()
    {
        IsValid = false;
    }
}