using Box2D.NetStandard.Collision;
using Box2D.NetStandard.Dynamics.Contacts;
using Box2D.NetStandard.Dynamics.World;
using Box2D.NetStandard.Dynamics.World.Callbacks;
using GEngine.Modules.Physics2d.UseCases;

namespace GEngine.Modules.Physics2d.ContactListeners;

public sealed class PhysicsContactListener2d : ContactListener
{
    readonly Physics2dBeginContactUseCase _physics2dBeginContactUseCase;
    readonly Physics2dEndContactUseCase _physics2dEndContactUseCase;

    public PhysicsContactListener2d(
        Physics2dBeginContactUseCase physics2dBeginContactUseCase,
        Physics2dEndContactUseCase physics2dEndContactUseCase
    )
    {
        _physics2dBeginContactUseCase = physics2dBeginContactUseCase;
        _physics2dEndContactUseCase = physics2dEndContactUseCase;
    }

    public override void BeginContact(in Contact contact)
        => _physics2dBeginContactUseCase.Execute(in contact);

    public override void EndContact(in Contact contact)
        => _physics2dEndContactUseCase.Execute(in contact);

    public override void PreSolve(in Contact contact, in Manifold oldManifold) { }
    public override void PostSolve(in Contact contact, in ContactImpulse impulse) { }
}