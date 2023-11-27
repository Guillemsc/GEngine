using Box2D.NetStandard.Dynamics.Joints;
using GEngine.Modules.Physics2d.Objects;
using GEngine.Modules.Physics2d.UseCases;

namespace GEngine.Modules.Physics2d.Interactors;

public sealed class Physics2dInteractor : IPhysics2dInteractor
{
    readonly CreatePhysicsBody2dUseCase _createPhysicsBody2dUseCase;
    readonly DestroyPhysicsBody2dUseCase _destroyPhysicsBody2dUseCase;
    readonly CreatePhysicsDistanceJoint2dUseCase _createPhysicsDistanceJoint2dUseCase;

    public Physics2dInteractor(
        CreatePhysicsBody2dUseCase createPhysicsBody2dUseCase, 
        DestroyPhysicsBody2dUseCase destroyPhysicsBody2dUseCase, 
        CreatePhysicsDistanceJoint2dUseCase createPhysicsDistanceJoint2dUseCase
        )
    {
        _createPhysicsBody2dUseCase = createPhysicsBody2dUseCase;
        _destroyPhysicsBody2dUseCase = destroyPhysicsBody2dUseCase;
        _createPhysicsDistanceJoint2dUseCase = createPhysicsDistanceJoint2dUseCase;
    }

    public PhysicsBody2d CreatePhysicsBody2d(object owner)
        => _createPhysicsBody2dUseCase.Execute(owner);

    public void DestroyPhysicsBody2d(PhysicsBody2d physicsBody2d)
        => _destroyPhysicsBody2dUseCase.Execute(physicsBody2d);

    public PhysicsDistanceJoint2d CreateDistanceJoint(PhysicsBody2d physicsBody1, PhysicsBody2d physicsBody2)
        => _createPhysicsDistanceJoint2dUseCase.Execute(physicsBody1, physicsBody2);
}