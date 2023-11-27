using System.Numerics;
using Box2D.NetStandard.Collision.Shapes;

namespace GEngine.Modules.Physics2d.Objects;

public sealed class PhysicsCircleFixture2d : PhysicsFixture2d
{
    public Vector2 Offset { get; private set; }
    public float Radius { get; private set; }
    
    public PhysicsCircleFixture2d(float radius, bool isSensor) : base(isSensor)
    {
        Radius = radius;
    }

    public void SetRadius(float radius)
    {
        Radius = radius;
        
        Rebuild();
    }
    
    public void SetOffset(Vector2 position)
    {
        Offset = position;

        Rebuild();
    }

    protected override Shape CreateShape()
    {
        return new CircleShape();
    }

    protected override void BuildShape(Shape shape)
    {
        CircleShape polygonShape = (CircleShape)shape;
        
        float maxScaleComponent = Math.Max(Scale.X, Scale.Y);
        float finalRadius = Radius * maxScaleComponent;
        
        polygonShape.Set(Offset, finalRadius);
    }
}