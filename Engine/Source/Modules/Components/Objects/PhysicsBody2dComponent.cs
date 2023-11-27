using System.Numerics;
using GEngine.Core.Interactors;
using GEngine.Modules.Components.Interfaces;
using GEngine.Modules.Entities.Objects;
using GEngine.Modules.Physics2d.Enums;
using GEngine.Modules.Physics2d.Objects;
using GEngine.Utils.Optionals;
using GEngine.Utils.Extensions;

namespace GEngine.Modules.Components.Objects;

public sealed class PhysicsBody2dComponent : Component, INotifyTransformChanged
{
    readonly List<PhysicsFixture2d> _fixtures = new();
    
    public Optional<PhysicsBody2d> PhysicsBody2d { get; private set; }

    public PhysicsBody2dType BodyType { get; private set; } = PhysicsBody2dType.Dynamic;
    public float GravityScale { get; private set; } = 1f;
    
    public event Action<PhysicsBody2dComponent, PhysicsBody2dComponent>? OnCollisionBegin;
    public event Action<PhysicsBody2dComponent, PhysicsBody2dComponent>? OnCollisionStay;
    public event Action<PhysicsBody2dComponent, PhysicsBody2dComponent>? OnCollisionEnd;
    
    public PhysicsBody2dComponent(IREngineInteractor engine, Guid uid, Entity owner) : base(engine, uid, owner)
    {
    }

    public override void Init()
    {
        PhysicsBody2d physicsBody2d = Engine.Physics2d.CreatePhysicsBody2d(this);
        physicsBody2d.SetActive(false);
        
        PhysicsBody2d = physicsBody2d;
        
        ApplyPropertiesToBody();

        foreach (PhysicsFixture2d fixture in _fixtures)
        {
            fixture.Add(physicsBody2d);
        }
    }

    public override void Enable()
    {
        PhysicsBody2d physicsBody2d = PhysicsBody2d.UnsafeGet();
        
        physicsBody2d.OnCollisionBegin += WhenCollisionBegin;
        physicsBody2d.OnCollisionStay += WhenCollisionStay;
        physicsBody2d.OnCollisionEnd += WhenCollisionEnd;
        
        PhysicsBody2d.UnsafeGet().SetActive(true);
        
        SetTransformToBody();
    }

    public override void Tick()
    {
        if (BodyType == PhysicsBody2dType.Dynamic)
        {
            SetBodyToTransfrom();
        }
        else
        {
            SetTransformToBody();
        }
    }

    public override void Disable()
    {
        PhysicsBody2d physicsBody2d = PhysicsBody2d.UnsafeGet();
        
        physicsBody2d.OnCollisionBegin -= WhenCollisionBegin;
        physicsBody2d.OnCollisionStay -= WhenCollisionStay;
        physicsBody2d.OnCollisionEnd -= WhenCollisionEnd;
        
        physicsBody2d.SetActive(false);
    }

    public override void Dispose()
    {
        PhysicsBody2d physicsBody2d = PhysicsBody2d.UnsafeGet();
        
        foreach (PhysicsFixture2d fixture in _fixtures)
        {
            fixture.Remove(physicsBody2d);
        }
        
        Engine.Physics2d.DestroyPhysicsBody2d(physicsBody2d);
        PhysicsBody2d = Optional<PhysicsBody2d>.None;
    }

    public void SetType(PhysicsBody2dType bodyType)
    {
        BodyType = bodyType;
        
        bool hasPhysicsBody = PhysicsBody2d.TryGet(out PhysicsBody2d physicsBody2d);

        if (!hasPhysicsBody)
        {
            return;
        }

        physicsBody2d.SetBodyType(BodyType);
    }

    public void SetGravityScale(float gravityScale)
    {
        GravityScale = gravityScale;
        
        bool hasPhysicsBody = PhysicsBody2d.TryGet(out PhysicsBody2d physicsBody2d);

        if (!hasPhysicsBody)
        {
            return;
        }

        physicsBody2d.SetGravityScale(GravityScale);
    }

    public PhysicsBoxFixture2d CreateBox(Vector2 size, bool isSensor = false)
    {
        PhysicsBoxFixture2d fixture = new PhysicsBoxFixture2d(size, isSensor);

        AddFixture(fixture);
        
        return fixture;
    }

    public PhysicsCircleFixture2d CreateCircle(float radius, bool isSensor = false)
    {
        PhysicsCircleFixture2d fixture = new PhysicsCircleFixture2d(radius, isSensor);

        AddFixture(fixture);
        
        return fixture;
    }

    public void OnTransformChanged(bool byPhysics)
    {
        if (byPhysics)
        {
            return;
        }

        SetTransformToBody();
    }

    void SetTransformToBody()
    {
        bool hasPhysicsBody = PhysicsBody2d.TryGet(out PhysicsBody2d physicsBody2d);

        if (!hasPhysicsBody)
        {
            return;
        }
        
        Vector2 position = Owner.Transform.WorldPosition.ToVector2XY();
        float angleRadiants = Owner.Transform.WorldRotationRadiants.Z;
        Vector2 scale = Owner.Transform.WorldScale.ToVector2XY();

        physicsBody2d.SetPositionAndAngle(position, angleRadiants);

        foreach (PhysicsFixture2d fixture in _fixtures)
        {
            fixture.SetScale(scale);
        }
    }

    void SetBodyToTransfrom()
    {
        bool hasPhysicsBody = PhysicsBody2d.TryGet(out PhysicsBody2d physicsBody2d);

        if (!hasPhysicsBody)
        {
            return;
        }
        
        Vector2 position = physicsBody2d.Body.GetPosition();
        float angleRadiants = physicsBody2d.Body.GetAngle();
        
        Owner.Transform.SetWorldPositionXYByPhysics(position);
        Owner.Transform.SetWorldRotationRadiantsZByPhysics(angleRadiants);  
    }

    void ApplyPropertiesToBody()
    {
        bool hasPhysicsBody = PhysicsBody2d.TryGet(out PhysicsBody2d physicsBody2d);

        if (!hasPhysicsBody)
        {
            return;
        }

        physicsBody2d.SetBodyType(BodyType);
        physicsBody2d.SetGravityScale(GravityScale);
    }

    void AddFixture(PhysicsFixture2d fixture)
    {
        _fixtures.Add(fixture);
        
        bool hasPhysicsBody = PhysicsBody2d.TryGet(out PhysicsBody2d physicsBody2d);

        if (hasPhysicsBody)
        {
            fixture.Add(physicsBody2d);
        }
        
        Vector2 scale = Owner.Transform.WorldScale.ToVector2XY();
        fixture.SetScale(scale);
    }

    void WhenCollisionBegin(PhysicsCollision2d physicsCollision2d)
    {
        if (physicsCollision2d.PhysicsBody2d1.Owner is not PhysicsBody2dComponent physicsBody2dComponent1)
        {
            return;
        }
        
        if (physicsCollision2d.PhysicsBody2d2.Owner is not PhysicsBody2dComponent physicsBody2dComponent2)
        {
            return;
        }
        
        OnCollisionBegin?.Invoke(physicsBody2dComponent1, physicsBody2dComponent2);   
    }
    
    void WhenCollisionStay(PhysicsCollision2d physicsCollision2d)
    {
        if (physicsCollision2d.PhysicsBody2d1.Owner is not PhysicsBody2dComponent physicsBody2dComponent1)
        {
            return;
        }
        
        if (physicsCollision2d.PhysicsBody2d2.Owner is not PhysicsBody2dComponent physicsBody2dComponent2)
        {
            return;
        }
        
        OnCollisionStay?.Invoke(physicsBody2dComponent1, physicsBody2dComponent2);   
    }
    
    void WhenCollisionEnd(PhysicsCollision2d physicsCollision2d)
    {
        if (physicsCollision2d.PhysicsBody2d1.Owner is not PhysicsBody2dComponent physicsBody2dComponent1)
        {
            return;
        }
        
        if (physicsCollision2d.PhysicsBody2d2.Owner is not PhysicsBody2dComponent physicsBody2dComponent2)
        {
            return;
        }
        
        OnCollisionEnd?.Invoke(physicsBody2dComponent1, physicsBody2dComponent2);   
    }
}