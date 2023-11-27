using System.Numerics;
using Box2D.NetStandard.Collision.Shapes;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Dynamics.Fixtures;
using GEngine.Modules.Physics2d.UseCases;
using GEngine.Utils.Extensions;
using Raylib_cs;

namespace GEngine.Modules.GuizmoRenderer2d.UseCases;

public sealed class DrawAllPhysicsWithGuizmosUseCase
{
    readonly GetAllPhysicsBodies2dUseCase _getAllPhysicsBodies2dUseCase;
    readonly GetAllPhysicsBody2dFixturesUseCase _getAllPhysicsBody2dFixturesUseCase;
    readonly AddToGuizmoRendering2dQueueUseCase _addToGuizmoRendering2dQueueUseCase;

    public DrawAllPhysicsWithGuizmosUseCase(
        GetAllPhysicsBodies2dUseCase getAllPhysicsBodies2dUseCase,
        GetAllPhysicsBody2dFixturesUseCase getAllPhysicsBody2dFixturesUseCase,
        AddToGuizmoRendering2dQueueUseCase addToGuizmoRendering2dQueueUseCase
    )
    {
        _getAllPhysicsBodies2dUseCase = getAllPhysicsBodies2dUseCase;
        _getAllPhysicsBody2dFixturesUseCase = getAllPhysicsBody2dFixturesUseCase;
        _addToGuizmoRendering2dQueueUseCase = addToGuizmoRendering2dQueueUseCase;
    }

    public void Execute()
    {
        void Render()
        {
            IEnumerable<Body> bodies = _getAllPhysicsBodies2dUseCase.Execute();
            
            foreach (Body body in bodies)
            {
                IEnumerable<Fixture> fixtures = _getAllPhysicsBody2dFixturesUseCase.Execute(body);
            
                foreach (Fixture fixture in fixtures)
                {
                    switch (fixture.Shape)
                    {
                        case ChainShape chainShape:
                        {
                            break;
                        }
                        case CircleShape circleShape:
                        {
                            DrawCircleShape(body, circleShape);
                            break;
                        }
                        case EdgeShape edgeShape:
                        {
                            break;
                        }
                        case PolygonShape polygonShape:
                        {
                            DrawPoligonShape(body, polygonShape);
                            break;
                        }
                    }
                }
            } 
        }
        
        _addToGuizmoRendering2dQueueUseCase.Execute(Render);
    }
    
    void DrawCircleShape(Body body, CircleShape circleShape)
    {
        Vector2 center = body.GetWorldPoint(circleShape.Center);
        float radius = circleShape.Radius;
        
        Raylib.DrawCircleLines(
            (int)center.X,
            (int)-center.Y,
            (int)radius,
            Color.GREEN
        );

        float bodyAngleDegrees = body.GetAngle().ToDegrees() + 90f;
        Vector2 direction = MathExtensions.GetDirectionFromAngle(bodyAngleDegrees);

        Vector2 line = direction * radius;
        
        Raylib.DrawLine(
            (int)center.X ,
            (int)-center.Y,
            (int)center.X + (int)line.X,
            (int)-center.Y - (int)line.Y,
            Color.GREEN
        ); 
    }

    void DrawPoligonShape(Body body, PolygonShape polygonShape)
    {
        Vector2[] vertices = polygonShape.GetVertices();
        
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector2 currentVertice = vertices[i];
            Vector2 nextVertice = i == vertices.Length - 1 ? vertices[0] : vertices[i + 1];
            
            Vector2 currentVerticeWorld = body.GetWorldPoint(currentVertice);
            Vector2 nextVerticeWorld = body.GetWorldPoint(nextVertice);
            
            Raylib.DrawLine(
                (int)currentVerticeWorld.X ,
                (int)-currentVerticeWorld.Y,
                (int)nextVerticeWorld.X,
                (int)-nextVerticeWorld.Y,
                Color.GREEN
            ); 
        }
    }
}