using System.Numerics;
using Box2D.NetStandard.Dynamics.Bodies;
using GEngine.Modules.Physics2d.Enums;

namespace GEngine.Modules.Physics2d.Objects;

public sealed class PhysicsBody2d
{
    public Body Body { get; }
    public object Owner { get; }
    
    public bool IsValid { get; private set; } = true;
    
    public Action<PhysicsCollision2d>? OnCollisionBegin;
    public Action<PhysicsCollision2d>? OnCollisionStay;
    public Action<PhysicsCollision2d>? OnCollisionEnd;

    public PhysicsBody2d(Body body, object owner)
    {
        Body = body;
        Owner = owner;
    }

    public static PhysicsBody2d GetFromBody(Body body)
    {
        return body.GetUserData<PhysicsBody2d>();
    }

    public void SetActive(bool set)
    {
        Body.SetEnabled(set);
    }

    public void SetBodyType(PhysicsBody2dType physicsBody2dType)
    {
        BodyType bodyType;
        
        switch (physicsBody2dType)
        {
            case PhysicsBody2dType.Static:
            {
                bodyType = BodyType.Static;
                break;
            }
            case PhysicsBody2dType.Kinematic:
            {
                bodyType = BodyType.Kinematic;
                break;
            }
            case PhysicsBody2dType.Dynamic:
            {
                bodyType = BodyType.Dynamic;
                break;
            }
            default:
            {
                throw new ArgumentOutOfRangeException(nameof(physicsBody2dType), physicsBody2dType, null);
            }
        }
        
        Body.SetType(bodyType);
    }

    public PhysicsBody2dType GetBodyType()
    {
        switch (Body.Type())
        {
            default:
            case BodyType.Static:
            {
                return PhysicsBody2dType.Static;
            }
            case BodyType.Kinematic:
            {
                return PhysicsBody2dType.Kinematic;
            }
            case BodyType.Dynamic:
            {
                return PhysicsBody2dType.Dynamic;
            }
        }
    }
    
    public void SetPositionAndAngle(Vector2 position, float angleRadiants)
    {
        Body.SetTransform(position, angleRadiants);
    }
    
    public Vector2 GetPosition()
    {
        return Body.GetPosition();
    }
    
    public float GetAngleRadiants()
    {
        return Body.GetAngle();
    }

    public void SetGravityScale(float gravityScale)
    {
        Body.SetGravityScale(gravityScale);
    }

    public float GetGravityScale()
    {
        return Body.GetGravityScale();
    }
    
    public void Invalidate()
    {
        IsValid = false;
    }
}