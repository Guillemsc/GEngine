using System.Numerics;
using Box2D.NetStandard.Collision.Shapes;
using Box2D.NetStandard.Dynamics.Fixtures;
using GEngine.Utils.Optionals;

namespace GEngine.Modules.Physics2d.Objects;

public abstract class PhysicsFixture2d
{
    Optional<Fixture> _fixture;
    
    public bool IsSensor { get; }
    public Vector2 Scale { get; private set; } = Vector2.One;
    
    public PhysicsFixture2d(bool isSensor)
    {
        IsSensor = isSensor;
    }
    
    public void Add(PhysicsBody2d physicsBody)
    {
        if (_fixture.HasValue)
        {
            return;
        }
        
        Shape shape = CreateShape();
        
        BuildShape(shape);
        
        FixtureDef fixtureDefinition = new()
        {
            shape = shape,
            density = 1,
            isSensor = IsSensor,
        };
        
        _fixture = physicsBody.Body.CreateFixture(fixtureDefinition);
    }
    
    public void Remove(PhysicsBody2d physicsBody)
    {
        bool hasFixture = _fixture.TryGet(out Fixture createdFixture);

        if (!hasFixture)
        {
            return;
        }
        
        physicsBody.Body.DestroyFixture(createdFixture);
        _fixture = Optional<Fixture>.None;
    }

    public void SetScale(Vector2 scale)
    {
        Scale = scale;

        Rebuild();
    }
    
    protected void Rebuild()
    {
        bool hasFixture = _fixture.TryGet(out Fixture createdFixture);

        if (!hasFixture)
        {
            return;
        }

        BuildShape(createdFixture.Shape);
    }

    protected abstract Shape CreateShape();
    protected abstract void BuildShape(Shape shape);
}