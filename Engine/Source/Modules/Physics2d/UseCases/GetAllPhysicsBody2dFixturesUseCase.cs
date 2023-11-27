using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Dynamics.Fixtures;

namespace GEngine.Modules.Physics2d.UseCases;

public sealed class GetAllPhysicsBody2dFixturesUseCase
{
    public IEnumerable<Fixture> Execute(Body body)
    {
        Fixture fixture = body.GetFixtureList();

        while (fixture != null)
        {
            yield return fixture;
            
            fixture = fixture.GetNext();
        }
    }
}