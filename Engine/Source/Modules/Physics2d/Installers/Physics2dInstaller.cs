using GEngine.Modules.Physics2d.ContactListeners;
using GEngine.Modules.Physics2d.Data;
using GEngine.Modules.Physics2d.Interactors;
using GEngine.Modules.Physics2d.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Modules.Physics2d.Installers;

public static class Physics2dInstaller
{
    public static void InstallPhysics2d(this IDiContainerBuilder builder)
    {
        builder.Bind<IPhysics2dInteractor>()
            .FromFunction(c => new Physics2dInteractor(
                c.Resolve<CreatePhysicsBody2dUseCase>(),
                c.Resolve<DestroyPhysicsBody2dUseCase>(),
                c.Resolve<CreatePhysicsDistanceJoint2dUseCase>()
            ));
        
        builder.Bind<PhysicsStep2dData>().FromNew();
        builder.Bind<PhysicsWorld2dData>().FromNew();
        builder.Bind<PhysicsContacts2dData>().FromNew();

        builder.Bind<PhysicsContactListener2d>()
            .FromFunction(c => new PhysicsContactListener2d(
                c.Resolve<Physics2dBeginContactUseCase>(),
                c.Resolve<Physics2dEndContactUseCase>()
            ));
        
        builder.Bind<InitPhysics2dUseCase>()
            .FromFunction(c => new InitPhysics2dUseCase(
                c.Resolve<CreatePhysicsWorld2dUseCase>()
            ));

        builder.Bind<CreatePhysicsWorld2dUseCase>()
            .FromFunction(c => new CreatePhysicsWorld2dUseCase(
                c.Resolve<PhysicsWorld2dData>(),
                c.Resolve<PhysicsContactListener2d>()
            ));

        builder.Bind<TickPhysics2dUseCase>()
            .FromFunction(c => new TickPhysics2dUseCase(
                c.Resolve<StepPhysics2dWorldUseCase>(),
                c.Resolve<ResolvePhysics2dContactCallbacksUseCase>()
            ));

        builder.Bind<StepPhysics2dWorldUseCase>()
            .FromFunction(c => new StepPhysics2dWorldUseCase(
                c.Resolve<PhysicsWorld2dData>(),
                c.Resolve<PhysicsStep2dData>()
            ));
        
        builder.Bind<ResolvePhysics2dContactCallbacksUseCase>()
            .FromFunction(c => new ResolvePhysics2dContactCallbacksUseCase(
                c.Resolve<PhysicsContacts2dData>(),
                c.Resolve<PhysicsStep2dData>()
            ));
        
        builder.Bind<GetAllPhysicsBodies2dUseCase>()
            .FromFunction(c => new GetAllPhysicsBodies2dUseCase(
                c.Resolve<PhysicsWorld2dData>()
            ));

        builder.Bind<GetAllPhysicsBody2dFixturesUseCase>()
            .FromFunction(c => new GetAllPhysicsBody2dFixturesUseCase(
            ));

        builder.Bind<Physics2dBeginContactUseCase>()
            .FromFunction(c => new Physics2dBeginContactUseCase(
                c.Resolve<PhysicsContacts2dData>()
            ));

        builder.Bind<Physics2dEndContactUseCase>()
            .FromFunction(c => new Physics2dEndContactUseCase(
                c.Resolve<PhysicsContacts2dData>()
            ));

        builder.Bind<GetPhysics2dContactsCountUseCase>()
            .FromFunction(c => new GetPhysics2dContactsCountUseCase(
                c.Resolve<PhysicsContacts2dData>()
            ));

        builder.Bind<CreatePhysicsBody2dUseCase>()
            .FromFunction(c => new CreatePhysicsBody2dUseCase(
                c.Resolve<PhysicsWorld2dData>()
            ));

        builder.Bind<DestroyPhysicsBody2dUseCase>()
            .FromFunction(c => new DestroyPhysicsBody2dUseCase(
                c.Resolve<PhysicsWorld2dData>()
            ));

        builder.Bind<CreatePhysicsDistanceJoint2dUseCase>()
            .FromFunction(c => new CreatePhysicsDistanceJoint2dUseCase(
                c.Resolve<PhysicsWorld2dData>()
            ));
    }
}