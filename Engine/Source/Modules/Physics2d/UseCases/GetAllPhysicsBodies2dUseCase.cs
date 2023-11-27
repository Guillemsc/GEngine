using Box2D.NetStandard.Dynamics.Bodies;
using GEngine.Modules.Physics2d.Data;

namespace GEngine.Modules.Physics2d.UseCases;

public sealed class GetAllPhysicsBodies2dUseCase
{
    readonly PhysicsWorld2dData _physicsWorld2dData;

    public GetAllPhysicsBodies2dUseCase(
        PhysicsWorld2dData physicsWorld2dData
    )
    {
        _physicsWorld2dData = physicsWorld2dData;
    }

    public IEnumerable<Body> Execute()
    {
        Body body = _physicsWorld2dData.World!.GetBodyList();

        while (body != null)
        {
            yield return body;
            
            body = body.GetNext();
        }
    }
}