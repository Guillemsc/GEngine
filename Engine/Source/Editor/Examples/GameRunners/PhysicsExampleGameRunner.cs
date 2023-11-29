using System.Numerics;
using Box2D.NetStandard.Dynamics.Bodies;
using GEngine.Core.Interactors;
using GEngine.Modules.Components.Objects;
using GEngine.Modules.Entities.Objects;
using GEngine.Modules.Games.Core;
using GEngine.Modules.Physics2d.Enums;
using GEngine.Modules.Physics2d.Objects;
using GEngine.Utils.Randomization.Generators;
using GEngine.Utils.Time.Timers;
using Raylib_cs;
using GEngine.Utils.Logging.Enums;

namespace GEngine.Editor.Examples.GameRunners;

public sealed class PhysicsExampleGameRunner : GameRunner
{
    readonly IRandomGenerator _randomGenerator = new SeedRandomGenerator(1);
    readonly ITimer _spawnTimer = StopwatchTimer.FromStarted();
    
    WorldEntity? _entity1;
    WorldEntity? _entity2;
    WorldEntity? _entity3;
    WorldEntity? _entity4;

    PhysicsBoxFixture2d _physicsFixture2d;
    
    public PhysicsExampleGameRunner(IREngineInteractor engine) : base(engine)
    {
    }

    public override void Start()
    {
        _entity1 = Entities.CreateWorld("1");
        _entity2 = Entities.CreateWorld("2");
        _entity3 = Entities.CreateWorld("3");
        _entity4 = Entities.CreateWorld("4");
        
        PhysicsBody2dComponent pb1 = _entity1.AddComponent<PhysicsBody2dComponent>();
        pb1.CreateBox(new Vector2(100, 100));
        
        BoxRenderer2dComponent br1 = _entity1.AddComponent<BoxRenderer2dComponent>();
        br1.SetSize(new Vector2(100, 100));
        
        _entity1.Transform.SetLocalScaleY(2f);
        
        _entity2.Transform.SetWorldPositionXY(new Vector2(0, -130));
        
        PhysicsBody2dComponent pb2 = _entity2.AddComponent<PhysicsBody2dComponent>();
        pb2.SetType(PhysicsBody2dType.Kinematic);
        _physicsFixture2d = pb2.CreateBox(new Vector2(100, 20));
        
        BoxRenderer2dComponent br2 = _entity2.AddComponent<BoxRenderer2dComponent>();
        br2.SetSize(new Vector2(100, 20));
        br2.SetColor(Color.GOLD);

        
        _entity3.Transform.SetWorldPositionXY(new Vector2(0, -110));
        
        PhysicsBody2dComponent pb3 = _entity3.AddComponent<PhysicsBody2dComponent>();
        _physicsFixture2d = pb3.CreateBox(new Vector2(10, 10));
        
        BoxRenderer2dComponent br3 = _entity3.AddComponent<BoxRenderer2dComponent>();
        br3.SetSize(new Vector2(10, 10));
        
        _entity4.Transform.SetWorldPositionXY(new Vector2(0, 110));
        
        PhysicsBody2dComponent pb4 = _entity4.AddComponent<PhysicsBody2dComponent>();
        pb4.CreateCircle(30);
        
        CircleRenderer2dComponent cr4 = _entity4.AddComponent<CircleRenderer2dComponent>();
        cr4.SetRadius(30);
    }

    public override void Tick()
    {
        Vector2 size = Vector2.Zero;
            
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) size.Y += 1;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) size.Y -= 1;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) size.X -= 1;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) size.X += 1;

        if (size != Vector2.Zero)
        {
            _physicsFixture2d.SetSize(_physicsFixture2d.Size + size);   
        }
        
        // if (_spawnTimer.HasReached(0.1f.ToSeconds()))
        // {
        //     _spawnTimer.Restart();
        //     
        //     float randomPosition = _randomGenerator.NewFloat(-250, 250);
        //     int shapeType = _randomGenerator.NewInt(0, 2);
        //     
        //     EntityData entityData = EntitiesOld.Create("Entity");
        //     Transforms.SetWorldPosition(entityData, new Vector3(randomPosition, 300, 0));
        //     Components.Add<PhysicsBody2dComponentOld>(entityData);
        //
        //     if (shapeType == 0)
        //     {
        //         Components.Add<CircleCollider2dComponentOld>(entityData);
        //         Components.Add<CircleRenderer2dComponentOld>(entityData);
        //     }
        //     else
        //     {
        //         Components.Add<BoxCollider2dComponentOld>(entityData);
        //         Components.Add<BoxRenderer2dComponentOld>(entityData);
        //     }
        // }
    }

    public override void Dispose()
    {
        
    }
}