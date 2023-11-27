using System.Numerics;
using Box2D.NetStandard.Collision.Shapes;

namespace GEngine.Modules.Physics2d.Objects;

public sealed class PhysicsBoxFixture2d : PhysicsFixture2d
{
    public Vector2 Offset { get; private set; }
    public Vector2 Size { get; private set; }
    
    public PhysicsBoxFixture2d(Vector2 size, bool isSensor) : base(isSensor)
    {
        Size = size;
    }

    public void SetSize(Vector2 size)
    {
        Size = size;
        
        Rebuild();
    }
    
    public void SetScale(Vector2 size)
    {
        Size = size;
        
        Rebuild();
    }
    
    public void SetOffset(Vector2 position)
    {
        Offset = position;

        Rebuild();
    }

    protected override Shape CreateShape()
    {
        return new PolygonShape();
    }

    protected override void BuildShape(Shape shape)
    {
        Vector2 finalSize = Size * Scale;
        
        PolygonShape polygonShape = (PolygonShape)shape;
        polygonShape.SetAsBox(
            finalSize.X * 0.5f, finalSize.Y * 0.5f,
            Offset,
            0f
        );
    }
}